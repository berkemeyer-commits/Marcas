using System;
using System.Collections;

#region DS class ( Serialacion.,Pack,xml.. )
namespace Berke.Libs.Base.DSHelpers
{
	using System.Data;
	using System.Xml;
	using System.Xml.Serialization;
	using System.Text;
	using System.IO;
	using Libs.Base.Helpers;

	/// <summary>
	/// DataSet Helper
	/// </summary>
	public class DS
	{
		#region Serialization

		public void Serialize( DataSet ds, string archivoNombre ) 
		{ 
			XmlSerializer ser = new XmlSerializer( typeof( DataSet ));
			System.IO.TextWriter writer = new System.IO.StreamWriter( archivoNombre );
			ser.Serialize( writer, ds );
			writer.Close();	
		}

		public static string Serialize( DataSet dataSet )
		{
			return Serialize( dataSet, false );
		}

		public static string Serialize( DataSet dataSet, bool shouldCleanNS )
		{
			return Serialize( dataSet, shouldCleanNS, true );
		}
		
		private static string Serialize( DataSet dataSet, bool shouldCleanNS, bool shouldCleanNulls )
		{
			StringBuilder sb = new StringBuilder();
			if( shouldCleanNulls )
				Nulls.CleanDataSet( dataSet );
			dataSet.WriteXml( new XmlTextWriter( new StringWriter( sb ) ) );
			if( shouldCleanNS )
			{
				string ns = string.Format( @" xmlns=""{0}""", dataSet.Namespace );
				sb.Replace( ns , String.Empty );
			}
			return sb.ToString();
		}

	


		public static void Deserialize( DataSet dataSet, string s )
		{
			dataSet.ReadXml( new XmlTextReader( new StringReader( s ) ) );
		}
		#endregion

		#region Pack-UnPack


		public static DataSet Pack( string s , int i )
		{
			ParamDS ds = new ParamDS();
			ds.SimpleString.AddSimpleStringRow ( s );
			ds.SimpleInt.AddSimpleIntRow( i );
			return ds;
		}


		public static DataSet Pack( bool b )
		{
			ParamDS ds = new ParamDS();
			ds.SimpleBoolean.AddSimpleBooleanRow ( b );
			return ds;
		}


		public static DataSet Pack( string s )
		{
			ParamDS ds = new ParamDS();
			ds.SimpleString.AddSimpleStringRow ( s );
			return ds;
		}

		public static DataSet Pack( int i )
		{
			ParamDS ds = new ParamDS();
			ds.SimpleInt.AddSimpleIntRow( i );
			return ds;
		}

		public static DataSet Pack ( DateTime d )
		{
			ParamDS ds = new ParamDS();
			ds.SimpleDate.AddSimpleDateRow( d );
			return ds;
		}

		public static int UnpackInt( DataSet ds )
		{
			ParamDS pds = ( ParamDS ) ds;
			return pds.SimpleInt[ 0 ].Value;
		}

		public static string UnpackString( DataSet ds )
		{
			ParamDS pds = ( ParamDS ) ds;
			return pds.SimpleString[ 0 ].Value;
		}

		public static Boolean UnpackBoolean( DataSet ds)
		{
			ParamDS pds = ( ParamDS ) ds;
			return pds.SimpleBoolean[ 0 ].Value;
		}

		#endregion

		#region Datos-en-Xml


		public  static void fillFromXmlFile( System.Data.DataSet DS, String nombreArchivo )
		{
			//===== Carga un DataSet con datos contenidos en un archivo Xml 
			System.IO.FileStream fsXml;
			fsXml = new System.IO.FileStream( nombreArchivo, System.IO.FileMode.Open, System.IO.FileAccess.Read );
			System.Xml.XmlTextReader xmlReader = new System.Xml.XmlTextReader( fsXml);
			DS.Clear();
			DS.ReadXml( xmlReader, System.Data.XmlReadMode.Auto );
			xmlReader.Close();	

			fsXml.Close();
		
		} 

		public  static void saveToXmlFile( System.Data.DataSet DS, String nombreArchivo )
		{
			//===== Graba los datos del DataSet a un archivo Xml 
			System.IO.FileStream fsXml;
			fsXml = new System.IO.FileStream(nombreArchivo, System.IO.FileMode.Create, System.IO.FileAccess.Write);
			System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter( fsXml, System.Text.Encoding.Unicode );
			DS.WriteXml( xmlWriter, System.Data.XmlWriteMode.IgnoreSchema  );
			fsXml.Close();
		}



