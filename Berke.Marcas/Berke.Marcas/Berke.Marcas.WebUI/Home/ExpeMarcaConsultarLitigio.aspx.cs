using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class ExpeMarcaConsultarLitigio : System.Web.UI.Page
	{

		Berke.Libs.Base.Helpers.AccesoDB db;
		Berke.DG.ViewTab.vConsMarcaLitigios vConsMarcaLitigios ;
		string pertenciaParam = "";
        private bool pertenenciaParam = false;

        private const string REPORT_URL = "openReportPage('{0}')";
        private const string LIST = "list";
        private const string TABLE = "table";

        #region Controles del Web Form


        protected System.Web.UI.WebControls.Label lblExpedienteID_min;
		protected System.Web.UI.WebControls.TextBox txtExpedienteID_min;



		protected System.Web.UI.WebControls.Label lblSustitida;
		protected System.Web.UI.WebControls.TextBox txtAltaFecha;
		
		#endregion 
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{

			this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            //this.Button1.Click += new System.EventHandler(this.btBuscar_Click);
            //this.btGenDoc.Click += new System.EventHandler(this.btGenDoc_Click);
            this.btnGenReporte.Click += new System.EventHandler(this.btGenDoc_Click);
            this.cbxAgenteLocalID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxAgenteLocalID_LoadRequested); 

		}
		#endregion Asignar Delegados

		private void inicializar()
		{
            db = new Berke.Libs.Base.Helpers.AccesoDB();
            db.DataBaseName = WebUI.Helpers.MyApplication.CurrentDBName;
            db.ServerName = WebUI.Helpers.MyApplication.CurrentServerName;
            vConsMarcaLitigios = new Berke.DG.ViewTab.vConsMarcaLitigios(db);
            vConsMarcaLitigios.Adapter.Distinct = true;


        }		

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
	
			#region Tramite DropDown
			
