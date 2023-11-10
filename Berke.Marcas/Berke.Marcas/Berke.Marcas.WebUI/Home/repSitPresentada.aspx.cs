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
	public partial class repSitPresentada : System.Web.UI.Page
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
			if( this.txtFechaAlta.Text == "" ){
				ShowMessage(" Ingrese los parámetros ") ;
				return;
			}
			
			buffer+= HtmlPresentadas();
		
			buffer+=" <p></p>";

			buffer+= HtmlExamFondo();
			
			buffer+=" <p></p>";

			buffer+= HtmlConcedidas();
			
			buffer+=" <p></p>";

			buffer+= HtmlRetiradas();

			buffer+=" <p></p>";

			buffer+= HtmlEnvioDeTitulos();

			this.HtmlPayLoad = buffer;
		
	

			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );

		}
		#endregion Boton de Generar

		#region HtmlPresentadas
		private string HtmlPresentadas(){
		
		
			string Titulo = "PRESENTADAS  ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;

			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
/*
 * SQL Original
 db.Sql = @" SELECT
				      E.ID AS ExpID, ES.AltaFecha AS FechaAlta, OT.Nro AS NroHI, OT.Anio AS AñoHI, E.ActaNro AS NroActa, E.ActaAnio AS AñoActa, 
                      ES.SituacionFecha AS FecPres, M.Denominacion AS Denominacion, C.Nro AS Clase, dbo.CAgenteLocal.nromatricula AS Matricula, 
                      dbo.Situacion.Descrip AS Situacion, dbo.Tramite.Descrip AS Tramite, dbo.MarcaRegRen.RegistroNro, dbo.MarcaRegRen.RegistroAnio, 
                      dbo.CPersonaFisica.Nombre
			FROM 
					  dbo.Expediente_Situacion ES INNER JOIN
                      dbo.Expediente E ON E.ID = ES.ExpedienteID INNER JOIN
                      dbo.OrdenTrabajo OT ON OT.ID = E.OrdenTrabajoID INNER JOIN
                      dbo.Marca M ON M.ID = E.MarcaID INNER JOIN
                      dbo.Clase C ON C.ID = M.ClaseID INNER JOIN
                      dbo.Tramite_Sit trmSit ON E.TramiteSitID = trmSit.ID INNER JOIN
                      dbo.CAgenteLocal ON E.AgenteLocalID = dbo.CAgenteLocal.idagloc INNER JOIN
                      dbo.Situacion ON trmSit.SituacionID = dbo.Situacion.ID INNER JOIN
                      dbo.Tramite ON E.TramiteID = dbo.Tramite.ID AND trmSit.TramiteID = dbo.Tramite.ID INNER JOIN
                      dbo.MarcaRegRen ON E.MarcaRegRenID = dbo.MarcaRegRen.ID AND E.ID = dbo.MarcaRegRen.ExpedienteID AND 
                      M.MarcaRegRenID = dbo.MarcaRegRen.ID INNER JOIN
                      dbo.CInicial ON ES.FuncionarioID = dbo.CInicial.idini INNER JOIN
                      dbo.CPersonaFisica ON dbo.CInicial.identidad = dbo.CPersonaFisica.identidad
			WHERE     (trmSit.SituacionID = 2)";

*/

	db.Sql = @" SELECT  E.ID AS ExpID, ES.AltaFecha AS FechaAlta, OT.Nro AS NroHI, 
         OT.Anio AS AñoHI, E.ActaNro AS NroActa, E.ActaAnio AS AñoActa, 
         ES.SituacionFecha AS FecPres, M.Denominacion AS Denominacion, 
         C.Nro AS Clase, dbo.CAgenteLocal.nromatricula AS Matricula, 
         dbo.Situacion.Descrip AS Situacion, dbo.Tramite.Descrip AS Tramite, 
         dbo.MarcaRegRen.RegistroNro, dbo.MarcaRegRen.RegistroAnio, 
         
         u.Nombre
FROM 
		dbo.Expediente_Situacion ES 
		INNER JOIN  dbo.Expediente E ON E.ID = ES.ExpedienteID 
		INNER JOIN  dbo.OrdenTrabajo OT ON OT.ID = E.OrdenTrabajoID 
		INNER JOIN  dbo.Marca M ON M.ID = E.MarcaID 
		INNER JOIN  dbo.Clase C ON C.ID = M.ClaseID 
		INNER JOIN  dbo.Tramite_Sit trmSit ON E.TramiteSitID = trmSit.ID 
		INNER JOIN  dbo.CAgenteLocal ON E.AgenteLocalID = dbo.CAgenteLocal.idagloc 
		INNER JOIN  dbo.Situacion ON trmSit.SituacionID = dbo.Situacion.ID 
		INNER JOIN  dbo.Tramite ON E.TramiteID = dbo.Tramite.ID AND trmSit.TramiteID = dbo.Tramite.ID 
		INNER JOIN  dbo.MarcaRegRen 
		        ON E.MarcaRegRenID = dbo.MarcaRegRen.ID AND E.ID = dbo.MarcaRegRen.ExpedienteID 
		           AND M.MarcaRegRenID = dbo.MarcaRegRen.ID 
        INNER JOIN Usuario u ON ES.FuncionarioID = u.id
        
        
WHERE     (trmSit.SituacionID = " + GlobalConst.Situaciones.PRESENTADA + ")";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Exp.ID" );				//E.ID AS ExpID,
			tab.AddCell("Fecha Alta" );			//ES.AltaFecha AS FechaAlta, 
			tab.AddCell("Nro. HI" );			//OT.Nro AS NroHI, 
			tab.AddCell("Año HI" );				//OT.Anio AS AñoHI, 
			tab.AddCell( "Nro. Acta" );			//E.ActaNro AS NroActa,
			tab.AddCell( "Año Acta");		//	E.ActaAnio AS AñoActa, 
			tab.AddCell( "Fecha Pres.");	//	ES.SituacionFecha AS FecPres,
			tab.AddCell( "Denominación");	//	M.Denominacion AS Denominacion,	
			tab.AddCell( "Clase");			//	C.Nro AS Clase,
			tab.AddCell( "Ag.Loc.");		//	dbo.CAgenteLocal.nromatricula AS Matricula, 
			tab.AddCell( "Situación");		//	dbo.Situacion.Descrip AS Situacion,
			tab.AddCell( "Trámite");		//	dbo.Tramite.Descrip AS Tramite,
			tab.AddCell( "Reg. Nro.");		//	dbo.MarcaRegRen.RegistroNro,
			tab.AddCell( "Reg. Año");	    //  dbo.MarcaRegRen.RegistroAnio,                  
            tab.AddCell( "Usuario");       //   dbo.CPersonaFisica.Nombre,
			
			
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
				
				tab.AddCell( ObjConvert.AsString( reader["ExpID"]) );
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"]) );
				tab.AddCell( ObjConvert.AsString( reader["NroHI"]	) );
				tab.AddCell( ObjConvert.AsString( reader["AñoHI"]	) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"] ) );
				tab.AddCell( ObjConvert.AsString( reader["AñoActa"]	) );
				tab.AddCell( ObjConvert.AsString( reader["FecPres"]) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"]) );
				tab.AddCell( ObjConvert.AsString( reader["Matricula"]) );
				tab.AddCell( ObjConvert.AsString( reader["Situacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Tramite"]) );
				tab.AddCell( ObjConvert.AsString( reader["RegistroNro"]) );
				tab.AddCell( ObjConvert.AsString( reader["RegistroAnio"]) );
				tab.AddCell( ObjConvert.AsString( reader["Nombre"]) );
				
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
		#endregion HtmlPresentadas

		#region HtmlExamFondo
		private string HtmlExamFondo()
		{
		
		
			string Titulo = "Situaciones: EXAMEN DE FONDO  ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"select 
ES.AltaFecha as FechaAlta,
E.ActaNro as NroActa,
E.ActaAnio as AnioActa,
ES.SituacionFecha as FecSit,
M.Denominacion,
C.Nro as Clase
from expediente_situacion ES
inner join Expediente E on E.id = ES.ExpedienteID
inner join Marca M on M.ID = E.MarcaID
inner join Clase C on C.ID = M.ClaseID
where (ES.tramitesitid IN (Select ID FROM Tramite_Sit where SituacionID = " + GlobalConst.Situaciones.EXAMEN_FONDO + ") )";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha Alta" );
			tab.AddCell( "Nro. Acta" );
			tab.AddCell( "Año Acta");
			tab.AddCell( "Fecha");
			tab.AddCell( "Denominación");
			tab.AddCell( "Clase");

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
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"]) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"] ) );
				tab.AddCell( ObjConvert.AsString( reader["AnioActa"]	) );
				tab.AddCell( ObjConvert.AsString( reader["FecSit"]) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"]) );
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
		#endregion HtmlExamFondo

		#region HtmlConcedidas
		private string HtmlConcedidas()
		{
				
			string Titulo = "Situaciones CONCEDIDAS";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"select 
				ES.AltaFecha as FechaAlta,
				E.ActaNro as NroActa,
				E.ActaAnio as AnioActa,
				MRR.RegistroNro as Registro,
				MRR.RegistroAnio as RegistroAnio,
				ES.SituacionFecha as FecConce,
				E.VencimientoFecha as FecVtoReg,
				M.Denominacion,
				C.Nro as Clase
				from expediente_situacion ES
				inner join Expediente E on E.id = ES.ExpedienteID
				inner join MarcaRegRen MRR on MRR.ID = E.MarcaRegRenID
				inner join Marca M on M.ID = E.MarcaID
				inner join Clase C on C.ID = M.ClaseID
where (ES.tramitesitid IN (Select ID FROM Tramite_Sit where SituacionID = " + GlobalConst.Situaciones.CONCEDIDA + ") )";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha Alta" );
			tab.AddCell( "Nro. Acta" );
			tab.AddCell( "Año Acta");
			tab.AddCell( "Registro");
			tab.AddCell( "Registro Año");
			tab.AddCell( "Fecha Conc.");
			tab.AddCell( "Fecha Venc. Reg.");
			tab.AddCell( "Denominación");
			tab.AddCell( "Clase");


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
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"]) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"] ) );
				tab.AddCell( ObjConvert.AsString( reader["AnioActa"]	) );
				tab.AddCell( ObjConvert.AsString( reader["Registro"]	) );
				tab.AddCell( ObjConvert.AsString( reader["RegistroAnio"]	) );
				tab.AddCell( ObjConvert.AsString( reader["FecConce"]) );
				tab.AddCell( ObjConvert.AsString( reader["FecVtoReg"]) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"]) );
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
		#endregion HtmlConcedidas

		#region HtmlRetiradas
		private string HtmlRetiradas()
		{
				
			string Titulo = "Titulo Retirado  ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"select 
				ES.AltaFecha as FechaAlta,
				E.ActaNro as NroActa,
				E.ActaAnio as AnioActa,
				ES.SituacionFecha as FecSit,
				M.Denominacion,
				C.Nro as Clase
				from expediente_situacion ES
				inner join Expediente E on E.id = ES.ExpedienteID
				inner join Marca M on M.ID = E.MarcaID
				inner join Clase C on C.ID = M.ClaseID
where (ES.tramitesitid IN (Select ID FROM Tramite_Sit where SituacionID = "+ GlobalConst.Situaciones.TITULO_RETIRADO + " ) )";
//				where (ES.tramitesitid = 7 or ES.tramitesitid = 35)";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha Alta" );
			tab.AddCell( "Nro. Acta" );
			tab.AddCell( "Año Acta");
			tab.AddCell( "Fecha Sit.");
			tab.AddCell( "Denominación");
			tab.AddCell( "Clase");

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
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"]) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"] ) );
				tab.AddCell( ObjConvert.AsString( reader["AnioActa"]	) );
				tab.AddCell( ObjConvert.AsString( reader["FecSit"]	) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"]) );
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
		#endregion HtmlRetiradas

		#region HtmlEnvioDeTitulos
		private string HtmlEnvioDeTitulos()
		{
				
			string Titulo = "Titulo Enviado ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"select 
			ES.AltaFecha as FechaAlta,
			E.ActaNro as NroActa,
			E.ActaAnio as AnioActa,
			ES.SituacionFecha as FecSit,
			M.Denominacion,
			C.Nro as Clase
			from expediente_situacion ES
			inner join Expediente E on E.id = ES.ExpedienteID
			inner join Marca M on M.ID = E.MarcaID
			inner join Clase C on C.ID = M.ClaseID
where (ES.tramitesitid IN (Select ID FROM Tramite_Sit where SituacionID = " + GlobalConst.Situaciones.TITULO_ENVIADO + ") )";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha Alta" );
			tab.AddCell( "Nro. Acta" );
			tab.AddCell( "Año Acta");
			tab.AddCell( "Fecha Sit.");
			tab.AddCell( "Denominación");
			tab.AddCell( "Clase");
	
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
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"]) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"] ) );
				tab.AddCell( ObjConvert.AsString( reader["AnioActa"]	) );
				tab.AddCell( ObjConvert.AsString( reader["FecSit"]	) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"]) );
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
		#endregion HtmlEnvioDeTitulos


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
			
			buffer+= HtmlPresentadas();
		
			buffer+=" <p></p>";

			buffer+= HtmlExamFondo();
			
			buffer+=" <p></p>";

			buffer+= HtmlConcedidas();
			
			buffer+=" <p></p>";

			buffer+= HtmlRetiradas();

			buffer+=" <p></p>";

			buffer+= HtmlEnvioDeTitulos();

			
			this.HtmlPayLoad = buffer;

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=CambioSituacion.xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
						
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
		}

		#endregion Enviar a Excel

	}
}
