using System;
using System.Data;
using System.Data.SqlClient;

#region Audit

namespace Berke.Excep
{
	public class Audit
	{
		private Audit()
		{
		}

		public static void LogEntry( string Clave, string Mensaje )
		{
			string userName;
			try
			{
				Berke.Libs.Base.Acceso acc = new Berke.Libs.Base.Acceso();
				userName = acc.Usuario;
			}
			catch(Exception ){
				userName = "unknown user";				
			}


			// Sacar la Fecha de algun lado
			DateTime now = DateTime.Now;
			Mensaje = userName+"["+now.ToString()+"] "+Mensaje;
			System.Diagnostics.EventLog.WriteEntry( Clave, Mensaje, System.Diagnostics.EventLogEntryType.Information );
			
		}

		public static void Trace( string Mensaje )
		{
			LogEntry( "SGE_TRACE", Mensaje );
		}
	}

}
#endregion Audit

namespace Berke.Excep
{
	/// <summary>
	/// Summary description for errors.
	/// </summary>

	#region Base
	namespace Base
	{
		public class BaseException : Exception
		{
			protected string _Message;
			protected string _Descrip;

			public BaseException()
			{
				_Message = "";
				_Descrip = "Excepción";
			}
			
			public BaseException( string Mensaje )
			{
				_Message = Mensaje;
				_Descrip = "Excepción";
			}		
			
			public override string Message
			{
				get
				{
					return _Descrip + ":" + _Message;
				}
			}
	
		}

	
	
		public class BusinessException : BaseException
		{
			public BusinessException( ) : base( )
			{
				_Descrip = _Descrip +="/Negocios";
			}

			public BusinessException( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Negocios";
			}
		}


		public class TechnicalException : BaseException
		{
			public TechnicalException( ) : base( )
			{
				_Descrip = _Descrip +="/Error técnico";
			}
			public TechnicalException( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Error técnico";
			}
		}

	} // end namespace Base
	#endregion Base


	#region BusinessExcetions
	namespace Biz
	{
		using Berke.Excep.Base;

		#region TooManyRowsException
		public class TooManyRowsException : BusinessException
		{
			public string Tabla = "";
			public int Limite = 0;
			public int Recuperados= 0;
			public TooManyRowsException( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Filas Excedidas";
			}

			public TooManyRowsException( string TableName, int NumberOfRowsLimit ) : base()
			{
				Tabla		= TableName;
				Limite		= NumberOfRowsLimit;
				_Descrip	= _Descrip +="/Filas Excedidas";
				this._Message = " Tabla: "+ TableName + "  Limite: " + NumberOfRowsLimit.ToString();					
			}


			public TooManyRowsException( string TableName, int NumberOfRowsLimit, int RowsFound ) : base()
			{
				Tabla		= TableName;
				Limite		= NumberOfRowsLimit;
				Recuperados = RowsFound;
				_Descrip	= _Descrip +="/Filas Excedidas";

				this._Message = " Tabla: "+ TableName + "; los "+ RowsFound.ToString() + 
                    " registros obtenidos exceden el limite de " + NumberOfRowsLimit.ToString();					
			}
		}
		#endregion TooManyRowsException

		#region UsuarioNoRegistrado
		public class UsuarioNoRegistrado : BusinessException
		{
			public UsuarioNoRegistrado( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Usuario No Registrado";
			}

			public UsuarioNoRegistrado( string userName, string Mensaje ) : base()
			{
				_Descrip = _Descrip +="/Usuario No Registrado";
				this._Message = " Usuario: [ "+ userName + " ]" +  Mensaje ;					
			}
		}

		#endregion UsuarioNoRegistrado

		#region ClaveInexistente
		public class ClaveInexistente : BusinessException
		{
			public ClaveInexistente( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/ClaveInexistente";
			}

			public ClaveInexistente( string tabla, object ID, string Mensaje ) : base()
			{
				_Descrip = _Descrip +="/ClaveInexistente";
				this._Message = " Tabla: [ "+ tabla + " ] ID:[ " +  Convert.ToString(ID) + " ] " + Mensaje;					
			}
		}
		#endregion ClaveInexistente

		#region OperacionRestringida
		public class OperacionRestringida : BusinessException
		{
			public OperacionRestringida( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Operacion_Restringida";
			}
		}
		#endregion OperacionRestringida

	} // end namespace Biz
	#endregion BusinessExcetions


	#region TechnicalExceptions
	namespace Tech
	{
		using Berke.Excep.Base;

		#region UnexpectedDataType
		public class UnexpectedDataType : TechnicalException
		{
			public UnexpectedDataType( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Tipo de dato inesperado";
			}
		}
		#endregion

		#region UnexpectedColumnName
		public class UnexpectedColumnName : TechnicalException
		{
			public UnexpectedColumnName( string Mensaje ) : base( Mensaje )
			{
				_Descrip = _Descrip +="/Nombre de columna inesperado";
			}
		}
		#endregion

