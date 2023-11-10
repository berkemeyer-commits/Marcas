using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for PertenenciaMotivo.
	/// </summary>
	public class PertenenciaMotivo
	{
		

		#region ReadList
		public static Berke.DG.DBTab.PertenenciaMotivo ReadList( Berke.DG.DBTab.PertenenciaMotivo inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "PertenenciaMotivo_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.PertenenciaMotivo outTB	=  new Berke.DG.DBTab.PertenenciaMotivo( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList


	}
}
