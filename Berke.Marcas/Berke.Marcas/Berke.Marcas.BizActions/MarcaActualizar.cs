
using System;

namespace Berke.Marcas.BizActions.MarcaActualizar
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
//	#region ReadByID
//	public class ReadByID: IAction
//	{	
//		public void Execute( Command cmd ) 
//		{		 
//			Berke.Libs.Base.DSHelpers.ParamDS inDS;
//			Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS outDS;
//			outDS = new Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS();
//			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
//			AccesoDB db		= new AccesoDB();
//
//			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
//			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
//			
//
//
//			TableGateway tb = new TableGateway();
//
//			#region Parametros de Entrada
//
//			// SimpleInt
//			tb.Bind(inDS.Tables["SimpleInt"] );
//			int expedienteID = tb.AsInt("Value");
//			
//
//			#endregion Parametros de Entrada
//
//			#region Leer Expediente
//			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
//			expe.InitAdapter( db );
//			expe.Dat.ID.Filter = expedienteID;
//			expe.Adapter.ReadAll();
//			#endregion Leer Expediente
//
//			#region Leer Marca
//			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
//			marca.InitAdapter( db );
//			marca.Dat.ID.Filter = expe.Dat.MarcaID.AsInt;
//			marca.Adapter.ReadAll();
//			#endregion
//
//			#region Leer Clase
//			Berke.DG.DBTab.Clase clase = new  Berke.DG.DBTab.Clase();
//			clase.InitAdapter( db );
//			clase.Dat.ID.Filter = marca.Dat.ClaseID.AsInt;
//			clase.Adapter.ReadAll();
//			#endregion Leer Clase
//
//			#region Leer MarcaRegRen  ( para numero de acta )
//			Berke.DG.DBTab.MarcaRegRen regRen = new Berke.DG.DBTab.MarcaRegRen();
//			regRen.InitAdapter( db );
//			regRen.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
//			regRen.Adapter.ReadAll();
//			#endregion Leer MarcaRegRen
//
//			#region Expediente_Pertenencia
//			Berke.DG.DBTab.Expediente_Pertenencia expePertencia = new Berke.DG.DBTab.Expediente_Pertenencia();
//			expePertencia.InitAdapter( db );
//			expePertencia.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
//			expePertencia.Adapter.ReadAll();
//			expePertencia.Dat.Fecha.Order = -1; // En orden decreciente
//			expePertencia.Dat.ID.Order = -1; // En orden decreciente de ID. Para tomar el ultimo del dia
//			expePertencia.Sort();
//			#endregion Expediente_Pertenencia
//
//			#region Leer Motivo
//			Berke.DG.DBTab.PertenenciaMotivo motivo = new Berke.DG.DBTab.PertenenciaMotivo();
//			motivo.InitAdapter( db );
//			motivo.Dat.ID.Filter = expePertencia.Dat.PertenenciaMotivoID.AsInt;
//			motivo.Adapter.ReadAll();
//			#endregion Leer Motivo
//
//
//			#region Leer AgenteLocal
//
//			Berke.DG.DBTab.CAgenteLocal agente = new Berke.DG.DBTab.CAgenteLocal();
//			agente.InitAdapter( db );
//			agente.Dat.idagloc.Filter = expe.Dat.AgenteLocalID.AsInt;
//			agente.Adapter.ReadAll();
//			// Entidad
//			Berke.DG.DBTab.CEntidad entidadAgente = new Berke.DG.DBTab.CEntidad();
//			entidadAgente.InitAdapter( db );
//			entidadAgente.Dat.identidad.Filter = agente.Dat.identidad.AsInt;
//			entidadAgente.Adapter.ReadAll();
//
//			#endregion Leer AgenteLocal
//
//			#region Asignacion de RESPONSE
//			
//			// MarcaActualizar
//			tb.Bind(outDS.Tables["MarcaActualizar"] );
//			tb.NewRow();
//			tb.SetValue("ExpedienteID", expe.Dat.ID.AsInt );
//			tb.SetValue("Denominacion", marca.Dat.Denominacion.AsString );
//			tb.SetValue("ClaseID", marca.Dat.ClaseID.AsInt );
//			tb.SetValue("ActaNro",  expe.Dat.ActaNro.AsInt);
//			tb.SetValue("ActaAnio", expe.Dat.ActaAnio.AsInt );
//			tb.SetValue("RegistroNro",regRen.Dat.RegistroNro.AsInt );
//			tb.SetValue("RegistroAnio", regRen.Dat.RegistroAnio.AsInt );
//			tb.SetValue("Fecha", expePertencia.Dat.Fecha.Value );
//			tb.SetValue("MotivoID", expePertencia.Dat.PertenenciaMotivoID.Value );
//			tb.SetValue("AgenteLocalID", expe.Dat.AgenteLocalID.AsInt );
//
//			tb.SetValue("Nuestra"		, expe.Dat.Nuestra.AsBoolean  );
//			tb.SetValue("Vigilada"		, expe.Dat.Vigilada.AsBoolean  );
//			tb.SetValue("Sustituida"	, expe.Dat.Sustituida.AsBoolean  );
//			tb.SetValue("Standby"		, expe.Dat.StandBy.AsBoolean  );
//			tb.SetValue("Clase"			, clase.Dat.DescripBreve.AsString  );
//			tb.SetValue("Motivo"		, motivo.Dat.Descrip.AsString  );
//			tb.SetValue("AgenteLocal"	, entidadAgente.Dat.Nombre.AsString  );
//
//			tb.PostNewRow();
//			
//			#endregion Asignacion de RESPONSE
//
//			outDS.AcceptChanges();
//			cmd.Response = new Response( outDS );     
//			db.CerrarConexion();
//
//		}
//	} // class ReadByID END
//	#endregion ReadByID


	#region Upsert
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS inDS;
			Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS outDS;
			outDS = new Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS();
			inDS  = ( Berke.Marcas.BizDocuments.Marca.MarcaActualizarDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
			TableGateway tb = new TableGateway();
	string bf;
			#region Parametros de Entrada
			// MarcaActualizar
			tb.Bind(inDS.Tables["MarcaActualizar"] );
	
			int       	expedienteID = tb.AsInt("ExpedienteID");
			DateTime  	fecha = tb.AsDateTime("Fecha");
			int       	motivoID = tb.AsInt("MotivoID");			
			
			bool     	nuestra		= tb.AsBoolean("Nuestra");
			bool     	vigilada	= tb.AsBoolean("Vigilada");
			bool     	sustituida	= tb.AsBoolean("Sustituida");
			bool     	standby		= tb.AsBoolean("Standby");
		
			/* No se modifican
			String    	MarcaActualizar_Denominacion = tb.AsString("Denominacion");// no modif
			int       	MarcaActualizar_ClaseID = tb.AsInt("ClaseID");// no modif
			int       	MarcaActualizar_ActaNro = tb.AsInt("ActaNro");// no modif
			int       	MarcaActualizar_ActaAnio = tb.AsInt("ActaAnio");// no modif
			int       	MarcaActualizar_RegistroNro = tb.AsInt("RegistroNro");// no modif
			int       	MarcaActualizar_RegistroAnio = tb.AsInt("RegistroAnio");// no modif
			
			int       	MarcaActualizar_AgenteLocalID = tb.AsInt("AgenteLocalID");// no modif
			*/

			#endregion Parametros de Entrada

			#region Leer Expediente
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			expe.InitAdapter( db );
			expe.Dat.ID.Filter = expedienteID;
			expe.Adapter.ReadAll();
			bf = expe.Adapter.UpdateRow_CommandString();
			if( expe.RowCount < 1 ){throw new Exception("Expediente ID = "+ expedienteID +" No registrado"); }
			expe.GoTop();				// <-
			expe.AcceptAllChanges();	// <- pq Se va a hacer update posteriormente
			#endregion Leer Expediente

			#region Leer Marca
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			marca.InitAdapter( db );
			marca.Dat.ID.Filter = expe.Dat.MarcaID.AsInt;
			marca.Adapter.ReadAll();
			marca.GoTop();					// <-
			marca.Table.AcceptChanges();	// <- pq Se va a hacer update posteriormente
			#endregion Leer Expediente

			#region Leer MarcaRegRen  ( para numero de acta )
			Berke.DG.DBTab.MarcaRegRen regRen = new Berke.DG.DBTab.MarcaRegRen();
			regRen.InitAdapter( db );
			regRen.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			regRen.Adapter.ReadAll();
			#endregion Leer MarcaRegRen

			#region Datos de Funcionario
			Berke.Libs.Base.Acceso acc = new Acceso();

			Berke.DG.DBTab.Usuario ini = new Berke.DG.DBTab.Usuario();
			ini.InitAdapter( db );
			ini.Dat.Usuario.Filter = acc.Usuario;
			ini.Adapter.ReadAll();

			if( ini.RowCount < 1 ){
				throw new Berke.Excep.Biz.UsuarioNoRegistrado( acc.Usuario, " Action: MarcaActualizar.Upsert" );
			}
			#endregion Datos de Funcionario

			Berke.DG.DBTab.Expediente_Pertenencia expePertenencia = new Berke.DG.DBTab.Expediente_Pertenencia();
			expePertenencia.InitAdapter( db );

			expePertenencia.NewRow();
			expePertenencia.Dat.ExpedienteID.Value			= expedienteID;
			expePertenencia.Dat.Fecha.Value					= fecha;
			expePertenencia.Dat.PertenenciaMotivoID.Value	= motivoID;

			expePertenencia.Dat.Nuestra.Value		= nuestra;
			expePertenencia.Dat.StandBy.Value		= standby;
			expePertenencia.Dat.Sustituida.Value	= sustituida;
			expePertenencia.Dat.Vigilada.Value		= vigilada;

			marca.Dat.Nuestra.Value = nuestra;
			expe.Dat.Nuestra.Value = nuestra;
	
			marca.Dat.Vigilada.Value	= vigilada;
			expe.Dat.Vigilada.Value		= vigilada;

			marca.Dat.Sustituida.Value	= sustituida;
			expe.Dat.Sustituida.Value	= sustituida;

			marca.Dat.StandBy.Value		= standby;
			expe.Dat.StandBy.Value		= standby;

			expePertenencia.Dat.FuncionarioID.Value = ini.Dat.ID.AsInt;

			expePertenencia.PostNewRow();

			#region Actualizar la BD
			db.IniciarTransaccion();
		 
			bf = expePertenencia.Adapter.InsertRow_CommandString();
			bf = expe.Adapter.UpdateRow_CommandString();
			bf = marca.Adapter.UpdateRow_CommandString();

			expePertenencia.Adapter.InsertRow();
			expe.Adapter.UpdateRow();
			marca.Adapter.UpdateRow();

			db.Commit();

			#endregion Actualizar la BD

//			expePertenencia.Dat.Obs.Value =  TODO: Ver de donde se obtiene
																					   


			#region Asignacion de RESPONSE
			
			// MarcaActualizar
			tb.Bind(outDS.Tables["MarcaActualizar"] );
	
			tb.NewRow();
			tb.SetValue("ExpedienteID",		expedienteID						);
			tb.SetValue("Denominacion",		marca.Dat.Denominacion.AsString		);
			tb.SetValue("ClaseID",			marca.Dat.ClaseID.AsInt				);
			tb.SetValue("ActaNro",			expe.Dat.ActaNro.AsInt				);
			tb.SetValue("ActaAnio",			expe.Dat.ActaAnio.AsInt				);
			tb.SetValue("RegistroNro",		regRen.Dat.RegistroNro.AsInt		);
			tb.SetValue("RegistroAnio",		regRen.Dat.RegistroAnio.AsInt		);
			tb.SetValue("Fecha",			fecha								);
			tb.SetValue("MotivoID",			motivoID							);
			tb.SetValue("AgenteLocalID",	expe.Dat.AgenteLocalID.AsInt		);
			tb.PostNewRow();
	
			
			#endregion Asignacion de RESPONSE

			outDS.AcceptChanges();
			cmd.Response = new Response( outDS );        
			db.CerrarConexion();

		}
	} // class Upsert END

	#endregion Upsert

	#region ClienteUpdate
	public class ClienteUpdate: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Marcas.BizDocuments.Marca.ExpedienteListDS inDS;
			inDS  = ( Berke.Marcas.BizDocuments.Marca.ExpedienteListDS ) cmd.Request.RawDataSet;

			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
			
			Berke.Libs.Base.DSHelpers.DSTab tbExpeList = new DSTab(inDS.Tables["ExpedienteList"] );
			Berke.Libs.Base.DSHelpers.DSTab tbCliente  = new DSTab(inDS.Tables["Cliente"] );

			#region Parametros de Entrada
			// Cliente
			
			int newClienteID = tbCliente.AsInt("ClienteID");

			#endregion Parametros de Entrada

			#region  Datos de Funcionario
			Acceso acc = new Acceso();
			int funcionarioID;
			Berke.DG.DBTab.Usuario inicial = new Berke.DG.DBTab.Usuario();
			inicial.InitAdapter( db );
	
			inicial.Dat.Usuario.Filter = acc.Usuario;
			
			inicial.Adapter.ReadAll();
			funcionarioID = inicial.Dat.ID.AsInt;

			#endregion Datos de Funcionario

			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			marca.InitAdapter( db );

			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			expe.InitAdapter( db );
			
			db.IniciarTransaccion();
			for( tbExpeList.GoTop(); !tbExpeList.EOF; tbExpeList.Skip() ){
				if( !tbExpeList.AsBoolean("Seleccionado" )) continue;
	
				int marcaID =  tbExpeList.AsInt("MarcaID");

				marca.Dat.ID.Filter = marcaID;
				marca.Adapter.ReadAll();
	
				if( marca.RowCount != 1 ) throw new Exception("La marca ID=" + marcaID + " no existe");
			
				string tmp = marca.Dat.Denominacion.AsString;
	
				int clienteAct = marca.Dat.ClienteID.AsInt;

				#region Actualizar Marca
				marca.Edit();
				marca.Dat.ClienteID.Value = newClienteID;
				marca.PostEdit();
				marca.Adapter.UpdateRow();
				#endregion Actualizar Marca

				#region  Actualizar Expediente
				int expeID =  tbExpeList.AsInt("ExpedienteID");

				expe.Dat.ID.Filter = expeID;
				expe.Adapter.ReadAll();
				if( expe.RowCount != 1 ) throw new Exception("El expediente ID=" + expeID + " no existe");
				expe.Edit();
				expe.Dat.ClienteID.Value = newClienteID;
				expe.PostEdit();
				expe.Adapter.UpdateRow();
				#endregion  Actualizar Expediente
				
				#region Actualizar histórico

				Berke.DG.DBTab.Expediente_ClienteCambio historico = new Berke.DG.DBTab.Expediente_ClienteCambio();
				historico.InitAdapter( db );
				historico.NewRow();
				historico.Dat.ExpedienteID.Value = expeID;
				historico.Dat.ClienteAntID.Value = clienteAct;
				historico.Dat.CambioFecha.Value  = DateTime.Today;
				historico.Dat.FuncionarioID.Value= funcionarioID;
				historico.PostNewRow();
				historico.Adapter.InsertRow();
				#endregion Actualizar histórico

			}

			#region Elimina los procesados
			for( tbExpeList.GoTop(); !tbExpeList.EOF; tbExpeList.Skip() )
			{
				if( tbExpeList.AsBoolean("Seleccionado" ))
				{
						tbExpeList.Delete();
				};
			}
			#endregion Elimina los procesados

			db.Commit();
		
			inDS.AcceptChanges();
			cmd.Response = new Response( inDS );           	
			db.CerrarConexion();

		}
	} // class ClienteUpdate END

	#endregion ClienteUpdate

 
} // namespace Berke.Marcas.BizActions.MarcaActualizar END




