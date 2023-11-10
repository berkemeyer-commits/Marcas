#region Renovacion. BorrarMarca
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

public class BorrarMarca			
{
	public void Execute (Berke.Libs.Base.Helpers.AccesoDB db, int ordenTrabajoID, int marcaID)
	{
		#region Declaración de objetos locales
		Berke.DG.DBTab.OrdenTrabajo				ot				= new Berke.DG.DBTab.OrdenTrabajo( db );
		Berke.DG.DBTab.Expediente				expe			= new Berke.DG.DBTab.Expediente( db );
		Berke.DG.DBTab.ExpedienteCampo			eCampo			= new Berke.DG.DBTab.ExpedienteCampo( db ) ;
		Berke.DG.DBTab.MarcaRegRen				marcaRegRen		= new Berke.DG.DBTab.MarcaRegRen( db );
		Berke.DG.DBTab.Marca					marca			= new Berke.DG.DBTab.Marca ( db );
		Berke.DG.DBTab.Expediente_Instruccion	expeInst		= new Berke.DG.DBTab.Expediente_Instruccion( db );
		Berke.DG.DBTab.ExpedienteXPropietario   ePro			= new Berke.DG.DBTab.ExpedienteXPropietario( db );
		Berke.DG.DBTab.ExpedienteXPoder			ePod			= new Berke.DG.DBTab.ExpedienteXPoder( db );
		Berke.DG.DBTab.PropietarioXMarca		PxM				= new Berke.DG.DBTab.PropietarioXMarca( db );
		Berke.DG.DBTab.Expediente_Situacion		expeSit			= new Berke.DG.DBTab.Expediente_Situacion( db );
		Berke.DG.DBTab.Marca_ClaseIdioma		marcaIdioma		= new Berke.DG.DBTab.Marca_ClaseIdioma( db );

		Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit(db);
		Berke.DG.DBTab.Situacion sit          = new Berke.DG.DBTab.Situacion(db);

		int tipoTramite = Convert.ToInt16(GlobalConst.Marca_Tipo_Tramite.RENOVACION);
		#endregion Declaración de objetos locales

		#region Expediente
		expe.Dat.MarcaID.Filter = marcaID;
		expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
		expe.Adapter.ReadAll();
		int expedienteID = expe.Dat.ID.AsInt;
		#endregion

		#region Controlar situacion de HI
		tramitesit.ClearFilter();
		tramitesit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
		sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
		if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
		{
			throw new Exception("Solo se pueden eliminar o modificar las HI que se encuentren en situación hoja de inicio");
		}
		#endregion Controlar situacion de HI



		#region Expediente-Marca
		int expedientePadreID = expe.Dat.ExpedienteID.AsInt;
		expe.Edit();
		expe.Dat.MarcaRegRenID.Value = DBNull.Value;
		expe.Dat.MarcaID.Value = DBNull.Value;
		expe.PostEdit();
		expe.Adapter.UpdateRow();
		#endregion Expediente-Marca

		#region PropietarioXMarca
		PxM.Dat.MarcaID.Filter = marcaID;
		PxM.Adapter.ReadAll();
		for (PxM.GoTop();!PxM.EOF;PxM.Skip()) {
			PxM.Adapter.DeleteRow();
		}
		#endregion PropietarioXMarca

		#region Expediente_Situacion
		expeSit.Dat.ExpedienteID.Filter = expedienteID;
		expeSit.Adapter.ReadAll();
		for (expeSit.GoTop();!expeSit.EOF;expeSit.Skip()) {
			expeSit.Adapter.DeleteRow();
		}
		#endregion Expediente_Situacion

		#region Expediente_Instruccion
		expeInst.Dat.ExpedienteID.Filter = expedienteID;
		expeInst.Adapter.ReadAll();
		for (expeInst.GoTop();!expeInst.EOF;expeInst.Skip()) {
			expeInst.Adapter.DeleteRow();
		}
		#endregion Expediente_Instruccion

		#region Marca_ClaseIdioma
		marcaIdioma.ClearFilter();
		marcaIdioma.Dat.MarcaID.Filter = marcaID;
		marcaIdioma.Adapter.ReadAll();
		for (marcaIdioma.GoTop(); !marcaIdioma.EOF; marcaIdioma.Skip()) {
			marcaIdioma.Adapter.DeleteRow();
		}
		#endregion Marca_ClaseIdioma		

		#region MarcaRegRen
		marcaRegRen.Dat.ExpedienteID.Filter = expedienteID;
		marcaRegRen.Adapter.ReadAll();
		marcaRegRen.Adapter.DeleteRow();
		#endregion MarcaRegRen		

		#region ExpedienteXPoder
		ePod.Dat.ExpedienteID.Filter = expedienteID;
		ePod.Adapter.ReadAll();
		for (ePod.GoTop();!ePod.EOF;ePod.Skip()) {
			ePod.Adapter.DeleteRow();
		}
		#endregion ExpedienteXPoder

		#region ExpedienteXPropietario
		ePro.Dat.ExpedienteID.Filter = expedienteID;
		ePro.Adapter.ReadAll();
		for (ePro.GoTop();!ePro.EOF;ePro.Skip()) {
			ePro.Adapter.DeleteRow();
		}
		#endregion ExpedienteXPropietario

		#region Marca
		//Recuperar marca actual
		marca.ClearFilter();
		marca.Adapter.ReadByID(marcaID);
		int claseActualID = marca.Dat.ClaseID.AsInt;

		//Recuperar marca anterior
		expe.ClearFilter();
		expe.Adapter.ReadByID(expedientePadreID);
		marca.ClearFilter();
		marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);

		if (marca.Dat.ClaseID.AsInt == claseActualID) {
			/* Si la marca fue renovada en la misma clase y misma edición,
			 * se debe restaurar el propietario actual y actualizar el 
			 * expediente vigente de la marca */

			#region Recuperar propietario anterior de expediente campo
			//Nombre del propietario anterior
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
			eCampo.Adapter.ReadAll();
			string prop_anterior_nombre = eCampo.Dat.Valor.AsString;
			//Dirección del propietario anterior
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
			eCampo.Adapter.ReadAll();
			string prop_anterior_dir = eCampo.Dat.Valor.AsString;
			//Pais del propietario anterior
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
			eCampo.Adapter.ReadAll();
			string prop_anterior_pais = eCampo.Dat.Valor.AsString;
			//Id del propietario anterior
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_ID;
			eCampo.Adapter.ReadAll();
			string prop_anterior_id = eCampo.Dat.Valor.AsString.Trim();
			#endregion Recuperar propietario anterior de expediente campo

			#region Actualizar expediente vigente y propietario en marca actual
			marca.ClearFilter();
			marca.Adapter.ReadByID(marcaID);
			marca.Edit();
			marca.Dat.ExpedienteVigenteID.Value = expedientePadreID;
			marca.Dat.Propietario.Value = prop_anterior_nombre;
			marca.Dat.ProDir.Value = prop_anterior_dir;
			marca.Dat.ProPais.Value = prop_anterior_pais;
			marca.PostEdit();
			marca.Adapter.UpdateRow();
			#endregion Actualizar expediente vigente y propietario en marca actual

			#region Actualizar PropietarioXMarca
			string [] aPropietarios = prop_anterior_id.Split( ((String)",").ToCharArray() );
			for (int i = 0; i < aPropietarios.Length; i++) {
				PxM.NewRow();
				PxM.Dat.MarcaID.Value = marcaID;
				PxM.Dat.PropietarioID.Value = aPropietarios[i];
				PxM.PostNewRow();
				PxM.Adapter.InsertRow();
			}
			#endregion Actualizar PropietarioXMarca
		} 
		else 
		{
            /* Si la marca fue renovada en otra edición, deberá ser borrada
			 * y en el caso en que no exista otra marca que apunte a la marca
			 * padre, se deberá restaurar a la marca padre como vigente */
			expe.ClearFilter();
			expe.Dat.ExpedienteID.Filter = expedientePadreID;
			expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;			
			expe.Adapter.ReadAll();
			if (expe.RowCount == 1) {
				#region Revivir a la marca padre
				expe.ClearFilter();
				expe.Adapter.ReadByID(expedientePadreID);
				marca.ClearFilter();
				marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
				marca.Edit();
				marca.Dat.Vigente			.Value = true;   //bit
				marca.PostEdit();
				marca.Adapter.UpdateRow();
				#endregion Revivir a la marca padre
			}
			#region Recuperar y borrar marca actual
			marca.ClearFilter();
			marca.Adapter.ReadByID(marcaID);
			marca.Adapter.DeleteRow();
			#endregion Recuperar y borrar marca actual
		}
		#endregion Marca

		#region ExpedienteCampo
		eCampo.ClearFilter();
		eCampo.Dat.ExpedienteID.Filter = expedienteID;
		eCampo.Adapter.ReadAll();
		for (eCampo.GoTop();!eCampo.EOF;eCampo.Skip()) {
			eCampo.Adapter.DeleteRow();
		}
		#endregion ExpedienteCampo

		#region Expediente
		expe.ClearFilter();
		expe.Adapter.ReadByID(expedienteID);		
		expe.Adapter.DeleteRow();
		#endregion Expediente
	}
}
#endregion Renovacion. BorrarMarca

