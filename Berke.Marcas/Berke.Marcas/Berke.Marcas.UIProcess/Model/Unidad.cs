using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{


	public class Unidad
	{
		#region ReadByID
		public static Berke.DG.DBTab.Unidad ReadByID( int unidadID )
		{
			Berke.DG.DBTab.Unidad inTB = new Berke.DG.DBTab.Unidad();
			inTB.DisableConstraints();

			inTB.NewRow();
			inTB.Dat.ID.Value = unidadID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Unidad_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Unidad outTB	=  new Berke.DG.DBTab.Unidad( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID

		#region Read
		public static Berke.DG.DBTab.Unidad Read( Berke.DG.DBTab.Unidad inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Unidad_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Unidad outTB	=  new Berke.DG.DBTab.Unidad( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read

		#region ReadForSelect con parametros
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Unidad_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion 

		#region ReadForSelect Sin parametros
		public static Berke.DG.ViewTab.ListTab ReadForSelect()
		{	
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Unidad_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}

		#endregion ReadForSelect
	
	}


}// end namespace Berke.Marcas.UIProcess.Model
