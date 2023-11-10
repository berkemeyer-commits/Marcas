using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Propietario
	public class Propietario
	{
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Propietario_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 


		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( string pattern, bool ntra )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Alfa.Value = pattern;
			inTB.Dat.Logico.Value = ntra;
			inTB.PostNewRow();


			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Propietario_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 



		#region ReadList
		public static Berke.DG.ViewTab.vPropietario ReadList( Berke.DG.ViewTab.vPropietario inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Propietario_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vPropietario outTB	=  new Berke.DG.ViewTab.vPropietario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList
	
	}
	#endregion class Propietario

}// end namespace Berke.Marcas.UIProcess.Model

