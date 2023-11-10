#region Using
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
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.Base;

#endregion Using

namespace Berke.Marcas.WebUI.Home
{
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	/// <summary>
	/// Summary description for repRenovadoPorOtro.
	/// </summary>
	public partial class repRenovadoPorOtro : System.Web.UI.Page
	{


		#region Properties

		#region HtmlPayLoad
		private string HtmlPayLoad
		{
			get{ return Convert.ToString(( Session["HtmlPayLoad"] == null )? "" : Session["HtmlPayLoad"] ) ; }
			set{ Session["HtmlPayLoad"] = Convert.ToString( value );}
		}
		#endregion Html


		#endregion Properties

		#region Controles del Form
		#endregion Controles del Form

		#region Variables Globales
		private int OFFSET = 4;
		#endregion

		#region Generado por el designer

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( ! this.IsPostBack )
			{
				ValoreIniciales();
			}

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

		#endregion Generado por el designer

		#region ValoresIniciales
		private void ValoreIniciales()
		{
			

			#region DropDown de IntruccionTipo
			SimpleEntryDS seInstrucTipo = Berke.Marcas.UIProcess.Model.Instruccion.ReadForSelect();

			#endregion DropDown de IntruccionTipo

			#region Funcionario DropDown
			Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();

			#endregion Funcionario DropDown
		
			
		
		
		}
		#endregion ValoresIniciales

		#region NewWhere
		private string NewWhere ( string campo , string desde, string hasta, string tipo, string oldWhere )
		{
		
			#region  Parametros
			string where = "";

	
			if( desde == "" && hasta == "" )
			{
				where = "";
			}
			else
			{
				if( hasta == "" )
				{
					hasta = desde;
				}

				if( tipo == "Date" )
				{
				
					Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.DB_Culture );

					desde = ObjConvert.AsString( Convert.ToDateTime(desde) );
					hasta = ObjConvert.AsString( Convert.ToDateTime(hasta) );

					Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.UI_Culture );

				}
					
				string conjuncion = "";	
				if( oldWhere.ToUpper().IndexOf("WHERE") == -1 )
				{
					conjuncion = " WHERE ";
				}
				else
				{
					conjuncion = " AND ";
				}

