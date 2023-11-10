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
	/// <summary>
	/// Summary description for repInstruccion.
	/// </summary>
	public partial class repSitTramitesVarios : System.Web.UI.Page
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
			
			buffer+= HtmlSituacionesTramitesVarios();
		
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;
					
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );

		}
		#endregion Boton de Generar

		
		#region HtmlSituacionesTramitesVarios
		private string HtmlSituacionesTramitesVarios()
		{
		
					
			Berke.Html.HtmlTextFormater tituloFmt = new Berke.Html.HtmlTextFormater();

			tituloFmt.Size = "2";
			tituloFmt.SetColor( System.Drawing.Color.Red );
			tituloFmt.Bold = true;

			string Titulo = "Resumen de: " + tituloFmt.Html( "TRAMITES VARIOS"   );
			
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"select 
				T.Abrev as Tramite,
				ES.AltaFecha as FechaAlta,
				OT.Nro as NroHI,
				OT.Anio as AnioHI,
				E.ActaNro as NroActa,
				E.ActaAnio as AnioActa,
				ES.SituacionFecha as FecPres,
				M.Denominacion,
				C.Nro as Clase
				from expediente_situacion 	ES
				inner join Expediente 		E 	on E.id = ES.ExpedienteID
				inner join OrdenTrabajo 	OT	on OT.ID = E.OrdenTrabajoID
				inner join Marca 		M	on M.ID = E.MarcaID
				inner join Clase 		C	on C.ID = M.ClaseID
				inner join Tramite_Sit 		TS 	on TS.ID = ES.TramiteSitID
				inner join Tramite		T	on T.ID = TS.TramiteID
				where 
				(
				ES.tramitesitid = 60 or  --DOM 
				ES.tramitesitid = 74 or  --DUP
				ES.tramitesitid = 54 or  --FUS
				ES.tramitesitid = 66 or  --LIC
				ES.tramitesitid = 48 or  --NOM
				ES.tramitesitid = 40     --TRA
				) ";
		
			db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= " order by ES.AltaFecha ";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell( "Trámite"		);
			tab.AddCell( "Fecha Alta"	);
			tab.AddCell( "Nro. HI"		);
			tab.AddCell( "Año HI"		);
			tab.AddCell( "Nro. Acta"	);
			tab.AddCell( "Año Acta"		);
			tab.AddCell( "Fecha Pres."	);
			tab.AddCell( "Denominación"	);
			tab.AddCell( "Clase"		);
					
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
				tab.AddCell( ObjConvert.AsString( reader["Tramite"		]) );
				tab.AddCell( ObjConvert.AsString( reader["FechaAlta"	]) );
				tab.AddCell( ObjConvert.AsString( reader["NroHI"		]) );
				tab.AddCell( ObjConvert.AsString( reader["AnioHI"		]) );
				tab.AddCell( ObjConvert.AsString( reader["NroActa"		]) );
				tab.AddCell( ObjConvert.AsString( reader["AnioActa"		]) );
				tab.AddCell( ObjConvert.AsString( reader["FecPres"		]) );
				tab.AddCell( ObjConvert.AsString( reader["Denominacion"	]) );
				tab.AddCell( ObjConvert.AsString( reader["Clase"		]) );
				tab.EndRow();
			}
				
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

			txtFmt.Bold = true;
			txtFmt.Size = "3";
			txtFmt.Size = "1";

			Titulo += txtFmt.Html( " ( "+ this.txtFechaAlta.Text + " - " + this.txtFechaAlta_Max.Text + " ) ");

			this.HtmlPayLoad =  Titulo + "<br>" +  tab.Html();

			txtFmt.Size = "3";

			return Titulo + "<br>" +  tab.Html() + txtFmt.Html( MensajeError );

		}
		#endregion HtmlSituacionesTramitesVarios


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
			
			buffer+= HtmlSituacionesTramitesVarios();
		
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=TVIngresdos.xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
						
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
		}

		#endregion Enviar a Excel
	}

}



