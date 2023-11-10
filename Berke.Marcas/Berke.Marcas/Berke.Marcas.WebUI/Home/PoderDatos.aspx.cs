using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
//	using Berke.Marcas.BizActions;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;



	/// <summary>
	/// Summary description for PoderDatos.
	/// </summary>
	public partial class PoderDatos : System.Web.UI.Page
	{

		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			try
			{

				if( !IsPostBack )
				{	
					if(UrlParam.GetParam("PoderID") != "")
					{
						MySession.ID = Convert.ToInt32(UrlParam.GetParam("PoderID"));	
					}
					DesplegarPoder();
				}

			}
			catch(Exception m)
			{
				string redirectString = "../Generic/Message.aspx" + "?page=" + m.Message + "(" + m.Source + ")";
				Response.Redirect(redirectString);
				redirectString = "";
			}

		}

		private void DesplegarPoder()
		{

			#region Obtener Parametros
			int poderID=0;

			if( UrlParam.GetParam("PoderID") != "") 
			{
				poderID = Convert.ToInt32(UrlParam.GetParam("PoderID"));
			}

			
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;

			#endregion Obtener Parametros

			#region Datos del Poder
			Berke.DG.ViewTab.vPoderDatos view_PoderDatos = new Berke.DG.ViewTab.vPoderDatos(); 
			
			view_PoderDatos.InitAdapter( db );
			view_PoderDatos.Dat.ID.Filter= poderID;
			view_PoderDatos.Adapter.ReadAll();

			lblIDPoder.Text       = view_PoderDatos.Dat.ID.AsString    ;
		    lblConcepto.Text      = view_PoderDatos.Dat.Concepto.AsString;
			lblDenominacion .Text = view_PoderDatos.Dat.Denominacion.AsString;
			lblDomicilio.Text     = view_PoderDatos.Dat.Domicilio.AsString; 
			lblPais     .Text     = view_PoderDatos.Dat.pais.AsString;
			lblObs      .Text     = view_PoderDatos.Dat.Obs.AsString;

			lblActa     .Text     = view_PoderDatos.Dat.ActaNro.AsString +"/"+ view_PoderDatos.Dat.ActaAnio.AsString;
			lblInscripcion.Text   = view_PoderDatos.Dat.Inscripcion.AsString;
			lblInscAnho.Text      = view_PoderDatos.Dat.InscripcionNro.AsString +"/"+ view_PoderDatos.Dat.InscripcionAnio.AsString;

			lblNuestra.Text       = view_PoderDatos.Dat.Nuestra.AsString;
			lblLegCons.Text       = view_PoderDatos.Dat.LegCons.AsString; 

			lblLegNot.Text        = view_PoderDatos.Dat.LegNot.AsString; 
			lblLegRelExt.Text     = view_PoderDatos.Dat.LegRelExt.AsString; 
			lblOriginal.Text      = view_PoderDatos.Dat.Original.AsString; 

			 
			/*
			 *  Se obtienen las vias de comunicacion del cliente
			 * 			 
			 * */
			Berke.DG.ViewTab.vPropietarioXPoder view_PropietarioXPoder = new Berke.DG.ViewTab.vPropietarioXPoder();
			view_PropietarioXPoder.InitAdapter( db );
			view_PropietarioXPoder.Dat.PoderID.Filter= poderID;
			view_PropietarioXPoder.Adapter.ReadAll();


			#endregion datos del Propietario

			#region Enlazar datos con grillas

			if( !view_PropietarioXPoder.IsEmpty ) 
			{
				pnlPropietarioXPoder.Visible = true;
			}

			dgPropietarioXPoder.DataSource = view_PropietarioXPoder.Table;
			dgPropietarioXPoder.DataBind();



			
			#endregion Enlazar datos con grillas
	
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
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
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
