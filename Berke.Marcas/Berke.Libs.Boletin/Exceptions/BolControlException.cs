using System;

namespace Berke.Libs.Boletin.Exceptions
{
	/// <summary>
	/// Summary description for BolImportException.
	/// </summary>
	public class BolControlException : Exception
	{
		public BolControlException() : base()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public BolControlException(string message) : base(message)
		{
		}
	}
}
