using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Documento.
	/// </summary>
	public class Documento
	{
	
		#region ReadList
		public static Berke.DG.ViewTab.vDocum ReadList( Berke.DG.ViewTab.vDocum inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Documento_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vDocum outTB	=  new Berke.DG.ViewTab.vDocum( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList


	}
}
