using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Situacion
	public class Situacion
	{

//		#region ReadForSelect
//		public static SimpleEntryDS ReadForSelect( int TramiteID )
//		{
//			SimpleEntryDS outDS = new SimpleEntryDS();
//			ParamDS inDS = (ParamDS) DS.Pack( TramiteID );
//			outDS = (SimpleEntryDS) Action.Execute( Actions.Situacion_ReadByPattern.ToString() , inDS );
//			
//			return outDS;
//		}
//		#endregion ReadForSelect


		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect(  )
		{
			Berke.DG.DBTab.Situacion inTB = new Berke.DG.DBTab.Situacion();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Situacion_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Situacion outTB	=  new Berke.DG.DBTab.Situacion( tmp_OutDS.Tables[0] );

			
			Berke.DG.ViewTab.ListTab lista = new Berke.DG.ViewTab.ListTab();
			for( outTB.GoTop(); ! outTB.EOF; outTB.Skip() ){
				lista.NewRow();
				lista.Dat.ID.Value	= outTB.Dat.ID.AsInt;
				lista.Dat.Descrip.Value = outTB.Dat.Descrip.AsString;
				lista.PostNewRow();
			}
			
			return lista;
		}
		#endregion ReadForSelect

	

		#region ReadByID
		public static Berke.DG.DBTab.Situacion ReadByID( int situacionID )
		{
			Berke.DG.DBTab.Situacion inTB = new Berke.DG.DBTab.Situacion();
			inTB.NewRow();
			inTB.Dat.ID.Value = situacionID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Situacion_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Situacion outTB	=  new Berke.DG.DBTab.Situacion( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID 

		#region Read
		public static Berke.DG.DBTab.Situacion Read( Berke.DG.DBTab.Situacion inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Situacion_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Situacion outTB	=  new Berke.DG.DBTab.Situacion( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read
	
	}
	#endregion class Situacion

}// end namespace Berke.Marcas.UIProcess.Model
