namespace Berke.Libs.WebBase.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;
	using System.Web.UI;
	using Berke.Libs.WebBase;
	using Framework.Core;
	using Framework.Util;
	using Berke.Libs.Base;

	/// <summary>
	///	Combo
	/// </summary>
	/// 

	// Publics delegate for this namespaces
	public delegate void OnChangePattern( String patron );   
	public delegate void OnChangeSelectedIndex( int ID );   

	public class Combo : System.Web.UI.UserControl
	{
		#region Datos_Miembros

		private OnChangePattern _OnChangePatternHandler;
		private OnChangeSelectedIndex _OnChangeSelectedIndexHandler;


		protected DropDown ddFoundEntries;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldReqFoundEntries;
		protected System.Web.UI.WebControls.TextBox txtSearchPattern;

		private bool isRequired = true;
		protected System.Web.UI.WebControls.Literal lSearcher;

		#endregion Datos_Miembros


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.txtSearchPattern.TextChanged += new System.EventHandler(this.txtSearchPattern_TextChanged);
			this.ddFoundEntries.SelectedIndexChanged += new System.EventHandler(this.ddFoundEntries_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		#region Properties

		// Asigna la funcion que se ejecuta cuando cambia el patron de búsqueda 
		public OnChangePattern OnChangePatternHandler {
			get{
				return _OnChangePatternHandler;
			}set{
				_OnChangePatternHandler = value;
			}
		}


		
		// Asigna la funcion que se ejecuta cuando se cambia el item seleccionado 
		public OnChangeSelectedIndex OnChangeSelectedIndexHandler 
		{
			get
			{
				return _OnChangeSelectedIndexHandler;
			}set
			 {
				 _OnChangeSelectedIndexHandler = value;
			 }
		}


		public string SearchHandler 
		{
			get
			{
				return lSearcher.Text;
			}set
			 {
				 lSearcher.Text = value;
			 }
		}

		public void SetSearcHandler( Type type )
		{
			SearchHandler = string.Format( "{0},{1}", type.FullName, type.Assembly );
		}

		public string Value {
			get{
				return ddFoundEntries.Value;
			}set{
				ddFoundEntries.Value = value;
			}
		}



		public string Text 
		{
			get{
				ListItem selectedItem = ddFoundEntries.SelectedItem;
				return ( selectedItem == null ) ? String.Empty : selectedItem.Text;
			}set{
				ddFoundEntries.Items.Add(value);
			}	
		}

		public Boolean Enabled {
			get{
				return ddFoundEntries.Enabled;
			}set{
				ddFoundEntries.Enabled = value;
				txtSearchPattern.Enabled = value;
			}
		}

		public bool IsRequired {
			get {
				return isRequired;
			}set{
				isRequired = value;
				vldReqFoundEntries.Enabled = value;
			}
		}

		public bool DropDownPostBack {
			set {
				ddFoundEntries.AutoPostBack = value;
			}
		}
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e){
			if(!IsPostBack){}
		}
		#endregion

		#region Fill
		[Description("Fills from a Datatable")]
		public void Fill( DataTable list ){
			ddFoundEntries.Fill( list, true );
		}
		
		public void Fill( DataTable list, bool prependBlank ){
			ddFoundEntries.Fill( list, prependBlank );
		}

		public void SetInitialItem( String valor, String texto ) 
		{
			ddFoundEntries.Items.Clear();
			System.Web.UI.WebControls.ListItem itm = new ListItem(texto, valor );
			ddFoundEntries.Items.Add(itm);
			ddFoundEntries.Items[0].Selected = true;
		}


		#endregion

		#region Clear
		[Description("Clear a Dropdown & Textbox")]
		public void Clear( ){
			ddFoundEntries.Items.Clear();
			txtSearchPattern.Text = String.Empty;
		}
		
		
		#endregion

		#region txtSearchPattern_TextChanged
		private void txtSearchPattern_TextChanged(object sender, System.EventArgs e) 
		{
			if ( _OnChangePatternHandler != null )
			{
				_OnChangePatternHandler( txtSearchPattern.Text );
			}
			else
			{

				Type type = TypeLoader.LoadType( SearchHandler );
				IABMProvider abmProvider = ( IABMProvider ) Activator.CreateInstance( type );
				DataTable list = abmProvider.ReadByPattern( txtSearchPattern.Text );
				Fill( list );
			}
		}
		#endregion

		private void ddFoundEntries_SelectedIndexChanged(object sender, System.EventArgs e) {
			if ( _OnChangeSelectedIndexHandler != null )
			{
				_OnChangeSelectedIndexHandler( int.Parse( ddFoundEntries.Value) );
			}


		}

		#region Ejemplo_de_uso
/*
 
		using Berke.Libs.WebBase.Controls;
 
		private void Page_Load(object sender, System.EventArgs e) 
		{
			// Asignar manejadores
			cbPropietario.OnChangePatternHandler = new OnChangePattern( asignarComboPropietarios );
			cbPropietario.OnChangeSelectedIndexHandler = new OnChangeSelectedIndex( llenarPoderes );
				
			if( !IsPostBack )
			{
				cbPropietario.DropDownPostBack = false;
				cbPropietario.IsRequired = false;
			}
		}

		private void asignarComboPropietarios( String patron )
		{		
			DataTable prop = UIPModelEntidades.Propietario.ReadByPattern(patron);
			cbPropietario.Fill(prop, true);		
		}

		private void llenarPoderes( int propietarioID  )
		{		
			SimpleEntryDS poder = UIPModelMarcas.Poder.ReadForSelect(propietarioID);
			ddPoder.Fill(poder.Tables[0], false);		
		}


*/
		#endregion Ejemplo_de_uso
	}
}