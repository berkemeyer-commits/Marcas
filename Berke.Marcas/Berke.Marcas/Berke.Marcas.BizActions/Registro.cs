namespace Berke.Marcas.BizActions.Registro
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	using Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;

	#region BorrarMarca
	public class BorrarMarca			
	{
		public void Execute (Berke.Libs.Base.Helpers.AccesoDB db, int marcaID)
		{
			#region Declaracion objetos locales
			int tipoTramite=Convert.ToInt16(GlobalConst.Marca_Tipo_Tramite.REGISTRO);
			//Tablas locales
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.MarcaRegRen marcaregren = new Berke.DG.DBTab.MarcaRegRen();
			Berke.DG.DBTab.Expediente_Situacion expedientesit = new Berke.DG.DBTab.Expediente_Situacion();
			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit();
			Berke.DG.DBTab.ExpedienteXPoder expeXPoder = new Berke.DG.DBTab.ExpedienteXPoder();
			Berke.DG.DBTab.ExpedienteXPropietario expeXPropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
			Berke.DG.DBTab.PropietarioXPoder propietarioXPoder = new Berke.DG.DBTab.PropietarioXPoder();
			Berke.DG.DBTab.PropietarioXMarca propietarioXMarca = new Berke.DG.DBTab.PropietarioXMarca();
			Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = new Berke.DG.DBTab.Expediente_Instruccion();
			Berke.DG.DBTab.ExpedienteCampo expedientecampo = new Berke.DG.DBTab.ExpedienteCampo();
			Berke.DG.DBTab.Expediente_Documento expedientedoc = new Berke.DG.DBTab.Expediente_Documento();
			Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento();
			Berke.DG.DBTab.DocumentoCampo doccampo = new Berke.DG.DBTab.DocumentoCampo();
			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion();

			//Inicializar Adapters		
			marca.InitAdapter( db );
			expediente.InitAdapter( db );
			marcaregren.InitAdapter( db );
			expedientesit.InitAdapter( db );
			expeXPoder.InitAdapter( db );
			expeXPropietario.InitAdapter( db );
			propietarioXPoder.InitAdapter( db );
			propietarioXMarca.InitAdapter( db );
			tramitesit.InitAdapter( db );
			expedienteinstr.InitAdapter( db );
			expedientecampo.InitAdapter( db );
			expedientedoc.InitAdapter( db );
			doc.InitAdapter( db );
			doccampo.InitAdapter( db );
			sit.InitAdapter( db );
			#endregion Declaracion objetos locales

			#region Obtener tramitesitID
			tramitesit.Dat.TramiteID.Filter = Convert.ToInt16(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO);
			tramitesit.Dat.SituacionID.Filter = Convert.ToInt16(GlobalConst.Situaciones.HOJA_INICIO);
			tramitesit.Adapter.ReadAll();
			int tramitesitID = tramitesit.Dat.ID.AsInt;
			#endregion Obtener tramitesitID

			#region Expediente
			expediente.Dat.MarcaID.Filter = marcaID;
			expediente.Dat.TramiteID.Filter = tipoTramite;
			expediente.Adapter.ReadAll();
			int expedienteID = expediente.Dat.ID.AsInt;
			#endregion Expediente

			#region Controlar situacion de HI
			tramitesit.ClearFilter();
			tramitesit.Adapter.ReadByID( expediente.Dat.TramiteSitID.AsInt );
			sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
			if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) {
				throw new Exception("Solo se pueden eliminar las HI que se encuentren en situación hoja de inicio");
			}
			#endregion Controlar situacion de HI

			#region Expediente-MarcaRegRen
			expediente.Edit();
			expediente.Dat.MarcaRegRenID.SetNull();
			expediente.PostEdit();
			expediente.Adapter.UpdateRow();
			#endregion Expediente-MarcaRegRen

			#region Marca-Expediente
			marca.Adapter.ReadByID( marcaID );
			marca.Edit();
			marca.Dat.ExpedienteVigenteID.SetNull();
			marca.PostEdit();
			marca.Adapter.UpdateRow();
			#endregion Marca-Expediente

			#region expeXPropietario
			expeXPropietario.Dat.ExpedienteID.Filter = expedienteID;
			expeXPropietario.Adapter.ReadAll();
			for (expeXPropietario.GoTop(); !expeXPropietario.EOF; expeXPropietario.Skip()) {
				expeXPropietario.Delete();
				expeXPropietario.Adapter.DeleteRow();
			}
			#endregion expeXPropietario

			#region propietarioXMarca
			propietarioXMarca.Dat.MarcaID.Filter = marcaID;
			propietarioXMarca.Adapter.ReadAll();
			for (propietarioXMarca.GoTop(); !propietarioXMarca.EOF; propietarioXMarca.Skip()) {
				propietarioXMarca.Delete();
				propietarioXMarca.Adapter.DeleteRow();
			}
			#endregion propietarioXMarca

			#region MarcaRegRen
			marcaregren.Dat.ExpedienteID.Filter = expedienteID;
			marcaregren.Adapter.ReadAll();
			marcaregren.Delete();
			marcaregren.Adapter.DeleteRow();
			#endregion MarcaRegRen

			#region ExpedienteXPoder
			expeXPoder.Dat.ExpedienteID.Filter = expedienteID;
			expeXPoder.Adapter.ReadAll();
			if (expeXPoder.RowCount > 0) {
				expeXPoder.Delete();
				expeXPoder.Adapter.DeleteRow();
			}
			#endregion ExpedienteXPoder

			#region Expediente_Situacion
			expedientesit.ClearFilter();
			expedientesit.Dat.ExpedienteID.Filter = expedienteID;
			expedientesit.Dat.TramiteSitID.Filter = tramitesitID;
			expedientesit.Adapter.ReadAll();
			expedientesit.Delete();
			expedientesit.Adapter.DeleteRow();
			#endregion Expediente_Situacion

			#region ExpedienteInstruccion
			expedienteinstr.Dat.ExpedienteID.Filter = expedienteID;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
			expedienteinstr.Adapter.ReadAll();
			if (expedienteinstr.RowCount > 0) {
				expedienteinstr.Delete();
				expedienteinstr.Adapter.DeleteRow();
			}

			expedienteinstr.Dat.ExpedienteID.Filter = expedienteID;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
			expedienteinstr.Adapter.ReadAll();
			if (expedienteinstr.RowCount > 0) {
				expedienteinstr.Delete();
				expedienteinstr.Adapter.DeleteRow();
			}
			#endregion ExpedienteInstruccion

			#region ExpedienteCampo
			//Borrar expedientecampo actuales de propietarios
			expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
			expedientecampo.Adapter.ReadAll();
			for (expedientecampo.GoTop(); !expedientecampo.EOF; expedientecampo.Skip()) {
					expedientecampo.Delete();
					expedientecampo.Adapter.DeleteRow();
			}
			#endregion ExpedienteCampo

			#region ExpedienteDocumento
			expedientedoc.Dat.ExpedienteID.Filter = expedienteID;
			expedientedoc.Adapter.ReadAll();
			for (expedientedoc.GoTop(); ! expedientedoc.EOF; expedientedoc.Skip()) {
				doc.Adapter.ReadByID( expedientedoc.Dat.DocumentoID.AsInt );
				doccampo.Dat.DocumentoID.Filter = expedientedoc.Dat.DocumentoID.AsInt;
				doccampo.Adapter.ReadAll();
				if (doc.Dat.DocumentoTipoID.AsInt == (int) GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD) {
					for (doccampo.GoTop(); !doccampo.EOF; doccampo.Skip()) {
						doccampo.Delete();
						doccampo.Adapter.DeleteRow();
					}
					expedientedoc.Delete();
					expedientedoc.Adapter.DeleteRow();
					doc.Delete();
					doc.Adapter.DeleteRow();
				}
			}
			#endregion ExpedienteDocumento

			#region Expediente
			expediente.Dat.MarcaID.Filter = marcaID;
			expediente.Adapter.ReadAll();
			expediente.Delete();
			expediente.Adapter.DeleteRow();
			#endregion Expediente

			#region Marca
			marca.Adapter.ReadByID( marcaID );
			marca.Delete();
			marca.Adapter.DeleteRow();
			#endregion Marca		
		}
	}
    #endregion BorrarMarca

	#region BorrarMarcasMasiva
	public class BorrarMarcasMasiva
	{
		public void Execute (Berke.Libs.Base.Helpers.AccesoDB db, string str_marca)
		{
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.Expediente_Documento expedientedoc = new Berke.DG.DBTab.Expediente_Documento(db);
			Berke.DG.DBTab.DocumentoCampo doccampo = new Berke.DG.DBTab.DocumentoCampo(db);
			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit(db);
			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion(db);

			#region Expediente
			expediente.Dat.MarcaID.Filter = ObjConvert.GetFilter(str_marca);
			expediente.Dat.TramiteID.Filter = (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO;
			expediente.Adapter.ReadAll();
			string str_expediente = "";
			for (expediente.GoTop(); !expediente.EOF; expediente.Skip()) {
				if (str_expediente == "") {
					str_expediente = expediente.Dat.ID.AsString;
				} else {
					str_expediente = str_expediente + "," + expediente.Dat.ID.AsString;
				}
			}			
			#endregion Expediente

			#region Controlar situacion de HI
			expediente.GoTop();
			tramitesit.ClearFilter();			
			tramitesit.Adapter.ReadByID( expediente.Dat.TramiteSitID.AsInt );
			sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				
			if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) {
				throw new Exception("Solo se pueden eliminar las HI que se encuentren en situación hoja de inicio");
			}
			#endregion Controlar situacion de HI

			#region Expediente-MarcaRegRen
			db.Sql = "UPDATE Expediente SET MarcaRegRenID = NULL WHERE ID in ( " + str_expediente + " )";
			db.EjecutarDML();
			#endregion Expediente-MarcaRegRen

			#region Marca-Expediente
			db.ClearParams();
			db.Sql = "UPDATE Marca SET ExpedienteVigenteID = NULL WHERE ID in ( " + str_marca + " )";
			db.EjecutarDML();
			#endregion Marca-Expediente

			#region expeXPropietario
			db.ClearParams();
			db.Sql = "DELETE FROM ExpedienteXPropietario WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion expeXPropietario

			#region propietarioXMarca
			db.ClearParams();
			db.Sql = "DELETE FROM PropietarioXMarca WHERE MarcaID in ( " + str_marca + " )";
			db.EjecutarDML();
			#endregion propietarioXMarca

			#region MarcaRegRen
			db.ClearParams();
			db.Sql = "DELETE FROM MarcaRegRen WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion MarcaRegRen

			#region ExpedienteXPoder
			db.ClearParams();
			db.Sql = "DELETE FROM ExpedienteXPoder WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion ExpedienteXPoder

			#region Expediente_Situacion
			db.ClearParams();
			db.Sql = "DELETE FROM Expediente_Situacion WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion Expediente_Situacion

			#region ExpedienteInstruccion
			db.ClearParams();
			db.Sql = "DELETE FROM Expediente_Instruccion WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion ExpedienteInstruccion

			#region ExpedienteCampo
			//Borrar expedientecampo actuales de propietarios
			db.ClearParams();
			db.Sql = "DELETE FROM ExpedienteCampo WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion ExpedienteCampo

			#region ExpedienteDocumento
			expedientedoc.ClearFilter();
			expedientedoc.Dat.ExpedienteID.Filter = ObjConvert.GetFilter(str_expediente);
			expedientedoc.Adapter.ReadAll();
			string str_doc = "";
			for (expedientedoc.GoTop(); ! expedientedoc.EOF; expedientedoc.Skip()) {
				#region lista str_doc
				if (str_doc == "") 
				{
					str_doc = expedientedoc.Dat.DocumentoID.AsString;
				} else {
					str_doc = str_doc + "," + expedientedoc.Dat.DocumentoID.AsString;
				}
				#endregion lista str_doc

				#region borrar expediente_documento
				/*
				db.ClearParams();
				db.Sql = "DELETE FROM Expediente_Documento WHERE ExpedienteID in (  " + str_expediente + "  )";
				db.EjecutarDML();
				db.ClearParams();
				*/
				#endregion borrar expediente_documento

				expedientedoc.Delete();
				expedientedoc.Adapter.DeleteRow();

			}
			#endregion ExpedienteDocumento

			#region borrar documento
			//Control agregado por mbaez. 14/05/2007
			//Verificar si existen otros expedientes apuntando al
			//mismo documento
			if (str_doc != "") 
			{
				expedientedoc.ClearFilter();
				expedientedoc.Dat.DocumentoID.Filter = ObjConvert.GetFilter(str_doc);
				expedientedoc.Adapter.ReadAll();
				if (expedientedoc.RowCount==0)
				{
					if (str_doc != "") 
					{
						db.Sql = "DELETE FROM DocumentoCampo WHERE DocumentoID in ( " + str_doc + " )";
						db.EjecutarDML();
					}

					if (str_doc != "") 
					{
						db.ClearParams();
						db.Sql = "DELETE FROM Documento WHERE ID in ( " + str_doc + " )";
						db.EjecutarDML();
					}
				}
			 }

			#endregion borrar documento

			#region borrar expediente cliente cambio
			/*BUG#254*/
			db.ClearParams();
			db.Sql = "DELETE FROM expediente_clientecambio WHERE ExpedienteID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion

			#region Expediente
			db.ClearParams();
			db.Sql = "DELETE FROM Expediente WHERE ID in (  " + str_expediente + "  )";
			db.EjecutarDML();
			#endregion Expediente

			#region Marca
			db.ClearParams();
			db.Sql = "DELETE FROM Marca_ClaseIdioma WHERE MarcaID in ( " + str_marca + " )";
			db.EjecutarDML();

			db.ClearParams();
			db.Sql = "DELETE FROM Marca WHERE ID in ( " + str_marca + " )";
			db.EjecutarDML();
			#endregion Marca		
		}
	}
	#endregion BorrarMarcasMasiva

	#region Registro_Upsert
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{
			int tipoTramite, docPrioridad;
			//Obtener el id del tramite REGISTRO y el id del documento PRIORIDAD Y CORRESPONDENCIA
			tipoTramite=Convert.ToInt16(GlobalConst.Marca_Tipo_Tramite.REGISTRO);
			docPrioridad=Convert.ToInt16(GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD);

			#region Delaracion de alias de DG y objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.RegistroDG inDG	= new Berke.DG.RegistroDG( cmd.Request.RawDataSet );
			Berke.DG.ExpedienteDG outDG	= new Berke.DG.ExpedienteDG();
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab();

			//Tablas recibidas como inDG
			Berke.DG.DBTab.OrdenTrabajo				ot				= inDG.OrdenTrabajo;
			Berke.DG.DBTab.Poder poder				= inDG.Poder;
			Berke.DG.DBTab.Propietario propietario	= inDG.Propietario;
			Berke.DG.ViewTab.vExpeMarca				vExpeMarca		= inDG.vExpeMarca;
			Berke.DG.DBTab.Atencion at = inDG.Atencion;
			
			//Tablas locales
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase();
			Berke.DG.DBTab.MarcaTipo marcatipo = new Berke.DG.DBTab.MarcaTipo();
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.MarcaRegRen marcaregren = new Berke.DG.DBTab.MarcaRegRen();
			Berke.DG.DBTab.Expediente_Situacion expedientesit = new Berke.DG.DBTab.Expediente_Situacion();
			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit();
			Berke.DG.DBTab.ExpedienteXPoder expeXPoder = new Berke.DG.DBTab.ExpedienteXPoder();
			Berke.DG.DBTab.ExpedienteXPropietario expeXPropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
			Berke.DG.DBTab.PropietarioXPoder propietarioXPoder = new Berke.DG.DBTab.PropietarioXPoder();
			Berke.DG.DBTab.PropietarioXMarca propietarioXMarca = new Berke.DG.DBTab.PropietarioXMarca();			
			Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento();
			Berke.DG.DBTab.DocumentoCampo doccampo = new Berke.DG.DBTab.DocumentoCampo();
			Berke.DG.DBTab.Expediente_Documento expedientedoc = new Berke.DG.DBTab.Expediente_Documento();
			Berke.DG.DBTab.Correspondencia corr = new Berke.DG.DBTab.Correspondencia();
			Berke.DG.DBTab.ExpedienteCampo expedientecampo = new Berke.DG.DBTab.ExpedienteCampo();
			Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = new Berke.DG.DBTab.Expediente_Instruccion();
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			Berke.DG.DBTab.Propietario propietario2 = new Berke.DG.DBTab.Propietario();
			//Berke.DG.DBTab.BussinessUnitxMarca bUxMar = new Berke.DG.DBTab.BussinessUnitxMarca();

			//Inicializar Adapters		
			ot.InitAdapter( db );
			at.InitAdapter( db );
			poder.InitAdapter( db );
			propietario.InitAdapter( db );
			vExpeMarca.InitAdapter( db );
			marca.InitAdapter( db );
			clase.InitAdapter( db );
			marcatipo.InitAdapter( db );
			expediente.InitAdapter( db );
			marcaregren.InitAdapter( db );
			expedientesit.InitAdapter( db );
			tramitesit.InitAdapter( db );
			expeXPoder.InitAdapter( db );
			expeXPropietario.InitAdapter( db );
			propietarioXPoder.InitAdapter( db );
			propietarioXMarca.InitAdapter( db );
			doc.InitAdapter( db );
			doccampo.InitAdapter( db );
			expedientedoc.InitAdapter( db );
			corr.InitAdapter( db );
			expedientecampo.InitAdapter( db );
			expedienteinstr.InitAdapter( db );
			pais.InitAdapter( db );
			propietario2.InitAdapter( db );
			//bUxMar.InitAdapter( db );

			int clienteID = 0;
			int funcionarioID = 0;
			int atencionID = 0;
			int claseID = 0;
			int marcatipoID = 0;
			int ordenTrabajoID = 0;
			int tramitesitID = 0;
			int marcaID = 0;
			int expedienteID = 0;
			int marcaregrenID = 0;
			int prioridadID = 0;
			string nro_prioridad = "";
			string fecha_prioridad = "";
			string pais_prioridad = "";
			string [] aPrioridad;
			#endregion Delaracion de alias de DG y objetos locales			

			#region Obtener tramitesitID
			tramitesit.Dat.TramiteID.Filter = tipoTramite;
			tramitesit.Dat.SituacionID.Filter = Convert.ToInt16(GlobalConst.Situaciones.HOJA_INICIO);
			tramitesit.Adapter.ReadAll();
			tramitesitID = tramitesit.Dat.ID.AsInt;
			#endregion Obtener tramitesitID
			
			bool insert = false;
			try 
			{
				try
				{
					db.IniciarTransaccion();
					clienteID=ot.Dat.ClienteID.AsInt;
					funcionarioID = ot.Dat.FuncionarioID.AsInt;
					atencionID = ot.Dat.AtencionID.AsInt;

					#region Enlazar correspondencia
					corr.Dat.Nro.Filter = ot.Dat.CorrNro.AsInt;
					corr.Dat.Anio.Filter = ot.Dat.CorrAnio.AsInt;
					corr.Adapter.ReadAll();
					ot.Edit();
					if (corr.RowCount > 0) 
					{
						//Si existe la correspondencia, se guarda una referencia en la OT
						ot.Dat.CorrespondenciaID.Value = corr.Dat.ID.AsInt;
					} 
					else 
					{
						ot.Dat.CorrespondenciaID.SetNull();
					}
					ot.PostEdit();
					#endregion Enlazar correspondencia

					#region Nueva Atención
					if (! at.EOF ) 
					{
						at.Edit();
						at.Dat.ClienteID.Value = clienteID;
						at.Dat.AreaID.Value = (int)GlobalConst.Area.REGISTRO;
						at.PostEdit();
						atencionID = at.Adapter.InsertRow();
					}
					#endregion Nueva Atención

					#region OrdenTrabajo
					if (ot.Dat.ID.AsInt == -1) 
					{
						ot.Edit();
						ot.Dat.TrabajoTipoID			.Value = tipoTramite;  //tipotramite = ordentrabajotipo (1 es Registro)
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
					expediente.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
					expediente.Adapter.ReadAll();
					bool borrar_marca = true;
					BorrarMarca bm = new BorrarMarca();
					for (expediente.GoTop(); ! expediente.EOF;	expediente.Skip())
					{
						borrar_marca = true;
						for (vExpeMarca.GoTop(); ! vExpeMarca.EOF;	vExpeMarca.Skip()) 
						{
							if (expediente.Dat.MarcaID.AsInt == vExpeMarca.Dat.MarcaID.AsInt) 
							{
								borrar_marca = false;
								break;
							}
						}
						if (borrar_marca == true) 
						{
							bm.Execute(db, expediente.Dat.MarcaID.AsInt);
						}
					}
					#endregion Borrar marcas de la BD



					for (vExpeMarca.GoTop(); ! vExpeMarca.EOF;	vExpeMarca.Skip())
					{
						#region Marca
						//Obtener claseID
						clase.Dat.Nro.Filter = vExpeMarca.Dat.ClaseNro.AsInt;
						clase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
						clase.Adapter.ReadAll();
						claseID = clase.Dat.ID.AsInt;

						//Obtener marcatipoID
						marcatipo.Dat.Abrev.Filter = vExpeMarca.Dat.MarcaTipo.AsString;
						marcatipo.Adapter.ReadAll();
						marcatipoID = marcatipo.Dat.ID.AsInt;

						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							marca.NewRow();
						} 
						else 
						{
							marca.Adapter.ReadByID( vExpeMarca.Dat.MarcaID.AsInt );
							marcaID = vExpeMarca.Dat.MarcaID.AsInt;
							marca.Edit();
						}
						marca.Dat.Denominacion.Value = vExpeMarca.Dat.Denominacion.AsString;
						marca.Dat.DenominacionClave.Value = vExpeMarca.Dat.DenominacionClave.AsString;
						marca.Dat.ClaseID.Value = claseID;
						marca.Dat.MarcaTipoID.Value = marcatipoID;
						marca.Dat.Limitada.Value = vExpeMarca.Dat.Limitada.AsBoolean;
						marca.Dat.Sustituida.Value = vExpeMarca.Dat.Sustituida.AsBoolean;
						marca.Dat.ClaseDescripEsp.Value = vExpeMarca.Dat.ClaseDescripEsp.AsString;
						marca.Dat.ClienteID.Value = clienteID;					
						marca.Dat.OtrosClientes		.Value = false;
						marca.Dat.Nuestra			.Value = true;
						marca.Dat.Vigilada			.Value = true;
						//marca.Dat.Sustituida		.Value = false;
						marca.Dat.StandBy			.Value = false;
						marca.Dat.Vigente			.Value = true;
						//[mbaez
						if (vExpeMarca.Dat.Sustituida.AsBoolean)
						{
							marca.Dat.AgenteLocalID.Value = vExpeMarca.Dat.AgenteLocalID.AsInt;
						}
						//]

						//[ggaleano 04/02/2012] Atencion o Bussiness Unit por marca, se graba el ID de la atencion en la marca
						#region Atencion x Marca
						if (vExpeMarca.Dat.TipoAtencionxMarca.AsString != "")
						{
							marca.Dat.TipoAtencionxMarca.Value = vExpeMarca.Dat.TipoAtencionxMarca.AsString;
							marca.Dat.IDTipoAtencionxMarca.Value = vExpeMarca.Dat.IDTipoAtencionxMarca.AsString;
						}
						#endregion Atencion x Marca
						

						if (vExpeMarca.Dat.LogotipoID.AsInt > 0) 
						{
							marca.Dat.LogotipoID		.Value = vExpeMarca.Dat.LogotipoID.AsInt;
						}
						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							marca.PostNewRow();
							marcaID = marca.Adapter.InsertRow();
						} 
						else 
						{
							marca.PostEdit();
							marca.Adapter.UpdateRow();
						}

						
						#endregion Marca

						#region Expediente
						
						int trSitID = tramitesitID;

						if (vExpeMarca.Dat.ExpedienteID.AsString == "") 
						{
							expediente.NewRow();
							expediente.Dat.AltaFecha.Value = System.DateTime.Now.Date;
							
						} 
						else 
						{
							expediente.Adapter.ReadByID( vExpeMarca.Dat.ExpedienteID.AsInt );
							expedienteID = vExpeMarca.Dat.ExpedienteID.AsInt;
							trSitID= vExpeMarca.Dat.TramiteSitID.AsInt;
							expediente.Edit();
						}
						expediente.Dat.OrdenTrabajoID.Value = ordenTrabajoID;
						expediente.Dat.MarcaID.Value = marcaID;
						expediente.Dat.ClienteID.Value = clienteID;
						expediente.Dat.TramiteID.Value = tipoTramite;
						expediente.Dat.TramiteSitID.Value = trSitID;

						//expediente.Dat.TramiteSitID.Value = tramitesitID;
						if (vExpeMarca.Dat.Sustituida.AsBoolean)
						{
							expediente.Dat.AgenteLocalID.Value = vExpeMarca.Dat.AgenteLocalID.AsInt;
						}

						/*
						if (insert) 
						{
							expediente.Dat.TramiteSitID.Value = tramitesitID;
						}
						*/

						expediente.Dat.Nuestra.Value = true;
						expediente.Dat.StandBy.Value = false;
						expediente.Dat.Concluido.Value = false;
						expediente.Dat.Sustituida.Value = vExpeMarca.Dat.Sustituida.AsBoolean;
						//expediente.Dat.Sustituida.Value = false;
						expediente.Dat.Vigilada.Value = true;
						expediente.Dat.Label.Value = vExpeMarca.Dat.Label.AsString;
						if (vExpeMarca.Dat.ExpedienteID.AsString == "") 
						{
							expediente.PostNewRow();
							expedienteID = expediente.Adapter.InsertRow();
						} 
						else 
						{
							expediente.PostEdit();
							expediente.Adapter.UpdateRow();
						}
						#endregion Expediente

						#region Borrar expediente_documento
						//---Borrar todos los expediente documento de la BD para volver a insertarlos----
						expedientedoc.Dat.ExpedienteID.Filter = expedienteID;
						expedientedoc.Adapter.ReadAll();
						for (expedientedoc.GoTop(); ! expedientedoc.EOF; expedientedoc.Skip()) 
						{
							doc.Adapter.ReadByID( expedientedoc.Dat.DocumentoID.AsInt );
							doccampo.Dat.DocumentoID.Filter = expedientedoc.Dat.DocumentoID.AsInt;
							doccampo.Adapter.ReadAll();
							if (doc.Dat.DocumentoTipoID.AsInt == (int) GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD) 
							{
								for (doccampo.GoTop(); !doccampo.EOF; doccampo.Skip()) 
								{
									doccampo.Delete();
									doccampo.Adapter.DeleteRow();
								}
								expedientedoc.Delete();
								expedientedoc.Adapter.DeleteRow();
								doc.Delete();
								doc.Adapter.DeleteRow();
							}
						}
						#endregion Borrar expediente_documento

						#region Prioridad
						//Utilizamos el campo situaciondescrip para guardar los datos de la prioridad
						aPrioridad = vExpeMarca.Dat.SituacionDecrip.AsString.Split( ((String)"~").ToCharArray() );
						if ( (aPrioridad.Length > 0) & (vExpeMarca.Dat.SituacionDecrip.AsString != "") ) 
						{
							nro_prioridad = aPrioridad[0];
							fecha_prioridad = aPrioridad[1];
							pais_prioridad = aPrioridad[2];

							//----Insertar el nuevo expediente documento----
							doc.NewRow();
							doc.Dat.DocumentoTipoID.Value = docPrioridad;
							doc.PostNewRow();
							prioridadID = doc.Adapter.InsertRow();

							//Nro de prioridad
							doccampo.NewRow();
							doccampo.Dat.DocumentoID.Value = prioridadID;
							doccampo.Dat.Campo.Value = "Nro";
							doccampo.Dat.Valor.Value = nro_prioridad;
							doccampo.PostNewRow();
							doccampo.Adapter.InsertRow();

							//Fecha de prioridad
							doccampo.NewRow();
							doccampo.Dat.DocumentoID.Value = prioridadID;
							doccampo.Dat.Campo.Value = "Fecha";
							doccampo.Dat.Valor.Value = fecha_prioridad;
							doccampo.PostNewRow();
							doccampo.Adapter.InsertRow();

							//Pais de prioridad
							doccampo.NewRow();
							doccampo.Dat.DocumentoID.Value = prioridadID;
							doccampo.Dat.Campo.Value = "Pais";
							doccampo.Dat.Valor.Value = pais_prioridad;
							doccampo.PostNewRow();
							doccampo.Adapter.InsertRow();

							//Expediente documento
							expedientedoc.NewRow();
							expedientedoc.Dat.DocumentoID.Value = prioridadID;
							expedientedoc.Dat.ExpedienteID.Value = expedienteID;
							expedientedoc.PostNewRow();
							expedientedoc.Adapter.InsertRow();
						}
						#endregion Prioridad

						#region MarcaRegRen
						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							marcaregren.NewRow();
						} 
						else 
						{
							marcaregren.Dat.ExpedienteID.Filter = vExpeMarca.Dat.ExpedienteID.AsInt;						
							marcaregren.Adapter.ReadAll();
							marcaregrenID = marcaregren.Dat.ID.AsInt;
							marcaregren.Edit();
						}
						marcaregren.Dat.ExpedienteID.Value = expedienteID;
						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							marcaregren.PostNewRow();
							marcaregrenID = marcaregren.Adapter.InsertRow();
						} 
						else 
						{
							marcaregren.PostEdit();
							marcaregren.Adapter.UpdateRow();
						}
						#endregion MarcaRegRen

						#region Expediente-MarcaRegRen
						expediente.Adapter.ReadByID( expedienteID );
						expediente.Edit();
						expediente.Dat.MarcaRegRenID.Value = marcaregrenID;
						expediente.PostEdit();
						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							expediente.Adapter.ConcurrenceOn = false;
						}
						expediente.Adapter.UpdateRow();
						#endregion Expediente-MarcaRegRen

						#region Marca-Expediente
						marca.Adapter.ReadByID( marcaID );
						marca.Edit();
						marca.Dat.ExpedienteVigenteID.Value = expedienteID;
						marca.Dat.MarcaRegRenID.Value = marcaregrenID;


						
	


						marca.PostEdit();
						if (vExpeMarca.Dat.MarcaID.AsString == "") 
						{
							marca.Adapter.ConcurrenceOn = false;
						}
						marca.Adapter.UpdateRow();



						#endregion Marca-Expediente

						#region Expediente_Situacion
						if (vExpeMarca.Dat.ExpedienteID.AsString == "") 
						{
							expedientesit.NewRow();
							expedientesit.Dat.ExpedienteID.Value = expedienteID;
							expedientesit.Dat.TramiteSitID.Value = tramitesitID;
							expedientesit.Dat.AltaFecha.Value = System.DateTime.Now.Date;
							expedientesit.Dat.SituacionFecha.Value = System.DateTime.Now.Date;
							expedientesit.Dat.FuncionarioID.Value = funcionarioID;
							expedientesit.Dat.VencimientoFecha.Value = Calc.SitFechaVencim(DateTime.Today,1);
							expedientesit.PostNewRow();
							expedientesit.Adapter.InsertRow();
						}
						#endregion Expediente_Situacion

						#region Borrar propietarios actuales
						// Borrar todos los expedienteXPropietario asignados al expediente
						expeXPropietario.ClearFilter();
						expeXPropietario.Dat.ExpedienteID.Filter = expedienteID;
						expeXPropietario.Adapter.ReadAll();
						for (expeXPropietario.GoTop(); !expeXPropietario.EOF; expeXPropietario.Skip()) 
						{
							expeXPropietario.Delete();
							expeXPropietario.Adapter.DeleteRow();
						}

						// Borrar todos los expedienteXPoder asignados al expediente
						expeXPoder.ClearFilter();
						expeXPoder.Dat.ExpedienteID.Filter = expedienteID;
						expeXPoder.Adapter.ReadAll();
						for (expeXPoder.GoTop(); !expeXPoder.EOF; expeXPoder.Skip()) 
						{
							expeXPoder.Delete();
							expeXPoder.Adapter.DeleteRow();
						}

						// Borrar todos los propietarioXMarca asignados a la marca
						propietarioXMarca.ClearFilter();
						propietarioXMarca.Dat.MarcaID.Filter = marcaID;
						propietarioXMarca.Adapter.ReadAll();
						for (propietarioXMarca.GoTop(); !propietarioXMarca.EOF; propietarioXMarca.Skip()) 
						{
							propietarioXMarca.Delete();
							propietarioXMarca.Adapter.DeleteRow();
						}
						#endregion Borrar propietarios actuales

						#region ExpedienteXPropietario
						if (propietario.EOF == false) 
						{
							expeXPropietario.NewRow();
							expeXPropietario.Dat.ExpedienteID.Value = expedienteID;
							expeXPropietario.Dat.PropietarioID.Value = propietario.Dat.ID.AsInt;
							expeXPropietario.Dat.DerechoPropio.Value = true;
							expeXPropietario.PostNewRow();
							expeXPropietario.Adapter.InsertRow();
						}
						#endregion ExpedienteXPropietario

						#region ExpedienteXPoder
						if (poder.EOF == false) 
						{
							expeXPoder.NewRow();
							expeXPoder.Dat.ExpedienteID.Value = expedienteID;
							expeXPoder.Dat.PoderID.Value = poder.Dat.ID.AsInt;
							expeXPoder.PostNewRow();
							expeXPoder.Adapter.InsertRow();

							//Insertar todos los propietarios asignados al poder en expedienteXPropietario
							#region Asignar propietarios en ExpedienteXPropietario
							propietarioXPoder.ClearFilter();
							propietarioXPoder.Dat.PoderID.Filter = poder.Dat.ID.AsInt;
							propietarioXPoder.Adapter.ReadAll();
							for (propietarioXPoder.GoTop(); !propietarioXPoder.EOF; propietarioXPoder.Skip()) 
							{
								expeXPropietario.NewRow();
								expeXPropietario.Dat.ExpedienteID.Value = expedienteID;
								expeXPropietario.Dat.PropietarioID.Value = propietarioXPoder.Dat.PropietarioID.AsInt;
								expeXPropietario.Dat.DerechoPropio.Value = false;
								expeXPropietario.PostNewRow();
								expeXPropietario.Adapter.InsertRow();
							}
							#endregion Asignar propietarios en ExpedienteXPropietario
						}
						#endregion ExpedienteXPoder				

						#region PropietarioXMarca
						// Insertar todos los propietarios del expediente a la marca
						expeXPropietario.ClearFilter();
						expeXPropietario.Dat.ExpedienteID.Filter = expedienteID;
						expeXPropietario.Adapter.ReadAll();
						for (expeXPropietario.GoTop(); ! expeXPropietario.EOF; expeXPropietario.Skip()) 
						{
							propietarioXMarca.NewRow();
							propietarioXMarca.Dat.MarcaID.Value = marcaID;
							propietarioXMarca.Dat.PropietarioID.Value = expeXPropietario.Dat.PropietarioID.AsInt;
							propietarioXMarca.PostNewRow();
							propietarioXMarca.Adapter.InsertRow();
						}
						#endregion PropietarioXMarca

						#region ExpedienteCampo
						//Borrar expedientecampo actuales de propietarios
						expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
						expedientecampo.Adapter.ReadAll();
						for (expedientecampo.GoTop(); !expedientecampo.EOF; expedientecampo.Skip()) 
						{
							if ( (expedientecampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_NOMBRE) |
								(expedientecampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_DIR) |
								(expedientecampo.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_PAIS) ) 
							{
								expedientecampo.Delete();
								expedientecampo.Adapter.DeleteRow();
							}
						}

						//Insertar expedientecampo con nuevos propietarios
						for (expeXPropietario.GoTop(); ! expeXPropietario.EOF; expeXPropietario.Skip()) 
						{
							propietario2.Adapter.ReadByID(expeXPropietario.Dat.PropietarioID.AsInt);
							//Nombre del propietario
							expedientecampo.NewRow();
							expedientecampo.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_NOMBRE;
							expedientecampo.Dat.ExpedienteID.Value = expedienteID;
							expedientecampo.Dat.Valor.Value = propietario2.Dat.Nombre.AsString;
							expedientecampo.PostNewRow();
							expedientecampo.Adapter.InsertRow();

							//Domicilio del propietario
							expedientecampo.NewRow();
							expedientecampo.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_DIR;
							expedientecampo.Dat.ExpedienteID.Value = expedienteID;
							expedientecampo.Dat.Valor.Value = propietario2.Dat.Direccion.AsString;
							expedientecampo.PostNewRow();
							expedientecampo.Adapter.InsertRow();

							pais.Dat.idpais.Filter = propietario2.Dat.PaisID.AsInt;
							pais.Adapter.ReadAll();
							//Pais del propietario
							expedientecampo.NewRow();
							expedientecampo.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_PAIS;
							expedientecampo.Dat.ExpedienteID.Value = expedienteID;
							expedientecampo.Dat.Valor.Value = pais.Dat.paisalfa.AsString;
							expedientecampo.PostNewRow();
							expedientecampo.Adapter.InsertRow();
						}
						#endregion ExpedienteCampo

						#region ExpedienteInstruccion
						//Borrar las instrucciones asociadas al expediente
						if (vExpeMarca.Dat.ExpedienteID.AsString != "") 
						{
							expedienteinstr.Dat.ExpedienteID.Filter = expedienteID;
							expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
							expedienteinstr.Adapter.ReadAll();
							if (expedienteinstr.RowCount > 0) 
							{
								expedienteinstr.Delete();
								expedienteinstr.Adapter.DeleteRow();
							}

							expedienteinstr.Dat.ExpedienteID.Filter = expedienteID;
							expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
							expedienteinstr.Adapter.ReadAll();
							if (expedienteinstr.RowCount > 0) 
							{
								expedienteinstr.Delete();
								expedienteinstr.Adapter.DeleteRow();
							}
						}

						//Insertar instrucción para poder - Utilizamos tramiteabrev para la observacion
						if (vExpeMarca.Dat.TramiteAbrev.AsString != "") 
						{
							expedienteinstr.NewRow();
							expedienteinstr.Dat.ExpedienteID.Value = expedienteID;
							expedienteinstr.Dat.Fecha.Value = System.DateTime.Now.Date;						
							expedienteinstr.Dat.FuncionarioID.Value = ot.Dat.FuncionarioID.AsInt;
							expedienteinstr.Dat.InstruccionTipoID.Value = (int) GlobalConst.InstruccionTipo.PODER;
							expedienteinstr.Dat.MarcaID.Value = marcaID;
							expedienteinstr.Dat.CorrespondenciaID.Value = corr.Dat.ID.AsInt;
							expedienteinstr.Dat.Obs.Value = vExpeMarca.Dat.TramiteAbrev.AsString;
							expedienteinstr.PostNewRow();
							expedienteinstr.Adapter.InsertRow();
						}

						//Insertar instrucción para contabilidad - Utilizamos tramitedescrip para la observacion
						if (vExpeMarca.Dat.TramiteDescrip.AsString != "") 
						{
							expedienteinstr.NewRow();
							expedienteinstr.Dat.ExpedienteID.Value = expedienteID;
							expedienteinstr.Dat.Fecha.Value = System.DateTime.Now.Date;
							expedienteinstr.Dat.FuncionarioID.Value = ot.Dat.FuncionarioID.AsInt;
							expedienteinstr.Dat.InstruccionTipoID.Value = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
							expedienteinstr.Dat.MarcaID.Value = marcaID;
							expedienteinstr.Dat.CorrespondenciaID.Value = corr.Dat.ID.AsInt;
							expedienteinstr.Dat.Obs.Value = vExpeMarca.Dat.TramiteDescrip.AsString;
							expedienteinstr.PostNewRow();
							expedienteinstr.Adapter.InsertRow();
						}
						#endregion ExpedienteInstruccion

						#region Marca-Propietario
						marca.Adapter.ReadByID( marcaID );
						marca.Edit();
						if (poder.EOF == false) 
						{
							marca.Dat.Propietario.Value = poder.Dat.Denominacion.AsString;
							marca.Dat.ProDir.Value = poder.Dat.Domicilio.AsString;
							pais.Dat.idpais.Filter = poder.Dat.PaisID.AsInt;
							pais.Adapter.ReadAll();
							marca.Dat.ProPais.Value = pais.Dat.abrev.AsString;
						} 
						else 
						{
							marca.Dat.Propietario.Value = propietario.Dat.Nombre.AsString;
							marca.Dat.ProDir.Value = propietario.Dat.Direccion.AsString;
							pais.Dat.idpais.Filter = propietario.Dat.PaisID.AsInt;
							pais.Adapter.ReadAll();
							marca.Dat.ProPais.Value = pais.Dat.abrev.AsString;
						}
						marca.PostEdit();
						marca.Adapter.UpdateRow();
						#endregion Marca-Propietario
					}
					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
				
					throw new Exception(" Error en Registro Upsert" + e.Message );
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
	} // End RegistroUpsert class

	/* Entrada para el fwk.Config
						<action code="Registro_Upsert" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Registro.Upsert,Berke.Marcas.BizActions"
						request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
						DebugMode="true" log-header="true" log-request="true" log-response="true" />
	*/
	#endregion Registro_Upsert

	#region Registro_Delete
	public class Delete: IAction
	{	
		public void Execute( Command cmd ) 
		{
			#region Delaracion de alias de DG y objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
			Berke.DG.ViewTab.ParamTab outTB	= new Berke.DG.ViewTab.ParamTab();

			//Tablas locales
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo();

			//Inicializar Adapters		
			ot.InitAdapter( db );
			expediente.InitAdapter( db );
			int ordenTrabajoID = inTB.Dat.Entero.AsInt;
			ot.Adapter.ReadByID( ordenTrabajoID );
			BorrarMarcasMasiva bm = new BorrarMarcasMasiva();
			bool okReg = true;
			#endregion Delaracion de alias de DG y objetos locales			

			try 
			{
				try
				{
					db.IniciarTransaccion();
					expediente.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
					expediente.Adapter.ReadAll();
					string str_marca = "";
					for (expediente.GoTop(); ! expediente.EOF;	expediente.Skip()) 
					{
						if (str_marca == "") 
						{
							str_marca = expediente.Dat.MarcaID.AsString;
						} 
						else 
						{
							str_marca = str_marca + "," + expediente.Dat.MarcaID.AsString;
						}
					}
					bm.Execute(db, str_marca);
					ot.Delete();
					ot.Adapter.DeleteRow();
					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
					okReg = false;
					throw new Exception(" Error en Registro Delete" + e.Message );
				}
				//Notificacion
				string noti;
				noti = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link("RegistroDetalle.aspx", ot.Dat.Nro.AsString + "/" + ot.Dat.Anio.AsString, ordenTrabajoID.ToString(),"otID");
				db.IniciarTransaccion();
				Berke.Marcas.BizActions.Lib.Notificar( 9, noti , db );
				db.Commit();
				
				//Notificacion

				DataSet tmp_OutDS=new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = okReg;
				outTB.Dat.Entero.Value = ordenTrabajoID;
				outTB.PostNewRow();
				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );
			}
			finally {
				db.CerrarConexion();
			}
			}	
	} // End RegistroDelete class

	/* Entrada para el fwk.Config
						<action code="Registro_Delete" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Registro.Delete,Berke.Marcas.BizActions"
						request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
						DebugMode="true" log-header="true" log-request="true" log-response="true" />
	*/
	#endregion Registro_Delete

} // End Namespace