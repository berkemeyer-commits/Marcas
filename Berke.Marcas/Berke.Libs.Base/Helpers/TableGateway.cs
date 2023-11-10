using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Threading;

namespace Berke.Libs.Base.Helpers
{
	
	#region Table_Gateway

	internal struct ColumnItem
	{
		public string name;
		public string currentValue;
		public string oldValue;
		public string tipo;
		public bool	isPK;
		public bool isReadOnly;
	}

	public class TableGateway 
	{

		#region Datos_Miembro

		private System.Data.DataTable _dt;
		private Int32 _currentRow;
		private Object[] col; // Array utilizado para inserciones
		private String[] filt; // Array utilizado para filtrado
		private String[] filtTop; // Array utilizado para filtrado. Cotas superiores

		private int[] order; // Array utilizado para Order By 

		// Valor que entrega cuando el dato en el DataSet es nulo 
		private static Int32	_nullInt        = 0; 
		private static DateTime _nullDateTime	= DateTime.MinValue;
		private static Decimal	_nullDecimal	= 0;
		private static bool		_nullBool		= false;
		private static Double	_nullDouble		= 0.0;
		private  bool			_nullAsEmpty	= true;
		private	static byte[]	_nullIMAGE		= null;

		// String utilizado para asignar un valor nulo a un campo

		private static String _nullString = "<NULL>";

		private bool _isInserting = false;

		private System.Globalization.NumberFormatInfo _NumFmt;   // Formato numerico 
		private System.Globalization.DateTimeFormatInfo _FecFmt; // Formato de Fecha 
		private bool _isDbFormatOn;

		string _pkColumnName;

		#endregion Datos_Miembro

		#region Propiedades

		public System.Data.DataTable Table 
		{
			get{ return  _dt;}
		}

		public string NullString 
		{
			get{ return _nullString;}
		}

		#endregion Propiedades

		#region Constructores_y_Afines 

		private void initVars()
		{
			_currentRow = 0;
			_isInserting = false;
			dbFormatOFF();
		}
		
		
		public TableGateway()
		{
			initVars();

		}

		public TableGateway( System.Data.DataTable dataTable )
		{
			initVars();
			_dt = dataTable;
			_pkColumnName = _dt.PrimaryKey.GetLength(0) > 0 ? ((DataColumn[]) (_dt.PrimaryKey))[0].ColumnName : "";
		}

		public void Bind( System.Data.DataTable dataTable )
		{
			if( dataTable == null) throw new Exception( "Error en Bind(): DataTable nulo " );
			try
			{				
				initVars();
				_dt = dataTable;
				_pkColumnName = _dt.PrimaryKey.GetLength(0) > 0 ? ((DataColumn[]) (_dt.PrimaryKey))[0].ColumnName : "";
			}
			catch( Exception )
			{
				throw new Exception( "Error en Bind(): DataTable=" + dataTable.TableName);
			}
		}

		public void Clear( )
		{
			initVars();
			_dt.Clear();
		}

		
		#endregion  Constructores_y_Afines


		#region Versionado_de_datos_y_Concurrencia

		public void AcceptAllChanges()
		{
			_dt.AcceptChanges();
		}


		public void AcceptRowChanges()
		{
			_dt.Rows[_currentRow].AcceptChanges();
		}


		public DataRowState RowState()
		{
			return _dt.Rows[_currentRow].RowState;
		}
		

		#endregion  Versionado_de_datos_y_Concurrencia

		
		#region Obtener_valores

		#region definir_Version_y_formatos_a_obtener
	
		public void dbFormatON()
		{
			// Formato numerico y de fecha para DML
			_NumFmt = System.Globalization.NumberFormatInfo.InvariantInfo;
			_FecFmt = System.Globalization.DateTimeFormatInfo.InvariantInfo;
			_isDbFormatOn = true;
		}

		public void dbFormatOFF()
		{
			// Formato numerico y de fecha para User Interface
			_NumFmt = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat;
			_FecFmt = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat;
			_isDbFormatOn = false;
		}

			
		#endregion definir_Version_y_formatos_a_obtener

		#region NextId

		public int NextId( ) 
		{
			if( this._pkColumnName != "" ) 
			{
				return NextId( _pkColumnName );
			}
			else
			{
				throw new Exception(" Error al calcular NextID ");
			}
		}

