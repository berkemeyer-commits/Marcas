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
using Berke.Marcas.WebUI.Helpers;
using  Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Home
{
	using Berke.Marcas.WebUI.Tools.Helpers;

	/// <summary>
	/// Summary description for MarcaDetalleLitigios.
	/// </summary>
	public partial class MarcaDetalleLitigios : System.Web.UI.Page
	{
		#region Atributos
		string boxPattern		= "<div id='{0}'><table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('{1}');\"><img id='img_{1}' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> {3} </td><td width='10' class='td_button'><a  onclick=\"closeDiv('{0}');\"> <img src=\"../Tools/imx/close.gif\" border=\"0\"></a></td></tr><tr><td><div id='{1}'> {2}</div></td></tr></table></div>";		
		#endregion Atributos
		#region Properties
		
		#region MarcaID
		private int MarcaID
		{
			get{ return ViewState["MarcaID"] == null ? -1 : Convert.ToInt32( ViewState["MarcaID"] ) ; }
			set{ ViewState["MarcaID"] = Convert.ToString( value );}
		}
		#endregion MarcaID

		#region ExpeID
		private int ExpeID
		{
			get{ return ViewState["ExpeID"] == null ? -1 : Convert.ToInt32( ViewState["ExpeID"] ) ; }
			set{ ViewState["ExpeID"] = Convert.ToString( value );}
		}
		#endregion ExpeID

		#region PaginaAnterior
		private string PaginaAnterior
		{
			get{ return ViewState["PaginaAnterior"] == null ? "" : Convert.ToString( ViewState["PaginaAnterior"] ) ; }
			set{ ViewState["PaginaAnterior"] = Convert.ToString( value );}
		}
		#endregion PaginaAnterior
		
		#endregion Properties
		
		#region Controles del Form
		#endregion Controles del Form

		#region Valores Iniciales
		private void ValoresIniciales()
		{
			Berke.DG.DBTab.Expediente expe = null;
		
			if( MarcaID != 0)
			{
				Berke.DG.DBTab.Marca marca = Berke.Marcas.UIProcess.Model.Marca.ReadByID( MarcaID );
				this.ExpeID = marca.Dat.ExpedienteVigenteID.AsInt;
				expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( this.ExpeID );
			}
			else
			{
				if( ExpeID != 0)
				{
					expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( ExpeID );
					this.MarcaID = expe.Dat.MarcaID.AsInt;
				}
			}

			mostrarDocLitigios(expe.Dat.ActaNro.AsInt, expe.Dat.ActaAnio.AsInt);
			

			lblMarcaBasic.Text = Berke.Marcas.UIProcess.Model.Marca.BasicDataAsHTL( MarcaID, true );

			#region Motivo de Standby
			string motivo = MotivoDeStandBy(MarcaID);
			if( motivo != "" ){
				motivo = @"<font color=""#FF0000"">( " + motivo + @" )</font>";
				lblMarcaBasic.Text = lblMarcaBasic.Text.Replace("Standby", "StandBy "+motivo);
			}
			#endregion Motivo de Standby


			lblExpedienteBasic.Text = Berke.Marcas.UIProcess.Model.Expediente.BasicDataAsHTL( ExpeID , true );

			this.lblInstrucciones.Text = Instrucciones();
			this.lblMerge.Text = Merge();
			this.lblCambioEstado.Text	= CambioEstado();

			/* [rgimenez] : Oculto lo que es de Correspondencia */
			lblCorresp.Visible  = false;
			lnkCorresp.Visible	= false;

			mostrarJerarquia();
			MostrarHistorico();
			//mostrarCorrespondencia();  //rgimenez
			mostrarCambiosDeCliente();
			//mostrarCorrespondencia();  //rgimenez
			mostrarDatosDeBoletin();
		}
		#endregion Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			#region Not IsPostBack
			if( !IsPostBack )
			{
				this.chkCascada.Visible = false;
				if( HttpContext.Current.Request.UrlReferrer != null )
				    this.PaginaAnterior = HttpContext.Current.Request.UrlReferrer.PathAndQuery;
				else
					this.PaginaAnterior = HttpContext.Current.Request.Path;
				MarcaID = 0;
				string MarcaID_str = UrlParam.GetParam("MarcaID");
				if( MarcaID_str != "" )
				{ 
					MarcaID = Convert.ToInt32( MarcaID_str );
				}
				string ExpeID_str = UrlParam.GetParam("ExpeID");
				if( ExpeID_str != "" )
				{ 
					ExpeID = Convert.ToInt32( ExpeID_str );
				}

				ValoresIniciales();
			}
			#endregion Not IsPostBack
		}
		#endregion Page_Load

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

		protected void lnkJerarquia_Click(object sender, System.EventArgs e)
		{	
			mostrarJerarquia();
			this.lnkJerarquia.Visible = false;
			//			this.pnlJerarquia.Visible = true;
			//			this.lblJerarquia.Text = Berke.Marcas.UIProcess.Model.Expediente.Jerarquia( ExpeID );
		}
		#region Cambios de Estado
		private string CambioEstado()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			
			#region Leer
			Berke.DG.DBTab.Expediente_Pertenencia expP = new Berke.DG.DBTab.Expediente_Pertenencia( db );
			expP.Dat.ExpedienteID.Filter = this.ExpeID;
			expP.Dat.Fecha.Order = 1;
			expP.Adapter.ReadAll();
			string comando = expP.Adapter.ReadAll_CommandString();
			#endregion Leer
			if( expP.RowCount < 1 )
			{
				return "";
			}
			Berke.DG.DBTab.PertenenciaMotivo mot = new Berke.DG.DBTab.PertenenciaMotivo( db );
			Berke.DG.ViewTab.vFuncionario fun = new Berke.DG.ViewTab.vFuncionario( db );
			Berke.DG.DBTab.CAgenteLocal agLoc = new Berke.DG.DBTab.CAgenteLocal( db );

			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha");
			tab.AddCell("Pasa a");
			tab.AddCell("Motivo");
			tab.AddCell("Comentario");
			tab.AddCell("Ag.Local");	
			tab.AddCell("Usuario");	
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));		

			for( expP.GoTop(); ! expP.EOF; expP.Skip())
			{
				mot.Adapter.ReadByID( expP.Dat.PertenenciaMotivoID.Value );
				fun.Dat.ID.Filter = expP.Dat.FuncionarioID.AsInt;
				fun.Adapter.ReadAll();
				agLoc.Adapter.ReadByID( expP.Dat.AgenteLocalID.Value );
				string pasaA = "";
				if( ! expP.Dat.Vigilada.IsNull  )
				{
					if( expP.Dat.Vigilada.AsBoolean  )
					{
						pasaA +="Vigilada ";
					}
					else
					{
						pasaA += "NO Vigilada ";
					}
				}


				if( ! expP.Dat.Nuestra.IsNull  )
				{
					if( expP.Dat.Nuestra.AsBoolean  )
					{
						pasaA +="Nuestra ";
					}
					else 
					{
						pasaA += "Terceros  ";
					}
				}


				if( !expP.Dat.Sustituida.IsNull )
				{
					if( expP.Dat.Sustituida.AsBoolean )
					{
						pasaA += "Sustituida ";					
					}
					else
					{
						pasaA += "NO Sustituida ";										
					}
				}
				if( ! expP.Dat.StandBy.IsNull )	
				{
					if( expP.Dat.StandBy.AsBoolean )
					{
						pasaA += "StandBy ";					
					}
					else
					{
						pasaA += "NO StandBy ";	
					}
				}
				
				
				tab.BeginRow();

				tab.AddCell(  chkSpc( expP.Dat.Fecha.AsString ));
				tab.AddCell(  chkSpc( pasaA ));
				tab.AddCell(  chkSpc( mot.Dat.Descrip.AsString ));
				tab.AddCell(  chkSpc( expP.Dat.Obs.AsString ));
				tab.AddCell(  chkSpc( agLoc.Dat.Nombre.AsString ));
				tab.AddCell(  chkSpc( fun.Dat.NombreCorto.AsString ));

				tab.EndRow();
			}
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//return txtFmt.Html("<p>"+ "Cambios de Estado")+"<br>"+ tab.Html()+"</p>";
			//return  "<table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divCambioEstado');\"><img id='img_divCambioEstado' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Cambios de Estado</td></tr><tr><td><div id='divCambioEstado'>"+ tab.Html()+"</div></td></tr></table>";
			return string.Format(boxPattern, "cambiosEstadoBox", "divCambiosEstado", tab.Html(), "Cambios de Estado");
		
		}
		#endregion Cambios de Estado



		#region Motivo de StandBy
		private string  MotivoDeStandBy( int MarcaID )
		{
			string ret = "";

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca(db);
			mar.Adapter.ReadByID( MarcaID );

			Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );
			expeSit.Dat.ExpedienteID	.Filter	=	this.ExpeID;
			expeSit.Dat.SitStandBy		.Filter = true;
			expeSit.Dat.SituacionFecha.Order	= 1;
			expeSit.Adapter.ReadAll();
			for( expeSit.GoTop(); ! expeSit.EOF; expeSit.Skip() )
			{
				if( ret != "") ret+= ",";
			    ret+= expeSit.Dat.Abrev.AsString;
			}
			return ret;
		}
		#endregion 

		#region MostrarHistorico
		private void MostrarHistorico()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("hist");			
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("hist_header"));
			
			
			#region Cabecera

			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Situación");
			tab.AddCell("Fecha");
			tab.AddCell("Observaciones");
			/*
			 * BUG#289
			  tab.AddCell("Fin Plazo");
			  tab.AddCell("Usuario");
			  tab.AddCell("Fecha Alta");
			*/
				
			tab.EndRow();
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("hist"));

			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.MarcaRegRen regRen = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit( db );
			expe.Adapter.ReadByID( this.ExpeID );
			regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );

			Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );
			expeSit.Dat.ExpedienteID	.Filter	=	this.ExpeID;

			/* [BUG#125]
			 * Mostrar las situaciones ordenadas por la fecha de situacion
			 */
			//expeSit.Dat.Orden.Order				= 1;
			expeSit.Dat.SituacionFecha.Order	= 1;
			//			string tmp = expeSit.Adapter.ReadAll_CommandString();

			expeSit.Adapter.ReadAll();
			

			for( expeSit.GoTop(); ! expeSit.EOF; expeSit.Skip() )
			{
				string docRef = "";
				trmSit.Adapter.ReadByID( expeSit.Dat.TramiteSitID.Value );
				switch( trmSit.Dat.SituacionID.AsInt )
				{
						#region Presentada
					case (int)GlobalConst.Situaciones.PRESENTADA : // Presentada

						int hj = 0;
						if ( expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO || expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) 
						{
							hj = (int)GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA ;
						}
						docRef =  Berke.Libs.Base.DocPath.digitalDocPath(
							expe.Dat.ActaAnio		.AsInt,
							expe.Dat.ActaNro		.AsInt,
							hj // Hoja Descriptiva
							);
						/*docRef =  Berke.Marcas.BizActions.Lib.digitalDocPath(
							expe.Dat.ActaAnio		.AsInt,
							expe.Dat.ActaNro		.AsInt,
							hj // Hoja Descriptiva
							);*/


						if( expeSit.Dat.Datos.AsString.Trim() == "")
						{
							docRef = "Acta:  " + expe.Dat.ActaNro.AsString+ 
								"/" + expe.Dat.ActaAnio.AsString+ docRef;
						}
						else
						{
							docRef = expeSit.Dat.Datos.AsString + docRef;
						}
						break;
						#endregion Presentada

						#region Publicada
					case (int)GlobalConst.Situaciones.PUBLICADA : // Publicada
						docRef = Berke.Libs.Base.DocPath.digitalDocPath(
							expe.Dat.PublicAnio.AsInt,
							expe.Dat.PublicPag.AsInt,
							(int)GlobalConst.DocumentoTipo.PUBLICACION ); // 6- Publicacion
						/*docRef = Berke.Marcas.BizActions.Lib.digitalDocPath(
							expe.Dat.PublicAnio.AsInt,
							expe.Dat.PublicPag.AsInt,
							6 ); // 6- Publicacion*/


						if( expeSit.Dat.Datos.AsString.Trim() == "")
						{
							docRef = " Pág/Año: "+
								expe.Dat.PublicPag.AsString +"/"+
								expe.Dat.PublicAnio.AsString + " "+ docRef;
						}
						else
						{
							docRef = expeSit.Dat.Datos.AsString + docRef;
						}
						break;
						#endregion Publicada

						#region Concedida
					case (int)GlobalConst.Situaciones.CONCEDIDA : // Concedida
						docRef = Berke.Libs.Base.DocPath.digitalDocPath(
							regRen.Dat.RegistroAnio	.AsInt,
							regRen.Dat.RegistroNro	.AsInt,
							(int)GlobalConst.DocumentoTipo.TITULO // Titulo
							);
						/*docRef = Berke.Marcas.BizActions.Lib.digitalDocPath(
							regRen.Dat.RegistroAnio	.AsInt,
							regRen.Dat.RegistroNro	.AsInt,
							8 // Titulo
							);*/

						if( expeSit.Dat.Datos.AsString.Trim() == "")
						{
							docRef = "Registro: "+regRen.Dat.RegistroNro.AsString+
								"/"+regRen.Dat.RegistroAnio.AsString+ " "+docRef;
						}
						else
						{
							docRef = expeSit.Dat.Datos.AsString + docRef;
						}
						
						break;
						#endregion Concedida

						#region Archivada
					case (int)GlobalConst.Situaciones.ARCHIVADA : // Archivada
						if( expeSit.Dat.Datos.AsString.Trim() == "")
						{
							docRef = "Bib/Exp: " + expe.Dat.Bib.AsString+"/"+expe.Dat.Exp.AsString;
						}
						else
						{
							docRef = expeSit.Dat.Datos.AsString;
						}
						break;
						#endregion Archivada
				}// end switch
				string obs = expeSit.Dat.Obs.AsString.Trim();
				if(  obs == "" )
				{
					obs = docRef;
				}
				else if( docRef != "")
				{
					obs = docRef +"<br>" + obs;
				}
				string vencimiento = "";
				if( ! expeSit.Dat.VencimientoFecha.IsNull)
				{
					vencimiento = expeSit.Dat.VencimientoFecha.AsString; 
					vencimiento = vencimiento.Replace("01/01/1900","");
				}
				tab.BeginRow();
				tab.AddCell( chkSpc( expeSit.Dat.Descrip.AsString  ));
				tab.AddCell( chkSpc( expeSit.Dat.SituacionFecha.AsString ));
				tab.AddCell( chkSpc( obs ));
				/*
				 * BUG#289
				tab.AddCell( chkSpc(  vencimiento ));
				tab.AddCell( chkSpc( expeSit.Dat.Nombre.AsString ));
				tab.AddCell( chkSpc( expeSit.Dat.AltaFecha.AsString ));
				*/
				tab.EndRow();
			}

			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			lblHistorico.Text       = string.Format(boxPattern, "sitBox", "divSituaciones", tab.Html(), "Histórico de Situaciones");
			//lblHistorico.Text		= "<div id='sitBox'><table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divSituaciones');\"><img src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Histórico de Situaciones</td><td align='right'><a  onclick=\"closeDiv('sitBox');\"> <img id='img_divSituaciones' src=\"../Tools/imx/close.gif\" border=\"0\"></a></td></tr><tr><td><div id='divSituaciones'>"+ tab.Html()+"</div></td></tr></table></div>";
			lblHistorico.Visible	= true;
			lnkHistorico.Visible	= false;
			db.CerrarConexion();
		}
		#endregion MostrarHistorico

		#region Linkbutton1_Click
		protected void Linkbutton1_Click(object sender, System.EventArgs e)
		{
			MostrarHistorico();
		}
		#endregion Linkbutton1_Click


		#region Correspondencia
		protected void lnkCorresp_Click(object sender, System.EventArgs e)
		{
			mostrarCorrespondencia();
		}
		#endregion 

		#region Correspondencia
		private void mostrarCorrespondencia( )
		{
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha");
			tab.AddCell("Nro.");
			tab.AddCell("Ref.Externa");
			tab.AddCell("Ref.Correspondencia");
				
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));
			Berke.DG.ViewTab.vExpeCorresp view = new Berke.DG.ViewTab.vExpeCorresp(db);
			view.Dat.ID.Filter = this.ExpeID;
			//			view.Dat.FechaAlta.Order = 1;
			view.Dat.CorrespAnio.Order	= 1;
			view.Dat.CorrespNro.Order	= 2;
			view.Adapter.ReadAll();
			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				string path = "";
				if( view.Dat.CorrespNro.AsString != "" )
				{
					path = Berke.Libs.Base.DocPath.digitalDocPath(
						view.Dat.CorrespAnio.AsInt, view.Dat.CorrespNro.AsInt, 
						1 ); // Corresp del area de Marcas
					/*path = Berke.Marcas.UIProcess.Model.Corresp.digitalDocPath(
						view.Dat.CorrespAnio.AsInt, view.Dat.CorrespNro.AsInt, 
						1 ); // Corresp del area de Marcas*/
				}

				string numero = view.Dat.CorrespNro.AsString+" / "+view.Dat.CorrespAnio.AsString+path;
				string resCorresp = view.Dat.RefCorresp.AsString;
				if( view.Dat.RefCliente.AsString.Trim() != "")
				{
					resCorresp+="<br>"+ view.Dat.RefCliente.AsString.Trim();
				}
				if( view.Dat.Obs.AsString.Trim() != "")
				{
					resCorresp+="<br>"+ view.Dat.Obs.AsString.Trim();
				}

				
				tab.BeginRow();

				tab.AddCell( view.Dat.FechaAlta.AsString );
				tab.AddCell( numero );
				tab.AddCell( view.Dat.ReferenciaExterna.AsString );
				tab.AddCell( resCorresp);

				tab.EndRow();
			}
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//lblCorresp.Text		= "<table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divCorrespondencia');\"><img id='img_divCorrespondencia' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Correspondencia </td></tr><tr><td><div id='divCorrespondencia'>"+ tab.Html()+"</div></td></tr></table>";
			lblCorresp.Text         = string.Format(boxPattern, "correspBox", "divCorrespondencia", tab.Html(), "Correspondencia");
			if( view.RowCount > 0 )
			{
				lblCorresp.Visible	= true;
				lnkCorresp.Visible	= false;
			}
			else
			{
				lblCorresp.Visible	= false;
				lnkCorresp.Visible	= false;
			}
		
		}
		#endregion Correspondencia
	
		#region Instrucciones
		private string Instrucciones()
		{
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Fecha");
			tab.AddCell("Instrucción");
			tab.AddCell("Comentario");
			tab.AddCell("Correspondencia");
			tab.AddCell("Usuario");	
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));
		
			Berke.DG.ViewTab.vInstruccion view = new Berke.DG.ViewTab.vInstruccion(db);
			view.Dat.ExpedienteID.Filter = this.ExpeID;
			view.Dat.CorrespAnio.Order = 1;
			view.Dat.CorrespNro.Order  = 2;
			view.Adapter.ReadAll();
			if( view.RowCount < 1 )
			{
				return "";
			}

			Berke.DG.ViewTab.vCorrespNro correspnroarea = new Berke.DG.ViewTab.vCorrespNro(db);

			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				string path = "";
				if( view.Dat.CorrespNro.AsString != "" )
				{
					correspnroarea.Adapter.ClearParams();
					correspnroarea.Adapter.AddParam("nro",view.Dat.CorrespNro.AsInt);
					correspnroarea.Dat.vigente.Filter = true;
					correspnroarea.Adapter.ReadAll();
					
					if ( correspnroarea.RowCount == 1) 
					{
						if ((correspnroarea.Dat.IDArea.AsInt != 0 ))
						{
							path = Berke.Libs.Base.DocPath.digitalDocPath(
								view.Dat.CorrespAnio.AsInt, view.Dat.CorrespNro.AsInt, 
								correspnroarea.Dat.IDArea.AsInt ); // Corresp del area de Marcas
						}
					}
					
				}

				
				tab.BeginRow();

				tab.AddCell(  chkSpc( view.Dat.Fecha.AsString ));
				tab.AddCell(  chkSpc( view.Dat.Descrip.AsString ));
				tab.AddCell(  chkSpc( view.Dat.Obs.AsString ));
				tab.AddCell(  chkSpc( view.Dat.CorrespNro.AsString + " / " + view.Dat.CorrespAnio.AsString +path ));
				tab.AddCell(  chkSpc( view.Dat.Nick.AsString));

				tab.EndRow();
			}
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//return "<table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divInstrucciones');\"><img id='img_divInstrucciones' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Instrucciones </td></tr><tr><td><div id='divInstrucciones'>"+ tab.Html()+"</div></td></tr></table>";
			return string.Format(boxPattern, "instBox", "divInstrucciones", tab.Html(), "Instrucciones");
		
		}
		#endregion Instrucciones


		#region Ver Merge

		
		private string Merge()
		{
		
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

            MergeTools	mtools =	new MergeTools();
			
			return (mtools.obtMergeTable(db, this.ExpeID)); 
		
		}
		
		#endregion


		private void mostrarJerarquia()
		{
			this.lnkJerarquia.Visible = false;
			//this.pnlJerarquia.Visible = true;
			this.lblJerarquia.Text = Berke.Marcas.UIProcess.Model.Expediente.Jerarquia( ExpeID );
			this.lblJerarquia.Text = string.Format(boxPattern, "jerBox", "divJerarquia", lblJerarquia.Text, "Historial de la Marca");
		}
		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}

		#region CambioCliente
		protected void lnkCambioCLiente_Click(object sender, System.EventArgs e)
		{
			mostrarCambiosDeCliente();
		}


		private void mostrarCambiosDeCliente()
		{
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();


			tab.AddCell("Fecha");
			tab.AddCell("Cliente Anterior");
			tab.AddCell("Observación");
			tab.AddCell("Usuario");
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));

			Berke.DG.ViewTab.vFuncionario fun = new Berke.DG.ViewTab.vFuncionario( db );
			Berke.DG.DBTab.Cliente cli  = new Berke.DG.DBTab.Cliente( db );
			Berke.DG.DBTab.Expediente_ClienteCambio view = new Berke.DG.DBTab.Expediente_ClienteCambio(db);
			view.Dat.ExpedienteID.Filter = this.ExpeID;
			view.Dat.CambioFecha.Order = 1;
			view.Adapter.ReadAll();
			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				cli.Adapter.ReadByID( view.Dat.ClienteAntID.Value );
				fun.Dat.ID.Filter = view.Dat.FuncionarioID.AsInt;
				fun.Adapter.ReadAll();

				tab.BeginRow();
				tab.AddCell(  chkSpc( view.Dat.CambioFecha.AsString ));
				tab.AddCell(  chkSpc( cli.Dat.Nombre.AsString + "("+cli.Dat.ID.AsString+")" ));
				tab.AddCell(  chkSpc( view.Dat.Obs.AsString ));
				tab.AddCell(  chkSpc( fun.Dat.NombreCorto.AsString ));

				tab.EndRow();
			}
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//this.lblCambioCLiente.Text =  txtFmt.Html("Cambios de Cliente")+"<br>"+ tab.Html();
			//this.lblCambioCLiente.Text =  "<table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divCambioCliente');\"><img id='img_divCambioCliente' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Cambios de Cliente </td></tr><tr><td><div id='divCambioCliente'>"+ tab.Html()+"</div></td></tr></table>";
			this.lblCambioCLiente.Text   =  string.Format(boxPattern,"cambioClienteBox", "divCambioCliente", tab.Html(), "Cambios de Cliente");
			if( view.RowCount > 0 )
			{
				lblCambioCLiente.Visible	= true;
				lnkCambioCLiente.Visible	= false;
			}
			else
			{
				lblCambioCLiente.Visible	= false;
				lnkCambioCLiente.Visible	= false;
			}
		}
		#endregion CambioCliente

		#region btnEliminar_Click
		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
		
			ArrayList lst = new ArrayList();
			getExpeList(this.ExpeID, ref lst, db );
			
			db.IniciarTransaccion();
			if( chkCascada.Checked )
			{
				foreach( Object obj in lst )
				{
					int expedId = (int) obj;
					EliminarUnExpediente( expedId, db );
				}	
			}
			else
			{
				EliminarUnExpediente( this.ExpeID, db );
			}
			db.Commit();
			
			ShowMessage(" LA INFORMACION FUE ELIMINADA " );
			if( this.PaginaAnterior != "" )
			{
				Response.Redirect( this.PaginaAnterior, true );
			}
		}
		#endregion btnEliminar_Click

		#region EliminarUnExpediente
		private void EliminarUnExpediente( int expeID, Berke.Libs.Base.Helpers.AccesoDB  db )
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			expe.Adapter.ReadByID( expeID );
			int marcaID = expe.Dat.MarcaID.AsInt;
			int marRegRenID = expe.Dat.MarcaRegRenID.AsInt;
			string strExpeID = expeID.ToString();
			string strMarcaID = marcaID.ToString();
	

			#region Anular Claves Foraneas de relaciones cruzadas	
			db.Sql = "update expediente set  MarcaID = null where id = "+ strExpeID;
			db.EjecutarDML();

			db.Sql = "update expediente set marcaregrenid = null where id = "+ strExpeID;
			int mod = db.EjecutarDML();

			db.Sql = "update Marca set ExpedienteVigenteID = null where ExpedienteVigenteID = "+ strExpeID;
			db.EjecutarDML();

			#endregion

			#region Anular claves de Expedientes Que hacen referencia a este
			db.Sql = "Update Expediente SET ExpedienteID = null  WHERE ExpedienteID = "+ strExpeID;
			db.EjecutarDML();
			#endregion 

			#region Verificar si hay algun expediente que tambien hace referencia a la marca
			db.Sql = "select count(*) from Expediente where MarcaID = "+ strMarcaID;
			int cantMarcasDependientes = (int) db.getValue();
			#endregion

			#region Eliminar Registros dependientes de Expediente
			eliminarRegistro( "Expediente_ClienteCambio"	,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_Documento"		,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_DocumentoTipo"	,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_Instruccion"		,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_Pendiente"		,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_Pertenencia"		,"ExpedienteID", strExpeID, db );
//			eliminarRegistro( "ExpedienteXPoder"			,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "Expediente_Situacion"		,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "ExpedienteCampo"				,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "ExpedienteXPoder"			,"ExpedienteID", strExpeID, db );
			eliminarRegistro( "ExpedienteXPropietario"		,"ExpedienteID", strExpeID, db );

	
			eliminarRegistro( "marcaregren"		,"expedienteid",strExpeID, db );
			#endregion
 
			if ( cantMarcasDependientes == 0 )
			{
				#region Eliminar Registros dependientes de Marca 
				eliminarRegistro( "marca_claseidioma"	,"MarcaID", strMarcaID, db );
				eliminarRegistro( "PropietarioXMarca"	,"MarcaID", strMarcaID, db );
				#endregion Eliminar Marca 

				#region Eliminar Marca
				
				#region Anular MarcaID de ExpedienteInstruccion
				db.Sql = "Update Expediente_Instruccion SET marcaID = null  WHERE marcaID = "+ strMarcaID;
				db.EjecutarDML();
				#endregion 

				eliminarRegistro( "marca"				,"ID", strMarcaID, db );
				#endregion Eliminar Marca
			}

			#region Eliminar Expediente
			eliminarRegistro( "expediente"	,"id", strExpeID, db );
			#endregion

		
		}
		#endregion EliminarUnExpediente

		#region getExpeList
		private void getExpeList( int expeID, ref ArrayList lst, Berke.Libs.Base.Helpers.AccesoDB  db ){
		
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			expe.Dat.ExpedienteID.Filter = expeID;
			expe.Adapter.ReadAll( 200 );
			for( expe.GoTop(); !expe.EOF; expe.Skip() )
			{
				getExpeList( expe.Dat.ID.AsInt, ref lst, db );
			}
			lst.Add( expeID );
		}
		#endregion getExpeList

		#region eliminarRegistro()
		private void eliminarRegistro( string tabla, string campoID, string valID, Berke.Libs.Base.Helpers.AccesoDB  db )
		{
			db.Sql = "delete from "+ tabla + " where "+campoID + " = "+ valID;
			db.EjecutarDML();
		}
		#endregion eliminarRegistro()

		private void lnkEliminar_Click(object sender, System.EventArgs e)
		{
			btnEliminar.Visible = true;
			this.chkCascada.Visible = true;
		    //lnkEliminar.Visible = false;
		}
		
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion ShowMessage

		#region Datos de Boletin
		protected void lnkBoletin_Click(object sender, System.EventArgs e)
		{
			mostrarDatosDeBoletin();
		}
		private void mostrarDatosDeBoletin()
		{
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("bol");

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			tab.setCellFormater(new Berke.Html.HtmlCellFormater("bol_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Bol.");
			tab.AddCell("Tram");
			tab.AddCell("Fecha");
			tab.AddCell("Acta");
			tab.AddCell("Clase");
			tab.AddCell("T");
			tab.AddCell("Denominacion");
			tab.AddCell("Propietario");
			tab.AddCell("Agen");
			tab.AddCell("Ref.Acta");
			tab.AddCell("Ref.Reg.");
			tab.AddCell("Obs.");
			tab.EndRow();
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("bol"));			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
		
			Berke.DG.DBTab.BoletinDet det = new Berke.DG.DBTab.BoletinDet( db );
			det.Dat.ExpedienteID.Filter = this.ExpeID;
			det.Dat.SolicitudFecha.Order = 1;
			det.Adapter.ReadAll();
			for( det.GoTop(); ! det.EOF; det.Skip())
			{
				/*
				string path= Berke.Marcas.BizActions.Lib.digitalDocPath( 
					det.Dat.ExpAnio.AsInt,
					det.Dat.ExpNro.AsInt, 
					0 );
				*/

				
				string path = Berke.Libs.Base.DocPath.digitalDocPath( det.Dat.ExpAnio.AsInt,
					                                                  det.Dat.ExpNro.AsInt, 
					                                                  det.Dat.Tramite.AsString.Trim());

				tab.BeginRow();
				tab.AddCell(  chkSpc( det.Dat.BolNro.AsString + "/" + det.Dat.BolAnio.AsString));
				tab.AddCell(  chkSpc( "<b>"+ det.Dat.Tramite.AsString +"</b>" ));
				tab.AddCell(  chkSpc( det.Dat.SolicitudFecha.AsString ));
				tab.AddCell(  chkSpc( det.Dat.ExpNro.AsString + "/"+det.Dat.ExpAnio.AsString+ path));
				tab.AddCell(  chkSpc( det.Dat.Clase.AsString ));
				tab.AddCell(  chkSpc( det.Dat.MarcaTipo.AsString ));
				tab.AddCell(  chkSpc( det.Dat.Denominacion.AsString ));
				tab.AddCell(  chkSpc( det.Dat.Propietario.AsString + " ("+det.Dat.Pais.AsString +")"));
				tab.AddCell(  chkSpc( det.Dat.AgenteLocal.AsString ));
				tab.AddCell(  chkSpc( det.Dat.RefNro.AsString + "/"+det.Dat.RefAnio.AsString  ));
				tab.AddCell(  chkSpc( det.Dat.RefRegNro.AsString +"/"+det.Dat.RefRegAnio.AsString  ));
				tab.AddCell(  chkSpc( det.Dat.Obs.AsString ));
				
			
				tab.EndRow();
			}
			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//this.lblBoletin.Text =  "<table width=\"750\" class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('divBoletin');\"><img id='img_divBoletin' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> Datos del Bolet&iacute;n</td><tr><td><div id='divBoletin'>"+ tab.Html()+"</div></td></tr></table>";
			lblBoletin.Text        = string.Format(boxPattern, "boletinBox", "divBoletin", tab.Html(), "Datos del Bolet&iacute;n");
		
			if( det.RowCount > 0 )
			{
				lblBoletin.Visible	= true;
				lnkBoletin.Visible	= false;
			}
			else
			{
				lblBoletin.Visible	= false;
				lnkBoletin.Visible	= false;
	
			}

		}
		#endregion Datos de Boletin

		#region Mostrar documentos de litigios
		public void mostrarDocLitigios(int actanro, int actaanio)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			lblDocLitigios.Text = "";
			string [] path = Berke.Libs.Base.DocPath.getPathCedulas( actaanio, actanro, db);
			
			if (path != null)
			{
				Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("bol");
				tab.setCellFormater(new Berke.Html.HtmlCellFormater("bol_header"));

				#region Cabecera
				tab.BeginRow();
				tab.AddCell("Tipo de Documento");
				tab.AddCell("Documento");
				tab.EndRow();
				#endregion Cabecera
				
				tab.setCellFormater(new Berke.Html.HtmlCellFormater("bol"));

				for (int i = 0; i< path.Length ; i++ )
				{
					tab.BeginRow();
					tab.AddCell("C&eacute;dula");
					tab.AddCell(path[i]);
					tab.EndRow();
				}

				db.CerrarConexion();
				if (path.Length>0)
				{
					lblDocLitigios.Visible = true;
					lblDocLitigios.Text = string.Format(boxPattern, "docBox", "divDocLitigios", tab.Html(), "Documentos de Litigios");
				}
			}
		}
		#endregion Mostrar documentos de litigios

		private void myLink_Click(object sender, System.EventArgs e)
		{
			/**/
		}

		

	} // -------
}