		public  static void cargarDatosDeGuau( System.Data.DataSet DS, String nombreArchivo )
		{
			try
			{
				fillFromXmlFile( DS, nombreArchivo );
				foreach ( System.Data.DataTable tab in DS.Tables )
				{
					TableGateway gw = new TableGateway( tab );
					for( gw.Go(0); gw.Row() < gw.RowCount(); gw.Skip() )
					{
						foreach( System.Data.DataColumn col in tab.Columns )
						{
							if ( col.DataType.Name == "String" )
							{
								if ( ! gw.IsNull( col.ColumnName ))
								{
									gw.SetValue(col.ColumnName, gw.AsString(col.ColumnName ) + "*" );
								}
							}
						}	
						
					}
				}
			}
			catch( Exception e )
			{
				throw new Exception("*Error en datos de Gua'u. :"+ e.Message );
			}
		}

		#endregion Datos-en-Xml

	}

} // namespace Berke.Libs.Base.DSHelpers

#endregion DS  class 

#region Gateway sobre DataTable ( DSRow, DSTab, ObjConvert.. )

namespace Berke.Libs.Base.DSHelpers 
{
	using System.Data;

	#region DSRowComparer

	public class DSRowComparer : IComparer
	{
	
		ArrayList _Ord = null;

		public DSRowComparer( ArrayList Ord)
		{
			_Ord = Ord;
		}
	
		public int Compare( object obj1, object obj2)
		{
			try
			{
				int result = 0;
				DataRow r1 = (DataRow) obj1;
				DataRow r2 = (DataRow) obj2;
				for(int i=0; i < _Ord.Count; i++)
				{
					Par par = (Par) _Ord[i];
					object val1 = r1[ par.Idx];
					object val2 = r2[ par.Idx];
					result = ObjConvert.Compare( val1, val2 );

					if( result != 0 )
					{
						result = ( par.Sentido == DSOrden.Ascendente ) ? result : - result;
						break;
					}
				}
				return result;
			}
			catch ( Exception ex ){
				throw new Exception(this.GetType().Name+": Error en la comparacion", ex );
			}
		
		}

	
	}

	#endregion DSRowComparer

	#region Par

	internal enum DSOrden { Ascendente, Descendente }

	internal class Par : IComparable
	{

		#region Datos miembros
		
		int _idx;
		int _orden;
		DSOrden _sent;

		#endregion Datos miembros
	
		#region Constructor

		public Par( int indice, int orden )
		{
			_idx = indice;
			if( orden > 0 )
			{
				_sent = DSOrden.Ascendente;
				_orden = orden;
			}
			else
			{
				_sent = DSOrden.Descendente;
				_orden = -orden;
			}
		}

		#endregion Constructor

		#region Propiedades
		
		public int Idx		{ get { return _idx;	}}
		public int Orden	{ get { return _orden;	}}
		public DSOrden Sentido	{ get { return _sent;	}}

		#endregion Propiedades

		#region Metodo CompareTo()

		public int CompareTo( object obj )
		{
			Par p = (Par) obj;
			return ( _orden - p._orden );	
		}

		#endregion Metodo CompareTo()
	}

	#endregion Par

	#region DSFilter
	public class DSFilter
	{
		#region Datos Miembros

		private Object _minValue;
		private Object _maxValue;
		private Object _value;
		private ArrayList _lst;
		private string _oper;
		private string _fieldName;

		#endregion Datos Miembros
	
		#region Constructores y Afines
		
		public DSFilter()
		{
			this.InitList();
		}
	
		public DSFilter( string oper,  string operando, string dummy )
		{
			_oper	= oper;
			_fieldName = operando;
		}

		public DSFilter( string oper,  Berke.DG.Base.Field field )
		{
			_oper	= oper;
			_fieldName = field.Name;
		}

		public DSFilter( Object Valor  )
		{
			_value = Valor;
		}


		public DSFilter( Object LimiteInferior, Object LimiteSuperior   )
		{
			#region LimiteInferior
			if( LimiteInferior is string )
			{
				if( (string) LimiteInferior == "" ){
					LimiteInferior = DBNull.Value;
				}
			}
			#endregion LimiteInferior

			#region LimiteSuperior
			if( LimiteSuperior is string )
			{
				if( (string) LimiteSuperior == "" )
				{
					LimiteSuperior = DBNull.Value;
				}
			}
			#endregion LimiteSuperior

			if( LimiteInferior == null) LimiteInferior = DBNull.Value;
			if( LimiteSuperior == null) LimiteSuperior = DBNull.Value;

			if( LimiteInferior is DBNull && LimiteSuperior is DBNull  ){
				;
			}
			else{
				if( !(LimiteInferior is DBNull) &&  !(LimiteSuperior is DBNull) )
				{
					_minValue = LimiteInferior;
					_maxValue = LimiteSuperior;
				}
				else{
					if( LimiteInferior is DBNull ){ _value = LimiteSuperior ; }
					else{_value = LimiteInferior; }
				}
			}
		}



