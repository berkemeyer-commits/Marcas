#region TV. BorrarExpediente
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	public class BorrarExpediente
	{

	public void Execute (Berke.Libs.Base.Helpers.AccesoDB db, int expedienteID)
	{
		#region Declaracion objetos locales
		//Tablas locales
		Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
		Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
		Berke.DG.DBTab.Expediente_Situacion expedientesit = new Berke.DG.DBTab.Expediente_Situacion();
		Berke.DG.DBTab.ExpedienteXPoder expeXPoder = new Berke.DG.DBTab.ExpedienteXPoder();
		Berke.DG.DBTab.ExpedienteXPropietario expeXPropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
		Berke.DG.DBTab.PropietarioXPoder propietarioXPoder = new Berke.DG.DBTab.PropietarioXPoder();
		Berke.DG.DBTab.PropietarioXMarca propietarioXMarca = new Berke.DG.DBTab.PropietarioXMarca();
		Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = new Berke.DG.DBTab.Expediente_Instruccion();
		Berke.DG.DBTab.ExpedienteCampo expedientecampo = new Berke.DG.DBTab.ExpedienteCampo();
		Berke.DG.DBTab.Expediente expe2 = new Berke.DG.DBTab.Expediente();
		Berke.DG.DBTab.Tramite_Sit tramSit = new Berke.DG.DBTab.Tramite_Sit();
		Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion();

		//Inicializar Adapters		
		marca.InitAdapter( db );
		expediente.InitAdapter( db );
		expedientesit.InitAdapter( db );
		expeXPoder.InitAdapter( db );
		expeXPropietario.InitAdapter( db );
		propietarioXPoder.InitAdapter( db );
		propietarioXMarca.InitAdapter( db );
		expedienteinstr.InitAdapter( db );
		expedientecampo.InitAdapter( db );
		expe2.InitAdapter( db );
		tramSit.InitAdapter( db );
		sit.InitAdapter( db );

		string [] aPropietarios;
		string [] aPoderes;
		#endregion Declaracion objetos locales

		expediente.Adapter.ReadByID( expedienteID );

		#region Controlar situacion de HI
		
		tramSit.ClearFilter();
		tramSit.Adapter.ReadByID( expediente.Dat.TramiteSitID.AsInt );
		sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );				
		if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
		{
			throw new Exception("Solo se pueden eliminar las HI que se encuentren en situación hoja de inicio");
		}
		#endregion Controlar situacion de HI

		#region expeXPropietario
		expeXPropietario.Dat.ExpedienteID.Filter = expedienteID;
		expeXPropietario.Adapter.ReadAll();
		for (expeXPropietario.GoTop(); !expeXPropietario.EOF; expeXPropietario.Skip()) 
		{
			expeXPropietario.Delete();
			expeXPropietario.Adapter.DeleteRow();
		}
		#endregion expeXPropietario

		#region propietarioXMarca
		//Borrar propietarioXMarca nuevos
		propietarioXMarca.Dat.MarcaID.Filter = expediente.Dat.MarcaID.AsInt;
		propietarioXMarca.Adapter.ReadAll();
		for (propietarioXMarca.GoTop(); !propietarioXMarca.EOF; propietarioXMarca.Skip()) 
		{
			propietarioXMarca.Delete();
			propietarioXMarca.Adapter.DeleteRow();
		}

		//Restaurar propietarioXMarca anteriores
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_ID;
		expedientecampo.Adapter.ReadAll();
		aPropietarios = expedientecampo.Dat.Valor.AsString.Split( ((String)",").ToCharArray() );
		for (int i = 0; i < aPropietarios.Length; i++) {
			propietarioXMarca.NewRow();
			propietarioXMarca.Dat.MarcaID.Value = expediente.Dat.MarcaID.AsInt;
			propietarioXMarca.Dat.PropietarioID.Value = aPropietarios[i];
			propietarioXMarca.PostNewRow();
			propietarioXMarca.Adapter.InsertRow();
		}
		#endregion propietarioXMarca

		#region Marca
		marca.Adapter.ReadByID( expediente.Dat.MarcaID.AsInt );
		marca.Edit();

		//Nombre del propietario anterior
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
		expedientecampo.Adapter.ReadAll();
        marca.Dat.Propietario.Value = expedientecampo.Dat.Valor.AsString;
		//Direccion del propietario anterior
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
		expedientecampo.Adapter.ReadAll();
		marca.Dat.ProDir.Value = expedientecampo.Dat.Valor.AsString;
		//Pais del propietario anterior
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
		expedientecampo.Adapter.ReadAll();
		marca.Dat.ProPais.Value = expedientecampo.Dat.Valor.AsString;
		//Identificador del cliente anterior
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.CLI_ANTERIOR_ID;
		expedientecampo.Adapter.ReadAll();
		marca.Dat.ClienteID.Value = expedientecampo.Dat.Valor.AsString;
		//Vigilada anterior
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Dat.Campo.Filter = GlobalConst.VIGILADA_ANTERIOR;
		expedientecampo.Adapter.ReadAll();
		if (expedientecampo.RowCount > 0) {
			marca.Dat.Vigilada.Value = expedientecampo.Dat.Valor.AsString;
		}
		marca.PostEdit();
		marca.Adapter.UpdateRow();
		#endregion Marca

		#region Ver si se debe restaurar el expediente padre
		//En caso en que se haya actualizado el expediente padre de Registro o Renovación
		//con los datos del propietario, es necesario restaurar el propietario anterior
		//si fuese por derecho propio o el poder anterior
		expe2.Adapter.ReadByID( expediente.Dat.ExpedienteID.AsInt );
		expedientesit.ClearFilter();
		expedientesit.Dat.ExpedienteID.Filter = expediente.Dat.ExpedienteID.AsInt;
		expedientesit.Adapter.ReadAll();

		bool actualizarExpePadre = true;
		//Si el expediente padre aun no fue concedido, se debe restaurar el poder anterior
		for (expedientesit.GoTop(); !expedientesit.EOF; expedientesit.Skip()) {
			tramSit.Adapter.ReadByID( expedientesit.Dat.TramiteSitID.AsInt );
			sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );
			if (sit.Dat.ID.AsInt == (int)GlobalConst.Situaciones.CONCEDIDA) {
				actualizarExpePadre = false;
				break;
			}
		}
		#endregion Ver si se debe restaurar el expediente padre

		#region Restaurar expediente padre
		if (actualizarExpePadre) 
		{
			//Borrar expedienteXpoder anterior
			expeXPoder.Dat.ExpedienteID.Filter = expediente.Dat.ExpedienteID.AsInt;
			expeXPoder.Adapter.ReadAll();
			for (expeXPoder.GoTop(); !expeXPoder.EOF; expeXPoder.Skip()) {
				expeXPoder.Delete();
				expeXPoder.Adapter.DeleteRow();
			}

			//Borrar expedienteXpropietario anterior
			expeXPropietario.Dat.ExpedienteID.Filter = expediente.Dat.ExpedienteID.AsInt;
			expeXPropietario.Adapter.ReadAll();
			for (expeXPropietario.GoTop(); !expeXPropietario.EOF; expeXPropietario.Skip()) {
				expeXPropietario.Delete();
				expeXPropietario.Adapter.DeleteRow();
			}
			
			//Restaurar poder anterior en expedienteXpoder y expedienteXpropietario
			expedientecampo.ClearFilter();
			expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
			expedientecampo.Dat.Campo.Filter = GlobalConst.POD_ANTERIOR_ID;
			expedientecampo.Adapter.ReadAll();
			aPoderes = expedientecampo.Dat.Valor.AsString.Split( ((String)",").ToCharArray() );
			for (int i = 0; i < aPoderes.Length; i++) {
				expeXPoder.NewRow();
				expeXPoder.Dat.ExpedienteID.Value = expediente.Dat.ExpedienteID.AsInt;
				expeXPoder.Dat.PoderID.Value = aPoderes[i];
				expeXPoder.PostNewRow();
				expeXPoder.Adapter.InsertRow();
				propietarioXPoder.ClearFilter();
				propietarioXPoder.Dat.PoderID.Filter = expeXPoder.Dat.PoderID.AsInt;
				propietarioXPoder.Adapter.ReadAll();
				for (propietarioXPoder.GoTop(); !propietarioXPoder.EOF; propietarioXPoder.Skip()) {
					expeXPropietario.NewRow();					
					expeXPropietario.Dat.PropietarioID.Value = propietarioXPoder.Dat.PropietarioID.AsInt;
					expeXPropietario.Dat.DerechoPropio.Value = false;
					expeXPropietario.Dat.ExpedienteID.Value = expediente.Dat.ExpedienteID.AsInt;
					expeXPropietario.PostNewRow();
					expeXPropietario.Adapter.InsertRow();
				}				
			}
			//Restaurar propietario anterior en expedienteXpropietario si fuese por derecho propio
			if (aPoderes.Length <= 0) {
				expedientecampo.ClearFilter();
				expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
				expedientecampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_ID;
				expedientecampo.Adapter.ReadAll();
				aPropietarios = expedientecampo.Dat.Valor.AsString.Split( ((String)",").ToCharArray() );
				for (int i = 0; i < aPropietarios.Length; i++) 
				{
					expeXPropietario.NewRow();
					expeXPropietario.Dat.DerechoPropio.Value = true;
					expeXPropietario.Dat.ExpedienteID.Value = expediente.Dat.ExpedienteID.AsInt;
					expeXPropietario.Dat.PropietarioID.Value = aPropietarios[i];
					expeXPropietario.PostNewRow();
					expeXPropietario.Adapter.InsertRow();
				}
			}
		}
		#endregion Restaurar expediente padre

		#region ExpedienteXPoder
		expeXPoder.ClearFilter();
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
		expedientesit.Adapter.ReadAll();
		expedientesit.Delete();
		expedientesit.Adapter.DeleteRow();
		#endregion Expediente_Situacion

		#region ExpedienteInstruccion
		expedienteinstr.ClearFilter();
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
		expedientecampo.ClearFilter();
		expedientecampo.Dat.ExpedienteID.Filter = expedienteID;
		expedientecampo.Adapter.ReadAll();
		for (expedientecampo.GoTop(); !expedientecampo.EOF; expedientecampo.Skip()) {
			expedientecampo.Delete();
			expedientecampo.Adapter.DeleteRow();
		}
		#endregion ExpedienteCampo

		expediente.Delete();		
		expediente.Adapter.DeleteRow();		
	}
	}
}
#endregion TV. BorrarExpediente