#region Renovacion.	Delete
	public class Delete: IAction
	{	
		public void Execute( Command cmd ) 
		{
			#region Declaración de objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			Berke.DG.RenovacionDG inDG	= new Berke.DG.RenovacionDG( cmd.Request.RawDataSet );
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo;
			int ordenTrabajoID = ot.Dat.ID.AsInt;
			ot.InitAdapter( db );
			Berke.DG.ViewTab.vRenovacionMarca vRM = inDG.vRenovacionMarca;
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab();
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.PropietarioXMarca PxM = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.Expediente_Situacion	expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.Expediente_Instruccion expeInst = new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca ( db );
			Berke.DG.DBTab.MarcaRegRen marcaRegRen = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.ExpedienteCampo eCampo = new Berke.DG.DBTab.ExpedienteCampo( db ) ;
			Berke.DG.DBTab.ExpedienteXPoder ePod = new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.ExpedienteXPropietario ePro = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.Marca_ClaseIdioma marcaIdioma = new Berke.DG.DBTab.Marca_ClaseIdioma( db );
			System.Collections.ArrayList filtroxExpediente = new System.Collections.ArrayList();
			System.Collections.ArrayList filtroxExpedientePadre = new System.Collections.ArrayList();
			System.Collections.ArrayList filtroxMarca = new System.Collections.ArrayList();
			#endregion Declaración de objetos locales

			try 
			{
				try 
				{
					db.IniciarTransaccion();

					#region Actualizar expedientes
					for (vRM.GoTop(); !vRM.EOF; vRM.Skip()) 
					{
						expe.ClearFilter();
						expe.Adapter.ReadByID( vRM.Dat.ExpedienteID.AsInt );
						if (expe.Dat.TramiteSitID.AsInt!= 29)
							throw new Exception("No se puede eliminar la HI de Renovación. Al menos una marca ha cambiado de situación");

						filtroxExpediente.Add(expe.Dat.ID.AsInt);
						filtroxExpedientePadre.Add(expe.Dat.ExpedienteID.AsInt);
						filtroxMarca.Add(expe.Dat.MarcaID.AsInt);

						//Actualizar Expediente
						expe.Edit();
						expe.Dat.MarcaRegRenID.Value = DBNull.Value;
						expe.Dat.MarcaID.Value = DBNull.Value;
						expe.PostEdit();
						expe.Adapter.UpdateRow();
					}
					#endregion Actualizar expedientes

					#region PropietarioXMarca
					PxM.ClearFilter();
					PxM.Dat.MarcaID.Filter =  new DSFilter(filtroxMarca);
					PxM.Adapter.ReadAll();
					for (PxM.GoTop();!PxM.EOF;PxM.Skip()) 
					{
						PxM.Adapter.DeleteRow();
					}
					#endregion PropietarioXMarca

					#region Expediente_Situacion
					expeSit.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
					expeSit.Adapter.ReadAll();
					for (expeSit.GoTop();!expeSit.EOF;expeSit.Skip()) 
					{
						expeSit.Adapter.DeleteRow();
					}
					#endregion Expediente_Situacion

					#region Expediente_Instruccion
					expeInst.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
					expeInst.Adapter.ReadAll();
					for (expeInst.GoTop();!expeInst.EOF;expeInst.Skip()) 
					{
						expeInst.Adapter.DeleteRow();
					}
					#endregion Expediente_Instruccion

					#region Marca_ClaseIdioma
					marcaIdioma.ClearFilter();
					marcaIdioma.Dat.MarcaID.Filter = new DSFilter(filtroxMarca);
					marcaIdioma.Adapter.ReadAll();
					for (marcaIdioma.GoTop(); !marcaIdioma.EOF; marcaIdioma.Skip()) 
					{
						marcaIdioma.Adapter.DeleteRow();
					}
					#endregion Marca_ClaseIdioma

					#region MarcaRegRen
					marcaRegRen.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
					marcaRegRen.Adapter.ReadAll();
					for (marcaRegRen.GoTop();!marcaRegRen.EOF;marcaRegRen.Skip()) 
					{
						marcaRegRen.Adapter.DeleteRow();
					}
					#endregion MarcaRegRen

					#region ExpedienteXPoder
					ePod.ClearFilter();
					ePod.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
					ePod.Adapter.ReadAll();
					for (ePod.GoTop();!ePod.EOF;ePod.Skip()) 
					{
						ePod.Adapter.DeleteRow();
					}
					#endregion ExpedienteXPoder

					#region ExpedienteXPropietario
					ePro.ClearFilter();
					ePro.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
					ePro.Adapter.ReadAll();
					for (ePro.GoTop();!ePro.EOF;ePro.Skip()) 
					{
						ePro.Adapter.DeleteRow();
					}
					#endregion ExpedienteXPropietario
				
					#region Marca
					int expedientePadreID = 0;
					for (vRM.GoTop(); !vRM.EOF; vRM.Skip()) 
					{
						//Recuperar el expediente padre
						expe.ClearFilter();				
						expe.Adapter.ReadByID(vRM.Dat.ExpedienteID.AsInt);
						expedientePadreID = expe.Dat.ExpedienteID.AsInt;

						if (vRM.Dat.ClaseAntID.AsInt == vRM.Dat.ClaseID.AsInt) 
						{
							/* Si la marca fue renovada en la misma clase y misma edición,
							 * se debe restaurar el propietario actual y actualizar el 
							 * expediente vigente de la marca */
						
							#region Recuperar propietario anterior de expediente campo
							//Nombre del propietario anterior
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
							eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
							eCampo.Adapter.ReadAll();
							string prop_anterior_nombre = eCampo.Dat.Valor.AsString;
							//Dirección del propietario anterior
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
							eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
							eCampo.Adapter.ReadAll();
							string prop_anterior_dir = eCampo.Dat.Valor.AsString;
							//Pais del propietario anterior
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
							eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
							eCampo.Adapter.ReadAll();
							string prop_anterior_pais = eCampo.Dat.Valor.AsString;
							//Id del propietario anterior
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
							eCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_ID;
							eCampo.Adapter.ReadAll();
							string prop_anterior_id = eCampo.Dat.Valor.AsString.Trim();
							#endregion Recuperar propietario anterior de expediente campo

							#region Actualizar expediente vigente y propietario en marca actual
							marca.ClearFilter();
							marca.Adapter.ReadByID(vRM.Dat.MarcaID.AsInt);
							marca.Edit();
							marca.Dat.ExpedienteVigenteID.Value = expedientePadreID;
							marca.Dat.Propietario.Value = prop_anterior_nombre;
							marca.Dat.ProDir.Value = prop_anterior_dir;
							marca.Dat.ProPais.Value = prop_anterior_pais;
							marca.PostEdit();
							marca.Adapter.UpdateRow();
							#endregion Actualizar expediente vigente y propietario en marca actual

							#region Actualizar PropietarioXMarca
							string [] aPropietarios = prop_anterior_id.Split( ((String)",").ToCharArray() );
							for (int i = 0; i < aPropietarios.Length; i++) 
							{
								PxM.NewRow();
								PxM.Dat.MarcaID.Value = vRM.Dat.MarcaID.AsInt;
								PxM.Dat.PropietarioID.Value = aPropietarios[i];
								PxM.PostNewRow();
								PxM.Adapter.InsertRow();
							}
							#endregion Actualizar PropietarioXMarca
						} 
						else 
						{
							/* Si la marca fue renovada en otra edición, deberá ser borrada
							 * y en el caso en que no exista otra marca que apunte a la marca
							 * padre, se deberá restaurar a la marca padre como vigente */
							expe.ClearFilter();
							expe.Dat.ExpedienteID.Filter = expedientePadreID;
							expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;			
							expe.Adapter.ReadAll();
							if (expe.RowCount == 1) 
							{
								#region Revivir a la marca padre
								marca.ClearFilter();
								marca.Adapter.ReadByID(vRM.Dat.MarcaAnteriorID.AsInt);
								marca.Edit();
								marca.Dat.Vigente			.Value = true;   //bit
								marca.PostEdit();
								marca.Adapter.UpdateRow();
								#endregion Revivir a la marca padre
							}
							#region Recuperar y borrar marca actual
							marca.ClearFilter();
							marca.Adapter.ReadByID(vRM.Dat.MarcaID.AsInt);
							marca.Adapter.DeleteRow();
							#endregion Recuperar y borrar marca actual
						}

						#region ExpedienteCampo
						eCampo.ClearFilter();
						eCampo.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
						eCampo.Adapter.ReadAll();
						for (eCampo.GoTop();!eCampo.EOF;eCampo.Skip()) 
						{
							eCampo.Adapter.DeleteRow();
						}
						#endregion ExpedienteCampo

						#region Expediente
						expe.ClearFilter();				
						expe.Adapter.ReadByID(vRM.Dat.ExpedienteID.AsInt);
						expe.Adapter.DeleteRow();
						#endregion Expediente
					}
					#endregion Marca

					ot.Adapter.DeleteRow();
					db.Commit();
				} 
				catch ( Exception ex ) 
				{
					db.RollBack();
				
					throw new Exception(" Error en Renovacion Delete : " + ex.Message );
				}

				DataSet tmp_OutDS = new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = true;
				outTB.Dat.Entero.Value = ordenTrabajoID;
				outTB.PostNewRow();
				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );
			}
			finally {
				db.CerrarConexion();
			}

		} // End RenovacionDelete class
	}// end namespace 

	/* Entrada para el fwk.Config

					<action code="Renovacion_Delete" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.Delete,Berke.Marcas.BizActions"
						request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
						DebugMode="true" log-header="true" log-request="true" log-response="true" />
	*/
#endregion Renovacion. Delete

