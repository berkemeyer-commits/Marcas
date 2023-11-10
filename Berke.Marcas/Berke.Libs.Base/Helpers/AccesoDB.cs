using System;

namespace Berke.Libs.Base.Helpers
{
	using System.Data;
	using System.Collections;
	using System.Data.SqlClient;
	using Berke.Libs.Base.DSHelpers;
	using Berke.DG.Base;
    using System.Web.Configuration;

	#region Helper_de_acceso_a_datos

	#region Param Class
	internal class Param 
	{

		#region Datos Miembro
		
		string		_nombre;
		SqlDbType	_sqlType;
		int			_longitud;
		object		_valor;

		#endregion Datos Miembro	

		#region Constructor
		public Param(){	}

		public Param(string	nombre, SqlDbType sqlType, int longitud, object valor)
		{
			_nombre		= nombre;
			_sqlType	= sqlType;
			_longitud	= longitud;
			_valor		= valor;
		}

		#endregion Constructor

		#region Propiedades

		public string Nombre		{ get { return _nombre; }	set{ _nombre = value;} }

		public SqlDbType SqlType	{ get { return _sqlType;}	set{ _sqlType = value;} }

		public int Longitud			{ get { return _longitud;}	set{ _longitud = value;} }

		public object Valor			{ get { return _valor;}		set{ _valor = value;} }

		#endregion Propiedades
	}

	#endregion Param Class

	#region AccesoDB Class

	public class AccesoDB
	{
		#region Datos Miembro

		private string _DataBaseName;
		private string _ServerName;
		private string _dbUser;
		private string _pwd;
		private SqlCommand Comando;
		private SqlConnection Conex;
		private SqlTransaction Trans;

		private String CadenaConex;
		private String vSql; 

		private ArrayList param;

		private bool _dbAutentication;
		private int _timeOut;   // en segundos
        #endregion Datos Miembro

        #region Constantes
        private const string CADENA_CONEX = "cadenaConex";
        #endregion Constantes

        #region Constructores y afines 


        public AccesoDB()
		{
            string cadenaConex = this.GetCadenaConexFromConfig();

            //if (WebConfigurationManager.AppSettings[CADENA_CONEX] != null)
            //{
            //    cadenaConex = WebConfigurationManager.AppSettings[CADENA_CONEX].ToString();
            //}

            if (cadenaConex == string.Empty)
            {
                _DataBaseName = "Indefinido";
                _ServerName = "Indefinido";
                _dbUser = "sa";
                _pwd = dream;
                _dbAutentication = false;
                _timeOut = 90;
                init();
            }
            else init(cadenaConex);
		}

		~AccesoDB(){
			this.CerrarConexion();
		}

		public AccesoDB( string pServerName, string pDataBaseName)
		{
			_ServerName		= pServerName;
			_DataBaseName	= pDataBaseName;
			_dbUser			= "sa";
			_pwd			= dream;
			_dbAutentication = false;
			_timeOut = 90;
			init();
		}


		public AccesoDB( string pServerName, string pDataBaseName, string dbUser, string pwd )
		{
			_ServerName		= pServerName;
			_DataBaseName	= pDataBaseName;
			_dbUser = dbUser;
			_pwd    = pwd;
			_dbAutentication = true;
			_timeOut = 90;
			init();
		}

        public AccesoDB(string cadenaConex)
        {
            init(cadenaConex);
        }

        private void init(string cadenaConex)
        {
            CadenaConex = cadenaConex;
            Trans = null;
            Conex = null;
            param = null;
        }

		private void init()
		{
			if( _dbAutentication )
			{
				//CadenaConex = "server="+ _ServerName+";user id=sa;pwd="+ _pwd + ";Database="+ _DataBaseName;
                CadenaConex = "server=" + _ServerName + ";user id=" + _dbUser + ";pwd=" + _pwd + ";Database=" + _DataBaseName;
			}
			else
			{ 
				CadenaConex = "packet size=4096; integrated security=SSPI; Trusted_Connection=Yes; Connect Timeout="+_timeOut.ToString() +";  Pooling=no; Current Language=us_english; data source="+ _ServerName+"; persist security info=False; initial catalog="+_DataBaseName + ";Network Library=dbmssocn";
                //CadenaConex = "packet size=4096; integrated security=SSPI; Trusted_Connection=Yes; Connect Timeout="+_timeOut.ToString() +";             Current Language=us_english; data source="+ _ServerName+"; persist security info=False; initial catalog="+_DataBaseName;
                //CadenaConex = "packet size=4096; integrated security=SSPI; Trusted_Connection=Yes; Connect Timeout=90; Pooling=no; Current Language=us_english; data source="+ _ServerName+"; persist security info=False; initial catalog="+_DataBaseName;
				//CadenaConex = "packet size=4096;Trusted_Connection=Yes;data source="+ _ServerName+";persist security info=False;initial catalog=BerkeDBMgr4";
			}

			Trans = null;
			Conex = null;	
			param = null;
		}

        private string GetCadenaConexFromConfig()
        {
            string cadenaConex = string.Empty;
            if (WebConfigurationManager.AppSettings[CADENA_CONEX] != null)
            {
                cadenaConex = WebConfigurationManager.AppSettings[CADENA_CONEX].ToString();
            }
            return cadenaConex;
        }

		#endregion Constructores y afines

		#region Propiedades

		public int TimeOut 
		{
			get{ return this._timeOut;}
			set{ this._timeOut = value; init(); }
		}

		public bool DBAutentication {
			get{ return _dbAutentication;}
			set{ _dbAutentication = value;}
		}

		#region TransaccionActiva
		private bool TransaccionActiva 
		{
			get
			{ 
				if( Trans != null )
				{
					if( Trans.Connection != null )
					{
						if( Trans.Connection.State == ConnectionState.Open )
						{
							return true;
						}
					}
				};
				return false;
			}
		}
		#endregion
		
		#region ConexionActiva

		private bool ConexionActiva
		{
			get
			{ 
				if( Conex != null )
				{
					if ( Conex.State == ConnectionState.Open )
					{
						if( Comando != null ) 
						{
							return true;	
						}
					}
				}
				return false; 
			}
		}
		#endregion ConexionActiva

		#region DataBaseName
		public string  DataBaseName 
		{
			get { return _DataBaseName;}
			set
            {
                _DataBaseName = value;

                string cadenaConex = this.GetCadenaConexFromConfig();
                if (cadenaConex == string.Empty)
                    init();
                else init(cadenaConex);
            }
		}
		#endregion DataBaseName

		#region ServerName
		public string  ServerName 
		{
			get { return _ServerName;}
			set
            {
                _ServerName = value;

                string cadenaConex = this.GetCadenaConexFromConfig();
                if (cadenaConex == string.Empty)
                    init();
                else init(cadenaConex);
            }
		}
		#endregion ServerName

		#region DBUser
		public string  DBUser 
		{
			get { return _dbUser;}
			set { _dbUser = value; init(); }
		}
		#endregion DBUser

		#region Password
		public string  Password 
		{
			get { return _pwd;}
			set { _pwd = value; init(); }
		}
		#endregion Password

		#region Sql
		public string  Sql 
		{
			get { return vSql;  }
			set { vSql = value;	}
		}
		#endregion Sql

		#region dream
		private string dream 
		{
			get 
			{
				string ret = "";
				int[] dig  = { 98,70,116,110,105,47,121,120,84,51,52,61,60,61,66};
				for( int i = 0; i < dig.Length ; i++ )
				{
					ret+= ( (char) (dig[i]-i) ).ToString();
				}return ret; 
			}
		}
		#endregion dream

		#region ParamCount

		public int ParamCount 
		{
			get
			{
				if( param == null )	
				{
					return 0;
				}
				else
				{
					return param.Count;
				}
			}
		}

		#endregion ParamCount

		#region CommandString
		public string CommandString
		{
			get
			{
				string buf = vSql;
				int i;
				string nullStr = ObjConvert.NullString;
				ObjConvert.NullString = "<NULL>";
				ObjConvert.SetCulture( CultureFormat.DB_Culture);
				
				for( i = 0 ; i < ParamCount; i++ ) 	
				{
					Param par = (Param ) param[i];
					buf = buf.Replace( par.Nombre + " ", "["+ObjConvert.AsString( par.Valor )+"] " );
					buf = buf.Replace( par.Nombre + ",", "["+ObjConvert.AsString( par.Valor )+"]," );
				}
				ObjConvert.SetCulture(CultureFormat.UI_Culture );
				ObjConvert.NullString = nullStr;
				
				return buf;
			}
		}
		#endregion CommandString()


		#endregion Propiedades

		#region EstablecerConexion()

		public void  EstablecerConexion()
		{
			if( this.TransaccionActiva )
			{
				throw new Exception("Error en EstablecerConexion(): Transaccion aun activa.");
			}

			if( Conex == null ) 
			{
                CadenaConex = "server=ber-sql-prod.berke.com.py;user id=ggaleano;pwd=C0r0navirus;Database=BerkeDB";
                Conex = new SqlConnection(CadenaConex);
				Comando = new SqlCommand();
				Comando.Connection = Conex;
                Comando.CommandTimeout = 90;	
			}
			if( Conex.State != ConnectionState.Open )
			{
				Conex.Open();
			}
			Trans = null;
		}
					
		#endregion EstablecerConexion()

		#region CerrarConexion()
		public void CerrarConexion()
		{
			if( ConexionActiva )
			{ 
				try
				{
					Conex.Close();
				}
				catch{}
			}
		}

		#endregion CerrarConexion()

		#region Transacciones