		public DSFilter( ArrayList ListaDeValores )
		{
			_lst = ListaDeValores;
		}

		#endregion Constructores y Afines
		

		#region Propiedades

		public Object Value	{ get{ return _value;} }
		public Object MinValue { get{ return _minValue;} }
		public Object MaxValue { get{ return _maxValue;} }
		public Object List { get{ return _lst;} }
		public string Oper{ get { return this._oper;}}
		public string OperFieldName{ get {return this._fieldName; }}

		#endregion Propiedades
	

		#region Metodos

		public void	InitList(){ _lst = new ArrayList();}
		public void AddToList( Object Valor ){ _lst.Add( Valor ); }

		#endregion Metodos


	}



	#endregion DSFilter

	#region DSRowField  Class

	public struct  DSRowField	// Se utiliza esta clase para devolver los elementos de DSRow
	{							// El objetivo es hacer conversion implicita segun el tipo del
		// destinatario
		#region Datos Miembro
	
		Object dato;
				
		#endregion Datos Miembro

		#region Constructor
	
		public	DSRowField( Object pDato )
		{
			dato = pDato;
		}

		#endregion Constructor

		#region Conversiones Implicitas

		public static implicit operator Boolean	( DSRowField f ){ return Convert.ToBoolean(f.dato);	}
		public static implicit operator Byte	( DSRowField f ){ return Convert.ToByte(f.dato);	}
		public static implicit operator DateTime( DSRowField f ){ return Convert.ToDateTime(f.dato);}
		public static implicit operator Decimal	( DSRowField f ){ return Convert.ToDecimal(f.dato);	}
		public static implicit operator Double	( DSRowField f ){ return Convert.ToDouble(f.dato);	}
		public static implicit operator Single	( DSRowField f ){ return Convert.ToSingle(f.dato);	}
		public static implicit operator Int16	( DSRowField f ){ return Convert.ToInt16(f.dato);	}
		public static implicit operator Int32	( DSRowField f ){ return Convert.ToInt32(f.dato);	}
		public static implicit operator Int64	( DSRowField f ){ return Convert.ToInt64(f.dato);	}
		public static implicit operator String	( DSRowField f ){ return Convert.ToString(f.dato);	}
		
		#endregion Conversiones Implicitas
		

	}
	#endregion DSRowField Class

	#region ColInfo  Class

	public class ColInfo
	{
		#region Datos Miembro

		string	_colName;
		string	_colType;
		bool	_isAutoIncrement;
		bool	_isPK;
		bool	_isReadOnly;

		#endregion Datos Miembro	
		
		#region Constructores
		
		public ColInfo()
		{
			_colName			=	null;
			_colType			=	null;
			_isAutoIncrement	=	false;
			_isPK				=	false;
			_isReadOnly			=	false;
		}
		
		public ColInfo( string colName, string colType, bool isAutoIncrement, bool isPK, bool isReadOnly )	
		{
			_colName			=	colName;
			_colType			=	colType;
			_isAutoIncrement	=	isAutoIncrement;
			_isPK				=	isPK;
			_isReadOnly			=	isReadOnly;
		}


		#endregion Constructores
		
		#region Propiedades

		public string	ColName	{	get { return _colName;	}}
		public string	ColType	{	get { return _colType;	}}
		public bool		IsAutoIncrement	{	get { return _isAutoIncrement; }}
		public bool		IsPK	{	get { return _isPK; }}
		public bool		IsReadOnly	{	get { return _isReadOnly; }}

		#endregion Propiedades
		
		#region Metodos
		
		public ColInfo Copy()
		{
			return new ColInfo( this._colName, this._colType, this._isAutoIncrement, this._isPK, this._isReadOnly );
		}

		#endregion Metodos

	}


	#endregion ColInfo  Class

	#region DSRow class

	public class DSRow
	{
	
		#region Datos Miembro

		Object[] _val;
		int _nCols;		
		ColInfo[] _col;
		int _pkIndex;
		string _pkColName;

		#endregion Datos Miembro	

		#region Constructores y Afines

