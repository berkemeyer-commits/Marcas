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

namespace Berke.Marcas.WebUI
{
	using Berke.Libs.Base.Helpers;
	using Berke.Marcas.WebUI.Helpers;

	public partial class Limitada : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			TableGateway otTV = new TableGateway(MySession.ordenTrabajoTVDS.MarcaVarios);
			otTV.Find( "ExpedienteID", MySession.MarcaRegRenID );
			txtClaseDescrip.Text = otTV.AsString("ClaseDescripEsp");
			lblTitIdioma.Text = "Limitaciones";
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

		protected void lnkGuardar_Click(object sender, System.EventArgs e)
		{
			TableGateway otrabTVDS = new TableGateway( MySession.ordenTrabajoTVDS.MarcaVarios );
			otrabTVDS.Find("ExpedienteID", MySession.MarcaRegRenID );
			otrabTVDS.SetValue( "ClaseDescripEsp", txtClaseDescrip.Text );
			otrabTVDS.SetValue( "Limitaciones", true );
		}

		protected void btnAtras_Click(object sender, System.EventArgs e)
		{
			switch(MySession.Estado)
			{
				case "11":
					Response.Redirect("OrdenTrabajoTRAME.aspx" + "?page=Limitada" );
					break;
				case "12":
					Response.Redirect("OrdenTrabajoLCAME.aspx");
					break;
				case "26":
					Response.Redirect("OrdenTrabajoTRAME.aspx");
					break;
				case "27":
					Response.Redirect("OrdenTrabajoLCAME.aspx");
					break;
			}
		}
	}
}