		#region IniciarTransaccion()
		public void IniciarTransaccion()
		{
			if( this.TransaccionActiva )
			{
				throw new Exception("Error en IniciarTransaccion():  La transaccion aun esta activa. No se puede volver a iniciar");
			}

			try	
			{
				if( ! ConexionActiva )
				{
					EstablecerConexion();
				}

				Trans = Conex.BeginTransaction();
				Comando.Transaction = Trans;
			}
			catch( Exception e )
			{
				CerrarConexion();
				throw new Exception(" Error al iniciar Transaccion : "+ e.Message  );
			}
		}

		#endregion IniciarTransaccion()

		#region Commit()
		public void Commit()
		{
			if( TransaccionActiva )
			{
				try	
				{
					Trans.Commit();
					Trans = null;
				}
				catch( Exception e )
				{
					CerrarConexion();
					throw new Exception(" Error en Commit :"+ e.Message  );
				}
			}
			else
			{
				throw new Exception( "Error en Commit(): La transaccion no esta activa");
			}
		}
		#endregion Commit()

		#region RollBack()
		public void RollBack()
		{
			if( TransaccionActiva )	
			{ 
				try
				{
					Trans.Rollback();
					Trans = null;
				}
				catch( Exception e )
				{
					CerrarConexion();
					throw new Exception(" Error en RollBack(): "+ e.Message  );
				}
			}
		}
			
		#endregion RollBack()

		#endregion Transacciones

		#region EjecutarDML

		public int EjecutarDML()
		{
			int regAfectados = 0;
			if( TransaccionActiva )
			{
				try
				{
					if( vSql.Trim() == "" ) return 1;
					Comando.CommandText = vSql;
					aplicarParametros( Comando );
//					string ver = this.CommandString;
					regAfectados = Comando.ExecuteNonQuery();
					return regAfectados;
				}
				catch( Exception e )
				{
					CerrarConexion();
					throw new Exception(" Error al ejecutar DML: "+ e.Message + " * " + this.CommandString  , e );
				}
			}
			else 
			{
				throw new Exception(" Error en EjecutarDML(): No hay Transaccion Activa");
			}

		}
		
		#endregion EjecutarDML

		#region getDataReader
		public SqlDataReader getDataReader()
		{
			SqlDataReader Result;

			try
			{		
				if( ! ConexionActiva )	EstablecerConexion();

				Comando.CommandText = vSql;
				aplicarParametros( Comando );
				Result = Comando.ExecuteReader();  
				return Result;
			}
			catch( Exception e )
			{
				CerrarConexion();
				throw new Exception(" Error al ejecutar getDataReader() " + e.Message , e );
			}
		
		}

		public SqlDataReader getDataReaderAlone()
		{
			SqlDataReader Result;
			SqlConnection Conex1 = null;
			SqlCommand Comando1;
			try
			{
				Conex1 = new SqlConnection(CadenaConex);
				Comando1 = new SqlCommand();
				Comando1.Connection = Conex1;
				Conex1.Open();
				
				Comando1.CommandText = vSql;
				aplicarParametros( Comando1 );
				Result = Comando1.ExecuteReader(CommandBehavior.CloseConnection);  
				return Result;
			}
			catch( Exception e )
			{
				if( Conex1 != null ) 
				{ 
					if ( Conex1.State == ConnectionState.Open )
					{
						Conex1.Close();
					}
				}
				throw new Exception(" Error al ejecutar getDataReader() " + e.Message , e );
			}
		}

		#endregion getDataReader

		#region FillDataSet

		public void FillDataSet( DataSet ds)
		{
			SqlDataAdapter sqlDa;
			try
			{
				ds.Clear();
				if( ! ConexionActiva )	EstablecerConexion();
				Comando.CommandText = vSql;
				aplicarParametros( Comando );
				sqlDa = new SqlDataAdapter(Comando );
				sqlDa.Fill(ds);
			}
			catch( Exception e )
			{
				CerrarConexion();
				throw new Exception(" Error al ejecutar FillDataSet() "+ e.Message, e  );
			}
		}

		public void FillDataSet( DataSet ds, String  pTabla )
		{
			SqlDataAdapter sqlDa;
			try
			{
				ds.Tables[pTabla].Clear();

				if( ! ConexionActiva )	EstablecerConexion();
				Comando.CommandText = vSql;
				aplicarParametros( Comando );
				sqlDa = new SqlDataAdapter( Comando );
				sqlDa.Fill(ds, pTabla);
			}
			catch( Exception e )
			{
				CerrarConexion();
				throw new Exception(" Error al ejecutar FillDataSet() "+ e.Message , e );
			}
		}

		#endregion FillDataSet

		#region getValue

		public Object getValue( ) 
		{
			Object val;
			try
			{
				if( ! ConexionActiva )	EstablecerConexion();
				
				Comando.CommandText = vSql+ " ";
				aplicarParametros( Comando );

				val = Comando.ExecuteScalar();
//				if( ObjConvert.IsNull( val ) ) {
//					throw new Exception( "Error al recuperar ID de registro ingresado [" + CommandString + "]");
//				}
				return val;
			}
			catch( Exception e )
			{
				CerrarConexion();
				throw new Exception(" Error al ejecutar getValue() "+ e.Message , e  );
			}

		}
		#endregion getValue

		#region Parameters 

		#region ClearParams()
		public void ClearParams( )
		{
			this.param = null;
		}
		#endregion ClearParams()

		#region AddParam()
		public void AddParam( string nombre, SqlDbType sqlType, object valor, int longitud )
		{
			if( this.param == null )
			{
				param = new ArrayList();
			}
			param.Add( new Param(nombre, sqlType, longitud, valor ) ); 
		}
		#endregion AddParam()

		#region SetParam()
		public void SetParam( string nombre, object valor )
		{
			if( this.param == null ){
				throw new Exception("AccesoDB.SetParam(). No existe parametro: "+ nombre);
			}
			for( int i= 0; i < param.Count; i++ ){
				Param par = (Param) param[i];
				if( par.Nombre == nombre ){
					par.Valor = valor;
					break;
				}
			}
		}
		#endregion SetParam()

		#region aplicarParametros()
		private void aplicarParametros( SqlCommand cmd )
		{
			int i;
			cmd.Parameters.Clear();
//			if( cmd.Parameters != null ) cmd.Parameters.Clear();
			for( i = 0 ; i < ParamCount; i++ ) 	
			{
				Param par = (Param ) param[i];
				object val =  par.Valor;
				cmd.Parameters.Add( par.Nombre,par.SqlType, par.Longitud).Value = par.Valor;
			}	
		}
		#endregion aplicarParametros()
	
		#endregion Parameters

	
	} // end class AccesoDB

	#endregion AccesoDB Class

	#endregion Helper_de_acceso_a_datos
}

#region Adapter
namespace Berke.DG.Adapters
{
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using System.Data;
	using System.Collections;
	using System.Data.SqlClient;
	using Berke.DG.Base;

	#region FMapper class 
	public class FMapper 
	{

		#region Datos Miembro

		string		_dtColName;
		int			_dtColIndex;
		string		_sqlColName;
		string		_dbColName;
		int			_len;
		SqlDbType	_sqlDbType;
		bool		_checkConcurrence;

		#endregion Datos Miembro
	
		#region constructores
		
		public FMapper( ){
			
		}
		

		#endregion Constructores

		#region Propiedades
		
		public string		DtColName		{ get { return _dtColName;}			set{ _dtColName = value;}}
		public int			DtColIndex		{ get { return _dtColIndex;}		set{ _dtColIndex = value;}}
		public string		SqlColName		{ get { return _sqlColName;}		set{ _sqlColName = value;}}
		public string		DbColName		{ get { return _dbColName;}			set{ _dbColName = value;}}
		public int			Len				{ get { return _len;}				set{ _len = value;}}
		public SqlDbType	SqlDbType		{ get { return _sqlDbType;}			set{ _sqlDbType = value;}}
		public bool		CheckConcurrence	{ get { return _checkConcurrence;}	set{ _checkConcurrence = value;}}

		#endregion Propiedades


	}

	#endregion FMapper class

	#region AdapterBase class
	public class AdapterBase 
	{

		#region Datos Miembro
		
			protected Berke.Libs.Base.DSHelpers.DSTab _dst;
			protected AccesoDB _db;
			protected string _dbTableName;

			protected FMapper[] _fMap;
			protected System.Collections.ArrayList _paramLst;

			protected string	_defaultWhere;

			protected bool _ConcurrenceOn;

			protected bool _AutogeneratedID;

			protected 	SqlDataReader _dr = null;
			
			protected int _top = -1;

		#endregion Datos Miembro
		
		#region Constructores y afines
		
			#region Constructor por defecto
				public AdapterBase( ) 
				{
					_dst = null;
					_db  = null;
					_dbTableName = null;
					_defaultWhere = "";
					_AutogeneratedID = true;

				}
			#endregion Constructor por defecto

			#region Constuctor basado en TableBase

				public AdapterBase( TableBase dst,  AccesoDB db ) 
				{
					Bind( dst.DST, db, dst.DST.Table.TableName );
					_AutogeneratedID = true;
					_dbTableName = dst._dbTableName;
				}

	
				public AdapterBase( TableBase dst,  AccesoDB db, string dbTableName  ) 
				{
					Bind( dst.DST, db, dbTableName );
					_AutogeneratedID = true;
					_dbTableName = dst._dbTableName;
				}

				public void Bind( TableBase dst,  AccesoDB db ) 
				{
					Bind( dst.DST, db, dst.DST.Table.TableName );
					_dbTableName = dst._dbTableName;
				}
			
