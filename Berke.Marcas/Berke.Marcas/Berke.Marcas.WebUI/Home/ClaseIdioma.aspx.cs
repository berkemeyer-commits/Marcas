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
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
	//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Marcas.WebUI.Helpers;

	public partial class ClaseIdioma : System.Web.UI.Page
	{
		
		#region Properties
		

		

		#endregion Properties

		#region Controles del Web Form

		protected System.Web.UI.WebControls.TextBox txtPwd;
		protected System.Web.UI.WebControls.TextBox txtPubNro;
		protected System.Web.UI.WebControls.Panel pnlRegistro;
		#endregion Controles del Web Form


		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.ViewTab.vMarcaClaseIdiomaTraduccion vTrad = new Berke.DG.ViewTab.vMarcaClaseIdiomaTraduccion(db);
				
				string MarcaID_str = UrlParam.GetParam("MarcaID");
				if( MarcaID_str != "" )
				{ 
					int MarcaID = Convert.ToInt32( MarcaID_str );
					vTrad.ClearFilter();
					vTrad.Dat.marcaid.Filter = MarcaID;
					vTrad.Adapter.ReadAll();

					if (vTrad.RowCount <= 0)
					{
						lblTitulo.Text = "No existen traducciones asociadas a esta marca.";
						dgResult.Visible = false;
					}
					else
					{
						lblTitulo.Text = "Traducciones";
						dgResult.DataSource = vTrad.Table;
						dgResult.DataBind();
					}

					
				}
				
				//AsignarValoresIniciales();
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

	

	}
}