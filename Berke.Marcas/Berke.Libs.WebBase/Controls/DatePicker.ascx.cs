namespace Berke.Libs.WebBase.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;
	using System.Threading;

	// using RM = Helpers.Resources;

	/// <summary>
	/// DatePicker: textBox + Calendar to improve date entering
	/// </summary>
	/// 

	public delegate void OnChangeSelection( DateTime fecha );   
	public delegate void OnChangeDate( DateTime dmy );   


	[Description( "textBox + Calendar to improve date entering" )]
	public class DatePicker : System.Web.UI.UserControl {

		protected System.Web.UI.WebControls.Calendar mcalChooseDate;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.CompareValidator vldCmpDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldReqDate;
		protected System.Web.UI.HtmlControls.HtmlImage _imgButton; 

		private	bool isVisible = false;

		private OnChangeDate _OnChangeDateHandler;

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
			this.txtDate.TextChanged += new System.EventHandler(this.txtDate_TextChanged);
			this.mcalChooseDate.VisibleMonthChanged += new System.Web.UI.WebControls.MonthChangedEventHandler(this.mcalChooseDate_VisibleMonthChanged);
			this.mcalChooseDate.SelectionChanged += new System.EventHandler(this.mcalChooseDate_SelectionChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Properties
		[Description( "The chosen date" )]
		public DateTime ChosenDate {
			get {
					return mcalChooseDate.SelectedDate;
			}
			set {
				if (value != DateTime.MinValue)
				{
					txtDate.Text = value.ToString( "d" );
					//txtDate.Text = value.ToString("dd/MM/yyyy");
					//txtDate.Text = Helpers.DateHelper.Format(value);
					mcalChooseDate.SelectedDate = value;
					if ( _OnChangeDateHandler != null ) _OnChangeDateHandler( value );
				}
				else
				{
					txtDate.Text = "";
				}
				IsVisible = false;
			}
		}

		[Description( "The text of the label on top of the text box" )]
		public string Label {
			get {
				return lblDate.Text;
			}
			set {
				lblDate.Text = value;
			}
		}

		public bool IsReqDate{
			get 
			{
				return vldReqDate.Enabled;
			}
			set
			{
				vldReqDate.Enabled = value;
			}

		}
		private bool IsVisible {
			get {
				return isVisible;
			}
			set {
				isVisible = value;
				if ( value ) {
					mcalChooseDate.Style.Remove( "DISPLAY" );
				} else {
					mcalChooseDate.Style.Add( "DISPLAY", "none" );
				}
			}
		}

		[Description( "The lower date accepted" )]
		public string DateLowerLimit {
			get {
				return vldCmpDate.ValueToCompare;
			}
			set {
				vldCmpDate.ValueToCompare = value;
			}
		}

		[Description( "For transactions, are usually only from today" )]
		public bool IsFromNowOn {
			set {
				if ( value ) {
					DateLowerLimit = Helpers.DateHelper.Format( DateTime.Today );
				} else {
					DateLowerLimit = Helpers.DateHelper.Format( DateTime.Today.AddYears( -1 ) );
				};
			}
		}

		[Description( "The text of the required field validator error message" )]
		public string RequiredFieldErrorMessage {
			get {
				return vldReqDate.ErrorMessage;
			}
			set {
				vldReqDate.ErrorMessage = value;
			}
		}
		
		[Description( "The text of the comparer validator error message" )]
		public string InvalidInputErrorMessage {
			get {
				return vldCmpDate.ErrorMessage;
			}
			set {
				vldCmpDate.ErrorMessage = value;
			}
		}

		// Asigna la funcion que se ejecuta cuando se cambia el item seleccionado 
		public OnChangeDate OnChangeDateHandler
		{
			get
			{
				return _OnChangeDateHandler;
			}
			set
			{
				_OnChangeDateHandler = value;
			}
		}
		#endregion

		#region Page_Load
		private void Page_Load( object sender, System.EventArgs e ) 
		{

			mcalChooseDate.ID = mcalChooseDate.ID + this.ID;
			_imgButton.Attributes.Add( "onmousedown", mcalChooseDate.ID + ".style.display=''" );
//			if ( !IsPostBack ) {
				IsVisible = false;
//			}
		}
		#endregion
		
		#region mcalChooseDate events
		private void mcalChooseDate_SelectionChanged( object sender, System.EventArgs e ) {
			Calendar calendar = ( Calendar ) sender;
			ChosenDate = calendar.SelectedDate;
			//_OnChangeDateHandler(calendar.SelectedDate);
		}

		private void mcalChooseDate_VisibleMonthChanged( object sender, System.Web.UI.WebControls.MonthChangedEventArgs e ) {
			// show it after a month change
			IsVisible = true;		
		}
		#endregion

		private void txtDate_TextChanged(object sender, System.EventArgs e)
		{
//			if ( _OnChangeDateHandler != null )
//			{
//				ChosenDate = DateTime.Parse(txtDate.Text);
//				//_OnChangeDateHandler( DateTime.Parse(txtDate.Text) );
//			}
			if (txtDate.Text == "")
			{
				ChosenDate = DateTime.MinValue;
			}
			else
			{
				ChosenDate = DateTime.Parse(txtDate.Text);
			}
		}
	}
}
