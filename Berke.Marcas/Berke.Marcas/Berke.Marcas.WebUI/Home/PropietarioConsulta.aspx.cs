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
	public partial class PropietarioConsulta : System.Web.UI.Page
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

		
		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Propietarios";
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

		protected void btBuscar_Click_1(object sender, System.EventArgs e)
		{
						
			#region Asignar Parametros 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.ViewTab.vPropietarioDatos view_PropietarioDatos = new Berke.DG.ViewTab.vPropietarioDatos(db);


			view_PropietarioDatos.Dat.ID.Filter     = ObjConvert.GetFilter(txtID_min.Text);
			view_PropietarioDatos.Dat.Nombre.Filter = ObjConvert.GetFilter_Str(txtDescrip.Text);
			view_PropietarioDatos.Dat.pais  .Filter = ObjConvert.GetFilter_Str(txtPais.Text);
			view_PropietarioDatos.Dat.nomciudad.Filter = ObjConvert.GetFilter_Str(txtCiudad.Text);
			view_PropietarioDatos.Dat.grupo.Filter = ObjConvert.GetFilter_Str(txtGrupo.Text);
			view_PropietarioDatos.Adapter.ReadAll();
			

	
			#endregion Asignar Parametros ( vPropietarioDatos )

			#region Obtener datos
			int recuperados = -1;
			try 
			{

				#region Convertir a Link
				for( view_PropietarioDatos.GoTop(); ! view_PropietarioDatos.EOF ; view_PropietarioDatos.Skip() )
				{
					view_PropietarioDatos.Edit();

					view_PropietarioDatos.Dat.Nombre.Value = HtmlGW.Redirect_Link(
						view_PropietarioDatos.Dat.ID.AsString, 
						view_PropietarioDatos.Dat.Nombre.AsString,
						"PropietarioDatos.aspx","PropietarioID" );		 						
					view_PropietarioDatos.PostEdit();
				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			} 
			catch( Exception exep ) 
			{
				throw new Exception("ClienteConsulta", exep );
			} 

			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = view_PropietarioDatos.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( view_PropietarioDatos.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Clientes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+view_PropietarioDatos.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion

		}

		#region Carga de Combo




		#endregion Carga de Combo

	} // end class PropietarioConsulta
} // end namespace Berke.Marcas.WebUI.Home


