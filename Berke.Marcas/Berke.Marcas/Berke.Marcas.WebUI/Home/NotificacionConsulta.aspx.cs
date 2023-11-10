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
using Berke.Marcas.WebUI.Tools.Helpers;

namespace Berke.Marcas.WebUI.Home
{
	public partial class NotificacionConsulta : System.Web.UI.Page
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
						
			#region Asignar Parametros (Notificacion)
				Berke.DG.DBTab.Notificacion Notificacion = new Berke.DG.DBTab.Notificacion();
				Notificacion.DisableConstraints();
				Notificacion.NewRow(); 

				Notificacion.Dat.ID.Value			= txtID_min.Text;
				Notificacion.Dat.Descrip.Value		= txtDescrip.Text;
				Notificacion.Dat.Mail_Destino.Value = txtMail_Destino.Text;
				Notificacion.Dat.Func_Destino.Value = txtFunc_Destino.Text;
				Notificacion.Dat.Activo.Value		= ddlActivo.SelectedValue;
				
				Notificacion.PostNewRow();
				Notificacion.NewRow(); 
				Notificacion.Dat.ID	.Value = txtID_max.Text;
				Notificacion.PostNewRow();
				//param.PostNewRow();

			#endregion Asignar Parametros ( Notificacion )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				Notificacion =  Berke.Marcas.UIProcess.Model.Notificacion.ReadList( Notificacion );
				//Notificacion =  Berke.Marcas.UIProcess.Model.Notificacion.ReadList( param );	
	
				#region Convertir a Link
				for( Notificacion.GoTop(); ! Notificacion.EOF ; Notificacion.Skip() )
				{
					Notificacion.Edit();
		
						Notificacion.Dat.Descrip.Value = HtmlGW.Redirect_Link(
						Notificacion.Dat.ID.AsString, 
						Notificacion.Dat.Descrip.AsString,
						"NotificacionDetalle.aspx","NotificacionID" );		 						
					Notificacion.PostEdit();
				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
				dgResult.DataSource = Notificacion.Table;
				dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( Notificacion.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Notificaciones &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+Notificacion.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Notificaciones";
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

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			UrlParam param = new UrlParam();

			param.AddParam("Mode", "Insert");
			
			param.redirect("NotificacionDetalle.aspx");
		}

		#region Carga de Combo




		#endregion Carga de Combo

	} // end class NotificacionConsulta
} // end namespace Berke.Marcas.WebUI.Home


