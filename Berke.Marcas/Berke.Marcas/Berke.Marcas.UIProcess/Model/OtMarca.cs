using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for OtMarca.
	/// </summary>
	public class OtMarca
	{
	

		#region ReadList
		public static Berke.DG.ViewTab.vOtMarca ReadList( Berke.DG.ViewTab.vOtMarca inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "OtMarca_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vOtMarca outTB	=  new Berke.DG.ViewTab.vOtMarca( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList



	}
}