		#region DataSetNameMissmatch		
		public class DataSetNameMissmatch : TechnicalException
		{
			public DataSetNameMissmatch( string DataSetName1, string DataSetName2 ) : base( )
			{
				_Descrip = _Descrip +="/Los nombre de DataSet ["+DataSetName1+"] y ["+DataSetName2+"] no coinciden";
			}
		}
		#endregion

		#region FileNotExists
		public class FileNotExists : TechnicalException
		{
			public FileNotExists( string fileName ) : base( )
			{
				_Descrip = _Descrip +="/El archivo ["+fileName+"] no existe";
			}
		}
		#endregion

	} // end namespace Tech

	#endregion TechnicalExceptions


	//	public class BaseException : Exception
	//	{
	//		private string _Message;
	//
	//		public BaseException( string Source, string ErrorCode )
	//		{
	//			_Message = Management.GetMessage( ErrorCode );
	//			Management.LogError( Source, _Message );
	//		}
	//		
	//		public BaseException( string Source, string ErrorCode, Exception ex ) : base( "", ex )
	//		{
	//			_Message = Management.GetMessage( ErrorCode );
	//			Management.LogError( Source, _Message );
	//		}
	//
	//		public override string Message
	//		{
	//			get
	//			{
	//				return _Message;
	//			}
	//		}
	//	
	//	}
	//
	//	public class BusinessException : BaseException
	//	{
	//		public BusinessException( string ErrorCode ) : base( "Business", ErrorCode )
	//		{
	//		}
	//	}
	//
	//	public class TechnicalException : BaseException
	//	{
	//		public TechnicalException( string ErrorCode ) : base( "Technical", ErrorCode )
	//		{
	//		}
	//
	//		public TechnicalException( string ErrorCode, Exception ex ) : base( "Technical", ErrorCode, ex )
	//		{
	//		}
	//	}
	//
	//	public class Management
	//	{
	//		public static string GetMessage( string errorcode )
	//		{
	//			string message = "Codigo no encontrado: " + errorcode.ToString();
	//
	//			try
	//			{
	//				using( SqlConnection c = (SqlConnection) Management.GetDBConnection( "ERROR" ) )
	//				{
	//					SqlCommand cmd = new SqlCommand( "Select description from msg_error where error_code=@error_code", c );
	//					cmd.Parameters.Add( "@error_code", SqlDbType.NVarChar ).Value = errorcode;
	//					cmd.CommandType = CommandType.Text;
	//					SqlDataReader rdr = cmd.ExecuteReader();
	//					if( rdr.Read() )
	//						message = rdr[0].ToString();
	//					
	//					rdr.Close();
	//				}
	//			}
	//			catch
	//			{
	//			
	//			}
	//
	//			return message;
	//		}
	//
	//		static Management()
	//		{
	//			//Load plugins
	//
	//		}
	//		
	//		private Management()
	//		{
	//		}
	//
	//		/// <summary>
	//		/// Errores. Solo para demostracion de la arquitectura. 
	//		/// Probalemente se deba utilizar EMAB: Exception Management Application Block
	//		/// </summary>
	//		/// <param name="Source"></param>
	//		/// <param name="ErrorCode"></param>
	//		public static void LogErrorMessage( string Source, string ErrorCode )
	//		{
	//			LogError( Source, GetMessage( ErrorCode ) );
	//		}
	//
	//		public static void LogError( string Source, string Error )
	//		{
	//			System.Diagnostics.EventLog.WriteEntry( Source, Error, System.Diagnostics.EventLogEntryType.Error );
	//		}
	//
	//		public static void LogException( string Source, Exception ex )
	//		{
	//			LogError( Source, BuildExceptionMessage( ex ) );
	//		}
	//
	//		public static void LogErrorAndThrow( string Source, string ErrorCode )
	//		{
	//			string msg = GetMessage( ErrorCode );
	//			LogError( Source, msg );
	//			throw new Exception( msg );
	//		}
	//
	//		private static string BuildExceptionMessage( Exception ex )
	//		{
	//			if( ex.InnerException == null )
	//				return ex.Message;
	//
	//			return ex.Message + "\n" + BuildExceptionMessage( ex.InnerException );
	//		}
	//
	//		public static string GetParameter( string name, string defaultValue )
	//		{
	//			return "p_" + name;
	//		}
	//
	//		/// <summary>
	//		/// Deberia sacar de una tabla, NO HARDCODEADO
	//		/// </summary>
	//		/// <param name="name"></param>
	//		/// <returns></returns>
	//		public static IDbConnection GetDBConnection( string name )
	//		{
	//			SqlConnection c = new SqlConnection( "Data Source=MORPHEUS;Integrated Security=SSPI;Initial Catalog=Marcas;" );
	//			c.Open();
	//			return c;
	//		}	
	//	}

} // End namespace Berke.Excep