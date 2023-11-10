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
	public partial class DynHeader : System.Web.UI.UserControl {
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
				lblDBName.Text = MyApplication.CurrentServerName + " / " + MyApplication.CurrentDBName; //Berke.Marcas.UIProcess.Model.GAD.CurrentDB(); // (string) Config.GetConfigParam("CURRENT_DATABASE");
				//CargarMenu();

				lblMenu.Text = this.getMenuTree();
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

		#region MenuTree
		private string getMenuTree()
		{
			#region Obtener lista de opciones
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Menu menu = new Berke.DG.DBTab.Menu( db );
			menu.ClearOrder();
			menu.Dat.Ord.Order = 1;
			menu.Adapter.ReadAll();
			#endregion Obtener lista de opciones

			int currLevel = -1;
			string singleOption = @"<li><a class=""dropElement"" href=""{0}"">{1}</a></li>";
			string menuTree = "";
			for (menu.GoTop(); !menu.EOF; menu.Skip())
			{
				#region Armar url
				string destino = menu.Dat.Destino.AsString;
				if (destino == "")
				{
					destino = "#";
				}
				if( menu.Dat.Param.AsString.Trim() != "" )
				{
					destino += "?"+menu.Dat.Param.AsString.Trim();
				}
				#endregion Armar url

				#region Armar menu
				if (menu.Dat.Nivel.AsInt > currLevel)
				{					
					menuTree += "<ul>\n";
				}

				while (menu.Dat.Nivel.AsInt < currLevel)
				{					
					menuTree += "</ul>\n";
					currLevel--;
				}
				menuTree += string.Format(singleOption, destino, menu.Dat.Texto.AsString) +"\n";
				currLevel = menu.Dat.Nivel.AsInt;
				#endregion Armar menu
			}
			menuTree += "</ul>";
			return  "<div class='mlmenu horizontal arrow bluewhite'>"+menuTree+"</div>";
		}
		#endregion MenuTree

		#region CargarMenu
		/*
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
					if( Berke.Libs.Base.Acceso.OperacionPermitida( menu.Dat.Cod.AsString )) {
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
		*/
		#endregion CargarMenu

		#region ddlMenu1_SelectedIndexChanged
		/*
		private void ddlMenu1_SelectedIndexChanged(object sender, System.EventArgs e) 
		{
            DropDownList ddl = (DropDownList) sender;
			string url = ddl.SelectedValue.ToString();
			Response.Redirect(url);
		}
		*/
		#endregion ddlMenu1_SelectedIndexChanged

	}
}