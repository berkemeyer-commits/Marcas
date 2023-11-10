using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Berke.Marcas.WebUI.Navigator {
	using Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	/// <summary>
	/// Navigator page
	/// </summary>
	public partial class Navigator : System.Web.UI.Page {
	
		protected void Page_Load(object sender, System.EventArgs e) {
			if(  Request.QueryString["page"] != null ){
				Label1.Text = Label1.Text + (string)  Request.QueryString["page"];
			}
			Navigate();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {    

		}
		#endregion

		private void Navigate(){
			MySession.Clean();
			string page =  UrlParam.GetParam("page");

			if( Request.QueryString["page"] != null ){
//				string page = Request.QueryString["page"];
				if( !IsAuthorized( page ) )
					Redirect( Const.PAGE_ERROR_HTM );
				switch( page ){

					case "5": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "6": Redirect( Const.PAGE_CAMBIOSITUACIONINGRESAR ); break;
					case "7": Redirect( Const.PAGE_CAMBIOSITUACIONMODIFICAR ); break;
					case "8": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "9": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "10": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page );break;
					case "11": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "12": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "13": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "14": Redirect( Const.PAGE_ORDENTRABAJORENOVING + "?page=" + page); break;
//					case "15": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page); break;
					case "15": Redirect( "~/Home/OtMarcaConsulta.aspx"); break;
					case "16": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page); break;
					case "17": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "18": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "19": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page ); break;
					case "20": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page); break;
					case "21": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page); break;
					case "22": Redirect( Const.PAGE_ORDENTRABAJOCONSULTAR + "?page=" + page); break;
					case "23": Redirect( Const.PAGE_ORDENTRABAJOTVCN + "?page=" + page ); break;
					case "24": Redirect( Const.PAGE_ORDENTRABAJOTVCN + "?page=" + page ); break;
					case "25": Redirect( Const.PAGE_ORDENTRABAJOTVDT + "?page=" + page ); break;
					case "26": Redirect( Const.PAGE_ORDENTRABAJOTVTR + "?page=" + page); break;
					case "27": Redirect( Const.PAGE_ORDENTRABAJOTVLC + "?page=" + page); break;
					case "28": Redirect( Const.PAGE_ORDENTRABAJOTVFS + "?page=" + page); break;
					case "29": Redirect( Const.PAGE_SITUACIONINGRESAR + "?page=" + page ); break;
					case "30": Redirect( Const.PAGE_SITUACIONINGRESAR + "?page=" + page ); break;
					case "35": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "37": Redirect( Const.PAGE_BOLETINMODIFICAR + "?page=" + page ); break;
					case "38": Redirect( Const.PAGE_BOLETININGRESAR + "?page=" + page ); break;
					case "39": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "40": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "41": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "42": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "43": Redirect( Const.PAGE_CARTASGENERAR + "?page=" + page ); break;
					case "44": Redirect( Const.PAGE_BOLETINIMPORTAR + "?page=" + page ); break;
					case "45": Redirect( Const.PAGE_PODERCONSULTA + "?page=" + page ); break;
					case "46": Redirect( Const.PAGE_PODERCONSULTA + "?page=" + page ); break;
					case "47": Redirect( Const.PAGE_PODERSITUACION + "?page=" + page ); break;
					case "48": Redirect( Const.PAGE_PODERSITUACION + "?page=" + page ); break;
					//Busqueda fonetica
					case "50": Process.Start(@"c:/dev/Berke.Marca/Code/Fonet/bin/Debug/Fonet.exe"); break;
//					case "50": Process.Start(@"c:\dev\Berke.Marca\Code\Fonet\bin\Debug\Fonet.exe"); break;
						//Entidades
					case "51": Redirect( Const.PAGE_ENTIDADESING + "?page=" + page ); break;
					case "52": Redirect( Const.PAGE_ENTIDADESCONACT + "?Operacion=Consultar&paginaSiguiente=../../Entidades/ABM/WebForm_Actualizacion.aspx" ); break;
					case "53": Redirect( Const.PAGE_CONTACTOSING + "?page=" + page ); break;
					case "54": Redirect( Const.PAGE_CONTACTOSCONACT + "?page=" + page ); break;

					case "55": Redirect( @"~/Home/AvisoConsulta.aspx" + "?page=" + page ); break;
					case "56": Redirect( @"~/Home/ExpedienteConsulta.aspx" + "?page=" + page ); break;

					// agregar aqui proximas paginas
					default :
					Redirect( @"~/Home/Login.aspx" );
					break;
				}
			}
			else
				Redirect( "/" );			
		}

		private bool IsAuthorized( string page ){
			bool isAuthorized = true; // model.Task_Authorize( page, MySession.Rol );
			return isAuthorized;
		} 

		#region Utils
		private void Redirect( string page ){
			Response.Redirect( page, false );
		}
		#endregion
	}
}
