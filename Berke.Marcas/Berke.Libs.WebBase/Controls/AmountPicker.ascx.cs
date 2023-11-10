namespace Berke.Libs.WebBase.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

	/// <summary>
	/// AmountPicker: textBox and validators, with a decimal property
	/// </summary>
	public class AmountPicker : System.Web.UI.UserControl {

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
			this.Load += new System.EventHandler( this.Page_Load );

		}
		#endregion

		protected System.Web.UI.WebControls.CompareValidator vldCmpAmount;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldReqAmount;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.Label lblAmount;

		#region properties
		[Description( "The text of the label on top of the text box" )]
		public string Label {
			get {
				return lblAmount.Text;
			}
			set {
				lblAmount.Text = value;
			}
		}


		[Description( "The amount expressed in the TextBox" )]
		public decimal Amount {
			get {
				decimal dec;

				dec = decimal.Zero;
				if ( txtAmount.Text.Length != 0 ) {
					try {
						dec = Convert.ToDecimal( txtAmount.Text );
					} catch {}
				}
				return dec;
			}
			set {
				if ( value == decimal.Zero )
					txtAmount.Text = "";
				else
					txtAmount.Text = value.ToString( "n" );
			}
		}

		[Description( "The text of the required field validator error message" )]
		public string RequiredFieldErrorMessage {
			get {
				return vldReqAmount.ErrorMessage;
			}
			set {
				vldReqAmount.ErrorMessage = value;
			}
		}
		
		[Description( "The text of the comparer validator error message" )]
		public string InvalidInputErrorMessage {
			get {
				return vldCmpAmount.ErrorMessage;
			}
			set {
				vldCmpAmount.ErrorMessage = value;
			}
		}

		#endregion

		#region Page_Load
		private void Page_Load( object sender, System.EventArgs e ) {

		}
		#endregion
	}
}
