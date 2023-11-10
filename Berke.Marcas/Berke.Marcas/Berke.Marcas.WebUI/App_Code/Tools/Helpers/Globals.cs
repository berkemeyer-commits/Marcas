using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace Berke.Marcas.WebUI
{
	/// <summary>
	/// Summary description for Globals.
	/// </summary>
	public class Globals {
		public const String APP_STTINGS_PREFIX = "RedIntercable.";
		public const String APP_STTINGS_SECTION = "RedIntercableSettings";

		static public String UrlWebSite {
			get {
				return SafeConfigString(APP_STTINGS_SECTION, "urlWebSite", string.Empty).Replace("^", "&");
			}
		}

		static public String ApplicationVRoot {
			get {
				return HttpContext.Current.Request.ApplicationPath;
			}
		}

		private static string SafeConfigUrl(string configSection, string configKey, string defaultValue) {
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configSection) as NameValueCollection;
			if ( configSettings != null ) {
				string configValue = configSettings[configKey] as string;
				if ( configValue != null ) {
					return Globals.ApplicationVRoot + configValue;
				}
			}

			return defaultValue;
		}

		private static string SafeConfigString(string configSection, string configKey, string defaultValue) {
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configSection) as NameValueCollection;
			if ( configSettings != null ) {
				string configValue = configSettings[configKey] as string;
				if ( configValue != null ) {
					return configValue;
				}
			}

			return defaultValue;
		}

		private static int SafeConfigNumber(string configSection, string configKey, int defaultValue) {
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configSection) as NameValueCollection;
			if ( configSettings != null ) {
				try {
					int configValue = Int32.Parse(configSettings[configKey]);
					return configValue;
				} catch {}
			}

			return defaultValue;
		}

		private static bool SafeConfigBoolean(string configSection, string configKey, bool defaultValue) {
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configSection) as NameValueCollection;
			if ( configSettings != null ) {
				try {
					bool configValue = bool.Parse(configSettings[configKey]);
					return configValue;
				} catch {}
			}

			return defaultValue;
		}


	}
}
