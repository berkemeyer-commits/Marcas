using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Marcas.WebUI.Helpers;
using System.Security.Principal;


using System.Web.UI.WebControls.WebParts;


namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
	//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class ExpeMarcaConsultar : System.Web.UI.Page
	{
		#region Controles del Web Form
        protected System.Web.UI.WebControls.Label lblSustitida;
        #endregion 
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{

			//			this.cbxPropietarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxPropietarioID_LoadRequested); 
			this.cbxAgenteLocalID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxAgenteLocalID_LoadRequested); 
			//this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 

			this.ddlTramiteID.SelectedIndexChanged += new System.EventHandler(this.ddlTramiteID_SelectedIndexChanged_1);

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
	
			#region Tramite DropDown
			
			//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
			//			ddlTramiteID.Fill( se.Tables[0], true);	

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);
            
			if (pertenciaParam == "Publicaciones")
			{
				int i = 0;
				int cantEl = ddlTramiteID.Items.Count;
				for ( i = 1;  i < cantEl ; i++)
				{
					string nombre = ddlTramiteID.Items[i].Text;
					int idTramite = Convert.ToInt32(ddlTramiteID.Items[i].Value);
					if ((idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)
						&& (idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
						&& (idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA)
						&& (idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA))
					{
						ddlTramiteID.Items.Remove(ddlTramiteID.Items[i]);
						i--;
						cantEl--;
					}
				}
	
			}

			if (pertenciaParam == "Titulos")
			{
				int i = 0;
				int cantEl = ddlTramiteID.Items.Count;
				for ( i = 1;  i < cantEl ; i++)
				{
					string nombre = ddlTramiteID.Items[i].Text;
					int idTramite = Convert.ToInt32(ddlTramiteID.Items[i].Value);
					if ((idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)
						&& (idTramite != (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION))
					{
						ddlTramiteID.Items.Remove(ddlTramiteID.Items[i]);
						i--;
						cantEl--;
					}
				}
	
			}
		}


		#endregion Tramite DropDown


		
		#endregion Asignar Valores Iniciales

		#region variable global
		string pertenciaParam="";
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			pertenciaParam = UrlParam.GetParam("pertenciaParam");

            #region Javascript abrir pagina de reporte
            // Define the name and type of the client script on the page.
            string csName = "ReportButtonClickScript";
            Type csType = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(csType, csName))
            {
                StringBuilder csText = new StringBuilder();
                csText.Append("<script language='javascript'>function openReportPage(){");
                csText.Append("window.open('ReportLogos.aspx', '_blank');}");
                //csText.Append("return false;}");
                csText.Append("</script>");
                cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
            }
            #endregion Javascript abrir pagina de reporte

			if( !IsPostBack )
			{
				#region procesar Parametros
				//string pertenciaParam = UrlParam.GetParam("pertenciaParam");
				
				lblMarcaNuestra.Visible		= false;
				//ddlMarcaNuestra.Visible		= false;
				rbMarcaNuestra.Visible      = false; // mbaez
				lblMarcaVigilada.Visible	= false;
				//ddlMarcaVigilada.Visible	= false;
				rbMarcaVigilada.Visible     = false;
				//ddlMarcaActiva.Visible		= false;
				rbMarcaActiva.Visible       = false;
				lblMarcaActiva.Visible		= false;

				this.lblStandBy.Visible		= false;
				//this.ddlStandBy.Visible		= false;
				this.rbStandBy.Visible		= false;
				this.lblSustituida.Visible	= false;
				//this.ddlSustituida.Visible	= false;
				this.rbSustituida.Visible   = false;

				this.chkTitulosRetiradoCorregido.Enabled = false;

				lblOcultarPub.Text = "<script language='JavaScript'>closeDiv('divPublicacion')</script>";
				lblOcultarBol.Text = "<script language='JavaScript'>closeDiv('divBoletinDet')</script>";
				//lblTituloSituacion.Text = "<script language='JavaScript'>closeDiv('divTituloSituacion')</script>";

				if ( pertenciaParam == ""  )
				{
			
					lblTitulo.Text = "Consulta de Marcas";
					lblTituloAclaracion.Text = "Consulta General de Marcas";

				} 
				else if ( pertenciaParam == "HDescriptiva"  )
				{
			
					lblTitulo.Text = "Consulta de Hojas Descriptivas ";
					lblTituloAclaracion.Text = " De Marcas ";
					lblOcultarBol.Text = "";
					/*[ggaleano 08/10/2007] Ocultamos la columan Expediente ID. (Ref Bug#423)*/
					dgResult.Columns[0].Visible = false;
					

				}
					/*[ggaleano 13/08/2007] Se agrega consulta de títulos y publicaciones Bug#367*/
				else if ( pertenciaParam == "Publicaciones" )
				{
					lblTitulo.Text = "Consulta de Publicaciones ";
					lblTituloAclaracion.Text = " De Marcas ";
					dgResult.Columns[11].HeaderText = "Publicación";
					Label1.Text = "Fec. Sit.";
					lblTramiteSitID.Visible = false;
					ddlTramiteSitID.Visible = false;
					lblOcultarPub.Text = "";				

					Berke.DG.ViewTab.ListTab ListaDiarios = new Berke.DG.ViewTab.ListTab();
					ListaDiarios = this.CargarDDLDiarioID();

					ddlDiarioID.Fill(ListaDiarios.Table, true);

				}
				else if ( pertenciaParam == "Titulos" )
				{
					lblTitulo.Text = "Consulta de Titulos ";
					lblTituloAclaracion.Text = " De Marcas ";
					dgResult.Columns[11].HeaderText = "Título";
					lblTramiteSitID.Visible = false;
					ddlTramiteSitID.Visible = false;
				}

				if ( pertenciaParam == ""  ||  pertenciaParam == "HDescriptiva" 
					|| pertenciaParam == "Publicaciones" || pertenciaParam == "Titulos" )
				{
					lblMarcaNuestra.Visible		= true;
					//ddlMarcaNuestra.Visible		= true;
					rbMarcaNuestra.Visible      = true; // mbaez
					lblMarcaVigilada.Visible	= true;
					//ddlMarcaVigilada.Visible	= true;
					rbMarcaVigilada.Visible     = true;
					//ddlMarcaActiva.Visible		= true;
					rbMarcaActiva.Visible		= true;
					lblMarcaActiva.Visible		= true;
					this.lblStandBy.Visible		= true;
					//this.ddlStandBy.Visible		= true;
					this.rbStandBy.Visible      = true;
					this.lblSustituida.Visible	= true;
					//this.ddlSustituida.Visible	= true;
					this.rbSustituida.Visible   = true;

					if (pertenciaParam == "Titulos")
					{
						//lblTituloSituacion.Text = "<script language='JavaScript'>closeDiv('divTituloSituacion')</script>";
						pnlTituloSituacion.Visible = true;
						chkTitulosRetiradoCorregido.Enabled = true;
					}
				}
				else if ( pertenciaParam == "Propias" )
				{
					//lblTitulo.Text = "Consulta de Marcas Propias ";
					lblTitulo.Text = "Consulta de Marcas";
					lblTituloAclaracion.Text = "Consulta de Marcas Propias";

				
					//ddlMarcaNuestra.SelectedIndex	= 0; // Null
					rbMarcaNuestra.SelectedIndex    = 0; // Omitir. mbaez.
					//ddlMarcaVigilada.SelectedIndex	= 1; // Si
					rbMarcaVigilada.SelectedIndex	= 1; // Si

					/*[BUG#42]
					 * Se comenta el indicador 
					 */

					//	ddlMarcaActiva.SelectedIndex	= 1; // Si

					//ddlStandBy.SelectedIndex	= 0; // Null
					//ddlSustituida.SelectedIndex	= 0; // Null

					rbStandBy.SelectedIndex	= 0; // Null
					rbSustituida.SelectedIndex	= 0; // Null
				} 
				else if ( pertenciaParam == "Terceros" )
				{
					//lblTitulo.Text = "Consulta de Marcas de Terceros";
					lblTitulo.Text = "Consulta de Marcas";
					lblTituloAclaracion.Text = "Consulta de Marcas de Terceros";

					//ddlMarcaNuestra.SelectedIndex	= 2; // No
					rbMarcaNuestra.SelectedIndex    = 2; // No 
					//ddlMarcaVigilada.SelectedIndex	= 0; // Null
					rbMarcaVigilada.SelectedIndex	= 0; // Null

					/*[BUG#42]
					 * Se comenta el indicador 
					 */
					//ddlMarcaActiva.SelectedIndex	= 1; // Si

					//ddlStandBy.SelectedIndex	= 0; // Null
					//ddlSustituida.SelectedIndex	= 0; // Null

					rbStandBy.SelectedIndex	= 0; // Null
					rbSustituida.SelectedIndex	= 0; // Null
				}
				else if ( pertenciaParam == "Historico" )
				{
					//lblTitulo.Text = "Consulta al Histórico de Marcas";
					//lblTituloAclaracion.Text = " ( Reg. No Vigente )";
					lblTitulo.Text = "Consulta de Marcas";
					lblTituloAclaracion.Text = "Consulta al Histórico de Marcas";
					//ddlMarcaNuestra.SelectedIndex	= 0; // Null
					rbMarcaNuestra.SelectedIndex    = 0; // Omitir. mbaez
					//ddlMarcaVigilada.SelectedIndex	= 0; // Null
					rbMarcaVigilada.SelectedIndex	= 0; // Null
					//ddlVigente.SelectedIndex		= 2; // No
					rbVigente.SelectedIndex		    = 2; // No

					/*[BUG#42]
					 * Se comenta el indicador 
					 */
					//ddlMarcaActiva.SelectedIndex	= 1; // Si


					//ddlStandBy.SelectedIndex	= 0; // Null
					//ddlSustituida.SelectedIndex	= 0; // Null

					rbStandBy.SelectedIndex	= 0; // Null
					rbSustituida.SelectedIndex	= 0; // Null
				}
				else if ( pertenciaParam == "StandBy" )
				{
					//lblTitulo.Text = "Consulta de Marcas en StandBy";
					//lblTituloAclaracion.Text = " ( Parado por )";
					lblTitulo.Text = "Consulta de Marcas";
					lblTituloAclaracion.Text = "Consulta de Marcas en StandBy";

					//ddlMarcaNuestra.SelectedIndex	= 0; // Null
					rbMarcaNuestra.SelectedIndex    = 0; // Omitir. mbaez
					//ddlMarcaVigilada.SelectedIndex	= 0; // Null
					rbMarcaVigilada.SelectedIndex	= 0; // Null

					/*[BUG#42]
					 * Se comenta el indicador 
					 */
					//ddlMarcaActiva.SelectedIndex	= 1; // Si

					//ddlStandBy.SelectedIndex	= 1; // Si
					//ddlSustituida.SelectedIndex	= 0; // Null

					rbStandBy.SelectedIndex	= 1; // Si
					rbSustituida.SelectedIndex	= 0; // Null
				}if ( pertenciaParam == "Sustituida" )
				 {
					 //lblTitulo.Text = "Consulta de Marcas Sustituidas";
					 //lblTituloAclaracion.Text = " ( Sustituidas )";
					 lblTitulo.Text = "Consulta de Marcas";
					 lblTituloAclaracion.Text = "Consulta de Marcas Sustituidas";

					 //ddlMarcaNuestra.SelectedIndex	= 0; // Null
					 rbMarcaNuestra.SelectedIndex   = 0; // Omitir
					 //ddlMarcaVigilada.SelectedIndex	= 0; // Null
					 rbMarcaVigilada.SelectedIndex	= 0; // Null

					 /*[BUG#42]
					 * Se comenta el indicador 
					 */
					 //ddlMarcaActiva.SelectedIndex	= 1; // Si

					 //ddlStandBy.SelectedIndex	= 0; // Null
					 //ddlSustituida.SelectedIndex	= 1; // Si

					 rbStandBy.SelectedIndex	= 0; // Null
					 rbSustituida.SelectedIndex	= 1; // Si
				 }
				#endregion
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

		#region Busqueda de registros 

		private void Buscar()
		{
		
			#region Asignar Parametros ( vExpeMarca )
            if ((pertenciaParam == "Publicaciones") || (pertenciaParam == "Titulos"))
			{
				this.BuscarPub();
			}
			else if (pertenciaParam == "HDescriptiva")
			{
				this.BuscarHD();
			}
			else
			{
                this.btnReporteLogos.Visible = this.chkMostrarLogos.Checked;
				this.BuscarMarca();
			}
			#endregion Asignar Parametros ( vExpeMarca )
		}

		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
			Buscar();			
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Buscar();		
		}

		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Expedientes de Marcas";
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


		#region Propietario
		private void cbxPropietarioID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
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
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Propietario.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Propietario		

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

		private void ddlTramiteID_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			if ( ddlTramiteID.SelectedIndex < 1 )
			{
				ddlTramiteSitID.Items.Clear();
			}
			else
			{
				SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramiteID.SelectedValue ) );
				ddlTramiteSitID.Fill( situacion.Tables[0], true );
				
			}
		}

	
		private object GetFilter_Str( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			if( cadena.IndexOf("*") != -1 )
			{
				cadena = cadena.Replace("*","%");
			}
			else
			{
				cadena+= "%";
			}
			return cadena;
		}

		private object GetFilter_Bool( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			bool ret = false;
			switch( cadena.ToUpper() )
			{
				case "SI":
				case "TRUE":
				case "1":
					ret = true;
					break;
				case "NO":
				case "False":
				case "0":
					ret = false;
					break;
			}
			return ret;
		}

		private object GetFilter( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo

			#region Rango
			if( cadena.IndexOf("-") != -1 )
			{
				string [] aVal = cadena.Split( ((String)"-").ToCharArray() );
				string min = aVal[0].Trim();
				string max = aVal[1].Trim();
				if( min != "" && max != "")
				{
					return new DSFilter( (object)min, (object)max );
				}
			}
			#endregion Rango

			#region Lista
			if( cadena.IndexOf(",") != -1 )
			{
				return new DSFilter( new ArrayList(cadena.Split( ((String)",").ToCharArray()) ));
			}
			#endregion Lista

			return new DSFilter( cadena );
		
		}

		private void BuscarMarca()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vExpeMarca vExpeMarca = new Berke.DG.ViewTab.vExpeMarca(db);

			vExpeMarca.Dat.AltaFecha		.Filter = ObjConvert.GetFilter_Date( txtAltaFecha.Text );
			//vExpeMarca.Dat.Vigente			.Filter = GetFilter_Bool( ddlVigente.SelectedValue);
			vExpeMarca.Dat.Vigente			.Filter = GetFilter_Bool( rbVigente.SelectedValue);
			//vExpeMarca.Dat.MarcaNuestra		.Filter = GetFilter_Bool( ddlMarcaNuestra.SelectedValue);
			vExpeMarca.Dat.MarcaNuestra		.Filter = GetFilter_Bool( rbMarcaNuestra.SelectedValue);
			vExpeMarca.Dat.VencimientoFecha	.Filter = GetFilter( txtVencimientoFecha_min.Text );
			//vExpeMarca.Dat.MarcaActiva		.Filter = GetFilter_Bool( ddlMarcaActiva.SelectedValue);
			vExpeMarca.Dat.MarcaActiva		.Filter = GetFilter_Bool( rbMarcaActiva.SelectedValue);
	
			vExpeMarca.Dat.PropietarioID	.Filter = GetFilter( txtPropietarioID.Text.Trim()) ;
			vExpeMarca.Dat.PropietarioNombre.Filter = GetFilter_Str( this.txtPropietarioNombre.Text.Trim()) ;
			vExpeMarca.Dat.PropietarioPais	.Filter	= GetFilter( this.txtPropietarioPais.Text.Trim() );
					
			/*[12-04-2007. BUG#14]
			 * Permitimos realizar la busqueda por codigo y nombre de uno o mas clientes
			*/

			/*
			if( txtClienteID.Text.Trim() != "" ){
				vExpeMarca.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
			}else{
				vExpeMarca.Dat.ClienteID		.Filter = GetFilter( cbxClienteID.SelectedValue );
			}
			*/

			vExpeMarca.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
			vExpeMarca.Dat.NombreCliente    .Filter = GetFilter_Str( this.txtNombreCli.Text.Trim() );


			vExpeMarca.Dat.AgenteLocalID	.Filter = GetFilter( cbxAgenteLocalID.SelectedValue );
			vExpeMarca.Dat.OtNro			.Filter = GetFilter( txtOtNro_min.Text );
			vExpeMarca.Dat.OtAnio			.Filter = GetFilter( txtOtAnio.Text );
			vExpeMarca.Dat.ClaseNro			.Filter = GetFilter( txtClaseNro.Text	);
			if( ddlTipoReg.SelectedValue == "REG" )
			{
				vExpeMarca.Dat.RegistroNro	.Filter = GetFilter( txtRegistroNro_min.Text );
				vExpeMarca.Dat.RegistroAnio	.Filter = GetFilter( txtRegistroAnio.Text);		
			}
			else
			{
				vExpeMarca.Dat.RegVigenteNro	.Filter = GetFilter( txtRegistroNro_min.Text );
				vExpeMarca.Dat.RegVigenteAnio	.Filter = GetFilter( txtRegistroAnio.Text);
			}
			vExpeMarca.Dat.ActaNro			.Filter = GetFilter( txtActaNro_min.Text );
			vExpeMarca.Dat.ActaAnio			.Filter = GetFilter( txtActaAnio.Text );
			vExpeMarca.Dat.TramiteSitID		.Filter = GetFilter( ddlTramiteSitID.SelectedValue );
			vExpeMarca.Dat.TramiteID		.Filter = GetFilter( ddlTramiteID.SelectedValue );
			vExpeMarca.Dat.ExpedienteID		.Filter = GetFilter( txtExpedienteID_min.Text );
			vExpeMarca.Dat.Denominacion		.Filter = GetFilter_Str( txtDenomEmpieza.Text );
			vExpeMarca.Dat.MarcaID			.Filter = GetFilter( txtMarcaID_min.Text );
			//vExpeMarca.Dat.Vigilada			.Filter = GetFilter_Bool(ddlMarcaVigilada.SelectedValue);
			//vExpeMarca.Dat.StandBy			.Filter = GetFilter_Bool(ddlStandBy.SelectedValue);
			//vExpeMarca.Dat.Sustituida		.Filter = GetFilter_Bool(ddlSustituida.SelectedValue);
			vExpeMarca.Dat.Vigilada			.Filter = GetFilter_Bool(rbMarcaVigilada.SelectedValue);
			vExpeMarca.Dat.StandBy			.Filter = GetFilter_Bool(rbStandBy.SelectedValue);
			vExpeMarca.Dat.Sustituida		.Filter = GetFilter_Bool(rbSustituida.SelectedValue);
			vExpeMarca.Dat.EnTramite		.Filter = GetFilter_Bool(rbEnTramite.SelectedValue);
			/*[ggaleano 08/10/2007] Ref. Bug#423*/

			/*vExpeMarca.Dat.bolnro.Filter = GetFilter(txtBoletinNro.Text);
			vExpeMarca.Dat.bolanio.Filter = GetFilter(txtBoletinAnio.Text);*/

			string tipo = "";
			string comma = "";
			for(int k=0; k<chkTipo.Items.Count;k++)
			{
				if(chkTipo.Items[k].Selected)
				{
					tipo += comma+chkTipo.Items[k].Value;
					comma = ",";
				}
			}

            for (int k = 0; k < chkTipo1.Items.Count; k++)
            {
                if (chkTipo1.Items[k].Selected)
                {
                    tipo += comma + chkTipo1.Items[k].Value;
                    comma = ",";
                }
            }

			vExpeMarca.Dat.MarcaTipo		.Filter = ObjConvert.GetFilter(tipo);

			/*if (pertenciaParam == "HDescriptiva")
			{
				BoundColumn str_public = new BoundColumn();
				str_public.DataField = "str_public";
				str_public.HeaderText = "Ult. Public.";
				str_public.DataFormatString = "{0:dd/MM/yy}";
				dgResult.Columns.AddAt(11, str_public);
			}*/

			


			#region Obtener datos
			int recuperados = -1;
			try
			{

				try 
				{
					//				vExpeMarca =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList( vExpeMarca );
					vExpeMarca.Dat.Denominacion.Order = 1;

					recuperados = vExpeMarca.Adapter.Count();
					if( recuperados < 2000 )
					{
						recuperados = -1;
						string bf = vExpeMarca.Adapter.ReadAll_CommandString();
						vExpeMarca.Adapter.ReadAll( 2000 );
					
						#region eliminar Duplicados ( si no se buscó por propietario ) 
						//					if( cbxPropietarioID.SelectedValue.Trim() == "" )
						//					{
						int antID = -19992221;
						for( vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip() )
						{
							if( vExpeMarca.Dat.ExpedienteID.AsInt == antID )
							{
								vExpeMarca.Delete();
							}
							else
							{
								antID = vExpeMarca.Dat.ExpedienteID.AsInt;
							}
						}// end for
						vExpeMarca.AcceptAllChanges();
						//					}

						#endregion eliminar Duplicados

						#region Agregar columna para ver atencion
						if (!vExpeMarca.Table.Columns.Contains("AtencionPorMarca"))
						{
							vExpeMarca.Table.Columns.Add(new DataColumn("AtencionPorMarca", typeof(String)));
						}
						#endregion Agregar columna para ver atencion

						#region Convertir a Link y asignar pais a propietario
						for( vExpeMarca.GoTop(); ! vExpeMarca.EOF ; vExpeMarca.Skip() )
						{
							vExpeMarca.Edit();

							string denom = vExpeMarca.Dat.Denominacion.AsString;
							if( denom.Trim() == "" )
							{
								denom = "*Sin Denominación*";
							}
							string propPais = vExpeMarca.Dat.PropietarioPais.AsString.Trim();
							if( propPais != "" )
							{
								vExpeMarca.Dat.PropietarioNombre.Value = vExpeMarca.Dat.PropietarioNombre.AsString + "("+propPais+")";
							}

	 					   
							vExpeMarca.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
								vExpeMarca.Dat.ExpedienteID.AsString, 
								denom,
								"MarcaDetalleL.aspx","ExpeID" );	
							

							int hj = 0;
							int anho = 0;
							int iddoc = 0;
							string docRef = "";
							if (pertenciaParam == "HDescriptiva" || pertenciaParam == "")
							{
								if ( vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
									|| vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION) ) 
								{
									hj= Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA);
									anho = vExpeMarca.Dat.ActaAnio.AsInt;
									iddoc = vExpeMarca.Dat.ActaNro.AsInt;
								}
							}
							else if (pertenciaParam == "Publicaciones" || pertenciaParam == "Titulos")
							{
								if ( vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
									|| vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
									|| vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA)
									|| vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA)) 
								{
									if (pertenciaParam == "Publicaciones")
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.PUBLICACION);
										anho = vExpeMarca.Dat.PublicAnio.AsInt;
										iddoc = vExpeMarca.Dat.PublicPag.AsInt;
									}
									else
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.TITULO);
										anho = vExpeMarca.Dat.RegistroAnio.AsInt;
										iddoc = vExpeMarca.Dat.RegistroNro.AsInt;
									}
								}	

							}

							docRef =  Berke.Libs.Base.DocPath.digitalDocPath(anho, iddoc, hj);
							/*docRef =  Berke.Marcas.BizActions.Lib.digitalDocPath(
								vExpeMarca.Dat.ActaAnio		.AsInt,
								vExpeMarca.Dat.ActaNro		.AsInt,
								hj // Hoja Descriptiva
								);*/

                            vExpeMarca.Dat.TramiteDescrip.Value = docRef;

							vExpeMarca.Table.Rows[vExpeMarca.RowIndex]["AtencionPorMarca"] = this.getNombreAtencionxMarBU(vExpeMarca.Dat.TipoAtencionxMarca.AsInt,
								vExpeMarca.Dat.IDTipoAtencionxMarca.AsInt, vExpeMarca.Dat.MarcaID.AsInt,
								db);

							vExpeMarca.PostEdit();
						}
						#endregion  Convertir a Link
					}
				}
				catch ( Berke.Excep.Biz.TooManyRowsException ex )
				{	
					recuperados = ex.Recuperados;
				}
				catch( Exception exep ) 
				{
					throw new Exception("Class: ExpeMarcaConsulta ", exep );
				}
				#endregion Obtener datos
			
				#region Asignar dataSource de grilla

				/*
				foreach( DataGridItem item in dgResult.Items )
				{
					((TextBox)item.FindControl("H.Descriptiva")).Visible = false;
				}
				*/

				/*if (pertenciaParam == "HDescriptiva")
				{
					for (vExpeMarca.GoTop(); !vExpeMarca.EOF; vExpeMarca.Skip())
					{
						Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
						TramSit.ClearFilter();
						TramSit.Dat.TramiteID.Filter = vExpeMarca.Dat.TramiteID.AsInt;
						TramSit.Dat.SituacionID.Filter = (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
						TramSit.Adapter.ReadAll();

						vExpeMarca.Edit();
						Berke.DG.DBTab.Expediente_Situacion ExpeSitu = new Berke.DG.DBTab.Expediente_Situacion(db);
						ExpeSitu.ClearFilter();
						ExpeSitu.ClearOrder();
						ExpeSitu.Dat.ExpedienteID.Filter = vExpeMarca.Dat.ExpedienteID.AsInt;
						ExpeSitu.Dat.TramiteSitID.Filter = TramSit.Dat.ID.AsInt;
						ExpeSitu.Dat.VencimientoFecha.Order = -1;
						ExpeSitu.Adapter.ReadAll();

						vExpeMarca.Dat.str_public.Value = ExpeSitu.Dat.VencimientoFecha.AsString;
						vExpeMarca.PostEdit();

					}						
				}*/

				BoundColumn AtencionPorMarca = new BoundColumn();
				AtencionPorMarca.DataField = "AtencionPorMarca";
				AtencionPorMarca.HeaderText = "At. Marca";
				dgResult.Columns.AddAt(10, AtencionPorMarca);

                dgResult.DataSource = vExpeMarca.Table;
                dgResult.DataBind();

                if (this.chkMostrarLogos.Checked)
                {
                    Session["MarcasLogosDS"] = vExpeMarca.Table;
                }
                #endregion

				#region Mostrar Panel segun cantidad de registros obtenidos
				if( recuperados != -1 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
				}
				else if( vExpeMarca.RowCount == 0 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text = "No se encontraron registros";
				}
				else 
				{
					lblTituloGrid.Text = "Expedientes de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vExpeMarca.RowCount+ "&nbsp;regs.)";
					MostrarPanel_Resultado();
				}
				#region Pasa a la pagina de detalle si hay un unico registro
				/* pero quedarse en la grilla si la consulta fue invocada 
				 * para visualizar la hoja descriptiva 
				 * */
				if( vExpeMarca.RowCount == 1  && pertenciaParam != "HDescriptiva"  )
				{
					Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
					vExpeMarca.GoTop();
					url.AddParam("ExpeID", vExpeMarca.Dat.ExpedienteID.AsString );
					url.redirect( "MarcaDetalleL.aspx" );
				}
				#endregion 

				#endregion
			}
			finally
			{
				db.CerrarConexion();
			}
		}
	

		private void BuscarPub()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vExpeMarcaPub vExpeMarcaPub = new Berke.DG.ViewTab.vExpeMarcaPub(db);

			/*[ggaleano 07/09/2007] Se reutiliza el text box txtAltaFecha, pero el valor en realidad
			 * corresponde a Fecha Situación*/
			vExpeMarcaPub.Dat.FechaSituacion.Filter   = ObjConvert.GetFilter_Date(txtAltaFecha.Text); 
			vExpeMarcaPub.Dat.Vigente.Filter		  = GetFilter_Bool( rbVigente.SelectedValue);
			vExpeMarcaPub.Dat.MarcaNuestra.Filter	  =	GetFilter_Bool( rbMarcaNuestra.SelectedValue);
			vExpeMarcaPub.Dat.VencimientoFecha.Filter = GetFilter( txtVencimientoFecha_min.Text );
			vExpeMarcaPub.Dat.MarcaActiva.Filter		  =	GetFilter_Bool( rbMarcaActiva.SelectedValue);
	
			vExpeMarcaPub.Dat.PropietarioID.Filter		  = GetFilter( txtPropietarioID.Text.Trim()) ;
			vExpeMarcaPub.Dat.PropietarioNombre.Filter	  = GetFilter_Str( this.txtPropietarioNombre.Text.Trim()) ;
			vExpeMarcaPub.Dat.PropietarioPais.Filter	  = GetFilter( this.txtPropietarioPais.Text.Trim() );
			vExpeMarcaPub.Dat.ClienteID.Filter			  = GetFilter( txtClienteID.Text.Trim() );
			vExpeMarcaPub.Dat.NombreCliente.Filter		  = GetFilter_Str( this.txtNombreCli.Text.Trim() );

			vExpeMarcaPub.Dat.AgenteLocalID.Filter		  = GetFilter( cbxAgenteLocalID.SelectedValue );
			vExpeMarcaPub.Dat.OtNro.Filter				  = GetFilter( txtOtNro_min.Text );
			vExpeMarcaPub.Dat.OtAnio.Filter				  = GetFilter( txtOtAnio.Text );
			vExpeMarcaPub.Dat.ClaseNro.Filter			  = GetFilter( txtClaseNro.Text	);

			if( ddlTipoReg.SelectedValue == "REG" )
			{
				vExpeMarcaPub.Dat.RegistroNro.Filter	  = GetFilter( txtRegistroNro_min.Text );
				vExpeMarcaPub.Dat.RegistroAnio.Filter	  = GetFilter( txtRegistroAnio.Text);		
			}
			else
			{
				vExpeMarcaPub.Dat.RegVigenteNro.Filter	  = GetFilter( txtRegistroNro_min.Text );
				vExpeMarcaPub.Dat.RegVigenteAnio.Filter   = GetFilter( txtRegistroAnio.Text);
			}

			vExpeMarcaPub.Dat.ActaNro			.Filter = GetFilter( txtActaNro_min.Text );
			vExpeMarcaPub.Dat.ActaAnio			.Filter = GetFilter( txtActaAnio.Text );
			
			vExpeMarcaPub.Dat.TramiteID		.Filter = GetFilter( ddlTramiteID.SelectedValue );
			vExpeMarcaPub.Dat.ExpedienteID		.Filter = GetFilter( txtExpedienteID_min.Text );
			vExpeMarcaPub.Dat.Denominacion		.Filter = GetFilter_Str( txtDenomEmpieza.Text );
			vExpeMarcaPub.Dat.MarcaID			.Filter = GetFilter( txtMarcaID_min.Text );
			vExpeMarcaPub.Dat.Vigilada			.Filter = GetFilter_Bool(rbMarcaVigilada.SelectedValue);
			vExpeMarcaPub.Dat.StandBy			.Filter = GetFilter_Bool(rbStandBy.SelectedValue);
			vExpeMarcaPub.Dat.Sustituida		.Filter = GetFilter_Bool(rbSustituida.SelectedValue);
			vExpeMarcaPub.Dat.EnTramite		.Filter = GetFilter_Bool(rbEnTramite.SelectedValue);

			/*[ggaleano 10/09/2007] Filtro específicos de publicación*/
			vExpeMarcaPub.Dat.DiarioPublic.Filter = GetFilter(ddlDiarioID.SelectedValue);
			vExpeMarcaPub.Dat.AnhoPublic.Filter   = GetFilter(txtAnhoPublic.Text);
			vExpeMarcaPub.Dat.PaginaPublic.Filter = GetFilter(txtPaginaPublic.Text);

			string tipo = "";
			string comma = "";
			for(int k=0; k<chkTipo.Items.Count;k++)
			{
				if(chkTipo.Items[k].Selected)
				{
					tipo += comma+chkTipo.Items[k].Value;
					comma = ",";
				}
			}

			vExpeMarcaPub.Dat.MarcaTipo		.Filter = ObjConvert.GetFilter(tipo);

			if (pertenciaParam == "Publicaciones")
			{
				vExpeMarcaPub.Dat.ExpSitTramiteSitID.Filter = (int) GlobalConst.Situaciones.PUBLICADA;  //GetFilter( ddlTramiteSitID.SelectedValue );
				
			}
			else if (pertenciaParam == "Titulos")
			{
				if (ddlTramiteID.SelectedValue == "")
				{
					vExpeMarcaPub.Dat.TramiteID.Filter = ObjConvert.GetFilter(Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + ',' + Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString());
					vExpeMarcaPub.Dat.ExpSitTramiteSitID.Filter = ObjConvert.GetFilter(this.GetListaSituaciones(0,db));
				}
				else
				{
					vExpeMarcaPub.Dat.TramiteID.Filter = ObjConvert.GetFilter(ddlTramiteID.SelectedValue);
					vExpeMarcaPub.Dat.ExpSitTramiteSitID.Filter = ObjConvert.GetFilter(this.GetListaSituaciones(Convert.ToInt32(ddlTramiteID.SelectedValue), db));
				}

				if (pertenciaParam == "Titulos")
				{
					if (chkTitulosRetiradoCorregido.Checked)
					{
						string situaciones = "";
						if (ddlTramiteID.SelectedValue == "")
						{
							situaciones = ((int)GlobalConst.SituacionesXTramite.REGISTRO_TITULO_RETIRADO).ToString() + "," + ((int)GlobalConst.SituacionesXTramite.REGISTRO_TITULO_CORREGIDO).ToString() + "," +
								((int)GlobalConst.SituacionesXTramite.RENOVACION_TITULO_RETIRADO).ToString() + "," + ((int)GlobalConst.SituacionesXTramite.RENOVACION_TITULO_CORREGIDO).ToString();
						}
						else
						{
							if (ddlTramiteID.SelectedValue == ((int)GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString())
							{
								situaciones = ((int)GlobalConst.SituacionesXTramite.REGISTRO_TITULO_RETIRADO).ToString() + "," + ((int)GlobalConst.SituacionesXTramite.REGISTRO_TITULO_CORREGIDO).ToString();
							}
							else if (ddlTramiteID.SelectedValue == ((int)GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString())
							{
								situaciones = ((int)GlobalConst.SituacionesXTramite.RENOVACION_TITULO_RETIRADO).ToString() + "," + ((int)GlobalConst.SituacionesXTramite.RENOVACION_TITULO_CORREGIDO).ToString();
							}
						}

						/*
						 * REGISTRO_TITULO_RETIRADO		= 7,
			REGISTRO_TITULO_CORREGIDO		= 151,
			RENOVACION_TITULO_RETIRADO		= 35,
			RENOVACION_TITULO_CORREGIDO		= 152
						 * */
						//string situaciones = ((int)GlobalConst.Situaciones.TITULO_RETIRADO).ToString() + "," + ((int)GlobalConst.Situaciones.TITULO_CORREGIDO_RETIRADO).ToString();

						vExpeMarcaPub.Dat.TramiteSitID.Filter = GetFilter(situaciones);
					}
				}
			}
			

			#region Obtener datos
			int recuperados = -1;
			try
			{

				try 
				{
					vExpeMarcaPub.Dat.Denominacion.Order = 1;
					recuperados = vExpeMarcaPub.Adapter.Count();

					if( recuperados < 2000 )
					{
						recuperados = -1;
						string bf = vExpeMarcaPub.Adapter.ReadAll_CommandString();
						vExpeMarcaPub.Adapter.ReadAll( 2000 );
					
						#region eliminar Duplicados ( si no se buscó por propietario ) 
						
						int antID = -19992221;
						for( vExpeMarcaPub.GoTop(); ! vExpeMarcaPub.EOF; vExpeMarcaPub.Skip() )
						{
							if( vExpeMarcaPub.Dat.ExpedienteID.AsInt == antID )
							{
								vExpeMarcaPub.Delete();
							}
							else
							{
								antID = vExpeMarcaPub.Dat.ExpedienteID.AsInt;
							}
						}// end for
						vExpeMarcaPub.AcceptAllChanges();
						//					}

						#endregion eliminar Duplicados

						#region Convertir a Link y asignar pais a propietario
						for( vExpeMarcaPub.GoTop(); ! vExpeMarcaPub.EOF ; vExpeMarcaPub.Skip() )
						{
							vExpeMarcaPub.Edit();

							string denom = vExpeMarcaPub.Dat.Denominacion.AsString;
							if( denom.Trim() == "" )
							{
								denom = "*Sin Denominación*";
							}
							string propPais = vExpeMarcaPub.Dat.PropietarioPais.AsString.Trim();
							if( propPais != "" )
							{
								vExpeMarcaPub.Dat.PropietarioNombre.Value = vExpeMarcaPub.Dat.PropietarioNombre.AsString + "("+propPais+")";
							}

	 					   
							vExpeMarcaPub.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
								vExpeMarcaPub.Dat.ExpedienteID.AsString, 
								denom,
								"MarcaDetalleL.aspx","ExpeID" );	
							

							int hj = 0;
							int anho = 0;
							int iddoc = 0;
							string docRef = "";
							if (pertenciaParam == "HDescriptiva" || pertenciaParam == "")
							{
								if ( vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
									|| vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION) ) 
								{
									hj= Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA);
									anho = vExpeMarcaPub.Dat.ActaAnio.AsInt;
									iddoc = vExpeMarcaPub.Dat.ActaNro.AsInt;
								}
							}
							else if (pertenciaParam == "Publicaciones" || pertenciaParam == "Titulos")
							{
								if ( vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
									|| vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
									|| vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA)
									|| vExpeMarcaPub.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA)) 
								{
									if (pertenciaParam == "Publicaciones")
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.PUBLICACION);
										anho = vExpeMarcaPub.Dat.PublicAnio.AsInt;
										iddoc = vExpeMarcaPub.Dat.PublicPag.AsInt;
									}
									else
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.TITULO);
										anho = vExpeMarcaPub.Dat.RegistroAnio.AsInt;
										iddoc = vExpeMarcaPub.Dat.RegistroNro.AsInt;
									}
								}	

							}

							docRef =  Berke.Libs.Base.DocPath.digitalDocPath(anho, iddoc, hj);
							/*docRef =  Berke.Marcas.BizActions.Lib.digitalDocPath(
								vExpeMarca.Dat.ActaAnio		.AsInt,
								vExpeMarca.Dat.ActaNro		.AsInt,
								hj // Hoja Descriptiva
								);*/

							vExpeMarcaPub.Dat.TramiteDescrip.Value = docRef;


							vExpeMarcaPub.PostEdit();
						}
						#endregion  Convertir a Link
					}
				}
				catch ( Berke.Excep.Biz.TooManyRowsException ex )
				{	
					recuperados = ex.Recuperados;
				}
				catch( Exception exep ) 
				{
					throw new Exception("Class: ExpeMarcaConsulta ", exep );
				}
				#endregion Obtener datos
			
				#region Asignar dataSuorce de grilla

				/*
				foreach( DataGridItem item in dgResult.Items )
				{
					((TextBox)item.FindControl("H.Descriptiva")).Visible = false;
				}
				*/

				dgResult.Columns.RemoveAt(7);
				BoundColumn FechaVencimientoSit = new BoundColumn();
				FechaVencimientoSit.DataField = "FechaVencimiento";
				FechaVencimientoSit.HeaderText = "Venc. Public.";
				FechaVencimientoSit.DataFormatString = "{0:dd/MM/yy}";
				dgResult.Columns.AddAt(7, FechaVencimientoSit);
				
				dgResult.DataSource = vExpeMarcaPub.Table;
                dgResult.DataBind();
				#endregion

				#region Mostrar Panel segun cantidad de registros obtenidos
				if( recuperados != -1 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
				}
				else if( vExpeMarcaPub.RowCount == 0 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text = "No se encontraron registros";
				}
				else 
				{
					lblTituloGrid.Text = "Expedientes de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vExpeMarcaPub.RowCount+ "&nbsp;regs.)";
					MostrarPanel_Resultado();
				}
				#region Pasa a la pagina de detalle si hay un unico registro
				/* pero quedarse en la grilla si la consulta fue invocada 
				 * para visualizar la hoja descriptiva 
				 * 
				if( vExpeMarcaPub.RowCount == 1  && pertenciaParam != "HDescriptiva"  )
				{
					Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
					vExpeMarcaPub.GoTop();
					url.AddParam("ExpeID", vExpeMarcaPub.Dat.ExpedienteID.AsString );
					url.redirect( "MarcaDetalleL.aspx" );
				}*/
				#endregion 

				#endregion
			}
			finally
			{
				db.CerrarConexion();
			}
		}

		private Berke.DG.ViewTab.ListTab CargarDDLDiarioID()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Diario Diario = new Berke.DG.DBTab.Diario(db);
			Berke.DG.ViewTab.ListTab LDiario = new Berke.DG.ViewTab.ListTab();
			Diario.Adapter.ReadAll();

			for (Diario.GoTop(); !Diario.EOF; Diario.Skip())
			{
				LDiario.NewRow();
				LDiario.Dat.ID.Value = Diario.Dat.ID.AsInt;
				LDiario.Dat.Descrip.Value = Diario.Dat.Descrip.AsString;
				LDiario.PostNewRow();
			}		

			db.CerrarConexion();

			return LDiario;
		}

        protected void btnGenDoc_Click(object sender, System.EventArgs e)
		{
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.word";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            dgResult.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
		}

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

		protected void btnGenXls_Click(object sender, System.EventArgs e)
		{
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            dgResult.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
		}

		private string GetListaSituaciones(int tramiteid, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
			TramSit.ClearFilter();
			TramSit.Dat.SituacionID.Filter = (int)GlobalConst.Situaciones.CONCEDIDA;

			if (tramiteid > 0)
			{
				TramSit.Dat.TramiteID.Filter = tramiteid;
			}
			else
			{
				TramSit.Dat.TramiteID.Filter = ObjConvert.GetFilter(Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + ',' + Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString());
			}

			TramSit.Adapter.ReadAll();

			string lista = "";
			for (TramSit.GoTop(); !TramSit.EOF; TramSit.Skip())
			{
				if (lista != "")
				{
					lista += ",";
				}
				lista += TramSit.Dat.ID.AsString;
			}
			return lista;
		}

		private void BuscarHD()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vExpeMarcaHD vExpeMarcaHD = new Berke.DG.ViewTab.vExpeMarcaHD(db);

			vExpeMarcaHD.Dat.AltaFecha		.Filter = ObjConvert.GetFilter_Date( txtAltaFecha.Text );
			vExpeMarcaHD.Dat.Vigente			.Filter = GetFilter_Bool( rbVigente.SelectedValue);
			vExpeMarcaHD.Dat.MarcaNuestra		.Filter = GetFilter_Bool( rbMarcaNuestra.SelectedValue);
			vExpeMarcaHD.Dat.VencimientoFecha	.Filter = GetFilter( txtVencimientoFecha_min.Text );
			vExpeMarcaHD.Dat.MarcaActiva		.Filter = GetFilter_Bool( rbMarcaActiva.SelectedValue);
			vExpeMarcaHD.Dat.PropietarioID	.Filter = GetFilter( txtPropietarioID.Text.Trim()) ;
			vExpeMarcaHD.Dat.PropietarioNombre.Filter = GetFilter_Str( this.txtPropietarioNombre.Text.Trim()) ;
			vExpeMarcaHD.Dat.PropietarioPais	.Filter	= GetFilter( this.txtPropietarioPais.Text.Trim() );
			vExpeMarcaHD.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
			vExpeMarcaHD.Dat.NombreCliente    .Filter = GetFilter_Str( this.txtNombreCli.Text.Trim() );
			vExpeMarcaHD.Dat.AgenteLocalID	.Filter = GetFilter( cbxAgenteLocalID.SelectedValue );
			vExpeMarcaHD.Dat.OtNro			.Filter = GetFilter( txtOtNro_min.Text );
			vExpeMarcaHD.Dat.OtAnio			.Filter = GetFilter( txtOtAnio.Text );
			vExpeMarcaHD.Dat.ClaseNro			.Filter = GetFilter( txtClaseNro.Text	);

			if( ddlTipoReg.SelectedValue == "REG" )
			{
				vExpeMarcaHD.Dat.RegistroNro	.Filter = GetFilter( txtRegistroNro_min.Text );
				vExpeMarcaHD.Dat.RegistroAnio	.Filter = GetFilter( txtRegistroAnio.Text);		
			}
			else
			{
				vExpeMarcaHD.Dat.RegVigenteNro	.Filter = GetFilter( txtRegistroNro_min.Text );
				vExpeMarcaHD.Dat.RegVigenteAnio	.Filter = GetFilter( txtRegistroAnio.Text);
			}

			vExpeMarcaHD.Dat.ActaNro			.Filter = GetFilter( txtActaNro_min.Text );
			vExpeMarcaHD.Dat.ActaAnio			.Filter = GetFilter( txtActaAnio.Text );
			vExpeMarcaHD.Dat.TramiteSitID		.Filter = GetFilter( ddlTramiteSitID.SelectedValue );
			vExpeMarcaHD.Dat.TramiteID		.Filter = GetFilter( ddlTramiteID.SelectedValue );
			vExpeMarcaHD.Dat.ExpedienteID		.Filter = GetFilter( txtExpedienteID_min.Text );
			vExpeMarcaHD.Dat.Denominacion		.Filter = GetFilter_Str( txtDenomEmpieza.Text );
			vExpeMarcaHD.Dat.MarcaID			.Filter = GetFilter( txtMarcaID_min.Text );
			vExpeMarcaHD.Dat.Vigilada			.Filter = GetFilter_Bool(rbMarcaVigilada.SelectedValue);
			vExpeMarcaHD.Dat.StandBy			.Filter = GetFilter_Bool(rbStandBy.SelectedValue);
			vExpeMarcaHD.Dat.Sustituida		.Filter = GetFilter_Bool(rbSustituida.SelectedValue);
			vExpeMarcaHD.Dat.EnTramite		.Filter = GetFilter_Bool(rbEnTramite.SelectedValue);

			/*[ggaleano 08/10/2007] Ref. Bug#423*/
			vExpeMarcaHD.Dat.bolnro.Filter = GetFilter(txtBoletinNro.Text);
			vExpeMarcaHD.Dat.bolanio.Filter = GetFilter(txtBoletinAnio.Text);

			string tipo = "";
			string comma = "";
			for(int k=0; k<chkTipo.Items.Count;k++)
			{
				if(chkTipo.Items[k].Selected)
				{
					tipo += comma+chkTipo.Items[k].Value;
					comma = ",";
				}
			}

			vExpeMarcaHD.Dat.MarcaTipo		.Filter = ObjConvert.GetFilter(tipo);
			
			BoundColumn str_public = new BoundColumn();
			str_public.DataField = "str_public";
			str_public.HeaderText = "Ult. Public.";
			str_public.DataFormatString = "{0:dd/MM/yy}";
			dgResult.Columns.AddAt(11, str_public);

			for (vExpeMarcaHD.GoTop(); !vExpeMarcaHD.EOF; vExpeMarcaHD.Skip())
			{
				Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
				TramSit.ClearFilter();
				TramSit.Dat.TramiteID.Filter = vExpeMarcaHD.Dat.TramiteID.AsInt;
				TramSit.Dat.SituacionID.Filter = (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
				TramSit.Adapter.ReadAll();

				vExpeMarcaHD.Edit();
				Berke.DG.DBTab.Expediente_Situacion ExpeSitu = new Berke.DG.DBTab.Expediente_Situacion(db);
				ExpeSitu.ClearFilter();
				ExpeSitu.ClearOrder();
				ExpeSitu.Dat.ExpedienteID.Filter = vExpeMarcaHD.Dat.ExpedienteID.AsInt;
				ExpeSitu.Dat.TramiteSitID.Filter = TramSit.Dat.ID.AsInt;
				ExpeSitu.Dat.VencimientoFecha.Order = -1;
				ExpeSitu.Adapter.ReadAll();

				vExpeMarcaHD.Dat.str_public.Value = ExpeSitu.Dat.VencimientoFecha.AsString;
				vExpeMarcaHD.PostEdit();

			}						
			

			#region Obtener datos
			int recuperados = -1;
			try
			{

				try 
				{
					//				vExpeMarcaHD =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList( vExpeMarcaHD );
					vExpeMarcaHD.Dat.Denominacion.Order = 1;

					recuperados = vExpeMarcaHD.Adapter.Count();
					if( recuperados < 2000 )
					{
						recuperados = -1;
						string bf = vExpeMarcaHD.Adapter.ReadAll_CommandString();
						vExpeMarcaHD.Adapter.ReadAll( 2000 );
					
						#region eliminar Duplicados ( si no se buscó por propietario ) 
						//					if( cbxPropietarioID.SelectedValue.Trim() == "" )
						//					{
						int antID = -19992221;
						for( vExpeMarcaHD.GoTop(); ! vExpeMarcaHD.EOF; vExpeMarcaHD.Skip() )
						{
							if( vExpeMarcaHD.Dat.ExpedienteID.AsInt == antID )
							{
								vExpeMarcaHD.Delete();
							}
							else
							{
								antID = vExpeMarcaHD.Dat.ExpedienteID.AsInt;
							}
						}// end for
						vExpeMarcaHD.AcceptAllChanges();
						//					}

						#endregion eliminar Duplicados

						#region Convertir a Link y asignar pais a propietario
						for( vExpeMarcaHD.GoTop(); ! vExpeMarcaHD.EOF ; vExpeMarcaHD.Skip() )
						{
							vExpeMarcaHD.Edit();

							string denom = vExpeMarcaHD.Dat.Denominacion.AsString;
							if( denom.Trim() == "" )
							{
								denom = "*Sin Denominación*";
							}
							string propPais = vExpeMarcaHD.Dat.PropietarioPais.AsString.Trim();
							if( propPais != "" )
							{
								vExpeMarcaHD.Dat.PropietarioNombre.Value = vExpeMarcaHD.Dat.PropietarioNombre.AsString + "("+propPais+")";
							}

	 					   
							vExpeMarcaHD.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
								vExpeMarcaHD.Dat.ExpedienteID.AsString, 
								denom,
								"MarcaDetalleL.aspx","ExpeID" );	
							

							int hj = 0;
							int anho = 0;
							int iddoc = 0;
							string docRef = "";
							if (pertenciaParam == "HDescriptiva" || pertenciaParam == "")
							{
                                if (vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)
                                    || vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION))
                                {
                                    hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA);
                                    anho = vExpeMarcaHD.Dat.ActaAnio.AsInt;
                                    iddoc = vExpeMarcaHD.Dat.ActaNro.AsInt;
                                }
                                else
                                {
                                    hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA_TV);
                                    anho = vExpeMarcaHD.Dat.ActaAnio.AsInt;
                                    iddoc = vExpeMarcaHD.Dat.ActaNro.AsInt;
                                }
							}
							else if (pertenciaParam == "Publicaciones" || pertenciaParam == "Titulos")
							{
								if ( vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
									|| vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
									|| vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA)
									|| vExpeMarcaHD.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA)) 
								{
									if (pertenciaParam == "Publicaciones")
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.PUBLICACION);
										anho = vExpeMarcaHD.Dat.PublicAnio.AsInt;
										iddoc = vExpeMarcaHD.Dat.PublicPag.AsInt;
									}
									else
									{
										hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.TITULO);
										anho = vExpeMarcaHD.Dat.RegistroAnio.AsInt;
										iddoc = vExpeMarcaHD.Dat.RegistroNro.AsInt;
									}
								}	

							}

							docRef =  Berke.Libs.Base.DocPath.digitalDocPath(anho, iddoc, hj);
							/*docRef =  Berke.Marcas.BizActions.Lib.digitalDocPath(
								vExpeMarcaHD.Dat.ActaAnio		.AsInt,
								vExpeMarcaHD.Dat.ActaNro		.AsInt,
								hj // Hoja Descriptiva
								);*/

							vExpeMarcaHD.Dat.TramiteDescrip.Value = docRef;


							vExpeMarcaHD.PostEdit();
						}
						#endregion  Convertir a Link
					}
				}
				catch ( Berke.Excep.Biz.TooManyRowsException ex )
				{	
					recuperados = ex.Recuperados;
				}
				catch( Exception exep ) 
				{
					throw new Exception("Class: ExpeMarcaConsulta ", exep );
				}
				#endregion Obtener datos
			
				#region Asignar dataSuorce de grilla

				/*if (pertenciaParam == "HDescriptiva")
				{
					for (vExpeMarcaHD.GoTop(); !vExpeMarcaHD.EOF; vExpeMarcaHD.Skip())
					{
						Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
						TramSit.ClearFilter();
						TramSit.Dat.TramiteID.Filter = vExpeMarcaHD.Dat.TramiteID.AsInt;
						TramSit.Dat.SituacionID.Filter = (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
						TramSit.Adapter.ReadAll();

						vExpeMarcaHD.Edit();
						Berke.DG.DBTab.Expediente_Situacion ExpeSitu = new Berke.DG.DBTab.Expediente_Situacion(db);
						ExpeSitu.ClearFilter();
						ExpeSitu.ClearOrder();
						ExpeSitu.Dat.ExpedienteID.Filter = vExpeMarcaHD.Dat.ExpedienteID.AsInt;
						ExpeSitu.Dat.TramiteSitID.Filter = TramSit.Dat.ID.AsInt;
						ExpeSitu.Dat.VencimientoFecha.Order = -1;
						ExpeSitu.Adapter.ReadAll();

						vExpeMarcaHD.Dat.str_public.Value = ExpeSitu.Dat.VencimientoFecha.AsString;
						vExpeMarcaHD.PostEdit();

					}						
				}*/

				dgResult.DataSource = vExpeMarcaHD.Table;
                dgResult.DataBind();
				#endregion

				#region Mostrar Panel segun cantidad de registros obtenidos
				if( recuperados != -1 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
				}
				else if( vExpeMarcaHD.RowCount == 0 )
				{
					MostrarPanel_Busqueda();
					lblMensaje.Text = "No se encontraron registros";
				}
				else 
				{
					lblTituloGrid.Text = "Expedientes de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vExpeMarcaHD.RowCount+ "&nbsp;regs.)";
					MostrarPanel_Resultado();
				}
				#region Pasa a la pagina de detalle si hay un unico registro
				/* pero quedarse en la grilla si la consulta fue invocada 
				 * para visualizar la hoja descriptiva 
				 * */
				if( vExpeMarcaHD.RowCount == 1  && pertenciaParam != "HDescriptiva"  )
				{
					Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
					vExpeMarcaHD.GoTop();
					url.AddParam("ExpeID", vExpeMarcaHD.Dat.ExpedienteID.AsString );
					url.redirect( "MarcaDetalleL.aspx" );
				}
				#endregion 

				#endregion
			}
			finally
			{
				db.CerrarConexion();
			}
		}
	
		#region Obtener Atención por Marca o BU
		private string getNombreAtencionxMarBU(int TipoAtencionxMarca, int IDTipoAtencionxMarca, int MarcaID, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			int atencionID = 0;

			string result = "";
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion(db);
			
			if (TipoAtencionxMarca == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA)
			{
				atencionID = IDTipoAtencionxMarca;
				atencion.Adapter.ReadByID(atencionID);
				result = atencion.Dat.Nombre.AsString;
			}
			else if (TipoAtencionxMarca == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
			{
				Berke.DG.DBTab.BussinessUnit bussinessUnit = new Berke.DG.DBTab.BussinessUnit(db);
				bussinessUnit.Adapter.ReadByID(IDTipoAtencionxMarca);
				atencionID = bussinessUnit.Dat.AtencionID.AsInt;
				atencion.Adapter.ReadByID(atencionID);
				result = atencion.Dat.Nombre.AsString;
			}
			else if (TipoAtencionxMarca == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE)
			{
				result = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
						"AtencionesxMarca.aspx",
						GlobalConst.DESCRIP_ATENCIONXMARCAXTRAMITE,
						MarcaID.ToString(),
						"MarcaID");
			}
			return result;
		}
		#endregion Obtener Atención por Marca o BU


        protected void dgResult_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.Cells[e.Item.Cells.Count - 2].Text == "&nbsp;") || (!this.chkMostrarLogos.Checked))
            {
                e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;
            }
            else
            {
                e.Item.Cells[e.Item.Cells.Count - 1].Attributes["onload"] = "redimensionar(this)";
            }
        }

        protected string GetUrl(string imagepath)
        {
            string[] splits = Request.Url.AbsoluteUri.Split('/');

            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";

                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }
                return url + imagepath;
            }
            return imagepath;
        }
        protected void btnReporteLogos_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenReportPage", "openReportPage()", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "OpenReportPage", "openReportPage();", false);
        }
} // end class ExpeMarcaConsulta
} // end namespace Berke.Marcas.WebUI.Home


