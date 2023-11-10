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
	public partial class DocumentoTipoConsulta : System.Web.UI.Page
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
						
			#region Asignar Parametros (DocumentoTipo)
				Berke.DG.DBTab.DocumentoTipo DocumentoTipo = new Berke.DG.DBTab.DocumentoTipo();
				DocumentoTipo.DisableConstraints();
				DocumentoTipo.NewRow(); 
				
				DocumentoTipo.Dat.ID.Value				= txtID_min.Text;
				DocumentoTipo.Dat.Descrip.Value			= txtDescrip.Text;
				DocumentoTipo.Dat.Abrev.Value			= txtAbrev.Text;

			

				DocumentoTipo.PostNewRow();
				DocumentoTipo.NewRow ();
				DocumentoTipo.Dat.ID.Value				= txtID_max.Text;
				DocumentoTipo.PostNewRow();	

			#endregion Asignar Parametros ( DocumentoTipo )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				DocumentoTipo =  Berke.Marcas.UIProcess.Model.DocumentoTipo.ReadList( DocumentoTipo );
			
				/*
				#region Convertir a Link
				for( DocumentoTipo.GoTop(); ! DocumentoTipo.EOF ; DocumentoTipo.Skip() )
				{
					DocumentoTipo.Edit();
		
					DocumentoTipo.Dat.Descrip.Value = HtmlGW.Redirect_Link(
					DocumentoTipo.Dat.ID.AsString,
					DocumentoTipo.Dat.Descrip.AsString,  
					DocumentoTipo.Dat.Abrev.AsString,
					"DocumentoTipoDetalle.aspx","DocumentoTipoID" );		 						
					DocumentoTipo.PostEdit();
				}
				#endregion  Convertir a Link
				*/
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = DocumentoTipo.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( DocumentoTipo.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "DocumentoTipo &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+DocumentoTipo.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "DocumentoTipo";
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

	} // end class DocumentoTipoConsulta
} // end namespace Berke.Marcas.WebUI.Home


