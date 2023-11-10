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
	public partial class AgenteLocalConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form

		#endregion 
	
	
	

	

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
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
						

			#region Asignar Parametros (view_AgenteLocal)
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.ViewTab.vAgenteLocalDatos view_AgenteLocal = new Berke.DG.ViewTab.vAgenteLocalDatos(db);

			view_AgenteLocal.Dat.idagloc.Filter = ObjConvert.GetFilter(txtID_min.Text);
			view_AgenteLocal.Dat.Nombre .Filter = ObjConvert.GetFilter_Str(txtNombre.Text);
			view_AgenteLocal.Dat.nromatricula.Filter = ObjConvert.GetFilter(txtMatricula.Text);

			
			if ( chkNuestro.Checked ) 
			{
				string nuestro = "true";
				view_AgenteLocal.Dat.Nuestro.Filter  = ObjConvert.GetFilter(nuestro);
			}	
	
			#endregion Asignar Parametros ( view_AgenteLocal )


			


			#region Obtener datos
			int recuperados = -1;
			try 
			{
				view_AgenteLocal.Adapter.ReadAll();
				view_AgenteLocal.Dat.Nombre.Order = 1;
			
				view_AgenteLocal.Sort();

				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("AgenteLocalConsulta", exep );
			}
			
			
			
			
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			try
			{
				dgResult.DataSource = view_AgenteLocal.Table;
				dgResult.DataBind();
			} 
			catch( Exception exep ) 
			{
				throw new Exception("AgenteLocalConsulta", exep );
			}

			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( view_AgenteLocal.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "AgentesLocales &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+view_AgenteLocal.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "AgentesLocales";
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

	
	} // end class AgenteLocalConsulta
} // end namespace Berke.Marcas.WebUI.Home


