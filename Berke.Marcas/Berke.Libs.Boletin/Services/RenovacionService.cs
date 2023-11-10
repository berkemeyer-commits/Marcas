using System;
using Berke.Libs.Boletin.Libs;
namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// RenovacionService.cs
	/// Clase que provee servicios varios para operaciones de Registro.
	/// Autor: Marcos Báez
	/// </summary>
	public class RenovacionService
	{
		public static string ABREV_REN = "REN";
		public static int SIT_TRAMITE = 30;
		public static int TIPO_TRAMITE  = (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION;
		public static int VENC_PERIODO_GRACIA = 6;
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
		public RenovacionService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public RenovacionService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			expe    = new Berke.DG.DBTab.Expediente(db);
			regren  = new Berke.DG.DBTab.MarcaRegRen(db);
			mar     = new Berke.DG.DBTab.Marca(db);
			bol     = new Berke.DG.DBTab.BoletinDet(db);
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

		#region Obtener Expediente Padre
		/// <summary>Obtiene información del Expediente padre para el trámite
		/// especificado en bol, a partir del registro o acta de referencia (en 
		/// ese orden). </summary>
		/// <returns>Retorna el expediente padre Berke.DG.DBTab.Expediente.
		/// Si no posee información de reg/ren padre entonces se retorna null
		/// </returns>
		///		
		public Berke.DG.DBTab.Expediente getExpedientePadre()
		{
			ExpedienteService expeServ = new ExpedienteService(db);
			return expeServ.getExpedientePadre(this.bol);			
		}
		#endregion Obtener Expediente Padre

		#region Guardar Expediente
		/// <summary>Guarda información básica del expediente para el trámite de
		/// Renovación, a partir de la información de Boletin.
		/// </summary>
		/// <param name="expePadreID">Id. del Expediente Padre del nuevo expediente
		/// a insertar</param>
		/// <returns>Retorna el ID del Expediente creado</returns> 
		/// 
		public int guardarExpediente(int expePadreID)
		{			
			expe = new Berke.DG.DBTab.Expediente(db);
			expe.NewRow(); 
			expe.Dat.ID					.Value = DBNull.Value;   //int PK  Oblig.
			expe.Dat.TramiteID			.Value = RenovacionService.TIPO_TRAMITE;   //int Oblig.
			expe.Dat.TramiteSitID		.Value = RenovacionService.SIT_TRAMITE;   //int Oblig.
			expe.Dat.ActaNro			.Value = bol.Dat.ExpNro.AsInt;   //int
			expe.Dat.ActaAnio			.Value = bol.Dat.ExpAnio.AsInt;   //int
			expe.Dat.OrdenTrabajoID		.Value = DBNull.Value;   //int
			expe.Dat.ClienteID			.Value = DBNull.Value;   //int
			expe.Dat.AgenteLocalID		.Value = this.ag.Dat.idagloc.AsInt;   //int
			expe.Dat.ExpedienteID		.Value = expePadreID;   //int
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
			expe.Dat.VencimientoFecha	.Value = bol.Dat.SolicitudFecha.AsDateTime.AddYears(10);   //smalldatetime
			expe.Dat.MarcaRegRenID		.Value = DBNull.Value;   //int
			expe.Dat.PoderInscID		.Value = DBNull.Value;   //int
			expe.Dat.MarcaID			.Value = DBNull.Value;   //int
			expe.Dat.FechaAband			.Value = DBNull.Value;   //smalldatetime
			expe.Dat.Obs				.Value = DBNull.Value;   //nvarchar
			//		expe.Dat.Acta			.Value = DBNull.Value;   //nvarchar ReadOnly
			//		expe.Dat.Publicacion			.Value = DBNull.Value;   //nvarchar ReadOnly
			expe.Dat.Label				.Value = DBNull.Value;   //nvarchar
			expe.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //datetime
			expe.Dat.PresentacionFecha	.Value = bol.Dat.SolicitudFecha.AsDateTime;   //datetime
			expe.PostNewRow(); 
			int expeID = expe.Adapter.InsertRow(); 
			expe.Adapter.ReadByID(expeID);
			return expeID;
		}
		#endregion Guardar Expediente

		#region Guardar Reg Ren
		/// <summary>
		/// Inserta una entrada en MarcaRegRen, con la información básica
		/// del boletín. En este paso se enlaza con el expediente.
		/// </summary>
		/// <returns>Retorna el ID del MarcaRegRen creado</returns>
		///	
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
			regren.Adapter.ReadByID(regrenID);
			return regrenID;									
		}
		#endregion Guardar Reg Ren

		#region Guardar Marca
		/// <summary>
		/// Inserta una entrada en Marca, con la información básica
		/// del boletín si la renovación se encuentra en una edición diferente al
		/// del registro anterior. Si se encuentra en la misma edición, se actualiza
		/// la marca padre.
		/// </summary>
		/// <param name="marcaPadre">Marca asociada al expediente padre.</param>
		/// <returns>Retorna el ID de la Marca creada, o de la marca padre.</returns>		
		public int guardarMarca(Berke.DG.DBTab.Marca marcaPadre, Berke.DG.DBTab.Expediente expePadre)
		{
			int marTipo;
			Berke.DG.DBTab.Clase clase;
			
			marTipo	= MarcaService.getMarcaTipo(bol.Dat.MarcaTipo.AsString,db);
			clase		= MarcaService.getClaseVigente(bol.Dat.Clase.AsString,db);

			// Si las clases son iguales, entonces ambas marcas se encuentran
			// en la misma edición
			if (clase.Dat.ID.AsInt == marcaPadre.Dat.ClaseID.AsInt) 
			{
				#region Actualizar Marca Vigente				
				marcaPadre.Edit();
				marcaPadre.Dat.Denominacion			.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
				marcaPadre.Dat.DenominacionClave	.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
				marcaPadre.Dat.Fonetizada			.Value = DBNull.Value;   //nvarchar											
				marcaPadre.Dat.MarcaTipoID			.Value = marTipo;   //int Oblig.
				marcaPadre.Dat.ClaseID				.Value = clase.Dat.ID.AsInt;   //int Oblig.
				marcaPadre.Dat.ClaseDescripEsp		.Value = clase.Dat.Descrip.AsString;   //ntext
				marcaPadre.Dat.Limitada				.Value = DBNull.Value;   //bit
				marcaPadre.Dat.ClienteID			.Value = DBNull.Value;   //int
				marcaPadre.Dat.AgenteLocalID		.Value = ag.Dat.idagloc.AsInt;   //int
				marcaPadre.Dat.Nuestra				.Value = false;   //bit Oblig.
				marcaPadre.Dat.Sustituida			.Value = false;   //bit Oblig.
				marcaPadre.Dat.StandBy				.Value = false;   //bit Oblig.
				marcaPadre.Dat.Vigente				.Value = true;   //bit
				marcaPadre.Dat.LogotipoID			.Value = DBNull.Value;   //int
				marcaPadre.Dat.ExpedienteVigenteID	.Value = expe.Dat.ID.AsInt;   //int											
				marcaPadre.Dat.OtrosClientes		.Value = false;   //bit Oblig.
				marcaPadre.Dat.MarcaRegRenAnt		.Value = DBNull.Value;   //int
				marcaPadre.Dat.Propietario			.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Propietario.AsString);//nvarchar
				marcaPadre.Dat.ProDir				.Value = DBNull.Value;   //nvarchar
				marcaPadre.Dat.ProPais				.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Pais.AsString);   //nvarchar
				marcaPadre.PostEdit();
				marcaPadre.Adapter.UpdateRow();				
				#endregion Actualizar Marca Vigente
				return marcaPadre.Dat.ID.AsInt;
			} 
			else 
			{
				#region Poner inactiva la marca padre
				marcaPadre.Edit();
				marcaPadre.Dat.Vigente.Value = false;
				marcaPadre.PostEdit();
				marcaPadre.Adapter.UpdateRow();
				#endregion Poner inactiva a la marca padre

				#region Asignar valores a Marca
				mar.NewRow(); 
				mar.Dat.ID					.Value = DBNull.Value;   //int PK  Oblig.
				mar.Dat.Denominacion		.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
				mar.Dat.DenominacionClave	.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Denominacion.AsString);   //nvarchar
				mar.Dat.Fonetizada			.Value = DBNull.Value;   //nvarchar									
				mar.Dat.Vigilada			.Value = marcaPadre.Dat.Vigilada.AsBoolean;

				mar.Dat.MarcaTipoID			.Value = marTipo;   //int Oblig.
				mar.Dat.ClaseID				.Value = clase.Dat.ID.AsInt;   //int Oblig.
				mar.Dat.ClaseDescripEsp		.Value = clase.Dat.Descrip.AsString;   //ntext

				mar.Dat.Limitada			.Value = DBNull.Value;   //bit
				mar.Dat.ClienteID			.Value = DBNull.Value;   //int
				mar.Dat.AgenteLocalID		.Value = ag.Dat.idagloc.AsInt;   //int

				mar.Dat.Nuestra				.Value = false;   //bit Oblig.
				mar.Dat.Sustituida			.Value = false;   //bit Oblig.
				mar.Dat.StandBy				.Value = false;   //bit Oblig.
				mar.Dat.Vigente				.Value = true;   //bit
				mar.Dat.LogotipoID			.Value = DBNull.Value;   //int
				mar.Dat.ExpedienteVigenteID	.Value = expe.Dat.ID.AsInt;   //int
				//mar.Dat.ExpedienteVigenteID	.Value = expePadre;
				mar.Dat.OtrosClientes		.Value = false;   //bit Oblig.

				/* ---aacuna--- 31/ago/2006
				* Hacer que la nueva marca apunte al marcaregren del 
				* expediente padre, en lugar del nuevo marcaregren
				* mar.Dat.MarcaRegRenID		.Value = marRRID;   //int
				*/
				mar.Dat.MarcaRegRenID		.Value = expePadre.Dat.MarcaRegRenID.AsInt;

				mar.Dat.MarcaRegRenAnt		.Value = DBNull.Value;   //int
				mar.Dat.Propietario			.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Propietario.AsString);//nvarchar
				mar.Dat.ProDir				.Value = DBNull.Value;   //nvarchar
				mar.Dat.ProPais				.Value = Utils.cambiarCaracteresEspeciales(bol.Dat.Pais.AsString);   //nvarchar
				mar.PostNewRow(); 
				int marcaID = mar.Adapter.InsertRow();
				mar.Adapter.ReadByID(marcaID);
				return marcaID;
				#endregion Asignar valores a Marca
			}

		}
		#endregion Guardar Marca

		#region Asociar Marca y MarcaRegRen
		/// <summary>
		/// Engancha la Marca y MarcaRegRen al Expediente.
		/// Requiere de una llamada previa a guardarExpediente()
		/// </summary>
		/// <param name="marcaID">Id. de Marca</param>
		/// <param name="regrenID">Id. de MarcaRegRen</param>
		public void asociarExpeMarcaRegistro(int marcaID, int regrenID)
		{
			expe.Edit();
			expe.Dat.MarcaID.Value		= marcaID;
			expe.Dat.MarcaRegRenID.Value	= regrenID;
			expe.PostEdit();
			expe.Adapter.UpdateRow();
		}
		#endregion Asociar Marca y MarcaRegRen

		#region IsRenovacion
		/// <summary>
		/// Verifica si el trámite es renovación, de acuerdo 
		/// a la abreviatura utilizada en el boletín
		/// </summary>
		/// <param name="tramite">Abreviatura del tripo de trámite</param>
		/// <param name="db">Conexion a la BD</param>
		/// <returns>true si es que el trámite corresponde a una Renovación</returns>
		public static bool isRenovacion(string tramite, Berke.Libs.Base.Helpers.AccesoDB db) 
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
		/// Verifica si el trámite es renovación, de acuerdo 
		/// a la abreviatura utilizada en el boletín
		/// </summary>
		/// <param name="tramite">Abreviatura del tripo de trámite</param>
		/// <returns>true si es que el trámite corresponde a una Renovación</returns>
		public bool isRenovacion(string tramite)
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
		/// Verifica si el trámite es renovación, de acuerdo 
		/// al tramiteid
		/// </summary>
		/// <param name="tramiteid">ID. del trámite</param>
		/// <returns></returns>
		public static bool isRenovacion(int tramiteid)
		{
			return TIPO_TRAMITE == tramiteid;
		}
		#endregion IsRenovacion

		#region Desistir y Cancelar
		/// <summary>
		/// Realiza las operaciones que involucra el desistimiento de una Renovación
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud del desistimiento</param>
		/// <param name="obs">Observación que se registrará en la situación.</param>
		public void desistir(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación desistida
			expeServ.addSituacionDesistida(TIPO_TRAMITE,solicitudFecha, obs, expe.Dat.ID.AsInt);
		}

		public void desistir(Berke.DG.DBTab.Expediente expe )
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
		}

		public void revertirDesistir(Berke.DG.DBTab.Expediente expe )
		{
			revertirCancelacion(expe);
		}

		/// <summary>
		/// Realiza las operaciones que involucra la cancelación de una Renovación
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud del desistimiento</param>
		/// <param name="obs">Observación que se registrará en la situación.</param>
		public void cancelar(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación cancelada
			expeServ.addSituacionCancelada(TIPO_TRAMITE,solicitudFecha, obs, expe.Dat.ID.AsInt);
		}

		public void cancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
		}

		/// <summary>
		/// Realiza las operaciones que involucra la reversion de cancelación de una Renovación
		/// </summary>
		/// <param name="expe">Expediente</param>
		public void revertirCancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos Activo el trámite
			revertirCancelacion(expe);
			//this.borrarSituacionActual();
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

		#region revertirCancelacion by rgimenez
		/// <summary>
		/// Revertir la Cancelacion de un tramite de REN
		/// </summary>
		/// <param name="expe">Expediente de Renovacion</param>		/// 
		public void revertirCancelacion(Berke.DG.DBTab.Expediente expe)
		{
			/* rgimenez
			 * para revertir CANCELACION
			 * */
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.Expediente expeActual;


			Berke.DG.DBTab.MarcaRegRen regrenActual;
			Berke.DG.DBTab.ExpedienteCampo expeCampo;

			/* Obtener el expediente Padre */
			expeServ.setExpeID(expe.Dat.ExpedienteID.AsInt);			
			expePadre = expeServ.getExpediente();

			// Tanto el Reg/Ren padre como la nueva Ren se encuentran en
			// en distintas ediciones
			if (expePadre.Dat.MarcaID.AsInt != expe.Dat.MarcaID.AsInt)
			{
				#region Marcas en distinta edición
				//Si no estan en la misma edición verificar si aun 
				//se puede poner activa la marca de la actual renovacion

				/* setear datos del expediente actual */
				expeServ.setExpeID(expe.Dat.ID.AsInt);			
				expeActual   = expeServ.getExpediente();
				regrenActual = expeServ.getMarcaRegRen();

				// Hoy menos PERIODO_GRACIA
				DateTime todaym6m = System.DateTime.Now;
				todaym6m = todaym6m.AddMonths(-VENC_PERIODO_GRACIA);
				
				

				#region Poner Activa la marca actual
				// Activamos la marca actual
				marServ.setMarcaID(expeActual.Dat.MarcaID.AsInt);
				marServ.pasarActiva(expeActual.Dat.ID.AsInt);
				#endregion Poner Activa la marca actual

				// Verificamos si la marca esta concedida y aun no ha vencido
				if (!regrenActual.Dat.VencimientoFecha.IsNull &&
					(regrenActual.Dat.VencimientoFecha.AsDateTime >= todaym6m))
				{
					

					#region Poner Activo el registro de la REN actual
					expeServ.setExpediente(expeActual);
					expeServ.pasarActivoRegistro();
					#endregion Poner Activo 

					#region Poner Inactivo el registro anterior
					expeServ.setExpediente(expePadre);
					expeServ.pasarInactivoRegistro();
					#endregion Poner Inactivo el registro anterior

				}

				#region Poner Inactiva la marca anterior
					marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);
					marServ.pasarInactiva();
				#endregion Poner Inactiva la marca anterior

				#endregion Marcas en distinta edición

			}
			else 
			{
				#region Verificar vigencia de Marca
				//Estan en la misma edición, tenemos que verificar
				//si tenemos que seguir manteniéndola activa.
				
				expeServ.setExpeID(expe.Dat.ID.AsInt);			
				expeActual   = expeServ.getExpediente();
				regrenActual = expeServ.getMarcaRegRen();

				// Hoy menos PERIODO_GRACIA
				DateTime todaym6m = System.DateTime.Now;
				todaym6m = todaym6m.AddMonths(-VENC_PERIODO_GRACIA);

				#region Poner Activa la marca actual
				marServ.setMarcaID(expeActual.Dat.MarcaID.AsInt);
				marServ.pasarActiva(expeActual.Dat.ID.AsInt);
				#endregion Poner Activa la marca actual
				
				// Si esta concedida pero ya vencio
				if (!regrenActual.Dat.VencimientoFecha.IsNull &&
					(regrenActual.Dat.VencimientoFecha.AsDateTime < todaym6m))
				{
					#region Poner Inactiva la marca actual 
					// Ponemos Inactiva la marca actual porque ya vencio
					marServ.setMarcaID(expeActual.Dat.MarcaID.AsInt);
					marServ.pasarInactiva();
					#endregion Poner Inactiva la marca actual

				}
				else 
				{
					/* Si Todavia no vencio */
					if (!regrenActual.Dat.VencimientoFecha.IsNull ) 
					{
						#region Poner Activo el registro de la renovacion actual
						expeServ.setExpediente(expeActual);
						expeServ.pasarActivoRegistro();
						#endregion Poner Activo el registro de la renovación actual

						#region Poner Inactivo el registro anterior
						expeServ.setExpediente(expePadre);
						expeServ.pasarInactivoRegistro();
						#endregion Poner Inactivo el registro anterior

					}

					

				}
				#endregion Verificar vigencia de Marca
				
				#region Restaurar datos de la Marca
				expeServ.setExpediente(expe);
				expeCampo = expeServ.getCampos(expe.Dat.ID.AsInt);
				marServ.updateFromCampos(expeCampo,true,true,true);
				#endregion Restaurar datos de la Marca
			}


		}
		#endregion


		#region pasarInactiva by mbaez
		/// <summary>
		/// Pone inactivo un trámite de registro
		/// </summary>
		/// <param name="expe">Expediente de Registro</param>
		public void pasarInactiva(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.MarcaRegRen regrenPadre;
			Berke.DG.DBTab.ExpedienteCampo expeCampo;

			expeServ.setExpeID(expe.Dat.ExpedienteID.AsInt);			
			expePadre = expeServ.getExpediente();

			// Tanto el Reg/Ren padre como la nueva Ren se encuentran en
			// la misma edición, por lo tanto comparten la marca
			if (expePadre.Dat.MarcaID.AsInt != expe.Dat.MarcaID.AsInt)
			{
				#region Marcas en distinta edición
				//Si no estan en la misma edición pongo inactiva la marca
				// de la actual renovacion
				marServ.setMarcaID(expe.Dat.MarcaID.AsInt);
				marServ.pasarInactiva();
				// Hoy menos PERIODO_GRACIA
				DateTime todaym6m = System.DateTime.Now;
				todaym6m = todaym6m.AddMonths(-VENC_PERIODO_GRACIA);
				regrenPadre = expeServ.getMarcaRegRen();
				// Debería estar vigente la marca?
				if (!regrenPadre.Dat.VencimientoFecha.IsNull &&
					(regrenPadre.Dat.VencimientoFecha.AsDateTime >= todaym6m))
				{
					// Activamos la marca anterior
					marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);
					marServ.pasarActiva(expePadre.Dat.ID.AsInt);

					#region Poner Activo el registro anterior
					expeServ.setExpediente(expePadre);
					expeServ.pasarActivoRegistro();
					#endregion Poner Activo el registro anterior
				}
				#endregion Marcas en distinta edición
			}
			else 
			{
				#region Verificar vigencia de Marca
				//Estan en la misma edición, tenemos que verificar
				//si tenemos que seguir manteniéndola activa.
				marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);

				// Hoy menos PERIODO_GRACIA
				DateTime todaym6m = System.DateTime.Now;
				todaym6m = todaym6m.AddMonths(-VENC_PERIODO_GRACIA);
				regrenPadre = expeServ.getMarcaRegRen();
				// Debería estar vigente la marca?				

				if (!regrenPadre.Dat.VencimientoFecha.IsNull &&
					(regrenPadre.Dat.VencimientoFecha.AsDateTime < todaym6m))
				{
					#region Poner Inactiva la marca
					// Ponemos inactiva a la marca anterior
					marServ.pasarInactiva();
					#endregion Poner Inactiva la marca
				}
				else 
				{
					/* Si Todavia no vencio */
					if (!regrenPadre.Dat.VencimientoFecha.IsNull ) 
					{
						#region Poner Activo el registro anterior
						expeServ.setExpediente(expePadre);
						expeServ.pasarActivoRegistro();
						#endregion Poner Activo el registro anterior

					}

					#region Poner Activa la marca anterior
					// Activamos la marca anterior (posiblemente redundante)
					// pero seteamos el nuevo expediente vigente
					
					marServ.pasarActiva(expePadre.Dat.ID.AsInt);
					#endregion Poner Activa la marca anterior
				}
				#endregion Verificar vigencia de Marca 
				
				#region Restaurar datos de la Marca
				expeServ.setExpediente(expe);
				expeCampo = expeServ.getCampos(expe.Dat.ID.AsInt);
				marServ.updateFromCampos(expeCampo,false,true,true);
				#endregion Restaurar datos de la Marca
			}

			#region Poner inactivo el registro de la renovación actual
			expeServ.setExpediente(expe);
			expeServ.pasarInactivoRegistro();
			#endregion Poner inactivo el registro de la renovación actual

		}
		#endregion pasarInactiva by mbaez

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

		#region Borrar Renovacion de Tercero
		public void Borrar(Berke.DG.DBTab.Expediente expe)
		{
			MarcaRegRenService regrenService = new MarcaRegRenService(db);
			Berke.DG.DBTab.MarcaRegRen marRR = regrenService.getRegRenByExpe(expe.Dat.ID.AsInt);

			MarcaService marcaService = new MarcaService(db);
			marcaService.setMarcaID(expe.Dat.MarcaID.AsInt);
			Berke.DG.DBTab.Marca marca = marcaService.getMarca();
			
			ExpedienteService expeService = new ExpedienteService(db);
			expeServ.setExpeID(expe.Dat.ExpedienteID.AsInt);
			Berke.DG.DBTab.Expediente expePadre = expeServ.getExpediente();

			//Restaurar Marca según corresponda
			marcaService.setMarcaID(expe.Dat.MarcaID.AsInt);
			this.pasarInactiva(expe);

			expe.Edit();
			//Poner Null a MarcaRegRenID del Expediente de Renovación
			expe.Dat.MarcaRegRenID.SetNull();
			//Poner Null a MarcaID del Expediente de Renovación
			expe.Dat.MarcaID.SetNull();
			expe.PostEdit();
			expe.Adapter.UpdateRow();
			
			//Borrar instrucciones
			Berke.DG.DBTab.Expediente_Instruccion expeInstruccion = expeServ.getInstrucciones(expe.Dat.ID.AsInt);
			for(expeInstruccion.GoTop(); !expeInstruccion.EOF; expeInstruccion.Skip())
			{
				expeInstruccion.Adapter.DeleteRow();
			}

			//Borrar situaciones
			Berke.DG.DBTab.Expediente_Situacion expeSituacion = expeServ.getSituaciones(expe.Dat.ID.AsInt);
			for(expeSituacion.GoTop(); !expeSituacion.EOF; expeSituacion.Skip())
			{
				expeSituacion.Adapter.DeleteRow();
			}

			//Borrar fila de MarcaRegRen
			marRR.Adapter.DeleteRow();
			//Borrar expediente
			expe.Adapter.DeleteRow();
		}
		#endregion Borrar Renovacion de Tercero

	}
}
