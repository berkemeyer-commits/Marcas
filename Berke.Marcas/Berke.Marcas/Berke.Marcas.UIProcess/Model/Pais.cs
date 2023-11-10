
//----------------------  MODEL -----------------------
using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Pais
	public class Pais
	{
		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect(  )
		{
			Berke.DG.DBTab.CPais inTB = new Berke.DG.DBTab.CPais();

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Pais_Read" , tmp_InDS );
			
			Berke.DG.DBTab.CPais pais	=  new Berke.DG.DBTab.CPais( tmp_OutDS.Tables[0] );

			Berke.DG.ViewTab.ListTab outTab = new Berke.DG.ViewTab.ListTab();
			for( pais.GoTop() ; ! pais.EOF ; pais.Skip() ){
				outTab.NewRow();
				outTab.Dat.ID.Value			= pais.Dat.idpais.AsString;
				outTab.Dat.Descrip.Value	= pais.Dat.descrip.AsString + " ("+pais.Dat.paisalfa.AsString +")";
				outTab.PostNewRow();
			}
			return outTab;
		}
		#endregion ReadForSelect 
	
		#region ReadByID
		public static Berke.DG.DBTab.CPais ReadByID( int ID  )
		{
			Berke.DG.DBTab.CPais inTB = new Berke.DG.DBTab.CPais();
			inTB.NewRow();
			inTB.Dat.idpais.Value = ID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Pais_Read" , tmp_InDS );
			
			Berke.DG.DBTab.CPais outTB	=  new Berke.DG.DBTab.CPais( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID


		#region Read
		public static Berke.DG.DBTab.CPais Read( Berke.DG.DBTab.CPais inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Pais_Read" , tmp_InDS );
			
			Berke.DG.DBTab.CPais outTB	=  new Berke.DG.DBTab.CPais( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read 
	
	}
	#endregion class Pais

}// end namespace Berke.Marcas.UIProcess.Model

