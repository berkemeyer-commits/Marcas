namespace Berke.Marcas.WebUI.Controls {
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.UI;
	using System.Web.Security;
	using Framework.Core;

	using Helpers;
	// using RM = Helpers.Resources;
	/// <summary>
	/// Login.
	/// </summary>
	public partial class Login : System.Web.UI.UserControl {

		#region Web Form Designer generated code
		override protected void OnInit( EventArgs e ) {

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

		private UIProcess.Model.Usuario _model = new UIProcess.Model.Usuario();

		protected void Page_Load( object sender, System.EventArgs e ) {
			if(!IsPostBack){
				//TODO: remove next line
				vldReqPin.Enabled = false;
			}
		}

		protected void btnLogin_Click(object sender, System.EventArgs e) {
			try {
				//BizDocuments.Customer.UserDS userDS = _model.Identify( txtAtmCardNumber.Text, txtPin.Text );

				//MySession.User = userDS;

				//FormsAuthentication.SetAuthCookie( userDS.User[0].Description, false );
				FormsAuthentication.RedirectFromLoginPage( txtAtmCardNumber.Text, false );
				lblMessage.Visible = false;
			} catch ( FunctionalException ) {
				lblMessage.Visible = true;
			}		
		}
	}
}