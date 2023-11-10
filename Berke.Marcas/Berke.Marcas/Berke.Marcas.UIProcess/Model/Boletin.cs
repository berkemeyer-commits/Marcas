using System;

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

	public class Boletin
	{

		#region Import 		
//		public static int Import( Berke.DataSets.BoletinDS inDS)
//		{
//			ParamDS outDS;
//			
//			outDS =  (ParamDS) Action.Execute( "Boletin_Import" , inDS.PayLoad );
//			int registrosInsertados = DS.UnpackInt( outDS );
//			return registrosInsertados;
//		}

		#endregion Import 

		#region ReadList
		public static BoletinListDS ReadList( BoletinParamDS inDS )
		{
//			BoletinListDS outDS = new BoletinListDS();
//			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			DS.fillFromXmlFile( outDS, xmlPath + "BoletinListDS.xml" );
//			//---------------------------------------------
//			return outDS;

			BoletinListDS outDS = new BoletinListDS();

			outDS = (BoletinListDS) Action.Execute("Boletin_ReadList" , inDS );

			return outDS;


		}
		#endregion ReadList

		#region ReadByID
		public static BoletinDetalleDS ReadByID( int ID)
		{
//			BoletinDetalleDS outDS = new BoletinDetalleDS();
//			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			DS.fillFromXmlFile( outDS, xmlPath + "BoletinDetalleDS.xml" );
//			//---------------------------------------------
//			return outDS;

			BoletinDetalleDS outDS = new BoletinDetalleDS();
			ParamDS inDS = (ParamDS) DS.Pack(  ID );
			outDS = (BoletinDetalleDS) Action.Execute("Boletin_ReadByID" , inDS );

			return outDS;

		}
		#endregion ReadByID


		#region Upsert
		public static BoletinDetalleDS Upsert( BoletinDetalleDS inDS )
		{ 
			BoletinDetalleDS outDS ;
	
			outDS = (BoletinDetalleDS) Action.Execute("Boletin_Upsert" , inDS );

			return outDS;
		}
		#endregion Upsert

	}
}
