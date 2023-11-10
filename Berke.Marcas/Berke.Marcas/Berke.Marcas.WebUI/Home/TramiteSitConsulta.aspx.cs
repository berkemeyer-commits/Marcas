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
	public partial class TramiteSitConsulta : System.Web.UI.Page
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
			#region Llenar DropDpwn de Tramites
//			SimpleEntryDS se = Berke.Entidades.UIProcess.Model.Tramite.ReadForSelect( (int) Const.Proceso.MARCAS );
			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);
			#endregion
			
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
						


			#region Asignar Parametros (vTramiteSit)
			Berke.DG.ViewTab.vTramiteSit vTramiteSit = new Berke.DG.ViewTab.vTramiteSit();

	
			vTramiteSit.NewRow(); 
	
			vTramiteSit.Dat.TramiteID	.Value = ddlTramiteID.Value;
			vTramiteSit.Dat.ID	.Value = txtID_min.Text;
			vTramiteSit.Dat.Descrip	.Value = Berke.Libs.Base.ObjConvert.GetSqlPattern( txtDescrip.Text );
			vTramiteSit.Dat.Vigente	.Value = ddlVigente.SelectedValue;
			vTramiteSit.Dat.Automatico	.Value = ddlAutomatico.SelectedValue;

			vTramiteSit.PostNewRow();
	
			vTramiteSit.NewRow(); 
	
			vTramiteSit.Dat.ID	.Value = txtID_max.Text;

			vTramiteSit.PostNewRow();
	
			#endregion Asignar Parametros ( vTramiteSit )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vTramiteSit =  Berke.Marcas.UIProcess.Model.TramiteSit.ReadList( vTramiteSit );
				
				#region Convertir a Link
				for( vTramiteSit.GoTop(); ! vTramiteSit.EOF ; vTramiteSit.Skip() )
				{
					vTramiteSit.Edit();

		
					vTramiteSit.Dat.Descrip.Value = HtmlGW.Redirect_Link(
						vTramiteSit.Dat.ID.AsString, 
						vTramiteSit.Dat.Descrip.AsString,
						"TramiteSitDetalle.aspx","TramiteSitID" );		 						
					vTramiteSit.PostEdit();
				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: TramiteSitConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vTramiteSit.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vTramiteSit.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "TramiteSit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vTramiteSit.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "TramiteSit";
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

	} // end class TramiteSitConsulta
} // end namespace Berke.Marcas.WebUI.Home


