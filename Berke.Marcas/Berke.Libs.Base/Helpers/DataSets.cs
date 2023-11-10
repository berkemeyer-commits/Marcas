using System;

namespace Berke.Libs.Base.Helpers
{
	using System.Data;
	using System.Xml;
	using System.Text;
	using System.IO;

	/// <summary>
	/// DataSet Helper
	/// </summary>
	public class DataSets
	{
		public static string Serialize( DataSet dataSet ){
			return Serialize( dataSet, false );
		}

		public static string Serialize( DataSet dataSet, bool shouldCleanNS ){
			return Serialize( dataSet, shouldCleanNS, true );
		}
		
		private static string Serialize( DataSet dataSet, bool shouldCleanNS, bool shouldCleanNulls ){
			StringBuilder sb = new StringBuilder();
			if( shouldCleanNulls )
				Nulls.CleanDataSet( dataSet );
			dataSet.WriteXml( new XmlTextWriter( new StringWriter( sb ) ) );
			if( shouldCleanNS ){
				string ns = string.Format( @" xmlns=""{0}""", dataSet.Namespace );
				sb.Replace( ns , String.Empty );
			}
			return sb.ToString();
		}

		public static void Deserialize( DataSet dataSet, string s ){
			dataSet.ReadXml( new XmlTextReader( new StringReader( s ) ) );
		}
	}
}