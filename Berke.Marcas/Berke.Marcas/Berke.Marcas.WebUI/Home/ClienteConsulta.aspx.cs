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
	public partial class ClienteConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form

		protected System.Web.UI.WebControls.Label lblDenominacion;
		protected System.Web.UI.WebControls.TextBox txtDenominacion;
		protected System.Web.UI.WebControls.Label lblConcepto;
		protected System.Web.UI.WebControls.TextBox txtConcepto;
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
						
			#region Asignar Parametros 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.ViewTab.vClienteDatos view_ClienteDatos = new Berke.DG.ViewTab.vClienteDatos(db);


			
			view_ClienteDatos.Dat.ID.Filter     = ObjConvert.GetFilter(txtID_min.Text);
			view_ClienteDatos.Dat.Nombre.Filter = ObjConvert.GetFilter_Str(txtNombre.Text);
			view_ClienteDatos.Dat.pais  .Filter = ObjConvert.GetFilter_Str(txtPais.Text);
			view_ClienteDatos.Dat.Correo.Filter = ObjConvert.GetFilter_Str(txtCorreo.Text);
			view_ClienteDatos.Dat.nomciudad.Filter = ObjConvert.GetFilter_Str(txtCiudad.Text);
            view_ClienteDatos.Dat.Obs.Filter = ObjConvert.GetFilter_Str(txtObservacion.Text);
			
			if ( chkActivo.Checked ) 
			{
                string activos = "true";
				view_ClienteDatos.Dat.Activo.Filter     = ObjConvert.GetFilter(activos);
			}
		    

			if ( chkInubicable.Checked ) 
			{
				string inubicable = "true";
				view_ClienteDatos.Dat.Inubicable.Filter = ObjConvert.GetFilter(inubicable);
			}

			

			
			if ( chkMultiple.Checked ) 
			{
				string multiple =  "true";
			    view_ClienteDatos.Dat.Multiple.Filter   = ObjConvert.GetFilter(multiple);
			}

			

			view_ClienteDatos.Adapter.ReadAll();

	
			#endregion Asignar Parametros ( vClienteDatos )

			#region Obtener datos
			int recuperados = -1;
			try 
			{

				#region Convertir a Link
				for( view_ClienteDatos.GoTop(); ! view_ClienteDatos.EOF ; view_ClienteDatos.Skip() )
				{
					view_ClienteDatos.Edit();

					view_ClienteDatos.Dat.Nombre.Value = HtmlGW.Redirect_Link(
					view_ClienteDatos.Dat.ID.AsString, 
					view_ClienteDatos.Dat.Nombre.AsString,
						"ClienteDatos.aspx","ClienteID" );		 						
					view_ClienteDatos.PostEdit();
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

			if (!view_ClienteDatos.Table.Columns.Contains("clientes_relacionados"))
			{
				view_ClienteDatos.Table.Columns.Add("clientes_relacionados");
			}

			Berke.DG.DBTab.ClienteXTramite cliXtram = new Berke.DG.DBTab.ClienteXTramite(db);
			Berke.DG.DBTab.Cliente cli = new Berke.DG.DBTab.Cliente(db);
			string strClientes = "";	
			foreach(DataRow dr in view_ClienteDatos.Table.Rows)
			{
				cliXtram.ClearFilter();
				cliXtram.Dat.ClienteMultipleID.Filter = Convert.ToInt32(dr["ID"].ToString());
				cliXtram.Adapter.ReadAll();
				if (cliXtram.RowCount > 0)
				{
					for(cliXtram.GoTop(); !cliXtram.EOF; cliXtram.Skip())
					{
						if (strClientes != "")
						{
							strClientes += "<br>";// Berke.Libs.Boletin.Libs.Utils.ENTER;
						}
						cli.ClearFilter();
						cli.Adapter.ReadByID(cliXtram.Dat.ClienteID.AsInt);
						strClientes += @"<A href='ClienteDatos.aspx?ClienteID=" + cliXtram.Dat.ClienteID.AsString + "'>" + cli.Dat.Nombre.AsString + "(" + cliXtram.Dat.ClienteID.AsString + ")"; 
					}
				}
				else
				{
					strClientes = "";
				}

				dr["clientes_relacionados"] = strClientes;
				strClientes = "";
			}

			dgResult.DataSource = view_ClienteDatos.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( view_ClienteDatos.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Clientes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+view_ClienteDatos.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Clientes";
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


	} 
} 


