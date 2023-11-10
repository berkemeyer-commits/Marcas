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
	using Berke.Libs.Base;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Marcas.WebUI.Helpers;
	using BizDocuments.Marca;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;

	public partial class LedesIngresar : System.Web.UI.Page
	{

		#region Properties
		private DataTable GridDataTable
		{
			set {ViewState["dgw.Table"] = value;}
			get {return ViewState["dgw.Table"] == null ? new DataTable() : (DataTable) ViewState["dgw.Table"];}
		}
		#endregion Properties

		#region Controles del Form
		protected Berke.Libs.WebBase.Helpers.GridGW dgw;		
		protected Berke.DG.DBTab.LedesDetalle LedesDetalle;
		protected int LedesFacturaId;
		//Constantes para estados de LedesFactura
		protected const string eABIERTO = "A";
		protected const string eCONFIRMADO = "C";
		protected const string eANULADO = "U";
		//Constantes para acciones del formulario
		protected const char aELIMINARDETALLE = 'E';
		protected const char aGRABAR = 'G';
		protected const char aCONFIRMAR = 'C';
		protected const char aANULAR = 'A';
		protected const char aGENARCHIVO = 'U';
		#endregion Controles del Form

		#region ConfigurarGrilla
		private void ConfigurarGrilla()
		{
			//             CtrlName           Header       CtrlWidth
			dgw.AddCheck( "cbSel"			,"Sel."			, 20 );
			dgw.AddLabel( "lbId"				,"ID"			, 30 );		
			dgw.AddText( "tbItemNumber"	,"Item Number"  , 40 );
			dgw.AddDropDown ( "ddlInvAdjType", "InvAdjType", 95);
			dgw.AddText( "tbNumberUnits"	,"Number Units"	, 60 );
			dgw.AddText( "tbAdjustmentAmount"	,"Adjustment amount", 60);
			dgw.AddText( "tbTotal"	,"Total", 60);
			dgw.AddText( "tbDate", "Date", 70);
			dgw.AddText( "tbTaskCode", "Task Code", 35);
			dgw.AddText( "tbExpenseCode", "Expense Code", 35);
			dgw.AddText( "tbActivityCode", "Activity Code", 35);
			dgw.AddText( "tbDescripcion", "Descripcion", 100);
			dgw.AddText( "tbUnitCost", "Unit Cost", 60);
		}
		#endregion ConfigurarGrilla

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			// Put user code to initialize the page here			
			LedesFacturaId = 0;
			dgw = new GridGW( GridLedesDetalle );
			this.ConfigurarGrilla();

			if ( ! this.IsPostBack ) 
			{   // Si es el primer load				
				LedesDetalle = new Berke.DG.DBTab.LedesDetalle();
				GridDataTable = LedesDetalle.Table;
				if( Request.QueryString.Count < 1 ) 
				{
					dgw.Inicializar( 1 );			
				} 
				else 
				{
					#region Obtener ID
					if( Request.QueryString.Count >= 1 ) 
					{
						LedesFacturaId = Convert.ToInt32(Request.QueryString[0]);
						RecuperarDatos();
					}
					#endregion Obtener ID
				}
			} 
			else 
			{
				LedesDetalle = new Berke.DG.DBTab.LedesDetalle(GridDataTable);
			}
		}
		#endregion Page_Load

		#region RecuperarDatos
		private void RecuperarDatos()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			CargarCabecera( db );
			CargarDetalles( db );
		}
		#endregion RecuperarDatos

		#region CargarCabecera
		private void CargarCabecera(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			LedesFactura.Adapter.ReadByID ( LedesFacturaId );

			tbID.Text = LedesFacturaId.ToString();
			tbCodEstado.Text = LedesFactura.Dat.Estado.AsString;
			tbDescripEstado.Text = obtDescripEstado(LedesFactura.Dat.Estado.AsString);
			tbFechaCreacion.Text = String.Format("{0:d}", LedesFactura.Dat.Fecha.AsDateTime);
			tbUsuario.Text = LedesFactura.Dat.usuario.AsString;
			tbInvoiceDate.Text = String.Format("{0:d}", LedesFactura.Dat.invoice_date.AsDateTime);
			tbInvoiceNumber.Text = LedesFactura.Dat.invoice_number.AsString;
			tbClienteId.Text = LedesFactura.Dat.client_id.AsString;
			tbLawFirmId.Text = LedesFactura.Dat.law_firm_id.AsString;
			tbLawFirmMatterId.Text = LedesFactura.Dat.law_firm_matter_id.AsString;
			//tbInvoiceTotal.Text = String.Format("{0:g}", LedesFactura.Dat.invoice_total.AsDecimal);
			tbInvoiceTotal.Text = String.Format("{0:0.00}", LedesFactura.Dat.invoice_total.AsDecimal);
			tbBillingStartDate.Text = String.Format("{0:d}", LedesFactura.Dat.billing_start_date.AsDateTime);
			tbBillingEndDate.Text = String.Format("{0:d}", LedesFactura.Dat.billing_end_date.AsDateTime);
			tbDescription.Text = LedesFactura.Dat.invoice_description.AsString;
			tbTimeKeeperId.Text = LedesFactura.Dat.timekeeper_id.AsString;
			tbTimeKeeperName.Text = LedesFactura.Dat.timekeeper_name.AsString;
			ddlTimeKeeperClassification.SelectedValue = LedesFactura.Dat.timekeeper_classification.AsString;
			tbClientMatterId.Text = LedesFactura.Dat.client_matter_id.AsString;
		}
		#endregion CargarCabecera

		#region CargarDetalles
		private void CargarDetalles(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			LedesDetalle.InitAdapter( db );
			LedesDetalle.Dat.LedesFacturaID.Filter = LedesFacturaId;
			LedesDetalle.Adapter.ReadAll();	
			dgw.Inicializar( LedesDetalle.RowCount+1 );
			//for (LedesDetalle.GoTop(); ! LedesDetalle.EOF; LedesDetalle.Skip()) {
			foreach (DataGridItem item in GridLedesDetalle.Items) 
			{
				dgw.Set(item, "lbId", LedesDetalle.Dat.ID.AsString);
				dgw.Set(item, "tbItemNumber", LedesDetalle.Dat.line_item_number.AsString);
				dgw.Set(item, "ddlInvAdjType", LedesDetalle.Dat.exp_fee_inv_adj_type.AsString.Trim());
				//dgw.Set(item, "tbNumberUnits", LedesDetalle.Dat.line_item_number_of_units.AsString);
				//dgw.Set(item, "tbNumberUnits", String.Format("{0:g}", LedesDetalle.Dat.line_item_number_of_units.AsString));
				dgw.Set(item, "tbNumberUnits", String.Format("{0:0.00}", LedesDetalle.Dat.line_item_number_of_units.AsDecimal));
				if (LedesDetalle.Dat.line_item_unit_cost.AsString != "") 
				{
					//dgw.Set(item, "tbUnitCost", String.Format("{0:g}", LedesDetalle.Dat.line_item_unit_cost.AsDecimal));
					dgw.Set(item, "tbUnitCost", String.Format("{0:0.00}", LedesDetalle.Dat.line_item_unit_cost.AsDecimal));
				}
				if (LedesDetalle.Dat.line_item_adjustment_amount.AsString != "") 
				{
					//dgw.Set(item, "tbAdjustmentAmount", String.Format("{0:g}", LedesDetalle.Dat.line_item_adjustment_amount.AsDecimal));
					dgw.Set(item, "tbAdjustmentAmount", String.Format("{0:0.00}", LedesDetalle.Dat.line_item_adjustment_amount.AsDecimal));
				}
				if (LedesDetalle.Dat.line_item_total.AsString != "") 
				{
					//dgw.Set(item, "tbTotal", String.Format("{0:g}", LedesDetalle.Dat.line_item_total.AsDecimal));
					dgw.Set(item, "tbTotal", String.Format("{0:0.00}", LedesDetalle.Dat.line_item_total.AsDecimal));
				}
				if (LedesDetalle.Dat.line_item_date.AsString != "") 
				{
					dgw.Set(item, "tbDate", String.Format("{0:d}", LedesDetalle.Dat.line_item_date.AsDateTime));
				}
				dgw.Set(item, "tbTaskCode", LedesDetalle.Dat.line_item_task_code.AsString);
				dgw.Set(item, "tbExpenseCode", LedesDetalle.Dat.line_item_expense_code.AsString);
				dgw.Set(item, "tbActivityCode", LedesDetalle.Dat.line_item_activity_code.AsString);
				dgw.Set(item, "tbDescripcion", LedesDetalle.Dat.line_item_description.AsString);
				LedesDetalle.Skip();
			}
			GridDataTable = LedesDetalle.Table;
		}
		#endregion CargarDetalles

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

		#region Grabar_Click
		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			string errmsg = "";
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			try {
				db.IniciarTransaccion();
				if (tbID.Text != "") {
					errmsg = ControlarEstado(aGRABAR);
					if (errmsg != "") { throw new Exception(errmsg); }				
				}

				errmsg = GrabarCabecera( db );
				if (errmsg != "") { throw new Exception(errmsg); }
				errmsg = GrabarDetalle( db );
				if (errmsg != "") { throw new Exception(errmsg); }
				db.Commit();
				GridDataTable = LedesDetalle.Table;
				CargarDetalles( db );			
				Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
				url.AddParam("ID", LedesFacturaId.ToString() );
				url.redirect( "LedesIngresar.aspx" );
			} catch (Exception exep) {
				db.RollBack(); 
				if (errmsg != "") {
					this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
					return;
				} else {
					throw new Exception(exep.Message); 
				}
			}
		}
		#endregion Grabar_Click

		#region GrabarCabecera
		private string GrabarCabecera(Berke.Libs.Base.Helpers.AccesoDB db)
		{			
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			string msg = "";
			string estado, fecha;
			
			if (tbID.Text == "") {
				LedesFactura.NewRow();
				estado = eABIERTO;
				fecha = DateTime.Today.ToString("d");
			} else {
				LedesFacturaId = Convert.ToInt32 (tbID.Text);
				LedesFactura.Adapter.ReadByID( LedesFacturaId );
				LedesFactura.AcceptAllChanges();
				LedesFactura.Edit();
				estado = tbCodEstado.Text;
				fecha = tbFechaCreacion.Text;
			}

			#region Validar ingreso de campos no nulos
			//Validar invoice_date
			try { LedesFactura.Dat.invoice_date.Value = Convert.ToDateTime(tbInvoiceDate.Text);
			} catch { msg = "Formato inválido para el campo Invoice date";
					  return msg;
			}
			//Validar billing_start_date
			try { LedesFactura.Dat.billing_start_date.Value = Convert.ToDateTime(tbBillingStartDate.Text);
			} catch { msg = "Formato inválido para el campo Billing start date";
					  return msg;
			}
			//Validar billing_end_date
			try { LedesFactura.Dat.billing_end_date.Value = Convert.ToDateTime(tbBillingEndDate.Text);				
			} catch { msg = "Formato inválido para el campo Billing end date";
					  return msg;
			}
			//Validar invoice_total
			try { LedesFactura.Dat.invoice_total.Value = Convert.ToDecimal(tbInvoiceTotal.Text);
			} catch { msg = "Formato inválido para el campo Invoice Total";
					  return msg;
			}
			#endregion Validar ingreso de campos no nulos

			LedesFactura.Dat.Fecha.Value = fecha;
			LedesFactura.Dat.Estado.Value = estado;			
			Acceso acc = new Acceso();
			LedesFactura.Dat.usuario.Value = acc.Usuario;
			LedesFactura.Dat.invoice_number.Value = tbInvoiceNumber.Text;
			LedesFactura.Dat.client_id.Value = tbClienteId.Text;
			LedesFactura.Dat.law_firm_id.Value = tbLawFirmId.Text;
			LedesFactura.Dat.law_firm_matter_id.Value = tbLawFirmMatterId.Text;
			LedesFactura.Dat.invoice_total.Value = tbInvoiceTotal.Text;
			LedesFactura.Dat.invoice_description.Value = tbDescription.Text;
			LedesFactura.Dat.timekeeper_id.Value = tbTimeKeeperId.Text;
			LedesFactura.Dat.timekeeper_name.Value = tbTimeKeeperName.Text;
			LedesFactura.Dat.timekeeper_classification.Value = ddlTimeKeeperClassification.SelectedValue;
			LedesFactura.Dat.client_matter_id.Value = tbClientMatterId.Text;			
			
			if (tbID.Text == "") {
				LedesFactura.PostNewRow();
				LedesFacturaId = LedesFactura.Adapter.InsertRow();
			} else {
				LedesFactura.PostEdit();
				LedesFactura.Adapter.UpdateRow();
			}
			
			LedesFactura.AcceptAllChanges();
			return msg;
		}
		#endregion GrabarCabecera

		#region GrabarDetalle
		private string GrabarDetalle(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			string msg = "";
			int LedesDetalleId = 0;
			int item_number = 0;
			LedesDetalle.InitAdapter( db );
			foreach( DataGridItem item in GridLedesDetalle.Items ) {
				if (dgw.GetText( item, "tbItemNumber" ) != "") {
					if (dgw.GetText( item, "lbId" ) == "") {
						LedesDetalle.NewRow();
					} else {					
						LedesDetalleId = Convert.ToInt32 (dgw.GetText( item, "lbId" ));
						LedesDetalle.Adapter.ReadByID( LedesDetalleId );
						LedesDetalle.AcceptAllChanges();
						LedesDetalle.Edit();
					}
					
					#region Validar ingreso de campos no nulos
					//Validar line_item_number
					try { item_number = Convert.ToInt32(dgw.GetText( item, "tbItemNumber" ));
					} catch { msg = "Formato inválido para el campo Item Number. Linea #" + dgw.GetText( item, "tbItemNumber" );
							  return msg;
					}
					//Validar line_item_unit_cost
					//try { LedesDetalle.Dat.line_item_unit_cost.Value = Convert.ToDecimal(dgw.GetText( item, "tbUnitCost" ));
					try { LedesDetalle.Dat.line_item_unit_cost.Value = dgw.GetText( item, "tbUnitCost" );
					} catch { msg = "Formato inválido para el campo UnitCost. Linea #" + item_number.ToString();
							  return msg;
					}
					//Validar line_item_date
					try { LedesDetalle.Dat.line_item_date.Value = Convert.ToDateTime(dgw.GetText( item, "tbDate" ));
					} catch { msg = "Formato inválido para el campo Item Date. Linea #" + item_number.ToString();
						      return msg;
					}
					//Validar line_item_total
					//try { LedesDetalle.Dat.line_item_total.Value = Convert.ToDecimal(dgw.GetText( item, "tbTotal" ));
					try { LedesDetalle.Dat.line_item_total.Value = dgw.GetText( item, "tbTotal" );
					} catch { msg = "Formato inválido para el campo Item Total. Linea #" + item_number.ToString();
							  return msg;
					}
					//Validar line_item_number_of_units
					//try { LedesDetalle.Dat.line_item_number_of_units.Value = Convert.ToDecimal(dgw.GetText( item, "tbNumberUnits" ), ni);
					try { LedesDetalle.Dat.line_item_number_of_units.Value = dgw.GetText( item, "tbNumberUnits" );
					} catch { msg = "Formato inválido para el campo Number of Units. Linea #" + item_number.ToString();
							  return msg;
					}
					#endregion Validar ingreso de campos no nulos

					LedesDetalle.Dat.LedesFacturaID.Value = LedesFacturaId;
					LedesDetalle.Dat.line_item_number.Value = item_number;
					LedesDetalle.Dat.exp_fee_inv_adj_type.Value = dgw.GetText( item, "ddlInvAdjType" ).Trim();
					LedesDetalle.Dat.line_item_adjustment_amount.Value = dgw.GetText( item, "tbAdjustmentAmount" );					
					LedesDetalle.Dat.line_item_task_code.Value = dgw.GetText( item, "tbTaskCode" );
					LedesDetalle.Dat.line_item_expense_code.Value = dgw.GetText( item, "tbExpenseCode" );
					LedesDetalle.Dat.line_item_activity_code.Value = dgw.GetText( item, "tbActivityCode" );
					LedesDetalle.Dat.line_item_description.Value = dgw.GetText( item, "tbDescripcion" );

					if (dgw.GetText( item, "lbId" ) == "") {
						LedesDetalle.PostNewRow();
						LedesDetalleId = LedesDetalle.Adapter.InsertRow();
					} else {
						LedesDetalle.PostEdit();
						LedesDetalle.Adapter.UpdateRow();
					}

					LedesDetalle.AcceptAllChanges();
					dgw.Set(item, "lbId", LedesDetalleId.ToString());				
				}
			}
			return msg;
		}
		#endregion GrabarDetalle

		#region btnEliminarDetalle_Click
		protected void btnEliminarDetalle_Click(object sender, System.EventArgs e)
		{
			if (tbID.Text == "") { return; }
			string errmsg = ControlarEstado(aELIMINARDETALLE);
			if (errmsg != "") {
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
				return;
			}
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			db.IniciarTransaccion();
			LedesDetalle.InitAdapter( db );
			LedesFacturaId = Convert.ToInt32 (tbID.Text);
			foreach( DataGridItem item in GridLedesDetalle.Items ) 
			{
				if ( (dgw.GetText( item, "cbSel" ) == "Si") & 
					(dgw.GetText( item, "lbId" ) != "") )
				{
					LedesDetalle.Adapter.ReadByID(Convert.ToInt32(dgw.GetText( item, "lbId" )));
					LedesDetalle.Adapter.DeleteRow();
				}
			}
			db.Commit();
			GridDataTable = LedesDetalle.Table;
			CargarDetalles( db );
		}
		#endregion btnEliminarDetalle_Click	

		#region CambiarEstado
		private void CambiarEstado (string estado)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			db.IniciarTransaccion();
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			LedesFacturaId = Convert.ToInt32 (tbID.Text);
			LedesFactura.Adapter.ReadByID( LedesFacturaId );
			LedesFactura.AcceptAllChanges();
			LedesFactura.Edit();
			LedesFactura.Dat.Estado.Value = estado;
			Acceso acc = new Acceso();
			LedesFactura.Dat.usuario.Value = acc.Usuario;
			LedesFactura.PostEdit();
			LedesFactura.Adapter.UpdateRow();
			LedesFactura.AcceptAllChanges();
			tbCodEstado.Text = estado;
			tbDescripEstado.Text = obtDescripEstado(estado);
			tbUsuario.Text = LedesFactura.Dat.usuario.AsString;
			db.Commit();
		}
		#endregion CambiarEstado

		#region btnConfirmar_Click
		protected void btnConfirmar_Click(object sender, System.EventArgs e)
		{
			if (tbID.Text == "") { return; }

			string errmsg = ControlarEstado(aCONFIRMAR);
			if (errmsg != "") {
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
				return;
			} 
			
			errmsg = ValidarDatos();
			if (errmsg != "") {
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
				return;
			}

			CambiarEstado(eCONFIRMADO);
		}
		#endregion btnConfirmar_Click

		#region ValidarDatos
		private string ValidarDatos() 
		{
			string msg = "";
			decimal monto_total_detalles = 0;

			#region Recuperar valores de la base de datos
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			LedesFacturaId = Convert.ToInt32 (tbID.Text);
			LedesFactura.Adapter.ReadByID( LedesFacturaId );
			LedesDetalle.InitAdapter( db );
			LedesDetalle.Dat.LedesFacturaID.Filter = LedesFacturaId;
			LedesDetalle.Adapter.ReadAll();
			#endregion Recuperar valores de la base de datos

			#region Verificar campos requeridos de la cabecera
			if (LedesFactura.Dat.invoice_number.AsString == "") {
				msg = "Debe completar un valor para el campo Invoice number";
				return msg;
			} else if (LedesFactura.Dat.client_id.AsString == "") {
				msg = "Debe completar un valor para el campo Cliente Id";
				return msg;
			} else if (LedesFactura.Dat.law_firm_id.AsString == "") {
				msg = "Debe completar un valor para el campo Law Firm Id";
				return msg;
			} else if (LedesFactura.Dat.law_firm_matter_id.AsString == "") {
				msg = "Debe completar un valor para el campo Law Firm Matter Id";
				return msg;
			} else if (LedesFactura.Dat.billing_start_date.AsString == "") {
				msg = "Debe completar un valor para el campo Billing start date";
				return msg;
			} else if (LedesFactura.Dat.invoice_total.AsString == "") {
				msg = "Debe completar un valor para el campo Invoice total";
				return msg;
			} else if (LedesFactura.Dat.timekeeper_id.AsString == "") {
				msg = "Debe completar un valor para el campo Time Keeper Id";
				return msg;
			} else if (LedesFactura.Dat.timekeeper_name.AsString == "") {
				msg = "Debe completar un valor para el campo Time Keeper Name";
				return msg;
			} else if (LedesFactura.Dat.timekeeper_classification.AsString == "") {
				msg = "Debe completar un valor para el campo Time Keeper Classification";
				return msg;
			} else if (LedesFactura.Dat.client_matter_id.AsString == "") {
				msg = "Debe completar un valor para el campo Client Matter Id";
				return msg;
			}
			#endregion Verificar campos requeridos de la cabecera

			#region Verificar detalles
			for (LedesDetalle.GoTop(); ! LedesDetalle.EOF; LedesDetalle.Skip()) {
				monto_total_detalles = monto_total_detalles + LedesDetalle.Dat.line_item_total.AsDecimal;
				#region Verificar campos requeridos del detalle
				if (LedesDetalle.Dat.line_item_number.AsString == "") {
					msg = "Debe completar un valor para el campo Item Number en la linea con ID " + LedesFacturaId.ToString();
					return msg;
				} else if (LedesDetalle.Dat.exp_fee_inv_adj_type.AsString == "") {
					msg = "Debe completar un valor para el campo Inv. Adj. Type en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				} else if (LedesDetalle.Dat.line_item_number_of_units.AsString == "") {
					msg = "Debe completar un valor para el campo Number of Units en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				} else if (LedesDetalle.Dat.line_item_unit_cost.AsString == "") {
					msg = "Debe completar un valor para el campo Unit Cost en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				} else if (LedesDetalle.Dat.line_item_total.AsString == "") {
					msg = "Debe completar un valor para el campo Item Total en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				} else if (LedesDetalle.Dat.line_item_date.AsString == "") {
					msg = "Debe completar un valor para el campo Item Date en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				} else if (LedesDetalle.Dat.line_item_description.AsString == "") {
					msg = "Debe completar un valor para el campo Description en la linea " + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				}
				#endregion Verificar campos requeridos del detalle

				#region Validar inv_adj_type
				// Controlar valores posibles para inv_adj_type (E, F, IF, IE)
				if ( (LedesDetalle.Dat.exp_fee_inv_adj_type.AsString.Trim() != "E") &
					 (LedesDetalle.Dat.exp_fee_inv_adj_type.AsString.Trim() != "F") &
					 (LedesDetalle.Dat.exp_fee_inv_adj_type.AsString.Trim() != "IF") &
					 (LedesDetalle.Dat.exp_fee_inv_adj_type.AsString.Trim() != "IE") ) {
					msg = "El valor del campo Inv. Adj. Type es invalido. " +
						  "Los valores posibles son: E(Expense), F(Fee), IF(Invoice on fees) y IE(Invoice on expenses)." +
						  "Linea #" + LedesDetalle.Dat.line_item_number.AsString;;
					return msg;
				}
				#endregion Validar inv_adj_type

				/*
				#region Validar number_of_units
				// Controlar valor positivo para number_of_units
				if (LedesDetalle.Dat.line_item_number_of_units.AsInt < 0) {
					msg = "El valor del campo Number of Units no puede ser negativo. " +
						"Linea #" + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				}
				#endregion Validar number_of_units
				
				#region Validar unit_cost
				// Controlar valor positivo para unit_cost
				if (LedesDetalle.Dat.line_item_unit_cost.AsDecimal < 0) {
					msg = "El valor del campo Unit Cost no puede ser negativo. " +
							"Linea #" + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				}
				#endregion Validar unit_cost

				#region Validar item_total
				// Verificar valor de line_item_total = 
				//   unit_cost * number_of_units + adjustment_amount
				if ( LedesDetalle.Dat.line_item_total.AsDecimal != 
					 (LedesDetalle.Dat.line_item_unit_cost.AsDecimal *
					  LedesDetalle.Dat.line_item_number_of_units.AsInt +
					  LedesDetalle.Dat.line_item_adjustment_amount.AsDecimal) ) {
					msg = "El valor del campo Item Total es incorrecto. " +
						  "El valor correcto debe ser (unit_cost * number_of_units + adjustment_amount). " +
 						  "Linea #" + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				}
				#endregion Validar item_total
				*/
			
				#region Validar item_date
				/* 
				//Validar valor de item_date. Debe estar comprendido
				//entre billing_start_date y billing_end_date
				if ( (LedesDetalle.Dat.line_item_date.AsDateTime < 
				 	  LedesFactura.Dat.billing_start_date.AsDateTime) |
					 (LedesDetalle.Dat.line_item_date.AsDateTime >
					  LedesFactura.Dat.billing_end_date.AsDateTime) ) {
					msg = "El valor del campo Item Date es incorrecto. " +
						  "Debe estar comprendido entre billing_start_date y billing_end_date. " +
						  "Linea #" + LedesDetalle.Dat.line_item_number.AsString;
					return msg;
				}
				*/
				#endregion Validar item_date
			}
			#endregion Verificar detalles

			#region Validar rango de fechas
			if (LedesFactura.Dat.billing_start_date.AsDateTime >
				LedesFactura.Dat.billing_end_date.AsDateTime) {
				msg = "Rango de fechas invalido (billing start date > billing end date)";
				return msg;
			}
			#endregion Validar rango de fechas

			#region Validar time_keeper_classification
			/* Controlar valores posibles para time_keeper_classification. Los valores posibles son:
			   PT(Partner),AS(Associate),OC(Counsel),LA(Legal assistant),OT(Other timekeeper)*/
			if ( (LedesFactura.Dat.timekeeper_classification.AsString.Trim() != "PT") &
				 (LedesFactura.Dat.timekeeper_classification.AsString.Trim() != "AS") &
				 (LedesFactura.Dat.timekeeper_classification.AsString.Trim() != "OC") &
				 (LedesFactura.Dat.timekeeper_classification.AsString.Trim() != "LA") &
				 (LedesFactura.Dat.timekeeper_classification.AsString.Trim() != "OT") ) {
 				msg = "El valor del campo Time Keeper Classification es invalido. " +
				 	   "Los valores posibles son: PT (Partner), AS(Associate), OC(Counsel), LA(Legal assistant) y OT(Other timekeeper)";
				return msg;
			}
			#endregion Validar time_keeper_classification

			#region Validar invoice_total
			//Controlar que total de la cabecera coincida con total de detalles
			if (LedesFactura.Dat.invoice_total.AsDecimal != monto_total_detalles) {
				msg = "La suma de los Item Total no coincide con Invoice Total";
				return msg;
            }
			#endregion Validar invoice_total

			return msg;
		}
		#endregion ValidarDatos

		#region btnAnular_Click
		protected void btnAnular_Click(object sender, System.EventArgs e)
		{
			if (tbID.Text == "") { return; }

			string errmsg = ControlarEstado(aANULAR);
			if (errmsg != "") {
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
				return;
			}
			CambiarEstado(eANULADO);
		}
		#endregion btnAnular_Click

		#region obtDescripEstado
		protected string obtDescripEstado (string estado)
		{
			string descripEstado;
			switch (estado)
			{
				case eABIERTO : descripEstado = "Abierto"; break;
				case eCONFIRMADO : descripEstado = "Confirmado"; break;
				case eANULADO : descripEstado = "Anulado"; break;
				default : descripEstado = "<Desconocido>"; break;
			}
			return descripEstado;
		}
		#endregion obtDescripEstado

		#region ControlarEstado
		protected string ControlarEstado(char accion)
		{
			string mensaje = "";
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			LedesFacturaId = Convert.ToInt32 (tbID.Text);
			LedesFactura.Adapter.ReadByID( LedesFacturaId );
			switch (LedesFactura.Dat.Estado.AsString)
			{
			case eABIERTO : 
				if (accion == aGENARCHIVO) {
					mensaje = "No se puede generar el archivo. Antes debe confirmar la factura";
				}
				break;
			case eCONFIRMADO :
				if ( (accion == aELIMINARDETALLE) ||
				     (accion == aGRABAR) ||
					 (accion == aCONFIRMAR) ) {				
					mensaje = "No se puede modificar la factura porque ya se encuentra confirmada";
				}				
				break;
			case eANULADO :
				mensaje = "No se puede modificar la factura porque ya se encuentra anulada";
				break;
			default : 
				mensaje = "Estado desconocido";
				break;
			}
			return mensaje;			
		}
		#endregion ControlarEstado

		#region btnGenerarArchivo_Click
		protected void btnGenerarArchivo_Click(object sender, System.EventArgs e)
		{
			if (tbID.Text == "") { return; }
			string errmsg = ControlarEstado(aGENARCHIVO);
			if (errmsg != "") {
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(errmsg));
				return;
			}

			#region Cabecera archivo LEDES
			string cabecera = @"LEDES1998B[]
