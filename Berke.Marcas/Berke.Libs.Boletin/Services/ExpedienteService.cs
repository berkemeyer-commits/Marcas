using System;
using System.Collections;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.Boletin.Libs;
namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// ExpedienteService.cs
	/// Provee servicios varios correspondientes al Expediente.
	/// Autor: Marcos Báez
	/// </summary>
	public class ExpedienteService
	{
		public static int MAX_ACTA = 100000;
		public static int INSTR_RENXOTRO = 4;
		public static int VENC_PERIODO_GRACIA = 6;
		
		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.Expediente        expediente;
		protected int expeID;
		protected Berke.DG.ViewTab.vExpeService view;
		protected int usuarioID;
		#endregion Atributos

		#region Constructores
		public ExpedienteService()
		{

		}
		public ExpedienteService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db		= db;
			expediente	= new Berke.DG.DBTab.Expediente(db);
			view = new Berke.DG.ViewTab.vExpeService(db);
			usuarioID = BoletinService.BOL_FUNCID;
		}
		#endregion Constructores

		#region setters
		/// <summary>
		/// Setea el ID de Expediente
		/// </summary>
		/// <param name="expeID">ID. de Expediente</param>
		public void setExpeID(int expeID)
		{
			this.expeID = expeID;
			expediente	= new Berke.DG.DBTab.Expediente(db);
			expediente.Adapter.ReadByID(expeID);
		}
		/// <summary>
		/// Setea el Expediente
		/// </summary>
		/// <param name="expe">Expediente</param>
		public void setExpediente(Berke.DG.DBTab.Expediente expe) 
		{
			this.expeID = expe.Dat.ID.AsInt;
			this.expediente = expe;
		}
		/// <summary>
		/// Setea el usuario con el que se registrarán 
		/// las acciones realizadas.
		/// </summary>
		public void setUsuarioID(int usuarioID)
		{
			this.usuarioID = usuarioID;
		}
		#endregion setters

		#region ObtenerActaNro
		public int ObtenerActaNro(string NumAnioActa)
		{
			int nro=0,acta;
			string NumActa;
			NumAnioActa=NumAnioActa.Trim();
			acta=Berke.Libs.Base.ObjConvert.AsInt(NumAnioActa);
			
			if (acta<MAX_ACTA)
			{
				nro=acta;
			}
			else
			{
				if (NumAnioActa.Length == 6)
				{
					NumActa=NumAnioActa.Substring(1,5);
					
				}
				else if (NumAnioActa.Length == 7)
				{
					/*if (NumAnioActa.Substring(NumAnioActa.Length,1) == 9)
					{
						NumActa = NumAnioActa.Substring(
					}
					else
					{*/
						NumActa= NumAnioActa.Substring(2,5);
					//}
				}
                else if (NumAnioActa.Length == 8)
                {
                    NumActa = NumAnioActa.Substring(2, 6);
                }
                else if (NumAnioActa.Length == 9)
                {
                    NumActa = NumAnioActa.Substring(2, 7);
                }
				else
				{
					NumActa=NumAnioActa.Substring(1,5);
				}
				
				/*if (NumAnioActa.Substring(0,1)=="9")
				{
					NumActa=NumAnioActa.Substring(2,NumAnioActa.Length -2);
				}
				else
				{
					NumActa=NumAnioActa.Substring(1,NumAnioActa.Length -1);
				}*/
				nro=Berke.Libs.Base.ObjConvert.AsInt(NumActa);
			}
			return nro;
		}
		#endregion ObtenerActaNro

		#region ObtenerActaAnio
		public int ObtenerActaAnio(string NumAnioActa)
		{
			int anio=0,acta;
			string NumAnio;
			NumAnioActa=NumAnioActa.Trim();
			acta=Berke.Libs.Base.ObjConvert.AsInt(NumAnioActa);
			if (acta<MAX_ACTA)
			{
				anio=2000;
			}
			else
			{
				if (NumAnioActa.Length == 6)
				{
					NumAnio=NumAnioActa.Substring(0,1);
					anio=Berke.Libs.Base.ObjConvert.AsInt(NumAnio)+2000;
				}
				else if (NumAnioActa.Length > 6)
				{
					int len = NumAnioActa.Length - 5;

                    if (NumAnioActa.Length == 8)
                    {
                        len = NumAnioActa.Length - 6;
                    }
                    else if (NumAnioActa.Length == 9)
                    {
                        len = NumAnioActa.Length - 7;
                    }

					NumAnio=NumAnioActa.Substring(0,len);
					if (Berke.Libs.Base.ObjConvert.AsInt(NumAnio) > 90)
					{
						anio=Berke.Libs.Base.ObjConvert.AsInt(NumAnio)+1900;
					}
					else
					{
						anio=Berke.Libs.Base.ObjConvert.AsInt(NumAnio)+2000;
					}
				}

				/*if (NumAnioActa.Substring(0,1)=="9")
				{
					NumAnio=NumAnioActa.Substring(0,2);
					anio=Berke.Libs.Base.ObjConvert.AsInt(NumAnio)+1900;
				}
				else
				{
					NumAnio=NumAnioActa.Substring(0,1);
					anio=Berke.Libs.Base.ObjConvert.AsInt(NumAnio)+2000;
				}*/
			}
			return anio;
		}
		#endregion ObtenerActaAnio

		#region Obtener Expediente
		public Berke.DG.DBTab.Expediente getExpediente()
		{
			return expediente;
		}

		public Berke.DG.DBTab.Expediente getExpediente( int ID )
		{
			expediente.Adapter.ReadByID(ID);
			return expediente;
		}

		/// <summary>
		/// Obtiene el Expediente Padre del expediente actual. 
		/// Retorna Null si no existe.
		/// </summary>
		/// <returns>Retorna Null si no existe</returns>
		public Berke.DG.DBTab.Expediente getExpedientePadre()
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			if (!expediente.Dat.ExpedienteID.IsNull && expediente.Dat.ExpedienteID.AsInt>0)
			{
				expe.Adapter.ReadByID(expediente.Dat.ExpedienteID.AsInt);
				if (expe.RowCount>0) return expe;
			}
			return null;
		}
		public Berke.DG.DBTab.Expediente getExpediente(int actanro, int actaanio)
		{
			if (actanro>0 && actaanio>0)
			{

				expediente.ClearFilter();
				expediente.Dat.ActaNro.Filter  = actanro;
				expediente.Dat.ActaAnio.Filter = actaanio;
				try
				{
					expediente.Adapter.ReadAll();
				}
				catch(Exception ex)
				{
					throw new Exceptions.BolImportException("Error al leer ActaNro: "+actanro+ " ActaAño: " + actaanio + ". Detalle:"+ex.Message);							
				}
				return expediente;
			}
			else 
			{
				expediente = new Berke.DG.DBTab.Expediente(db);
				return expediente;
			}
		}

		public Berke.DG.DBTab.Expediente getExpediente(int actanro, int actaanio, int tramiteID)
		{
			if (actanro>0 && actaanio>0)
			{

				expediente.ClearFilter();
				expediente.Dat.ActaNro.Filter  = actanro;
				expediente.Dat.ActaAnio.Filter = actaanio;
				expediente.Dat.TramiteID.Filter = tramiteID;
				try
				{
					expediente.Adapter.ReadAll();
				}
				catch(Exception ex)
				{
					throw new Exceptions.BolImportException("Error al leer ActaNro: "+actanro+ " ActaAño: " + actaanio + ". Detalle:"+ex.Message);							
				}
				return expediente;
			}
			else 
			{
				expediente = new Berke.DG.DBTab.Expediente(db);
				return expediente;
			}
		}
		#endregion Obtener Expediente

		#region Propietario Actual
		/// <summary>
		/// Agrega el nombre del Propietario actual a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Nombre del propietario actual</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioActualNombre(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE, valor, expeID);   
		}
		/// <summary>
		/// Agrega el pais del Propietario actual a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Pais del propietario actual</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioActualPais(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS, valor, expeID);  
		}

		/// <summary>
		/// Agrega la Dirección del Propietario actual a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Dirección del propietario actual</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioActualDireccion(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR, valor, expeID);   
		}
		/// <summary>
		/// Agrega todos los campos de propietario Actual
		/// </summary>
		/// <param name="prop">Propietario Actual</param>
		/// <param name="expeID">ID. del Expediente</param>
		public void addCampoPropietarioActual(Berke.DG.DBTab.Propietario prop, int expeID)
		{
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
			pais.Adapter.ReadByID(prop.Dat.PaisID.AsInt);
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS, pais.Dat.paisalfa.AsString, expeID);   
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR, prop.Dat.Direccion.AsString, expeID);   
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE, prop.Dat.Nombre.AsString, expeID);   
		}

		/// <summary>
		/// Elimina el nombre del Propietario actual de ExpedienteCampo
		/// </summary>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void delCampoPropietarioActualNombre( int expeID)
		{
			this.delCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE, expeID);   
		}
		/// <summary>
		/// Elimina el pais del Propietario actual de ExpedienteCampo
		/// </summary>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void delCampoPropietarioActualPais(int expeID)
		{
			this.delCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS, expeID);  
		}
		/// <summary>
		/// Elimina la Dirección del Propietario actual de ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Dirección del propietario actual</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void delCampoPropietarioActualDireccion(int expeID)
		{
			this.delCampo(Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR, expeID);   
		}
		/// <summary>
		/// Elimina los campos de Propietario Actual de ExpedienteCampo
		/// </summary>
		/// <param name="expeID">ID. del Expediente</param>
		public void delCampoPropietarioActual(int expeID)
		{
			this.delCampoPropietarioActualDireccion(expeID);
			this.delCampoPropietarioActualNombre(expeID);
			this.delCampoPropietarioActualPais(expeID);
		}
		#endregion Propietario Actual

		#region Propietario Anterior
		/// <summary>
		/// Agrega el nombre del Propietario anterior a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Nombre del propietario anterior</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioAnteriorNombre(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE, valor, expeID);   //nvarchar Oblig.
		}
		/// <summary>
		/// Agrega el pais del Propietario anterior a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Pais del propietario anterior</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioAnteriorPais(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS, valor, expeID);   //nvarchar Oblig.
		}

		/// <summary>
		/// Agrega la Dirección del Propietario anterior a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Dirección del propietario anterior</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioAnteriorDireccion(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR, valor, expeID);   //nvarchar Oblig.
		}

		/// <summary>
		/// Agrega IDs de Propietarios anteriores de una marca a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> ID. de la marca</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPropietarioAnteriorID(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_ID, valor, expeID);   //nvarchar Oblig.
		}
		#endregion Propietario Anterior

		#region Poder Anterior
		/// <summary>
		/// Agrega el ID. del Poder anterior anterior a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> ID. Poder anterior</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoPoderAnteriorID(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.POD_ANTERIOR_ID, valor, expeID); 
		}
		/// <summary>
		/// Agrega todos los campos del Poder anterior anterior a ExpedienteCampo
		/// </summary>
		/// <param name="poder"> Poder del trámite </param>
		/// <param name="expeID">ID. del Expediente</param>
		///	
		public void addCampoPoderAnterior(Berke.DG.DBTab.Poder poder, int expeID)
		{
			this.addCampoPoderAnteriorID(poder.Dat.ID.AsString,expeID);
		}
		#endregion Poder Anterior

		#region Licenciatario
		/// <summary>
		/// Agrega el Nombre del Licenciatario a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Nombre del Licenciatario</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoLicenciatarioNombre(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.LIC_NOMBRE, valor, expeID);  
		}

		/// <summary>
		/// Agrega la Dirección del Licenciatario a ExpedienteCampo
		/// </summary>
		/// <param name="valor"> Dirección del Licenciatario</param>
		/// <param name="expeID"> ID. del expediente</param>
		///	
		public void addCampoLicenciatarioDireccion(string valor, int expeID)
		{
			this.addCampo(Berke.Libs.Base.GlobalConst.LIC_DIRECCION, valor, expeID);  
		}
		#endregion Licenciatario

		#region Agregar a ExpedienteCampo
		public void addCampo(string campo, string valor, int expeID)
		{
			Berke.DG.DBTab.ExpedienteCampo expeC = new Berke.DG.DBTab.ExpedienteCampo(db);
			expeC.NewRow(); 
			expeC.Dat.ID			.Value = DBNull.Value;   //int PK  Oblig.
			expeC.Dat.ExpedienteID	.Value = expeID;   //int Oblig.
			expeC.Dat.Campo			.Value = campo; //nvarchar Oblig.
			expeC.Dat.Valor			.Value = Utils.cambiarCaracteresEspeciales(valor);
			expeC.Dat.Migrado		.Value = DBNull.Value;   //bit
			expeC.PostNewRow();
			expeC.Adapter.InsertRow();
		}
		#endregion Agregar a ExpedienteCapo

		#region Elimina de ExpedienteCampo
		/// <summary>
		/// Elimina un campo de ExpedienteCampo
		/// </summary>
		/// <param name="campo">Campo</param>
		/// <param name="expeID">ID. Expediente</param>
		public void delCampo(string campo, int expeID)
		{
			Berke.DG.DBTab.ExpedienteCampo expeC = new Berke.DG.DBTab.ExpedienteCampo(db); 
			expeC.Dat.ExpedienteID	.Filter = expeID;   //int Oblig.
			expeC.Dat.Campo			.Filter = campo; //nvarchar Oblig.
			expeC.Adapter.ReadAll();
			for (expeC.GoTop(); ! expeC.EOF; expeC.Skip())
			{
				expeC.Delete();
				expeC.Adapter.DeleteRow();
			}
		}
		#endregion Eliminar ExpedienteCapo

		#region Obtener ExpedienteCampo
		/// <summary>
		/// Obtiene todos los campos asociados al expediente
		/// </summary>
		/// <param name="expeID">ID. del Expediente en cuestión</param>
		/// <returns>Lista de campos asociados en ExpedienteCampo</returns>
		public Berke.DG.DBTab.ExpedienteCampo getCampos(int expeID)
		{
			Berke.DG.DBTab.ExpedienteCampo expeC = new Berke.DG.DBTab.ExpedienteCampo(db);
			expeC.Dat.ExpedienteID.Filter = expeID;
			expeC.Adapter.ReadAll();
			return expeC;
		}
		#endregion Obtener ExpedienteCampo

		#region Agregar Situacion
		/// <summary>
		/// Agrega Situaciones al expediente
		/// </summary>
		/// <param name="sitTramite"> Id. tramideSitID</param>
		/// <param name="solicitudFecha">Fecha de solicitud</param>
		/// <param name="tramiteSitID">Segundo parámetro para Calc.SitFechaVencim. (no se sabe exactamente
		///  que representa)</param>
		/// <param name="expeID">Id. del Expediente.</param>
		/// <returns>El ID. del ExpedienteSituación creado</returns>
		public int addSituacion(int sitTramite, DateTime solicitudFecha, int tramiteSitID, int expeID)
		{
			
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			expeSit.NewRow(); 
			expeSit.Dat.ID						.Value = DBNull.Value;   //int PK  Oblig.
			expeSit.Dat.ExpedienteID			.Value = expeID;   //int Oblig.
			expeSit.Dat.TramiteSitID			.Value = sitTramite;   //int Oblig.
			expeSit.Dat.AltaFecha				.Value = System.DateTime.Now.Date;   //datetime Oblig.
			expeSit.Dat.SituacionFecha			.Value = solicitudFecha;   //datetime Oblig.
			expeSit.Dat.VencimientoFecha		.Value = Berke.Libs.Base.Helpers.Calc.SitFechaVencim(solicitudFecha,tramiteSitID,db);   //datetime
			expeSit.Dat.FuncionarioID			.Value = usuarioID;   //int Oblig.
			expeSit.Dat.Obs						.Value = DBNull.Value;   //nvarchar
			expeSit.Dat.Datos					.Value = DBNull.Value;   //nvarchar
			//expedientesit.Dat.VencimientoFecha.Value = Calc.SitFechaVencim(DateTime.Today,1);
			expeSit.PostNewRow();
			int expeSitID = expeSit.Adapter.InsertRow();
			return expeSitID;								
		}


		/// <summary>
		/// Agrega la situación desistida al trámite
		/// </summary>
		/// <param name="tramiteID">ID. del trámite</param>
		/// <param name="solicitudFecha">Fecha de la solicitud</param>
		/// <param name="obs">Observacion</param>
		/// <param name="expeID">ID. del Expediente</param>
		/// <returns>ID. de la entrada generada en Expediente_sit</returns>
		public int addSituacionDesistida(int tramiteID, DateTime solicitudFecha, string obs, int expeID)
		{
			return addSituacion(tramiteID,(int)GlobalConst.Situaciones.DESISTIDA,solicitudFecha, obs,expeID);
		}

		/// <summary>
		/// Agrega la situación cancelada al trámite
		/// </summary>
		/// <param name="tramiteID">ID. del trámite</param>
		/// <param name="solicitudFecha">Fecha de la solicitud</param>
		/// <param name="obs">Observacion</param>
		/// <param name="expeID">ID. del Expediente</param>
		/// <returns>ID. de la entrada generada en Expediente_sit</returns>
		public int addSituacionCancelada(int tramiteID, DateTime solicitudFecha, string obs, int expeID)
		{	
			if ((RegistroService.TIPO_TRAMITE == tramiteID) || (RenovacionService.TIPO_TRAMITE == tramiteID) )
			{
				return addSituacion(tramiteID,(int)GlobalConst.Situaciones.CANCELACION_REG,solicitudFecha, obs,expeID);
			}
			else 
			{
				return addSituacion(tramiteID,(int)GlobalConst.Situaciones.CANCELACION_TV,solicitudFecha, obs,expeID);
			}
		}


		/// <summary>
		/// Agrega una situación a un trámite
		/// </summary>
		/// <param name="tramiteID">ID. del trámite</param>
		/// <param name="situacionID">ID. de la Situación</param>
		/// <param name="solicitudFecha">Fecha de la solicitud</param>
		/// <param name="obs">Observacion</param>
		/// <param name="expeID">ID. del Expediente</param>
		/// <returns>ID. de la entrada generada en Expediente_sit</returns>
		public int addSituacion(int tramiteID, int situacionID, DateTime solicitudFecha, string obs,int expeID)
		{
			int tramiteSitID = this.getTramiteSitID(situacionID,tramiteID);
			if (tramiteSitID <0)
			{
				throw new Exception("La situación "+ situacionID+ " no esta habilitada para el trámite "+ tramiteID);
			}
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			expeSit.NewRow(); 
			expeSit.Dat.ID						.Value = DBNull.Value;   //int PK  Oblig.
			expeSit.Dat.ExpedienteID			.Value = expeID;   //int Oblig.
			expeSit.Dat.TramiteSitID			.Value = tramiteSitID;   //int Oblig.
			expeSit.Dat.AltaFecha				.Value = System.DateTime.Now.Date;   //datetime Oblig.
			expeSit.Dat.SituacionFecha			.Value = solicitudFecha;   //datetime Oblig.
			expeSit.Dat.VencimientoFecha		.Value = Berke.Libs.Base.Helpers.Calc.SitFechaVencim(solicitudFecha,tramiteSitID,db);   //datetime
			expeSit.Dat.FuncionarioID			.Value = usuarioID;   //int Oblig.
			expeSit.Dat.Obs						.Value = obs;   //nvarchar
			expeSit.Dat.Datos					.Value = DBNull.Value;   //nvarchar
			//expedientesit.Dat.VencimientoFecha.Value = Calc.SitFechaVencim(DateTime.Today,1);
			expeSit.PostNewRow();
			int expeSitID = expeSit.Adapter.InsertRow();
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);

			#region Seteamos como última situación del expediente
			expe.Adapter.ReadByID(expeID);
			expe.Edit();
			expe.Dat.TramiteSitID.Value = tramiteSitID;
			expe.PostEdit();
			expe.Adapter.UpdateRow();
			#endregion Seteamos como última situación del expediente

			return expeSitID;
		}
		#endregion Agregar Situacion

		#region Obtener Situacion
		/// <summary>
		/// Obtiene la situaciones asociadas a un expediente
		/// </summary>
		/// <param name="expeID">ID. del expediente en cuestión</param>
		/// <returns>Lista de expedientes asociados en Expediente_situacion</returns>
		public Berke.DG.DBTab.Expediente_Situacion getSituaciones(int expeID)
		{
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			expeSit.Dat.ExpedienteID.Filter = expeID;
			expeSit.Adapter.ReadAll();
			return expeSit;
		}

		/// <summary>
		/// Obtiene la situaciones asociadas a un expediente
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="situacionID">ID. de Situación</param>
		/// <returns>Lista de expedientes asociados en Expediente_situacion</returns>
		public Berke.DG.DBTab.Expediente_Situacion getSituacion(Berke.DG.DBTab.Expediente expe, int situacionID)
		{
			int tramiteSitID = this.getTramiteSitID(situacionID, expe.Dat.TramiteID.AsInt);
			if (tramiteSitID <0)  
			{
				throw new Exception("La situación "+ situacionID+ " no esta habilitada para el trámite "+ expe.Dat.TramiteID.AsInt);
			}
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			expeSit.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expeSit.Dat.TramiteSitID.Filter = tramiteSitID;
			if (situacionID == (int)GlobalConst.Situaciones.PUBLICADA) { expeSit.Dat.SituacionFecha.Order = -1; }
			expeSit.Adapter.ReadAll();
			return expeSit;
		}

		public Berke.DG.DBTab.Expediente_Situacion getSituacion(Berke.DG.DBTab.Expediente expe, ArrayList situacionList)
		{
			ArrayList tramiteSitIDList = this.getTramiteSitID(situacionList, expe.Dat.TramiteID.AsInt);
			if (tramiteSitIDList.Count == 0)  
			{
				string str_situacionList = "";
				for (int i= 0; i<situacionList.Count; i++)
				{
					str_situacionList+="["+situacionList[i].ToString()+"]";
				}
				throw new Exception("La situaciones "+str_situacionList+" no estan habilitadas para el trámite "+ expe.Dat.TramiteID.AsInt);
			}
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			expeSit.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expeSit.Dat.TramiteSitID.Filter = new DSFilter(tramiteSitIDList);
			expeSit.Adapter.ReadAll();
			return expeSit;
		}
		#endregion Obtener Situacion

		public int getTramiteSitID(int situacionID, int tramiteID) 
		{
			Berke.DG.DBTab.Tramite_Sit ts = new Berke.DG.DBTab.Tramite_Sit(db);
			ts.Dat.SituacionID.Filter = situacionID;
			ts.Dat.TramiteID.Filter   = tramiteID;
			ts.Adapter.ReadAll();
			if (ts.RowCount == 0) return -1;
			return ts.Dat.ID.AsInt;
		}
		public ArrayList getTramiteSitID(ArrayList situacionList, int tramiteID) 
		{
			Berke.DG.DBTab.Tramite_Sit ts = new Berke.DG.DBTab.Tramite_Sit(db);
			ts.Dat.SituacionID.Filter = new DSFilter (situacionList);
			ts.Dat.TramiteID.Filter   = tramiteID;
			ts.Adapter.ReadAll();
			ArrayList arr = new ArrayList();
			for (ts.GoTop(); ! ts.EOF; ts.Skip())
			{
				arr.Add(ts.Dat.ID.AsString);
			}
			return arr;
		}

		#region Agregar Instruccion Renovado x Otro
		public int addInstruccionRenxOtro(int marcaID, int expeID, string obs)
		{
			Berke.DG.DBTab.Expediente_Instruccion expeInst = new Berke.DG.DBTab.Expediente_Instruccion(db);
			expeInst.NewRow();
			expeInst.Dat.InstruccionTipoID.Value	= INSTR_RENXOTRO;//RENOVADO X OTRO 
			expeInst.Dat.MarcaID		.Value		= marcaID;   //int Oblig.
			expeInst.Dat.ExpedienteID	.Value		= expeID;   //int Oblig.
			expeInst.Dat.Fecha			.Value		= System.DateTime.Now.Date;   //smalldatetime
			expeInst.Dat.Obs			.Value		= obs;
			expeInst.Dat.FuncionarioID	.Value		= BoletinService.BOL_FUNCID;
			expeInst.PostNewRow();
			int expeInstID = expeInst.Adapter.InsertRow();
			return expeInstID;

		}
		#endregion Agregar Instruccion Renovado x Otro

		#region Actualizar el expediente renovado por otro con el ID cliente anterior

		public void updateClienteIDRXO(int expeID, int ClienteID)
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			expe.ClearFilter();
			expe.Adapter.ReadByID(expeID);
			expe.Edit();
			expe.Dat.ClienteID.Value = ClienteID;
			expe.PostEdit();
			expe.Adapter.UpdateRow();
		}

		#endregion Actualizar el expediente renovado por otro con el ID cliente anterior


		#region Obtener Instrucciones
		/// <summary>
		/// Obtiene las instrucciones asociadas a un expediente
		/// </summary>
		/// <param name="expeID">ID. del Expediente en cuestión</param>
		/// <returns>Lista de instrucciones asociadas en Expediente_Instrucción</returns>
		public Berke.DG.DBTab.Expediente_Instruccion getInstrucciones(int expeID)
		{
			Berke.DG.DBTab.Expediente_Instruccion expeInst = new Berke.DG.DBTab.Expediente_Instruccion(db);
			expeInst.Dat.ExpedienteID.Filter = expeID;
			expeInst.Adapter.ReadAll();
			return expeInst;
		}
		#endregion Obtener Instrucciones

		#region Asociar Marca y MarcaRegRen
		/// <summary>
		/// Engancha el la Marca y MarcaRegRen al Expediente.
		/// </summary>
		/// <param name="marcaID">Id. de Marca</param>
		/// <param name="regrenID">Id. de MarcaRegRen</param>
		public void asociarMarcaRegistro(int marcaID, int regrenID)
		{
			expediente.Edit();
			expediente.Dat.MarcaID.Value		= marcaID;
			expediente.Dat.MarcaRegRenID.Value	= regrenID;
			expediente.PostEdit();
			expediente.Adapter.UpdateRow();
		}
		#endregion Asociar Marca y MarcaRegRen

		#region Obtener Expediente Padre
		/// <summary>Obtiene información del Expediente padre para el trámite
		/// especificado en bol, a partir del registro o acta de referencia (en 
		/// ese orden). </summary>
		/// <returns>Retorna el expediente padre Berke.DG.DBTab.Expediente.
		/// Si no posee información de reg/ren padre entonces se retorna null
		/// </returns>
		///		
		public Berke.DG.DBTab.Expediente getExpedientePadre(Berke.DG.DBTab.BoletinDet bol)
		{
			// Verificamos a partir del Nro. de registro de referencia
			expediente.ClearFilter();
			MarcaRegRenService regrenServ	  = new MarcaRegRenService(db);
			Berke.DG.DBTab.MarcaRegRen regren = regrenServ.getRegRen(bol.Dat.RefRegNro.AsInt);
			Berke.DG.DBTab.Marca marca		  = new Berke.DG.DBTab.Marca(db);
			if (regren.RowCount > 0) 
			{
				expediente.Adapter.ReadByID(regren.Dat.ExpedienteID.AsInt);			
				return expediente;
			} 
			// Verificamos a partir del acta de referencia
			if ( (bol.Dat.RefNro.IsNull)  || (bol.Dat.RefNro.AsInt == 0) ||
				(bol.Dat.RefAnio.IsNull) || (bol.Dat.RefAnio.AsInt== 0)) 
			{
				return null;
			}

			expediente.Dat.ActaNro.Filter	= bol.Dat.RefNro.AsInt;
			expediente.Dat.ActaAnio.Filter	= bol.Dat.RefAnio.AsInt;			
			expediente.Adapter.ReadAll();

			// Si existen actas o registros duplicados, se debe
			// considerar el que corresponde a la marca propia										
			if (expediente.RowCount > 1) 
			{
				for (expediente.GoTop(); ! expediente.EOF; expediente.Skip()) 
				{
					marca.Adapter.ReadByID( expediente.Dat.MarcaID.AsInt );
					if (marca.Dat.Nuestra.AsBoolean) 
					{
						break;
					}
				}
				if (expediente.EOF) expediente.GoTop();
			}
			else if (expediente.RowCount == 0)
			{
				return null;
			}
			return expediente;
			
		}
		#endregion Obtener Expediente Padre

		#region Obtener Registro o Renovacion posterior
		/// <summary>
		/// Obtiene un trámite de Registro o Renovacion posterior.
		/// </summary>
		/// <returns>El ID del trámite posterior si existe, caso contrario el 
		/// ID del trámite actual .</returns>
		public Berke.DG.DBTab.Expediente getExpeRenRegPosterior()
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			/* En caso en que exista alguna renovación posterior que aún no 
			 * fuese concedida, se debe tomar ese expediente como
			 * el expediente padre */
			expe.ClearFilter();
			expe.Dat.ExpedienteID.Filter = this.expeID;
			expe.Dat.TramiteID.Filter    = ObjConvert.GetFilter("1,2");
			expe.Adapter.ReadAll();
			for (expe.GoTop(); !expe.EOF; expe.Skip()) 
			{				
				/* Reasignar el expediente padre de manera a que apunte al último
													* registro o renovación que se encuentre en trámite */
				if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) |
					(expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) ) 
				{
					return  expe;											
					
				}
			}
			//expe.Adapter.ReadByID(this.expeID);
			//return expe;
			return expediente;

		}
		#endregion Obtener Registro o Renovacion posterior

		#region Trámites hijos
		public Berke.DG.DBTab.Expediente getTramites(int expeID)
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			expe.Dat.ExpedienteID.Filter = expeID;
			expe.Adapter.ReadAll();
			return expe;
		}

	
		/// <summary>
		/// Verifica si el expediente indicado posee trámites hijos
		/// </summary>
		/// <param name="expeID">ID. del expediente</param>
		/// <returns>True si el expediente posee trámites hijos.</returns>
		public bool tieneTramitesHijos(int expeID)
		{
			Berke.DG.DBTab.Expediente expe = this.getTramites(expeID);
			return (expe.RowCount >0);
		}
		/// <summary>
		/// Retorna el primero o último tramite del tipo tramiteID, que corresponde
		/// a un expediente.
		/// </summary>
		/// <param name="expeID">Id. Expediente</param>
		/// <param name="tramiteID">Id. Trámite</param>
		/// <param name="first">TRUE para el primero, FALSE para el último</param>
		/// <returns></returns>
		public Berke.DG.DBTab.Expediente getTramite(int expeID, int tramiteID, bool first)
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			if (first)
			{
				expe.Dat.ActaAnio.Order = 1;
				expe.Dat.ActaNro.Order  = 2;
			} 
			else 
			{
				expe.Dat.ActaAnio.Order = -1;
				expe.Dat.ActaNro.Order  = -2;
			}
			expe.Dat.ExpedienteID.Filter = expeID;
			expe.Dat.TramiteID.Filter = tramiteID;
			expe.Adapter.ReadAll();
			return expe;
		}
		#endregion Trámites hijos

		#region Obtener sustituidas
		/// <summary>
		/// Obtiene las marcas sustituidas que aún se encuentran en situacion HI
		/// </summary>
		/// <param name="ag">Nro. de matricula del Agente Local</param>
		/// <param name="clase">Nro. de clase</param>
		/// <param name="marcatipo">Tipo de marca</param>
		/// <returns>Lista de marcas sustituidas</returns>
		public Berke.DG.ViewTab.vExpeService getSustituidas(string ag, string clase, string marcatipo)
		{
			#region Setear where
			string def_where = view.Adapter.DefaultWhere;
			string bool_sep = "";
			if (def_where.Length>0)
			{
				bool_sep = " AND ";
			}
			string where = def_where + bool_sep + " e.actanro is null AND e.actaanio is null ";
			view.Adapter.SetDefaultWhere(where);
			#endregion Setear where

			view.Dat.sustituida.Filter = true;
			view.Dat.agenteLocalMatric.Filter = ag.Trim();
			//view.Dat.claseID.Filter = MarcaService.getClaseVigente(clase,db).Dat.ID.AsInt;
			view.Dat.claseNro.Filter = clase;
			view.Dat.marctipoAbrev.Filter = marcatipo.Trim();
			view.Adapter.ReadAll();
			view.Adapter.SetDefaultWhere(def_where);
			return view;
				
		}
		#endregion Obtener sustituidas

		#region Agregar Propietario
		/// <summary>
		/// Agrega un propietario a ExpedientexPropietario
		/// </summary>
		/// <param name="propID">ID. Propietario</param>
		/// <param name="derechoPropio">Si el propietario tiene derecho propio</param>
		public void addPropietario(int propID, bool derechoPropio)
		{
			Berke.DG.DBTab.ExpedienteXPropietario expeProp = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			expeProp.NewRow();
			expeProp.Dat.ExpedienteID.Value  = this.expeID;
			expeProp.Dat.PropietarioID.Value = propID;
			expeProp.Dat.DerechoPropio.Value = derechoPropio;
			expeProp.PostNewRow();
			expeProp.Adapter.InsertRow();
		}
		#endregion Agregar Propietario

		#region Delete Propietarios
		public int delPropietarios()
		{
			Berke.DG.DBTab.ExpedienteXPropietario expeProp = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			expeProp.Dat.ExpedienteID.Filter = expeID;
			expeProp.Adapter.ReadAll();
			int ndel = 0;
			for(expeProp.GoTop(); !expeProp.EOF; expeProp.Skip())
			{
				expeProp.Delete();
				expeProp.Adapter.DeleteRow();
				ndel++;
			}
			return ndel;
		}
		#endregion Delete Propietarios

		#region Obtener Propietarios
		/*
		public Berke.DG.DBTab.Propietario getPropietarios()
		{
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
			return prop;
		}
		*/
		
		/// <summary>
		/// Obtiene la lista de PropietariosID asociados al expediente
		/// </summary>
		/// <returns>Lista de propietarios ID en ExpedienteXPropietario</returns>
		public Berke.DG.DBTab.ExpedienteXPropietario getPropietariosID()
		{
			Berke.DG.DBTab.ExpedienteXPropietario expeProp = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			expeProp.Dat.ExpedienteID.Filter = expeID;
			expeProp.Adapter.ReadAll();
			return expeProp;
		}
		#endregion Obtener Propietarios

		#region Obtener Poder
		/// <summary>
		/// Obtiene el poder de un expediente. Se asume un solo poder
		/// por expediente.
		/// </summary>
		/// <returns>Poder del Trámite</returns>
		public Berke.DG.DBTab.Poder getPoder()
		{
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder(db);
			Berke.DG.DBTab.ExpedienteXPoder expePod = new Berke.DG.DBTab.ExpedienteXPoder(db);
			expePod.Dat.ExpedienteID.Filter = this.expeID;
			expePod.Adapter.ReadAll();
			if (expePod.RowCount==0)
			{
				return poder;
			}
			poder.Adapter.ReadByID(expePod.Dat.PoderID.AsInt);
			return poder;
		}
		#endregion Obtener Poder

		#region Agregar Poder
		public void addPoder(int poderID)
		{
			Berke.DG.DBTab.ExpedienteXPoder expePod = new Berke.DG.DBTab.ExpedienteXPoder(db);
			expePod.NewRow();
			expePod.Dat.ExpedienteID.Value = this.expeID;
			expePod.Dat.PoderID.Value      = poderID;
			expePod.PostNewRow();
			expePod.Adapter.InsertRow();
		}
		#endregion Agregar Poder

		#region Delete Poder
		public void delPoder()
		{
			Berke.DG.DBTab.ExpedienteXPoder expePod = new Berke.DG.DBTab.ExpedienteXPoder(db);
			expePod.Dat.ExpedienteID.Filter = this.expeID;
			expePod.Adapter.ReadAll();
			expePod.Delete();
			expePod.Adapter.DeleteRow();
		}
		#endregion Delete Poder

		#region Esta en trámite?
		public bool tieneRegistro()
		{
			return ! getMarcaRegRen().Dat.RegistroNro.IsNull;
		}
		public bool  sinRegistro()
		{
			return ! tieneRegistro();
		}
		#endregion Esta en trámite?

		#region MarcaRegRen
		public Berke.DG.DBTab.MarcaRegRen getMarcaRegRen()
		{
			Berke.DG.DBTab.MarcaRegRen mr = new Berke.DG.DBTab.MarcaRegRen(db);
			mr.Adapter.ReadByID(expediente.Dat.MarcaRegRenID.AsInt);
			return mr;
		}
		#endregion MarcaRegRen

		#region Pasar Inactivo Registro
		public void pasarInactivoRegistro()
		{
			Berke.DG.DBTab.MarcaRegRen regren = this.getMarcaRegRen();
			regren.Edit();
			regren.Dat.Vigente.Value = false;
			regren.PostEdit();
			regren.Adapter.UpdateRow();
		}
		#endregion Pasar Inactivo Registro

		#region Pasar Activo Registro
		public void pasarActivoRegistro()
		{
			Berke.DG.DBTab.MarcaRegRen regrenPadre;
			Berke.DG.DBTab.MarcaRegRen regren = this.getMarcaRegRen();

			// Hoy menos PERIODO_GRACIA
			DateTime todaym6m = System.DateTime.Now;
			todaym6m = todaym6m.AddMonths(-VENC_PERIODO_GRACIA);
			regrenPadre = this.getMarcaRegRen();

			// Debería estar vigente la marca?
			if (!regrenPadre.Dat.VencimientoFecha.IsNull &&
				(regrenPadre.Dat.VencimientoFecha.AsDateTime >= todaym6m))
			{
				regren.Edit();
				regren.Dat.Vigente.Value = true;
				regren.PostEdit();
				regren.Adapter.UpdateRow();
			}
		}
		#endregion Pasar Inactivo Registro


		#region Actualizar desde campos
		/// <summary>
		/// Actualiza los datos de propietario del Expediente
		/// a partir de los campos prop_anterior_id and 
		/// poder_anterior_id
		/// </summary>
		/// <param name="campos"></param>
		/// <returns> Retorna 0 si se actualizó con éxito.</returns>
		public int updateFromCampos(Berke.DG.DBTab.ExpedienteCampo campos)
		{
			string propID   = "";
			string poderID  = "";
			for (campos.GoTop(); !campos.EOF; campos.Skip())
			{
				string campo = campos.Dat.Campo.AsString;
				string valor = campos.Dat.Valor.AsString;

				if (campo == GlobalConst.PROP_ANTERIOR_ID)
				{
					propID = valor;
				}
				else if(campo == GlobalConst.POD_ANTERIOR_ID)
				{
					poderID = valor;
				}				
			}

			// Debo Actualizar el propietario?

			bool derechoPropio = poderID.Trim().Length==0;

			if (!derechoPropio)
			{
				Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder(db);
				poder.Adapter.ReadByID(Convert.ToInt32(poderID));
				if (poder.RowCount==0)
				{
					throw new Exception("No pudo restaurarse Poder "+ poderID);
				}
				this.delPoder();
				this.addPoder(poder.Dat.ID.AsInt);
				this.delPropietarios();
				this.addPropietariosByPoder(poder.Dat.ID.AsInt);
			}
			else if (propID.Trim().Length>0)
			{
				this.delPropietarios();
				string [] apropID = propID.Split(",".ToCharArray());
				for (int i=0; i<apropID.Length; i++)
				{
					if ( apropID[i] != ""  )
					{
						this.addPropietario(Convert.ToInt32(apropID[i]), derechoPropio);
						
					}
				}
				
			}
			else 
			{
				return -1;
			}

			return 0;

		}
		#endregion Actualizar desde campos

		#region Actualizar desde campos # 2
		public int updateFromXXX(Berke.DG.DBTab.Expediente expeTV)
		{
			
			Berke.DG.DBTab.ExpedienteXPropietario expXProp = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			Berke.DG.DBTab.ExpedienteXPoder       expXPoder = new Berke.DG.DBTab.ExpedienteXPoder(db);
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder(db);
			
			/*Obtener Propietario del Exp de TV */
			expXProp.Dat.ExpedienteID.Filter = expeTV.Dat.ID.AsInt;
			expXProp.Adapter.ReadAll();

			if ( expXProp.RowCount == 0 ) {
                throw new Exception("El expediente no tiene asociado los propietarios [ExpedienteXPropietario] ");
			}
		
			
			// Debo Actualizar el propietario?
			bool derechoPropio = expXProp.Dat.DerechoPropio.AsBoolean;
			if (!derechoPropio)
			{
				/*Obtener poder del Expediente de TV */	
				#region Obtener Poderl Expediente de TV
				expXPoder.Dat.ExpedienteID.Filter = expeTV.Dat.ID.AsInt;
				expXPoder.Adapter.ReadAll();
				if ( expXPoder.RowCount==0) 
				{
					throw new Exception("El expediente " + expeTV.Dat.ID.AsInt + " no tiene poder asociado");
				}
				#endregion

				
				poder.Adapter.ReadByID(expXPoder.Dat.PoderID.AsInt);
				if (poder.RowCount==0)
				{
					throw new Exception("No pudo restaurarse Poder "+ expXPoder.Dat.PoderID.AsInt);
				}
				this.delPoder();
				this.addPoder(poder.Dat.ID.AsInt);
				this.delPropietarios();
				this.addPropietariosByPoder(poder.Dat.ID.AsInt);
			}
			else if (expXProp.RowCount > 0)
			{
				/*Borrar expediente por Propietario */
				this.delPropietarios();
				
				for (expXProp.GoTop(); !expXProp.EOF; expXProp.Skip())
				{
					this.addPropietario(expXProp.Dat.PropietarioID.AsInt , derechoPropio);
				}

			}
			else 
			{
				return -1;
			}

			return 0;

		}
		#endregion Actualizar desde campos




		#region Agregar propietario a partir de un poder
		/// <summary>
		/// Agrega propietarios a ExpedienteXPropietario a partir
		/// de PoderXPropietario
		/// </summary>
		/// <param name="poderID"></param>
		public void addPropietariosByPoder(int poderID)
		{
			Berke.DG.DBTab.ExpedienteXPropietario expeProp  = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			Berke.DG.DBTab.PropietarioXPoder poderProp      = new Berke.DG.DBTab.PropietarioXPoder(db);
			poderProp.Dat.PoderID.Filter = poderID;
			poderProp.Adapter.ReadAll();

			// Agregar propietarios a ExpedientexPropietario a partir
			// PropietarioXPoder
			for(poderProp.GoTop(); !poderProp.EOF; poderProp.Skip())
			{		
				expeProp.NewRow();
				expeProp.Dat.PropietarioID.Value = poderProp.Dat.PropietarioID.AsInt;
				expeProp.Dat.ExpedienteID.Value  = this.expeID;
				expeProp.Dat.DerechoPropio.Value = false;
				expeProp.PostNewRow();
				expeProp.Adapter.InsertRow();
			}
		}

		#endregion Agregar propoietario a partir de un poder
	}
}
