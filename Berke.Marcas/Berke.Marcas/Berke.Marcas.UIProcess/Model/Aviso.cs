using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Aviso
	public class Aviso
	{

		#region ReadList
		public static Berke.DG.ViewTab.vAviso ReadList( Berke.DG.ViewTab.vAviso inTB )
		{
//			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_InDS = inTB.AsDataSet();

			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Aviso_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vAviso outTB	=  new Berke.DG.ViewTab.vAviso( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList
	
		#region Read
		public static Berke.DG.DBTab.Aviso Read( Berke.DG.DBTab.Aviso inTB )
		{

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Aviso_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Aviso outTB	=  new Berke.DG.DBTab.Aviso( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read

		#region ReadByID
		public static Berke.DG.DBTab.Aviso ReadByID( int ID )
		{
			Berke.DG.DBTab.Aviso inTB = new Berke.DG.DBTab.Aviso();
			inTB.NewRow();
			inTB.Dat.ID.Value = ID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Aviso_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Aviso outTB	=  new Berke.DG.DBTab.Aviso( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID

	}
	#endregion class Aviso


}// end namespace Berke.Marcas.UIProcess.Model
