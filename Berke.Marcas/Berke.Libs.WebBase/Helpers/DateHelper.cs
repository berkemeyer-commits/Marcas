using System;

namespace Berke.Libs.WebBase.Helpers {
	using System.Threading;
	/// <summary>
	/// DateHelper
	/// </summary>
	public class DateHelper {

		#region Format
		public static string Format( DateTime dateTime ) {
		
			return dateTime.ToString( "d", Thread.CurrentThread.CurrentCulture );
		}

		public static string FormatUI( DateTime dateTime ) {
			return dateTime.ToString( "d", Thread.CurrentThread.CurrentUICulture );
		}
		#endregion
	}
}