				public void Bind( TableBase dst,  AccesoDB db, string dbTableName ) 
				{
					Bind( dst.DST, db, dbTableName );
					_dbTableName = dst._dbTableName;
				}
			
	
			#endregion Constuctor basado en TableBase

			#region Constuctor basado en DSTab

				public AdapterBase( DSTab dst,  AccesoDB db ) 
				{
					Bind( dst, db, dst.Table.TableName );
					_AutogeneratedID = true;
				}

				public AdapterBase( DSTab dst,  AccesoDB db, string dbTableName  ) 
				{
					Bind( dst, db, dbTableName );
					_AutogeneratedID = true;
				}
			

			#endregion Constuctor basado en DSTab

			#region Bind 
				public void Bind( DSTab dst,  AccesoDB db, string dbTableName ) 
				{
					_dst = dst;
					_db  = db;
					_defaultWhere = "";
					_ConcurrenceOn = false;
					_dbTableName = dbTableName;
					_fMap = new FMapper[ _dst.Dat.numCols ];
					_paramLst = null;
					for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
					{
						_fMap[i] = new FMapper();
						_fMap[i].DtColIndex			= i;
						_fMap[i].DtColName			= _dst.Dat.Column[i].ColName;
						_fMap[i].DbColName			= _dst.Dat.Column[i].ColName;
						_fMap[i].SqlColName			= _dst.Dat.Column[i].ColName;
						_fMap[i].CheckConcurrence	= true;
						_fMap[i].Len				= 0;
						_fMap[i].SqlDbType			= getSqlType( _dst.Dat.Column[i].ColType );
					}
				}

			#endregion Bind 

			#region SetDefaultWhere

		public void SetDefaultWhere( string whereString ){
			this._defaultWhere = whereString;
		}

		#endregion SetDefaultWhere

		#endregion Constructores y afines

		#region Propiedades
		
		/// <summary>
		/// Establece y recupera el número máximo de filas a recuperar<see cref="AdapterBase"/>.
		/// </summary>
		/// <value>Número de filas a recuperar.</value>
		/// mbaez
		public int Top { get { return _top;} set { _top = value;}}		
	    
		public bool AutogeneratedID{ get {return _AutogeneratedID;} set{ _AutogeneratedID = value;}}

		public bool ConcurrenceOn { get {return false; /*_ConcurrenceOn;*/} set{ _ConcurrenceOn = value;}}
		public string DbTableName { get { return _dbTableName;} set { _dbTableName = value;}}

		public FMapper[] FMap { get { return _fMap; } }
		public string CommandString	{ get {	return _db.CommandString;} }
		public AccesoDB Db { get { return _db; } }
		#endregion Propiedades

		#region Params


		#region ClearParams()
		public void ClearParams()
		{
			this._paramLst = null;
		}
		#endregion ClearParams()

		#region AddParam()
		public void AddParam( string nombreParam, object valor )
		{
			nombreParam = nombreParam.Trim();
			if( _defaultWhere.IndexOf( nombreParam ) == -1 ){
				throw new Exception("AdapterBase::AddParam(). Parametro [ " + nombreParam + " ] no corresponde" );
			}
			string nombreCampo;
			if( nombreParam.Substring(0,1) == "@" )
			{
				nombreCampo = nombreParam.Substring( 1 );
			} else {
				nombreCampo = nombreParam;
				nombreParam = "@" + nombreParam;
			}
			if( this.indexOf( nombreCampo ) != -1 ){ throw new Exception( "El nombre de parametro no puede coincidir con un nombre de columna");}
			
			if( valor is ArrayList ){
				string nameLst = "";
				ArrayList lst = (ArrayList) valor;
				for( int j = 0; j < lst.Count; j++ ){
					string nombre = nombreParam + "_" + j.ToString().Trim();
					nameLst+= nombre + ", ";
					SqlDbType tipo = getSqlType( lst[j].GetType().Name );
					if( this._paramLst == null ) _paramLst = new System.Collections.ArrayList();
					_paramLst.Add( new Param(nombre, tipo, 0, lst[j] ));		
				}		
				nameLst = nameLst.Substring(0, nameLst.Length - 2);
				this._defaultWhere = _defaultWhere.Replace( nombreParam, nameLst);
			}
			else
			{
				SqlDbType tipo = getSqlType( valor.GetType().Name );
				if( this._paramLst == null ) _paramLst = new System.Collections.ArrayList();
				_paramLst.Add( new Param(nombreParam, tipo, 0, valor ));		
			}	 
		}
		#endregion AddParam()

		#region setDefaultWhereParams()
		private void setDefaultWhereParams()
		{
			if( _paramLst == null ) return;
			for( int i = 0 ; i < _paramLst.Count ; i++ ){

				Param par = (Param) _paramLst[i];
				_db.AddParam( par.Nombre, par.SqlType, par.Valor, par.Longitud );
			}
		}
		#endregion setDefaultWhereParams()
		
		#region setFilterWhereParams()
		private void setFilterWhereParams() 
		{
			for( int i= 0; i < _dst.Filter.numCols ; i++ ) 
			{
				Object filObj = _dst.Filter[i];

				if( ObjConvert.IsNull( filObj )  ){ continue; }

				if( !(filObj is DSFilter) )	
				{
					filObj = new DSFilter(_dst.Filter[i]); 
				}
				DSFilter fil  = (DSFilter) filObj;

				#region Single Value
				if( fil.Value != null ) {
					string nombre = "@" + _fMap[i].DtColName;
					_db.AddParam( nombre, _fMap[i].SqlDbType, fil.Value, _fMap[i].Len );			
				}		
				#endregion Single Value

				#region Range 
				if( fil.MinValue != null )
				{
					string nombre1 = "@Min_" + _fMap[i].DtColName;
					string nombre2 = "@Max_" + _fMap[i].DtColName;
					_db.AddParam( nombre1, _fMap[i].SqlDbType, fil.MinValue, _fMap[i].Len );	
					_db.AddParam( nombre2, _fMap[i].SqlDbType, fil.MaxValue, _fMap[i].Len );	
				}
				#endregion Range 

				#region Lista 
				if( fil.List != null ) 
				{
					ArrayList lst = (ArrayList) fil.List;
					for( int j = 0; j < lst.Count; j++ )
					{
						string nombre = "@" + _fMap[i].DtColName + "_" + j.ToString().Trim();
						_db.AddParam( nombre, _fMap[i].SqlDbType, lst[j], _fMap[i].Len );	
					}
				}
				#endregion Lista 
			}


		}
		#endregion setFilterWhereParams()

		#endregion Params

		#region ReadByID
		public bool ReadByID( params object[] ID ) 
		{
			#region Vacia la Tabla
			DataTable tab = _dst.Table.Clone();
			_dst.Table = tab;
			_dst.Dat.Init( tab );
			#endregion
			int rowCounter = 0;
			if( !(ID[0] is System.DBNull || ID == null) )
			{
							
				_db.ClearParams();
				setDefaultWhereParams();
				//			setFilterWhereParams();

				_db.Sql =	"SELECT " +
					selectList() +
					" FROM " +
					this.DbTableName +
					" WHERE " +
					this.ID_WhereString();

				#region Asignar parametros de UpdateWhereString()

				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if(_dst.Dat.Column[i].IsPK ) 
					{
						string par = "@old_" + _fMap[i].DtColName.Trim();
						Object obj = ID[i];
						_db.AddParam( par, _fMap[i].SqlDbType, obj, _fMap[i].Len );
					}
				}
				#endregion Asignar parametros de UpdateWhereString()


				SqlDataReader dr = _db.getDataReader();
				rowCounter = 0;
				while( dr.Read() )
				{  
					rowCounter++;
					_dst.NewRow(); 
					for( int i= 0; i < _dst.Dat.numCols; i++) 
					{ 
						_dst.Dat[i] = dr[i]; 
					} 
					_dst.PostNewRow(); 
				} 
				dr.Close();
			}

			_dst.AcceptAllChanges(); 
			_dst.GoTop();
			if( rowCounter != 1 ) return false;

			return true;
		}
		#endregion ReadByID

		#region Count
		public int Count( ){
			int ret = -1;
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.CountString();

			ret = ( int ) _db.getValue();
 
			return ret;
		}
		#endregion Count

		#region ReadAll
		/// <summary>
		/// Realiza una consulta a la base de acuerdo a los
		/// fitros especificados en el DBTab.
		/// </summary>
		/// <exception cref="Berke.Excep.Biz.TooManyRowsException">Si el conjunto de 
		/// datos es superior un número máximo predefinido</exception>
		public void ReadAll() 
		{
			ReadAll( 1000 ); // Sin limite de filas recuperadas
		}

		public string ReadAll_CommandString() 
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.SelectString();

			return _db.CommandString;
		}

		/// <summary>
		/// Realiza una consulta a la base de acuerdo a los
		/// fitros especificados en el DBTab.
		/// </summary>
		/// <param name="numberOfRowsLimit">Número máximo de filas a recuperar</param>
		/// <exception cref="Berke.Excep.Biz.TooManyRowsException">Si el conjunto de 
		/// datos es superior a <param>numberOfRowsLimit</param></exception>
		public void ReadAll( int numberOfRowsLimit ) 
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.SelectString();

