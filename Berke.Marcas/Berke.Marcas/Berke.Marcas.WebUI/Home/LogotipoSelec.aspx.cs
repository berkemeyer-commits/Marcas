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
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base.Helpers;
using Berke.Marcas.WebUI.Tools.Helpers;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for LogotipoSelec.
	/// </summary>
	public partial class LogotipoSelec : System.Web.UI.Page
	{
		#region Controles del Formulario
		private string w_campo = "";
		#endregion Controles del Formulario
	
		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			w_campo = "";
			if( UrlParam.GetParam("campo") != "") {
				w_campo = UrlParam.GetParam("campo");
			}
			string scriptString = "<script language='javascript'> function pasaValores( tipos ) {";
			scriptString += " window.opener.document.Form1. " + w_campo + ".value = tipos ; ";
			scriptString += " window.opener.focus()  ; ";
			scriptString += " window.close(); }" ;
			scriptString += "</script>";
			Response.Write( scriptString );
			if( !IsPostBack ) {
				#region Habilitar paneles
				this.pnlResultado.Visible	= false;
				this.pnlBuscar.Visible		= true;
				ImgLogotipo.Visible = false;
				lbEtiqueta.Visible = false;
				lbNoReg.Visible = false;
				#endregion Habilitar paneles

				#region Cargar lista de usuarios
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.ViewTab.vFuncionario vFunc = new Berke.DG.ViewTab.vFuncionario( db );
				vFunc.Dat.Funcionario.Order = 1;
				vFunc.Adapter.ReadAll();
				vFunc.NewRow();
				vFunc.Dat.Funcionario.Value = "";
				vFunc.PostNewRow();
				vFunc.Sort();			
				ddlUsuario.DataSource = vFunc.Table;
				ddlUsuario.DataTextField = "Funcionario";
				ddlUsuario.DataValueField = "ID";
				ddlUsuario.DataBind();
				#endregion Cargar lista de usuarios
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

		#region btnBuscar_Click
		protected void btnBuscar_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			#region Ejecutar consulta
			Berke.DG.ViewTab.vLogo logo = new Berke.DG.ViewTab.vLogo( db );
			logo.Dat.ID.Filter = ObjConvert.GetFilter(tbID.Text);
			logo.Dat.Denominacion.Filter = ObjConvert.GetFilter(tbDenominacion.Text);
			logo.Dat.FechaAlta.Filter = ObjConvert.GetFilter(tbFechaAlta.Text);
			logo.Dat.FuncionarioID.Filter = ObjConvert.GetFilter(ddlUsuario.SelectedValue);
			logo.Adapter.ReadAll();
			#endregion Ejecutar consulta

			ImgLogotipo.Visible = false;
			lbEtiqueta.Visible = false;
			if (logo.RowCount > 0) {
				lbNoReg.Visible = false;
				this.pnlResultado.Visible	= true;
				dgResult.DataSource = logo.Table;
				dgResult.DataBind();
			} else {
				this.pnlResultado.Visible	= false;
				lbNoReg.Visible = true;
			}
		}
		#endregion btnBuscar_Click

		#region btnVerLogotipo_Click
		protected void btnVerLogotipo_Click(object sender, System.EventArgs e)
		{		
			CheckBox ch = null;	
			foreach( DataGridItem item in dgResult.Items ) {
				ch = (CheckBox)item.FindControl("cbSel");					
				if ( ch.Checked ) {
                    Label lbID = (Label) item.FindControl("lbID");
					ImgLogotipo.Visible = true;
					lbEtiqueta.Visible = true;
					ImgLogotipo.ImageUrl = "Imagen.aspx?logotipoID=" + lbID.Text;
					break;
				}
			}			
		}
		#endregion btnVerLogotipo_Click

		#region btnCerrar_Click
		protected void btnCerrar_Click(object sender, System.EventArgs e)
		{			
			CheckBox ch;
			string tipos ="";
			foreach( DataGridItem item in dgResult.Items ) {
				ch = (CheckBox)item.FindControl("cbSel");
				if ( ch.Checked ) {
					Label lblID = (Label)item.FindControl("lbID");
					tipos = lblID.Text;
					break;
				}
			}
			
			string scriptString = "<script language='javascript'>pasaValores('" + tipos + "') </script>";
			Page.RegisterClientScriptBlock("Prueba",scriptString);
		}
		#endregion btnCerrar_Click
	}
}
