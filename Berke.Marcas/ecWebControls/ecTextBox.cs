using System;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;

[assembly: TagPrefix("ecWebControls","ecCtrl")] // (namespace/assembly, prefijo )

namespace ecWebControls
{
	#region ecTextBox Class

	[	DefaultProperty("Text"),
		Designer(typeof(ecWebControls.ecTextBoxDesigner)),
		ParseChildren(true),
		PersistChildren(false),
		ToolboxData("<{0}:ecTextBox runat=server></{0}:ecTextBox>")]
	public class ecTextBox : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		#region Datos Miembro
		private Label	_label;
		private TextBox	_textBox;
		private RequiredFieldValidator _reqValidator;
		private ValidationDataType _validDataType;
		private Calendar _calendar;
		private Button _calendarButton;

		#endregion Datos Miembro


		#region Propiedades

		#region HAlign
		[ Bindable(true),	Category("Layout"),	DefaultValue( System.Web.UI.WebControls.HorizontalAlign.Left)]
		public System.Web.UI.WebControls.HorizontalAlign HAlign
		{
			get	{ return (System.Web.UI.WebControls.HorizontalAlign) ViewState["HAlign"]; }
			set	{
				ViewState["HAlign"] = value;
				this.Controls.Clear();
				this.CreateChildControls();
			}
		}
		#endregion

