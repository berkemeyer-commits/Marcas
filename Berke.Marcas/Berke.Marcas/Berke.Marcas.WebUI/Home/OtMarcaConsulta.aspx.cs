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
	public partial class OtMarcaConsulta : System.Web.UI.Page
	{
		#region Properties

		#region SessionID
		private string SessionID 
		{
			get{ return HttpContext.Current.Session.SessionID.ToString();}
		}
		#endregion SessionID
	
		#region Oper_Param 
		private string Oper_Param // Operacion indicada en UrlParam  .Mantenimiento, Consulta,....
		{
			set{ ViewState["Oper_Param" + SessionID ] = value; }
			get{ return (string) ViewState["Oper_Param" + SessionID ];}
		}
		#endregion Oper_Param

		#region Item_Param
		private string Item_Param // Item de menu indicado en UrlParam. Marcas,Poderes,...
		{
			set{ ViewState["Item_Param" + SessionID ] = value; }
			get{ return (string) ViewState["Item_Param" + SessionID ];}
		}
		#endregion item_param

		#region Borrar_Param
		private string Borrar_Param // Parametro para indicar ingreso por la opcion de Eliminar HI
		{
			set{ ViewState["Borrar_Param" + SessionID ] = value; }
			get{ return (string) ViewState["Borrar_Param" + SessionID ];}
		}
		#endregion Borrar_param


		#endregion Properties

		#region Variables Globales
	
		#endregion 

		#region Controles del Web Form























	
		#endregion 
		
		#region Asignar Delegados
		private void AsignarDelegados()
		{

			this.cbxFuncionarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxFuncionarioID_LoadRequested); 
			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
			ddlTramiteID.SelectedIndexChanged +=new EventHandler(ddlTramiteID_SelectedIndexChanged);

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Tramite DropDown
			
//			SimpleEntryDS seTramite = Berke.Entidades.UIProcess.Model.Tramite.ReadForSelect( (int) Const.Proceso.MARCAS );
//			ddlTramiteID.Fill( seTramite.Tables[0], true);

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);


			
			#endregion Tramite DropDown



			#region Clase
			SimpleEntryDS claseSE = Berke.Marcas.UIProcess.Model.Clase.ReadForSelect();
			ddlClaseID.Fill( claseSE.Tables[0], true );
			#endregion Clase
			
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				#region Obtener Parametros del Menu
				string sesionID = SessionID;
				Oper_Param	= Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("Oper");  // Operacion indicada en UrlParam  .Mantenimiento, Consulta,....
				Item_Param	= Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("Item");  // Item de menu indicado en UrlParam. Marcas,Poderes,...  
				Borrar_Param= Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("opBorrar");  // Item de menu indicado en UrlParam. Marcas,Poderes,...  
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
			this.cbxClienteID.LoadRequested += new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested);
			this.cbxFuncionarioID.LoadRequested += new ecWebControls.LoadRequestedHandler(this.cbxFuncionarioID_LoadRequested);

		}
		#endregion

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{				

			#region Asignar Parametros (vOtMarca)
			Berke.DG.ViewTab.vOtMarca vOtMarca = new Berke.DG.ViewTab.vOtMarca();

	
			vOtMarca.NewRow(); 
			//vOtMarca.Dat.TramiteID	.Value = ddlTramiteID.Value;
			vOtMarca.Dat.TrabajoTipoID	.Value = this.GetTrabajoTipoID(ddlTramiteID.Value);
			vOtMarca.Dat.SituacionID	.Value = ddlSituacionID.Value;

			vOtMarca.Dat.ActaNro	.Value = txtActaNro_min.Text;
			vOtMarca.Dat.ClaseID	.Value = ddlClaseID.Value;
			vOtMarca.Dat.ActaAnio	.Value = txtActaAnio.Text;
			vOtMarca.Dat.RegistroAnio	.Value = txtRegistroAnio.Text;
			vOtMarca.Dat.RegistroNro	.Value = txtRegistroNro_min.Text;
			vOtMarca.Dat.FuncionarioID	.Value = cbxFuncionarioID.SelectedValue;
			vOtMarca.Dat.ClienteID	.Value = cbxClienteID.SelectedValue;
			vOtMarca.Dat.Anio	.Value = txtAnio.Text;
			vOtMarca.Dat.Nro	.Value = txtNro_min.Text;
			vOtMarca.Dat.OtID	.Value = txtOtID_min.Text;
			vOtMarca.Dat.Obs	.Value = txtObs.Text;
			vOtMarca.Dat.Denominacion	.Value = txtDenominacion.Text;

			

			
			vOtMarca.Dat.ExpClienteID   .Value = cbxClienteID.SelectedValue; 

			vOtMarca.Dat.CorrespNro     .Value = txtCorrespNro.Text;
			vOtMarca.Dat.Correspanio    .Value = txtCorrespAnio.Text;


			vOtMarca.PostNewRow();
	
			vOtMarca.NewRow(); 
	
			vOtMarca.Dat.ActaNro	.Value = txtActaNro_max.Text;
			vOtMarca.Dat.RegistroNro	.Value = txtRegistroNro_max.Text;
			vOtMarca.Dat.Nro	.Value = txtNro_max.Text;
			vOtMarca.Dat.OtID	.Value = txtOtID_max.Text;

			vOtMarca.PostNewRow();
	
			#endregion Asignar Parametros ( vOtMarca )

			#region Obtener datos
			int recuperados = -1;
			string destino="";
			string w_OtID = "";
			bool OtUnica = true;
			try 
			{
				
				vOtMarca =  Berke.Marcas.UIProcess.Model.OtMarca.ReadList( vOtMarca );

				vOtMarca.GoTop();
				w_OtID = vOtMarca.Dat.OtID.AsString ;

				#region Convertir a Link
				for( vOtMarca.GoTop(); ! vOtMarca.EOF ; vOtMarca.Skip() )
				{
					OtUnica =  w_OtID.ToString() == vOtMarca.Dat.OtID.AsString  ;

					vOtMarca.Edit();

					
					//switch( vOtMarca.Dat.TrabajoTipoID.AsInt ){
					switch( vOtMarca.Dat.TramiteID.AsInt){

						#region Registro
						
						case (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO :  // Registro

							if( Oper_Param == "Mantenimiento" )
							{
//								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
//									vOtMarca.Dat.OtID.AsString, 
//									vOtMarca.Dat.OrdenTrabajo.AsString,
//									"OrdenTrabajoRegistro.aspx" , "otID",@"&page=5" );
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"HIRegistroMarcas.aspx" , "otID" );
								destino="HIRegistroMarcas.aspx?OtID=" + vOtMarca.Dat.OtID.AsString ;
							} 
							else 
							{
								string complemento= "&page=4" + "&opBorrar=" + Borrar_Param;
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									//"OrdenTrabajoDetalle.aspx" , "OtID",@"&page=4");
									"OrdenTrabajoDetalle.aspx" , "OtID",@complemento);
								destino="OrdenTrabajoDetalle.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + "&page=4" + "&opBorrar=" + Borrar_Param;
							}
							break;
						#endregion Registro

						#region Renovaciones
						case (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION :  // Renovaciones

							if( Oper_Param == "Mantenimiento" ){
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"Renovacion.aspx" , "OtID" );
								destino="Renovacion.aspx?OtID=" + vOtMarca.Dat.OtID.AsString ;
							} else {
								string complemento= "&oper=Consulta" + "&opBorrar=" + Borrar_Param;
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"RenovacionDetalle.aspx" , "OtID",@complemento  );
								destino="RenovacionDetalle.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + "&opBorrar=" + Borrar_Param;;
							}
							break;
						#endregion Renovaciones

						#region TramitesVarios
						case (int)GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA :  // Transferencia
						case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE :  // Cambio de Nombre
						case (int)GlobalConst.Marca_Tipo_Tramite.FUSION :  // Fusion
						case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO :  // Cambio de Domicilio
						case (int)GlobalConst.Marca_Tipo_Tramite.LICENCIA :  // Licencia
						case (int)GlobalConst.Marca_Tipo_Tramite.DUPLICADO :  // Duplicado de Titulo
						case (int)GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC :

							if( Oper_Param == "Mantenimiento" )
							{
								string tramite = "&tramiteID=" + vOtMarca.Dat.TrabajoTipoID.AsString;
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"TramitesVarios.aspx" , "OtID",@tramite );
								destino="TramitesVarios.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + tramite;
							} 
							else 
							{
								string complemento= "&oper=Consulta" + "&opBorrar=" + Borrar_Param;
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"TramitesVariosDetalle.aspx" , "OtID",@complemento  );
								destino="TramitesVariosDetalle.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + "&opBorrar=" + Borrar_Param;;
							}
							break;
						#endregion TramitesVarios

							#region Renovaciones
						case (int)GlobalConst.Marca_Tipo_Tramite.REG_ADUANA :  // Reg. en Aduanas

							if( Oper_Param == "Mantenimiento" )
							{
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"RegAduana.aspx" , "OtID" );
								destino="RegAduana.aspx?OtID=" + vOtMarca.Dat.OtID.AsString ;
							} 
							else 
							{
								string complemento= "&oper=Consulta" + "&opBorrar=" + Borrar_Param;
								vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
									vOtMarca.Dat.OtID.AsString, 
									vOtMarca.Dat.OrdenTrabajo.AsString,
									"RegAduanaDetalle.aspx" , "OtID",@complemento  );
								destino="RegAduanaDetalle.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + "&opBorrar=" + Borrar_Param;;
							}
							break;
							#endregion Renovaciones

						#region Default
						default :
							vOtMarca.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
								vOtMarca.Dat.OtID.AsString, 
								vOtMarca.Dat.OrdenTrabajo.AsString,
								"OtMarcaDetalle.aspx" , "OtID" );
		 					destino="OtMarcaDetalle.aspx?OtID=" + vOtMarca.Dat.OtID.AsString + "&opBorrar=" + Borrar_Param ;
								break;
						#endregion  Default

					}
					vOtMarca.PostEdit();
				}
				#endregion
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}//Aqui
			catch( Exception exep ) 
			{
				throw new Exception("OtMarcaConsulta", exep );
			} // a Aqui

			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vOtMarca.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vOtMarca.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				//if( vOtMarca.RowCount == 1 ){

				if( OtUnica ){
					
					Response.Redirect(destino);
				}

				lblTituloGrid.Text = "Ordenes de Trabajo de Marcas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vOtMarca.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Ordenes de Trabajo de Marcas";
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


		#region Funcionario
		private void cbxFuncionarioID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
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
				Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Funcionario.ReadForSelect(inTB );
				combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
			
		}
			#endregion Funcionario		

		#region Cliente
		private void cbxClienteID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			#region Asignar Parametros
			Berke.DG.ViewTab.ParamTab inTB	=   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )
			{
				inTB.Dat.Entero.Value = Convert.ToInt32( combo.Text );
			}
			else
			{
				inTB.Dat.Alfa.Value = combo.Text;			
			}
			inTB.PostNewRow(); 
			#endregion Asignar Parametros
		
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.Cliente.ReadForSelect( inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
				#endregion Cliente		



		#endregion Carga de Combo

		#region ddlTramiteID_SelectedIndexChanged
		private void ddlTramiteID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlTramiteID.SelectedIndex == 0)
			{
				ddlSituacionID.Items.Clear();
			}
			else
			{
				SimpleEntryDS situacion = Berke.Marcas.UIProcess.Model.TramiteSit.ReadForSelect( int.Parse(ddlTramiteID.SelectedValue ) );
				ddlSituacionID.Fill( situacion.Tables[0], true );
				
			}
		}
		#endregion 

		private string GetTrabajoTipoID(string tramiteID)
		{
			string tipoTrabajoID = "";

			if (tramiteID != "")
			{
				#region Crear Conexión a BD
				Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

				db.DataBaseName	 = WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	 = WebUI.Helpers.MyApplication.CurrentServerName;
				#endregion Crear Conexión a BD
				Berke.DG.DBTab.Tramite trm = new Berke.DG.DBTab.Tramite(db);
				trm.Adapter.ReadByID(Convert.ToInt32(tramiteID));
				tipoTrabajoID = trm.Dat.TrabajoTipoID.AsString;
				#region Cerrar Conexión a BD
				db.CerrarConexion();
				#endregion Cerrar Conexión a BD
			}

			return tipoTrabajoID;
		}


	} // end class OtMarcaConsulta
	} // end namespace Berke.Marcas.WebUI.Home


