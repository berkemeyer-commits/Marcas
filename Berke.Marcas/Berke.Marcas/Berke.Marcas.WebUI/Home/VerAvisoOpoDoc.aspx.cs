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
	using UIPModel = UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using System.Text;
	/// <summary>
	/// Summary description for VerAvisoOpoDoc.
	/// </summary>
	public partial class VerAvisoOpoDoc : System.Web.UI.Page
	{
		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(UrlParam.GetParam("VigilanciaDocID") != "")
			{
				MySession.ID = Convert.ToInt32(UrlParam.GetParam("VigilanciaDocID"));	
			}

			Ejecutar();
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

		private void Ejecutar()
		{
			ObtenerDocumento();
		}
	
		private void ObtenerDocumento()
		{
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.VigilanciaDoc VigilanciaDoc = new Berke.DG.DBTab.VigilanciaDoc(db);
			VigilanciaDoc.Adapter.ReadByID(MySession.ID);

			if ( VigilanciaDoc.RowCount > 0 ) 
			{
				#region Activar MS-Word
				
				Response.Clear();
				Response.Buffer = true;
				Response.ContentType = "application/vnd.ms-word";
				
				Response.AddHeader("Content-Disposition", "attachment;filename=AvisoOpo.doc" );
				Response.Charset = "UTF-8";
				Response.ContentEncoding = System.Text.Encoding.UTF8;
				Response.BinaryWrite(VigilanciaDoc.Dat.Doc.AsBinary);
				Response.End();
				#endregion Activar MS-Word	
			}
		}

	}
}
