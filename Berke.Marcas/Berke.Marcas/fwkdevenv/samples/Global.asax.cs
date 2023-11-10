using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace Framework.Sample
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(Object sender, EventArgs e)
		{
			System.Runtime.Remoting.RemotingConfiguration.Configure( Server.MapPath( "Web.config" ) );
		}
	}
}

