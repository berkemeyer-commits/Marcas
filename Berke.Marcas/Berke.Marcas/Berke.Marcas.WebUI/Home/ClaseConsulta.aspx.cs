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
	public partial class ClaseConsulta: System.Web.UI.Page
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
			#region NizaEdicionID DropDown
				Berke.DG.ViewTab.ListTab seNizaEdicionID = Berke.Marcas.UIProcess.Model.NizaEdicion.ReadForSelect();
				ddlNizaEdicionID.Fill( seNizaEdicionID.Table, true);
			#endregion NizaEdicionID DropDown
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
						
			#region Asignar Parametros (vClase)
			Berke.DG.ViewTab.vClase vClase = new Berke.DG.ViewTab.vClase();

	
			vClase.NewRow(); 
	
			vClase.Dat.NizaAbrev		.Value = txtNizaAbrev.Text;
			vClase.Dat.NizaEdicionID	.Value = ddlNizaEdicionID.SelectedValue;
			vClase.Dat.ID				.Value = txtID_min.Text;
			vClase.Dat.Nro				.Value = txtNro.Text;
			vClase.Dat.Descrip			.Value = txtDescrip.Text;

			vClase.PostNewRow();
	
			vClase.NewRow(); 
	
			vClase.Dat.ID	.Value = txtID_max.Text;

			vClase.PostNewRow();
	
			#endregion Asignar Parametros ( vClase )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vClase =  Berke.Marcas.UIProcess.Model.Clase.ReadList( vClase );
				
				#region Convertir a Link
				//				for( vClase.GoTop(); ! vClase.EOF ; vClase.Skip() )
				//				{
				//					vClase.Edit();
				//
				//		
				//					vClase.Dat.Nro.Value = HtmlGW.Redirect_Link(
				//					vClase.Dat.ID.AsString, 
				//					vClase.Dat.Nro.AsString,
				//					"ClaseDetalle.aspx","ClaseID" );		 						
				//					vClase.PostEdit();
				//				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep )
			{
				throw new Exception(" ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vClase.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vClase.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Clases &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vClase.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Clases";
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

	} // end class Clase
} // end namespace Berke.Marcas.WebUI.Home