#region Renovacion.	Upsert
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{
			int tipoTramite;
			//Obtengo el id del tramite RENOVACION
			tipoTramite=Convert.ToInt16(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION);

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.RenovacionDG inDG	= new Berke.DG.RenovacionDG( cmd.Request.RawDataSet );
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab();
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais( db);
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario( db );
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder( db );
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.PropietarioXMarca PxM = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.ExpedienteXPropietario ep = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.ExpedienteXPoder epoder = new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.Expediente_Instruccion ei = new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.Tramite_Sit	tramitesit = new Berke.DG.DBTab.Tramite_Sit( db );
			Berke.DG.DBTab.Expediente expe2 = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.MarcaRegRen marcaRegRen2 = new Berke.DG.DBTab.MarcaRegRen( db );			
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase( db );
			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion(db);

			#region Asignar alias a las tablas de inDG
			Berke.DG.DBTab.OrdenTrabajo				ot				= inDG.OrdenTrabajo ;
			Berke.DG.DBTab.Expediente				expe			= inDG.Expediente ;
			Berke.DG.DBTab.ExpedienteCampo			eCampo			= inDG.ExpedienteCampo ;
			Berke.DG.DBTab.MarcaRegRen				marcaRegRen		= inDG.MarcaRegRen ;
			Berke.DG.DBTab.Marca					marca			= inDG.Marca ;
			Berke.DG.DBTab.Marca_ClaseIdioma		marcaClaseIdioma= inDG.Marca_ClaseIdioma ;
			Berke.DG.ViewTab.vRenovacionMarca		vRenMarca		= inDG.vRenovacionMarca ;
			Berke.DG.DBTab.Expediente_Instruccion	expeInst		= inDG.Expediente_Instruccion;
			Berke.DG.DBTab.ExpedienteXPropietario   ePro			= inDG.ExpedienteXPropietario;
			Berke.DG.DBTab.ExpedienteXPoder			ePod			= inDG.ExpedienteXPoder;
			Berke.DG.DBTab.Atencion					at				= inDG.Atencion;

			//Inicializar Adapters		
			ot.InitAdapter( db );
			eCampo.InitAdapter ( db );
			expe.InitAdapter( db );
			expeInst.InitAdapter( db );
			ePro.InitAdapter( db );
			ePod.InitAdapter( db );
			marcaRegRen.InitAdapter( db );
			marca.InitAdapter( db );
			marcaClaseIdioma.InitAdapter ( db );
			at.InitAdapter ( db );
			#endregion
			bool insert = false;
			int ordenTrabajoID = 0;
			int expedienteID = 0;
			int NizaAntId = 0;
			int RegRenID = 0;
			int clienteID = 0;
			int marcaID = 0;
			//int sitTramite = 29;			
			int funcionarioID = 0;
			int atencionID = 0;
			string str_propietario = "";

			

			#region Obtener tramitesitID
			tramitesit.Dat.TramiteID.Filter = tipoTramite;
			tramitesit.Dat.SituacionID.Filter = Convert.ToInt16(GlobalConst.Situaciones.HOJA_INICIO);
			tramitesit.Adapter.ReadAll();
			int sitTramite = tramitesit.Dat.ID.AsInt;
			#endregion Obtener tramitesitID

			try 
			{
				try
				{
					db.IniciarTransaccion();			
					clienteID=ot.Dat.ClienteID.AsInt;
					funcionarioID = ot.Dat.FuncionarioID.AsInt;
					atencionID = ot.Dat.AtencionID.AsInt;

					#region Nueva Atención
					if (! at.EOF ) 
					{
						at.Edit();
						at.Dat.ClienteID.Value = clienteID;
						at.Dat.AreaID.Value = (int)GlobalConst.Area.RENOVACION;
						at.PostEdit();
						atencionID = at.Adapter.InsertRow();
					}
					#endregion Nueva Atención
				
					#region OrdenTrabajo
					if (ot.Dat.ID.AsInt == -1) 
					{
						ot.Edit();
						ot.Dat.TrabajoTipoID			.Value = tipoTramite;   //2 es Renovacion
						ot.Dat.Nro			.Value = Berke.Libs.Base.Helpers.Calc.OrdenTrabajoNro(tipoTramite);   //numero magico
						ot.Dat.Anio			.Value = System.DateTime.Now.Year;   //int Oblig.
						ot.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
						if (atencionID != 0) 
						{
							ot.Dat.AtencionID	.Value = atencionID;
						}
						ot.PostEdit();
						ordenTrabajoID = ot.Adapter.InsertRow();
						insert = true;
					} 
					else 
					{
						ot.Edit();
						if (atencionID != 0) 
						{
							ot.Dat.AtencionID	.Value = atencionID;
						}
						ot.PostEdit();
						ordenTrabajoID = ot.Dat.ID.AsInt;
						ot.Adapter.UpdateRow(); 
						insert = false;
					}
				
					#endregion OrdenTrabajo
				
					#region Borrar marcas de la BD
					/* Eliminar de la base de datos las marcas-clases que fueron eliminadas por
					 * el usuario en la interfaz de modificación. */
					expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
					expe.Adapter.ReadAll();
					bool borrar_marca = true;
					BorrarMarca bm = new BorrarMarca();
					for (expe.GoTop(); ! expe.EOF;	expe.Skip())
					{
						borrar_marca = true;
						for (vRenMarca.GoTop(); ! vRenMarca.EOF;	vRenMarca.Skip())
						{
							if (expe.Dat.MarcaID.AsInt == vRenMarca.Dat.MarcaID.AsInt) 
							{
								borrar_marca = false;
								break;
							}						
						}
						if (borrar_marca == true) 
						{
							bm.Execute(db, ordenTrabajoID, expe.Dat.MarcaID.AsInt);
						}
					}
					#endregion Borrar marcas de la BD

					
					for (vRenMarca.GoTop(); ! vRenMarca.EOF;	vRenMarca.Skip()) 
					{
						marca.Adapter.ReadByID(vRenMarca.Dat.MarcaAnteriorID.AsInt);
						marcaID = marca.Dat.ID.AsInt;

						#region expediente
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expe.NewRow();
							expe.Dat.OrdenTrabajoID		.Value = ordenTrabajoID;   //int
							expe.Dat.ExpedienteID		.Value = vRenMarca.Dat.ExpedienteID.AsInt;   //int
							/*[09-jul-2007] BUG#376
							 * El tramitesitid del expediente se guardaba solo cuando se insertaba una
							 * OT. El bug tiene su origen  porque se modificaba la HI en el sentido de 
							 * que se le agregaba una nueva marca a una OT ya existente y por ello no se
							 * asignaba el tramitesitid al expediente.
							 * */
							expe.Dat.TramiteSitID		.Value = sitTramite;   //int Oblig.
						} 
						else 
						{
							expe.ClearFilter();
							expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
							expe.Dat.MarcaID.Filter = vRenMarca.Dat.MarcaID.AsInt;
							expe.Adapter.ReadAll();

							#region Controlar situacion de HI
							tramitesit.ClearFilter();
							tramitesit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
							sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
							if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
							{
								throw new Exception("Solo se pueden modificar las HI que se encuentren en situación hoja de inicio");
							}
							#endregion Controlar situacion de HI


							expe.Edit();
							
						}
						expe.Dat.TramiteID			.Value = tipoTramite;   //int Oblig.

						//mbaez. Se corrigio problema de la pérdida de la situación actual
						//en la operación de actualización.

						/*[09-jul-2007] BUG#376
						 * 
						 * 
						if (insert) 
						{
							expe.Dat.TramiteSitID		.Value = sitTramite;   //int Oblig.
						}
						*/
						

						expe.Dat.ClienteID			.Value = clienteID;   //int
						expe.Dat.Nuestra			.Value = true;   //bit Oblig.
						expe.Dat.Sustituida			.Value = false;   //bit Oblig.
						expe.Dat.StandBy			.Value = false;   //bit Oblig.
						expe.Dat.Vigilada			.Value = true;   //bit Oblig.
						expe.Dat.Concluido			.Value = false;   //bit Oblig.
						expe.Dat.VencimientoFecha	.Value = vRenMarca.Dat.Vencimiento.AsDateTime.AddYears(10);
						expe.Dat.Label				.Value = vRenMarca.Dat.Referencia.AsString;
						expe.Dat.MarcaID			.Value = vRenMarca.Dat.MarcaAnteriorID.AsInt;   //int
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expe.PostNewRow();
							expedienteID = expe.Adapter.InsertRow();
						} 
						else 
						{
							expe.PostEdit();
							expedienteID = expe.Dat.ID.AsInt;
							expe.Adapter.UpdateRow();
						}
						#endregion expediente

						#region Controlar otra renovación en tramite
						marcaRegRen2.ClearFilter();
						marcaRegRen2.Dat.RegistroNro.Filter = vRenMarca.Dat.RegistroNro.AsInt;
						marcaRegRen2.Adapter.ReadAll();

						expe2.ClearFilter();
						expe2.Dat.ExpedienteID.Filter = marcaRegRen2.Dat.ExpedienteID.AsInt;
						expe2.Dat.TramiteID.Filter = (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION;
						expe2.Adapter.ReadAll();
						for (expe2.GoTop(); !expe2.EOF; expe2.Skip()) 
						{
							if ( (expe2.Dat.ID.AsInt != expedienteID) &
								(expe2.Dat.OrdenTrabajoID.AsInt != expe.Dat.OrdenTrabajoID.AsInt) ) 
							{
								if ( (marca.Dat.Nuestra.AsBoolean) &
									(expe2.Dat.TramiteSitID.AsInt != (int)GlobalConst.Situaciones.ARCHIVADA) ) 
								{
									throw new Exception("Existe otra renovación en trámite para la marca " + marca.Dat.Denominacion.AsString);
								}

								if ( (!marca.Dat.Nuestra.AsBoolean) &
									(expe2.Dat.TramiteSitID.AsInt != (int) GlobalConst.Situaciones.CONCEDIDA) ) 
								{
									throw new Exception("Existe otra renovación en trámite para la marca " + marca.Dat.Denominacion.AsString);
								}
							}
						}
						#endregion Controlar otra renovación en tramite

						#region MarcaRegRen
						//Insertar MarcaRegRen
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							marcaRegRen.NewRow(); 
							marcaRegRen.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
						} 
						else 
						{
							marcaRegRen.Dat.ExpedienteID.Filter = expedienteID;
							marcaRegRen.Adapter.ReadAll();
							marcaRegRen.Edit();
						}
						marcaRegRen.Dat.RegistroNro		.Value = DBNull.Value;   //int
						marcaRegRen.Dat.RegistroAnio	.Value = DBNull.Value;   //int
						marcaRegRen.Dat.ConcesionFecha	.Value = DBNull.Value;   //smalldatetime
						marcaRegRen.Dat.Limitada		.Value = vRenMarca.Dat.Limitada.AsBoolean;   //bit
						marcaRegRen.Dat.Vigente			.Value = false;   //bit
						marcaRegRen.Dat.RefMarca		.Value = DBNull.Value;   //nvarchar
						marcaRegRen.Dat.ObsAvRen		.Value = DBNull.Value;   //nvarchar
						marcaRegRen.Dat.TituloError		.Value = DBNull.Value;   //bit
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							marcaRegRen.PostNewRow();
							RegRenID = marcaRegRen.Adapter.InsertRow();
						} 
						else 
						{
							marcaRegRen.PostEdit();
							RegRenID = marcaRegRen.Dat.ID.AsInt;
							marcaRegRen.Adapter.UpdateRow();
						}

						//Actualizar Expediente
						expe.Adapter.ReadByID(expedienteID);
						expe.Edit();
						expe.Dat.MarcaRegRenID.Value = RegRenID;
						expe.PostEdit();
						expe.Adapter.UpdateRow();
						#endregion MarcaRegRen

						#region Guardar propietario anterior en ExpedienteCampo
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							//Insertar propietario anterior en expediente campo
							eCampo.NewRow();
							eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							eCampo.Dat.Campo			.Value = GlobalConst.PROP_ANTERIOR_NOMBRE;
							eCampo.Dat.Valor			.Value = marca.Dat.Propietario.AsString;   //nvarchar Oblig.
							eCampo.PostNewRow();
							eCampo.Adapter.InsertRow();
 
							eCampo.NewRow(); 
							eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							eCampo.Dat.Campo			.Value = GlobalConst.PROP_ANTERIOR_DIR;
							eCampo.Dat.Valor			.Value = marca.Dat.ProDir.AsString;   //nvarchar Oblig.
							eCampo.PostNewRow();
							eCampo.Adapter.InsertRow();

							eCampo.NewRow(); 
							eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							eCampo.Dat.Campo			.Value = GlobalConst.PROP_ANTERIOR_PAIS;
							eCampo.Dat.Valor			.Value = marca.Dat.ProPais.AsString;   //nvarchar Oblig.
							eCampo.PostNewRow();
							eCampo.Adapter.InsertRow();

							PxM.ClearFilter();
							PxM.Dat.MarcaID.Filter = marca.Dat.ID.AsInt;
							PxM.Adapter.ReadAll();
							str_propietario = "";
							for (PxM.GoTop(); !PxM.EOF; PxM.Skip()) 
							{
								if (str_propietario == "") 
								{
									str_propietario = PxM.Dat.PropietarioID.AsString;
								} 
								else 
								{
									str_propietario = str_propietario + "," + PxM.Dat.PropietarioID.AsString;
								}
							}
							eCampo.NewRow(); 
							eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							eCampo.Dat.Campo			.Value = GlobalConst.PROP_ANTERIOR_ID;
							eCampo.Dat.Valor			.Value = str_propietario;   //nvarchar Oblig.
							eCampo.PostNewRow();
							eCampo.Adapter.InsertRow();
						} 
						else 
						{
							/* Si es una modificación de la renovación, se deben borrar de
							 * expedientecampo solo los datos del propietario actual 
							 * ya que el propietario anterior ya fue insertado */
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = expedienteID;
							eCampo.Adapter.ReadAll();
							for (eCampo.GoTop(); !eCampo.EOF; eCampo.Skip()) 
							{
								if ( (eCampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_NOMBRE) |
									(eCampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_DIR) |
									(eCampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_PAIS) ) 
								{
									eCampo.Delete();
									eCampo.Adapter.DeleteRow();
								}
							}
						}
						#endregion Guardar propietario anterior en ExpedienteCampo
					
						#region Marca
						ePod.GoTop();
						if (vRenMarca.Dat.ClaseID.AsInt == vRenMarca.Dat.ClaseAntID.AsInt) 
						{
							#region Actualizar Marca Vigente
							marca.Edit();
							marca.Dat.Denominacion		.Value = vRenMarca.Dat.Denominacion.AsString;   //nvarchar

							marca.Dat.DenominacionClave	.Value = vRenMarca.Dat.DenominacionClave.AsString;   //nvarchar

							marca.Dat.ClaseDescripEsp	.Value = vRenMarca.Dat.DesEspLim.AsString;   //nvarchar
							marca.Dat.Limitada			.Value = vRenMarca.Dat.Limitada.AsBoolean;   //bit
							marca.Dat.ClienteID			.Value = clienteID;   //int
							marca.Dat.Nuestra			.Value = true;   //bit Oblig.
							marca.Dat.Vigilada			.Value = true;   //bit Oblig.
							marca.Dat.Sustituida		.Value = false;   //bit Oblig.
							marca.Dat.StandBy			.Value = false;   //bit Oblig.
							marca.Dat.Vigente			.Value = true;   //bit
							marca.Dat.ExpedienteVigenteID			.Value = expedienteID;   //int
							if (vRenMarca.Dat.LogotipoID.AsInt > 0) 
							{
								marca.Dat.LogotipoID		.Value = vRenMarca.Dat.LogotipoID.AsInt;
							}
							//marca.Dat.MarcaRegRenID.Value = RegRenID;
							if (ePod.EOF == false) 
							{
								//leer poder							
								poder.Adapter.ReadByID(ePod.Dat.PoderID.AsInt);
								marca.Dat.Propietario		.Value = poder.Dat.Denominacion.AsString;
								marca.Dat.ProDir			.Value = poder.Dat.Domicilio.AsString;
								pais.Adapter.ReadByID(poder.Dat.PaisID.AsInt);
								marca.Dat.ProPais			.Value = pais.Dat.paisalfa.AsString;
							} 
							else 
							{
								//leer propietario
								propietario.Adapter.ReadByID(ePro.Dat.PropietarioID.AsInt);
								marca.Dat.Propietario		.Value = propietario.Dat.Nombre.AsString;
								marca.Dat.ProDir			.Value = propietario.Dat.Direccion.AsString;
								pais.Adapter.ReadByID(propietario.Dat.PaisID.AsInt);
								marca.Dat.ProPais			.Value = pais.Dat.paisalfa.AsString;
							}
							marca.PostEdit();
							#endregion Actualizar Marca Vigente
						} 
						else 
						{
							#region Comparar edicion de clase
							clase.ClearFilter();
							clase.Adapter.ReadByID(vRenMarca.Dat.ClaseAntID.AsInt);
							NizaAntId = clase.Dat.NizaEdicionID.AsInt;
							clase.ClearFilter();
							clase.Adapter.ReadByID(vRenMarca.Dat.ClaseID.AsInt);
							if (NizaAntId == clase.Dat.NizaEdicionID.AsInt) 
							{
								throw new Exception("No se pueden seleccionar clases diferentes de la misma edición para la marca " + marca.Dat.Denominacion.AsString);
							}
							#endregion Comparar edicion de clase

							//Actualizar marca anterior
							marca.Edit();
							marca.Dat.Vigente			.Value = false;   //bit
							marca.PostEdit();

							#region Insertar Nueva Marca
							//Hay que insertar una nueva marca
							Berke.DG.DBTab.Marca nuevaMarca = new Berke.DG.DBTab.Marca( db );
							if (vRenMarca.Dat.MarcaID.AsInt < 0) 
							{
								nuevaMarca.NewRow(); 
							} 
							else 
							{
								nuevaMarca.Adapter.ReadByID(vRenMarca.Dat.MarcaID.AsInt);
								nuevaMarca.Edit();
							}
							nuevaMarca.Dat.Denominacion		.Value = vRenMarca.Dat.Denominacion.AsString;   //nvarchar
							nuevaMarca.Dat.DenominacionClave.Value = vRenMarca.Dat.DenominacionClave.Value;   //nvarchar
							//nuevaMarca.Dat.DenominacionClave.Value = marca.Dat.DenominacionClave.Value;   //nvarchar
							nuevaMarca.Dat.Fonetizada		.Value = marca.Dat.Fonetizada.Value;   //nvarchar
							nuevaMarca.Dat.MarcaTipoID		.Value = marca.Dat.MarcaTipoID.Value;   //int Oblig.
							nuevaMarca.Dat.ClaseID			.Value = vRenMarca.Dat.ClaseID.AsInt;   //int Oblig.
							nuevaMarca.Dat.ClaseDescripEsp	.Value = vRenMarca.Dat.DesEspLim.AsString;   //nvarchar
							nuevaMarca.Dat.Limitada			.Value = vRenMarca.Dat.Limitada.AsBoolean;   //bit
							nuevaMarca.Dat.ClienteID		.Value = clienteID;   //int
							nuevaMarca.Dat.AgenteLocalID	.Value = marca.Dat.AgenteLocalID.Value;   //int
							nuevaMarca.Dat.Nuestra			.Value = true;   //bit Oblig.
							nuevaMarca.Dat.Vigilada			.Value = true;   //bit Oblig.
							nuevaMarca.Dat.Sustituida		.Value = false;   //bit Oblig.
							nuevaMarca.Dat.StandBy			.Value = false;   //bit Oblig.
							nuevaMarca.Dat.Vigente			.Value = true;   //bit
							nuevaMarca.Dat.LogotipoID		.Value = marca.Dat.LogotipoID.Value;   //int
							nuevaMarca.Dat.ExpedienteVigenteID.Value = expedienteID;   //int
							nuevaMarca.Dat.OtrosClientes	.Value = marca.Dat.OtrosClientes.Value;   //bit Oblig.
							nuevaMarca.Dat.MarcaRegRenID	.Value = marca.Dat.MarcaRegRenID.Value;   //int
							if (vRenMarca.Dat.LogotipoID.AsInt > 0) 
							{
								nuevaMarca.Dat.LogotipoID		.Value = vRenMarca.Dat.LogotipoID.AsInt;
							}

							if (ePod.EOF == false) 
							{
								//leer poder
								poder.Adapter.ReadByID(ePod.Dat.PoderID.AsInt);
								nuevaMarca.Dat.Propietario		.Value = poder.Dat.Denominacion.AsString;
								nuevaMarca.Dat.ProDir			.Value = poder.Dat.Domicilio.AsString;
								pais.Adapter.ReadByID(poder.Dat.PaisID.AsInt);
								nuevaMarca.Dat.ProPais			.Value = pais.Dat.paisalfa.AsString;
							} 
							else 
							{
								//leer propietario
								propietario.Adapter.ReadByID(ePro.Dat.PropietarioID.AsInt);
								nuevaMarca.Dat.Propietario		.Value = propietario.Dat.Nombre.AsString;
								nuevaMarca.Dat.ProDir			.Value = propietario.Dat.Direccion.AsString;
								pais.Adapter.ReadByID(propietario.Dat.PaisID.AsInt);
								nuevaMarca.Dat.ProPais			.Value = pais.Dat.paisalfa.AsString;
							}
							if (vRenMarca.Dat.MarcaID.AsInt < 0) 
							{
								nuevaMarca.PostNewRow();
								marcaID = nuevaMarca.Adapter.InsertRow();
							} 
							else 
							{
								nuevaMarca.PostEdit();
								marcaID = nuevaMarca.Dat.ID.AsInt;
								nuevaMarca.Adapter.UpdateRow();
							}

							//Actualizar Expediente
							expe.ClearFilter();
							expe.Adapter.ReadByID(expedienteID);
							expe.Edit();
							expe.Dat.MarcaID.Value = marcaID;
							expe.PostEdit();
							expe.Adapter.UpdateRow();
							#endregion Insertar Nueva Marca
						}
						marca.Adapter.UpdateRow();

						//[ggaleano 04/02/2012] Atencion o Bussiness Unit por marca, se graba el ID de la atencion en la marca
						#region Atencion x Marca
						if (ot.Dat.TipoAtencionxMarca.AsString != "")
						{
							marca.Adapter.ReadByID(marcaID);
							marca.Edit();
							marca.Dat.TipoAtencionxMarca.Value = ot.Dat.TipoAtencionxMarca.AsString;
							marca.Dat.IDTipoAtencionxMarca.Value = ot.Dat.IDTipoAtencionxMarca.AsString;
							marca.PostEdit();
							marca.Adapter.UpdateRow();
						}
						#endregion Atencion x Marca


						#endregion Marca

						#region Guardar propietario actual en Expediente Campo
						//Insertar propietario actual en expediente campo
						eCampo.NewRow();
						eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
						eCampo.Dat.Campo			.Value = GlobalConst.PROP_ACTUAL_NOMBRE;
						if (ePod.EOF == false) 
						{
							eCampo.Dat.Valor			.Value = poder.Dat.Denominacion.AsString;
						} 
						else 
						{
							eCampo.Dat.Valor			.Value = propietario.Dat.Nombre.AsString;
						}
						eCampo.PostNewRow();
						eCampo.Adapter.InsertRow();

						eCampo.NewRow();
						eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
						eCampo.Dat.Campo			.Value = GlobalConst.PROP_ACTUAL_DIR;
						if (ePod.EOF == false) 
						{
							eCampo.Dat.Valor			.Value = poder.Dat.Domicilio.AsString;
						} 
						else 
						{
							eCampo.Dat.Valor			.Value = propietario.Dat.Direccion.AsString;
						}
						eCampo.PostNewRow();
						eCampo.Adapter.InsertRow();

						eCampo.NewRow();
						eCampo.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
						eCampo.Dat.Campo			.Value = GlobalConst.PROP_ACTUAL_PAIS;
						eCampo.Dat.Valor			.Value = pais.Dat.paisalfa.AsString;
						eCampo.PostNewRow();
						eCampo.Adapter.InsertRow();
						#endregion Guardar propietario actual en Expediente Campo
					
						#region Cargar ExpedienteSituacion
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expeSit.NewRow();
							expeSit.Dat.ExpedienteID.Value = expedienteID;
							expeSit.Dat.TramiteSitID.Value = sitTramite;   //int Oblig.
							expeSit.Dat.AltaFecha		.Value = System.DateTime.Now.Date;   //datetime Oblig.
							expeSit.Dat.SituacionFecha	.Value = System.DateTime.Now.Date;   //datetime Oblig.
							Berke.DG.DBTab.Tramite_Sit ts = new Berke.DG.DBTab.Tramite_Sit( db );
							ts.Adapter.ReadByID(sitTramite);
							expeSit.Dat.VencimientoFecha.Value = Berke.Marcas.BizActions.Lib.FechaMasPlazo(System.DateTime.Now.Date,ts.Dat.Plazo.AsInt,ts.Dat.UnidadID.AsInt,db);
							expeSit.Dat.FuncionarioID	.Value = funcionarioID;   //int Oblig.
							expeSit.PostNewRow();
							expeSit.Adapter.InsertRow();
						}
						#endregion Cargar ExpedienteSituacion

						#region Asignar valores a Expediente_Instruccion
						ei.ClearFilter();
						ei.Dat.ExpedienteID.Filter = expedienteID;
						ei.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
						ei.Adapter.ReadAll();
						if (ei.RowCount > 0) 
						{
							ei.Delete();
							ei.Adapter.DeleteRow();
						}

						ei.ClearFilter();
						ei.Dat.ExpedienteID.Filter = expedienteID;
						ei.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
						ei.Adapter.ReadAll();
						if (ei.RowCount > 0) 
						{
							ei.Delete();
							ei.Adapter.DeleteRow();
						}

						for (expeInst.GoTop(); ! expeInst.EOF;	expeInst.Skip()) 
						{
							expeInst.Edit(); 
							expeInst.Dat.MarcaID		.Value = marcaID;   //int Oblig.
							expeInst.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							expeInst.Dat.Fecha			.Value = System.DateTime.Now.Date;   //smalldatetime
							expeInst.PostEdit(); 
							expeInst.Adapter.InsertRow();
						}
						#endregion Asignar valores a Expediente_Instruccion

						#region Asignar valores a ExpedienteXPropietario y PropietarioXMarca
						//Borrar los ExpedienteXPropietario y PropietarioXMarca actuales
						ep.ClearFilter();
						ep.Dat.ExpedienteID.Filter = expedienteID;
						ep.Adapter.ReadAll();
						for (ep.GoTop(); !ep.EOF; ep.Skip()) 
						{
							ep.Delete();
							ep.Adapter.DeleteRow();
						}
						PxM.ClearFilter();
						PxM.Dat.MarcaID.Filter = marcaID;
						PxM.Adapter.ReadAll();
						for (PxM.GoTop(); !PxM.EOF; PxM.Skip()) 
						{
							PxM.Delete();
							PxM.Adapter.DeleteRow();
						}

						//Insertar en ExpedienteXPropietario y PropietarioXMarca
						for (ePro.GoTop(); ! ePro.EOF;	ePro.Skip()) 
						{
							ePro.Edit(); 
							ePro.Dat.ExpedienteID			.Value = expedienteID;   //int Oblig.					
							ePro.PostEdit(); 
							ePro.Adapter.InsertRow();

							PxM.NewRow(); 
							PxM.Dat.PropietarioID	.Value = ePro.Dat.PropietarioID.Value;   //int
							PxM.Dat.MarcaID			.Value = marcaID;   //int
							PxM.PostNewRow();
							PxM.Adapter.InsertRow();
						}
						#endregion Asignar valores a ExpedienteXPropietario y PropietarioXMarca

						#region Asignar valores a ExpedienteXPoder
						epoder.ClearFilter();
						epoder.Dat.ExpedienteID.Filter = expedienteID;
						epoder.Adapter.ReadAll();
						for (epoder.GoTop(); !epoder.EOF; epoder.Skip()) 
						{
							epoder.Delete();
							epoder.Adapter.DeleteRow();
						}

						for (ePod.GoTop(); ! ePod.EOF; ePod.Skip()) 
						{
							ePod.Edit(); 
							ePod.Dat.ExpedienteID			.Value = expedienteID;   //int
							ePod.PostEdit();
							ePod.Adapter.InsertRow(); 
						}
						#endregion Asignar valores a ExpedienteXPoder
					}

					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
				
					throw new Exception(" Error en Renovacion Upsert: " + e.Message );
				}
				//Notificacion
				string noti;
				noti = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link("RenovacionDetalle.aspx", ot.Dat.Nro.AsString + "/" + ot.Dat.Anio.AsString, ordenTrabajoID.ToString(),"otID");
				db.IniciarTransaccion();
				Berke.Marcas.BizActions.Lib.Notificar( 9, noti , db );
				db.Commit();
			
				//Notificacion


				DataSet tmp_OutDS=new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = true;
				outTB.Dat.Entero.Value = ordenTrabajoID;
				outTB.PostNewRow();
				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );
			}
			finally { db.CerrarConexion();}

		}

	
	} // End RenovacionUpsert class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_Upsert" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.Upsert,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion RenovacionUpsert

