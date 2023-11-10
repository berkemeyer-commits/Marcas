using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for AgenteLocal.
	/// </summary>
	public class AgenteLocal
	{

		#region ReadList
		public static Berke.DG.ViewTab.vAgenteLocal ReadList( Berke.DG.ViewTab.vAgenteLocal inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "AgenteLocal_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vAgenteLocal outTB	=  new Berke.DG.ViewTab.vAgenteLocal( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect(  )
		{
			Berke.DG.ViewTab.vAgenteLocal inTB = new Berke.DG.ViewTab.vAgenteLocal();
			inTB.NewRow();
			inTB.PostNewRow();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "AgenteLocal_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vAgenteLocal vAgente	=  new Berke.DG.ViewTab.vAgenteLocal( tmp_OutDS.Tables[0] );

			Berke.DG.ViewTab.ListTab outTB = new Berke.DG.ViewTab.ListTab();

			for( vAgente.GoTop() ; ! vAgente.EOF; vAgente.Skip() )
			{
				outTB.NewRow();
				outTB.Dat.ID.Value = vAgente.Dat.ID.AsInt;
				outTB.Dat.Descrip.Value	= vAgente.Dat.Nombre.AsString + " Mat.:"+ vAgente.Dat.NroMatricula.AsString;
				outTB.PostNewRow();
			}
			return outTB;
		}
		#endregion ReadForSelect

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB  )
		{
			
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "AgenteLocal_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );
			return outTB;
		}
		#endregion ReadForSelect


	}// end class
}
