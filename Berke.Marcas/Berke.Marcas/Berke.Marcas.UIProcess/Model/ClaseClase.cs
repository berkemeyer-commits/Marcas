using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for ClaseClase.
	/// </summary>
	public class ClaseClase
	{
		//public ClaseClase()
		//{
			#region ReadList
			public static Berke.DG.ViewTab.vClaseClase ReadList( Berke.DG.ViewTab.vClaseClase inTB )
			{
				DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
				DataSet  tmp_OutDS;

				tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ClaseClase_ReadList" , tmp_InDS );
			
				Berke.DG.ViewTab.vClaseClase outTB	=  new Berke.DG.ViewTab.vClaseClase( tmp_OutDS.Tables[0] );

				return outTB;
			}
		#endregion ReadList
		//}

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ClaseClase_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 

	
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ClaseClase_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 
	
	}
}
