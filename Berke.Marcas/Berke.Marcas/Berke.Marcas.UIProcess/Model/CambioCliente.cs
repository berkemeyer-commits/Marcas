
//----------------------  MODEL -----------------------
using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{


	#region class CambioCliente
	public class CambioCliente
	{
		#region Read
		public static Berke.DG.ViewTab.vMarcaClientePropietario Read( Berke.DG.ViewTab.RegistrosActasClientePropietario inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "CambioCliente_Read" , tmp_InDS );
			
			Berke.DG.ViewTab.vMarcaClientePropietario outTB	=  new Berke.DG.ViewTab.vMarcaClientePropietario( tmp_OutDS.Tables[0] );

			return outTB;
		}

		public static Berke.DG.ViewTab.vMarcaClientePropietarioTVS ReadTVS( Berke.DG.ViewTab.RegistrosActasClientePropietario inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "CambioCliente_ReadTVS" , tmp_InDS );
			
			Berke.DG.ViewTab.vMarcaClientePropietarioTVS outTB	=  new Berke.DG.ViewTab.vMarcaClientePropietarioTVS( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read 

		#region Upsert
		public static Berke.DG.ViewTab.ParamTab Upsert( Berke.DG.ViewTab.vMarcaClientePropietario inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "CambioCliente_Upsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Upsert 
	
		#region UpsertTVS
		public static Berke.DG.ViewTab.ParamTab UpsertTVS( Berke.DG.ViewTab.vMarcaClientePropietarioTVS inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "CambioCliente_UpsertTVS" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion UpsertTVS
	
	}
	#endregion class CambioCliente

}// end namespace Berke.Marcas.UIProcess.Model