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
#endregion Using

namespace Berke.Marcas.WebUI.Home
{
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	/// <summary>
	/// Summary description for repInstruccion.
	/// </summary>
	public partial class repInstruccion : System.Web.UI.Page
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
		private void ValoreIniciales(){
			

			#region DropDown de IntruccionTipo
			SimpleEntryDS seInstrucTipo = Berke.Marcas.UIProcess.Model.Instruccion.ReadForSelect();
			ddlInstruccion.Fill( seInstrucTipo.Tables[0], true );
			#endregion DropDown de IntruccionTipo

			#region Funcionario DropDown
			Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlFuncionario.Fill( seFuncionario.Table, true);
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
				else{
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
			buffer+= HtmlInstrucciones();
		
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;
					
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );
			

		}
		#endregion Boton de Generar


		private string HtmlInstrucciones()
		{
			string Titulo = "Carga de Instrucciones";
			string MensajeError = "";
			this.lblMensaje.Visible = false;
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			/* SQL original
						db.Sql = @"SELECT     expInst.Fecha, mr.RegistroNro, it.Descrip, c.Nro, c.Anio, dbo.Marca.Denominacion, dbo.CPersonaFisica.nombrecorto AS Nick, 
								  dbo.CInicial.nomusuario AS usuario, dbo.CInicial.idini AS FuncionarioID, expInst.Obs, expInst.ExpedienteID, dbo.Expediente.Acta
			FROM         dbo.Marca INNER JOIN
								  dbo.Expediente_Instruccion expInst INNER JOIN
								  dbo.InstruccionTipo it ON it.ID = expInst.InstruccionTipoID ON dbo.Marca.ID = expInst.MarcaID INNER JOIN
								  dbo.Expediente ON expInst.ExpedienteID = dbo.Expediente.ID LEFT OUTER JOIN
								  dbo.MarcaRegRen mr ON expInst.ExpedienteID = mr.ExpedienteID LEFT OUTER JOIN
								  dbo.Correspondencia c ON expInst.CorrespondenciaID = c.ID LEFT OUTER JOIN
								  dbo.CPersonaFisica RIGHT OUTER JOIN
								  dbo.CInicial ON dbo.CPersonaFisica.identidad = dbo.CInicial.identidad ON expInst.FuncionarioID = dbo.CInicial.idini";
			*/


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



			//						NOMBRE COL         DESDE					HASTA
			db.Sql+= NewWhere( "expInst.Fecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );
			db.Sql+= NewWhere( "u.id", this.ddlFuncionario.SelectedValue, "", "string", db.Sql );
			db.Sql+= NewWhere( "expInst.InstruccionTipoID", this.ddlInstruccion.SelectedValue, "", "string", db.Sql );

			db.Sql+= " order by expInst.Fecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha");
			tab.AddCell("Registro");
			tab.AddCell("Acta");

			tab.AddCell("Instrucción");
			tab.AddCell("Nro.Corresp.");
			tab.AddCell("Año Corresp.");
			tab.AddCell("Denominación");
			tab.AddCell("Observacion");
			tab.AddCell("Usuario");
				
			tab.EndRow();
			
			tab.cell.BgColor = "ffffc0";
			tab.cell.Text.Bold = false;

			Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.UI_Culture );
			while ( reader.Read() )
			{
				tab.BeginRow();

				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Fecha"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["registronro"]	)) );
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Acta"]	)) );
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["descrip"] ) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["nro"]	) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["anio"]) ));
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Denominacion"])) );
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Obs"])) );
				tab.AddCell( chkSpc( ObjConvert.AsString( reader["Nick"]) ));
				
				tab.EndRow();
			}


			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

			txtFmt.Bold = true;
			txtFmt.Size = "3";

			Titulo = txtFmt.Html( Titulo );
			txtFmt.Size = "1";

			Titulo += txtFmt.Html( " ( "+ this.txtFechaAlta.Text + " - " + this.txtFechaAlta_Max.Text + " ) ");


			this.HtmlPayLoad =  Titulo + "<br>" +  tab.Html() ;
		
	
			//Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			//url.redirect( "reportViewer.aspx" );

			return Titulo + "<br>" +  tab.Html() + txtFmt.Html( MensajeError );

		}



		#region Enviar a Excel
		protected void btnGenExcel_Click(object sender, System.EventArgs e)
		{
			
			string buffer = "";		
			if( this.txtFechaAlta.Text == "" )
			{
				ShowMessage(" Ingrese los parámetros ") ;
				return;
			}
			
			
			buffer+= HtmlInstrucciones();
		
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=Instrucciones.xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
						
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
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

		protected void btnContar_Click(object sender, System.EventArgs e)
		{
		
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql = @"SELECT count(*)
				FROM  dbo.CPersonaFisica RIGHT OUTER JOIN
                      dbo.CInicial ON dbo.CPersonaFisica.identidad = dbo.CInicial.identidad RIGHT OUTER JOIN
                      dbo.Marca INNER JOIN
                      dbo.Expediente_Instruccion expInst INNER JOIN
                      dbo.InstruccionTipo it ON it.ID = expInst.InstruccionTipoID ON dbo.Marca.ID = expInst.MarcaID ON dbo.CInicial.idini = expInst.FuncionarioID LEFT OUTER JOIN
                      dbo.MarcaRegRen mr ON mr.ExpedienteID = expInst.ExpedienteID LEFT OUTER JOIN
                      dbo.Correspondencia c INNER JOIN
                      dbo.CorrespondenciaMov cm ON c.ID = cm.CorrespondenciaID ON expInst.CorrespondenciaMovID = cm.ID
				";
			db.Sql+= NewWhere( "expInst.Fecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );
			db.Sql+= NewWhere( "dbo.CInicial.idini", this.ddlFuncionario.SelectedValue, "", "string", db.Sql );
			db.Sql+= NewWhere( "expInst.InstruccionTipoID", this.ddlInstruccion.SelectedValue, "", "string", db.Sql );

			int cont = (int) db.getValue();
			this.lblMensaje.Text = "Registros Encontrados: " +cont.ToString();
			this.lblMensaje.Visible = true;
			db.CerrarConexion();
		}

	}
}
