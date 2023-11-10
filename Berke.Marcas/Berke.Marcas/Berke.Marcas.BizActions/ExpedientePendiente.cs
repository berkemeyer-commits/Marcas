
using System;

namespace Berke.Marcas.BizActions.ExpedientePendiente
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
	#region ReadList
	public class ReadList: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.DSHelpers.ParamDS inDS;
			Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS outDS;
			outDS = new Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS();
			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();
			Berke.Libs.Base.DSHelpers.DSTab tb = new DSTab();

			#region Parametros de Entrada

			tb.Bind(inDS.Tables["SimpleInt"] );
			int  expedienteID = tb.AsInt("Value");

			#endregion Parametros de Entrada

			Berke.DG.DBTab.Expediente_Pendiente expePend = new Berke.DG.DBTab.Expediente_Pendiente();
			expePend.InitAdapter( db );
			expePend.Dat.ExpedienteID.Filter = expedienteID;
			expePend.Adapter.ReadAll();

			#region Asignacion de RESPONSE
		
			Berke.DG.ViewTab.vFuncionario fun = new Berke.DG.ViewTab.vFuncionario();
			fun.InitAdapter( db );

			Berke.DG.DBTab.Pendiente pend = new Berke.DG.DBTab.Pendiente();
			pend.InitAdapter( db );

			Berke.DG.ViewTab.vBoletin bol = new Berke.DG.ViewTab.vBoletin();
			bol.InitAdapter( db );

			// Expediente_Pendiente
			tb.Bind(outDS.Tables["Expediente_Pendiente"] );
			for( expePend.Go(0);! expePend.EOF; expePend.Skip() ){
				#region Datos de Funcionario
	
				fun.Dat.ID.Filter = expePend.Dat.FuncionarioID.AsInt;
				fun.Adapter.ReadAll();
				#endregion Datos de Funcionario

				#region Pendiente
				pend.Dat.ID.Filter = expePend.Dat.PendienteID.AsInt;
				pend.Adapter.ReadAll();
				#endregion Pendiente

				#region Boletin
				//bol.Dat.BoletinDetalleID.Filter = expePend.Dat.BoletinDetalleID.AsInt;
				bol.Adapter.ReadAll();
				#endregion Boletin

				tb.NewRow();
				tb.Dat["ID"]			= expePend.Dat.ID.AsInt;
				tb.Dat["ExpedienteID"]	= expePend.Dat.ExpedienteID.AsInt;
				tb.Dat["PendienteID"]	= expePend.Dat.PendienteID.AsInt;
				tb.Dat["Pendiente"]		= pend.Dat.Descrip.AsString;
				tb.Dat["Fecha"]			= expePend.Dat.Fecha.Value;
				tb.Dat["Funcionario"]	= fun.Dat.Funcionario.AsString;
				tb.Dat["Obs"]			= expePend.Dat.Obs.Value;
				tb.Dat["ProximaFecha"]	= expePend.Dat.ProximaFecha.Value;
				tb.Dat["Concluido"]		= expePend.Dat.Concluido.Value;
				//tb.Dat["Boletin"]		= bol.Dat.BoletinNro.AsString + "/" + bol.Dat.BoletinAnio.AsString;
				//tb.Dat["BoletinDetalleID"]		= bol.Dat.BoletinDetalleID.Value;
				tb.PostNewRow();
			}
	
			#endregion Asignacion de RESPONSE

			outDS.AcceptChanges();
			cmd.Response = new Response( outDS );   
			db.CerrarConexion();
	
		}
	} // class ReadList END
	#endregion ReadList

	#region Upsert
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS inDS;
//			Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS outDS;
//			outDS = new Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS();
			inDS  = ( Berke.Marcas.BizDocuments.Marca.ExpedientePendienteDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();
	
			Berke.Libs.Base.DSHelpers.DSTab tb = new DSTab();

			#region Datos de Funcionario
			Berke.Libs.Base.Acceso acc = new Acceso();

			Berke.DG.DBTab.Usuario ini = new Berke.DG.DBTab.Usuario();
			ini.InitAdapter( db );
			ini.Dat.Usuario.Filter = acc.Usuario;
			ini.Adapter.ReadAll();

			if( ini.RowCount < 1 )
			{
				throw new Berke.Excep.Biz.UsuarioNoRegistrado( acc.Usuario, " Action: MarcaActualizar.Upsert" );
			}
			#endregion Datos de Funcionario

	
			tb.Bind(inDS.Tables["Expediente_Pendiente"] );

			Berke.DG.DBTab.Expediente_Pendiente expePend = new Berke.DG.DBTab.Expediente_Pendiente();
			expePend.InitAdapter( db );
			
			bool modif = false;
			db.IniciarTransaccion();
			for( tb.Go(0); ! tb.EOF; tb.Skip() ){
				#region Insertar
				if( tb.IsRowAdded ){
					modif = true;
					expePend.NewRow();
//					expePend.Dat.ID.Value				= tb.Dat["ID"];
					expePend.Dat.ExpedienteID.Value		= tb.Dat["ExpedienteID"];
					expePend.Dat.PendienteID.Value		= tb.Dat["PendienteID"];
			
					expePend.Dat.Fecha.Value			= tb.Dat["Fecha"];
					expePend.Dat.FuncionarioID.Value	= ini.Dat.ID.AsInt;
					expePend.Dat.Obs.Value				= tb.Dat["Obs"];
					expePend.Dat.ProximaFecha.Value		= tb.Dat["ProximaFecha"];
					expePend.Dat.Concluido.Value		= tb.Dat["Concluido"];
					expePend.Dat.BoletinDetalleID.Value	= tb.Dat["BoletinDetalleID"];
					expePend.PostNewRow();
					expePend.Adapter.InsertRow();
				}
				#endregion Insertar

				#region Modificar
				if( tb.IsRowModified ) {
					modif = true;

					expePend.Dat.ID.Filter	= tb.Dat["ID"];
					expePend.Adapter.ReadAll();

					expePend.Edit();
					expePend.Dat.ID.Value				= tb.Dat["ID"];
					expePend.Dat.ExpedienteID.Value		= tb.Dat["ExpedienteID"];
					expePend.Dat.PendienteID.Value		= tb.Dat["PendienteID"];
			
					expePend.Dat.Fecha.Value			= tb.Dat["Fecha"];
//					expePend.Dat.FuncionarioID.Value	= tb.Dat["Funcionario"];
					expePend.Dat.Obs.Value				= tb.Dat["Obs"];
					expePend.Dat.ProximaFecha.Value		= tb.Dat["ProximaFecha"];
					expePend.Dat.Concluido.Value		= tb.Dat["Concluido"];
					expePend.Dat.BoletinDetalleID.Value	= tb.Dat["BoletinDetalleID"];
					expePend.PostEdit();

					expePend.Adapter.UpdateRow();

				}
				#endregion Modificar


				#region Eliminar
				if( tb.IsRowDeleted ) {
					modif = true;

					expePend.Dat.ID.Filter	= tb.Dat["ID"];
					expePend.Adapter.ReadAll();

					expePend.Adapter.DeleteRow();

				}
				#endregion Eliminar


			}
			if ( modif ) db.Commit();
	
			inDS.AcceptChanges();
			cmd.Response = new Response( inDS );           
			db.CerrarConexion();

		}
	} // class Upsert END
	#endregion Upsert 

} // namespace Berke.Marcas.BizActions.ExpedientePendiente END




