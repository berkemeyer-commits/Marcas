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
	//using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using System.Globalization;
	using System.Threading;


	public partial class Historico : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
//			DateTimeFormatInfo myDTFI = new CultureInfo( "en-US", false ).DateTimeFormat;
//		
//			Label1.Text = "en-US" + myDTFI.ShortDatePattern + DateTime.Today.ToString("d", myDTFI);
//
//			DateTimeFormatInfo myDTFI1 = new CultureInfo( "en-GB", false ).DateTimeFormat;
//			Label2.Text = "en-GB " + myDTFI1.ShortDatePattern + " " + DateTime.Today.ToString("d", myDTFI1);
//
//			DateTimeFormatInfo myDTFI2 = new CultureInfo( "es-PY", false ).DateTimeFormat;
//			Label3.Text = "es-PY " + myDTFI2.ShortDatePattern + " " + DateTime.Today.ToString("d", myDTFI2);

			Label4.Text = Thread.CurrentThread.CurrentCulture.Name; // + " " + DateTime.Today.ToString("d");

//			DateTimeFormatInfo myDTFI3 = new CultureInfo( Thread.CurrentThread.CurrentCulture.Name, false ).DateTimeFormat;
//
//			Label5.Text = myDTFI3.ShortDatePattern;



			if( !IsPostBack )
			{
//				TableGateway tb = new TableGateway();
//				MarcaRegRenDS marcaDS = UIPModel.MarcaRegRen.ReadByID(1);
//				MySession.marcaDS = marcaDS;
//				//ddlOtrosDetalles.DataTextField= "TabLabel";
//				//ddlOtrosDetalles.DataValueField = "MarcaRegRenID";
//				//ddlOtrosDetalles.DataSource = marcaDS.MarcaHist;
//				//ddlOtrosDetalles.DataBind();
//				//lblLabel.Text= marcaDS.MarcaHist.Rows.Count.ToString();
//				PlaceHolder1.Controls.Add(new LiteralControl("<table width='100%'>"));
//				PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
//				tb.Bind(marcaDS.MarcaHist);
//
//				for (tb.Go(0); tb.Row() < tb.RowCount(); tb.Skip())
//				{
//					Label lbl = new Label();
////					lbl.ID= "lbl" + i.ToString();
//					if ( tb.AsInt("TabIndice") == 0)
//					{
//						PlaceHolder1.Controls.Add(new LiteralControl("<td bgcolor='#ffff66'>"));
//					}
//					else
//					{
//						PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
//					}
//					lbl.Text = tb.AsString("TabLabel");
//					PlaceHolder1.Controls.Add(lbl);
//					PlaceHolder1.Controls.Add(new LiteralControl("&nbsp;"));
//					PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
//				}
//				PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
//				PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