#region Renovacion.	Read
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Read: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.RenovacionDG inDG	= new Berke.DG.RenovacionDG( cmd.Request.RawDataSet );

			Berke.DG.DBTab.OrdenTrabajo				inot			= inDG.OrdenTrabajo ;

					
			Berke.DG.RenovacionDG outDG	= new Berke.DG.RenovacionDG();

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida
			// OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo ot	= outDG.OrdenTrabajo ;
			ot.InitAdapter( db );
			ot.Adapter.ReadByID(inot.Dat.ID.AsInt);
			if (ot.RowCount!=0)
			{
		
				// Expediente
				Berke.DG.DBTab.Expediente expe	= outDG.Expediente ;
				expe.Dat.OrdenTrabajoID.Filter= ot.Dat.ID.AsInt;   //int
		
				expe.InitAdapter( db );
				expe.Adapter.ReadAll();

				// vRenovacionMarca
				Berke.DG.ViewTab.vRenovacionMarca vRenovacionMarca	= outDG.vRenovacionMarca ;
				// Marca
				Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca( db );
				// Clase
				Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase( db );
				// Expediente ANTERIOR
				Berke.DG.DBTab.Expediente eAnt = new Berke.DG.DBTab.Expediente( db );
				// MarcaRegRen
				Berke.DG.DBTab.MarcaRegRen marRR = new Berke.DG.DBTab.MarcaRegRen( db );

				#region Asignar valores a Marca
				Berke.DG.DBTab.Marca mar = outDG.Marca;
				mar.InitAdapter( db);
				mar.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
				#endregion Asignar valores a Marca
				

				#region Asignar valores a Expediente_Instruccion
				Berke.DG.DBTab.Expediente_Instruccion expeInst = outDG.Expediente_Instruccion;
				expeInst.InitAdapter( db ); 
				expeInst.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				expeInst.Adapter.ReadAll();
				#endregion Asignar valores a Expediente_Instruccion

	
				#region Documento

				Berke.DG.DBTab.Expediente_Documento eDoc = new Berke.DG.DBTab.Expediente_Documento ( db );
				eDoc.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				eDoc.Adapter.ReadAll();

				Berke.DG.DBTab.Documento doc	= outDG.Documento ;

				doc.InitAdapter( db );
				doc.Adapter.ReadByID(eDoc.Dat.DocumentoID.AsInt);
				#endregion Documento	

				for (expe.GoTop();!expe.EOF;expe.Skip())
				{
					#region vRenovacionMarca
					vRenovacionMarca.NewRow(); 

					vRenovacionMarca.Dat.MarcaID			.Value = expe.Dat.MarcaID.AsInt;   //Int32
					vRenovacionMarca.Dat.ExpedienteID		.Value = expe.Dat.ID.AsInt;   //Int32
					vRenovacionMarca.Dat.Referencia			.Value = expe.Dat.Label.AsString;


					#region Marca - Clase Actual
					marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);

					vRenovacionMarca.Dat.ClaseID			.Value = marca.Dat.ClaseID.AsInt;   //Int32
					vRenovacionMarca.Dat.Denominacion		.Value = marca.Dat.Denominacion.AsString;   //String
					vRenovacionMarca.Dat.DenominacionClave  .Value = marca.Dat.DenominacionClave.AsString;   //String

					vRenovacionMarca.Dat.Limitada			.Value = marca.Dat.Limitada.AsBoolean;   //Boolean
					vRenovacionMarca.Dat.DesEspLim			.Value = marca.Dat.ClaseDescripEsp.AsString;   //String
					vRenovacionMarca.Dat.LogotipoID			.Value = marca.Dat.LogotipoID.AsInt;

					Berke.DG.DBTab.MarcaTipo mt = new Berke.DG.DBTab.MarcaTipo( db );
					mt.Adapter.ReadByID(marca.Dat.MarcaTipoID.AsInt);
					vRenovacionMarca.Dat.DesEsp			.Value = mt.Dat.Abrev.AsString;

	
					clase.Adapter.ReadByID(marca.Dat.ClaseID.AsInt);
					vRenovacionMarca.Dat.ClaseDescrip		.Value = clase.Dat.DescripBreve.AsString;   //String

					#endregion Marca - Clase Actual

					#region Expediente - Marca - Clase Anterior
					eAnt.Adapter.ReadByID(expe.Dat.ExpedienteID.AsInt);

					vRenovacionMarca.Dat.MarcaAnteriorID	.Value = eAnt.Dat.MarcaID.AsInt;   //Int32
					vRenovacionMarca.Dat.ActaNro			.Value = eAnt.Dat.ActaNro.AsInt;   //Int32
					vRenovacionMarca.Dat.ActaAnio			.Value = eAnt.Dat.ActaAnio.AsInt;   //Int32

// Tomar de RegRen	vRenovacionMarca.Dat.Vencimiento		.Value = eAnt.Dat.VencimientoFecha.AsDateTime;   //DateTime

					marca.Adapter.ReadByID(eAnt.Dat.MarcaID.AsInt);
					vRenovacionMarca.Dat.ClaseAntID			.Value = marca.Dat.ClaseID.AsInt;   //Int32
					vRenovacionMarca.Dat.MarcaTipoID		.Value = marca.Dat.MarcaTipoID.AsInt;

					clase.Adapter.ReadByID(marca.Dat.ClaseID.AsInt);
					vRenovacionMarca.Dat.ClaseAntDescrip	.Value = clase.Dat.DescripBreve.AsString;   //String


					marRR.Adapter.ReadByID(eAnt.Dat.MarcaRegRenID.AsInt);
					vRenovacionMarca.Dat.RegistroNro		.Value = marRR.Dat.RegistroNro.AsInt;   //Int32
					vRenovacionMarca.Dat.ConcesionFecha.Value = marRR.Dat.ConcesionFecha.AsDateTime;   //Int32

					vRenovacionMarca.Dat.Vencimiento		.Value = marRR.Dat.VencimientoFecha.AsDateTime;

					#endregion Expediente - Marca - Clase Anterior

					vRenovacionMarca.PostNewRow(); 

					#endregion vRenovacionMarca


				}

				#endregion 
			}
			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End Read class


}// end namespace 

