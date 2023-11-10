using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model
{
	using UIPModel = UIProcess.Model;


	/// <summary>
	/// Summary description for ExpeMarcaCambioSit.
	/// </summary>
	public class ExpeMarcaCambioSit
	{
		#region ReadList
		public static Berke.DG.ViewTab.vMarcaCambioSit ReadList( Berke.DG.ViewTab.vMarcaCambioSit inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;
			
			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeMarcaCambioSit_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vMarcaCambioSit outTB	=  new Berke.DG.ViewTab.vMarcaCambioSit( tmp_OutDS.Tables[0] );
			
			return outTB;
		}
		#endregion ReadList
		
	}
}
