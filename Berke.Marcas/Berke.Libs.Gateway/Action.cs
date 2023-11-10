using System;

namespace Berke.Libs.Gateway {
	using System.Data;
	using Framework.Core;
	using Framework.Channels;
	using Framework.Util;
	using System.Xml;
	using System.Security.Principal;
	
	public class UpsertRequest : Request{
		// Dato miembro
		public DataSet _oldDS;
		// Properties
		public DataSet OldDS { get { return _oldDS;	} }
		// Constructor
		public UpsertRequest( String actionCode, DataSet inputDS, DataSet oldDS ): base( actionCode, inputDS )
		{
			_oldDS = oldDS;
		}
	}

	public class Action {

		
		public static DataSet Execute( string actionCode, DataSet inputDS, DataSet oldDS )
		{
			UpsertRequest request = new UpsertRequest( actionCode, inputDS, oldDS );
			bool isDispatcherOn = IsDispatcherOn();
			Response response = isDispatcherOn ? ExecuteByDispatcher( request ) : ExecuteDirect( request );
			return response.RawDataSet;
		}
		


		#region Execute == public ==
		public static DataSet Execute( string actionCode, DataSet inputDS )
		{
			Request request = new Request( actionCode, inputDS );
			bool isDispatcherOn = IsDispatcherOn();
			Response response = isDispatcherOn ? ExecuteByDispatcher( request ) : ExecuteDirect( request );
			return response.RawDataSet;
		}
		#endregion

		#region ExecuteDirect
		private static Response ExecuteDirect ( Request request ){
			const string FWKCONFIG_PATH		= @"c:\program files\fwkdevenv\fwk.config";
			const string ACTION_XPATH		= @"//actions/action[@code='{0}']";
			const string HANDLERCLASS_XPATH = @"handler-class";

			// Abro el catalogo
			XmlDocument config = new XmlDocument();
			config.Load( FWKCONFIG_PATH );
			XmlNode actionNode = config.SelectSingleNode( string.Format( ACTION_XPATH, request.Action ) );
			if( actionNode == null )
				throw new Exception( string.Format( "Non catalogued action: {0}", request.Action ) );

			// Levanto la IAction
			string handler = actionNode.Attributes[ HANDLERCLASS_XPATH ].Value;			
			Type type = TypeLoader.LoadType( handler );
			IAction action = (IAction) Activator.CreateInstance( type );

			// -------- DEBUG ------

//			Write( request );
			
			// Ejecuto la action ==== MBI ================
			Command command = new Command();
			command.Request = request;
			action.Execute( command );
			Response response = command.Response;
			return response;
		} 
		#endregion

		#region ExecuteByDispatcher
		private static Response ExecuteByDispatcher ( Request request ){
			string dispURL = (string) Config.GetConfigParam( "DISPURL" );
			DispDirect dispatcher = new DispDirect(	dispURL	);
			Response response = dispatcher.ExecuteAction( request, GetPrincipal() );
			return response;
		}
		#endregion

		#region Utils
		public static IPrincipal GetPrincipal() {
			string user = "MyUser";
			string[] roles = {"MyRol"};
			IIdentity identity = new GenericIdentity( user );
			IPrincipal principal = new GenericPrincipal( identity, roles );
			return principal;
		}


		private static Boolean IsDispatcherOn(){
			try{
				bool isDipatcherOn = ( (string) Config.GetConfigParam( "DISPATCHER" ) == "ON" );
				return isDipatcherOn;
//				return false;
			}catch(Exception ex){
				String xx = ex.Message;
				return false;
			}
		}

		private static void Write( Request request ){
			string dsName = request.RawDataSet.DataSetName;
			XmlTextWriter xw = new XmlTextWriter( string.Format( @"c:\req_{0}_{1}.xml", request.Action, dsName ), System.Text.Encoding.UTF8 );
			request.WriteXml( xw );
			xw.Close();
		}

		private static void Write( Request request, Response response ){
			XmlTextWriter xw = new XmlTextWriter( string.Format( @"c:\res_{0}.xml", request.Action ), System.Text.Encoding.UTF8 );
			response.WriteXml( xw );
			xw.Close();
		}
		#endregion
	}
}
