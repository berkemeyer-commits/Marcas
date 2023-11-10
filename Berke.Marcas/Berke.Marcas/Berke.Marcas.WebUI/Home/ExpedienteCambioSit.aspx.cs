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
	#region Using
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Helpers;
	#endregion Using

	public partial class ExpedienteCambioSit : System.Web.UI.Page
	{
		#region Controles del Form
		protected System.Web.UI.WebControls.Label lblTitulo;
		protected System.Web.UI.WebControls.Label Label9;

		protected System.Web.UI.WebControls.Label Label1;

		#endregion Controles del Form	

		#region Properties

		#region RountripCounter
		private int RountripCounter
		{
			get{ return Convert.ToInt32( ViewState["RountripCounter"]) ; }
			set{ ViewState["RountripCounter"] = Convert.ToString( value );}
		}
		#endregion RountripCounter

		#region ExpedienteID
		private int ExpedienteID
		{
			get{ return Convert.ToInt32(( ViewState["ExpedienteID"] == null )? -1 : ViewState["ExpedienteID"] ) ; }
			set{ ViewState["ExpedienteID"] = Convert.ToString( value );}
		}
		#endregion ExpedienteID

		#endregion Properties

		#region Asignar Delegados
		private void AsignarDelegados()
		{
			//DropDown Tramite
			this.ddlTramite.SelectedIndexChanged += new System.EventHandler(this.ddlTramite_SelectedIndexChanged);

			//DropDown TramiteSitDestino
			this.ddlTramiteSitDestino.SelectedIndexChanged += new System.EventHandler(this.ddlTramiteSitDestino_SelectedIndexChanged);
		

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Llenar DropDpwn de Tramites
	//		SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( (int) Const.Proceso.MARCAS );
	//		ddlTramite.Fill( se.Tables[0], true);

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramite.Fill( lst.Table, true);
			#endregion

			#region llenar DropDown de AgenteBerke
			Berke.DG.ViewTab.ListTab agLoc =  Berke.Marcas.UIProcess.Model.AgenteBerke.ReadForSelect();
			ddlAgenteLocal.Fill( agLoc.Table, true);
			#endregion

			#region llenar DropDown de Unidad
			Berke.DG.ViewTab.ListTab unidad =  Berke.Marcas.UIProcess.Model.Unidad.ReadForSelect( );
			ddlUnidad.Fill( unidad.Table, true );
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

			#region llenar DropDown de Diario
			SimpleEntryDS diarioSE = UIPModel.Diario.ReadForSelect();
			ddlDiario.Fill( diarioSE.Tables[0], true );
			#endregion

			txtNroAnio.Text			= DateTime.Today.Year.ToString();
			txtRegistroAnio.Text	= DateTime.Today.Year.ToString();

			DateTime fechaCambioSit = DateTime.Today;

			txtFecha.Text	= fechaCambioSit.ToString("d");
			txtHora.Text	= "7:00"; // fechaCambio.ToString("t");

		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
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
			this.dgMarcas.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMarcas_ItemCommand);

		}
		#endregion

		#region Buscar
		private void Buscar(){

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

			#region Eliminar los duplicados
			int idAnt = -77;
			for( vExpe.GoTop() ; ! vExpe.EOF ; vExpe.Skip() )
			{
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
			#endregion Eliminar los duplicados
			
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
			else{
				MostrarPanel_Resultado();
			}
			#endregion
				
		}

		protected void btnBuscar_Click(object sender, System.EventArgs e){
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

		#region MostrarPanel_Modificar
		private void MostrarPanel_Modificar( int expedienteID )
		{
			Berke.DG.ViewTab.vExpeMarca vExpe =  UIPModel.ExpeMarca.ReadByID( expedienteID );

			#region Hacer visibles el panel de modificacion
			this.pnlActualizar.Visible	= true;
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= false;

			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;
			this.pnlPlazo.Visible		= false;

			#endregion 
		
			#region Hacer visible el boton de aceptar
			this.btnAceptar.Enabled = true;
			#endregion
			
			#region Asignar Valores de display a TexBoxs
			this.lblExpeID.Text		= vExpe.Dat.ExpedienteID.AsString+ HtmlGW.Spaces(20) + ( vExpe.Dat.ExpeNuestro.AsBoolean ? "" : "Exped.de Terceros" );
			this.lblActa.Text		= vExpe.Dat.Acta.AsString;
			this.lblClase.Text		= vExpe.Dat.Clase.AsString;
			this.lblHI.Text			= vExpe.Dat.OrdenTrabajo.AsString;
			this.lblMarca.Text		= vExpe.Dat.Denominacion.AsString;
			this.lblTramite.Text	= vExpe.Dat.TramiteDescrip.AsString;
			this.lblSituacion.Text	= vExpe.Dat.SituacionDecrip.AsString;
			this.lblRegistro.Text	= vExpe.Dat.Registro.AsString;
			this.lblVencim.Text		= vExpe.Dat.VencimientoFecha.AsString;
			this.lblMarcaDescrip.Text= vExpe.Dat.Sustituida.AsBoolean	? "Sustituida " : "No-Sustituida"	;
			#endregion Asignar Valores de display a TexBoxs

			if( vExpe.Dat.ExpeNuestro.AsBoolean &&  !vExpe.Dat.Sustituida.AsBoolean)
			{
				#region asigna los destinos siguientes a la situacion Actual		
				Berke.DG.ViewTab.ListTab sitSgte =  UIPModel.TramiteSitSgte.ReadForSelect(  vExpe.Dat.TramiteSitID.AsInt );
				if( sitSgte.RowCount > 0 )
				{
					this.ddlTramiteSitDestino.Fill( sitSgte.Table, true );
				}
				else
				{
					this.btnAceptar.Enabled = false;
				}
				#endregion asigna los destinos siguientes a la situacion Actual
			}
			else
			{
				#region Asigna todos los destinos posibles del Tramite
				Berke.DG.ViewTab.ListTab sitSgte =  UIPModel.TramiteSit.ReadByTramiteID_AsListTab( vExpe.Dat.TramiteID.AsInt );
				if( sitSgte.RowCount > 0 )
				{
					this.ddlTramiteSitDestino.Fill( sitSgte.Table, true );
				}
				else
				{
					this.btnAceptar.Enabled = false;
				}

				#endregion Asigna todos los destinos posibles del Tramite	
			}
			ddlTramiteSitDestino.SelectedIndex = 0;
		}
		#endregion MostrarPanel_Modificar

		#region Boton de Recalcular Fecha Vencimiento
		protected void btnRecalcVencim_Click(object sender, System.EventArgs e)
		{
			this.recalcularFechaVenc();															
		}
		/** Se recalcula la fecha de vencimiento.*/
		private void recalcularFechaVenc()
		{
			DateTime	fechaCambioSit	= ObjConvert.AsDateTime( txtFecha.Text );
			int			plazo			= ObjConvert.AsInt( this.txtPlazo.Text );
			int			unidadID		= ObjConvert.AsInt( this.ddlUnidad.Value );
			txtFechaVencim.Text	= Berke.Marcas.UIProcess.Model.Fecha.SumarPlazo(fechaCambioSit,
																				plazo ,unidadID ).ToString("d");			
		}
		#endregion

		#region Elegir de la grilla    ItemCommand
		private void dgMarcas_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			int expeID  = ObjConvert.AsInt( e.Item.Cells[0].Text );
			
			ExpedienteID = expeID;
			MostrarPanel_Modificar( expeID );
		}
		#endregion

		#region Boton Cancelar
		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			MostrarPanel_Resultado();
		}
		#endregion

		#region Boton Aceptar
		protected void btnAceptar_Click(object sender, System.EventArgs e)
		{
			// Se recalcula la fecha de vencimiento implicitamente.
			// De esta manera evitamos que el usuario se olvide 
			// y se creen incosistencias. Mbaez 16/11/2006
			if (pnlPlazo.Visible)
			{
				this.recalcularFechaVenc();
			}
			if (pnlRegistro.Visible) 
			{
				this.recalcularFechaVencRegistro();
			}

			if ( pnlRegistro.Visible && 
				 (txtRegistroAnio.Text == "" || txtRegistroNro.Text == "")
			   )
			{
				ShowMessage("Debe completar todos los datos del registro: Nro. Registro / Año. Registro.");	
				return;
			}

			Expediente_CambiarSituacion();
			txtObservacion.Text = "";
			Buscar();
			MostrarPanel_Resultado();
		}
		#endregion

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


		private void ddlTramiteSitDestino_SelectedIndexChanged(object sender, EventArgs e)
		{

			#region Leer TramiteSit
			int tramiteSitID = ObjConvert.AsInt( ddlTramiteSitDestino.Value );

			Berke.DG.DBTab.Tramite_Sit tramSit_Param =   new Berke.DG.DBTab.Tramite_Sit();

			tramSit_Param.NewRow(); 
			tramSit_Param.Dat.ID	.Value = tramiteSitID; 
			tramSit_Param.PostNewRow(); 
		
			Berke.DG.DBTab.Tramite_Sit tramSit =  Berke.Marcas.UIProcess.Model.TramiteSit.ReadByParam( tramSit_Param );
		
			int plazo		= tramSit.Dat.Plazo.AsInt;
			int unidadID	= tramSit.Dat.UnidadID.AsInt;
			#endregion

			#region Asignar valores para Plazo 
	

			DateTime fechaCambioSit = Convert.ToDateTime( txtFecha.Text );

			if( tramSit.Dat.Plazo.AsInt == 0 )
			{
				this.pnlPlazo.Visible = false;
			}
			else
			{
				this.pnlPlazo.Visible = true;

				this.txtPlazo.Text	= tramSit.Dat.Plazo.AsString;
				ddlUnidad.Value = tramSit.Dat.UnidadID.AsString;

				txtFechaVencim.Text	= Berke.Marcas.UIProcess.Model.Fecha.SumarPlazo(
					fechaCambioSit,
					plazo ,
					unidadID ).ToString("d");
			}

			#endregion

			#region Mostrar Paneles segun Situacion
			this.pnlAgenteLocal.Visible	= false;
			this.pnlPublicacion.Visible	= false;
			this.pnlRegistro.Visible	= false;	
			this.pnlArchivada.Visible	= false;

			switch ( tramSit.Dat.SituacionID.AsInt )
			{
				case  (int) Berke.Libs.Base.GlobalConst.Situaciones.PRESENTADA:
					//Pasar a Sit Presentada
					this.pnlAgenteLocal.Visible	= true;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= false;	
					//						Response.Redirect( "SitPresentadaAME.aspx" );
					break;
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA:
					//Pasar a Sit Publicada
					//						Response.Redirect( "SitPublicada.aspx" );
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= true;
					this.pnlRegistro.Visible	= false;	

					break;
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.CONCEDIDA:
					//Pasar a Sit Ingresar Concesion
					//						Response.Redirect( "SitConcedidaAME.aspx" );

					this.txtRegVto.Text	= Berke.Marcas.UIProcess.Model.Fecha.SumarPlazo(
						fechaCambioSit,
						10 ,
						3 ).ToString("d"); // 10 años
	
					this.pnlAgenteLocal.Visible	= false;
					this.pnlPublicacion.Visible	= false;
					this.pnlRegistro.Visible	= true;	
					break;
				case (int) Berke.Libs.Base.GlobalConst.Situaciones.ARCHIVADA:
					this.pnlArchivada.Visible	= true;
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



		#endregion DropDown IndexChanged 
	
		#region Expediente_CambiarSituacion
		private Berke.DG.ViewTab.ParamTab	Expediente_CambiarSituacion( )
		{
		
			#region Asignar Parametros
		
				
		
			// CambioSitParam
			Berke.DG.ViewTab.CambioSitParam inTB =   new Berke.DG.ViewTab.CambioSitParam();

			inTB.NewRow(); 
			inTB.Dat.ExpedienteID		.Value = ExpedienteID; 
			inTB.Dat.TramiteSitDestinoID.Value = this.ddlTramiteSitDestino	.Value;
			inTB.Dat.SitFecha			.Value = this.txtFecha				.Text;
			inTB.Dat.SitHora			.Value = this.txtHora				.Text;
			inTB.Dat.Plazo				.Value = this.txtPlazo				.Text;
			inTB.Dat.UnidadID			.Value = this.ddlUnidad				.Value;
			inTB.Dat.SitVencim			.Value = this.txtFechaVencim		.Text;
			inTB.Dat.AgenteLocalID		.Value = this.ddlAgenteLocal		.SelectedValue;
			inTB.Dat.NroActa			.Value = this.txtActaNro			.Text;
			inTB.Dat.AnioActa			.Value = this.txtActaAnio			.Text;
			inTB.Dat.NroRegistro		.Value = this.txtRegistroNro		.Text;  
			inTB.Dat.AnioRegistro		.Value = this.txtRegistroAnio		.Text;
			inTB.Dat.DiarioID			.Value = this.ddlDiario				.Value;
			inTB.Dat.PublicPagina		.Value = this.txtPubicPagina		.Text;
			inTB.Dat.PublicAnio			.Value = this.txtPublicAnio			.Text;
			// Archivada
			inTB.Dat.Bib				.Value = txtBib.Text; 
			inTB.Dat.Exp				.Value = txtExp.Text;   
			// -- 
			inTB.Dat.RegVencim			.Value = this.txtRegVto.Text;
		
			inTB.Dat.Obs				.Value = this.txtObservacion.Text;

			inTB.PostNewRow(); 
		
	
			#endregion Asignar Parametros
		
			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.Expediente.CambiarSituacion( inTB );
			return outTB;
		}
		#endregion Expediente_CambiarSituacion

		#region btnRecalcVencimReg_Click
		protected void btnRecalcVencimReg_Click(object sender, System.EventArgs e)
		{
			this.recalcularFechaVencRegistro();
		}
		private void recalcularFechaVencRegistro () 
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB( );
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;

			bool existePadre = false;
			DateTime fVencim = DateTime.MinValue;

			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expePadre = new Berke.DG.DBTab.Expediente( db );

			expe.Adapter.ReadByID( this.ExpedienteID );

			if( expe.Dat.TramiteID.AsInt ==  Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) )// Registro
			{ 
				if( this.txtFecha.Text.Trim() != "" )
				{
					try
					{
						fVencim = Convert.ToDateTime( txtFecha.Text );
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
			else if(expe.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)) 
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

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion

	} // end  class ExpedienteCambioSit ( Form )
}// end namespace Berke.Marcas.WebUI.Home
