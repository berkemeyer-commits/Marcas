using System;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;
	using BizDocuments.Helpers;

	public class MarcaActualizar
	{
//		#region ReadByID
//		public static MarcaActualizarDS ReadByID( int ID )
//		{ 
//			ParamDS inDS = (ParamDS) DS.Pack( ID );
//			MarcaActualizarDS outDS ;
////			//--- Datos de Gua'u --------------------------
////			MarcaActualizarDS outDS = new MarcaActualizarDS();
////			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
////			DS.fillFromXmlFile( outDS, xmlPath + "MarcaActualizarDS.xml" );
////			//---------------------------------------------
//
//			outDS = (MarcaActualizarDS) Action.Execute("MarcaActualizar_ReadByID" , inDS );
//
//			return outDS;
//		}
//		#endregion ReadByID

		#region Upsert
		public static MarcaActualizarDS Upsert( MarcaActualizarDS inDS )
		{ 
			MarcaActualizarDS outDS ;
	
			outDS = (MarcaActualizarDS) Action.Execute("MarcaActualizar_Upsert" , inDS );

			return outDS;
		}
		#endregion Upsert

		#region ClienteUpdate
		public static ExpedienteListDS ClienteUpdate( ExpedienteListDS inDS )
		{ 
			ExpedienteListDS  outDS ;
	
			outDS = (ExpedienteListDS) Action.Execute("MarcaActualizar_ClienteUpdate" , inDS );

			return outDS;
		}

		#endregion ClienteUpdate
	}


	
	
	

}
