using System;
using System.Data;

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

	/// <summary>
	/// Summary description for HI.
	/// </summary>
	public class HI
	{
		
			#region ReadList
			public static Berke.DG.ViewTab.vHIresu ReadList( Berke.DG.ViewTab.vHIresu inTB )
			{
				DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
				DataSet  tmp_OutDS;

				tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "HI_ReadList" , tmp_InDS );
			
				Berke.DG.ViewTab.vHIresu outTB	=  new Berke.DG.ViewTab.vHIresu( tmp_OutDS.Tables[0] );

				return outTB;
			}
		#endregion ReadList
		
	}
}
