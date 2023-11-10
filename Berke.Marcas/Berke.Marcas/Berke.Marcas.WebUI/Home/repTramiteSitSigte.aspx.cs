
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


namespace Berke.Marcas.WebUI.Home
{
	using Berke.Libs.Base;
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	
	/// <summary>
	/// Summary description for repTramiteSitSigte.
	/// </summary>
	public partial class repTramiteSitSigte : System.Web.UI.Page
	{

	
		#region Controles del Web Form
		#endregion Controles del Web Form

		#region Properties

		#region HtmlPayLoad
		private string HtmlPayLoad
		{
			get{ return Convert.ToString(( Session["HtmlPayLoad"] == null )? "" : Session["HtmlPayLoad"] ) ; }
			set{ Session["HtmlPayLoad"] = Convert.ToString( value );}
		}
		#endregion Html


		#endregion Properties
		
		#region Asignar Delegados
		private void AsignarDelegados()
		{

			//			this.cbxAgenteLocalID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxAgenteLocalID_LoadRequested); 
			//			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
			//
			ddlTramiteID.SelectedIndexChanged+= new EventHandler(ddlTramiteID_SelectedIndexChanged);


		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		
			#region Llenar DropDpwn de Tramites
	
//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
//			ddlTramiteID.Fill( se.Tables[0], true);	

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);
			#endregion
				
		}
		#endregion Asignar Valores Iniciales

		
		protected void ddlTramiteID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( ddlTramiteID.SelectedIndex < 1 )
			{
				ddlTramiteSitID.Items.Clear();
			}
			else
			{
				SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramiteID.SelectedValue ) );
				ddlTramiteSitID.Fill( situacion.Tables[0], true );
				
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				AsignarValoresIniciales();
				//MostrarPanel_Busqueda();
			}		
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}
		#endregion


//		#region NewWhere
//		private string NewWhere ( string campo , string desde, string hasta, string tipo, string oldWhere )
//		{
//		
//			#region  Parametros
//			string where = "";
//
//	
//			if( desde == "" && hasta == "" )
//			{
//				where = "";
//			}
//			else
//			{
//				if( hasta == "" )
//				{
//					hasta = desde;
//				}
//
//				if( tipo == "Date" )
//				{
//				
//					Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.DB_Culture );
//
//					desde = ObjConvert.AsString( Convert.ToDateTime(desde) );
//					hasta = ObjConvert.AsString( Convert.ToDateTime(hasta) );
//
//					Berke.Libs.Base.ObjConvert.SetCulture( Berke.Libs.Base.CultureFormat.UI_Culture );
//
//				}
//					
//				string conjuncion = "";	
//				if( oldWhere.ToUpper().IndexOf("WHERE") == -1 )
//				{
//					conjuncion = " WHERE ";
//				}
//				else
//				{
//					conjuncion = " AND ";
//				}
//
//				where = conjuncion + campo +  " between '" + desde + "' and '" + hasta +"'";
//
//			}
//			#endregion 
//			return where;
//					
//		}
//		#endregion NewWhere

		#region Boton de Generar
		protected void btnGenerar_Click(object sender, System.EventArgs e)
		{
			string buffer = "";	
	

			if(( this.ddlTramiteID.SelectedIndex < 1 && this.ddlTramiteSitID.SelectedIndex < 1))
			{
				ShowMessage(" Ingrese los parámetros ") ;// si no se cargó la fecha solicita el ingreso.
				return;
			}
			
			buffer+= HtmlTramiteSitSgte();
		
			buffer+=" <p></p>";

			this.HtmlPayLoad = buffer;
		
	
			Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
			url.redirect( "reportViewer.aspx" );

		}
		#endregion Boton de Generar

		#region HtmlTramiteSitSgte
		private string HtmlTramiteSitSgte()
		{
		
			string Titulo = " Trámite Situación Siguiente  ";
			string MensajeError = "";
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.Sql= @"SELECT DISTINCT ts1.TramiteID, t1.Descrip AS T_Orig, tss.TramiteSitID, s1.Descrip AS Origen, tss.TramiteSitSgteID, s2.Descrip AS Destino
FROM         dbo.Tramite_SitSgte tss INNER JOIN
                      dbo.Tramite_Sit ts1 ON tss.TramiteSitID = ts1.ID INNER JOIN
                      dbo.Tramite_Sit ts2 ON tss.TramiteSitSgteID = ts2.ID INNER JOIN
                      dbo.Tramite t1 ON ts1.TramiteID = t1.ID INNER JOIN
                      dbo.Tramite t2 ON ts2.TramiteID = t2.ID INNER JOIN
                      dbo.Situacion s1 ON ts1.SituacionID = s1.ID INNER JOIN
                      dbo.Situacion s2 ON ts2.SituacionID = s2.ID CROSS JOIN
                      dbo.Tramite t
			WHERE ts1.TramiteID = ";
				string Param1 =  this.ddlTramiteID.Value;
				string Param2 =  this.ddlTramiteSitID.Value;
					

//			db.Sql+= NewWhere( "tss.TramiteSitSgteID", this.ddlTramiteSitID.Value, this.ddlTramiteSitID.Value, db.Sql );
//			//db.Sql+= NewWhere( "ES.AltaFecha", this.txtFechaAlta.Text, this.txtFechaAlta_Max.Text, "Date", db.Sql );

			db.Sql+= Param1 + " AND tss.TramiteSitID = " + Param2;	
		    db.Sql+= " order by s2.Descrip";

			tab.cell.Text.Size = "-1";

			System.Data.SqlClient.SqlDataReader reader = db.getDataReader();

			tab.cell.BgColor = "navy";
			tab.cell.Text.Color = "ffffff";
			tab.cell.Text.Bold = true;
			
			tab.BeginRow();

			tab.AddCell("Trámite ID" );
			tab.AddCell( "Trámite" );
			tab.AddCell( "Situación ID");
			tab.AddCell( "Situación");
			tab.AddCell( "Sit. Sgte. ID");
			tab.AddCell( "Destino");

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
				tab.AddCell( ObjConvert.AsString( reader["TramiteID"]) );
				tab.AddCell( ObjConvert.AsString( reader["T_Orig"] ) );
				tab.AddCell( ObjConvert.AsString( reader["TramiteSitID"]	) );
				tab.AddCell( ObjConvert.AsString( reader["Origen"]) );
				tab.AddCell( ObjConvert.AsString( reader["TramiteSitSgteID"]) );
				tab.AddCell( ObjConvert.AsString( reader["Destino"]) );
				tab.EndRow();
						
			}


			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

			txtFmt.Bold = true;
			txtFmt.Size = "3";

			Titulo = txtFmt.Html( Titulo );
			txtFmt.Size = "1";

			//Titulo += txtFmt.Html( " ( "+ this.txtFechaAlta.Text + " - " + this.txtFechaAlta_Max.Text + " ) ");
			Titulo += txtFmt.Html( " - REPORTE AL :  " + DateTime.Today.ToShortDateString() );

			this.HtmlPayLoad =  Titulo + "<br>" +  tab.Html();

			txtFmt.Size = "3";

			return Titulo + "<br>" +  tab.Html() + txtFmt.Html( MensajeError );

		}
		#endregion HtmlTramiteSitSgte

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

		protected void btnGenExcel_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