		public int NextId( string campoID )	
		{
			if ( this.IsEmpty() ) return 1;
			int pos = this.Row();
			bool mode = this._isInserting;
			this.Go(0);
			int val = this.AsInt( campoID );
			for( ; this.Row() < this.RowCount(); this.Skip() )
			{
				if( this.AsInt( campoID ) > val	)
				{
					val = this.AsInt( campoID );
				}
			}
			this.Go(pos);
			_isInserting =  mode;
			return val + 1 ;
		}

		#endregion NextId	

		// IMAGEN
		public byte[] AsIMAGE( String campo )
		{
			return AsIMAGE( campo, DataRowVersion.Current );
		}

		public byte[] AsIMAGE( String campo, DataRowVersion pDataVersion )
		{
			if (this.IsNull(campo, pDataVersion ) )
				return _nullIMAGE;
			else 
			{ 
				return (byte[]) _dt.Rows[_currentRow][campo,pDataVersion];
			}
		}


		public Int32 AsInt( String campo )
		{
			return AsInt( campo, DataRowVersion.Current );
		}

		public Int32 AsInt( String campo, DataRowVersion pDataVersion )  // Devuelve el valor como entero
		{ 
			if (this.IsNull(campo, pDataVersion ) )
				return _nullInt;
			else
			{ 
				return (Int32) _dt.Rows[_currentRow][campo,pDataVersion];
			}
		}

		public Decimal AsDecimal( String campo )
		{
			return AsDecimal( campo, DataRowVersion.Current );
		}

		public Decimal AsDecimal( String campo, DataRowVersion pDataVersion )  // Devuelve el valor como Decimal
		{
			if (this.IsNull(campo,pDataVersion) )
				return _nullDecimal;
			else
				return (Decimal) _dt.Rows[_currentRow][campo,pDataVersion];
		}

		public Double AsDouble( String campo )
		{
			return AsDouble( campo, DataRowVersion.Current );
		}

		public Double AsDouble( String campo, DataRowVersion pDataVersion )  // Devuelve el valor como Double
		{
			if (this.IsNull(campo,pDataVersion) )
				return _nullDouble;
			else
				return (Double) _dt.Rows[_currentRow][campo,pDataVersion];
		}


		public DateTime AsDateTime( String campo )
		{ 
			return AsDateTime(campo, DataRowVersion.Current); 
		}

		public DateTime AsDateTime( String campo, DataRowVersion pDataVersion )  // Devuelve el valor como DateTime
		{
			if (this.IsNull(campo,pDataVersion) )
				return _nullDateTime;
			else
				return (DateTime) _dt.Rows[_currentRow][campo,pDataVersion];
		}


		public bool AsBoolean( String campo ) 
		{
			return AsBoolean( campo, DataRowVersion.Current);
		}
		public bool AsBoolean( String campo, DataRowVersion pDataVersion )  // Devuelve el valor como boolean
		{
			if (this.IsNull(campo,pDataVersion) )
				return _nullBool;
			else
				return (bool) _dt.Rows[_currentRow][campo,pDataVersion];
		}

		public String AsString( String campo ) 
		{
			return AsString( campo, DataRowVersion.Current); 
		}

		public String AsString( String campo , DataRowVersion pDataVersion)  // Devuelve el valor de la columna en formato String
		{
			String buffer;
			if (this.IsNull(campo,pDataVersion) )
			{
				return ( _nullAsEmpty ) ? "" : _nullString;
			}
			else
			{
				Object valor = _dt.Rows[_currentRow][campo,pDataVersion];

				switch( _dt.Columns[campo].DataType.Name )  // Se considera el tipo de dato del campo
				{
					case "int64":
					case "Int32":
					case "Int16":
						buffer =  ( (Int32) valor ).ToString("G", _NumFmt); break;
					case "String":
						buffer =  valor.ToString(); break;
					case "Decimal":
						buffer =  ( (Decimal) valor ).ToString("G", _NumFmt ); break;
					case "Double":
						buffer =  ( (Double) valor ).ToString("G", _NumFmt ); break;
					case "Boolean":
						if ( _isDbFormatOn)
						{
							buffer =  ((bool) valor ) ? "1" : "0"; break;
						}
						else
						{
							buffer =  ((bool) valor ) ? "Si" : "No"; break;
						}
					case "DateTime":
						//						if( (DateTime) valor == DateTime.MinValue )	{
						//							return ( _nullAsEmpty ) ? "" : _nullString; 
						//						}
						buffer = ((DateTime) valor ).ToString( "d", _FecFmt ); break;

					case "Byte[]":
						buffer = "<Binary>"; break;
					case "Object":
						buffer = (String) valor; break;
					default :
						buffer = "?"; break;
				};
			}
			return buffer;
		}