#region TV. Delete
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Delete: IAction
	{	
		public void Execute( Command cmd ) 
		{
			bool okTV = true;
			string mensaje = "";
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");	

			try 
			{
				try
				{
					db.IniciarTransaccion();

					#region Declaración de objetos locales
					Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
					Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo();
					Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
					Berke.DG.DBTab.Tramite_Sit tramSit = new Berke.DG.DBTab.Tramite_Sit();
					Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion();
					Berke.DG.DBTab.Expediente expe2 = new Berke.DG.DBTab.Expediente();
					Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
					ot.InitAdapter( db );
					expe.InitAdapter( db );
					tramSit.InitAdapter( db );
					sit.InitAdapter( db );
					expe2.InitAdapter( db );
					marca.InitAdapter( db );
					ot.Adapter.ReadByID(inTB.Dat.Entero.AsInt);
					expe.Dat.OrdenTrabajoID.Filter = inTB.Dat.Entero.AsInt;
					expe.Adapter.ReadAll();
					BorrarExpediente be = new BorrarExpediente();
					#endregion Declaración de objetos locales

					for (expe.GoTop(); !expe.EOF; expe.Skip() ) 
					{
						#region Controles de datos del expediente
						//Verificar situación de la HI
						tramSit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
						sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );				
						if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
						{
							throw new Exception("Solo se pueden eliminar las HI que se encuentren en situación hoja de inicio");
						}
                    
						//Verificar otros TV posteriores
						expe2.Dat.MarcaID.Filter = expe.Dat.MarcaID.AsInt;
						//expe2.Dat.AltaFecha.Order = 1;
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
										" es referenciada en otros TV posteriores y no podra ser eliminada");
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
										" es referenciada en otros TV posteriores y no podra ser eliminada");
								}
							}
						}					
						#endregion Controles de datos del expediente

						be.Execute(db, expe.Dat.ID.AsInt);
					}
					ot.Delete();
					ot.Adapter.DeleteRow();
					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
					throw new Exception(" Error en TV Delete" + e.Message );
					//okTV = false;
				}

				
				#region Asigacion de Valores de Salida		
				// ParamTab
				Berke.DG.ViewTab.ParamTab outTB	= new Berke.DG.ViewTab.ParamTab();
				DataSet  tmp_OutDS = new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = okTV;
				outTB.Dat.Alfa.Value = mensaje;
				outTB.PostNewRow();
				#endregion Asigacion de Valores de Salida

				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );	
			} 
			finally {db.CerrarConexion(); }
		}
	}
}
/*
				<action code="TV_Delete" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.Delete,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" /> 
*/
#endregion TV. Delete

