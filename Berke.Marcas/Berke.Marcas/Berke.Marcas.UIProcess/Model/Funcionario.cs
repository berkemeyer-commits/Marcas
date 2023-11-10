using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Funcionario.
	/// </summary>
	public class Funcionario
	{
		#region ReadByID  

		public static Berke.DG.ViewTab.vFuncionario ReadByID( int funcionarioID )
		{
			Berke.DG.ViewTab.vFuncionario inTB = new Berke.DG.ViewTab.vFuncionario();
			inTB.NewRow();
			inTB.Dat.ID.Value = funcionarioID;
			inTB.PostNewRow();
						
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Funcionario_ReadList_M" , tmp_InDS );
			
			Berke.DG.ViewTab.vFuncionario outTB	=  new Berke.DG.ViewTab.vFuncionario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID

		#region ReadByUserName  

		public static Berke.DG.ViewTab.vFuncionario ReadByUserName( string userName  )
		{
			Berke.DG.ViewTab.vFuncionario inTB = new Berke.DG.ViewTab.vFuncionario();
			inTB.NewRow();
			inTB.Dat.Usuario.Value = userName;
			
			inTB.PostNewRow();
						
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Funcionario_ReadList_M" , tmp_InDS );
			
			Berke.DG.ViewTab.vFuncionario outTB	=  new Berke.DG.ViewTab.vFuncionario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByUserName

		#region ReadList  

		public static Berke.DG.ViewTab.vFuncionario ReadList( Berke.DG.ViewTab.vFuncionario inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Funcionario_ReadList_M" , tmp_InDS );
			
			Berke.DG.ViewTab.vFuncionario outTB	=  new Berke.DG.ViewTab.vFuncionario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region ReadForSelect   Sin Argumento
		public static Berke.DG.ViewTab.ListTab ReadForSelect( )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Funcionario_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect

		#region ReadForSelect   Con Argumento
		public static Berke.DG.ViewTab.ListTab ReadForSelect( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Funcionario_ReadForSelect" , tmp_InDS );
			
			Berke.DG.ViewTab.ListTab outTB	=  new Berke.DG.ViewTab.ListTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadForSelect

	}
}
