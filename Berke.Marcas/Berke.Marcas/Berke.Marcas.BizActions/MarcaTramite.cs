
using System;

namespace Berke.Marcas.BizActions.MarcaTramite
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	 
//	public class ReadByID: IAction
//	{	
//		public void Execute( Command cmd ) 
//		{		 
//			Berke.Libs.Base.DSHelpers.ParamDS inDS;
//			Berke.Marcas.BizDocuments.Marca.MarcaTramiteDS outDS;
//
//			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
//			
//			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			outDS = new Berke.Marcas.BizDocuments.Marca.MarcaTramiteDS();
//			DS.cargarDatosDeGuau( outDS, xmlPath + "MarcaTramite2.xml" );
//			//---------------------------------------------
//
//			//outDS = Berke.Entidades.Dalc.MarcaTramite.ReadByID( inDS, Connections.Berke );
//
//			cmd.Response = new Response( outDS );           		
//		}
//	}
}
