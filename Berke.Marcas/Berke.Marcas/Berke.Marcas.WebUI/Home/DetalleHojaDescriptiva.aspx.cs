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
using System.Text.RegularExpressions;
using Berke.Libs.Base.Helpers;
using Framework.Core;

namespace Berke.Marcas.WebUI.Home
{	
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;

	public partial class DetalleHojaDescriptiva : System.Web.UI.Page
    {
        #region Constantes Globales
        private const string MODELO_NUEVO_FORM001_REG = "HI2";
        private const string MODELO_NUEVO_FORM001_REG_BLANCO = "HI2_Blanco";
        private const string MODELO_NUEVO_FORM001_REN_BLANCO = "HR2_Blanco";
        private const string MODELO_ANTERIOR_REG = "HI";
        private const string MODELO_NUEVO_FORM001_REN = "HR2";
        private const string MODELO_ANTERIOR_REN = "HR";
        private const string MODELO_NUEVO_FORM001 = "N";
        private const string MODELO_NUEVO_SOLO_DATOS = "D";
        private const string MODELO_ANTERIOR = "A";
        #endregion Constantes Globales
        #region Controles del Form

        #endregion Controles del Form

        private static int ExpedienteID;
		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( (! this.IsPostBack ) &
				 (Request.QueryString.Count >= 1 ) ) {
				MostrarDetalles(Convert.ToInt32(Request.QueryString[0]));
                ExpedienteID = Convert.ToInt32(Request.QueryString[0]);
			}
		}
		#endregion Page Load

		#region MostrarDetalles
		private void MostrarDetalles(int ExpedienteId) 
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

            #region Objetos de acceso a datos
            Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.Expediente expeAnterior = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			Berke.DG.DBTab.MarcaRegRen marcaRegRen = new Berke.DG.DBTab.MarcaRegRen();
			Berke.DG.DBTab.Tramite tramite = new Berke.DG.DBTab.Tramite();
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase();
			Berke.DG.DBTab.ClaseTipo claseTipo = new Berke.DG.DBTab.ClaseTipo();
			Berke.DG.DBTab.MarcaTipo marcaTipo = new Berke.DG.DBTab.MarcaTipo();
			Berke.DG.DBTab.ExpedienteXPoder expeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
			Berke.DG.DBTab.ExpedienteXPropietario expeXpropietario = new Berke.DG.DBTab.ExpedienteXPropietario();
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario();			
			Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder();
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
            Berke.DG.DBTab.PropietarioXVia propxVia = new Berke.DG.DBTab.PropietarioXVia();
            Berke.DG.DBTab.CCiudad ciudad = new DG.DBTab.CCiudad();
            Berke.DG.DBTab.Expediente_Documento expedientedoc = new DG.DBTab.Expediente_Documento();
            Berke.DG.DBTab.Documento documento = new DG.DBTab.Documento();
            Berke.DG.DBTab.DocumentoCampo documentocampo = new DG.DBTab.DocumentoCampo();
			Berke.DG.ViewTab.ListTab agLoc =  Berke.Marcas.UIProcess.Model.AgenteBerke.ReadForSelect();
			expe.InitAdapter( db );
			expeAnterior.InitAdapter( db );
			marca.InitAdapter( db );
			marcaRegRen.InitAdapter( db );
			tramite.InitAdapter( db );
			clase.InitAdapter( db );
			claseTipo.InitAdapter( db );
			marcaTipo.InitAdapter( db );
			expeXpoder.InitAdapter( db );
			expeXpropietario.InitAdapter( db );
			propietario.InitAdapter( db );
			poder.InitAdapter( db );
			pais.InitAdapter( db );
            propxVia.InitAdapter(db);
            ciudad.InitAdapter(db);
            expedientedoc.InitAdapter(db);
            documento.InitAdapter(db);
            documentocampo.InitAdapter(db);
			expe.Adapter.ReadByID( ExpedienteId);
			expeAnterior.Adapter.ReadByID( expe.Dat.ExpedienteID.AsInt );
			marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
			marcaRegRen.Adapter.ReadByID( expeAnterior.Dat.MarcaRegRenID.AsInt );
			tramite.Adapter.ReadByID( expe.Dat.TramiteID.AsInt );
			clase.Adapter.ReadByID( marca.Dat.ClaseID.AsInt );
			claseTipo.Adapter.ReadByID( clase.Dat.ClaseTipoID.AsInt );
			marcaTipo.Adapter.ReadByID( marca.Dat.MarcaTipoID.AsInt );
			expeXpoder.Dat.ExpedienteID.Filter = ExpedienteId;
			expeXpoder.Adapter.ReadAll();
			expeXpropietario.Dat.ExpedienteID.Filter = ExpedienteId;
			expeXpropietario.Adapter.ReadAll();
			string nombre_solicitante = "";
			string domicilio_solicitante = "";
			string pais_solicitante = "";
            string telefono_solicitante = "";
            string email_solicitante = "";
			string nro_poder = "";
            string prioridadid = "";
            string prioridad_fecha = "";
            string prioridad_pais = "";
			if (expeXpoder.RowCount > 0) {
				/* Recpuerar datos de los propietarios del poder*/
				poder.Adapter.ReadByID(expeXpoder.Dat.PoderID.AsInt);
				nombre_solicitante = poder.Dat.Denominacion.AsString;
				domicilio_solicitante = poder.Dat.Domicilio.AsString;
				pais.Dat.idpais.Filter = poder.Dat.PaisID.AsInt;
				pais.Adapter.ReadAll();
				pais_solicitante = pais.Dat.paisalfa.AsString;
                telefono_solicitante = "";
                email_solicitante = "";
                if (poder.Dat.InscripcionNro.AsString != "") {
					nro_poder = poder.Dat.InscripcionNro.AsString + "/"  + poder.Dat.InscripcionAnio.AsString;
				} else {
					nro_poder = "";
				}
			} else {
				/* Si el poder es por derecho propio, se obtienen los datos del propietario
				 * directamente de la tabla propietario a traves de expedienteXPropietario */
				propietario.Adapter.ReadByID(expeXpropietario.Dat.PropietarioID.AsInt);
				nombre_solicitante = propietario.Dat.Nombre.AsString;
				domicilio_solicitante = propietario.Dat.Direccion.AsString;
                pais.Dat.idpais.Filter = propietario.Dat.PaisID.AsInt;
				pais.Adapter.ReadAll();
				pais_solicitante = pais.Dat.paisalfa.AsString;
				nro_poder = "";

                #region Obtener teléfono del Propietario
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = expeXpropietario.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
                propxVia.Adapter.ReadAll();
                propxVia.GoTop();

                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propietario.Dat.CiudadID.AsInt);

