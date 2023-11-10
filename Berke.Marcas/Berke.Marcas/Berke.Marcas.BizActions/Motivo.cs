
using System;

namespace Berke.Marcas.BizActions.Motivo
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
			
			TableGateway tb = new TableGateway();

			#region Parametros de Entrada
			// SimpleString
			tb.Bind(inDS.Tables["SimpleString"] );
			String patron = tb.AsString("Value");

			#endregion Parametros de Entrada

			Berke.DG.DBTab.PertenenciaMotivo motivo = new Berke.DG.DBTab.PertenenciaMotivo();
			motivo.InitAdapter( db );
			motivo.Dat.Descrip.Filter = Berke.Libs.Base.ObjConvert.GetSqlPattern( patron );
			motivo.Adapter.ReadAll();

            #region Asignacion de RESPONSE
			
			// SimpleEntry
			tb.Bind(outDS.Tables["SimpleEntry"] );
			for( motivo.GoTop(); ! motivo.EOF; motivo.Skip() ){
				tb.NewRow();
				tb.SetValue("ID",		motivo.Dat.ID.AsInt );
				tb.SetValue("Descr",	motivo.Dat.Descrip.AsString );
				tb.PostNewRow();
			}
			#endregion Asignacion de RESPONSE

			outDS.AcceptChanges();
			cmd.Response = new Response( outDS );       
			db.CerrarConexion();

		}
	} // class ReadByPattern END

 
} // namespace Berke.Marcas.BizActions.Motivo END