		#region Label
		[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
		public string Label
		{
			get	{ _label.Text =  (string) ViewState["label"]; return _label.Text; }
			set	{ ViewState["label"] = value; }
		}
		#endregion

		#region LabelWidth
		[ Bindable(true),	Category("Layout")]
		public System.Web.UI.WebControls.Unit LabelWidth
		{
			get	{
					_label.Width = (System.Web.UI.WebControls.Unit) ViewState["LabelWidth"];
					return _label.Width;
				}
		
			set	{
				ViewState["LabelWidth"] =  value; 
			}
		}
		#endregion

		#region Text
		[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
		public string Text
		{
			get	{ _textBox.Text =  (string) ViewState["Text"]; return _textBox.Text; }
			set	{ 				
				ViewState["Text"] = value;
				_textBox.Text = value;
			}

		}
		#endregion

		#region TextWidth
		[ Bindable(true),	Category("Layout")]
		public System.Web.UI.WebControls.Unit TextWidth
		{
			get	{
				_textBox.Width = (System.Web.UI.WebControls.Unit) ViewState["TextWidth"];
				return _textBox.Width;
			}
			set	{
				ViewState["TextWidth"] =  value; 
			}
		}
		#endregion

		#region MaxLength
		[ Bindable(true),	Category("Behavior")]
		public int MaxLength
		{
			get	{
				_textBox.MaxLength = (int) ViewState["MaxLength"];
				return _textBox.MaxLength;
			}
			set	{
				ViewState["MaxLength"] =  value; 
			}
		}
		#endregion

		#region ShowLabel
		[ Bindable(true),	Category("Behavior"),	DefaultValue(true)]
		public bool ShowLabel
		{
			get	{ return (bool) ViewState["ShowLabel"]; }
			set	{ ViewState["ShowLabel"] = value;
				this.Controls.Clear();
				this.CreateChildControls();
			}
		}
		#endregion

		#region Required
		[ Bindable(true),	Category("Behavior"),	DefaultValue(false)]
		public bool Required
		{
			get{	return (bool) ViewState["Required"];}
			set	{
				ViewState["Required"] = value;
				this.Controls.Clear();
				this.CreateChildControls();
			}
		}
		#endregion

		#region ValidDataType
		[ Bindable(true),	Category("Behavior"),	DefaultValue("")]
		public ValidationDataType ValidDataType
		{
			get	
			{
				_validDataType =  (ValidationDataType) ViewState["ValidDataType"];
				return _validDataType; }
			set	
			{
				ViewState["ValidDataType"] = value;
				this.Controls.Clear();
				this.CreateChildControls();
			}
		}
		#endregion

		#endregion Propiedades


		#region CreateChildControls  override  

		protected override void CreateChildControls()
		{
			base.CreateChildControls ();
		
			#region Valores iniciales

			if( ViewState["label"]			== null ){	ViewState["label"]			= this.ID;	}
			if( ViewState["LabelWidth"]		== null ){	ViewState["LabelWidth"]		= (System.Web.UI.WebControls.Unit) 100;		}
			if( ViewState["Text"]			== null ){	ViewState["Text"]			= "";	}
			if( ViewState["TextWidth"]		== null ){	ViewState["TextWidth"]		= (System.Web.UI.WebControls.Unit) 100;		}
			if( ViewState["ShowLabel"]		== null ){	ViewState["ShowLabel"]		= true;		}
			if( ViewState["Required"]		== null ){	ViewState["Required"]		= false;		}
			if( ViewState["ValidDataType"]	== null ){	ViewState["ValidDataType"]	= ValidationDataType.String;}
			if( ViewState["LeftAlign"]		== null ){	ViewState["LeftAlign"]	= true;}
			if( ViewState["HAlign"]			== null ){	ViewState["HAlign"]	= System.Web.UI.WebControls.HorizontalAlign.Left;}
			if( ViewState["MaxLength"]		== null ){	ViewState["MaxLength"]	= (int) 32767;}
			
			#endregion

			#region Label
			_label			= new Label();
			_label.ID		= "label1";
			_label.Text		= this.Label;
			_label.Width	= this.LabelWidth;
			_label.Attributes["Align"] = "Right";
			#endregion
	
			#region TextBox
			this._textBox	= new TextBox();
			_textBox.ID		= "textBox1";
			_textBox.Text	= this.Text;
			_textBox.Width	= this.TextWidth;
			_textBox.MaxLength = this.MaxLength;
			_textBox.TextChanged+=new EventHandler(_textBox_TextChanged);
			#endregion

			#region Agregar Controles

			#region HAlign Abrir
			Controls.Add(new LiteralControl(@"<Div Align=""" + HAlign +@""" >"));
			#endregion

			#region ShowLabel
			if(ShowLabel )
			{
				Controls.Add(_label);
			
				Controls.Add(new LiteralControl("&nbsp"));
				Controls.Add(_textBox);
			}
			else{
				Controls.Add(_textBox);
			}
			#endregion ShowLabel

			#region Required
			if( Required )
			{
				Controls.Add(new LiteralControl("<B>!</b>"));
				_reqValidator = new RequiredFieldValidator();
				_reqValidator.ID = "requiredField1";
				_reqValidator.ControlToValidate = "textBox1";
				_reqValidator.Text = "*";
				_reqValidator.ErrorMessage = "Obligatorio";
				_reqValidator.Display = ValidatorDisplay.Dynamic;


				Controls.Add(new LiteralControl("&nbsp") );
				Controls.Add( _reqValidator );
			}
			#endregion Required

			#region Calendar
			if( ValidDataType == ValidationDataType.Date )
			{
				string div= @"<div id=""pnlCalendar"" style=""Z-INDEX: 4000; POSITION: absolute"">";
				_calendarButton = new Button();
				_calendarButton.Text = "+";
				_calendarButton.Width = 15;
				_calendarButton.Click += new System.EventHandler(this.btn_Click);
				Controls.Add(_calendarButton);

				_calendar = new Calendar();
				_calendar.BackColor = System.Drawing.Color.White;
				_calendar.ShowDayHeader = true;
				_calendar.SelectionChanged +=new EventHandler(_calendar_SelectionChanged);
				_calendar.SelectedDayStyle.BackColor = System.Drawing.Color.WhiteSmoke;
				_calendar.SelectedDayStyle.ForeColor = System.Drawing.Color.Red;
				_calendar.TodayDayStyle.BorderColor= System.Drawing.Color.Red;
				_calendar.TodayDayStyle.BorderWidth = 1;
				_calendar.DayNameFormat = DayNameFormat.FirstTwoLetters;
				_calendar.OtherMonthDayStyle.ForeColor =  System.Drawing.Color.WhiteSmoke;
				_calendar.Visible = false;

				Controls.Add(new LiteralControl(div));

				Controls.Add(_calendar);
				
				Controls.Add(new LiteralControl("</div>"));

			}
			#endregion Calendar

			#region DataType Validator
			if ( ValidDataType != ValidationDataType.String )
			{
				CompareValidator valid = new CompareValidator();
				valid.ID = "DataType1";
				valid.ControlToValidate =  "textBox1";
				valid.Text = this.ValidDataType.ToString();
				valid.Operator = ValidationCompareOperator.DataTypeCheck;
				valid.Type	= ValidDataType;
				Controls.Add( valid );
			}
			#endregion DataType Validator

			#region HAlign Cerrar
			Controls.Add(new LiteralControl(@"</Div>"));
			#endregion
			#endregion Agregar Controles
		}


		#endregion 

		#region override of Controls 
		
		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}
		#endregion


		#region Funciones Locales

		private void btn_Click(object sender, System.EventArgs e )
		{
			if( _calendarButton.Text == "+"){
				_calendarButton.Text = "-";
				if( _textBox.Text != string.Empty ){
					_calendar.SelectedDate = DateTime.Parse( _textBox.Text );
				}
				_calendar.Visible = true;
			}
			else{
				_calendarButton.Text = "+";
				_calendar.Visible = false;
			}
		}

		private void _calendar_SelectionChanged(object sender, EventArgs e)
		{
			_textBox.Text = _calendar.SelectedDate.ToShortDateString();
		}

		#endregion Funciones Locales

		//		protected override void Render(HtmlTextWriter output)
		//		{
		////			output.Write(Text);
		//			Label lbl = (Label) this.Controls[0];
		//			TextBox tx = (TextBox)this.Controls[2];
		//
		//			lbl.RenderControl( output );
		//			tx.RenderControl( output );
		//		
		//		}

		private void _textBox_TextChanged(object sender, EventArgs e)
		{
			ViewState["Text"] = this._textBox.Text;
		}
	}
	#endregion ecTextBox Class

	#region ecTextBox Designer

	public class ecTextBoxDesigner : ControlDesigner {

		public override string GetDesignTimeHtml()
		{
	//		string buf="";
			ecTextBox control = (ecTextBox) Component;
			ControlCollection children = control.Controls;
			return base.GetDesignTimeHtml();
		}

		public override void Initialize(IComponent component)
		{
			if ( ! (component is ecTextBox) ) throw new ArgumentException("El componente debe ser de tipo ecTextBoc","component");
			base.Initialize (component);
		}

//		protected override  void   OnControlResize() // <== No funciona
//		{
//			ecTextBox control = (ecTextBox) Component;
//			Label lbl = (Label) control.Controls[0];
//			TextBox tx = (TextBox)control.Controls[2];
////			base.OnControlResize ();
//
//			control.TextWidth =  (System.Web.UI.WebControls.Unit) 25;
//			this.UpdateDesignTimeHtml();
//
//		}

	}

	#endregion ecTextBox Designer


} // end namespace
