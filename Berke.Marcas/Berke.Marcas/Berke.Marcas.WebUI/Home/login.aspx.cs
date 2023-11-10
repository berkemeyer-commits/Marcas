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

namespace Berke.Marcas.WebUI.Home
{
	using Berke.Libs.WebBase.Helpers;

	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();

			try
			{
				//const int charsForTab = 10;
				//const string BR = "<BR>";

				
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

				lblMenu.Text = Berke.Marcas.UIProcess.Model.Menu.ReadAsHTMLString();

				#region Verificar Notificaciones
				
				Berke.DG.ViewTab.vFuncionario fun = Berke.Marcas.UIProcess.Model.Funcionario.ReadByUserName(Berke.Libs.Base.Acceso.GetCurrentUser());
				#region Asignar Parametros (vAviso)
				Berke.DG.ViewTab.vAviso vAviso = new Berke.DG.ViewTab.vAviso();
	
				vAviso.NewRow(); 
				vAviso.Dat.Pendiente	.Value = true;
				vAviso.Dat.Destinatario	.Value = fun.Dat.ID.AsInt;
				vAviso.Dat.FechaAviso	.Value = "01/01/1980";
				vAviso.PostNewRow();
	
				vAviso.NewRow(); 
				vAviso.Dat.FechaAviso	.Value = DateTime.Now.ToString("g");
				vAviso.PostNewRow();
	
				#endregion Asignar Parametros ( vAviso )
				lnkNotif.Visible = false;
				vAviso =  Berke.Marcas.UIProcess.Model.Aviso.ReadList( vAviso );
				if( vAviso.RowCount > 0)
				{
					//					Response.Redirect("../Home/AvisoConsulta.aspx", true );
					lnkNotif.Visible = true;
				}
				#endregion Verificar Notificaciones

				//this.lblAviso.Text = @"";
				/*
								this.lblAviso.Text = @"<font size=-1><i>Estimado Usuario <br><br>

				Estamos probando el módulo de Control de Accesos.<br>
				En el nuevo menú, Ud. debería ver todas las tareas que realiza habitualmente.<br><br>
				Si detecta que que falta algún item en el menú, por favor, contacte con Laura Wu o envie su requerimiento via mail a Implementacion<br><br>

				Muchas Gracias<br><br>

					Equipo de Desarrollo</i></font>";

				*/


			}
			catch(Exception ex )
			{
				string mensaje = ex.Message;
				mensaje = "";
			}
			finally {
				db.CerrarConexion();
			}
		
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
        #endregion

    }
}
