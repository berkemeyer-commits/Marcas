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
	using Berke.Libs;
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;

	/// <summary>
	/// Summary description for REP_39.
	/// </summary>
	public partial class REP_39 : System.Web.UI.Page
	{
		#region Variables Globales
		Berke.Libs.Base.Helpers.AccesoDB db;
		#endregion Variables Globales

		#region Properties

		#endregion Properties

		#region Controles del Web Form

	
		#endregion Controles del Web Form

		#region Asignar Delegados
		//Delegados: son como punteros o atributos, lo cual permite añadir metainformación al código. 
		//Los delegados tienen como objetivo almacenar referencias a métodos de otras clases, de tal forma que,
		//al ser invocados, ejecutan todos estos métodos almacenados de forma secuencial. 
		
		private void AsignarDelegados()
		{

			this.cbxPropietarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxPropietarioID_LoadRequested); 
			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Cargar Idiomas
			Berke.DG.DBTab.CIdioma CIdioma = new Berke.DG.DBTab.CIdioma(db);
			CIdioma.Adapter.ReadAll();

			for (CIdioma.GoTop(); ! CIdioma.EOF; CIdioma.Skip())
			{
				ddlIdiomaDoc.Items.Add(new System.Web.UI.WebControls.ListItem(CIdioma.Dat.descrip.AsString, CIdioma.Dat.ididioma.AsString));
			}
			ddlIdiomaDoc.SelectedIndex = 1;

			#endregion Cargar Idiomas
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			db      		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			AsignarDelegados();
			
			if( !IsPostBack )
			{
				//		pedidoID_param = Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam ("PedidoID");
				AsignarValoresIniciales();
			}		
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

		#region Carga de Combo

		#region Propietario
		private void cbxPropietarioID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )	
			{
				inTB.Dat.Entero .Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa	.Value = combo.Text;   //String	
			}
			inTB.PostNewRow(); 				
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Propietario.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Propietario		
		
		#region Cliente
		private void cbxClienteID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )	
			{
				inTB.Dat.Entero .Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa	.Value = combo.Text;   //String	
			}
			inTB.PostNewRow(); 				
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Cliente.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Cliente		

	
		#endregion Carga de Combo

		#region btBuscar_Click
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
			this.GenerarReporte();
		}
		#endregion btBuscar_Click

		#region ParametrosOk
		private bool parametrosOK (out string mensajeError )
		{
			mensajeError = "";
			//			if( cbxPropietarioID.SelectedValue.Trim() == "")
			//			{
			//				mensajeError = "Debe Ingresar un Propietario";
			//				return false;
			//			}
			return true;
		}
		#endregion ParametrosOk

		#region AplicarFiltro
		private void AplicarFiltro( Berke.DG.ViewTab.vExpeMarca view  )
		{
			
			//			view.Dat.Denominacion	.Filter = ObjConvert.GetFilter_Str	( txtDenominacion.Text);
			//			view.Dat.AltaFecha		.Filter = ObjConvert.GetFilter		( txtAltaFecha_min.Text);
			//			view.Dat.FuncionarioID	.Filter = ObjConvert.GetFilter		( ddlFuncionario.SelectedValue);
			//			view.Dat.Nuestra		.Filter	= ObjConvert.GetFilter_Bool	( ddlNuestra.SelectedValue);
			//		
			string FiltroTramite = Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + "," + Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString();
			view.Dat.TramiteID.Filter	= ObjConvert.GetFilter(FiltroTramite);//1=REG, 2=REN
			view.Dat.ClienteID.Filter	= ObjConvert.GetFilter( this.cbxClienteID.SelectedValue );

			#region Propias
			view.Dat.MarcaActiva.Filter = true;
			view.Dat.MarcaNuestra.Filter = DBNull.Value;
			view.Dat.Vigilada.Filter	= true;
			#endregion Propias

			view.Dat.PropietarioID.Filter = ObjConvert.GetFilter( this.cbxPropietarioID.SelectedValue );
			//view.Dat.PropietarioID.Filter = "17";

			// mbaez 13/10/2006. Se incluyen ahora las marcas en tramite..,
			// Solucion sugerida por laura
			//view.Dat.RegistroVigente.Filter = true;

		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vExpeMarca view)
		{
			view.ClearOrder();
			view.Dat.PropietarioID.Order	= 1;
			view.Dat.ClienteID.Order		= 2;
			view.Dat.Denominacion.Order		= 3;
		}
		#endregion SetOrder

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vExpeMarca view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			
			mensajeError = "";
			int recuperados = -1;
			try 
			{
				recuperados = view.Adapter.Count();
				if( recuperados != 0 )
				{
					if( recuperados < limite )
					{
						view.Adapter.ReadAll( limite );
						resultado = true;
					}
					else
					{
						mensajeError = "Los " + recuperados.ToString()+ " registros a recuperar exceden el limite de "+ limite.ToString();
					}
				}
				else
				{
					mensajeError = "No se encontró ningún registro ";		
				}
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				mensajeError = "Los " + ex.Recuperados.ToString()+ " registros a recuperar exceden el limite de "+ ex.Limite.ToString();
			}
			catch( Exception exep ) 
			{
				mensajeError = this.GetType().Name + " ERROR:"+ exep.Message;
			}
			return resultado;
		}
		#endregion ObtenerDatosOk

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion

		#region GenerarReporte
		private void GenerarReporte()
		{
			//---------------
			string mensajeError = "";
			
			/*Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;*/

			Berke.DG.ViewTab.vExpeMarca view = new Berke.DG.ViewTab.vExpeMarca( db );
			if( parametrosOK( out mensajeError ) )
			{
				AplicarFiltro( view );
				SetOrder( view );
				if( ObtenerDatosOk( view, 5000, out mensajeError, db ))
				{
					GenerarDocumento( view );
				}
			}
		
			if(mensajeError != "" ) 
			{
				ShowMessage( mensajeError );
			}
			//----------
		}
		#endregion GenerarReporte

		// Agregado por mbaez. Elimina los rows que corresponden
		// a marcas vencidas con hijos en trámite o activos.
		// Donde padre e hijo comparten la misma marcaid
		// 8va-8va / 7ma-7ma
		#region Eliminar Marcas vencidas de la 7ma.-7ma / 8va.-8va.
		private int EliminarDuplicados( Berke.DG.ViewTab.vExpeMarca  view ) 
		{
			int num_eliminados = 0;
			for(  view.GoTop(); ! view.EOF; view.Skip() ) 
			{
				if (existeHijoVigente(view)) 
				{
					view.Delete();
					num_eliminados++;
				}
			}
			return num_eliminados;
		}
		private bool existeHijoVigente(Berke.DG.ViewTab.vExpeMarca  view)
		{
			int curr_index = view.RowIndex;
			int expedienteid = view.Dat.ExpedienteID.AsInt;
			int marcaid      = view.Dat.MarcaID.AsInt;

			for(  view.GoTop(); ! view.EOF; view.Skip() ) 
			{
				if (   (view.Dat.ExpedienteIDPadre.AsInt == expedienteid )
					&& (view.Dat.MarcaID.AsInt == marcaid) ) 
				{
					view.Go(curr_index);
					return true;
				}
			}
			view.Go(curr_index);
			return false;
		}
		#endregion Eliminar Marcas vencidas de la 7ma.-7ma / 8va.-8va.

		#region GenerarDocumento
		private void GenerarDocumento( Berke.DG.ViewTab.vExpeMarca  view )
		{
			#region Leer Plantilla
			/* [ggaleano 16/04/2007] Se agregó selección de plantilla para diversos idiomas.*/
			string template = this.GetPlantilla();
		
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );

			CodeGenerator tabProp		= cg.ExtraerTabla("tabProp" );
			CodeGenerator tabCli		= tabProp.ExtraerTabla("tabCli" );

			CodeGenerator filaTit		= tabCli.ExtraerFila("filaTit",2 );
			CodeGenerator fila			= tabCli.ExtraerFila("fila" );
			#endregion Obtener "Generators"

			#region Inicializar variables y otros
			Berke.Libs.Base.Helpers.AccesoDB db		= view.Adapter.Db;
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario( view.Adapter.Db );
									
			Berke.DG.DBTab.Expediente expeActual	= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expeAnt		= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.MarcaRegRen regRenActual = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.MarcaRegRen regRenAnt	= new Berke.DG.DBTab.MarcaRegRen( db );
								
			cg.clearText();
			cg.copyTemplateToBuffer();

			tabProp.clearText();
			tabCli.clearText();
			
			int num_eliminados = this.EliminarDuplicados(view);
			int num_reg = view.Adapter.Count() - num_eliminados;
			#endregion Inicializar variables y otros

			#region Asignar valores iniciales a variables para corte de control

			view.GoTop();
			int antProp = 0;
						
			while (!view.EOF)
			{
				if (!view.IsRowDeleted)
				{
					antProp = view.Dat.PropietarioID.AsInt;
					break;
				}
				else
				{
					view.Skip();
				}
			}
			#endregion Asignar valores iniciales a variables para corte de control

			#region Corte de Control
			/* [ggaleano 14/06/2007] Se rehizo el corte de control ya que el anterior código 
			 * además de ser extenso e incorrecto era muy difícil de entender y por tanto 
			 * de corregir.
			 */
			for (view.GoTop(); ! view.EOF; view.Skip())
			{
				if (! view.IsRowDeleted)
				{		
					
					if (antProp != view.Dat.PropietarioID.AsInt)
					{
						//Corte por PropietarioID
						antProp = this.Corte_PropietarioID(prop, tabProp, fila, num_reg, antProp, view);
					}
					this.Procesar(expeActual, expeAnt, regRenActual, regRenAnt, tabCli, fila, view);
				}
			}
			antProp = this.Corte_PropietarioID(prop, tabProp, fila, num_reg, antProp, view);
			#endregion Corte de Control

			#region Asignar valores a "generators" de la plantilla
			
			filaTit.clearText();
			filaTit.copyTemplateToBuffer();
			filaTit.addBufferToText();
			tabCli.replaceLabel("filaTit", filaTit.Texto);
			tabCli.addBufferToText();
			tabProp.replaceLabel("tabCli", tabCli.Texto );
			tabProp.addBufferToText();
			cg.replaceLabel( "tabProp", tabProp.Texto );
			cg.replaceField("Hoy", string.Format("{0:d}", DateTime.Today));
			cg.addBufferToText();

			string buffer = cg.Texto;	
			#endregion Asignar valores a "generators" de la plantilla

			#region Activar WORD
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=rep_39.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();

			#endregion Activar WORD
		}
		#endregion GenerarDocumento

		#region Obtener Plantilla
		private string GetPlantilla()
		{
			int idiomaID =  Convert.ToInt16(ddlIdiomaDoc.SelectedValue.ToString()); 
			string template = "";

			switch (idiomaID)
			{
				case 1:
					template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_39_1", idiomaID);
					break;
				case 2:
					template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_39", idiomaID);
					break;
				case 3:
					template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_39_3", idiomaID);
					break;
				case 4:
					template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_39_4", idiomaID);
					break;
				default :
					template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_39", Convert.ToInt32(Berke.Libs.Base.GlobalConst.Idioma.ESPANOL));
					break;
			}
			return template;
		}

		#endregion Obtener Plantilla

		#region Corte por PropietarioID
		private int Corte_PropietarioID(Berke.DG.DBTab.Propietario prop, CodeGenerator tabProp, CodeGenerator fila, int num_reg, int PropID, Berke.DG.ViewTab.vExpeMarca view)
		{
			prop.Adapter.ReadByID(PropID);
			tabProp.copyTemplateToBuffer();
			tabProp.replaceField("Propietario",prop.Dat.Nombre.AsString+ " ( "+PropID.ToString()+ ")");
			tabProp.replaceField("Direccion", prop.Dat.Direccion.AsString );
			tabProp.replaceField("NumReg", ""+ num_reg);
			fila.clearText();
			return view.Dat.PropietarioID.AsInt;
		}
		#endregion Corte por PropietarioID

		#region Procesar datos del ciclo del corte
		private void Procesar(Berke.DG.DBTab.Expediente expeActual, Berke.DG.DBTab.Expediente expeAnt, Berke.DG.DBTab.MarcaRegRen regRenActual, Berke.DG.DBTab.MarcaRegRen regRenAnt, CodeGenerator tabCli, CodeGenerator fila, Berke.DG.ViewTab.vExpeMarca view)
		{
			#region Leer
			Berke.DG.MarcaDG mar;
			mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( view.Dat.MarcaID.AsInt );
			expeActual.Adapter.ReadByID( view.Dat.ExpedienteID.Value );
			expeAnt.Adapter.ReadByID( expeActual.Dat.ExpedienteID.Value );
			regRenActual.Adapter.ReadByID( expeActual.Dat.MarcaRegRenID.Value );
			regRenAnt.Adapter.ReadByID( expeAnt.Dat.MarcaRegRenID.Value );
			#endregion Leer

			fila.copyTemplateToBuffer();
			fila.replaceField("Marca",		view.Dat.Denominacion.AsString);
			fila.replaceField("Tipo",		mar.MarcaTipo.Dat.Abrev.AsString);
			fila.replaceField("Tramite",	view.Dat.TramiteAbrev.AsString);
			fila.replaceField("Clase",		mar.Clase.Dat.Nro.AsString);
			fila.replaceField("RegActual",	regRenActual.Dat.RegistroNro.AsString );
			fila.replaceField("FConc",		regRenActual.Dat.ConcesionFecha.AsString);
			fila.replaceField("RegAnt",		regRenAnt.Dat.RegistroNro.AsString);
			fila.replaceField("Acta",		expeActual.Dat.Acta.AsString);
			fila.replaceField("FPres",		expeActual.Dat.PresentacionFecha.AsString);
			fila.replaceField("FVto",		regRenActual.Dat.VencimientoFecha.AsString);
			fila.addBufferToText();
			tabCli.copyTemplateToBuffer();
			tabCli.replaceLabel("fila", fila.Texto);
			
			
		}
		#endregion Procesar datos del ciclo del corte
	}
}
