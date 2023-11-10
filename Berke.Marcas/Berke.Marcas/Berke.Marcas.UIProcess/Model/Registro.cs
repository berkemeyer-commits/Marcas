using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Registro
	public class Registro
	{
		#region Read
		/*
		public static Berke.DG.RegistroDG Read( Berke.DG.RegistroDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Registro_Read" , tmp_InDS );
			
			Berke.DG.RegistroDG outDG	=  new Berke.DG.RegistroDG( tmp_OutDS );

			return outDG;
		}
		*/
		#endregion Read

		#region Upsert
		public static Berke.DG.ViewTab.ParamTab Upsert( Berke.DG.RegistroDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Registro_Upsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Upsert 

		#region Delete
		public static Berke.DG.ViewTab.ParamTab Delete( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Registro_Delete" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Delete 
	}
	#endregion class Registro

}// end namespace Berke.Marcas.UIProcess.Model