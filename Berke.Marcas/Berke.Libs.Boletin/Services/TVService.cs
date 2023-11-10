using System;
using System.Collections;
using Berke.Libs.Base;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// TVService.cs
	/// Clase que provee servicios varios para Trámites Varios.
	/// Autor: Marcos Báez
	/// </summary>
	public class TVService
	{

		public const int TIPO_TR = (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA;
		public const int TIPO_CN = (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE;
		public const int TIPO_FS = (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.FUSION;
		public const int TIPO_CD = (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO;
		public const int TIPO_LC = (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA;
		public const int SIT_TR =  (int) Berke.Libs.Base.GlobalConst.SituacionesXTramite.TRANSFERENCIA_PRESENTADA;
		public const int SIT_CN =  (int) Berke.Libs.Base.GlobalConst.SituacionesXTramite.CAMBIO_NOMBRE_PRESENTADA;
		public const int SIT_FS =  (int) Berke.Libs.Base.GlobalConst.SituacionesXTramite.FUSION_PRESENTADA; 
		public const int SIT_CD =  (int) Berke.Libs.Base.GlobalConst.SituacionesXTramite.CAMBIO_DOMICILIO_PRESENTADA;
		public const int SIT_LC =  (int) Berke.Libs.Base.GlobalConst.SituacionesXTramite.LICENCIA_PRESENTADA;

		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.BoletinDet bol;
		protected Berke.DG.DBTab.CAgenteLocal ag;
		protected ExpedienteService expeServ;
		protected MarcaService marServ;
		private int tipoTramite;
		private int sitTramite;
		string [] bolAbrev;
		private string abrevTr = "";
		private string abrevCn = "";
		private string abrevFs = "";
		private string abrevCd = "";
		private string abrevLc = "";
		#endregion Atributos

		#region Constructores
		public TVService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public TVService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db  = db;
			bol      = new Berke.DG.DBTab.BoletinDet(db);
			expeServ = new ExpedienteService(db);
			marServ  = new MarcaService(db);

			#region Abreviaturas
			string listTr = TIPO_TR + "," +
							TIPO_CN + "," +
							TIPO_FS + "," +
							TIPO_CD + "," +
							TIPO_LC ;
			Berke.DG.DBTab.Tramite tr = new Berke.DG.DBTab.Tramite(db);
			tr.Dat.ID.Filter = ObjConvert.GetFilter(listTr);
			tr.Adapter.ReadAll();
			string abrevList = "";
			for (tr.GoTop(); !tr.EOF; tr.Skip())
			{
				abrevList +=  tr.Dat.BolAbrev.AsString + ",";
				if (tr.Dat.ID.AsInt == TIPO_CD)
				{
					abrevCd = tr.Dat.BolAbrev.AsString;
				}
				else if (tr.Dat.ID.AsInt == TIPO_CN)
				{
					abrevCn = tr.Dat.BolAbrev.AsString;
				}
				else if (tr.Dat.ID.AsInt == TIPO_FS)
				{
					abrevFs = tr.Dat.BolAbrev.AsString;
				}
				else if (tr.Dat.ID.AsInt == TIPO_LC)
				{
					abrevLc = tr.Dat.BolAbrev.AsString;
				}
				else if (tr.Dat.ID.AsInt == TIPO_TR)
				{
					abrevTr = tr.Dat.BolAbrev.AsString;
				}
			}
			bolAbrev = abrevList.Split(",".ToCharArray());
			#endregion Abreviaturas
		}
		#endregion Constructores

		#region setters
		/// <summary>
		/// Establece el tipo de TV: LC, TR, FS, CD, CN..
		/// </summary>
		/// <param name="tramite">Descripcion del Tipo de TV: LC, TR, FS, CD, CN</param>
		public void setTramite(string tramite)
		{

			if (this.isTransferencia(tramite) )
			{					
				tipoTramite= TIPO_TR;
				sitTramite = SIT_TR;
			} 
			else if (this.isCambioNombre(tramite))
			{				
				tipoTramite= TIPO_CN;
				sitTramite = SIT_CN;
			}
			else if (this.isFusion(tramite))
			{
				tipoTramite= TIPO_FS;
				sitTramite = SIT_FS;
			}
			else if (this.isCambioDomicilio(tramite))
			{
				tipoTramite= TIPO_CD;
				sitTramite = SIT_CD;
			}
			else if (this.isLicencia(tramite))
			{
				tipoTramite=TIPO_LC;
				sitTramite =SIT_LC;			
			}
			else {
				throw new Exceptions.BolImportException("El tramite "+ tramite+ " no es un TV válido.");
			}
		}
		/// <summary>
		/// Establece el ID. del Trámite
		/// </summary>
		/// <param name="tramiteID">ID. del TV</param>
		public void setTramiteID(int tramiteID)
		{
			switch(tramiteID) 
			{
					#region Transferencia
						
				case TIPO_TR:
					tipoTramite= TIPO_TR;
					sitTramite = SIT_TR;
					break;

					#endregion Transferencia

					#region Cambio de Nombre

				case TIPO_CN:
					tipoTramite= TIPO_CN;
					sitTramite = SIT_CN;
					break;

					#endregion Cambio de Nombre

					#region Fusion

				case TIPO_FS:
					tipoTramite= TIPO_FS;
					sitTramite = SIT_FS;
					break;

					#endregion Fusion

					#region Cambio de Domicilio

				case TIPO_CD:
					tipoTramite= TIPO_CD;
					sitTramite = SIT_CD;
					break;

					#endregion Cambio de Domicilio

					#region Licencia

				case TIPO_LC:
					tipoTramite=TIPO_LC;
					sitTramite =SIT_LC;			
					break;
				default: throw new Exceptions.BolImportException("El tramiteID "+ tramiteID+ " no es un TV válido.");
					#endregion Licencia
			}
		}
		public void setAgenteLocal(Berke.DG.DBTab.CAgenteLocal ag)
		{
			this.ag = ag;
		}
		public void setBoletin(Berke.DG.DBTab.BoletinDet bol)
		{
			this.bol = bol;
		}
		#endregion setters

		#region getters
		public int getTipoTramite()
		{
			return this.tipoTramite;
		}
		public int getSitTramite()
		{
			return this.sitTramite;
		}
		
		#endregion getters

		#region Verificar tipo de tramite
		public bool isCambioNombre()
		{
			return tipoTramite == TIPO_CN;
		}
		public bool isCambioDomicilio()
		{
			return tipoTramite == TIPO_CD;
		}
		public bool isTransferencia()
		{
			return tipoTramite == TIPO_TR;
		}
		public bool isFusion()
		{
			return tipoTramite == TIPO_FS;
		}
		public bool isLicencia()
		{
			return tipoTramite == TIPO_LC;
		}

		public bool isCambioNombre(string tramite)
		{
			return isTipoTramite(tramite, abrevCn);
		}
		public bool isCambioDomicilio(string tramite)
		{
			return isTipoTramite(tramite, abrevCd);
		}
		public bool isTransferencia(string tramite)
		{
			return isTipoTramite(tramite, abrevTr);
		}
		public bool isFusion(string tramite)
		{
			return isTipoTramite(tramite, abrevFs);
		}
		public bool isLicencia(string tramite)
		{
			return isTipoTramite(tramite, abrevLc);
		}

		private bool isTipoTramite(string tramite, string abrevList) 
		{
			string [] abrev = abrevList.Split(",".ToCharArray());
			for (int i=0; i<abrev.Length; i++)
			{
				if ( (abrev[i] != "") && (abrev[i] == tramite) )
				{
					return true;
				}
			}
			return false;
		}

		/*
		public static bool isCambioNombre(string tipoTramite)
		{
			return tipoTramite == ABREV_CN;
		}
		public static bool isCambioDomicilio(string tipoTramite)
		{
			return tipoTramite == ABREV_CD;
		}
		public static bool isTransferencia(string tipoTramite)
		{
			return tipoTramite == ABREV_TR;
		}
		public static bool isFusion(string tipoTramite)
		{
			return tipoTramite == ABREV_FS;
		}
		public static bool isLicencia(string tipoTramite)
		{
			return tipoTramite == ABREV_LC;
		}*/
		#endregion Verificar tipo de tramite

		#region Guardar Expediente
		/// <summary>
		/// Genera un expediente para el TV
		/// </summary>
		/// <param name="expePadreID">Expediente de Registro o Renovación Padre</param>
		/// <returns>ID. del expediente generado</returns>
		public int guardarExpediente(Berke.DG.DBTab.Expediente expePadre)
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
		
			expe.NewRow(); 
			expe.Dat.ID					.Value = DBNull.Value;   //int PK  Oblig.
			expe.Dat.TramiteID			.Value = tipoTramite;   //int Oblig.
			expe.Dat.TramiteSitID		.Value = sitTramite;   //int Oblig.
			expe.Dat.ActaNro			.Value = bol.Dat.ExpNro.AsInt;   //int
			expe.Dat.ActaAnio			.Value = bol.Dat.ExpAnio.AsInt;   //int
			expe.Dat.OrdenTrabajoID		.Value = DBNull.Value;   //int
			expe.Dat.ClienteID			.Value = DBNull.Value;   //int
			expe.Dat.AgenteLocalID		.Value = ag.Dat.idagloc.AsInt;   //int
			expe.Dat.ExpedienteID		.Value = expePadre.Dat.ID.AsInt;   //int
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
			expe.Dat.MarcaID			.Value = expePadre.Dat.MarcaID.AsInt;
			//expe.Dat.MarcaRegRenID.Value = marRRID;
			expe.Dat.PoderInscID		.Value = DBNull.Value;   //int
			expe.Dat.FechaAband			.Value = DBNull.Value;   //smalldatetime
			expe.Dat.Obs				.Value = DBNull.Value;   //nvarchar
			//		expe.Dat.Acta			.Value = DBNull.Value;   //nvarchar ReadOnly
			//		expe.Dat.Publicacion			.Value = DBNull.Value;   //nvarchar ReadOnly
			expe.Dat.Label				.Value = DBNull.Value;   //nvarchar
			expe.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //datetime
			expe.Dat.PresentacionFecha	.Value = bol.Dat.SolicitudFecha.AsDateTime;   //datetime
			expe.PostNewRow(); 
			int expeID = expe.Adapter.InsertRow(); 
			return expeID;
			
		}
		#endregion Guardar Expediente

		#region Is TV?
		/// <summary>
		/// Verifica si un trámite corresponde a un TV
		/// </summary>
		/// <param name="tramite">Abrev del Trámite (nomenclatura del Boletín)</param>
		/// <returns>True si es un TV</returns>
		public static bool isTramiteVario(string tramite, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Tramite tr = new Berke.DG.DBTab.Tramite(db);
			tr.Dat.BolAbrev.Filter = ObjConvert.GetSqlPattern(tramite);
			tr.Adapter.ReadAll();

			for (tr.GoTop(); !tr.EOF; tr.Skip())
			{
				string [] abrev = tr.Dat.BolAbrev.AsString.Split(",".ToCharArray());
				for (int i=0; i<abrev.Length; i++)
				{
					if (abrev[i] == tramite)
					{
						return ( (tr.Dat.ID.AsInt == TIPO_CD ) || 
							(tr.Dat.ID.AsInt == TIPO_CN) || 
							(tr.Dat.ID.AsInt == TIPO_FS) ||
							(tr.Dat.ID.AsInt == TIPO_LC) || 
							(tr.Dat.ID.AsInt == TIPO_TR) );	
					}
				}
			}
			return false;								
		}
		public bool isTramiteVario(string tramite)
		{
			for (int i=0; i<bolAbrev.Length; i++)
			{
				if ( (bolAbrev[i] != "") && (bolAbrev[i] == tramite) )
				{
					return true;
				}
			}
			return false;								
		}
		public static bool isTramiteVario(int tramiteID)
		{
			return ( (tramiteID == TIPO_CD ) || 
				(tramiteID == TIPO_CN) || 
				(tramiteID == TIPO_FS) ||
				(tramiteID == TIPO_LC) || 
				(tramiteID == TIPO_TR) );	
		}
		#endregion Is TV?

		#region Posee TV posterior?
		/// <summary>
		/// Verifica si existe un trámite posterior al indicado por el 
		/// detalle actual del Boletín. Se verifica a partir del 
		/// trámite de Reg/Ren Padre.
		/// </summary>
		/// <param name="bdet">Detalle del boletín</param>
		/// <returns>True si posee trámites posteriores</returns>
		public bool tieneTVPosterior(Berke.DG.DBTab.BoletinDet bdet)
		{
			Berke.DG.DBTab.Expediente expeHijos;
			ArrayList lstSituaciones = new ArrayList();
			ArrayList lstTramiteSit  = new ArrayList();

			bool skipTramite = false;
			
			lstSituaciones.Add(GlobalConst.Situaciones.DESISTIDA); 
			lstSituaciones.Add(GlobalConst.Situaciones.CANCELACION_TV);
			lstSituaciones.Add(GlobalConst.Situaciones.MARCA_ANULADA);

			if (  TVService.isTramiteVario(bdet.Dat.Tramite.AsString,db)  )
			{
				expeHijos = expeServ.getTramites(bdet.Dat.ExpedienteID.AsInt);
				//Marca tiene tramites posteriores a la fecha del acta
				for (expeHijos.GoTop(); !expeHijos.EOF; expeHijos.Skip()) 
				{
					if (expeHijos.Dat.PresentacionFecha.AsDateTime > bdet.Dat.SolicitudFecha.AsDateTime )
					{ 

						lstTramiteSit = expeServ.getTramiteSitID(lstSituaciones,expeHijos.Dat.TramiteID.AsInt);

						for ( expeHijos.GoTop();  !expeHijos.EOF ; expeHijos.Skip() )
						{
							skipTramite = false;

							#region Verificar si el tr posterior es válido
							foreach ( string lstItem in lstTramiteSit )	
							{
								if (expeHijos.Dat.TramiteSitID.AsString == lstItem)
								{
									skipTramite = true;
									break;
								}
							}
							#endregion Verificar si el tr posterior es válido
						}
						
						if ( ! skipTramite ) 
						{
							return true;
						}
			

					}
				}
			}

			return false;





		}

		/// <summary>
		/// Verifica si un TV posee trámites posteriores válidos, es decir
		/// que no se encuentren en situacion Cancelada, desistida o anulada
		/// </summary>
		/// <param name="expe">Expediente del TV</param>
		/// <returns>true si posee un TV válido</returns>
		public bool tieneTVPosterior(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.Expediente expeHijos;
			ArrayList lstSituaciones = new ArrayList();
			ArrayList lstTramiteSit  = new ArrayList();

			bool skipTramite = false;
			
			lstSituaciones.Add(GlobalConst.Situaciones.DESISTIDA); 
			lstSituaciones.Add(GlobalConst.Situaciones.CANCELACION_TV);
			lstSituaciones.Add(GlobalConst.Situaciones.MARCA_ANULADA);

			if ( TVService.isTramiteVario(expe.Dat.TramiteID.AsInt) )
			{
				expeHijos = expeServ.getTramites(expe.Dat.ExpedienteID.AsInt);
				//Marca tiene tramites posteriores a la fecha del acta
				for (expeHijos.GoTop(); !expeHijos.EOF; expeHijos.Skip()) 
				{
					if (expeHijos.Dat.PresentacionFecha.AsDateTime > expe.Dat.PresentacionFecha.AsDateTime )
					{ 

						lstTramiteSit = expeServ.getTramiteSitID(lstSituaciones,expeHijos.Dat.TramiteID.AsInt);

						for ( expeHijos.GoTop();  !expeHijos.EOF ; expeHijos.Skip() )
						{
							skipTramite = false;

							#region Verificar si el tr posterior es válido
							foreach ( string lstItem in lstTramiteSit )	
							{
								if (expeHijos.Dat.TramiteSitID.AsString == lstItem)
								{
									skipTramite = true;
									break;
								}
							}
							#endregion Verificar si el tr posterior es válido
						}
						
						if ( ! skipTramite ) 
						{
							return true;
						}
			

					}
				}
			}

			return false;
		}
		#endregion Posee TV posterior?

		#region Desistir y Cancelar
		/// <summary>
		/// Realiza las operaciones que involucra el desistimiento de un TV
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud del desistimiento</param>
		/// <param name="obs">Observación que se registrará en la situación.</param>
		public void desistir(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación desistida
			expeServ.addSituacionDesistida(tipoTramite, solicitudFecha, obs, expe.Dat.ID.AsInt);
		}

		public void desistir(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
		}

		/// <summary>
		/// Realiza las operaciones que involucra la cancelación de un TV
		/// </summary>
		/// <param name="expe">Expediente</param>
		/// <param name="solicitudFecha">Fecha de solicitud del desistimiento</param>
		/// <param name="obs">Observación que se registrará en la situación.</param>
		public void cancelar(Berke.DG.DBTab.Expediente expe, DateTime solicitudFecha, string obs)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
			// Agregamos la situación cancelada
			expeServ.addSituacionCancelada(tipoTramite, solicitudFecha, obs, expe.Dat.ID.AsInt);
		}

		public void cancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			pasarInactiva(expe);
		}

		public void revertirDesistir(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			revertir(expe);
		}

		public void revertirCancelar(Berke.DG.DBTab.Expediente expe)
		{
			// Ponemos inactivo el trámite
			revertir(expe);
		}

		/// <summary>
		/// Pone inactivo un trámite vario
		/// </summary>
		/// <param name="expe">Expediente de Registro</param>
		public void pasarInactiva(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.MarcaRegRen regrenPadre;
			Berke.DG.DBTab.ExpedienteCampo expeCampo;

			// Obtenemos el expediente padre de Registro/Renovación
			expeServ.setExpeID(expe.Dat.ExpedienteID.AsInt);			
			expePadre = expeServ.getExpediente();
			regrenPadre = expeServ.getMarcaRegRen();

			// Obtenemos los campos asociados al TV
			expeServ.setExpediente(expe);
			expeCampo = expeServ.getCampos(expe.Dat.ID.AsInt);

			//Actualizamos los datos del propietario de la marca 
			//en base a expedienteCampo. Esto incluye también
			//PropietarioxMarca
			marServ.setMarcaID(expe.Dat.MarcaID.AsInt);
			marServ.updateFromCampos(expeCampo,false,true,false);

			expeServ.setExpediente(expePadre);
		
	 

			if( regrenPadre.Dat.ConcesionFecha.IsNull || (expe.Dat.PresentacionFecha.AsDateTime < regrenPadre.Dat.ConcesionFecha.AsDateTime)  )
			{
				if ( expeServ.updateFromCampos(expeCampo) == -1) 
				{
					throw new Exception("No se puede restaurar información de Poder/Propietario del expediente.");
				}
			}

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
		#region Revertir

		public void revertir(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.MarcaRegRen regrenPadre;
			Berke.DG.DBTab.ExpedienteCampo expeCampo;

			// Obtenemos el expediente padre de Registro/Renovación
			expeServ.setExpeID(expe.Dat.ExpedienteID.AsInt);			
			expePadre = expeServ.getExpediente();
			regrenPadre = expeServ.getMarcaRegRen();

			// Obtenemos los campos asociados al TV
			expeServ.setExpediente(expe);
			expeCampo = expeServ.getCampos(expe.Dat.ID.AsInt);

			//Actualizamos los datos del propietario de la marca 
			//en base a expedienteCampo. NO actualiza propietario por Marca
			marServ.setMarcaID(expe.Dat.MarcaID.AsInt);

			marServ.updateFromCampos(expeCampo,true,false,false);
			marServ.updatePropFromExpe(expe);

			/* Borramos la situacion actual del expediente de TV que se esta revirtiendo */
			//this.borrarSituacionActual(expe);

			/* seteo nuevamente el expediente Padre */
			expeServ.setExpediente(expePadre);
		
			if( regrenPadre.Dat.ConcesionFecha.IsNull || (expe.Dat.PresentacionFecha.AsDateTime < regrenPadre.Dat.ConcesionFecha.AsDateTime)  )
			{
				if ( expeServ.updateFromXXX(expe)== -1) 
				{
					throw new Exception("No se puede restaurar información de Poder/Propietario del expediente.");
				}
			}

		}
		#endregion

	}
}
