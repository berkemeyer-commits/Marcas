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
using Berke.Libs.Boletin.Services;

namespace Berke.Marcas.WebUI.Home
{
	
	#region Using
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Helpers;

	#endregion Using

	public  partial class ExpedienteCambioSit_Modificar : System.Web.UI.Page
	{
		#region Variables Globales
		Berke.DG.ExpeMarCambioSitDG				cambioSitDG;
		Berke.DG.DBTab.Expediente				expe;
		Berke.DG.DBTab.Expediente_Situacion		expeSit ;		
		Berke.DG.DBTab.Expediente_Situacion		expeSit_bkp ;		
		Berke.DG.DBTab.Marca					mar;
		Berke.DG.DBTab.MarcaRegRen				regRen;
		Berke.DG.DBTab.MarcaRegRen				regRenPadre;
		Berke.DG.ViewTab.vExpeMarca				vExpe;

	

		#endregion Varibles Globales

		#region Properties

		#region SituacionActualID
		private int SituacionActualID
		{
			get{ return Convert.ToInt32( ViewState["SituacionActualID"] ) ; }
			set{ ViewState["SituacionActualID"] = Convert.ToString( value );}
		}
		#endregion SituacionActualID

		#region ExpedienteID
		private int ExpedienteID
		{
			get{ return Convert.ToInt32(( ViewState["ExpedienteID"] == null )? -1 : ViewState["ExpedienteID"] ) ; }
			set{ ViewState["ExpedienteID"] = Convert.ToString( value );}
		}
		#endregion ExpedienteID



		#endregion Properties

		#region Controles del Form

		//protected Berke.Libs.WebBase.Controls.DropDown ddlTramite;
        //protected System.Web.UI.WebControls.Button btnBuscar;
        //protected Berke.Libs.WebBase.Controls.DropDown ddlTramiteSit;
        //protected System.Web.UI.WebControls.TextBox txtDenominacion;
		//protected Berke.Libs.WebBase.Controls.DropDown ddNro;
        //protected System.Web.UI.WebControls.TextBox txtNroDesde;
        //protected System.Web.UI.WebControls.TextBox txtNroHasta;
        //protected System.Web.UI.WebControls.TextBox txtNroAnio;
        //protected System.Web.UI.WebControls.Label lblMensaje;
        //protected System.Web.UI.WebControls.Panel pnlBuscar;
        //protected System.Web.UI.WebControls.DataGrid dgMarcas;
        //protected System.Web.UI.WebControls.Panel pnlResultado;
        //protected System.Web.UI.WebControls.Label lblExpeID;
        //protected System.Web.UI.WebControls.Label lblHI;
        //protected System.Web.UI.WebControls.Label lblActa;
        //protected System.Web.UI.WebControls.Label lblRegistro;
        //protected System.Web.UI.WebControls.Label lblMarca;
        //protected System.Web.UI.WebControls.Label lblClase;
        //protected System.Web.UI.WebControls.Label lblTramite;
        //protected System.Web.UI.WebControls.Label lblSituacion;
        //protected System.Web.UI.WebControls.TextBox txtSitActual;
        //protected System.Web.UI.WebControls.Label Label1;
        //protected System.Web.UI.WebControls.TextBox txtFechaSitActual;
        //protected System.Web.UI.WebControls.TextBox txtSitAnterior;
        //protected System.Web.UI.WebControls.Label Label2;
        //protected System.Web.UI.WebControls.TextBox txtFechaSitAnterior;
        //protected System.Web.UI.WebControls.TextBox txtFechaVencim;
        //protected System.Web.UI.WebControls.Panel pnlPlazo;
        //protected Berke.Libs.WebBase.Controls.DropDown ddlAgenteLocal;
        //protected System.Web.UI.WebControls.TextBox txtActaNro;
        //protected System.Web.UI.WebControls.Panel pnlAgenteLocal;
        //protected System.Web.UI.WebControls.TextBox txtRegistroNro;
        //protected System.Web.UI.WebControls.TextBox txtRegVto;
        //protected System.Web.UI.WebControls.Panel pnlRegistro;
        //protected Berke.Libs.WebBase.Controls.DropDown ddlDiario;
        //protected System.Web.UI.WebControls.TextBox txtPubicPagina;
        //protected System.Web.UI.WebControls.TextBox txtPublicAnio;
        //protected System.Web.UI.WebControls.Panel pnlPublicacion;
        //protected System.Web.UI.WebControls.Button btnAceptar;
        //protected System.Web.UI.WebControls.Button btnCancelar;
        //protected System.Web.UI.WebControls.Panel pnlArchivada;
        //protected System.Web.UI.WebControls.Label lblVencim;
        //protected System.Web.UI.WebControls.TextBox txtBib;
        //protected System.Web.UI.WebControls.TextBox txtExp;
        //protected System.Web.UI.WebControls.Button btnRecalcVencimReg;
        //protected System.Web.UI.WebControls.TextBox txtActaAnio;
        //protected System.Web.UI.WebControls.Button btnRevertir;
        //protected System.Web.UI.WebControls.CheckBox chkRevertir;
        //protected System.Web.UI.WebControls.TextBox txtObservacion;
        //protected System.Web.UI.WebControls.TextBox txtFechaSituacion;
        //protected System.Web.UI.WebControls.Panel pnlActualizar;


