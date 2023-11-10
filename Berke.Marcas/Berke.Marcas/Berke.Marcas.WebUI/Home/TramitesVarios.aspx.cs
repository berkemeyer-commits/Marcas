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

	public partial class TramitesVarios : System.Web.UI.Page
	{
		#region Declaracion de Controles

		#endregion Declaracion de Controles
		
		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				pnlActualPoder.Visible = false;
				btnSalir.Visible = false;

				#region Asignar TV

				Session["TramiteID"] = Convert.ToInt32(UrlParam.GetParam("tramiteID"));
				Berke.DG.DBTab.ExpedienteCampo ec = new Berke.DG.DBTab.ExpedienteCampo();

				//Año actual por defecto
				txtCorrespAnio.Text = Convert.ToString(System.DateTime.Today.Year);
				cbDerechoPropio.Checked = false;
				
				tbNuevaAtencion.Enabled = false;
				tbNuevaAtencion.Visible = false;

				switch((int)Session["TramiteID"])
				{
						#region Transferencia
					
					case (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA :

						lblTV.Text = "Transferencia (TR)";
						//lbDerechoPropio.Enabled = true;
						//lbDerechoPropio.Visible = true;
						cbDerechoPropio.Enabled = true;
						cbDerechoPropio.Visible = true;						
						break;

						#endregion Transferencia

						#region Cambio de Nombre

					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE :
						
						lblTV.Text = "Cambio de Nombre (CN)";

						/*
						#region Asignar Documentos

						ec.NewRow();
						ec.Dat.Campo.Value = "Obs. del Certificado de CN";
						ec.PostNewRow();

						dgAsignarDocumentos.DataSource = ec.Table;
						dgAsignarDocumentos.DataBind();

						Session["ecampo"]=ec.Table;

						// Determina cual de los TextBoxes es Multilínea

						for (int i=0; i<dgAsignarDocumentos.Items.Count; i++) 
						{
							if (i == 0)
							{
								TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
								txtVal.TextMode = TextBoxMode.MultiLine;
								txtVal.Rows = 2;
							}
						}

						//===============================================

						pnlAsignarDocumentos.Visible = true;

						#endregion Asignar Documentos
						*/

						//lbDerechoPropio.Enabled = true;
						//lbDerechoPropio.Visible = true;
						cbDerechoPropio.Enabled = true;
						cbDerechoPropio.Visible = true;

						break;

						#endregion Cambio de Nombre

						#region Fusion

					case (int) GlobalConst.Marca_Tipo_Tramite.FUSION :

						lblTV.Text = "Fusión (FUS)";

						#region Asignar Documentos
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.FUS_NOMBRE_OTROS_PROP;
						ec.PostNewRow();
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.FUS_DIR_OTROS_PROP;
						ec.PostNewRow();
						dgAsignarDocumentos.DataSource = ec.Table;
						dgAsignarDocumentos.DataBind();
						Session["ecampo"]=ec.Table;

						// Determina cual de los TextBoxes es Multilínea
						for (int i=0; i<dgAsignarDocumentos.Items.Count; i++) 
						{
							TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
							txtVal.TextMode = TextBoxMode.MultiLine;
							txtVal.MaxLength = 400;
							txtVal.Rows = 2;
						}
						//===============================================
						pnlAsignarDocumentos.Visible = true;
						#endregion Asignar Documentos			

						//lbDerechoPropio.Enabled = false;
						//lbDerechoPropio.Visible = false;
						cbDerechoPropio.Enabled = false;
						cbDerechoPropio.Visible = false;
						break;

						#endregion Fusion

						#region Cambio de Domicilio

					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO :

						lblTV.Text = "Cambio de Domicilio (CD)";
						//lbDerechoPropio.Enabled = true;
						//lbDerechoPropio.Visible = true;
						cbDerechoPropio.Enabled = true;
						cbDerechoPropio.Visible = true;
						break;

						#endregion Cambio de Domicilio

						#region Licencia

					case (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA :

						lblTV.Text = "Licencia (LIC)";
						pnlActual.Visible = false;

						#region Asignar Documentos

						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.LIC_NOMBRE;
						ec.PostNewRow();
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.LIC_DIRECCION;
						ec.PostNewRow();
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.LIC_VIGENC_DESDE;
						ec.PostNewRow();
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.LIC_VIGENC_HASTA;
						ec.PostNewRow();
						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.LIC_DESCRIPCION;
						ec.PostNewRow();
						dgAsignarDocumentos.DataSource = ec.Table;
						dgAsignarDocumentos.DataBind();

						Session["ecampo"]=ec.Table;

						// Determina cual de los TextBoxes es Multilínea

						for (int i=0; i<dgAsignarDocumentos.Items.Count; i++) 
						{
							if (i < 2 )
							{
								TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
								txtVal.TextMode = TextBoxMode.MultiLine;
								txtVal.MaxLength = 400;
								txtVal.Rows = 2;
							}
						}

						//===============================================

						pnlAsignarDocumentos.Visible = true;

						#endregion Asignar Documentos			

						//lbDerechoPropio.Enabled = false;
						//lbDerechoPropio.Visible = false;
						cbDerechoPropio.Enabled = false;
						cbDerechoPropio.Visible = false;

						break;

						#endregion Licencia

						#region Duplicado de Titulo

					case (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO :

						lblTV.Text = "Duplicado de Título (DUP)";
						pnlActual.Visible = false;

						#region Asignar Documentos


						ec.NewRow();
						ec.Dat.Campo.Value = GlobalConst.DUP_LEGISLACION_CONSULAR;
						ec.PostNewRow();

						dgAsignarDocumentos.DataSource = ec.Table;
						dgAsignarDocumentos.DataBind();

						Session["ecampo"]=ec.Table;

						// Determina cual de los TextBoxes es Multilínea

						for (int i=0; i<dgAsignarDocumentos.Items.Count; i++) 
						{
							if (i == 0)
							{
								TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
								txtVal.TextMode = TextBoxMode.MultiLine;
								txtVal.Rows = 2;
							}
						}

						//===============================================

						pnlAsignarDocumentos.Visible = true;

						#endregion Asignar Documentos			

						//lbDerechoPropio.Enabled = false;
						//lbDerechoPropio.Visible = false;
						cbDerechoPropio.Enabled = false;
						cbDerechoPropio.Visible = false;

						break;

						#endregion Duplicado de Titulo

						#region Cambio de Nombre y Domicilio					
					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC:
						lblTV.Text = "Cambio de Nombre y Domicilio (CND)";
						//lbDerechoPropio.Enabled = true;
						//lbDerechoPropio.Visible = true;
						cbDerechoPropio.Enabled = true;
						cbDerechoPropio.Visible = true;
						break;
						#endregion Cambio de Nombre y Domicilio
				}

				#endregion Asignar TV

				RecuperarDatos();
				VerSituacionHI();
			} 
		}

		#endregion Page_Load

		#region RecuperarDatos
		private void RecuperarDatos() 
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;




			if (UrlParam.GetParam("otID") != "") 
			{
				Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
				inTB.NewRow(); 
				inTB.Dat.Entero.Value = Convert.ToInt32(UrlParam.GetParam("otID"));   //Int32
				inTB.PostNewRow(); 		
				Berke.DG.TVDG outDG =  Berke.Marcas.UIProcess.Model.TV.Read( inTB );
				lblTV.Text = lblTV.Text + "Hoja de Inicio Nro." + outDG.OrdenTrabajo.Dat.OrdenTrabajo.AsString;
				eccCliente.SetInitialValue(outDG.OrdenTrabajo.Dat.ClienteID.AsInt);

				


				#region Atencion
				Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
				atencion.InitAdapter( db );
				Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente();
				cliente.InitAdapter( db );
				Berke.DG.DBTab.ClienteXTramite clienteXtramite = new Berke.DG.DBTab.ClienteXTramite();
				clienteXtramite.InitAdapter( db );

				cliente.Adapter.ReadByID( outDG.OrdenTrabajo.Dat.ClienteID.AsInt );
				if (cliente.Dat.Multiple.AsBoolean) {
					clienteXtramite.Dat.ClienteMultipleID.Filter = outDG.OrdenTrabajo.Dat.ClienteID.AsInt;
					clienteXtramite.Adapter.ReadAll();
					string str_cliente = outDG.OrdenTrabajo.Dat.ClienteID.AsString;
					for (clienteXtramite.GoTop(); !clienteXtramite.EOF; clienteXtramite.Skip()) {
						if (str_cliente == "") {
							str_cliente = clienteXtramite.Dat.ClienteID.AsString;
						} else {
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

				txtCorrespAnio.Text = outDG.OrdenTrabajo.Dat.CorrAnio.AsString;
				txtCorrespNro.Text = outDG.OrdenTrabajo.Dat.CorrNro.AsString;
				chkFacturable.Checked = outDG.OrdenTrabajo.Dat.Facturable.AsBoolean;
				txtObsClientes.Text = outDG.OrdenTrabajo.Dat.Obs.AsString;
				tbReferenciaCliente.Text = outDG.OrdenTrabajo.Dat.RefCliente.AsString;
				txtRefCorresp.Text = outDG.OrdenTrabajo.Dat.RefCorr.AsString;

				#region Derecho Propio
				Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
				expediente.InitAdapter( db );
				expediente.Dat.OrdenTrabajoID.Filter = Convert.ToInt32(UrlParam.GetParam("otID"));
				expediente.Adapter.ReadAll();
				Berke.DG.DBTab.ExpedienteXPoder ExpeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
				ExpeXpoder.InitAdapter( db );
				ExpeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				ExpeXpoder.Adapter.ReadAll();
				if (ExpeXpoder.RowCount > 0) {
					cbDerechoPropio.Checked = false;
				} else {
					cbDerechoPropio.Checked = true;
				}
				#endregion Derecho Propio

				#region Instrucciones
				for (outDG.Expediente_Instruccion.GoTop();!outDG.Expediente_Instruccion.EOF;outDG.Expediente_Instruccion.Skip()) {
					if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt == (int) GlobalConst.InstruccionTipo.PODER) {
						tbInstruccionPoder.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
					}
					if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt == (int) GlobalConst.InstruccionTipo.CONTABILIDAD) {
						tbInstruccionContabilidad.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
					}
				}
				#endregion Instrucciones

				#region ExpedienteCampo
				dgAsignarDocumentos.DataSource = outDG.ExpedienteCampo.Table;
				dgAsignarDocumentos.DataBind();
				Session["ecampo"] = outDG.ExpedienteCampo.Table;
				outDG.ExpedienteCampo.GoTop();
				for (int i=0; i < dgAsignarDocumentos.Items.Count; i++) {
					TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
					txtVal.Text = outDG.ExpedienteCampo.Dat.Valor.AsString;
					outDG.ExpedienteCampo.Skip();					
				}
				//outDG.ExpedienteCampo.GoTop();
				#endregion ExpedienteCampo

				dgPoderAnterior.DataSource = outDG.vPoderAnterior.Table;
				dgPoderAnterior.DataBind();
				Session["vPAnterior"] = outDG.vPoderAnterior.Table;
				dgPoderActual.DataSource = outDG.vPoderActual.Table;
				dgPoderActual.DataBind();
				Session["vPActual"] = outDG.vPoderActual.Table;
				txtIDPoder.Text = outDG.vPoderActual.Dat.ID.AsString;
				
				if (outDG.vRenovacionMarca.RowCount > 0) 
				{
					#region Recuperar Datos de Marcas
					Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca();
					txtActas.Text = outDG.vRenovacionMarca.Dat.Denominacion.AsString;
					string [] aActas = outDG.vRenovacionMarca.Dat.Denominacion.AsString.Split( ((String)";, ").ToCharArray() );
					int actanro = 0;
					int actaanio = 0;
					string [] actacompleta;
					foreach( string acta in aActas ) {
						actacompleta=acta.Split(((String)"/").ToCharArray(),2);
						if( acta.Trim() == "" ) continue;
						actanro	= Convert.ToInt32( actacompleta[0]);
						actaanio= Convert.ToInt32( actacompleta[1]);

						Berke.DG.ViewTab.ActaRegistroPoder inTB2 =   new Berke.DG.ViewTab.ActaRegistroPoder();
						inTB2.NewRow(); 
						inTB2.Dat.Acta			.Value = actanro;   //Int32
						inTB2.Dat.Anio			.Value = actaanio;   //Int32
						inTB2.PostNewRow(); 
						Berke.DG.RenovacionDG outDG2 =  Berke.Marcas.UIProcess.Model.Renovacion.Fill( inTB2 );
						#region Completar marcaid en vRM para saber que es una modificación
						/* Por defecto el marcaid de vRM es null, pero lo marcamos con el valor 1 para
						 * saber si es una modificación o inserción de la marca en el action Upser de TV */
						outDG2.vRenovacionMarca.Edit();
						outDG2.vRenovacionMarca.Dat.MarcaID.Value = 1;
						outDG2.vRenovacionMarca.PostEdit();						
						#endregion Completar marcaid en vRM para saber que es una modificación
						vRM.Table.Rows.Add(outDG2.vRenovacionMarca.Table.Rows[0].ItemArray);
					}
					dgMarcasAsignadas.DataSource = vRM.Table;
					dgMarcasAsignadas.DataBind();
					Session["MarcasAsignadas"] = vRM.Table;
					#endregion Recuperar Datos de Marcas

					pnlMostrarMarcas.Visible = true;
					pnlBusquedaMarcas.Visible = false;
					pnlAnterior.Visible = true;									
					pnlAsignarPoderes.Visible = false;
					pnlActualPoder.Visible = true;
					pnlGrabar.Visible = true;
				}
			}

			db.CerrarConexion();
		}
		#endregion RecuperarDatos

		#region VerSituacion HI
		private void VerSituacionHI ()
		{
			if (UrlParam.GetParam("otID") != "") 
			{
				int OtID = Convert.ToInt32(UrlParam.GetParam("otID"));

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
					btnVolverMarcas.Enabled = false;
					btnVolverPoderes.Enabled = false;
					cbDerechoPropio.Enabled = false;
				}
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
			this.asignarEventos();
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
		private void asignarEventos()
		{
			this.btnPoderActual.Click += new System.EventHandler(this.btnPoderActual_Click);
			this.eccCliente.LoadRequested += new ecWebControls.LoadRequestedHandler(this.eccCliente_LoadRequested);
			this.eccCliente.SelectedIndexChanged += new ecWebControls.SelectedIndexChangedHandler(this.eccCliente_SelectedIndexChanged);
			this.btnNuevaAtencion.Click += new System.EventHandler(this.btnNuevaAtencion_Click);
			this.btnAsignarMarcas.Click += new System.EventHandler(this.btnAsignarMarcas_Click);
			this.btnVolverMarcas.Click += new System.EventHandler(this.btnVolverMarcas_Click);
			this.btnVolverPoderes.Click += new System.EventHandler(this.btnVolverPoderes_Click);
			this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
			this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
		}

		#region Eventos Controles

		#region btnAsignarMarcas

		protected void btnAsignarMarcas_Click(object sender, System.EventArgs e)
		{
			Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca();

			string	menError	= "Atención.";
			bool	inputError	= false;

			#region Renovacion_Fill

			#region Registros

			if (this.txtRegistros.Text.Length > 0)
			{
				string [] aRegistros = this.txtRegistros.Text.Split( ((String)";, ").ToCharArray() );
				int regis=0;

				foreach( string registro in aRegistros )
				{
					if( registro.Trim() == "" ) continue;

					try	
					{
						regis	= Convert.ToInt32( registro	);
					}
					catch
					{
						//throw new Excep.Tech.UnexpectedDataType( "Error en el formato de los datos de registros introducidos");
						menError = menError + " Formato inválido para el registro:" + registro.ToString() + ".";
						inputError = true;
					}
					if (inputError == false)
					{	
						#region Asignar Parametros
						// ActaRegistroPoder
						Berke.DG.ViewTab.ActaRegistroPoder inTB =   new Berke.DG.ViewTab.ActaRegistroPoder();
						inTB.NewRow(); 
						//inTB.Dat.Acta			.Value = actaNro;   //Int32
						//inTB.Dat.Anio			.Value = actaAnio;   //Int32
						inTB.Dat.Registro		.Value = regis;   //Int32
						inTB.PostNewRow(); 
						#endregion Asignar Parametros

						Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Fill( inTB );

						#region Verificar que sea REG/REN
						if (outDG.Expediente.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO &&
							outDG.Expediente.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION)
						{
							string scriptCliente=@"<script type=""text\javascript""> displayStaticMessage( ""<span class='titulo'>Verificar Datos</span><br>Los tr&aacute;mites varios solo pueden aplicarse a trámites de Registro/Renovaci&oacute;n<br><input type='submit' class='btn_close' value='Cerrar' onclick='javascript:closeMessage()'>"" ,false);</script>";
							Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
							return;

						}
						#endregion Verificar que sea REG/REN

						if (outDG.vRenovacionMarca.Dat.MarcaAnteriorID.AsInt > 0)
						{
							for (vRM.GoTop();!vRM.EOF;vRM.Skip())
							{
								if (vRM.Dat.MarcaAnteriorID.AsInt == outDG.vRenovacionMarca.Dat.MarcaAnteriorID.AsInt)
								{
									inputError = true;
								}
							}
							if (inputError == false)
							{
								vRM.Table.Rows.Add(outDG.vRenovacionMarca.Table.Rows[0].ItemArray);
							}
						} 
						else
						{
							menError = menError + " No existe la marca con el registro:" + registro + ".";
						}
					}
					inputError = false;
				} // end foreeach


			}
			#endregion Registros

			#region Actas

			if (this.txtActas.Text.Length > 0)
			{	

				string [] aActas = this.txtActas.Text.Split( ((String)";, ").ToCharArray() );
				int actanro = 0;
				int actaanio = 0;
				string [] actacompleta;


				foreach( string acta in aActas )
				{
					actacompleta=acta.Split(((String)"/").ToCharArray(),2);
					if( acta.Trim() == "" ) continue;
					try	
					{
						actanro	= Convert.ToInt32( actacompleta[0]);
						actaanio= Convert.ToInt32( actacompleta[1]);
					}
					catch
					{
						//throw new Excep.Tech.UnexpectedDataType( "Error en el formato de los datos de actas introducidas");
						menError = menError + " Formato inválido para el acta:" + acta.ToString() + ".";
						inputError = true;
					}
					if (inputError == false)
					{
						#region Asignar Parametros
						// ActaRegistroPoder
						Berke.DG.ViewTab.ActaRegistroPoder inTB =   new Berke.DG.ViewTab.ActaRegistroPoder();
						inTB.NewRow(); 
						inTB.Dat.Acta			.Value = actanro;   //Int32
						inTB.Dat.Anio			.Value = actaanio;   //Int32
						//inTB.Dat.Registro		.Value = regis;   //Int32
						inTB.PostNewRow(); 
						#endregion Asignar Parametros


						Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Fill( inTB );

						#region Verificar que sea REG/REN
						if (outDG.Expediente.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO &&
							outDG.Expediente.Dat.TramiteID.AsInt != (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION)
						{
							string scriptCliente=@"<script type=""text\javascript""> displayStaticMessage( ""<span class='titulo'>Verificar Datos</span><br>Los tr&aacute;mites varios solo pueden aplicarse a trámites de Registro/Renovaci&oacute;n<br><br><input type='submit' class='btn_close' value='Cerrar' onclick='javascript:closeMessage()'>"" ,false);</script>";
							Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
							return;

						}
						#endregion Verificar que sea REG/REN

						if (outDG.vRenovacionMarca.Dat.MarcaAnteriorID.AsInt > 0)
						{
							for (vRM.GoTop();!vRM.EOF;vRM.Skip())
							{
								if (vRM.Dat.MarcaAnteriorID.AsInt == outDG.vRenovacionMarca.Dat.MarcaAnteriorID.AsInt)
								{
									inputError = true;
								}
							}
							if (inputError == false)
							{
								vRM.Table.Rows.Add(outDG.vRenovacionMarca.Table.Rows[0].ItemArray);
							}						
						} 
						else
						{
							menError = menError + " No existe la marca con el acta:" + acta + ".";
						}
					}
					inputError = false;
				} // end foreeach
			}

			#endregion Actas

			#endregion Renovacion_Fill

			dgMarcasAsignadas.DataSource = vRM.Table;
			dgMarcasAsignadas.DataBind();

			Session["MarcasAsignadas"] = vRM.Table;

			if (dgMarcasAsignadas.Items.Count != 0)
			{
				pnlMostrarMarcas.Visible = true;
				pnlBusquedaMarcas.Visible = false;

				#region Verificar expedientes padre en tramite
				/* ---aacuna--- 04/nov/2006
				 * En el caso en que el expediente padre se encuentre en tramite,
				 * se debe actualizar vRM con el marcaid del expediente en cuestión */
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );				
				for (vRM.GoTop(); !vRM.EOF; vRM.Skip()) {
					expe.ClearFilter();
					expe.Dat.ExpedienteID.Filter = vRM.Dat.ExpedienteID.AsInt;
					expe.Adapter.ReadAll();
					for (expe.GoTop(); !expe.EOF; expe.Skip()) {						
						if ( (expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION) |
							(expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO) ) {
                            vRM.Edit();
							vRM.Dat.MarcaAnteriorID.Value = expe.Dat.MarcaID.AsInt;
							vRM.PostEdit();
							break;
						}
					}
				}
				#endregion Verificar expedientes padre en tramite

				#region TV_FillPoderAnterior
				Berke.DG.TVDG inDG	= new Berke.DG.TVDG();
				inDG.vRenovacionMarca = vRM;
				if (UrlParam.GetParam("otID") != "") {
					inDG.OrdenTrabajo.NewRow();
					inDG.OrdenTrabajo.Dat.ID.Value = Convert.ToInt32(UrlParam.GetParam("otID"));
					inDG.OrdenTrabajo.PostNewRow();
				}
				Berke.DG.ViewTab.vPoderAnterior vPAnterior =  Berke.Marcas.UIProcess.Model.TV.FillPoderAnterior( inDG );
				dgPoderAnterior.DataSource = vPAnterior.Table;
				dgPoderAnterior.DataBind();

				Session["vPAnterior"] = vPAnterior.Table;

				pnlAnterior.Visible = true;

				#endregion TV_FillPoderAnterior

			}
			if (menError != "Atención.")
			{
				//Sucedio al menos un error
				string scriptCliente= "<script language='javascript'>alert('" + menError + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
			if ((pnlMostrarMarcas.Visible == true) && (pnlActualPoder.Visible == true))
			{
				pnlGrabar.Visible = true;
			}

			//DUPLICADO DE TITULO
			if ((int)Session["TramiteID"]== (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO)
			{
				pnlGrabar.Visible = true;
			}

			//LICENCIA
			if ((int)Session["TramiteID"]== (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA)
			{
				pnlGrabar.Visible = true;
			}
		}
		#endregion btnAsignarMarcas

		#region btnVolverMarcas

		private void btnVolverMarcas_Click(object sender, System.EventArgs e)
		{
			pnlMostrarMarcas.Visible = false;
			pnlBusquedaMarcas.Visible = true;
			pnlAnterior.Visible = false;
			pnlGrabar.Visible = false;
		}

		#endregion btnVolverMarcas

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

			#region Cliente
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente(db);
				
			if  ( outTB.Dat.ID.AsInt != 0 ) 
			{
				cliente.Adapter.ReadByID( outTB.Dat.ID.AsInt );
				lbDireccionCliente.Text = cliente.Dat.Correo.AsString;
				Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
				pais.Adapter.ReadByID( cliente.Dat.PaisID.AsInt );
				lbCiudadCliente.Text = pais.Dat.descrip.AsString;
			}

			db.CerrarConexion();
			#endregion Cliente
	
			MostrarAtencion(outTB.Dat.ID.AsInt);
		}

		private void eccCliente_SelectedIndexChanged(ecWebControls.ecCombo combo, System.EventArgs e)
		{			
			MostrarAtencion(Convert.ToInt32(eccCliente.SelectedValue));
		}

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
		}

		#endregion eccCliente

		#region btnGrabar

		private void btnGrabar_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			#region Controlar cliente múltiple	
			if ( (eccCliente.SelectedValue != "") &
				(tbNuevaAtencion.Enabled) ) {
				Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
				cliente.Adapter.ReadByID(eccCliente.SelectedValue);
				if (cliente.Dat.Multiple.AsBoolean) {
					this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("No se puede agregar atención para cliente múltiple"));
					return;
				}
			}
			#endregion Controlar cliente múltiple

			#region Obtener ID de Funcionario

			Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
			Func = UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
			int FuncionarioID = Func.Dat.ID.AsInt;

			#endregion Obtener ID de Funcionario

			#region Upsert	
			Berke.DG.TVDG inDG = new Berke.DG.TVDG();

			#region OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
			ot.InitAdapter( db );
			if (UrlParam.GetParam("OtID") == "") {
				ot.NewRow(); 
				ot.Dat.ID.Value = -1;
			} else {
				ot.Adapter.ReadByID(Convert.ToInt32(UrlParam.GetParam("OtID")));
				ot.Edit();
			}			
			ot.Dat.FuncionarioID.Value = FuncionarioID;
			ot.Dat.TrabajoTipoID.Value = Session["TramiteID"]; //OJO!! Cuidado con el ID
			ot.Dat.Facturable	.Value = this.chkFacturable.Checked;
			ot.Dat.Obs			.Value = this.txtObsClientes.Text;
			ot.Dat.ClienteID	.Value = eccCliente.SelectedValue;
			ot.Dat.AtencionID	.Value = ddlAtencion.SelectedValue;
			ot.Dat.CorrNro		.Value = this.txtCorrespNro.Text;
			ot.Dat.CorrAnio		.Value = this.txtCorrespAnio.Text;
			ot.Dat.RefCliente	.Value = this.tbReferenciaCliente.Text;
			ot.Dat.RefCorr		.Value = this.txtRefCorresp.Text;
			if (UrlParam.GetParam("OtID") == "") {
				ot.PostNewRow();
			} else {
				ot.PostEdit();
			}
			#endregion OrdenTrabajo

			#region Nueva Atencion
			if (tbNuevaAtencion.Enabled) {
				Berke.DG.DBTab.Atencion at = inDG.Atencion;
				at.NewRow();
				at.Dat.Nombre.Value = tbNuevaAtencion.Text;
				at.PostNewRow();
			}            
			#endregion Nueva Atencion

			#region Expediente_Instruccion
			Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = inDG.Expediente_Instruccion;
			//Instruccion poder
			expedienteinstr.NewRow();
			expedienteinstr.Dat.Fecha.Value = System.DateTime.Now.Date;						
			expedienteinstr.Dat.FuncionarioID.Value = FuncionarioID;
			expedienteinstr.Dat.InstruccionTipoID.Value = (int) GlobalConst.InstruccionTipo.PODER;
			expedienteinstr.Dat.Obs.Value = tbInstruccionPoder.Text;
			expedienteinstr.PostNewRow();

			//Instruccion contabilidad
			expedienteinstr.NewRow();
			expedienteinstr.Dat.Fecha.Value = System.DateTime.Now.Date;						
			expedienteinstr.Dat.FuncionarioID.Value = FuncionarioID;
			expedienteinstr.Dat.InstruccionTipoID.Value = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
			expedienteinstr.Dat.Obs.Value = tbInstruccionContabilidad.Text;
			expedienteinstr.PostNewRow();
			#endregion Expediente_Instruccion

			#region Documento
			if (pnlAsignarDocumentos.Visible == true)
			{
				inDG.ExpedienteCampo	= new Berke.DG.DBTab.ExpedienteCampo((DataTable) Session["ecampo"]);

			
				for (int i=0; i<dgAsignarDocumentos.Items.Count; i++) 
				{
					TextBox txtVal = (TextBox)dgAsignarDocumentos.Items[i].FindControl("txtValor");
					inDG.ExpedienteCampo.Go(i);
					#region Verificar campo otros propietarios para fusion
					if ( (inDG.ExpedienteCampo.Dat.Campo.AsString == GlobalConst.FUS_NOMBRE_OTROS_PROP) &
					     (txtVal.Text.Trim() == "") ) {
						this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("Debe completar el campo Nombre otros Propietarios"));
						return;
					}
					#endregion Verificar campo otros propietarios para fusion
					inDG.ExpedienteCampo.Edit();
					inDG.ExpedienteCampo.Dat.Valor.Value = txtVal.Text;
					inDG.ExpedienteCampo.PostEdit();
				}
			}
			//Session["ecampo"]=ec.Table;

			//Berke.DG.DBTab.ExpedienteCampo ec = new Berke.DG.DBTab.ExpedienteCampo( (DataTable) Session["ecampo"]);
			
			#endregion Documento

			#region Vistas

			inDG.vRenovacionMarca	= new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
			if (dgPoderActual.Items.Count != 0) inDG.vPoderActual		= new Berke.DG.ViewTab.vPoderActual((DataTable) Session["vPActual"]);
			inDG.vPoderAnterior		= new Berke.DG.ViewTab.vPoderAnterior((DataTable) Session["vPAnterior"]);
			
			#endregion Vistas

			#region Llamada al MODEL
			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.TV.Upsert( inDG );
			if (outTB.Dat.Logico.AsBoolean) {
				string pagconfirmacion = "TramitesVariosDetalle.aspx?OtID=" + outTB.Dat.Alfa.AsString;
				Response.Redirect(pagconfirmacion);
			}
			#endregion Llamada al MODEL

			#endregion Upsert
		}
		
		#endregion btnGrabar

		#region btnPoderActual

		private void btnPoderActual_Click(object sender, System.EventArgs e)
		{
			Berke.DG.ViewTab.vPoderActual vPActual = new Berke.DG.ViewTab.vPoderActual();
			string menError = "Atención.";
			bool inputError = false;

			#region txtIDPoder

			if (txtIDPoder.Text.Length > 0)
			{
					int pod = 0;
					try	{
						pod	= Convert.ToInt32(txtIDPoder.Text);
					} catch {
						menError = menError + " Formato inválido para el ID Poder:" + txtIDPoder.Text + ".";
						inputError = true;
					}

					if (inputError == false) {	
						#region Asignar Parametros
						// ActaRegistroPoder
						Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
						inTB.NewRow(); 
						//inTB.Dat.Entero		.Value = pod;   //Int32
						inTB.Dat.Alfa.Value = txtIDPoder.Text;
						/* Utilizamos el campo Logico para indicarle al action si debe recuperar
						 * los datos del propietario o del poder, dependiendo que el tramite sea
						 * por derecho propio o no. */						
						inTB.Dat.Logico.Value = cbDerechoPropio.Checked;
						inTB.PostNewRow();
						#endregion Asignar Parametros

						vPActual =  Berke.Marcas.UIProcess.Model.TV.FillPoderActual( inTB );
						if (vPActual.Dat.ID.AsInt <= 0) {
							inputError = true;
							menError = menError + " No existe el Poder o Propietario con el ID:" + pod.ToString() + ".";
						}
					}
					inputError = false;
			}
			#endregion txtIDPoder

			dgPoderActual.DataSource = vPActual.Table;
			dgPoderActual.DataBind();

			Session["vPActual"] = vPActual.Table;

			if (menError != "Atención.")
			{
				//Sucedio al menos un error
				string scriptCliente= "<script language='javascript'>alert('" + menError + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
			else {
				pnlAsignarPoderes.Visible = false;
				pnlActualPoder.Visible = true;
				if ((pnlMostrarMarcas.Visible == true) && (pnlActualPoder.Visible == true))
				{
					pnlGrabar.Visible = true;
				}
			}

		}

		#endregion btnPoderActual

		#region btnVolverPoderes
		private void btnVolverPoderes_Click(object sender, System.EventArgs e)
		{
			pnlAsignarPoderes.Visible = true;
			pnlActualPoder.Visible = false;
			pnlGrabar.Visible = false;
		
		}
		#endregion btnVolverPoderes

		#region btnSalir_Click
		private void btnSalir_Click(object sender, System.EventArgs e)
		{
            Response.Redirect("Login.aspx");
		}
		#endregion btnSalir_Click

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

		#endregion Eventos Controles
	}
}
