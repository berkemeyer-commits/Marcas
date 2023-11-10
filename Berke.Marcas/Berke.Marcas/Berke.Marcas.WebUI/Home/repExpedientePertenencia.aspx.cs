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
	using Berke.Libs.Base;
	/// <summary>
	/// Summary description for repInstruccion.
	/// </summary>
	public partial class repExpedientePertenencia : System.Web.UI.Page
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

		#region Generado por el designer

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//			if( Html != "" ){
			//					Response.Clear();
			//					Response.Write( this.Html );
			//
			//			}
		
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
			if( this.txtFechaAlta.Text == "" )
			{
				ShowMessage(" Ingrese los parámetros ") ;
				return;
			}
			
			buffer+= HtmlExpedientePertenencia();
		
			this.HtmlPayLoad = buffer;
		
	

			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );

		}
		#endregion Boton de Generar

		#region HtmlExpedientePertenencia
		
		private string HtmlExpedientePertenencia()
		{
				
			string Titulo = "Cambios de Estado  ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"SELECT     dbo.Expediente_Pertenencia.Fecha AS AltaFecha, dbo.Expediente_Pertenencia.ID AS ID, dbo.Expediente_Pertenencia.ExpedienteID AS ExpID, 
                      dbo.Expediente_Pertenencia.Nuestra AS Nuestra, dbo.Expediente_Pertenencia.Vigilada AS Vigilada, 
                      dbo.Expediente_Pertenencia.Sustituida AS Sustituida, dbo.PertenenciaMotivo.Descrip AS Motivo, dbo.Expediente.ActaNro AS ActaNro, 
                      dbo.Expediente.ActaAnio AS ActaAnio, dbo.Expediente_Pertenencia.FuncionarioID AS FuncionarioID, dbo.MarcaRegRen.RegistroNro AS RegistroNro, 
                      dbo.MarcaRegRen.RegistroAnio AS RegistroAnio, dbo.Marca.ClienteID AS ClienteID, dbo.Tramite.Descrip AS TramiteDescrip, 
                      dbo.Tramite.Abrev AS tramiteAbrev
					FROM         dbo.Expediente_Pertenencia INNER JOIN
                      dbo.Expediente ON dbo.Expediente_Pertenencia.ExpedienteID = dbo.Expediente.ID INNER JOIN
                      dbo.MarcaRegRen ON dbo.Expediente.MarcaRegRenID = dbo.MarcaRegRen.ID AND dbo.Expediente.ID = dbo.MarcaRegRen.ExpedienteID INNER JOIN
                      dbo.Marca ON dbo.Expediente.MarcaID = dbo.Marca.ID AND dbo.Expediente.ID = dbo.Marca.ExpedienteVigenteID AND 
                      dbo.MarcaRegRen.ID = dbo.Marca.MarcaRegRenID INNER JOIN
                      dbo.PertenenciaMotivo ON dbo.Expediente_Pertenencia.PertenenciaMotivoID = dbo.PertenenciaMotivo.ID INNER JOIN
                      dbo.Tramite ON dbo.Expediente.TramiteID = dbo.Tramite.ID";
		
			db.Sql+= NewWhere( "Expediente_Pertenencia.Fecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by Expediente_Pertenencia.Fecha";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha Alta" );
			tab.AddCell("ID" );
			tab.AddCell("Exp. ID" );
			tab.AddCell( "Ntra" );
			tab.AddCell( "Vig");
			tab.AddCell( "Sust.");
			tab.AddCell( "Motivo");
			tab.AddCell( "Tram.");
			tab.AddCell( "Acta Nro.");
			tab.AddCell( "Acta Año");
			tab.AddCell( "Funcionario");
			tab.AddCell( "Reg. Nro.");
			tab.AddCell( "Reg. Año");	
			tab.AddCell( "Cliente ID");

			
			tab.EndRow();
			
			tab.cell.BgColor = "White";
			tab.cell.Text.Bold = false;

			Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.UI_Culture );

			int cont = 0;
			int limite = 500;
			while ( reader.Read() )
			{
				if( cont++ > limite )
				{
					MensajeError = "La consulta excede el límite establecido de " +limite.ToString()+ " registros ";
					break;
				}
				
				tab.BeginRow();
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["AltaFecha"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["ID"]	) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["ExpID"]	) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Nuestra"] ) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Vigilada"]	) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Sustituida"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Motivo"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["tramiteAbrev"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["ActaNro"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["ActaAnio"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["FuncionarioID"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["RegistroNro"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["RegistroAnio"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["ClienteID"]) ));
								
				tab.EndRow();
			}

			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

			txtFmt.Bold = true;
			txtFmt.Size = "3";

			Titulo = txtFmt.Html( Titulo );
			txtFmt.Size = "1";

			Titulo += txtFmt.Html( " ( "+ this.txtFechaAlta.Text + " - " + this.txtFechaAlta_Max.Text + " ) ");


			this.HtmlPayLoad =  Titulo + "<br>" +  tab.Html();

			txtFmt.Size = "3";

			return Titulo + "<br>" +  tab.Html() + txtFmt.Html( MensajeError );

		}

		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
			{
				return "&nbsp;";
			}
			else{
				return buf;
			}
		}
		#endregion HtmlExpedientePertenencia

// DESCOMENTAR LUEGO

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion

		#region Enviar a Excel
		protected void btnGenExcel_Click(object sender, System.EventArgs e)
		{
			
			string buffer = "";		
			if( this.txtFechaAlta.Text == "" )
			{
				ShowMessage(" Ingrese los parámetros ") ;
				return;
			}
			
			buffer+= HtmlExpedientePertenencia();
					
			this.HtmlPayLoad = buffer;

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=CambiosEstado.xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
						
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
		}

		#endregion Enviar a Excel

	}
}