//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
//			ddlTramiteID.Fill( se.Tables[0], true);	

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			//ddlTramiteID.Fill( lst.Table, true);


			#endregion Tramite DropDown

		



		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			inicializar();
			AsignarDelegados();
			pertenciaParam = UrlParam.GetParam("pertenciaParam");
			rbMarcaVigilada.Visible     = false;

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
                csText.Append("<script language='javascript'>function openReportPage(reptype){");
                csText.Append("window.open('ReportUltTramMarcas.aspx?reptype=' + reptype, '_blank');}");
                csText.Append("</script>");
                cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
            }
            #endregion Javascript abrir pagina de reporte

            if ( pertenciaParam == "M" ) 
			{
				lblTitulo.Text = "Consulta de Marcas";
				lblTituloAclaracion.Text = " Ultimo Tramite REG/REN (Marcas)";

				chkActiva.Visible = true;
				chkActiva.Checked = true;
				lblActiva.Visible = true;

                this.pertenenciaParam = true;
			}
			else 
			{
				lblTitulo.Text = "Consulta de Marcas";
				lblTituloAclaracion.Text = " Ultimo Tramite REG/REN (Litigios)";
				chkActiva.Visible = false;
				chkActiva.Checked = false;
				lblActiva.Visible = false;

                this.pertenenciaParam = false;

			}
			if( !IsPostBack )
			{
				#region procesar Parametros
		        pertenciaParam = UrlParam.GetParam("pertenciaParam");
				rbMarcaVigilada.Visible     = false;

				//lblMarcaVigilada.Visible	= true;
				//ddlMarcaVigilada.Visible	= true;
				rbMarcaVigilada.Visible		= true;
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
			//this.dgResult.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgResult_ItemDataBound);

		}
		#endregion

		private void FiltrarDatos()
		{
			#region Asignar Parametros ( vConsMarcaLitigios )

			//vConsMarcaLitigios = new Berke.DG.ViewTab.vConsMarcaLitigios(db);
			//vConsMarcaLitigios.Dat.AltaFecha		.Filter = ObjConvert.GetFilter_Date( txtAltaFecha.Text );
			//vConsMarcaLitigios.Dat.Vigente			.Filter = GetFilter_Bool( ddlVigente.SelectedValue);
			//vConsMarcaLitigios.Dat.MarcaNuestra		.Filter = GetFilter_Bool( ddlMarcaNuestra.SelectedValue);
			//vConsMarcaLitigios.Dat.VencimientoFecha	.Filter = GetFilter( txtVencimientoFecha_min.Text );
			//vConsMarcaLitigios.Dat.MarcaActiva		.Filter = GetFilter_Bool( ddlMarcaActiva.SelectedValue);
	
			//vConsMarcaLitigios.Dat.PropietarioID	.Filter = GetFilter( txtPropietarioID.Text.Trim()) ;

			

			string defaultWhere = vConsMarcaLitigios.Adapter.DefaultWhere;
			if (txtPropietarioID.Text.Trim() != "")
			{
				if (defaultWhere != "")
				{
					defaultWhere += " and ";
				}
				defaultWhere += " pm.propietarioid IN (" + txtPropietarioID.Text + ") ";
				vConsMarcaLitigios.Adapter.SetDefaultWhere(defaultWhere);
			}


			vConsMarcaLitigios.Dat.PropietarioNombre.Filter = GetFilter_Str( this.txtPropietarioNombre.Text.Trim()) ;
			vConsMarcaLitigios.Dat.PropietarioPais	.Filter	= GetFilter( this.txtPropietarioPais.Text.Trim() );

			if ( chkActiva.Checked) 
			{
				vConsMarcaLitigios.Dat.Activa           .Filter = true;
			}
					
			

			vConsMarcaLitigios.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
			vConsMarcaLitigios.Dat.ClienteNombre    .Filter = GetFilter_Str( this.txtNombreCli.Text.Trim() );

			
			vConsMarcaLitigios.Dat.AgenteLocalID	.Filter = GetFilter( cbxAgenteLocalID.SelectedValue );
			vConsMarcaLitigios.Dat.ClaseNro			.Filter = GetFilter( txtClaseNro.Text	);

			//if ( txtTipo.Text.ToString().Length > 0 )
			//	vConsMarcaLitigios.Dat.marcatipo		.Filter = GetFilter( txtTipo.Text	);
			string tipo = "";
			string comma = "";
			for(int k=0; k<chkTipo.Items.Count;k++){
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
			vConsMarcaLitigios.Dat.marcatipo			.Filter = ObjConvert.GetFilter(tipo);
			
			
			vConsMarcaLitigios.Dat.RegistroNro	.Filter = GetFilter( txtRegistroNro_min.Text );
			vConsMarcaLitigios.Dat.RegistroAnio.Filter = GetFilter( txtRegistroAnio.Text);		
			
			vConsMarcaLitigios.Dat.ActaNro			.Filter = GetFilter( txtActaNro_min.Text );
			vConsMarcaLitigios.Dat.ActaAnio			.Filter = GetFilter( txtActaAnio.Text );
	
			vConsMarcaLitigios.Dat.Denominacion		.Filter = GetFilter_Str( txtDenomEmpieza.Text );
			//vConsMarcaLitigios.Dat.Vigilada			.Filter = GetFilter_Bool(ddlMarcaVigilada.SelectedValue);
			vConsMarcaLitigios.Dat.Vigilada			.Filter = GetFilter_Bool(rbMarcaVigilada.SelectedValue);

			#region Registro Anterior
			if (chkBoxRegistroAnterior.Checked)
			{
				Berke.DG.DBTab.MarcaRegRen marRR = new Berke.DG.DBTab.MarcaRegRen(this.db);
				Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca(this.db);

				marRR.ClearFilter();
				marRR.Dat.RegistroNro.Filter = ObjConvert.GetFilter(txtRegistroNro_min.Text); //Convert.ToInt32( txtRegistroNro_min.Text);
				marRR.Adapter.ReadAll();
				
				string ExpedienteVigenteIDs = "";
				//if (marRR.RowCount > 0)
				for(marRR.GoTop(); !marRR.EOF; marRR.Skip())
				{
					mar.ClearFilter();
					mar.Dat.MarcaRegRenID.Filter = marRR.Dat.ID.AsInt;
					mar.Dat.Vigente.Filter = true;
					mar.Adapter.ReadAll();
					if (mar.RowCount == 1)
					{
						
						if (ExpedienteVigenteIDs != "")
						{
							ExpedienteVigenteIDs += ",";
						}
						ExpedienteVigenteIDs += mar.Dat.ExpedienteVigenteID.AsString;
						//vConsMarcaLitigios.Dat.ExpedienteID.Filter = mar.Dat.ExpedienteVigenteID.AsInt;
					}
				}
				if (ExpedienteVigenteIDs != "")
				{
					vConsMarcaLitigios.Dat.ExpedienteID.Filter = ObjConvert.GetFilter(ExpedienteVigenteIDs);
					vConsMarcaLitigios.Dat.RegistroNro.Filter = System.DBNull.Value;
					chkActiva.Checked = false;
					vConsMarcaLitigios.Dat.Activa.Filter = System.DBNull.Value;
				}
			}
			#endregion Registro Anterior

			#endregion Asignar Parametros ( vConsMarcaLitigios )
		}

		#region Busqueda de registros 

		private void Buscar(){

            /*
			db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			*/

            #region Filtrar Datos
			FiltrarDatos();
			#endregion

			#region Obtener datos
			int recuperados = -1;
			try 
			{

				vConsMarcaLitigios.ClearOrder();
				vConsMarcaLitigios.Dat.Denominacion.Order = 1;
				vConsMarcaLitigios.Dat.ClaseNro.Order = 2;
				vConsMarcaLitigios.Dat.PropietarioNombre.Order = 3;
				vConsMarcaLitigios.Dat.Acta.Order = 4;
				vConsMarcaLitigios.Dat.PresentacionFecha.Order = 5;
				vConsMarcaLitigios.Dat.Registro.Order = 6;
				vConsMarcaLitigios.Dat.ConcesionFecha.Order = 7;
				vConsMarcaLitigios.Dat.VencimientoFecha.Order = 8;

				recuperados = vConsMarcaLitigios.Adapter.Count();
				if( recuperados < 2000 )
				{
					recuperados = -1;
					string bf = vConsMarcaLitigios.Adapter.ReadAll_CommandString();
					vConsMarcaLitigios.Adapter.ReadAll( 2000 );

					/*
					#region eliminar Duplicados ( si no se buscó por propietario ) 
						int antID = -19992221;
						for( vConsMarcaLitigios.GoTop(); ! vConsMarcaLitigios.EOF; vConsMarcaLitigios.Skip() )
						{
							if( vConsMarcaLitigios.Dat.ExpedienteID.AsInt == antID )
							{
								vConsMarcaLitigios.Delete(); 
							}
							else
							{
								antID = vConsMarcaLitigios.Dat.ExpedienteID.AsInt;
							}
						}// end for
						vConsMarcaLitigios.AcceptAllChanges();

					#endregion eliminar Duplicados
					*/

					#region Agregar columna para ver atencion
					if (!vConsMarcaLitigios.Table.Columns.Contains("AtencionPorMarca"))
					{
						vConsMarcaLitigios.Table.Columns.Add(new DataColumn("AtencionPorMarca", typeof(String)));
					}
                    #endregion Agregar columna para ver atencion

                    #region Primer Registro
                    vConsMarcaLitigios.Table.Columns.Add(new DataColumn("PrimerRegistro", typeof(int)));
                    Berke.Libs.Boletin.Services.MarcaRegRenService mrrServ = new Libs.Boletin.Services.MarcaRegRenService(db);
                    Berke.DG.DBTab.MarcaRegRen mrr = new DG.DBTab.MarcaRegRen();
                    #endregion Primer Registro


                    #region Convertir a Link y asignar pais a propietario

                    Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );

					string npublic = "";
					for( vConsMarcaLitigios.GoTop(); ! vConsMarcaLitigios.EOF ; vConsMarcaLitigios.Skip() )
					{
						expeSit.ClearOrder();
						expeSit.Dat.ExpedienteID   .Filter	= vConsMarcaLitigios.Dat.ExpedienteID.AsInt;
						expeSit.Dat.TramiteSitID   .Filter  = 4;
						expeSit.Dat.SituacionFecha .Order	= -1;
						
						expeSit.Adapter.ReadAll();

						vConsMarcaLitigios.Edit();

                        #region Primer Registro
                        int rootExpeID = Berke.Marcas.BizActions.Lib.ExpedienteRootID(vConsMarcaLitigios.Dat.ExpedienteID.AsInt, db);

                        if (vConsMarcaLitigios.Dat.ExpedienteID.AsInt != rootExpeID)
                        {
                            mrr = mrrServ.getRegRenByExpe(rootExpeID);
                            vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimerRegistro"] = mrr.Dat.RegistroNro.AsInt;
                        }
                        #endregion Primer Registro

                        if ( expeSit.RowCount > 0 ) 
						{
							if (expeSit.RowCount > 1 ) { npublic =  " * " ; }

							vConsMarcaLitigios.Dat.str_public.Value =  "Venc.Ult.Public. " + expeSit.Dat.VencimientoFecha.AsString + npublic; 
						}

						string denom = vConsMarcaLitigios.Dat.Denominacion.AsString;
						if( denom.Trim() == "" )
						{
							denom = "*Sin Denominación*";
						}

	 				
						vConsMarcaLitigios.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
							vConsMarcaLitigios.Dat.ExpedienteID.AsString, 
							denom,
							"MarcaDetalleLitigios.aspx","ExpeID" );	
						string clienteNombre = vConsMarcaLitigios.Dat.ClienteNombre.AsString;
					
						
						vConsMarcaLitigios.Dat.ClienteNombre.Value = HtmlGW.Redirect_Link(vConsMarcaLitigios.Dat.ClienteID.AsString,
																						  vConsMarcaLitigios.Dat.ClienteID.AsString, 
																						  "ClienteDatos.aspx", "ClienteID")
																	 + " - " + clienteNombre;
						
                        
						


						//	HtmlGW.Redirect_Link(vConsMarcaLitigios.Dat.ClienteID.AsString, clienteNombre, "ClienteDatos.aspx", "ClienteID");

						Berke.DG.DBTab.BoletinDet boldet = new Berke.DG.DBTab.BoletinDet(db);
						boldet.ClearFilter();
						//boldet.Dat.ExpedienteID.Filter = vConsMarcaLitigios.Dat.ExpedienteID.AsInt;
						boldet.Dat.ExpNro.Filter = vConsMarcaLitigios.Dat.ActaNro.AsInt;
						boldet.Dat.ExpAnio.Filter = vConsMarcaLitigios.Dat.ActaAnio.AsInt;
						boldet.Adapter.ReadAll();

						string boletines = "";
						for (boldet.GoTop(); !boldet.EOF; boldet.Skip())
						{
							if (boletines != "")
							{
								boletines += "; ";
							}

							boletines = boldet.Dat.BolNro.AsString + "/" + boldet.Dat.BolAnio.AsString;
						}

						vConsMarcaLitigios.Dat.bolinfo.Value = boletines;
	
						vConsMarcaLitigios.Dat.str_AgenteLocal.Value = vConsMarcaLitigios.Dat.AgenteLocalID.AsString + " - " + vConsMarcaLitigios.Dat.Nombre.AsString;

						vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["AtencionPorMarca"] = this.getNombreAtencionxMarBU(vConsMarcaLitigios.Dat.TipoAtencionxMarca.AsInt,
																																vConsMarcaLitigios.Dat.IDTipoAtencionxMarca.AsInt, vConsMarcaLitigios.Dat.MarcaID.AsInt,
																																db);

						vConsMarcaLitigios.PostEdit();
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
            //if (pertenciaParam == "M")
            //{
            //    dgResult.Columns[13].Visible = true;     //situacion
            //}
            //else
            //{
            //    dgResult.Columns[13].Visible = false;  //situacion

            //}
            dgResult.Columns[this.GetColumnIndex(dgResult, "PrimerRegistro")].Visible = this.pertenenciaParam;
            //dgResult.Columns[this.GetColumnIndex(dgResult, "RegistroNro")].Visible = this.pertenenciaParam;
            dgResult.Columns[this.GetColumnIndex(dgResult, "AtencionPorMarca")].Visible = this.pertenenciaParam;

            dgResult.Columns[this.GetColumnIndex(dgResult, "ConcesionFecha")].Visible = !this.pertenenciaParam;
            dgResult.Columns[this.GetColumnIndex(dgResult, "MarcaActiva")].Visible = !this.pertenenciaParam;
            dgResult.Columns[this.GetColumnIndex(dgResult, "str_public")].Visible = !this.pertenenciaParam;
            dgResult.Columns[this.GetColumnIndex(dgResult, "bolinfo")].Visible = !this.pertenenciaParam;

            //dgResult.Columns[16].Visible = false;

            //BoundColumn AtencionPorMarca = new BoundColumn();
            //AtencionPorMarca.DataField = "AtencionPorMarca";
            //AtencionPorMarca.HeaderText = "At. Marca";
            //dgResult.Columns.AddAt(14, AtencionPorMarca);

            //         BoundColumn PrimerRegistro = new BoundColumn();
            //         PrimerRegistro.DataField = "PrimerRegistro";
            //         PrimerRegistro.HeaderText = "Primer Registro";
            //         PrimerRegistro.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            //         dgResult.Columns.AddAt(7, PrimerRegistro);


            dgResult.DataSource = vConsMarcaLitigios.Table;
			dgResult.DataBind();

            //this.btGenDoc.Visible = vConsMarcaLitigios.RowCount > 0;
            //this.chkUnaTablaPorMarca.Visible = vConsMarcaLitigios.RowCount > 0;
            this.lblError.Text = String.Empty;

			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vConsMarcaLitigios.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Expedientes de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vConsMarcaLitigios.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#region Pasa a la pagina de detalle si hay un unico registro
			/*
			if( vConsMarcaLitigios.RowCount == 1 )
			{
				Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
				vConsMarcaLitigios.GoTop();
				url.AddParam("ExpeID", vConsMarcaLitigios.Dat.ExpedienteID.AsString );
				url.redirect( "MarcaDetalleLitigios.aspx" );
			}
			*/
			#endregion 

			#endregion

		}

        private int GetColumnIndex(DataGrid grid, string colName)
        {
            foreach (DataGridColumn col in grid.Columns)
            {
                //if (col.HeaderText.ToLower().Trim() == ColName.ToLower().Trim())
                //{
                //    return grid.Columns.IndexOf(col);
                //}

                if (col.GetType() == typeof(BoundColumn))
                {
                    BoundColumn bCol = col as BoundColumn;

                    if (bCol.DataField == colName)
                    {
                        return grid.Columns.IndexOf(col);
                    }
                }
                else if (col.GetType() == typeof(TemplateColumn))
                {
                    if (col.HeaderText == "Marca Activa")
                    {
                        string hola = "xxx";
                    }

                    if (col.HeaderText.Replace(" ", string.Empty).ToLower().Trim() == colName.ToLower().Trim())
                    {
                        return grid.Columns.IndexOf(col);
                    }
                }
            }
            return -1;
        }

        private void btBuscar_Click(object sender, System.EventArgs e)
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

		protected void dgResult_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

        protected void btGenDoc_Click(object sender, System.EventArgs e)
        {
            this.lblError.Text = String.Empty;
            //this.lblError.Visible = false;
            #region marcados

            CheckBox ch;

            string str_exp = "";
            foreach (DataGridItem item in dgResult.Items)
            {
                ch = (CheckBox)item.FindControl("cbSel");
                
                if (ch.Checked)
                {
                    //Label bDenominacion=(TextBox)item.FindControl("Expediente").;
                    string exp = ((Label)item.FindControl("ExpedienteID")).Text.ToString();
                    str_exp = str_exp + exp + ",";

                }

            }
            #endregion
            //FiltrarDatos();
            if (str_exp.Length > 0)
            {
                vConsMarcaLitigios.ClearOrder();
                vConsMarcaLitigios.Dat.Denominacion.Order = 1;
                vConsMarcaLitigios.Dat.ClaseNro.Order = 2;
                vConsMarcaLitigios.Dat.PropietarioNombre.Order = 3;
                vConsMarcaLitigios.Dat.Acta.Order = 4;
                vConsMarcaLitigios.Dat.PresentacionFecha.Order = 5;
                vConsMarcaLitigios.Dat.Registro.Order = 6;
                vConsMarcaLitigios.Dat.ConcesionFecha.Order = 7;
                vConsMarcaLitigios.Dat.VencimientoFecha.Order = 8;

                vConsMarcaLitigios.ClearFilter();
                vConsMarcaLitigios.Dat.ExpedienteID.Filter = GetFilter(str_exp.Substring(0, str_exp.Length - 1));
                vConsMarcaLitigios.Adapter.ReadAll();

                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("PrimerActa", typeof(string)));
                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("PrimeraFechaSolicitud", typeof(DateTime)));
                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("PrimerRegistro", typeof(int)));
                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("PrimeraConcesion", typeof(DateTime)));
                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("DescripcionClase", typeof(string)));
                vConsMarcaLitigios.Table.Columns.Add(new DataColumn("LogotipoID", typeof(int)));

                Berke.Libs.Boletin.Services.ExpedienteService expeServ = new Libs.Boletin.Services.ExpedienteService(db);
                Berke.Libs.Boletin.Services.MarcaService marServ = new Libs.Boletin.Services.MarcaService(db);
                Berke.Libs.Boletin.Services.MarcaRegRenService mrrServ = new Libs.Boletin.Services.MarcaRegRenService(db);
                
                Berke.DG.DBTab.Expediente expe = new DG.DBTab.Expediente();
                Berke.DG.DBTab.Marca mar = new DG.DBTab.Marca();
                Berke.DG.DBTab.MarcaRegRen mrr = new DG.DBTab.MarcaRegRen();
                Berke.DG.DBTab.Clase cla = new DG.DBTab.Clase(db);

                for (vConsMarcaLitigios.GoTop(); !vConsMarcaLitigios.EOF; vConsMarcaLitigios.Skip())
                {
                    vConsMarcaLitigios.Edit();
                    #region Primer Registro
                    int rootExpeID = Berke.Marcas.BizActions.Lib.ExpedienteRootID(vConsMarcaLitigios.Dat.ExpedienteID.AsInt, db);

                    vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimeraFechaSolicitud"] = expe.Dat.PresentacionFecha.AsDateTime;

                    if (vConsMarcaLitigios.Dat.ExpedienteID.AsInt != rootExpeID)
                    {
                        expe = expeServ.getExpediente(rootExpeID);
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimerActa"] = expe.Dat.Acta.AsString.Trim() != String.Empty ? expe.Dat.Acta.AsString : "--";
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimeraFechaSolicitud"] = expe.Dat.PresentacionFecha.AsDateTime;

                        mrr = mrrServ.getRegRenByExpe(rootExpeID);
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimerRegistro"] = mrr.Dat.RegistroNro.AsInt;
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimeraConcesion"] = mrr.Dat.ConcesionFecha.AsDateTime;

                        mar = marServ.getMarca(vConsMarcaLitigios.Dat.MarcaID.AsInt);
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["LogotipoID"] = mar.Dat.LogotipoID.AsInt;

                        //cla.ClearFilter();
                        //cla.Adapter.ReadByID(vConsMarcaLitigios.Dat.ClaseID.AsInt);
                        //vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["DescripcionClase"] = cla.Dat.Descrip.AsString.Trim() != String.Empty ? cla.Dat.Descrip.AsString : "--"; //cla.Dat.Descrip.AsString;

                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["DescripcionClase"] = mar.Dat.ClaseDescripEsp.AsString.Trim() != String.Empty ? mar.Dat.ClaseDescripEsp.AsString : "--";
                    }
                    else
                    {
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimerActa"] = "--";
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimeraFechaSolicitud"] = DBNull.Value;

                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimerRegistro"] = DBNull.Value;
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["PrimeraConcesion"] = DBNull.Value;

                        mar = marServ.getMarca(vConsMarcaLitigios.Dat.MarcaID.AsInt);
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["LogotipoID"] = mar.Dat.LogotipoID.AsInt;

                        //cla.ClearFilter();
                        //cla.Adapter.ReadByID(vConsMarcaLitigios.Dat.ClaseID.AsInt);
                        //vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["DescripcionClase"] = cla.Dat.Descrip.AsString.Trim() != String.Empty ? cla.Dat.Descrip.AsString : "--";
                        vConsMarcaLitigios.Table.Rows[vConsMarcaLitigios.RowIndex]["DescripcionClase"] = mar.Dat.ClaseDescripEsp.AsString.Trim() != String.Empty ? mar.Dat.ClaseDescripEsp.AsString : "--";
                    }

                    #endregion Primer Registro
                    vConsMarcaLitigios.PostEdit();
                }

                Session["UltimoTramiteMarcasDS"] = vConsMarcaLitigios.Table;
                ClientScript.RegisterClientScriptBlock(this.GetType(),
                                                        "OpenReportPage",
                                                        String.Format(REPORT_URL, this.chkUnaTablaPorMarca.Checked ? TABLE : LIST), //"openReportPage()", 
                                                        true);
            }
            //else
            //{
            //    this.lblError.Text = "Debe seleccionar alguna marca para generar el reporte";
            //    this.lblError.Visible = true;
            //}
            
        }

        private void btGenDoc_Click1(object sender, System.EventArgs e)
		{
			#region marcados

			CheckBox ch;

			string str_exp ="";
			foreach( DataGridItem item in dgResult.Items ) 
			{
				ch = (CheckBox)item.FindControl("cbSel");
			
			
				if ( ch.Checked )
				{
					//Label bDenominacion=(TextBox)item.FindControl("Expediente").;
					 string exp = ((Label)item.FindControl("ExpedienteID")).Text.ToString();
					 str_exp= str_exp + exp + ",";
				
				}

			}
			#endregion
			//FiltrarDatos();
			if ( str_exp.Length > 0 ) 
			{
				vConsMarcaLitigios.Dat.ExpedienteID.Filter = GetFilter(str_exp.Substring(0,str_exp.Length-1));
				vConsMarcaLitigios.Adapter.ReadAll();
			}
			

			string buffer= "";
			Berke.Libs.CodeGenerator cg = null;

			if (!chkUnaTablaPorMarca.Checked)
			{
				#region Obtener plantilla 
				//string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("MarCliLit", 2);
				//if( plantilla == "" )
				//{
				//	this.ShowMessage( "Error con la plantilla" );
				//	return ;
				//}
				#endregion Obtener plantilla

				#region Inicializar Generadores de codigo
				//cg = new  Berke.Libs.CodeGenerator(plantilla);
				//Berke.Libs.CodeGenerator tabla        = cg.ExtraerTabla("tabla");
				//Berke.Libs.CodeGenerator tablaTitulo  = tabla.ExtraerFila("tablaTitulo");
				//Berke.Libs.CodeGenerator tablaFila    = tabla.ExtraerFila("tablaFila");
				#endregion Inicializar Generadores de codigo

				//tablaFila.clearText();
			
				vConsMarcaLitigios.GoTop();

				int cntMarcas = 0;

				#region Generar Archivo
				Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );
				string npublic = "";
				for( vConsMarcaLitigios.GoTop(); ! vConsMarcaLitigios.EOF ; vConsMarcaLitigios.Skip() )
				{

					expeSit.ClearOrder();
					expeSit.Dat.ExpedienteID   .Filter	= vConsMarcaLitigios.Dat.ExpedienteID.AsInt;
					expeSit.Dat.TramiteSitID   .Filter  = 4;
					expeSit.Dat.SituacionFecha .Order	= -1;
						
					expeSit.Adapter.ReadAll();

					vConsMarcaLitigios.Edit();

					if ( expeSit.RowCount > 0 ) 
					{
						if (expeSit.RowCount > 1 ) { npublic =  " * " ; }

						vConsMarcaLitigios.Dat.str_public.Value =  expeSit.Dat.VencimientoFecha.AsString + npublic; 
					}

					//tablaFila.copyTemplateToBuffer();
					//tablaFila.replaceField("Marca",vConsMarcaLitigios.Dat.Denominacion.AsString);
					//tablaFila.replaceField("T",vConsMarcaLitigios.Dat.marcatipo.AsString);
					//tablaFila.replaceField("Trm",vConsMarcaLitigios.Dat.TramiteAbrev.AsString);
					//tablaFila.replaceField("Clase",vConsMarcaLitigios.Dat.ClaseNro.AsString);

					//tablaFila.replaceField("Propietario",vConsMarcaLitigios.Dat.PropietarioNombre.AsString);

					//tablaFila.replaceField("RegNro",vConsMarcaLitigios.Dat.Registro.AsString);
					//tablaFila.replaceField("Concesion",vConsMarcaLitigios.Dat.ConcesionFecha.AsString);
					//tablaFila.replaceField("ActaNro",vConsMarcaLitigios.Dat.Acta.AsString);
					//tablaFila.replaceField("Presentacion",vConsMarcaLitigios.Dat.PresentacionFecha.AsString);
					//tablaFila.replaceField("Vencimiento",vConsMarcaLitigios.Dat.VencimientoFecha.AsString);

					//tablaFila.replaceField("activa",vConsMarcaLitigios.Dat.Activa.AsString);
					//tablaFila.replaceField("propia",vConsMarcaLitigios.Dat.Vigilada.AsString);

					//// Aqui se debe agregar el dato para mostrar fecha de vencimiento de la ultima publicacion
					//tablaFila.replaceField("vencpublic",vConsMarcaLitigios.Dat.str_public.AsString);

					//tablaFila.addBufferToText();

					cntMarcas++ ;
				
				}
                //tabla.copyTemplateToBuffer();

                //tablaTitulo.copyTemplateToBuffer();
                //tablaTitulo.addBufferToText();
                //tabla.replaceLabel("tablaTitulo", tablaTitulo.Texto);
                //tabla.replaceLabel("tablaFila"  , tablaFila.Texto);
                //tabla.addBufferToText();

                //cg.copyTemplateToBuffer();
                //cg.replaceLabel("tabla",tabla.Texto);
                //cg.replaceField("Fecha",System.DateTime.Today.ToShortDateString());
                //cg.replaceField("Nmarcas", cntMarcas.ToString());

                //cg.addBufferToText();
                #endregion
                
            }
			else
			{
				#region Obtener plantilla 
				string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("Listado_tablas", 1);
				if( plantilla == "" )
				{
					this.ShowMessage( "Error con la plantilla" );
					return ;
				}
				#endregion Obtener plantilla

				#region Inicializar Generadores de codigo
				cg = new  Berke.Libs.CodeGenerator(plantilla);
				Berke.Libs.CodeGenerator tabla        = cg.ExtraerTabla("tabla");
				Berke.Libs.CodeGenerator tablaFila	  = tabla.ExtraerFila("tablaFila", 12);
				#endregion Inicializar Generadores de codigo

				tablaFila.clearText();
			
				vConsMarcaLitigios.GoTop();

				int cntMarcas = 0;

				Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );
				string npublic = "";
				for( vConsMarcaLitigios.GoTop(); ! vConsMarcaLitigios.EOF ; vConsMarcaLitigios.Skip() )
				{

					expeSit.ClearOrder();
					expeSit.Dat.ExpedienteID   .Filter	= vConsMarcaLitigios.Dat.ExpedienteID.AsInt;
					expeSit.Dat.TramiteSitID   .Filter  = 4;
					expeSit.Dat.SituacionFecha .Order	= -1;
						
					expeSit.Adapter.ReadAll();

					vConsMarcaLitigios.Edit();

					if ( expeSit.RowCount > 0 ) 
					{
						if (expeSit.RowCount > 1 ) { npublic =  " * " ; }

						vConsMarcaLitigios.Dat.str_public.Value =  expeSit.Dat.VencimientoFecha.AsString + npublic; 
					}

					tablaFila.copyTemplateToBuffer();
					tablaFila.replaceField("marca.denominacion",vConsMarcaLitigios.Dat.Denominacion.AsString);
					//tablaFila.replaceField("T",vConsMarcaLitigios.Dat.marcatipo.AsString);
					//tablaFila.replaceField("Trm",vConsMarcaLitigios.Dat.TramiteAbrev.AsString);
					tablaFila.replaceField("clase.nro",vConsMarcaLitigios.Dat.ClaseNro.AsString);
					tablaFila.replaceField("expediente.Actanro",vConsMarcaLitigios.Dat.ActaNro.AsString);
					tablaFila.replaceField("expediente.Actanio",vConsMarcaLitigios.Dat.ActaAnio.AsString);

					//RegAnt o RegAct
					tablaFila.replaceField("regant.nro",vConsMarcaLitigios.Dat.Registro.AsString);
					//tablaFila.replaceField("regact.nro","--");
					tablaFila.replaceField("marcaregren.concesionfecha",vConsMarcaLitigios.Dat.ConcesionFecha.AsString);
					tablaFila.replaceField("marcaregren.VencimientoFecha",vConsMarcaLitigios.Dat.VencimientoFecha.AsString);

					tablaFila.replaceField("proact.nombre",vConsMarcaLitigios.Dat.PropietarioNombre.AsString);
					tablaFila.replaceField("expediente.situacion",vConsMarcaLitigios.Dat.SituacionDescrip.AsString);
					tablaFila.replaceField("expediente.fecha",vConsMarcaLitigios.Dat.PresentacionFecha.AsString);
					

					/*tablaFila.replaceField("activa",vConsMarcaLitigios.Dat.Activa.AsString);
					tablaFila.replaceField("propia",vConsMarcaLitigios.Dat.Vigilada.AsString);*/

					// Aqui se debe agregar el dato para mostrar fecha de vencimiento de la ultima publicacion
					/*tablaFila.replaceField("vencpublic",vConsMarcaLitigios.Dat.str_public.AsString);*/

					tablaFila.addBufferToText();

					cntMarcas++ ;
				
				}
				tabla.copyTemplateToBuffer();

				//tablaTitulo.copyTemplateToBuffer();
				//tablaTitulo.addBufferToText();
				//tabla.replaceLabel("tablaTitulo", tablaTitulo.Texto);
				tabla.replaceLabel("tablaFila"  , tablaFila.Texto);
				tabla.addBufferToText();

				cg.copyTemplateToBuffer();
				cg.replaceLabel("tabla",tabla.Texto);
				/*cg.replaceField("Fecha",System.DateTime.Today.ToShortDateString());
				cg.replaceField("Nmarcas", cntMarcas.ToString());*/

				cg.addBufferToText();
			}





			buffer = cg.Texto;

			#region Activar MS-Word
			
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=marcasCli.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); 
			Response.End();
			#endregion Activar MS-Word		
		}

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion ShowMessage

		#region Marcar/DesMarcar Todos
		protected void btnMarcarDes_Click(object sender, System.EventArgs e)
		{

            CheckBox ch;
            foreach (DataGridItem item in dgResult.Items)
            {
                ch = (CheckBox)item.FindControl("cbSel");

                if (ch.Checked)
                {
                    ch.Checked = false;
                }
                else
                {
                    ch.Checked = true;
                }
            }
        }
		#endregion 

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			btnMarcarDes_Click( sender,  e);
		}

		//private void dgResult_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		//{
			
			
		//}

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

    } // end class ExpeMarcaConsulta
} // end namespace Berke.Marcas.WebUI.Home


