using System;
using System.Security.Principal;
using System.Web.Configuration;

namespace Berke.Libs.Base
{

	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Acceso
	{
		#region Datos Miembro

		private WindowsIdentity vWinIdentity;
		private string vUsuario;
		private int pos;

		#endregion Datos Miembro

        #region Constantes
        private const string DEFAULT_DOMAIN_NAME = "BERKE";
        #endregion Constantes

        #region Constructor

        public Acceso()
		{
			vWinIdentity = WindowsIdentity.GetCurrent();
			pos = vWinIdentity.Name.IndexOf(@"\");
			vUsuario = vWinIdentity.Name.Substring(pos+1);

//			if ( vUsuario == "nhcaceresa" || vUsuario == "jmfernandezg")
//			{
//				vUsuario = "lbwud";
//			}
		}

		#endregion Constructor

		#region CurrentDB
		public static string CurrentDB( )
		{
			return ( string ) Config.GetConfigParam("CURRENT_DATABASE");
		}
		#endregion CurrentDB

		#region CurrentServer
		public static string CurrentServer( )
		{	
			return ( string ) Config.GetConfigParam("CURRENT_SERVER");
		}
		#endregion CurrentServer

		#region GetCurrentUser
		public static string GetCurrentUser()
		{
            string vUsuario = "";
            if (Config.GetConfigParam("CURRENT_USER") == null)
            {
                int pos = 0;
                WindowsIdentity vWinIdentity = WindowsIdentity.GetCurrent();
                pos = vWinIdentity.Name.IndexOf(@"\");
                vUsuario = vWinIdentity.Name.Substring(pos + 1);
                //vUsuario = "mbromanr";
            }
            else vUsuario = (string)Config.GetConfigParam("CURRENT_USER");

            return vUsuario;
		}
		#endregion GetCurrentUser

		#region OperaciónPermitida 
		public static bool OperacionPermitida( string CodigoDeOperacion )
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER");
			return OperacionPermitida( CodigoDeOperacion, db );
		}

		public static bool chkOperacionPermitida(string codOp, string user, Berke.Libs.Base.Helpers.AccesoDB db)
		{
            //string domainName = WebConfigurationManager.AppSettings[GlobalConst.KEY_BERKE_DOMAIN_NAME].ToString();

            string domainName = DEFAULT_DOMAIN_NAME;

            if (WebConfigurationManager.AppSettings[GlobalConst.KEY_BERKE_DOMAIN_NAME] != null)
                domainName = WebConfigurationManager.AppSettings[GlobalConst.KEY_BERKE_DOMAIN_NAME].ToString();
            else
            {
                //System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
                //domainName = (string)configurationAppSettings.GetValue("berkeDomainName", typeof(string));
            }

            #region sql
            string comando = @" SELECT DISTINCT 
                                dbo.Operacion.Codigo
                                FROM dbo.Operacion 
                                INNER JOIN dbo.OperacionXGrupo 
                                    ON dbo.Operacion.ID = dbo.OperacionXGrupo.OperacionID
                                INNER JOIN dbo.UsuarioXGrupo 
                                    ON dbo.OperacionXGrupo.GrupoID = dbo.UsuarioXGrupo.GrupoID
                                INNER JOIN   dbo.Usuario 
                                    ON dbo.UsuarioXGrupo.UsuarioID = dbo.Usuario.ID
                                WHERE (dbo.Usuario.UsuarioDominio2 = @usuario ) 
                                AND dbo.Operacion.Codigo = @oper ";

            if ((domainName == Environment.UserDomainName) || (Config.GetConfigParam("CURRENT_USER") != null))
            {
                comando = @" SELECT DISTINCT 
                                dbo.Operacion.Codigo
                                FROM dbo.Operacion 
                                INNER JOIN dbo.OperacionXGrupo 
                                    ON dbo.Operacion.ID = dbo.OperacionXGrupo.OperacionID
                                INNER JOIN dbo.UsuarioXGrupo 
                                    ON dbo.OperacionXGrupo.GrupoID = dbo.UsuarioXGrupo.GrupoID
                                INNER JOIN   dbo.Usuario 
                                    ON dbo.UsuarioXGrupo.UsuarioID = dbo.Usuario.ID
                                WHERE (dbo.Usuario.Usuario = @usuario ) 
                                AND dbo.Operacion.Codigo = @oper ";
            }
			#endregion sql

			db.Sql = comando;
			db.ClearParams();
			db.AddParam("@usuario"	,System.Data.SqlDbType.NVarChar, user, 30);
			db.AddParam("@oper"		,System.Data.SqlDbType.NVarChar, codOp, 20);
			string val = (string)db.getValue();
			return (val == codOp );
		}

		
		public static bool OperacionPermitida( string CodigoDeOperacion, Berke.Libs.Base.Helpers.AccesoDB db )
		{
			int pos = 0;
			string vUsuario = "";
			WindowsIdentity vWinIdentity = WindowsIdentity.GetCurrent();
			pos = vWinIdentity.Name.IndexOf(@"\");
			vUsuario = vWinIdentity.Name.Substring(pos+1);

            if (vUsuario == "Gustavo") vUsuario = "gagaleanod";

			bool result = chkOperacionPermitida(CodigoDeOperacion, vUsuario,db);
			db.CerrarConexion();
			return result;			
		}

		#endregion OperaciónPermitida 

		#region GetCurrentMachineName
		public static string GetCurrentMachineName()
		{
			int pos = 0;
			string machineName = "";
			WindowsIdentity vWinIdentity = WindowsIdentity.GetCurrent();
			pos = vWinIdentity.Name.IndexOf(@"\");
			machineName = vWinIdentity.Name.Substring(0,pos);

			return machineName;
		}
		#endregion GetCurrentMachineName

		#region Propiedades

		public string Usuario 
		{
			get{ return vUsuario; }
		}	
	
		#endregion Propiedades

	
	}
}
