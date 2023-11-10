using System;

namespace Berke.Libs.Base
{	
	/// <summary>
	#region ObjConvert

	public enum CultureFormat	{ UI_Culture, DB_Culture }
	public enum DateFormat		{ Fecha, Fecha_Hora, Hora, FHMS }

	public class ObjConvert
	{

		#region Datos Miembro

		#region Valores devueltos si el objeto es nulo

		static Boolean		_nullBoolean	=	false;
		static Byte[]		_nullBinary		=	null;
		static Byte		_nullByte		=	0;
		static DateTime	_nullDateTime	=	DateTime.MinValue;
		static Decimal		_nullDecimal	=	0;
		static Double		_nullDouble		=	0.0;
		static Single		_nullSingle		=	0.0F;
		static Guid		_nullGuid			=	new Guid("00000000-0000-0000-0000-000000000000");	// byte[16]
		static short		_nullShort		=	0;
		static int			_nullInt		=	0;
		static long		_nullLong		=	0L;
		static string		_nullString		=	"";
		static Object		_nullObject		=	System.DBNull.Value;

		#endregion Valores devueltos si el objeto es nulo

		#region Strings  devueltos si el objeto es nulo

		//		string	_strNullBoolean		=	"";
		//		string	_strNullBinary		=	"";
		//		string	_strNullByte		=	"";
		//		string	_strNullDateTime	=	"";
		//		string	_strNullDecimal		=	"";
		//		string	_strNullDouble		=	"";
		//		string	_strNullSingle		=	"";
		//		string	_strNullGuid		=	"";	
		//		string	_strNullShort		=	"";
		//		string	_strNullInt			=	"";
		//		string	_strNullLong		=	"";
		static string	_strNullString		=	"";
		//		string	_strNullObject		=	"";

		#endregion Strings  devueltos si el objeto es nulo

		#region Constantes

		static private string _setNullStr	=	"<NULL>";
		static private string _emptyString	=	"";

		// Formato User Interface 
		static private System.Globalization.NumberFormatInfo	_UI_NumberCultureFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat;   
		static private System.Globalization.DateTimeFormatInfo	_UI_DateCultureFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat;  
		// Formato DB
		static private System.Globalization.NumberFormatInfo	_DB_NumberCultureFormat = System.Globalization.NumberFormatInfo.InvariantInfo; 
		static private System.Globalization.DateTimeFormatInfo	_DB_DateCultureFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo; 



		#endregion Constantes

		#region Cultura

		static private CultureFormat	_culture 	= CultureFormat.UI_Culture;
		static private DateFormat		_dateFormat	= DateFormat.Fecha;

		#endregion Cultura


		#endregion Datos Miembro	

		#region Propiedades

		static public string NullString
		{
			get { return _strNullString;}
			set { _strNullString = value; }
		}

		#endregion Propiedades



		#region Metodos

		#region Establecer Formatos

		static public void SetCulture( CultureFormat cultFmt ){	_culture	= cultFmt;	}
		static public void SetDateFormat( DateFormat dateFmt ){	_dateFormat	= dateFmt;	}
		
		#endregion Establecer Formatos

		static public object GetFilter_Date( string cadena ){
			#region Filtro Nulo
			if( cadena.Trim() == "" ){
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
            
			string fFecha = cadena.Trim();
			if( fFecha.IndexOf(":") == -1  && fFecha.IndexOf(",") == -1 )
			{
				if( fFecha.IndexOf("-") == -1 )
				{
					fFecha = fFecha + " 00:00 - "+fFecha +" 23:59";
				}
				else 
				{
					string [] aVal = fFecha.Split( ((String)"-").ToCharArray() );
					string min = aVal[0].Trim() + " 00:00";
					string max = aVal[1].Trim() + " 23:59";
					fFecha = min+ " - "+ max;
				}
			}
		    return GetFilter( fFecha );
		}

