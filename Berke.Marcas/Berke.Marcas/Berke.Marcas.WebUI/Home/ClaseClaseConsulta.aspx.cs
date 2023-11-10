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
	public partial class ClaseClaseConsulta : System.Web.UI.Page
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
		
			#region ClaseRelac DropDown
			
			//			SimpleEntryDS seClaseRelac = Berke.Marcas.UIProcess.Model.ClaseRelac.ReadForSelect();
			//			ddlClaseRelacID.Fill( seClaseRelac.Tables[0], true);

			Berke.DG.ViewTab.ListTab ltClaseRelac = Berke.Marcas.UIProcess.Model.ClaseClase.ReadForSelect();
			ddlClaseRelacID.Fill( ltClaseRelac.Table, true);
			
			#endregion ClaseRelac DropDown
		
			#region Clase DropDown
			
			//			SimpleEntryDS seClase = Berke.Marcas.UIProcess.Model.Clase.ReadForSelect();
			//			ddlClaseID.Fill( seClase.Tables[0], true);

			Berke.DG.ViewTab.ListTab ltClase = Berke.Marcas.UIProcess.Model.ClaseClase.ReadForSelect();
			ddlClaseID.Fill( ltClase.Table, true);
			
			#endregion Clase DropDown


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
						

			#region Asignar Parametros (vClaseClase)
			Berke.DG.ViewTab.vClaseClase vClaseClase = new Berke.DG.ViewTab.vClaseClase();

	
			vClaseClase.NewRow(); 
	
			vClaseClase.Dat.ClaseRelacID	.Value = ddlClaseRelacID.Value;
			vClaseClase.Dat.ClaseID	.Value = ddlClaseID.Value;
			vClaseClase.Dat.ID	.Value = txtID_min.Text;
			//vClaseClase.Dat.ClaseDescrip	.Value = txtClaseDescrip.Text;
			//vClaseClase.Dat.ClaseRelacDescrip	.Value = txtClaseRelacDescrip.Text;
			vClaseClase.Dat.Ancestro	.Value = ddlAncestro.SelectedValue;

			vClaseClase.PostNewRow();
	
			vClaseClase.NewRow(); 
	
			vClaseClase.Dat.ID	.Value = txtID_max.Text;

			vClaseClase.PostNewRow();
	
			#endregion Asignar Parametros ( vClaseClase )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vClaseClase =  Berke.Marcas.UIProcess.Model.ClaseClase.ReadList( vClaseClase );
				
//				#region Convertir a Link
////				for( vClaseClase.GoTop(); ! vClaseClase.EOF ; vClaseClase.Skip() )
////				{
////					vClaseClase.Edit();
////
////		
////					vClaseClase.Dat.ClaseDescrip.Value = HtmlGW.Redirect_Link(
////						vClaseClase.Dat.ID.AsString, 
////						vClaseClase.Dat.ClaseDescrip.AsString,
////						"ClaseClaseDetalle.aspx","ClaseClaseID" );		 						
////					vClaseClase.PostEdit();
////				}
//				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: ClaseClaseConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vClaseClase.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vClaseClase.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "ClaseClase &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vClaseClase.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "ClaseClase";
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

	} // end class ClaseClaseConsulta
} // end namespace Berke.Marcas.WebUI.Home


