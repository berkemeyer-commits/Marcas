namespace Berke.Marcas.WebUI.Home
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;

	public partial class ucTabStrip : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				CreateDynamicContent();
			}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		
		private void CreateDynamicContent()
		{
			tsMain.TabDefaultStyle.Add("text-align", "center");
			tsMain.TabDefaultStyle.Add("border-style", "solid");
			tsMain.TabDefaultStyle.Add("color", "black");
			tsMain.TabDefaultStyle.Add("background-color", "#629acd");
			tsMain.TabDefaultStyle.Add("border-color", "black");
			tsMain.TabDefaultStyle.Add("border-width", "1px");
			tsMain.TabDefaultStyle.Add("width", "190px");
//			tsMain.TabDefaultStyle.Add("height", "");

			tsMain.TabHoverStyle.Add("color", "black");
			tsMain.TabHoverStyle.Add("background-color", "#629acd");
			tsMain.TabHoverStyle.Add("border-color", "black");
			tsMain.TabHoverStyle.Add("border-width", "1px");
			
//
			tsMain.TabSelectedStyle.Add("border-bottom", "none");
			tsMain.TabSelectedStyle.Add("color", "black");
			tsMain.TabSelectedStyle.Add("background-color", "aliceblue");
			tsMain.TabSelectedStyle.Add("font-weight", "bold");
			tsMain.TabSelectedStyle.Add("border-color", "black");
			tsMain.TabSelectedStyle.Add("border-width", "1px");

			tsMain.SepDefaultStyle.Add("border-style", "solid");
			tsMain.SepDefaultStyle.Add("border-top", "none");
			tsMain.SepDefaultStyle.Add("border-left", "none");
			tsMain.SepDefaultStyle.Add("border-right", "none");
			tsMain.SepDefaultStyle.Add("color", "black");
			tsMain.SepDefaultStyle.Add("background-color", "#FFFFFF");
			tsMain.SepDefaultStyle.Add("border-color", "black");
			tsMain.SepDefaultStyle.Add("border-width", "1px");

			MarcaRegRenDS marcaDS = MySession.marcaRegRenDS;
			TableGateway m = new TableGateway( marcaDS.Tables["MarcaHist"] );
			int i;
			for(i=0 ; i < marcaDS.MarcaHist.Count ; i++)
			{
				m.Go(i);
				Microsoft.Web.UI.WebControls.Tab tab=new Microsoft.Web.UI.WebControls.Tab();
				tab.Text= m.AsString("TabLabel");
				tsMain.Items.Add(tab);
				Microsoft.Web.UI.WebControls.TabSeparator sep=new Microsoft.Web.UI.WebControls.TabSeparator();
				tsMain.Items.Add(sep);
				if(m.AsBoolean("tabActual"))
				{MySession.Count = m.AsInt("Indice");}
			}
			tsMain.SelectedIndex = MySession.Count;

		}

		protected void SelectedIndexChange(object sender, EventArgs e)
		{
			MarcaRegRenDS marcaDS = MySession.marcaRegRenDS;
			TableGateway m = new TableGateway( marcaDS.Tables["MarcaHist"] );
			int i = tsMain.SelectedIndex;
			m.Go(i);
			MySession.ID = m.AsInt("ID");
			Response.Redirect("MarcaDetalle.aspx");

		}


	}
}
