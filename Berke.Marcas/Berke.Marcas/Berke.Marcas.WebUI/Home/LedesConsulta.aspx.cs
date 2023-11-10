using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using Berke.Libs.WebBase.Helpers;
using System.Web.UI.HtmlControls;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for LedesConsulta.
	/// </summary>
	public partial class LedesConsulta : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

		protected void btnBuscar_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			#region Asignar Parametros
			LedesFactura.Dat.ID.Filter = ObjConvert.GetFilter(tbLedesId.Text.Trim());
			LedesFactura.Dat.invoice_date.Filter = ObjConvert.GetFilter(tbInvoiceDate.Text.Trim());
			LedesFactura.Dat.invoice_total.Filter = ObjConvert.GetFilter(tbInvoiceTotal.Text.Trim());
			LedesFactura.Dat.invoice_number.Filter = ObjConvert.GetFilter(tbInvoiceNumber.Text.Trim());
			LedesFactura.Dat.client_id.Filter = ObjConvert.GetFilter(tbClienteId.Text.Trim());
			LedesFactura.Dat.Estado.Filter = ObjConvert.GetFilter(ddlEstado.SelectedValue);
			LedesFactura.Dat.timekeeper_id.Filter = ObjConvert.GetFilter_Str(tbTimeKeeperId.Text.Trim());
			LedesFactura.Dat.timekeeper_name.Filter = ObjConvert.GetFilter_Str(tbTimeKeeperName.Text.Trim());
			LedesFactura.Dat.usuario.Filter = ObjConvert.GetFilter_Str(tbUsuario.Text.Trim());
			LedesFactura.Adapter.ReadAll();
			#endregion Asignar Parametros

			#region Convertir a Link			
			for( LedesFactura.GoTop(); ! LedesFactura.EOF ; LedesFactura.Skip() ) {
				LedesFactura.Edit();
				LedesFactura.Dat.invoice_description.Value = HtmlGW.Redirect_Link(
					LedesFactura.Dat.ID.AsString,
					LedesFactura.Dat.ID.AsString,
					"LedesIngresar.aspx","ID" );				 
				LedesFactura.PostEdit();
			}
			dgResult.DataSource = LedesFactura.Table;
			dgResult.DataBind();
			#endregion Convertir a Link

		}
	}
}
