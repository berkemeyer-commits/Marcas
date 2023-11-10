using System;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;
	using BizDocuments.Helpers;

	public class Instruccion
	{
		public static SimpleEntryDS ReadForSelect()
		{
//			SimpleEntryDS outDS = new SimpleEntryDS();
//			//--- Datos de Gua'u --------------------------
//			string xmlPath =  (String) Config.GetConfigParam("XML_PATH");
//			DS.fillFromXmlFile( outDS, xmlPath + "InstruccionReadForSelect.xml" );
//			//---------------------------------------------
//			return outDS;

			SimpleEntryDS outDS;

			ParamDS inDS = (ParamDS) DS.Pack( "" );
			outDS = (SimpleEntryDS) Action.Execute("Instruccion_ReadByPattern" , inDS );

			return outDS;

		}
	}
}