//			string cadena = _db.CommandString;

			DataTable tab = _dst.Table.Clone();
			_dst.Table = tab;
			SqlDataReader dr = _db.getDataReader();
		
			int rowCounter = 0;
			while( dr.Read() ){  
				if( numberOfRowsLimit > 0 && rowCounter++ > numberOfRowsLimit ) 
				{
					throw new Berke.Excep.Biz.TooManyRowsException( _dst.Table.TableName, numberOfRowsLimit );
				}
				_dst.NewRow(); 
				for( int i= 0; i < _dst.Dat.numCols; i++){ 
					_dst.Dat[i] = dr[i]; 
				} 
				_dst.PostNewRow(); 
			} 
			dr.Close();
			_dst.AcceptAllChanges(); 
			_dst.GoTop();
		}

		#endregion ReadAll

		#region GetListOfID
		public ArrayList GetListOfID( ) 
		{
			return GetListOfID( false );
		}
		public ArrayList GetListOfID( bool distinct ) 
		{
			_db.ClearParams();
	
			_db.Sql = SelectIDListString( distinct );
		
			setDefaultWhereParams();
			setFilterWhereParams();

			string cadena = _db.CommandString;

			ArrayList aLst = new ArrayList();
			SqlDataReader dr = _db.getDataReader();
			while( dr.Read() )
			{  
					aLst.Add( dr[0] );  
			} 
			dr.Close();
			return aLst;
		}
		#endregion GetListOfID
		
		#region GetListOfField
		public ArrayList GetListOfField( Berke.DG.Base.Field field ) 
		{
			return GetListOfField( field, false );
		}

		public ArrayList GetListOfField( Berke.DG.Base.Field field, bool distinct ){
			_db.ClearParams();
			string listaCampos = SelectFieldListString( field.Name, distinct );
			if( listaCampos.Trim() == "" ){
				return new ArrayList();
			}
			_db.Sql = SelectFieldListString( field.Name, distinct );
			_db.Sql += " order by " + field.Name;

			setDefaultWhereParams();
			setFilterWhereParams();

			ArrayList aLst = new ArrayList();
			SqlDataReader dr = _db.getDataReader();
			while( dr.Read() )
			{  
				object obj = dr[0];
				if( obj != DBNull.Value )
				{
					aLst.Add( ObjConvert.AsString( obj ) );  
				}
			} 
			dr.Close();
			return aLst;
		}
		#endregion GetListOfField

		#region DataReader_Init
		public void DataReader_Init()
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.SelectString();

			_dr = _db.getDataReaderAlone(); // _db.getDataReader();
			

			_dst.NewRow(); 

			_dst.PostNewRow(); 


		}
		#endregion DataReader_Init

		#region DataReader_Read
		public bool DataReader_Read()
		{
			bool ret;

			ret = _dr.Read();

			if( ret )
			{
				_dst.Edit(); 
			
				for( int i= 0; i < _dst.Dat.numCols; i++)
				{ 
					_dst.Dat[i] = _dr[i]; 
				} 
				_dst.PostEdit(); 
			}
			return ret;
		}
		#endregion DataReader_Read

		#region DataReader_Close
		public void DataReader_Close()
		{
			_dr.Close();
		}
		#endregion DataReader_Close

		#region Metodos DML

		#region Lista de Campos ID
		private string ListaDeCamposID(){
			string ret="";
			for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
			{ 
				if(_dst.Dat.Column[i].IsPK ) 
				{
					if( ret == "")
					{
						ret+= _fMap[i].DbColName;
					}
					else{
						ret+= "," + _fMap[i].DbColName;					
					}
				}
			}
			return ret;
		}
		#endregion Lista de Campos ID


			#region InsertRow()

				public string InsertRow_CommandString()
				{
					if( _dst.IsEmpty ) return "";
					// Insertar
					_db.Sql = this.InsertString();	
					_db.ClearParams();
					for( int i= 0; i < _dst.Dat.numCols ; i++ ) 
					{
						if( (	! _dst.Dat.Column[i].IsPK && 
								! _dst.Dat.Column[i].IsReadOnly  &&
								! _dst.Dat.Column[i].IsAutoIncrement 
							) ||
								( _dst.Dat.Column[i].IsPK && ! _AutogeneratedID  )
						   ) 
						{
							Object obj = _dst.Dat[i];
							string nombre = "@" + _fMap[i].DtColName.Trim();
							_db.AddParam( nombre, _fMap[i].SqlDbType, obj, _fMap[i].Len );				
						}
					}
					return _db.CommandString;
				}
			
		
				public int InsertRow()
				{
					if( _dst.IsEmpty ) return -1;
					int newID = 0;

					// Insertar
					_db.Sql = this.InsertString();	
					_db.ClearParams();
					for( int i= 0; i < _dst.Dat.numCols ; i++ ) {
						if( ( ! _dst.Dat.Column[i].IsPK && 
							  ! _dst.Dat.Column[i].IsReadOnly  &&
							  ! _dst.Dat.Column[i].IsAutoIncrement 
							) ||
							( _dst.Dat.Column[i].IsPK && ! _AutogeneratedID  )
						  ) 
						{
							Object obj = _dst.Dat[i];
							string nombre = "@" + _fMap[i].DtColName.Trim();
							_db.AddParam( nombre, _fMap[i].SqlDbType, obj, _fMap[i].Len );	
						}

						if(_dst.Dat.Column[i].IsPK && ! _AutogeneratedID ){					
							newID = ObjConvert.AsInt( _dst.Dat[i] ); 
						}

					
					
					}
					int afectados = _db.EjecutarDML();
					if( afectados < 1 ){
						throw new  Exception( "SQL ERROR:  "+ _db.CommandString );
					}

					// Obtener ID asignado
					/* da problemas con los campos "computed"
					_db.Sql = InsertedRowIDString();
					_db.ClearParams();
					for( int i= 0; i < _dst.Dat.numCols ; i++ ) {
						if(  ! _dst.Dat.Column[i].IsPK && 
							 ! _dst.Dat.Column[i].IsReadOnly  &&
							 ! _dst.Dat.Column[i].IsAutoIncrement ) 
							{
							if( !ObjConvert.IsNull( _dst.Dat[i] )  )
							{
								if( _dst.Dat.Column[i].ColType != "Byte[]" )
								{
									Object obj = _dst.Dat[i];
									string nombre = "@" + _fMap[i].DtColName.Trim();
									_db.AddParam( nombre, _fMap[i].SqlDbType, obj, _fMap[i].Len );
								}
							}			
						} 
					}	
					int newID = (int) _db.getValue();
					*/
					if( _AutogeneratedID )
					{
						_db.Sql = "Select IDENT_CURRENT('"+_dst.Table.TableName.Trim() + "')";

						newID  = ObjConvert.AsInt( _db.getValue() );
					}
					return newID;
				}
			#endregion InsertRow()

			#region UpdateRow()

				#region PrepareUpdateRow()
				private void PrepareUpdateRow()
				{ // Asigna _db.Sql y los parametros a _db
					string sql;
					// String 
					sql = this.UpdateString();   // Campos del "SET"
					if ( sql.Trim() == "" ){ _db.Sql = "";	return;	}

					sql+= " WHERE " + UpdateWhereString();	
					_db.Sql = sql;
					_db.ClearParams();

					#region Asigna Parametros del "SET"
					int cont = 0;
					for( int i= 0; i < _dst.Dat.numCols ; i++ ) 
					{
						bool hayCambio =  ( ObjConvert.Compare( _dst.Dat[i], _dst.Old[i] ) != 0 );
						if(  !(	_dst.Dat.Column[i].IsPK ||
							_dst.Dat.Column[i].IsReadOnly || 
							_dst.Dat.Column[i].IsAutoIncrement )  && hayCambio ) 
						{
							Object obj = _dst.Dat[i];
							string nombre = "@" + _fMap[i].DtColName.Trim();
							_db.AddParam( nombre, _fMap[i].SqlDbType, obj, _fMap[i].Len );
							cont++;
						}
					}

							
					#endregion Asigna Parametros del "SET"
							
					#region Asignar parametros de UpdateWhereString()

					string par;
					for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
					{ 
						if(_dst.Dat.Column[i].IsPK || ( _ConcurrenceOn && _fMap[i].CheckConcurrence ) ) 
						{
							if(ObjConvert.IsNull( _dst.Old[i] )  )
							{
								;
							}
							else
							{
								if( _dst.Dat.Column[i].ColType != "Byte[]" && _fMap[i].SqlDbType != SqlDbType.NText && _fMap[i].SqlDbType != SqlDbType.Image )
								{
									par = "@old_" + _fMap[i].DtColName.Trim();
									Object obj = _dst.Old[i];
									#region Cambiar caracteres "comodin"
									if( _fMap[i].SqlDbType == SqlDbType.NVarChar || _fMap[i].SqlDbType == SqlDbType.NChar )
									{
										string tVal = (string)obj;
										tVal= tVal.Replace("[","->*");
										tVal= tVal.Replace("%","[%]");
										tVal= tVal.Replace("_","[_]");
										tVal= tVal.Replace("->*","[[]");
										
										obj = tVal;
				
									}
									#endregion
									_db.AddParam( par, _fMap[i].SqlDbType, obj, _fMap[i].Len );
								}
							}
						}
					}

					#endregion Asignar parametros de UpdateWhereString()

				}
				#endregion PrepareUpdateRow()

				#region UpdateRow()
				public void UpdateRow()	
				{
					if( _dst.IsEmpty ) return;

					int afectados = 0;

					PrepareUpdateRow();
					if( _db.Sql.Trim() == "" ) return;

					#region Ejecutar sentencia
					// --
//					string cmd = _db.CommandString;
					afectados = _db.EjecutarDML();
					if( afectados == 0 ){throw new Exception("Error de Concurrencia"); }
					if( afectados < 1 ){throw new Exception("Error en UpdateRow(). Reg.Afectados =" + afectados.ToString());}
					#endregion Ejecutar sentencia
				
				}
				#endregion UpdateRow()

				#region UpdateRow_CommandString()
				public string UpdateRow_CommandString()	
				{
					if( _dst.IsEmpty ) return "";
					PrepareUpdateRow();
					return _db.CommandString;
						
				}
				#endregion UpdateRow_CommandString()


			#endregion UpdateRow()

			#region DeleteRow()


				#region PrepareDeleteRow()
				private void PrepareDeleteRow()
				{ // Asigna _db.Sql y los parametros a _db
					string sql;
					// String 
					sql = this.DeleteString();
					if ( sql.Trim() == "" ){ _db.Sql = "";	return;	}

					sql+= " WHERE " + UpdateWhereString();	
					_db.Sql = sql;
					_db.ClearParams();
								
					#region Asignar parametros de UpdateWhereString() 

					string par;
					for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
					{ 
						if(_dst.Dat.Column[i].IsPK || ( _ConcurrenceOn && _fMap[i].CheckConcurrence ) ) 
						{
							if(ObjConvert.IsNull( _dst.Old[i] )  )
							{
								;
							}
							else
							{
								if( _dst.Dat.Column[i].ColType != "Byte[]" )
								{
									par = "@old_" + _fMap[i].DtColName.Trim();
									Object obj = _dst.Old[i];
									_db.AddParam( par, _fMap[i].SqlDbType, obj, _fMap[i].Len );
								}
							}
						}
					}

					#endregion Asignar parametros de UpdateWhereString()

				}
				#endregion PrepareDeleteRow()


				public void DeleteRow() 
				{
					if( _dst.IsEmpty ) return;
					int afectados = 0;

					PrepareDeleteRow();
					if( _db.Sql.Trim() == "" ) return;
				
					#region Ejecutar sentencia
					// --
					string cmd = _db.CommandString;
					afectados = _db.EjecutarDML();
					if( afectados == 0 ){throw new Exception("Error de Concurrencia");}
					if( afectados < 1 ){throw new Exception("Error en UpdateRow(). Reg.Afectados =" + afectados.ToString());}
					#endregion Ejecutar sentencia
				

				}

				public string DeleteRow_CommandString()	
				{
					if( _dst.IsEmpty ) return "";
					PrepareDeleteRow();
					return _db.CommandString;
								
				}


			#endregion DeleteRow()



		#endregion Metodos DML

		#region Metodos privados
 
			
			#region FilterWhereString()
			private string FilterWhereString(){
				string ret = "";			
				for( int i= 0; i < _dst.Filter.numCols ; i++ ) {
					Object filObj = _dst.Filter[i];

					if( ObjConvert.IsNull( filObj )  ){ continue; }

					if( !(filObj is DSFilter) )	{

						filObj = new DSFilter(_dst.Filter[i]); 
					}
					DSFilter fil  = (DSFilter) filObj;

					#region OperFieldName  
					if( fil.OperFieldName != null )
					{
						string where = defaultWhereString();
						string field = "";
						if( where.Trim() != "")
						{
							where+= " AND ";
						}

						for( int k = 0; k < _dst.Filter.numCols; k++ )
						{
							if( fil.OperFieldName == _fMap[k].DbColName )
							{
								field	= _fMap[k].SqlColName;
								break;
							}
						}
						where += this._fMap[i].SqlColName + " " + fil.Oper + " " + field;
						this.SetDefaultWhere( where );
						
					}
					#endregion OperFieldName

					#region Oper
//					if( fil.Oper != null )
//					{
//						ret+= this._fMap[i].SqlColName +" "+ fil.Oper +" @" + _fMap[i].DtColName;
//					}
					
					#endregion Oper

					#region Single Value
					if( fil.Value != null )
					{
						ret+= (ret == "") ? "" : " AND ";
						if( _fMap[i].SqlDbType == SqlDbType.Char ||
							_fMap[i].SqlDbType == SqlDbType.NChar ||
							_fMap[i].SqlDbType == SqlDbType.NText ||
							_fMap[i].SqlDbType == SqlDbType.NVarChar ||
							_fMap[i].SqlDbType == SqlDbType.Text ||
							_fMap[i].SqlDbType == SqlDbType.VarChar )
						{

							ret+= this._fMap[i].SqlColName + " like @" + _fMap[i].DtColName; //nca 22/12/05
						}
						else
						{
							ret+= this._fMap[i].SqlColName + " = @" + _fMap[i].DtColName;
						}
			
					}		
					#endregion Single Value
	
					#region Range 
					if( fil.MinValue != null )
					{
						ret+= (ret == "") ? "" : " AND ";

						ret+= this._fMap[i].SqlColName + " between "+"@Min_" + _fMap[i].DtColName +
														" and " +"@Max_" + _fMap[i].DtColName;

					}
					#endregion Range 

					#region Lista 
					if( fil.List != null ) 
					{
						ret+= (ret == "") ? "" : " AND ";
						ArrayList lst = (ArrayList) fil.List;
						if( lst.Count > 0 )
						{
							ret+= this._fMap[i].SqlColName + " IN ( ";
						
							string bf = "";
							for( int j = 0; j < lst.Count; j++ )
							{
								bf += ( bf == "") ? "" : ", ";
						
								bf+= "@" + _fMap[i].DtColName + "_" + j.ToString().Trim();
							}
							ret+= bf + " ) ";
						}
						else
						{
							ret+= " 1 > 2 ";   // Obligar a que no traiga nada
						}
					}
					#endregion Lista 
			

				}
			
				return ret;
			}

			#endregion FilterWhereString()

			#region defaultWhereString()

			private string defaultWhereString()
			{
				return _defaultWhere == null ? "" : _defaultWhere;
			}

			#endregion defaultWhereString()


			#region orderByString

			private string orderByString()
			{
				string lista = "";
				for( int i= 0; i < _dst.OrderList.Count; i++ )
				{
					Par par = (Par) _dst.OrderList[i];
					lista+= this._fMap[  par.Idx  ].DbColName + ((par.Sentido == DSOrden.Ascendente ) ? "" : " DESC " )  + ", ";

				}
				if( lista.Length > 0 )
				{
					lista= lista.Substring(0, lista.Length - 2);
					lista = " ORDER BY " + lista;
				}
				return lista;
			}

			#endregion orderByString


			#region CountString()
			public string CountString()
			{
				string ret= "SELECT count(*) ";
				ret+= " FROM "+ this.DbTableName;

				string filterWhere		= FilterWhereString();
				string defaultWhere		= defaultWhereString();
				if( filterWhere != "" || defaultWhere != "" )
				{
					ret+= " WHERE ";
				}
				if( defaultWhere != "" ) 
				{
					ret+= " "+ defaultWhere;
				}
				if( filterWhere != "" )
				{
					ret+= (defaultWhere != "") ? " AND " : "";
					ret+= filterWhere;
						
				}
				return ret + " ";
				
			}
			#endregion CountString()


		#region SelectIDListString()
		private string SelectIDListString( bool distinct  )
		{
			string lst = ListaDeCamposID();
			if( lst.IndexOf(",") != -1 ){
				throw new Exception("SelectIDList(): no es aplicable a IDs compuestos.Tabla"+this.DbTableName);
			}
			if( lst.Trim() == "" ){
				throw new Exception("SelectIDList(): ID no definido Tabla"+this.DbTableName);
			}
			string ret= "SELECT  ";
			if( distinct ){
				ret= "SELECT DISTINCT ";
			}
			ret+= lst;

			ret+= " FROM "+ this.DbTableName;

			string filterWhere		= FilterWhereString();
			string defaultWhere		= defaultWhereString();
			if( filterWhere != "" || defaultWhere != "" )
			{
				ret+= " WHERE ";
			}
			if( defaultWhere != "" ) 
			{
				ret+= " "+ defaultWhere;
			}
			if( filterWhere != "" )
			{
				ret+= (defaultWhere != "") ? " AND " : "";
				ret+= filterWhere;
					
			}
//			ret+= orderByString();
			return ret + " ";
			
		}
		#endregion SelectIDListString()

		#region SelectFieldListString
		private string SelectFieldListString( string fieldName, bool distinct  )
		{
			string ret= "SELECT ";
			if( distinct )	{
				ret= "SELECT DISTINCT ";
			}
			ret+= fieldName+" ";
			ret+= " FROM "+ this.DbTableName;

			string filterWhere		= FilterWhereString();
			string defaultWhere		= defaultWhereString();
			if( filterWhere != "" || defaultWhere != "" )
			{
				ret+= " WHERE ";
			}
			if( defaultWhere != "" ) 
			{
				ret+= " "+ defaultWhere;
			}
			if( filterWhere != "" )
			{
				ret+= (defaultWhere != "") ? " AND " : "";
				ret+= filterWhere;
					
			}
			//			ret+= orderByString();
			return ret + " ";
			
		}
		#endregion SelectIDListString()



			#region SelectString()
				public string SelectString()
				{
					string ret= "SELECT ";
					if (_top > 0)
					{
						ret += " TOP " + _top + " ";
					}
					ret+= selectList();

					ret+= " FROM "+ this.DbTableName;

					string filterWhere		= FilterWhereString();
					string defaultWhere		= defaultWhereString();
					if( filterWhere != "" || defaultWhere != "" ){
						ret+= " WHERE ";
					}
					if( defaultWhere != "" ) {
						ret+= " "+ defaultWhere;
					}
					if( filterWhere != "" ){
						ret+= (defaultWhere != "") ? " AND " : "";
						ret+= filterWhere;
					
					}
					ret+= orderByString();
					return ret + " ";
			
				}
			#endregion SelectString()

			#region InsertString()
			private string InsertString(){
				// Lista de campos
				string fieldLst = "";
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if( (! _dst.Dat.Column[i].IsPK && 
						 ! _dst.Dat.Column[i].IsReadOnly &&
						 ! _dst.Dat.Column[i].IsAutoIncrement 
						) ||
						( _dst.Dat.Column[i].IsPK && ! _AutogeneratedID  )
                      ) {
						fieldLst+= _fMap[i].DbColName + ",";
					}
				}

				fieldLst = fieldLst.Substring( 0, fieldLst.Length - 1 ) + " ";	
				// Lista de parametros
				string parLst = "";
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if( (! _dst.Dat.Column[i].IsPK &&
						 ! _dst.Dat.Column[i].IsReadOnly  &&
						 ! _dst.Dat.Column[i].IsAutoIncrement
						) ||
						( _dst.Dat.Column[i].IsPK && ! _AutogeneratedID  ) ) 
					{
						parLst+= "@" + _fMap[i].DtColName.Trim() + ",";	
					}
				}
				parLst = parLst.Substring( 0, parLst.Length - 1 ) + " ";	

				string ret = "INSERT INTO " + this.DbTableName;
				ret+= " ( " + fieldLst + " ) VALUES ( " + parLst + " )";
				return ret + " ";
			}
			#endregion InsertString()


			#region DeleteString()

			private string DeleteString(){
				return "DELETE FROM " + DbTableName;
			
			}

			#endregion DeleteString()


			#region UpdateString()
			private string UpdateString()
			{
				#region Parametros del "SET"
				string fieldLst = "";
				string par;
				int cont = 0;
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					bool hayCambio =  ( ObjConvert.Compare( _dst.Dat[i], _dst.Old[i] ) != 0 );
					if( !(	_dst.Dat.Column[i].IsPK ||
							_dst.Dat.Column[i].IsReadOnly || 
							_dst.Dat.Column[i].IsAutoIncrement ) && hayCambio )
					{
						par = "@" + _fMap[i].DtColName.Trim();
						fieldLst+= _fMap[i].DbColName + " = " + par + ",";
						cont++;
					}
				}
				if ( cont == 0 ){
					return "";
				}
				fieldLst = fieldLst.Substring( 0, fieldLst.Length - 1 ) + " ";	
				#endregion Parametros del "SET"

				string ret = "UPDATE " + DbTableName + " SET " + fieldLst;
			
				return ret;
			}
			#endregion UpdateString()

			#region ID_WhereString
			private string ID_WhereString()   
			{
				string fieldLst = "";
				string par;

				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if(_dst.Dat.Column[i].IsPK ) 
					{
						par = "@old_" + _fMap[i].DtColName.Trim();
						fieldLst+= _fMap[i].DbColName + " = " + par + " and ";
					}
				}
				fieldLst = fieldLst.Substring( 0, fieldLst.Length - 5 ) + " ";	// elimina el ultimo AND

				return fieldLst;
			}

			#endregion ID_WhereString

			#region UpdateWhereString()
			private string UpdateWhereString()   // Tambien se usa con el delete
			{
				string fieldLst = "";
				string par;

				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if(_dst.Dat.Column[i].IsPK || ( _ConcurrenceOn && _fMap[i].CheckConcurrence ) ) 
					{
//						if( _dst.Dat.Column[i].ColType != "Byte[]" && _fMap[i].SqlDbType != SqlDbType.NText && _fMap[i].SqlDbType != SqlDbType.Image )
						if( _dst.Dat.Column[i].ColType != "Byte[]" && _fMap[i].SqlDbType != SqlDbType.NText && _fMap[i].SqlDbType != SqlDbType.Image )
							{
							if(ObjConvert.IsNull( _dst.Old[i] )  )
							{
								fieldLst+= _fMap[i].DbColName + " IS NULL "  + " and ";
							}
							else
							{
								par = "@old_" + _fMap[i].DtColName.Trim();

								fieldLst+= _fMap[i].DbColName + " LIKE " + par + " and "; // nca 4/01/2005
//								fieldLst+= _fMap[i].DbColName + " = " + par + " and ";
							}
						}
					}
				}
				fieldLst = fieldLst.Substring( 0, fieldLst.Length - 5 ) + " ";	// elimina el ultimo AND


				return fieldLst;
			}
			#endregion UpdateWhereString()


			#region InsertedRowIDString()
			private string InsertedRowIDString()
			{
				string pkName="";
				// Obtener nombre del campo PK			
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) { 
					if( this._dst.Dat.Column[i].IsPK ){
						pkName = _fMap[i].DbColName;
						break;
					}	
				}
		
				// Lista de parametros
				string parLst="";
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
				{ 
					if(  ! _dst.Dat.Column[i].IsPK && 
						 ! _dst.Dat.Column[i].IsReadOnly  &&
						 ! _dst.Dat.Column[i].IsAutoIncrement ) 
					  {
						if(ObjConvert.IsNull( _dst.Dat[i] )  )
						{
							parLst+= _fMap[i].DbColName + " IS NULL "  + " and ";
						}
						else
						{
							if( _dst.Dat.Column[i].ColType != "Byte[]" ){
								parLst+= " " + _fMap[i].DbColName+ " LIKE " + "@"+_fMap[i].DtColName.Trim() + " and ";
							}
						}
					}	
				}
				parLst = parLst.Substring( 0, parLst.Length - 5 );
				
				return "SELECT " + pkName + " FROM " + this.DbTableName + " where " + parLst + " ";

			}
			#endregion InsertedRowIDString()

			#region selectList()
			private string selectList()
			{
				string ret = "";
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) { 
					ret+= _fMap[i].SqlColName +" "+ _fMap[i].DtColName + ",";	
				}
				return ret.Substring( 0, ret.Length - 1 ) + " ";	
			}
			#endregion selectList()

			#region getSqlType()
			private SqlDbType getSqlType( string tipo )
			{
				SqlDbType ret;
				switch( tipo )
				{
					case "String"	: ret = SqlDbType.NVarChar			; break;
					case "Boolean"	: ret = SqlDbType.Bit				; break;
					case "Byte"		: ret = SqlDbType.TinyInt			; break	;
					case "Byte[]"	: ret = SqlDbType.Binary			; break	;
					case "Binary"	: ret = SqlDbType.Binary			; break	;
					case "Guid"		: ret = SqlDbType.UniqueIdentifier	; break;
					case "DateTime"	: ret = SqlDbType.DateTime			; break;
					case "Decimal"	: ret = SqlDbType.Decimal			; break;
					case "Double"	: ret = SqlDbType.Float				; break;
					case "Single"	: ret = SqlDbType.Real				; break;
					case "Int16"	: ret = SqlDbType.SmallInt			; break;
					case "Short"	: ret = SqlDbType.SmallInt			; break;
					case "Int32"	: ret = SqlDbType.Int				; break;
					case "Int64"	: ret = SqlDbType.BigInt			; break;
					case "Long"		: ret = SqlDbType.BigInt			; break;
					default : throw new Exception (this.GetType().Name + " Conversion no prevista" );				
				}
				return ret;
			}

			#endregion getSqlType()

			#region indexOf()
			private int indexOf( string campo )
			{
				int idx = -1;
				for( int i = 0 ; i < _dst.Dat.numCols; i++ ) {
					if( _dst.Dat.Column[i].ColName.ToUpper() == campo.ToUpper() ) {
						idx = i;
						break;
					}
				}
//				if ( idx == -1 ) throw new Exception( this.GetType().Name + ": El campo "+ campo + " no existe");
				return idx;
			}

			#endregion indexOf()


		#endregion Metodos privados

		#region Manejo de Transacciones

		public void BeginTransaction() { this._db.IniciarTransaccion(); }
		
		public void Commit() { this._db.Commit(); }

		public void RollBack() { this._db.RollBack(); }

		#endregion Manejo de Transacciones


	}

	#endregion AdapterBase class



	#region AdapterBase for Views
	public class ViewAdapter 
	{

		#region Datos Miembro
		
		protected Berke.Libs.Base.DSHelpers.DSTab _dst;
		protected AccesoDB _db;
		protected string _dbTableName;

		protected FMapper[] _fMap;
		protected System.Collections.ArrayList _paramLst;

		protected string	_defaultWhere;

		protected bool _ConcurrenceOn;

		protected 	SqlDataReader _dr = null;

		protected bool _Distinct = false;
		protected int _top = -1;

		#endregion Datos Miembro
		
		#region Constructores y afines
		
		#region Constructor por defecto
		public ViewAdapter( ) 
		{
			_dst = null;
			_db  = null;
			_dbTableName = null;
			_defaultWhere = "";

		}
		#endregion Constructor por defecto

		#region Constuctor basado en TableBase

		public ViewAdapter( ViewBase dst,  AccesoDB db ) 
		{
			Bind( dst.DST, db, dst.DST.Table.TableName );
		}

	
		public ViewAdapter( ViewBase dst,  AccesoDB db, string dbTableName  ) 
		{
			Bind( dst.DST, db, dbTableName );
		}

		public void Bind( ViewBase dst,  AccesoDB db ) 
		{
			Bind( dst.DST, db, dst.DST.Table.TableName );
		}
			
		public void Bind( ViewBase dst,  AccesoDB db, string dbTableName ) 
		{
			Bind( dst.DST, db, dbTableName );
		}
			
	
		#endregion Constuctor basado en TableBase

		#region Constuctor basado en DSTab

		public ViewAdapter( DSTab dst,  AccesoDB db ) 
		{
			Bind( dst, db, dst.Table.TableName );
		}

		public ViewAdapter( DSTab dst,  AccesoDB db, string dbTableName  ) 
		{
			Bind( dst, db, dbTableName );
		}
			

		#endregion Constuctor basado en DSTab

		#region Bind 
		public void Bind( DSTab dst,  AccesoDB db, string dbTableName ) 
		{
			_dst = dst;
			_db  = db;
			_defaultWhere = "";
			_ConcurrenceOn = false;
			_dbTableName = dbTableName;
			_fMap = new FMapper[ _dst.Dat.numCols ];
			_paramLst = null;
			for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
			{
				_fMap[i] = new FMapper();
				_fMap[i].DtColIndex			= i;
				_fMap[i].DtColName			= _dst.Dat.Column[i].ColName;
				_fMap[i].DbColName			= _dst.Dat.Column[i].ColName;
				_fMap[i].SqlColName			= _dst.Dat.Column[i].ColName;
				_fMap[i].CheckConcurrence	= true;
				_fMap[i].Len				= 0;
				_fMap[i].SqlDbType			= getSqlType( _dst.Dat.Column[i].ColType );
			}
		}

		#endregion Bind 

		#region SetDefaultWhere

		public void SetDefaultWhere( string whereString )
		{
			this._defaultWhere = whereString;
		}

		#endregion SetDefaultWhere

		#endregion Constructores y afines

		#region Propiedades

		/// <summary>
		/// Establece y recupera el número máximo de filas a recuperar<see cref="AdapterBase"/>.
		/// </summary>
		/// <value>Número de filas a recuperar.</value>
		/// mbaez
		public int Top { get { return _top;} set { _top = value;}}	

		public bool Distinct { get { return _Distinct;} set { _Distinct = value;}}

		public string DbTableName { get { return _dbTableName;} set { _dbTableName = value;}}

		public FMapper[] FMap { get { return _fMap; } }
		public string CommandString	{ get {	return _db.CommandString;} }
		public AccesoDB Db { get { return _db; } }

		public string DefaultWhere { get{ return _defaultWhere; }}

		#endregion Propiedades

		#region Params


		#region ClearParams()
		public void ClearParams()
		{
			this._paramLst = null;
		}
		#endregion ClearParams()

		#region AddParam()
		public void AddParam( string nombreParam, object valor )
		{
			nombreParam = nombreParam.Trim();
			if( _defaultWhere.IndexOf( nombreParam ) == -1 )
			{
				throw new Exception("AdapterBase::AddParam(). Parametro [ " + nombreParam + " ] no corresponde" );
			}
			string nombreCampo;
			if( nombreParam.Substring(0,1) == "@" )
			{
				nombreCampo = nombreParam.Substring( 1 );
			} 
			else 
			{
				nombreCampo = nombreParam;
				nombreParam = "@" + nombreParam;
			}
			if( this.indexOf( nombreCampo ) != -1 ){ throw new Exception( "El nombre de parametro no puede coincidir con un nombre de columna");}
			
			if( valor is ArrayList )
			{
				string nameLst = "";
				ArrayList lst = (ArrayList) valor;
				for( int j = 0; j < lst.Count; j++ )
				{
					string nombre = nombreParam + "_" + j.ToString().Trim();
					nameLst+= nombre + ", ";
					SqlDbType tipo = getSqlType( lst[j].GetType().Name );
					if( this._paramLst == null ) _paramLst = new System.Collections.ArrayList();
					_paramLst.Add( new Param(nombre, tipo, 0, lst[j] ));		
				}		
				nameLst = nameLst.Substring(0, nameLst.Length - 2);
				this._defaultWhere = _defaultWhere.Replace( nombreParam, nameLst);
			}
			else
			{
				SqlDbType tipo = getSqlType( valor.GetType().Name );
				if( this._paramLst == null ) _paramLst = new System.Collections.ArrayList();
				_paramLst.Add( new Param(nombreParam, tipo, 0, valor ));		
			}	 
		}
		#endregion AddParam()

		#region setDefaultWhereParams()
		private void setDefaultWhereParams()
		{
			if( _paramLst == null ) return;
			for( int i = 0 ; i < _paramLst.Count ; i++ )
			{

				Param par = (Param) _paramLst[i];
				_db.AddParam( par.Nombre, par.SqlType, par.Valor, par.Longitud );
			}
		}
		#endregion setDefaultWhereParams()
		
		#region setFilterWhereParams()
		private void setFilterWhereParams() 
		{
			for( int i= 0; i < _dst.Filter.numCols ; i++ ) 
			{
				Object filObj = _dst.Filter[i];

				if( ObjConvert.IsNull( filObj )  ){ continue; }

				if( !(filObj is DSFilter) )	
				{
					filObj = new DSFilter(_dst.Filter[i]); 
				}
				DSFilter fil  = (DSFilter) filObj;

				#region Single Value
				if( fil.Value != null ) 
				{
					string nombre = "@" + _fMap[i].DtColName;
					_db.AddParam( nombre, _fMap[i].SqlDbType, fil.Value, _fMap[i].Len );			
				}		
				#endregion Single Value

				#region Range 
				if( fil.MinValue != null )
				{
					string nombre1 = "@Min_" + _fMap[i].DtColName;
					string nombre2 = "@Max_" + _fMap[i].DtColName;
					_db.AddParam( nombre1, _fMap[i].SqlDbType, fil.MinValue, _fMap[i].Len );	
					_db.AddParam( nombre2, _fMap[i].SqlDbType, fil.MaxValue, _fMap[i].Len );	
				}
				#endregion Range 

				#region Lista 
				if( fil.List != null ) 
				{
					ArrayList lst = (ArrayList) fil.List;
					for( int j = 0; j < lst.Count; j++ )
					{
						string nombre = "@" + _fMap[i].DtColName + "_" + j.ToString().Trim();
						_db.AddParam( nombre, _fMap[i].SqlDbType, lst[j], _fMap[i].Len );	
					}
				}
				#endregion Lista 
			}


		}
		#endregion setFilterWhereParams()

		#endregion Params

		#region Count

		public int Count() 
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.CountString();
			return (int) _db.getValue();
		}
		#endregion Count

		#region ReadAll
		/// <summary>
		/// Realiza una consulta a la base de acuerdo a los
		/// fitros especificados en el DBTab.
		/// </summary>
		/// <exception cref="Berke.Excep.Biz.TooManyRowsException">Si el conjunto de 
		/// datos es superior un número máximo predefinido</exception>
		public void ReadAll() 
		{
			ReadAll( 1000 ); // Sin limite de filas recuperadas
		}

		public string ReadAll_CommandString() 
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();
		
	

			_db.Sql = this.SelectString();

			return _db.CommandString;
		}

		/// <summary>
		/// Realiza una consulta a la base de acuerdo a los
		/// fitros especificados en el DBTab.
		/// </summary>
		/// <exception cref="Berke.Excep.Biz.TooManyRowsException">Si el conjunto de 
		/// datos es superior al parámetro especificado</exception>
		public void ReadAll( int numberOfRowsLimit ) 
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.SelectString();