		public Object AsObject( String campo )
		{
			return AsObject( campo, DataRowVersion.Current);
		}


		public Object AsObject( String campo, DataRowVersion pDataVersion  )  // Devuelve directamente el valor
		{
			return _dt.Rows[_currentRow][campo,pDataVersion];
		}


		public String RowAsString ()
		{
			return RowAsString ( "," );
		}


		public String RowAsString ( String sep )
		{
			String buf = "";
			for ( int i = 0; i < _dt.Columns.Count ; i++)
			{
				buf+= (i == 0 ? "" : sep) + this.AsString( _dt.Columns[i].ColumnName );
			}
			return buf;
		}


		#endregion  Obtener_valores


		#region Insertar_Eliminar

		public void DeleteCurrent()    // Elimina la fila corriente
		{
			_dt.Rows[_currentRow].Delete();
		}


		public void NewRow()
		{
			col = new Object[_dt.Columns.Count];
			for( int i = 0; i < _dt.Columns.Count ;i++)
				col[i] = DBNull.Value;
			_isInserting = true;
		}


		public void SetNewRow(String campo,  Object valor )
		{
			col[_dt.Columns.IndexOf(campo)] = valor;

		}


		public void PostNewRow()
		{
			//			int pkPos = _dt.Columns.IndexOf(_pkColumnName);
			//	
			//			if( col[pkPos] == DBNull.Value ){
			//				col[pkPos] = -1;
			//			}

			if( col == null ) throw new Exception("PostNewRow() requiere de una llamada previa a NewRow()");
			_dt.Rows.Add( col );
			_isInserting = false;
		}

		

		#endregion Insertar_Eliminar


		#region Establecer_Valores 


		public void SetNull(String campo )    // Asigna Null al campo
		{
			if ( !_isInserting )
				_dt.Rows[_currentRow][campo] = DBNull.Value;
			else
				SetNewRow(campo,  DBNull.Value );
		}
		

		public void SetValue(String campo, String val )	
		{        // Asigna un valor al campo
			String tipoCampo = _dt.Columns[campo].DataType.Name; // Se considera el tipo de dato del campo

			if( tipoCampo == "String" )
			{
				if( val == _nullString )
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = DBNull.Value; 
					else
						SetNewRow(campo, DBNull.Value );
				else
					if ( !_isInserting )
					_dt.Rows[_currentRow][campo] = val;
				else
					SetNewRow(campo, val );
				return;
			}
			if ( val == String.Empty )
			{
				if ( !_isInserting )
					_dt.Rows[_currentRow][campo] = DBNull.Value;
				else
					SetNewRow(campo,  DBNull.Value );
				return;
			}
			switch( tipoCampo )      
			{
				case "Int64":
				case "Int32":
				case "Int16":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = int.Parse( val, Thread.CurrentThread.CurrentUICulture );
					else
						SetNewRow(campo, Int32.Parse( val, Thread.CurrentThread.CurrentUICulture ) );
					break;
				case "Decimal":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = Decimal.Parse( val , Thread.CurrentThread.CurrentUICulture); 
					else
						SetNewRow(campo, Decimal.Parse( val , Thread.CurrentThread.CurrentUICulture) );
					break;
				case "Double":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = Double.Parse( val , Thread.CurrentThread.CurrentUICulture); 
					else
						SetNewRow(campo, Double.Parse( val , Thread.CurrentThread.CurrentUICulture) );
					break;
				case "Boolean":
				switch( val.Trim() )
				{
					case "1":
					case "Si":
					case "SI":
					case "True":
					case "TRUE":
					case "true":
					case "Yes":
					case "YES":

						if ( !_isInserting )
							_dt.Rows[_currentRow][campo] = true;
						else
							SetNewRow( campo, true );
						break;
					case "0":
					case "No":
					case "NO":
					case "False":
					case "FALSE":
					case "false":
						if ( !_isInserting )
							_dt.Rows[_currentRow][campo] = false;
						else
							SetNewRow( campo, false );
						break;

					default :
						throw new Exception("Error al asignar " + _dt.TableName +"." + campo + "= \"" + val + "\"");
				}
					break;
				case "DateTime":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = DateTime.Parse( val, Thread.CurrentThread.CurrentUICulture ); 
					else
						SetNewRow( campo, DateTime.Parse( val, Thread.CurrentThread.CurrentUICulture ) );
					break; 

				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= \"" + val + "\"" );
			};
		}


