

#region Tramite.	ReadList
namespace Berke.Marcas.BizActions.Tramite
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadList: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.DBTab.Tramite inTB	= new Berke.DG.DBTab.Tramite( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ProcesoID		= inTB.Dat.ProcesoID.Value;
			object ID_min		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.DBTab.Tramite outTB = new Berke.DG.DBTab.Tramite( db );

			#region Filtros


			outTB.Dat.ProcesoID.		Filter = ProcesoID;
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern	( Abrev );
	
			#endregion Filtros
			
			outTB.Adapter.ReadAll( 500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();

		}

	
	} // End ReadList class


}// end namespace 



#endregion  Tramite.	ReadList

//
//#region Tramite.	ReadForSelect
//namespace Berke.Marcas.BizActions.Tramite
//{
//	using System;
//	using Framework.Core;
//	using System.Data;
//	using Libs.Base;
//	using Libs.Base.DSHelpers;
//	
//	public class ReadForSelect: IAction
//	{	
//		public void Execute( Command cmd ) 
//		{		 
//			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
//
//			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
//			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
//			
//		
//			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
//		
//			#region Asignacion de Valores de Salida
//		
//			Berke.DG.ViewTab.vTramiteSit Tramite = new Berke.DG.ViewTab.vTramiteSit( db );
//
//			Tramite.Dat.Descrip	.Filter	= ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
//			Tramite.Dat.ID			.Filter	= inTB.Dat.Entero.Value;
//
//			Tramite.Dat.Descrip.Order = 1;
//
//			Tramite.Adapter.ReadAll();
//
//			// ListTab
//			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();
//
//			for( Tramite.GoTop(); !Tramite.EOF; Tramite.Skip() )
//			{
//				outTB.NewRow(); 
//				outTB.Dat.ID		.Value = Tramite.Dat.ID.AsInt;			//Int32
//				outTB.Dat.Descrip	.Value = Tramite.Dat.Descrip.AsString;   //String
//				outTB.PostNewRow(); 
//			}
//			#endregion 
//
//			DataSet  tmp_OutDS;
//			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
//		
//			cmd.Response = new Response( tmp_OutDS );	
//			db.CerrarConexion();
//
//		}
//
//	
//	} // End ReadForSelect class
//
//
//}// end namespace 
//
//
//#endregion ReadForSelect