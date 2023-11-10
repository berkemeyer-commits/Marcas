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

namespace Berke.Marcas.WebUI
{
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	/// <summary>
	/// Summary description for RepMarcasTramites.
	/// </summary>
	public partial class RepMarcasTramites : System.Web.UI.Page
	{
		static private System.Globalization.NumberFormatInfo	_UI_NumberCultureFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat;
		System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

		#region GenerarReporte
		Berke.Libs.Base.Helpers.AccesoDB db;
		private void GenerarReporte()
		{
            int limite = Convert.ToInt32(this.txtLimite.Text);
			string mensajeError = "";
			
			db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vRepMarcasTramites view = new Berke.DG.ViewTab.vRepMarcasTramites( db );
			if( parametrosOK() )
			{
				#region Aplicar Filtros
				view.ClearFilter();
				view.Adapter.ClearParams();
				view.Adapter.AddParam("fecdesde",fecdesde);
				view.Adapter.AddParam("fechasta",fechasta);
				#endregion Aplicar Filtros
				view.Dat.AgenteLocal.Order	= 1;
				view.Dat.tramite.Order		= 2;

                int recuperados = view.Adapter.Count();

                if (recuperados != 0)
                {
                    if (recuperados < limite)
                    {
                        view.Adapter.ReadAll(limite);
                        GenerarDocumento(view);
                    }
                    else
                    {
                        mensajeError = "Los " + recuperados.ToString() + " registros a recuperar exceden el limite de " + limite.ToString();
                    }
                }
			}
		
			if(mensajeError != "" ) 
			{
				ShowMessage( mensajeError );
			}
			//----------
			
		}
		#endregion GenerarReporte
		private static string CLAVE_PLANTILLA = "M10";
		private int OFFSET = 5;
		private void GenerarDocumento(Berke.DG.ViewTab.vRepMarcasTramites view) 
		{

			Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
			dp.Dat.Clave.Filter = CLAVE_PLANTILLA;
			dp.Adapter.ReadAll();
			if (dp.RowCount == 0) 
			{
				ShowMessage("No se encuentra definida la plantilla para el reporte.");
			}
			
			string template = dp.Dat.PlantillaHTML.AsString;
			Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
			Berke.Libs.CodeGenerator cab = cg.ExtraerRowExcel("cabecera", 2);
			Berke.Libs.CodeGenerator row = cg.ExtraerRowExcel("row");
			Berke.Libs.CodeGenerator sub = cg.ExtraerRowExcel("sub");
			Berke.Libs.CodeGenerator tot = cg.ExtraerRowExcel("tot");
			cg.copyTemplateToBuffer();
			row.copyTemplateToBuffer();
			string str_rows = "";
			string curr_ag      = view.Dat.AgenteLocal.AsString;
			string curr_nombre  = view.Dat.Nombre.AsString;
			int subtotal = 0;
			int total    = 0;
			int nagentes = 0;
			bool mostrardet = chkDetalles.Checked;
			for (view.GoTop(); !view.EOF; view.Skip()) 
			{
				if (curr_ag != view.Dat.AgenteLocal.AsString)  
				{
					sub.clearText();
					sub.copyTemplateToBuffer();
					sub.replaceField("nombre", curr_nombre);
					sub.replaceField("subtotal", ""+subtotal);
					sub.addBufferToText();
					str_rows = str_rows + sub.Texto;
					subtotal = 0;	
					nagentes++;
				}
				
				curr_nombre = view.Dat.Nombre.AsString.Replace("<","(").Replace(">",")");
			    curr_ag     = view.Dat.AgenteLocal.AsString;
				subtotal    = subtotal + view.Dat.numTramites.AsInt;
				total       = total    + view.Dat.numTramites.AsInt;

				if (mostrardet)
				{
					row.clearText();
					row.copyTemplateToBuffer();
					row.replaceField("codigo"   , view.Dat.AgenteLocal.AsString);
					row.replaceField("nombre"   , view.Dat.Nombre.AsString.Replace("<", "(" ).Replace(">",")") ) ;
					row.replaceField("tramite"  , view.Dat.tramite.AsString);
					row.replaceField("subtotal" , view.Dat.numTramites.AsString);
					row.addBufferToText();
					str_rows = str_rows + row.Texto.ToString();
				}
				
			}

			#region cabecera
			cab.copyTemplateToBuffer();
			cab.replaceField("fecdesde", txtDesde.Text);
			cab.replaceField("fechasta", txtHasta.Text);
			cab.addBufferToText();
			#endregion cabecera

			cg.replaceLabel("cabecera", cab.Texto);
			if (view.RowCount > 0) 
			{
				sub.clearText();
				sub.copyTemplateToBuffer();
				sub.replaceField("nombre", curr_nombre);
				sub.replaceField("subtotal", ""+subtotal);
				sub.addBufferToText();
				str_rows = str_rows + sub.Texto;

				cg.replaceLabel("row", str_rows);
				nagentes++;
			}
			else 
			{
				cg.replaceLabel("row", "");
			}

			#region fila total
			tot.copyTemplateToBuffer();
			tot.replaceField("total", ""+total);
			tot.addBufferToText();
			cg.replaceLabel("tot", tot.Texto);
			#endregion fila total
			
			#region fila sub
			cg.replaceLabel("sub","");
			#endregion fila sub

			int nrows = OFFSET + nagentes + 1;
			if (mostrardet)
			{
				nrows = nrows + view.RowCount;
			}
			cg.replaceField("nrows", ""+ nrows);
			cg.addBufferToText();

			#region Activar MS-Excel
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=marcastramite.xls" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(cg.Texto); //Llamada al procedimiento HTML
			Response.End();
			#endregion Activar MS-Word

		}
		private DateTime fecdesde;
		private DateTime fechasta;
		private bool parametrosOK() 
		{
			try 
			{
				//DateTime.Parse(txtDesde.Text.Trim().ToString(), _UI_NumberCultureFormat );
				//DateTime.Parse(txtHasta.Text.Trim().ToString(), _UI_NumberCultureFormat );
				fecdesde = Convert.ToDateTime(txtDesde.Text.Trim().ToString(), culture);
				fechasta = Convert.ToDateTime(txtHasta.Text.Trim().ToString(), culture);
			}
			catch(Exception e) 
			{
				ShowMessage("Formato de fecha incorrecto.");
				return false;
			}
			return true;
		}
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion

		protected void BtnGenerar_Click_1(object sender, System.EventArgs e)
		{
			this.GenerarReporte();
		}
	}
}