		#endregion Controles del Form

		#region Push / Pop

		#region cambioSitDG

		#region	Push_cambioSitDG
		private void 	Push_cambioSitDG ()
		{
			Session["cambioSitDG"] = cambioSitDG.AsDataSet();
		}
		#endregion 	Push_cambioSitDG

		#region	Pop_cambioSitDG
		private void 	Pop_cambioSitDG()
		{
			cambioSitDG = new Berke.DG.ExpeMarCambioSitDG( (DataSet) Session["cambioSitDG"]  );

			expe	= cambioSitDG.Expediente ;
			expeSit	= cambioSitDG.Expediente_Situacion ;		
			expeSit_bkp = cambioSitDG.Expediente_Situacion_bkp;
			mar		= cambioSitDG.Marca ;
			regRen	= cambioSitDG.MarcaRegRen ;
			vExpe   = cambioSitDG.vExpeMarca ;
		    regRenPadre = cambioSitDG.MarcaRegRenPadre;

		}
		#endregion 	Pop_cambioSitDG

		#endregion cambioSitDG

		#endregion  Push / Pop

		#region Asignar Delegados
		private void AsignarDelegados()
		{
			//DropDown Tramite
			this.ddlTramite.SelectedIndexChanged += new System.EventHandler(this.ddlTramite_SelectedIndexChanged);
		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Llenar DropDpwn de Tramites
	//		SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( (int) Const.Proceso.MARCAS );
//			ddlTramite.Fill( se.Tables[0], true);

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramite.Fill( lst.Table, true);
			#endregion
			
			#region Cargar dropDown de Numeros
			ddNro.Items.Clear();
			//				ddNro.Items.Add("");
			ddNro.Items.Add(new ListItem("Acta","1"));
			ddNro.Items.Add(new ListItem("Registro","2"));
			ddNro.Items.Add(new ListItem("Hoja de Inicio", "3"));
			ddNro.Items.Add(new ListItem("ID de Expediente", "4"));
			ddNro.SelectedIndex=0;
			#endregion

			#region llenar DropDown de AgenteBerke
			Berke.DG.ViewTab.ListTab agLoc =  Berke.Marcas.UIProcess.Model.AgenteLocal.ReadForSelect();
			ddlAgenteLocal.Fill( agLoc.Table, true);
			#endregion

			#region llenar DropDown de Diario
			SimpleEntryDS diarioSE = UIPModel.Diario.ReadForSelect();
			ddlDiario.Fill( diarioSE.Tables[0], true );
			#endregion

			txtNroAnio.Text = DateTime.Today.Year.ToString();

		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{

			AsignarDelegados();
			lblMensaje.Text = "";

			if( !IsPostBack )
			{
	
				AsignarValoresIniciales();

				MostrarPanel_Busqueda();
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
			this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
			this.dgMarcas.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMarcas_ItemCommand);
			this.btnRevertir.Click += new System.EventHandler(this.btnRevertir_Click);
			this.btnRecalcVencimReg.Click += new System.EventHandler(this.btnRecalcVencimReg_Click);
			this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Buscar
		private void Buscar()
		{

			#region Asignar Parametros
			Berke.DG.ViewTab.vExpeMarca inTB =   new Berke.DG.ViewTab.vExpeMarca();

			inTB.NewRow(); 
			inTB.Dat.Denominacion	.Value = this.txtDenominacion.Text;   //String
			inTB.Dat.TramiteID		.Value = this.ddlTramite.Value;   //Int32
			inTB.Dat.TramiteSitID	.Value = this.ddlTramiteSit.Value;   //Int32
			switch( this.ddNro.Value )
			{
				case "1":
					inTB.Dat.ActaNro		.Value = this.txtNroDesde.Text;   //Int32
					inTB.Dat.ActaAnio		.Value = this.txtNroAnio.Text;   //Int32
					break;
				case "2":
					inTB.Dat.RegistroNro	.Value = this.txtNroDesde.Text;   //Int32
					inTB.Dat.RegistroAnio	.Value = this.txtNroAnio.Text;   //Int32
					break;
				case "3":
					inTB.Dat.OtNro			.Value = this.txtNroDesde.Text;	 //Int32
					inTB.Dat.OtAnio			.Value = this.txtNroAnio.Text;  //Int32
					break;
				case "4":
					inTB.Dat.ExpedienteID	.Value = this.txtNroDesde.Text;	 //Int32
					break;
			}
			inTB.PostNewRow();

			inTB.NewRow(); 
			switch( this.ddNro.Value )
			{
				case "1":
					inTB.Dat.ActaNro		.Value = this.txtNroHasta.Text;   //Int32
					break;
				case "2":
					inTB.Dat.RegistroNro	.Value = this.txtNroHasta.Text;   //Int32
					break;
				case "3":
					inTB.Dat.OtNro			.Value = this.txtNroHasta.Text;	 //Int32
					break;
				case "4":
					inTB.Dat.ExpedienteID	.Value = this.txtNroHasta.Text;	 //Int32
					break;
			}
			inTB.PostNewRow();
			#endregion
		
			#region Obtener datos
			
			Berke.DG.ViewTab.vExpeMarca vExpe =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList( inTB );
			#region Eliminar Duplicados
			int idAnt = -77;
			for( vExpe.GoTop() ; ! vExpe.EOF ; vExpe.Skip() ){
				if( vExpe.Dat.ExpedienteID.AsInt == idAnt )
				{
					vExpe.Delete();
				}
				else
				{
					idAnt = vExpe.Dat.ExpedienteID.AsInt;
				}
			}
			vExpe.AcceptAllChanges();
			vExpe.GoTop();
			#endregion Eliminar Duplicados
			#endregion
			
			#region Asignar dataSuorce de grilla
			dgMarcas.DataSource = vExpe.Table;
			dgMarcas.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( vExpe.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else if( vExpe.RowCount == 1 )
			{
				ExpedienteID = vExpe.Dat.ExpedienteID.AsInt;
				MostrarPanel_Modificar( vExpe.Dat.ExpedienteID.AsInt );

			}
			else
			{
				MostrarPanel_Resultado();
			}
			#endregion
				
		}

		private void btnBuscar_Click(object sender, System.EventArgs e)
		{
			Buscar();
		}

		#endregion Buscar

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda()
		{
			lblMensaje.Text = "";
			this.pnlActualizar.Visible	= false;
			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= true;
			this.pnlPlazo.Visible		= false;
			this.pnlArchivada.Visible	= false;
			
		}
		#endregion MostrarPanel_Busqueda

		#region MostrarPanel_Resultado
		private void MostrarPanel_Resultado()
		{
			lblMensaje.Text = "";
			this.pnlActualizar.Visible	= false;
			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;
			this.pnlResultado.Visible	= true;
			this.pnlBuscar.Visible		= true;
			this.pnlPlazo.Visible		= false;
			
		}
		#endregion MostrarPanel_Resultado

		#region CambioSitByID
		private void CambioSitByID( int expedienteID )
		{

			cambioSitDG = UIPModel.ExpeMarca.CambioSitByID( expedienteID );

			expe		= cambioSitDG.Expediente ;
			expeSit		= cambioSitDG.Expediente_Situacion ;		
			expeSit_bkp = cambioSitDG.Expediente_Situacion_bkp ;		
			mar			= cambioSitDG.Marca ;
			regRen		= cambioSitDG.MarcaRegRen ;
			regRenPadre = cambioSitDG.MarcaRegRenPadre;
			vExpe		= cambioSitDG.vExpeMarca ;
		}
		#endregion CambioSitByID

		#region MostrarPanel_Modificar 
		private void MostrarPanel_Modificar( int expedienteID )
		{
		
			CambioSitByID( expedienteID );

			Push_cambioSitDG();

			#region Hacer visibles paneles Actualizar y Plazo 
			this.pnlActualizar.Visible	= true;
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= false;

			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;
			this.pnlPlazo.Visible		= true;

			#endregion 
		
			#region Hacer visible el boton de aceptar
			this.btnAceptar.Enabled = true;
			#endregion
			
			#region Asignar Valores de display a Label
			this.lblActa.Text		= vExpe.Dat.Acta.AsString  ;
			this.lblClase.Text		= vExpe.Dat.Clase.AsString;
			this.lblExpeID.Text		= vExpe.Dat.ExpedienteID.AsString+ HtmlGW.Spaces(20) + ( vExpe.Dat.ExpeNuestro.AsBoolean ? "" : "Exped.de Terceros" );
			this.lblHI.Text			= vExpe.Dat.OrdenTrabajo.AsString;
			this.lblMarca.Text		= vExpe.Dat.Denominacion.AsString;
			this.lblTramite.Text	= vExpe.Dat.TramiteDescrip.AsString;
			this.lblRegistro.Text	= vExpe.Dat.Registro.AsString;
			this.lblVencim.Text		= vExpe.Dat.VencimientoFecha.AsString;

			this.lblSituacion.Text	= vExpe.Dat.SituacionDecrip.AsString;
			#endregion

			#region Situacion Actual
			SituacionActualID = -1;
			Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit();

			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion();

			expeSit.GoTop();
			if(  expeSit.RowCount > 0 )
			{

				trmSit	= UIPModel.TramiteSit.ReadByID_M( expeSit.Dat.TramiteSitID.AsInt );
				sit		= UIPModel.Situacion.ReadByID( trmSit.Dat.SituacionID.AsInt );		
				SituacionActualID = trmSit.Dat.SituacionID.AsInt;

				this.txtSitActual.Text		= sit.Dat.Descrip.AsString;
				this.txtFechaSitActual.Text = expeSit.Dat.SituacionFecha.AsString;
				this.txtFechaSituacion.Text = expeSit.Dat.SituacionFecha.AsString;

				this.txtFechaVencim.Text = expeSit.Dat.VencimientoFecha.AsString;

				this.txtObservacion.Text = expeSit.Dat.Obs.AsString;

			}
			else{
				txtSitActual.Text = "* No Disponible *";
			}

			#endregion Situacion Actual
			
			#region Situacion Anterior
			
			if( expeSit.RowCount > 1 )
			{
				expeSit.Skip();
				if( ! expeSit.EOF )
				{
					trmSit	= UIPModel.TramiteSit.ReadByID_M( expeSit.Dat.TramiteSitID.AsInt );
					sit		= UIPModel.Situacion.ReadByID( trmSit.Dat.SituacionID.AsInt );		

					this.txtSitAnterior.Text		= sit.Dat.Descrip.AsString;
					this.txtFechaSitAnterior.Text	= expeSit.Dat.SituacionFecha.AsString;
				}
				else{
					txtSitAnterior.Text = "* No Disponible *";				
				}
			}
			else
			{
				txtSitAnterior.Text = "* No Disponible *";
			}
			#endregion Situacion Anterior
	
			#region Mostrar Paneles segun Situacion
			

			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;	
			this.pnlArchivada.Visible	= false;

			switch ( SituacionActualID )
			{
				case  (int) Berke.Libs.Base.GlobalConst.Situaciones.PRESENTADA:
					//Pasar a Sit Presentada
					this.pnlAgenteLocal.Visible	= true;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= false;	
	
//					ddlAgenteLocal.SelectedValue	= vExpe.Dat.AgenteLocalID.AsString;
					// Existe un error porque queda guardado un ítem como seleccionado.
					// Por esta razón deseleccionamos el elemento actual. Y posteriormente
					// se selecciona el correspondiente por ID.
					// En caso contrario arroja el error DropDownList no puede tener 2
					// items seleccionados. mbaez 17/11/2006
					ddlAgenteLocal.SelectedItem.Selected = false;
					//-

					ddlAgenteLocal.Items.FindByValue(vExpe.Dat.AgenteLocalID.AsString).Selected = true;
					txtActaNro.Text					= vExpe.Dat.ActaNro.AsString;
					this.txtActaAnio.Text			= vExpe.Dat.ActaAnio.AsString;
					break;
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA:
					//Pasar a Sit Publicada
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= true;
					this.pnlRegistro.Visible	= false;	

					ddlDiario.SelectedValue = expe.Dat.DiarioID.AsString;
					txtPubicPagina.Text		= expe.Dat.PublicPag.AsString;
					txtPublicAnio.Text		= expe.Dat.PublicAnio.AsString;

					break;
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONCEDIDA:
					//Pasar a Sit Ingresar Concesion

					txtRegistroNro.Text		= regRen.Dat.RegistroNro.AsString;
					txtRegVto.Text			= expe.Dat.VencimientoFecha.AsString;

//					this.txtRegVto.Text	= Berke.Marcas.UIProcess.Model.Fecha.SumarPlazo(
//						fechaCambioSit,
//						10 ,
//						3 ).ToString("d"); // 10 años
	
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= true;	
					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.ARCHIVADA:
					this.pnlArchivada.Visible	= true;
					this.txtBib.Text	= expe.Dat.Bib.AsString;
					this.txtExp.Text	= expe.Dat.Exp.AsString;

					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.TITULO_RETIRADO:
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.TITULO_ENVIADO:
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.TITULO_A_FACTURACION:
						
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONSTANCIA:						
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONSTANCIA_ENVIADA:						
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONSTANCIA_RETIRADA:	
					//						Response.Redirect( "SitTituloArchivadaAME.aspx" );
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= false;	
					break;
				default:
					//Pasar a Orden Publicacion, Examen Fondo, Concesion, 
					//Vista, Rechazo, Suspension, Adecuar, Sit Intermedia
					//						Response.Redirect( "SitGeneralAME.aspx" );
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= false;	
					break;
			}
			#endregion 

		}
		#endregion MostrarPanel_Modificar

		#region Elegir de la grilla    ItemCommand
		private void dgMarcas_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			int expeID  = ObjConvert.AsInt( e.Item.Cells[0].Text );

			ExpedienteID = expeID;
			MostrarPanel_Modificar( expeID );
		}
		#endregion

		#region Boton Cancelar
		private void btnCancelar_Click(object sender, System.EventArgs e)
		{
			MostrarPanel_Resultado();
		}
		#endregion

		#region Boton Aceptar
		private void btnAceptar_Click(object sender, System.EventArgs e)
		{
			//this.recalcularFechaVenc();
			try
			{
				Expediente_CambiarSituacion();
			}
			catch(Exception ex ){
				ShowMessage( @"Falló la modificación. "+ ex.Message );	
				return;
			}
			ShowMessage( @"Modificación Completada");	

		}
		#endregion Boton Aceptar

		#region DropDown IndexChanged 

		#region ddlTramite_SelectedIndexChanged
		private void ddlTramite_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlTramite.SelectedIndex == 0)
			{
				ddlTramiteSit.Items.Clear();
			}
			else
			{
				SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramite.Value) );
				ddlTramiteSit.Fill( situacion.Tables[0], true );
				
			}
		}
		#endregion 



		#endregion DropDown IndexChanged 



		#region Expediente_CambiarSituacion
		private void Expediente_CambiarSituacion()
		{
			Pop_cambioSitDG();
	
			#region ExpeSit
			expeSit.Edit();
			expeSit.Dat.VencimientoFecha.Value = this.txtFechaVencim.Text;
			expeSit.Dat.SituacionFecha	.Value = this.txtFechaSituacion.Text;
			expeSit.Dat.Obs.Value				= this.txtObservacion.Text;
			expeSit.PostEdit();
			#endregion ExpeSit
			
			#region AsignarValores segun Situacion
			switch ( SituacionActualID )
			{
				case  (int) Berke.Libs.Base.GlobalConst.Situaciones.PRESENTADA:
					expe.Edit();
					expe.Dat.AgenteLocalID.Value	= ddlAgenteLocal.SelectedValue;
					expe.Dat.ActaNro.Value			= txtActaNro.Text;
					expe.Dat.ActaAnio.Value			= this.txtActaAnio.Text;
					expe.PostEdit();
					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA:
					expe.Edit();
					expe.Dat.DiarioID.Value	= ddlDiario.SelectedValue;
					expe.Dat.PublicPag.Value	= txtPubicPagina.Text;
					expe.Dat.PublicAnio.Value	= txtPublicAnio.Text;
					expe.PostEdit();

					break;


				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONCEDIDA:
					regRen.Edit();
					regRen.Dat.RegistroNro.Value	= txtRegistroNro.Text;
					regRen.Dat.VencimientoFecha.Value =	txtRegVto.Text;
                    //[ggaleano 21/11/2018] Actualizamos el campo ConcesionFecha de MarcaRegRen
                    //porque de aqui se toma la Fecha de Concesión
                    regRen.Dat.ConcesionFecha.Value = this.txtFechaSituacion.Text;
					regRen.PostEdit();

					expe.Edit();
					expe.Dat.VencimientoFecha.Value = txtRegVto.Text;
					expe.PostEdit();                    

                    // Agregado por mbaez. 16/11/2006
					/* No hace falta, se hace del lado del Action.
					expeSit.Edit();
					expeSit.Dat.Datos.Value = "Registro: "+ txtRegistroNro.Text +"/"+ regRen.Dat.RegistroAnio.AsString;
					expeSit.PostEdit();
					*/
					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.ARCHIVADA:
					expe.Edit();
					expe.Dat.Exp.Value = this.txtExp.Text;
					expe.Dat.Bib.Value = this.txtBib.Text;
					expe.PostEdit();
					break;

			}

			#endregion 
			
			Berke.Marcas.UIProcess.Model.Expediente.ModifCambioSituacion( cambioSitDG );
            this.Buscar();
		}
		#endregion Expediente_CambiarSituacion

		#region btnRecalcVencimReg_Click
		private void btnRecalcVencimReg_Click(object sender, System.EventArgs e)
		{
			this.recalcularFechaVenc();
		}
		private void recalcularFechaVenc() 
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB( );
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;

			bool existePadre = false;
			DateTime fVencim = DateTime.MinValue;
            DateTime fVencimSituacion = DateTime.MinValue;

			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expePadre = new Berke.DG.DBTab.Expediente( db );

			expe.Adapter.ReadByID( this.ExpedienteID );

			if( expe.Dat.TramiteID.AsInt == (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO )// Registro
			{ 
				//if( this.txtFechaSitActual.Text.Trim() != "" )
                if (this.txtFechaSituacion.Text.Trim() != "")
				{
					try
					{
						//fVencim = Convert.ToDateTime( txtFechaSitActual.Text );
                        fVencim = Convert.ToDateTime(this.txtFechaSituacion.Text);

                        //Nuevo Vencimiento Situacion
                        fVencimSituacion = Berke.Marcas.UIProcess.Model.TramiteSit.FechaVencim(fVencim, expe.Dat.TramiteSitID.AsInt);
                        this.txtFechaVencim.Text = string.Format("{0:d}", fVencimSituacion);
                        
                        fVencim = fVencim.AddYears( 10 );
						this.txtRegVto.Text = string.Format("{0:d}", fVencim );

                        
					}
					catch
					{
						ShowMessage( "Verifique la Fecha de Concesión ");
					}
				}
				else
				{
					ShowMessage( "Ingrese la Fecha de Concesión ");
				}
			}
			else if(expe.Dat.TramiteID.AsInt == (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION ) 
			{ // Renovacion

				if( ! expe.Dat.ExpedienteID.IsNull )
				{
					expePadre.Adapter.ReadByID( expe.Dat.ExpedienteID.AsInt );
					if( expePadre.RowCount > 0 )
					{
						existePadre = true;
					}
				}
				if( existePadre )
				{
					if( ! expePadre.Dat.VencimientoFecha.IsNull )
					{
						fVencim = expePadre.Dat.VencimientoFecha.AsDateTime.AddYears( 10 );
					}
				}
				db.CerrarConexion();

				if( fVencim != DateTime.MinValue )
				{
					this.txtRegVto.Text = string.Format("{0:d}", fVencim );
				}
				else
				{
					ShowMessage( "Expediente padre con información insuficiente para el calculo del vencimiento");
				}
			}
		}
		#endregion btnRecalcVencimReg_Click

		#region Expediente_RevertirSituacion
		private bool Expediente_RevertirSituacion()
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB( );
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;

			Pop_cambioSitDG();

			string tmp = expeSit.Dat.VencimientoFecha.AsString;

			if( expeSit.RowCount < 2 ){
				ShowMessage( "Para revertir, deben haber por lo menos un cambio de situacion previo");
				return false;
			}

			if( ! chkRevertir.Checked ){

				ShowMessage( @"CUIDADO!!!.Los datos de la última situación se perderán.\n Para revertir, marque el CheckBox y reintente");

				return false;
			}
			expeSit.GoTop();

			#region Obtener SituacionActualID
			Berke.DG.DBTab.Tramite_Sit	trmSit;
			Berke.DG.DBTab.Situacion	sit;

			trmSit	= UIPModel.TramiteSit.ReadByID_M( expeSit.Dat.TramiteSitID.AsInt );
			sit		= UIPModel.Situacion.ReadByID( trmSit.Dat.SituacionID.AsInt );		
			SituacionActualID = trmSit.Dat.SituacionID.AsInt;
			#endregion Obtener SituacionActualID

			#region Restaurar anterior TramiteSit en Expediente
			expeSit.Skip();   // El anterior
			int TramiteSitIDAnterior =  expeSit.Dat.TramiteSitID.AsInt;

			expe.Edit();
			expe.Dat.TramiteSitID.Value = TramiteSitIDAnterior;
			expe.PostEdit();

			#endregion Restaurar anterior TramiteSit en Expediente

			#region Eliminar Entrada en ExpedienteSituacion
			expeSit.GoTop();
			expeSit.Delete();
			#endregion
			
			string msgPattern = "Se ha revertido la situación {0} para el acta {1}/{2}. Verifique la información con el usuario: " + Berke.Libs.Base.Acceso.GetCurrentUser() +".";
			string msg = "";

			#region Reversion de Situaciones
			switch ( SituacionActualID )
			{
				case  (int) Berke.Libs.Base.GlobalConst.Situaciones.PRESENTADA:
		            msg += string.Format(msgPattern, "Presentada", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);			
					expe.Edit();
					expe.Dat.AgenteLocalID.SetNull();
					expe.Dat.ActaNro.SetNull();
					expe.Dat.ActaAnio.SetNull();
					expe.PostEdit();

					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA:
		            msg += string.Format(msgPattern, "Publicada", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);			
					expe.Edit();
					expe.Dat.DiarioID.SetNull();
					expe.Dat.PublicPag.SetNull();
					expe.Dat.PublicAnio.SetNull();
					expe.PostEdit();
					break;


				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONCEDIDA:
		            msg += string.Format(msgPattern, "Presentada", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);			
					regRen.Edit();

					// Agregado por mbaez. Se tiene poner como no vigente el registro.

					if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO ) ||
						(expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) ) 
					{
						regRen.Dat.RegistroNro.SetNull();
						regRen.Dat.RegistroAnio.SetNull();
						regRen.Dat.ConcesionFecha.SetNull();
						regRen.Dat.VencimientoFecha.SetNull();
						regRen.Dat.Vigente.Value = false;
						expe.Edit();
						expe.Dat.VencimientoFecha.SetNull();
						expe.PostEdit();
					}
					if (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION) 
					{
						// Si es necesario. Mbaez.
						mar.Edit();
						mar.Dat.MarcaRegRenID.Value = regRenPadre.Dat.ID.AsInt;
						mar.PostEdit();
						

						DateTime fec_actual = DateTime.Now; 
						fec_actual.AddMonths( -6 );

						if (fec_actual.CompareTo(regRenPadre.Dat.VencimientoFecha.AsDateTime) >= 0 ) 
						{
							regRenPadre.Edit();
							regRenPadre.Dat.Vigente.Value = true;
							regRenPadre.PostEdit();
						}
					}

					regRen.PostEdit();

