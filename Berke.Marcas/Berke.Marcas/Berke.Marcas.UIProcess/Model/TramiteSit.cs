using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Helpers;
	using BizDocuments.Marca;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments;
	
	
	#region class TramiteSit
	public class TramiteSit
	{
		#region Read_AsViewTab
		public static Berke.DG.ViewTab.vTramiteSit Read_AsViewTab( Berke.DG.ViewTab.vTramiteSit inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_Read_AsViewTab" , tmp_InDS );
			
			Berke.DG.ViewTab.vTramiteSit outTB	=  new Berke.DG.ViewTab.vTramiteSit( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read_AsViewTab 


		#region ReadByTramiteID_AsListTab
		public static Berke.DG.ViewTab.ListTab ReadByTramiteID_AsListTab( int TramiteID )
		{
			Berke.DG.ViewTab.vTramiteSit param = new Berke.DG.ViewTab.vTramiteSit();
			param.NewRow();
			param.Dat.TramiteID.Value = TramiteID;
			param.PostNewRow();

			DataSet  tmp_InDS	= param.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_Read_AsViewTab" , tmp_InDS );
			
			Berke.DG.ViewTab.vTramiteSit trmSit	=  new Berke.DG.ViewTab.vTramiteSit( tmp_OutDS.Tables[0] );

			Berke.DG.ViewTab.ListTab salida = new Berke.DG.ViewTab.ListTab();

			for( trmSit.GoTop(); ! trmSit.EOF ; trmSit.Skip() ) {
				salida.NewRow();
				salida.Dat.ID.Value		= trmSit.Dat.ID.AsInt;
				salida.Dat.Descrip.Value	= trmSit.Dat.Descrip.AsString;
				salida.PostNewRow();						   
			}

			salida.Dat.Descrip.Order = 1;
			salida.Sort();
			salida.AcceptAllChanges();

			return salida;
		}
		#endregion Read_AsViewTab 

		#region ReadListByID
		public static Berke.DG.ViewTab.vTramiteSit ReadListByID( int trmSitID )
		{
			Berke.DG.ViewTab.vTramiteSit inTB = new Berke.DG.ViewTab.vTramiteSit();
			inTB.NewRow();
			inTB.Dat.ID.Value = trmSitID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vTramiteSit outTB	=  new Berke.DG.ViewTab.vTramiteSit( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadListByID

		#region ReadList
		public static Berke.DG.ViewTab.vTramiteSit ReadList( Berke.DG.ViewTab.vTramiteSit inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vTramiteSit outTB	=  new Berke.DG.ViewTab.vTramiteSit( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region ReadForSelect
		public static SimpleEntryDS ReadForSelect( int TramiteID )
		{
			SimpleEntryDS outDS = new SimpleEntryDS();
			ParamDS inDS = (ParamDS) DS.Pack( TramiteID );
			outDS = (SimpleEntryDS) Action.Execute( Actions.TramiteSit_ReadByPattern.ToString() , inDS );
			
			return outDS;
		}
		#endregion ReadForSelect

		#region VencimientoFecha
//		public static DateTime VencimientoFecha (int TramiteSitID, DateTime Fecha)
//		{
//			//Calcula la fecha de vencimiento de la situación
//			//Ver MA_FV_2 CalcFecVto en el Manual pag.103
//			return DateTime.Today;
//		}
		#endregion

		#region ReadByID_M
		public static Berke.DG.DBTab.Tramite_Sit ReadByID_M (int TramiteSit_ID)
		{
			Berke.DG.DBTab.Tramite_Sit inTB = new Berke.DG.DBTab.Tramite_Sit();
			inTB.NewRow();
			inTB.Dat.ID.Value = TramiteSit_ID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_ReadByParam" , tmp_InDS );
			
			Berke.DG.DBTab.Tramite_Sit outTB	=  new Berke.DG.DBTab.Tramite_Sit( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID_M

		#region ReadByParam
		public static Berke.DG.DBTab.Tramite_Sit ReadByParam( Berke.DG.DBTab.Tramite_Sit inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;
			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_ReadByParam" , tmp_InDS );
			
			Berke.DG.DBTab.Tramite_Sit outTB	=  new Berke.DG.DBTab.Tramite_Sit( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByParam 

		#region FechaVencim
		public static DateTime FechaVencim( DateTime fechaSit, int tramiteSitID )
		{
		
			#region Parametros
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();

			inTB.NewRow(); 
			inTB.Dat.Entero		.Value = tramiteSitID;   //Int32
			inTB.Dat.Fecha		.Value = fechaSit;   //DateTime
			inTB.PostNewRow(); 
			#endregion
		
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "TramiteSit_FechaVencim" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB.Dat.Fecha.AsDateTime;
		}
		#endregion FechaVencim 

	}
	#endregion class TramiteSit

}// end namespace Berke.Marcas.UIProcess.Model
