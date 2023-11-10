using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Proceso

	public class Proceso
	{

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( )
		{
			 Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Proceso_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 

	
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Proceso_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 

		#region ReadList
		public static Berke.DG.DBTab.Proceso  ReadList( Berke.DG.DBTab.Proceso inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Proceso_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Proceso outTB	=  new Berke.DG.DBTab.Proceso( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList 
	
	}
	#endregion class Proceso

}// end namespace Berke.Marcas.UIProcess.Model
