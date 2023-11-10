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
using Berke.Marcas.WebUI.Tools.Helpers;

namespace Berke.Marcas.WebUI.Home
{
    public partial class ReportUltTramMarcas : System.Web.UI.Page
    {
        #region Constantes
        private const string LIST = "list";
        private const string TABLE = "table";
        #endregion Constantes
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string reptype = UrlParam.GetParam("reptype");
                // Set the processing mode for the ReportViewer to Local  
                this.rptUltTramMarcas.ProcessingMode = ProcessingMode.Local;
                this.rptUltTramMarcas.ShowPrintButton = false;
                //Berke.DG.ViewTab.vExpeMarca vExpeMarca = new Berke.DG.ViewTab.vExpeMarca((DataTable)Session["MarcasLogosDS"]);
                DataTable dtUltimoTramiteMarcasDS = (DataTable)Session["UltimoTramiteMarcasDS"];
                
                Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
                db.DataBaseName = Helpers.MyApplication.CurrentDBName;
                db.ServerName = Helpers.MyApplication.CurrentServerName;
                Berke.DG.DBTab.Logotipo lg = new Berke.DG.DBTab.Logotipo(db);

                //Regex rx = new Regex(@">(.*?)</A>");

                if (dtUltimoTramiteMarcasDS.Columns.IndexOf("Logo") < 0)
                {
                    DataColumn colLogo = new DataColumn("Logo", System.Type.GetType("System.Byte[]"));
                    dtUltimoTramiteMarcasDS.Columns.Add(colLogo);

                    foreach (DataRow rw in dtUltimoTramiteMarcasDS.Rows)
                    {
                        if (rw["LogotipoID"].ToString() != string.Empty)
                        {
                            lg.ClearFilter();
                            lg.Adapter.ReadByID((int)rw["LogotipoID"]);
                            rw["Logo"] = lg.Dat.Imagen.AsBinary;
                        }
                    }
                }

                ReportDataSource rpt = new ReportDataSource("DataSet1", dtUltimoTramiteMarcasDS);
                this.rptUltTramMarcas.LocalReport.DataSources.Add(rpt);
                this.rptUltTramMarcas.LocalReport.ReportPath = reptype == LIST ? "Reports/ReportUltTramMarcas.rdlc" : "Reports/ReportUltTramMarcasTabla.rdlc";
                this.rptUltTramMarcas.LocalReport.Refresh();
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

            foreach(RenderingExtension rExt in this.rptUltTramMarcas.LocalReport.ListRenderingExtensions())
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