using System;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Data;

[assembly: TagPrefix("ecWebControls","ecCtrl")] // (namespace/assembly, prefijo )

namespace ecWebControls
{
public enum ComboMode { EnterQuery, ShowResult, ShowError };

public delegate void LoadRequestedHandler		( ecCombo combo, EventArgs e );   
public delegate void SelectedIndexChangedHandler( ecCombo combo, EventArgs e );   


#region ecCombo Class

[	DefaultProperty("Text"),
Designer(typeof(ecWebControls.ecComboDesigner)),
ParseChildrenAttribute(true),
PersistChildrenAttribute(true),
ToolboxData("<{0}:ecCombo runat=server></{0}:ecCombo>")]
public class ecCombo : System.Web.UI.WebControls.WebControl, INamingContainer
{

	#region Datos Miembro

	private Label	_label;
	private TextBox	_textBox;
	private DropDownList _dropClave;
	private DropDownList _drop;
	private Button _btnSearch;
	private Button _btnQry;
	
	#endregion Datos Miembro

	#region Eventos
	
	public  event LoadRequestedHandler			LoadRequested;
	public  event SelectedIndexChangedHandler	SelectedIndexChanged;

	#region OnLoadRequested
	
	protected virtual void OnLoadRequested( EventArgs e )
	{
		if( LoadRequested != null ){
			LoadRequested( this, e );
		}
	}

	#endregion

	#region OnSelectedIndexChanged
	protected virtual void OnSelectedIndexChanged( EventArgs e )
	{
		if( SelectedIndexChanged != null ) {
			SelectedIndexChanged( this, e );
		}
	}

	#endregion

	#endregion

	#region BindDataSource
	public void BindDataSource()
	{
		DataTable tab = (DataTable) ViewState["DataSource"];
		if( tab.Rows.Count <= MaxRecords ){
			_drop.DataSource		= new DataView( tab );
			_drop.DataValueField	= DataValueField;
			_drop.DataTextField		= DataTextField;
			_drop.DataBind();
		}else{
			throw new Exception("Excedida la cantidad maxima de registros");
		}
	}

	public void BindDataSource( DataTable tab, string TextField, string ValueField)
	{
		if( tab.Rows.Count <= MaxRecords ){
			ViewState["DataSource"] = tab;
			DataValueField = ValueField;
			DataTextField  = TextField;
			_drop.DataSource		= new DataView( tab );
			_drop.DataValueField	= ValueField;
			_drop.DataTextField		= TextField;
			_drop.DataBind();
		}else{
			throw new Exception("Excedida la cantidad maxima de registros");
		}

	}
	#endregion 


	#region Metodos privados


	#region SetInitialValue
	public void SetInitialValue( int ID ){
		if( LoadRequested != null )
		{
			ViewState["Text"] = ID.ToString();
			ViewState["SelectedKeyIndex"]	= "1";
			ViewState["SelectedKeyValue"]	= "ID";			

			_btnSearch_Click( this, System.EventArgs.Empty );

			_textBox.Text = ID.ToString();
			_dropClave.SelectedIndex = 1;

//		    OnLoadRequested(System.EventArgs.Empty );
//			this.Mode = ComboMode.ShowResult;
		}

	}
	#endregion SetInitialValue