#region TV.	InsertarExpediente
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class InsertarExpediente
	{	
		public void Execute( Berke.DG.TVDG inDG, Berke.Libs.Base.Helpers.AccesoDB db, 
							 int ordenTrabajoID,
							 int tipoTramite ) 
		{
			#region Declaración de objetos locales
			//DataGroup inputTVDG
			Berke.DG.ViewTab.vRenovacionMarca vM = inDG.vRenovacionMarca ;
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
			Berke.DG.ViewTab.vPoderActual vPA = inDG.vPoderActual ;
			Berke.DG.ViewTab.vPoderAnterior vPN = inDG.vPoderAnterior ;
			Berke.DG.DBTab.ExpedienteCampo ec = inDG.ExpedienteCampo ;
			Berke.DG.DBTab.Expediente_Instruccion ei = inDG.Expediente_Instruccion;						

			//DataTables
			Berke.DG.DBTab.ExpedienteXPoder expeXpoder = new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.ExpedienteXPropietario expeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.PropietarioXPoder propXpoder = new Berke.DG.DBTab.PropietarioXPoder( db );
			Berke.DG.DBTab.Expediente_Situacion expeSit = new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.ExpedienteCampo ec2 = new Berke.DG.DBTab.ExpedienteCampo( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais( db );
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder( db );
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario( db );
			Berke.DG.DBTab.Expediente_Instruccion expeInstruccion = new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.Correspondencia corr = new Berke.DG.DBTab.Correspondencia( db );
			Berke.DG.DBTab.Expediente expe2 = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Tramite_Sit tramSit = new Berke.DG.DBTab.Tramite_Sit( db );
			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion( db );
			
			int expedienteID = 0;
			int expedientePadreID = 0;

			bool actualizarPropietario = true;
			bool actualizarPoder = true;
			bool actualizarExpePadre = false;

			string str_propietario = "";
			string str_poder = "";
			int sitTramite = 0;
			int clienteID = ot.Dat.ClienteID.AsInt;
			int funcionarioID = ot.Dat.FuncionarioID.AsInt;
			int marcaID = vM.Dat.MarcaAnteriorID.AsInt;
			#endregion Declaración de objetos locales

			#region Determinar Tipo de Tramite
			switch(tipoTramite) {
					#region Transferencia
					
				case (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA:
					sitTramite = (int) GlobalConst.SituacionesXTramite.TRANSFERENCIA_HI;
					break;

					#endregion Transferencia

					#region Cambio de Nombre

				case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE:
					sitTramite = (int) GlobalConst.SituacionesXTramite.CAMBIO_NOMBRE_HI;
					break;

					#endregion Cambio de Nombre

					#region Fusion

				case (int) GlobalConst.Marca_Tipo_Tramite.FUSION:
					sitTramite = (int) GlobalConst.SituacionesXTramite.FUSION_HI;
					break;

					#endregion Fusion

					#region Cambio de Domicilio

				case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO:
					sitTramite = (int) GlobalConst.SituacionesXTramite.CAMBIO_DOMICILIO_HI;
					break;

					#endregion Cambio de Domicilio

					#region Licencia

				case (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA:

					sitTramite = (int) GlobalConst.SituacionesXTramite.LICENCIA_HI;
					actualizarPropietario = false;						
					break;
					#endregion Licencia

					#region Duplicado de Titulo

				case (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO:

					sitTramite = (int) GlobalConst.SituacionesXTramite.DUPLICADO_HI;
					actualizarPropietario = false;
					actualizarPoder = false;
					break;

					#endregion Duplicado de Titulo
			}			
			#endregion Determinar Tipo de Tramite				

			#region Recuperar expediente padre
			/* En caso en que exista alguna renovación posterior que aún no 
			 * fuese concedida, se debe tomar dicho expediente como
			 * el expediente padre. Esta búsqueda solo se debe realizar cuando
			 * se inserta la marca en el TV (marcaid = 0) */
			if (vM.Dat.MarcaID.AsInt == 0) {
				expe2.ClearFilter();
				expe2.Dat.ExpedienteID.Filter = vM.Dat.ExpedienteID.AsInt;			
				expe2.Adapter.ReadAll();
				for (expe2.GoTop(); !expe2.EOF; expe2.Skip()) {
					expeSit.ClearFilter();
					expeSit.Dat.ExpedienteID.Filter = expe2.Dat.ID.AsInt;
					expeSit.Adapter.ReadAll();
					/* Si existe algun expediente posterior que ya fue concedido, 
							* no es posible asignarle el registro indicado */
					for (expeSit.GoTop(); !expeSit.EOF; expeSit.Skip()) {
						tramSit.ClearFilter();
						tramSit.Adapter.ReadByID( expeSit.Dat.TramiteSitID.AsInt );
						sit.ClearFilter();
						sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );
						if ( (expe2.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) &
							(sit.Dat.ID.AsInt == (int)GlobalConst.Situaciones.CONCEDIDA) ) {
							throw new Exception("El numero de registro " + vM.Dat.RegistroNro.AsString +
								" para la marca " + vM.Dat.Denominacion.AsString +
								" no se encuentra vigente. Debe indicar el numero de registro vigente");
						}
					}
					
					/* Reasignar el expediente padre de manera a que apunte al último
							* registro o renovación que se encuentre en trámite */
					if ( (expe2.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) |
						(expe2.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) ) {
						expedientePadreID = expe2.Dat.ID.AsInt;
						marcaID = expe2.Dat.MarcaID.AsInt;
						actualizarExpePadre = true;
						break;
					}
				}
			}

			if (expedientePadreID == 0) {
				expedientePadreID = vM.Dat.ExpedienteID.AsInt;
			}
			#endregion Recuperar expediente padre
				
			#region Ver si es necesario actualizar expediente padre
			/* Verificar la situación del expediente padre para saber si se
					* deben actualizar los datos del propietario*/
			if (!actualizarExpePadre) 
			{
				expeSit.ClearFilter();
				expeSit.Dat.ExpedienteID.Filter = expedientePadreID;
				expeSit.Adapter.ReadAll();
				actualizarExpePadre = true;
				for (expeSit.GoTop(); !expeSit.EOF; expeSit.Skip()) 
				{
					tramSit.ClearFilter();
					tramSit.Adapter.ReadByID( expeSit.Dat.TramiteSitID.AsInt );
					sit.ClearFilter();
					sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );
					if (sit.Dat.ID.AsInt == (int)GlobalConst.Situaciones.CONCEDIDA) 
					{
						actualizarExpePadre = false;
						break;
					}
				}
			}
			#endregion Ver si es necesario actualizar expediente padre

			#region Cargar Expediente
			expe.ClearFilter();
			expe.Dat.MarcaID.Filter = marcaID;
			expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
			expe.Dat.TramiteID.Filter = tipoTramite;
			expe.Adapter.ReadAll();
			bool ins_expediente = false;
			if (expe.RowCount > 0) {
				ins_expediente = false;
			} else {
				ins_expediente = true;
			}

			if (!ins_expediente ) 
			{

				#region Controlar situacion de HI
		
				tramSit.ClearFilter();
				tramSit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
				sit.Adapter.ReadByID( tramSit.Dat.SituacionID.AsInt );				
				if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
				{
					throw new Exception("Solo se pueden modifica las HI que se encuentren en situación hoja de inicio");
				}
				#endregion Controlar situacion de HI
			}

			if (ins_expediente) {
				expe.NewRow(); 
			} else {
				expe.Edit();
			}
			expe.Dat.TramiteID			.Value = tipoTramite;   //int Oblig.
		
			if (ins_expediente) 
			{
				expe.Dat.TramiteSitID		.Value = sitTramite;   //int Oblig.
				expe.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
			}
			

			expe.Dat.OrdenTrabajoID		.Value = ordenTrabajoID;   //int
			expe.Dat.ClienteID			.Value = clienteID;   //int
			expe.Dat.ExpedienteID		.Value = expedientePadreID;
			expe.Dat.Nuestra			.Value = true;   //bit Oblig.
			expe.Dat.Sustituida			.Value = false;   //bit Oblig.
			expe.Dat.StandBy			.Value = false;   //bit Oblig.
			expe.Dat.Vigilada			.Value = true;   //bit Oblig.
			expe.Dat.Concluido			.Value = false;   //bit Oblig.
			//expe.Dat.AltaFecha			.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
			expe.Dat.MarcaID			.Value = marcaID;   //int
			if (ins_expediente) {
				expe.PostNewRow();
				expedienteID = expe.Adapter.InsertRow();
			} else {
				expe.PostEdit();
				expedienteID = expe.Dat.ID.AsInt;
				expe.Adapter.UpdateRow();
			}
			#endregion Cargar Expediente

			#region Cargar ExpedienteSituacion
			if (ins_expediente) {
				expeSit.NewRow(); 
				expeSit.Dat.ExpedienteID	.Value = expedienteID;   //int Oblig.
				expeSit.Dat.TramiteSitID	.Value = sitTramite;   //int Oblig.
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
				
			#region Cargar ExpedienteCampo
			ec2.ClearFilter();
			ec2.Dat.ExpedienteID.Filter = expedienteID;
			ec2.Adapter.ReadAll();
			for (ec2.GoTop(); !ec2.EOF; ec2.Skip()) {
				if ( (ec2.Dat.Campo.AsString == GlobalConst.FUS_NOMBRE_OTROS_PROP) |
				 	 (ec2.Dat.Campo.AsString == GlobalConst.FUS_DIR_OTROS_PROP) |
					 (ec2.Dat.Campo.AsString == GlobalConst.LIC_NOMBRE) |
					 (ec2.Dat.Campo.AsString == GlobalConst.LIC_DIRECCION) |
					 (ec2.Dat.Campo.AsString == GlobalConst.LIC_VIGENC_DESDE) |
					 (ec2.Dat.Campo.AsString == GlobalConst.LIC_VIGENC_HASTA) |
					 (ec2.Dat.Campo.AsString == GlobalConst.LIC_DESCRIPCION) |
					 (ec2.Dat.Campo.AsString == GlobalConst.DUP_LEGISLACION_CONSULAR) ) {
					ec2.Delete();
					ec2.Adapter.DeleteRow();
				}
			}

			for (ec.GoTop();!ec.EOF; ec.Skip()) {
				ec.Edit();
				ec.Dat.ExpedienteID			.Value = expedienteID;   //int Oblig.
				ec.PostEdit();
                ec.Adapter.InsertRow();
			}
			#endregion Cargar ExpedienteCampo

			#region Cargar Expediente_Instruccion
			//Borrar Instrucción de Poder
			expeInstruccion.ClearFilter();
			expeInstruccion.Dat.ExpedienteID.Filter = expedienteID;
			expeInstruccion.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
            expeInstruccion.Adapter.ReadAll();
			if (expeInstruccion.RowCount > 0) {
				expeInstruccion.Adapter.DeleteRow();
			}

			//Borrar Instrucción de Contabilidad
			expeInstruccion.ClearFilter();
			expeInstruccion.Dat.ExpedienteID.Filter = expedienteID;
			expeInstruccion.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
			expeInstruccion.Adapter.ReadAll();
			if (expeInstruccion.RowCount > 0) {
				expeInstruccion.Adapter.DeleteRow();
			}

			//Insertar instrucciones
			ei.InitAdapter( db );
			for (ei.GoTop(); !ei.EOF; ei.Skip()) {
				ei.Edit();
				ei.Dat.ExpedienteID.Value = expedienteID;
				ei.Dat.MarcaID.Value = vM.Dat.MarcaAnteriorID.AsInt;
				ei.Dat.CorrespondenciaID.Value = corr.Dat.ID.AsInt;
				ei.PostEdit();
				ei.Adapter.InsertRow();
			}					
			#endregion Cargar Expediente_Instruccion

			#region Guardar propietarios actuales en ExpedienteCampo
			if (! ins_expediente ) {
				/* Si es una modificación del TV, se deben borrar de
					* expedientecampo solo los datos del propietario actual,
					* ya que el propietario anterior ya fue insertado */
				ec2.ClearFilter();
				ec2.Dat.ExpedienteID.Filter = expedienteID;
				ec2.Adapter.ReadAll();
				for (ec2.GoTop(); !ec2.EOF; ec2.Skip()) {
					if ( (ec2.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_NOMBRE) |
						 (ec2.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_DIR) |
						 (ec2.Dat.Campo.AsString == GlobalConst.PROP_ACTUAL_PAIS) ) {
						ec2.Delete();
						ec2.Adapter.DeleteRow();
					}
				}
			}

			//Nombre propietario actual
			ec2.NewRow();
			ec2.Dat.ExpedienteID.Value = expedienteID;
			ec2.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_NOMBRE;
			if (vPA.Dat.Denominacion.AsString == "") {
				ec2.Dat.Valor.Value = vPN.Dat.Denominacion.AsString;
			} else {
				ec2.Dat.Valor.Value = vPA.Dat.Denominacion.AsString;
			}
			ec2.PostNewRow();
			ec2.Adapter.InsertRow();

			//Dirección propietario actual
			ec2.NewRow();
			ec2.Dat.ExpedienteID.Value = expedienteID;
			ec2.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_DIR;
			if (vPA.Dat.Domicilio.AsString == "") {
				ec2.Dat.Valor.Value = vPN.Dat.Domicilio.AsString;
			} else {
				ec2.Dat.Valor.Value = vPA.Dat.Domicilio.AsString;
			}
			ec2.PostNewRow();
			ec2.Adapter.InsertRow();

			//Pais propietario actual
			if (vPA.Dat.Concepto.AsString == "Por derecho propio") 
			{				
				if (vPA.Dat.ID.AsInt == 0) {
					propietario.Adapter.ReadByID(vPN.Dat.ID.AsInt);
				} else {
					propietario.Adapter.ReadByID(vPA.Dat.ID.AsInt);
				}
				pais.Dat.idpais.Filter = propietario.Dat.PaisID.AsInt;
				pais.Adapter.ReadAll();
			} 
			else 
			{
				if (vPA.Dat.ID.AsInt == 0) {
					poder.Adapter.ReadByID(vPN.Dat.ID.AsInt);
				} else {
					poder.Adapter.ReadByID(vPA.Dat.ID.AsInt);
				}
				pais.Dat.idpais.Filter = poder.Dat.PaisID.AsInt;
				pais.Adapter.ReadAll();
			}

			ec2.NewRow();
			ec2.Dat.ExpedienteID.Value = expedienteID;
			ec2.Dat.Campo.Value = GlobalConst.PROP_ACTUAL_PAIS;
			ec2.Dat.Valor.Value = pais.Dat.paisalfa.AsString;
			ec2.PostNewRow();
			ec2.Adapter.InsertRow();
			#endregion Guardar propietarios actuales en ExpedienteCampo

			#region Guardar cliente anterior en ExpedienteCampo
			marca.Adapter.ReadByID( marcaID );
			if (ins_expediente ) {
				/* Solo se debe guardar el cliente anterior id cuando
				 * se inserta en el expediente */
				ec2.NewRow();
				ec2.Dat.ExpedienteID.Value = expedienteID;
				ec2.Dat.Campo.Value = GlobalConst.CLI_ANTERIOR_ID;
				ec2.Dat.Valor.Value = marca.Dat.ClienteID.AsInt;
				ec2.PostNewRow();
				ec2.Adapter.InsertRow();
			}
			#endregion Guardar cliente anterior en ExpedienteCampo
			
			#region Guardar propietarios anteriores en ExpedienteCampo
			if (ins_expediente) {
				/* Solo se deben guardar los datos del propietario anterior
					 * al insertar el expediente */
				//Nombre propietario anterior
				ec2.NewRow();
				ec2.Dat.ExpedienteID.Value = expedienteID;
				ec2.Dat.Campo.Value = GlobalConst.PROP_ANTERIOR_NOMBRE;
				ec2.Dat.Valor.Value = vPN.Dat.Denominacion.AsString;
				ec2.PostNewRow();
				ec2.Adapter.InsertRow();

				//Dirección propietario anterior
				ec2.NewRow();
				ec2.Dat.ExpedienteID.Value = expedienteID;
				ec2.Dat.Campo.Value = GlobalConst.PROP_ANTERIOR_DIR;
				ec2.Dat.Valor.Value = vPN.Dat.Domicilio.AsString;
				ec2.PostNewRow();
				ec2.Adapter.InsertRow();

				//Pais propietario anterior - Utilizamos el campo Obs para el Pais del propietario anterior
				ec2.NewRow();
				ec2.Dat.ExpedienteID.Value = expedienteID;
				ec2.Dat.Campo.Value = GlobalConst.PROP_ANTERIOR_PAIS;
				ec2.Dat.Valor.Value = vPN.Dat.Obs.AsString;
				ec2.PostNewRow();
				ec2.Adapter.InsertRow();

				//Guardar el ID de los PropietarioXMarca actuales antes de actualizarlos
				propXmarca.Dat.MarcaID.Filter = marcaID;
				propXmarca.Adapter.ReadAll();
				str_propietario = "";
				for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) {
					if (str_propietario == "") {
						str_propietario = propXmarca.Dat.PropietarioID.AsString;
					} else {
						str_propietario = str_propietario + "," + propXmarca.Dat.PropietarioID.AsString;
					}
				}
				ec2.NewRow();
				ec2.Dat.ExpedienteID.Value = expedienteID;
				ec2.Dat.Campo.Value = GlobalConst.PROP_ANTERIOR_ID;
				ec2.Dat.Valor.Value = str_propietario;
				ec2.PostNewRow();
				ec2.Adapter.InsertRow();
			}
			#endregion Guardar propietarios anteriores en ExpedienteCampo

			if (actualizarPropietario) {				
				#region Insertar PropietarioXMarca Nuevos
				//Borrar los PropietarioXMarca actuales
				propXmarca.Dat.MarcaID.Filter = marcaID;
				propXmarca.Adapter.ReadAll();
				for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
				{
					propXmarca.Delete();
					propXmarca.Adapter.DeleteRow();
				}

				//Insertar los nuevos PropietarioXMarca
				if (vPA.Dat.Concepto.AsString == "Por derecho propio") 
				{
					propXmarca.NewRow();
					propXmarca.Dat.MarcaID.Value = marcaID;
					propXmarca.Dat.PropietarioID.Value = vPA.Dat.ID.AsInt;
					propXmarca.PostNewRow();
					propXmarca.Adapter.InsertRow();
				} 
				else 
				{
					/* Si el tramite se registro con poder, se debe insertar en 
							* PropietarioXMarca a todos los propietarios del poder */
					propXpoder.Dat.PoderID.Filter = vPA.Dat.ID.AsInt;
					propXpoder.Adapter.ReadAll();
					for (propXpoder.GoTop(); ! propXpoder.EOF; propXpoder.Skip()) 
					{
						propXmarca.NewRow();
						propXmarca.Dat.MarcaID.Value = marcaID;
						propXmarca.Dat.PropietarioID.Value = propXpoder.Dat.PropietarioID.AsInt;
						propXmarca.PostNewRow();
						propXmarca.Adapter.InsertRow();
					}
				}
				#endregion Insertar PropietarioXMarca Nuevos										

				#region Actualizar marca
				if (vPA.Dat.Concepto.AsString == "Por derecho propio") 
				{
					propietario.Adapter.ReadByID(vPA.Dat.ID.AsInt);
					pais.Dat.idpais.Filter = propietario.Dat.PaisID.AsInt;
					pais.Adapter.ReadAll();
				} 
				else 
				{
					poder.Adapter.ReadByID(vPA.Dat.ID.AsInt);
					pais.Dat.idpais.Filter = poder.Dat.PaisID.AsInt;
					pais.Adapter.ReadAll();
				}							
				marca.Edit();
				//marca.Dat.ClienteID.Value = clienteID;
				marca.Dat.Propietario.Value = vPA.Dat.Denominacion.AsString;
				marca.Dat.ProDir.Value = vPA.Dat.Domicilio.AsString;
				marca.Dat.ProPais.Value = pais.Dat.abrev.AsString;
				if ( (marca.Dat.Vigilada.AsBoolean == false) &
					 (marca.Dat.Nuestra.AsBoolean == false ) ) {
					#region Guardar vigilada anterior
					if (ins_expediente) {
						ec2.NewRow();
						ec2.Dat.ExpedienteID.Value = expedienteID;
						ec2.Dat.Campo.Value = GlobalConst.VIGILADA_ANTERIOR;
						ec2.Dat.Valor.Value = marca.Dat.Vigilada.AsString;
						ec2.PostNewRow();
						ec2.Adapter.InsertRow();
					}
					#endregion Guardar vigilada anterior
					marca.Dat.Vigilada.Value = true;
				}
				marca.PostEdit();
				marca.Adapter.UpdateRow();
				#endregion Actualizar marca

				#region Actualizar Propietario en Expediente Padre
				if (actualizarExpePadre) 
				{
					/* Borrar los expedienteXpropietario actuales del expediente padre */
					expeXpropietario.ClearFilter();
					expeXpropietario.Dat.ExpedienteID.Filter = expedientePadreID;
					expeXpropietario.Adapter.ReadAll();
					for (expeXpropietario.GoTop(); !expeXpropietario.EOF; expeXpropietario.Skip()) 
					{
						expeXpropietario.Delete();
						expeXpropietario.Adapter.DeleteRow();
					}

					/* Borrar los expedienteXpoder actuales del expediente padre */
					expeXpoder.ClearFilter();
					expeXpoder.Dat.ExpedienteID.Filter = expedientePadreID;
					expeXpoder.Adapter.ReadAll();
					str_poder = "";
					for (expeXpoder.GoTop(); !expeXpoder.EOF; expeXpoder.Skip() ) 
					{
						if (str_poder == "") 
						{
							str_poder = expeXpoder.Dat.PoderID.AsString;
						} 
						else 
						{
							str_poder = str_poder + "," + expeXpoder.Dat.PoderID.AsString;
						}
						expeXpoder.Delete();
						expeXpoder.Adapter.DeleteRow();
					}
					// Guardar Poder Anterior del Expediente Padre
					if (ins_expediente) {
						ec2.NewRow();
						ec2.Dat.ExpedienteID.Value = expedienteID;
						ec2.Dat.Campo.Value = GlobalConst.POD_ANTERIOR_ID;
						ec2.Dat.Valor.Value = str_poder;
						ec2.PostNewRow();
						ec2.Adapter.InsertRow();
					}

					if (vPA.Dat.Concepto.AsString == "Por derecho propio") 
					{
						/* Si es por derecho propio, se debe insertar en expedienteXpropietario */
						expeXpropietario.NewRow();
						expeXpropietario.Dat.ExpedienteID.Value = expedientePadreID;
						expeXpropietario.Dat.PropietarioID.Value = vPA.Dat.ID.AsInt;
						expeXpropietario.Dat.DerechoPropio.Value = true;
						expeXpropietario.PostNewRow();
						expeXpropietario.Adapter.InsertRow();							
					} 
					else 
					{
						/* Si el tramite se registro con poder, se debe insertar en las tablas
								* expedienteXpoder y expedienteXpropietario */
						expeXpoder.NewRow();
						expeXpoder.Dat.ExpedienteID.Value = expedientePadreID;
						expeXpoder.Dat.PoderID.Value = vPA.Dat.ID.AsInt;
						expeXpoder.PostNewRow();
						expeXpoder.Adapter.InsertRow();

						propXpoder.Dat.PoderID.Filter = vPA.Dat.ID.AsInt;
						propXpoder.Adapter.ReadAll();
						for (propXpoder.GoTop(); ! propXpoder.EOF; propXpoder.Skip()) 
						{
							expeXpropietario.NewRow();
							expeXpropietario.Dat.ExpedienteID.Value = expedientePadreID;
							expeXpropietario.Dat.PropietarioID.Value = propXpoder.Dat.PropietarioID.AsInt;
							expeXpropietario.Dat.DerechoPropio.Value = false;
							expeXpropietario.PostNewRow();
							expeXpropietario.Adapter.InsertRow();
						}
					}
				}
				#endregion Actualizar Propietario en Expediente Padre
			}

			if (actualizarPoder) {
				#region Insertar Poder o Propietario Nuevo
				/* Borrar los expedienteXpropietario actuales */
				expeXpropietario.ClearFilter();
				expeXpropietario.Dat.ExpedienteID.Filter = expedienteID;
				expeXpropietario.Adapter.ReadAll();
				for (expeXpropietario.GoTop(); !expeXpropietario.EOF; expeXpropietario.Skip()) {
					expeXpropietario.Delete();
					expeXpropietario.Adapter.DeleteRow();
				}

				/* Borrar los expedienteXpoder actuales */
				expeXpoder.ClearFilter();
				expeXpoder.Dat.ExpedienteID.Filter = expedienteID;
				expeXpoder.Adapter.ReadAll();
				for (expeXpoder.GoTop(); !expeXpoder.EOF; expeXpoder.Skip() ) {
					expeXpoder.Delete();
					expeXpoder.Adapter.DeleteRow();
				}

				if (vPA.Dat.Concepto.AsString == "Por derecho propio") {
					/* Si es por derecho propio, se debe insertar ademas en expedienteXpropietario */
					expeXpropietario.NewRow();
					expeXpropietario.Dat.ExpedienteID.Value = expedienteID;
					expeXpropietario.Dat.PropietarioID.Value = vPA.Dat.ID.AsInt;
					expeXpropietario.Dat.DerechoPropio.Value = true;
					expeXpropietario.PostNewRow();
					expeXpropietario.Adapter.InsertRow();							
				} else {
					/* Si el tramite se registro con poder, se debe insertar en las tablas
							* expedienteXpoder y expedienteXpropietario */
					expeXpoder.NewRow();
					expeXpoder.Dat.ExpedienteID.Value = expedienteID;
					expeXpoder.Dat.PoderID.Value = vPA.Dat.ID.AsInt;
					expeXpoder.PostNewRow();
					expeXpoder.Adapter.InsertRow();

					propXpoder.Dat.PoderID.Filter = vPA.Dat.ID.AsInt;
					propXpoder.Adapter.ReadAll();
					for (propXpoder.GoTop(); ! propXpoder.EOF; propXpoder.Skip()) {
						expeXpropietario.NewRow();
						expeXpropietario.Dat.ExpedienteID.Value = expedienteID;
						expeXpropietario.Dat.PropietarioID.Value = propXpoder.Dat.PropietarioID.AsInt;
						expeXpropietario.Dat.DerechoPropio.Value = false;
						expeXpropietario.PostNewRow();
						expeXpropietario.Adapter.InsertRow();
					}
				}
				#endregion Insertar Poder o Propietario Nuevo						
			}
		}
	}
}
#endregion TV.	InsertarExpediente

#region TV.	Upsert
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");	
			
			bool insert = false;

			#region Declaración de objetos locales
			Berke.DG.TVDG inDG	= new Berke.DG.TVDG( cmd.Request.RawDataSet );
			Berke.DG.ViewTab.vRenovacionMarca vM = inDG.vRenovacionMarca ;
			Berke.DG.ViewTab.vPoderAnterior vPN = inDG.vPoderAnterior ;
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
			Berke.DG.DBTab.Atencion at = inDG.Atencion;
			Berke.DG.DBTab.ExpedienteCampo ec = inDG.ExpedienteCampo ;
			at.InitAdapter( db );
			ot.InitAdapter( db );
			ec.InitAdapter( db );
			Berke.DG.DBTab.Correspondencia corr = new Berke.DG.DBTab.Correspondencia( db );
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );

			int ordenTrabajoID = 0;
			int atencionID = 0;
			int clienteID = 0;
			bool okTV = true;

			string mensaje;
			int funcionarioID;
			#endregion Declaración de objetos locales

			InsertarExpediente ie = new InsertarExpediente();
			try 
			{
				try
				{
					db.IniciarTransaccion();				
					clienteID = ot.Dat.ClienteID.AsInt;
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
						ot.Dat.Nro			.Value = Berke.Libs.Base.Helpers.Calc.OrdenTrabajoNro(ot.Dat.TrabajoTipoID.AsInt);   //numero magico
						ot.Dat.Anio			.Value = System.DateTime.Now.Year;   //int Oblig.
						ot.Dat.AltaFecha	.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
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
					mensaje = ordenTrabajoID.ToString();
					#endregion OrdenTrabajo

					#region Borrar marcas de la BD
					/* Eliminar de la base de datos los expedientes que fueron eliminadas por
					 * el usuario en la interfaz de modificación. */
					expe.Dat.OrdenTrabajoID.Filter = ordenTrabajoID;
					expe.Adapter.ReadAll();
					bool borrar_expe = true;				
					BorrarExpediente be = new BorrarExpediente();
					for (expe.GoTop(); ! expe.EOF;	expe.Skip()) 
					{
						borrar_expe = true;					
						for (vM.GoTop(); ! vM.EOF;	vM.Skip()) 
						{
							if (expe.Dat.MarcaID.AsInt == vM.Dat.MarcaAnteriorID.AsInt) 
							{
								borrar_expe = false;
								break;
							}
						}
						if (borrar_expe == true) 
						{
							be.Execute(db, expe.Dat.ID.AsInt);
						}
					}
					#endregion Borrar marcas de la BD

					vPN.GoTop();
					for (vM.GoTop(); !vM.EOF; vM.Skip()) 
					{
						if (ot.Dat.TrabajoTipoID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC) 
						{
							/* Para el TV Cambio de nombre y domicilio, es necesario insertar
							 * un expediente para cambio de nombre y otro para cambio de
							 * domicilio */
							ie.Execute( inDG, db, ordenTrabajoID, (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE);
							ie.Execute( inDG, db, ordenTrabajoID, (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO);
						} 
						else 
						{
							ie.Execute( inDG, db, ordenTrabajoID, ot.Dat.TrabajoTipoID.AsInt);
						}
						vPN.Skip();
					}				

					db.Commit();
				}
				catch( Exception e )
				{
					db.RollBack();
					mensaje=e.Message;
					okTV = false;
					throw new Exception(" Error en TV Upsert : " + e.Message );
				}
			
				#region Asigacion de Valores de Salida
				//Notificacion
				string noti;
				db.IniciarTransaccion();
				noti = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link("TramitesVariosDetalle.aspx",
					ot.Dat.Nro.AsString + "/" + ot.Dat.Anio.AsString, mensaje,"otID");
				Berke.Marcas.BizActions.Lib.Notificar( 10, noti , db );
				db.Commit();
				db.CerrarConexion();
				//Notificacion
		
				// ParamTab
				Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

				DataSet  tmp_OutDS=new DataSet();
				outTB.NewRow();
				outTB.Dat.Logico.Value = okTV;
				outTB.Dat.Alfa.Value = mensaje;
				outTB.PostNewRow();
				#endregion

				tmp_OutDS.Tables.Add(outTB.Table);
				cmd.Response = new Response( tmp_OutDS );	
			}
			finally { db.CerrarConexion(); }
		}


	
	} // End Upsert class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="TV_Upsert" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.Upsert,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion Upsert