		static public object GetFilter_Str( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			if( cadena.IndexOf("*") != -1 )
			{
				cadena = cadena.Replace("*","%");
			}
			cadena+= "%";
			return cadena;
		}

		static public object GetFilter_Bool( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			bool ret = false;
			switch( cadena.ToUpper() )
			{
				case "SI":
				case "TRUE":
				case "1":
					ret = true;
					break;
				case "NO":
				case "False":
				case "0":
					ret = false;
					break;
			}
			return ret;
		}

		static public object GetFilter( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo

			#region Rango
			if( cadena.IndexOf("-") != -1 )
			{
				string [] aVal = cadena.Split( ((String)"-").ToCharArray() );
				string min = aVal[0].Trim();
				string max = aVal[1].Trim();
				if( min != "" && max != "")
				{
					return new Berke.Libs.Base.DSHelpers.DSFilter( (object)min, (object)max );
				}
				//				else{
				//					if( min == "")
				//					{
				//						return new DSFilter( "<=", max , "");
				//					}
				//					else
				//					{
				//						return new DSFilter( ">=", max , "");
				//					}
				//				}
				//			
			}
			#endregion Rango

			#region Lista
			if( cadena.IndexOf(",") != -1 )
			{
				return new Berke.Libs.Base.DSHelpers.DSFilter( new System.Collections.ArrayList(cadena.Split( ((String)",").ToCharArray()) ));
			}
			#endregion Lista

			return new Berke.Libs.Base.DSHelpers.DSFilter( cadena );
		
		}


		#region GetSqlPattern
		static public object GetSqlPattern( object obj )
		{
			if( obj is DBNull ) return DBNull.Value;
			if( obj is String || obj is string )
			{
				if( (string)obj == "" )	
				{
					return DBNull.Value;
				}
				else
				{
					return "%"+(string)obj+"%";
				}
			}
			else
			{
				throw new Exception( "ObjConvert::GetPattern(). El patron debe ser de tipo string");
			}
		}
		#endregion GetSqlPattern

		#region GetSqlStringValue
		static public object GetSqlStringValue( object obj )
		{
			if( obj is DBNull ) return DBNull.Value;
			if( obj is String || obj is string )
			{
				if( (string)obj == "" )	
				{
					return DBNull.Value;
				}
				else
				{
					return (string)obj;
				}
			}
			else
			{
				throw new Exception( "ObjConvert::GetSqlStringValue(). El patron debe ser de tipo string");
			}
		}
		#endregion GetSqlStringValue

		#region Asignar valores
		
