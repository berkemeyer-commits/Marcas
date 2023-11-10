using System;

namespace Berke.Libs.Base
{	
	#region Formatters
	/// <summary>
	/// Formatters
	/// </summary>
	public class Formatters
	{
		public static string Format( bool value ){
			return value ? "Si" : ""; 
		}

		public static string Format( decimal value ){
			return "";
		}

	}
	#endregion Formatters
	

}