#endregion Read

#region Renovacion.	Fill
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Fill: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.ActaRegistroPoder inTB	= new Berke.DG.ViewTab.ActaRegistroPoder( cmd.Request.RawDataSet.Tables[0]);

			Berke.DG.RenovacionDG outDG	= new Berke.DG.RenovacionDG();
			Berke.DG.DBTab.Expediente expe	= outDG.Expediente ;
			Berke.DG.DBTab.MarcaRegRen marcaRegRen	= outDG.MarcaRegRen ;
			Berke.DG.DBTab.Marca marca	= outDG.Marca ;

			Berke.DG.DBTab.Clase clase= new Berke.DG.DBTab.Clase();

			#region Expediente - MarcaRegRen
			if (inTB.Dat.Registro.IsNull)
			{
				// Expediente
				expe.Dat.ActaNro.Filter = inTB.Dat.Acta.AsInt;
				expe.Dat.ActaAnio.Filter= inTB.Dat.Anio.AsInt;
				expe.InitAdapter( db );
				expe.Adapter.ReadAll();
				int actasNoREG_ADUANA = 0;
				int expeINDEX = -1;
				for (expe.GoTop(); !expe.EOF; expe.Skip())
				{
					if (expe.Dat.TramiteID.AsInt != Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REG_ADUANA))
					{
						actasNoREG_ADUANA++;
						expeINDEX = expe.RowIndex;
					}
				}
				//if (expe.RowCount > 1)
				if (actasNoREG_ADUANA > 1)
				{
					throw new Exception(" Expedientes con actas duplicadas");
				}
				expe.Go(expeINDEX);
				// MarcaRegRen
				marcaRegRen.InitAdapter( db );
				marcaRegRen.Adapter.ReadByID(expe.Dat.MarcaRegRenID.AsInt);
				
			}
			else
			{
				// MarcaRegRen
				marcaRegRen.Dat.RegistroNro.Filter= inTB.Dat.Registro.AsInt;
				marcaRegRen.InitAdapter( db );
				marcaRegRen.Adapter.ReadAll();
				if (marcaRegRen.RowCount > 1)
				{
					throw new Exception(" Marcas con regristros duplicados");
				}
				// Expediente
				expe.InitAdapter( db );
				expe.Adapter.ReadByID(marcaRegRen.Dat.ExpedienteID.AsInt);

			}
			#endregion Expediente - MarcaRegRen

			#region Marca - Clase
			// Marca
			marca.InitAdapter( db );
			marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);

			// Clase
			clase.InitAdapter( db );
			clase.Adapter.ReadByID(marca.Dat.ClaseID.AsInt);
			#endregion Marca - Clase
	        
			#region vRenovacionMarca

			// vRenovacionMarca
			Berke.DG.ViewTab.vRenovacionMarca vRenovacionMarca	= outDG.vRenovacionMarca ;

			vRenovacionMarca.NewRow(); 
			vRenovacionMarca.Dat.MarcaID			.Value = DBNull.Value;   //Int32
			vRenovacionMarca.Dat.ClaseID			.Value = DBNull.Value;   //Int32
			vRenovacionMarca.Dat.ClaseDescrip		.Value = DBNull.Value;   //String

			vRenovacionMarca.Dat.MarcaAnteriorID	.Value = marca.Dat.ID.AsInt;   //Int32
			vRenovacionMarca.Dat.ClaseAntID			.Value = marca.Dat.ClaseID.AsInt;   //Int32
			vRenovacionMarca.Dat.MarcaTipoID		.Value = marca.Dat.MarcaTipoID.AsInt;   //Int32
			vRenovacionMarca.Dat.ClaseAntDescrip	.Value = clase.Dat.DescripBreve.AsString;   //String


			vRenovacionMarca.Dat.DesEsp				.Value = clase.Dat.Descrip.AsString;   //String
			vRenovacionMarca.Dat.DesEspLim			.Value = marca.Dat.ClaseDescripEsp.AsString;   //String

			vRenovacionMarca.Dat.ClaseEdicionID		.Value = clase.Dat.NizaEdicionID.AsInt;   //Int32

			vRenovacionMarca.Dat.Denominacion		.Value = marca.Dat.Denominacion.AsString;   //String
            vRenovacionMarca.Dat.DenominacionClave  .Value = marca.Dat.DenominacionClave.AsString;   //String

			vRenovacionMarca.Dat.Limitada			.Value = marca.Dat.Limitada.AsBoolean;   //Boolean
			vRenovacionMarca.Dat.ExpedienteID		.Value = expe.Dat.ID.AsInt;   //Int32
			vRenovacionMarca.Dat.ActaNro			.Value = expe.Dat.ActaNro.AsInt;   //Int32
			vRenovacionMarca.Dat.ActaAnio			.Value = expe.Dat.ActaAnio.AsInt;   //Int32
			//vRenovacionMarca.Dat.Vencimiento		.Value = expe.Dat.VencimientoFecha.AsDateTime;   //DateTime
			if (!marcaRegRen.Dat.VencimientoFecha.IsNull) {
				vRenovacionMarca.Dat.Vencimiento		.Value = marcaRegRen.Dat.VencimientoFecha.AsDateTime;
			}
			vRenovacionMarca.Dat.Referencia			.Value = expe.Dat.Obs.AsString;
			if (!marcaRegRen.Dat.RegistroNro.IsNull) {
				vRenovacionMarca.Dat.RegistroNro		.Value = marcaRegRen.Dat.RegistroNro.AsInt;   //Int32
			}
			if (!marcaRegRen.Dat.ConcesionFecha.IsNull) {
				vRenovacionMarca.Dat.ConcesionFecha		.Value = marcaRegRen.Dat.ConcesionFecha.AsDateTime;   //DateTime
			}

			vRenovacionMarca.PostNewRow(); 
			#endregion vRenovacionMarca

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );
			db.CerrarConexion();
		}

	
	} // End Fill class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_Fill" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.Fill,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion Fill

