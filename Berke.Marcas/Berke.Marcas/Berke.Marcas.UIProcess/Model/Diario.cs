using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;
	using BizDocuments.Helpers;

	public class Diario
	{
		#region ReadForSelect
		public static SimpleEntryDS ReadForSelect()
		{
//			SimpleEntryDS outDS = new SimpleEntryDS();
//			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			DS.fillFromXmlFile( outDS, xmlPath + "DiarioReadForSelect.xml" );
//			//---------------------------------------------
//			return outDS;

			SimpleEntryDS outDS;

			ParamDS inDS = (ParamDS) DS.Pack( "" );
			outDS = (SimpleEntryDS) Action.Execute( Actions.Diario_ReadByPattern.ToString() , inDS );

			return outDS;

		}
		#endregion ReadForSelect

		#region Read
		public static Berke.DG.DBTab.Diario Read( Berke.DG.DBTab.Diario inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Diario_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Diario outTB	=  new Berke.DG.DBTab.Diario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read 

		#region ReadByAbrev
		public static Berke.DG.DBTab.Diario ReadByAbrev( string abrev )
		{
			Berke.DG.DBTab.Diario inTB = new Berke.DG.DBTab.Diario();

			inTB.DisableConstraints();
			inTB.NewRow();
			inTB.Dat.Abrev.Value = abrev;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Diario_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Diario outTB	=  new Berke.DG.DBTab.Diario( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByAbrev 


	}
}
