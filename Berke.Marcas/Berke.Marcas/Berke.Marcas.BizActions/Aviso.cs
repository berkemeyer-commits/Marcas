

#region Aviso.	ReadList
namespace Berke.Marcas.BizActions.Aviso
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
			db.DataBaseName		= (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName		= (string) Config.GetConfigParam("CURRENT_SERVER");		

			Berke.DG.ViewTab.vAviso inTB	= new Berke.DG.ViewTab.vAviso( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object FechaAlta_min	= inTB.Dat.FechaAlta.Value;
			object FechaAviso_min	= inTB.Dat.FechaAviso.Value;
			object Pendiente		= inTB.Dat.Pendiente.Value;
			object Asunto		= inTB.Dat.Asunto.Value;
			object Contenido		= inTB.Dat.Contenido.Value;
			object Remitente		= inTB.Dat.Remitente.Value;
			object Destinatario		= inTB.Dat.Destinatario.Value;
			object Indicaciones		= inTB.Dat.Indicaciones.Value;
			object Origen		= inTB.Dat.Origen.Value;
			object Destino		= inTB.Dat.Destino.Value;
			object PrioridadID	= inTB.Dat.PrioridadID.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;
			object FechaAlta_max	= inTB.Dat.FechaAlta.Value;
			object FechaAviso_max	= inTB.Dat.FechaAviso.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vAviso outTB = new Berke.DG.ViewTab.vAviso( db );

			#region Filtros


			outTB.Dat.ID.				Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.FechaAlta.		Filter = new DSFilter( FechaAlta_min, FechaAlta_max );
			outTB.Dat.FechaAviso.		Filter = new DSFilter( FechaAviso_min, FechaAviso_max );
			outTB.Dat.Pendiente.		Filter = Pendiente;
			outTB.Dat.Asunto.			Filter = ObjConvert.GetSqlPattern	( Asunto );
			outTB.Dat.Contenido.		Filter = ObjConvert.GetSqlPattern	( Contenido );
			outTB.Dat.Remitente.		Filter = Remitente;
			outTB.Dat.Destinatario.		Filter = Destinatario;
			outTB.Dat.Indicaciones.		Filter = ObjConvert.GetSqlPattern	( Indicaciones );
			outTB.Dat.Origen.			Filter = ObjConvert.GetSqlPattern	( Origen );
			outTB.Dat.Destino.			Filter = ObjConvert.GetSqlPattern	( Destino );
			outTB.Dat.PrioridadID.		Filter = PrioridadID;
	
			#endregion Filtros
			
			outTB.Dat.FechaAlta.Order = -1;

			outTB.Adapter.ReadAll( 1000 );

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

					<action code="Aviso_ReadList" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Aviso.ReadList,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />

*/


/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.Aviso

*/



#endregion  Aviso.	ReadList



#region Aviso.	Read
namespace Berke.Marcas.BizActions.Aviso
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Read: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.DBTab.Aviso inTB	= new Berke.DG.DBTab.Aviso( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID		= inTB.Dat.ID.Value;
			object FechaAlta		= inTB.Dat.FechaAlta.Value;
			object FechaAviso		= inTB.Dat.FechaAviso.Value;
			object Pendiente		= inTB.Dat.Pendiente.Value;
			object Asunto		= inTB.Dat.Asunto.Value;
			object Contenido		= inTB.Dat.Contenido.Value;
			object Remitente		= inTB.Dat.Remitente.Value;
			object Destinatario		= inTB.Dat.Destinatario.Value;
			object Indicaciones		= inTB.Dat.Indicaciones.Value;

			#endregion Parametros

			Berke.DG.DBTab.Aviso outTB = new Berke.DG.DBTab.Aviso( db );

			#region Filtros
			outTB.Dat.ID.				Filter = ID;
			outTB.Dat.FechaAlta.		Filter = FechaAlta;
			outTB.Dat.FechaAviso.		Filter = FechaAviso;
			outTB.Dat.Pendiente.		Filter = Pendiente;
			outTB.Dat.Asunto.			Filter = ObjConvert.GetSqlPattern	( Asunto );
			outTB.Dat.Contenido.		Filter = ObjConvert.GetSqlPattern	( Contenido );
			outTB.Dat.Remitente.		Filter = Remitente;
			outTB.Dat.Destinatario.		Filter = Destinatario;
			outTB.Dat.Indicaciones.		Filter = ObjConvert.GetSqlPattern	( Indicaciones );
			#endregion Filtros
			
			outTB.Adapter.ReadAll( 500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();
		}
	
	} // End Read class

}// end namespace 
#endregion  Aviso.	Read