#region Renovacion.	FillPoder
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class FillPoder: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.RenovacionDG outDG	= new Berke.DG.RenovacionDG();

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida

//			Berke.DG.DBTab.Poderdante poderdante = new Berke.DG.DBTab.Poderdante();
//
//			poderdante.Dat.PoderID.Filter = inTB.Dat.Entero.AsInt;
//			poderdante.InitAdapter( db );
//			poderdante.Adapter.ReadAll();
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillPoder class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_FillPoder" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.FillPoder,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion FillPoder

#region Renovacion.	FillPropietario
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class FillPropietario: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida


//            Berke.DG.DBTab.Poderdante pod = new Berke.DG.DBTab.Poderdante( db );
//			pod.Dat.PoderID.Filter = inTB.Dat.Entero.AsInt;
//			pod.Adapter.ReadAll();
//			System.Collections.ArrayList filtroPropietarios = new System.Collections.ArrayList();
//			
//			for (pod.GoTop();!pod.EOF;pod.Skip())
//			{
//				filtroPropietarios.Add(pod.Dat.PropietarioID.AsInt);
//			}
		
			// vPropietario

			
			Berke.DG.ViewTab.vPropietario outTB	=   new Berke.DG.ViewTab.vPropietario();
//
//			outTB.Dat.ID.Filter=new DSFilter( filtroPropietarios );
//			outTB.InitAdapter( db );
//			outTB.Adapter.ReadAll();

			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillPropietario class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_FillPropietario" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.FillPropietario,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion FillPropietario

#region Renovacion.	FillLimitaciones
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class FillLimitaciones: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion

			Berke.DG.DBTab.CIdioma idioma = new Berke.DG.DBTab.CIdioma();
			idioma.InitAdapter( db );
			idioma.Adapter.ReadAll();

			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase();
			clase.InitAdapter( db );
			clase.Adapter.ReadByID(inTB.Dat.Entero.AsInt);

			Berke.DG.ViewTab.vRenovacionLimitadas outTB	=   new Berke.DG.ViewTab.vRenovacionLimitadas();

			Berke.DG.DBTab.Clase_Idioma cidioma = new Berke.DG.DBTab.Clase_Idioma();
		//	int idiomaID;

//			for (idioma.GoTop(); ! idioma.EOF;	idioma.Skip())
//			{
//
//				 vRenovacionLimitadas
//
//				idiomaID=idioma.Dat.ididioma.AsInt;
//
//				outTB.NewRow(); 
//				outTB.Dat.Denominacion	.Value = DBNull.Value;   //String
//				outTB.Dat.Clase			.Value = DBNull.Value;   //String
//				outTB.Dat.ClaseID		.Value = clase.Dat.ID.AsInt;   //Int32
//				outTB.Dat.MarcaID		.Value = DBNull.Value;   //Int32
//				outTB.Dat.IdiomaID		.Value = idiomaID;   //Int32
//				outTB.Dat.Idioma		.Value = idioma.Dat.descrip.AsString;   //String
//			
//				if ( idiomaID == (int) Berke.Libs.Base.GlobalConst.Idioma.ESPANOL)
//				{
//					outTB.Dat.Descrip		.Value = clase.Dat.Descrip.AsString;   //String
//				}
//				else
//				{
//					cidioma.InitAdapter( db );
//					cidioma.Dat.IdiomaID.Filter = idiomaID;
//					cidioma.Dat.ClaseID.Filter = clase.Dat.ID.AsInt;
//					cidioma.Adapter.ReadAll();
//					outTB.Dat.Descrip		.Value =cidioma.Dat.Descrip.AsString;
//				}
//			outTB.PostNewRow(); 
//			}
			outTB.NewRow();
			outTB.Dat.Descrip		.Value = clase.Dat.Descrip.AsString;
			outTB.PostNewRow(); 
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillLimitaciones class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_FillLimitaciones" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.FillLimitaciones,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion FillLimitaciones

#region Renovacion.	ReadClase
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadClase: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida
		
			// Clase
			Berke.DG.DBTab.Clase outTB	=   new Berke.DG.DBTab.Clase();

			outTB.Dat.NizaEdicionID.Filter = inTB.Dat.Entero.AsInt;
		
			outTB.InitAdapter( db );
			outTB.Adapter.ReadAll();
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End ReadClase class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Renovacion_ReadClase" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Renovacion.ReadClase,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion ReadClase

