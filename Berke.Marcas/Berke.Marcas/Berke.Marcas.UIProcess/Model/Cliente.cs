using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Cliente
	public class Cliente
	{
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Cliente_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect 

		#region ReadList
		public static Berke.DG.ViewTab.vCliente ReadList( Berke.DG.ViewTab.vCliente inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Cliente_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vCliente outTB	=  new Berke.DG.ViewTab.vCliente( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region ReadByID
		public static Berke.DG.ViewTab.vCliente ReadByID( int ClienteID )
		{
			Berke.DG.ViewTab.vCliente inTB = new Berke.DG.ViewTab.vCliente();
			inTB.NewRow();
			inTB.Dat.ID.Value = ClienteID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Cliente_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vCliente outTB	=  new Berke.DG.ViewTab.vCliente( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID
/*
		#region Read
		public static Berke.DG.ClienteDG Read( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Cliente_Read" , tmp_InDS );
			
			Berke.DG.ClienteDG outDG	=  new Berke.DG.ClienteDG( tmp_OutDS );

			return outDG;
		}
		#endregion Read 
	*/
	}
	#endregion class Cliente

}// end namespace Berke.Marcas.UIProcess.Model
