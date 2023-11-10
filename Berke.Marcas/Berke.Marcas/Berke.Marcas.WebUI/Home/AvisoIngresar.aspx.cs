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
using UIPModel = Berke.Marcas.UIProcess.Model ;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for AvisoIngrear.
	/// </summary>
	public partial class AvisoIngresar : System.Web.UI.Page
	{
		#region Controles del Form
		#endregion Controles del Form

		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Destinatario DropDown
			Berke.DG.ViewTab.ListTab seDestinatario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlDestinatario.Fill( seDestinatario.Table, true);

			#endregion Destinatario DropDown

			#region Prioridad DropDown
			Berke.DG.ViewTab.ListTab sePrioridad = Berke.Marcas.UIProcess.Model.Prioridad.ReadForSelect();
			ddlPrioridad.Fill( sePrioridad.Table, true );
			#endregion Prioridad DropDown
	

			txtFechaAviso.Text = DateTime.Now.ToString("d")+ " 7:00";
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				lblMensaje.Text = "";
				AsignarValoresIniciales();
			}		
		}
		#endregion Page_Load

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

		#region Grabar
		protected void btGrabar_Click(object sender, System.EventArgs e)
		{
			string usuario = Berke.Libs.Base.Acceso.GetCurrentUser();
			#region Obtener datos de funcionario

	
			Berke.DG.ViewTab.vFuncionario remit = UIPModel.Funcionario.ReadByUserName( usuario );
//			string remitName = remit.Dat.PriNombre.AsString.Trim()+" "+ remit.Dat.SegNombre.AsString.Trim() +" "+remit.Dat.PriApellido.AsString.Trim();
			string remitName = remit.Dat.Funcionario.AsString.Trim();
			remitName = remitName.Trim()=="" ?"SGE":remitName;

			string destinatarioID			= ddlDestinatario.SelectedValue;
			Berke.DG.ViewTab.vFuncionario dest  = UIPModel.Funcionario.ReadByUserName( usuario );
			if( destinatarioID.Trim() != "" )
			{
				dest = Berke.Marcas.UIProcess.Model.Funcionario.ReadByID( Convert.ToInt32(destinatarioID ) );
			}
			#endregion

			#region Asignar Parametros (Aviso)
			string ahora = DateTime.Now.ToString("g");
//			string indicacion = ahora + " " + usuario + " -> " + remit.Dat.Usuario.AsString + " * ";			
			string indicacion = ahora + " " + remit.Dat.NombreCorto.AsString + 
							" -> " + dest.Dat.NombreCorto.AsString + " * ";			

			Berke.DG.DBTab.Aviso Aviso = new Berke.DG.DBTab.Aviso();

	
			Aviso.NewRow(); 
	
			Aviso.Dat.Leido			.Value = false;
			Aviso.Dat.FechaAviso	.Value = txtFechaAviso.Text;
			Aviso.Dat.Asunto		.Value = txtAsunto.Text;
			Aviso.Dat.Destinatario	.Value = ddlDestinatario.Value;
			Aviso.Dat.Indicaciones	.Value = indicacion + txtIndicaciones.Text;

			if( txtContenido.Text.Length > 4000 )
			{
				Aviso.Dat.Contenido.Value = txtContenido.Text.Substring(0,4000) 
					+"  ** Contenido Truncado ** ";
			}
			else
			{
				Aviso.Dat.Contenido		.Value = txtContenido.Text;
			}

			Aviso.Dat.Remitente		.Value = remit.Dat.ID.AsInt;
			Aviso.Dat.Pendiente		.Value = true;
			Aviso.Dat.FechaAlta		.Value = DateTime.Now;
			Aviso.Dat.PrioridadID	.Value = this.ddlPrioridad.SelectedValue;

			Aviso.PostNewRow();
	
			#endregion Asignar Parametros ( Aviso )

			#region Grabar en BD
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Aviso.InitAdapter( db );
		
			db.IniciarTransaccion();

			Aviso.Adapter.InsertRow();

			db.Commit();
			
			#endregion Grabar en BD

			GoBack();

		}
		#endregion

		#region GoBack
		private void GoBack()
		{
			if( litPaginaAnterior.Text.Trim() != "" )
			{
				Response.Redirect( litPaginaAnterior.Text, true );
			}
			else{
				txtAsunto.Text			= "";
				txtIndicaciones.Text	= "";
				txtContenido.Text		= "";

				lblMensaje.Text = "Grabado";
			}
		}
		#endregion GoBack

	}
}
