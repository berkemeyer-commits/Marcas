using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model {
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Helpers;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;

	/// <summary>
	/// Summary description for Clase.
	/// </summary>
	public class Clase {
		
		#region ReadForSelect()
		public static SimpleEntryDS ReadForSelect()
		{
			SimpleEntryDS outDS;

			ParamDS inDS = (ParamDS) DS.Pack( "" );
			outDS = (SimpleEntryDS) Action.Execute( Actions.Clase_ReadByPattern.ToString() , inDS );

			return outDS;
		}
		#endregion

		#region ReadForSelect( int EdicionID )
		public static SimpleEntryDS ReadForSelect( int EdicionID )
		{
//			SimpleEntryDS outDS = new SimpleEntryDS();			
			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			DS.fillFromXmlFile( outDS, xmlPath + "ClaseEdicionDS.xml" );
			//---------------------------------------------
			SimpleEntryDS outDS;
			ParamDS inDS = (ParamDS) DS.Pack(  EdicionID );
			outDS = (SimpleEntryDS) Action.Execute( Actions.Clase_ReadByPattern.ToString() , inDS );
			return outDS;
		}
		#endregion 
//
//		#region ReadByID
//		public static ClaseDS ReadByID (int ID)
//		{
//			ClaseDS outDS;
//			ParamDS inDS = (ParamDS) DS.Pack( ID );
//			outDS = (ClaseDS) Action.Execute( "Clase_ReadByID" , inDS );
//			return outDS;
//		}
//		#endregion
		#region ReadByID
		public static Berke.DG.DBTab.Clase ReadByID (int ID)
		{
//			ClaseDS outDS;
			ParamDS inDS = (ParamDS) DS.Pack( ID );
//			outDS = (ClaseDS) Action.Execute( "Clase_ReadByID" , inDS );

			DataSet outDS; 
			outDS = (DataSet) Action.Execute( "Clase_ReadByID" , inDS );
			return new Berke.DG.DBTab.Clase( outDS.Tables[0] );;
		}
		#endregion

		#region ReadList
		public static Berke.DG.ViewTab.vClase ReadList( Berke.DG.ViewTab.vClase inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Clase_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vClase outTB	=  new Berke.DG.ViewTab.vClase( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

	}
}
