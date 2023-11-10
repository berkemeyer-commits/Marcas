using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Berke.Libs.Base.Helpers
{

	using System.Threading;
	
	/// <summary>
	/// Nulls: manages typed nulls for specialized datasets
	/// </summary>
	/// 



	public class Nulls
	{
		private static Int16 int16 = Int16.MinValue;
		private static int Int = Int32.MinValue;
		private static string String = "###NULL###";
		private static DateTime Date = DateTime.MinValue;
		private static Decimal Decimal = Decimal.MinValue;

		#region Format
		public static string Format( int value )
		{
			return IsNull( value ) ? String.Empty : string.Format( "{0:n}", value );
		}

		public static string Format( Int16 value )
		{
			return IsNull( value ) ? String.Empty : string.Format( "{0:n}", value );
		}

		public static string Format( string value )
		{
			return IsNull( value ) ? String.Empty : value;
		}

		public static string Format( DateTime value )
		{
			return IsNull( value ) ? String.Empty : string.Format( "{0:d}", value );
		}

		public static string Format( decimal value )
		{
			return IsNull( value ) ? String.Empty : string.Format( "{0:n}", value );
		}
		#endregion

		#region Parse
		public static int ParseInt( string value )
		{
			return ( value.Length == 0 ) ? Int : int.Parse( value );
		}

		public static decimal ParseDecimal( string value )
		{
			return ( value.Length == 0 ) ? Int : decimal.Parse( value );
		}

		#endregion

		#region IsNull
		public static bool IsNull( int value )
		{
			return value == Int;	   
		}

		public static bool IsNull( Int16 value )
		{
			return value == int16;	   
		}

		public static bool IsNull( string value )
		{
			return value == String;	   
		}

		public static bool IsNull( DateTime value )
		{
			return value == Date;	   
		}

		public static bool IsNull( Decimal value )
		{
			return value == Decimal;	   
		}

		public static bool IsNull( bool value )
		{
			return false ;	   
		}

		public static bool IsNull( object value )
		{
			if( value.GetType() == typeof(int) )
				return IsNull( (int) value );
			if( value.GetType() == typeof(string) )
				return IsNull( (string) value );
			if( value.GetType() == typeof(string) )
				return IsNull( (decimal) value );
			if( value.GetType() == typeof(decimal) )
				return IsNull( (string) value );
			if( value.GetType() == typeof(DateTime) )
				return IsNull( (DateTime) value );
			if( value.GetType() == typeof(bool) )
				return IsNull( (bool) value );
			if( value.GetType() == typeof(System.DBNull) )
				return true;
			throw new NotImplementedException("Tipo:" + value.GetType().Name);
		}
		#endregion

		#region  IDataReader
		public static int GetInt( IDataReader reader, int columnIx )
		{
			return reader.IsDBNull( columnIx ) ? Nulls.Int : reader.GetInt32( columnIx );
		}

		public static int GetInt16( IDataReader reader, int columnIx )
		{
			return reader.IsDBNull( columnIx ) ? Nulls.Int : reader.GetInt16( columnIx );
		}

		public static string GetString( IDataReader reader, int columnIx )
		{
			return reader.IsDBNull( columnIx ) ? Nulls.String : reader.GetString( columnIx );
		}

		public static DateTime GetDateTime( IDataReader reader, int columnIx )
		{
			return reader.IsDBNull( columnIx ) ? Nulls.Date : reader.GetDateTime( columnIx );
		}

		public static decimal GetDecimal( IDataReader reader, int columnIx )
		{
			return reader.IsDBNull( columnIx ) ? Nulls.Decimal : reader.GetDecimal( columnIx );
		}
		#endregion

		#region CleanDataSet
		public static void CleanDataSet( DataSet ds )
		{
			foreach( DataTable tbl in ds.Tables )
				CleanDataTable( tbl );
				
		}
		#endregion

		#region CleanDataTable
		public static void CleanDataTable( DataTable tbl )
		{
			foreach( DataRow dr in tbl.Rows )
				foreach( DataColumn col in tbl.Columns )
					if( IsNull( dr[ col ] ) )
						dr[ col ] = DBNull.Value;
		}

		#endregion
	}

	//============================================================

}