#region TV.	FillPoderAnterior
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class FillPoderAnterior: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			Berke.DG.TVDG inDG	= new Berke.DG.TVDG( cmd.Request.RawDataSet );
			Berke.DG.ViewTab.vRenovacionMarca vRM = inDG.vRenovacionMarca;
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo;

			#region Declaración de objetos locales de acceso a datos
			Berke.DG.ViewTab.vPoderAnterior outTB	=   new Berke.DG.ViewTab.vPoderAnterior();
			outTB.InitAdapter( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			marca.InitAdapter( db );
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			expe.InitAdapter( db );
			Berke.DG.DBTab.ExpedienteCampo expeCampo = new Berke.DG.DBTab.ExpedienteCampo();
			expeCampo.InitAdapter( db );
			int marcaID = 0;
			#endregion Declaración de objetos locales de acceso a datos
	
			for (vRM.GoTop();!vRM.EOF;vRM.Skip()) {
				if (ot.RowCount > 0) {
					expe.ClearFilter();
					expe.Dat.OrdenTrabajoID.Filter = ot.Dat.ID.AsInt;
					expe.Dat.MarcaID.Filter = vRM.Dat.MarcaAnteriorID.AsInt;
					expe.Adapter.ReadAll();
					/* Si el expediente correspondiente al TV ya fue insertado en la BD,
					 * entonces se deben recuperar los datos del propietario anterior de
					 * la tabla expediente campo. Esto se debe a que el propietario actual
					 * de la marca ya fue cambiado en la insercion */
					if (expe.RowCount > 0) {
						#region Recuperar propietario anterior de expediente campo
						outTB.NewRow();
						//Nombre del propietario anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
						expeCampo.Adapter.ReadAll();						
						if (expeCampo.RowCount > 0) 
						{
							outTB.Dat.Denominacion.Value = expeCampo.Dat.Valor.AsString;
						}
						//Direccion del propietario anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
						expeCampo.Adapter.ReadAll();						
						if (expeCampo.RowCount > 0) 
						{
							outTB.Dat.Domicilio.Value = expeCampo.Dat.Valor.AsString;
						}
						//Pais del propietario anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
						expeCampo.Adapter.ReadAll();						
						if (expeCampo.RowCount > 0) 
						{
							outTB.Dat.Obs.Value = expeCampo.Dat.Valor.AsString;
						}
						outTB.PostNewRow();
						#endregion Recuperar propietario anterior de expediente campo
					}
				}
				
				if ( (ot.RowCount <= 0) | (expe.RowCount <= 0) )  {									
					#region Buscar marca vigente
					marcaID = vRM.Dat.MarcaAnteriorID.AsInt;
					/* En caso en que exista alguna renovación posterior que aún no 
					 * fuese concedida, se debe tomar la marca a la que apunta 
					 * el nuevo expediente */
					expe.ClearFilter();					
					expe.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
					expe.Adapter.ReadAll();
					for (expe.GoTop(); !expe.EOF; expe.Skip()) {			
						if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) |
							 (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO) ) {
							marcaID = expe.Dat.MarcaID.AsInt;
							break;
						}
					}
					#endregion Buscar marca vigente

					#region Recuperar propietarion actual de la marca
					marca.Adapter.ReadByID( marcaID );
					outTB.NewRow();
					outTB.Dat.Denominacion.Value = marca.Dat.Propietario.AsString;
					outTB.Dat.Domicilio.Value = marca.Dat.ProDir.AsString;
					outTB.Dat.MarcaID.Value = marca.Dat.ID.AsInt;
					//Utilizamos el campo Observacion para mostrar el Pais del Propietario de la marca
					outTB.Dat.Obs.Value = marca.Dat.ProPais.AsString;
					outTB.PostNewRow();
					#endregion Recuperar propietarion actual de la marca
				}
			}

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
	
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillPoderAnterior class


}// end namespace 

	/* Entrada para el fwk.Config

					<action code="TV_FillPoderAnterior" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.FillPoderAnterior,Berke.Marcas.BizActions"
						request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
						DebugMode="true" log-header="true" log-request="true" log-response="true" />
	*/