#region Renovacion. RegAduanaUpsert
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

	public class RegAduanaUpsert: IAction
	{
		public void Execute( Command cmd )
		{
			int tipoTramite;
			//Obtengo el id del tramite REGISTRO EN ADUANAS
			tipoTramite=Convert.ToInt16(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REG_ADUANA);

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.RenovacionDG inDG					= new Berke.DG.RenovacionDG( cmd.Request.RawDataSet );
			Berke.DG.ViewTab.ParamTab outTB				=  new Berke.DG.ViewTab.ParamTab();
			Berke.DG.DBTab.CPais pais					= new Berke.DG.DBTab.CPais( db);
			Berke.DG.DBTab.Propietario propietario		= new Berke.DG.DBTab.Propietario( db );
			Berke.DG.DBTab.Poder poder					= new Berke.DG.DBTab.Poder( db );
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.ExpedienteXPropietario ep	= new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.ExpedienteXPoder epoder		= new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.Tramite_Sit	tramitesit		= new Berke.DG.DBTab.Tramite_Sit( db );
			Berke.DG.DBTab.Situacion sit				= new Berke.DG.DBTab.Situacion(db);
			Berke.DG.DBTab.Expediente_Instruccion ei	= new Berke.DG.DBTab.Expediente_Instruccion(db);
			Berke.DG.DBTab.Expediente_Distribuidor ed   = new Berke.DG.DBTab.Expediente_Distribuidor(db);
			//Berke.DG.DBTab.Marca marca2					= new Berke.DG.DBTab.Marca(db);
			Berke.DG.DBTab.ExpedienteCampo eCampo		= new Berke.DG.DBTab.ExpedienteCampo(db);
						
			#region Asignar alias a las tablas de inDG
			Berke.DG.DBTab.OrdenTrabajo				ot				= inDG.OrdenTrabajo ;
			Berke.DG.DBTab.Expediente				expe			= inDG.Expediente ;
			Berke.DG.ViewTab.vRenovacionMarca		vRenMarca		= inDG.vRenovacionMarca ;
			Berke.DG.DBTab.ExpedienteXPropietario   ePro			= inDG.ExpedienteXPropietario;
			Berke.DG.DBTab.ExpedienteXPoder			ePod			= inDG.ExpedienteXPoder;
			Berke.DG.DBTab.Atencion					at				= inDG.Atencion;
			Berke.DG.DBTab.Expediente_Distribuidor  eDistr		    = inDG.Expediente_Distribuidor;
			Berke.DG.DBTab.Expediente_Instruccion	expeInst		= inDG.Expediente_Instruccion;
			Berke.DG.DBTab.Marca marca					= inDG.Marca ;

			ot.InitAdapter( db );
			expe.InitAdapter( db );
			ePro.InitAdapter( db );
			ePod.InitAdapter( db );
			at.InitAdapter ( db );
			eDistr.InitAdapter( db );
			expeInst.InitAdapter( db );
			marca.InitAdapter( db );
			#endregion Asignar alias a las tablas de inDG

			#region Variables
			bool insert = false;
			int ordenTrabajoID = 0;
			int expedienteID = 0;
			int NizaAntId = 0;
			int RegRenID = 0;
			int clienteID = 0;
			int marcaID = 0;
			int funcionarioID = 0;
			int atencionID = 0;
			string str_propietario = "";
			int TipoTrabajoID = 0;
			#endregion Variables

			#region Obtener tramitesitID
			tramitesit.Dat.TramiteID.Filter = tipoTramite;
			tramitesit.Dat.SituacionID.Filter = Convert.ToInt16(GlobalConst.Situaciones.HOJA_INICIO);
			tramitesit.Adapter.ReadAll();
			int sitTramite = tramitesit.Dat.ID.AsInt;
			#endregion Obtener tramitesitID

			#region Obtener TipoTrabajoID
			Berke.DG.DBTab.Tramite tram = new Berke.DG.DBTab.Tramite(db);
			tram.Adapter.ReadByID(tipoTramite);
			TipoTrabajoID = tram.Dat.TrabajoTipoID.AsInt;
			#endregion Obtener TipoTrabajoID

			try 
			{
				try
				{
					db.IniciarTransaccion();			
					clienteID=ot.Dat.ClienteID.AsInt;
					funcionarioID = ot.Dat.FuncionarioID.AsInt;
					atencionID = ot.Dat.AtencionID.AsInt;

					#region Nueva Atención
					if (! at.EOF ) 
					{
						at.Edit();
						at.Dat.ClienteID.Value = clienteID;
						at.Dat.AreaID.Value = (int)GlobalConst.Area.LEGALES;
						at.PostEdit();
						atencionID = at.Adapter.InsertRow();
					}
					#endregion Nueva Atención

					#region OrdenTrabajo
					if (ot.Dat.ID.AsInt == -1) 
					{
						ot.Edit();
						ot.Dat.TrabajoTipoID			.Value =  TipoTrabajoID; //tipoTramite;   //2 es Renovacion
						ot.Dat.Nro			.Value = Berke.Libs.Base.Helpers.Calc.OrdenTrabajoNro(tipoTramite);   //numero magico
						ot.Dat.Anio			.Value = System.DateTime.Now.Year;   //int Oblig.
						ot.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
						if (atencionID != 0) 
						{
							ot.Dat.AtencionID	.Value = atencionID;
						}
						ot.PostEdit();
						ordenTrabajoID = ot.Adapter.InsertRow();
						insert = true;
					} 
					else 
					{
						ot.Edit();
						if (atencionID != 0) 
						{
							ot.Dat.AtencionID	.Value = atencionID;
						}
						ot.PostEdit();
						ordenTrabajoID = ot.Dat.ID.AsInt;
						ot.Adapter.UpdateRow(); 
						insert = false;
					}
					#endregion OrdenTrabajo

					#region Borrar marcas de la BD
					expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
					expe.Adapter.ReadAll();
					bool borrar_marca = true;
					RegAduanaBorrarExpe be = new RegAduanaBorrarExpe();

					for (expe.GoTop(); ! expe.EOF;	expe.Skip())
					{
						borrar_marca = true;
						for (vRenMarca.GoTop(); ! vRenMarca.EOF;	vRenMarca.Skip())
						{
							if (expe.Dat.MarcaID.AsInt == vRenMarca.Dat.MarcaID.AsInt) 
							{
								borrar_marca = false;
								break;
							}						
						}
						if (borrar_marca == true) 
						{
							be.Execute(db, ordenTrabajoID, expe.Dat.ID.AsInt);
						}
					}
					#endregion Borrar marcas de la BD

					for (vRenMarca.GoTop(); ! vRenMarca.EOF;	vRenMarca.Skip()) 
					{
						marca.Adapter.ReadByID(vRenMarca.Dat.MarcaAnteriorID.AsInt);
						marcaID = marca.Dat.ID.AsInt;

						#region expediente
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expe.NewRow();
							expe.Dat.OrdenTrabajoID		.Value = ordenTrabajoID;   //int
							expe.Dat.ExpedienteID		.Value = vRenMarca.Dat.ExpedienteID.AsInt;   //int
							expe.Dat.TramiteSitID		.Value = sitTramite;   //int Oblig.
							expe.Dat.AltaFecha			.Value = System.DateTime.Now;
						}						 
						else 
						{
							expe.ClearFilter();
							expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
							expe.Dat.MarcaID.Filter = vRenMarca.Dat.MarcaID.AsInt;
							expe.Adapter.ReadAll();

							#region Controlar situacion de HI
							tramitesit.ClearFilter();
							tramitesit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
							sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
							if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
							{
								throw new Exception("Solo se pueden modificar las HI que se encuentren en situación hoja de inicio");
							}
							#endregion Controlar situacion de HI


							expe.Edit();
							
						}
						expe.Dat.TramiteID			.Value = tipoTramite;   //int Oblig.
						expe.Dat.ClienteID			.Value = clienteID;   //int
						expe.Dat.Nuestra			.Value = true;   //bit Oblig.
						expe.Dat.Sustituida			.Value = false;   //bit Oblig.
						expe.Dat.StandBy			.Value = false;   //bit Oblig.
						expe.Dat.Vigilada			.Value = true;   //bit Oblig.
						expe.Dat.Concluido			.Value = false;   //bit Oblig.
						expe.Dat.VencimientoFecha	.Value = vRenMarca.Dat.Vencimiento.AsDateTime.AddYears(10);
						expe.Dat.Label				.Value = vRenMarca.Dat.Referencia.AsString;
						expe.Dat.MarcaID			.Value = vRenMarca.Dat.MarcaAnteriorID.AsInt;   //int
						expe.Dat.MarcaRegRenID		.Value = marca.Dat.MarcaRegRenID.AsInt;

						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expe.PostNewRow();
							expedienteID = expe.Adapter.InsertRow();

							#region Guardar "Vigilada anterior"
							if ((marca.Dat.Vigilada.AsBoolean == false) &
								(marca.Dat.Nuestra.AsBoolean == false ) ) 
							{
								eCampo.NewRow();
								eCampo.Dat.ExpedienteID.Value = expedienteID;
								eCampo.Dat.Campo.Value = GlobalConst.VIGILADA_ANTERIOR;
								eCampo.Dat.Valor.Value = marca.Dat.Vigilada.AsString;
								eCampo.PostNewRow();
								eCampo.Adapter.InsertRow();
								marca.Edit();
								marca.Dat.Vigilada.Value = true;
								marca.PostEdit();
								marca.Adapter.UpdateRow();
							}
							#endregion Guardar "Vigilada anterior"
						} 
						else 
						{
							expe.PostEdit();
							expedienteID = expe.Dat.ID.AsInt;
							expe.Adapter.UpdateRow();
						}
						#endregion expediente

						#region Cargar ExpedienteSituacion
						if (vRenMarca.Dat.MarcaID.AsInt < 0) 
						{
							expeSit.NewRow();
							expeSit.Dat.ExpedienteID.Value = expedienteID;
							expeSit.Dat.TramiteSitID.Value = sitTramite;   //int Oblig.
							expeSit.Dat.AltaFecha		.Value = System.DateTime.Now.Date;   //datetime Oblig.
							expeSit.Dat.SituacionFecha	.Value = System.DateTime.Now.Date;   //datetime Oblig.
							Berke.DG.DBTab.Tramite_Sit ts = new Berke.DG.DBTab.Tramite_Sit( db );
							ts.Adapter.ReadByID(sitTramite);
							expeSit.Dat.VencimientoFecha.Value = Berke.Marcas.BizActions.Lib.FechaMasPlazo(System.DateTime.Now.Date,ts.Dat.Plazo.AsInt,ts.Dat.UnidadID.AsInt,db);
							expeSit.Dat.FuncionarioID	.Value = funcionarioID;   //int Oblig.
							expeSit.PostNewRow();
							expeSit.Adapter.InsertRow();
						}
						#endregion Cargar ExpedienteSituacion

						#region Asignar valores a Expediente_Instruccion
						ei.ClearFilter();
						ei.Dat.ExpedienteID.Filter = expedienteID;
						ei.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
						ei.Adapter.ReadAll();
						if (ei.RowCount > 0) 
						{
							ei.Delete();
							ei.Adapter.DeleteRow();
						}

						ei.ClearFilter();
						ei.Dat.ExpedienteID.Filter = expedienteID;
						ei.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
						ei.Adapter.ReadAll();
						if (ei.RowCount > 0) 
						{
							ei.Delete();
							ei.Adapter.DeleteRow();
						}

						for (expeInst.GoTop(); ! expeInst.EOF;	expeInst.Skip()) 
						{
							expeInst.Edit(); 
							expeInst.Dat.MarcaID		.Value = marcaID;   //int Oblig.
							expeInst.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
							expeInst.Dat.Fecha			.Value = System.DateTime.Now.Date;   //smalldatetime
							expeInst.PostEdit(); 
							expeInst.Adapter.InsertRow();
						}
						#endregion Asignar valores a Expediente_Instruccion

						#region Asignar valores a ExpedienteXPropietario
						//Borrar los ExpedienteXPropietario
						ep.ClearFilter();
						ep.Dat.ExpedienteID.Filter = expedienteID;
						ep.Adapter.ReadAll();
						for (ep.GoTop(); !ep.EOF; ep.Skip()) 
						{
							ep.Delete();
							ep.Adapter.DeleteRow();
						}
						

						//Insertar en ExpedienteXPropietario
						for (ePro.GoTop(); ! ePro.EOF;	ePro.Skip()) 
						{
							ePro.Edit(); 
							ePro.Dat.ExpedienteID			.Value = expedienteID;   //int Oblig.					
							ePro.PostEdit(); 
							ePro.Adapter.InsertRow();
						}
						#endregion Asignar valores a ExpedienteXPropietario

						#region Asignar valores a ExpedienteXPoder
						epoder.ClearFilter();
						epoder.Dat.ExpedienteID.Filter = expedienteID;
						epoder.Adapter.ReadAll();
						for (epoder.GoTop(); !epoder.EOF; epoder.Skip()) 
						{
							epoder.Delete();
							epoder.Adapter.DeleteRow();
						}

						for (ePod.GoTop(); ! ePod.EOF; ePod.Skip()) 
						{
							ePod.Edit(); 
							ePod.Dat.ExpedienteID			.Value = expedienteID;   //int
							ePod.PostEdit();
							ePod.Adapter.InsertRow(); 
						}
						#endregion Asignar valores a ExpedienteXPoder

						#region Asignar valores a Expediente_Distribuidor
						ed.ClearFilter();
						ed.Dat.ExpedienteID.Filter = expedienteID;
						ed.Dat.MarcaID.Filter = marcaID;
						ed.Adapter.ReadAll();

						if (ed.RowCount > 0)
						{
							for (ed.GoTop(); !ed.EOF; ed.Skip())
							{
								ed.Delete();
								ed.Adapter.DeleteRow();
							}
						}

						for (eDistr.GoTop(); ! eDistr.EOF; eDistr.Skip())
						{
							if (marcaID == eDistr.Dat.MarcaID.AsInt)
							{
								eDistr.Edit();
								eDistr.Dat.ExpedienteID.Value = expedienteID;
								eDistr.PostEdit();
								eDistr.Adapter.InsertRow();
							}
						}

						#region Borrar Expediente para las marcas eliminadas en el formulario de modificación

						#endregion Borrar Expediente para las marcas eliminadas en el formulario de modificación

						#endregion Asignar valores a Expediente_Distribuidor

					}


					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
				
					throw new Exception(" Error en Renovacion Upsert: " + e.Message );
				}
				
				DataSet tmp_OutDS=new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = true;
				outTB.Dat.Entero.Value = ordenTrabajoID;
				outTB.PostNewRow();
				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );
			}
			finally { db.CerrarConexion();}
		}
	}

	
	
	
}
#endregion Renovacion. RegAduanaB

