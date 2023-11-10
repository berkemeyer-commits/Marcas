using System;
using System.Data;
using Berke.Libs.Base.Helpers;
using Berke.Libs.Base;
using Berke.Libs.Boletin.Services;
using Berke.Libs.Base.DSHelpers;
using System.Collections;

namespace Berke.Marcas.BizActions.Reports
{
	/// <summary>
	/// Generación del documento de Actividad de Cliente
	/// Autor: Marcos Báez
	/// </summary>
	/// 
	public class ClientActivityDoc
	{
		#region Atribute
		Berke.Libs.Base.Helpers.AccesoDB db;
		DataTable dt;
		Berke.Libs.CodeGenerator cgDoc;
		Berke.Libs.CodeGenerator cgClient;
		Berke.Libs.CodeGenerator cgProp;
		Berke.Libs.CodeGenerator cgTabla;
		Berke.Libs.CodeGenerator cgTablaTit;
		Berke.Libs.CodeGenerator cgTablaHead;
		Berke.Libs.CodeGenerator cgTablaRow;
		Berke.Libs.CodeGenerator cgResumen;
		Berke.Libs.CodeGenerator cgAgentes;

		ClienteService cliServ;
		
		string plantilla;
		DateTime fechainf;
		DateTime fechasup;
		int nNuestras  = 0;
		int nVigiladas = 0;
		bool show_nuestras;
		bool show_terceros;
		bool show_resumen;
		bool show_agentes;
		int MAX_ROWS = 10000;

		#endregion Atribute

		#region Constructors
		public ClientActivityDoc(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db   = db;
			plantilla = ReportUtils.loadTemplate(db,Berke.Libs.Base.PlantillaTipo.TMPL_REPORT_CLIENT_ACTIVITY.ToString());			
			if (plantilla == null)
			{
				throw new Exception("Plantilla no definida: " + Berke.Libs.Base.PlantillaTipo.TMPL_REPORT_CLIENT_ACTIVITY.ToString());
			}
			cliServ  = new ClienteService(db);
			fechainf = new DateTime(0);
			fechasup = new DateTime(0);
		}
		#endregion Constructors

		#region Setters
		/// <summary>
		/// Establece conjunto de datos sobre los cuales operará el reporte
		/// </summary>
		/// <param name="dt"></param>
		public void setDataSet(DataTable dt)
		{
			this.dt = dt;
		}

		public void setFechaInf(DateTime fechainf)
		{
			this.fechainf = fechainf;
		}

		public void setFechaSup(DateTime fechasup)
		{
			this.fechasup = fechasup;
		}

		public void showNuestras(bool show_nuestras)
		{
			this.show_nuestras = show_nuestras;
		}

		public void showTerceros(bool show_terceros)
		{
			this.show_terceros = show_terceros;
		}

		public void showResumen(bool show_resumen)
		{
			this.show_resumen = show_resumen;
		}

		public void showAgentes(bool show_agentes)
		{
			this.show_agentes = show_agentes;
		}
		#endregion Setters

