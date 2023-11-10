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
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Marcas.WebUI.Helpers;
using System.Security.Principal;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
	//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class CtrlGenHDesc : System.Web.UI.Page
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

		#region Busqueda de registros 

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
        #endregion Carga de Combo

		
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

		private void Buscar()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

            //Berke.DG.ViewTab.vExpeMarca vExpeMarca = new Berke.DG.ViewTab.vExpeMarca(db);

            //vExpeMarca.Dat.AltaFecha		.Filter = ObjConvert.GetFilter_Date( txtAltaFecha.Text );
            ////vExpeMarca.Dat.Vigente			.Filter = GetFilter_Bool( ddlVigente.SelectedValue);
            //vExpeMarca.Dat.Vigente			.Filter = GetFilter_Bool( rbVigente.SelectedValue);
            ////vExpeMarca.Dat.MarcaNuestra		.Filter = GetFilter_Bool( ddlMarcaNuestra.SelectedValue);
            //vExpeMarca.Dat.MarcaNuestra		.Filter = GetFilter_Bool( rbMarcaNuestra.SelectedValue);
            //vExpeMarca.Dat.VencimientoFecha	.Filter = GetFilter( txtVencimientoFecha_min.Text );
            ////vExpeMarca.Dat.MarcaActiva		.Filter = GetFilter_Bool( ddlMarcaActiva.SelectedValue);
            //vExpeMarca.Dat.MarcaActiva		.Filter = GetFilter_Bool( rbMarcaActiva.SelectedValue);
	
            //vExpeMarca.Dat.PropietarioID	.Filter = GetFilter( txtPropietarioID.Text.Trim()) ;
            //vExpeMarca.Dat.PropietarioNombre.Filter = GetFilter_Str( this.txtPropietarioNombre.Text.Trim()) ;
            //vExpeMarca.Dat.PropietarioPais	.Filter	= GetFilter( this.txtPropietarioPais.Text.Trim() );
					
            ///*[12-04-2007. BUG#14]
            // * Permitimos realizar la busqueda por codigo y nombre de uno o mas clientes
            //*/

            ///*
            //if( txtClienteID.Text.Trim() != "" ){
            //    vExpeMarca.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
            //}else{
            //    vExpeMarca.Dat.ClienteID		.Filter = GetFilter( cbxClienteID.SelectedValue );
            //}
            //*/

            //vExpeMarca.Dat.ClienteID		.Filter = GetFilter( txtClienteID.Text.Trim() );
            //vExpeMarca.Dat.NombreCliente    .Filter = GetFilter_Str( this.txtNombreCli.Text.Trim() );


            //vExpeMarca.Dat.AgenteLocalID	.Filter = GetFilter( cbxAgenteLocalID.SelectedValue );
            //vExpeMarca.Dat.OtNro			.Filter = GetFilter( txtOtNro_min.Text );
            //vExpeMarca.Dat.OtAnio			.Filter = GetFilter( txtOtAnio.Text );
            //vExpeMarca.Dat.ClaseNro			.Filter = GetFilter( txtClaseNro.Text	);
            //if( ddlTipoReg.SelectedValue == "REG" )
            //{
            //    vExpeMarca.Dat.RegistroNro	.Filter = GetFilter( txtRegistroNro_min.Text );
            //    vExpeMarca.Dat.RegistroAnio	.Filter = GetFilter( txtRegistroAnio.Text);		
            //}
            //else
            //{
            //    vExpeMarca.Dat.RegVigenteNro	.Filter = GetFilter( txtRegistroNro_min.Text );
            //    vExpeMarca.Dat.RegVigenteAnio	.Filter = GetFilter( txtRegistroAnio.Text);
            //}
            //vExpeMarca.Dat.ActaNro			.Filter = GetFilter( txtActaNro_min.Text );
            //vExpeMarca.Dat.ActaAnio			.Filter = GetFilter( txtActaAnio.Text );
            //vExpeMarca.Dat.TramiteSitID		.Filter = GetFilter( ddlTramiteSitID.SelectedValue );
            //vExpeMarca.Dat.TramiteID		.Filter = GetFilter( ddlTramiteID.SelectedValue );
            //vExpeMarca.Dat.ExpedienteID		.Filter = GetFilter( txtExpedienteID_min.Text );
            //vExpeMarca.Dat.Denominacion		.Filter = GetFilter_Str( txtDenomEmpieza.Text );
            //vExpeMarca.Dat.MarcaID			.Filter = GetFilter( txtMarcaID_min.Text );
            ////vExpeMarca.Dat.Vigilada			.Filter = GetFilter_Bool(ddlMarcaVigilada.SelectedValue);
            ////vExpeMarca.Dat.StandBy			.Filter = GetFilter_Bool(ddlStandBy.SelectedValue);
            ////vExpeMarca.Dat.Sustituida		.Filter = GetFilter_Bool(ddlSustituida.SelectedValue);
            //vExpeMarca.Dat.Vigilada			.Filter = GetFilter_Bool(rbMarcaVigilada.SelectedValue);
            //vExpeMarca.Dat.StandBy			.Filter = GetFilter_Bool(rbStandBy.SelectedValue);
            //vExpeMarca.Dat.Sustituida		.Filter = GetFilter_Bool(rbSustituida.SelectedValue);
            //vExpeMarca.Dat.EnTramite		.Filter = GetFilter_Bool(rbEnTramite.SelectedValue);
            ///*[ggaleano 08/10/2007] Ref. Bug#423*/

            ///*vExpeMarca.Dat.bolnro.Filter = GetFilter(txtBoletinNro.Text);
            //vExpeMarca.Dat.bolanio.Filter = GetFilter(txtBoletinAnio.Text);*/

            //string tipo = "";
            //string comma = "";
            //for(int k=0; k<chkTipo.Items.Count;k++)
            //{
            //    if(chkTipo.Items[k].Selected)
            //    {
            //        tipo += comma+chkTipo.Items[k].Value;
            //        comma = ",";
            //    }
            //}

            //vExpeMarca.Dat.MarcaTipo		.Filter = ObjConvert.GetFilter(tipo);

            ///*if (pertenciaParam == "HDescriptiva")
            //{
            //    BoundColumn str_public = new BoundColumn();
            //    str_public.DataField = "str_public";
            //    str_public.HeaderText = "Ult. Public.";
            //    str_public.DataFormatString = "{0:dd/MM/yy}";
            //    dgResult.Columns.AddAt(11, str_public);
            //}*/

			


            //#region Obtener datos
            //int recuperados = -1;
            //try
            //{

            //    try 
            //    {
            //        //				vExpeMarca =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList( vExpeMarca );
            //        vExpeMarca.Dat.Denominacion.Order = 1;

            //        recuperados = vExpeMarca.Adapter.Count();
            //        if( recuperados < 2000 )
            //        {
            //            recuperados = -1;
            //            string bf = vExpeMarca.Adapter.ReadAll_CommandString();
            //            vExpeMarca.Adapter.ReadAll( 2000 );
					
            //            #region eliminar Duplicados ( si no se buscó por propietario ) 
            //            //					if( cbxPropietarioID.SelectedValue.Trim() == "" )
            //            //					{
            //            int antID = -19992221;
            //            for( vExpeMarca.GoTop(); ! vExpeMarca.EOF; vExpeMarca.Skip() )
            //            {
            //                if( vExpeMarca.Dat.ExpedienteID.AsInt == antID )
            //                {
            //                    vExpeMarca.Delete();
            //                }
            //                else
            //                {
            //                    antID = vExpeMarca.Dat.ExpedienteID.AsInt;
            //                }
            //            }// end for
            //            vExpeMarca.AcceptAllChanges();
            //            //					}

            //            #endregion eliminar Duplicados

            //            #region Agregar columna para ver atencion
            //            if (!vExpeMarca.Table.Columns.Contains("AtencionPorMarca"))
            //            {
            //                vExpeMarca.Table.Columns.Add(new DataColumn("AtencionPorMarca", typeof(String)));
            //            }
            //            #endregion Agregar columna para ver atencion

            //            #region Convertir a Link y asignar pais a propietario
            //            for( vExpeMarca.GoTop(); ! vExpeMarca.EOF ; vExpeMarca.Skip() )
            //            {
            //                vExpeMarca.Edit();

            //                string denom = vExpeMarca.Dat.Denominacion.AsString;
            //                if( denom.Trim() == "" )
            //                {
            //                    denom = "*Sin Denominación*";
            //                }
            //                string propPais = vExpeMarca.Dat.PropietarioPais.AsString.Trim();
            //                if( propPais != "" )
            //                {
            //                    vExpeMarca.Dat.PropietarioNombre.Value = vExpeMarca.Dat.PropietarioNombre.AsString + "("+propPais+")";
            //                }

	 					   
            //                vExpeMarca.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
            //                    vExpeMarca.Dat.ExpedienteID.AsString, 
            //                    denom,
            //                    "MarcaDetalleL.aspx","ExpeID" );	
							

            //                int hj = 0;
            //                int anho = 0;
            //                int iddoc = 0;
            //                string docRef = "";
            //                if (pertenciaParam == "HDescriptiva" || pertenciaParam == "")
            //                {
            //                    if ( vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
            //                        || vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION) ) 
            //                    {
            //                        hj= Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA);
            //                        anho = vExpeMarca.Dat.ActaAnio.AsInt;
            //                        iddoc = vExpeMarca.Dat.ActaNro.AsInt;
            //                    }
            //                }
            //                else if (pertenciaParam == "Publicaciones" || pertenciaParam == "Titulos")
            //                {
            //                    if ( vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO) 
            //                        || vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
            //                        || vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA)
            //                        || vExpeMarca.Dat.TramiteID.AsInt == Convert.ToInt32(Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA)) 
            //                    {
            //                        if (pertenciaParam == "Publicaciones")
            //                        {
            //                            hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.PUBLICACION);
            //                            anho = vExpeMarca.Dat.PublicAnio.AsInt;
            //                            iddoc = vExpeMarca.Dat.PublicPag.AsInt;
            //                        }
            //                        else
            //                        {
            //                            hj = Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.TITULO);
            //                            anho = vExpeMarca.Dat.RegistroAnio.AsInt;
            //                            iddoc = vExpeMarca.Dat.RegistroNro.AsInt;
            //                        }
            //                    }	

            //                }

            //                docRef =  Berke.Libs.Base.DocPath.digitalDocPath(anho, iddoc, hj);
            //                /*docRef =  Berke.Marcas.BizActions.Lib.digitalDocPath(
            //                    vExpeMarca.Dat.ActaAnio		.AsInt,
            //                    vExpeMarca.Dat.ActaNro		.AsInt,
            //                    hj // Hoja Descriptiva
            //                    );*/

            //                vExpeMarca.Dat.TramiteDescrip.Value = docRef;

            //                vExpeMarca.Table.Rows[vExpeMarca.RowIndex]["AtencionPorMarca"] = this.getNombreAtencionxMarBU(vExpeMarca.Dat.TipoAtencionxMarca.AsInt,
            //                    vExpeMarca.Dat.IDTipoAtencionxMarca.AsInt, vExpeMarca.Dat.MarcaID.AsInt,
            //                    db);

            //                vExpeMarca.PostEdit();
            //            }
            //            #endregion  Convertir a Link
            //        }
            //    }
            //    catch ( Berke.Excep.Biz.TooManyRowsException ex )
            //    {	
            //        recuperados = ex.Recuperados;
            //    }
            //    catch( Exception exep ) 
            //    {
            //        throw new Exception("Class: ExpeMarcaConsulta ", exep );
            //    }
            //    #endregion Obtener datos
			
            //    #region Asignar dataSource de grilla

            //    /*
            //    foreach( DataGridItem item in dgResult.Items )
            //    {
            //        ((TextBox)item.FindControl("H.Descriptiva")).Visible = false;
            //    }
            //    */

            //    /*if (pertenciaParam == "HDescriptiva")
            //    {
            //        for (vExpeMarca.GoTop(); !vExpeMarca.EOF; vExpeMarca.Skip())
            //        {
            //            Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
            //            TramSit.ClearFilter();
            //            TramSit.Dat.TramiteID.Filter = vExpeMarca.Dat.TramiteID.AsInt;
            //            TramSit.Dat.SituacionID.Filter = (int) Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
            //            TramSit.Adapter.ReadAll();

            //            vExpeMarca.Edit();
            //            Berke.DG.DBTab.Expediente_Situacion ExpeSitu = new Berke.DG.DBTab.Expediente_Situacion(db);
            //            ExpeSitu.ClearFilter();
            //            ExpeSitu.ClearOrder();
            //            ExpeSitu.Dat.ExpedienteID.Filter = vExpeMarca.Dat.ExpedienteID.AsInt;
            //            ExpeSitu.Dat.TramiteSitID.Filter = TramSit.Dat.ID.AsInt;
            //            ExpeSitu.Dat.VencimientoFecha.Order = -1;
            //            ExpeSitu.Adapter.ReadAll();

            //            vExpeMarca.Dat.str_public.Value = ExpeSitu.Dat.VencimientoFecha.AsString;
            //            vExpeMarca.PostEdit();

            //        }						
            //    }*/

            //    BoundColumn AtencionPorMarca = new BoundColumn();
            //    AtencionPorMarca.DataField = "AtencionPorMarca";
            //    AtencionPorMarca.HeaderText = "At. Marca";
            //    dgResult.Columns.AddAt(10, AtencionPorMarca);

            //    dgResult.DataSource = vExpeMarca.Table;
            //    dgResult.DataBind();
            //    #endregion

                //#region Mostrar Panel segun cantidad de registros obtenidos
                //if( recuperados != -1 )
                //{
                //    MostrarPanel_Busqueda();
                //    lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
                //}
                //else if( vExpeMarca.RowCount == 0 )
                //{
                //    MostrarPanel_Busqueda();
                //    lblMensaje.Text = "No se encontraron registros";
                //}
                //else 
                //{
                //    lblTituloGrid.Text = "Expedientes de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vExpeMarca.RowCount+ "&nbsp;regs.)";
                //    MostrarPanel_Resultado();
                //}
                //#region Pasa a la pagina de detalle si hay un unico registro
                ///* pero quedarse en la grilla si la consulta fue invocada 
                // * para visualizar la hoja descriptiva 
                // * */
                //if( vExpeMarca.RowCount == 1  && pertenciaParam != "HDescriptiva"  )
                //{
                //    Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
                //    vExpeMarca.GoTop();
                //    url.AddParam("ExpeID", vExpeMarca.Dat.ExpedienteID.AsString );
                //    url.redirect( "MarcaDetalleL.aspx" );
                //}
                //#endregion 

                //#endregion
            //}
            //finally
            //{
            //    db.CerrarConexion();
            //}
		}
	
        protected void btnGenDoc_Click(object sender, System.EventArgs e)
		{
			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.word";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dgResult.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();			
		}

		protected void btnGenXls_Click(object sender, System.EventArgs e)
		{
			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
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

		
		


	} // end class ExpeMarcaConsulta
} // end namespace Berke.Marcas.WebUI.Home


