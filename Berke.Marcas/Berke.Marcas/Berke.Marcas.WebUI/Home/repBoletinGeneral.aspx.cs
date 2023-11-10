

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
	public partial class repBoletinGeneral : System.Web.UI.Page

	{
		private static string TR_NUESTRO = "2";
		private static string TR_TERCERO = "3";
		private static int NCOLS = 10;

		#region Controles del Form

		#endregion Controles del Form

		#region Properties

			#region HtmlPayLoad
			private string HtmlPayLoad
			{
				get{ return Convert.ToString(( Session["HtmlPayLoad"] == null )? "" : Session["HtmlPayLoad"] ) ; }
				set{ Session["HtmlPayLoad"] = Convert.ToString( value );}
			}
			#endregion Html


		#endregion Properties

		
		#region Generado por el designer

			protected void Page_Load(object sender, System.EventArgs e)
			{
				if( ! this.IsPostBack )
				{
					ValoresIniciales();
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
				#endregion Web Form Designer generated code

			#endregion Generado por el designer

		#region ValoresIniciales
			private void ValoresIniciales()
			{
					
			}
		#endregion ValoresIniciales


		#region HtmlBoletinGeneral
		private string HtmlBoletinGeneral( Berke.DG.DBTab.BoletinDet vBol )
		
		{
		
			string pertenencia = "";
			string listado = "Listado General de Trámites";
			string tramite = "";
		

			#region Cabecera del Table HTML
			
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			tab.cell.Text.Size = "-1";
			
			tab.BeginRow();

			tab.AddCell("Fecha");
			tab.AddCell("Boletín");
			tab.AddCell("Exp. No.");
			tab.AddCell("Clase");
			tab.AddCell("DFM");
			tab.AddCell("Denominación");
			tab.AddCell("Propietario");
			tab.AddCell("País");
			tab.AddCell("Agente");
			tab.AddCell("Tram");
			tab.AddCell("Ref.Acta");
			tab.AddCell("Ref.Reg");				
			tab.AddCell("H.Desc.");	
			tab.AddCell("Fec. Pub.");
			tab.AddCell("Vto. Pub.");
			tab.AddCell("Pub.");
			tab.AddCell("Obs.");
			
			tab.EndRow();
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));
			tab.setTextFormater(new Berke.Html.HtmlTextFormater("_none_"));
					
			
			#endregion Cabecera del Table HTML

			
			vBol.Adapter.DataReader_Init();		
			// modificado por mbaez,  se especifica el formato de la 
			// fecha, para que no dependa de la config. del servidor.
			System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
			DateTime fechamin = Convert.ToDateTime("31/01/2050", culture);	
			DateTime fechamax = Convert.ToDateTime("01/01/1900", culture);
			string FechaMin = "";
			string FechaMax = "";
			int contador = 0;//, conREG=0, conREN=0, conOP=0, conTRA=0, conFUS=0, conNOM=0, conDOM=0, conLIC=0, conDUP=0, conPOD=0;
			
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(vBol.Adapter.Db);
			Berke.DG.DBTab.Expediente_Situacion expesitu = new Berke.DG.DBTab.Expediente_Situacion(vBol.Adapter.Db);
			Berke.DG.DBTab.Tramite_Sit tramsit = new Berke.DG.DBTab.Tramite_Sit(vBol.Adapter.Db);
			string fechapub = "";
			string fechavtopub = "";
			string pathpub = "";

			while ( vBol.Adapter.DataReader_Read() )
			{			

				#region Agrega Datos
						
				{
					tab.BeginRow();

					tab.AddCell( chkSpc( vBol.Dat.SolicitudFecha.AsString	));
						DateTime fecha = DateTime.Parse(vBol.Dat.SolicitudFecha.AsString);

					if  (fecha < fechamin)
					{
						FechaMin = fecha.ToShortDateString();
						fechamin = fecha;
					}
					if (fecha > fechamax)
					{
						FechaMax = fecha.ToShortDateString();
						fechamax = fecha;
					} 

					tab.AddCell( chkSpc( vBol.Dat.BolNro.AsString+" /"+vBol.Dat.BolAnio.AsString		));
					tab.AddCell( chkSpc( vBol.Dat.ExpNro.AsString ));
					tab.AddCell( chkSpc( vBol.Dat.Clase.AsString ));
					tab.AddCell( chkSpc( vBol.Dat.MarcaTipo.AsString ));
					tab.AddCell( chkSpc( vBol.Dat.Denominacion.AsString		));
					tab.AddCell( chkSpc( vBol.Dat.Propietario.AsString		));
					tab.AddCell( chkSpc( vBol.Dat.Pais.AsString 		) );
					tab.AddCell( chkSpc( vBol.Dat.AgenteLocal.AsString		));
					tab.AddCell(  chkSpc(  vBol.Dat.Tramite.AsString 	));
					
			
					
					# region incluir referencia Acta
					string refActa = vBol.Dat.RefNro.AsString;
					if (vBol.Dat.RefAnio.AsInt !=  0 ){
							refActa+= "/" + vBol.Dat.RefAnio.AsString;
					}
					tab.AddCell(chkSpc( refActa ));
				
					# endregion 

					# region incluir referencia Registro
					string refReg = vBol.Dat.RefRegNro.AsString;
					if (vBol.Dat.RefRegAnio.AsInt != 0)
					{
						refReg+= "/" + vBol.Dat.RefRegAnio.AsString;
					}
					tab.AddCell(chkSpc( refReg ));

					
					tab.AddCell(chkSpc( Berke.Libs.Base.DocPath.digitalDocPath(vBol.Dat.ExpAnio.AsInt,vBol.Dat.ExpNro.AsInt, vBol.Dat.Tramite.AsString.Trim() ) ));
					# endregion
					
					#region Datos Publicación
					expe.ClearFilter();
					expe.Dat.ActaNro.Filter = vBol.Dat.ExpNro.AsInt;
					expe.Dat.ActaAnio.Filter = vBol.Dat.ExpAnio.AsInt;
					expe.Adapter.ReadAll();

					if (expe.RowCount > 0)
					{
						tramsit.ClearFilter();
						tramsit.Dat.TramiteID.Filter = expe.Dat.TramiteID.AsInt;
						tramsit.Dat.SituacionID.Filter = Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
						tramsit.Adapter.ReadAll();

						expesitu.ClearFilter();
						expesitu.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
						expesitu.Dat.TramiteSitID.Filter = tramsit.Dat.ID.AsInt;
						expesitu.ClearOrder();
						expesitu.Dat.SituacionFecha.Order = -1;
						expesitu.Adapter.ReadAll();

						if (expesitu.RowCount > 0)
						{
							fechapub = expesitu.Dat.SituacionFecha.AsString;
							fechavtopub = expesitu.Dat.VencimientoFecha.AsString;
							pathpub = Berke.Libs.Base.DocPath.digitalDocPath(expe.Dat.PublicAnio.AsInt,
																			 expe.Dat.PublicPag.AsInt,
																			 Convert.ToInt32(Berke.Libs.Base.GlobalConst.DocumentoTipo.PUBLICACION));
						}
					}

					tab.AddCell(chkSpc(fechapub));
					tab.AddCell(chkSpc(fechavtopub));
					tab.AddCell(chkSpc(pathpub));

					fechapub = "";
					fechavtopub = "";
					pathpub = "";

					#endregion Datos Publicación

					tab.AddCell(chkSpc( vBol.Dat.Obs.AsString));
				
					 
	

					

					
					contador += 1;	
					tab.EndRow();
				}
		
				
				#endregion Agrega Datos
									
			}

			vBol.Adapter.DataReader_Close();

			if (contador > 0)
			{
				string Titulo = "" + "<table><tr><td><small>" + DateTime.Today.ToLongDateString() + " " + System.DateTime.Now.ToShortTimeString() + "</small><td></tr>" + "<tr ><td colspan=10 class=\"titulo\">Boletines - " + listado + " (" + pertenencia   
					+  tramite + " )</td></tr>" + "<tr ><td colspan=10 class=\"subtitulo\"> Del :   " + FechaMin + "  Al :  " + FechaMax +  "</td></tr></table>" ;
				Titulo = Titulo + "<p class=\"subtitulo\"> Registros:&nbsp;"+ contador +"</p>";
				
			
				Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();

				txtFmt.Bold = true;
				txtFmt.Size = "3";

				Titulo = txtFmt.Html( Titulo );
				txtFmt.Size = "1";

				return  Titulo + "<br>" +  tab.Html(); //+ "<b><font color='navy'>  Total de Registros por Trámite...  " + "REG=" + conREG + " - REN=" + conREN + " - OP=" + conOP + " - TRA=" + conTRA + " - FUS=" + conFUS + " - NOM=" + conNOM + " - LIC=" + conLIC +" - DUP=" + conDUP + " - POD=" + conPOD + "</b></font>";	
			}
			else
			{
				return "Error";
			}
		
			}
		
		#endregion HtmlBoletinGeneral


		#region Boton de Generar
		
		private int chkParametros() 
		{
			if( this.txtActa.Text == "" && this.txtActaAnio.Text == "" && this.txtAgLoc.Text == ""
				&& this.txtAnio.Text == "" && this.txtBoletinNro.Text == "" && this.txtDenom.Text == "" && this.txtFecha.Text == ""
				&& this.txtPais.Text == "" && this.txtPropietario.Text == "" && this.txtTramite.Text == ""
				&& this.txtProcAnio.Text.Trim().Length==0
				&& this.txtCarpeta.Text.Trim().Length==0
				&& this.txtRefActaNro.Text == ""
				&& this.txtRefActaAnho.Text == ""
				&& this.txtRefRegNro.Text == ""
				&& this.txtRefRegAnho.Text == ""
				&& this.txtObservacion.Text == ""
				&& this.txtClase.Text == ""
				)
			{
				return 1;
			} 
			else {  return 0 ; } 
		}


		#region obtener datos
		private  Berke.DG.DBTab.BoletinDet obtenerDatos() 
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				
			Berke.DG.DBTab.BoletinDet vBol = new Berke.DG.DBTab.BoletinDet( db );
			Berke.DG.DBTab.Boletin bolcab = new Berke.DG.DBTab.Boletin( db );

			#region Filtro
			object BoletinNro 	= ObjConvert.AsObject( txtBoletinNro.Text );
			object Anio 		= txtAnio.Text;
			object Fecha 		= txtFecha.Text;
			object Acta 		= ObjConvert.AsObject( txtActa.Text );
			object ActaAnio 	= ObjConvert.AsObject( txtActaAnio.Text );
			object Denom 		= txtDenom.Text;
			object Propietario 	= txtPropietario.Text;
			object Pais 		= txtPais.Text;
			object AgLoc 		= ObjConvert.AsObject( txtAgLoc.Text );
			object Tramite		= ObjConvert.AsObject( txtTramite.Text );

				    
			if (rbTramite.SelectedValue == TR_NUESTRO)
			{
                vBol.Adapter.SetDefaultWhere(" AgenteLocal in (select nromatricula from cagentelocal where nuestro = 1) ");
			}
			else if (rbTramite.SelectedValue == TR_TERCERO) 
			{
                vBol.Adapter.SetDefaultWhere(" AgenteLocal not in (select nromatricula from cagentelocal where nuestro = 1) ");
			}

			if (txtProcAnio.Text.Trim().Length>0 || txtCarpeta.Text.Trim().Length>0 )
			{
				bolcab.Dat.Anio.Filter			= ObjConvert.GetFilter( txtProcAnio.Text );
				bolcab.Dat.Nro.Filter			= ObjConvert.GetFilter( txtCarpeta.Text  );
				bolcab.Adapter.ReadAll();
				ArrayList lst =  new ArrayList();
				for (int i=0; i< bolcab.RowCount ; i++) 
				{
					lst.Add(bolcab.Dat.ID.AsInt);
				}
				vBol.Dat.BoletinID.Filter = new DSFilter(lst);

			}

			vBol.Dat.BolNro.Filter		    = ObjConvert.GetFilter( txtBoletinNro.Text );
			vBol.Dat.BolAnio.Filter		    = ObjConvert.GetFilter( txtAnio.Text );
			vBol.Dat.SolicitudFecha.Filter	= ObjConvert.GetFilter(  txtFecha.Text	);
			vBol.Dat.ExpNro.Filter		    = ObjConvert.GetFilter(  txtActa.Text	);
			vBol.Dat.ExpAnio.Filter			= ObjConvert.GetFilter( txtActaAnio.Text );
			vBol.Dat.Denominacion.Filter	= ObjConvert.GetFilter_Str( txtDenom.Text );
			vBol.Dat.Propietario.Filter		= ObjConvert.GetFilter_Str( txtPropietario.Text );
			vBol.Dat.Pais.Filter			= ObjConvert.GetFilter_Str( txtPais.Text );
						
			vBol.Dat.AgenteLocal.Filter		= ObjConvert.GetFilter(txtAgLoc.Text );
			vBol.Dat.Tramite.Filter			= ObjConvert.GetFilter( txtTramite.Text );
			vBol.Dat.RefAnio.Filter			= ObjConvert.GetFilter(txtRefActaAnho.Text);
			vBol.Dat.RefNro.Filter			= ObjConvert.GetFilter(txtRefActaNro.Text);
			vBol.Dat.RefRegAnio.Filter		= ObjConvert.GetFilter(txtRefRegAnho.Text);
			vBol.Dat.RefRegNro.Filter		= ObjConvert.GetFilter(txtRefRegNro.Text);

			/*{ggaleano 06/09/2007] Se verifica que el tipo de filtro ingresado en la clase
				 * sea del tipo rango, en caso positivo, se realiza un preproceso para expandirlo
				 * de la siguiente manera, sea por ejemplo el criterio de filtro 20-25, entonces
				 * se debe expandir el mismo a 20,21,22,23,24,25 ya que Clase es un tipo de dato
				 * de cadena y al realizar una cláusula BETWEEN pueden introducirse valores no deseados 
				 * en el resultado de la consulta para nuestro ejemplo, la clase 2 sería incluida dentro
				 * del rango 20-25 debido a su valor en la tabla de caracteres ASCII*/
			if( txtClase.Text.IndexOf("-") != -1 )
			{
				vBol.Dat.Clase.Filter = ObjConvert.GetFilter(this.GetSQLStringRank(txtClase.Text));
			}
			else
			{
				vBol.Dat.Clase.Filter			= ObjConvert.GetFilter(txtClase.Text);
			}
			vBol.Dat.Obs.Filter				= ObjConvert.GetSqlPattern(txtObservacion.Text);
			vBol.Dat.MarcaTipo.Filter       = ObjConvert.GetFilter(txtTipoMarca.Text);
	
			
												
			#endregion Filtro


			#region Define Sort
								
			if (this.ddOrden1.SelectedValue != "") 
			{
				switch (this.ddOrden1.SelectedValue)

				{
					case "Fecha":
						vBol.Dat.SolicitudFecha.Order = 1;
						break;
					case "Expediente":
						vBol.Dat.ExpNro.Order = 1;
						break;
					case "Clase":
						vBol.Dat.Clase.Order = 1;
						break;
					case "Denominacion":
						vBol.Dat.Denominacion.Order = 1;
						break;
					case "Solicitante":
						vBol.Dat.Propietario.Order = 1;
						break;
					case "Agente":
						vBol.Dat.AgenteLocal.Order = 1;
						break;
				}
				
			}
				
			if (this.ddOrden2.SelectedValue != "")
				
			{
				switch (this.ddOrden2.SelectedValue)

				{
					case "Fecha":
						vBol.Dat.SolicitudFecha.Order = 2;
						break;
					case "Expediente":
						vBol.Dat.ExpNro.Order = 2;
						break;
					case "Clase":
						vBol.Dat.Clase.Order = 2;
						break;
					case "Denominacion":
						vBol.Dat.Denominacion.Order = 2;
						break;
					case "Solicitante":
						vBol.Dat.Propietario.Order = 2;
						break;
					case "Agente":
						vBol.Dat.AgenteLocal.Order = 2;
						break;
				}
			}


			#endregion Define Sort
		
			return vBol;
		}

		#endregion obtener datos



		protected void btnGenerar_Click(object sender, System.EventArgs e)
		{
				#region Validar Parametros
				string buffer = "";		
			    if ( chkParametros()== 1 ) 
				{
					ShowMessage(" Ingrese los parámetros ") ;// si no se cargó la fecha solicita el ingreso.
					return;
				}
				#endregion Validar Parametros

				this.lblMensaje.Visible = false;


				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				
				Berke.DG.DBTab.BoletinDet vBol = new Berke.DG.DBTab.BoletinDet( db );
				vBol = obtenerDatos();
					

				#region Verificar limite de filas recuperadas
				int filas = vBol.Adapter.Count();
				if ( filas > 5000 )
				{
					this.ShowMessage( "Las "+ filas.ToString()+" filas a recuperar superan el limite de 5.000 registros" );
					return;
				}
				#endregion Verificar limite de filas recuperadas
				
				string temporal;
				temporal = HtmlBoletinGeneral( vBol );
				if (temporal == "Error")
				{
					this.ShowMessage ("No hay registros encontrados para estos parámetros" );
					return;
				}
				else
				{
					buffer+= temporal;
					buffer+=" <p></p>";
				}

				this.HtmlPayLoad = buffer;
			
				Berke.Marcas.WebUI.Tools.Helpers.UrlParam url = new Berke.Marcas.WebUI.Tools.Helpers.UrlParam();
		
				url.redirect( "reportViewer.aspx" );

			}

		#endregion Boton de Generar


		#region Enviar a Excel
			protected void btnGenExcel_Click(object sender, System.EventArgs e)
			{

				#region Validar Parametros
				if ( chkParametros()== 1 ) 
				{
					ShowMessage(" Ingrese los parámetros ") ;// si no se cargó la fecha solicita el ingreso.
					return;
				}
					#endregion Validar Parametros

					this.lblMensaje.Visible = false;


					Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
					db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
					db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				
					Berke.DG.DBTab.BoletinDet vBol = new Berke.DG.DBTab.BoletinDet( db );
					vBol = obtenerDatos();
					

					#region Verificar limite de filas recuperadas
					int filas = vBol.Adapter.Count();
				if ( filas > 5000 )
					{
						this.ShowMessage( "Las "+ filas.ToString()+" filas a recuperar superan el limite de 5.000 registros" );
						return;
					}
					#endregion Verificar limite de filas recuperadas

					#region Obtener plantilla 
					string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("repBoletinDoc", 2);
					if( plantilla == "" )
					{
						this.ShowMessage( "Error con la plantilla" );
						return ;
					}
					#endregion Obtener plantilla

					#region Inicializar Generadores de codigo
					Berke.Libs.CodeGenerator cg           = new  Berke.Libs.CodeGenerator(plantilla);
					Berke.Libs.CodeGenerator tabla        = cg.ExtraerTabla("tabla");
					Berke.Libs.CodeGenerator tablaTitulo  = tabla.ExtraerFila("tablaTitulo");
					Berke.Libs.CodeGenerator tablaFila    = tabla.ExtraerFila("tablaFila");
					#endregion Inicializar Generadores de codigo

					tablaFila.clearText();
						
					vBol.Adapter.ReadAll();
					vBol.GoTop();

					int cntMarcas = 0;

					#region Generar Archivo

					System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
					DateTime fechamin = Convert.ToDateTime("31/01/2050", culture);	
					DateTime fechamax = Convert.ToDateTime("01/01/1900", culture);

					Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
					Berke.DG.DBTab.Expediente_Situacion expesitu = new Berke.DG.DBTab.Expediente_Situacion(db);
					Berke.DG.DBTab.Tramite_Sit tramsit = new Berke.DG.DBTab.Tramite_Sit(db);

					string FechaMin = "";
					string FechaMax = "";
					string fechavtopub = "";
			

					for( vBol.GoTop(); ! vBol.EOF ; vBol.Skip() )
					{

						DateTime fecha = DateTime.Parse(vBol.Dat.SolicitudFecha.AsString);

						if  (fecha < fechamin)
						{
							FechaMin = fecha.ToShortDateString();
							fechamin = fecha;
						}
						if (fecha > fechamax)
						{
							FechaMax = fecha.ToShortDateString();
							fechamax = fecha;
						} 

						tablaFila.copyTemplateToBuffer();
						tablaFila.replaceField("fecha",vBol.Dat.SolicitudFecha.AsString);
						tablaFila.replaceField("boletin",vBol.Dat.BolNro.AsString+" /"+vBol.Dat.BolAnio.AsString);
						tablaFila.replaceField("nroExpediente",vBol.Dat.ExpNro.AsString);
						tablaFila.replaceField("clase",vBol.Dat.Clase.AsString);
						tablaFila.replaceField("tipoMarca",vBol.Dat.MarcaTipo.AsString);
						tablaFila.replaceField("propietario",vBol.Dat.Propietario.AsString);
						tablaFila.replaceField("denominacion",vBol.Dat.Denominacion.AsString);

						tablaFila.replaceField("pais",vBol.Dat.Pais.AsString);
						tablaFila.replaceField("agente",vBol.Dat.AgenteLocal.AsString);
						tablaFila.replaceField("tramite",vBol.Dat.Tramite.AsString);
						tablaFila.replaceField("Obs",vBol.Dat.Obs.AsString);
				

						# region incluir referencia Acta
						string refActa = vBol.Dat.RefNro.AsString;
						if (vBol.Dat.RefAnio.AsInt !=  0 )
						{
							refActa+= "/" + vBol.Dat.RefAnio.AsString;
						}

						tablaFila.replaceField("refActa",refActa);
					
				
						# endregion 

						# region incluir referencia Registro
						string refReg = vBol.Dat.RefRegNro.AsString;
						if (vBol.Dat.RefRegAnio.AsInt != 0)
						{
							refReg+= "/" + vBol.Dat.RefRegAnio.AsString;
						}

						tablaFila.replaceField("refReg",refReg);
			
						

						#endregion

						#region Datos Publicación
						expe.ClearFilter();
						expe.Dat.ActaNro.Filter = vBol.Dat.ExpNro.AsInt;
						expe.Dat.ActaAnio.Filter = vBol.Dat.ExpAnio.AsInt;
						expe.Adapter.ReadAll();

						if (expe.RowCount > 0)
						{
							tramsit.ClearFilter();
							tramsit.Dat.TramiteID.Filter = expe.Dat.TramiteID.AsInt;
							tramsit.Dat.SituacionID.Filter = Berke.Libs.Base.GlobalConst.Situaciones.PUBLICADA;
							tramsit.Adapter.ReadAll();

							expesitu.ClearFilter();
							expesitu.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
							expesitu.Dat.TramiteSitID.Filter = tramsit.Dat.ID.AsInt;
							expesitu.ClearOrder();
							expesitu.Dat.SituacionFecha.Order = -1;
							expesitu.Adapter.ReadAll();

							if (expesitu.RowCount > 0)
							{
								fechavtopub = expesitu.Dat.VencimientoFecha.AsString;
							}
						}
						tablaFila.replaceField("vtopub", fechavtopub);
						fechavtopub = "";
						#endregion Datos Publicación
						
						tablaFila.addBufferToText();
		
						cntMarcas++ ;
				
					}
					tabla.copyTemplateToBuffer();

					tablaTitulo.copyTemplateToBuffer();
					tablaTitulo.addBufferToText();
					tabla.replaceLabel("tablaTitulo", tablaTitulo.Texto);
					tabla.replaceLabel("tablaFila"  , tablaFila.Texto);
					tabla.addBufferToText();

					cg.copyTemplateToBuffer();
					cg.replaceLabel("tabla",tabla.Texto);
					cg.replaceField("Fecha",System.DateTime.Today.ToShortDateString());
				
					cg.replaceField("Periodo", "Del " + FechaMin + " al " + FechaMax); 
					cg.replaceField("Nmarcas", cntMarcas.ToString());

					cg.addBufferToText();
					string buffer = cg.Texto;

					#endregion

					#region Activar MS-Word
			
					Response.Clear();
					Response.Buffer = true;
					Response.ContentType = "application/vnd.ms-word";
					Response.AddHeader("Content-Disposition", "attachment;filename=repBoletinDoc.doc" );
					Response.Charset = "UTF-8";
					Response.ContentEncoding = System.Text.Encoding.UTF8;
					Response.Write(buffer); 
					Response.End();
					#endregion Activar MS-Word		

			}

		#endregion Enviar a Excel

		
		


	
		#region Funciones Genericas Replicadas
		
		#region chkSpc 
		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}
		#endregion chkSpc 
	
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

		#region Agentes Berke
		protected void lbtnAgBerke_Click(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
	
			Berke.DG.DBTab.CAgenteLocal agLoc = new Berke.DG.DBTab.CAgenteLocal( db );
			agLoc.Dat.Nuestro.Filter = true;
			System.Collections.ArrayList lista = agLoc.Adapter.GetListOfField( agLoc.Dat.nromatricula );
			foreach( object obj in lista )
			{
				if( txtAgLoc.Text != "" ) txtAgLoc.Text +=",";
				txtAgLoc.Text += (string) obj;
			}
		}
		#endregion 

		#endregion Funciones Genericas Replicadas
 
		/*[ggaleano 06/09/2007] Función que expande el rango dado. Por ejemplo,
		 * 20-25, es expandido a 20,21,22,23,24,25*/
		private string GetSQLStringRank(string filter)
		{
			string InSQL = "";
			string [] aVal = filter.Split( ((String)"-").ToCharArray() );
			int min = Convert.ToInt32(aVal[0].Trim());
			int max = Convert.ToInt32(aVal[1].Trim());
			
			for (int i = min; i <= max; i++)
			{
				if (InSQL != "")
				{
					InSQL = InSQL + ",";
				}
				InSQL = InSQL + i.ToString();
			}
			return InSQL;
		}

		protected void btnTrSug_Click(object sender, System.EventArgs e)
		{
			if (lblTrSug.Visible)
			{
				lblTrSug.Visible = false;
				btnTrSug.Text = "Sugerir";
				return;
				
			}
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vTrBoletin vTr = new Berke.DG.ViewTab.vTrBoletin(db);

			vTr.Adapter.Distinct = true;
			vTr.Adapter.ReadAll();
			string script = @"<script type='text/javascript'>
                                function addCodigo(codigo) {
									var control = document.getElementById('txtTramite');
									if (control.value == ''){
                                        control.value = codigo;
									}
                                    else {
                                        control.value = control.value +','+ codigo;	
									}
                                }
                              </script>";
			string table = "<table class='tbl' style='margin-left:85'>{0}</table>";
			string td    = "<td class='td'><a href=\"javascript:addCodigo('{0}')\">{0}</a></td>";
			string tr    = "<tr>{0}</tr>";
			string _td   = "";
			string _tr   = "";

			int c = 0;
			for(vTr.GoTop(); !vTr.EOF; vTr.Skip())
			{
				c++;
				_td += string.Format(td, vTr.Dat.tramite.AsString);
				if (c>0 && (c%NCOLS==0) )
				{
					_tr+= string.Format(tr, _td);
					_td = "";
					c=0;
				}

			}
			if (_td != "") 
			{
				_tr+= string.Format(tr, _td);
			}
			table = string.Format(table, _tr);
			lblTrSug.Visible = true;
			lblTrSug.Text    = script + table;
			btnTrSug.Text = "Ocultar";
			db.CerrarConexion();
		}


	}
}



// hasta aquí