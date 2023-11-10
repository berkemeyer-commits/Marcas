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
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
//	using Berke.Marcas.BizActions;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;

	public partial class OrdenTrabajoDetalle : System.Web.UI.Page
	{
		#region Declaración de controles
		protected System.Web.UI.WebControls.CheckBox chkFacturable;
		#endregion

		#region Page Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{	
				if(UrlParam.GetParam("otID") != "")
				{
					MySession.ID = Convert.ToInt32(UrlParam.GetParam("otID"));	
				}
				DesplegarConfirmacion();

				#region Habilitar Borrado de HI
				btnEliminar.Enabled = false;
				btnEliminar.Visible = false;
				if ( (Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("opBorrar") == "S") &
					(Berke.Libs.Base.Acceso.OperacionPermitida("delHI")) ) 
				{
					btnEliminar.Enabled = true;
					btnEliminar.Visible = true;
				}
				#endregion Habilitar Borrado de HI
			}
		}
		#endregion

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
			this.dtlMarcaClase.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlMarcaClase_ItemDataBound);

		}
		#endregion
	
		#region DesplegarConfirmacion
		private void DesplegarConfirmacion()
		{
			int OrdenTrabajoId = 0;
			Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;
			if( UrlParam.GetParam("OtId") != "") {
				OrdenTrabajoId = Convert.ToInt32(UrlParam.GetParam("OtId"));
			}

			Berke.DG.ViewTab.vExpeMarcaHIConf vExpeMarcaHIConf = new Berke.DG.ViewTab.vExpeMarcaHIConf();
			vExpeMarcaHIConf.InitAdapter( db );
			vExpeMarcaHIConf.Dat.OrdenTrabajoID.Filter = OrdenTrabajoId;
			vExpeMarcaHIConf.Dat.Denominacion.Order = 1;
			vExpeMarcaHIConf.Dat.ClaseNro.Order = 2;
			vExpeMarcaHIConf.Adapter.ReadAll();			

			#region Obtener datos del cliente
			Berke.DG.DBTab.OrdenTrabajo OrdenTrabajo = new Berke.DG.DBTab.OrdenTrabajo();
			OrdenTrabajo.InitAdapter( db );
			OrdenTrabajo.Adapter.ReadByID ( OrdenTrabajoId );			
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente();
			cliente.InitAdapter( db );
			cliente.Adapter.ReadByID( OrdenTrabajo.Dat.ClienteID.AsInt );
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			pais.InitAdapter( db );
			pais.Dat.idpais.Filter = cliente.Dat.PaisID.AsInt;
			pais.Adapter.ReadAll();
			Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
			Func.InitAdapter( db );
			Func.Dat.ID.Filter = OrdenTrabajo.Dat.FuncionarioID.AsInt;
			Func.Adapter.ReadAll();
			lblNroHojaInicio.Text = OrdenTrabajo.Dat.Nro.AsString + "/" +
									OrdenTrabajo.Dat.Anio.AsString;
            lblCliente.Text = OrdenTrabajo.Dat.ClienteID.AsString + " - " +
							  cliente.Dat.Nombre.AsString;
            txtCorreo.Text = cliente.Dat.Correo.AsString;			
			lblCiudadPais.Text = pais.Dat.descrip.AsString;
            lblObservacion.Text = OrdenTrabajo.Dat.Obs.AsString;
            lblRefNro.Text = OrdenTrabajo.Dat.CorrNro.AsString + "/" +
							 OrdenTrabajo.Dat.CorrAnio.AsString;
            lblRefCliente.Text = OrdenTrabajo.Dat.RefCliente.AsString;
			if( OrdenTrabajo.Dat.Facturable.IsNull ) {
				lblFacturable.Text = "S/D";
			} else {
				lblFacturable.Text = OrdenTrabajo.Dat.Facturable.AsString;
			}
			#region Sustituidas
			// mbaez. Para desplegar
			Berke.DG.DBTab.CAgenteLocal ag = new Berke.DG.DBTab.CAgenteLocal(db);
			if(vExpeMarcaHIConf.Dat.Sustituida.AsBoolean)
			{
				lblSustituida.Text   = "Si";
				lbltitAgenteSustit.Visible = true;
				lblAgenteEspacio.Visible = true;
				lblAgenteSustit.Visible = true;
				ag.Adapter.ReadByID(vExpeMarcaHIConf.Dat.AgenteLocalID.AsInt);
				if (ag.RowCount==0)
				{
					lblAgenteSustit.Text = "Información no disponible";
				}
				else 
				{
					lblAgenteSustit.Text = "Matric: "+ ag.Dat.nromatricula.AsString +" "+ag.Dat.Nombre.AsString;
				}
			}
			else 
			{
				lblSustituida.Text = "NO";
			}

			#endregion Sustituidas

			lblFechaAlta.Text = OrdenTrabajo.Dat.AltaFecha.AsString;
			lblFuncionario.Text = Func.Dat.Funcionario.AsString;
			lbRuc.Text = cliente.Dat.RUC.AsString;
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
			atencion.InitAdapter( db );
			atencion.Adapter.ReadByID( OrdenTrabajo.Dat.AtencionID.AsInt );
			lblAtencion.Text = atencion.Dat.Nombre.AsString;



			
			#endregion Obtener datos del cliente

			#region Poder o Propietario
			Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
			expediente.InitAdapter( db );
			expediente.Dat.OrdenTrabajoID.Filter = OrdenTrabajoId;
			expediente.Adapter.ReadAll();
			Berke.DG.DBTab.ExpedienteXPoder ExpeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
			ExpeXpoder.InitAdapter( db );
			ExpeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			ExpeXpoder.Adapter.ReadAll();
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
			poder.InitAdapter( db );
			if (ExpeXpoder.Dat.PoderID.AsString == "") 
			{
				/* Si el expediente no tiene poder asociado, significa que es un poder por
				 * derecho propio. En ese caso, se deben recuperar los datos del propietario
				 * de la tabla ExpedienteXPropietario */

				lblDerechoPropio.Text = "Si";
				Berke.DG.DBTab.ExpedienteXPropietario ExpeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
				ExpeXpropietario.InitAdapter( db );
				ExpeXpropietario.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				ExpeXpropietario.Adapter.ReadAll();
				Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();
				propietario.InitAdapter( db );
				propietario.Adapter.ReadByID( ExpeXpropietario.Dat.PropietarioID.AsInt );
				poder.NewRow();
				poder.Dat.ID.Value = ExpeXpropietario.Dat.PropietarioID.AsString;
				poder.Dat.Denominacion.Value = propietario.Dat.Nombre.AsString;
				poder.Dat.Domicilio.Value = propietario.Dat.Direccion.AsString;
                poder.PostNewRow();				
			} else {
				poder.Adapter.ReadByID(ExpeXpoder.Dat.PoderID.AsInt);
				lblDerechoPropio.Text = "No";
			}
			#endregion Poder o Propietario

			#region Instrucciones
			Berke.DG.DBTab.Expediente_Instruccion expedienteinstr = new Berke.DG.DBTab.Expediente_Instruccion();
			expedienteinstr.InitAdapter( db );
			expedienteinstr.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
			expedienteinstr.Dat.Fecha.Order = 1;
			expedienteinstr.Adapter.ReadAll();
			lblInstruccionPoder.Text = expedienteinstr.Dat.Obs.AsString;
			expedienteinstr.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
			expedienteinstr.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
			expedienteinstr.Adapter.ReadAll();
			if (expedienteinstr.RowCount > 0) {
				lblInstruccionContabilidad.Text = expedienteinstr.Dat.Obs.AsString;
			} else {
				lblInstruccionContabilidad.Text = "";
			}
			#endregion Instrucciones

			#region Enlazar datos con grillas
			if( !OrdenTrabajo.IsEmpty ) 
			{
				Grid.Bind( dgPropietarios, poder.Table );
				pnlPropietario.Visible = true;
				dgPoderObs.DataSource = poder.Table;
				dgPoderObs.DataBind();
				pnlPoderObs.Visible = true;
			}

			dtlMarcaClase.DataSource = vExpeMarcaHIConf.Table;			
			dtlMarcaClase.DataBind();
			#endregion Enlazar datos con grillas

			#region Envío de Notificaciones
			if( UrlParam.GetParam("page") == "3") {
				db.IniciarTransaccion();
				Berke.Marcas.BizActions.Lib.Notificar( 8, PedidoString(OrdenTrabajo, vExpeMarcaHIConf), db );
				db.Commit();
				db.CerrarConexion();
			}
			#endregion Envío de Notificaciones
		}
		#endregion DesplegarConfirmacion

		#region dtlMarcaClase
		private void dtlMarcaClase_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			#region Declaración de objetos locales
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
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
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			expe.InitAdapter( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			marca.InitAdapter( db );
			#endregion Declaración de objetos locales

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label vlblLimitada = (Label) e.Item.FindControl("lblLimitada");
				Label vlblClaseLim = (Label) e.Item.FindControl("lblClaseLim");
				Label vlblGenHD = (Label) e.Item.FindControl("lblGenHD");
				Label vlblIdExpediente = (Label) e.Item.FindControl("lblIdExpediente");
				System.Web.UI.WebControls.Image vImgLogo = (System.Web.UI.WebControls.Image) e.Item.FindControl("ImgLogo");
				System.Web.UI.WebControls.Label lblLogo = (System.Web.UI.WebControls.Label) e.Item.FindControl("lblLogo");
				vlblGenHD.Text = HtmlGW.Redirect_Link(
					vlblIdExpediente.Text,
					"Ver Hoja Descriptiva",
					"DetalleHojaDescriptiva.aspx",
					"ExpedienteID");

				#region Obtener descripción de marca tipo
				Label vlblMarcaTipo			= (Label) e.Item.FindControl("lblTipo");
				marcatipo.Dat.Abrev.Filter = vlblMarcaTipo.Text;
				marcatipo.Adapter.ReadAll();
				vlblMarcaTipo.Text = marcatipo.Dat.Descrip.AsString;
				#endregion Obtener descripción de marca tipo

				#region Obtener prioridad
				Label vlblPrioridad			= (Label) e.Item.FindControl("lblPrioridad");
				Label vlblPrioridadFecha	= (Label) e.Item.FindControl("lblPrioridadFecha");
				Label vlblPrioridadPais		= (Label) e.Item.FindControl("lblPrioridadPais");
				expedientedoc.Dat.ExpedienteID.Filter = Convert.ToInt32(vlblIdExpediente.Text);
				expedientedoc.Adapter.ReadAll();
				for (expedientedoc.GoTop(); !expedientedoc.EOF; expedientedoc.Skip()) {
					documento.Adapter.ReadByID( expedientedoc.Dat.DocumentoID.AsInt );
					if (documento.Dat.DocumentoTipoID.AsInt == (int) GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD) {
						documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
						documentocampo.Dat.Campo.Filter = "Nro";
						documentocampo.Adapter.ReadAll();
						vlblPrioridad.Text = documentocampo.Dat.Valor.AsString;

						documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
						documentocampo.Dat.Campo.Filter = "Fecha";
						documentocampo.Adapter.ReadAll();
						vlblPrioridadFecha.Text = documentocampo.Dat.Valor.AsString;

						documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
						documentocampo.Dat.Campo.Filter = "Pais";
						documentocampo.Adapter.ReadAll();
						if (documentocampo.Dat.Valor.AsString != "") {
							pais.Dat.idpais.Filter = documentocampo.Dat.Valor.AsString;
							pais.Adapter.ReadAll();
							vlblPrioridadPais.Text = pais.Dat.descrip.AsString;
						}
					}
				}
				#endregion Obtener prioridad

				expe.Adapter.ReadByID(Convert.ToInt32(vlblIdExpediente.Text));
				marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
				if (marca.Dat.LogotipoID.AsInt > 0) {
					//vImgLogo.ImageUrl = "Imagen.aspx?logotipoID=" + marca.Dat.LogotipoID.AsString;
					lblLogo.Text = "<img onLoad=\"javascript:redimensionar(this);\" src=\"Imagen.aspx?logotipoID=" + marca.Dat.LogotipoID.AsString+"\">";
				} else {
					//vImgLogo.Visible = false;
					lblLogo.Visible = false;
				}

				Panel vpnlRegistro = (Panel) e.Item.FindControl("pnlRegistro");
				vpnlRegistro.Visible = true;

				if( vlblLimitada.Text == "True" ) {
					vlblClaseLim.Text = "(Limitada)";
					vlblClaseLim.ForeColor = System.Drawing.Color.Red;
					vlblClaseLim.Visible = true;
				}
			}
		}

		#endregion dtlMarcaClase
	
		#region PedidoString
		private string PedidoString(Berke.DG.DBTab.OrdenTrabajo OrdenTrabajo, Berke.DG.ViewTab.vExpeMarcaHIConf vExpeMarcaHIConf)
		{
			#region Datos Miembros

			string str;
			string HojaNro;
            string Marcas;

			#endregion Datos Miembros

			#region Asignaciones
			
			HojaNro		= OrdenTrabajo.Dat.Nro.AsString + "/" + OrdenTrabajo.Dat.Anio.AsString;

			#region Leer Marcas
			
			Marcas = string.Empty;

			for (vExpeMarcaHIConf.GoTop();!vExpeMarcaHIConf.EOF;vExpeMarcaHIConf.Skip()) {
				Marcas = Marcas + @"
					Marca           : #denominacion#

					";

				Marcas = Marcas.Replace( "#denominacion#", vExpeMarcaHIConf.Dat.Denominacion.AsString );
			}

			#endregion Leer Marcas

			#endregion Asignaciones

			str = @"
					Hoja Numero     : #HICompleta#
					{0}";

			str = str.Replace("#HICompleta#", HtmlGW.OpenPopUp_Link( "OrdenTrabajoDetalle.aspx", HojaNro, MySession.ID.ToString(), "otID" ));

			str = string.Format( str, Marcas );

			return str;
		}

		#endregion PedidoString

		#region btnEliminar_Click
		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			#region Asignar Parametros
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			inTB.Dat.Entero.Value = Convert.ToInt32(UrlParam.GetParam("otID"));
			inTB.PostNewRow();
			#endregion Asignar Parametros

			Berke.DG.ViewTab.ParamTab outTB = Berke.Marcas.UIProcess.Model.Registro.Delete( inTB );
			if (outTB.Dat.Logico.AsBoolean) 
			{				
				string scriptCliente= "<script language='javascript'>alert('La HI fue eliminada exitosamente')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			} 
			else 
			{
				string scriptCliente= "<script language='javascript'>alert('Atención. NO se pudo eliminar la HI : " + outTB.Dat.Alfa.AsString + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}		
		}
		#endregion btnEliminar_Click

		#region btnSalir_Click
		protected void btnSalir_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("login.aspx");
		}
		#endregion btnSalir_Click
	}
}
