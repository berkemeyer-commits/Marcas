
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
	/// Summary description for MergeTools.
	/// </summary>
	public class MergeTools
	{
		public MergeTools()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Ver Merge
		public string obtMergeTable(Berke.Libs.Base.Helpers.AccesoDB db, int ExpeID) 
		{
		string boxPattern		= "<div id='{0}'><table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('{1}');\"><img id='img_{1}' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> {3} </td><td width='10' class='td_button'><a  onclick=\"closeDiv('{0}');\"> <img src=\"../Tools/imx/close.gif\" border=\"0\"></a></td></tr><tr><td><div id='{1}'> {2}</div></td></tr></table></div>";		
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			//Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			//db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			//db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Merge");
			tab.AddCell("Fecha Generacion");
			tab.AddCell("Usuario");
			tab.AddCell("Obs.");
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));		
			Berke.DG.ViewTab.vVerMerge view = new Berke.DG.ViewTab.vVerMerge(db);
			view.Dat.expedienteid.Filter = ExpeID;
			view.Dat.generado.Filter = true;
		
			
			view.Dat.mergeid.Filter = ObjConvert.GetFilter  ( "1,2,3,4,5,7");  //omitir Presupuesto

			view.Dat.fechagen.Order = 1;
			view.Adapter.ReadAll();

			Berke.DG.ViewTab.vVerMergeAvisos viewAvisos = new Berke.DG.ViewTab.vVerMergeAvisos(db);
			viewAvisos.Adapter.SetDefaultWhere(" cab.enviofecha is not null");

			viewAvisos.Dat.expedienteid.Filter = ExpeID;
			viewAvisos.Dat.fechagen.Order = 1;
			viewAvisos.Adapter.ReadAll();
			viewAvisos.Adapter.SetDefaultWhere("");

			if(( viewAvisos.RowCount < 1 ) && (view.RowCount < 1))
			{
				return "";
			}
			string obs="";
			string via="";

			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				
				tab.BeginRow();
				tab.AddCell(  chkSpc( HtmlGW.Redirect_Link(view.Dat.mergedocid.AsString, 
					view.Dat.descrip.AsString,
					"VerDocMerge.aspx","mergedocid" )
					));

				
				tab.AddCell(  chkSpc( view.Dat.fechagen.AsString ));
				tab.AddCell(  chkSpc( view.Dat.nick.AsString ));
				tab.AddCell(  chkSpc(""));

				tab.EndRow();
			}
			


			
			for( viewAvisos.GoTop(); ! viewAvisos.EOF; viewAvisos.Skip())
			{
				
				tab.BeginRow();
				tab.AddCell(  chkSpc( HtmlGW.Redirect_Link(viewAvisos.Dat.mergedocid.AsString, 
					viewAvisos.Dat.descrip.AsString,
					"VerDocMerge.aspx","mergedocid" )
					));

				
				tab.AddCell(  chkSpc( viewAvisos.Dat.fechagen.AsString ));
				tab.AddCell(  chkSpc( viewAvisos.Dat.nick.AsString ));

				if ( viewAvisos.Dat.enviomodo.AsString == "F" ) 
				{
					via = "FAX/Mail";
				} 
				else if ( viewAvisos.Dat.enviomodo.AsString == "C" ) 
				{
					via = "Correo";
				} 
				else 
				{
					via = viewAvisos.Dat.enviomodo.AsString;
				}

				obs = "Envio " + viewAvisos.Dat.enviofecha.AsString + " Via " + via ;

				tab.AddCell(  chkSpc(obs));

				tab.EndRow();
			}


			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			return  string.Format(boxPattern, "mergeBox", "divMerge", tab.Html(), "Merges");			
			
		}
		#endregion
		

		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}
	}
}
