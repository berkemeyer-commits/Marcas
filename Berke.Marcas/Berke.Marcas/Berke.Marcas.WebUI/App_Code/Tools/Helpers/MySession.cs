using System;
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Helpers {
	using System.Web;

	/// <summary>
	/// SessionKeys
	/// </summary>
	public class MySession {

		enum keys {

			User, ID, marcaDS, logo, marcaVariosDS, ordenTrabajoDS, ordenTrabajoUPDS, oTUPDS,
			Activo, MarcaID, ClaseID, MarcaRegRenID, Estado, PorderMarcaID, ClaseIdiomaID, sitListDS,
			Varios, ordenTrabajoTVDS, marcaRegRenListDS, marcaRegRenDS, Situacion, SituacionID, 
			DocumentoID, Count, marcaActualizarDS, boletinDetalleDS, boletinListDS, expedienteMensajeDS,
			claseDS, TramiteID, TramiteSitID, Variable1, Variable2, poderListDS, poderDS, poderSitListDS,
			expedientePendienteDS, ClienteID, ClienteDS, PropietarioDS, FuncionarioID, UserName, MenuHtml
			
		}

		public static string MenuHtlm 
		{
			get 
			{
				return (string) Read( keys.MenuHtml );
			}
			set 
			{
				Write( keys.MenuHtml, value );
			}
		}

		#region UserName
		public static string UserName
		{
			get
			{
				if( HttpContext.Current.Session.Count == 0 )
				{
					return "";
				}
				else
				{
					return (string) HttpContext.Current.Session[ keys.UserName.ToString() ];

				}
			}set
			 {
				 HttpContext.Current.Session[ keys.UserName.ToString() ] = value ;
			 }
		}

		#endregion UserName

		#region FuncionarioID
		public static int FuncionarioID
		{
			get
			{
				return (int) HttpContext.Current.Session[ keys.FuncionarioID.ToString() ];
			}
			set
			 {
				 HttpContext.Current.Session[ keys.FuncionarioID.ToString() ] = value ;
			 }
		}

		#endregion FuncionarioID

		#region Properties

//		public static Entidades.BizDocuments.Entidades.PropietarioDS PropietarioDS
//		{
//			get
//			{
//				return (Entidades.BizDocuments.Entidades.PropietarioDS) Read( keys.PropietarioDS);
//			}set
//			 {
//				 Write( keys.PropietarioDS, value );
//			 }
//		}

//		public static Entidades.BizDocuments.Entidades.ClienteDS ClienteDS
//		{
//			get
//			{
//				return (Entidades.BizDocuments.Entidades.ClienteDS) Read( keys.ClienteDS);
//			}set
//			 {
//				 Write( keys.ClienteDS, value );
//			 }
//		}

		public static int ClienteID 
		{
			get 
			{
				return (int) Read( keys.ClienteID );
			} set 
			  {
				  Write( keys.ClienteID, value );
			  }
		}

		/*
		public static Marcas.BizDocuments.Marca.ExpedientePendienteDS expedientePendienteDS
		{
			get
			{
				return (Marcas.BizDocuments.Marca.ExpedientePendienteDS) Read( keys.expedientePendienteDS);
			}set
			 {
				 Write( keys.expedientePendienteDS, value );
			 }
		}
		*/

		public static Marcas.BizDocuments.Marca.ClaseDS claseDS
		{
			get
			{
				return (Marcas.BizDocuments.Marca.ClaseDS) Read( keys.claseDS);
			}set
			 {
				 Write( keys.claseDS, value );
			 }
		}

		public static Marcas.BizDocuments.Marca.ExpedienteMensajeDS expedienteMensajeDS
		{
			get
			{
				return (Marcas.BizDocuments.Marca.ExpedienteMensajeDS) Read( keys.expedienteMensajeDS );
			}set
			 {
				 Write( keys.expedienteMensajeDS, value );
			 }
		}

		public static Marcas.BizDocuments.Marca.BoletinListDS boletinListDS
		{
			get
			{
				return (Marcas.BizDocuments.Marca.BoletinListDS) Read( keys.boletinListDS );
			}set
			 {
				 Write( keys.boletinListDS, value );
			 }
		}

		public static Marcas.BizDocuments.Marca.BoletinDetalleDS boletinDetalleDS
		{
			get
			{
				return (Marcas.BizDocuments.Marca.BoletinDetalleDS) Read( keys.boletinDetalleDS );
			}set
			 {
				 Write( keys.boletinDetalleDS, value );
			 }
		}

		public static Marcas.BizDocuments.Marca.MarcaActualizarDS marcaActualizarDS
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.MarcaActualizarDS) Read( keys.marcaActualizarDS );
			} set 
			  {
				  Write( keys.marcaActualizarDS, value );
			  }
		}

		public static int Count
		{
			get
			{
				return (int) Read( keys.Count );
			}set
			 {
				 Write( keys.Count, value );
			 }
		}

		public static int DocumentoID 
		{
			get 
			{
				return (int) Read( keys.DocumentoID );
			} set 
			  {
				  Write( keys.DocumentoID, value );
			  }
		}

		public static int SituacionID 
		{
			get 
			{
				return (int) Read( keys.SituacionID );
			} set 
			  {
				  Write( keys.SituacionID, value );
			  }
		}
		public static int TramiteID 
		{
			get 
			{
				return (int) Read( keys.TramiteID );
			} set 
			  {
				  Write( keys.TramiteID, value );
			  }
		}

		public static int TramiteSitID 
		{
			get 
			{
				return (int) Read( keys.TramiteSitID );
			} set 
			  {
				  Write( keys.TramiteSitID, value );
			  }
		}

		public static string Situacion 
		{
			get 
			{
				return (string) Read( keys.Situacion );
			} set 
			  {
				  Write( keys.Situacion, value );
			  }
		}

		public static string Variable1 
		{
			get 
			{
				return (string) Read( keys.Variable1 );
			} set 
			  {
				  Write( keys.Variable1, value );
			  }
		}

		public static string Variable2 
		{
			get 
			{
				return (string) Read( keys.Variable2 );
			} set 
			  {
				  Write( keys.Variable2, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.OrdenTrabajoTVDS ordenTrabajoTVDS
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.OrdenTrabajoTVDS) Read( keys.ordenTrabajoTVDS );
			} set 
			  {
				  Write( keys.ordenTrabajoTVDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.MarcaRegRenDS marcaRegRenDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.MarcaRegRenDS) Read( keys.marcaRegRenDS );
			} set 
			  {
				  Write( keys.marcaRegRenDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.ExpedienteListDS marcaRegRenListDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.ExpedienteListDS) Read( keys.marcaRegRenListDS );
			} set 
			  {
				  Write( keys.marcaRegRenListDS, value );
			  }
		}

		public static bool Varios
		{
			get
			{
				return (bool) Read( keys.Varios );
			}set
			 {
				 Write( keys.Varios, value );
			 }
		}

		public static Marcas.BizDocuments.Marca.ExpedienteSitListDS sitListDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.ExpedienteSitListDS) Read( keys.sitListDS );
			} set 
			  {
				  Write( keys.sitListDS, value );
			  }
		}

		public static int ClaseIdiomaID 
		{
			get 
			{
				return (int) Read( keys.ClaseIdiomaID);
			} set 
			  {
				  Write( keys.ClaseIdiomaID, value );
			  }
		}

		public static int PoderMarcaID 
		{
			get 
			{
				return (int) Read( keys.PorderMarcaID);
			} set 
			  {
				  Write( keys.PorderMarcaID, value );
			  }
		}

		public static int MarcaRegRenID 
		{
			get 
			{
//				object obj = Read( keys.MarcaRegRenID);
//
//				if(obj==null)
//				{
//					return -1;
//				}
//				else
//				{
//					return (int)obj;
//				}
				return ObjConvert.AsInt(Read( keys.MarcaRegRenID));
				
			} set 
			  {
				  Write( keys.MarcaRegRenID, value );
			  }
		}

		public static string Estado 
		{
			get 
			{
				return (string) Read( keys.Estado );
			} set 
			  {
				  Write( keys.Estado, value );
			  }
		}

		public static int ClaseID 
		{
			get 
			{
				return (int) Read( keys.ClaseID );
			} set 
			  {
				  Write( keys.ClaseID, value );
			  }
		}

		public static int MarcaID 
		{
			get 
			{
				return (int) Read( keys.MarcaID );
			} set 
			  {
				  Write( keys.MarcaID, value );
			  }
		}

		public static string logo 
		{
			get 
			{
				return (string) Read( keys.logo );
			} set 
			  {
				  Write( keys.logo, value );
			  }
		}

		public static int Activo 
		{
			get 
			{
				return (int) Read( keys.Activo );
			} set 
			  {
				  Write( keys.Activo, value );
			  }
		}

		public static int ID {
			get {
				return (int) Read( keys.ID );
			} set {
				  Write( keys.ID, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.MarcaRegRenDS marcaDS {
			get 
			{
				return (Marcas.BizDocuments.Marca.MarcaRegRenDS) Read( keys.marcaDS );
			} set 
			  {
				  Write( keys.marcaDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.MarcaVariosDS marcaVariosDS{
			get
			{
				return (Marcas.BizDocuments.Marca.MarcaVariosDS) Read (keys.marcaVariosDS);
			}set
			 {
				Write (keys.marcaVariosDS,value);
			 }

		}

		public static Marcas.BizDocuments.Marca.OrdenTrabajoDS ordenTrabajoDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.OrdenTrabajoDS) Read( keys.ordenTrabajoDS );
			} set 
			  {
				  Write( keys.ordenTrabajoDS, value );
			  }
		}

//		public static Marcas.BizDocuments.Marca.OrdenTrabajoUPDS ordenTrabajoUPDS 
//		{
//			get 
//			{
//				return (Marcas.BizDocuments.Marca.OrdenTrabajoUPDS) Read( keys.ordenTrabajoUPDS );
//			} set 
//			  {
//				  Write( keys.ordenTrabajoUPDS, value );
//			  }
//		}

		public static Berke.Libs.Base.Helpers.TableGateway oTUPDS 
		{
			get 
			{
				return (Berke.Libs.Base.Helpers.TableGateway) Read( keys.oTUPDS );
			} set 
			  {
				  Write( keys.oTUPDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.PoderListDS poderListDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.PoderListDS) Read( keys.poderListDS );
			} set 
			  {
				  Write( keys.poderListDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.PoderDS poderDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.PoderDS) Read( keys.poderDS );
			} set 
			  {
				  Write( keys.poderDS, value );
			  }
		}

		public static Marcas.BizDocuments.Marca.PoderSitListDS poderSitListDS 
		{
			get 
			{
				return (Marcas.BizDocuments.Marca.PoderSitListDS) Read( keys.poderSitListDS );
			} set 
			  {
				  Write( keys.poderSitListDS, value );
			  }
		}

		#endregion
	
		#region Clean
		public static void Clean(){
			//Clean( keys.AccountStatus );
			Clean( keys.User );
			Clean( keys.ID );
			Clean( keys.marcaDS );
			Clean( keys.marcaVariosDS );
			Clean( keys.ordenTrabajoDS );
			Clean( keys.ordenTrabajoUPDS );
			Clean( keys.oTUPDS );
			Clean( keys.Activo );
			Clean( keys.logo );
			Clean( keys.Estado );
			Clean( keys.MarcaRegRenID );
			Clean( keys.PorderMarcaID );
			Clean( keys.ClaseIdiomaID );
			Clean( keys.Varios );
			Clean( keys.marcaRegRenListDS );
			Clean( keys.ordenTrabajoTVDS );
			Clean( keys.Situacion );
			Clean( keys.SituacionID );
			Clean( keys.DocumentoID );
			Clean( keys.Count );
			Clean( keys.marcaActualizarDS );
			Clean( keys.boletinDetalleDS );
			Clean( keys.boletinListDS );
			Clean( keys.expedienteMensajeDS );
			Clean( keys.claseDS );
			Clean( keys.TramiteID );
			Clean( keys.TramiteSitID );
			Clean( keys.Variable1 );
			Clean( keys.Variable2 );
			Clean( keys.poderListDS );
			Clean( keys.poderDS );
			Clean( keys.poderSitListDS );
		}
		#endregion

		#region Utils
		private static object Read( keys key )  {
			if ( HttpContext.Current.Session.Count == 0 )
			{
				HttpContext.Current.Response.Redirect( Const.PAGE_LOGIN );
			}
			return HttpContext.Current.Session[ key.ToString() ];
		}

		private static void Write( keys key, object value )  {
			HttpContext.Current.Session[ key.ToString() ] = value;
		}

		private static void Clean( keys key )  {
			HttpContext.Current.Session.Remove( key.ToString() );
		}
		#endregion
	}
}
