using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Tramite.
	/// </summary>
	public class Tramite
	{
	
		#region ReadList
		public static Berke.DG.DBTab.Tramite ReadList( Berke.DG.DBTab.Tramite inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Tramite_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Tramite outTB	=  new Berke.DG.DBTab.Tramite( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect()
		{
			Berke.DG.DBTab.Tramite inTB = new Berke.DG.DBTab.Tramite();
			inTB.DisableConstraints();
			inTB.NewRow();
			inTB.Dat.ProcesoID.Value = 1; // Proceso de marca
			inTB.PostNewRow();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Tramite_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Tramite trm	=  new Berke.DG.DBTab.Tramite( tmp_OutDS.Tables[0] );

			Berke.DG.ViewTab.ListTab ret = new Berke.DG.ViewTab.ListTab();
			for( trm.GoTop(); ! trm.EOF; trm.Skip())
			{
				ret.NewRow();
				ret.Dat.ID.Value = trm.Dat.ID.AsInt;
				ret.Dat.Descrip.Value = trm.Dat.Descrip.AsString;
				ret.PostNewRow();
			}


			return ret;
		}
		#endregion ReadForSelect
	
//		#region ReadForSelect   Sin Argumento
//		public static Berke.DG.ViewTab.ListTab ReadForSelect( )
//		{
//			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
//			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
//			DataSet  tmp_OutDS;
//
//			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Tramite_ReadForSelect" , tmp_InDS );
//			
//			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );
//
//			return outTB;
//		}
//		#endregion ReadForSelect

		#region ReadForSelect   Con Argumento
//		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
//		{
//			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
//			DataSet  tmp_OutDS;
//
//			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Tramite_ReadForSelect" , tmp_InDS );
//			
//			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );
//
//			return outTB;
//		}
		#endregion ReadForSelect







	}
}
