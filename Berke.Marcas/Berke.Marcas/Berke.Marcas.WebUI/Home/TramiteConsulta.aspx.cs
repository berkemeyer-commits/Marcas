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
	public partial class TramiteConsulta : System.Web.UI.Page
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
		
			#region Proceso DropDown
			
//			SimpleEntryDS seProceso = Berke.Marcas.UIProcess.Model.Proceso.ReadForSelect();
//			ddlProcesoID.Fill( seProceso.Tables[0], true);

			Berke.DG.ViewTab.ListTab ltProceso = Berke.Marcas.UIProcess.Model.Proceso.ReadForSelect();
			ddlProcesoID.Fill( ltProceso.Table, true);
			
			#endregion Proceso DropDown



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
						


			#region Asignar Parametros (Tramite)
			Berke.DG.DBTab.Tramite Tramite = new Berke.DG.DBTab.Tramite();

	
			Tramite.NewRow(); 
			Tramite.DisableConstraints();
	
			Tramite.Dat.ProcesoID	.Value = ddlProcesoID.Value;
			Tramite.Dat.ID	.Value = txtID_min.Text;
			Tramite.Dat.Descrip	.Value = txtDescrip.Text;
			Tramite.Dat.Abrev	.Value = txtAbrev.Text;

			Tramite.PostNewRow();
	
			Tramite.NewRow(); 
	
			Tramite.Dat.ID	.Value = txtID_max.Text;

			Tramite.PostNewRow();
	
			#endregion Asignar Parametros ( Tramite )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				Tramite =  Berke.Marcas.UIProcess.Model.Tramite.ReadList( Tramite );
				
//				#region Convertir a Link
//				for( Tramite.GoTop(); ! Tramite.EOF ; Tramite.Skip() )
//				{
//					Tramite.Edit();
//
//		
//					Tramite.Dat.Descrip.Value = HtmlGW.Redirect_Link(
//						Tramite.Dat.ID.AsString, 
//						Tramite.Dat.Descrip.AsString,
//						"TramiteDetalle.aspx","TramiteID" );		 						
//					Tramite.PostEdit();
//				}
//				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: TramiteConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = Tramite.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( Tramite.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Tramites &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+Tramite.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Tramites";
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

	} // end class TramiteConsulta
} // end namespace Berke.Marcas.WebUI.Home


