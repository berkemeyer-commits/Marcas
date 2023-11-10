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
	public partial class FuncionarioConsulta : System.Web.UI.Page
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
						


			#region Asignar Parametros (vFuncionario)
			Berke.DG.ViewTab.vFuncionario vFuncionario = new Berke.DG.ViewTab.vFuncionario();

	
			vFuncionario.NewRow(); 

			vFuncionario.Dat.ID			.Value = txtID_min.Text;
			vFuncionario.Dat.Usuario	.Value = txtUsuario.Text;
			vFuncionario.Dat.NombreCorto.Value = txtPriNombre.Text;

//			vFuncionario.Dat.SegNombre	.Value = txtSegNombre.Text;
//			vFuncionario.Dat.PriApellido.Value = txtPriApellido.Text;
//			vFuncionario.Dat.SegApellido.Value = txtSegApellido.Text;

			vFuncionario.PostNewRow();
	
			vFuncionario.NewRow(); 
	
			vFuncionario.Dat.ID	.Value = txtID_max.Text;

			vFuncionario.PostNewRow();
	
			#endregion Asignar Parametros ( vFuncionario )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vFuncionario =  Berke.Marcas.UIProcess.Model.Funcionario.ReadList( vFuncionario );
				
//				#region Convertir a Link
//				for( vFuncionario.GoTop(); ! vFuncionario.EOF ; vFuncionario.Skip() )
//				{
//					vFuncionario.Edit();
//
//		
//					vFuncionario.Dat.?CampoDescrip?.Value = HtmlGW.Redirect_Link(
//						vFuncionario.Dat.ID.AsString, 
//						vFuncionario.Dat.?CampoDescrip?.AsString,
//						"FuncionarioDatos.aspx","FuncionarioId" );		 						
//						vFuncionario.PostEdit();
//				}
//				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}//Aqui
			catch( Exception exep ) 
			{
				throw new Exception("ExpedienteConsulta", exep );
			} // a Aqui

			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vFuncionario.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vFuncionario.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Funcionarios &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vFuncionario.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Funcionarios";
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

	} // end class FuncionarioConsulta
} // end namespace Berke.Marcas.WebUI.Home