#region Renovacion. RegAduanaBorrarExpe
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

	
	public class RegAduanaBorrarExpe			
	{
		public void Execute (Berke.Libs.Base.Helpers.AccesoDB db, int ordenTrabajoID, int expeID)
		{
			#region Declaración de objetos locales
			Berke.DG.DBTab.OrdenTrabajo				ot				= new Berke.DG.DBTab.OrdenTrabajo( db );
			Berke.DG.DBTab.Expediente				expe			= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente_Instruccion	expeInst		= new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.ExpedienteXPropietario   ePro			= new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.ExpedienteXPoder			ePod			= new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.Expediente_Situacion		expeSit			= new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.Expediente_Distribuidor  eDistr			= new Berke.DG.DBTab.Expediente_Distribuidor( db );

			System.Collections.ArrayList filtroxExpediente = new System.Collections.ArrayList();
			System.Collections.ArrayList filtroxExpedientePadre = new System.Collections.ArrayList();
			System.Collections.ArrayList filtroxMarca = new System.Collections.ArrayList();

			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit(db);
			Berke.DG.DBTab.Situacion sit          = new Berke.DG.DBTab.Situacion(db);
			Berke.DG.DBTab.Expediente expe2		  = new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.Marca marca			  = new Berke.DG.DBTab.Marca(db);
			Berke.DG.DBTab.ExpedienteCampo eCampo = new Berke.DG.DBTab.ExpedienteCampo(db);

			int tipoTramite = Convert.ToInt16(GlobalConst.Marca_Tipo_Tramite.REG_ADUANA);
			#endregion Declaración de objetos locales

			#region Expediente
			expe.Dat.ID.Filter = expeID;
			expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
			expe.Adapter.ReadAll();
			int expedienteID = expe.Dat.ID.AsInt;
			#endregion

			#region Controlar situacion de HI
			tramitesit.ClearFilter();
			tramitesit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
			sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
			if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
			{
				throw new Exception("Solo se pueden eliminar o modificar las HI que se encuentren en situación hoja de inicio");
			}
			#endregion Controlar situacion de HI

			#region Verificar que no existan trámites posteriores
			expe2.ClearFilter();
			expe2.Dat.MarcaID.Filter = expe.Dat.MarcaID.AsInt;
			expe2.Dat.AltaFecha.Order = -1;
			expe2.Adapter.ReadAll();

			for (expe2.GoTop(); !expe2.EOF; expe2.Skip()) 
			{
				if ((expe2.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) &
					(expe2.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) )
				{

					if ( expe2.Dat.AltaFecha.AsDateTime > expe.Dat.AltaFecha.AsDateTime) 
								 
					{
						marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
						throw new Exception("La marca " + marca.Dat.Denominacion.AsString +
							" es referenciada en trámites posteriores y no podra ser eliminada");
					}
						// Solución parche para el caso en los trámites sean registrados el mismo día.
						// El control es debido a que en las HI solo se registra la fecha (no hora) 
						// de inserción :(
					else if ( (expe2.Dat.AltaFecha.AsDateTime == expe.Dat.AltaFecha.AsDateTime)&&
						(expe2.Dat.OrdenTrabajoID.AsInt != expe.Dat.OrdenTrabajoID.AsInt)&&
						expe2.Dat.ID.AsInt> expe.Dat.ID.AsInt)
					{
						marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
						throw new Exception("La marca " + marca.Dat.Denominacion.AsString +
							" es referenciada en trámites posteriores y no podra ser eliminada");
					}
				}
			}
			#endregion Verificar que no existan trámites posteriores


			#region Expediente_Situacion
			expeSit.Dat.ExpedienteID.Filter = expedienteID;
			expeSit.Adapter.ReadAll();
			for (expeSit.GoTop();!expeSit.EOF;expeSit.Skip()) 
			{
				expeSit.Adapter.DeleteRow();
			}
			#endregion Expediente_Situacion

			#region Expediente_Instruccion
			expeInst.Dat.ExpedienteID.Filter = expedienteID;
			expeInst.Adapter.ReadAll();
			for (expeInst.GoTop();!expeInst.EOF;expeInst.Skip()) 
			{
				expeInst.Adapter.DeleteRow();
			}
			#endregion Expediente_Instruccion

			#region ExpedienteXPoder
			ePod.Dat.ExpedienteID.Filter = expedienteID;
			ePod.Adapter.ReadAll();
			for (ePod.GoTop();!ePod.EOF;ePod.Skip()) 
			{
				ePod.Adapter.DeleteRow();
			}
			#endregion ExpedienteXPoder

			#region ExpedienteXPropietario
			ePro.Dat.ExpedienteID.Filter = expedienteID;
			ePro.Adapter.ReadAll();
			for (ePro.GoTop();!ePro.EOF;ePro.Skip()) 
			{
				ePro.Adapter.DeleteRow();
			}
			#endregion ExpedienteXPropietario

			#region Expediente_Distribuidor
			eDistr.Dat.ExpedienteID.Filter = expedienteID;
			eDistr.Adapter.ReadAll();
			for (eDistr.GoTop(); !eDistr.EOF; eDistr.Skip())
			{
				eDistr.Adapter.DeleteRow();
			}
			#endregion Expediente_Distribuidor

			#region Actualizar Vigilada de Marca
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Dat.Campo.Filter = GlobalConst.VIGILADA_ANTERIOR;
			eCampo.Adapter.ReadAll();
			if (eCampo.RowCount > 0) 
			{
				marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
				marca.Edit();
				marca.Dat.Vigilada.Value = eCampo.Dat.Valor.AsString;
				marca.PostEdit();
				marca.Adapter.UpdateRow();
			}
			#endregion Actualizar Vigilada de Marca

			#region ExpedienteCampo
			eCampo.ClearFilter();
			eCampo.Dat.ExpedienteID.Filter = expedienteID;
			eCampo.Adapter.ReadAll();
			for (eCampo.GoTop(); !eCampo.EOF; eCampo.Skip())
			{
				eCampo.Adapter.DeleteRow();
			}
			#endregion ExpedienteCampo

			#region Expediente
			expe.ClearFilter();
			expe.Adapter.ReadByID(expedienteID);		
			expe.Adapter.DeleteRow();
			#endregion Expediente
		}
	}
	
}
#endregion Renovacion. RegAduanaBorrarExpe

#region Renovacion.	RegAduanaDelete
namespace Berke.Marcas.BizActions.Renovacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

	public class RegAduanaDelete: IAction
		{	
		public void Execute( Command cmd ) 
		{
			#region Declaración de objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
				
			Berke.DG.RenovacionDG inDG	= new Berke.DG.RenovacionDG( cmd.Request.RawDataSet );
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo;
			ot.InitAdapter( db );
			Berke.DG.ViewTab.vRenovacionMarca vRM = inDG.vRenovacionMarca;
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab();
			Berke.DG.DBTab.Expediente				expe			= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente_Instruccion	expeInst		= new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.ExpedienteXPropietario   ePro			= new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.ExpedienteXPoder			ePod			= new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.Expediente_Situacion		expeSit			= new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.Expediente_Distribuidor  eDistr			= new Berke.DG.DBTab.Expediente_Distribuidor( db );
			Berke.DG.DBTab.Tramite_Sit				trmSit			= new Berke.DG.DBTab.Tramite_Sit(db);
			Berke.DG.DBTab.Expediente				expe2			= new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.Marca					marca			= new Berke.DG.DBTab.Marca(db);
			Berke.DG.DBTab.ExpedienteCampo			eCampo			= new Berke.DG.DBTab.ExpedienteCampo(db);

				System.Collections.ArrayList filtroxExpediente = new System.Collections.ArrayList();
				
				int ordenTrabajoID = ot.Dat.ID.AsInt;
				int tipoTramite = Convert.ToInt16(GlobalConst.Marca_Tipo_Tramite.REG_ADUANA);
				#endregion Declaración de objetos locales

				try 
				{
					try 
					{
						db.IniciarTransaccion();

						#region Actualizar expedientes
						for (vRM.GoTop(); !vRM.EOF; vRM.Skip()) 
						{
							expe.ClearFilter();
							expe.Adapter.ReadByID( vRM.Dat.ExpedienteID.AsInt );
							trmSit.ClearFilter();
							trmSit.Adapter.ReadByID(expe.Dat.TramiteSitID.AsInt);
							
							if (trmSit.Dat.SituacionID.AsInt != Convert.ToInt32(GlobalConst.Situaciones.HOJA_INICIO))
								throw new Exception("No se puede eliminar la HI de Registro en Aduanas. Al menos una marca ha cambiado de situación");

							#region Verificar que no existan trámites posteriores
							expe2.ClearFilter();
							expe2.Dat.MarcaID.Filter = expe.Dat.MarcaID.AsInt;
							//expe2.Dat.AltaFecha.Order = -1;
							expe2.Adapter.ReadAll();

							for (expe2.GoTop(); !expe2.EOF; expe2.Skip()) 
							{
								if ((expe2.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) &
									(expe2.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) )
								{

									if ( expe2.Dat.AltaFecha.AsDateTime > expe.Dat.AltaFecha.AsDateTime) 
								 
									{
										marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
										throw new Exception("La marca " + marca.Dat.Denominacion.AsString +
											" es referenciada en trámites posteriores y no podra ser eliminada");
									}
										// Solución parche para el caso en los trámites sean registrados el mismo día.
										// El control es debido a que en las HI solo se registra la fecha (no hora) 
										// de inserción :(
									else if ( (expe2.Dat.AltaFecha.AsDateTime == expe.Dat.AltaFecha.AsDateTime)&&
										(expe2.Dat.OrdenTrabajoID.AsInt != expe.Dat.OrdenTrabajoID.AsInt)&&
										expe2.Dat.ID.AsInt> expe.Dat.ID.AsInt)
									{
										marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
										throw new Exception("La marca " + marca.Dat.Denominacion.AsString +
											" es referenciada en trámites posteriores y no podra ser eliminada");
									}
								}
							}
							#endregion Verificar que no existan trámites posteriores
							


							filtroxExpediente.Add(expe.Dat.ID.AsInt);
							
							//Actualizar Expediente
							expe.Edit();
							expe.Dat.OrdenTrabajoID.Value = DBNull.Value;
							expe.PostEdit();
							expe.Adapter.UpdateRow();

							#region Actualizar Vigilada de Marca
							eCampo.ClearFilter();
							eCampo.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
							eCampo.Dat.Campo.Filter = GlobalConst.VIGILADA_ANTERIOR;
							eCampo.Adapter.ReadAll();
							if (eCampo.RowCount > 0) 
							{
								marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
								marca.Edit();
								marca.Dat.Vigilada.Value = eCampo.Dat.Valor.AsString;
								marca.PostEdit();
								marca.Adapter.UpdateRow();
							}
							#endregion Actualizar Vigilada de Marca
						}
						#endregion Actualizar expedientes

						#region Expediente_Situacion
						expeSit.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						expeSit.Adapter.ReadAll();
						for (expeSit.GoTop();!expeSit.EOF;expeSit.Skip()) 
						{
							expeSit.Adapter.DeleteRow();
						}
						#endregion Expediente_Situacion

						#region Expediente_Instruccion
						expeInst.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						expeInst.Adapter.ReadAll();
						for (expeInst.GoTop();!expeInst.EOF;expeInst.Skip()) 
						{
							expeInst.Adapter.DeleteRow();
						}
						#endregion Expediente_Instruccion

						#region ExpedienteXPoder
						ePod.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						ePod.Adapter.ReadAll();
						for (ePod.GoTop();!ePod.EOF;ePod.Skip()) 
						{
							ePod.Adapter.DeleteRow();
						}
						#endregion ExpedienteXPoder

						#region ExpedienteXPropietario
						ePro.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						ePro.Adapter.ReadAll();
						for (ePro.GoTop();!ePro.EOF;ePro.Skip()) 
						{
							ePro.Adapter.DeleteRow();
						}
						#endregion ExpedienteXPropietario

						#region Expediente_Distribuidor
						eDistr.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						eDistr.Adapter.ReadAll();
						for (eDistr.GoTop(); !eDistr.EOF; eDistr.Skip())
						{
							eDistr.Adapter.DeleteRow();
						}
						#endregion Expediente_Distribuidor

						#region ExpedienteCampo
						eCampo.ClearFilter();
						eCampo.Dat.ExpedienteID.Filter = new DSFilter(filtroxExpediente);
						eCampo.Adapter.ReadAll();
						for (eCampo.GoTop(); !eCampo.EOF; eCampo.Skip())
						{
							eCampo.Adapter.DeleteRow();
						}
						#endregion ExpedienteCampo

						#region Expediente
						expe.ClearFilter();
						expe.Dat.ID.Filter = new DSFilter(filtroxExpediente);
						expe.Adapter.ReadAll();
						//expe.Adapter.ReadByID(expedienteID);		
						expe.Adapter.DeleteRow();
						#endregion Expediente

						ot.Adapter.DeleteRow();
						db.Commit();
					} 
					catch ( Exception ex ) 
					{
						db.RollBack();
				
						throw new Exception(" Error en RegAduana Delete : " + ex.Message );
					}

					DataSet tmp_OutDS = new DataSet();
					outTB.NewRow();
					outTB.Dat.Logico.Value = true;
					outTB.Dat.Entero.Value = ordenTrabajoID;
					outTB.PostNewRow();
					tmp_OutDS.Tables.Add(outTB.Table);
					cmd.Response = new Response( tmp_OutDS );
				}
				finally 
				{
					db.CerrarConexion();
				}

			} 
		}
	#endregion Renovacion.	RegAduanaDelete
}