		#region Generar Documento
		public string generar()
		{
			cgDoc        = new Berke.Libs.CodeGenerator(plantilla);			
			cgClient     = cgDoc.ExtraerTabla("cgClient");
			cgProp       = cgClient.ExtraerTabla("cgProp");
			cgTabla      = cgProp.ExtraerTabla("cgTabla");
			cgTablaTit   = cgTabla.ExtraerFila("cgTablaTit");
			cgTablaHead  = cgTabla.ExtraerFila("cgTablaHead");
			cgTablaRow   = cgTabla.ExtraerFila("cgTablaRow");
			cgResumen    = cgDoc.ExtraerTabla("cgResumen");
			cgAgentes    = cgResumen.ExtraerTabla("cgAgentes");

			string clienteID = dt.Rows[0][ClientActivityRow.COL_CLIENTE_ID].ToString();
			DataRow lastRow  = dt.Rows[0];
			string propID    = "";

			string tabla = "";

			cgProp.copyTemplateToBuffer();
			cgClient.copyTemplateToBuffer();

			DataRow currRow = null;

			foreach (DataRow dr in dt.Rows)
			{
				currRow = dr;
				string currClienteID = dr[ClientActivityRow.COL_CLIENTE_ID].ToString();
				string currPropID    = dr[ClientActivityRow.COL_PROPIETARIO_ID].ToString();
				
				if (currClienteID != clienteID)
				{
					#region Corte por cliente
					if (show_resumen)
					{
						string resumen = this.displayResumen(cgAgentes, Convert.ToInt32(clienteID));
						cgResumen.clearText();
						cgResumen.copyTemplateToBuffer();
						cgResumen.replaceField("resumen.cnuestras", nNuestras.ToString());
						cgResumen.replaceField("resumen.cterceros", nVigiladas.ToString());
						cgResumen.replaceLabel("cgAgentes", resumen);
						cgResumen.addBufferToText();
						cgClient.replaceField("resumen", cgResumen.Texto);
					}
					else 
					{
						cgClient.replaceField("resumen", "");
					}
					nNuestras = 0;
					nVigiladas = 0;

					// Actualizamos la tabla de propietarios en el cliente
					cgClient.replaceLabel("cgProp", cgProp.Texto);	

					// Verificamos si el cliente es múltiple
					int cliTrID = cliServ.getClienteTramiteID(Convert.ToInt32(clienteID), 
						                                      (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION);
					if (cliTrID < 0) 
					{
						// Aceptamos los datos del cliente
						cgClient.replaceField("clienteData.id",          lastRow[ClientActivityRow.COL_CLIENTE_ID].ToString());
						cgClient.replaceField("clienteData.nombre",      lastRow[ClientActivityRow.COL_CLIENTE_NOMBRE].ToString());
						cgClient.replaceField("clienteData.direccion",   lastRow[ClientActivityRow.COL_CLIENTE_DIR].ToString());
						cgClient.replaceField("clienteData.pais",        lastRow[ClientActivityRow.COL_CLIENTE_PAIS].ToString());
					}
					else
					{
						cliServ.setClienteID(cliTrID);
						Berke.DG.DBTab.Cliente cli = cliServ.getCliente();
						cgClient.replaceField("clienteData.id",          cli.Dat.ID.AsString);
						cgClient.replaceField("clienteData.nombre",      cli.Dat.Nombre.AsString);
						cgClient.replaceField("clienteData.direccion",   cli.Dat.Direccion.AsString);
						Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
						pais.Adapter.ReadByID(cli.Dat.PaisID.AsInt);
						cgClient.replaceField("clienteData.pais", pais.Dat.paisalfa.AsString );
					}
					cgClient.addBufferToText();
					cgClient.clearBuffer();
					cgClient.copyTemplateToBuffer();

					// Liberamos los datos del propietario
					cgProp.clearText();

					clienteID = currClienteID;
					propID    = currPropID;
					lastRow   = currRow;
					#endregion Corte por cliente
				} 

				// Reemplazar tablas incrustadas dentro de la tabla de propietarios


				//Renovadas por otro
				
				tabla = this.displayRXO(cgTablaRow, Convert.ToInt32(currClienteID), Convert.ToInt32(currPropID) );
				if (tabla != "")
				{
					cgTablaTit.clearText();
					cgTablaTit.copyTemplateToBuffer();
					cgTablaTit.replaceField("tabMarcas.title", "Marcas Renovadas por otro");
					cgTablaTit.addBufferToText();

					cgTabla.copyTemplateToBuffer();
					cgTabla.replaceLabel("cgTablaTit",  cgTablaTit.Texto);
					cgTabla.replaceLabel("cgTablaHead", cgTablaHead.Template);
					cgTabla.replaceLabel("cgTablaRow",  cgTablaRow.Texto);
					cgTabla.addBufferToText();
				}

				
				//--> Marcas nuestras
				// hay que ejecutar sólo si debe visualizarse nuestras y resúmen
				tabla = this.displayMarcas(cgTablaRow, Convert.ToInt32(currClienteID), Convert.ToInt32(currPropID), true );
				if (show_nuestras)
				{
					if (tabla != "")
					{
						cgTablaTit.clearText();
						cgTablaTit.copyTemplateToBuffer();
						cgTablaTit.replaceField("tabMarcas.title", "Marcas Nuestras");
						cgTablaTit.addBufferToText();
					
						cgTabla.copyTemplateToBuffer();
						cgTabla.replaceLabel("cgTablaTit",  cgTablaTit.Texto);
						cgTabla.replaceLabel("cgTablaHead", cgTablaHead.Template);
						cgTabla.replaceLabel("cgTablaRow",  cgTablaRow.Texto);
						cgTabla.addBufferToText();
					}
				}
				// hay que ejecutar sólo si debe visualizarse terceros y resúmen
				tabla = this.displayMarcas(cgTablaRow, Convert.ToInt32(currClienteID), Convert.ToInt32(currPropID), false );
				if (show_terceros) 
				{
					//--> Marcas Vigiladas
					if (tabla != "")
					{
						cgTablaTit.clearText();
						cgTablaTit.copyTemplateToBuffer();
						cgTablaTit.replaceField("tabMarcas.title", "Marcas Vigiladas");
						cgTablaTit.addBufferToText();
			
						cgTabla.copyTemplateToBuffer();				
						cgTabla.replaceLabel("cgTablaTit",  cgTablaTit.Texto);
						cgTabla.replaceLabel("cgTablaHead", cgTablaHead.Template);
						cgTabla.replaceLabel("cgTablaRow",  cgTablaRow.Texto);
						cgTabla.addBufferToText();
					}
				}

				cgProp.copyTemplateToBuffer();
				cgProp.replaceField("propData.id", dr[ClientActivityRow.COL_PROPIETARIO_ID].ToString());
				cgProp.replaceField("propData.nombre", dr[ClientActivityRow.COL_PROPIETARIO_NOMBRE].ToString());
				cgProp.replaceField("propData.direccion", dr[ClientActivityRow.COL_PROPIETARIO_DIRECCION].ToString());
				cgProp.replaceField("propData.pais", dr[ClientActivityRow.COL_PROPIETARIO_PAIS].ToString());

				cgProp.replaceLabel("cgTabla",cgTabla.Texto);
				cgProp.addBufferToText();
				cgProp.clearBuffer();		

				cgTabla.clearText();
				

			}
			if (dt.Rows.Count>0)
			{
				#region Corte por cliente
				string currClienteID = currRow[ClientActivityRow.COL_CLIENTE_ID].ToString();
				if (show_resumen)
				{
					string resumen = this.displayResumen(cgAgentes, Convert.ToInt32(currClienteID) );
					cgResumen.clearText();
					cgResumen.copyTemplateToBuffer();
					cgResumen.replaceField("resumen.cnuestras", nNuestras.ToString());
					cgResumen.replaceField("resumen.cterceros", nVigiladas.ToString());
					cgResumen.replaceLabel("cgAgentes", resumen);
					cgResumen.addBufferToText();
					nNuestras = 0;
					nVigiladas = 0;

					// Actualizamos la tabla de propietarios en el cliente						
					cgClient.replaceField("resumen", cgResumen.Texto);
				}
				else
				{
					cgClient.replaceField("resumen", "");
				}
				cgClient.replaceLabel("cgProp", cgProp.Texto);


				// Verificamos si el cliente es múltiple
				int cliTrID = cliServ.getClienteTramiteID(Convert.ToInt32(currClienteID), 
					(int) GlobalConst.Marca_Tipo_Tramite.RENOVACION);
				if (cliTrID < 0) 
				{
					// Aceptamos los datos del cliente
					cgClient.replaceField("clienteData.id",          currRow[ClientActivityRow.COL_CLIENTE_ID].ToString());
					cgClient.replaceField("clienteData.nombre",      currRow[ClientActivityRow.COL_CLIENTE_NOMBRE].ToString());
					cgClient.replaceField("clienteData.direccion",   currRow[ClientActivityRow.COL_CLIENTE_DIR].ToString());
					cgClient.replaceField("clienteData.pais",        currRow[ClientActivityRow.COL_CLIENTE_PAIS].ToString());
				}
				else
				{
					cliServ.setClienteID(cliTrID);
					Berke.DG.DBTab.Cliente cli = cliServ.getCliente();
					cgClient.replaceField("clienteData.id",          cli.Dat.ID.AsString);
					cgClient.replaceField("clienteData.nombre",      cli.Dat.Nombre.AsString);
					cgClient.replaceField("clienteData.direccion",   cli.Dat.Direccion.AsString);
					Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
					pais.Adapter.ReadByID(cli.Dat.PaisID.AsInt);
					cgClient.replaceField("clienteData.pais", pais.Dat.paisalfa.AsString );
				}
				cgClient.addBufferToText();
				cgClient.clearBuffer();
				cgClient.copyTemplateToBuffer();

				// Liberamos los datos del propietario
				cgProp.clearText();

				#endregion Corte por cliente
			}
			cgDoc.copyTemplateToBuffer();
			cgDoc.replaceLabel("cgClient", cgClient.Texto);
			cgDoc.replaceField("fecConsulta.inicio", fechainf.ToString("dd/MM/yyyy"));
			cgDoc.replaceField("fecConsulta.fin", fechasup.ToString("dd/MM/yyyy"));
			cgDoc.replaceField("cgResumen", "");
			cgDoc.addBufferToText();

			return cgDoc.Texto ;
		}
		#endregion Generar Documento

		#region Renovados por otro
		private string displayRXO(Berke.Libs.CodeGenerator cgBody, int clienteID, int propID)
		{
			Berke.DG.ViewTab.vClientActivityRenXOtro vRxo = new Berke.DG.ViewTab.vClientActivityRenXOtro(db);
			vRxo.Dat.ClienteID.Filter        = clienteID;
			vRxo.Dat.PropietarioID.Filter    = propID;
			vRxo.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter(fechainf + "-" + fechasup); 
			vRxo.Adapter.ReadAll(MAX_ROWS);
			return this.generarTabla(cgBody,vRxo.Table);		
		}
		#endregion Renovados por otro

		#region Resumen
		private string displayResumen(Berke.Libs.CodeGenerator cgBody, int clienteID)
		{
			Berke.DG.ViewTab.vClientActivityAgentes vAg = new Berke.DG.ViewTab.vClientActivityAgentes(db);
			vAg.Dat.ClienteID.Filter           = clienteID;
			vAg.Dat.AgenteLocalMatricula.Order = 1;
			vAg.Adapter.Distinct = true;
			vAg.Adapter.ReadAll(MAX_ROWS);
			return this.getAgentesLocales(cgBody, vAg.Table);		
		}
		#endregion Resumen

		#region Marcas nuestras
		private string displayMarcas(Berke.Libs.CodeGenerator cgBody, int clienteID, int propID, bool nuestra)
		{
			ArrayList lst = new ArrayList();
			lst.Add((int)GlobalConst.Marca_Tipo_Tramite.REGISTRO);
			lst.Add((int)GlobalConst.Marca_Tipo_Tramite.RENOVACION);

			Berke.DG.ViewTab.vClientActivityMarcas vMar = new Berke.DG.ViewTab.vClientActivityMarcas(db);
			vMar.Dat.ClienteID.Filter     = clienteID;
			vMar.Dat.TramiteID.Filter     = new DSFilter(lst);
			vMar.Dat.PropietarioID.Filter = propID;
			vMar.Dat.Nuestra.Filter       = nuestra;			
			vMar.Dat.Vigilada.Filter      = true;
			vMar.Dat.ActaAnio.Order = 1;
			vMar.Dat.ActaNro.Order = 2;
			vMar.Adapter.ReadAll(MAX_ROWS);
			if (nuestra)
			{
				nNuestras += vMar.RowCount;
			}
			else 
			{
				nVigiladas += vMar.RowCount;
			}
			return this.generarTabla(cgBody, vMar.Table);		
		}
		#endregion Marcas nuestras

		#region Generar tabla
		private string generarTabla(Berke.Libs.CodeGenerator cg, DataTable dtMarca)
		{
			cg.clearText();
			foreach (DataRow dr in dtMarca.Rows)
			{
				cg.copyTemplateToBuffer();
				if ( dr[ClientActivityInfoRow.COL_FECHASOL.Column].ToString() != "") 
				{													
					cg.replaceField(ClientActivityInfoRow.COL_FECHASOL.Field, DateTime.Parse(dr[ClientActivityInfoRow.COL_FECHASOL.Column].ToString()).ToString("dd/MM/yyyy"));
				}
				else 
				{
					cg.replaceField(ClientActivityInfoRow.COL_FECHASOL.Field, "");
				}
				cg.replaceField(ClientActivityInfoRow.COL_MARCATIPO.Field, dr[ClientActivityInfoRow.COL_MARCATIPO.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_REGISTRONRO.Field, dr[ClientActivityInfoRow.COL_REGISTRONRO.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_DENOMINACION.Field, dr[ClientActivityInfoRow.COL_DENOMINACION.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_CLASE.Field, dr[ClientActivityInfoRow.COL_CLASE.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_AGENTELOCAL.Field, dr[ClientActivityInfoRow.COL_AGENTELOCAL.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_ACTANRO.Field, dr[ClientActivityInfoRow.COL_ACTANRO.Column].ToString());
				cg.replaceField(ClientActivityInfoRow.COL_ACTAANIO.Field, dr[ClientActivityInfoRow.COL_ACTAANIO.Column].ToString());				
				cg.replaceField(ClientActivityInfoRow.COL_TRAMITE.Field, dr[ClientActivityInfoRow.COL_TRAMITE.Column].ToString());				

				if ( dr[ClientActivityInfoRow.COL_FECHAVENC.Column].ToString() != "") 
				{													
				cg.replaceField(ClientActivityInfoRow.COL_FECHAVENC.Field, DateTime.Parse(dr[ClientActivityInfoRow.COL_FECHAVENC.Column].ToString()).ToString("dd/MM/yyyy"));			
				}
				else 
				{
					cg.replaceField(ClientActivityInfoRow.COL_FECHAVENC.Field, "");
				}
				cg.addBufferToText();
			}
			
			return cg.Texto;
			
		}
		#endregion Generar tabla

		#region Tabla de Agentes
		/// <summary>
		/// Obtiene la lista de Agentes con los que trabaja el Cliente
		/// </summary>
		/// <param name="cg"></param>
		/// <param name="dtMarca"></param>
		/// <returns></returns>
		private string getAgentesLocales(Berke.Libs.CodeGenerator cg, DataTable dtMarca)
		{
			cg.clearText();
			foreach (DataRow dr in dtMarca.Rows)
			{
				cg.copyTemplateToBuffer();
				cg.replaceField("aglocal.matricula", dr["AgenteLocalMatricula"].ToString());
				cg.replaceField("aglocal.nombre",    dr["AgenteLocalNombre"].ToString() );
				cg.addBufferToText();
			}
			
			return cg.Texto;

		}
		#endregion Tabla de Agentes
	}


	/// <summary>
	/// La idea detrás de esta clase es agrupar las columnas que se esperan
	/// que se encuentren en el DataTable utilizado por el reporte ClientActivity
	/// </summary>
	public class ClientActivityRow 
	{
		public const string COL_CLIENTE_ID     = "ClienteID";
		public const string COL_CLIENTE_NOMBRE = "ClienteNombre";
		public const string COL_CLIENTE_PAIS   = "ClientePais";
		public const string COL_CLIENTE_DIR    = "ClienteDir";
		public const string COL_PROPIETARIO_ID = "PropietarioID";
		public const string COL_PROPIETARIO_NOMBRE    = "PropietarioNombre";
		public const string COL_PROPIETARIO_DIRECCION = "PropietarioDir";
		public const string COL_PROPIETARIO_PAIS      = "PropietarioPais";
	}

	public class ClientActivityInfoRow 
	{
		public static ColumnFieldMap COL_FECHASOL  = new ColumnFieldMap("FechaSol", "tabMarcas.fecha");
		public static ColumnFieldMap COL_ACTANRO   = new ColumnFieldMap("ActaNro","tabMarcas.actaNro");
		public static ColumnFieldMap COL_ACTAANIO  = new ColumnFieldMap("ActaAnio","tabMarcas.actaAnio");
		public static ColumnFieldMap COL_CLASE     = new ColumnFieldMap("Clase","tabMarcas.clase");
		public static ColumnFieldMap COL_MARCATIPO = new ColumnFieldMap("MarcaTipo","tabMarcas.tipo");
		public static ColumnFieldMap COL_DENOMINACION = new ColumnFieldMap("Denominacion","tabMarcas.denominacion");
		public static ColumnFieldMap COL_AGENTELOCAL  = new ColumnFieldMap("AgenteLocal","tabMarcas.agenteLocal");
		public static ColumnFieldMap COL_REGISTRONRO  = new ColumnFieldMap("RegistroNro", "tabMarcas.registroNro");
		public static ColumnFieldMap COL_TRAMITE      = new ColumnFieldMap("Abrev", "tabMarcas.tramite");
		public static ColumnFieldMap COL_FECHAVENC    = new ColumnFieldMap("VencimientoFecha", "tabMarcas.fechavenc");
	}

	public class ColumnFieldMap 
	{
		string field;
		string column;
		public ColumnFieldMap(string column, string field)
		{
			this.column = column;
			this.field  = field;
		}
		public string Column 
		{
			get {return column;	}
		}
		public string Field 
		{
			get {return field;	}
		}	
	
	}

}
