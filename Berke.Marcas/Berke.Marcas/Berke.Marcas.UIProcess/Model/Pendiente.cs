
using System;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Marca;
	using Libs.Base.DSHelpers;
	using BizDocuments.Helpers;

	public class Pendiente
	{
		public static SimpleEntryDS ReadForSelect()
		{
			SimpleEntryDS outDS;

			ParamDS inDS = (ParamDS) DS.Pack( "" );
			outDS = (SimpleEntryDS) Action.Execute("Pendiente_ReadByPattern" , inDS );

			return outDS;
		}
	}
}
