using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class ExpeMarca
	public class ExpeMarca
	{
		#region ReadByID
		public static Berke.DG.ViewTab.vExpeMarca ReadByID( int expedienteID  )
		{
			Berke.DG.ViewTab.vExpeMarca expe = new Berke.DG.ViewTab.vExpeMarca();
			expe.NewRow();
			expe.Dat.ExpedienteID.Value = expedienteID;
			expe.PostNewRow();

			DataSet  tmp_InDS = expe.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeMarca_ReadList" , expe.AsDataSet() );
			
			Berke.DG.ViewTab.vExpeMarca outTB	=  new Berke.DG.ViewTab.vExpeMarca( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID 

		#region ReadList
		public static Berke.DG.ViewTab.vExpeMarca ReadList( Berke.DG.ViewTab.vExpeMarca inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeMarca_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vExpeMarca outTB	=  new Berke.DG.ViewTab.vExpeMarca( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList 


		#region CambioSitByID
		public static Berke.DG.ExpeMarCambioSitDG CambioSitByID( int ExpedienteID )
		{

			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = ExpedienteID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeMarca_CambioSitByID" , tmp_InDS );
			
			Berke.DG.ExpeMarCambioSitDG outDG	=  new Berke.DG.ExpeMarCambioSitDG( tmp_OutDS );

			return outDG;
		}
		#endregion CambioSitByID 
	
	}
	#endregion class ExpeMarca

}// end namespace Berke.Marcas.UIProcess.Model
