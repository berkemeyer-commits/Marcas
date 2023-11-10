using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	///
	/// Autor mbaez
	/// 04/2007
	///
	/// </summary>
	
	using Berke.Libs;
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;

	public partial class RepPropBoletin : System.Web.UI.Page
	{
		private static int MAX_ROWS = 50000;
		private static string CLAVE_PLANTILLA = "RepPropBol";
		private static string FILTRO_CARPETA = "1";
		private static string APROX_EXACTO = "-1";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Conexion a la BD
		private Berke.Libs.Base.Helpers.AccesoDB getDB()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			return db;
		}
		#endregion Conexion a la BD

		#region Aplicar Filtros
		private void aplicarFiltros(Berke.DG.DBTab.BoletinDet boldet, AccesoDB db)
		{
			ArrayList lst;

			Berke.DG.DBTab.Boletin    bol    = new Berke.DG.DBTab.Boletin(db);

			#region Filtros para la carpeta y año de proceso
			if ( (txtCarpetaAnio.Text.Trim().Length>0) || (txtCarpetaNro.Text.Trim().Length>0) )
			{
				if (ddlTipoFiltro.SelectedValue == FILTRO_CARPETA) 
				{
					lst = new ArrayList();
					bol.Dat.Nro.Filter  = ObjConvert.GetFilter(txtCarpetaNro.Text);	
					bol.Dat.Anio.Filter = ObjConvert.GetFilter(txtCarpetaAnio.Text);
					bol.Adapter.ReadAll();
				
					for (int i=0; i<bol.RowCount; i++)
					{
						lst.Add(bol.Dat.ID.AsInt);
					}
					boldet.Dat.BoletinID.Filter = new DSFilter(lst); 
				}
				else 
				{
					boldet.Dat.BolNro.Filter = ObjConvert.GetFilter(txtCarpetaNro.Text);
					boldet.Dat.BolAnio.Filter = ObjConvert.GetFilter(txtCarpetaAnio.Text);
				}
			}
			#endregion Filtros para la carpeta y año de proceso
			
			#region Filtros para tramites nuestros
			Berke.DG.DBTab.CAgenteLocal agente = new Berke.DG.DBTab.CAgenteLocal(db);
			agente.Dat.Nuestro.Filter = true;
			agente.Adapter.ReadAll();
			lst = new ArrayList();
			for(agente.GoTop(); !agente.EOF; agente.Skip())
			{
				lst.Add(agente.Dat.nromatricula.AsInt);
			}
			boldet.Dat.AgenteLocal.Filter = new DSFilter(lst);
			#endregion Filtros para tramites nuestros

			boldet.Dat.ExpNro.Filter  = new DSFilter(txtActaDe.Text, txtActaHasta.Text);
			boldet.Dat.ExpAnio.Filter = ObjConvert.GetFilter(txtActaAnio.Text);

		}
		#endregion Aplicar Filtros

		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}

		protected void btnVerificar_Click(object sender, System.EventArgs e)
		{
			#region Verificar entrada
			if( (txtActaDe.Text.Trim().Length==0)&&(txtActaHasta.Text.Trim().Length==0 )&&
				(txtCarpetaAnio.Text.Trim().Length==0)&&(txtCarpetaNro.Text.Trim().Length==0)&&
				(txtActaAnio.Text.Trim().Length==0))
			{
				this.ShowMessage("Debe especificar al menos un parámetro de filtrado.");
				return;
			}


			try
			{
				if (txtActaDe.Text.Trim().Length>0)
					ObjConvert.AsInt(txtActaDe.Text);
				if (txtActaHasta.Text.Trim().Length>0)
					ObjConvert.AsInt(txtActaHasta.Text);
				if (txtCarpetaAnio.Text.Trim().Length>0)
					ObjConvert.AsInt(txtCarpetaAnio.Text);
				//if (txtCarpetaNro.Text.Trim().Length>0)
				//	ObjConvert.AsInt(txtCarpetaNro.Text);
				if (txtActaAnio.Text.Trim().Length>0)
					ObjConvert.AsInt(txtActaAnio.Text);

			}
			catch(Exception ex)
			{
				this.ShowMessage("Error en los parámetros.");
				return;
			}
			#endregion Verificar entrada

			this.generarReporte();		
		}

		#region Generar Reporte
		private void generarReporte()
		{
			AccesoDB db = this.getDB();
			Berke.DG.DBTab.BoletinDet boldet = new Berke.DG.DBTab.BoletinDet(db);			
			string res = "";
			int nactaslistadas=0;
			int actainicio = 0;
			int actafin    = 0;

			#region Buscar Plantilla
			Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
			dp.Dat.Clave.Filter = CLAVE_PLANTILLA;
			dp.Adapter.ReadAll();
			if (dp.RowCount == 0) 
			{
				ShowMessage("No se encuentra definida la plantilla para el reporte.");
			}
			
			string template = dp.Dat.PlantillaHTML.AsString;
			#endregion Buscar Plantilla

			#region Inicializar Code-Generators
			Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
			Berke.Libs.CodeGenerator cab = cg.ExtraerFila("cab");
			Berke.Libs.CodeGenerator row = cg.ExtraerFila("row");
			#endregion Inicializar Code-Generators



			try 
			{
				this.aplicarFiltros(boldet,db);
				boldet.Adapter.ReadAll(MAX_ROWS);
				
				boldet.GoTop();
				actainicio = boldet.Dat.ExpNro.AsInt;

				for (boldet.GoTop(); ! boldet.EOF; boldet.Skip())
				{
					
					res = this.checkTramite(boldet.Dat, db);
					if (res.Length>0)
					{
						row.copyTemplateToBuffer();
						row.replaceField("diagnostico"      ,res);
						row.replaceField("expnro"         ,boldet.Dat.ExpNro.AsString);
						row.replaceField("expanio"        ,boldet.Dat.ExpAnio.AsString);
						row.replaceField("refnro"         ,boldet.Dat.RefNro.AsString);
						row.replaceField("refanio"        ,boldet.Dat.RefAnio.AsString);
						row.replaceField("refregnro"      ,boldet.Dat.RefRegNro.AsString);
						row.replaceField("tramite"        ,boldet.Dat.Tramite.AsString);
						row.replaceField("denominacion"   ,boldet.Dat.Denominacion.AsString);
						row.replaceField("expedienteID"   ,boldet.Dat.ExpedienteID.AsString);
						row.replaceField("Propietario"    ,boldet.Dat.Propietario.AsString);
						row.replaceField("SolicitudFecha" , boldet.Dat.SolicitudFecha.AsString);
						row.addBufferToText();
						nactaslistadas++;
					}
					actafin = boldet.Dat.ExpNro.AsInt;
					
				}
				
				
				db.CerrarConexion();
			}
			catch(Exception ex)
			{
				db.CerrarConexion();
				throw new Exception("Error:"+ex.Message);
			}


			cg.copyTemplateToBuffer();
			cg.replaceLabel("cab", cab.Template);
			cg.replaceLabel("row", row.Texto);
			cg.replaceField("actainicio"    , actainicio.ToString());
			cg.replaceField("actafin"       , actafin.ToString());
			cg.replaceField("nactaslistadas", nactaslistadas.ToString());
			cg.replaceField("nactas", boldet.RowCount.ToString());
			cg.addBufferToText();

			#region Activar MS-Word
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=repPropBol.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(cg.Texto); //Llamada al procedimiento HTML
			Response.End();
			#endregion Activar MS-Word		

		}
		#endregion Generar Reporte

		#region Verificar trámite
		private string checkTramite(Berke.DG.DBTab.Row.BoletinDetRow row, AccesoDB db)
		{
			Berke.DG.DBTab.Expediente expe    = new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.MarcaRegRen regren = new Berke.DG.DBTab.MarcaRegRen(db);
			string diagnostico ="";
			int expePadreID = 0;
			int marcaHijoID = 0;
			string ERR_ENGANCHE = "El detalle del boletin no esta enganchado al expediente correcto.";
			switch (row.Tramite.AsString)
			{
					//En estos casos la entrada se engancha al expediente de REG
					// Buscamos el padre a través de expnro y expanio
				case "REG": 
				case "RG" : 
					#region Verificar si se encuentra enganchado
					if (row.ExpedienteID.IsNull)
					{
						return "No se ha procesado.";
					}
					expe.ClearFilter();
					expe.Adapter.ReadByID( row.ExpedienteID.AsInt );
					if ( (expe.Dat.ActaNro.AsInt != row.ExpNro.AsInt) ||
						 (expe.Dat.ActaAnio.AsInt != row.ExpAnio.AsInt) )

					{
						return ERR_ENGANCHE;
					}
					#endregion Verificar si se encuentra enganchado
					
					#region Verificar Denominacion
					diagnostico = this.marcasParecidas(expe.Dat.MarcaID.AsInt, row.Denominacion.AsString, db);
					if (diagnostico.Length > 0 ) 
					{
						return "Denominaciones no coinciden: "+ diagnostico +".";
					}
					#endregion Verificar Denominacion

					#region Verificar Datos de Marcas
					diagnostico = this.verificarDatosMarcas(expe.Dat.MarcaID.AsInt, row, db);
					if (diagnostico.Length > 0 ) 
					{
						return  diagnostico;
					}
					#endregion Verificar Datos de Marcas

					#region Verificar Tipo de trámite
					diagnostico = this.verificarDatosTramite(row,db);
					if (diagnostico.Length > 0 ) 
					{
						return diagnostico;
					}
					#endregion Verificar tipo de trámite

					#region Verificar datos no necesarios. Control a pedido de Prof. Blanca
					if ( (!row.RefRegNro.IsNull ) && (row.RefRegNro.AsInt>0) )
					{
						return "El detalle del Boletin hace referencia a un registro padre.";
					}
					if ( (!row.RefNro.IsNull && row.RefNro.AsInt  >0) ||
						(!row.RefAnio.IsNull && row.RefAnio.AsInt>0) ) 
					{
						return "El detalle del Boletin hace referencia a un acta/año padre.";
					}
					#endregion Verificar datos no necesarios

					break;
					//En estos casos la entrada se engancha al expediente de REG
					// Buscamos el padre a través de refNro/refAnio o refRegNro/refRegAnio
				case "REN": 
				case "RN" :

					#region Verificar si se encuentra enganchado
					if (row.ExpedienteID.IsNull)
					{
						return "No se ha procesado.";
					}
					expe.ClearFilter();
					expe.Adapter.ReadByID( row.ExpedienteID.AsInt );
					
					if( (! row.RefAnio.IsNull && (row.RefAnio.AsInt > 0) ) &&
						! row.RefNro.IsNull  && (row.RefNro.AsInt > 0)  )
					{
						if ( (expe.Dat.ActaNro.AsInt != row.RefNro.AsInt) ||
							(expe.Dat.ActaAnio.AsInt != row.RefAnio.AsInt) )

						{
							return ERR_ENGANCHE;
						}
					}
					else if (! row.RefRegNro.IsNull && (row.RefRegNro.AsInt > 0) ) 
					{
						regren.ClearFilter();
						regren.Adapter.ReadByID(expe.Dat.MarcaRegRenID.AsInt);
						if ( regren.RowCount==0)
						{
							return "El detalle del boletin hace referencia a un Registro/Renovación anterior que no existe.";
						}
						if ( ( regren.Dat.RegistroNro.AsInt != row.RefRegNro.AsInt) && (expe.Dat.ActaNro.AsInt != row.ExpNro.AsInt) && 
							(expe.Dat.ActaAnio.AsInt != row.ExpAnio.AsInt))
						{
							return ERR_ENGANCHE;
						}

					}
					else if ((expe.Dat.ActaNro.AsInt != row.ExpNro.AsInt) && (expe.Dat.ActaAnio.AsInt != row.ExpAnio.AsInt))
					{
						return ERR_ENGANCHE;
					}

					#endregion Verificar si se encuentra enganchado
						    
					#region Verificar si concide el registro padre
					expe.ClearFilter();
					expe.Dat.ActaNro.Filter  = row.ExpNro.AsInt;
					expe.Dat.ActaAnio.Filter = row.ExpAnio.AsInt;
					expe.Adapter.ReadAll();

					if (expe.RowCount <= 0)
					{
						return "El expediente perteneciente al trámite no existe o aún se encuentra en situación H.I.";
					}

					expePadreID = expe.Dat.ExpedienteID.AsInt;
					marcaHijoID = expe.Dat.MarcaID.AsInt;
					// Buscamos al padre
					expe.Adapter.ReadByID(expePadreID);
					// Buscamos el registro del padre
					regren.Adapter.ReadByID(expe.Dat.MarcaRegRenID.AsInt);
					
					if ( (!row.RefRegNro.IsNull ) && (row.RefRegNro.AsInt>0) && 
						(row.RefRegNro.AsInt != regren.Dat.RegistroNro.AsInt ) )
					{
						return "El detalle del boletín posee un nro. de registro padre que no coincide con el del mismo expediente en el sistema : "+ row.RefRegNro.AsString +"!="+ regren.Dat.RegistroNro.AsString;
					}
					if ( (!row.RefNro.IsNull && row.RefNro.AsInt  >0)&& (expe.Dat.ActaNro.AsInt != row.RefNro.AsInt) ||
						 (!row.RefAnio.IsNull && row.RefAnio.AsInt>0)&& (expe.Dat.ActaAnio.AsInt != row.RefAnio.AsInt) ) 
					{
						return "El detalle del boletín posee un acta padre que no coincide con el del mismo expediente en el sistema.";
					}

					
					#endregion Verificar si concide el registro padre

					#region Verificar Denominacion
					diagnostico = this.marcasParecidas(marcaHijoID, row.Denominacion.AsString, db);
					if (diagnostico.Length > 0 ) 
					{
						return "Denominaciones no coinciden: "+ diagnostico +".";
					}
					#endregion Verificar Denominacion

					#region Verificar Datos de Marcas
					diagnostico = this.verificarDatosMarcas(marcaHijoID, row, db);
					if (diagnostico.Length > 0 ) 
					{
						return  diagnostico;
					}
					#endregion Verificar Datos de Marcas

					#region Verificar Tipo de trámite
					diagnostico = this.verificarDatosTramite(row,db);
					if (diagnostico.Length > 0 ) 
					{
						return diagnostico;
					}
					#endregion Verificar tipo de trámite

					break;
				// En estos casos el trámite se engancha al tramite de REG/REN padre
				// Buscamos a partir del refnro/refanio o refregnro/refreganio
				case "CD":
				case "CN":
				case "TR":
				case "FS":
				case "LC":

					#region Verificar si se encuentra enganchado
					if (row.ExpedienteID.IsNull)
					{
						return "No se ha procesado.";
					}
					// Buscamos el expediente al que se encuentra enganchado
					expe.ClearFilter();
					expe.Adapter.ReadByID( row.ExpedienteID.AsInt );
					int expeEngancheID = expe.Dat.ID.AsInt;


					// Buscamos el expediente perteneciente al tramite;
					expe.ClearFilter();
					expe.Dat.ActaNro.Filter  = row.ExpNro.AsInt;
					expe.Dat.ActaAnio.Filter = row.ExpAnio.AsInt;
					expe.Adapter.ReadAll();
					expePadreID = expe.Dat.ExpedienteID.AsInt;

					// Buscamos al "Padre"
					// A veces si existe una renovación en trámite, el trámite (TR,CD..)
					// se engancha al registro
					expe.ClearFilter();
					expe.Adapter.ReadByID(expePadreID);

					// Si todo esta el detalle del boletin debería estar
					// enganchado al padre del expediente correspondiente al trámite
					// En ciertos casos, cuando el padre esta en tramite se engancha al abuelo.
					if ((expePadreID != expeEngancheID) && 
						(expe.Dat.ExpedienteID.IsNull || (expe.Dat.ExpedienteID.AsInt != expeEngancheID) ) )
					{
							// Laura creyo no conveniente informar sobre este problema
							//return "No esta enganchado al expediente correcto.";
					}

					#endregion 

					#region Verificar si concide el registro padre
					expe.ClearFilter();
					expe.Dat.ActaNro.Filter  = row.ExpNro.AsInt;
					expe.Dat.ActaAnio.Filter = row.ExpAnio.AsInt;
					expe.Adapter.ReadAll();

					if (expe.RowCount<=0)
					{
						return "El expediente perteneciente al trámite no existe o aún se encuentra en situación H.I.";
					}
					#endregion Verificar si concide el registro padre

					#region Verificar Denominacion
					diagnostico = this.marcasParecidas(expe.Dat.MarcaID.AsInt, row.Denominacion.AsString, db);
					if (diagnostico.Length > 0 ) 
					{
						return "Denominaciones no coinciden: "+ diagnostico +".";
					}
					#endregion Verificar Denominacion

					#region Verificar Tipo de trámite
					diagnostico = this.verificarDatosTramite(row,db);
					if (diagnostico.Length > 0 ) 
					{
						return diagnostico;
					}
					#endregion Verificar tipo de trámite

					break;

				// Todos los tramites que se "enganchan" simplemente
				case "OTROS":
					break;

				default: break;
			}
			return "";
		}
		#endregion Verificar trámite

		#region Verificar datos de trámite
		private string verificarDatosTramite(Berke.DG.DBTab.Row.BoletinDetRow row, AccesoDB db)
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			expe.Dat.ActaNro.Filter  = ObjConvert.GetFilter(row.ExpNro.AsString);
			expe.Dat.ActaAnio.Filter = ObjConvert.GetFilter(row.ExpAnio.AsString);
			expe.Adapter.ReadAll();
			
			#region Buscar descripcion del trámite
			Berke.DG.DBTab.Tramite tr = new Berke.DG.DBTab.Tramite(db);
			tr.Adapter.ReadByID(expe.Dat.TramiteID.AsInt);
			#endregion Buscar descripcion del trámite 
			

			switch (row.Tramite.AsString)
			{
				case "REG": 
				case "RG" : 
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
					{
						return "El tipo de trámite difiere:"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "RN":
				case "REN":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "CD":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "CN":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "TR":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "FS":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.FUSION) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				case "LC":
					if (expe.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.LICENCIA) 
					{
						return "El tipo de trámite con concuerda con :"+ tr.Dat.Descrip.AsString+".";
					}
					break;
				default:
					// No se hace nada..
					break;
			}

			#region Comparar agentes locales
			Berke.DG.DBTab.CAgenteLocal ag = new Berke.DG.DBTab.CAgenteLocal(db);
			ag.Adapter.ReadByID(expe.Dat.AgenteLocalID.AsInt);
			if (ag.Dat.nromatricula.AsString != row.AgenteLocal.AsString)
			{
				return "No coincide agente local:" + ag.Dat.nromatricula.AsString+ " != "+ row.AgenteLocal.AsString+".";
			}

			#endregion Comparar agentes locales

			return "";
			

		}
		#endregion Verificar datos de trámite

		#region Verificar Datos de la Marca
		private string verificarDatosMarcas(int marcaID, Berke.DG.DBTab.Row.BoletinDetRow row, AccesoDB db)
		{
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca(db);
			mar.ClearFilter();
			mar.Adapter.ReadByID(marcaID);

			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			clase.Adapter.ReadByID(mar.Dat.ClaseID.AsInt);

			
			if (clase.Dat.Nro.AsString != row.Clase.AsString) 
			{
				return "Las clases no coinciden: "+ clase.Dat.Nro.AsString +" != "+ row.Clase.AsString+".";
			}
			
			if (!this.strSemejante(mar.Dat.Propietario.AsString, row.Propietario.AsString))
			{
				return "Los propietarios no coinciden: "+ mar.Dat.Propietario.AsString +" != "+ row.Propietario.AsString;
			}

			return "";


		}
		#endregion Verificar Datos de la Marca

		#region Verificar Denominaciones

		private string marcasParecidas(int marcaID, string denBoletin, AccesoDB db)
		{
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca(db);
			mar.ClearFilter();
			mar.Adapter.ReadByID(marcaID);

			// No se comprueban las Marcas figurativas.
			if ( mar.Dat.MarcaTipoID.AsInt == (int)GlobalConst.MarcaTipo.FIGURATIVA) 
			{
				return "";
			}
			// Si no son parecidos
			if(! this.strSemejante(mar.Dat.Denominacion.AsString, denBoletin) )
			{
				return mar.Dat.Denominacion.AsString +" != "+ denBoletin;
			}
			return "";

		}

		private bool strSemejante(string texto1, string texto2)
		{
			if (rbAproximacion.SelectedValue != APROX_EXACTO) 
			{
				texto1   = normalizarTexto(texto1);
				texto2 = normalizarTexto(texto2);
			}
			else 
			{
				texto1.Trim();
				texto2.Trim();
			}
			if (texto1 == texto2) 
				return true;

			return  this.textIn(texto1, texto2) || this.textIn(texto2, texto1);

		}

		private bool textIn(string textbloques, string text)
		{
			if (text.IndexOf(textbloques)>=0) 
				return true;

			int MAX_CHAR_CMP = Convert.ToInt16(rbAproximacion.SelectedValue);
			if (MAX_CHAR_CMP < 0) 
			{
				MAX_CHAR_CMP = textbloques.Length;
			}

			for (int i=0; i< (textbloques.Length - MAX_CHAR_CMP); i++)
			{
				string bloque = textbloques.Substring(i,MAX_CHAR_CMP);
				if (text.IndexOf(bloque)>=0)
				{
					return true;
				}
			}
			return false;
		}

		private string normalizarTexto(string texto)
		{
			string lstCharEspeciales   = "ÁÉÍÓÚÀÈÌÒÙÄËÏÖÜÂÊÎÔÛÑ";
			string lstCharNormalizados = "AEIOUAEIOUAEIOUAEIOUN";
			string lstCharEliminar     = "°!\"#$%&/()=?¡*¨][_:;,.-}{+´¿'|¬\\~`^";
			texto = texto.ToUpper();
			texto = this.str_replace(texto,lstCharEspeciales.ToCharArray(),lstCharNormalizados.ToCharArray());
			texto = this.str_replace(texto,lstCharEliminar.ToCharArray(), " ".PadRight(lstCharEliminar.Length).ToCharArray() );
			
			// Pseudo Trim
			for(int i=texto.Length; i > 1; i--)
			{
				// Reemplazo una secuencia de espacios un por solo espacio
				texto = texto.Replace(" ".PadRight(i), " ");
			}
			// Sacar algunos patrones " Y LOGOTIPO" Y ETIQUETA
			string[] patrones = {"Y LOGOTIPO", 
								 "Y ETIQUETA", 
								 "Y DISENO",
								 " SRL",
								 " S R L",
								 " S A ",
								 " S A E C A",
								 "  SAECA"}; 
			for(int i=0; i<patrones.Length; i++)
			{
				texto = texto.Replace(patrones[i], "");
			}

			return texto.Trim();
		}
		private string str_replace(string texto, char[] oldChar, char[] newChar)
		{
			for(int i=0; i< oldChar.Length; i++)
			{
				texto = texto.Replace(oldChar[i], newChar[i]);
			}
			return texto;			
		}
		#endregion Verificar Denominaciones

	}

}
