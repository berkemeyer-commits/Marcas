
#region Notificacion.	ReadList
namespace Berke.Marcas.BizActions.Notificacion
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadList: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName = (string) Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.DBTab.Notificacion inTB	= new Berke.DG.DBTab.Notificacion( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Mail_Destino		= inTB.Dat.Mail_Destino.Value;
			object Func_Destino		= inTB.Dat.Func_Destino.Value;
			object Activo		= inTB.Dat.Activo.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.DBTab.Notificacion outTB = new Berke.DG.DBTab.Notificacion( db );

			#region Filtros

			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Mail_Destino.		Filter = ObjConvert.GetSqlPattern	( Mail_Destino );
			outTB.Dat.Func_Destino.		Filter = ObjConvert.GetSqlPattern	( Func_Destino );
			outTB.Dat.Activo.		Filter = Activo;
	
			#endregion Filtros
			
			outTB.Adapter.ReadAll( 500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();

		}

	
	} // End ReadList class


}// end namespace 

/*  Catalogo de la Action en fwk.Config 

	

*/






#endregion  Notificacion.	ReadList