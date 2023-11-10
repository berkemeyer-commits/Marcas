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
public partial class repTramitesRecientes : System.Web.UI.Page
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
	txtFechaAlta.Text		= string.Format("{0:d}", DateTime.Today.AddDays( -1) );
	txtFechaAlta_Max.Text	= string.Format("{0:d}", DateTime.Today );
	
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
ShowMessage(" Ingrese los parámetros ") ;// si no se cargó la fecha solicita el ingreso.
return;
}
			
buffer+= HtmlTramitesRecientes();
		
buffer+=" <p></p>";

this.HtmlPayLoad = buffer;
		
	
Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
url.redirect( "reportViewer.aspx" );

}
#endregion Boton de Generar

	#region HtmlTramitesRecientes
private string HtmlTramitesRecientes()
{
		
		
string Titulo = "Trámites Grabados Recientemente ";
string MensajeError = "";
		
Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

/* SQL Original
	db.Sql= @"SELECT     
					dbo.Marca.Denominacion AS Denominacion,
					dbo.Clase.Nro AS Clase,
					dbo.Tramite.Abrev AS TramiteAbrev, 
					dbo.MarcaRegRen.Registro AS Registro, 
                    dbo.Situacion.Abrev AS SituacionAbrev, 
					dbo.Expediente_Situacion.AltaFecha AS AltaFecha, 
					dbo.Expediente.VencimientoFecha, 
					dbo.Expediente.Bib, 
                    dbo.Expediente.Exp,
					dbo.Expediente.Acta AS Acta, 
					dbo.CPersonaFisica.nombrecorto AS UsuarioNombreCorto
		FROM        
					dbo.CInicial LEFT OUTER JOIN dbo.CPersonaFisica ON dbo.CInicial.identidad = dbo.CPersonaFisica.identidad 
					RIGHT OUTER JOIN dbo.Expediente INNER JOIN dbo.Expediente_Situacion ON 
					dbo.Expediente.ID = dbo.Expediente_Situacion.ExpedienteID INNER JOIN dbo.Tramite_Sit 
					ON dbo.Expediente_Situacion.TramiteSitID = dbo.Tramite_Sit.ID INNER JOIN
                    dbo.Situacion ON dbo.Tramite_Sit.SituacionID = dbo.Situacion.ID INNER JOIN
                    dbo.Tramite ON dbo.Tramite_Sit.TramiteID = dbo.Tramite.ID INNER JOIN
                    dbo.Marca ON dbo.Expediente.MarcaID = dbo.Marca.ID INNER JOIN
                    dbo.MarcaRegRen ON dbo.Expediente.ID = dbo.MarcaRegRen.ExpedienteID INNER JOIN
                    dbo.Clase ON dbo.Marca.ClaseID = dbo.Clase.ID ON 
					dbo.CInicial.idini = dbo.Expediente_Situacion.FuncionarioID
					";
*/

	db.Sql= @"SELECT    
	m.Denominacion AS Denominacion,
	c.Nro AS Clase,
	tr.Abrev AS TramiteAbrev, 
	mregren.Registro AS Registro, 
        sit.Abrev AS SituacionAbrev, 
        exp_sit.AltaFecha AS AltaFecha, 
	exp.VencimientoFecha, 
	exp.Bib, 
        exp.Exp,
	exp.Acta AS Acta, 
	u.Nombre AS UsuarioNombreCorto
FROM  Expediente exp

JOIN dbo.Expediente_Situacion exp_sit
ON ( exp.ID = exp_sit.ExpedienteID )

JOIN dbo.Tramite_Sit tr_sit
ON (exp_sit.TramiteSitID = tr_sit.ID ) 

JOIN dbo.Situacion sit
ON (tr_sit.SituacionID = sit.ID )

JOIN dbo.Tramite tr
ON (tr_sit.TramiteID = tr.ID)

JOIN dbo.Marca m
ON ( exp.MarcaID = m.ID )

LEFT JOIN dbo.MarcaRegRen mregren
ON ( exp.ID = mregren.ExpedienteID )

JOIN dbo.Clase c
ON ( m.ClaseID = c.ID ) 

JOIN Usuario u
ON  ( u.id = exp_sit.FuncionarioID ) ";
		
db.Sql+= NewWhere( "exp_sit.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

db.Sql+= " order by exp_sit.AltaFecha";

tab.cell.Text.Size = "-1";

System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

tab.cell.BgColor = "navy";
tab.cell.Text.Color = "white";
tab.cell.Text.Bold = true;
tab.BeginRow();

tab.AddCell("Denominación" );
tab.AddCell( "Clase" );
tab.AddCell( "Trámite");
tab.AddCell( "Registro");
tab.AddCell( "Situación");
tab.AddCell( "Fec. Alta");
tab.AddCell( "Fec. Venc.");
tab.AddCell( "Bib.");
tab.AddCell( "Exp.");
tab.AddCell( "Acta");
tab.AddCell( "Nick");

tab.EndRow();
			
tab.cell.BgColor = "ffffc0";
tab.cell.Text.Color = "navy";
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
tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
tab.AddCell( ObjConvert.AsString( reader["Clase"] ) );
tab.AddCell( ObjConvert.AsString( reader["TramiteAbrev"]	) );
tab.AddCell( ObjConvert.AsString( reader["Registro"]) );
tab.AddCell( ObjConvert.AsString( reader["SituacionAbrev"]) );
tab.AddCell( ObjConvert.AsString( reader["AltaFecha"]) );
tab.AddCell( ObjConvert.AsString( reader["VencimientoFecha"]) );
tab.AddCell( ObjConvert.AsString( reader["Bib"]) );
tab.AddCell( ObjConvert.AsString( reader["Exp"]) );
tab.AddCell( ObjConvert.AsString( reader["Acta"]) );
tab.AddCell( ObjConvert.AsString( reader["UsuarioNombreCorto"]) );

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
	#endregion HtmlTramitesRecientes

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
			
buffer+= HtmlTramitesRecientes();
			
buffer+=" <p></p>";


			
this.HtmlPayLoad = buffer;

Response.Clear();
Response.Buffer = true;
Response.ContentType = "application/vnd.ms-excel";
Response.AddHeader("Content-Disposition", "attachment;filename=TramitesRecientes.xls");
Response.Charset = "UTF-8";
Response.ContentEncoding = System.Text.Encoding.UTF8;
Response.Write(buffer); //Llamada al procedimiento HTML
Response.End();
						
Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
}

#endregion Enviar a Excel

}
}






