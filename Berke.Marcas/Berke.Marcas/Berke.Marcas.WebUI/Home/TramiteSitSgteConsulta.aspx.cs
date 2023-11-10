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
	using UIPModel = UIProcess.Model;

	public partial class SituacionSiguienteConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form



		protected System.Web.UI.WebControls.Label lblid_min;
		protected System.Web.UI.WebControls.TextBox txtid_min;

		protected System.Web.UI.WebControls.Label lblT_Orig;
		protected System.Web.UI.WebControls.TextBox txtT_Orig;

		protected System.Web.UI.WebControls.Label lblOrigen;
		protected System.Web.UI.WebControls.TextBox txtOrigen;

		protected System.Web.UI.WebControls.Label lblDestino;
		protected System.Web.UI.WebControls.TextBox txtDestino;

		protected System.Web.UI.WebControls.Label lblid_max;
		protected System.Web.UI.WebControls.TextBox txtid_max;



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
//			ddlTramite.Fill( se.Tables[0], true);
			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramite.Fill( lst.Table, true);
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
					
			#region Asignar Parametros (vSituacionSigte)
			Berke.DG.ViewTab.vSituacionSigte vSituacionSigte = new Berke.DG.ViewTab.vSituacionSigte();

			vSituacionSigte.NewRow(); 
	
			vSituacionSigte.Dat.TramiteSitID	.Value = ddlTramiteSit.SelectedValue;
			vSituacionSigte.Dat.TramiteSitSgteID	.Value = ddlTramiteSitSgte.SelectedValue;
			vSituacionSigte.Dat.TramiteID	.Value = ddlTramite.SelectedValue;
		
			vSituacionSigte.PostNewRow();
	
			vSituacionSigte.NewRow(); 
	
			vSituacionSigte.PostNewRow();
	
			#endregion Asignar Parametros ( vSituacionSigte )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vSituacionSigte =  Berke.Marcas.UIProcess.Model.TramiteSitSgte.ReadList( vSituacionSigte );
				
				#region eliminar duplicados
				int antID = -123;
				for( vSituacionSigte.GoTop(); ! vSituacionSigte.EOF ; vSituacionSigte.Skip() ){
					if( vSituacionSigte.Dat.id.AsInt  == antID )
					{
						vSituacionSigte.Delete();
					}
					else{
						antID = vSituacionSigte.Dat.id.AsInt;
					}
				}
				vSituacionSigte.AcceptAllChanges();
				#endregion 

				#region Convertir a Link
//				for( vSituacionSigte.GoTop(); ! vSituacionSigte.EOF ; vSituacionSigte.Skip() )
//				{
//					vSituacionSigte.Edit();
//
//		
//					vSituacionSigte.Dat.T_Orig.Value = HtmlGW.Redirect_Link(
//						vSituacionSigte.Dat.id.AsString, 
//						vSituacionSigte.Dat.T_Orig.AsString,
//						"SituacionSiguienteDetalle.aspx","SituacionSiguienteID" );		 						
//					vSituacionSigte.PostEdit();
//				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: SituacionSiguienteConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vSituacionSigte.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vSituacionSigte.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "SituacionSiguiente &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vSituacionSigte.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "SituacionSiguiente";
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

		protected void ddlTramite_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramite.Value) );
			this.ddlTramiteSit.Fill( situacion.Tables[0], true );
			this.ddlTramiteSitSgte.Fill( situacion.Tables[0], true );

		}

		#region Carga de Combo




		#endregion Carga de Combo

	} // end class SituacionSiguienteConsulta
} // end namespace Berke.Marcas.WebUI.Home