";
			cabecera = cabecera + "INVOICE_DATE" + @"|" +
                                  "INVOICE_NUMBER" + @"|" +
                                  "CLIENT_ID" + @"|" +
								  "LAW_FIRM_MATTER_ID" + @"|" +
                                  "INVOICE_TOTAL" + @"|" +
                                  "BILLING_START_DATE" + @"|" +
								  "BILLING_END_DATE" + @"|" +
                                  "INVOICE_DESCRIPTION" + @"|" +
                                  "LINE_ITEM_NUMBER" + @"|" +
								  "EXP/FEE/INV_ADJ_TYPE" + @"|" +
                                  "LINE_ITEM_NUMBER_OF_UNITS" + @"|" +
                                  "LINE_ITEM_ADJUSTMENT_AMOUNT" + @"|" +
								  "LINE_ITEM_TOTAL" + @"|" +
                                  "LINE_ITEM_DATE" + @"|" +
                                  "LINE_ITEM_TASK_CODE" + @"|" +
								  "LINE_ITEM_EXPENSE_CODE" + @"|" +
                                  "LINE_ITEM_ACTIVITY_CODE" + @"|" +
                                  "TIMEKEEPER_ID" + @"|" +
								  "LINE_ITEM_DESCRIPTION" + @"|" +
                                  "LAW_FIRM_ID" + @"|" +
                                  "LINE_ITEM_UNIT_COST" + @"|" +
								  "TIMEKEEPER_NAME" + @"|" +
                                  "TIMEKEEPER_CLASSIFICATION" + @"|" +
								  "CLIENT_MATTER_ID" + @"[]