				where = conjuncion + campo +  " between '" + desde + "' and '" + hasta +"'";

			}
			#endregion 
			return where;
					
		}
		#endregion NewWhere

		#region Boton de Generar
		protected void btnGenerar_Click(object sender, System.EventArgs e)
		{
			string buffer = "";		
			buffer= HtmlInstrucciones();
			if (buffer!= "")
			{
				ActivarExcel(buffer);
			}
			else 
			{
				lblMensaje.Text = "No se encontraron datos para los criterios seleccionados.";
			}
		
			/*
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;
					
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );
			*/
	
		}
		#endregion Boton de Generar

		#region Activar MS-Excel
		private void ActivarExcel(string b)
		{
		Response.Clear();
		Response.Buffer = true;
		Response.ContentType = "application/vnd.ms-excel";
		Response.AddHeader("Content-Disposition", "attachment;filename=RenovadasPorOtro.xls" );
		Response.Charset = "UTF-8";
		Response.ContentEncoding = System.Text.Encoding.UTF8;
		Response.Write(b); 
		Response.End();
		}

		#endregion


		private string HtmlInstrucciones()
		{
			string mfecha = "";
			this.lblMensaje.Visible = false;
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.ViewTab.vRenovadoPorOtro view_vRenovadoPorOtro = new Berke.DG.ViewTab.vRenovadoPorOtro(db);


/*
			db.Sql = @"SELECT  expInst.Fecha, mr.RegistroNro, it.Descrip, c.Nro, c.Anio, dbo.Marca.Denominacion, 
        u.nick, 
        u.Nombre AS usuario,  expInst.FuncionarioID, 
        expInst.Obs, expInst.ExpedienteID, dbo.Expediente.Acta
FROM   dbo.Marca 

INNER JOIN  dbo.Expediente_Instruccion expInst 

INNER JOIN  dbo.InstruccionTipo it 
        ON it.ID = expInst.InstruccionTipoID 
        ON dbo.Marca.ID = expInst.MarcaID 
        
INNER JOIN  dbo.Expediente 
        ON expInst.ExpedienteID = dbo.Expediente.ID 
        
LEFT OUTER JOIN dbo.MarcaRegRen mr 
        ON expInst.ExpedienteID = mr.ExpedienteID 
        
LEFT OUTER JOIN dbo.Correspondencia c 
        ON expInst.CorrespondenciaID = c.ID 


RIGHT OUTER JOIN Usuario u
        ON expInst.FuncionarioID = u.id";

*/

			//						NOMBRE COL         DESDE					HASTA

/*
			db.Sql+= NewWhere( "expInst.Fecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );
			db.Sql+= NewWhere( "u.id", this.ddlFuncionario.SelectedValue, "", "string", db.Sql );
			db.Sql+= NewWhere( "expInst.InstruccionTipoID", this.ddlInstruccion.SelectedValue, "", "string", db.Sql );

			db.Sql+= " order by expInst.Fecha ";
*/

			
			view_vRenovadoPorOtro.Dat.FechaInstruccion .Filter = ObjConvert.GetFilter( txtFechaAlta.Text);
			view_vRenovadoPorOtro.Dat.PresentacionFecha.Filter = ObjConvert.GetFilter( txtFechaSol.Text );

			view_vRenovadoPorOtro.ClearOrder();
			view_vRenovadoPorOtro.Dat.PresentacionFecha.Order=1;
		    view_vRenovadoPorOtro.Dat.ActaNro.Order=2;
			view_vRenovadoPorOtro.Adapter.ReadAll();

			string buffer= "";
			if (view_vRenovadoPorOtro.RowCount>0)
			{

				#region Obtener plantilla 
				string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("RenPorOtro", (int) GlobalConst.Idioma.ESPANOL);
				if( plantilla == "" )
				{
					this.ShowMessage( "Error con la plantilla" );
					return "";
				}
				#endregion Obtener plantilla

				#region Inicializar Generadores de codigo
				Berke.Libs.CodeGenerator cg           = new  Berke.Libs.CodeGenerator(plantilla);
				Berke.Libs.CodeGenerator cabecera     = cg.ExtraerRowExcel("cabecera",  3);
				Berke.Libs.CodeGenerator fila         = cg.ExtraerRowExcel("fila");
				#endregion Inicializar Generadores de codigo

				fila.clearText();
			
			

				/*
							tab.cell.Text.Size = "-1";
							tab.cell.BgColor = "silver";
							tab.cell.Text.Bold = true;
							tab.BeginRow();
							tab.AddCell("Fecha");
							tab.AddCell("  Acta  ");
							tab.AddCell("Clase");
							tab.AddCell("Tipo");
							tab.AddCell("Denominacion");
							tab.AddCell("Vencimiento");
							tab.AddCell("Solicitante");
							tab.AddCell("Pais");

							tab.AddCell("Renovado Por");

							tab.AddCell("Agente Anterior");

							tab.AddCell("Nro. Registro");
				
							tab.EndRow();
			
							tab.cell.BgColor = "ffffc0";
							tab.cell.Text.Bold = false;
				*/

				#region Generar Archivo

				Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.UI_Culture );
				int cnt=0;

				string nuestra_vigilada ="";
				int cntNuestra  = 0;
				int cntVigilada = 0;

				while ( ! view_vRenovadoPorOtro.EOF )
				{
					/*
					tab.BeginRow();
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.PresentacionFecha.AsString) ));
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.ActaNro.AsString+" - "+ view_vRenovadoPorOtro.Dat.ActaAnio.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.Clase.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.Marcatipo.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.Denominacion.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.Vencimientofecha.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.Propietario.AsString)) );

					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.ProPais.AsString)) );
					tab.AddCell( chkSpc( ( "(" + view_vRenovadoPorOtro.Dat.IDRenovadoPor.AsString + ")  " + view_vRenovadoPorOtro.Dat.RenovadoPorAgenteLocal.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.AgenteAnterior.AsString)) );
					tab.AddCell( chkSpc( ( view_vRenovadoPorOtro.Dat.RegistroNro.AsString)) );
			
					tab.EndRow();
					*/

					fila.copyTemplateToBuffer();

				
				
					fila.replaceField("fecha", view_vRenovadoPorOtro.Dat.PresentacionFecha.AsString);
					fila.replaceField("clase", view_vRenovadoPorOtro.Dat.Clase.AsString);

					fila.replaceField("acta", view_vRenovadoPorOtro.Dat.ActaNro.AsString + " - " + view_vRenovadoPorOtro.Dat.ActaAnio.AsString);

					fila.replaceField("tipo" , view_vRenovadoPorOtro.Dat.Marcatipo.AsString);

					fila.replaceField("denominacion" , view_vRenovadoPorOtro.Dat.Denominacion.AsString);

					fila.replaceField("registronro" , view_vRenovadoPorOtro.Dat.RegistroNro.AsString);
					fila.replaceField("renovadopor" , "( " +  view_vRenovadoPorOtro.Dat.IDRenovadoPor.AsString  + " ) " + view_vRenovadoPorOtro.Dat.RenovadoPorAgenteLocal.AsString);
					fila.replaceField("vencimientofecha", view_vRenovadoPorOtro.Dat.Vencimientofecha.AsString);

					fila.replaceField("solicitante", view_vRenovadoPorOtro.Dat.Propietario.AsString);
					fila.replaceField("pais", view_vRenovadoPorOtro.Dat.ProPais.AsString);

					fila.replaceField("cliente", "( " + view_vRenovadoPorOtro.Dat.clienteID.AsString +") " + view_vRenovadoPorOtro.Dat.nombre.AsString);
					fila.replaceField("agenteanterior", view_vRenovadoPorOtro.Dat.AgenteAnterior.AsString);
					fila.replaceField("BoletinObs",view_vRenovadoPorOtro.Dat.ObsInstruccion.AsString);

					if ( view_vRenovadoPorOtro.Dat.Nuestra.AsBoolean==true) 
					{
						nuestra_vigilada ="Nuestra";	
						cntNuestra++;
				
					} 
					else if ( view_vRenovadoPorOtro.Dat.Vigilada.AsBoolean==true) 
					{
						nuestra_vigilada ="Vigilada";
						cntVigilada++;

					}

					fila.replaceField("NuestraVig", nuestra_vigilada);
				
				
				
					fila.replaceField("obs", obtenerInstPN(view_vRenovadoPorOtro.Dat.expedienteIDPadre.AsInt));


					fila.addBufferToText();
					if (chkEspacio.Checked)
					{
						fila.copyTemplateToBuffer();
						#region reemplazar por cadena vacia
						fila.replaceField("fecha",            "");
						fila.replaceField("clase",            "");
						fila.replaceField("acta",             "");
						fila.replaceField("tipo" ,            "");
						fila.replaceField("denominacion" ,    "");
						fila.replaceField("registronro" ,     "");
						fila.replaceField("renovadopor" ,     "");
						fila.replaceField("vencimientofecha", "");
						fila.replaceField("solicitante",      "");
						fila.replaceField("pais",             "");
						fila.replaceField("cliente",          "");
						fila.replaceField("agenteanterior",   "");
						fila.replaceField("BoletinObs",       "");
						fila.replaceField("NuestraVig",       "");
						fila.replaceField("obs",              "");

						#endregion 
						fila.addBufferToText();
					}

					cnt++ ;

					view_vRenovadoPorOtro.Skip();
				}


				cabecera.copyTemplateToBuffer();
			
				mfecha = txtFechaAlta.Text;

				if ( txtFechaAlta.Text=="" ) 
				{
					mfecha = txtFechaSol.Text;
				}
				cabecera.replaceField("Fecdesde", mfecha );
			
			
				cabecera.replaceField("cntntra", cntNuestra.ToString());
				cabecera.replaceField("cntvig", cntVigilada.ToString());
				cabecera.replaceField("Cnt", cnt.ToString());
				cabecera.addBufferToText();

				cg.copyTemplateToBuffer();
				cg.replaceLabel("cabecera", cabecera.Texto);
				cg.replaceLabel("fila", fila.Texto);
				if (chkEspacio.Checked)
				{
					cnt = cnt * 2;
				}
				int nrows = OFFSET + cnt + 1;

				cg.replaceField("nrows", ""+ nrows);
				cg.addBufferToText();
				
			
				buffer = cg.Texto;

				#endregion

				/*
							Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

							txtFmt.Bold = true;
							txtFmt.Size = "3";

							Titulo = txtFmt.Html( Titulo );
							txtFmt.Size = "1";

							Titulo += txtFmt.Html( " ( "+ this.txtFechaAlta.Text + " -- " + this.txtFechaSol.Text + " ) ");


							this.HtmlPayLoad =  Titulo + "<br>" +  tab.Html() ;
		

							return Titulo + "<br>" +  tab.Html() + txtFmt.Html( MensajeError );
							*/
			}

			return buffer;

		}



		#region Obtener Instruccion Posible Nulidad
		private string obtenerInstPN(int expedienteID)
		{
			//const string PN = "9";
			string instruccion = "";
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Expediente_Instruccion expInst = new Berke.DG.DBTab.Expediente_Instruccion(db);

			expInst.Dat.ExpedienteID.Filter     = ObjConvert.GetFilter(expedienteID.ToString());
			expInst.Dat.InstruccionTipoID.Filter= ObjConvert.GetFilter(((int)GlobalConst.InstruccionTipo.POSIBLE_NULIDAD).ToString());
			expInst.Adapter.ReadAll();
			if ( expInst.RowCount > 0 ) 
			{
				instruccion = "PN";
			}
		
			return instruccion;
		}

		#endregion


		#region Enviar a Excel
		protected void btnGenExcel_Click(object sender, System.EventArgs e)
		{
			
			string buffer = "";		
			if( this.txtFechaAlta.Text == ""  && this.txtFechaSol.Text == "")
			{
				ShowMessage("Por favor, ingrese la fecha de alta o la fecha de la solicitud") ;
				return;
			}
			
			
			buffer= HtmlInstrucciones();
			ActivarExcel(buffer);
		
			/*
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=RenovadoPorOtro.xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); 
			Response.End();
						
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
			*/
		
		}

		#endregion Enviar a Excel

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion

		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}

		private void btnContar_Click(object sender, System.EventArgs e)
		{
			
		}

	}
}
