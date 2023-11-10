using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for DocumentoTipo.
	/// </summary>
	public class DocumentoTipo
	{
		public DocumentoTipo()
		{
			
		}
		#region ReadList
		public static Berke.DG.DBTab.DocumentoTipo ReadList( Berke.DG.DBTab.DocumentoTipo inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "DocumentoTipo_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.DocumentoTipo outTB	=  new Berke.DG.DBTab.DocumentoTipo( tmp_OutDS.Tables[0] );
	
			return outTB;
		}
		#endregion ReadList

		#region ReadForSelect
		public static Berke.DG.ViewTab.ListTab ReadForSelect()
		{
			Berke.DG.ViewTab.ListTab result = new Berke.DG.ViewTab.ListTab();

			Berke.DG.DBTab.DocumentoTipo inTB = new Berke.DG.DBTab.DocumentoTipo();
//			inTB.
//			inTB.NewRow();
//			inTB.Dat.Descrip.Value = "";
//			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "DocumentoTipo_ReadList" , tmp_InDS );
			


			Berke.DG.DBTab.DocumentoTipo docTipo	=  new Berke.DG.DBTab.DocumentoTipo( tmp_OutDS.Tables[0] );
			for( docTipo.GoTop(); ! docTipo.EOF; docTipo.Skip() ){
				result.NewRow();
				result.Dat.ID.Value = docTipo.Dat.ID.AsInt;
				result.Dat.Descrip.Value = docTipo.Dat.Descrip.AsString;
				result.PostNewRow();
			}

			return result;
		}
		#endregion ReadForSelect




	}
}