		public DSRow ()
		{
			_val		= null;
			_nCols		= 0;
			_col		= null;
			_pkIndex	= -1;
			_pkColName	= "";
		}

		public DSRow ( DataTable tab ) 
		{
			Init( tab );
		}

		public void Init ( DataTable tab )	
		{
			_nCols	= tab.Columns.Count;
			_val	= new Object[_nCols];
			_col	= new ColInfo[_nCols];

			int i = 0;
			DataColumn[] pk = ((DataColumn[]) (tab.PrimaryKey));
			if( pk.Length > 0 )
			{
				if( pk.Length > 1 ) throw new Exception(" DSRow no se aplica para Clave Primaria Compuesta");
				_pkColName = pk[0].ColumnName;
			}
			i = 0;
			foreach( DataColumn col in tab.Columns )
			{
				bool bpk = ( col.ColumnName == _pkColName );
				if( bpk ) _pkIndex = i;
				

				_col[i]	=	new ColInfo( col.ColumnName, col.DataType.Name, col.AutoIncrement, bpk, col.ReadOnly );
				_val[i]	=	DBNull.Value;
				i++;
			}

		}

		#endregion Constructores y Afines

		#region Indexers

		private void setValue( int idx, Object val )
		{
			ObjConvert.SetValue( ref _val[ idx ], val, this._col[idx].ColType );
		}


		public Object this[ int idx ]
		{
			get{ return _val[ idx ];}
			set{ setValue( idx , value); }
		}

		public Object this[ string nombreCampo ]
		{
			get
			{ 
				int idx = IndexOf(nombreCampo);
				if( idx == -1 ) throw new Exception( this.GetType().Name + " Error en nombre de columna '"+nombreCampo+"'");
				return _val[ idx ];
			}
			set
			{ 
				int idx = IndexOf(nombreCampo);
				if( idx == -1 ) throw new Exception( this.GetType().Name + " Error en nombre de columna '"+nombreCampo+"'");
				setValue( idx , value);
			}
		}

		#endregion Indexers

		#region Propiedades

		public int numCols{ get { return _nCols;}}

		public int PkIndex{ get { return _pkIndex;} }

		public string PkColumnName{ get { return _pkColName;} }

		public ColInfo[] Column { get { return this._col; }}

		public Object[] Values { get { return this._val; }}

		#endregion Propiedades

		#region Metodos

		#region Equals

		public  override bool Equals( Object r )
		{
			//			if( this._nCols != this._nCols ) return false;
			//
			//			for( int i=0; i < this._nCols; i++) {
			//				if( this._col[i].ColName.ToUpper() != r._col[i].ColName.ToUpper() ) return false;
			//				if( this._col[i].ColType != r._col[i].ColType) return false;
			//				if( this._val[i] != r._val[i] ) return false;
			//			}
			//			return true;
			if( this.GetHashCode() == ((DSRow)r).GetHashCode()) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public new static bool Equals( Object obj1 , Object obj2 )
		{
			return ( (DSRow)obj1).Equals( (DSRow) obj2);
		} 

		#endregion Equals

		#region GetHashCode
		public override int GetHashCode()
		{
			int ret = this._nCols.GetHashCode();
			for( int i=0; i < this._nCols; i++) 
			{
				ret ^= this._col[i].ColName.ToUpper().GetHashCode();
				ret ^= this._col[i].ColType.GetHashCode();
				ret ^= this._val[i].GetHashCode() + i;
			}
			return ret;
		}
		#endregion GetHashCode

		#region Clear()

		public  void Clear()
		{
			for( int i=0; i < this._nCols; i++)
			{
				_val[i]		=	DBNull.Value;
			}
		}

		#endregion Clear

		#region IndexOf()
		public int IndexOf( string nombreCampo )
		{
			for ( int i = 0 ; i < this._nCols; i++ )
			{
				if ( _col[i].ColName.ToUpper() == nombreCampo.ToUpper() ) return i;
			}
			return -1;
		}
		#endregion IndexOf

		#region Copy()
		public	DSRow Copy()
		{
			DSRow cpy		= new DSRow();

			cpy._nCols	= this._nCols;
			cpy._val	= new Object[_nCols];
			cpy._col	= new ColInfo[_nCols];
			for( int i=0; i < this._nCols; i++)
			{
				cpy._nCols	=	this._nCols;
				cpy._col[i]	=	this._col[i].Copy();
				cpy._val[i]	=	this._val[i];
			}

			return cpy;
		}
		#endregion Copy()



		#endregion Metodos

		#region Operadores

		public static bool operator==( DSRow r1, DSRow r2 )
		{
			return r1.Equals(r2);
		}

		public static bool operator!=( DSRow r1, DSRow r2 )
		{
			return !r1.Equals(r2);
		}

		#endregion Operadores


	}

