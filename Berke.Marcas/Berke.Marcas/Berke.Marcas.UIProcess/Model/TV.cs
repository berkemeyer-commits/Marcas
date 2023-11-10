using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class TV
	public class TV
	{
		#region Delete
		public static Berke.DG.ViewTab.ParamTab Delete( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_Delete" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Delete 

		#region Upsert
		public static Berke.DG.ViewTab.ParamTab Upsert( Berke.DG.TVDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_Upsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Upsert 

		#region FillPoderAnterior
		public static Berke.DG.ViewTab.vPoderAnterior FillPoderAnterior( Berke.DG.TVDG inDG)
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_FillPoderAnterior" , tmp_InDS );
			
			Berke.DG.ViewTab.vPoderAnterior outTB	=  new Berke.DG.ViewTab.vPoderAnterior( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion FillPoderAnterior 

		#region FillPoderActual
		public static Berke.DG.ViewTab.vPoderActual FillPoderActual( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_FillPoderActual" , tmp_InDS );
			
			Berke.DG.ViewTab.vPoderActual outTB	=  new Berke.DG.ViewTab.vPoderActual( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion FillPoderActual 

		#region Read
		public static Berke.DG.TVDG Read( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_Read" , tmp_InDS );
			
			Berke.DG.TVDG outDG	=  new Berke.DG.TVDG( tmp_OutDS );

			return outDG;
		}
		#endregion Read 

		#region FillPoder
		public static Berke.DG.ViewTab.vPoderExpe FillPoder( Berke.DG.ViewTab.vHIPoder inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TV_FillPoder" , tmp_InDS );
			
			Berke.DG.ViewTab.vPoderExpe outTB	=  new Berke.DG.ViewTab.vPoderExpe( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion FillPoder
	}
	#endregion class TV

}// end namespace Berke.Marcas.UIProcess.Model