#endregion FillPoderAnterior

//Chequeado Nuevo Sistema
#region TV.	FillPoderActual
namespace Berke.Marcas.BizActions.TV
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class FillPoderActual: IAction
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
		
			// vPoderActual
			Berke.DG.ViewTab.vPoderActual outTB	=   new Berke.DG.ViewTab.vPoderActual();
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
			poder.InitAdapter( db );
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
			propietario.InitAdapter( db );

			/* El campo logico indica si se debe recuperar el poder o el propietario, dependiendo
			 * de que el tramite que lo invoco sea por derecho propio o no. */
			if (inTB.Dat.Logico.AsBoolean)
			{
				//Buscar propietario
				propietario.Dat.ID.Filter = ObjConvert.GetFilter(inTB.Dat.Alfa.AsString);
				propietario.Adapter.ReadAll();
				for (propietario.GoTop(); !propietario.EOF; propietario.Skip()) {
					outTB.NewRow();
					outTB.Dat.ID.Value = propietario.Dat.ID.AsInt;
					outTB.Dat.Denominacion.Value = propietario.Dat.Nombre.AsString;
					outTB.Dat.Domicilio.Value = propietario.Dat.Direccion.AsString;
					outTB.Dat.Concepto.Value = "Por derecho propio";
					outTB.Dat.Obs.Value = propietario.Dat.Obs.AsString;
					outTB.PostNewRow();
				}
			} else {
				//Buscar poder
				poder.Dat.ID.Filter = ObjConvert.GetFilter(inTB.Dat.Alfa.AsString);
				poder.Adapter.ReadAll();
				for (poder.GoTop(); !poder.EOF; poder.Skip()) {
					outTB.NewRow();
					outTB.Dat.ID.Value = poder.Dat.ID.AsInt;
					outTB.Dat.Denominacion.Value = poder.Dat.Denominacion.AsString;
					outTB.Dat.Domicilio.Value = poder.Dat.Domicilio.AsString;
					outTB.Dat.Concepto.Value = poder.Dat.Concepto.AsString;
					outTB.Dat.Obs.Value = poder.Dat.Obs.AsString;
					outTB.PostNewRow();
				}
			}
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillPoderActual class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="TV_FillPoderActual" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.FillPoderActual,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion FillPoderActual

