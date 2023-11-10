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
	/// <summary>
	/// Summary description for InstruccionTipo.
	/// </summary>
	public partial class InstruccionTipo : System.Web.UI.Page
	{
		#region Controles del Web Form

		#endregion 
	
		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if( !IsPostBack )
			{
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.DBTab.InstruccionTipo instruccion = new Berke.DG.DBTab.InstruccionTipo( db );
			
				int recuperados = -1;
				instruccion.Adapter.ReadAll(); 
				recuperados = instruccion.RowCount;

				this.dgResult.DataSource = instruccion.Table;
				dgResult.DataBind();

				db.CerrarConexion();
				
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


	}// endclass
}
