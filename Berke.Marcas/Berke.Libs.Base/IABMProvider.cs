namespace Berke.Libs.Base
{
	using System;
	using System.Data;
	
	public interface IABMProvider {
		void WriteTableID( string tableID );
		DataTable ReadByPattern( string searchText );
		DataSet ReadNew();
		DataSet ReadByID( object ID );
		string Save( DataSet dataSet );
	}
}
