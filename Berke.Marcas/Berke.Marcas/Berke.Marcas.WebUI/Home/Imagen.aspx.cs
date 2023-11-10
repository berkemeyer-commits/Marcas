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
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;

	/// <summary>
	/// Summary description for Imagen.
	/// </summary>
	public partial class Imagen : System.Web.UI.Page
	{
		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;
			//Berke.DG.DBTab.Logotipo logo = new Berke.DG.DBTab.Logotipo( db );
			if (UrlParam.GetParam("logotipoID") != "") {
				Berke.DG.DBTab.Logotipo logo = new Berke.DG.DBTab.Logotipo( db );
				logo.Adapter.ReadByID(Convert.ToInt32(UrlParam.GetParam("logotipoID")));
				Response.BinaryWrite(logo.Dat.Imagen.AsBinary);				
			}
			if (UrlParam.GetParam("TarjetaID") != "")
			{
				Berke.DG.DBTab.Tarjeta_Atencion tAtencion = new Berke.DG.DBTab.Tarjeta_Atencion( db );
				tAtencion.Adapter.ReadByID(Convert.ToInt32(UrlParam.GetParam("TarjetaID")));
				Response.BinaryWrite(tAtencion.Dat.Imagen.AsBinary);
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
	}
}
