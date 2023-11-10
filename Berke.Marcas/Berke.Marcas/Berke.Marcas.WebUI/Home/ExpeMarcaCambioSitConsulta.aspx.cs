#region Usings
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
using Berke.Libs;

#endregion Usings

namespace Berke.Marcas.WebUI.Home
{
	#region Using
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	#endregion Using

	public partial class ExpeMarcaCambioSitConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form





		protected System.Web.UI.WebControls.Label lblActaNro;
		protected System.Web.UI.WebControls.TextBox txtActaNro;

		protected System.Web.UI.WebControls.Label lblRegistroNro;
		protected System.Web.UI.WebControls.TextBox txtRegistroNro;



		protected System.Web.UI.WebControls.Label lblAltaFecha;
		//protected System.Web.UI.WebControls.TextBox txtAltaFecha;



		protected System.Web.UI.WebControls.Button btnGenExcel;
		#endregion 
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{

//			this.cbxAgenteLocalID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxAgenteLocalID_LoadRequested); 
//			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
//
			ddlTramiteID.SelectedIndexChanged+= new EventHandler(ddlTramiteID_SelectedIndexChanged);



		}
		#endregion Asignar Delegados

		int  LIMITE_REC = 100000;
		bool agregarLink = false;

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		
			#region Llenar DropDpwn de Tramites
	
//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
//			ddlTramiteID.Fill( se.Tables[0], true);	

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);
//[ggaleano 26/09/2007] - Ref. Bug#548. Se carga el DropDown con todas las situaciones inicialmente.
			this.CargarddlTramiteSitID();


			#endregion

			#region Funcionario DropDown
			Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlFuncionario.Fill( seFuncionario.Table, true);
			this.ddlInstrucFunc.Fill(seFuncionario.Table, true);
			#endregion Funcionario DropDown
		
	
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
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
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}
		#endregion
		
		#region AplicarFiltro
		private void AplicarFiltro( Berke.DG.ViewTab.vMarcaCambioSit view  ){
			string defWhere = "";
			
			#region Determinar TramiteSitID a utilizar

			/*[ggaleano 26/09/2007] - Ref. Bug#548
			 * Para permitir la consulta de cambio de situación de expedientes sólo por
			 * SituacionID sin necesidad de ingresar el trámite se introdujo la siguiente porción de código.
			 * Si sólo se seleccionó la situación y no el trámite se genera una lista de todos los trámites
			 * que tienen asociada esa situación y mediante esto se realiza el filtro*/
			string situacionid = "";

			if ((ddlTramiteID.SelectedIndex < 1) && (ddlTramiteSitID.SelectedIndex >= 1))
			{
				Berke.DG.DBTab.Tramite_Sit Tramite_Sit = new Berke.DG.DBTab.Tramite_Sit(view.Adapter.Db);
				Tramite_Sit.ClearFilter();
				Tramite_Sit.Dat.SituacionID.Filter = Convert.ToInt32(ddlTramiteSitID.SelectedValue);
				Tramite_Sit.Adapter.ReadAll();

				for (Tramite_Sit.GoTop(); !Tramite_Sit.EOF; Tramite_Sit.Skip())
				{
					if (situacionid != "")
					{
						situacionid += ",";
					}
					situacionid += Tramite_Sit.Dat.ID.AsString;
				}
			}
			else
			{ 
				situacionid = ddlTramiteSitID.SelectedValue;
			}
			#endregion Determinar TramiteSitID a utilizar
			
			if( chkSitActual.Checked )
			{
				defWhere = " dbo.Expediente_Situacion.TramiteSitID = dbo.Expediente.TramiteSitID ";
				view.Dat.TramiteSitID_Actual .Filter = ObjConvert.GetFilter(situacionid);
			}
			else
			{
				view.Dat.TramiteSitID	.Filter = ObjConvert.GetFilter(situacionid);
			}
			if( chkObs.Checked )
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}
				defWhere += " dbo.Expediente_Situacion.Obs is not null and dbo.Expediente_Situacion.Obs <> '' ";
			}

            if (!chkMarcasInactivas.Checked)
            {
                if (defWhere != "")
                {
                    defWhere += " and ";
                }
                defWhere += " dbo.Marca.Vigente = 1 ";
            }

			if( chkUltimaSituacion.Checked )
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}
				defWhere +=	"dbo.Expediente_Situacion.SituacionFecha = (SELECT max(expeSitu.SituacionFecha) " +
							"FROM dbo.Expediente_Situacion  expeSitu " +	
							"WHERE expeSitu.ExpedienteID = dbo.Expediente_Situacion.ExpedienteID " + 
							"AND expeSitu.TramiteSitID = dbo.Expediente_Situacion.TramiteSitID)";
			}
			/*if( !chkAbandonadas.Checked )
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}
				defWhere +=	"dbo.Expediente.tramiteSitID <> (SELECT ID " +
							"FROM Tramite_Sit " +
							"WHERE ID = " + ddlTramiteID.SelectedValue +
							" AND SituacionID = " + Convert.ToInt32(Berke.Libs.Base.GlobalConst.Situaciones.ABANDONADA) + ")";
			}*/


			if( defWhere != "" ){
				view.Adapter.SetDefaultWhere( defWhere );
			}
			
			
			view.Dat.ClienteID		.Filter = ObjConvert.GetFilter	    ( txtClienteID.Text.Trim());
			view.Dat.ClienteNombre  .Filter = ObjConvert.GetFilter_Str	( txtClienteNombre.Text.Trim());

			//view.Dat.ClienteID		.Filter = ObjConvert.GetFilter	( cbxClienteID.SelectedValue);
			view.Dat.RegistroAnio	.Filter = ObjConvert.GetFilter		( txtRegistroAnio.Text);	
			view.Dat.RegistroNro	.Filter = ObjConvert.GetFilter		( txtRegistroNro_min.Text );
			view.Dat.ExpeID			.Filter = ObjConvert.GetFilter		( txtExpeID_min.Text);

			if( txtTrmList.Text.Trim() == "" )
			{
				view.Dat.TramiteID	.Filter = ObjConvert.GetFilter		( ddlTramiteID.SelectedValue );
			}
			else{
				view.Dat.TramiteID	.Filter = ObjConvert.GetFilter( txtTrmList.Text.Trim() );	
			}
			//view.Dat.TramiteSitID	.Filter = ObjConvert.GetFilter		( ddlTramiteSitID.SelectedValue);
			//view.Dat.TramiteSitID_Actual .Filter = ObjConvert.GetFilter		( ddlTramiteSitID.SelectedValue);
			view.Dat.ActaNro		.Filter = ObjConvert.GetFilter		( txtActaNro_min.Text);
			view.Dat.ActaAnio		.Filter = ObjConvert.GetFilter		( txtActaAnio.Text);
			view.Dat.Denominacion	.Filter = ObjConvert.GetFilter_Str	( txtDenominacion.Text);

			view.Dat.ClaseNro       .Filter = ObjConvert.GetFilter		( txtClaseNro.Text);

			view.Dat.AltaFecha		.Filter = ObjConvert.GetFilter_Date	( txtAltaFecha_min.Text);
            view.Dat.SituacionFecha	.Filter = ObjConvert.GetFilter_Date	( txtSitFecha.Text );
            view.Dat.FuncionarioID	.Filter = ObjConvert.GetFilter		( ddlFuncionario.SelectedValue);
			//view.Dat.Nuestra		.Filter	= ObjConvert.GetFilter_Bool	( ddlNuestra.SelectedValue);
			view.Dat.Vigilada		.Filter = ObjConvert.GetFilter_Bool	( rbVigilada.SelectedValue);
			view.Dat.EnTramite		.Filter = ObjConvert.GetFilter_Bool	( rbEnTramite.SelectedValue);
			view.Dat.SitVencimientoFecha.Filter = ObjConvert.GetFilter	( txtVencimPlazo.Text );
			view.Dat.InstrucAbrev.Filter	= ObjConvert.GetFilter( this.txtInstruc.Text );
			view.Dat.InstrucFuncID.Filter	= ObjConvert.GetFilter		( this.ddlInstrucFunc.SelectedValue);
			//view.Dat.expeVigilada.Filter    = ObjConvert.GetFilter_Bool ( rbExpePropio.SelectedValue );
			view.Dat.expeNuestra.Filter = ObjConvert.GetFilter_Bool ( rbExpePropio.SelectedValue );
			view.Dat.marSustituida.Filter = ObjConvert.GetFilter_Bool(rbSustituida.SelectedValue);
			view.Dat.expeStandBy.Filter     = ObjConvert.GetFilter_Bool ( rbEnStandBy.SelectedValue );

            view.Dat.Nuestra.Filter = ObjConvert.GetFilter_Bool(rbMarcaNuestra.SelectedValue);
			
		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vMarcaCambioSit view)
		{
			view.ClearOrder();
			view.Dat.ExpeSitID.Order	= 1; // Para eliminar los duplicados
		
		}
		#endregion SetOrder

		#region Reordenar
		private void Reordenar(  Berke.DG.ViewTab.vMarcaCambioSit view)
		{
			view.ClearOrder();
			view.Dat.AltaFecha.Order = 1;
			view.Sort();
		}
		#endregion Reordenar

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vMarcaCambioSit view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			Berke.DG.DBTab.Tramite_Sit Tramite_Sit = new Berke.DG.DBTab.Tramite_Sit(db);
            			
			mensajeError = "";
			int recuperados = -1;
			try 
			{
				recuperados = view.Adapter.Count();
				if( recuperados != 0 )
				{
					if( recuperados < limite )
					{
						view.Adapter.ReadAll( limite );
						#region Eliminar duplicados por tener mas de una instruccion
						int antID = -992277;
						for( view.GoTop(); !view.EOF ; view.Skip() ){
							if( view.Dat.ExpeSitID.AsInt == antID ){
								view.Delete();
							}else{
								antID = view.Dat.ExpeSitID.AsInt;
							}
							/* [ggaleano 08/07/2007] En caso de que los registros recuperados
							 * estén en StandBy se puede optar por incluir o no aquellos 
							 * que tengan como situación "Abandonada" */
							 
							Tramite_Sit.ClearFilter();
							Tramite_Sit.Dat.TramiteID.Filter = view.Dat.TramiteID.AsInt;
							Tramite_Sit.Dat.SituacionID.Filter = Convert.ToInt32(Berke.Libs.Base.GlobalConst.Situaciones.ABANDONADA);
							Tramite_Sit.Adapter.ReadAll();
							if (((rbEnStandBy.SelectedIndex == 0) || (rbEnStandBy.SelectedIndex == 1)) 
								&& (chkAbandonadas.Checked)
								&& (view.Dat.TramiteSitID_Actual.AsInt == Tramite_Sit.Dat.ID.AsInt))
							{
								view.Delete();
							}
						}
						view.AcceptAllChanges();
						Reordenar( view );
						view.GoTop();
						#endregion Eliminar duplicados ...


						#region Agregar Link
						if( agregarLink )
						{
							for( view.GoTop(); !view.EOF ; view.Skip() )
							{

						
								string denom = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link( 
									"MarcaDetalleL.aspx", 			// Pagina
									view.Dat.Denominacion.AsString,	// Texto
									view.Dat.ExpeID.AsString,		// Valor del parametro
									"ExpeID");						// Nombre del parametro


								view.Edit();
								view.Dat.Denominacion.Value = denom;
								view.PostEdit();
							}
						}
						#endregion Agregar Link

						resultado = true;
					}
					else
					{
						mensajeError = "Los " + recuperados.ToString()+ " registros a recuperar exceden el limite de "+ limite.ToString();
					}
				}
				else{
					mensajeError = "No se encontró ningún registro ";		
				}
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				mensajeError = "Los " + ex.Recuperados.ToString()+ " registros a recuperar exceden el limite de "+ ex.Limite.ToString();
			}
			catch( Exception exep ) 
			{
				mensajeError = this.GetType().Name + " ERROR:"+ exep.Message;
			}
			return resultado;
		}
		#endregion ObtenerDatosOk

		#region ParametrosOk
		private bool parametrosOK (out string mensajeError ){
		  mensajeError = "";
		  return true;
		}
		#endregion ParametrosOk


        protected bool GenerateBindString(object dataItem)
        {
            bool ret = false;

            // if column is null set checkbox.checked = false

            if (dataItem.ToString() == "")
                ret = false;
            else // set checkbox.checked to boolean value in Status column
                ret = (bool)dataItem;

            return ret;
        }

		#region AplicarBusqueda
		private void AplicarBusqueda(
										Berke.DG.ViewTab.vMarcaCambioSit vMarcaCambioSit,
										int limite,
										DataGrid dgResult,
										out string mensajeError,
										Berke.Libs.Base.Helpers.AccesoDB db	
									){
			if( parametrosOK( out mensajeError ) )
			{	
				AplicarFiltro( vMarcaCambioSit );
				SetOrder( vMarcaCambioSit );
				if( ObtenerDatosOk( vMarcaCambioSit, limite, out mensajeError, db ))
				{
					dgResult.DataSource = vMarcaCambioSit.Table;
					dgResult.DataBind();

					lblTituloGrid.Text = "Resultado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vMarcaCambioSit.RowCount+ "&nbsp;regs.)";
					MostrarPanel_Resultado();
				}
			}
		}
		#endregion AplicarBusqueda

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
			agregarLink = true;
			LIMITE_REC = int.Parse( this.txtLimite.Text );

			string mensajeError = "";
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vMarcaCambioSit vMarcaCambioSit = new Berke.DG.ViewTab.vMarcaCambioSit( db );
			AplicarBusqueda( vMarcaCambioSit, LIMITE_REC, dgResult, out mensajeError, db );
	
			agregarLink = false;
			if(mensajeError != "" ) {
				ShowMessage( mensajeError );
			}
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Cambios de Situación";
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= true;
		}
		#endregion MostrarPanel_Busqueda

		#region MostrarPanel_Resultado
		private void MostrarPanel_Resultado()
		{
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= true;
			this.pnlBuscar.Visible		= true;			
		}
		#endregion MostrarPanel_Resultado

		#region Carga de Combo


		#region AgenteLocal
		private void cbxAgenteLocalID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )	
			{
				inTB.Dat.Entero .Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa	.Value = combo.Text;   //String	
			}
			inTB.PostNewRow(); 				
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. AgenteLocal.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion AgenteLocal		

		#region Cliente
		private void cbxClienteID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )	
			{
				inTB.Dat.Entero .Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa	.Value = combo.Text;   //String	
			}
			inTB.PostNewRow(); 				
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Cliente.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Cliente		




		#endregion Carga de Combo

		#region Eventos
		private void ddlTramiteID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( ddlTramiteID.SelectedIndex < 1 )
			{
				//ddlTramiteSitID.Items.Clear();
				//[ggaleano 26/09/2007] - Ref. Bug#548. Se carga el DropDown con todas las situaciones.
				//Cargar ddlTramiteSitID con Situaciones en general
				this.CargarddlTramiteSitID();
			}
			else
			{
				SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramiteID.SelectedValue ) );
				ddlTramiteSitID.Fill( situacion.Tables[0], true );
				
			}
		}

		protected void btnGenDocum_Click(object sender, System.EventArgs e)
		{
            this.GenerarReporte();
		}
		#endregion Eventos

		#region GenerarReporte
		private void GenerarReporte()
		{
			//---------------
			string mensajeError = "";
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vMarcaCambioSit vMarcaCambioSit = new Berke.DG.ViewTab.vMarcaCambioSit( db );
			if( parametrosOK( out mensajeError ) )
			{
				AplicarFiltro( vMarcaCambioSit );
				SetOrder( vMarcaCambioSit );
				if( ObtenerDatosOk( vMarcaCambioSit, LIMITE_REC, out mensajeError, db ))
				{
					GenerarDocumento( vMarcaCambioSit );
				}
			}
		
			if(mensajeError != "" ) 
			{
				ShowMessage( mensajeError );
			}
			//----------
		}
		#endregion GenerarReporte

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

		#region GenerarDocumento
		private void GenerarDocumento( Berke.DG.ViewTab.vMarcaCambioSit vMarcaCambioSit )
		{
			int idiomaID =  2;

			#region Leer Plantilla
			//			string template = Berke.Libs.Base.Helpers.Files.GetStringFromFile(@"c:\temp\CambioSit.xml");

			string template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambioSit", idiomaID );
			if( chkObs.Checked ){
				template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("CambioSit_Obs", idiomaID );
			}
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );

			CodeGenerator tabla		= cg.ExtraerTabla("tabla" );
			CodeGenerator filaTit	= tabla.ExtraerFila("filaTit" );
			CodeGenerator fila		= tabla.ExtraerFila("fila" );
			string buf = fila.Template;
			buf = filaTit.Template;
			buf = tabla.Template;
			buf = cg.Template;
			CodeGenerator encabezado= cg.ExtraerTabla("encabezado" );//Encabezado Ultimo
			#endregion Obtener "Generators"

			
			cg.clearText();
			cg.copyTemplateToBuffer();

			#region Encabezado
			string rango_fecha="";

			if (txtSitFecha.Text != "") 
			{
				rango_fecha = txtSitFecha.Text.ToString();
			} 
			else if ( txtAltaFecha_min.Text != "" ) {
			    rango_fecha = txtAltaFecha_min.Text.ToString();
	        }

			encabezado.clearText();
			encabezado.copyTemplateToBuffer();
			encabezado.replaceField("Hoy", string.Format("{0:d}", DateTime.Today));
			encabezado.replaceField("rango_fecha", rango_fecha);
			encabezado.addBufferToText();
			cg.replaceLabel( "encabezado", encabezado.Texto );

			filaTit.clearText();
			filaTit.copyTemplateToBuffer();
			filaTit.addBufferToText();

			#endregion Encabezado
			tabla.clearText();
			tabla.copyTemplateToBuffer();
			fila.clearText();
			int contador = 0;
			for(  vMarcaCambioSit.GoTop(); ! vMarcaCambioSit.EOF; vMarcaCambioSit.Skip() )
			{
				contador++;
				//string bib = vMarcaCambioSit.Dat.Bib.AsString + "/" +vMarcaCambioSit.Dat.Exp.AsString;
				string pub = vMarcaCambioSit.Dat.PublicPag.AsString + "/" + vMarcaCambioSit.Dat.PublicAnio.AsString;
						
				fila.copyTemplateToBuffer();
				fila.replaceField("ExpeID", vMarcaCambioSit.Dat.ExpeID.AsString );
				string denom = vMarcaCambioSit.Dat.Denominacion.AsString;
				fila.replaceField("DENOM",  denom );
				fila.replaceField("ExpeID",	vMarcaCambioSit.Dat.ExpeID.AsString);
				fila.replaceField("DENOM",	vMarcaCambioSit.Dat.Denominacion.AsString);
				fila.replaceField("Reg",	vMarcaCambioSit.Dat.RegistroNro.AsString);
				fila.replaceField("Cls",	vMarcaCambioSit.Dat.ClaseNro.AsString );
				fila.replaceField("Tram",	vMarcaCambioSit.Dat.tramiteAbrev.AsString);
                //fila.replaceField("Sit",	vMarcaCambioSit.Dat.SituacionDescrip.AsString );
                fila.replaceField("Sit", vMarcaCambioSit.Dat.SitAbrev.AsString);
				fila.replaceField("Fecha",	vMarcaCambioSit.Dat.SituacionFecha.AsString );
				fila.replaceField("Vencim", vMarcaCambioSit.Dat.SitVencimientoFecha.AsString );
				fila.replaceField("Acta",	vMarcaCambioSit.Dat.Acta.AsString );
				fila.replaceField("Obs",	vMarcaCambioSit.Dat.Obs_CambioSit.AsString );
				//fila.replaceField("Bib",	bib );
				/*[ggaleano 24/04/2008] Se agregó vencimiento del registro al documento generado.*/
				fila.replaceField("Bib",	vMarcaCambioSit.Dat.VencimientoFecha.AsString );
				fila.replaceField("Pub",	pub );
				fila.replaceField("HI", vMarcaCambioSit.Dat.OrdenTrabajo.AsString);
				fila.replaceField("Usr", vMarcaCambioSit.Dat.UsuarioNombreCorto.AsString);

                fila.replaceField("Nuestra", vMarcaCambioSit.Dat.Nuestra.AsBoolean ? "X" : "--");
                fila.replaceField("Vigilada", vMarcaCambioSit.Dat.Vigilada.AsBoolean ? "X" : "--");
                fila.replaceField("StandBy", vMarcaCambioSit.Dat.expeStandBy.AsBoolean ? "X" : "--");
                fila.replaceField("Sustituida", vMarcaCambioSit.Dat.marSustituida.AsBoolean ? "X" : "--");
				

				fila.addBufferToText();		
			} // end for
			
			tabla.replaceLabel("filaTit", filaTit.Texto );
			tabla.replaceLabel("fila", fila.Texto );
			tabla.addBufferToText();

			cg.replace("#TotReg#","Total Registros: "+contador.ToString("###,###,##0") );
			cg.replaceLabel( "tabla", tabla.Texto );
			cg.replaceLabel( "encabezado", encabezado.Texto );
			cg.addBufferToText();
			
	
			#region Activar WORD
			string carpeta = @"K:\Cache\Reportes\";
			//carpeta = @"\\Trinity\Siberk\Dev\BERKE.MARCA\Code\Berke.Marcas\Berke.Marcas.WebUI\Reports\";
			carpeta = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;

			string archivo = @"repCambioSit";
			string ext		= ".doc";
			int version = 0;
			string path = carpeta+ archivo + "_v"+version.ToString()+ext;
			while( System.IO.File.Exists( path ))
			{
				version++;
				 path = carpeta+ archivo + "_v"+version.ToString()+ext;
			}
			Berke.Libs.Base.Helpers.Files.SaveStringToFile(cg.Texto, path);
			lnkDocum.NavigateUrl = path;
			lnkDocum.Text = "Ver Documento";
			#endregion Activar WORD

		}
		#endregion GenerarDocumento

		#region Cargar ddlTramiteSit
		private void CargarddlTramiteSitID()
		{
			Berke.DG.ViewTab.ListTab situaciones = UIPModel.Situacion.ReadForSelect();
			ddlTramiteSitID.Fill(situaciones.Table, true);
		}
		#endregion Cargar ddlTramiteSit
		
	} // end class ExpeMarcaCambioSitConsulta
} // end namespace Berke.Marcas.WebUI.Home