                    telefono_solicitante = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = expeXpropietario.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    telefono_solicitante = propxVia.Dat.Descrip.AsString;
                }
                #endregion Obtener teléfono del Propietario

                #region Obtener e-mail del Propietario
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = expeXpropietario.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
                propxVia.Adapter.ReadAll();
                propxVia.GoTop();
                email_solicitante = propxVia.Dat.Descrip.AsString;
                #endregion Obtener e-mail del Propietario
            }
			lbNombreSolicitante.Text = nombre_solicitante;
			lbDomicilioSolicitante.Text = domicilio_solicitante;
			lbPaisSolicitante.Text = pais_solicitante;
            lbTelefonoSolicitante.Text = telefono_solicitante;
            lbEmailSolicitante.Text = email_solicitante;
			lbNroPoder.Text = nro_poder;
			ddlAgente.Fill( agLoc.Table, true);
			#endregion Objetos de acceso a datos
			
			lbTipoTramite.Text = tramite.Dat.Abrev.AsString;
			if (tramite.Dat.ID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)) {
			//if (tramite.Dat.Abrev.AsString == "REG") {
				lbTitulo.Text = "Hoja Descriptiva de Registro de Marcas";
				lbNroRegistroAnterior.Text = "--";
				lbFechaVencimiento.Text = "--";
			} else {
				lbTitulo.Text = "Hoja Descriptiva de Renovación de Marcas";
				lbNroRegistroAnterior.Text = marcaRegRen.Dat.RegistroNro.AsString;
				lbFechaVencimiento.Text = String.Format("{0:d}", marcaRegRen.Dat.VencimientoFecha.AsDateTime.AddYears(10));
			}
			lbDenominacion.Text = marca.Dat.Denominacion.AsString;
			lbClase.Text = clase.Dat.DescripBreve.AsString + " - " + claseTipo.Dat.Descrip.AsString;
			lbClaseNro.Text = clase.Dat.Nro.AsString;
			lbClaseTipo.Text = clase.Dat.ClaseTipoID.AsString;
			lbTipoMarca.Text = marcaTipo.Dat.Descrip.AsString;
			lbTipoMarcaId.Text = marca.Dat.MarcaTipoID.AsString;
			lbDescripClase.Text = marca.Dat.ClaseDescripEsp.AsString;			
			if (marca.Dat.LogotipoID.AsInt > 0) {
				//ImgLogotipo.ImageUrl = "Imagen.aspx?logotipoID=" + marca.Dat.LogotipoID.AsString;
				lblLogo.Text="<img src=\"Imagen.aspx?logotipoID=" + marca.Dat.LogotipoID.AsString+"\" onLoad=\"javascript:redimensionar(this);\">";
				lbLogotipoId.Text = marca.Dat.LogotipoID.AsString;
			} else {
				//ImgLogotipo.Visible = false;
				lblLogo.Visible = false;
				lbLogotipoId.Text = "";
			}

            #region Obtener prioridad
            lbNroPrioridad.Text = "--";
            lbFechaPrioridad.Text = "--";
            lbPaisPrioridad.Text = "--";
            expedientedoc.Dat.ExpedienteID.Filter = ExpedienteId;
            expedientedoc.Adapter.ReadAll();
            for (expedientedoc.GoTop(); !expedientedoc.EOF; expedientedoc.Skip())
            {
                documento.Adapter.ReadByID(expedientedoc.Dat.DocumentoID.AsInt);
                if (documento.Dat.DocumentoTipoID.AsInt == (int)GlobalConst.DocumentoTipo.DOCUMENTO_PRIORIDAD)
                {
                    documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
                    documentocampo.Dat.Campo.Filter = "Nro";
                    documentocampo.Adapter.ReadAll();
                    prioridadid = documentocampo.Dat.Valor.AsString;

                    documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
                    documentocampo.Dat.Campo.Filter = "Fecha";
                    documentocampo.Adapter.ReadAll();
                    prioridad_fecha = documentocampo.Dat.Valor.AsString;

                    documentocampo.Dat.DocumentoID.Filter = documento.Dat.ID.AsInt;
                    documentocampo.Dat.Campo.Filter = "Pais";
                    documentocampo.Adapter.ReadAll();
                    if (documentocampo.Dat.Valor.AsString != "")
                    {
                        pais.Dat.idpais.Filter = documentocampo.Dat.Valor.AsString;
                        pais.Adapter.ReadAll();
                        prioridad_pais = pais.Dat.descrip.AsString;
                    }
                }
            }
            if (prioridadid != "")
            {
                lbNroPrioridad.Text = prioridadid;
                lbFechaPrioridad.Text = prioridad_fecha;
                lbPaisPrioridad.Text = prioridad_pais;
                this.PanelPrioridad.Visible = true;
            }
            #endregion Obtener prioridad


            this.ddlTipoModelo.SelectedValue = MODELO_NUEVO_FORM001;
		}
		#endregion MostrarDetalles

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

		#region btnImprimir_Click
		protected void btnImprimir_Click(object sender, System.EventArgs e)
		{			
			if (ddlAgente.SelectedValue == "") 
			{
				this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("Antes debe seleccionar un agente local"));
				return;
			}

			#region Constantes para marcas
			const string marca_denominacion = "&lt;DENOMINACION&gt;";
            const string marca_fechavencimiento = "&lt;FECHA_VENC&gt;";
			const string marca_clase = "&lt;C&gt;";
			const string marca_producto = "&lt;P&gt;";
			const string marca_servicio = "&lt;S&gt;";
			const string marca_denominativa = "&lt;D&gt;";
			const string marca_figurativa = "&lt;F&gt;";
			const string marca_mixta = "&lt;I&gt;"; //[ggaleano 13/03/2015] Se utiliza la letra "I" para evitar que el modelo se vea mal
            const string marca_tridimensional = "&lt;T&gt;";
            const string marca_olfativa = "&lt;O&gt;";
            const string marca_sonora = "&lt;N&gt;"; //[ggaleano 13/03/2015] Se utiliza la letra "N" porque la "S" ya se utiliza como para el campo "Servicios"
			const string marca_nombre_solicitante = "&lt;NOMBRE_SOLICITANTE&gt;";
			const string marca_pais_solicitante = "&lt;PAIS_SOLICITANTE&gt;";
			const string marca_domicilio_solicitante = "&lt;DOMICILIO_SOLICITANTE&gt;";
            const string marca_telefono_solicitante = "&lt;TELEFONO_SOLICITANTE&gt;";
            const string marca_email_solicitante = "&lt;EMAIL_SOLICITANTE&gt;";
			const string marca_nombre_agente = "&lt;NOMBRE_AGENTE&gt;";
			const string marca_domicilio_agente = "&lt;DOMICILIO_AGENTE&gt;";
			const string marca_nromatricula = "&lt;NRO_MATRICULA&gt;";
			const string marca_nropoder = "&lt;NRO_PODER&gt;";
			const string marca_descripclase = "&lt;DESCRIP_CLASE&gt;";
			const string marca_nroregistro = "&lt;NRO_REGISTRO&gt;";
			const string marca_vencimiento = "&lt;FEC_VENCIMIENTO&gt;";
			const string logotipo = "&lt;LOGOTIPO&gt;";
            const string marca_fechaprioridad = "&lt;FEC_PRIORIDAD&gt;";
            const string marca_nroprioridad = "&lt;NRO_PRIORIDAD&gt;";
            const string marca_paisprioridad = "&lt;PAIS_PRIORIDAD&gt;";
            const string marca_otras = "&lt;OTRAS&gt;";
			const string tmDENOMINATIVA = "1";
			const string tmFIGURATIVA = "2";
			const string tmMIXTA = "3";
            const string tmTRIDIMENSIONAL = "4";
            const string tmOLFATIVA = "5";
            const string tmSONORA = "6";
			const string ctPRODUCTO = "1";
			const string ctSERVICIO = "2";
            const string SI = "Sí";
            const string NO = "No";
            #endregion Constantes para marcas

			#region Obtener datos de agente local
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.CAgenteLocal agente = new Berke.DG.DBTab.CAgenteLocal();
			agente.InitAdapter( db );
			agente.Dat.idagloc.Filter = ddlAgente.SelectedValue;
			agente.Adapter.ReadAll();
			string nombreAgente = agente.Dat.Nombre.AsString;
			string nroMatricula = agente.Dat.nromatricula.AsString;
			#endregion Obtener datos de agente local

            #region Configurar tipo de marca y clase
			string txtProducto = "";
			string txtServicio = "";
			switch (lbClaseTipo.Text) 
			{
                //case ctPRODUCTO : txtProducto = "XX"; break;
                //case ctSERVICIO : txtServicio = "XX"; break;
                case ctPRODUCTO: txtProducto = "X"; break;
                case ctSERVICIO: txtServicio = "X"; break;
			}
			string txtDenominativa = "";
			string txtFigurativa = "";
			string txtMixta = "";
            string txtTridimensional = "";
            string txtOlfativa = "";
            string txtSonora = "";
			switch (lbTipoMarcaId.Text) 
			{
                //case tmDENOMINATIVA : txtDenominativa = "XX"; break;
                //case tmFIGURATIVA : txtFigurativa = "XX";  break;
                //case tmMIXTA : txtMixta = "XX"; break;
                case tmDENOMINATIVA: txtDenominativa = "X"; break;
                case tmFIGURATIVA: txtFigurativa = "X"; break;
                case tmMIXTA: txtMixta = "X"; break;
                case tmTRIDIMENSIONAL: txtTridimensional = "X"; break;
                case tmOLFATIVA: txtOlfativa = "X"; break;
                case tmSONORA: txtSonora = "X"; break;
			}
			#endregion Configurar tipo de marca y clase	
                        
            #region Leer Plantilla
            Berke.DG.DBTab.DocumentoPlantilla plantilla = new Berke.DG.DBTab.DocumentoPlantilla(); 
			plantilla.InitAdapter( db ); 
			if (lbTipoTramite.Text == "REG") 
			{
                if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                    plantilla.Dat.Clave.Filter = MODELO_NUEVO_FORM001_REG;
                else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                    plantilla.Dat.Clave.Filter = MODELO_ANTERIOR_REG;
                else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                    plantilla.Dat.Clave.Filter = MODELO_NUEVO_FORM001_REG_BLANCO;
			} 
			else 
			{
                if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                    plantilla.Dat.Clave.Filter = MODELO_NUEVO_FORM001_REN;
                else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                    plantilla.Dat.Clave.Filter = MODELO_ANTERIOR_REN;
                else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                    plantilla.Dat.Clave.Filter = MODELO_NUEVO_FORM001_REN_BLANCO;
				//plantilla.Dat.Clave.Filter = "HR";
			}
			plantilla.Adapter.ReadAll();
			if ( plantilla.RowCount != 1 ) throw new Exception("Error al recuperar Plantilla HI. Cant. registros=" + plantilla.RowCount.ToString());
			string str_file = plantilla.Dat.PlantillaHTML.AsString;
			#endregion Leer Plantilla

			#region Verificar directorios para creación de archivos
			//const string dir_base = @"\\\\Trinity\\Siberk\\Dev\\BERKE.MARCA\\Code\\Berke.Marcas\\Berke.Marcas.WebUI\\Reports";
			const string dir_base = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;
			const string nom_archivo_xml = "hdr";
			string nom_archivo_img = "logotipo" + Acceso.GetCurrentUser() + ".gif";
			if (! System.IO.Directory.Exists(dir_base)) {
				System.IO.Directory.CreateDirectory(dir_base);
			}
			if (! System.IO.Directory.Exists(dir_base + "\\" + nom_archivo_xml + "_files")) {
				System.IO.Directory.CreateDirectory(dir_base + "\\" + nom_archivo_xml + "_files");
			}
			#endregion Verificar directorios para creación de archivos

			#region Logotipo
			if (lbLogotipoId.Text != "") {
				Berke.DG.DBTab.Logotipo logo = new Berke.DG.DBTab.Logotipo(db);
				logo.Adapter.ReadByID(Convert.ToInt32(lbLogotipoId.Text));
				Files.SaveBytesToFile(logo.Dat.Imagen.AsBinary, 
					dir_base + "\\" + nom_archivo_xml + "_files\\" + nom_archivo_img );
			}
			#endregion Logotipo

			#region Reemplazar marcas
			str_file = str_file.Replace(marca_denominacion, lbDenominacion.Text.Replace("&", "&amp;"));
			str_file = str_file.Replace(marca_clase, lbClaseNro.Text);
			str_file = str_file.Replace(marca_producto, txtProducto);
			str_file = str_file.Replace(marca_servicio, txtServicio);
			str_file = str_file.Replace(marca_denominativa, txtDenominativa);
			str_file = str_file.Replace(marca_figurativa, txtFigurativa);
			str_file = str_file.Replace(marca_mixta, txtMixta);
            str_file = str_file.Replace(marca_tridimensional, txtTridimensional);
            str_file = str_file.Replace(marca_olfativa, txtOlfativa);
            str_file = str_file.Replace(marca_sonora, txtSonora);
            str_file = str_file.Replace(marca_nombre_solicitante, lbNombreSolicitante.Text.Replace("&", "&amp;"));
			str_file = str_file.Replace(marca_pais_solicitante, lbPaisSolicitante.Text);
            str_file = str_file.Replace(marca_domicilio_solicitante, lbDomicilioSolicitante.Text.Replace("&", "&amp;"));
            str_file = str_file.Replace(marca_telefono_solicitante, lbTelefonoSolicitante.Text != "" ? lbTelefonoSolicitante.Text : "--");
            str_file = str_file.Replace(marca_email_solicitante, lbEmailSolicitante.Text != "" ?lbEmailSolicitante.Text : "--");
            str_file = str_file.Replace(marca_nombre_agente, nombreAgente.Replace("&", "&amp;").ToUpper());
            str_file = str_file.Replace(marca_domicilio_agente, tbDomicilioAgente.Text.Replace("&", "&amp;"));
			str_file = str_file.Replace(marca_nromatricula, nroMatricula);
			str_file = str_file.Replace(marca_nropoder, lbNroPoder.Text);
            str_file = str_file.Replace(marca_descripclase, lbDescripClase.Text.Replace("&", "&amp;"));
			str_file = str_file.Replace(marca_nroregistro, lbNroRegistroAnterior.Text);			
			str_file = str_file.Replace(marca_vencimiento, lbFechaVencimiento.Text);
            str_file = str_file.Replace(marca_vencimiento, lbFechaVencimiento.Text);
            str_file = str_file.Replace(marca_nroprioridad, lbNroPrioridad.Text);
            str_file = str_file.Replace(marca_paisprioridad, lbPaisPrioridad.Text);
            str_file = str_file.Replace(marca_fechaprioridad, lbFechaPrioridad.Text);
            str_file = str_file.Replace(marca_otras, this.PanelPrioridad.Visible ? SI : NO);
            str_file = str_file.Replace(marca_fechavencimiento, lbFechaVencimiento.Text);
			//str_file = str_file.Replace(directorio_base, dir_base + @"\\" + nom_archivo_xml + "_files" + @"\\");
            if (lbLogotipoId.Text != "")
            {
                //str_file = str_file.Replace(logotipo, /*dir_base + "\\" + nom_archivo_xml + "_files\\" +*/ nom_archivo_img);
                if ((this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001) || (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS))
                    str_file = Regex.Replace(str_file, logotipo, "<v:imagedata src=\"" + dir_base + "\\" + nom_archivo_xml + "_files\\" + nom_archivo_img + "\"/>", RegexOptions.Multiline);
                else
                    str_file = Regex.Replace(str_file, "imagedata src=\".*\"", "imagedata src=\"" + dir_base + "\\" + nom_archivo_xml + "_files\\" + nom_archivo_img + "\"", RegexOptions.Multiline);
                
            }
            else
            {
                str_file = Regex.Replace(str_file, logotipo, "", RegexOptions.Multiline);
            }
			#endregion Reemplazar marcas

			/*string nombArch =  "hdr_" + Berke.Libs.Base.Acceso.GetCurrentUser() ;//+ "_" + System.DateTime.Now.ToFileTime();
			string nombDir = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;
			string pathArchSinExt = nombDir + @"\" + nombArch;

			string pathHTML = pathArchSinExt + @".html";
			Berke.Libs.Base.Helpers.Files.SaveStringToFile(str_file, pathHTML);

			string pathURL = Berke.Libs.Base.GlobalConst.URL_REPORTES + nombArch + @".html";

			Response.Clear();
			Response.Redirect(pathURL);

			//Berke.Libs.Base.Helpers.Files.SaveStringToFile(str_file, @"c:\tmp\hd.xml");

			
			/*Berke.Libs.Base.Print2PDF print2PDF = new Berke.Libs.Base.Print2PDF(str_file);
			byte [] binaryFile = print2PDF.GetBinaryContent();
			print2PDF.finalizar();
			
			#region Activar PDF-Reader
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/pdf";
			Response.AddHeader("Content-Disposition", "attachment;filename=hoja-descriptiva.pdf" );
			Response.OutputStream.Write(binaryFile, 0, binaryFile.Length);
			Response.End();
			#endregion Activar PDF-Reader*/

            this.AuditarGeneracion();
			
			#region Enviar a word
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=hoja-descriptiva.doc");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(str_file);
			Response.End();
			#endregion Enviar a word
			
		}
		#endregion btnImprimir_Click

        private void AuditarGeneracion()
        {
            Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
            db.DataBaseName = (string)Config.GetConfigParam("CURRENT_DATABASE");
            db.ServerName = (string)Config.GetConfigParam("CURRENT_SERVER");

            Berke.DG.DBTab.Expediente exp = new DG.DBTab.Expediente(db);
            Berke.DG.DBTab.MarcaRegRen mrr = new DG.DBTab.MarcaRegRen(db);
            Berke.DG.DBTab.OrdenTrabajo ot = new DG.DBTab.OrdenTrabajo(db);
            try
            {
                db.IniciarTransaccion();
                Berke.DG.DBTab.CtrlGenHDesc cHDesc = new DG.DBTab.CtrlGenHDesc();
                cHDesc.InitAdapter(db);
                cHDesc.NewRow();
                cHDesc.Dat.ExpedienteID.Value = ExpedienteID;

                #region Expediente
                exp.ClearFilter();
                exp.Adapter.ReadByID(ExpedienteID);
                #endregion Expediente

                #region RegistroNro
                mrr.ClearFilter();
                mrr.Adapter.ReadByID(exp.Dat.MarcaRegRenID.AsInt);
                #endregion RegistroNro

                #region OrdenTrabajo
                ot.ClearFilter();
                ot.Adapter.ReadByID(exp.Dat.OrdenTrabajoID.AsInt);
                #endregion OrdenTrabajo

                if (mrr.Dat.RegistroNro.AsInt > 0)
                    cHDesc.Dat.RegistroNro.Value = mrr.Dat.RegistroNro.AsInt;
                
                cHDesc.Dat.HIAnio.Value = ot.Dat.Anio.AsInt;
                cHDesc.Dat.HINro.Value = ot.Dat.Nro.AsInt;
                cHDesc.Dat.TipoTrabajoID.Value = ot.Dat.TrabajoTipoID.AsInt;
                cHDesc.Dat.AgenteLocalID.Value = this.ddlAgente.SelectedValue;
                cHDesc.Dat.FuncionarioID.Value = WebUI.Helpers.MySession.FuncionarioID;
                cHDesc.Dat.fechahorageneracion.Value = System.DateTime.Now;
                cHDesc.PostNewRow();
                cHDesc.Adapter.InsertRow();
                cHDesc.AcceptAllChanges();
                db.Commit();
            }
            catch (Exception ex)
            {
                db.RollBack();
                throw new Exception("AuditarGeneracionHDesc", ex);
            }

        }
	}
}