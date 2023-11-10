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

namespace Berke.Marcas.WebUI.Home
{
	public partial class HIConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form








		#endregion 
	
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{


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

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
						


			#region Asignar Parametros (vHIresu)
			Berke.DG.ViewTab.vHIresu vHIresu = new Berke.DG.ViewTab.vHIresu();

	
			vHIresu.NewRow(); 
	
			vHIresu.Dat.Anio		.Value = txtAnio.Text;
			vHIresu.Dat.Nro			.Value = txtNro_min.Text;
			vHIresu.Dat.AltaFecha	.Value = txtAltaFecha_min.Text;

			vHIresu.PostNewRow();
	
			vHIresu.NewRow(); 
	
			vHIresu.Dat.Nro			.Value = txtNro_max.Text;
			vHIresu.Dat.AltaFecha	.Value = txtAltaFecha_max.Text;

			vHIresu.PostNewRow();
	
			#endregion Asignar Parametros ( vHIresu )


			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vHIresu =  Berke.Marcas.UIProcess.Model.HI.ReadList( vHIresu );
				for( vHIresu.GoTop(); ! vHIresu.EOF ; vHIresu.Skip() ){
					#region Asignar Link

					vHIresu.Edit();

					switch( vHIresu.Dat.TipoTrabajoID.AsInt )
					{

							#region Registro
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO :  // Registro

							vHIresu.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
								vHIresu.Dat.ID.AsString, 
								vHIresu.Dat.OrdenTrabajo.AsString,
								"OrdenTrabajoDetalle.aspx" , "OtID"  );
							break;
							#endregion Registro

							#region Renovaciones
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION :  // Renovaciones

							vHIresu.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
								vHIresu.Dat.ID.AsString, 
								vHIresu.Dat.OrdenTrabajo.AsString,
								"RenovacionDetalle.aspx" , "OtID"  );
							
							break;
							#endregion Renovaciones

							#region TramitesVarios
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA :  // Transferencia
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE :  // Cambio de Nombre
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.FUSION :  // Fusion
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO :  // Cambio de Domicilio
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.LICENCIA :  // Licencia
						case (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.DUPLICADO :  // Duplicado de Titulo

						
							vHIresu.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
								vHIresu.Dat.ID.AsString, 
								vHIresu.Dat.OrdenTrabajo.AsString,
								"TramitesVariosDetalle.aspx" , "OtID",@"&oper=Consulta"  );
						
							break;
							#endregion TramitesVarios

							#region Default
						default :
							vHIresu.Dat.OrdenTrabajo.Value = HtmlGW.Redirect_Link(
								vHIresu.Dat.ID.AsString, 
								vHIresu.Dat.OrdenTrabajo.AsString,
								"OtMarcaDetalle.aspx" , "OtID" );		 						
							break;
							#endregion  Default

					}// end switch
					
					vHIresu.PostEdit();

					#endregion Asignar Link
			}// endfor

				
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: HIConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vHIresu.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vHIresu.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "HI &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vHIresu.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "HI";
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




		#endregion Carga de Combo

	} // end class HIConsulta
} // end namespace Berke.Marcas.WebUI.Home


