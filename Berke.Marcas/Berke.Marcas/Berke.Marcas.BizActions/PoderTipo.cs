using System;

namespace Berke.Marcas.BizActions.PoderTipo
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
	public class ReadByPattern: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.DSHelpers.ParamDS inDS;
			Berke.Libs.Base.DSHelpers.SimpleEntryDS outDS;
			outDS = new Berke.Libs.Base.DSHelpers.SimpleEntryDS();
			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();
			TableGateway tb = new TableGateway();

			#region Parametros de Entrada
			// SimpleString
			tb.Bind(inDS.Tables["SimpleString"] );
			String    	SimpleString_Value = tb.AsString("Value");

			#endregion Parametros de Entrada

			#region Asignacion de RESPONSE

			Berke.DG.DBTab.PoderTipo poderTipo = new Berke.DG.DBTab.PoderTipo();
			poderTipo.InitAdapter(db);
			poderTipo.Dat.Descrip.Filter = ObjConvert.GetSqlPattern(SimpleString_Value);
			poderTipo.Adapter.ReadAll();

			tb.Bind(outDS.Tables["SimpleEntry"] );

			for (poderTipo.GoTop(); !poderTipo.EOF; poderTipo.Skip())
			{
				tb.NewRow();
				tb.SetValue("ID", poderTipo.Dat.ID.AsInt );
				tb.SetValue("Descr", poderTipo.Dat.Descrip.AsString );
				tb.PostNewRow();
			}
			
			#endregion Asignacion de RESPONSE


			cmd.Response = new Response( outDS );     
			db.CerrarConexion();

		}
	} // class ReadByPattern END

 
} // namespace Berke.Marcas.BizActions.PoderTipo END