	#endregion DSRow class

	#region DSTab  Class
	
	public enum TabMode { Read, Insert, Edit }

	public class DSTab 
	{
		#region Datos Miembro

		DataTable _dt;
		DSRow _dat;
		DSRow _old;
		DSRow _filt;

		ArrayList _Ord;

		private TabMode _mode;
		int _currentRow;
		bool _FilterOn;

		int _generatedID;
		bool _autogenerateOn;
		

		#endregion Datos Miembro	

		#region Constructores y Afines

		private void Init()
		{
			_dt = null;
			_dat= null; 
			_old= null; 
			_filt= null;
			_mode = TabMode.Read;
			_currentRow = -1;
			_FilterOn = false;
			_Ord = null;
			_generatedID = 0;

			_autogenerateOn = true;
		
		}

		public DSTab()
		{
			Init();
		}

		public DSTab( DataTable tab)
		{
			Bind( tab );
		}

		public void Bind(  DataTable tab ) 
		{
			Init();
			_dt  =	tab;
			_dat =	new DSRow( tab ); 
			_old =  new DSRow( tab ); 
			_filt=	new DSRow( tab );
			_Ord =	new ArrayList();
			GoTop();		
		}

		public void Clear( )
		{
			Init();
			_dt.Clear();
		}

		#endregion Constructores y Afines

		#region Propiedades

		#region Acceso a datos privados

		public ArrayList	OrderList	{	get{ return _Ord;} }
		
		public DSRow		Dat			{	get{ return _dat;	} set{ _dat = value;}	}
		public DSRow		Old			{	get{ return _old;	}	}
		public DSRow		Filter		{	get{ return _filt;	}	}
		public DataTable	Table		{	get{ return _dt;	} set{ _dt = value; } }
		public int			RowIndex	{	get{ return _currentRow ;} }
		public TabMode		Mode		{	get{ return _mode; }}
		
																    
			#endregion Acceso a datos privados

			#region Informacion de Estado

			public bool IsEmpty	
				   { 
			get
			{
				if ( _dt == null) return true;
				return  _dt.Rows.Count <= 0;
			}
		}

		public bool EOF	{ 
			get
			{ 
				if ( _dt == null) return true;
				return  _currentRow >= _dt.Rows.Count; 
			}
		}

		public Int32 RowCount {	get{ return _dt.Rows.Count;}}

		public DataRowState RowState{ get{	return _dt.Rows[_currentRow].RowState;	}}
		
		public bool IsRowAdded		{
			get{	return _dt.Rows[_currentRow].RowState == DataRowState.Added ||
						_dt.Rows[_currentRow].RowState == DataRowState.Detached;	
			}
		}
		public bool IsRowDeleted	{ get{	return _dt.Rows[_currentRow].RowState == DataRowState.Deleted ;	}}
		public bool IsRowModified	{ get{	return _dt.Rows[_currentRow].RowState == DataRowState.Modified ||
												_dt.Rows[_currentRow].RowState == DataRowState.Detached	; }}
		
		#endregion Informacion de Estado

		#endregion Propiedades

		#region Metodos

		public void DisableConstraints(){
			_autogenerateOn = false;
			_dt.Constraints.Clear();	
		}

		#region Obtener valores de Campos


		public Byte[]	AsBinary( string campo )	{	return AsBinary( Dat.IndexOf(campo), false );	}
		public bool		AsBoolean( string campo )	{	return AsBoolean( Dat.IndexOf(campo), false );	}
		public Byte		AsByte( string campo )		{	return AsByte( Dat.IndexOf(campo), false );	}
		public DateTime	AsDateTime( string campo )	{	return AsDateTime( Dat.IndexOf(campo), false );	}
		public Decimal	AsDecimal( string campo)	{	return AsDecimal( Dat.IndexOf(campo), false );	}
		public Double	AsDouble( string campo )	{	return AsDouble( Dat.IndexOf(campo), false );	}
		public Guid		AsGuid( string campo )		{	return AsGuid( Dat.IndexOf(campo), false );	}
		public int		AsInt( string campo )		{	return AsInt( Dat.IndexOf(campo), false );	}
		public long		AsLong( string campo )		{	return AsLong( Dat.IndexOf(campo), false );	}
		public Object	AsObject( string campo )	{	return AsObject( Dat.IndexOf(campo), false );	}
		public short	AsShort( string campo )		{	return AsShort( Dat.IndexOf(campo), false );	}
		public Single	AsSingle( string campo )	{	return AsSingle( Dat.IndexOf(campo), false );	}
		public String	AsString( string campo )	{	return AsString( Dat.IndexOf(campo), false );	}



