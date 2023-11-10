using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for tvCN.
	/// </summary>
	public class tvCN
	{
		#region Upsert
		
		public static Berke.DG.ViewTab.ParamTab Upsert( Berke.DG.TVDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "tvCN_Upsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		
		#endregion Upsert
	}
}
