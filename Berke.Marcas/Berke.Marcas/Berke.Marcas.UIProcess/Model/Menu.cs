
//----------------------  MODEL -----------------------
using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Menu
	public class Menu
	{
		#region ReadAsHTMLString
		public static string ReadAsHTMLString( )
		{	Berke.DG.ViewTab.ParamTab param = new Berke.DG.ViewTab.ParamTab();
			DataSet  tmp_InDS	= param.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Menu_ReadAsHTMLString" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB.Dat.Alfa.AsString;
		}
		#endregion ReadAsHTMLString 
	}
	#endregion class Menu

}// end namespace Berke.Marcas.UIProcess.Model