
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
	/// Summary description for VerDocMerge.
	/// </summary>
	public partial class VerDocMerge : System.Web.UI.Page
	{
		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();

		#region PageLoad

		protected void Page_Load(object sender, System.EventArgs e)
		{
			

				if(UrlParam.GetParam("mergedocid") != "")
				{
					MySession.ID = Convert.ToInt32(UrlParam.GetParam("mergedocid"));	
				}

				ejecutar();

			

			/*
				try
				{

					if( !IsPostBack )
					{	
						if(UrlParam.GetParam("mergedocid") != "")
						{
							MySession.ID = Convert.ToInt32(UrlParam.GetParam("mergedocid"));	
						}
						ejecutar();
					}

				}
				catch(Exception m)
				{
					string redirectString = "../Generic/Message.aspx" + "?page=" + m.Message + "(" + m.Source + ")";
					Response.Redirect(redirectString);
					redirectString = "";
				}
				*/
			}
		#endregion
    

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


		private void ejecutar()
		{
			ObtenerDocumento();
		}

		private void ObtenerDocumento()
		{

			#region Obtener Parametros
			int mergedocid=0;

			if( UrlParam.GetParam("mergedocid") != "") 
			{
				mergedocid = Convert.ToInt32(UrlParam.GetParam("mergedocid"));
			}

			
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;

			#endregion Obtener Parametros

			
			Berke.DG.DBTab.MergeDoc mergedoc = new Berke.DG.DBTab.MergeDoc(db); 
						
			
			mergedoc.Adapter.ReadByID(mergedocid);
			if ( mergedoc.RowCount > 0 ) 
			{
				#region Activar MS-Word
				
				Response.Clear();
				Response.Buffer = true;
				Response.ContentType = "application/vnd.ms-word";
				
				Response.AddHeader("Content-Disposition", "attachment;filename=merge.doc" );
				Response.Charset = "UTF-8";
				Response.ContentEncoding = System.Text.Encoding.UTF8;
				Response.BinaryWrite(mergedoc.Dat.Contenido.AsBinary);
				Response.End();
				#endregion Activar MS-Word	
			}
			
		}
	}



}
