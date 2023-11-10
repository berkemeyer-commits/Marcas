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
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;

namespace Berke.Marcas.WebUI
{
	/// <summary>
	/// Summary description for RepMarcasPropiasTramite.
	/// </summary>
	public partial class RepMarcasPropiasTramite : System.Web.UI.Page
	{
		Berke.Libs.Base.Helpers.AccesoDB db;
		Berke.DG.ViewTab.vMarcasPropTram vMarcasPropTram;
		
		static private System.Globalization.NumberFormatInfo	_UI_NumberCultureFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat;
		System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");

		private void inicializar()
		{
			db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			vMarcasPropTram = new Berke.DG.ViewTab.vMarcasPropTram(db);
  	    }

		private DateTime fecdesde;
		private DateTime fechasta;
		private bool parametrosOK() 
		{
			try 
			{
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

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			inicializar();
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
	    private int OFFSET = 3;
		protected void BtnGenerar_Click(object sender, System.EventArgs e)
		{
			//
			if (parametrosOK()) 
            {
				#region Aplicar Filtros
				vMarcasPropTram.ClearFilter();
				vMarcasPropTram.Adapter.ClearParams();
				vMarcasPropTram.Adapter.AddParam("fecdesde",fecdesde);
				vMarcasPropTram.Adapter.AddParam("fechasta",fechasta);
				#endregion Aplicar Filtros
				vMarcasPropTram.Adapter.ReadAll();

				#region Obtener plantilla 
				string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("MarcasPropTram", (int) GlobalConst.Idioma.ESPANOL);
				if( plantilla == "" )
				{
					this.ShowMessage( "Error con la plantilla" );
					return ;
				}
				#endregion Obtener plantilla

				#region Inicializar Generadores de codigo
				Berke.Libs.CodeGenerator cg           = new  Berke.Libs.CodeGenerator(plantilla);
				Berke.Libs.CodeGenerator cabecera     = cg.ExtraerRowExcel("cabecera", 3);
				Berke.Libs.CodeGenerator fila         = cg.ExtraerRowExcel("fila");

				#region Inicializar DBTab para clientes múltiples
				Berke.DG.DBTab.Cliente Cliente = new Berke.DG.DBTab.Cliente(db);
				Berke.DG.DBTab.ClienteXTramite ClienteXTramite = new Berke.DG.DBTab.ClienteXTramite(db);
				Berke.DG.DBTab.CPais CPais = new Berke.DG.DBTab.CPais(db);
				#endregion

				#region Inicializar DBTab para "situación propietario"
				Berke.DG.DBTab.Poder Poder = new Berke.DG.DBTab.Poder(db);
				Berke.DG.DBTab.ExpedienteXPoder ExpedienteXPoder = new Berke.DG.DBTab.ExpedienteXPoder(db);
				#endregion

				#endregion Inicializar Generadores de codigo

				fila.clearText();
			
				string buffer= "";

				vMarcasPropTram.GoTop();

				int cntMarcas = 0;

				#region Generar Archivo
				for( vMarcasPropTram.GoTop(); ! vMarcasPropTram.EOF ; vMarcasPropTram.Skip() )
				{

					fila.copyTemplateToBuffer();
					if (!vMarcasPropTram.Dat.multiple.AsBoolean)
					{
						fila.replaceField("clienteid", vMarcasPropTram.Dat.clienteid.AsString);
						fila.replaceField("Agente", vMarcasPropTram.Dat.nombre.AsString);
						fila.replaceField("Paisagente", vMarcasPropTram.Dat.paisalfa.AsString);
					}
					else
					{
						ClienteXTramite.Dat.ClienteMultipleID.Filter = vMarcasPropTram.Dat.clienteid.AsInt;
						ClienteXTramite.Dat.TramiteID.Filter = vMarcasPropTram.Dat.tramiteid.AsInt;
						ClienteXTramite.Adapter.ReadAll();
						Cliente.Adapter.ReadByID(ClienteXTramite.Dat.ClienteID.AsInt);
						CPais.Adapter.ReadByID(Cliente.Dat.PaisID.AsInt);
						fila.replaceField("clienteid", Cliente.Dat.ID.AsString);
						fila.replaceField("Agente", Cliente.Dat.Nombre.AsString);
						fila.replaceField("Paisagente", CPais.Dat.paisalfa.AsString);
					}
					fila.replaceField("Situacion", vMarcasPropTram.Dat.situacion_descrip.AsString);
					fila.replaceField("Denominacion", vMarcasPropTram.Dat.denominacion.AsString);
					fila.replaceField("Clase", vMarcasPropTram.Dat.nro.AsString);
					fila.replaceField("Anhoacta", vMarcasPropTram.Dat.actaanio.AsString);
					fila.replaceField("Nroacta", vMarcasPropTram.Dat.actanro.AsString);
					fila.replaceField("Fecpres", vMarcasPropTram.Dat.presentacionfecha.AsString);
					fila.replaceField("Fecpublic", vMarcasPropTram.Dat.publicacionfecha.AsString);
					fila.replaceField("Tipo", vMarcasPropTram.Dat.abrev.AsString);
					fila.replaceField("Fechasituacion", vMarcasPropTram.Dat.situacionfecha.AsString);
					fila.replaceField("Propietario", vMarcasPropTram.Dat.propietario.AsString);
					fila.replaceField("Paispropietario", vMarcasPropTram.Dat.propais.AsString);

					//fila.replaceField("Situacionprop", vMarcasPropTram.Dat.tramitesitid.AsString);

				    char situacion;
					situacion = 'N';
					ExpedienteXPoder.Dat.ExpedienteID.Filter = vMarcasPropTram.Dat.id.AsInt;
					ExpedienteXPoder.Adapter.ReadAll();
					Poder.Adapter.ReadByID(ExpedienteXPoder.Dat.PoderID.AsInt);	

					if (!Poder.Dat.ActaNro.IsNull)
					{
						situacion = 'A';
					}

					if (!Poder.Dat.Inscripcion.IsNull)
					{
						situacion = 'I';
					}

					fila.replaceField("Situacionprop", situacion.ToString());
					fila.replaceField("Fecconcesion", vMarcasPropTram.Dat.concesionfecha.AsString);
					fila.replaceField("Nroregistro", vMarcasPropTram.Dat.registronro.AsString);
		
					fila.addBufferToText();

					cntMarcas++ ;
				
				}
				
				cabecera.copyTemplateToBuffer();
				cabecera.replaceField("Fecdesde", txtDesde.Text);
				cabecera.replaceField("Fechasta", txtHasta.Text);
				cabecera.replaceField("Canttram", cntMarcas.ToString());
				cabecera.addBufferToText();

				cg.copyTemplateToBuffer();
				cg.replaceLabel("cabecera", cabecera.Texto);
				cg.replaceLabel("fila", fila.Texto);

				int nrows = OFFSET + cntMarcas + 1;

				cg.replaceField("nrows", ""+ nrows);
				cg.addBufferToText();
				
				buffer = cg.Texto;

				#endregion

				#region Activar MS-Excel
				Response.Clear();
				Response.Buffer = true;
				Response.ContentType = "application/vnd.ms-excel";
				Response.AddHeader("Content-Disposition", "attachment;filename=marcaspropiastramite.xls" );
				Response.Charset = "UTF-8";
				Response.ContentEncoding = System.Text.Encoding.UTF8;
				Response.Write(buffer); //Llamada al procedimiento HTML
				Response.End();
				#endregion Activar MS-Excel

			}

		}
	}
}
