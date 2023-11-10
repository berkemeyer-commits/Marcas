using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Web.Configuration;

namespace Berke.Marcas.WebUI {

	using System.Web.Security;
//	using EMAB = Microsoft.ApplicationBlocks.ExceptionManagement;
	using BizDocuments;
	using UIPModel = UIProcess.Model;
	using BizDocs = BizDocuments;
    using WebUI.Helpers;


	/// <summary>
	/// Global
	/// </summary>
	public class Global : System.Web.HttpApplication {
		/// <summary>
		/// Required designer variable
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
		}
        #endregion

        #region Constants
        private const string USER_NAME = "userName";
        #endregion Constants

        #region Application_Start

        protected void Application_Start( Object sender, EventArgs e ) {		
			//MyApplication.Pertenencia = UIPModel.Pertenencia.Buscar();
			//MyApplication.Pertenencia = UIPModel.Pertenencia.ReadForSelect();
			//TODO: reemplazar por Clase.ReadForSelect(1 y 2)
			//MyApplication.Clase = UIPModel.Clase.Buscar();
			//MyApplication.Tramite = Berke.Entidades.UIProcess.Model.Tramite.ReadForSelect( (int) Const.Area.MARCAS );
		}
		#endregion
		
		#region Session_End
		protected void Session_End( Object sender, EventArgs e ) {
			if( Session.Count > 0 ){
				Session.Clear();
			}
			FormsAuthentication.SignOut();
		}
		#endregion

		#region Application_Error
		protected void Application_Error( Object sender, EventArgs e ) {
			Exception ex = HttpContext.Current.Error;
			string page =  HttpContext.Current.Request.Url.AbsolutePath;
			try
			{

				#region Grabar LOG de Error
				string machineName = "Machine:" + HttpContext.Current.Server.MachineName;
				string user = " User:"+ HttpContext.Current.User.Identity.Name;

				Exception err = ex;
				string errorMessage = err.Message;

				while(  err.InnerException != null )
				{
					errorMessage+= errorMessage == "" ? "" : " / " + err.InnerException.Message ;
					err = err.InnerException;
				}
				errorMessage = errorMessage.Replace(Environment.NewLine," ");
				errorMessage = errorMessage.Replace("<", "*");
				errorMessage = errorMessage.Replace(">", "*");
				#region Grabar Archivo de log
				try
				{
					//string archivoLog = @"k:/cache/BerkeError.txt";
					string archivoLog = HttpContext.Current.Request.PhysicalApplicationPath + @"BerkeError.txt";

					System.IO.StreamWriter log = new StreamWriter(archivoLog, true );
			
					log.WriteLine(machineName + "\t" + DateTime.Now.ToString()+"\t" + user + "\t" + errorMessage + "\t" + "Pagina:" + page );
					log.Close();
				}
				catch {}
				#endregion

				#endregion 

				string paginaError = HttpContext.Current.Request.ApplicationPath + "/Generic/Message.aspx";

				if (ex is System.Security.SecurityException )
				{
					Response.Write( "***" + errorMessage );

//					Response.Redirect( Const.PAGE_LOGIN );
				}
				else
				{
					if( page == paginaError ) // Si la pagina de error dio error
						Response.Write( "***"+ ex.Message + " "+ errorMessage );

//						Response.Redirect( Const.PAGE_ERROR_HTM );
					else 
					{
						// EMAB.ExceptionManager.Publish( ex );
						try
						{
							Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
							url.AddParam("Mensaje", errorMessage );
							url.AddParam("Server", machineName );
							url.AddParam("User", user );
							url.AddParam("Page", page );
							url.redirect( paginaError );
						}
						catch( Exception erro )
						{
							Response.Write( "***"+ erro.Message + " "+ errorMessage );
						}
						//					Response.Redirect(  Const.PAGE_ERROR );
						//					throw ex;
					}
				}
			}
			catch{
				Response.Redirect( Const.PAGE_ERROR_HTM );
			}
		}
		#endregion
		