		static public void SetValue(ref Object destino, Object origen, string tipoDestino )
		{
			if( origen == null ) {destino = DBNull.Value; return;}
			string tipoOrigen = origen.GetType().Name; 
 
			if( tipoOrigen == "DBNUll")	
			{
				destino = origen;
				return;
			}

			if( tipoDestino != "String" && tipoOrigen == "String")
			{
				if( (String)origen == _emptyString )
				{
					destino = DBNull.Value;
					return;
				} 
			
			}
			switch( tipoOrigen )
			{

					#region Boolean
				case "Boolean" :
				switch( tipoDestino )
				{
					case "Boolean"	:	destino = Convert.ToBoolean(origen);	break;
					case "Int64"	:
					case "Int32"	:
					case "Int16"	:
					case "Decimal"	:
					case "Byte"		:
					case "Double"	:
					case "Single"	:	destino = Convert.ToBoolean(origen) ? 1 : 0 ;	break;
					case "String"	:	destino = Convert.ToBoolean(origen).ToString();	break;
					default :
						throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
				};
					break;
					#endregion Boolean

					#region Entero

				case "Byte"		:
				case "Decimal"	:
				case "Int16"	:
				case "Int32"	:
				case "Int64"	:
				switch( tipoDestino )
				{
					case "Int"	:
					case "int"	:
					case "Int32"	:
					case "Int16"	:	destino = Convert.ToInt32(origen);		break;
					case "Int64"	:	destino = Convert.ToInt64(origen);		break;
					case "Decimal"	:	destino = Convert.ToDecimal(origen);	break;
					case "Byte"		:	destino = Convert.ToByte(origen);		break;
					case "Double"	:	destino = Convert.ToDouble(origen);		break;
					case "Single"	:	destino	= Convert.ToSingle(origen);		break;
					case "String"	:	destino = Convert.ToInt64(origen).ToString();	break;
					default :
						throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
				};
					break;	
				
					#endregion Entero

					#region Real

				case "Double"	:
				case "Single"	:
				switch( tipoDestino )
				{
					case "Int32"	:
					case "Int16"	:	destino = Convert.ToInt32(origen);		break;
					case "Int64"	:	destino = Convert.ToInt64(origen);		break;
					case "Decimal"	:	destino = Convert.ToDecimal(origen);	break;
					case "Byte"		:	destino = Convert.ToByte(origen);		break;					case "Double"	:	destino = Convert.ToDouble(origen);		break;
					case "Single"	:	destino	= Convert.ToSingle(origen);		break;
					case "String"	:	destino = Convert.ToDouble(origen).ToString();	break;
					default :
						throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
				};
					break;	
				
					#endregion Real

					#region DateTime

				case "DateTime" :
				switch( tipoDestino )
				{
					case "DateTime"	:	destino = Convert.ToDateTime(origen);	break;
					case "String"	:	destino = Convert.ToDateTime(origen).ToString();	break;
					default :
						throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
				};
					break;	

					#endregion DateTime

					#region String 

				case "String" :
	
				switch( tipoDestino )
				{
					case "Boolean"	:
					switch( Convert.ToString(origen).ToUpper() )
					{
						case "YES"	:
						case "TRUE"	:							
						case "SI"	:							
						case "1"	:		
						case "Y"	:		
						case "T"	:		
						case "S"	:		
							destino = true; break;							
						case "NO"	:
						case "FALSE":
						case "0"	:		
						case "N"	:		
						case "F"	:		
							destino = false; break;							
						default :
							throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
					}	
							
						break;
					case "DateTime"	: destino = DateTime.Parse( Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Int64"	: destino = Int64.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Int32"	: destino = Int32.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Int16"	: destino = Int16.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Decimal"	: destino = Decimal.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Byte"		: destino = Byte.Parse(		Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Double"	: destino = Double.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
					case "Single"	: destino = Single.Parse(	Convert.ToString(origen) ,	_UI_NumberCultureFormat); break;
		
					case "String"	:
						if( Convert.ToString(origen) == _setNullStr )
						{
							destino = DBNull.Value;
						}
						else
						{
							destino = Convert.ToString(origen);
						}
						break;
					default :
						throw new Exception("Error al asignar " + tipoOrigen + " <= " +  tipoDestino );
				};

					break;


					#endregion String 
				default :
					destino = origen;
					break;

			}// switch

		}
	


		#endregion Asignar valores

		#region StringAsInt
		static public object StringAsInt( string val )
		{
			return ObjConvert.AsObject( val, "Int" );
		}
		#endregion StringAsInt	

		#region Obtener valores

		#region IsNull
		static public bool IsNull( Object obj )
		{
			try	
			{
				if ( obj == null ) return true;
				string typeName = obj.GetType().Name;

				if (typeName == "DBNull" ) return true;
				if (typeName == "DateTime" )
				{
					if( Convert.ToDateTime( obj)  == _nullDateTime ) return true;
				}
				if (typeName == "Guid" )
				{
					if( (Guid)obj == _nullGuid ) return true;
				}
				return false;
			}
			catch( Exception e )
			{
				throw new Exception("Error en ObjConvert.IsNull()" + e.Message );
			}	
		}
		#endregion IsNull

		#region AsString

		static public String AsString( Object valor )
		{
			return AsString( valor, _culture, _dateFormat );
		}
		
		static public String AsString( Object valor, CultureFormat cultFmt, DateFormat dateFmt )
		{
			string buffer;
			if (IsNull( valor ) )	return _nullString;
		
			#region Establecer formatos
			System.Globalization.NumberFormatInfo	_NumFmt;
			System.Globalization.DateTimeFormatInfo	_FecFmt;
			string datePicture;
			switch( dateFmt )
			{
				case DateFormat.Fecha :
					datePicture = "d";
					break;
				case DateFormat.Fecha_Hora :
					datePicture = "g";
					break;
				case DateFormat.Hora :
					datePicture = "t";
					break;
				case DateFormat.FHMS :
					datePicture = "G";
					break;
				default :
					datePicture = "d";
					break;		
			}//switch

			if( cultFmt == CultureFormat.UI_Culture)
			{
				_NumFmt = _UI_NumberCultureFormat ;
				_FecFmt = _UI_DateCultureFormat ;
			}
			else
			{
				_NumFmt = _DB_NumberCultureFormat ;
				_FecFmt = _DB_DateCultureFormat ;	
			}

			#endregion Establecer formatos

			#region Asignar segun el tipo de dato
			string tipo = valor.GetType().Name;
			switch( tipo )  
			{
				case "Boolean":
					if ( cultFmt == CultureFormat.DB_Culture )
					{
						buffer =  Convert.ToBoolean( valor ) ? "1" : "0"; break;
					}
					else
					{
						buffer =   Convert.ToBoolean( valor ) ? "Si" : "No"; break;
					}
				case "Byte":
					buffer = Convert.ToByte( valor).ToString("X", _NumFmt );
					break;
				case "Byte[]":
					buffer = "<Dato Binario>";
					// buffer = BitConverter.ToString((byte[])valor);
					break;
				case "DateTime":
					buffer = Convert.ToDateTime( valor ).ToString( datePicture, _FecFmt ); break;
				case "Decimal":
					buffer =  Convert.ToDecimal( valor ).ToString("G", _NumFmt ); break;
				case "Double":
					buffer =  Convert.ToDouble( valor ).ToString("G", _NumFmt ); break;
				case "Single":
					buffer =  Convert.ToSingle( valor ).ToString("G", _NumFmt ); break;
				case "Guid":
					buffer =  ( (Guid) valor ).ToString(); break;
				case "int64":
				case "Int32":
				case "Int16":
					buffer =  Convert.ToInt64( valor ).ToString("G", _NumFmt); break;
				case "String":
					buffer =  valor.ToString(); break;
					//				case "Object":
					//					buffer = valor.ToString(); break;
				default :
					throw new Exception(" Conversión a String no definidad para " + tipo);
			} // switch
			#endregion Asignar segun el tipo de dato
			
			return buffer;
		}
		#endregion AsString	

		#region AsBoolean
		static public Boolean AsBoolean( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj).Trim() == "" )
				{
					return  _nullBoolean;
				}
				else
				{
					string val	= (string) obj;
					string c1	= val.Trim().Substring(0,1);
					bool ret = false;
					switch( c1 )
					{
						case "S" :
						case "s" :
						case "T" :
						case "t" :
						case "1" :
							ret = true;
							break;
						case "N" :
						case "n" :
						case "F" :
						case "f" :
						case "0" :
							ret = false;
							break;
					} // end switch
					return ret;
				}// end if
			}
			if ( IsNull( obj ) )	return _nullBoolean;
			else					return Convert.ToBoolean( obj );
		}
		#endregion AsBoolean	

