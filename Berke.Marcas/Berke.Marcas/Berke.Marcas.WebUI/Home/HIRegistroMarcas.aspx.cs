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
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;


	public partial class HIRegistroMarcas : System.Web.UI.Page
	{

		#region Properties
		private DataTable GridDataTable
		{
			set {value.AcceptChanges(); ViewState["dgw.Table"] = value;}
			get {return ViewState["dgw.Table"] == null ? new DataTable() : (DataTable) ViewState["dgw.Table"];}
		}
		#endregion Properties

		#region Controles del Form
		protected int OrdenTrabajoId;
		protected Berke.Libs.WebBase.Helpers.GridGW dgwMarcas1;
		protected Berke.Libs.WebBase.Helpers.GridGW dgwMarcas2;		
		protected Berke.DG.ViewTab.vExpeMarca vExpeMarca;
		//protected Berke.DG.DBTab.BussinessUnit bussinessUnit;
		protected System.Web.UI.WebControls.Label lbViacomAtencion;
		protected System.Web.UI.WebControls.Label Label5;
		#endregion Controles del Form

		#region Varbiable Globales
		
		#endregion

		#region ConfigurarGrilla
		private void ConfigurarGrilla()
		{
			//					CtrlName           Header			CtrlWidth
			dgwMarcas1.AddCheck( "cbSel1"			,"Sel."				, 20 );			
			dgwMarcas1.AddText( "tbDenominacion"	,"Denominación"		, 270 );		
			dgwMarcas1.AddText( "tbClave"			,"Clave"			, 270 );		
			dgwMarcas1.AddDropDown ( "ddlTipoMarca"	, "Tipo"			, 90 );
			dgwMarcas1.AddText( "tbClase"			,"Clase"			, 110 );		
			dgwMarcas1.AddLabel( "lbIdExpediente",  "IdExpediente"		, 10 );

			dgwMarcas2.AddCheck( "cbSel"			,"Sel."				, 20 );
			dgwMarcas2.AddText( "tbDatosMarcas"			,"Datos Marca"		, 260 );		
			dgwMarcas2.AddText( "tbDescripClase"	,"Descripción de la clase", 260 );		
			dgwMarcas2.AddCheck( "cbLimitada"		,"Limitada"			, 50 );		
			dgwMarcas2.AddText( "tbNroPrioridad"	,"Nro. prioridad"	, 50 );		
			dgwMarcas2.AddText( "tbFechaPrioridad"	,"Fecha"			, 80 );		
			dgwMarcas2.AddDropDown( "ddlPaisPrioridad"	,"País"				, 100 );
			dgwMarcas2.AddText( "tbIdExpediente2"	,"IdExpediente2"	, 10 );
			dgwMarcas2.AddText( "tbDenominacion2"	,"Denominacion2"	, 10 );
			dgwMarcas2.AddText( "tbClave2"			,"Clave2"			, 10 );
			dgwMarcas2.AddText( "tbTipoMarca2"		, "Tipo2"			, 10 );
			dgwMarcas2.AddText( "tbClase2"			,"Clase2"			, 10 );
			dgwMarcas2.AddText( "tbReferenciaMarca"	, "Datos Marca"		, 260 );
			dgwMarcas2.AddText( "tbIdLogotipo"	, "ID Logo"		, 40 );
		}
		#endregion ConfigurarGrilla
	
		#region Asignar Eventos

		private void AsignarEventos() 
		{
			this.eccCliente.LoadRequested        += new ecWebControls.LoadRequestedHandler(this.eccCliente_LoadRequested);
			this.eccCliente.SelectedIndexChanged += new ecWebControls.SelectedIndexChangedHandler(this.eccCliente_SelectedIndexChanged);
			this.eccAgenteLocal.LoadRequested    += new ecWebControls.LoadRequestedHandler(this.eccAgenteLocal_LoadRequested);
			this.cbSustituida.CheckedChanged     += new System.EventHandler(this.cbSustituida_CheckedChanged);
			this.cbDerechoPropio.CheckedChanged  += new System.EventHandler(this.cbDerechoPropio_CheckedChanged);
			this.btnNuevaAtencion.Click          += new System.EventHandler(this.btnNuevaAtencion_Click);
			this.btnBuscar.Click                 += new System.EventHandler(this.btnBuscar_Click);
			this.btnRestaurarClase.Click         += new System.EventHandler(this.btnRestaurarClase_Click);
			this.btnCopiarClase.Click            += new System.EventHandler(this.btnCopiarClase_Click);
			this.btnCopiarPrioridad.Click        += new System.EventHandler(this.btnCopiarPrioridad_Click);
			this.btnAtras.Click                  += new System.EventHandler(this.btnAtras_Click);
			this.btnFinalizar.Click              += new System.EventHandler(this.btnFinalizar_Click);
			this.btnCancelar2.Click              += new System.EventHandler(this.btnCancelar2_Click);
			this.btnAgregarDetalle.Click         += new System.EventHandler(this.btnAgregarDetalle_Click);
			this.btnSiguiente.Click              += new System.EventHandler(this.btnSiguiente_Click);
			this.btnCancelar.Click               += new System.EventHandler(this.btnCancelar_Click);
			this.rblTipoAtencion.SelectedIndexChanged += new System.EventHandler(this.rblTipoAtencion_SelectedIndexChanged);
			this.ddlBussinessUnit.SelectedIndexChanged += new System.EventHandler(this.ddlBussinessUnit_SelectedIndexChanged);
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here


			

			OrdenTrabajoId = 0;
			dgwMarcas1 = new GridGW( GridMarcas1 );
			dgwMarcas2 = new GridGW( GridMarcas2 );			
			this.ConfigurarGrilla();
			if ( ! this.IsPostBack ) {  //Si es el primer load
				MostrarPanel1();
				vExpeMarca = new Berke.DG.ViewTab.vExpeMarca();
				//bussinessUnit = new Berke.DG.DBTab.BussinessUnit();
				tbNuevaAtencion.Enabled = false;
				tbNuevaAtencion.Visible = false;
				if( Request.QueryString.Count < 1 ) {
					dgwMarcas1.Inicializar( 1 );
					cbSustituida.Checked = false;
					cbDerechoPropio.Checked = false;
					lbPoderPropietario.Text = "Poder:";
				} else {
					#region Obtener ID
					if( Request.QueryString.Count >= 1 ) {
						OrdenTrabajoId = Convert.ToInt32(Request.QueryString[0]);
						Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
						db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
						db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
						vExpeMarca.InitAdapter( db );
						vExpeMarca.Dat.OrdenTrabajoID.Filter = OrdenTrabajoId;
						vExpeMarca.Dat.Denominacion.Order = 1;
						vExpeMarca.Dat.ClaseNro.Order = 2;
						vExpeMarca.Adapter.ReadAll();
						CargarCabecera( db );
						CargarMarcas1( false );
						VerSituacionHI( db );
					}
					#endregion Obtener ID
				}
				GridDataTable = vExpeMarca.Table;
			} else {
				vExpeMarca = new Berke.DG.ViewTab.vExpeMarca(GridDataTable);
			}
		}
		#endregion Page_Load

		#region VerSituacion HI

		private void VerSituacionHI (Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit();
			Berke.DG.DBTab.Situacion   sit        = new Berke.DG.DBTab.Situacion();

			tramitesit.InitAdapter( db );
			sit.InitAdapter( db );

			#region Controlar situacion de HI
			tramitesit.ClearFilter();
			


			vExpeMarca.GoTop();
			tramitesit.Adapter.ReadByID( vExpeMarca.Dat.TramiteSitID.AsInt );
			sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );				

			if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
			{
				DesHabilitarPaneles();
				DesHabilitarCamposCabecera();
			}


			#endregion Controlar situacion de HI

		}

		#endregion

		#region CargarCabecera
		private void CargarCabecera(Berke.Libs.Base.Helpers.AccesoDB db)
		{			
			#region OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo OrdenTrabajo = new Berke.DG.DBTab.OrdenTrabajo();
			OrdenTrabajo.InitAdapter( db );
			OrdenTrabajo.Adapter.ReadByID ( OrdenTrabajoId );
			tbNroCorrespondencia.Text = OrdenTrabajo.Dat.CorrNro.AsString;
			tbAnho.Text = OrdenTrabajo.Dat.CorrAnio.AsString;
			cbFacturable.Checked = OrdenTrabajo.Dat.Facturable.AsBoolean;
			eccCliente.SetInitialValue(OrdenTrabajo.Dat.ClienteID.AsInt);
			tbObservacion.Text = OrdenTrabajo.Dat.Obs.AsString;
			tbRefCliente.Text = OrdenTrabajo.Dat.RefCliente.AsString;
			lbCabecera.Text = lbCabecera.Text + " - " + 
							  vExpeMarca.Dat.OtNro.AsString + "/" + vExpeMarca.Dat.OtAnio.AsString;
			rblTipoAtencion.SelectedIndex = OrdenTrabajo.Dat.TipoAtencionxMarca.AsInt;
			#endregion OrdenTrabajo

			#region Cliente
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente();
			cliente.InitAdapter( db );
			cliente.Adapter.ReadByID( OrdenTrabajo.Dat.ClienteID.AsInt );
			lbDireccionCliente.Text = cliente.Dat.Correo.AsString;
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			pais.InitAdapter( db );
			pais.Adapter.ReadByID( cliente.Dat.PaisID.AsInt );
			lbCiudadCliente.Text = pais.Dat.descrip.AsString;
			#endregion Cliente

			#region Cliente Múltiple
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
			Berke.DG.DBTab.ClienteXTramite clienteXtramite = new Berke.DG.DBTab.ClienteXTramite();
			atencion.InitAdapter( db );
			clienteXtramite.InitAdapter( db );
			if (cliente.Dat.Multiple.AsBoolean) {
				clienteXtramite.Dat.ClienteMultipleID.Filter = OrdenTrabajo.Dat.ClienteID.AsInt;
				clienteXtramite.Adapter.ReadAll();
				string str_cliente = OrdenTrabajo.Dat.ClienteID.AsString;
				for (clienteXtramite.GoTop(); !clienteXtramite.EOF; clienteXtramite.Skip()) {
					if (str_cliente == "") {
						str_cliente = clienteXtramite.Dat.ClienteID.AsString;
					} else {
						str_cliente = str_cliente + "," + clienteXtramite.Dat.ClienteID.AsString;
					}
				}
				atencion.Dat.ClienteID.Filter = ObjConvert.GetFilter(str_cliente);
				atencion.Adapter.ReadAll();
			} else {
				atencion.Dat.ClienteID.Filter = OrdenTrabajo.Dat.ClienteID.AsInt;
				atencion.Adapter.ReadAll();
			}
			#endregion Cliente Múltiple

			#region Atencion
			ddlAtencion.DataSource = atencion.Table;
			ddlAtencion.DataTextField = "nombre";
			ddlAtencion.DataValueField = "id";				
			ddlAtencion.DataBind();
			ddlAtencion.Items.Insert (0, string.Empty );
			ddlAtencion.SelectedValue = OrdenTrabajo.Dat.AtencionID.AsString;
			#endregion Atencion

			#region Bussiness Unit
			if (OrdenTrabajo.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
			{
				Berke.DG.DBTab.BussinessUnit bussinessUnit = new Berke.DG.DBTab.BussinessUnit();
				bussinessUnit.InitAdapter( db );
				bussinessUnit.ClearFilter();
				bussinessUnit.Dat.ClienteID.Filter = OrdenTrabajo.Dat.ClienteID.AsInt;
				bussinessUnit.Adapter.ReadAll();	
				
				ddlBussinessUnit.DataSource = bussinessUnit.Table;
				ddlBussinessUnit.DataTextField = "descripcion";
				ddlBussinessUnit.DataValueField = "id";
				ddlBussinessUnit.DataBind();
				ddlBussinessUnit.Items.Insert (0, string.Empty );
				ddlBussinessUnit.SelectedValue = OrdenTrabajo.Dat.IDTipoAtencionxMarca.AsString;

				this.setAtencionVisible(false);

				tbAtencionBU.Text = ddlAtencion.SelectedItem.Text;
			}
			#endregion Bussiness Unit
			
			#region Poder o Propietario
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			expediente.InitAdapter( db );
			expediente.Dat.OrdenTrabajoID.Filter = OrdenTrabajoId;
			expediente.Adapter.ReadAll();
			cbSustituida.Checked = expediente.Dat.Sustituida.AsBoolean;
			// mbaez -> Para manejo de sustituidas.
			if (!expediente.Dat.AgenteLocalID.IsNull) 
			{
				eccAgenteLocal.SetInitialValue(expediente.Dat.AgenteLocalID.AsInt);
				this.setVisibleSustituida(true);
			}
			
			Berke.DG.DBTab.ExpedienteXPoder ExpeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
			ExpeXpoder.InitAdapter( db );
			ExpeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			ExpeXpoder.Adapter.ReadAll();
			if (ExpeXpoder.Dat.PoderID.AsString == "") {
				lbPoderPropietario.Text = "Propietario:";
				/* Si el expediente no tiene poder asociado, significa que es un poder por
				 * derecho propio. En ese caso, se deben recuperar los datos del propietario
				 * de la tabla ExpedienteXPropietario */
				Berke.DG.DBTab.ExpedienteXPropietario ExpeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
				ExpeXpropietario.InitAdapter( db );
				ExpeXpropietario.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				ExpeXpropietario.Adapter.ReadAll();
				Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
				propietario.InitAdapter( db );
				propietario.Adapter.ReadByID( ExpeXpropietario.Dat.PropietarioID.AsInt );
				tbIdPoder.Text = ExpeXpropietario.Dat.PropietarioID.AsString;
				lbDenominacion.Text = propietario.Dat.Nombre.AsString;
				lbDomicilio.Text = propietario.Dat.Direccion.AsString;
				lbActa.Text = "";
				lbInscripcion.Text = "";
				cbDerechoPropio.Checked = true;
			} else {
				lbPoderPropietario.Text = "Poder:";
				tbIdPoder.Text = ExpeXpoder.Dat.PoderID.AsString;
				Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
				poder.InitAdapter( db );
				poder.Adapter.ReadByID(ExpeXpoder.Dat.PoderID.AsInt);
				lbDenominacion.Text = poder.Dat.Denominacion.AsString;
				lbDomicilio.Text = poder.Dat.Domicilio.AsString;
				if ( (poder.Dat.ActaAnio.AsString == "") &
					(poder.Dat.ActaNro.AsString == "") ) {
					lbActa.Text = "";
				} else {
					lbActa.Text = poder.Dat.ActaAnio.AsString + "/" + poder.Dat.ActaNro.AsString;
				}
				if ( (poder.Dat.InscripcionAnio.AsString == "") &
					(poder.Dat.InscripcionNro.AsString == "") ) {
					lbInscripcion.Text = "";
				} else {
					lbInscripcion.Text = poder.Dat.InscripcionAnio.AsString + "/" + poder.Dat.InscripcionNro.AsString;
				}
				cbDerechoPropio.Checked = false;
			}
			#endregion Poder o Propietario

			#region Instrucciones
			Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = new Berke.DG.DBTab.Expediente_Instruccion();
			expedienteinstr.InitAdapter( db );
            expedienteinstr.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
			expedienteinstr.Dat.Fecha.Order = 1;
			expedienteinstr.Adapter.ReadAll();
			tbInstruccionPoder.Text = expedienteinstr.Dat.Obs.AsString;
			expedienteinstr.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
			expedienteinstr.Adapter.ReadAll();
			if (expedienteinstr.RowCount > 0) {
				tbInstruccionContabilidad.Text = expedienteinstr.Dat.Obs.AsString;
			} else {
				tbInstruccionContabilidad.Text = "";
			}
			#endregion Instrucciones
		}
		#endregion CargarCabecera

		#region CargarMarcas1
		private void CargarMarcas1(bool AgregarRegistro)
		{
			#region Obtener cantidad de marcas
			string denominacion_ant = "";
			int cant_marcas = 0;			
			for (vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip()) 
			{
				if (vExpeMarca.Dat.Denominacion.AsString != denominacion_ant) {
					cant_marcas = cant_marcas + 1;
					denominacion_ant = vExpeMarca.Dat.Denominacion.AsString;
				}
			}
			if (AgregarRegistro) {
				cant_marcas = cant_marcas + 1;
			}
			dgwMarcas1.Inicializar( cant_marcas );
			#endregion Obtener cantidad de marcas

			string str_clase = "";
			string str_expediente = "";
			DataGridItem item;
			int posItem = 0;			
			vExpeMarca.GoTop();
			denominacion_ant = vExpeMarca.Dat.Denominacion.AsString;
			string denominacionclave_ant = vExpeMarca.Dat.DenominacionClave.AsString;
			string marcatipo_ant = vExpeMarca.Dat.MarcaTipo.AsString;
			for (vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip()) {
				if (vExpeMarca.Dat.Denominacion.AsString != denominacion_ant) {					
					item = GridMarcas1.Items[posItem];
					dgwMarcas1.Set(item, "tbDenominacion", denominacion_ant);
					dgwMarcas1.Set(item, "tbClave", denominacionclave_ant);		
                    dgwMarcas1.Set(item, "ddlTipoMarca", marcatipo_ant);
					dgwMarcas1.Set(item, "tbClase", str_clase);
					dgwMarcas1.Set(item, "lbIdExpediente", str_expediente);
					str_clase = "";
					str_expediente = "";
					denominacion_ant = vExpeMarca.Dat.Denominacion.AsString;
					denominacionclave_ant = vExpeMarca.Dat.DenominacionClave.AsString;
					marcatipo_ant = vExpeMarca.Dat.MarcaTipo.AsString;
					posItem = posItem + 1;
				}
				if (str_clase == "") {
					str_clase = vExpeMarca.Dat.ClaseNro.AsString;
					str_expediente = vExpeMarca.Dat.ExpedienteID.AsString;
				} else {
					str_clase = str_clase + "," + vExpeMarca.Dat.ClaseNro.AsString;
					str_expediente = str_expediente + "," + vExpeMarca.Dat.ExpedienteID.AsString;
				}
			}
			item = GridMarcas1.Items[posItem];
			dgwMarcas1.Set(item, "tbDenominacion", denominacion_ant);
			dgwMarcas1.Set(item, "tbClave", denominacionclave_ant);
			dgwMarcas1.Set(item, "ddlTipoMarca", marcatipo_ant);
			dgwMarcas1.Set(item, "tbClase", str_clase);
			dgwMarcas1.Set(item, "lbIdExpediente", str_expediente);
		}
		#endregion CargarMarcas1

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.AsignarEventos();
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

		#region eccCliente

		private void eccCliente_LoadRequested(ecWebControls.ecCombo combo, System.EventArgs e)
		{
		
			#region Asignar Parametros

			Berke.DG.ViewTab.ParamTab inTB	=   new Berke.DG.ViewTab.ParamTab();

			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )
			{
				inTB.Dat.Entero.Value = Convert.ToInt32( combo.Text );
			}
			else
			{
				inTB.Dat.Alfa.Value = combo.Text;			
			}
			inTB.PostNewRow();

			#endregion Asignar Parametros
		
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.Cliente.ReadForSelect( inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );	
			
            MostrarDatosCliente(outTB.Dat.ID.AsInt);
		}


		private void eccAgenteLocal_LoadRequested(ecWebControls.ecCombo combo, System.EventArgs e)
		{			
			this.initCbSustituida(combo);
		}
		private void initCbSustituida(ecWebControls.ecCombo combo)
		{
			#region Asignar Parametros

			Berke.DG.ViewTab.ParamTab inTB	=   new Berke.DG.ViewTab.ParamTab();

			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )
			{
				inTB.Dat.Entero.Value = Convert.ToInt32( combo.Text );
			}
			else
			{
				inTB.Dat.Alfa.Value = combo.Text;			
			}
			inTB.PostNewRow();

			#endregion Asignar Parametros
		
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.AgenteLocal.ReadForSelect( inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );	
					
		}

		private void eccCliente_SelectedIndexChanged(ecWebControls.ecCombo combo, System.EventArgs e)
		{
			MostrarDatosCliente(Convert.ToInt32(eccCliente.SelectedValue)); 
		}

		private void MostrarDatosCliente(int clienteID)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
			atencion.InitAdapter( db );
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente();
			cliente.InitAdapter( db );
			Berke.DG.DBTab.ClienteXTramite clienteXtramite = new Berke.DG.DBTab.ClienteXTramite();
			clienteXtramite.InitAdapter( db );
			Berke.DG.DBTab.BussinessUnit bussinessUnit = new Berke.DG.DBTab.BussinessUnit();
			bussinessUnit.InitAdapter( db );

			#region Obtener datos del Cliente
			cliente.Adapter.ReadByID( clienteID );
			lbDireccionCliente.Text = cliente.Dat.Correo.AsString;			
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			pais.InitAdapter( db );
			pais.Adapter.ReadByID( cliente.Dat.PaisID.AsInt );
			lbCiudadCliente.Text = pais.Dat.descrip.AsString;
			#endregion Obtener datos del Cliente

			#region Cliente Múltiple
			if (cliente.Dat.Multiple.AsBoolean) {
				clienteXtramite.Dat.ClienteMultipleID.Filter = clienteID;
				clienteXtramite.Adapter.ReadAll();
				string str_cliente = clienteID.ToString();
				for (clienteXtramite.GoTop(); !clienteXtramite.EOF; clienteXtramite.Skip()) {
					if (str_cliente == "") {
						str_cliente = clienteXtramite.Dat.ClienteID.AsString;
					} else {
						str_cliente = str_cliente + "," + clienteXtramite.Dat.ClienteID.AsString;
					}
				}
				atencion.Dat.ClienteID.Filter = ObjConvert.GetFilter(str_cliente);
				atencion.Adapter.ReadAll();
			} else {
				atencion.Dat.ClienteID.Filter = clienteID;
				atencion.Adapter.ReadAll();
			}
			#endregion Cliente Múltiple

			#region Atencion
			ddlAtencion.DataSource = atencion.Table;
			ddlAtencion.DataTextField = "nombre";
			ddlAtencion.DataValueField = "id";
            ddlAtencion.DataBind();
			ddlAtencion.Items.Insert (0, string.Empty );
			#endregion Atencion

			#region Bussiness Unit
			/*if (rblTipoAtencion.SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT) //Por Bussiness Unit
			{*/
				bussinessUnit.ClearFilter();
				bussinessUnit.Dat.ClienteID.Filter = clienteID;
				bussinessUnit.Adapter.ReadAll();	
				
				ddlBussinessUnit.DataSource = bussinessUnit.Table;
				ddlBussinessUnit.DataTextField = "descripcion";
				ddlBussinessUnit.DataValueField = "id";
				ddlBussinessUnit.DataBind();
				ddlBussinessUnit.Items.Insert (0, string.Empty );
			//}
			#endregion Bussiness Unit
		}


		//private void Cargar
		#endregion eccCliente

		#region btnSiguiente_Click
		private void btnSiguiente_Click(object sender, System.EventArgs e)
		{
			validateForm();
			SyncDataTableMarcas1();
			MostrarPanel2();
			CargarMarcas2();			
		}
		#endregion btnSiguiente_Click

		#region btnAtras_Click
		private void btnAtras_Click(object sender, System.EventArgs e)
		{
			SyncDataTableMarcas2();
			MostrarPanel1();
			CargarMarcas1( false );
		}
		#endregion btnAtras_Click

		#region MostrarPanel1
		protected void MostrarPanel1()
		{
			Panel1.Style.Remove("TOP");
			Panel1.Style.Add("TOP", "450px");
			Panel1.Visible = true;
			Panel2.Visible = false;							
		}
		#endregion MostrarPanel1

		#region DesHabilitar Paneles
		protected void DesHabilitarPaneles()
		{
			SyncDataTableMarcas1();
			MostrarPanel2();
			CargarMarcas2();	

			//GridMarcas2.Enabled = false;
			
			foreach( DataGridItem item in GridMarcas2.Items ) 
			{
				dgwMarcas2.SetReadOnly(item,"tbDatosMarcas",true);
				dgwMarcas2.SetReadOnly(item,"tbDescripClase",true);
				dgwMarcas2.SetReadOnly(item,"cbSel",true);
				dgwMarcas2.SetReadOnly(item,"tbReferenciaMarca",true);
				dgwMarcas2.SetReadOnly(item,"cbLimitada",true);
				dgwMarcas2.SetReadOnly(item,"tbNroPrioridad",true);
				dgwMarcas2.SetReadOnly(item,"tbFechaPrioridad",true);
				dgwMarcas2.SetReadOnly(item,"ddlPaisPrioridad",true);

				((CheckBox)item.FindControl("cbSel")).Enabled	= false;
				((CheckBox)item.FindControl("cbLimitada")).Enabled	= false;
				((DropDownList)item.FindControl("ddlPaisPrioridad")).Enabled = false;
				
			} 
			

			btnRestaurarClase.Enabled  = false;
			btnCopiarClase.Enabled     = false;
			btnCopiarPrioridad.Enabled = false;

		}
		#endregion 

		#region DesHabilitar Paneles
		protected void DesHabilitarCamposCabecera()
		{
			tbIdPoder.Enabled      = false;
			cbDerechoPropio.Enabled= false;
		}
		#endregion 

		#region MostrarPanel2
		protected void MostrarPanel2()
		{
			Panel2.Style.Remove("TOP");
			Panel2.Style.Add("TOP", "450px");
			Panel2.Visible = true;
			btnAtras.Visible = false;
			btnAtras.Enabled = false;
			Panel1.Visible = false;		
		}
		#endregion MostrarPanel2

		#region btnAgregarDetalle_Click
		private void btnAgregarDetalle_Click(object sender, System.EventArgs e)
		{
			SyncDataTableMarcas1();
			CargarMarcas1( true );
		}
		#endregion btnAgregarDetalle_Click

		#region SyncDataTableMarcas1
		//Sincronizar el DataTable con los cambios de la grilla Marcas1
		private void SyncDataTableMarcas1() {
			#region EliminarFilasEnMemoria
			string [] aClases;
			string [] aExpedientes;
			for (vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip()) {
				if (vExpeMarca.Dat.ExpedienteID.AsString == "") {					
					vExpeMarca.Delete();
				}
			}
			#endregion EliminarFilasEnMemoria

			int i, j;
			foreach( DataGridItem item in GridMarcas1.Items ) {
				aClases = dgwMarcas1.GetText(item, "tbClase").Trim().Split(((String) ",").ToCharArray());
				aExpedientes = dgwMarcas1.GetText(item, "lbIdExpediente").Trim().Split(((String) ",").ToCharArray());

				if (dgwMarcas1.GetText(item, "lbIdExpediente").Trim() != "") {
					#region Actualizar lista de expedientes
					for (i = 0; i < aExpedientes.Length; i++) {
						//Posicionarse sobre el expediente a modificar
						for (vExpeMarca.GoTop(); !vExpeMarca.EOF; vExpeMarca.Skip()) {
							if (vExpeMarca.Dat.ExpedienteID.AsString == aExpedientes[i]) {
								break;
							}
						}						

						//Buscar clase en array de clases
						bool modificar_registro = false;
						for (j = 0; j < aClases.Length; j++) {
							if (vExpeMarca.Dat.ClaseNro.AsString == aClases[j]) {
								modificar_registro = true;
								break;
							}
						}

						if (modificar_registro) {
							vExpeMarca.Edit();
							vExpeMarca.Dat.MarcaTipo.Value = dgwMarcas1.GetText(item, "ddlTipoMarca").Trim();
                            vExpeMarca.Dat.Denominacion.Value = dgwMarcas1.GetText(item, "tbDenominacion").Trim();
							vExpeMarca.Dat.DenominacionClave.Value = Berke.Libs.Boletin.Libs.Utils.LimpiarDenominacionClave(vExpeMarca.Dat.MarcaTipo.Value.ToString(),
                                                                                                                             vExpeMarca.Dat.Denominacion.Value.ToString());
                            //vExpeMarca.Dat.DenominacionClave.Value = dgwMarcas1.GetText(item, "tbClave").Trim();
                            vExpeMarca.Dat.ClaseNro.Value = aClases[j];
							vExpeMarca.PostEdit();
						} else {
							vExpeMarca.Delete();
						}
					}
					#endregion Actualizar lista de expedientes

					#region Insertar expediente para clases nuevas
					for (j = 0; j < aClases.Length; j++) {
						for (vExpeMarca.GoTop(); !vExpeMarca.EOF; vExpeMarca.Skip()) {
							if ( (vExpeMarca.Dat.ClaseNro.AsString == aClases[j]) &
							     (vExpeMarca.Dat.ExpedienteID.AsString != "") ) {
								break;
							}
						}

						if (vExpeMarca.EOF == true) {
							vExpeMarca.NewRow();
							//vExpeMarca.Dat.DenominacionClave.Value = dgwMarcas1.GetText(item, "tbClave").Trim();
							vExpeMarca.Dat.MarcaTipo.Value = dgwMarcas1.GetText(item, "ddlTipoMarca").Trim();
                            vExpeMarca.Dat.Denominacion.Value = dgwMarcas1.GetText(item, "tbDenominacion").Trim();
                            vExpeMarca.Dat.DenominacionClave.Value = Berke.Libs.Boletin.Libs.Utils.LimpiarDenominacionClave(vExpeMarca.Dat.MarcaTipo.Value.ToString(),
                                                                                                                             vExpeMarca.Dat.Denominacion.Value.ToString());
							vExpeMarca.Dat.ClaseNro.Value = aClases[j];
							vExpeMarca.PostNewRow();
						}
					}
					#endregion Insertar expediente para clases nuevas
				} else {
					if (dgwMarcas1.GetText(item, "tbDenominacion" ).Trim() != "") {
						#region Insertar expedientes nuevos
						for (i = 0; i < aClases.Length; i++) {
							vExpeMarca.NewRow();
							//vExpeMarca.Dat.DenominacionClave.Value = dgwMarcas1.GetText(item, "tbClave").Trim();
							vExpeMarca.Dat.MarcaTipo.Value = dgwMarcas1.GetText(item, "ddlTipoMarca").Trim();
                            vExpeMarca.Dat.Denominacion.Value = dgwMarcas1.GetText(item, "tbDenominacion").Trim();
							vExpeMarca.Dat.DenominacionClave.Value = Berke.Libs.Boletin.Libs.Utils.LimpiarDenominacionClave(vExpeMarca.Dat.MarcaTipo.Value.ToString(),
                                                                                                                             vExpeMarca.Dat.Denominacion.Value.ToString());
							vExpeMarca.Dat.ClaseNro.Value = aClases[i];
							vExpeMarca.PostNewRow();
						}
						#endregion Insertar expedientes nuevos
					}
				}				
			}
			GridDataTable = vExpeMarca.Table;
		}		
		#endregion SyncDataTableMarcas1

		#region CargarMarcas2
		private void CargarMarcas2()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			dgwMarcas2.Inicializar( vExpeMarca.RowCount );
			DataGridItem item;
			int posItem = 0;	
			string datos_marca = "";
			#region Declaracion de objetos de acceso a datos
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase();
			clase.InitAdapter( db );
			Berke.DG.DBTab.MarcaTipo marcatipo = new Berke.DG.DBTab.MarcaTipo();
			marcatipo.InitAdapter( db );
			Berke.DG.DBTab.Expediente_Documento expedientedoc = new Berke.DG.DBTab.Expediente_Documento();
			expedientedoc.InitAdapter( db );
			Berke.DG.DBTab.Documento documento = new Berke.DG.DBTab.Documento();
			documento.InitAdapter( db );
			Berke.DG.DBTab.DocumentoCampo documentocampo = new Berke.DG.DBTab.DocumentoCampo();
			documentocampo.InitAdapter( db );
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			pais.InitAdapter( db );
			pais.Dat.descrip.Order = 1;
			pais.Adapter.ReadAll();
			#endregion Declaracion de objetos de acceso a datos

			for (vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip()) 
			{
				item = GridMarcas2.Items[posItem];			
				DropDownList dpais = (DropDownList) item.FindControl("ddlPaisPrioridad");
				dpais.DataSource = pais.Table;
				dpais.DataTextField = "descrip";
				dpais.DataValueField = "idpais";				
				dpais.DataBind();
				dpais.Items.Insert (0, string.Empty );

				#region Obtener datos de marca-clase
				marcatipo.Dat.Abrev.Filter = vExpeMarca.Dat.MarcaTipo.AsString;
				marcatipo.Adapter.ReadAll();
				datos_marca = "Denominación: " + vExpeMarca.Dat.Denominacion.AsString + Convert.ToChar(13) +
							  "Clave: " + vExpeMarca.Dat.DenominacionClave.AsString + Convert.ToChar(13) +
							  "Tipo: " + marcatipo.Dat.Descrip.AsString + Convert.ToChar(13) +
							  "Clase: " + vExpeMarca.Dat.ClaseNro.AsString;
				dgwMarcas2.Set(item, "tbDatosMarcas", datos_marca);
				if (vExpeMarca.Dat.ClaseDescripEsp.AsString == "") {
					clase.Dat.Nro.Filter = vExpeMarca.Dat.ClaseNro.AsInt;
					clase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
					clase.Adapter.ReadAll();
					dgwMarcas2.Set(item, "tbDescripClase", clase.Dat.Descrip.AsString);
				} else {
					dgwMarcas2.Set(item, "tbDescripClase", vExpeMarca.Dat.ClaseDescripEsp.AsString);
				}
				if (vExpeMarca.Dat.Limitada.AsString == "") {
					dgwMarcas2.Set(item, "cbLimitada", "false");
				} else {
					dgwMarcas2.Set(item, "cbLimitada", vExpeMarca.Dat.Limitada.AsString);
				}
				dgwMarcas2.Set(item, "tbIdExpediente2", vExpeMarca.Dat.ExpedienteID.AsString);
				dgwMarcas2.Set(item, "tbDenominacion2", vExpeMarca.Dat.Denominacion.AsString);
				dgwMarcas2.Set(item, "tbClave2", vExpeMarca.Dat.DenominacionClave.AsString);
				dgwMarcas2.Set(item, "tbTipoMarca2", vExpeMarca.Dat.MarcaTipo.AsString);
				dgwMarcas2.Set(item, "tbClase2", vExpeMarca.Dat.ClaseNro.AsString);
				dgwMarcas2.Set(item, "tbReferenciaMarca", vExpeMarca.Dat.Label.AsString);				
				dgwMarcas2.Set(item, "tbIdLogotipo", vExpeMarca.Dat.LogotipoID.AsString);				
				#endregion Obtener datos de marca-clase

				#region Obtener prioridad
				if (vExpeMarca.Dat.ExpedienteID.AsString != "") {
					expedientedoc.Dat.ExpedienteID.Filter = vExpeMarca.Dat.ExpedienteID.AsInt;
					expedientedoc.Adapter.ReadAll();
					for (expedientedoc.GoTop(); !expedientedoc.EOF; expedientedoc.Skip()) {
						documento.Adapter.ReadByID( expedientedoc.Dat.DocumentoID.AsInt );
						if (documento.Dat.DocumentoTipoID.AsInt == (int) GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD) {
							documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
							documentocampo.Dat.Campo.Filter = "Nro";
							documentocampo.Adapter.ReadAll();
							dgwMarcas2.Set(item, "tbNroPrioridad", documentocampo.Dat.Valor.AsString);

							documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
							documentocampo.Dat.Campo.Filter = "Fecha";
							documentocampo.Adapter.ReadAll();
							dgwMarcas2.Set(item, "tbFechaPrioridad", documentocampo.Dat.Valor.AsString);

							documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
							documentocampo.Dat.Campo.Filter = "Pais";
							documentocampo.Adapter.ReadAll();
							dgwMarcas2.Set(item, "ddlPaisPrioridad", documentocampo.Dat.Valor.AsString);
						}
					}										
				}
				#endregion Obtener prioridad
				posItem = posItem + 1;
			}
		}
		#endregion CargarMarcas2

		#region SyncDataTableMarcas2
		//Sincronizar el DataTable con los cambios de la grilla Marcas2
		private void SyncDataTableMarcas2() 
		{
			#region EliminarFilasEnMemoria
			for (vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip()) {
				if (vExpeMarca.Dat.ExpedienteID.AsString == "") {
					vExpeMarca.Delete();
				}
			}
			#endregion EliminarFilasEnMemoria

			foreach( DataGridItem item in GridMarcas2.Items ) {
				if (dgwMarcas2.GetText(item, "tbIdExpediente2").Trim() != "") {
					for (vExpeMarca.GoTop(); !vExpeMarca.EOF; vExpeMarca.Skip()) {
						if (vExpeMarca.Dat.ExpedienteID.AsString == dgwMarcas2.GetText(item, "tbIdExpediente2").Trim()) {
							break;
						}
					}
					vExpeMarca.Edit();
					vExpeMarca.Dat.ClaseDescripEsp.Value = dgwMarcas2.GetText(item, "tbDescripClase").Trim();
					vExpeMarca.Dat.Limitada.Value = dgwMarcas2.GetText(item, "cbLimitada").Trim();
					vExpeMarca.Dat.Sustituida.Value = cbSustituida.Checked;
					vExpeMarca.Dat.AgenteLocalID.Value = eccAgenteLocal.SelectedValue;
					//Utilizamos el campo situaciondescrip para enviar los datos de la prioridad al Action
					if ( (dgwMarcas2.GetText(item, "tbNroPrioridad").Trim() != "") |
						(dgwMarcas2.GetText(item, "tbFechaPrioridad").Trim() != "") |
						(dgwMarcas2.GetText(item, "ddlPaisPrioridad").Trim() != "") )
					{
						vExpeMarca.Dat.SituacionDecrip.Value = dgwMarcas2.GetText(item, "tbNroPrioridad").Trim() + "~" +
							dgwMarcas2.GetText(item, "tbFechaPrioridad").Trim() + "~" +
							dgwMarcas2.GetText(item, "ddlPaisPrioridad").Trim();
					} else {
						vExpeMarca.Dat.SituacionDecrip.Value = "";
					}
					//Utilizamos los campos tramiteabrev y tramitedescrip para enviar instrucciones al Action
					vExpeMarca.Dat.TramiteAbrev.Value = tbInstruccionPoder.Text;
					vExpeMarca.Dat.TramiteDescrip.Value = tbInstruccionContabilidad.Text;
					vExpeMarca.Dat.Label.Value = dgwMarcas2.GetText(item, "tbReferenciaMarca").Trim();
					vExpeMarca.Dat.LogotipoID.Value = dgwMarcas2.GetText(item, "tbIdLogotipo").Trim();
					vExpeMarca.PostEdit();
				} else {
					vExpeMarca.NewRow();
					vExpeMarca.Dat.Denominacion.Value = dgwMarcas2.GetText(item, "tbDenominacion2" ).Trim();
					vExpeMarca.Dat.DenominacionClave.Value = dgwMarcas2.GetText(item, "tbClave2").Trim();
					vExpeMarca.Dat.MarcaTipo.Value = dgwMarcas2.GetText(item, "tbTipoMarca2").Trim();
					vExpeMarca.Dat.ClaseNro.Value = dgwMarcas2.GetText(item, "tbClase2").Trim();
					vExpeMarca.Dat.ClaseDescripEsp.Value = dgwMarcas2.GetText(item, "tbDescripClase").Trim();
					vExpeMarca.Dat.Limitada.Value = dgwMarcas2.GetText(item, "cbLimitada").Trim();
					vExpeMarca.Dat.Sustituida.Value = cbSustituida.Checked;
					vExpeMarca.Dat.AgenteLocalID.Value = eccAgenteLocal.SelectedValue;

					#region Atencion por marca
					if (rblTipoAtencion.SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA) //Por marca
					{
						vExpeMarca.Dat.TipoAtencionxMarca.Value = GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA;
						vExpeMarca.Dat.IDTipoAtencionxMarca.Value = ddlAtencion.SelectedValue;
					}
					else if (rblTipoAtencion.SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT) //Por Bussiness Unit
					{
						vExpeMarca.Dat.TipoAtencionxMarca.Value = GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT;
						vExpeMarca.Dat.IDTipoAtencionxMarca.Value = ddlBussinessUnit.SelectedValue; //este valor no será insertado
					}
					else //Atencion por tramite (normal)
					{
						vExpeMarca.Dat.TipoAtencionxMarca.Value = System.DBNull.Value;
						vExpeMarca.Dat.IDTipoAtencionxMarca.Value = System.DBNull.Value;
					}
					#endregion Atencion por marca


					//Utilizamos el campo situaciondescrip para enviar los datos de la prioridad al Action
					if ( (dgwMarcas2.GetText(item, "tbNroPrioridad").Trim() != "") |
						(dgwMarcas2.GetText(item, "tbFechaPrioridad").Trim() != "") |
						(dgwMarcas2.GetText(item, "ddlPaisPrioridad").Trim() != "") ) {
						vExpeMarca.Dat.SituacionDecrip.Value = dgwMarcas2.GetText(item, "tbNroPrioridad").Trim() + "~" +
							dgwMarcas2.GetText(item, "tbFechaPrioridad").Trim() + "~" +
							dgwMarcas2.GetText(item, "ddlPaisPrioridad").Trim();
					} else {
						vExpeMarca.Dat.SituacionDecrip.Value = "";
					}
					//Utilizamos los campos tramiteabrev y tramitedescrip para enviar instrucciones al Action
					vExpeMarca.Dat.TramiteAbrev.Value = tbInstruccionPoder.Text;
					vExpeMarca.Dat.TramiteDescrip.Value = tbInstruccionContabilidad.Text;
					vExpeMarca.Dat.Label.Value = dgwMarcas2.GetText(item, "tbReferenciaMarca").Trim();
					vExpeMarca.Dat.LogotipoID.Value = dgwMarcas2.GetText(item, "tbIdLogotipo").Trim();
					vExpeMarca.PostNewRow();
				}
			}
			GridDataTable = vExpeMarca.Table;
		}
		#endregion SyncDataTableMarcas2

		#region btnFinalizar_Click
		private void btnFinalizar_Click(object sender, System.EventArgs e)
		{
			SyncDataTableMarcas2();
			if( Request.QueryString.Count >= 1 ) {
				OrdenTrabajoId = Convert.ToInt32(Request.QueryString[0]);
			} else {
				OrdenTrabajoId = 0;
			}
			if(vExpeMarca.Dat.Sustituida.AsBoolean && eccAgenteLocal.SelectedValue== "")
			{
				lblInfoSustituidas.Text = "No se pudo guardar. Las marcas sustituidas requieren del Agente Local.";				
				return;
			}
			else 
			{
				lblInfoSustituidas.Text ="Esta información es requerida para marcas sustituidas";
			}
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			#region Controlar cliente múltiple	
			if ( (eccCliente.SelectedValue != "") &
			     (tbNuevaAtencion.Enabled) ){
				Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
				cliente.Adapter.ReadByID(eccCliente.SelectedValue);
				if (cliente.Dat.Multiple.AsBoolean) {
					this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se puede agregar atención para cliente múltiple"));
					return;
				}
			}
			#endregion Controlar cliente múltiple

			Berke.DG.RegistroDG inDG = new Berke.DG.RegistroDG();

			#region OrdenTrabajo
			Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
			Func = UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo;			
			ot.InitAdapter( db );
			if (OrdenTrabajoId == 0) { 
				ot.NewRow(); 
			} else { 
				ot.Adapter.ReadByID( OrdenTrabajoId );
				ot.Edit();
			}			
			ot.Dat.Facturable	.Value = cbFacturable.Checked;   //bit Oblig.
			ot.Dat.Obs			.Value = tbObservacion.Text;   //nvarchar
			ot.Dat.ClienteID	.Value = eccCliente.SelectedValue;
			ot.Dat.FuncionarioID.Value = Func.Dat.ID.AsInt;
			ot.Dat.CorrNro		.Value = tbNroCorrespondencia.Text;
			ot.Dat.CorrAnio		.Value = tbAnho.Text;
			ot.Dat.AtencionID	.Value = ddlAtencion.SelectedValue;
			ot.Dat.RefCliente	.Value = tbRefCliente.Text;
			
			if (rblTipoAtencion.SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA)
			{
				ot.Dat.TipoAtencionxMarca.Value = GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA;
				ot.Dat.IDTipoAtencionxMarca.Value = ddlAtencion.SelectedValue;
			}
			else if (rblTipoAtencion.SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
			{
				ot.Dat.TipoAtencionxMarca.Value = GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT;
				ot.Dat.IDTipoAtencionxMarca.Value = ddlBussinessUnit.SelectedValue;
			}	
			else
			{	
				ot.Dat.TipoAtencionxMarca.Value = System.DBNull.Value;
				ot.Dat.IDTipoAtencionxMarca.Value = System.DBNull.Value;
			}
			
			if (OrdenTrabajoId == 0) {
				ot.PostNewRow();
			} else {
				ot.PostEdit();
			}
			#endregion 

			#region Nueva Atencion
			if (tbNuevaAtencion.Enabled) {
				Berke.DG.DBTab.Atencion at = inDG.Atencion;
				at.NewRow();
				at.Dat.Nombre.Value = tbNuevaAtencion.Text;
				at.PostNewRow();
			}            
			#endregion Nueva Atencion

			#region Poderes y Propietarios
			Berke.DG.DBTab.Poder poder = inDG.Poder;
			poder.InitAdapter( db );
			Berke.DG.DBTab.Propietario propietario = inDG.Propietario;
			propietario.InitAdapter( db );
			if (cbDerechoPropio.Checked == true) {
				propietario.Adapter.ReadByID ( tbIdPoder.Text );
			} else {
				poder.Adapter.ReadByID ( tbIdPoder.Text );
			}
			#endregion Poderes y Propietarios

			inDG.vExpeMarca = new Berke.DG.ViewTab.vExpeMarca(vExpeMarca.Table);
			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.Registro.Upsert( inDG );
			if (outTB.Dat.Logico.AsBoolean) {
				Response.Redirect("OrdenTrabajoDetalle.aspx?otid=" + outTB.Dat.Entero.AsString +
								  "&page=3");
			}
		}
		#endregion btnFinalizar_Click

		#region ValidarPropietario
		private bool ValidarPropietario()
		{
			bool ok = true;
			try 
			{
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

				if (cbDerechoPropio.Checked) 
				{
					#region BuscarPropietario
					Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
					propietario.InitAdapter( db );
					propietario.Adapter.ReadByID( Convert.ToInt32(tbIdPoder.Text) );
					lbActa.Text = "";
					lbInscripcion.Text = "";
					if (propietario.RowCount <= 0) 
					{
						lbDenominacion.Text = "";
						lbDomicilio.Text = "";
						throw new Exception("No existe el propietario ingresado");
					} 
					else 
					{
						lbDenominacion.Text = propietario.Dat.Nombre.AsString;
						lbDomicilio.Text = propietario.Dat.Direccion.AsString;
					}
					#endregion BuscarPropietario
				} 
				else 
				{
					#region BuscarPoder
					Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
					poder.InitAdapter( db );
					poder.Adapter.ReadByID(Convert.ToInt32(tbIdPoder.Text));
					if (poder.RowCount <= 0) 
					{
						lbDenominacion.Text = "";
						lbDomicilio.Text = "";
						lbActa.Text = "";
						lbInscripcion.Text = "";
						throw new Exception("No existe el poder ingreado");
					} 
					else 
					{
						lbDenominacion.Text = poder.Dat.Denominacion.AsString;
						lbDomicilio.Text = poder.Dat.Domicilio.AsString;
						if ( (poder.Dat.ActaAnio.AsString == "") &
							(poder.Dat.ActaNro.AsString == "") ) 
						{
							lbActa.Text = "";
						} 
						else  
						{
							lbActa.Text = poder.Dat.ActaAnio.AsString + "/" + poder.Dat.ActaNro.AsString;
						}
						if ( (poder.Dat.InscripcionAnio.AsString == "") &
							(poder.Dat.InscripcionNro.AsString == "") ) 
						{
							lbInscripcion.Text = "";
						} 
						else 
						{
							lbInscripcion.Text = poder.Dat.InscripcionAnio.AsString + "/" + poder.Dat.InscripcionNro.AsString;
						}					
					}
					#endregion BuscarPoder
				}
			} 
			catch( Exception e ) 
			{
				ok = false;
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se pudo recuperar los datos del propietario/poder: " + e.Message));
			}
			return ok;
		}
		#endregion ValidarPropietario

		#region cbDerechoPropio_CheckedChanged
		private void cbDerechoPropio_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbDerechoPropio.Checked) 
			{
				lbPoderPropietario.Text = "Propietario:";
			} else {
				lbPoderPropietario.Text = "Poder:";
			}
		}
		#endregion cbDerechoPropio_CheckedChanged

		#region btnBuscar_Click
		private void btnBuscar_Click(object sender, System.EventArgs e)
		{
			ValidarPropietario();
		}
		#endregion btnBuscar_Click

		#region Copiar Descripcion de Clase
		private void btnCopiarClase_Click(object sender, System.EventArgs e)
		{

			#region SyncDataTableMarcas2
			    SyncDataTableMarcas2();
			#endregion 
			
			ArrayList aClases = new ArrayList(); 
			TClase c = new TClase();			

			aClases.Clear();

			#region Verificar Marca

		
			string clase = "";

			foreach( DataGridItem item in GridMarcas2.Items ) 
			{
				/*Registros marcados */
				if ( (dgwMarcas2.GetText( item, "cbSel" ) == "Si"))  
				{
					/*Si se marcan varios registros se consideran SOLO las clases diferentes*/
					if	( clase.ToString() != dgwMarcas2.GetText( item, "tbClase2").ToString() ) 
					{
						clase        = dgwMarcas2.GetText( item, "tbClase2").ToString();
						c.clase      = dgwMarcas2.GetText( item, "tbClase2").ToString() ;
						c.descripcion= dgwMarcas2.GetText( item, "tbDescripClase").ToString();
						aClases.Add(c);
					}
					
				}
			}


			#endregion Verificar Marca
  

			#region Copiar la clase
			/* Si se marcaron registros */
			if ( aClases.Count > 0 )
			{
				for (int i=0; i < aClases.Count; i++ )
				{
					c = (TClase)aClases[i];

					foreach( DataGridItem item in GridMarcas2.Items ) 
					{
						if ( dgwMarcas2.GetText( item, "tbClase2" ).ToString() == c.clase )
						{
							dgwMarcas2.Set(item,"tbDescripClase",c.descripcion);
						}
					} //Grilla
			
				} //Array de Clases
			}
			#endregion


			//this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(c.descripcion));
			
			
		}
		#endregion Copiar Descripcion de Clase

		#region restaurar Clase
		private void btnRestaurarClase_Click(object sender, System.EventArgs e)
		{
			#region SyncDataTableMarcas2
			SyncDataTableMarcas2();
			#endregion 

			#region Acceso a Datos
			  Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			  db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			  db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			  Berke.DG.DBTab.Clase w_clase = new Berke.DG.DBTab.Clase();
              w_clase.InitAdapter( db );
			#endregion 

			foreach( DataGridItem item in GridMarcas2.Items ) 
			{
			
				if ( (dgwMarcas2.GetText( item, "cbSel" ) == "Si"))  
				{
					
					w_clase.Dat.Nro.Filter  = dgwMarcas2.GetText( item, "tbClase2" ).ToString();
					w_clase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
					w_clase.Adapter.ReadAll();
					dgwMarcas2.Set(item, "tbDescripClase", w_clase.Dat.Descrip.AsString);
				}
			}



		}
		#endregion restaurar Clase

		#region btnCopiarPrioridad_Click
		private void btnCopiarPrioridad_Click(object sender, System.EventArgs e)
		{

			#region SyncDataTableMarcas2
			  SyncDataTableMarcas2();
			#endregion 

			ArrayList aMarcas = new ArrayList(); 
			TMarcas m = new TMarcas();			

			aMarcas.Clear();

			#region Verificar Marca

		
			string marca = "";

			foreach( DataGridItem item in GridMarcas2.Items ) 
			{
				/*Registros marcados */
				if ( (dgwMarcas2.GetText( item, "cbSel" ) == "Si"))  
				{
					/*Si se marcan varios registros se consideran SOLO las clases diferentes*/
					if	( marca.ToString() != dgwMarcas2.GetText( item, "tbDenominacion2").ToString() ) 
					{
						marca          = dgwMarcas2.GetText( item, "tbDenominacion2").ToString();
						m.denominacion = dgwMarcas2.GetText( item, "tbDenominacion2").ToString() ;
						m.prioridad    = dgwMarcas2.GetText( item, "tbNroPrioridad").ToString();
						m.fecha        = dgwMarcas2.GetText( item, "tbFechaPrioridad").ToString();
						m.pais         = dgwMarcas2.GetText( item, "ddlPaisPrioridad").ToString();
						aMarcas.Add(m);
					}
					
				}
			}


			#endregion Verificar Marca
  

			#region Copiar la clase
			/* Si se marcaron registros */
			if ( aMarcas.Count > 0 )
			{
				for (int i=0; i < aMarcas.Count; i++ )
				{
					m = (TMarcas)aMarcas[i];

					foreach( DataGridItem item in GridMarcas2.Items ) 
					{
						if ( dgwMarcas2.GetText( item, "tbDenominacion2" ).ToString() == m.denominacion)
						{
							dgwMarcas2.Set(item,"tbNroPrioridad",m.prioridad);
							dgwMarcas2.Set(item,"tbFechaPrioridad",m.fecha);
							dgwMarcas2.Set(item,"ddlPaisPrioridad",m.pais);
						}
					} //Grilla
			
				} //Array de Clases
			}
			#endregion


		}
		#endregion btnCopiarPrioridad_Click

		#region btnCancelar2_Click
		private void btnCancelar2_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Login.aspx");
		}
		#endregion btnCancelar2_Click

		#region Pick de Clases
		private void GridMarcas1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			
			string tb = e.Item.FindControl("tbClase").ClientID.ToString();
			string pagina = "'ClaseSelec.aspx?campo=" + tb + "'";

			string scriptCliente= "<script language='javascript'>window.open(" + pagina + ",'', ' scrollbars=yes, resizable=yes, width=700, height=450');</script>";

			Page.RegisterClientScriptBlock("ElegirClase",scriptCliente);

			
		}
		#endregion Pick de Clases

		#region Pick de Logotipo
		private void GridMarcas2_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			string tb = e.Item.FindControl("tbIdLogotipo").ClientID.ToString();
			string pagina = "'LogotipoSelec.aspx?campo=" + tb + "'";
			string scriptCliente= "<script language='javascript'>window.open(" + pagina + ",'', ' scrollbars=yes, resizable=yes, width=700, height=450');</script>";
			Page.RegisterClientScriptBlock("ElegirLogo",scriptCliente);
		}
		#endregion Pick de Logotipo

		#region btnCancelar_Click
		private void btnCancelar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Login.aspx");
		}
		#endregion btnCancelar_Click

		#region btnNuevaAtencion_Click
		private void btnNuevaAtencion_Click(object sender, System.EventArgs e)
		{
		if (ddlAtencion.Enabled) {
				#region Controlar cliente múltiple
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
                Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
				if (eccCliente.SelectedValue != "") {
					cliente.Adapter.ReadByID(eccCliente.SelectedValue);
					if (cliente.Dat.Multiple.AsBoolean) {
						this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se puede agregar atención para cliente múltiple"));
						return;
					}
				}
				#endregion Controlar cliente múltiple

				btnNuevaAtencion.Text = "Elegir atención";
				lbAtencion.Text = "Nueva atención:";
				tbNuevaAtencion.Enabled = true;
				tbNuevaAtencion.Visible = true;	
				ddlAtencion.Enabled = false;
				ddlAtencion.Visible = false;
			} else {
				btnNuevaAtencion.Text = "Nueva atención";
				lbAtencion.Text = "Atención:";
				tbNuevaAtencion.Enabled = false;
				tbNuevaAtencion.Visible = false;	
				ddlAtencion.Enabled = true;
				ddlAtencion.Visible = true;
			}		
		}
		#endregion btnNuevaAtencion_Click

		#region Eliminar detalle
		private void btnEliminarDetalle_Click(object sender, System.EventArgs e)
		{
		
		}
		#endregion Eliminar detalle

		#region Sustituidas
		private void cbSustituida_CheckedChanged(object sender, System.EventArgs e)
		{
			if (((System.Web.UI.WebControls.CheckBox)sender).Checked)
			{
				this.setVisibleSustituida(true);
			}
			else 
			{
				this.setVisibleSustituida(false);
			}

		}

		private void setVisibleSustituida(bool visible)
		{
			eccAgenteLocal.Visible = visible;
			lblAgenteLocal.Visible = visible;
			lblInfoSustituidas.Visible = visible;
		}
		#endregion Sustituidas

		#region Atencion X BU
		private void rblTipoAtencion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (((System.Web.UI.WebControls.RadioButtonList)sender).SelectedIndex == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
			{
				this.setAtencionVisible(false);
				//this.MostrarDatosCliente(Convert.ToInt32(eccCliente.SelectedValue));
			}
			else
			{
				this.setAtencionVisible(true);
			}
		}

		private void setAtencionVisible(bool visible)
		{
			ddlAtencion.Visible = visible;
			btnNuevaAtencion.Visible = visible;
			lbAtencion.Visible = visible;

			ddlBussinessUnit.Visible = !visible;
			tbAtencionBU.Visible = !visible;
			lbBU.Visible = !visible;
			lbAtencionAsigandaBU.Visible = !visible;
		}

		private void ddlBussinessUnit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlBussinessUnit.SelectedValue.ToString() != "")
			{
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

				Berke.DG.DBTab.BussinessUnit bu = new Berke.DG.DBTab.BussinessUnit( db );
				bu.Adapter.ReadByID(((System.Web.UI.WebControls.DropDownList)sender).SelectedValue);

				ddlAtencion.SelectedValue = bu.Dat.AtencionID.AsString;
				tbAtencionBU.Text = ddlAtencion.SelectedItem.Text;
			}
			else
			{
				ddlAtencion.SelectedIndex = 0;
				tbAtencionBU.Text = "";
			}
		}
		#endregion Atencion X BU

		#region Validate Form
		public void validateForm()
		{
		}
		#endregion Validate Form

		protected void GridMarcas1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}




        protected void tbDenominacion_TextChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnSiguiente_Click1(object sender, EventArgs e)
        {

        }
}

}

