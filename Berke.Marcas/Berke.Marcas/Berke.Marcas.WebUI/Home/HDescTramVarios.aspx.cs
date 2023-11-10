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
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Home
{	
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;

	public partial class HDescTramVarios : System.Web.UI.Page
    {
        #region Constantes Globales 
        private const string MODELO_NUEVO_FORM001_TR_LIC = "HTr";
        private const string MODELO_NUEVO_TR_LIC_BLANCO = "HTr_Blanco";
        private const string MODELO_NUEVO_CN_BLANCO = "HD_CN_Blanco";
        private const string MODELO_NUEVO_CD_BLANCO = "HD_CD_Blanco";
        private const string MODELO_NUEVO_FU_BLANCO = "HD_FU_Blanco";
        private const string MODELO_ANTERIOR_1 = "HD_TVarios_Conced1";
        private const string MODELO_ANTERIOR_2 = "HD_TVarios_Conced2";
        private const string MODELO_NUEVO_FORM001_CN = "HD_CN";
        private const string MODELO_NUEVO_FORM001_CD = "HD_CD";
        private const string MODELO_NUEVO_FORM001_FU = "HD_FU";
        
        private const string MODELO_NUEVO_FORM001 = "N";
        private const string MODELO_NUEVO_SOLO_DATOS = "D";
        private const string MODELO_ANTERIOR = "A";
        private const int IDIOMA_ESPANOL = 2;

        #endregion Constantes Globales

		Berke.Libs.Base.Helpers.AccesoDB db;
		Berke.DG.DBTab.OrdenTrabajo OrdenTrabajo;
		Berke.DG.DBTab.Expediente Expediente;
		Berke.DG.DBTab.ExpedienteCampo ExpedienteCampo;
		Berke.DG.DBTab.Marca Marca;
		Berke.DG.DBTab.MarcaRegRen MarcaRegRen;
		Berke.DG.DBTab.ExpedienteXPoder ExpedienteXPoder;
		Berke.DG.DBTab.Poder Poder;
		Berke.DG.DBTab.CAgenteLocal CAgenteLocal;
		Berke.DG.DBTab.Clase Clase;
        Berke.DG.DBTab.CPais pais;
        Berke.DG.DBTab.PropietarioXVia propxVia;
        Berke.DG.DBTab.CCiudad ciudad;
        Berke.DG.DBTab.PropietarioXPoder propxPod;
        Berke.DG.DBTab.Propietario propietario;
		protected System.Web.UI.WebControls.Panel Panel2;
		protected System.Web.UI.WebControls.Panel Panel3;
		protected System.Web.UI.WebControls.Panel Panel4;
		protected System.Web.UI.WebControls.Panel PanelDatosSegTitular;
		private static int TipoTrabajo;
		private static int RegistroNro;
        private static int PoderID;
		private static string DenominacionMarca;
		private static int ExpedientePadre;
        private static int HI_NroG;
        private static int HI_AnioG;
        //private bool ReadyState;
		//PDFCreator.clsPDFCreator pdfcreator;
					
		#region Controles del Form
		
		#endregion Controles del Form
	
		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			inicializar();
			if ( (! this.IsPostBack ) &
				(Request.QueryString.Count >= 1 ) ) 
			{ 
				MostrarDetalles(Convert.ToInt32(Request.QueryString[0]), Convert.ToInt32(Request.QueryString[1]),
					Convert.ToInt32(Request.QueryString[2]), Convert.ToInt32(Request.QueryString[3]));
                
			}
		}
		#endregion Page Load

		#region Mostrar Detalles		
		private void MostrarDetalles(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			Berke.DG.ViewTab.ListTab agLoc =  Berke.Marcas.UIProcess.Model.AgenteBerke.ReadForSelect();
			ddlAgente.Fill(agLoc.Table, true);

			this.LimpiarTexts();

			#region Recuperar datos comunes para trámites varios
				OrdenTrabajo.Dat.Nro.Filter = HI_Nro;
				OrdenTrabajo.Dat.Anio.Filter = HI_Anho;
				OrdenTrabajo.Adapter.ReadAll();

				#region Asignar datos Marca
				Expediente.Adapter.ReadByID(ExpedienteID);
            	Marca.Adapter.ReadByID(Expediente.Dat.MarcaID.AsInt);
			    DenominacionMarca = Marca.Dat.Denominacion.AsString;
				lbDenominacion.Text = '"' + Marca.Dat.Denominacion.AsString + '"';
				lbDenominacionTr.Text = '"' + Marca.Dat.Denominacion.AsString + '"';
			    Clase.Adapter.ReadByID(Marca.Dat.ClaseID.AsInt);
				lbClase.Text = Clase.Dat.Nro.AsString;
				lbClasificacionTr.Text = Clase.Dat.Nro.AsString;
			    //Expediente.ClearFilter();
				#endregion Asignar datos Marca

				#region Asignar Datos Registro
				//MarcaRegRen.Dat.ExpedienteID.Filter = ExpedienteID;//Expediente.Dat.ExpedienteID.AsInt;
				MarcaRegRen.Adapter.ReadByID(Expediente.Dat.MarcaRegRenID.AsInt);
			    
				//MarcaRegRen.Adapter.ReadAll();
				lbAnho.Text = MarcaRegRen.Dat.RegistroAnio.AsString;
			    RegistroNro = MarcaRegRen.Dat.RegistroNro.AsInt;
				lbNro.Text = MarcaRegRen.Dat.RegistroNro.AsString;
                lbNroRegistro.Text = MarcaRegRen.Dat.RegistroNro.AsString;
                lbNroExpediente.Text = ExpedienteID.ToString();
				lbNroRegistroTr.Text = MarcaRegRen.Dat.RegistroNro.AsString;
                lbFechaRegistro.Text = MarcaRegRen.Dat.ConcesionFecha.AsDateTime.ToString("dd/MM/yyyy");
				lbFechaRegistroTr.Text = MarcaRegRen.Dat.ConcesionFecha.AsDateTime.ToString("dd/MM/yyyy");
			    Expediente.ClearFilter();
				#endregion Asignar Datos Registro

				Expediente.Dat.ExpedienteID.Filter = ExpedienteID;
				Expediente.Dat.TramiteID.Filter = TipoTrabajoID;
				Expediente.Dat.OrdenTrabajoID.Filter = OrdenTrabajo.Dat.ID.AsInt;
				Expediente.Adapter.ReadAll();

				#region Asignar Agente por defecto

				Berke.Libs.Boletin.Services.AgenteLocalService agLocalService = new Berke.Libs.Boletin.Services.AgenteLocalService(this.db);	

				if (agLocalService.isAgenteNuestro(Expediente.Dat.AgenteLocalID.AsString))
				{
					ddlAgente.SelectedValue = Expediente.Dat.AgenteLocalID.AsString;
				}

			/*CAgenteLocal.Adapter.ReadByID(Expediente.Dat.AgenteLocalID.AsInt);
			    nombreApoderado = CAgenteLocal.Dat.Nombre.AsString;
			    nromatricApoderado = CAgenteLocal.Dat.nromatricula.AsString;*/
				#endregion Asignar Agente por defecto
				

				#region Asignar Nro. Poder
				ExpedienteXPoder.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
				ExpedienteXPoder.Adapter.ReadAll();
				Poder.Adapter.ReadByID(ExpedienteXPoder.Dat.PoderID.AsInt);
                PoderID = Poder.Dat.ID.AsInt;
                if (Poder.Dat.InscripcionNro.AsString != "") 
				{
					lbNroPoder.Text = Poder.Dat.InscripcionNro.AsString + "/" + Poder.Dat.InscripcionAnio.AsString;
                    
				}
				
				#endregion Asignar Nro. Poder

				ExpedienteCampo.ClearFilter();

			#endregion Recuperar datos comunes para trámites varios

			ExpedientePadre = ExpedienteID;
			TipoTrabajo = TipoTrabajoID;
            HI_NroG = HI_Nro;
            HI_AnioG = HI_Anho;

			if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - TRANSFERENCIA";
				DetTransferencia(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
			else if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - CAMBIO DE NOMBRE";
				DetCambioNombre(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
			else if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.FUSION))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - FUSION";
				DetFusion(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
			else if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - CAMBIO DE DOMICILIO";
				DetCambioDomicilio(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
			else if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - LICENCIA";
				DetLicencia(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
			else if (TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC))
			{
				lbTituloHDesc.Text = "HOJA DESCRIPTIVA - CAMBIO DE NOMBRE Y DOMICILIO";
				rblTipoDocumento.Visible = true;
				DetCambioNombreyDomicilio(ExpedienteID, TipoTrabajoID, HI_Nro, HI_Anho);
			}
		}
		#endregion MostrarDetalles
			
		private void DetCambioNombre(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			#region Ocultar paneles que no se utilizan para el trámite
			PanelSegTitular.Visible = false;
			PanelNvoDom.Visible = false;
			#endregion Ocultar paneles que no se utilizan para el trámite

			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

			#region Propietario Anterior
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior
			
			#region Propietario Anterior Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior Dirección

			#region Propietario Anterior País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

            #region Obtener teléfono del Propietario Anterior
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_ID;
            ExpedienteCampo.Adapter.ReadAll();
            int PropietarioActualID = ExpedienteCampo.Dat.Valor.AsString != String.Empty ? Convert.ToInt32(ExpedienteCampo.Dat.Valor.AsString) : 0;

            lbTelefono.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefono.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario Anterior

            #region Obtener e-mail del Propietario Anterior
            lbEmail.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmail.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario Anterior

			#region Propietario Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual

			#region Propietario Actual Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual Dirección

			#region Propietario Actual País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País

            #region Obtener teléfono del Propietario Actual
            propxPod.ClearFilter();
            propxPod.Dat.PoderID.Filter = PoderID;
            propxPod.Adapter.ReadAll();
            propxPod.GoTop();

            lbTelefonoCN.Text = "--";

            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefonoCN.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario  Actual

            #region Obtener e-mail del Propietario Actual
            lbEmailCN.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmailCN.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario Actual

            if (lbNroRegistro.Text == "--" && lbNroExpediente.Text == "--")
            {
                PanelDatosRegistroPriTitular.Visible = false;
            }
				
		    
		}
		
		private void DetCambioDomicilio(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			#region Ocultar paneles que no se utilizan para el trámite
			PanelSegTitular.Visible = false;
			PanelNombAdq.Visible = false;
			#endregion Ocultar paneles que no se utilizan para el trámite

			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

			#region Propietario
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior
			
			#region Propietario Dirección Anterior
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
            lbDomicilioAnterior.Text = ExpedienteCampo.Dat.Valor.AsString;  
			#endregion Propietario Anterior Dirección

			#region Propietario País 
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

			#region Propietario Dirección Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioNuevo.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion

            #region Propietario País Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCD.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

			if (lbNroRegistro.Text == "--" && lbNroExpediente.Text == "--")
			{
				PanelDatosRegistroPriTitular.Visible = false;
			}
			
		}

		private void DetFusion(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			
			#region Ocultar paneles que no se utilizan para el trámite
			PanelNvoDom.Visible = false;
			#endregion Ocultar paneles que no se utilizan para el trámite


			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

            #region 1° Titular
            #region Propietario 1° Titular
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario 1° Titular

            #region Propietario Domicilio 1° Titular
            ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Domicilio 1° Titular

			#region Propietario País 1° Titular
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario País 1° Titular

            #region Obtener teléfono del Propietario 1° Titular
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_ID;
            ExpedienteCampo.Adapter.ReadAll();
            int PropietarioActualID = ExpedienteCampo.Dat.Valor.AsString != String.Empty ?Convert.ToInt32(ExpedienteCampo.Dat.Valor.AsString) : 0;

            lbTelefono.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefono.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario 1° Titular

            #region Obtener e-mail del Propietario 1° Titular
            lbEmail.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmail.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario 1° Titular
            #endregion 1° Titular

            #region 2° Titular
            #region Propietario 2° Titular
            //ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.FUS_NOMBRE_OTROS_PROP;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre2.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString; 
			#endregion Propietario 2° Titular

			#region Propietario Domicilio 2° Titular
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			//ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.FUS_DIR_OTROS_PROP;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio2.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Domicilio 2° Titular

			#region Propietario País 2° Titular
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais2.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario País 2° Titular

            #region Obtener teléfono del Propietario 2° Titular
            propxPod.ClearFilter();
            propxPod.Dat.PoderID.Filter = PoderID;
            propxPod.Adapter.ReadAll();
            propxPod.GoTop();

            lbTelefono2.Text = "--";
            lbTelefonoCN.Text = "--";

            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefono2.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                    lbTelefonoCN.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefono2.Text = propxVia.Dat.Descrip.AsString;
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefono2.Text = propxVia.Dat.Descrip.AsString;
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario 2° Titular

            #endregion 2° Titular


            #region Propietario Actual
            #region Propietario Actual
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
            ExpedienteCampo.Adapter.ReadAll();
            lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
            #endregion Propietario Actual

            #region Propietario Actual Domicilio
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
            ExpedienteCampo.Adapter.ReadAll();
            lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
            #endregion Propietario Actual Domicilio

            #region Propietario Actual País
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
            ExpedienteCampo.Adapter.ReadAll();
            lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
            #endregion Propietario Actual País

            #region Obtener Teléfono del Propietario Actual
            //propxPod.ClearFilter();
            //propxPod.Dat.PoderID.Filter = PoderID;
            //propxPod.Adapter.ReadAll();
            //propxPod.GoTop();

            lbTelefonoCN.Text = "--";

            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefonoCN.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario 2° Titular
            
            #region Obtener e-mail del Propietario Actual
            lbEmailCN.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmailCN.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario Actual

            #endregion Propietario Actual


            if (lbNroRegistro.Text == "--" && lbNroExpediente.Text == "--")
			{
				PanelDatosRegistroPriTitular.Visible = false;
			}
			
			if (lbDenominacion2.Text == "--") 
			{
				PanelDatosMarcaSegTitular.Visible = false;
			}

			if (lbNroRegistro2.Text == "--" && lbNroExpediente2.Text == "--")
			{
				PanelDatosRegistroSegTitular.Visible = false;
			}
		}

		private void DetTransferencia(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			#region Ocultar paneles que no se utilizan para el trámite
			PanelSegTitular.Visible = false;
			PanelNvoDom.Visible = false;
			PanelDatosMarcaPriTitular.Visible = false;
			PanelDatosRegistroPriTitular.Visible = false;
			PanelDenominacion.Visible = true;
			PanelRegistroTransferencia.Visible = true;
			lblTitulo1.Text = "PROPIETARIO ACTUAL";
			lblTitulo3.Text = "SE SOLICITA TRANSFERIR A";
			#endregion Ocultar paneles que no se utilizan para el trámite

			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

			#region Propietario Actual
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual

			#region Propietario Actual Domicilio 
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual Domicilio 

			#region Propietario Actual País 
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País

            #region Obtener teléfono del Propietario Actual
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_ID;
            ExpedienteCampo.Adapter.ReadAll();
            int PropietarioActualID = ExpedienteCampo.Dat.Valor.AsString != String.Empty ? Convert.ToInt32(ExpedienteCampo.Dat.Valor.AsString) : 0;

            lbTelefono.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefono.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario Actual

            #region Obtener e-mail del Propietario Actual
            lbEmail.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmail.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario Actual

			#region Propietario Anterior 
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
            #endregion Propietario Anterior Anterior

            #region Propietario Anterior Domicilio
            ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior Domicilio

			#region Propietario Anterior País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

            #region Obtener teléfono del Propietario Anterior
            propxPod.ClearFilter();
            propxPod.Dat.PoderID.Filter = PoderID;
            propxPod.Adapter.ReadAll();
            propxPod.GoTop();

            lbTelefonoCN.Text = "--";

            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();
            
            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefonoCN.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario  Anterior

            #region Obtener e-mail del Propietario Anterior
            lbEmailCN.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmailCN.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario

		}

		private void DetLicencia(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			#region Ocultar paneles que no se utilizan para el trámite
			PanelSegTitular.Visible = false;
			PanelNvoDom.Visible = false;
			PanelDatosMarcaPriTitular.Visible = false;
			PanelDatosRegistroPriTitular.Visible = false;
			PanelDenominacion.Visible = true;
			PanelRegistroTransferencia.Visible = true;
			lblTitulo1.Text = "PROPIETARIO ACTUAL";
			lblTitulo3.Text = "SE SOLICITA LICENCIAR A FAVOR DE";
			#endregion Ocultar paneles que no se utilizan para el trámite

			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

			#region Propietario Actual
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual

			#region Propietario Actual Domicilio 
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual Domicilio 

			#region Propietario Actual País 
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País

            #region Obtener teléfono del Propietario Actual
            ExpedienteCampo.ClearFilter();
            ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
            ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_ID;
            ExpedienteCampo.Adapter.ReadAll();
            int PropietarioActualID = ExpedienteCampo.Dat.Valor.AsString != String.Empty ? Convert.ToInt32(ExpedienteCampo.Dat.Valor.AsString) : 0;

            lbTelefono.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefono.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefono.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Propietario Actual

            #region Obtener e-mail del Propietario Actual
            lbEmail.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = PropietarioActualID;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmail.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Propietario Actual

			#region Licenciatario 
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.LIC_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Licenciatario 

			#region Licenciatario Domicilio
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.LIC_DIRECCION;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Licenciatario Domicilio

			#region Licenciatario País
			/*ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;*/
			#endregion Licenciatario País

            #region Obtener teléfono del Licenciatario
            propxPod.ClearFilter();
            propxPod.Dat.PoderID.Filter = PoderID;
            propxPod.Adapter.ReadAll();
            propxPod.GoTop();

            lbTelefonoCN.Text = "--";

            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.TELEFONO;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                if (propxVia.Dat.Descrip.AsString != "")
                {
                    ciudad.ClearFilter();
                    ciudad.Adapter.ReadByID(propxPod.Dat.PropietarioID.AsInt);

                    lbTelefonoCN.Text = "+" + pais.Dat.paistel.AsString + ciudad.Dat.codciudad.AsString + propxVia.Dat.Descrip.AsString;
                }
                else
                {
                    propxVia.ClearFilter();
                    propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                    propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                    propxVia.Adapter.ReadAll();
                    propxVia.GoTop();

                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            else
            {
                propxVia.ClearFilter();
                propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
                propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.CELULAR;
                propxVia.Adapter.ReadAll();

                if (propxVia.RowCount > 0)
                {
                    propxVia.GoTop();
                    lbTelefonoCN.Text = propxVia.Dat.Descrip.AsString;
                }
            }
            #endregion Obtener teléfono del Licenciatario

            #region Obtener e-mail del Licenciatario
            lbEmailCN.Text = "--";
            propxVia.ClearFilter();
            propxVia.Dat.PropietarioID.Filter = propxPod.Dat.PropietarioID.AsInt;
            propxVia.Dat.ViaID.Filter = (int)GlobalConst.Vias.EMAIL;
            propxVia.Adapter.ReadAll();

            if (propxVia.RowCount > 0)
            {
                propxVia.GoTop();
                lbEmailCN.Text = propxVia.Dat.Descrip.AsString;
            }
            #endregion Obtener e-mail del Licenciatario

        }


		private void DetCambioNombreyDomicilio(int ExpedienteID, int TipoTrabajoID, int HI_Nro, int HI_Anho)
		{
			#region Ocultar paneles que no se utilizan para el trámite
			PanelSegTitular.Visible = false;
			PanelNvoDom.Visible = false;
			lblTitulo3.Text = "NOMBRE QUE ADQUIERE Y NUEVO DOMICILIO";
			#endregion Ocultar paneles que no se utilizan para el trámite

			Expediente.ClearFilter();
			Expediente.Dat.ExpedienteID.Filter = ExpedienteID;
			Expediente.Dat.TramiteID.Filter = Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE);
			Expediente.Dat.OrdenTrabajoID.Filter = OrdenTrabajo.Dat.ID.AsInt;
			Expediente.Adapter.ReadAll();
			
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Adapter.ReadAll();

			#region Propietario Anterior
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior
			
			#region Propietario Anterior Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbDomicilioAnterior.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior Dirección

			#region Propietario Anterior País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

			#region Propietario Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual

			#region Propietario Actual Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbDomicilioNuevo.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual Dirección

			#region Propietario Actual País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbPaisCD.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País

/*
			#region Propietario Anterior
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombre.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior
			
			#region Propietario Anterior Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilio.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior Dirección

			#region Propietario Anterior País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPais.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Anterior País

			#region Propietario Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_NOMBRE;
			ExpedienteCampo.Adapter.ReadAll();
			lbNombreCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual

			#region Propietario Actual Dirección
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ANTERIOR_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			lbDomicilioAnterior.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual Dirección

			#region Propietario Dirección Actual
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_DIR;
			ExpedienteCampo.Adapter.ReadAll();
			lbDomicilioNuevo.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion

			#region Propietario Actual País
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCN.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País
	
			#region Propietario Actual País Cambio Domicilio
			ExpedienteCampo.ClearFilter();
			ExpedienteCampo.Dat.ExpedienteID.Filter = Expediente.Dat.ID.AsInt;
			ExpedienteCampo.Dat.Campo.Filter = Berke.Libs.Base.GlobalConst.PROP_ACTUAL_PAIS;
			ExpedienteCampo.Adapter.ReadAll();
			lbPaisCD.Text = ExpedienteCampo.Dat.Valor.AsString;
			#endregion Propietario Actual País Cambio Domicilio*/

			if (lbNroRegistro.Text == "--" && lbNroExpediente.Text == "--")
			{
				PanelDatosRegistroPriTitular.Visible = false;
			}
				
		    
		}
		


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

		protected void btnImprimir_Click(object sender, System.EventArgs e)
		{
            if (ddlAgente.SelectedValue == "")
            {
                this.RegisterClientScriptBlock("key", Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script("Antes debe seleccionar un agente local"));
                return;
            }

			if (RegistroNro > 0)
			{
				GenerarDocConcedidas();
			}
			else
			{
				GenerarDocEnTramite();
			}
           
		}

        private void AuditarGeneracion()
        {
            try
            {
                db.IniciarTransaccion();
                Berke.DG.DBTab.CtrlGenHDesc cHDesc = new DG.DBTab.CtrlGenHDesc();
                cHDesc.InitAdapter(this.db);
                cHDesc.NewRow();
                cHDesc.Dat.ExpedienteID.Value = ExpedientePadre;
                if (this.lbNroRegistroTr.Text != "")
                    cHDesc.Dat.RegistroNro.Value = Convert.ToInt32(this.lbNroRegistroTr.Text);
                else
                    cHDesc.Dat.RegistroNro.Value = System.DBNull.Value;
                cHDesc.Dat.HIAnio.Value = HI_AnioG;
                cHDesc.Dat.HINro.Value = HI_NroG;
                cHDesc.Dat.TipoTrabajoID.Value = TipoTrabajo;
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

		private void inicializar()
		{
			db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			#region Crear DBTabs a utilizar
			OrdenTrabajo = new Berke.DG.DBTab.OrdenTrabajo(db);
			Expediente = new Berke.DG.DBTab.Expediente(db);
			ExpedienteCampo = new Berke.DG.DBTab.ExpedienteCampo(db);
			Marca = new Berke.DG.DBTab.Marca(db);
			MarcaRegRen = new Berke.DG.DBTab.MarcaRegRen(db);
			ExpedienteXPoder = new Berke.DG.DBTab.ExpedienteXPoder(db);
			Poder = new Berke.DG.DBTab.Poder(db);
			CAgenteLocal = new Berke.DG.DBTab.CAgenteLocal(db);
			Clase = new Berke.DG.DBTab.Clase(db);
            pais = new DG.DBTab.CPais(db);
            propxVia = new DG.DBTab.PropietarioXVia(db);
            ciudad = new DG.DBTab.CCiudad(db);
            propxPod = new DG.DBTab.PropietarioXPoder(db);
            propietario = new DG.DBTab.Propietario(db);
			#endregion Crear DBTabs a utilizar
		}

		private void LimpiarTexts()
		{
			lbNombre.Text			= "--";
			lbDomicilio.Text		= "--";
            lbTelefono.Text         = "--";
            lbEmail.Text            = "--";
			lbDenominacion.Text		= "--";
			lbClase.Text			= "--";
			lbNroRegistro.Text		= "--";
            lbFechaRegistro.Text    = "--";
			lbNroExpediente.Text	= "--";
			lbPais.Text				= "--";
			lbAnho.Text				= "--";
			lbNro.Text				= "--";
			lbNombre2.Text			= "--";
			lbDomicilio2.Text		= "--";
			lbDenominacion2.Text	= "--";
			lbClase2.Text			= "--";
			lbNroRegistro2.Text		= "--";
			lbNroExpediente2.Text	= "--";
			lbPais2.Text			= "--";
			lbAnho2.Text			= "--";
			lbNro2.Text				= "--";
            lbTelefono2.Text        = "--";
            lbEmail2.Text           = "--";
			lbNroPoder.Text			= "--";
			lbNombreCN.Text			= "--";
			lbDomicilioCN.Text		= "--";
            lbTelefonoCN.Text       = "--";
            lbEmailCN.Text          = "--";
			lbPaisCN.Text			= "--";
			lbDomicilioAnterior.Text= "--";
			lbDomicilioNuevo.Text	= "--";
			lbPaisCD.Text			= "--";
			lbNroRegistroTr.Text    = "--";
			lbFechaRegistroTr.Text  = "--";
			lbAreaTr.Text			= "--";
			lbClasificacionTr.Text  = "--";
		}
		

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion

        
		private void GenerarDocConcedidas()
		{
			#region Obtener plantilla 

			string plantilla = "";
			bool CN_CD = false;
            int TipoTrabajoID = TipoTrabajo;

            switch (TipoTrabajo)
			{
				case (int)GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA:
                case (int)GlobalConst.Marca_Tipo_Tramite.LICENCIA:
                    if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_TR_LIC, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_2, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_TR_LIC_BLANCO, IDIOMA_ESPANOL);
                    break;
				case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE:
                    if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_CN, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_1, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_CN_BLANCO, IDIOMA_ESPANOL);
                    break;
				case (int)GlobalConst.Marca_Tipo_Tramite.FUSION:
                    if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_FU, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_1, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FU_BLANCO, IDIOMA_ESPANOL);
                    break;
				case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO:
                    if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_CD, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_1, IDIOMA_ESPANOL);
                    else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                        plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_CD_BLANCO, IDIOMA_ESPANOL);
                    break;
				case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC:
                    TipoTrabajoID = Convert.ToInt32(rblTipoDocumento.SelectedValue);

                    if (TipoTrabajoID == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE)
                    {
                        if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_CN, IDIOMA_ESPANOL);
                        else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_1, IDIOMA_ESPANOL);
                        else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_CN_BLANCO, IDIOMA_ESPANOL);
                    }
                    else if (TipoTrabajoID == (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO)
                    {
                        if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_FORM001_CD, IDIOMA_ESPANOL);
                        else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_ANTERIOR_1, IDIOMA_ESPANOL);
                        else if (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS)
                            plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern(MODELO_NUEVO_CD_BLANCO, IDIOMA_ESPANOL);
                    }

					CN_CD = true;
					break;
			}

			if( plantilla == "" )
			{
				this.ShowMessage( "Error con la plantilla" );
				return ;
			}
			#endregion Obtener plantilla

            #region Obtener datos Apoderado
            CAgenteLocal.Adapter.ReadByID(Convert.ToInt32(ddlAgente.SelectedValue));
            #endregion Obtener datos Apoderado

            if ((this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_FORM001) || (this.ddlTipoModelo.SelectedValue == MODELO_NUEVO_SOLO_DATOS))
            {
                #region Modelo 2015

                #region Constantes para marcas
                //Transferencia
                const string marca_transferencia = "&lt;T&gt;";
                const string marca_licencia = "&lt;L&gt;";
                const string marca_denominacion = "&lt;denominacion&gt;";
                const string marca_registro = "&lt;registro&gt;";
                const string marca_fecha = "&lt;fecha&gt;";
                const string marca_clase = "&lt;clase&gt;";
                const string marca_nombre = "&lt;nombre&gt;";
                const string marca_telefono = "&lt;telefono&gt;";
                const string marca_domicilio = "&lt;domicilio&gt;";
                const string marca_email = "&lt;email&gt;";
                const string marca_pais = "&lt;pais&gt;";
                const string marca_nombre_trans = "&lt;nombre_trans&gt;";
                const string marca_telefono_trans = "&lt;telefono_trans&gt;";
                const string marca_domicilio_trans = "&lt;domicilio_trans&gt;";
                const string marca_email_trans = "&lt;email_trans&gt;";
                const string marca_pais_trans = "&lt;pais_trans&gt;";
                const string marca_nomb_apoderado = "&lt;nomb_apoderado&gt;";
                const string marca_domic_apoderado = "&lt;domic_apoderado&gt;";
                const string marca_poder = "&lt;poder&gt;";
                const string marca_matricula = "&lt;matricula&gt;";
                //Cambio de Nombre
                const string marca_marca_pri_titular = "&lt;marca_pri_titular&gt;";
                const string marca_nreg_pri_titular = "&lt;nreg_pri_titular&gt;";
                const string marca_freg_pri_titular = "&lt;freg_pri_titular&gt;";
                const string marca_clase_pri_titular = "&lt;clase_pri_titular&gt;";
                const string marca_nombre_pri_titular = "&lt;nombre_pri_titular&gt;";
                const string marca_telefono_pri_titular = "&lt;telefono_pri_titular&gt;";
                const string marca_dom_pri_titular = "&lt;dom_pri_titular&gt;";
                const string marca_email_pri_titular = "&lt;email_pri_titular&gt;";
                const string marca_pais_pri_titular = "&lt;pais_pri_titular&gt;";
                const string marca_nombre_cn = "&lt;nombre_cn&gt;";
                const string marca_domicilio_cn = "&lt;domicilio_cn&gt;";
                const string marca_telefono_cn = "&lt;telefono_cn&gt;";
                const string marca_email_cn = "&lt;email_cn&gt;";
                const string marca_pais_cn = "&lt;pais_cn&gt;";
                //Cambio de Domicilio
                const string marca_domicilio_anterior_cd = "&lt;domicilio_anterior_cd&gt;";
                const string marca_domicilio_nuevo_cd = "&lt;domicilio_nuevo_cd&gt;";
                const string marca_pais_cd = "&lt;pais_cd&gt;";
                //Fusión
                const string marca_nombre_seg_titular = "&lt;nombre_seg_titular&gt;";
                const string marca_telefono_seg_titular = "&lt;telefono_seg_titular&gt;";
                const string marca_dom_seg_titular = "&lt;dom_seg_titular&gt;";
                const string marca_email_seg_titular = "&lt;email_seg_titular&gt;";
                const string marca_pais_seg_titular = "&lt;pais_seg_titular&gt;";
                #endregion Constantes para marcas

                switch (TipoTrabajoID)
                {
                    case (int)GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA:
                    case (int)GlobalConst.Marca_Tipo_Tramite.LICENCIA:
                        #region Transferencia e Inscripción de Licencia
                        #region Reemplazar marcas
                        plantilla = plantilla.Replace(marca_transferencia, TipoTrabajo == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA) ? "X" : "");
                        plantilla = plantilla.Replace(marca_licencia, TipoTrabajo == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA) ? "X" : "");
                        plantilla = plantilla.Replace(marca_registro, lbNroRegistroTr.Text);
                        plantilla = plantilla.Replace(marca_fecha, lbFechaRegistroTr.Text);
                        plantilla = plantilla.Replace(marca_clase, lbClasificacionTr.Text);
                        plantilla = plantilla.Replace(marca_denominacion, lbDenominacion.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_nombre, lbNombre.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domicilio, lbDomicilio.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_pais, lbPais.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono, lbTelefono.Text);
                        plantilla = plantilla.Replace(marca_email, lbEmail.Text);
                        plantilla = plantilla.Replace(marca_nomb_apoderado, CAgenteLocal.Dat.Nombre.AsString);
                        plantilla = plantilla.Replace(marca_domic_apoderado, tbDomicilioAgente.Text);
                        plantilla = plantilla.Replace(marca_matricula, CAgenteLocal.Dat.nromatricula.AsString);
                        plantilla = plantilla.Replace(marca_poder, lbNroPoder.Text);
                        plantilla = plantilla.Replace(marca_nombre_trans, lbNombreCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domicilio_trans, lbDomicilioCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_pais_trans, lbPaisCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_trans, lbTelefonoCN.Text);
                        plantilla = plantilla.Replace(marca_email_trans, lbEmailCN.Text);
                        #endregion Reemplazar marcas
                        #endregion Transferencia e Inscripción de Licencia
                        break;
                    case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE:
                        #region Cambio de Nombre
                        plantilla = plantilla.Replace(marca_marca_pri_titular, lbDenominacion.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_nreg_pri_titular, lbNroRegistro.Text);
                        plantilla = plantilla.Replace(marca_freg_pri_titular, lbFechaRegistro.Text);
                        plantilla = plantilla.Replace(marca_clase_pri_titular, lbClase.Text);
                        plantilla = plantilla.Replace(marca_nombre_pri_titular, lbNombre.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_pri_titular, lbTelefono.Text);
                        plantilla = plantilla.Replace(marca_dom_pri_titular, lbDomicilio.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_email_pri_titular, lbEmail.Text);
                        plantilla = plantilla.Replace(marca_pais_pri_titular, lbPais.Text);
                        plantilla = plantilla.Replace(marca_nombre_cn, lbNombreCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domicilio_cn, lbDomicilioCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_cn, lbTelefonoCN.Text);
                        plantilla = plantilla.Replace(marca_email_cn, lbEmailCN.Text);
                        plantilla = plantilla.Replace(marca_pais_cn, lbPaisCN.Text);
                        plantilla = plantilla.Replace(marca_nomb_apoderado, CAgenteLocal.Dat.Nombre.AsString);
                        plantilla = plantilla.Replace(marca_domic_apoderado, tbDomicilioAgente.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_matricula, CAgenteLocal.Dat.nromatricula.AsString);
                        plantilla = plantilla.Replace(marca_poder, lbNroPoder.Text);
                        #endregion Cambio de Nombre
                        break;
                    case (int)GlobalConst.Marca_Tipo_Tramite.FUSION:
                        #region Fusión
                        plantilla = plantilla.Replace(marca_marca_pri_titular, lbDenominacion.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_nreg_pri_titular, lbNroRegistro.Text);
                        plantilla = plantilla.Replace(marca_freg_pri_titular, lbFechaRegistro.Text);
                        plantilla = plantilla.Replace(marca_clase_pri_titular, lbClase.Text);
                        //Datos Primer Titular
                        plantilla = plantilla.Replace(marca_nombre_pri_titular, lbNombre.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_pri_titular, lbTelefono.Text);
                        plantilla = plantilla.Replace(marca_dom_pri_titular, lbDomicilio.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_email_pri_titular, lbEmail.Text);
                        plantilla = plantilla.Replace(marca_pais_pri_titular, lbPais.Text);
                        //Datos Segundo Titular
                        plantilla = plantilla.Replace(marca_nombre_seg_titular, lbNombre2.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_seg_titular, lbTelefono2.Text);
                        plantilla = plantilla.Replace(marca_dom_seg_titular, lbDomicilio2.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_email_seg_titular, lbEmail2.Text);
                        plantilla = plantilla.Replace(marca_pais_seg_titular, lbPais2.Text);
                        //Datos Apoderado
                        plantilla = plantilla.Replace(marca_nomb_apoderado, CAgenteLocal.Dat.Nombre.AsString.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domic_apoderado, tbDomicilioAgente.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_matricula, CAgenteLocal.Dat.nromatricula.AsString);
                        plantilla = plantilla.Replace(marca_poder, lbNroPoder.Text);
                        //Nombre que Adquiere
                        plantilla = plantilla.Replace(marca_nombre_cn, lbNombreCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domicilio_cn, lbDomicilioCN.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_pais_cn, lbPaisCN.Text);
                        plantilla = plantilla.Replace(marca_telefono_cn, lbTelefonoCN.Text);
                        plantilla = plantilla.Replace(marca_email_cn, lbEmailCN.Text);
                        #endregion Fusión
                        break;
                    case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO:
                        #region Cambio de Domicilio
                        plantilla = plantilla.Replace(marca_marca_pri_titular, lbDenominacion.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_nreg_pri_titular, lbNroRegistro.Text);
                        plantilla = plantilla.Replace(marca_freg_pri_titular, lbFechaRegistro.Text);
                        plantilla = plantilla.Replace(marca_clase_pri_titular, lbClase.Text);
                        plantilla = plantilla.Replace(marca_nombre_pri_titular, lbNombre.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_telefono_pri_titular, lbTelefono.Text);
                        plantilla = plantilla.Replace(marca_dom_pri_titular, lbDomicilio.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_email_pri_titular, lbEmail.Text);
                        plantilla = plantilla.Replace(marca_pais_pri_titular, lbPais.Text);
                        plantilla = plantilla.Replace(marca_nomb_apoderado, CAgenteLocal.Dat.Nombre.AsString.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domic_apoderado, tbDomicilioAgente.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_matricula, CAgenteLocal.Dat.nromatricula.AsString);
                        plantilla = plantilla.Replace(marca_poder, lbNroPoder.Text);
                        plantilla = plantilla.Replace(marca_domicilio_anterior_cd, lbDomicilioAnterior.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_domicilio_nuevo_cd, lbDomicilioNuevo.Text.Replace("&", "&amp;"));
                        plantilla = plantilla.Replace(marca_pais_cd, lbPaisCD.Text);
                        #endregion Cambio de Domicilio
                        break;
                }
                
                this.AuditarGeneracion();

                #region Enviar a word
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-word";
                Response.AddHeader("Content-Disposition", "attachment;filename=hoja-descriptiva.doc");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(plantilla);
                Response.End();
                #endregion Enviar a word

                #endregion Modelo 2015
            }
            else if (this.ddlTipoModelo.SelectedValue == MODELO_ANTERIOR)
            {
                #region Modelo anterior
                #region Inicializar Generadores de codigo
                Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator(plantilla);
                #endregion Inicializar Generadores de codigo

                string buffer = "";

                string marca = "X";

                cg.copyTemplateToBuffer();

                #region Tipo de Trabajo
                switch (TipoTrabajoID)
                {
                    #region Transferencia

                    case 3:
                        cg.replaceField("n1", marca);
                        cg.replaceField("n2", "  ");
                        break;

                    #endregion Transferencia

                    #region Cambio de Nombre

                    case 4:
                        cg.replaceField("n1", marca);
                        cg.replaceField("n2", "  ");
                        cg.replaceField("n3", "  ");
                        break;

                    #endregion Cambio de Nombre

                    #region Fusion

                    case 5:
                        cg.replaceField("n1", "  ");
                        cg.replaceField("n2", "  ");
                        cg.replaceField("n3", marca);
                        break;

                    #endregion Fusion

                    #region Cambio de Domicilio

                    case 6:
                        cg.replaceField("n1", "  ");
                        cg.replaceField("n2", marca);
                        cg.replaceField("n3", "  ");
                        break;

                    #endregion Cambio de Domicilio

                    #region Licencia

                    case 7:
                        cg.replaceField("n1", "  ");
                        cg.replaceField("n2", marca);
                        break;

                    #endregion Licencia
                }
                #endregion Tipo de Trabajo

                #region Reemplazar marcas para ambos documentos
                switch (TipoTrabajoID)
                {
                    case 3:
                    case 7:
                        #region Reemplazar Marcas de documento (TR, LI)
                        cg.replaceField("registro", lbNroRegistroTr.Text);
                        cg.replaceField("fecha", lbFechaRegistroTr.Text);
                        cg.replaceField("area", lbAreaTr.Text);
                        cg.replaceField("clase", lbClasificacionTr.Text);
                        cg.replaceField("denominacion", lbDenominacionTr.Text);
                        cg.replaceField("nombre", lbNombre.Text);
                        cg.replaceField("domicilio", lbDomicilio.Text);
                        cg.replaceField("pais", lbPais.Text);
                        cg.replaceField("nomb_apoderado", CAgenteLocal.Dat.Nombre.AsString);
                        cg.replaceField("domic_apoderado", tbDomicilioAgente.Text);
                        cg.replaceField("matricula", CAgenteLocal.Dat.nromatricula.AsString);
                        cg.replaceField("poder", lbNroPoder.Text);
                        cg.replaceField("nombre_trans", lbNombreCN.Text);
                        cg.replaceField("domicilio_trans", lbDomicilioCN.Text);
                        cg.replaceField("pais_trans", lbPaisCN.Text);
                        cg.addBufferToText();

                        buffer = cg.Texto;
                        #endregion Reemplazar Marcas de documento (TR, LI)
                        break;
                    case 4:
                    case 5:
                    case 6:
                        #region Reemplazar Marcas de documento (CN, CD, FU)

                        cg.replaceField("nombre_pri_titular", lbNombre.Text);
                        cg.replaceField("dom_pri_titular", lbDomicilio.Text);
                        cg.replaceField("pais_pri_titular", lbPais.Text);
                        cg.replaceField("marca_pri_titular", lbDenominacion.Text);
                        cg.replaceField("clase_pri_titular", lbClase.Text);
                        cg.replaceField("anio_pri_titular", lbAnho.Text);
                        cg.replaceField("nr_pri_titular", lbNro.Text);
                        cg.replaceField("nreg_pri_titular", lbNroRegistro.Text);
                        cg.replaceField("nroexp_pri_titular", lbNroExpediente.Text);
                        cg.replaceField("nombre_seg_titular", lbNombre2.Text);
                        cg.replaceField("dom_seg_titular", lbDomicilio2.Text);
                        cg.replaceField("pais_seg_titular", lbPais2.Text);
                        cg.replaceField("marca_seg_titular", lbDenominacion2.Text);
                        cg.replaceField("clase_seg_titular", lbClase2.Text);
                        cg.replaceField("anio_seg_titular", lbAnho2.Text);
                        cg.replaceField("regnro_seg_titular", lbNro2.Text);
                        cg.replaceField("nroreg_seg_titular", lbNroRegistro2.Text);
                        cg.replaceField("nroexp_seg_titular", lbNroExpediente2.Text);
                        cg.replaceField("nomb_apoderado", CAgenteLocal.Dat.Nombre.AsString);
                        cg.replaceField("poder", lbNroPoder.Text);
                        cg.replaceField("domic_apoderado", tbDomicilioAgente.Text);
                        cg.replaceField("matricula", CAgenteLocal.Dat.nromatricula.AsString);

                        if (CN_CD && TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO))
                        {
                            cg.replaceField("nombre_cn", "--");
                            cg.replaceField("domicilio_cn", "--");
                            cg.replaceField("pais_cn", "--");
                            cg.replaceField("domicilio_anterior_cd", lbDomicilioAnterior.Text);
                            cg.replaceField("domicilio_nuevo_cd", lbDomicilioNuevo.Text);
                            cg.replaceField("pais_cd", lbPaisCD.Text);
                        }
                        else if (CN_CD && TipoTrabajoID == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE))
                        {
                            cg.replaceField("domicilio_anterior_cd", "--");
                            cg.replaceField("domicilio_nuevo_cd", "--");
                            cg.replaceField("pais_cd", "--");
                            cg.replaceField("nombre_cn", lbNombreCN.Text);
                            cg.replaceField("domicilio_cn", lbDomicilio.Text);
                            cg.replaceField("pais_cn", lbPais.Text);
                        }
                        else
                        {
                            cg.replaceField("nombre_cn", lbNombreCN.Text);
                            cg.replaceField("domicilio_cn", lbDomicilioCN.Text);
                            cg.replaceField("pais_cn", lbPaisCN.Text);
                            cg.replaceField("domicilio_anterior_cd", lbDomicilioAnterior.Text);
                            cg.replaceField("domicilio_nuevo_cd", lbDomicilioNuevo.Text);
                            cg.replaceField("pais_cd", lbPaisCD.Text);
                        }

                        cg.addBufferToText();

                        buffer = cg.Texto;
                        #endregion Reemplazar Marcas de documento (CN, CD, FU)
                        break;
                }
                #endregion Reemplazar marcas para ambos documentos

                buffer = cg.Texto;

                #region Deprecated
                /*
			    #region Activar MS-Word
			    Response.Clear();
			    Response.Buffer = true;
			    Response.ContentType = "application/vnd.ms-word";
			    Response.AddHeader("Content-Disposition", "attachment;filename=HD_TVarios.doc" );
			    Response.Charset = "UTF-8";
			    Response.ContentEncoding = System.Text.Encoding.UTF8;
			    Response.Write(buffer); 
			    Response.End();
			    #endregion Activar MS-Word*/

                /*string nombArch =  Berke.Libs.Base.Acceso.GetCurrentUser() + "_" + System.DateTime.Now.ToFileTime();
                string nombDir = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;
                string pathArchSinExt = nombDir + @"\" + nombArch;
                string pathdoc = pathArchSinExt + @".doc";
                string pathpdf = pathArchSinExt + @".pdf";
                string pdfcreatorParams = "/NoProcessingAtStartup";
			
                Berke.Libs.Base.Helpers.Files.SaveStringToFile(cg.Texto, pathdoc);

                PDFCreator.clsPDFCreator pdfcreator = new PDFCreator.clsPDFCreator();
                PDFCreator.clsPDFCreatorOptions pdfOpt = new PDFCreator.clsPDFCreatorOptions();
			
                pdfcreator.cStart(pdfcreatorParams, false);
			
                pdfOpt = pdfcreator.cOptions;
			
                pdfOpt.UseAutosaveDirectory = 1;
                pdfOpt.UseAutosave = 1;
                pdfOpt.AutosaveDirectory = nombDir;
                pdfOpt.AutosaveFilename = nombArch;
                pdfOpt.AutosaveFormat = 0;

                pdfcreator.cOptions = pdfOpt;
						
                pdfcreator.cClearCache();
                string defaultPrinter = pdfcreator.cDefaultPrinter;
                pdfcreator.cDefaultPrinter = "PDFCreator";
                pdfcreator.cPrinterStop = false;
                pdfcreator.cPrintFile(pathdoc);

                while (!System.IO.File.Exists(pathpdf))
                {
                    System.Threading.Thread.Sleep(250);
                }

                pdfcreator.cDefaultPrinter = defaultPrinter;
                pdfcreator.cPrinterStop = true;

                byte [] binaryFile = Berke.Libs.Base.Helpers.Files.GetBytesFromFile(pathpdf);

                System.IO.File.Delete(pathdoc);
                System.IO.File.Delete(pathpdf);*/
                #endregion Deprecated

                this.AuditarGeneracion();

                string nombArch = "hdtv_" + Berke.Libs.Base.Acceso.GetCurrentUser();//+ "_" + System.DateTime.Now.ToFileTime();
                string nombDir = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;
                string pathArchSinExt = nombDir + @"\" + nombArch;

                string pathHTML = pathArchSinExt + @".html";
                Berke.Libs.Base.Helpers.Files.SaveStringToFile(buffer, pathHTML);

                string pathURL = Berke.Libs.Base.GlobalConst.URL_REPORTES + nombArch + @".html";

                Response.Clear();
                Response.Redirect(pathURL);


                #region Deprecated
                /*Berke.Libs.Base.Print2PDF print2PDF = new Berke.Libs.Base.Print2PDF(buffer);
			    byte [] binaryFile = print2PDF.GetBinaryContent();
			    print2PDF.finalizar();
			
			    #region Activar PDF-Reader
			    Response.Clear();
			    Response.Buffer = true;
			    Response.ContentType = "application/pdf";
			    Response.AddHeader("Content-Disposition", "attachment;filename=HD_TVarios.pdf" );
			    Response.OutputStream.Write(binaryFile, 0, binaryFile.Length);
			    Response.End();
			    #endregion Activar PDF-Reader*/
                #endregion Deprecated
                #endregion Modelo anterior
            }
        }
	

		private string GetFecha(DateTime fecha)
		{
			int dia, mes, anho;
			dia = fecha.Day;
			mes = fecha.Month;
			anho = fecha.Year;

			Berke.DG.DBTab.Mes Mes = new Berke.DG.DBTab.Mes(db);
			Mes.Dat.ididioma.Filter = 2;
			Mes.Dat.Orden.Filter = mes;
			Mes.Adapter.ReadAll();
						
			string fecha_letras = dia.ToString() + " de " +
								  Mes.Dat.Mes.AsString + " de " +
								  anho.ToString();

			return fecha_letras;
		}
		

		private void GenerarDocEnTramite()
		{
			#region Obtener plantilla 

			string plantilla = "";
			bool CN_CD = false;
			
			switch (TipoTrabajo)
			{
				case 3:
					plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("Transf_EnTramite", 2);
					break;
				case 4:
					plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambNomb_EnTramite", 2);
					break;
				case 5:
					plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("Fusion_EnTramite", 2);
					break;
				case 6:
					plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambDom_EnTramite", 2);
					break;
				case 7:
					plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("Licencia_EnTramite", 2);
					break;
				case 10:
					CN_CD = true;
					if (Convert.ToInt32(rblTipoDocumento.SelectedValue) == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO))
					{
						plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambDom_EnTramite", 2);
						TipoTrabajo = Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO);
					}
					else if (Convert.ToInt32(rblTipoDocumento.SelectedValue) == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE))
					{
						plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambNomb_EnTramite", 2);
						TipoTrabajo = Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE);
					}
					break;
			}	

			if( plantilla == "" )
			{
				this.ShowMessage( "Error con la plantilla" );
				return ;
			}
			#endregion Obtener plantilla

			#region Inicializar Generadores de codigo
			Berke.Libs.CodeGenerator cg           = new  Berke.Libs.CodeGenerator(plantilla);
			#endregion Inicializar Generadores de codigo

			#region Armar Documento
			string buffer = "";

			cg.copyTemplateToBuffer();

			#region Obtener datos Apoderado
			CAgenteLocal.Adapter.ReadByID(Convert.ToInt32(ddlAgente.SelectedValue));
			#endregion Obtener datos Apoderado

			switch (TipoTrabajo)
			{
				case 3 :
					this.DocEnTramiteTransferencia_Licencia(cg);
					break;
				case 4:
					this.DocEnTramiteCambioNombre(cg);
					break;
				case 5:
					this.DocEnTramiteFusion(cg);
					break;
				case 6:
					this.DocEnTramiteCambioDomicilio(cg);
					break;
				case 7:
					this.DocEnTramiteTransferencia_Licencia(cg);
					break;
			}

			cg.replaceField("agente", CAgenteLocal.Dat.Nombre.AsString.ToUpper()); 
			cg.replaceField("domicilio", tbDomicilioAgente.Text);
			cg.replaceField("propietario_anterior", lbNombre.Text.ToUpper());
			cg.replaceField("domicilio_propietario_anterior", lbDomicilio.Text);
			cg.replaceField("marca", lbDenominacion.Text.ToUpper());
			cg.replaceField("claseid", lbClase.Text.ToUpper());
			Expediente.ClearFilter();
			Expediente.Adapter.ReadByID(ExpedientePadre);
			cg.replaceField("acta_anho", Expediente.Dat.Acta.AsString);
			cg.replaceField("fecha_num_letras", this.GetFecha(Expediente.Dat.PresentacionFecha.AsDateTime));
			/*cg.replaceField("propietario_actual", lbNombreCN.Text.ToUpper());
			cg.replaceField("domicilio_propietario_actual", lbDomicilioCN.Text);*/

			if (Expediente.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO))
			{
				cg.replaceField("tramite", Berke.Libs.Base.GlobalConst.SOLICITUD_REGISTRO.ToString().ToLower());
			}
			else if (Expediente.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION))
			{
				cg.replaceField("tramite", Berke.Libs.Base.GlobalConst.SOLICITUD_RENOVACION.ToString().ToLower());
			}

			cg.addBufferToText();
			buffer = cg.Texto;

			if (CN_CD) 
			{
				TipoTrabajo = Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC);
			}

            #endregion Armar Documento

            this.AuditarGeneracion();

			#region Activar MS-Word
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=HD_TVarios.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); 
			Response.End();
			#endregion Activar MS-Word
			

		}

		private void DocEnTramiteCambioNombre(Berke.Libs.CodeGenerator cg)
		{
			cg.replaceField("propietario_actual", lbNombreCN.Text.ToUpper());
			cg.replaceField("domicilio_propietario_actual", lbDomicilioCN.Text);
		}

		private void DocEnTramiteCambioDomicilio(Berke.Libs.CodeGenerator cg)
		{
			cg.replaceField("domicilio_propietario_anterior", lbDomicilioAnterior.Text);
			cg.replaceField("domicilio_propietario_actual", lbDomicilioNuevo.Text);
		}

		private void DocEnTramiteFusion(Berke.Libs.CodeGenerator cg)
		{
			cg.replaceField("propietario_actual", lbNombre2.Text.ToUpper());
			cg.replaceField("domicilio_propietario_actual", lbDomicilio2.Text);
		}
		
		private void DocEnTramiteTransferencia_Licencia(Berke.Libs.CodeGenerator cg)
		{
			cg.replaceField("propietario_actual", lbNombreCN.Text.ToUpper());
			cg.replaceField("domicilio_propietario_actual", lbDomicilioCN.Text);
		}
	}
}