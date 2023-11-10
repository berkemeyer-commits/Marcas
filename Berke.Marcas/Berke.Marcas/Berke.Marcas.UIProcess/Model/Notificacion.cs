using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Notificacion.
	/// </summary>
	public class Notificacion
	{
	
		#region ReadList
		public static Berke.DG.DBTab.Notificacion ReadList( Berke.DG.DBTab.Notificacion inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Notificacion_ReadList" , tmp_InDS );
			
			Berke.DG.DBTab.Notificacion outTB	=  new Berke.DG.DBTab.Notificacion( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList
	}
}