		public Byte[]	AsBinary( string campo, bool getOldVersion )	{	return AsBinary( Dat.IndexOf(campo), getOldVersion);	}
		public bool		AsBoolean( string campo, bool getOldVersion )	{	return AsBoolean( Dat.IndexOf(campo), getOldVersion );	}
		public Byte		AsByte( string campo, bool getOldVersion )		{	return AsByte( Dat.IndexOf(campo), getOldVersion );	}
		public DateTime	AsDateTime( string campo, bool getOldVersion )	{	return AsDateTime( Dat.IndexOf(campo), getOldVersion );	}
		public Decimal	AsDecimal( string campo, bool getOldVersion )	{	return AsDecimal( Dat.IndexOf(campo), getOldVersion );	}
		public Double	AsDouble( string campo, bool getOldVersion )	{	return AsDouble( Dat.IndexOf(campo), getOldVersion );	}
		public Guid		AsGuid( string campo, bool getOldVersion )		{	return AsGuid( Dat.IndexOf(campo), getOldVersion );	}
		public int		AsInt( string campo, bool getOldVersion )		{	return AsInt( Dat.IndexOf(campo), getOldVersion );	}
		public long		AsLong( string campo, bool getOldVersion )		{	return AsLong( Dat.IndexOf(campo), getOldVersion );	}
		public Object	AsObject( string campo, bool getOldVersion )	{	return AsObject( Dat.IndexOf(campo), getOldVersion );	}
		public short	AsShort( string campo, bool getOldVersion )		{	return AsShort( Dat.IndexOf(campo), getOldVersion );	}
		public Single	AsSingle( string campo, bool getOldVersion )	{	return AsSingle( Dat.IndexOf(campo), getOldVersion );	}
		public String	AsString( string campo, bool getOldVersion )	{	return AsString( Dat.IndexOf(campo), getOldVersion );	}


		public Byte[]	AsBinary( int idx, bool getOldVersion )		
		{
			if ( getOldVersion )
			return ObjConvert.AsBinary( this.Old[idx] );	
			else
			return ObjConvert.AsBinary( this.Dat[idx] );	
		}

		public bool		AsBoolean( int idx , bool getOldVersion )	{
			if ( getOldVersion )
				return ObjConvert.AsBoolean( this.Old[idx] );	

			else
				return ObjConvert.AsBoolean( this.Dat[idx] );	
		}

		public Byte		AsByte( int idx, bool getOldVersion  )		{	
			if ( getOldVersion )
				return ObjConvert.AsByte( this.Old[idx] );	

			else
				return ObjConvert.AsByte( this.Dat[idx] );	
		}

		public DateTime	AsDateTime( int idx, bool getOldVersion  )	{
			if ( getOldVersion )
				return ObjConvert.AsDateTime( this.Old[idx] );	

			else
				return ObjConvert.AsDateTime( this.Dat[idx] );	
		}

		public Decimal	AsDecimal( int idx, bool getOldVersion  )	{
			if ( getOldVersion )
				return ObjConvert.AsDecimal( this.Old[idx] );	

			else
				return ObjConvert.AsDecimal( this.Dat[idx] );	
		}

		public Double	AsDouble( int idx, bool getOldVersion  )	{
			if ( getOldVersion )
				return ObjConvert.AsDouble( this.Old[idx] );	

			else
				return ObjConvert.AsDouble( this.Dat[idx] );	
		}

		public Guid		AsGuid( int idx , bool getOldVersion )		{
			if ( getOldVersion )
				return ObjConvert.AsGuid( this.Old[idx] );	

			else
				return ObjConvert.AsGuid( this.Dat[idx] );	
		}

		public int		AsInt( int idx, bool getOldVersion  )		{
			if ( getOldVersion )
				return ObjConvert.AsInt( this.Old[idx] );	

			else
				return ObjConvert.AsInt( this.Dat[idx] );	
		}

		public long		AsLong( int idx, bool getOldVersion  )		{
			if ( getOldVersion )
				return ObjConvert.AsLong( this.Old[idx] );	

			else
				return ObjConvert.AsLong( this.Dat[idx] );	
		}