#region TV.	Read
namespace Berke.Marcas.BizActions.TV
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
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.TVDG outDG	= new Berke.DG.TVDG();


			#region Asigacion de Valores de Salida

			#region OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo ot	= outDG.OrdenTrabajo ;
			ot.InitAdapter( db );
			ot.Adapter.ReadByID(inTB.Dat.Entero.AsInt);
			#endregion OrdenTrabajo

			if (ot.RowCount!=0)
			{
		
				// Leer Expediente
				Berke.DG.DBTab.Expediente expe	= new Berke.DG.DBTab.Expediente( db );
				expe.Dat.OrdenTrabajoID.Filter= ot.Dat.ID.AsInt;   //int
				expe.Adapter.ReadAll();
				//expe.Dat.ActaNro

				System.Collections.ArrayList filtroxExpediente = new System.Collections.ArrayList();
				System.Collections.ArrayList filtroxMarca = new System.Collections.ArrayList();
				System.Collections.ArrayList filtroxPActual = new System.Collections.ArrayList();
				System.Collections.ArrayList filtroxPAnterior = new System.Collections.ArrayList();

				string ActaA="";
				Berke.DG.DBTab.Expediente expeOriginal	= new Berke.DG.DBTab.Expediente( db );

				#region Expediente_Instruccion
				expe.GoTop();
				Berke.DG.DBTab.Expediente_Instruccion ei = new Berke.DG.DBTab.Expediente_Instruccion( db );
				ei.InitAdapter( db );
				ei.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				ei.Adapter.ReadAll();
				for (ei.GoTop(); !ei.EOF; ei.Skip()) {
					if ( (ei.Dat.InstruccionTipoID.AsInt == (int) GlobalConst.InstruccionTipo.PODER) |
						 (ei.Dat.InstruccionTipoID.AsInt == (int) GlobalConst.InstruccionTipo.CONTABILIDAD) ) {
						outDG.Expediente_Instruccion.NewRow();
						outDG.Expediente_Instruccion.Dat.InstruccionTipoID.Value = ei.Dat.InstruccionTipoID.AsInt;
						outDG.Expediente_Instruccion.Dat.Obs.Value = ei.Dat.Obs.AsString;
						outDG.Expediente_Instruccion.PostNewRow();
					}
				}
				#endregion Expediente_Instruccion

				#region Recuperar Actas
				for (expe.GoTop();!expe.EOF;expe.Skip())
				{
					/* Para el caso de los tramites de Cambio de Nombre y Domicilio se generan dos
					 * expedientes: uno por el cambio de nombre y otro por el cambio de domicilio,
					 * pero en la consulta solo se debe recuperar uno de ellos. */
					if ( (ot.Dat.TrabajoTipoID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC) |
						 ( (ot.Dat.TrabajoTipoID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC) &
						   (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE) ) 
						) {
						filtroxMarca.Add(expe.Dat.MarcaID.AsInt);
						filtroxExpediente.Add(expe.Dat.ID.AsInt);
						expeOriginal.Adapter.ReadByID(expe.Dat.ExpedienteID.AsInt);
						if (expeOriginal.Dat.Acta.AsString.Trim() != "") {
							ActaA =ActaA + expeOriginal.Dat.Acta.AsString + " ";
						} else {
							/* ---aacuna--- 04/nov/2006
							 * En el caso en que el TV tenga como expediente padre a
							 * una renovación que se encuentra en trámite y aún no tiene
							 * asignados año y número de acta (situacion=HI), se deben
							 * recuperar los datos del acta del registro padre, es decir,
							 * los datos del expediente "abuelo" del TV. */
							expeOriginal.ClearFilter();
							expeOriginal.Adapter.ReadByID(expeOriginal.Dat.ExpedienteID.AsInt);
							if (expeOriginal.RowCount > 0) {
								ActaA =ActaA + expeOriginal.Dat.Acta.AsString + " ";
							}
						}
					}

				}
				#endregion Recuperar Actas

				// vRenovacionMarca
				Berke.DG.ViewTab.vRenovacionMarca vM	= outDG.vRenovacionMarca ;
				vM.NewRow(); 
				vM.Dat.Denominacion			.Value = ActaA ;   //String
				vM.PostNewRow();

				#region ExpedienteCampo
				/* Se deben retornar todos los campos de expediente campo para cualquiera de
				 * los expedientes, excepto los correspondientes al propietario actual, 
				 * propietario, cliente,poder y vigilada anterior */
                Berke.DG.DBTab.ExpedienteCampo ecRead	= new Berke.DG.DBTab.ExpedienteCampo( db );
				Berke.DG.DBTab.ExpedienteCampo ecOut	= outDG.ExpedienteCampo;
				ecRead.InitAdapter( db );
				ecOut.InitAdapter( db );
				ecRead.Dat.ExpedienteID.Filter =  new DSFilter(filtroxExpediente);
				//ecRead.Dat.ID.Order = 1;
				
				/* [18.04.2007]- BUG#119
				 * Se debe ordenar primero por ExpedienteID*/
				ecRead.Dat.ExpedienteID.Order = 1;
				ecRead.Dat.ID.Order = 2;
				
				ecRead.Adapter.ReadAll(5000);
				int expedienteAnteriorID = ecRead.Dat.ExpedienteID.AsInt;
				for (ecRead.GoTop(); !ecRead.EOF; ecRead.Skip()) {
					//Solo se recuperan los campos del primer expediente
					if (expedienteAnteriorID != ecRead.Dat.ExpedienteID.AsInt) {
						break;
					}

					if ( (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ACTUAL_NOMBRE) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ACTUAL_DIR) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ACTUAL_PAIS) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ANTERIOR_NOMBRE) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ANTERIOR_DIR) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ANTERIOR_PAIS) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.PROP_ANTERIOR_ID) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.CLI_ANTERIOR_ID) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.POD_ANTERIOR_ID) &
						 (ecRead.Dat.Campo.AsString != GlobalConst.VIGILADA_ANTERIOR) ) {
                        ecOut.NewRow();
						ecOut.Dat.ExpedienteID.Value = ecRead.Dat.ExpedienteID.AsInt;
						ecOut.Dat.Campo.Value = ecRead.Dat.Campo.AsString;
						ecOut.Dat.Valor.Value = ecRead.Dat.Valor.AsString;
						ecOut.PostNewRow();
					}
				}
				#endregion ExpedienteCampo
				
				#region Poder o Propietario Anterior
				for (expe.GoTop();!expe.EOF;expe.Skip()) {
					outDG.vPoderAnterior.NewRow();
					//Nombre del Propietario Anterior
					ecRead.ClearFilter();					
					ecRead.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
					ecRead.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
					ecRead.Adapter.ReadAll();
					outDG.vPoderAnterior.Dat.Denominacion.Value = ecRead.Dat.Valor.AsString;
					//Dirección del Propietario Anterior					
					ecRead.ClearFilter();					
					ecRead.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
					ecRead.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
					ecRead.Adapter.ReadAll();
					outDG.vPoderAnterior.Dat.Domicilio.Value = ecRead.Dat.Valor.AsString;
					//Pais del Propietario Anterior
					ecRead.ClearFilter();					
					ecRead.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
					ecRead.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
					ecRead.Adapter.ReadAll();
					outDG.vPoderAnterior.Dat.Obs.Value = ecRead.Dat.Valor.AsString;
					outDG.vPoderAnterior.PostNewRow();
				}
				#endregion Poder o Propietario Anterior

				#region Poder o Propietario Actual
				Berke.DG.DBTab.ExpedienteXPoder expeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
				Berke.DG.DBTab.ExpedienteXPropietario expeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
				Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
				Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
				expeXpoder.InitAdapter( db );
				expeXpropietario.InitAdapter( db );
				poder.InitAdapter( db );
				propietario.InitAdapter( db );
				expe.GoTop();

				expeXpoder.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				expeXpoder.Adapter.ReadAll();
				if (expeXpoder.RowCount > 0) {
					poder.Adapter.ReadByID(expeXpoder.Dat.PoderID.AsInt);
					outDG.vPoderActual.NewRow();
					outDG.vPoderActual.Dat.ID.Value = expeXpoder.Dat.PoderID.AsInt;
					outDG.vPoderActual.Dat.Denominacion.Value = poder.Dat.Denominacion.AsString;
					outDG.vPoderActual.Dat.Domicilio.Value = poder.Dat.Domicilio.AsString;
					outDG.vPoderActual.Dat.Concepto.Value = poder.Dat.Concepto.AsString;					
					outDG.vPoderActual.Dat.Obs.Value = poder.Dat.Obs.AsString;
					outDG.vPoderActual.PostNewRow();
				} else {
					expeXpropietario.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
					expeXpropietario.Adapter.ReadAll();
					if (expeXpropietario.RowCount > 0) {
						propietario.Adapter.ReadByID(expeXpropietario.Dat.PropietarioID.AsInt);
						outDG.vPoderActual.NewRow();
						outDG.vPoderActual.Dat.ID.Value = expeXpropietario.Dat.PropietarioID.AsInt;
						outDG.vPoderActual.Dat.Denominacion.Value = propietario.Dat.Nombre.AsString;
						outDG.vPoderActual.Dat.Domicilio.Value = propietario.Dat.Direccion.AsString;
						outDG.vPoderActual.Dat.Concepto.Value = "Por derecho propio";
						outDG.vPoderActual.Dat.Obs.Value = propietario.Dat.Obs.AsString;
						outDG.vPoderActual.PostNewRow();
					}
				}
				#endregion Poder o Propietario Actual
			}	
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );
			db.CerrarConexion();
		}

	
	} // End Read class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="TV_Read" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.Read,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion Read