//					expe.Edit();
//					expe.Dat.VencimientoFecha.SetNull();
//					expe.PostEdit();
					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.ARCHIVADA:
		            msg += string.Format(msgPattern, "Archivada", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);			
					expe.Edit();
					expe.Dat.Exp.SetNull();
					expe.Dat.Bib.SetNull();
					// Agregado por mbaez. Debe actualizarse el campo "Concluido"
					// de modo a revertir ARCHIVADA
					expe.Dat.Concluido.Value	= false;
					expe.PostEdit();

					/*
					 Ver BUG#476
					 - Al revertir ARCHIVADA se debe poner la marca a Activa  
				   */
					if( mar.Dat.StandBy.AsBoolean ) 
					{
						#region Marca se pone Activa
						mar.Edit();
						mar.Dat.Vigente.Value		= true;
						mar.PostEdit();
						#endregion
					}



					break;

				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CANCELACION_REG:
					/*
					ESTE CASO SE PROCESO EN EL ACTION Expediente.ModifCambioSituacion
					*/
					break;

			}
			#endregion 
		
			// Agregado. Se modifica el campo standby de acuerdo a la 
			// situacion de destino.

			#region Actualizar StandBy Segun Situacion Destino

			Berke.DG.DBTab.Tramite_Sit	trmSitAnt = UIPModel.TramiteSit.ReadByID_M( TramiteSitIDAnterior );
			Berke.DG.DBTab.Situacion sitDest	  = UIPModel.Situacion.ReadByID( trmSitAnt.Dat.SituacionID.AsInt );	

			if( ! sitDest.Dat.StandBy.IsNull )
			{
				expe.Edit();
				expe.Dat.StandBy.Value = sitDest.Dat.StandBy.AsBoolean;
				expe.PostEdit();
				if( expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
					expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION ){
					mar.Edit();
					mar.Dat.StandBy.Value = sitDest.Dat.StandBy.AsBoolean;
					mar.PostEdit();
				}
			}
			
			#endregion Actualizar StandBy Segun Situacion Destino

			try 
			{
				Berke.Marcas.UIProcess.Model.Expediente.ModifCambioSituacion( cambioSitDG );
				if (msg != "")
				{
					db.IniciarTransaccion();
					Berke.Marcas.BizActions.Lib.Notificar((int)GlobalConst.Notificacion.SIT_REVERSION,msg,db);
					db.Commit();
				}
			}
			catch(Exception ex)
			{
				ShowMessage("Error al revertir situación: " + ex.Message);
				db.CerrarConexion();
				return false;
			}
			db.CerrarConexion();
			return true;
		}
		#endregion Expediente_RevertirSituacion
			
	
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion

		#region Boton Revertir
		public void btnRevertir_Click(object sender, System.EventArgs e)
		{
			if ( Expediente_RevertirSituacion()){
				MostrarPanel_Busqueda();
				this.Buscar();
				ShowMessage( @"Reversión Completa y grabada en la base de Datos");	
			};
		}
		#endregion Boton Revertir



        
}// End Class ( Form )
}