	private void reloadControls()
	{
	
		Controls.Clear();

		#region Agregar Controles

		#region HAlign Abrir
		Controls.Add(new LiteralControl(@"<Div Align=""" + HAlign +@""" >"));
		#endregion

		#region ShowLabel
		if(ShowLabel )
		{
			Controls.Add(_label);		
			Controls.Add(new LiteralControl("&nbsp"));
		}
		#endregion ShowLabel

		#region TextBox
		Controls.Add(_textBox);
		#endregion

		#region btnSearch
		Controls.Add(_btnSearch);
		#endregion

		#region DropDownList Clave
		Controls.Add(_dropClave );
		#endregion

		#region DropDownList
		Controls.Add(_drop );
		#endregion

		#region btnQry
		Controls.Add(_btnQry );
		#endregion

		if( this.Mode == ComboMode.EnterQuery )
		{
			_textBox.Visible	= true;
			_btnSearch.Visible	= true;
			_dropClave.Visible	= true;

			_drop.Visible		= false;
			_btnQry.Visible		= false;
		} else {
			_textBox.Visible	= false;
			_btnSearch.Visible	= false;
			_dropClave.Visible	= false;

			_drop.Visible		= true;
			_btnQry.Visible		= true;
		} // end if

		#region HAlign Cerrar
		Controls.Add(new LiteralControl(@"</Div>"));
		#endregion

		#endregion Agregar Controles

	}

	#endregion


#region Propiedades

	#region DATA

	#region DataSource
	[ Bindable(true),	Category("Data"),	DefaultValue(""),
	Description("DataTable que provee lo datos para el DropDownList")]
	public DataTable DataSource
	{
		get{ return (DataTable) ViewState["DataSource"]; }
		set{ ViewState["DataSource"] = value; }
	}

	#endregion

	#region DataValueField
	
	[ Bindable(true),	Category("Data"),	DefaultValue(""),
	  Description("Nombre del campo que provee el valor obtenido al elegir un item del DropDownList")]
	public string DataValueField{
		get{ return (string) ViewState["DataValueField"];}
		set{ ViewState["DataValueField"] = value;}
	}
	#endregion

	#region DataTextField
	[ Bindable(true),	Category("Data"),	DefaultValue(""),
	  Description("Nombre del campo que provee el Texto visualizado en el DropDownList")]
	public string DataTextField
	{
		get{ return (string) ViewState["DataTextField"];}
		set{ ViewState["DataTextField"] = value;}
	}
	#endregion
	
	#region MaxRecords
	[ Bindable(true),	Category("Behavior"),	DefaultValue( 200 ),
	  Description("Cantidad maxima de registros que pueden ser recuperados para poblar el DropDownList")]
	public int MaxRecords{
		get{ return (int) ViewState["MaxRecords"];}
		set{ ViewState["MaxRecords"] = value;}
	}
	#endregion

	#endregion DATA

	#region HAlign
	[ Bindable(true),	Category("Layout"),	DefaultValue( System.Web.UI.WebControls.HorizontalAlign.Left)]
	public System.Web.UI.WebControls.HorizontalAlign HAlign
	{
		get	{ return (System.Web.UI.WebControls.HorizontalAlign) ViewState["HAlign"]; }
		set	{
			ViewState["HAlign"] = value;
			reloadControls();
		}
	}
	#endregion

	#region Label
	[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
	public string Label
	{
	get	{ return _label.Text; }
	set	{ _label.Text = value	; ViewState["label"] = value; }
	}
	#endregion

	#region LabelWidth
	[ Bindable(true),	Category("Layout")]
	public System.Web.UI.WebControls.Unit LabelWidth{
		get	{
//			_label.Width = (System.Web.UI.WebControls.Unit) ViewState["LabelWidth"];
			return _label.Width;
		}
				
		set	{
			_label.Width = value; 
			ViewState["LabelWidth"] =  value; 
		}
	}
	#endregion

	#region Text
	[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
	public string Text
	{
		get	{ return (string) ViewState["Text"];  }
		set	{ _textBox.Text = value; } // Activa el evento TextChanged
	}
	#endregion

	#region SelectedKeyValue
	[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
	public string SelectedKeyValue
	{
		get	{ return (string) ViewState["SelectedKeyValue"]; }
	}
	#endregion

	#region SelectedKeyIndex
	[ Bindable(true),	Category("Appearance"),	DefaultValue(0)]
	public int SelectedKeyIndex
	{
		get	{ return (int) ViewState["SelectedKeyIndex"]; }
		set	{ _dropClave.SelectedIndex = value;	} // Activa el evento SelectedIndexChanged 

	}
	#endregion


	#region SelectedValue
	[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
	public string SelectedValue
	{
		get	{ return (string) ViewState["SelectedValue"]; 
		}
	}
	#endregion

	#region SelectedIndex
	[ Bindable(true),	Category("Appearance"),	DefaultValue(0)]
	public int SelectedIndex
	{
		get	{ return (int) ViewState["SelectedIndex"]; }
		set	{ _drop.SelectedIndex = value; } // Activa el evento SelectedIndexChanged
	}
	#endregion

	#region SelectedText
	[ Bindable(true),	Category("Appearance"),	DefaultValue("")]
	public string SelectedText
	{
		get	{ return (string) ViewState["SelectedText"]; }
	}
	#endregion

	#region TextWidth
	[ Bindable(true),	Category("Layout")]
	public System.Web.UI.WebControls.Unit TextWidth
	{
		get	
		{
//			_textBox.Width = (System.Web.UI.WebControls.Unit) ViewState["TextWidth"];
			return _textBox.Width;
		}
		set	
		{
			ViewState["TextWidth"] =  value; 
			_textBox.Width = value;
		}
	}
	#endregion

	#region MaxLength
	[ Bindable(true),	Category("Behavior"),DefaultValue(32767)]
	public int MaxLength
	{
		get	
		{
//			_textBox.MaxLength = (int) ViewState["MaxLength"];
			return _textBox.MaxLength;
		}
		set	
		{
			ViewState["MaxLength"]	= value; 
			_textBox.MaxLength		= value;
		}
	}
	#endregion

	#region ShowLabel
	[ Bindable(true),	Category("Behavior"),	DefaultValue(true)]
	public bool ShowLabel
	{
	get	{ return (bool) ViewState["ShowLabel"]; }
	set	
	{
		ViewState["ShowLabel"] = value;
		this.reloadControls();
	}
	}
	#endregion

//	#region Required
//	[ Bindable(true),	Category("Behavior"),	DefaultValue(false)]
//	public bool Required
//	{
//		get{	return (bool) ViewState["Required"];}
//		set	{
//			ViewState["Required"] = value;
//			this.Controls.Clear();
//			this.CreateChildControls();
//		}
//	}
//	#endregion

	#region Mode
	[ Bindable(true),	Category("Behavior"),	DefaultValue(ComboMode.EnterQuery)]
	public ComboMode Mode
	{
		get{	return (ComboMode) ViewState["ComboMode"];}
		set	
		{
			ViewState["ComboMode"] = value;
			reloadControls();
		}
	}
	#endregion

//	#region KeyItems
//	public ListItemCollection KeyItems
//	{
//		get{	return this._dropClave.Items;}
//	}
//
//	#endregion

#endregion Propiedades

#region CreateChildControls  

protected override void CreateChildControls()
{
	base.CreateChildControls ();
			
	#region Valores iniciales

	if( ViewState["DataSource"]		== null ){	ViewState["DataSource"]	= new DataTable();}
	if( ViewState["DataValueField"]	== null ){	ViewState["DataValueField"]	= "ID";}
	if( ViewState["DataTextField"]	== null ){	ViewState["DataTextField"]	= "Descrip";}

	if( ViewState["MaxRecords"]	== null ){	ViewState["MaxRecords"]	= 200;}
	if( ViewState["ComboMode"]		== null ){	ViewState["ComboMode"]		= ComboMode.EnterQuery;	}

	if( ViewState["label"]			== null ){	ViewState["label"]			= this.ID;	}
	if( ViewState["LabelWidth"]		== null ){	ViewState["LabelWidth"]		= (System.Web.UI.WebControls.Unit) 100;		}
	if( ViewState["Text"]			== null ){	ViewState["Text"]			= "";	}
	if( ViewState["TextWidth"]		== null ){	ViewState["TextWidth"]		= (System.Web.UI.WebControls.Unit) 100;		}
	if( ViewState["ShowLabel"]		== null ){	ViewState["ShowLabel"]		= true;		}
	if( ViewState["Required"]		== null ){	ViewState["Required"]		= false;		}
	if( ViewState["LeftAlign"]		== null ){	ViewState["LeftAlign"]		= true;}
	if( ViewState["HAlign"]			== null ){	ViewState["HAlign"]			= System.Web.UI.WebControls.HorizontalAlign.Left;}
	if( ViewState["MaxLength"]		== null ){	ViewState["MaxLength"]		= (int) 32767;}

	if( ViewState["SelectedKeyIndex"]	== null ){	ViewState["SelectedKeyIndex"]	= (int) 0;}
	if( ViewState["SelectedKeyValue"]	== null ){	ViewState["SelectedKeyValue"]	= "DESC";}

	if( ViewState["SelectedIndex"]		== null ){	ViewState["SelectedIndex"]		= (int) -1;}
	if( ViewState["SelectedValue"]		== null ){	ViewState["SelectedValue"]		= "";}

	if( ViewState["SelectedText"]		== null ){	ViewState["SelectedText"]		= "";}
		
	#endregion

	#region Instanciar Controles
	if( _label == null )
	{
		#region Label
		_label			= new Label();
		_label.ID		= "label1";
		_label.Text		= (string) ViewState["label"];
		_label.Width	= this.LabelWidth;
		_label.Attributes["Align"] = "Right";
		#endregion

		#region TextBox
		this._textBox	= new TextBox();
		_textBox.ID		= "TextBox1";
		_textBox.Text	= (string) ViewState["Text"];
		_textBox.Width	= this.TextWidth;
		_textBox.MaxLength = this.MaxLength;
		_textBox.EnableViewState = true;
		_textBox.AutoPostBack = true;
		#endregion

		#region btnSearch
		_btnSearch = new Button();
		_btnSearch.Text = "Ver en";
		_btnSearch.Font.Size = 7;
		_btnSearch.Width = 40;
		_btnSearch.Height = 17;

		#endregion

		#region DropDownList Clave
		_dropClave = new DropDownList();
		_dropClave.Width = 70;

		_dropClave.Items.Add( new ListItem("Descrip.","DESC"));
		_dropClave.Items.Add( new ListItem("ID","ID"));
		_dropClave.SelectedIndex = (int) ViewState["SelectedKeyIndex"];

		_dropClave.AutoPostBack = true;

		#endregion DropDownList
		
		#region DropDownList de Seleccion
		_drop = new DropDownList();
		_drop.Width = 250;
		_drop.AutoPostBack = true;
		_drop.SelectedIndex = (int) ViewState["SelectedIndex"];
		_drop.ID = "Combo";
		this.BindDataSource();

		#endregion DropDownList

		#region btnQuery
		_btnQry = new Button();
		_btnQry.Text = "Reintentar";
		_btnQry.Font.Size = 7;
		_btnQry.Width = 60;
		_btnQry.Height = 17;
		_btnQry.ToolTip = "";
		#endregion

		#region asignarHadlers
		_dropClave.SelectedIndexChanged	+= new EventHandler(_dropClave_SelectedIndexChanged);
		_btnSearch.Click				+= new EventHandler(_btnSearch_Click);
		_drop.SelectedIndexChanged		+= new EventHandler(_drop_SelectedIndexChanged);
		_btnQry.Click					+= new EventHandler(this._btnQry_Click );
		_textBox.TextChanged			+= new EventHandler(_textBox_TextChanged);

		#endregion

	} // end if _label == null
	#endregion Instanciar Controles

	reloadControls();

} // end CreateChildControls()


#endregion CreateChildControls

#region Controls 
			
	public override ControlCollection Controls
	{
		get{ EnsureChildControls();	return base.Controls;}
	}
#endregion Controls

#region  ---- Tratamiento de Eventos ----

	private void _btnSearch_Click(object sender, EventArgs e)
	{
		OnLoadRequested( EventArgs.Empty );
		if( _drop.Items.Count > 0 )
		{
			_drop.SelectedIndex = 0;
			ViewState["SelectedIndex"]	= _drop.SelectedIndex;
			ViewState["SelectedValue"]	= _drop.SelectedValue;
			ViewState["SelectedText"]	= _drop.SelectedItem.Text;
			this._btnQry.ToolTip		=  _drop.SelectedValue+ " : "+_drop.SelectedItem.Text;
		}
		else{
			this._btnQry.ToolTip		= (string)ViewState["Text"] + " : No hay valores para elegir";
		}
		// this.Mode = ComboMode.ShowResult;
		ViewState["ComboMode"] = ComboMode.ShowResult;
		reloadControls();
	}

	private void _btnQry_Click(object sender, EventArgs e)
	{

//		ViewState["SelectedKeyIndex"]	= (int) 0; eliminado 23/04/06
//		ViewState["SelectedKeyValue"]	= "DESC";

		ViewState["SelectedIndex"]	= (int) -1;
		ViewState["SelectedValue"]	= "";
		ViewState["SelectedText"]	= "";
		_btnQry.ToolTip = "";

		// Mode = ComboMode.EnterQuery;

		ViewState["ComboMode"] = ComboMode.EnterQuery;
		reloadControls();

	}


	private void _textBox_TextChanged(object sender, EventArgs e)
	{
		ViewState["Text"] = _textBox.Text;
	}

	private void _dropClave_SelectedIndexChanged(object sender, EventArgs e)
	{
		ViewState["SelectedKeyIndex"] = _dropClave.SelectedIndex;
		ViewState["SelectedKeyValue"] = _dropClave.SelectedValue;
	}

	#endregion Tratamiento de Eventos

	private void _drop_SelectedIndexChanged(object sender, EventArgs e)
	{
		ViewState["SelectedIndex"] = _drop.SelectedIndex;
		ViewState["SelectedValue"] = _drop.SelectedValue;

		ViewState["SelectedText"] = _drop.SelectedItem.Text;
		
		this._btnQry.ToolTip =  _drop.SelectedValue+ " : "+_drop.SelectedItem.Text;
		OnSelectedIndexChanged( EventArgs.Empty );
	}
}
#endregion ecCombo Class

#region ecCombo Designer

public class ecComboDesigner : ControlDesigner 
{

	#region GetDesignTimeHtml
	public override string GetDesignTimeHtml()
	{
	//	string buf="";
		ecCombo control = (ecCombo) Component;
		ControlCollection children = control.Controls;
		return base.GetDesignTimeHtml();
	}
	#endregion GetDesignTimeHtml

	#region Initialize
	public override void Initialize(IComponent component)
	{
		if ( ! (component is ecCombo) ) throw new ArgumentException("El componente debe ser de tipo ecCombo","component");
		base.Initialize (component);
	}
	#endregion Initialize

}

#endregion ecCombo Designer


} // end namespace








