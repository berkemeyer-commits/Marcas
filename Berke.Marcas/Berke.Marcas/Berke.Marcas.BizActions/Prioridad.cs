#region Prioridad.	ReadList
namespace Berke.Marcas.BizActions.Prioridad
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
		
			Berke.DG.DBTab.Prioridad inTB	= new Berke.DG.DBTab.Prioridad( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		

			#endregion Parametros

			Berke.DG.DBTab.Prioridad outTB = new Berke.DG.DBTab.Prioridad( db );

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




#endregion  Prioridad.	ReadList