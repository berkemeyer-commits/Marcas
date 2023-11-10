using System;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;
	using BizDocuments.Helpers;

	public class PoderTipo
	{
		public static SimpleEntryDS ReadForSelect()
		{
			ParamDS inDS = (ParamDS) DS.Pack( "" ); 
			SimpleEntryDS outDS = (SimpleEntryDS) Action.Execute( "PoderTipo_ReadByPattern" , inDS );
			return outDS;
		}
	}
}