		#region Application_BeginRequest

		protected void Application_BeginRequest( Object sender, EventArgs e ) 
		{

            try
            {
			
	
				if( MyApplication.Pertenencia == null )
				{
					/* ---aacuna--- 12/oct/2006 Eliminación de tabla Pertenencia
					 * MyApplication.Pertenencia	= UIPModel.Pertenencia.ReadForSelect();
					*/

                    //MyApplication.CurrentDBName		= Berke.Marcas.UIProcess.Model.GAD.CurrentDB(); // (string) Config.GetConfigParam("CURRENT_DATABASE");
                    //MyApplication.CurrentServerName = Berke.Marcas.UIProcess.Model.GAD.CurrentServer(); // (string) Config.GetConfigParam("CURRENT_DATABASE");
                    // Cancelado ncaceres 04/08/06 				
                    MyApplication.CurrentDBName = Berke.Libs.Base.Acceso.CurrentDB();
                    MyApplication.CurrentServerName = Berke.Libs.Base.Acceso.CurrentServer();
					MyApplication.HomeFolder	= HttpContext.Current.Request.ApplicationPath;
                    MyApplication.CurrentWebServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

                    //[ggaleano 01/02/2022] Se configura System.Net.Mail para que utilice TLS 1.2
                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                Response.Redirect(Const.PAGE_ERROR_HTM);
            }
			CultureInfo culture = null;

			foreach( string lang in Request.UserLanguages ) 
			{
				try 
				{
					// Do not use CultureInfo constructor to avoid to create neutral culture.
					culture = CultureInfo.CreateSpecificCulture( lang );
				}
				catch{} // ignore

				if ( culture != null )
					break;
			}
			if ( culture == null )
				culture = new CultureInfo( "en-US" );

			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
	
		}
		#endregion

		#region _Others

		public Global()
		{
			InitializeComponent();
		}	
		protected void Session_Start( Object sender, EventArgs e ){
			if( MySession.UserName == "" )
			{
                MySession.UserName = Berke.Libs.Base.Acceso.GetCurrentUser();
                
                if (WebConfigurationManager.AppSettings[USER_NAME] != null)
                {
                    MySession.UserName = WebConfigurationManager.AppSettings[USER_NAME].ToString();
                }
                
                if ( MySession.UserName.Trim() == "" )
				{
					throw new Exception("El Usuario actual no pudo ser identificado");
				}
				Berke.DG.ViewTab.vFuncionario fun;
				fun = Berke.Marcas.UIProcess.Model.Funcionario.ReadByUserName( MySession.UserName );
				if( fun.RowCount != 1 ){

                    Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
                    db.DataBaseName = Berke.Libs.Base.Acceso.CurrentDB();
                    db.ServerName = Berke.Libs.Base.Acceso.CurrentServer();

                    string comando = @" SELECT ID
                                        FROM Usuario
                                        WHERE UsuarioDominio2 = @usuario";
                    db.Sql = comando;
                    db.ClearParams();
                    db.AddParam("@usuario", System.Data.SqlDbType.NVarChar, MySession.UserName, 30);
                    int val = (int)db.getValue();
                    db.CerrarConexion();

                    if (val > 0)
                        fun = Berke.Marcas.UIProcess.Model.Funcionario.ReadByID(val);
                    else
                        throw new Exception("Usuario No Registrado");
				}
				MySession.FuncionarioID = fun.Dat.ID.AsInt;

//				if( MySession.MenuHtlm == null )
//				{
//					MySession.MenuHtlm = UIPModel.Menu.ReadAsHTMLString();
//				}
			}

		}

		protected void Application_End( Object sender, EventArgs e ) {
		}
		
		protected void Application_EndRequest( Object sender, EventArgs e ) {
		}

		protected void Application_AuthenticateRequest( Object sender, EventArgs e ) {
		}
		#endregion
	}
}