#region TV.	FillPoder
namespace Berke.Marcas.BizActions.TV
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

			#region Declaración de objetos locales
			Berke.DG.ViewTab.vHIPoder inTB	= new Berke.DG.ViewTab.vHIPoder( cmd.Request.RawDataSet.Tables[0]);
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.ExpedienteXPoder expeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
			Berke.DG.DBTab.ExpedienteXPropietario expeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
			Berke.DG.DBTab.ExpedienteCampo expeCampo = new Berke.DG.DBTab.ExpedienteCampo();
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo();
			Berke.DG.ViewTab.vPoderExpe outTB	=   new Berke.DG.ViewTab.vPoderExpe();
			expediente.InitAdapter( db );
			expeXpoder.InitAdapter( db );
			expeXpropietario.InitAdapter( db );
			expeCampo.InitAdapter( db );
			poder.InitAdapter( db );
			propietario.InitAdapter( db );
			pais.InitAdapter( db );
			ot.InitAdapter( db );
			outTB.InitAdapter( db );
			ot.Adapter.ReadByID(inTB.Dat.OrdenTrabajoID.AsInt);
			expediente.Dat.OrdenTrabajoID.Filter = inTB.Dat.OrdenTrabajoID.AsInt;
			expediente.Adapter.ReadAll();
			#endregion Declaración de objetos locales

			if (inTB.Dat.Anterior.AsBoolean == true) {
				#region Propietario Anterior
				for (expediente.GoTop(); !expediente.EOF; expediente.Skip()) {
					/* Para el caso de los tramites de Cambio de Nombre y Domicilio se generan dos
					 * expedientes: uno por el cambio de nombre y otro por el cambio de domicilio,
					 * pero en la consulta solo los datos del propietario de uno de ellos. */
					if ( (ot.Dat.TrabajoTipoID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC) |
						( (ot.Dat.TrabajoTipoID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC) &
						(expediente.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE) ) 
						) {
						outTB.NewRow();
						outTB.Dat.ID.Value = expediente.Dat.ID.AsInt;

						//Nombre del Propietario Anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_NOMBRE;
						expeCampo.Adapter.ReadAll();
						if (expeCampo.RowCount > 0) {
							outTB.Dat.Denominacion.Value = expeCampo.Dat.Valor.AsString;
						}

						//Direccion del Propietario Anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_DIR;
						expeCampo.Adapter.ReadAll();
						if (expeCampo.RowCount > 0) {
							outTB.Dat.Domicilio.Value = expeCampo.Dat.Valor.AsString;
						}

						//Pais del Propietario Anterior
						expeCampo.ClearFilter();
						expeCampo.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
						expeCampo.Dat.Campo.Filter = GlobalConst.PROP_ANTERIOR_PAIS;
						expeCampo.Adapter.ReadAll();
						if (expeCampo.RowCount > 0) {
							outTB.Dat.Pais.Value = expeCampo.Dat.Valor.AsString;
						}
						outTB.PostNewRow();
					}
				}
				#endregion Propietario Anterior
			} else {
				#region Propietario Actual
				expeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				expeXpoder.Adapter.ReadAll();
				if (expeXpoder.RowCount > 0) {
					#region Poder
					// Si el expediente tiene poder, completar los datos del poder
					poder.Adapter.ReadByID( expeXpoder.Dat.PoderID.AsInt );
					pais.Dat.idpais.Filter = poder.Dat.PaisID.AsInt;
					pais.Adapter.ReadAll();
					outTB.NewRow();
					outTB.Dat.ID.Value = expeXpoder.Dat.PoderID.AsInt;
					outTB.Dat.Denominacion.Value = poder.Dat.Denominacion.AsString;
					outTB.Dat.Concepto.Value = poder.Dat.Concepto.AsString;
					outTB.Dat.Domicilio.Value = poder.Dat.Domicilio.AsString;
					if ( (poder.Dat.ActaNro.AsString == "") &
						(poder.Dat.ActaAnio.AsString == "") ) {
						outTB.Dat.Acta.Value = "";
					} else {
						outTB.Dat.Acta.Value = poder.Dat.ActaNro.AsString + "/" + poder.Dat.ActaAnio.AsString;
					}
					outTB.Dat.Inscripcion.Value = poder.Dat.Inscripcion.AsString;
					outTB.Dat.Obs.Value = poder.Dat.Obs.AsString;
					outTB.Dat.Pais.Value = pais.Dat.descrip.AsString;
					outTB.PostNewRow();
					#endregion Poder
				} else {
					#region Derecho Propio
					expeXpropietario.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
					expeXpropietario.Adapter.ReadAll();
					propietario.Adapter.ReadByID(expeXpropietario.Dat.PropietarioID.AsInt);
					pais.Dat.idpais.Filter = propietario.Dat.PaisID.AsInt;
					pais.Adapter.ReadAll();
					outTB.NewRow();
					outTB.Dat.ID.Value = expeXpropietario.Dat.PropietarioID.AsInt;
					outTB.Dat.Denominacion.Value = propietario.Dat.Nombre.AsString;
					outTB.Dat.Concepto.Value = "Por derecho propio";
					outTB.Dat.Domicilio.Value = propietario.Dat.Direccion.AsString;
					outTB.Dat.Acta.Value = "";
					outTB.Dat.Inscripcion.Value = "";
					outTB.Dat.Obs.Value = propietario.Dat.Obs.AsString;
					outTB.Dat.Pais.Value = pais.Dat.descrip.AsString;
					outTB.PostNewRow();
					#endregion Derecho Propio
				}
				#endregion Propietario Actual
			}

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End FillPoder class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="TV_FillPoder" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.TV.FillPoder,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion FillPoder