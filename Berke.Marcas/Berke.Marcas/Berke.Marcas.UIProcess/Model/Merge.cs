using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Merge
	public class Merge
	{
		#region Fill
		public static Berke.DG.MergeDG Fill( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Merge_Fill" , tmp_InDS );
			
			Berke.DG.MergeDG outDG	=  new Berke.DG.MergeDG( tmp_OutDS );

			return outDG;
		}
		#endregion Fill 
	
	}
	#endregion class Merge

}// end namespace Berke.Marcas.UIProcess.Model