//			string cadena = _db.CommandString;

			DataTable tab = _dst.Table.Clone();
			_dst.Table = tab;
			SqlDataReader dr = _db.getDataReader();
			
			int rowCounter = 0;
			while( dr.Read() )
			{  
				rowCounter++;
				if( numberOfRowsLimit <= 0 || (numberOfRowsLimit > 0 && rowCounter <= numberOfRowsLimit)  )
				{
					_dst.NewRow(); 
					for( int i= 0; i < _dst.Dat.numCols; i++)
					{ 
						_dst.Dat[i] = dr[i]; 
					} 
					_dst.PostNewRow(); 
				}
			} 

			dr.Close();
			_dst.AcceptAllChanges(); 
			_dst.GoTop();
			if( numberOfRowsLimit > 0 && rowCounter > numberOfRowsLimit )
			{
				throw new Berke.Excep.Biz.TooManyRowsException( _dst.Table.TableName, numberOfRowsLimit, rowCounter );
			}
		}

		#endregion ReadAll

		#region GetListOfField
		public ArrayList GetListOfField( Berke.DG.Base.Field field ) 
		{
			return GetListOfField( field, false );
		}

		public ArrayList GetListOfField( Berke.DG.Base.Field field, bool distinct )
		{
			_db.ClearParams();
	
			_db.Sql = SelectFieldListString( field.Name, distinct );
//			#region Obtener Nombre de Campo
//			string viewField = "";
//			for( int k = 0; k < _dst.Filter.numCols; k++ )
//			{
//				if( field.Name == _fMap[k].DbColName )
//				{
//					viewField	= _fMap[k].SqlColName;
//					break;
//				}
//			}
//			#endregion Obtener Nombre de Campo
//			_db.Sql += " order by " + viewField;
			_db.Sql += orderByString();
			setDefaultWhereParams();
			setFilterWhereParams();

			ArrayList aLst = new ArrayList();
			SqlDataReader dr = _db.getDataReader();
			while( dr.Read() )
			{  
				aLst.Add( dr[0] );  
			} 
			dr.Close();
			return aLst;
		}
		#endregion GetListOfField

		#region DataReader_Init
		public void DataReader_Init()
		{
			_db.ClearParams();
			setDefaultWhereParams();
			setFilterWhereParams();

			_db.Sql = this.SelectString();

			_dr = _db.getDataReaderAlone(); // _db.getDataReader();
			

			_dst.NewRow(); 

			_dst.PostNewRow(); 


		}
		#endregion DataReader_Init

		#region DataReader_Read
		public bool DataReader_Read()
		{
			bool ret;

			ret = _dr.Read();

			if( ret )
			{
				_dst.Edit(); 
			
				for( int i= 0; i < _dst.Dat.numCols; i++)
				{ 
					_dst.Dat[i] = _dr[i]; 
				} 
				_dst.PostEdit(); 
			}
			return ret;
		}
		#endregion DataReader_Read

		#region DataReader_Close
		public void DataReader_Close()
		{
			_dr.Close();
		}
		#endregion DataReader_Close



		#region Metodos privados
 
			
		#region FilterWhereString()

		private string FilterWhereString()
		{
			string ret = "";			
			for( int i= 0; i < _dst.Filter.numCols ; i++ ) 
			{
				Object filObj = _dst.Filter[i];

				if( ObjConvert.IsNull( filObj )  ){ continue; }

				if( !(filObj is DSFilter) )	
				{
					filObj = new DSFilter(_dst.Filter[i]); 
				}
				DSFilter fil  = (DSFilter) filObj;

				#region OperFieldName  
				if( fil.OperFieldName != null )
				{
					string where = defaultWhereString();
					string field = "";
					if( where.Trim() != "")
					{
						where+= " AND ";
					}

					for( int k = 0; k < _dst.Filter.numCols; k++ ){
						if( fil.OperFieldName == _fMap[k].DbColName )
						{
							field	= _fMap[k].SqlColName;
							break;
						}
					}
					where += this._fMap[i].SqlColName + " " + fil.Oper + " " + field;
					this.SetDefaultWhere( where );
						
				}
				#endregion OperFieldName

				#region Oper
//				if( fil.Oper != null )
//				{
//					ret+= this._fMap[i].SqlColName +" "+ fil.Oper +" @" + _fMap[i].DtColName;
//				}
//					
				#endregion Oper

				#region Single Value
				if( fil.Value != null )
				{
					ret+= (ret == "") ? "" : " AND ";
					if( _fMap[i].SqlDbType == SqlDbType.Char ||
						_fMap[i].SqlDbType == SqlDbType.NChar ||
						_fMap[i].SqlDbType == SqlDbType.NText ||
						_fMap[i].SqlDbType == SqlDbType.NVarChar ||
						_fMap[i].SqlDbType == SqlDbType.Text ||
						_fMap[i].SqlDbType == SqlDbType.VarChar )
					{

						ret+= this._fMap[i].SqlColName + " like @" + _fMap[i].DtColName; //nca 22/12/05
					}
					else
					{
						ret+= this._fMap[i].SqlColName + " = @" + _fMap[i].DtColName;
					}
//					ret+= this._fMap[i].SqlColName + " like @" + _fMap[i].DtColName;
			
				}		
				#endregion Single Value
	
				#region Range 
				if( fil.MinValue != null )
				{
					ret+= (ret == "") ? "" : " AND ";

					ret+= this._fMap[i].SqlColName + " between "+"@Min_" + _fMap[i].DtColName +
						" and " +"@Max_" + _fMap[i].DtColName;

				}
				#endregion Range 

				#region Lista 
				if( fil.List != null ) 
				{
					ret+= (ret == "") ? "" : " AND ";

					ArrayList lst = (ArrayList) fil.List;
					if( lst.Count > 0 )
					{
	
						ret+= this._fMap[i].SqlColName + " IN ( ";
						
						string bf = "";
				
						for( int j = 0; j < lst.Count; j++ )
						{
							bf += ( bf == "") ? "" : ", ";
						
							bf+= "@" + _fMap[i].DtColName + "_" + j.ToString().Trim();
						}

						ret+= bf + " ) ";
					}
					else{
						ret+= " 1 > 2 ";  // Obligar a que no traiga nada
					}
				}
				#endregion Lista 
			

			}
			
			return ret;
		}

		#endregion FilterWhereString()

		#region defaultWhereString()

		private string defaultWhereString()
		{
			return _defaultWhere == null ? "" : _defaultWhere;
		}

		#endregion defaultWhereString()


		#region orderByString

		private string orderByString()
		{
			string lista = "";
			for( int i= 0; i < _dst.OrderList.Count; i++ )
			{
				Par par = (Par) _dst.OrderList[i];
				lista+= this._fMap[  par.Idx  ].DbColName + ((par.Sentido == DSOrden.Ascendente ) ? "" : " DESC " )  + ", ";

//				lista+= this._fMap[  par.Idx  ].SqlColName + ((par.Sentido == DSOrden.Ascendente ) ? "" : " DESC " )  + ", ";

			}
			if( lista.Length > 0 )
			{
				lista= lista.Substring(0, lista.Length - 2);
				lista = " ORDER BY " + lista;
			}
			return lista;
		}

		#endregion orderByString


		#region CountString()
		public string CountString()
		{
			string ret= "SELECT ";
			ret+= " Count(*) As CantFilas "  ;

			ret+= " FROM "+ this.DbTableName;

			string filterWhere		= FilterWhereString();
			string defaultWhere		= defaultWhereString();
			if( filterWhere != "" || defaultWhere != "" )
			{
				ret+= " WHERE ";
			}
			if( defaultWhere != "" ) 
			{
				ret+= " "+ defaultWhere;
			}
			if( filterWhere != "" )
			{
				ret+= (defaultWhere != "") ? " AND " : "";
				ret+= filterWhere;
					
			}
			return ret + " ";
			
		}
		#endregion CountString()

		#region SelectFieldListString
		private string SelectFieldListString( string fieldName, bool distinct  )
		{
			string ret= "SELECT ";
			if( distinct )	
			{
				ret= "SELECT DISTINCT ";
			}
			if(_top > 0)
			{
				ret+= " TOP " + _top + " ";
			}

			#region Obtener Nombre de Campo
			string viewField = "";
			for( int k = 0; k < _dst.Filter.numCols; k++ )
			{
				if( fieldName == _fMap[k].DbColName )
				{
					viewField	= _fMap[k].SqlColName;
					break;
				}
			}
			#endregion Obtener Nombre de Campo

			ret+= viewField +" ";
			ret+= " FROM "+ this.DbTableName;

			string filterWhere		= FilterWhereString();
			string defaultWhere		= defaultWhereString();
			if( filterWhere != "" || defaultWhere != "" )
			{
				ret+= " WHERE ";
			}
			if( defaultWhere != "" ) 
			{
				ret+= " "+ defaultWhere;
			}
			if( filterWhere != "" )
			{
				ret+= (defaultWhere != "") ? " AND " : "";
				ret+= filterWhere;
					
			}
			//			ret+= orderByString();
			return ret + " ";
			
		}
		#endregion SelectIDListString()


		#region SelectString()
		public string SelectString()
		{
			string ret= "SELECT ";
			if( _Distinct ) ret= "SELECT Distinct ";
			if(_top > 0)
			{
				ret+= " TOP " + _top + " ";
			}

			ret+= selectList();

			ret+= " FROM "+ this.DbTableName;

			string filterWhere		= FilterWhereString();
			string defaultWhere		= defaultWhereString();
			if( filterWhere != "" || defaultWhere != "" )
			{
				ret+= " WHERE ";
			}
			if( defaultWhere != "" ) 
			{
				ret+= " "+ defaultWhere;
			}
			if( filterWhere != "" )
			{
				ret+= (defaultWhere != "") ? " AND " : "";
				ret+= filterWhere;
					
			}
			ret += orderByString();

			return ret + " ";
			
		}
		#endregion SelectString()

		#region selectList()
		private string selectList()
		{
			string ret = "";
			for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
			{ 
				ret+= _fMap[i].SqlColName +" "+ _fMap[i].DtColName + ",";	
			}
			return ret.Substring( 0, ret.Length - 1 ) + " ";	
		}
		#endregion selectList()

		#region getSqlType()
		private SqlDbType getSqlType( string tipo )
		{
			SqlDbType ret;
			switch( tipo )
			{
				case "String"	: ret = SqlDbType.NVarChar			; break;
				case "Boolean"	: ret = SqlDbType.Bit				; break;
				case "Byte"		: ret = SqlDbType.TinyInt			; break	;
				case "Byte[]"	: ret = SqlDbType.Binary			; break	;
				case "Binary"	: ret = SqlDbType.Binary			; break	;
				case "Guid"		: ret = SqlDbType.UniqueIdentifier	; break;
				case "DateTime"	: ret = SqlDbType.DateTime			; break;
				case "Decimal"	: ret = SqlDbType.Decimal			; break;
				case "Double"	: ret = SqlDbType.Float				; break;
				case "Single"	: ret = SqlDbType.Real				; break;
				case "Int16"	: ret = SqlDbType.SmallInt			; break;
				case "Short"	: ret = SqlDbType.SmallInt			; break;
				case "Int32"	: ret = SqlDbType.Int				; break;
				case "Int64"	: ret = SqlDbType.BigInt			; break;
				case "Long"		: ret = SqlDbType.BigInt			; break;
				default : throw new Exception (this.GetType().Name + " Conversion no prevista" );				
			}
			return ret;
		}

		#endregion getSqlType()

		#region indexOf()
		private int indexOf( string campo )
		{
			int idx = -1;
			for( int i = 0 ; i < _dst.Dat.numCols; i++ ) 
			{
				if( _dst.Dat.Column[i].ColName.ToUpper() == campo.ToUpper() ) 
				{
					idx = i;
					break;
				}
			}
			//				if ( idx == -1 ) throw new Exception( this.GetType().Name + ": El campo "+ campo + " no existe");
			return idx;
		}

		#endregion indexOf()


		#endregion Metodos privados

	}

	#endregion AdapterBase for Views


}// Berke.DG.Adapters

#endregion Adapter