		public Object	AsObject( int idx, bool getOldVersion   )	{
			if ( getOldVersion )
				return ObjConvert.AsObject( this.Old[idx] );	

			else
				return ObjConvert.AsObject( this.Dat[idx] );	
		}

		public short	AsShort( int idx, bool getOldVersion   )		{
			if ( getOldVersion )
				return ObjConvert.AsShort( this.Old[idx] );	

			else
				return ObjConvert.AsShort( this.Dat[idx] );	
		}

		public Single	AsSingle( int idx, bool getOldVersion  )	{
			if ( getOldVersion )
				return ObjConvert.AsSingle( this.Old[idx] );	

			else
				return ObjConvert.AsSingle( this.Dat[idx] );	
		}

		public String	AsString( int idx, bool getOldVersion  )	{
			if ( getOldVersion )
				return ObjConvert.AsString( this.Old[idx] );	

			else
				return ObjConvert.AsString( this.Dat[idx] );	
		}

	
		public bool	IsNull( string campo )	{
			return ObjConvert.IsNull( this.Dat[campo] );	
		}

		#endregion Obtener valores de Campos

		#region Filtrado de Filas

		public void FilterOn()	{ _FilterOn = true;		}
		public void FilterOff()	{ _FilterOn = false;	}

		public void ClearFilter() { _filt.Clear();	}
	
		private bool FilterOk(){
			if( _FilterOn == false ) return true;
			bool result = true;

			for( int i = 0 ; i < _filt.numCols && result ; i++){
				Object filObj = _filt[i];

				if( ObjConvert.IsNull( filObj )  ){ continue; }

				if( filObj is DSFilter )
				{
					DSFilter fil  = (DSFilter) filObj;

					#region Single Value
					if( fil.Value != null )
					{
						if( ObjConvert.Compare( _dat[i], fil.Value ) != 0 ){	result = false;	}						
					}		
					#endregion Single Value
	
					#region Range 
					if( fil.MinValue != null )
					{
						if( _dat[i] == DBNull.Value){
							result = false;
						}else{
							if( ObjConvert.Compare(_dat[i], fil.MinValue ) < 0 || 
								ObjConvert.Compare(_dat[i], fil.MaxValue ) > 0 )
							{ 
						
								result = false;
							}
						}
					}
					#endregion Range 

					#region Lista 
					if( fil.List != null ) {
						
						if ( ! ((ArrayList)fil.List).Contains( _dat[i] ) )
						{
							result = false;	
						}


					}
					#endregion Lista 
				}
				else // Objeto Normal
				{
					if( ObjConvert.Compare( _dat[i], _filt[i]) != 0 ){	result = false;	}
				}
			
			
			}
			return result;
		}


		#endregion Filtrado de Filas

		#region Mover entre Filas 

		#region SKIP
	
		public void Skip( )	{	
			_currentRow++;	
			FillBuffer();
			SkipFilteredRows();		

		}

		public void Skip( int avance )	{
			_currentRow+= avance;	
			FillBuffer();
			SkipFilteredRows();


		}
		
		private void SkipFilteredRows()
		{
			if( _FilterOn )
			{
				while ( !EOF && ! FilterOk()  )
				{
					_currentRow++;	
					FillBuffer();
				}
			}
		}


		#endregion SKIP

		#region GO, GoTop

		public void Go( int pos ) {
			_currentRow = pos;
			FillBuffer();
		}

		public void GoTop(){
			_currentRow = 0;
			if( IsEmpty ) return;
			FillBuffer();
			SkipFilteredRows();
		}


		#endregion GO, GoTop

		#endregion Mover entre Filas

		#region Buffer de Filas

		private void FillBuffer(){
			if( this.EOF ){
				for( int i = 0; i < _dat.numCols; i++ )
				{
					_dat[i] = DBNull.Value;		
					_old[i] = DBNull.Value;		
				}
				return;
			}

			for( int i = 0; i < _dat.numCols; i++ ){
				if( _dt.Rows[_currentRow].RowState != DataRowState.Deleted ) {
					_dat[i] = _dt.Rows[_currentRow][i, System.Data.DataRowVersion.Current];		
				}
				else{
						_dat[i] = DBNull.Value;	
				}
			}

			if(this._mode == TabMode.Edit || this._mode == TabMode.Read ) {

				for( int i = 0; i < _dat.numCols; i++ )	{
					if( _dt.Rows[_currentRow].RowState == DataRowState.Deleted ||
						_dt.Rows[_currentRow].RowState == DataRowState.Modified )
					{
						_old[i] = _dt.Rows[_currentRow][i, System.Data.DataRowVersion.Original];		
					}
					else
					{
						_old[i] = _dt.Rows[_currentRow][i, System.Data.DataRowVersion.Current];							
					}
				}

			}else { // TabMode.Insert
				for( int i = 0; i < _dat.numCols; i++ )	{
						_old[i] = DBNull.Value;
				}
			}
		}

