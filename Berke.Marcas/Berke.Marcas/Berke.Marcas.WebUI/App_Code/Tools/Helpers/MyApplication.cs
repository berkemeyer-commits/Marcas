using System;



namespace Berke.Marcas.WebUI.Helpers {
	using System.Web;
	using Libs.Base.DSHelpers;
	using BizDocuments.Marca;
    

	/// <summary>
	/// SessionKeys
	/// </summary>
	public class  MyApplication {

		enum keys {
			Pertenencia, Clase, Tramite, CurrentDBName, CurrentServerName,HomeFolder, CurrentWebServerName
		
		}

		#region Properties

	
		
		public static string HomeFolder 
		{
			get 
			{
				return (string) Read( keys.HomeFolder );
			}
			set 
			{
				Write( keys.HomeFolder, value );
			}
		}

        public static string CurrentWebServerName
        {
            get
            {
                return (string)Read(keys.CurrentWebServerName);
            }
            set
            {
                Write(keys.CurrentWebServerName, value);
            }
        }

		public static string CurrentServerName 
		{
			get 
			{
				return (string) Read( keys.CurrentServerName );
			}
			set 
			{
				Write( keys.CurrentServerName, value );
			}
		}


		public static string CurrentDBName 
		{
			get 
			{
				return (string) Read( keys.CurrentDBName );
			}
			set 
			{
				Write( keys.CurrentDBName, value );
			}
		}


		public static SimpleEntryDS.SimpleEntryDataTable Pertenencia 
		{
			get {
				return (SimpleEntryDS.SimpleEntryDataTable) Read( keys.Pertenencia );
			}
			set {
					Write( keys.Pertenencia, value );
				}
		}

		public static ClaseDS Clase {
			get {
				return (ClaseDS) Read( keys.Clase );
			}
			set {
				Write( keys.Clase, value );
			}
		}

		public static SimpleEntryDS Tramite 
		{
			get 
			{
				return ( SimpleEntryDS ) Read( keys.Tramite );
			}
			set 
			{
				Write( keys.Tramite, value );
			}
		}
		#endregion
	
		#region Clean
		public static void Clean(){
			//Clean( keys.AccountStatus );
		}
		#endregion

		#region Utils
		private static object Read( keys key )  {
			return HttpContext.Current.Application[ key.ToString() ];
		}

		private static void Write( keys key, object value )  {
			HttpContext.Current.Application[ key.ToString() ] = value;
		}

		private static void Clean( keys key )  {
			HttpContext.Current.Application.Remove( key.ToString() );
		}
		#endregion
	}
}
