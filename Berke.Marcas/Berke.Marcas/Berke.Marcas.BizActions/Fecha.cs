#region Fecha.	SumarPlazo
namespace Berke.Marcas.BizActions.Fecha
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizActions;
	public class SumarPlazo: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			#region Parametros
			int			plazo			= inTB.Dat.Entero	.AsInt;
			DateTime	fechaInicial	= inTB.Dat.Fecha	.AsDateTime;
			
			inTB.Skip();

			int unidadID	= inTB.Dat.Entero.AsInt;
			#endregion
			
			#region Asigacion de Valores de Salida
		
			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Fecha	.Value = Lib.FechaMasPlazo(fechaInicial, plazo, unidadID, db ); 
			outTB.PostNewRow(); 

			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}

	
	} // End SumarPlazo class


}// end namespace 


#endregion SumarPlazo