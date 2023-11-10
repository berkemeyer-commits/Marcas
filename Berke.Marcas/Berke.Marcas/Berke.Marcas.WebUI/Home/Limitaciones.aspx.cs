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
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class Limitaciones : System.Web.UI.Page
	{
		#region Declaración de los controles

		protected System.Web.UI.WebControls.TextBox TextBox1;
		#endregion

		#region PageLoad
		protected void Page_Load(object sender, System.EventArgs e)
		{
			TableGateway otTV = new TableGateway(MySession.ordenTrabajoTVDS.MarcaVarios);
			otTV.Find( "ExpedienteID", MySession.MarcaRegRenID );
			txtClaseDescrip.Text = otTV.AsString("ClaseDescripEsp");
			lblTitIdioma.Text = "Limitaciones";
			pnldgIdiomaClase.Visible=true;
		}
		#endregion

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
			this.dgIdioma.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgIdioma_ItemCommand);

		}
		#endregion

		#region dgIdioma
		
		private void dgIdioma_ItemCommand(object sender, DataGridCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "Editar":
					dgIdioma.EditItemIndex = e.Item.ItemIndex;
					Grid.Bind( dgIdioma, MySession.ordenTrabajoTVDS.MarcaVarios );
					break;

				case "Cancelar":
					dgIdioma.EditItemIndex =  -1;
					Grid.Bind( dgIdioma, MySession.ordenTrabajoTVDS.MarcaVarios );
					break;

				case "Guardar":
					TextBox vdescrip;
					vdescrip = (TextBox) e.Item.FindControl("txtClaseDescrip");
					TableGateway otrabTVDS = new TableGateway( MySession.ordenTrabajoTVDS.MarcaVarios );
					otrabTVDS.Go(e.Item.ItemIndex);
					otrabTVDS.SetValue( "ClaseDescripEsp", "Prueba" );
					otrabTVDS.SetValue( "Limitaciones", true );

					dgIdioma.EditItemIndex = -1;
					Grid.Bind( dgIdioma, MySession.ordenTrabajoTVDS.MarcaVarios );
					break;
			}

		}
		#endregion

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

		protected void lnkGuardar_Click(object sender, System.EventArgs e)
		{
			TableGateway otrabTVDS = new TableGateway( MySession.ordenTrabajoTVDS.MarcaVarios );
			otrabTVDS.Find("ExpedienteID", MySession.MarcaRegRenID );
			otrabTVDS.SetValue( "ClaseDescripEsp", txtClaseDescrip.Text );
			otrabTVDS.SetValue( "Limitaciones", true );
		}

		protected void txtClaseDescrip_TextChanged(object sender, System.EventArgs e)
		{
			string prueba = txtClaseDescrip.Text;
		}

	}
}
