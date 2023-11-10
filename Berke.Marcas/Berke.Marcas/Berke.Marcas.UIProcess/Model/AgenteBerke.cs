using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class AgenteBerke
	public class AgenteBerke
	{
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect(  )
		{
			DataSet  tmp_InDS	= new DataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "AgenteBerke_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 
	
	}
	#endregion class AgenteBerke

}// end namespace Berke.Marcas.UIProcess.Model