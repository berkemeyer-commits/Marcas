using System;

namespace Berke.Marcas.WebUI.Tools.Helpers
{
	using System.Web;

	public class UrlParam
	{
		private String _paramString = "";


		public  void AddParam( String key , String valor )
		{
			if ( _paramString == String.Empty )
				_paramString  = "?" + key + "=" + valor;
			else
				_paramString += "&" + key + "=" + valor;
		}


		public static String GetParam(String key) 
		{ 
			Object obj = HttpContext.Current.Request.QueryString[key];
			if( obj == null )
				return "";
			else
				return  (String) obj;
		}

		public void redirect( String pagina ){
			HttpContext.Current.Response.Redirect(pagina + _paramString);
		}
	}

}
