using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class TramiteSitSgte
	public class TramiteSitSgte
	{
		#region ReadForSelect 
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSitSgte_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 
	
		#region ReadForSelect 
		public static Berke.DG.ViewTab.ListTab ReadForSelect( int TramiteSitID )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = TramiteSitID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSitSgte_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 

		#region ReadList
		public static Berke.DG.ViewTab.vSituacionSigte ReadList( Berke.DG.ViewTab.vSituacionSigte inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "SituacionSiguiente_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vSituacionSigte outTB	=  new Berke.DG.ViewTab.vSituacionSigte( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

	}
	#endregion class TramiteSitSgte

}// end namespace Berke.Marcas.UIProcess.Model
