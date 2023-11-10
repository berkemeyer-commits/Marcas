using System;

namespace Berke.Marcas.BizActions.Reports
{
	using Berke.Libs.Base;
	/// <summary>
	/// Utilidades para Reportes
	/// </summary>
	public class ReportUtils
	{
		public ReportUtils()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Obtiene la platilla identificada por la clave "key"
		/// </summary>
		/// <param name="db">AccesoDB</param>
		/// <param name="key">Identificador de la plantilla</param>
		/// <returns>Plantilla, null si no existe</returns>
		public static string loadTemplate(Berke.Libs.Base.Helpers.AccesoDB db, string key)
		{
			Berke.DG.DBTab.DocumentoPlantilla docTmpl = new Berke.DG.DBTab.DocumentoPlantilla(db);
			docTmpl.Dat.Clave.Filter = ObjConvert.GetFilter(key);
			docTmpl.Adapter.ReadAll();
			if (docTmpl.RowCount > 0)
			{
				return docTmpl.Dat.PlantillaHTML.AsString;
			}
			return null;
		}

		public static void ActivarExcel(System.Web.HttpResponse Response, string outfilename, string doc)
		{
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename="+ outfilename );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(doc); 
			Response.End();
		}

		public static void ActivarWord(System.Web.HttpResponse Response, string outfilename, string doc)
		{
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename="+ outfilename );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(doc); 
			Response.End();
		}
	}
}
