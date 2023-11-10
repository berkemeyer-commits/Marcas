using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using Berke.Libs.Base;

using  Berke.Libs.WebBase.Helpers;

namespace Berke.Marcas.WebUI
{
	/// <summary>
	/// Summary description for AvisoOpoTools.
	/// </summary>
	public class AvisoOpoTools
	{
		public AvisoOpoTools()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Ver AvisoOpo
		public string getAvisoOpoTable(Berke.Libs.Base.Helpers.AccesoDB db, int MarcaID)
		{
			string boxPattern		= "<div id='{0}'><table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('{1}');\"><img id='img_{1}' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> {3} </td><td width='10' class='td_button'><a  onclick=\"closeDiv('{0}');\"> <img src=\"../Tools/imx/close.gif\" border=\"0\"></a></td></tr><tr><td><div id='{1}'> {2}</div></td></tr></table></div>";		
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));

			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Tipo Aviso");
			tab.AddCell("Fecha Envío");
			tab.AddCell("Usuario");
			tab.AddCell("Modo Envío");
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			
			#region Detalle
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));

			Berke.DG.ViewTab.vViewAvisoOpo view = new Berke.DG.ViewTab.vViewAvisoOpo(db);
			view.ClearFilter();
			view.Dat.MarcaBaseID.Filter = MarcaID;
			view.Dat.NroAviso.Order = 1;
			view.Dat.FecEnvio.Order = 2;
			view.Adapter.ReadAll();

			if (view.RowCount == 0)
			{
				return "";
			}

			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				
				tab.BeginRow();
				tab.AddCell(chkSpc(HtmlGW.Redirect_Link(view.Dat.VigilanciaDocID.AsString, view.Dat.NombreTipoAviso.AsString,
								   "VerAvisoOpoDoc.aspx", "VigilanciaDocID")));
				tab.AddCell(chkSpc(view.Dat.FecEnvio.AsDateTime.ToString()));
				tab.AddCell(chkSpc(view.Dat.Nick.AsString));
				tab.AddCell(chkSpc(view.Dat.ModoEnvio.AsString));

				
				tab.EndRow();
			}

			#endregion Detalle


			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			return  string.Format(boxPattern, "avisoopoBox", "divAvisoOpo", tab.Html(), "Avisos de Oposición");

		}


		#endregion Ver AvisoOpo


		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}
	}
}
