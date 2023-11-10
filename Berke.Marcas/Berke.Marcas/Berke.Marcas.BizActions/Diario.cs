using System;

#region Diario.	Read
namespace Berke.Marcas.BizActions.Diario
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
		
			Berke.DG.DBTab.Diario inTB	= new Berke.DG.DBTab.Diario( cmd.Request.RawDataSet.Tables[0]);
		
			#region Asigacion de Valores de Salida
		
			// Diario
			Berke.DG.DBTab.Diario outTB	=   new Berke.DG.DBTab.Diario();

			
			outTB.Dat.ID			.Filter = inTB.Dat.ID.Value;   //int PK  Oblig.
			outTB.Dat.Descrip		.Filter = ObjConvert.GetSqlPattern( inTB.Dat.Descrip.AsString );   //nvarchar Oblig.
			outTB.Dat.Abrev			.Filter = inTB.Dat.Abrev.Value;   //nvarchar

		 
		
			outTB.InitAdapter( db );
			outTB.Adapter.ReadAll();
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
		}

	
	} // End Read class


}// end namespace 


#endregion Read


namespace Berke.Marcas.BizActions.Diario
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
			String pattern = tb.AsString("Value");

			#endregion Parametros de Entrada

			#region Asignacion de RESPONSE

			// SimpleEntry
			Berke.DG.DBTab.Diario diario = new Berke.DG.DBTab.Diario( db );
			diario.Dat.Descrip.Filter	=  ObjConvert.GetSqlPattern	( pattern );
			diario.Dat.Descrip.Order = 1;
			diario.Adapter.ReadAll( 50 );

			tb.Bind(outDS.Tables["SimpleEntry"] );
			for( diario.GoTop() ; ! diario.EOF; diario.Skip()){
				tb.NewRow();
				tb.SetValue("ID", diario.Dat.ID.AsInt );
				tb.SetValue("Descr", diario.Dat.Descrip.AsString );
				tb.PostNewRow();
			}

			#endregion Asignacion de RESPONSE

			db.CerrarConexion();
			cmd.Response = new Response( outDS );           		
		}
	} // class ReadByPattern END

 
} // namespace Berke.Marcas.BizActions.Diario END




