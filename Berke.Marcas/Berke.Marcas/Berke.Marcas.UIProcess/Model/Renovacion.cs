using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Renovacion
	public class Renovacion
	{
		#region Read
		public static Berke.DG.RenovacionDG Read( Berke.DG.RenovacionDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_Read" , tmp_InDS );
			
			Berke.DG.RenovacionDG outDG	=  new Berke.DG.RenovacionDG( tmp_OutDS );

			return outDG;
		}
		#endregion Read 

		#region ReadClase
		public static Berke.DG.DBTab.Clase ReadClase( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_ReadClase" , tmp_InDS );
			
			Berke.DG.DBTab.Clase outTB	=  new Berke.DG.DBTab.Clase( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadClase 
		
		#region Fill
		public static Berke.DG.RenovacionDG Fill( Berke.DG.ViewTab.ActaRegistroPoder inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_Fill" , tmp_InDS );
			
			Berke.DG.RenovacionDG outDG	=  new Berke.DG.RenovacionDG( tmp_OutDS );

			return outDG;
		}
		#endregion Fill 	

		#region FillPoder
		public static Berke.DG.RenovacionDG FillPoder( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_FillPoder" , tmp_InDS );
			
			Berke.DG.RenovacionDG outDG	=  new Berke.DG.RenovacionDG( tmp_OutDS );

			return outDG;
		}
		#endregion FillPoder 

		#region FillPropietario
		public static Berke.DG.ViewTab.vPropietario FillPropietario( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_FillPropietario" , tmp_InDS );
			
			Berke.DG.ViewTab.vPropietario outTB	=  new Berke.DG.ViewTab.vPropietario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion FillPropietario 

		#region FillLimitaciones
		public static Berke.DG.ViewTab.vRenovacionLimitadas FillLimitaciones( Berke.DG.ViewTab.ParamTab inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_FillLimitaciones" , tmp_InDS );
			
			Berke.DG.ViewTab.vRenovacionLimitadas outTB	=  new Berke.DG.ViewTab.vRenovacionLimitadas( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion FillLimitaciones 

		#region Upsert
		public static Berke.DG.ViewTab.ParamTab Upsert( Berke.DG.RenovacionDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_Upsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Upsert 

		#region Delete
		public static Berke.DG.ViewTab.ParamTab Delete( Berke.DG.RenovacionDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_Delete" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Delete

		#region Upsert Registro en Aduana
		public static Berke.DG.ViewTab.ParamTab UpsertRegAduana( Berke.DG.RenovacionDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_RegAduanaUpsert" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
			
		}
		#endregion Upsert Registro en Aduana

		#region Delete Registro en Aduana
		public static Berke.DG.ViewTab.ParamTab RegAduanaDelete( Berke.DG.RenovacionDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Renovacion_RegAduanaDelete" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Delete Registro en Aduana
	}
	#endregion class Renovacion

}// end namespace Berke.Marcas.UIProcess.Model