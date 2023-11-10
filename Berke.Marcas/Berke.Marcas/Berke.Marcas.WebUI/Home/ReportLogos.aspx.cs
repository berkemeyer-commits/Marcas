using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Marcas.WebUI.Helpers;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using Berke.Marcas.WebUI;

namespace Berke.Marcas.WebUI.Home
{
    public partial class ReportLogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Set the processing mode for the ReportViewer to Local  
            this.rptMarcasLogos.ProcessingMode = ProcessingMode.Local;
            this.rptMarcasLogos.ShowPrintButton = false;
            //Berke.DG.ViewTab.vExpeMarca vExpeMarca = new Berke.DG.ViewTab.vExpeMarca((DataTable)Session["MarcasLogosDS"]);
            DataTable dtMarcasLogos = (DataTable)Session["MarcasLogosDS"];

            Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= Helpers.MyApplication.CurrentDBName;
			db.ServerName	= Helpers.MyApplication.CurrentServerName;
            Berke.DG.DBTab.Logotipo lg = new Berke.DG.DBTab.Logotipo(db);

            Regex rx = new Regex(@">(.*?)</A>");

            if (dtMarcasLogos.Columns.IndexOf("Logotipo") < 0)
            {
                DataColumn colLogo = new DataColumn("Logotipo", System.Type.GetType("System.Byte[]"));
                dtMarcasLogos.Columns.Add(colLogo);

                foreach (DataRow rw in dtMarcasLogos.Rows)
                {
                    Match match = rx.Match(rw["Denominacion"].ToString());
                    rw["Denominacion"] = match.Success ? Regex.Replace(match.Groups[1].Value, "<.*?>", "") : "--";

                    if (rw["LogotipoID"].ToString() != string.Empty)
                    {
                        lg.ClearFilter();
                        lg.Adapter.ReadByID((int)rw["LogotipoID"]);
                        rw["Logotipo"] = lg.Dat.Imagen.AsBinary;
                    }
                }
            }
            
            ReportDataSource rpt = new ReportDataSource("DataSet1", dtMarcasLogos);
            this.rptMarcasLogos.LocalReport.DataSources.Add(rpt);
            this.rptMarcasLogos.LocalReport.ReportPath = "Reports/ReporteMarcasLogos.rdlc";
            this.rptMarcasLogos.LocalReport.Refresh();
        }
    }
        protected void rptMarcasLogos_Load(object sender, EventArgs e)
        {
            string exportOption = "PDF";
            //RenderingExtension extension = this.rptMarcasLogos.LocalReport
            //                                .ListRenderingExtensions()
            //                                .ToList()
            //                                .Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));

            RenderingExtension extension = null;

            foreach(RenderingExtension rExt in this.rptMarcasLogos.LocalReport.ListRenderingExtensions())
            {
                if (rExt.Name == exportOption)
                    extension = rExt;
            }

            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension, false);
            }
        }
}
}