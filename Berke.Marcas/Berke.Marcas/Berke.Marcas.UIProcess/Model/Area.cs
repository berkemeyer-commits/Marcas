using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Area.
	/// </summary>
	/// 
	
	public class Area
	{
		

		#region ReadList
		public static Berke.DG.DBTab.CArea ReadList( Berke.DG.DBTab.CArea inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Area_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.CArea outTB	=  new Berke.DG.DBTab.CArea( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect(  )
		{
			Berke.DG.ViewTab.ListTab result = new Berke.DG.ViewTab.ListTab();
			Berke.DG.DBTab.CArea inTB = new Berke.DG.DBTab.CArea();
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Area_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.CArea outTB	=  new Berke.DG.DBTab.CArea( tmp_OutDS.Tables[0] );

			for( outTB.GoTop(); ! outTB.EOF; outTB.Skip() )
			{
				result.NewRow();
				result.Dat.ID.Value = outTB.Dat.idarea.AsInt;
				result.Dat.Descrip.Value = outTB.Dat.descrip.AsString;
				result.PostNewRow();
			}

			return result;
		}
		#endregion ReadForSelect


	}
	
}
