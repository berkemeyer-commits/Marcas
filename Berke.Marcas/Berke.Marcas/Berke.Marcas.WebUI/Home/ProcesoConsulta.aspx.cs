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
	public partial class ProcesoConsulta : System.Web.UI.Page
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
						


			#region Asignar Parametros (Proceso)
			Berke.DG.DBTab.Proceso Proceso = new Berke.DG.DBTab.Proceso();
			Proceso.DisableConstraints();
	
			Proceso.NewRow(); 
	
			Proceso.Dat.ID			.Value = txtID_min.Text;
			Proceso.Dat.PracticaID	.Value = txtPracticaID.Text;
			Proceso.Dat.Descrip		.Value = txtDescrip.Text;
			Proceso.Dat.Abrev		.Value = txtAbrev.Text;

			Proceso.PostNewRow();
	
			Proceso.NewRow(); 
	
			Proceso.Dat.ID			.Value = txtID_max.Text;

			Proceso.PostNewRow();
	
			#endregion Asignar Parametros ( Proceso )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				Proceso =  Berke.Marcas.UIProcess.Model.Proceso.ReadList( Proceso );
				
//				#region Convertir a Link
//				for( Proceso.GoTop(); ! Proceso.EOF ; Proceso.Skip() )
//				{
//					Proceso.Edit();
//
//		
//					Proceso.Dat.Descrip.Value = HtmlGW.Redirect_Link(
//						Proceso.Dat.ID.AsString, 
//						Proceso.Dat.Descrip.AsString,
//						"ProcesoDetalle.aspx","ProcesoID" );		 						
//					Proceso.PostEdit();
//				}
//				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: ProcesoConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = Proceso.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( Proceso.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Proceso &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+Proceso.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Proceso";
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

	} // end class ProcesoConsulta
} // end namespace Berke.Marcas.WebUI.Home


