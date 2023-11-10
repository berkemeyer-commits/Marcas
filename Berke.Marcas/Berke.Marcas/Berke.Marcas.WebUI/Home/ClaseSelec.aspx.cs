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
using Berke.Libs.Base;



namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for ClaseSelec.
	/// </summary>
	public partial class ClaseSelec : System.Web.UI.Page
	{

		protected Berke.Libs.WebBase.Helpers.GridGW dgwClases;

	
		

		
		
	
		#region Variables Globales

			Berke.DG.ViewTab.vClase vClase = new Berke.DG.ViewTab.vClase();
			string w_campo= "";
		#endregion

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{


			if( UrlParam.GetParam("campo") != "") 
			{
				w_campo = UrlParam.GetParam("campo");
			}

			string scriptString = "<script language='javascript'> function pasaValores( tipos ) {";
			scriptString += " window.opener.document.Form1. " + w_campo + ".value = tipos ; ";
			scriptString += " window.opener.focus()  ; ";
			scriptString += " window.close(); }" ;
			scriptString += "</script>";

			Response.Write( scriptString );


			if( !IsPostBack )
			{

				AsignarValoresIniciales();
				MostrarPanel_Busqueda();
				obtenerRegistros();

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


		private void obtenerRegistros(){

			#region Asignar Parametros (vClase)


			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			vClase.InitAdapter( db );

			
			vClase.ClearFilter();
			vClase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
			

	
			#endregion Asignar Parametros ( vClase )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vClase.Adapter.ReadAll();
				
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
		#region Busqueda de registros 
		private void btBuscar_Click(object sender, System.EventArgs e)
		{
		
						


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


		#region Cerrar

		protected void btnCerrar_Click(object sender, System.EventArgs e)
		{

			#region Retornar Clases

			CheckBox ch;

			string tipos ="";
			foreach( DataGridItem item in dgResult.Items ) 
			{
				ch = (CheckBox)item.FindControl("cbSel");
			
			
				if ( ch.Checked )
				{
					Label bNro=(Label)item.FindControl("Nro");
					tipos= tipos + bNro.Text + ",";
				
				}

			}

			
			if ( tipos.Length > 1) {

			    tipos = tipos.Substring(0,tipos.Length-1);
			}
			string scriptString = "<script language='javascript'>pasaValores('" + tipos + "') </script>";
			Page.RegisterClientScriptBlock("Prueba",scriptString);

		#endregion Retornar Clases
		
		}

		#endregion Cerrar


		#region Marcar/Desmarcar
		protected void btnMarcar_Click(object sender, System.EventArgs e)
		{
			CheckBox ch;
			foreach( DataGridItem item in dgResult.Items ) 
			{
				ch = (CheckBox)item.FindControl("cbSel");

				if ( ch.Checked ) 
				{
					ch.Checked = false;
				} 
				else 
				{
					ch.Checked = true; 
				}
			}

		}
		#endregion 


       
		
	}
}
