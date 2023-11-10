
using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class DocumPlantilla
	public class DocumPlantilla
	{

		public static string GetPattern( string clave ){
			Berke.DG.DBTab.DocumentoPlantilla inTB = new Berke.DG.DBTab.DocumentoPlantilla();
			inTB.DisableConstraints();

			inTB.NewRow();
			inTB.Dat.Clave.Value = clave;
			inTB.PostNewRow();

			DataSet tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "DocumPlantilla_Read" , inTB.AsDataSet() );
			
			Berke.DG.DBTab.DocumentoPlantilla outTB	=  new Berke.DG.DBTab.DocumentoPlantilla( tmp_OutDS.Tables[0] );
			return outTB.Dat.PlantillaHTML.AsString;

		}

		public static string GetPattern( string clave, int idiomaID )
		{
			Berke.DG.DBTab.DocumentoPlantilla inTB = new Berke.DG.DBTab.DocumentoPlantilla();
			inTB.DisableConstraints();

			inTB.NewRow();
			inTB.Dat.Clave.Value = clave;
			inTB.Dat.IdiomaID.Value = idiomaID;
			inTB.PostNewRow();

			DataSet tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "DocumPlantilla_Read" , inTB.AsDataSet() );
			
			Berke.DG.DBTab.DocumentoPlantilla outTB	=  new Berke.DG.DBTab.DocumentoPlantilla( tmp_OutDS.Tables[0] );
			return outTB.Dat.PlantillaHTML.AsString;

		}

		#region Read
		public static Berke.DG.DBTab.DocumentoPlantilla Read( Berke.DG.DBTab.DocumentoPlantilla inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "DocumPlantilla_Read" , tmp_InDS );
			
			Berke.DG.DBTab.DocumentoPlantilla outTB	=  new Berke.DG.DBTab.DocumentoPlantilla( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read 
	
	}
	#endregion class DocumPlantilla

}// end namespace Berke.Marcas.UIProcess.Model