		private void PostBuffer(){
			for( int i = 0; i < _dat.numCols; i++ )	{
				if( ! _dt.Columns[i].ReadOnly )
				{
					_dt.Rows[_currentRow][i] = _dat[i];
				}
			}
	
		}

		#endregion Buffer de Filas

		#region Insertar Fila

		public void NewRow(){
			_mode = TabMode.Insert;
			_dat.Clear();
		}

		public void PostNewRow(){
			if( this._mode != TabMode.Insert){ throw new Exception("PostNewRow() requiere el modo 'Insert' activo "); }

			#region Asigna valores a campos AutoIncrementados
			for( int i = 0; i < _dat.numCols; i++)
			{
				
				if( ( _dat.Column[i].IsAutoIncrement || _dat.Column[i].IsPK ) ){

					if( ! _autogenerateOn)	{
						_dt.Columns[i].AutoIncrement = false;
						_dt.Columns[i].ReadOnly = false;
						_dt.Columns[i].Unique = false;
						_dt.Columns[i].AllowDBNull = true;
					}else{
						if( _dat[i] == DBNull.Value ) {
							_dat[i] = --_generatedID;
						}					
					}
				}
			}
			#endregion

			_dt.Rows.Add( _dat.Values );
			_currentRow = _dt.Rows.Count - 1;
			this.Skip(0);

			_mode = TabMode.Read;

		}

		#endregion Insertar Fila

		#region Modificar Filas

		public void Edit()
		{
			_mode = TabMode.Edit;
			FillBuffer();
		}

		public void PostEdit()
		{
			if( this._mode != TabMode.Edit ){ throw new Exception("PostEdit() requiere el modo 'Edit' activo "); }
			PostBuffer();
			_mode = TabMode.Read;
		}

		public void CancelEdit()
		{
			if( this._mode != TabMode.Edit ){ throw new Exception("CancelEdit() requiere el modo 'Edit' activo "); }
			FillBuffer();
			_mode = TabMode.Read;
		}

		#endregion Modificar Filas

		#region Eliminar Filas

		public void Delete(){
			_dt.Rows[_currentRow].Delete();
			Skip( 0 );
		}

		#endregion Eliminar Filas

		#region Sort

		public void SetOrder( String Campo, int Orden ){
			int idx = _dat.IndexOf( Campo );
			if( idx == -1 ){
				throw new Exception( Campo + " no registrado en DataTable " + this.Table.TableName );
			}
			SetOrder( idx, Orden );
		}

		public void SetOrder( int idx, int Orden )
		{
			_Ord.Add( new Par( idx, Orden ));
			_Ord.Sort();
		}

		public void ClearOrder()
		{
			_Ord = new ArrayList();
		}


		public void Sort(){
			if( this.Table.Rows.Count < 2 ) return;
			bool filterOn = this._FilterOn;

			_FilterOn = false;
			
			DataTable dTab = this.Table.Clone();

			ArrayList lst = this.ArrayListOfDataRows();
			lst.Sort( new DSRowComparer( this._Ord ) );
		
			for( int i=0; i < lst.Count; i++ ){
				dTab.ImportRow((DataRow) lst[i]);
			}

			this.Table = dTab.Copy();

			_FilterOn = filterOn;
			this.Go(0);
		} 


		#endregion Sort

		#region Obtener filas en forma masiva
		
		private ArrayList ArrayListOfDataRows(){
			ArrayList lst = new ArrayList();
			for( this.GoTop() ; ! this.EOF ; this.Skip() ) {
				DataRow r = this.Table.Rows[this.RowIndex]; // Copia la referencia
				lst.Add( r );
			}
			return lst;
		}

		#endregion Obtener filas en forma masiva


		#region AcceptChanges

		public void AcceptAllChanges()
		{
			_dt.AcceptChanges();
		}


		public void AcceptRowChanges()
		{
			_dt.Rows[this._currentRow].AcceptChanges();
		}


	

		#endregion AcceptChanges

		#endregion Metodos

	}// class DSTab

	#endregion DSTab Class

}//namespace Berke.Libs.Base.DSHelpers 



#endregion Gateway sobre DataTable
