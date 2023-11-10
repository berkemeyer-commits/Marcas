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
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Marcas.WebUI.Helpers;

	public partial class MarcaModif : System.Web.UI.Page
	{
		const string  operCod = "upm";
		#region Properties
		
		#region MarcaID
		private int MarcaID
		{
			get{ return ViewState["MarcaID"] == null ? -1 : Convert.ToInt32( ViewState["MarcaID"] ) ; }
			set{ ViewState["MarcaID"] = Convert.ToString( value );}
		}
		#endregion MarcaID

		#region ExpeID
		private int ExpeID
		{
			get{ return ViewState["ExpeID"] == null ? -1 : Convert.ToInt32( ViewState["ExpeID"] ) ; }
			set{ ViewState["ExpeID"] = Convert.ToString( value );}
		}
		#endregion ExpeID
		
		#region MarcaTab
		private Berke.DG.DBTab.Marca MarcaTab
		{
			get{ return Session["MarcaTab"] == null ? new Berke.DG.DBTab.Marca() : new Berke.DG.DBTab.Marca( (DataTable)Session["MarcaTab"] ); }
			set{ Session["MarcaTab"] = ((Berke.DG.DBTab.Marca) value).Table ;}
		}
		#endregion MarcaTab

		#region RegRenTab
		private Berke.DG.DBTab.MarcaRegRen RegRenTab
		{
			get{ return Session["RegRenTab"] == null ? new Berke.DG.DBTab.MarcaRegRen() : new Berke.DG.DBTab.MarcaRegRen( (DataTable)Session["RegRenTab"] ); }
			set{ Session["RegRenTab"] = ((Berke.DG.DBTab.MarcaRegRen) value).Table ;}
		}
		#endregion RegRenTab

		#region ExpeTab
		private Berke.DG.DBTab.Expediente ExpeTab
		{
			get{ return Session["ExpeTab"] == null ? new Berke.DG.DBTab.Expediente() : new Berke.DG.DBTab.Expediente( (DataTable)Session["ExpeTab"] ); }
			set{ Session["ExpeTab"] = ((Berke.DG.DBTab.Expediente) value).Table ;}
		}
		#endregion ExpeTab

		#region PropXMarcaTab
		private Berke.DG.DBTab.PropietarioXMarca PropXMarcaTab
		{
			get{ return Session["PropXMarcaTab"] == null ? new Berke.DG.DBTab.PropietarioXMarca() : new Berke.DG.DBTab.PropietarioXMarca( (DataTable)Session["PropXMarcaTab"] ); }
			set{ Session["PropXMarcaTab"] = ((Berke.DG.DBTab.PropietarioXMarca) value).Table ;}
		}
		#endregion PropXMarcaTab

		#region expeXPrpoTab
		private Berke.DG.DBTab.ExpedienteXPropietario expeXPrpoTab
		{
			get{ return Session["expeXPrpoTab"] == null ? new Berke.DG.DBTab.ExpedienteXPropietario() : new Berke.DG.DBTab.ExpedienteXPropietario( (DataTable)Session["expeXPrpoTab"] ); }
			set{ Session["expeXPrpoTab"] = ((Berke.DG.DBTab.ExpedienteXPropietario) value).Table ;}	
		}
		#endregion expeXPrpoTab

		#endregion Properties

		#region Controles del Web Form
		protected System.Web.UI.WebControls.TextBox txtPwd;
		protected System.Web.UI.WebControls.TextBox txtPubNro;
		protected System.Web.UI.WebControls.Panel pnlRegistro;
		#endregion Controles del Web Form

		#region Asignar Delegados
		private void AsignarDelegados()
		{

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
				
			#region AccesoDB
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			#endregion AccesoDB

			#region Leer Marca , Expediente y MarcaRegRen
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca();
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			Berke.DG.DBTab.MarcaRegRen marRegRen = new Berke.DG.DBTab.MarcaRegRen(db);
			if( MarcaID != 0)
			{
				mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID( MarcaID );
				this.ExpeID = mar.Dat.ExpedienteVigenteID.AsInt;
				expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( ExpeID );
			}
			else
			{
				if( ExpeID != 0)
				{
					expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( ExpeID );
					this.MarcaID = expe.Dat.MarcaID.AsInt;
					mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID( MarcaID );
				}
			}
			marRegRen.Adapter.ReadByID( mar.Dat.MarcaRegRenID.Value );
			
		
			#endregion Leer Marca y Expediente

			#region Leer Poder
			Berke.DG.DBTab.ExpedienteXPoder expePod = new Berke.DG.DBTab.ExpedienteXPoder(db);
			Berke.DG.DBTab.Poder pod = new Berke.DG.DBTab.Poder( db );
			expePod.Dat.ExpedienteID.Filter = expe.Dat.ID.Value;
			expePod.Adapter.ReadAll();
			if( expePod.RowCount > 0 ){
				pod.Adapter.ReadByID( expePod.Dat.PoderID.AsInt );
			}
			#endregion Leer Poder

			#region Leer cliente,propietario, agenteLocal
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
			Berke.DG.DBTab.PropietarioXMarca ppm = new Berke.DG.DBTab.PropietarioXMarca(db);
			Berke.DG.DBTab.Cliente cli = new Berke.DG.DBTab.Cliente( db );
			Berke.DG.DBTab.CAgenteLocal agLoc = new Berke.DG.DBTab.CAgenteLocal( db );
			ppm.Dat.MarcaID.Filter = mar.Dat.ID.AsInt;
			ppm.Adapter.ReadAll();
			ArrayList appm = ppm.Adapter.GetListOfField( ppm.Dat.PropietarioID );
			prop.Dat.ID.Filter = new DSFilter( appm );
//			string dd = prop.Adapter.ReadAll_CommandString();
			prop.Adapter.ReadAll();
			cli.Adapter.ReadByID( mar.Dat.ClienteID.AsInt);
			agLoc.Adapter.ReadByID( mar.Dat.AgenteLocalID.AsInt );

			Berke.DG.DBTab.ExpedienteXPropietario epp = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			epp.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			epp.Adapter.ReadAll();

			// Verifico que sea por derecho propio o por poder.
			epp.GoTop();
			
			if ( epp.Dat.DerechoPropio.AsBoolean ) 
			{
				lblTituloPoder.Text = "Agregar poder";
				pnlTienePoder.Visible = false;
			}
			else 
			{
				pnlTienePoder.Visible = true;
				lblTituloPoder.Text = "Modificar poder";
			}

			
			

			#endregion Leer cliente,propietario, agenteLocal
			this.MarcaTab = mar;
			this.RegRenTab = marRegRen;
			this.ExpeTab	= expe;
			this.PropXMarcaTab = ppm;
			this.expeXPrpoTab = epp;

			lblMarcaRegRenID.Text	= "ID: "+ marRegRen.Dat.ID.AsString;
			lblExpedID.Text			= "ID: "+ expe.Dat.ID.AsString;

			#region Se recupera descripcion del trámite
			Berke.DG.DBTab.Tramite tram = new Berke.DG.DBTab.Tramite(db);
			tram.Adapter.ReadByID(expe.Dat.TramiteID.AsInt);
			lblDescTramite.Text = tram.Dat.Abrev.AsString;
			#endregion Se recupera descripcion del trámite			

			#region Asignar labels

			#region Poder
			this.lblPoderNombre.Text = "";
			if( pod.RowCount > 0 ){
				this.lblPoderNombre.Text += pod.Dat.Denominacion.AsString+" - "+ pod.Dat.ID.AsString;
			}
			#endregion Poder

			#region Propietario
			this.lblPropietarioNombre.Text = "";
			if( prop.RowCount > 0 ){
				for( prop.GoTop(); ! prop.EOF; prop.Skip()){
				  if(	this.lblPropietarioNombre.Text  != "" ) this.lblPropietarioNombre.Text +="<BR>";
				  this.lblPropietarioNombre.Text += prop.Dat.Nombre.AsString+" - "+ prop.Dat.ID.AsString;
				}
			}else{
				this.lblPropietarioNombre.Text = mar.Dat.Propietario.AsString;
			}
			#endregion Propietario

			this.lblClienteNombre.Text = cli.Dat.Nombre.AsString+ " - "+cli.Dat.ID.AsString;
			this.lblAgLocalNombre.Text =  agLoc.Dat.Nombre.AsString+ " - "+agLoc.Dat.idagloc.AsString;;



			#endregion

			#region Asignar TextBoxs
			txtID.Text					= mar.Dat.ID.AsString; txtID.ReadOnly = true;
			txtDenominacion.Text		= mar.Dat.Denominacion.AsString;
			txtDenominacionClave.Text	= mar.Dat.DenominacionClave.AsString;
			txtClaseDescripEsp.Text		= mar.Dat.ClaseDescripEsp.AsString;
			txtExpedienteVigenteID.Text = mar.Dat.ExpedienteVigenteID.AsString;
			txtMarcaRegRenID.Text		= mar.Dat.MarcaRegRenID.AsString;

			// Agregado por mbaez el 01/11/2006
			txtPropietarioTexto.Text    = mar.Dat.Propietario.AsString;
			txtPaisTexto.Text    		= mar.Dat.ProPais.AsString;
			txtDireccionTexto.Text		= mar.Dat.ProDir.AsString;
		
			#region MarcaRegRen
			this.txtRegNro.Text			= marRegRen.Dat.RegistroNro.AsString;
			this.txtRegAnio.Text		= marRegRen.Dat.RegistroAnio.AsString;
			this.txtRegVencim.Text		= marRegRen.Dat.VencimientoFecha.AsString;
			this.txtRegConcesion.Text	= marRegRen.Dat.ConcesionFecha.AsString;
			#endregion MarcaRegRen

			#region Expediente
			txtActaNro.Text		= expe.Dat.ActaNro.AsString;
			txtActaAnio.Text		= expe.Dat.ActaAnio.AsString;

			txtPubPag.Text		= expe.Dat.PublicPag.AsString;
			txtPubAnio.Text		= expe.Dat.PublicAnio.AsString;

			txtBib.Text		= expe.Dat.Bib.AsString;
			txtExp.Text		= expe.Dat.Exp.AsString;

			txtExpeMarcaID.Text		= expe.Dat.MarcaID.AsString;
			txtExpeRegRenID.Text	= expe.Dat.MarcaRegRenID.AsString;
			txtExpedPadre.Text		= expe.Dat.ExpedienteID.AsString;
			#endregion Expediente

			#endregion Asignar TextBoxs
		
			#region MarcaTipo DropDown
			/*
			ddlMarcaTipoID.Items.Add(new System.Web.UI.WebControls.ListItem("",""));
			ddlMarcaTipoID.Items.Add(new System.Web.UI.WebControls.ListItem("Denominativa","1"));
			ddlMarcaTipoID.Items.Add(new System.Web.UI.WebControls.ListItem("Figurativa","2"));
			ddlMarcaTipoID.Items.Add(new System.Web.UI.WebControls.ListItem("Mixta","3"));
			*/
		
			#endregion MarcaTipo DropDown
		
			#region Clase DropDown
			
			SimpleEntryDS seClase = Berke.Marcas.UIProcess.Model.Clase.ReadForSelect();
			ddlClaseID.Fill( seClase.Tables[0], true);
			
			#endregion Clase DropDown

			#region Ubicar DropDowns

			#region Marca Tipo

			if ( mar.Dat.MarcaTipoID.AsString != null) 
			{
				chkTipo.SelectedValue = mar.Dat.MarcaTipoID.AsString;
                //chkTipo1.SelectedValue = mar.Dat.MarcaTipoID.AsString;
			}

			/*
			if( ddlMarcaTipoID.Items.FindByValue(mar.Dat.MarcaTipoID.AsString)!= null )
			{
				ddlMarcaTipoID.Items.FindByValue(mar.Dat.MarcaTipoID.AsString).Selected = true;		
			}
			*/
			#endregion Marca Tipo
			#region ClaseID
			if( ddlClaseID.Items.FindByValue(mar.Dat.ClaseID.AsString)!= null )
			{
				ddlClaseID.Items.FindByValue(mar.Dat.ClaseID.AsString).Selected = true;		
			}
			#endregion ClaseID

			#region ddlLimitada
			if ( mar.Dat.Limitada.AsString != "") 
			{
				rbLimitada.SelectedValue = mar.Dat.Limitada.AsString;
			}
			/*
			if( ddlLimitada.Items.FindByValue(mar.Dat.Limitada.AsString)!= null ){
				ddlLimitada.Items.FindByValue(mar.Dat.Limitada.AsString).Selected = true;		
			}
			*/
			#endregion ddlLimitada

			#region ddlVigilada
			if ( mar.Dat.Vigilada.AsString != "") 
			{
				rbMarcaVigilada.SelectedValue = mar.Dat.Vigilada.AsString;
			}
			/*
			if( ddlVigilada.Items.FindByValue(mar.Dat.Vigilada.AsString)!= null )
			{
				rbMarcaVigilada.SelectedValue = mar.Dat.Vigilada.AsString;
				ddlVigilada.Items.FindByValue(mar.Dat.Vigilada.AsString).Selected = true;		
			}
			*/
			#endregion ddlVigilada

			#region ddlNuestra
			if ( mar.Dat.Nuestra.AsString != "" ) 
			{
				rbMarcaNuestra.SelectedValue= (mar.Dat.Nuestra.AsString);
			}

			
			/*
			if( ddlNuestra.Items.FindByValue(mar.Dat.Nuestra.AsString)!= null )
			{
				ddlNuestra.Items.FindByValue(mar.Dat.Nuestra.AsString).Selected = true;		
			}
			*/
			#endregion ddlNuestra

			#region ddlStandBy
			if (mar.Dat.StandBy.AsString != "") {
				 rbStandBy.SelectedValue = mar.Dat.StandBy.AsString;
			 }
			/*
			if( ddlStandBy.Items.FindByValue(mar.Dat.StandBy.AsString)!= null )
			{
				ddlStandBy.Items.FindByValue(mar.Dat.StandBy.AsString).Selected = true;		
			}
			*/
			#endregion ddlStandBy

			#region ddlSustituida
			if ( mar.Dat.Sustituida.AsString != "") 
			{
				rbSustituida.SelectedValue=mar.Dat.Sustituida.AsString;
			}
			/*
			if( ddlSustituida.Items.FindByValue(mar.Dat.Sustituida.AsString)!= null )
			{
				ddlSustituida.Items.FindByValue(mar.Dat.Sustituida.AsString).Selected = true;		
			}
			*/
			#endregion ddlSustituida

			#region Activa
			if ( mar.Dat.Vigente.AsString != "")
			{
				rbMarcaActiva.SelectedValue = mar.Dat.Vigente.AsString;
			}
			/*
			if( ddlVigente.Items.FindByValue(mar.Dat.Vigente.AsString)!= null )
			{
				ddlVigente.Items.FindByValue(mar.Dat.Vigente.AsString).Selected = true;		
			}
			*/
			#endregion Activa
					
			
			#region Reg Vigente
			if( ddlRegVigente.Items.FindByValue(marRegRen.Dat.Vigente.AsString)!= null )
			{
				ddlRegVigente.Items.FindByValue(marRegRen.Dat.Vigente.AsString).Selected = true;		
			}
			#endregion  Reg Vigente
					
			#endregion Ubicar DropDowns

		    #region Ubicar Combos

		#endregion Ubicar Combos

			bool permiso_acceso = Berke.Libs.Base.Acceso.OperacionPermitida( "upmf" );
			//this.pnlEnlaces.Visible = Berke.Libs.Base.Acceso.OperacionPermitida( "upem" );
			this.pnlEnlaces.Visible = false;
			this.pnlMarca.Enabled        = permiso_acceso;
			this.pnlDatosComunes.Enabled = permiso_acceso;
			this.pnlExpediente.Enabled   = permiso_acceso;
			this.ddlClaseID.Enabled      = permiso_acceso;
			this.ddlRegVigente.Enabled   = permiso_acceso;
	

		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				if( ! Berke.Libs.Base.Acceso.OperacionPermitida( operCod )){
					throw new Excep.Biz.OperacionRestringida( operCod );
				}
				MarcaID = 0;
				string MarcaID_str = UrlParam.GetParam("MarcaID");
				if( MarcaID_str != "" )
				{ 
					MarcaID = Convert.ToInt32( MarcaID_str );
				}
				string ExpeID_str = UrlParam.GetParam("ExpeID");
				if( ExpeID_str != "" )
				{ 
					ExpeID = Convert.ToInt32( ExpeID_str );
				}

				AsignarValoresIniciales();
			}
		}
		#endregion Page_Load

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

		#region Boton Grabar
		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{

			if (this.txtBoxIdPropPaso.Text != "" && this.txtPoderID.Text != "") 
			{
				ShowMessage("No puede modificar el poder y Pasar a derecho propio al mismo tiempo"+
					        "Descarte una de las opciones e intente de nuevo.");
				return;
			}

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Marca mar = this.MarcaTab;
			Berke.DG.DBTab.MarcaRegRen marRegRen = this.RegRenTab;
			Berke.DG.DBTab.Expediente expe = this.ExpeTab;

			Berke.DG.DBTab.PropietarioXMarca ppm = this.PropXMarcaTab;
			ppm.InitAdapter( db );
			ppm.AcceptAllChanges();
			Berke.DG.DBTab.ExpedienteXPropietario epp = this.expeXPrpoTab;
			epp.InitAdapter( db );
			epp.AcceptAllChanges();

			Berke.DG.DBTab.ModifMarcaLog modifMarcaLog = new Berke.DG.DBTab.ModifMarcaLog(db);

		
			mar.InitAdapter( db );
			mar.AcceptAllChanges();
			mar.Edit();

			#region Asignar TextBoxs
			/* BUG#263
			 * Si el panel de marca esta activo entonces se tiene acceso 
			 * total sobre el modificador
			 */
			if ( pnlMarca.Enabled ) 
			{
				mar.Dat.Denominacion.Value			= txtDenominacion.Text;
				mar.Dat.DenominacionClave.Value		= txtDenominacionClave.Text;
				mar.Dat.ClaseDescripEsp.Value		= txtClaseDescripEsp.Text;
				mar.Dat.ExpedienteVigenteID.Value	= txtExpedienteVigenteID.Text;
				mar.Dat.MarcaRegRenID.Value			= txtMarcaRegRenID.Text;
			}

			// Agregado por mbaez el 01/11/2006

			/* BUG#263
			 * Si el panel para cambio de datos del propietario en la marca esta activo  
			 */
			if ( pnlDatPropEnMarca.Enabled ) 
			{
				if (txtPropietarioID.Text == "")  
				{
					mar.Dat.ProPais.Value				= txtPaisTexto.Text;
					mar.Dat.ProDir.Value				= txtDireccionTexto.Text;
					mar.Dat.Propietario.Value			= txtPropietarioTexto.Text;
				}
			}


			#endregion Asignar TextBoxs
			/* BUG#263
			 * Si el panel de marca esta activo entonces se tiene acceso 
			 * total sobre el modificador
			 */
			if ( pnlMarca.Enabled ) 
			{
				#region MarcaTipoID
			
				if( chkTipo.SelectedValue != "" )
				{
					mar.Dat.MarcaTipoID.Value = chkTipo.SelectedValue;
				}
				#endregion MarcaTipoID

				#region ClaseID
				if( ddlClaseID.SelectedValue != "" )
				{
					mar.Dat.ClaseID.Value = ddlClaseID.SelectedValue;		
				}
				#endregion ClaseID
			
				#region ClienteID
				if( txtClienteID.Text != "" )
				{
					mar.Dat.ClienteID.Value = txtClienteID.Text;
			
				}
				#endregion ClienteID
				#region AgenteLocalID
				if( txtAgLocID.Text != "" )
				{
					mar.Dat.AgenteLocalID.Value = txtAgLocID.Text;
				}
				#endregion AgenteLocalID		

				#region Limitada
				if( this.rbLimitada.SelectedValue != " " )
				{
					mar.Dat.Limitada.Value = this.rbLimitada.SelectedValue;
				}
				#endregion Limitada
				#region Vigilada
			
				if( this.rbMarcaVigilada.SelectedValue != " " )
				{
					mar.Dat.Vigilada.Value = this.rbMarcaVigilada.SelectedValue;
				}
				#endregion Vigilada
				#region Nuestra
			
				if( this.rbMarcaNuestra.SelectedValue != " " )
				{
					mar.Dat.Nuestra.Value = this.rbMarcaNuestra.SelectedValue;
				}
				#endregion Nuestra
				#region Sustituida
			
				if( this.rbSustituida.SelectedValue != " " )
				{
					mar.Dat.Sustituida.Value = this.rbSustituida.SelectedValue;
				}
				#endregion Sustituida
				#region StandBy
			
				if( this.rbStandBy.SelectedValue != " " )
				{
					mar.Dat.StandBy.Value = this.rbStandBy.SelectedValue;
				}
				#endregion StandBy
				#region Activa
			
				if( this.rbMarcaActiva.SelectedValue != " " )
				{
					mar.Dat.Vigente.Value = this.rbMarcaActiva.SelectedValue;
				}
				#endregion Activa
			}
			mar.PostEdit();

		
			#region marRegRen
			/* BUG#263
			 * Si el panel de marca esta activo entonces se tiene acceso 
			 * total sobre el modificador
			 */
			if ( pnlMarca.Enabled ) 
			{
				if( marRegRen.RowCount > 0 )
				{
					marRegRen.InitAdapter( db );
					marRegRen.AcceptAllChanges();
					marRegRen.Edit();
					marRegRen.Dat.RegistroNro.Value			= this.txtRegNro.Text;
					marRegRen.Dat.RegistroAnio.Value		= this.txtRegAnio.Text;
					marRegRen.Dat.VencimientoFecha.Value	= this.txtRegVencim.Text;
					marRegRen.Dat.ConcesionFecha.Value		= this.txtRegConcesion.Text;

					#region RegVigente
					if( this.ddlRegVigente.SelectedValue != "" )
					{
						marRegRen.Dat.Vigente.Value = this.ddlRegVigente.SelectedValue;
					}
					#endregion RegVigente

					marRegRen.PostEdit();
				}
			}
			#endregion

			#region Expediente
			/* BUG#263
			 * Si el panel de marca esta activo entonces se tiene acceso 
			 * total sobre el modificador
			 */
			if ( pnlMarca.Enabled ) 
			{
				expe.InitAdapter( db );
				expe.Edit();
				expe.Dat.ActaNro.Value		= txtActaNro.Text; 
				expe.Dat.ActaAnio.Value		= txtActaAnio.Text;
				expe.Dat.PublicPag.Value	= txtPubPag.Text;
				expe.Dat.PublicAnio.Value	= txtPubAnio.Text;
				expe.Dat.Bib.Value			= txtBib.Text;
				expe.Dat.Exp.Value			= txtExp.Text;
				expe.Dat.MarcaID.Value		= txtExpeMarcaID.Text;
				expe.Dat.MarcaRegRenID.Value= txtExpeRegRenID.Text;
				expe.Dat.ExpedienteID.Value	= txtExpedPadre.Text;

				// Agregado el 01/11/2006 por mbaez, conforme a la actividad 522
				// Se actualiza la fecha de vencimiento tanto en MarcaRegRen
				// como en Expediente.

				expe.Dat.VencimientoFecha.Value   = txtRegVencim.Text;
 		
				#region Cliente
				if( txtClienteID.Text != "" && this.chkModExpeCliente.Checked )	
				{
					expe.Dat.ClienteID.Value  = txtClienteID.Text;
				}
				#endregion Cliente

				#region Agente Local
				if( txtAgLocID.Text  != "" && this.chkModExpeAgLocal.Checked )	
				{
					expe.Dat.AgenteLocalID.Value  = txtClienteID.Text;
				}
				#endregion Agente Local

				expe.PostEdit();
			}
			#endregion Expediente

			db.IniciarTransaccion();

			#region Componer Texto 
			string nl = @"
";
			string comandoMarca = "";
			string comandoRegRen = "";
			string comandoExpe = "";
			string usuario = Berke.Libs.Base.Acceso.GetCurrentUser();
			string texto = "";


			#region Marca
			if (pnlDatPropEnMarca.Enabled) 
			{
				comandoMarca = mar.Adapter.UpdateRow_CommandString().Trim();
				if( comandoMarca != "")
				{
					texto += " SQL = " + comandoMarca + nl;
				}
			}
			#endregion Marca

			#region RegRen
			if (pnlMarca.Enabled) 
			{
				if( marRegRen.RowCount > 0) 
				{
					comandoRegRen = marRegRen.Adapter.UpdateRow_CommandString().Trim();
					if( comandoRegRen != "")
					{
						texto += " SQL= " + comandoRegRen + nl;		
					}
				}
				#endregion RegRen

				#region Expe
				comandoExpe = expe.Adapter.UpdateRow_CommandString().Trim();
				if( comandoExpe != "" )
				{
					texto += " SQL = " + comandoExpe + nl;
				}
			}
			#endregion Expe

			#endregion Componer Texto 

			if( texto != "" || 
				txtPropietarioID.Text.Trim() != "" || 
				this.txtPoderID.Text.Trim() != ""  ||
				this.txtBoxIdPropPaso.Text.Trim() != "")
			{
				texto = @"Marca :["+mar.Dat.ID.AsString+"] "+
					mar.Dat.Denominacion.AsString+ nl + texto;

////				Berke.Marcas.BizActions.Lib.Notificar(   cancelado // 05/06/06
//					13,"["+usuario+"]" + texto,DateTime.Today,
//					db );

				if (pnlMarca.Enabled) 
				{

					#region Eliminar Propietarios de la marca
					if( this.txtPropietarioID.Text.Trim() != "" && ppm.RowCount > 0 )
					{
						for( ppm.GoTop(); !ppm.EOF; ppm.Skip() )
						{
							ppm.Adapter.DeleteRow();
						}
					}
					#endregion Eliminar Propietarios de la marca

					#region Eliminar Propietarios del Expediente

					// Agregado por mbaez. También se tienen que eliminar estos datos cuando se cambia el poder
					// o cuando se pasa a DerechoPropio.

					if( ( this.txtPropietarioID.Text.Trim() != "" && epp.RowCount > 0 && this.chkModExpePropietario.Checked )
						|| txtPoderID.Text != ""             // si se cambia de propietario eliminar estos datos. Mbaez.
						|| txtBoxIdPropPaso.Text != "" )      // si se pasa a derecho propio
					{
						for( epp.GoTop(); !epp.EOF; epp.Skip() )
						{
							epp.Adapter.DeleteRow();
						}
						string comando = @"delete from  ExpedienteCampo WHERE ExpedienteID = @ExpeID And Campo = @Campo ";
						//					string cmd ="";
						#region Propietario Nombre 
						db.ClearParams();
						db.Sql = comando;
						db.AddParam("@ExpeID", System.Data.SqlDbType.Int,expe.Dat.ID.AsInt,4 );
						db.AddParam("@Campo", System.Data.SqlDbType.NVarChar,(string) GlobalConst.PROP_ACTUAL_NOMBRE,40 );
						//					cmd = db.CommandString;
						db.EjecutarDML();

						#endregion Propietario Nombre 

						#region Propietario Direccion 
						db.SetParam("@Campo", (string) GlobalConst.PROP_ACTUAL_DIR  );
						//					cmd = db.CommandString;
						db.EjecutarDML();
						#endregion Propietario Direccion 

						#region Propietario Pais 
						db.SetParam("@Campo",(string) GlobalConst.PROP_ACTUAL_PAIS );
						//					cmd = db.CommandString;
						db.EjecutarDML();
						#endregion Propietario Pais 

					}
					#endregion Eliminar Propietarios de la marca
				
					#region Eliminar Poder del Expediente y Agregar Nuevo
					if( this.txtPoderID.Text.Trim() != "" )
					{
						db.ClearParams();
						db.Sql = @"delete from ExpedienteXPoder where ExpedienteID = @ExpeID ";
						db.AddParam("@ExpeID", System.Data.SqlDbType.Int, expe.Dat.ID.AsInt,4 );
						//string cmd = db.CommandString;
						db.EjecutarDML();
						int PoderID = int.Parse( this.txtPoderID.Text );

						db.ClearParams();
						db.Sql = @"insert into ExpedienteXPoder (ExpedienteID, PoderID ) values (@ExpeID , @PoderID ) ";
						db.AddParam("@ExpeID",  System.Data.SqlDbType.Int, expe.Dat.ID.AsInt,4 );
						db.AddParam("@PoderID", System.Data.SqlDbType.Int, PoderID			,4 );
						db.EjecutarDML();

						// Agregado por mbaez el 01/11/2006. 
						// Se agregan los nuevos datos para ExpedienteCampo y 
						// ExpedienteXPropietario

						#region Actualizacion de ExpedienteXPropietario

						// Se actualiza la tabla expedientexPropietario a partir de los datos de
						// PropietarioXPoder.
						Berke.DG.DBTab.PropietarioXPoder ppp = new Berke.DG.DBTab.PropietarioXPoder(db);
						ppp.Dat.PoderID.Filter = PoderID;
						ppp.Adapter.ReadAll();

						for( ppp.GoTop(); !ppp.EOF; ppp.Skip() )
						{
							epp = new Berke.DG.DBTab.ExpedienteXPropietario( db );
							epp.NewRow();
							epp.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
							epp.Dat.PropietarioID.Value =  ppp.Dat.PropietarioID.AsInt;
							epp.Dat.DerechoPropio.Value = false;
							epp.PostNewRow();
							epp.Adapter.InsertRow();
						}
						#endregion Actualización de ExpedienteXPropietario

						#region Actualizacion de ExpedienteCampo

						// Se actualiza los datos en Expediente campo, a partir de los datos
						// del Poder.	
			
						Berke.DG.DBTab.Poder pod_poder = new Berke.DG.DBTab.Poder( db );
						pod_poder.Adapter.ReadByID(PoderID);
						Berke.DG.DBTab.ExpedienteCampo expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );

						#region Insertar nuevo nombre en expedienteCampo
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string) GlobalConst.PROP_ACTUAL_NOMBRE;
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = pod_poder.Dat.Denominacion.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo nombre en expedienteCampo

						#region Insertar nuevo domicilio en expedienteCampo
						expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string)GlobalConst.PROP_ACTUAL_DIR; 
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = pod_poder.Dat.Domicilio.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo domicilio en expedienteCampo

						#region Insertar nuevo Pais en expedienteCampo

						// Recupero la descripción abreviada del país.
						Berke.DG.DBTab.CPais pa_pais = new Berke.DG.DBTab.CPais( db );
						pa_pais.Adapter.ReadByID(  pod_poder.Dat.PaisID.AsInt );
						
						expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string) GlobalConst.PROP_ACTUAL_PAIS; 
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = pa_pais.Dat.paisalfa.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo Pais en expedienteCampo					

						#endregion Actualizacion de ExpedienteCampo
	
					}
					#endregion  Eliminar Poder del Expediente y Agregar Nuevo

					#region Agregar Propietarios de marcas
					string propNombre = "";
					string propDireccion = "";
					string propPais = "";
					if( this.txtPropietarioID.Text.Trim() != "" )
					{
						string[] aID =txtPropietarioID.Text.Trim().Split(',');
						foreach( string vID in aID )
						{
							if( vID.Trim() != "" )
							{
								#region Ubicar Propietario
								Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario( db );
								prop.Adapter.ReadByID( vID );
								if( prop.RowCount == 0 )
								{
									throw new Exception( "Identificador de propietario incorrecto- "+ vID);
								}
								Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais( db );
								pais.Adapter.ReadByID( prop.Dat.PaisID.AsInt );
								propNombre		+= propNombre		!= "" ? " / " : "";
								propDireccion	+= propDireccion	!= "" ? " / " : "";
								propPais		+= propPais			!= "" ? " / " : "";

								propNombre		+= prop.Dat.Nombre.AsString;
								propDireccion	+= prop.Dat.Direccion.AsString;
								propPais		+= pais.Dat.paisalfa.AsString;
								#endregion Ubicar Propietario

								#region Agregar PropietarioXMarca
								ppm = new Berke.DG.DBTab.PropietarioXMarca( db );
								ppm.NewRow();
								ppm.Dat.MarcaID.Value = mar.Dat.ID.AsInt;
								ppm.Dat.PropietarioID.Value = vID;
								ppm.PostNewRow();
								ppm.Adapter.InsertRow();
								#endregion Agregar PropietarioXMarca			
							}
						}
						mar.Edit();
						mar.Dat.Propietario.Value	= propNombre;
						mar.Dat.ProDir.Value		= propDireccion;
						mar.Dat.ProPais.Value		= propPais;
						mar.PostEdit();
					}
					#endregion Agregar Propietarios de marcas

					#region Agregar Propietarios del expediente
					if( this.txtPropietarioID.Text.Trim() != "" && this.chkModExpePropietario.Checked )
					{
						string[] aID =txtPropietarioID.Text.Trim().Split(',');
						foreach( string vID in aID )
						{
							if( vID.Trim() != "" )
							{
								Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario( db );
								prop.Adapter.ReadByID( vID );
								if( prop.RowCount == 0 )
								{
									throw new Exception( "Identificador de propietario incorrecto- "+ vID);
								}
								epp = new Berke.DG.DBTab.ExpedienteXPropietario( db );
								epp.NewRow();
								epp.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
								epp.Dat.PropietarioID.Value = vID;
								epp.PostNewRow();
								epp.Adapter.InsertRow();
							}
						}

						string comando = @"insert into ExpedienteCampo (ExpedienteID, Campo, Valor ) values (@ExpeID , @Campo, @valor ) ";
					
						#region Propietario Nombre 
						db.ClearParams();
						db.Sql = comando;
						db.AddParam("@ExpeID", System.Data.SqlDbType.Int,expe.Dat.ID.AsInt,4 );
						db.AddParam("@Campo", System.Data.SqlDbType.NVarChar,(string) GlobalConst.PROP_ACTUAL_NOMBRE,40 );
						db.AddParam("@valor", System.Data.SqlDbType.NVarChar, propNombre, 200 );
						//					string cmd = db.CommandString;
						db.EjecutarDML();

						#endregion Propietario Nombre 

						#region Propietario Direccion 
						db.SetParam("@Campo", (string) GlobalConst.PROP_ACTUAL_DIR  );
						db.SetParam("@valor", propDireccion  );
						//					cmd = db.CommandString;
						db.EjecutarDML();
						#endregion Propietario Direccion 

						#region Propietario Pais 
						db.SetParam("@Campo",(string) GlobalConst.PROP_ACTUAL_PAIS );
						db.SetParam("@valor", propPais  );
						//					cmd = db.CommandString;
						db.EjecutarDML();
						#endregion Propietario Pais 

					}
					#endregion Agregar Propietarios del expediente

					#region Pasar a derecho propio
					if (txtBoxIdPropPaso.Text != "") 
					{
						db.ClearParams();
						db.Sql = @"delete from ExpedienteXPoder where ExpedienteID = @ExpeID ";
						db.AddParam("@ExpeID", System.Data.SqlDbType.Int, expe.Dat.ID.AsInt,4 );
						//string cmd = db.CommandString;
						db.EjecutarDML();

						int PropID = int.Parse( this.txtBoxIdPropPaso.Text );
						epp = new Berke.DG.DBTab.ExpedienteXPropietario( db );
						epp.NewRow();
						epp.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						epp.Dat.PropietarioID.Value = PropID;
						epp.Dat.DerechoPropio.Value = true;
						epp.PostNewRow();
						epp.Adapter.InsertRow();

						// Leemos datos del propietario
						Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
						prop.Adapter.ReadByID(PropID);

						Berke.DG.DBTab.ExpedienteCampo expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );

						#region Insertar nuevo nombre en expedienteCampo
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string) GlobalConst.PROP_ACTUAL_NOMBRE;
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = prop.Dat.Nombre.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo nombre en expedienteCampo

						#region Insertar nuevo domicilio en expedienteCampo
						expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string)GlobalConst.PROP_ACTUAL_DIR; 
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = prop.Dat.Direccion.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo domicilio en expedienteCampo

						#region Insertar nuevo Pais en expedienteCampo

						// Recupero la descripción abreviada del país.
						Berke.DG.DBTab.CPais pa_pais = new Berke.DG.DBTab.CPais( db );
						pa_pais.Adapter.ReadByID(  prop.Dat.PaisID.AsInt );
						
						expe_campo = new Berke.DG.DBTab.ExpedienteCampo( db );
						expe_campo.NewRow();
						expe_campo.Dat.Campo.Value = (string) GlobalConst.PROP_ACTUAL_PAIS; 
						expe_campo.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						expe_campo.Dat.Valor.Value = pa_pais.Dat.paisalfa.AsString;
						expe_campo.PostNewRow();
						expe_campo.Adapter.InsertRow();
						#endregion Insertar nuevo Pais en expedienteCampo					
				

					}
					#endregion Pasar a derecho propio
				}


				mar.Adapter.UpdateRow();

				if (pnlMarca.Enabled)  
				{
					if( marRegRen.RowCount > 0 )
					{
						marRegRen.Adapter.UpdateRow();
					}
					expe.Adapter.UpdateRow();
				}


				/* agregar aqui la insercion en la tabla modifmarcalog
				 * 
				 * 
				 * */
				

				modifMarcaLog.NewRow();
				modifMarcaLog.Dat.funcionarioID.Value = MySession.FuncionarioID;
				modifMarcaLog.Dat.fechains.Value      = DateTime.Now;
				modifMarcaLog.PostNewRow();
				modifMarcaLog.Adapter.InsertRow();
				

				db.Commit();		
				ShowMessage(" Los Cambios han sido grabados" );
				mar.AcceptAllChanges();
				this.MarcaTab = mar;
				

				if (pnlMarca.Enabled)  
				{
					marRegRen.AcceptAllChanges();
					this.RegRenTab = marRegRen;

					expe.AcceptAllChanges();
					this.ExpeTab = expe;
				}

			}
			else{
				
				ShowMessage(" NO HAY CAMBIOS PARA GRABAR !!!" );
			}
			db.CerrarConexion();

		}
		#endregion Boton Grabar
		
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

	}
}
