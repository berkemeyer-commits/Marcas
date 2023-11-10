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

	using UIPModel = UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;

	
	public partial class RegAduana : System.Web.UI.Page
	{
		#region Declaracion de los controles
		protected System.Web.UI.WebControls.Button bCancelarLimitaciones;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.Label Label1;
		private static bool PoderActual;
		//public DataTable dt = new DataTable();
		private static int IDc;
		
		#endregion
	
		#region Asignar Eventos
		private void AsignarEventos()
		{
			this.bCorrespondencia.Click += new System.EventHandler(this.bCorrespondencia_Click);
			this.eccCliente.LoadRequested += new ecWebControls.LoadRequestedHandler(this.eccCliente_LoadRequested);
			this.eccCliente.SelectedIndexChanged += new ecWebControls.SelectedIndexChangedHandler(this.eccCliente_SelectedIndexChanged);
			this.btnNuevaAtencion.Click += new System.EventHandler(this.btnNuevaAtencion_Click);
			this.btnAsignarP.Click += new System.EventHandler(this.btnAsignarP_Click);
			this.btnAsignarMarcas.Click += new System.EventHandler(this.btnAsignarMarcas_Click);
			//this.bDescripcion.Click += new System.EventHandler(this.lbbDescripcion_Click);
			//this.lbElegirLogo.Click += new System.EventHandler(this.lbElegirLogo_Click);
			this.bAceptarlimitaciones.Click += new System.EventHandler(this.bAceptarlimitaciones_Click);
			this.btnMarcasCancelar.Click += new System.EventHandler(this.btnMarcasCancelar_Click);
			this.BntMarcasGrabar.Click += new System.EventHandler(this.BntMarcasGrabar_Click);
			this.dgMarcasAsignadas.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMarcasAsignadas_UpdateCommand);
			this.dgMarcasAsignadas.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMarcasAsignadas_DeleteCommand);
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			this.BntGrabar.Click += new System.EventHandler(this.BntGrabar_Click);
			this.chkDerechoPropio.CheckedChanged += new System.EventHandler(this.chkDerechoPropio_CheckedChanged);			
			this.btnAsignarDistribuidores.Click += new System.EventHandler(this.btnAsignarDistribuidores_Click);
			this.eccDistribuidor.LoadRequested +=new ecWebControls.LoadRequestedHandler(eccDistribuidor_LoadRequested);
			this.dgDistribuidores.DeleteCommand += new DataGridCommandEventHandler(dgDistribuidores_DeleteCommand);
		}
		#endregion

		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if( !IsPostBack )
			{
				Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
				Session["marcaID"]=0;
				Session["MarcasAsignadas"]= null;
				inTB.NewRow(); 
				inTB.Dat.Entero			.Value = Const.NIZAEDICIONID_VIGENTE;   //Int32
				inTB.PostNewRow(); 

		
				Berke.DG.DBTab.Clase clase =  Berke.Marcas.UIProcess.Model.Renovacion.ReadClase( inTB );
				this.MakedtDistribuidores();
				/*ddlClaseNueva.DataSource=clase.Table;
				ddlClaseNueva.DataTextField="DescripBreve";
				ddlClaseNueva.DataValueField = "ID";
				ddlClaseNueva.DataBind();*/

				//Año actual por defecto
				this.txtReferenciaAnio.Text = Convert.ToString(System.DateTime.Today.Year);
				pnlMarcasAsignar.Visible = false;
				pnlMarcasLimitaciones.Visible = false;
				pnlEditar.Visible = false;
				pnlMarcasBotones.Visible = false;
				pnlMarcasRenovar.Visible = false;
				pnlBotones.Visible = false;
				txtRefCorrespondencia.Visible = false;
				tbNuevaAtencion.Enabled = false;
				tbNuevaAtencion.Visible = false;
				btnNuevaAtencion.Enabled = false;
				btnNuevaAtencion.Visible = false;
				PoderActual = false;
				pnlDistribuidores.Visible = false;

				IDc = 0;

				

				if (UrlParam.GetParam("OtID") != "") 
				{
					RecuperarDatos(Convert.ToInt32(UrlParam.GetParam("OtID")));
					VerSituacionHI(Convert.ToInt32(UrlParam.GetParam("OtID")));
				}
			}
		}
		#endregion

		#region RecuperarDatos
		private void RecuperarDatos (int OtID) 
		{
			Berke.DG.RenovacionDG inDG = new Berke.DG.RenovacionDG();
			inDG.OrdenTrabajo.NewRow();
			inDG.OrdenTrabajo.Dat.ID.Value = OtID;
			inDG.OrdenTrabajo.PostNewRow();
			Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Read( inDG );

			#region Datos de la Cabecera de la OT
			lRenovacion.Text =  "Nro." + outDG.OrdenTrabajo.Dat.OrdenTrabajo.AsString;
			txtReferenciaNro.Text = outDG.OrdenTrabajo.Dat.CorrNro.AsString;
			txtReferenciaAnio.Text = outDG.OrdenTrabajo.Dat.CorrAnio.AsString;
			chkFacturable.Checked = outDG.OrdenTrabajo.Dat.Facturable.AsBoolean;
			txtRefCorrespondencia.Visible = true;
			txtRefCorrespondencia.Text = outDG.OrdenTrabajo.Dat.RefCliente.AsString;
			eccCliente.SetInitialValue(outDG.OrdenTrabajo.Dat.ClienteID.AsInt);
			txtObservacion.Text = outDG.OrdenTrabajo.Dat.Obs.AsString;
			#endregion Datos de la Cabecera de la OT

			#region Atencion
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
			atencion.InitAdapter( db );
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente();
			cliente.InitAdapter( db );
			Berke.DG.DBTab.ClienteXTramite clienteXtramite = new Berke.DG.DBTab.ClienteXTramite();
			clienteXtramite.InitAdapter( db );

			cliente.Adapter.ReadByID( outDG.OrdenTrabajo.Dat.ClienteID.AsInt );
			if (cliente.Dat.Multiple.AsBoolean) 
			{
				clienteXtramite.Dat.ClienteMultipleID.Filter = outDG.OrdenTrabajo.Dat.ClienteID.AsInt;
				clienteXtramite.Adapter.ReadAll();
				string str_cliente = outDG.OrdenTrabajo.Dat.ClienteID.AsString;
				for (clienteXtramite.GoTop(); !clienteXtramite.EOF; clienteXtramite.Skip()) 
				{
					if (str_cliente == "") 
					{
						str_cliente = clienteXtramite.Dat.ClienteID.AsString;
					} 
					else 
					{
						str_cliente = str_cliente + "," + clienteXtramite.Dat.ClienteID.AsString;
					}
				}
				atencion.Dat.ClienteID.Filter = ObjConvert.GetFilter(str_cliente);
				atencion.Adapter.ReadAll();
			} 
			else 
			{
				atencion.Dat.ClienteID.Filter = outDG.OrdenTrabajo.Dat.ClienteID.AsInt;
				atencion.Adapter.ReadAll();
			}

			ddlAtencion.DataSource = atencion.Table;
			ddlAtencion.DataTextField = "nombre";
			ddlAtencion.DataValueField = "id";		
			ddlAtencion.DataBind();
			ddlAtencion.Items.Insert (0, string.Empty );
			ddlAtencion.SelectedValue = outDG.OrdenTrabajo.Dat.AtencionID.AsString;
			#endregion Atencion

			#region Instrucciones
			for (outDG.Expediente_Instruccion.GoTop();!outDG.Expediente_Instruccion.EOF;outDG.Expediente_Instruccion.Skip()) 
			{
				if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt == (int)GlobalConst.InstruccionTipo.PODER) 
				{
					txtInstPoder.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
				}

				if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt == (int)GlobalConst.InstruccionTipo.CONTABILIDAD) 
				{
					txtInstContabilidad.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
				}
			}
			#endregion Instrucciones

			#region Derecho Propio
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			expediente.InitAdapter( db );
			expediente.Dat.OrdenTrabajoID.Filter = OtID;
			expediente.Adapter.ReadAll();
			Berke.DG.DBTab.ExpedienteXPoder ExpeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
			ExpeXpoder.InitAdapter( db );
			ExpeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			ExpeXpoder.Adapter.ReadAll();
			if (ExpeXpoder.RowCount > 0) 
			{
				chkDerechoPropio.Checked = false;
				txtIDPoder.Text = ExpeXpoder.Dat.PoderID.AsString;
			} 
			else 
			{
				Berke.DG.DBTab.ExpedienteXPropietario ExpeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
				ExpeXpropietario.InitAdapter( db );
				ExpeXpropietario.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				ExpeXpropietario.Adapter.ReadAll();
				chkDerechoPropio.Checked = true;
				txtIDPoder.Text = ExpeXpropietario.Dat.PropietarioID.AsString;
			}
			#endregion Derecho Propio

			#region TV_FillPoderActual
			Berke.DG.ViewTab.ParamTab inFP =   new Berke.DG.ViewTab.ParamTab();
			inFP.NewRow(); 
			inFP.Dat.Alfa		.Value = txtIDPoder.Text;
			inFP.Dat.Logico		.Value = chkDerechoPropio.Checked;
			inFP.PostNewRow();
			Berke.DG.ViewTab.vPoderActual vPA =  Berke.Marcas.UIProcess.Model.TV.FillPoderActual( inFP );
			dgPoderActual.DataSource = vPA.Table;

			if (vPA.RowCount > 0)
			{
				PoderActual = true;
			}
			else
			{
				PoderActual = false;
			}

			dgPoderActual.DataBind();
     		#endregion TV_FillPoderActual

			#region Marcas
			dgMarcasAsignadas.DataSource = outDG.vRenovacionMarca.Table;
			dgMarcasAsignadas.DataBind();
			Session["MarcasAsignadas"] = outDG.vRenovacionMarca.Table;
			lTotalMarcas.Text = "Marcas a ser Renovadas : " + outDG.vRenovacionMarca.RowCount; 
			pnlMarcasAsignar.Visible = false;
			pnlMarcasLimitaciones.Visible = false;
			pnlMarcasBotones.Visible = false;
			pnlMarcasElegir.Visible = true ;
			pnlMarcasRenovar.Visible = true ;
			pnlBotones.Visible = true;
			#endregion Marcas

			#region Distribuidores
			this.MakedtDistribuidores();
			outDG.vRenovacionMarca.GoTop();
			string FiltroExpeID = outDG.vRenovacionMarca.Dat.ExpedienteID.AsString;
			for (outDG.vRenovacionMarca.GoTop(); !outDG.vRenovacionMarca.EOF; outDG.vRenovacionMarca.Skip())
			{
				if (FiltroExpeID.IndexOf(outDG.vRenovacionMarca.Dat.ExpedienteID.AsString) == -1)
				{
					if (FiltroExpeID != "")
					{
						FiltroExpeID += ",";
					}
					FiltroExpeID += outDG.vRenovacionMarca.Dat.ExpedienteID.AsString;
				}
			}

			Berke.DG.ViewTab.vExpedienteDistribuidor vExpeDistri = new Berke.DG.ViewTab.vExpedienteDistribuidor(db);
			vExpeDistri.ClearFilter();
			vExpeDistri.Dat.ExpedienteID.Filter = ObjConvert.GetFilter(FiltroExpeID);
			vExpeDistri.Adapter.ReadAll();
			
			#region Asignar PK para poder realizar eliminación en base a este
			DataColumn[] key = new DataColumn[1];
			key[0] = vExpeDistri.Table.Columns["ID"];
			vExpeDistri.Table.PrimaryKey = key;
			#endregion Asignar PK para poder realizar eliminación en base a este

			Session["Distribuidores"] = vExpeDistri.Table;

			#endregion Distribuidores
		}
		#endregion RecuperarDatos

		#region VerSituacion HI
		private void VerSituacionHI (int OtID)
		{
			//Declaración de controles
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Tramite_Sit tramitesit = new Berke.DG.DBTab.Tramite_Sit();
			Berke.DG.DBTab.Situacion   sit        = new Berke.DG.DBTab.Situacion();
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			tramitesit.InitAdapter( db );
			sit.InitAdapter( db );
			expediente.InitAdapter( db );

			tramitesit.ClearFilter();
			expediente.ClearFilter();
			expediente.Dat.OrdenTrabajoID.Filter = OtID;
			expediente.Adapter.ReadAll();
			tramitesit.Adapter.ReadByID( expediente.Dat.TramiteSitID.AsInt );
			/* Si la situacion de alguno de los expedientes es diferente a HI,
			 * se deben inhabilitar algunos campos de la cabecera y todos los 
			 * detalles para las modificaciones */
			sit.Adapter.ReadByID( tramitesit.Dat.SituacionID.AsInt );
			if (sit.Dat.ID.AsInt != (int)GlobalConst.Situaciones.HOJA_INICIO) 
			{				
				dgMarcasAsignadas.Enabled = false;
				txtIDPoder.Enabled      = false;
				chkDerechoPropio.Enabled = false;
				btnAsignarMarcas.Enabled = false;
			}
		}
		#endregion VerSituacion HI

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

		#region Accion de los controles
		private void btnAsignarMarcas_Click(object sender, System.EventArgs e)
		{
			string mensajeNoMarca;

			#region Verificacion
			//Verificar que se cargue un IDPoder, Registro o un Acta Nro/Año			
			if (txtRegistroNro.Text=="")
			{
				string scriptCliente= "<script language='javascript'>alert('Debe ingresar un número de Registro')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
				/*
				if(txtActaNro.Text=="")
				{
					string scriptCliente= "<script language='javascript'>alert('Debe ingresar un número de Registro o de Acta/Año')</script>";
					Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
					return;
				}
				else
				{
					if (txtActaAnio.Text=="")
					{
						string scriptCliente= "<script language='javascript'>alert('Debe ingresar un número de Registro o de Acta/Año')</script>";
						Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
						return;
					} 
					else mensajeNoMarca="Acta Nro.:" + txtActaNro.Text + "/" + txtActaAnio.Text + ".";
				}
				*/
			} 
			else mensajeNoMarca="Registro Nro.:" + txtRegistroNro.Text + ".";
			#endregion Verificacion

			string actaNro,actaAnio,registroNro,idPoder;
			actaNro = txtActaNro.Text;
			actaAnio = txtActaAnio.Text;
			registroNro = txtRegistroNro.Text;
			idPoder = txtIDPoder.Text;

			#region Renovacion_Fill
			Berke.DG.ViewTab.ActaRegistroPoder inTB =   new Berke.DG.ViewTab.ActaRegistroPoder();
			inTB.NewRow(); 
			inTB.Dat.Acta			.Value = actaNro;   //Int32
			inTB.Dat.Anio			.Value = actaAnio;   //Int32
			inTB.Dat.Registro		.Value = registroNro;   //Int32
			inTB.Dat.Poder			.Value = idPoder;   //Int32
			inTB.PostNewRow();	
			Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Fill( inTB );
			Berke.DG.ViewTab.vRenovacionMarca vRenovacionMarca = outDG.vRenovacionMarca;
			#endregion Renovacion_Fill

			if (vRenovacionMarca.Dat.MarcaAnteriorID.AsInt == 0) 
			{
				string scriptCliente= "<script language='javascript'>alert('No existe la Marca con" + mensajeNoMarca + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}
			int marcaIDvRM;
			marcaIDvRM = (int) Session["marcaID"];
			marcaIDvRM = marcaIDvRM - 1;
			Session["marcaID"] = marcaIDvRM;
			vRenovacionMarca.Edit();
			vRenovacionMarca.Dat.MarcaID.Value = marcaIDvRM;
			vRenovacionMarca.PostEdit();

			lIdMarca.Text = vRenovacionMarca.Dat.MarcaID.AsString;
			lblMarcaID.Text = vRenovacionMarca.Dat.MarcaAnteriorID.AsString;
			txtDenominacion.Text = vRenovacionMarca.Dat.Denominacion.AsString;
			lblDenominacion.Text = vRenovacionMarca.Dat.Denominacion.AsString;
			//txtDenominacionClave.Text = vRenovacionMarca.Dat.DenominacionClave.AsString;
			lRegistro.Text = vRenovacionMarca.Dat.RegistroNro.AsString;
			LClaseAnt.Text = vRenovacionMarca.Dat.ClaseAntDescrip.AsString;
			
			LVencimiento.Text = vRenovacionMarca.Dat.Vencimiento.AsString;
			//cbLimitada.Checked = vRenovacionMarca.Dat.Limitada.AsBoolean;
			txtDescripcion.Text = vRenovacionMarca.Dat.DesEsp.AsString;
			LblTipo.Text  =  obtenerMarcaTipo(vRenovacionMarca.Dat.MarcaTipoID.AsString);

			if(vRenovacionMarca.Dat.ClaseEdicionID.AsInt == Const.NIZAEDICIONID_VIGENTE) 
			{
				//this.ddlClaseNueva.SelectedValue=vRenovacionMarca.Dat.ClaseAntID.AsString;
			}
			txtRefMarca.Text = vRenovacionMarca.Dat.Referencia.AsString;
			//tbIdLogo.Text = vRenovacionMarca.Dat.LogotipoID.AsString;

			#region Agregar a marcas asignadas
			Berke.DG.ViewTab.vRenovacionMarca vRM = null;
			if (Session["MarcasAsignadas"] == null) 
			{
				vRM = new Berke.DG.ViewTab.vRenovacionMarca();
			} 
			else 
			{
				vRM = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
			}
			vRM.NewRow();
			vRM.Dat.ActaAnio.Value = vRenovacionMarca.Dat.ActaAnio.AsInt;
			vRM.Dat.ActaNro.Value = vRenovacionMarca.Dat.ActaNro.AsInt;
			vRM.Dat.ClaseAntDescrip.Value = vRenovacionMarca.Dat.ClaseAntDescrip.AsString;
			vRM.Dat.ClaseAntID.Value = vRenovacionMarca.Dat.ClaseAntID.AsInt;
			vRM.Dat.ClaseDescrip.Value = vRenovacionMarca.Dat.ClaseDescrip.AsString;
			vRM.Dat.ClaseEdicionID.Value = vRenovacionMarca.Dat.ClaseEdicionID.AsInt;
			vRM.Dat.ClaseID.Value = vRenovacionMarca.Dat.ClaseID.AsInt;
			vRM.Dat.ConcesionFecha.Value = vRenovacionMarca.Dat.ConcesionFecha.AsDateTime;
			vRM.Dat.Denominacion.Value = vRenovacionMarca.Dat.Denominacion.AsString;
			vRM.Dat.DesEsp.Value = vRenovacionMarca.Dat.DesEsp.AsString;
			vRM.Dat.DesEspLim.Value = vRenovacionMarca.Dat.DesEspLim.AsString;
			vRM.Dat.ExpedienteID.Value = vRenovacionMarca.Dat.ExpedienteID.AsInt;
			vRM.Dat.Limitada.Value = vRenovacionMarca.Dat.Limitada.AsBoolean;
			vRM.Dat.MarcaAnteriorID.Value = vRenovacionMarca.Dat.MarcaAnteriorID.AsInt;
			vRM.Dat.MarcaID.Value = vRenovacionMarca.Dat.MarcaID.AsInt;
			vRM.Dat.MarcaTipoID.Value = vRenovacionMarca.Dat.MarcaTipoID.AsInt;
			vRM.Dat.Referencia.Value = vRenovacionMarca.Dat.Referencia.AsString;
			vRM.Dat.RegistroNro.Value = vRenovacionMarca.Dat.RegistroNro.AsInt;
			vRM.Dat.Vencimiento.Value = vRenovacionMarca.Dat.Vencimiento.AsDateTime;
			vRM.Dat.LogotipoID.Value = vRenovacionMarca.Dat.LogotipoID.AsInt;
			vRM.PostNewRow();
			Session["MarcasAsignadas"] =  vRM.Table;
			#endregion Agregar a marcas asignadas

			this.RefrescarGrillaDistribuidores();

			pnlMarcasAsignar.Visible = true;
			pnlMarcasBotones.Visible = true;
			pnlMarcasElegir.Visible = false;
			pnlMarcasRenovar.Visible = false;
			pnlDistribuidores.Visible = false;
			pnlBotones.Visible = false;
		}


		private string obtenerMarcaTipo( string marcaTipoID)
		{
			string descrip = ""; 
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.MarcaTipo marcaTipo  = new Berke.DG.DBTab.MarcaTipo();
			marcaTipo.InitAdapter( db );
			marcaTipo.Adapter.ReadByID(marcaTipoID);
			

			if ( marcaTipo.RowCount > 0 ) 
			{
				 descrip = marcaTipo.Dat.Abrev.AsString;
				
			}

			return descrip;


		}

		protected void BntGrabar_Click(object sender, System.EventArgs e)
		{
			if (!PoderActual)
			{
				string scriptCli= "<script language='javascript'>alert('No se ha seleccionado ningún poder. Favor verifique.')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCli );
				return;
			}

			string scriptCliente;
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			#region Verificar que se cargue un IDPoder o Propietario, Registro o un Acta Nro/Año

			if (this.txtIDPoder.Text=="")
			{
				if (chkDerechoPropio.Checked==false)
					scriptCliente = "<script language='javascript'>alert('Debe ingresar un ID de Poder')</script>";
				else scriptCliente = "<script language='javascript'>alert('Debe ingresar un ID de Propietario')</script>";

				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}
			if (this.txtRefCorrespondencia.Text=="")
			{
				scriptCliente = "<script language='javascript'>alert('Debe ingresar la Referencia del Cliente')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}

			if (this.eccCliente.SelectedValue=="")
			{
				scriptCliente= "<script language='javascript'>alert('Debe ingresar un Cliente')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}
			#endregion Verificar que se cargue un IDPoder o Propietario, Registro o un Acta Nro/Año

			#region Controlar cliente múltiple	
			if ( (eccCliente.SelectedValue != "") &
				(tbNuevaAtencion.Enabled) ) 
			{
				Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
				cliente.Adapter.ReadByID(eccCliente.SelectedValue);
				if (cliente.Dat.Multiple.AsBoolean) 
				{
					this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se puede agregar atención para cliente múltiple"));
					return;
				}
			}
			#endregion Controlar cliente múltiple
	
			this.pnlBotones.Visible = false;

			#region Upsert	
			
			Berke.DG.RenovacionDG inDG = new Berke.DG.RenovacionDG();

			#region ExpedienteXPropietario y ExpedienteXPoder
			Berke.DG.DBTab.ExpedienteXPropietario ePro = inDG.ExpedienteXPropietario;
			Berke.DG.DBTab.ExpedienteXPoder ePod = inDG.ExpedienteXPoder;
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario( db);

			if (chkDerechoPropio.Checked==true)
			{
				prop.Dat.ID.Filter = ObjConvert.GetFilter(this.txtIDPoder.Text);
				prop.Adapter.ReadAll();
				for (prop.GoTop(); ! prop.EOF;	prop.Skip())
				{
					ePro.NewRow(); 
					ePro.Dat.PropietarioID			.Value = prop.Dat.ID.Value;   //int Oblig.
					ePro.Dat.DerechoPropio			.Value = true;   //bit
					ePro.PostNewRow();
				}
			}
			else
			{
				Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder( db);
				poder.Dat.ID.Filter = ObjConvert.GetFilter(this.txtIDPoder.Text);
				poder.Adapter.ReadAll();
				for (poder.GoTop(); ! poder.EOF;	poder.Skip())
				{
					ePod.NewRow(); 
					ePod.Dat.PoderID			.Value = poder.Dat.ID.Value;   //int Oblig.
					ePod.PostNewRow();

					Berke.DG.DBTab.PropietarioXPoder PxP = new Berke.DG.DBTab.PropietarioXPoder( db);
					PxP.Dat.PoderID.Filter = poder.Dat.ID.AsInt;
					PxP.Adapter.ReadAll();
					for (PxP.GoTop(); ! PxP.EOF;	PxP.Skip())
					{
						ePro.NewRow(); 
						ePro.Dat.PropietarioID			.Value = PxP.Dat.PropietarioID.Value;   //int Oblig.
						ePro.Dat.DerechoPropio			.Value = false;   //bit
						ePro.PostNewRow();
					}
				}
			}
			#endregion ExpedienteXPropietario y ExpedienteXPoder

			#region OrdenTrabajo
			Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
			Func = UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
			//			int FuncionarioID = Func.Dat.ID.AsInt;

			// OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
			ot.InitAdapter( db );
			if (UrlParam.GetParam("OtID") == "") 
			{
				ot.NewRow(); 
				ot.Dat.ID.Value = -1;
			} 
			else 
			{
				ot.Adapter.ReadByID(Convert.ToInt32(UrlParam.GetParam("OtID")));
				ot.Edit();
			}
			ot.Dat.Facturable	.Value = this.chkFacturable.Checked;   //bit Oblig.
			ot.Dat.Obs			.Value = this.txtObservacion.Text;   //nvarchar
			ot.Dat.ClienteID	.Value = eccCliente.SelectedValue;
			ot.Dat.FuncionarioID.Value = Func.Dat.ID.AsInt;
			ot.Dat.CorrNro		.Value = this.txtReferenciaNro.Text;
			ot.Dat.CorrAnio		.Value = this.txtReferenciaAnio.Text;
			ot.Dat.RefCliente	.Value = this.txtRefCorrespondencia.Text;
			ot.Dat.CorrespondenciaID.Value = this.lCorID.Text;
			ot.Dat.AtencionID	.Value = ddlAtencion.SelectedValue;
			if (UrlParam.GetParam("OtID") == "") 
			{
				ot.PostNewRow();
			} 
			else 
			{
				ot.PostEdit();
			}
			#endregion 

			#region Nueva Atencion
			if (tbNuevaAtencion.Enabled) 
			{
				Berke.DG.DBTab.Atencion at = inDG.Atencion;
				at.NewRow();
				at.Dat.Nombre.Value = tbNuevaAtencion.Text;
				at.PostNewRow();
			}
			#endregion Nueva Atencion

			#region Asignar valores a Expediente_Instruccion
			Berke.DG.DBTab.Expediente_Instruccion expeInst = inDG.Expediente_Instruccion;
			expeInst.NewRow(); 
			expeInst.Dat.InstruccionTipoID	.Value = Berke.Libs.Base.GlobalConst.InstruccionTipo.CONTABILIDAD;   //int
			expeInst.Dat.FuncionarioID		.Value = Func.Dat.ID.AsInt;   //int
			expeInst.Dat.CorrespondenciaID	.Value = this.lCorID.Text;   //int
			expeInst.Dat.Obs				.Value = this.txtInstContabilidad.Text;   //nvarchar
			expeInst.PostNewRow(); 
			expeInst.NewRow(); 
			expeInst.Dat.InstruccionTipoID	.Value = Berke.Libs.Base.GlobalConst.InstruccionTipo.PODER;   //int
			expeInst.Dat.FuncionarioID		.Value = Func.Dat.ID.AsInt;   //int
			expeInst.Dat.CorrespondenciaID	.Value = this.lCorID.Text;   //int
			expeInst.Dat.Obs				.Value = this.txtInstPoder.Text;   //nvarchar
			expeInst.PostNewRow(); 			
			#endregion Asignar valores a Expediente_Instruccion

			#region Asignar valores a Expediente_Distribuidor
			Berke.DG.DBTab.Expediente_Distribuidor expeDist = inDG.Expediente_Distribuidor;

			DataTable dat = (DataTable)Session["Distribuidores"];

			foreach (DataRow dr in dat.Rows)
			{
				expeDist.NewRow();
				expeDist.Dat.MarcaID.Value = dr["MarcaID"];
				expeDist.Dat.DistribuidorID.Value = dr["DistribuidorID"];
				expeDist.Dat.Producto_Servicio.Value = dr["Producto_Servicio"];
				expeDist.PostNewRow();
			}

			#endregion Asignar valores a Expediente_Distribuidor

			inDG.vRenovacionMarca = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
			
			#endregion Upsert
			
		
			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.Renovacion.UpsertRegAduana ( inDG );
			if (outTB.Dat.Logico.AsBoolean) 
			{
				string pagconfirmacion = "RegAduanaDetalle.aspx?OtID=" + outTB.Dat.Entero.AsString;
				Response.Redirect(pagconfirmacion);
			}
			else
			{
				scriptCliente= "<script language='javascript'>alert('Atención. Renovación NO Realizada!')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				this.pnlBotones.Visible = true;
			}
		}
		
	
		#endregion


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

			MostrarAtencion(outTB.Dat.ID.AsInt);
			lAtencion.Text = "Atención";
			ddlAtencion.Visible = true;
			btnNuevaAtencion.Enabled = true;
			btnNuevaAtencion.Visible = true;

			
			MostrarDireccionCte(outTB.Dat.ID.AsInt);
			

		}

		protected void BntMarcasGrabar_Click(object sender, System.EventArgs e)
		{
			//int claseSel = ObjConvert.AsInt(ddlClaseNueva.SelectedItem.Value);			
			//Berke.DG.DBTab.Clase clase = Berke.Marcas.UIProcess.Model.Clase.ReadByID( claseSel );
	
			//Ubicar la marca modificada en MarcasAsignadas
			Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
			for (vRM.GoTop(); !vRM.EOF; vRM.Skip()) 
			{
				if (vRM.Dat.MarcaID.AsString == lIdMarca.Text) 
				{
					break;
				}
			}

			vRM.Edit();
			vRM.Dat.Denominacion.Value = txtDenominacion.Text;
			//vRM.Dat.DenominacionClave.Value = txtDenominacionClave.Text;
			//vRM.Dat.ClaseDescrip.Value = ddlClaseNueva.SelectedItem.Text;
			vRM.Dat.Referencia.Value = txtRefMarca.Text;
			//vRM.Dat.DesEspLim.Value = clase.Dat.Descrip.AsString;
			/*vRM.Dat.ClaseID.Value = ddlClaseNueva.SelectedItem.Value;
			vRM.Dat.Limitada.Value = cbLimitada.Checked;
			vRM.Dat.DesEspLim.Value = txtDescripcion.Text;
			vRM.Dat.LogotipoID.Value = tbIdLogo.Text;*/
			vRM.PostEdit();

			Grid.Bind(dgMarcasAsignadas, vRM.Table);
			Session["MarcasAsignadas"] = vRM.Table;

			lTotalMarcas.Text = "Marcas a ser Registradas en Aduanas: " + dgMarcasAsignadas.Items.Count.ToString();

			pnlMarcasAsignar.Visible = false;
			pnlMarcasLimitaciones.Visible = false;
			pnlMarcasBotones.Visible = false;
			pnlMarcasElegir.Visible = true;
			pnlMarcasRenovar.Visible = true;
			pnlBotones.Visible = true;
		}

		private void dgMarcasLimitaciones_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			pnlEditar.Visible = true;
			Berke.DG.ViewTab.vRenovacionLimitadas vRL = new Berke.DG.ViewTab.vRenovacionLimitadas((DataTable) Session["Limitaciones"]);
			vRL.Go(e.Item.ItemIndex);
			txtIdioma.Text = vRL.Dat.Descrip.AsString;
			LIdioma.Text = vRL.Dat.Idioma.AsString;
			pnlMarcasLimitaciones.Visible = false;
			pnlMarcasAsignar.Visible = false;
			pnlMarcasBotones.Visible = false;
		}


		private void bAceptarlimitaciones_Click(object sender, System.EventArgs e)
		{
			Berke.DG.ViewTab.vRenovacionLimitadas vRL = new Berke.DG.ViewTab.vRenovacionLimitadas((DataTable) Session["Limitaciones"]);
			vRL.FilterOn();
			vRL.Dat.Idioma.Filter=LIdioma.Text;
			vRL.GoTop();
			vRL.Edit();
			vRL.Dat.Descrip.Value=txtIdioma.Text;
			vRL.PostEdit();
			Grid.Bind(dgMarcasLimitaciones,vRL.Table);
			Session["Limitaciones"]=vRL.Table;

			pnlEditar.Visible = false;
			pnlMarcasAsignar.Visible = true;
			pnlMarcasLimitaciones.Visible = true;
			pnlMarcasBotones.Visible = true;
		}

		private void cbLimitada_CheckedChanged(object sender, System.EventArgs e)
		{
			/*if (cbLimitada.Checked==true)
			{
				pnlMarcasLimitaciones.Visible = true;


				#region Renovacion.FillLimitaciones
				Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
				inTB.NewRow(); 
				inTB.Dat.Entero			.Value = ddlClaseNueva.SelectedValue;   //Int32
				inTB.PostNewRow(); 
				Berke.DG.ViewTab.vRenovacionLimitadas outTB =  Berke.Marcas.UIProcess.Model.Renovacion.FillLimitaciones( inTB );
				#endregion Renovacion.FillLimitaciones
				outTB.Edit();
				outTB.Dat.MarcaID.Value = (int) Session["marcaID"];
				outTB.PostEdit();
				Grid.Bind(dgMarcasLimitaciones,outTB.Table);
				Session["Limitaciones"]=outTB.Table;
			}
			else
			{
				Grid.Bind(dgMarcasLimitaciones,null);
				pnlMarcasLimitaciones.Visible = false;
			}*/
		}

		private void btnMarcasCancelar_Click(object sender, System.EventArgs e)
		{
			pnlMarcasAsignar.Visible = false;
			pnlMarcasBotones.Visible = false;
			pnlMarcasLimitaciones.Visible = false;
			pnlMarcasElegir.Visible = true ;
			if (dgMarcasAsignadas.Items.Count > 0)
			{
				pnlMarcasRenovar.Visible = true ;
				pnlBotones.Visible = true;
			}
		}

		private void btnCancelar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("RegAduana.aspx");
		}

		private void dgMarcasAsignadas_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
						
			vRM.Go(e.Item.ItemIndex);

			#region Borrar Distribuidores asociados
			DataTable dt = (DataTable) Session["Distribuidores"];
			string filterExp = "MarcaID = '" + vRM.Dat.MarcaAnteriorID.AsString + "'";

			DataRow[] drA = dt.Select(filterExp, "MarcaID", DataViewRowState.CurrentRows);
															
			for (int i = 0; i < drA.Length ; i++)
			{
				this.DeleteRowDistribuidores(drA[i]["ID"].ToString());					
			}
			#endregion Borrar Distribuidores asociados

			vRM.Delete();
			vRM.AcceptAllChanges();
			Grid.Bind(this.dgMarcasAsignadas, vRM.Table);
			if( vRM.RowCount > 0 )
			{
				//				pnlMarcasRenovar.Visible = true ;
				//				pnlBotones.Visible = true;
			}
			else
			{
				pnlMarcasRenovar.Visible = false ;
				pnlBotones.Visible = false;	
			}
			lTotalMarcas.Text = "Marcas a ser Renovadas : " + dgMarcasAsignadas.Items.Count.ToString(); 

		}

		private void bCorrespondencia_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.DBTab.Correspondencia Co = new Berke.DG.DBTab.Correspondencia( db );
			Co.Dat.Nro.Filter = txtReferenciaNro.Text;
			Co.Dat.Anio.Filter = txtReferenciaAnio.Text;
			Co.Adapter.ReadAll();

			if (Co.RowCount==0)
			{
				string scriptCliente= "<script language='javascript'>alert('No se encuentre la correspondencia " + txtReferenciaNro.Text + "/" + txtReferenciaAnio.Text + ".')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
			else
			{
				//				bCorrespondencia.Visible = false;
				//				txtRefCorrespondencia.Visible = true;
				txtRefCorrespondencia.Text = Co.Dat.RefCliente.AsString;
				string scriptCliente= "<script language='javascript'>alert('Correspondencia " + txtReferenciaNro.Text + "/" + txtReferenciaAnio.Text + " enlazada.')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				lCorID.Text = Co.Dat.ID.AsString;
				//				txtReferenciaNro.ReadOnly = true;
				//				txtReferenciaAnio.ReadOnly = true;
			}
			txtRefCorrespondencia.Visible = true;
		}


		private void bDescripcion_Click(object sender, System.EventArgs e)
		{
	
			#region Renovacion.FillLimitaciones
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			//inTB.Dat.Entero			.Value = ddlClaseNueva.SelectedValue;   //Int32
			inTB.PostNewRow(); 
			Berke.DG.ViewTab.vRenovacionLimitadas outTB =  Berke.Marcas.UIProcess.Model.Renovacion.FillLimitaciones( inTB );
			#endregion Renovacion.FillLimitaciones

			txtDescripcion.Text=outTB.Dat.Descrip.AsString;
		}

		private void chkDerechoPropio_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkDerechoPropio.Checked == true)
			{
				lPoder.Text = "Propietario";
				btnAsignarP.Text = "Elegir Propietario";
			}
			else 
			{
				lPoder.Text = "Poder";
				btnAsignarP.Text = "Elegir Poder";
			}
		}

		private void eccCliente_SelectedIndexChanged(ecWebControls.ecCombo combo, System.EventArgs e)
		{
			MostrarAtencion(Convert.ToInt32(eccCliente.SelectedValue));
			lAtencion.Text = "Atención";
			ddlAtencion.Visible = true;
			btnNuevaAtencion.Enabled = true;
			btnNuevaAtencion.Visible = true;
			
			MostrarDireccionCte(Convert.ToInt32(eccCliente.SelectedValue));
		

		}

		
		#region Mostrar Direccion del Cliente
		private void MostrarDireccionCte(int clienteID)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente(db);
			Berke.DG.DBTab.ClienteXTramite clienteXTramite = new Berke.DG.DBTab.ClienteXTramite(db);

			string correo = "";
			lDireccionCte.Text = "";
			cliente.Adapter.ReadByID(clienteID);
			
			if ( cliente.RowCount > 0 ) 
			{
				
				if (cliente.Dat.Multiple.AsBoolean) 
				{
					clienteXTramite.Dat.ClienteMultipleID.Filter = clienteID;
					clienteXTramite.Dat.TramiteID.Filter  = (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION; //RENOVACION
					clienteXTramite.Adapter.ReadAll();

					if ( clienteXTramite.RowCount > 0 )
					{
						cliente.Adapter.ReadByID(clienteXTramite.Dat.ClienteID.AsInt) ;

						if ( cliente.RowCount > 0 )
						{
							correo = cliente.Dat.Correo.AsString;
						}
					
					} 
					else 
					{
						clienteXTramite.Dat.ClienteMultipleID.Filter = clienteID;
						clienteXTramite.Adapter.ReadAll();

						if ( clienteXTramite.RowCount > 0 )
						{

							cliente.Adapter.ReadByID(clienteXTramite.Dat.ClienteID.AsInt) ;

							if ( cliente.RowCount > 0 )
							{
								correo = cliente.Dat.Correo.AsString;
							}
						
						}
					}
				}
				else {
					correo = cliente.Dat.Correo.AsString;
				}
				

				lDireccionCte.Text = correo;
			}
			db.CerrarConexion();
			

		}
		#endregion Mostrar Direccion del Cliente

		#region MostrarAtencion
		private void MostrarAtencion(int clienteID)
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
			cliente.Adapter.ReadByID( clienteID );

			#region Cliente Múltiple
			if (cliente.Dat.Multiple.AsBoolean) 
			{
				clienteXtramite.Dat.ClienteMultipleID.Filter = clienteID;
				clienteXtramite.Adapter.ReadAll();
				string str_cliente = clienteID.ToString();
				for (clienteXtramite.GoTop(); !clienteXtramite.EOF; clienteXtramite.Skip()) 
				{
					if (str_cliente == "") 
					{
						str_cliente = clienteXtramite.Dat.ClienteID.AsString;
					} 
					else 
					{
						str_cliente = str_cliente + "," + clienteXtramite.Dat.ClienteID.AsString;
					}
				}
				atencion.Dat.ClienteID.Filter = ObjConvert.GetFilter(str_cliente);
				atencion.Adapter.ReadAll();
			} 
			else 
			{
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
		}
		#endregion MostrarAtencion

		private void btnAsignarP_Click(object sender, System.EventArgs e)
		{
			if (this.txtIDPoder.Text=="")
			{
				string scriptCliente= "<script language='javascript'>alert('Debe ingresar un ID de Poder')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}

			#region TV_FillPoderActual

			Berke.DG.ViewTab.ParamTab inFP =   new Berke.DG.ViewTab.ParamTab();
			inFP.NewRow(); 
			inFP.Dat.Alfa		.Value = txtIDPoder.Text;   //Int32
			inFP.Dat.Logico		.Value = chkDerechoPropio.Checked;
			inFP.PostNewRow(); 

			Berke.DG.ViewTab.vPoderActual vPA =  Berke.Marcas.UIProcess.Model.TV.FillPoderActual( inFP );
			dgPoderActual.DataSource = vPA.Table;
			dgPoderActual.DataBind();

			if (vPA.RowCount > 0)
			{
				PoderActual = true;
			}
			else
			{
				PoderActual = false;
			}
	

			#endregion TV_FillPoderActual

		}

		#region btnNuevaAtencion_Click
		private void btnNuevaAtencion_Click(object sender, System.EventArgs e)
		{
			if (ddlAtencion.Enabled) 
			{
				#region Controlar cliente múltiple
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
				if (eccCliente.SelectedValue != "") 
				{
					cliente.Adapter.ReadByID(eccCliente.SelectedValue);
					if (cliente.Dat.Multiple.AsBoolean) 
					{
						this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se puede agregar atención para cliente múltiple"));
						return;
					}
				}
				#endregion Controlar cliente múltiple

				btnNuevaAtencion.Text = "Elegir atención";
				lAtencion.Text = "Nueva atención:";
				tbNuevaAtencion.Enabled = true;
				tbNuevaAtencion.Visible = true;	
				ddlAtencion.Enabled = false;
				ddlAtencion.Visible = false;
			} 
			else 
			{
				btnNuevaAtencion.Text = "Nueva atención";
				lAtencion.Text = "Atención:";
				tbNuevaAtencion.Enabled = false;
				tbNuevaAtencion.Visible = false;	
				ddlAtencion.Enabled = true;
				ddlAtencion.Visible = true;
			}            		
		}
		#endregion btnNuevaAtencion_Click

		#region dgMarcasAsignadas_UpdateCommand
		private void dgMarcasAsignadas_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{		
			Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);			
			vRM.Go(e.Item.ItemIndex);
			lIdMarca.Text = vRM.Dat.MarcaID.AsString;
			lblMarcaID.Text = vRM.Dat.MarcaAnteriorID.AsString;
			txtDenominacion.Text = vRM.Dat.Denominacion.AsString;
			lblDenominacion.Text = vRM.Dat.Denominacion.AsString;
			//txtDenominacionClave.Text = vRM.Dat.DenominacionClave.AsString;
			lRegistro.Text = vRM.Dat.RegistroNro.AsString;
			LClaseAnt.Text = vRM.Dat.ClaseAntDescrip.AsString;
			LVencimiento.Text = vRM.Dat.Vencimiento.AsString;
			//cbLimitada.Checked = vRM.Dat.Limitada.AsBoolean;
			txtDescripcion.Text = vRM.Dat.DesEspLim.AsString;			
			if(vRM.Dat.ClaseEdicionID.AsInt == Const.NIZAEDICIONID_VIGENTE) 
			{
				//this.ddlClaseNueva.SelectedValue = vRM.Dat.ClaseAntID.AsString;
			} 
			else 
			{
				//this.ddlClaseNueva.SelectedValue = vRM.Dat.ClaseID.AsString;
			}			
			txtRefMarca.Text = vRM.Dat.Referencia.AsString;
			//tbIdLogo.Text = vRM.Dat.LogotipoID.AsString;
			this.RefrescarGrillaDistribuidores();
			pnlDistribuidores.Visible = true;
			pnlMarcasAsignar.Visible = true;
			pnlMarcasBotones.Visible = true;
			pnlMarcasElegir.Visible = false;
			pnlMarcasRenovar.Visible = false;
			
			pnlBotones.Visible = false;
		}
		#endregion dgMarcasAsignadas_UpdateCommand

		#region lbElegirLogo_Click
		private void lbElegirLogo_Click(object sender, System.EventArgs e)
		{
			/*string pagina = "'LogotipoSelec.aspx?campo=" + tbIdLogo.ClientID.ToString() + "'";
			string scriptCliente= "<script language='javascript'>window.open(" + pagina + ",'', ' scrollbars=yes, resizable=yes, width=700, height=450');</script>";
			Page.RegisterClientScriptBlock("ElegirLogo",scriptCliente);*/
		}
		#endregion lbElegirLogo_Click

		private void btnAsignarDistribuidores_Click(object sender, System.EventArgs e)
		{
			if (txtProducto_Servicio.Text.Trim() == "")
			{
				string scriptCliente= "<script language='javascript'>alert('Debe definir el Producto/Servicio.')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}
			else if (eccDistribuidor.SelectedText.Trim() == "")
			{
				string scriptCliente= "<script language='javascript'>alert('Debe ingresar un distribuidor.')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;
			}

			this.InsertarFila();	
			pnlDistribuidores.Visible = true;
			eccDistribuidor.Mode = ecWebControls.ComboMode.EnterQuery;
			eccDistribuidor.Text = "";
			txtProducto_Servicio.Text = "";
			
		}

		public void MakedtDistribuidores()
		{
			DataTable dt = new DataTable();	
			dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
			dt.Columns.Add(new DataColumn("MarcaID", typeof(Int32)));
			dt.Columns.Add(new DataColumn("DistribuidorID", typeof(Int32)));
			dt.Columns.Add(new DataColumn("DistribuidorNombre", typeof(String)));
			dt.Columns.Add(new DataColumn("Producto_Servicio", typeof(String)));

			DataColumn[] key = new DataColumn[1];
			key[0] = dt.Columns["ID"];
			dt.PrimaryKey = key;
			
			Session["Distribuidores"] = dt;
			
		}

		public void InsertarFila()
		{
			DataTable dt = (DataTable)Session["Distribuidores"]; 
			DataRow dr = dt.NewRow();
				
			dr["ID"] = this.GetIDdtDistribuidores();
			dr["MarcaID"] = lblMarcaID.Text;
			dr["DistribuidorID"] = eccDistribuidor.SelectedValue;
			dr["DistribuidorNombre"] = eccDistribuidor.SelectedText;
			dr["Producto_Servicio"] = txtProducto_Servicio.Text;

			dt.Rows.Add(dr);
			dt.AcceptChanges();

			Session["Distribuidores"] = dt;

			this.RefrescarGrillaDistribuidores();
	
		}


		private void RefrescarGrillaDistribuidores()
		{
			string filterExp = "MarcaID = '" + lblMarcaID.Text + "'";

			DataTable dt = (DataTable) Session["Distribuidores"];

			DataRow[] drA = dt.Select(filterExp, "MarcaID", DataViewRowState.CurrentRows);
						
			DataTable dt1 = dt.Clone();
			dt1.Rows.Clear();

									
			for (int i = 0; i < drA.Length ; i++)
			{
				dt1.ImportRow(drA[i]);
				dt1.AcceptChanges();
			}

			dgDistribuidores.DataSource = dt1;
			dgDistribuidores.DataBind();

			if (drA.Length > 0)
			{
				pnlDistribuidores.Visible = true;
			}
			else
			{
				pnlDistribuidores.Visible = false;
			}
		}

		private void eccDistribuidor_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			#region Asignar Parametros
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente(db);
			cliente.ClearFilter();

			if( combo.SelectedKeyValue == "ID" )
			{
				cliente.Dat.ID.Filter = Convert.ToInt32( combo.Text );
			}
			else
			{
				cliente.Dat.Nombre.Filter = ObjConvert.GetSqlPattern(combo.Text);			
			}
			cliente.Dat.Distribuidor.Filter = true;
			cliente.Adapter.ReadAll();
			#endregion Asignar Parametros

			combo.BindDataSource(cliente.Table, cliente.Dat.Nombre.Name, cliente.Dat.ID.Name);
			
			db.CerrarConexion();
		}

		private void dgDistribuidores_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			//Hay que usar primary key para encontrar y borrar el elemento del datatable
			string ID = e.Item.Cells[3].Text;
			this.DeleteRowDistribuidores(ID);
			this.RefrescarGrillaDistribuidores();
			/*DataTable dt = (DataTable)Session["Distribuidores"];
			DataRow dr = dt.Rows.Find(ID);
			dt.Rows.Remove(dr);
			this.RefrescarGrillaDistribuidores();*/
		}

		private int GetIDdtDistribuidores()
		{
			return IDc++;
		}

		private void DeleteRowDistribuidores(string ID)
		{
			DataTable dt = (DataTable)Session["Distribuidores"];
			DataRow dr = dt.Rows.Find(ID);
			dt.Rows.Remove(dr);
		}
	}
}