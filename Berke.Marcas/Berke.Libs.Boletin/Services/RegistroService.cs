using System;
using Berke.Libs.Boletin.Libs;
using System.Collections;
using Berke.Libs.Base;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// RegistroService.cs
	/// Provee servicios para los trámites de Registro.
	/// Autor: Marcos Báez
	/// </summary>
	public class RegistroService
	{
		public static string ABREV_REG = "REG";
		public static int SIT_TRAMITE = 2;
		public static int TIPO_TRAMITE  = (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO;

		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.Expediente expe;
		protected Berke.DG.DBTab.CAgenteLocal ag;
		protected Berke.DG.DBTab.MarcaRegRen regren;
		protected Berke.DG.DBTab.Marca mar;
		protected Berke.DG.DBTab.BoletinDet bol;
		protected MarcaService marServ;
		protected ExpedienteService expeServ;
		protected string [] bolAbrev;
		#endregion Atributos

		#region Constructores
		public RegistroService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public RegistroService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db  = db;
			expe     = new Berke.DG.DBTab.Expediente(db);
			regren   = new Berke.DG.DBTab.MarcaRegRen(db);
			mar      = new Berke.DG.DBTab.Marca(db);
			bol      = new Berke.DG.DBTab.BoletinDet(db);
			marServ  = new MarcaService(db);
			expeServ = new ExpedienteService(db);

			Berke.DG.DBTab.Tramite tr = new Berke.DG.DBTab.Tramite(db);
			tr.Adapter.ReadByID(TIPO_TRAMITE);
			bolAbrev = tr.Dat.BolAbrev.AsString.Split(",".ToCharArray());
		}
		#endregion Constructores

		#region setters
		public void setAgenteLocal(Berke.DG.DBTab.CAgenteLocal ag)
		{
			this.ag = ag;
		}
		public void setBoletin(Berke.DG.DBTab.BoletinDet bol)
		{
			this.bol = bol;
		}
		#endregion setters

		#region Guardar Expediente de Registro
		public int guardarExpediente()
		{			
			expe.NewRow(); 
			expe.Dat.ID					.Value = DBNull.Value;   //int PK  Oblig.
			expe.Dat.TramiteID			.Value = RegistroService.TIPO_TRAMITE;   //int Oblig.
			expe.Dat.TramiteSitID		.Value = RegistroService.SIT_TRAMITE;   //int Oblig.
			expe.Dat.ActaNro			.Value = bol.Dat.ExpNro.AsInt;   //int
			expe.Dat.ActaAnio			.Value = bol.Dat.ExpAnio.AsInt;   //int
			expe.Dat.OrdenTrabajoID		.Value = DBNull.Value;   //int
			expe.Dat.ClienteID			.Value = DBNull.Value;   //int
			expe.Dat.AgenteLocalID		.Value = ag.Dat.idagloc.AsInt;   //int
			expe.Dat.ExpedienteID		.Value = DBNull.Value;   //int
			expe.Dat.BoletinDetalleID	.Value = DBNull.Value;   //int
			expe.Dat.DiarioID			.Value = DBNull.Value;   //int
			expe.Dat.PublicPag			.Value = DBNull.Value;   //int
			expe.Dat.PublicAnio			.Value = DBNull.Value;   //int
			expe.Dat.Documento			.Value = DBNull.Value;   //bit
			expe.Dat.Bib				.Value = DBNull.Value;   //int
			expe.Dat.Exp				.Value = DBNull.Value;   //int
			expe.Dat.Nuestra			.Value = false;   //bit Oblig.
			expe.Dat.Sustituida			.Value = false;   //bit Oblig.
			expe.Dat.StandBy			.Value = false;   //bit Oblig.
			expe.Dat.Vigilada			.Value = false;   //bit Oblig.
			expe.Dat.Concluido			.Value = false;   //bit Oblig.
			expe.Dat.VencimientoFecha	.Value = DBNull.Value;   //smalldatetime
			expe.Dat.MarcaRegRenID		.Value = DBNull.Value;   //int
			expe.Dat.PoderInscID		.Value = DBNull.Value;   //int
			expe.Dat.MarcaID			.Value = DBNull.Value;   //int
			expe.Dat.FechaAband			.Value = DBNull.Value;   //smalldatetime
			expe.Dat.Obs				.Value = DBNull.Value;   //nvarchar
			expe.Dat.Label				.Value = DBNull.Value;   //nvarchar
			expe.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //datetime
			expe.Dat.PresentacionFecha	.Value = bol.Dat.SolicitudFecha.AsDateTime;   //datetime
			expe.PostNewRow();
			int expeID = expe.Adapter.InsertRow();
			expe.Adapter.ReadByID(expeID);
			return expeID;
		}
		#endregion Guardar Expediente de Registro
		
		#region Guardar RegRen
		/// <summary>
		/// Inserta un registro en MarcaRegRen con la información básica del Boletín.
		/// Requiere que se setee previamente el expediente, o se genere uno nuevo.
		/// </summary>
		/// <returns>ID. de la entrada MarcaRegRen generada</returns>
		public int guardarRegRen()
		{		
			regren.NewRow(); 
			regren.Dat.ID				.Value = DBNull.Value;   //int PK  Oblig.
			regren.Dat.ExpedienteID		.Value = expe.Dat.ID.AsInt;   //int Oblig.
			regren.Dat.RegistroNro		.Value = DBNull.Value;   //int
			regren.Dat.RegistroAnio		.Value = DBNull.Value;   //int
			regren.Dat.ConcesionFecha	.Value = DBNull.Value;   //smalldatetime
			regren.Dat.Limitada			.Value = DBNull.Value;   //bit
			regren.Dat.Vigente			.Value = DBNull.Value;   //bit
			regren.Dat.RefMarca			.Value = DBNull.Value;   //nvarchar
			regren.Dat.ObsAvRen			.Value = DBNull.Value;   //nvarchar
			regren.Dat.TituloError		.Value = DBNull.Value;   //bit
			//		marRR.Dat.Registro	.Value = DBNull.Value;   //nvarchar ReadOnly
			regren.Dat.VencimientoFecha	.Value = DBNull.Value;   //datetime
			regren.PostNewRow();
			int regrenID = regren.Adapter.InsertRow();
			//regren.Adapter.ReadByID(regrenID);
			return regrenID;								
		}
		#endregion Guardar RegRen

		#region Guardar Marca
		/// <summary>
		/// Inserta una entrada en la tabla Marca con la información básica del Boletín.
		/// Requiere que se setee previamente el expediente y RegRen, o se genere unos nuevos.
		/// </summary>
		/// <returns>ID. de la Marca insertada</returns>
		public int guardarMarca(int regrenID)
		{			
			Berke.DG.DBTab.Clase clase = MarcaService.getClaseVigente(bol.Dat.Clase.AsString,db);

			mar.NewRow(); 
			mar.Dat.ID					.Value = DBNull.Value;   //int PK  Oblig.
			mar.Dat.Denominacion		.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
			mar.Dat.DenominacionClave	.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
			mar.Dat.Fonetizada			.Value = DBNull.Value;   //nvarchar
			mar.Dat.MarcaTipoID			.Value = MarcaService.getMarcaTipo(bol.Dat.MarcaTipo.AsString,db);  //int Oblig.		
			mar.Dat.ClaseID				.Value = clase.Dat.ID.AsInt;   //int Oblig.
			mar.Dat.ClaseDescripEsp		.Value = Utils.cambiarCaracteresEspeciales(clase.Dat.Descrip.AsString);   //ntext
			mar.Dat.Limitada			.Value = DBNull.Value;   //bit
			mar.Dat.ClienteID			.Value = DBNull.Value;   //int									
			mar.Dat.AgenteLocalID		.Value = this.ag.Dat.idagloc.AsInt;   //int
			mar.Dat.Nuestra				.Value = false;   //bit Oblig.
			mar.Dat.Vigilada			.Value = false;   //bit Oblig.
			mar.Dat.Sustituida			.Value = false;   //bit Oblig.
			mar.Dat.StandBy				.Value = false;   //bit Oblig.
			mar.Dat.Vigente				.Value = true;   //bit
			mar.Dat.LogotipoID			.Value = DBNull.Value;   //int
			mar.Dat.ExpedienteVigenteID	.Value = this.expe.Dat.ID.AsInt;   //int
			mar.Dat.OtrosClientes		.Value = false;   //bit Oblig.
			mar.Dat.MarcaRegRenID		.Value = regrenID; //this.regren.Dat.ID.AsInt;   //int
			mar.Dat.MarcaRegRenAnt		.Value = DBNull.Value;   //int
			mar.Dat.Propietario			.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Propietario.AsString);//nvarchar
			mar.Dat.ProDir				.Value = DBNull.Value;   //nvarchar
			mar.Dat.ProPais				.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Pais.AsString);   //nvarchar
			mar.PostNewRow(); 
			int marcaID = mar.Adapter.InsertRow();
			return marcaID;
		}
		#endregion Guardar Marca

		#region Asociar Marca Expediente MarcaRegRen
		public void asociarExpeMarcaRegistro(int marcaID, int regrenID)
		{					
			expe.Edit();
			expe.Dat.MarcaID.Value       = marcaID;
			expe.Dat.MarcaRegRenID.Value = regrenID;
			expe.PostEdit();
			expe.Adapter.UpdateRow();			
		}
		#endregion Asociar Marca Expediente MarcaRegRen

		#region Is Registro
		/// <summary>
		/// Verifica si el trámite es registro, de acuerdo 
		/// a la abreviatura utilizada en el boletín
		/// </summary>
		/// <param name="tramite">Abreviatura del tripo de trámite</param>
		/// <param name="db">Conexion a la BD</param>
		/// <returns>true si es que el trámite corresponde a una Renovación</returns>
		public static bool isRegistro(string tramite, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Tramite tr = new Berke.DG.DBTab.Tramite(db);
			tr.Adapter.ReadByID(TIPO_TRAMITE);
			string [] abrev = tr.Dat.BolAbrev.AsString.Split(",".ToCharArray());
			for (int i=0; i<abrev.Length; i++)
			{
				if (abrev[i] == tramite)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Verifica si el trámite es registro, de acuerdo 
		/// a la abreviatura utilizada en el boletín
		/// </summary>
		/// <param name="tramite">Abreviatura del tripo de trámite</param>
		/// <returns>true si es que el trámite corresponde a una Renovación</returns>
		public bool isRegistro(string tramite)
		{
			for (int i=0; i<bolAbrev.Length; i++)
			{
				if (bolAbrev[i] == tramite)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Verifica si el trámite es registro, de acuerdo 
		/// al tramiteID.
		/// </summary>
		/// <param name="tramiteid">ID. del trámite</param>
		/// <returns></returns>
		public static bool isRegistro(int tramiteid)
		{
			return TIPO_TRAMITE == tramiteid;
		}
		#endregion Is Registro

		#region Desistir y Cancelar
		/// <summary>
		/// Desiste un trámite de Registro
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud</param>
		/// <param name="obs">Observación</param>
		public void desistir(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación desistida
			expeServ.addSituacionDesistida(TIPO_TRAMITE,solicitudFecha, obs, expe.Dat.ID.AsInt);
		}

		public void desistir(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
		}


		/// <summary>
		/// Revertir Desistimiento de un trámite de Registro
		/// </summary>
		/// <param name="expe">Expediente</param>
		public void revertirDesistir(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos Activo el trámite
			pasarActiva(expe);
			// aqui
			// agregar borrado de situacion actual del expediente en expediente_situacion
			// 
			//
			//this.borrarSituacionActual(expe);

		}

		#region Borrar Situacion Actual del Expediente
		public void borrarSituacionActual(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.Expediente_Situacion expSit = new Berke.DG.DBTab.Expediente_Situacion(db);
			expSit.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expSit.Dat.TramiteSitID.Filter = expe.Dat.TramiteSitID.AsInt;
			expSit.Adapter.ReadAll();

			if ( expSit.RowCount == 0 ) 
			{
				throw new Exception("El expediente no tiene situaciones [Expediente_Situacion] ");
			}

			for (expSit.GoTop(); !expSit.EOF; expSit.Skip())
			{
				expSit.Delete();
				expSit.Adapter.DeleteRow();
			}

			/*  debo obtener antes el tramitesitid anterior */
			/*
			#region Seteamos como última situación del expediente
			expe.Adapter.ReadByID(expe.Dat.ID.AsInt);
			expe.Edit();
			expe.Dat.TramiteSitID.Value = tramiteSitID;
			expe.PostEdit();
			expe.Adapter.UpdateRow();
			#endregion Seteamos como última situación del expediente
			*/


		}

		#endregion

		/// <summary>
		/// Cancela un trámite de Registro
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud</param>
		/// <param name="obs">Observación</param>
		public void cancelar(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación cancelada
			expeServ.addSituacionCancelada(TIPO_TRAMITE,solicitudFecha, obs, expe.Dat.ID.AsInt);
		}


		public bool cancelacionREGFactible( Berke.DG.DBTab.Expediente expe )
		{

			ArrayList lstSituaciones = new ArrayList();
			ArrayList lstTramiteSit  = new ArrayList();

			
			/* Situaciones permitidas del expe de REN para que el Registro pueda ser cancelado */
			lstSituaciones.Add(GlobalConst.Situaciones.DESISTIDA); 
			lstSituaciones.Add(GlobalConst.Situaciones.CANCELACION_REG);
			lstSituaciones.Add(GlobalConst.Situaciones.MARCA_ANULADA);

			ExpedienteService expeServ         = new ExpedienteService(db);
			Berke.DG.DBTab.Expediente expHijo  = new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.MarcaRegRen regrenActual;
			
			#region Verificar si tiene renovacion posterior

		    expHijo.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expHijo.Dat.TramiteID.Filter    = (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION;
			expHijo.Adapter.ReadAll();

			if ( expHijo.RowCount == 0) 
			{
				return true;
			}

			#endregion
 

			#region Verificar si el Tramite de REN es válido

			expeServ.setExpediente(expHijo);
			regrenActual = expeServ.getMarcaRegRen();
			lstTramiteSit = expeServ.getTramiteSitID(lstSituaciones,expHijo.Dat.TramiteID.AsInt);

			bool situacion_valida = false;
			foreach ( string lstItem in lstTramiteSit )	
			{
				if (expHijo.Dat.TramiteSitID.AsString == lstItem)
				{
					situacion_valida = true; /*valido para Cancelar el tramite de REG padre */
					break;
				}
			}
			#endregion Verificar si el Tramite de REN es válido
			

		
			return situacion_valida;

		}


		

		public void cancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			if ( cancelacionREGFactible(expe) ) 
			{
				pasarInactiva(expe);
			}
			else 
			{
				throw new Exception("No se puede cancelar el Registro porque existe un tramite de Renovacion posterior");
			}
		
		}

		/// <summary>
		/// Revierte la Cancelacion de un trámite de Registro
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// 
		public void revertirCancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos activo el trámite
			pasarActiva(expe);
		
		}

		/// <summary>
		/// Pone inactivo un trámite de registro
		/// </summary>
		/// <param name="expe">Expediente de Registro</param>
		public void pasarInactiva(Berke.DG.DBTab.Expediente expe)
		{
			marServ.setMarcaID(expe.Dat.MarcaID.AsInt);
			marServ.pasarInactiva();
			expeServ.setExpediente(expe);
			expeServ.pasarInactivoRegistro();
		}

		/// <summary>
		/// Pone activo un trámite de registro
		/// </summary>
		/// <param name="expe">Expediente de Registro</param>
		public void pasarActiva(Berke.DG.DBTab.Expediente expe)
		{
			marServ.setMarcaID(expe.Dat.MarcaID.AsInt);
			marServ.pasarActiva(expe.Dat.ID.AsInt);
			expeServ.setExpediente(expe);
			expeServ.pasarActivoRegistro();
		}

		#endregion Desistir y Cancelar

		

		#region Obtener Abreviaturas
		public string getListaAbreviaturas(string comillas)
		{
			string abrev = "";
			for (int i=0; i<bolAbrev.Length; i++)
			{
				if (bolAbrev[i] == "") continue;
				if (i == 0)
				{
					abrev+= comillas + bolAbrev[i] + comillas;
				}
				else  
				{
					abrev+=","+ comillas + bolAbrev[i] + comillas ;
				}
			}
			return abrev;
		}
		#endregion Obtener Abreviaturas
	
	}
}
