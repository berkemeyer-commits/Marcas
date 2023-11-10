using System;
using Framework.Core;
using Berke.Libs.Gateway;
using Berke.Libs.Base;
using Berke.Marcas.BizDocuments.Helpers;
using Berke.Marcas.BizDocuments.Marca;
using Berke.Libs.Base.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.Marcas.BizDocuments;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for ExpedientePendiente.
	/// </summary>
	public class ExpedientePendiente
	{
		#region ReadList
		public static ExpedientePendienteDS ReadList( int ExpedienteID )
		{

			ParamDS inDS = (ParamDS) DS.Pack( ExpedienteID );

			ExpedientePendienteDS outDS = new ExpedientePendienteDS();

			outDS = (ExpedientePendienteDS) Berke.Libs.Gateway.Action.Execute("ExpedientePendiente_ReadList" , inDS );

			return outDS;
		}
		#endregion ReadList

		#region Upsert
		public static ExpedientePendienteDS Upsert( ExpedientePendienteDS inDS )
		{
			ExpedientePendienteDS outDS = new ExpedientePendienteDS();

            outDS = (ExpedientePendienteDS)Berke.Libs.Gateway.Action.Execute("ExpedientePendiente_Upsert", inDS);

			return outDS;
		}
		#endregion Upsert

	}
}