";
			#endregion Cabecera archivo LEDES

			#region Cuerpo archivo LEDES
			string cuerpo = "";
			LedesFacturaId = Convert.ToInt32 (tbID.Text);
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			//Recuperar datos de la cabecera de factura LEDES
			Berke.DG.DBTab.LedesFactura LedesFactura = new Berke.DG.DBTab.LedesFactura();
			LedesFactura.InitAdapter( db );
			LedesFactura.Adapter.ReadByID( LedesFacturaId );

			//Recuperar datos del detalle de factura LEDES
			LedesDetalle.InitAdapter( db );
			LedesDetalle.Dat.LedesFacturaID.Filter = LedesFacturaId;
			LedesDetalle.Adapter.ReadAll();		

			for (LedesDetalle.GoTop(); ! LedesDetalle.EOF; LedesDetalle.Skip()) {				
				cuerpo = cuerpo + LedesFactura.Dat.invoice_date.AsDateTime.ToString("yyyyMMdd") + @"|" +
					LedesFactura.Dat.invoice_number.AsString + @"|" +
					LedesFactura.Dat.client_id.AsString + @"|" +
					LedesFactura.Dat.law_firm_matter_id.AsString + @"|" +
					//LedesFactura.Dat.invoice_total.AsDecimal.ToString("############") + @"|" +
					//String.Format("{0:0.00}", Convert.ToDecimal(LedesFactura.Dat.invoice_total.AsString.Replace(",", "."))) + @"|" +
					//LedesFactura.Dat.invoice_total.AsString.Replace(",", ".") + @"|" +
					String.Format("{0:0.00}", LedesFactura.Dat.invoice_total.AsDecimal).Replace(",", ".") + @"|" +
					LedesFactura.Dat.billing_start_date.AsDateTime.ToString("yyyyMMdd") + @"|" +
					LedesFactura.Dat.billing_end_date.AsDateTime.ToString("yyyyMMdd") + @"|" +
					LedesFactura.Dat.invoice_description.AsString + @"|" +
					LedesDetalle.Dat.line_item_number.AsString + @"|" +
					LedesDetalle.Dat.exp_fee_inv_adj_type.AsString + @"|" +
					//String.Format("{0:0.00}", LedesDetalle.Dat.line_item_number_of_units.AsString.Replace(",", ".")) + @"|" +
					String.Format("{0:0.00}", LedesDetalle.Dat.line_item_number_of_units.AsDecimal).Replace(",", ".") + @"|" +
					//LedesDetalle.Dat.line_item_adjustment_amount.AsDecimal.ToString("############") + @"|" +
					//LedesDetalle.Dat.line_item_adjustment_amount.AsString.Replace(",", ".") + @"|" +
					String.Format("{0:0.00}", LedesDetalle.Dat.line_item_adjustment_amount.AsDecimal).Replace(",", ".") + @"|" +
					//LedesDetalle.Dat.line_item_total.AsDecimal.ToString("############") + @"|" +
					String.Format("{0:0.00}", LedesDetalle.Dat.line_item_total.AsDecimal).Replace(",", ".") + @"|" +
					//String.Format("{0:0.00}", LedesDetalle.Dat.line_item_total.AsString.Replace(",", ".")) + @"|" +
					LedesDetalle.Dat.line_item_date.AsDateTime.ToString("yyyyMMdd") + @"|" +
					LedesDetalle.Dat.line_item_task_code.AsString + @"|" +
					LedesDetalle.Dat.line_item_expense_code.AsString + @"|" +
					LedesDetalle.Dat.line_item_activity_code.AsString + @"|" +
					LedesFactura.Dat.timekeeper_id.AsString + @"|" +
					LedesDetalle.Dat.line_item_description.AsString + @"|" +
					LedesFactura.Dat.law_firm_id.AsString + @"|" +
					//LedesDetalle.Dat.line_item_unit_cost.AsDecimal.ToString("############") + @"|" +
					String.Format("{0:0.00}", LedesDetalle.Dat.line_item_unit_cost.AsDecimal).Replace(",", ".") + @"|" +
					//String.Format("{0:0.00}", LedesDetalle.Dat.line_item_unit_cost.AsString.Replace(",", ".")) + @"|" +
					LedesFactura.Dat.timekeeper_name.AsString + @"|" +
					LedesFactura.Dat.timekeeper_classification.AsString + @"|" +
					LedesFactura.Dat.client_matter_id.AsString + @"[]
";
			}
			#endregion Cuerpo archivo LEDES

			#region Enviar a archivo
			Response.Clear();
			Response.Buffer = true;			
			Response.AddHeader("Content-Disposition", "attachment;filename=" + LedesFactura.Dat.invoice_number.AsString + ".txt");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.Default;
			Response.Write(cabecera + cuerpo); //Llamada al procedimiento HTML
			Response.End();
			#endregion Enviar a archivo
		}
		#endregion btnGenerarArchivo_Click

		#region btnNuevo_Click
		protected void btnNuevo_Click(object sender, System.EventArgs e)
		{
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
			url.redirect( "LedesIngresar.aspx" );
		}
		#endregion btnNuevo_Click
	}
}