		#region AsBinary
		static public Byte[] AsBinary( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullBinary;
				}
			}
		
			if ( IsNull( obj ) )	return _nullBinary;
			else					return (Byte[]) obj;
		}
		#endregion AsBinary	

		#region AsByte
		static public Byte AsByte( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullByte;
				}
			}
			if ( IsNull( obj ) )	return _nullByte;
			else					return Convert.ToByte( obj );
		}
		#endregion AsByte	

		#region AsDateTime
		static public DateTime AsDateTime( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullDateTime;
				}
			}
			if (IsNull( obj ) )	return _nullDateTime;
			else				return Convert.ToDateTime( obj );
		}
		#endregion AsDateTime	

		#region AsDecimal
		static public Decimal AsDecimal( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullDecimal;
				}
			}
			if (IsNull( obj ) )	return _nullDecimal;
			else				return Convert.ToDecimal( obj );
		}
		#endregion AsDecimal	

		#region AsDouble
		static public Double AsDouble( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullDouble;
				}
			}
			if (IsNull( obj ) )	return _nullDouble;
			else				return Convert.ToDouble( obj );
		}
		#endregion AsDouble	

		#region AsSingle
		static public Single AsSingle( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullSingle;
				}
			}
			if (IsNull( obj ) )	return _nullSingle;
			else				return Convert.ToSingle( obj );
		}
		#endregion AsSingle
	
		#region AsGuid
		static public Guid AsGuid( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullGuid;
				}
			}
			if (IsNull( obj ) )	return _nullGuid;
			else						return (Guid) obj;
		}
		#endregion AsGuid	
		
		#region AsShort
		static public Int16 AsShort( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullShort;
				}
			}
			if (IsNull( obj ) )	return _nullShort;
			else				return Convert.ToInt16( obj );
		}
		#endregion AsShort	

		#region AsInt
		static public Int32 AsInt( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullInt;
				}
			}

			if (IsNull( obj ) )	return _nullInt;
			else				return Convert.ToInt32( obj );
		}
		#endregion AsInt	

		#region AsLong
		static public Int64 AsLong( Object obj )
		{
			if( obj is string )
			{
				if( ((string) obj) == "" )
				{
					return  _nullLong;
				}
			}
			if (IsNull( obj ) )	return _nullLong;
			else				return Convert.ToInt32( obj );
		}
		#endregion AsLong	

		#region AsObject
		static public Object AsObject( Object obj )
		{
			if (IsNull( obj ) )	return _nullObject;
			else				return obj;
		}


		static public Object AsObject( string valor, string tipoDestino )
		{
			if ( valor == null )return _nullObject;
			if ( valor == "" )	return _nullObject;
			object ret = null;
			ObjConvert.SetValue( ref ret, valor, tipoDestino );
			return ret;
		}

		#endregion AsObject	


		#endregion obtener valores

		#region Comparaciones

		public static int Compare( Object obj1, Object obj2 )
		{
			if( obj1 is DBNull && obj2 is DBNull ){	return 0; }
			if( obj1 is DBNull && !(obj2 is DBNull) ){	return -1; }
			if( obj2 is DBNull && !(obj1 is DBNull) ){	return 1; }
			string tipo1 = obj1.GetType().Name;
			string tipo2 = obj2.GetType().Name;
			int result = -7;

			switch( tipo1 )  
			{
				case "Boolean":
					result = Convert.ToBoolean( obj1 ).CompareTo( Convert.ToBoolean( obj2 ) );
					break;
				case "DateTime":
					result = Convert.ToDateTime( obj1 ).CompareTo( Convert.ToDateTime( obj2 ) );
					break;
				case "Byte":
				case "Decimal":
				case "Double":
				case "Single":
				case "int64":
				case "Int32":
				case "Int16":
					result = Convert.ToDouble( obj1 ).CompareTo( Convert.ToDouble( obj2 ) );
					break;
				case "String":
					result = Convert.ToString(obj1).CompareTo(Convert.ToString(obj2));
					break;
				case "Byte[]":
				case "byte[]":
					result = ((byte[]) obj1).Length.CompareTo( ((byte[]) obj2).Length );
					break;
			} // switch
			if( result != -7 ) return result;
			throw new Exception(" Comparación invalida entre " + tipo1 + " y " + tipo2 );
		}

		
		#endregion Comparaciones

		#endregion Metodos
	
	}


	#endregion ObjConvert
	
}
