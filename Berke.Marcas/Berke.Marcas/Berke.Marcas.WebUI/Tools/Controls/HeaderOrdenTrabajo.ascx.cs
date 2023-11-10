namespace Berke.Marcas.WebUI.Tools.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using UIPModel = UIProcess.Model;

	using Berke.Libs.Base.Helpers;
	using Berke.Marcas.WebUI.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class HeaderOrdenTrabajo : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				MySession.Estado = UrlParam.GetParam("page");
				TableGateway ot;
				
				switch(MySession.Estado)
				{
					case "3":
						lblTramite.Text = "Registro";
						break;

					case "4":
						lblTramite.Text = "Registro";
						break;

					case "MarcasAsignadasReg":
						lblTramite.Text = "Registro";
						break;

					case "MarcasActualizar":
						lblTramite.Text = "Registro";
						break;

					case "5":
						lblTramite.Text = "Registro";
						ot = new TableGateway(MySession.ordenTrabajoDS.OrdenTrabajo);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "8":
						lblTramite.Text = "Cambio de Domicilio";
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "9":
						lblTramite.Text = "Cambio de Nombre";
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "10":
						lblTramite.Text = "Duplicado de Título";
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "11":
						lblTramite.Text = "Transferencia";					
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "12":
						lblTramite.Text = "Licencia";
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "13":
						lblTramite.Text = "Fusión";
						ot = new TableGateway(MySession.ordenTrabajoTVDS.OrdenTrabajoTV);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "14":
						lblTramite.Text = "Renovación";
						break;

					case "15":
						lblTramite.Text = "Renovación";
						break;

					case "MarcasAsignadas":
						lblTramite.Text = "Renovación";
						break;

					case "16":
						lblTramite.Text = "Renovación";
						ot = new TableGateway(MySession.ordenTrabajoDS.OrdenTrabajo);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "Idioma":
						lblTramite.Text = "Renovación";
						ot = new TableGateway(MySession.ordenTrabajoDS.OrdenTrabajo);
						lblNroOrdenTrabajo.Text = ot.AsString("NroAnio");
						break;

					case "17":
						lblTramite.Text = "Cambio de Domicilio";
						break;

					case "18":
						lblTramite.Text = "Cambio de Nombre";
						break;

					case "19":
						lblTramite.Text = "Duplicado de Título";
						break;

					case "20":
						lblTramite.Text = "Transferencia";
						break;

					case "21":
						lblTramite.Text = "Licencia";
						break;

					case "22":
						lblTramite.Text = "Fusión";
						break;

					case "23":
						lblTramite.Text = "Cambio de Domicilio";
						break;

					case "24":
						lblTramite.Text = "Cambio de Nombre";
						break;

					case "25":
						lblTramite.Text = "Duplicado de Título";
						break;

					case "26":
						lblTramite.Text = "Transferencia";
						break;

					case "27":
						lblTramite.Text = "Licencia";
						break;

					case "28":
						lblTramite.Text = "Fusión";
						break;

				}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
