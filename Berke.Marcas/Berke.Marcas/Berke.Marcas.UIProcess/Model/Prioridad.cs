using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Prioridad.
	/// </summary>
	public class Prioridad
	{
		
		#region ReadList
		public static Berke.DG.DBTab.Prioridad ReadList( Berke.DG.DBTab.Prioridad inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Prioridad_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Prioridad outTB	=  new Berke.DG.DBTab.Prioridad( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList


		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( )
		{
			Berke.DG.ViewTab.ListTab result = new Berke.DG.ViewTab.ListTab();

			Berke.DG.DBTab.Prioridad inTB = new Berke.DG.DBTab.Prioridad();
		
			DataSet  tmp_InDS	= inTB.AsDataSet();
		
			DataSet tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Prioridad_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Prioridad prioridad	=  new Berke.DG.DBTab.Prioridad( tmp_OutDS.Tables[0] );

			for( prioridad.GoTop(); ! prioridad.EOF; prioridad.Skip() )
			{
				result.NewRow();
				result.Dat.ID.Value = prioridad.Dat.ID.AsInt;
				result.Dat.Descrip.Value = prioridad.Dat.Descrip.AsString;
				result.PostNewRow();
			}

			return result;
		}
		#endregion ReadForSelect

	}
}
