using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Framework.Core;
using Framework.Channels;

namespace Berke.Marcas.WebUI.Controls {

	// using RM = Helpers.Resources;
	using System.Security;
	using System.Web.Security;
	using Helpers;

	/// <summary>
	/// Header
	/// </summary>
	public partial class LoggedInHeader : System.Web.UI.UserControl {
		#region Declaración de controles
		private bool _menuIsVisible = true;
		#endregion Declaración de controles

		#region Web Form Designer generated code
		override protected void OnInit( EventArgs e ) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit( e );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

		}
		#endregion

		#region Properties
		public bool MenuIsVisible 
		{
			get {
				return _menuIsVisible;
			}
			set {
				_menuIsVisible = value;
			}
		}
		#endregion

		#region Page_Load
		protected void Page_Load( object sender, System.EventArgs e ) 
		{			
			if(!IsPostBack) {
				//lblDBName.Text = MyApplication.CurrentServerName + " / " + MyApplication.CurrentDBName; //Berke.Marcas.UIProcess.Model.GAD.CurrentDB(); // (string) Config.GetConfigParam("CURRENT_DATABASE");
                lblDBName.Text = MyApplication.CurrentServerName + " / " + MyApplication.CurrentDBName + " - Servidor Web: " + MyApplication.CurrentWebServerName;
                CargarMenu();
				lblScript.Enabled = false;
				lblScript.Visible = false;
			}
		}
		#endregion Page_Load

		#region lkbLogout_Click
		private void lkbLogout_Click( object sender, System.EventArgs e ) 
		{
			FormsAuthentication.SignOut();
			Session.RemoveAll();
			Response.Redirect( Const.PAGE_LOGIN, true );
		}
		#endregion lkbLogout_Click

		#region Spaces
		private string Spaces( int rep )
		{
			string str="";
			for( int i=0 ; i < rep; i++)
			{
				str+="-";
			}
			return str;
		}
		#endregion Spaces

		#region CargarMenu
		private void CargarMenu()
		{
			#region Declaración de objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Menu menu = new Berke.DG.DBTab.Menu( db );
			menu.ClearOrder();
			menu.Dat.Ord.Order = 1;
			menu.Adapter.ReadAll();
			string sangria, destino, param, texto = "";
			int nromenu = 0;
			const int charsForTab = 1;
			#endregion Declaración de objetos locales

			string user = Berke.Libs.Base.Acceso.GetCurrentUser();

			for( menu.GoTop(); ! menu.EOF; menu.Skip() ) {
				if (menu.Dat.Nivel.AsInt == 1) {
					sangria = "";
				} else {
					sangria = Spaces( menu.Dat.Nivel.AsInt * charsForTab );
				}
				destino = menu.Dat.Destino.AsString.Trim();
				param = menu.Dat.Param.AsString.Trim();
				texto = menu.Dat.Texto.AsString;
				if (menu.Dat.Nivel.AsInt == 0) {
					#region Configurar Titulo
					nromenu = nromenu + 1;
					switch (nromenu) {
						case 1: lbMenu1.Text = texto; ddlMenu1.Items.Add(""); break;
						case 2: lbMenu2.Text = texto; ddlMenu2.Items.Add(""); break;
						case 3: lbMenu3.Text = texto; ddlMenu3.Items.Add(""); break;
						case 4: lbMenu4.Text = texto; ddlMenu4.Items.Add(""); break;
						case 5: lbMenu5.Text = texto; ddlMenu5.Items.Add(""); break;
					}
					#endregion Configurar Titulo
				} else {
					#region Configurar Item

					if( Berke.Libs.Base.Acceso.chkOperacionPermitida( menu.Dat.Cod.AsString, user, db )) {
						if( destino != "") {
							if( param != "" ) {
								destino += "?"+param;
							}						
						}
						ListItem item = new ListItem(sangria + texto, destino);					
						switch (nromenu) {
							case 1: ddlMenu1.Items.Add(item); break;
							case 2: ddlMenu2.Items.Add(item); break;
							case 3: ddlMenu3.Items.Add(item); break;
							case 4: ddlMenu4.Items.Add(item); break;
							case 5: ddlMenu5.Items.Add(item); break;
						}
					}
					#endregion Configurar Item
				}
			}

			db.CerrarConexion();
		}
		#endregion CargarMenu

		#region ddlMenu1_SelectedIndexChanged
		protected void ddlMenu1_SelectedIndexChanged(object sender, System.EventArgs e) 
		{
            DropDownList ddl = (DropDownList) sender;
			string url = ddl.SelectedValue.ToString();
			Response.Redirect(url);
		}
		#endregion ddlMenu1_SelectedIndexChanged

		private static string SEARCH_REGISTRO = "0";
		private static string SEARCH_ACTA     = "1";
		private static string SEARCH_EXPEID   = "2";		

		protected void btnBuscar_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Expediente expe    = new Berke.DG.DBTab.Expediente(db);
			Berke.DG.DBTab.MarcaRegRen regren = new Berke.DG.DBTab.MarcaRegRen(db);
			int expeID = -1;

			try 
			{

				if (ddlSearch.SelectedValue == SEARCH_REGISTRO)
				{
					regren.Dat.RegistroNro.Filter = Convert.ToInt32(txtBuscar.Text);
					regren.Adapter.ReadAll();
					if(regren.RowCount>0)
					{
						expeID = regren.Dat.ExpedienteID.AsInt;
					}
				}
				else if (ddlSearch.SelectedValue == SEARCH_ACTA)
				{
					string [] acta = txtBuscar.Text.Split("/".ToCharArray());
					expe.Dat.ActaNro.Filter  = acta[0];
					expe.Dat.ActaAnio.Filter = acta[1];
					expe.Adapter.ReadAll();
					if (expe.RowCount>0)
					{
						expeID = expe.Dat.ID.AsInt;
					}

				}
				else if (ddlSearch.SelectedValue == SEARCH_EXPEID)
				{
					expeID = Convert.ToInt32(txtBuscar.Text);
					expe.Adapter.ReadByID(expeID);
					if (expe.RowCount>0)
					{
						expeID = expe.Dat.ID.AsInt;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error al realizar la búsqueda. Asegurese de haber ingresado correctamente los datos. "+ex.Message);
			}
			db.CerrarConexion();

			string pagina = "";
			if (expeID == -1)
			{
				string scriptCliente=@"<script type=""text\javascript""> displayStaticMessage( ""<span class='titulo'>B&uacute;squeda</span><br>No existen datos para el criterio seleccionado.<br><input type='submit' class='btn_close' value='Cerrar' onclick='javascript:closeMessage()'>"" ,false);</script>";
				lblScript.Text = scriptCliente;
				lblScript.Enabled = true;
				lblScript.Visible = true;
				pagina = "ExpeMarcaConsultar.aspx";
			}
			else 
			{
				pagina = "MarcaDetalleL.aspx";
			}

			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
			url.AddParam("ExpeID", expeID.ToString() );
			url.redirect( pagina );
		

			
		
		}

	}
}