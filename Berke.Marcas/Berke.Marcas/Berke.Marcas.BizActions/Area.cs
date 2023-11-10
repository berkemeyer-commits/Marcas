
#region Area.	ReadList
namespace Berke.Marcas.BizActions.Area
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
		
			Berke.DG.DBTab.CArea inTB	= new Berke.DG.DBTab.CArea( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		

			#endregion Parametros

			Berke.DG.DBTab.CArea outTB = new Berke.DG.DBTab.CArea( db );

			#region Filtros


	
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



/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.Area


*/



#endregion  Area.	ReadList