		public void SetValue(String campo, int val )
		{    // el valor se ajusta al tipo del campo
			switch( _dt.Columns[campo].DataType.Name )
			{
				case "int64":
				case "Int32":
				case "Int16":
				case "Decimal":
				case "Double":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val;
					else
						SetNewRow( campo, val );
					break;
				case "String":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] =  val.ToString();
					else
						SetNewRow( campo, val );
					break;
				case "Boolean":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = ( val == 0 ? false : true );
					else
						SetNewRow( campo, ( val == 0 ? false : true ) );
					break;
				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= " + val.ToString()  );
			};
		}


		public void SetValue(String campo, double val )
		{    
			switch( _dt.Columns[campo].DataType.Name )
			{
				case "Int32":
				case "Int16":
				case "Int64":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = ( Int32) val;
					else
						SetNewRow( campo,  val );
					break;
				case "Decimal":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = ( Decimal ) val;
					else
						SetNewRow( campo,  val );
					break;
				case "Double":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val;
					else
						SetNewRow( campo,  val );
					break;
				case "String":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] =  val.ToString();
					else
						SetNewRow( campo,  val.ToString() );
					break;
				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= " + val.ToString()  );
			};
		
		
		}


		public void SetValue(String campo, Decimal val )
		{    
			switch( _dt.Columns[campo].DataType.Name )
			{
				case "int64":
				case "Int32":
				case "Int16":
				case "Decimal":
				case "Double":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val;
					else
						SetNewRow( campo,  val );
					break;
				case "String":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] =  val.ToString();
					else
						SetNewRow( campo,  val.ToString() );
					break;
				case "Boolean":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = ( val == 0 ? false : true ) ; 
					else
						SetNewRow( campo,  ( val == 0 ? false : true ) );
					break;
				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= " + val.ToString()  );
			};
		}

		
		public void SetValue(String campo, DateTime val )
		{    
			switch( _dt.Columns[campo].DataType.Name )
			{
				case "DateTime":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val; 
					else
						SetNewRow( campo,  val );
					break;
				case "String":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] =  val.ToString();
					else
						SetNewRow( campo,  val.ToString() );
					break;
				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= " + val.ToString()  );
			};
		}

			
		public void SetValue(String campo, bool val )
		{    
			switch( _dt.Columns[campo].DataType.Name )
			{
				case "int64":
				case "Int32":
				case "Int16":
				case "Decimal":
				case "Double":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val ? 1 : 0 ;
					else
						SetNewRow( campo,  val ? 1 : 0 );
					break;
				case "Boolean":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val;
					else
						SetNewRow( campo,  val );
					break;
				case "String":
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] =  val.ToString();
					else
						SetNewRow( campo,  val.ToString() );
					break;
				default :
					throw new Exception("Error al asignar " + _dt.TableName +"." + campo+ "= " + val.ToString()  );
			};
		}



		public void SetValue(String campo, Object val )
		{  
			//			if ( !_isInserting )
			//			{
			switch( val.GetType().Name )
			{
				case "int64":
				case "Int32":
				case "Int16":
					SetValue(campo, (Int32) val );
					break;
				case "Decimal":
					SetValue(campo, (Decimal) val );
					break;
				case "Double":
					SetValue(campo, (Double) val );
					break;
				case "Boolean":
					SetValue(campo, (bool) val );
					break;
				case "String":
					SetValue(campo, (string) val );
					break;
				case "DateTime":
					SetValue(campo, (DateTime) val );
					break;
				default :
					if ( !_isInserting )
						_dt.Rows[_currentRow][campo] = val;
					else
						SetNewRow( campo,  val );
					break;
			};
	
			//			}else{
			//				SetNewRow( campo,  val );
			//			}
		}
		


		#endregion Establecer_Valores

		#region Mover_Entre_Filas

		public void Skip( )   // Avanza a la siguiente fila
		{
			_currentRow++;
			_isInserting = false;
		}
	
		public void Go( Int32 pos ) // Posiciona para leer una determinada fila
		{
			_currentRow = pos;
			_isInserting = false;
		}

		public bool Find( string campo, Object valor ) 
		{
			return ( IndexOf(campo, valor ) != -1 );
		}


		public Int32 IndexOf( String campo, Object val )
		{
			int pos = -1;		
			for( this.Go( 0 ); this.Row() < this.RowCount(); this.Skip())
			{
				if ( val.Equals( _dt.Rows[_currentRow][campo] ) )
				{
					pos = this.Row();
					break;
				}
			}
			return pos;
		}
		

		#endregion Mover_Entre_Filas

		#region Busquedas en la BD

		public void Order_Clear()
		{
			order = new int[_dt.Columns.Count+1];
			for( int i = 0; i <= _dt.Columns.Count ;i++)	
			{
				order[i] = -1;
			}
		}

		public void setOrder( string campo, int valor)
		{
			if( order == null )	Order_Clear();
			int idx = _dt.Columns.IndexOf(campo);
			if( idx == -1 ) throw new Exception( "setOrder(): La columna "+ campo + " no esta definida en "+ this.Table.TableName);
			order[valor] = idx ;
		}


		public void Filter_Clear()
		{
			filt = new String[_dt.Columns.Count];
			filtTop = new String[_dt.Columns.Count];
			for( int i = 0; i < _dt.Columns.Count ;i++)
			{
				filt[i] = "";
				filtTop[i] = "";
			}
		}

		public void Filter_Value( string campo, Object valor)
		{
			Filter_Value( campo, valor, false ); // MinValue 
		}

		public void Filter_MinValue( string campo, Object valor)
		{
			Filter_Value( campo, valor, false ); // MinValue 
		}

		public void Filter_MaxValue( string campo, Object valor)
		{
			Filter_Value( campo, valor, true ); // MaxValue 
		}



		private void Filter_Value( string campo, Object valor, bool top )
		{
			string buffer = "";
			if( filt == null )
			{
				Filter_Clear();
			}

			this.dbFormatON();
			switch( valor.GetType().Name )  // Se considera el tipo de dato del campo
			{
				case "Int64":
				case "Int32":
				case "Int16":
					buffer =  ( (Int32) valor ).ToString("G", _NumFmt); break;
				case "String":
					buffer =  valor.ToString(); 
					buffer = buffer != string.Empty ? "'" + buffer + "'" : "";
					break;
				case "Decimal":
					buffer =  ( (Decimal) valor ).ToString("G", _NumFmt ); break;
				case "Double":
					buffer =  ( (Double) valor ).ToString("G", _NumFmt ); break;
				case "Boolean":
					buffer =  ((bool) valor ) ? "1" : "0"; break;
				case "DateTime":
					buffer = ((DateTime) valor ).ToString( "d", _FecFmt ); 
					buffer = buffer != string.Empty ? "'" + buffer + "'" : "";
					break;
				case "Object":
					buffer = valor.ToString(); break;
				default:
					throw new Exception("TableGateway::Filter_Value(). Tipo de dato ["+ valor.GetType().Name + "]no previsto");
			};
			this.dbFormatOFF();
			if ( buffer != string.Empty) 
			{
				int idx = _dt.Columns.IndexOf(campo);
				if( top )
				{
					filtTop[ idx ] = buffer;
				}
				else
				{
					filt[ idx ] = buffer;
				}
			}
		}


		#endregion Busquedas en la BD

		#region Informacion_de_Estado

		public bool EOF()   // Verdadero si la fila actual esta "en rango" 
		{		
			return (  ! (_currentRow < _dt.Rows.Count) || _dt.Rows.Count < 1   );
		}

		public bool IsRowInRange()   // Verdadero si la fila actual esta "en rango" 
		{		
			return (  _currentRow < _dt.Rows.Count  );
		}


		public bool IsEmpty()   // Informa si el DataTable esta vacio
		{		
			return ( _dt.Rows.Count == 0 );
		}

		public bool IsNull( String campo )
		{
			return IsNull( campo, DataRowVersion.Current );
		}

		public bool IsNull( String campo, DataRowVersion pDataVersion   )  // Infroma si el campo es nulo 
		{
			try
			{
				Object obj = _dt.Rows[_currentRow][campo,pDataVersion];
				string typeName = obj.GetType().Name;
				if (typeName == "DBNull" ) return true;
				if (typeName == "DateTime" )
				{
					if( (DateTime)obj == DateTime.MinValue) return true;
				}
				return false;
			}
			catch( Exception )
			{
				return true; 
			}
		}

		public Int32 RowCount()
		{	// Informa la cantidad de filas en el DataTable
			return _dt.Rows.Count;
		}

		public Int32 Row() // Informa cual es el indice de fila actual
		{	
			return _currentRow;
		}

		public bool IsInserting()   // Informa si se esta insertando un nueva fila
		{		
			return _isInserting;
		}


		#endregion Informacion_de_Estado

		#region SQL_DINAMICO

		#region Cadenas_DML


		public string SQL_SelectById( int pID )
		{
			return SQL_SelectById( this.getColArray(), pID );
		} 

		public string SQL_SelectById( ArrayList colArray, int pID ) 
		{
			string buf= " select ";
			buf+= SQL_SelectColumnNameList( colArray );
			buf+= " from " +  _dt.TableName;
			buf+= " where " + this._pkColumnName + " = " + pID;
			return buf;
		}
		
		public string SQL_Select()
		{
			return SQL_Select( this.getColArray() );
		} 

		public string SQL_Select( ArrayList colArray ) 
		{
			string buf= " select ";
			string filtro;
			buf+= SQL_SelectColumnNameList( colArray );
			buf+= " from " +  _dt.TableName;
			filtro = SQL_WhereFiltString( colArray );
			if( filtro.Trim() != string.Empty )	
			{
				buf+=  " where " + filtro;
			}
			string orderBy = SQL_OrderString( colArray );
			if ( orderBy.Trim() != "" )
			{
				buf+=  " order by  " + orderBy;
			}
			return buf;
		}


		public string SQL_Update ()
		{
			return SQL_Update( this.getColArray() );
		} 
		
		public string SQL_Update ( ArrayList colArray ) 
		{
			string buf= SQL_SetString( colArray );
			if( buf.Trim() != string.Empty )
			{
				buf= " update " + _dt.TableName + " set " + buf + " where ";
				buf+= SQL_WhereString( colArray );
			}
			else
			{
				buf = "";
			}
			return buf;
		}


		public string SQL_Insert ()
		{
			return SQL_Insert( this.getColArray() );
		} 

		public string SQL_Insert ( ArrayList colArray ) 
		{
			string buf= " insert into " + _dt.TableName + " ";
			buf+= SQL_insertColumnNameList( colArray );
			buf+= " Values ";
			buf+= SQL_insertColumnValueList( colArray );
			return buf;
		}


		public string SQL_Delete ()
		{
			return SQL_Delete( this.getColArray() );
		} 

		public string SQL_Delete ( ArrayList colArray ) 
		{
			string buf= " delete from " + _dt.TableName + " where ";
			buf+= SQL_WhereString( colArray );
			return buf;
		}
		  

		#region Componentes_DML

		// Order by
		public string SQL_OrderString () // Cadena de order by
		{
			return SQL_OrderString( this.getColArray() );
		}

		public string SQL_OrderString ( ArrayList colArray ) // Cadena de order by
		{
			string buf= " ";
			if( order != null ) 
			{
				int cont=0;
				for(int i = 0; i <= colArray.Count; i++ ) 
				{
					string coma = ( cont == 0 ? "" : " , ");
					if( order[i] != -1 ) 
					{
						ColumnItem col = (ColumnItem) colArray[order[i]];
						cont++;
						buf+=  coma + col.name;
					}
				}
			}
			return buf;
		}


		// end Cadena de order by

		public string SQL_WhereFiltString () // Where para Filtro
		{
			return SQL_WhereFiltString( this.getColArray() );
		}

		public string SQL_WhereFiltString ( ArrayList colArray ) // Where para Filtro
		{
			string buf= " ";
			if( filt != null ) 
			{
				int cont=0;
				for(int i = 0; i < colArray.Count; i++ )  
				{
					ColumnItem col = (ColumnItem) colArray[i];
					string operador = (col.tipo == "String") ? " like " : " = ";
					string coma = ( cont == 0 ? "" : " AND ");
					if( filtTop[i].Trim() == "" )
					{ // No hay top value

						if( filt[i].Trim() != string.Empty ) 
						{
							cont++;
							buf+=  coma + col.name + operador + filt[i];
						}
					}
					else
					{ // Hay top value
						if( filt[i].Trim() != string.Empty ) 
						{
							cont++;
							buf+=  coma + col.name + " between " + filt[i] + " and " + filtTop[i];
						}
					}
				}
			}
			return buf;
		}


		private string SQL_WhereStringActual ( ArrayList colArray ) // Where con datos actuales
		{
			string buf= "";
			int cont = 0;
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				string operador = (col.tipo == "String") ? " like " : " = ";
				string coma = ( cont==0 ? "" : " AND ");
				string apost = (col.tipo == "String" || col.tipo == "DateTime" ) ? "'" : "";
				if ( ! col.isPK && ! col.isReadOnly )
				{
					cont++;
					if( col.currentValue == _nullString )
					{
						buf+= coma + col.name + " IS NULL ";
					} 
					else 
					{
						buf+=  coma + col.name + operador + apost + col.currentValue + apost;
					}
				}
			}
			return buf;
		}



		private string SQL_WhereString ( ArrayList colArray ) // Where para concurrencia optimista
		{
			string buf= "";
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				string operador = (col.tipo == "String") ? " like " : " = ";
				string coma = ( i==0 ? "" : " AND ");
				string apost = (col.tipo == "String" || col.tipo == "DateTime" ) ? "'" : "";
				if( col.oldValue == _nullString )
				{
					buf+= coma + col.name + " IS NULL ";
				}
				else
				{
					buf+=  coma + col.name + operador + apost + col.oldValue + apost;
				}
			}
			return buf;
		}


		private string SQL_insertColumnNameList ( ArrayList colArray )
		{
			string buf= " ( ";
			bool primero = true;
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				if ( ! col.isPK && ! col.isReadOnly && col.currentValue != this.NullString )
				{
					string coma = ( primero ? "" : ", ");
					buf+=  coma + col.name;
					primero = false;
				}
			}
			buf+= " ) ";
			return buf;
		}


		private string SQL_SelectColumnNameList ( ArrayList colArray )
		{
			string buf= "";
			bool primero = true;
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				string coma = ( primero ? "" : ", ");
				buf+=  coma + col.name;
				primero = false;
			}
			return buf;
		}


		private string SQL_insertColumnValueList ( ArrayList colArray )
		{
			string buf= " ( ";
			bool primero = true;
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				if ( ! col.isPK && ! col.isReadOnly && col.currentValue != this.NullString )
				{
					string coma = ( primero ? "" : ", ");
					string apost = (col.tipo == "String" || col.tipo == "DateTime" ) ? "'" : "";

					buf+=  coma + apost + col.currentValue + apost;
					primero = false;
				}
			}
			buf+= " ) ";
			return buf;
		}


		private string SQL_SetString ( ArrayList colArray )
		{
			string buf= "";
			bool primero = true;
			for(int i = 0; i < colArray.Count; i++ )
			{
				ColumnItem col = (ColumnItem) colArray[i];
				if ( ! col.isPK && ! col.isReadOnly && col.oldValue != col.currentValue )
				{
					string coma = ( primero ? "" : ", ");
					string apost = (col.tipo == "String" || col.tipo == "DateTime" ) ? "'" : "";
					if( col.currentValue == _nullString )
					{
						buf+=  coma + col.name + " = NULL ";
					}
					else 
					{
						buf+=  coma + col.name + " = " + apost + col.currentValue + apost;
					}
					primero = false;
				}
			}
			return buf;
		}


		public ArrayList getColArray( )
		{
			ArrayList ret = new ArrayList();
			this.dbFormatON();
			this._nullAsEmpty = false;

			for( int cl= 0 ; cl < this._dt.Columns.Count ; cl++ )
			{
				DataColumn col = this._dt.Columns[cl];
				ColumnItem item = new ColumnItem();
				item.isReadOnly = col.ReadOnly;
				item.name = col.ColumnName;
				item.isPK = ( col.ColumnName == _pkColumnName );
				item.tipo = col.DataType.Name;
				item.currentValue = this.AsString( item.name );

				item.oldValue = this.AsString( item.name, DataRowVersion.Original );

				ret.Add( item );

			}
			this.dbFormatOFF();
			this._nullAsEmpty = true;

			return ret;
		}


		#endregion Componentes_DML

		#endregion Cadenas_DML

		#region Sentencias DML

		public int InsertRow( AccesoDB db )
		{
			ArrayList colArray = this.getColArray();
			db.Sql = this.SQL_Insert(colArray);
			int afectados = db.EjecutarDML();
			if ( afectados == 0 ) throw new Exception("Error de concurrencia");

			string where = SQL_WhereStringActual(colArray);
			//			db.Sql = "select max(" + this._pkColumnName + ") from " + _dt.TableName + " where " + where;

			db.Sql = " select IDENT_CURRENT ('" + _dt.TableName + "')";

			int Id;
			Object obj =  db.getValue();
			if( obj == DBNull.Value )
			{
				Id = -1;
			}
			else
			{
				Id = int.Parse( obj.ToString());
			}
			return Id;
		}

		public void UpdateRow( AccesoDB db )
		{
			db.Sql = this.SQL_Update();
			int afectados = db.EjecutarDML();
			if ( afectados == 0 ) throw new Exception("Error de concurrencia");		
		}

		public void DeleteRow( AccesoDB db )
		{
			db.Sql = this.SQL_Delete();
			int afectados = db.EjecutarDML();
			if ( afectados == 0 ) throw new Exception("Error de concurrencia");		
		}

		public void ReadAll( AccesoDB db )
		{
			db.Sql = this.SQL_Select();
			db.FillDataSet( this._dt.DataSet, this._dt.TableName );
		}

		public void ReadById( AccesoDB db, int pID )
		{
			db.Sql = this.SQL_SelectById( pID );		
			db.FillDataSet( this._dt.DataSet, this._dt.TableName );
		}


		#endregion Sentencias DML


		#endregion SQL_DINAMICO


		#region Ejemplo_de_uso
		/*
			MarcaDS mds = (MarcaDS) ds;
	
			TableGateway tab1GW = new TableGateway(  mds.Tables["AreaTramiteSit"] );
			
			TableGateway marcaGW = new TableGateway();
			
			marcaGW.Bind(  mds.Tables["Marca"] );

			string buf,bf;
			int i;
			
			for( tab1GW.Go( 0 ); tab1GW.Row() < tab1GW.RowCount(); tab1GW.Skip()){
			
			  bf  = tab1GW.AsString("Descrip");
			}
				
			tab1GW.Go( 0 );

			bf  = tab1GW.AsString("Descrip");
			buf =  mds.Tables["AreaTramiteSit"].Rows[0]["Descrip"].ToString();
			
			tab1GW.Skip();
			bf  = tab1GW.AsString("Descrip");
			buf =  mds.Tables["AreaTramiteSit"].Rows[1]["Descrip"].ToString();
			

			bf  = marcaGW.AsString("ClaseID");
			buf = mds.Tables["Marca"].Rows[0]["ClaseID"].ToString();		
			i = marcaGW.AsInt("ClaseID");


			tab1GW.Go( 0 );
			tab1GW.SetValue("Descrip", "Algo impuesto al azar");


			bf  = tab1GW.AsString("Descrip");
			buf =  mds.Tables["AreaTramiteSit"].Rows[0]["Descrip"].ToString();

			if ( marcaGW.IsNull("ClaseID") ){
				marcaGW.SetValue("ClaseID", "1" );
			}
			else
			{
				marcaGW.SetNull("ClaseID");
			}

			marcaGW.SetValue("ClaseID", 1 );

			i = marcaGW.AsInt("ClaseID");
			
			//-------
			SimpleEntryDS outDS = new SimpleEntryDS();
			TableGateway gw = new TableGateway( outDS.Tables["SimpleEntry"] );
			using ( SqlConnection conn = ReadConnection( connName ) ) {
				using( SqlDataReader rdr = SqlHelper.ExecuteReader( conn, sp.Cliente_ReadByPattern.ToString(), DS2Xml( inDS ) ) ){
					while( rdr.Read() ){					
						gw.NewRow();
						gw.SetNewRow("Descr", rdr["Descrip"]);
						gw.SetNewRow("ID",    rdr["ID"]     );
						gw.PostNewRow();
					}
				}
			}

		*/

		#endregion Ejemplo_de_uso

	} // end class TableGateway 

	#endregion Table_Gateway


}// end namespace Berke.Libs.Base.Helpers
