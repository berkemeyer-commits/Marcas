
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
	using BizDocuments.Marca;
	using Helpers;
//	using Berke.Marcas.BizActions;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;

	public partial class AtencionXVia : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Panel pnlVClienteXVia;


		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();

		#region Controles Web

		
		
		#endregion Controles Web

		#region Datos Miembro


		#endregion Datos Miembro

		#region Properties

		#region SessionID
		private string SessionID 
		{
			get{ return HttpContext.Current.Session.SessionID.ToString();}
		}
		#endregion SessionID

		#region Atencion_Param
		private string Atencion_Param 
		{
			set{ ViewState["Atencion_Param" + SessionID ] = value; }
			get{ return (string) ViewState["Atencion_Param" + SessionID ];}
		}
		#endregion Atencion_param

		#endregion Properties

		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{

				if( !IsPostBack )
				{	
					if(UrlParam.GetParam("AtencionID") != "")
					{
						MySession.ID = Convert.ToInt32(UrlParam.GetParam("AtencionID"));	
					}
					DesplegarVias();
				}

			}
			catch(Exception m)
			{
				string redirectString = "../Generic/Message.aspx" + "?page=" + m.Message + "(" + m.Source + ")";
				Response.Redirect(redirectString);
				redirectString = "";
			}
		}

		#endregion Page_Load

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

		#region Funciones

	

		private void DesplegarVias()
		{
			int atencionID=0;

			if( UrlParam.GetParam("AtencionID") != "") 
			{
				atencionID    = Convert.ToInt32(UrlParam.GetParam("AtencionID"));
				Atencion_Param= UrlParam.GetParam("atencion");
			}

			
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;


			/*
			 *  Se obtienen los datos del Cliente
			 * 			 
			 * */

			Berke.DG.ViewTab.vAtencionXVia view_AtencionXVia = new Berke.DG.ViewTab.vAtencionXVia(); 
			
			view_AtencionXVia.InitAdapter( db );
			view_AtencionXVia.Dat.AtencionID.Filter= atencionID;
			view_AtencionXVia.Adapter.ReadAll();

			
			lblMensaje.Text = Atencion_Param;

			if( view_AtencionXVia.RowCount == 0 )
			{
				lblMensaje.Visible = true;
				lblMensaje.Text = Atencion_Param + " no tiene registrado vias de comunicación "  ;
			}
		

			if( !view_AtencionXVia.IsEmpty ) 
			{
				pnlAtencionXVia.Visible = true;
			}

			dgAtencionXVia.DataSource = view_AtencionXVia.Table;
			dgAtencionXVia.DataBind();

		}



		#endregion Funciones


	}
}
