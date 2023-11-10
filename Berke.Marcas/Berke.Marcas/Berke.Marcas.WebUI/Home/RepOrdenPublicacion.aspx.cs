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
using Berke.Libs.Base;
using Berke.Libs.WebBase.Helpers;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for RepOrdenPublicacion.
	/// </summary>
	public partial class RepOrdenPublicacion : System.Web.UI.Page
	{

		private string CLAVE_ORDPUBREGREN = "ordpubregren";
		private string CLAVE_ORDPUBTR = "ordpubtr";
		private string CLAVE_ORDPUBLC = "ordpublic";
		private string CLAVE_ORDPUBRESUMEN = "ordpubresumen";

		#region variables globales
		Berke.DG.ViewTab.vOrdenPublicacion	ordpub;
		Berke.Libs.Base.Helpers.AccesoDB	db;
		Berke.DG.DBTab.Mes					m;
		#endregion variables globales

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

		protected void btnGenerar_Click(object sender, System.EventArgs e)
		{
			int ndata=0;
			if(rbTipoListado.SelectedIndex == 0)
			{
				ndata = this.generarLista();
			}
			else 
			{
				if (rbTramite.SelectedIndex == 0)
				{
					ndata = this.generarReporteRegRen();
				}
				else if (rbTramite.SelectedIndex == 1)
				{
					ndata = this.generarReporteTr();
				}
				else if (rbTramite.SelectedIndex == 2)
				{
					ndata = this.generarReporteLc();
				}
			}
			if (ndata>0){
				lblMensaje.Visible = true;
				lblMensaje.Text = "Reporte generado exitosamente.";
			}
			else {
				lblMensaje.Visible = true;
				lblMensaje.Text = "No se encontraron datos.";
			}
			
		}

		#region Filtros		
		private void aplicarFiltros()
		{

			ordpub.Dat.situacionfecha.Filter = ObjConvert.GetFilter(txtFecha.Text);
			ordpub.Dat.trAbrev.Filter        = ObjConvert.GetFilter(rbTramite.SelectedValue);

		}
		#endregion Filtros

		#region generarReporteRegRen
		private int generarReporteRegRen()
		{
			db				= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			ordpub = new Berke.DG.ViewTab.vOrdenPublicacion(db);
			this.aplicarFiltros();

			#region Orden
			ordpub.Dat.situacionfecha.Order = 1;
            ordpub.Dat.ExpedienteSitID.Order = 2;
			#endregion Orden

			ordpub.Adapter.ReadAll();

			if (ordpub.RowCount>0)
			{

				#region seleccionar plantilla

				Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
				dp.Dat.Clave.Filter = CLAVE_ORDPUBREGREN;
				dp.Adapter.ReadAll();
				if (dp.RowCount == 0) 
				{
					ShowMessage("No se encuentra definida la plantilla para el reporte.");
				}
				string template = dp.Dat.PlantillaHTML.AsString;
				#endregion seleccionar plantilla

				Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
				Berke.Libs.CodeGenerator tab = cg.ExtraerTabla("tabla");

				for (ordpub.GoTop(); !ordpub.EOF; ordpub.Skip())
				{				
					tab.copyTemplateToBuffer();
					this.replaceRegRenData(tab);
					tab.addBufferToText();
				}
				cg.copyTemplateToBuffer();
				cg.replaceLabel("tabla", tab.Texto);
				cg.addBufferToText();
				this.ActivarWord(cg.Texto);
			}
			return ordpub.RowCount;
		}
		#endregion generarReporteRegRen
		
		#region Generar Lista
		private int generarLista()
		{
			db				= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			ordpub = new Berke.DG.ViewTab.vOrdenPublicacion(db);
			this.aplicarFiltros();

			#region Orden
			ordpub.Dat.situacionfecha.Order = 1;
            ordpub.Dat.ExpedienteSitID.Order = 2;
			#endregion Orden

			ordpub.Adapter.ReadAll();

			#region seleccionar plantilla

			Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
			dp.Dat.Clave.Filter = CLAVE_ORDPUBRESUMEN;
			dp.Adapter.ReadAll();
			if (dp.RowCount == 0) 
			{
				ShowMessage("No se encuentra definida la plantilla para el reporte.");
			}
			string template = dp.Dat.PlantillaHTML.AsString;
			#endregion seleccionar plantilla
						
			Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
			Berke.Libs.CodeGenerator tab = cg.ExtraerTabla("tabla");
			Berke.Libs.CodeGenerator cab = tab.ExtraerFila ("cab"  );
			Berke.Libs.CodeGenerator row = tab.ExtraerFila ("row"  );

			DateTime fechainf = System.DateTime.Now; 
			DateTime fechasup = System.DateTime.Now; 
			if (ordpub.RowCount>0)
			{
				fechainf = ordpub.Dat.situacionfecha.AsDateTime;

				for (ordpub.GoTop(); !ordpub.EOF; ordpub.Skip())
				{				
					row.copyTemplateToBuffer();
					this.replaceRegRenData(row);
					row.addBufferToText();

					fechasup = ordpub.Dat.situacionfecha.AsDateTime;
				}
				tab.copyTemplateToBuffer();
				tab.replaceLabel("cab", cab.Template);
				tab.replaceLabel("row", row.Texto);
				tab.addBufferToText();
				
				cg.copyTemplateToBuffer();
				cg.replaceField("situacionfecha"    , fechainf.ToString("dd/MM/yyyy") + " - "+ fechasup.ToString("dd/MM/yyyy"));
				cg.replaceLabel("tabla", tab.Texto);
				cg.addBufferToText();
				this.ActivarWord(cg.Texto);
			}
			return ordpub.RowCount;
		}
		private void replaceRegRenData(Berke.Libs.CodeGenerator row)
		{
			row.replaceField("denominacion"      , ordpub.Dat.denominacion.AsString);
			row.replaceField("presentacionfecha" , ordpub.Dat.presentacionfecha.AsString );
			row.replaceField("actanro"           , ordpub.Dat.actanro.AsString);
			row.replaceField("actaanio"          , ordpub.Dat.actaanio.AsString);
			row.replaceField("clase"             , ordpub.Dat.claseNro.AsString);
			row.replaceField("proNombre"         , ordpub.Dat.propietario.AsString);
			row.replaceField("proDir"            , ordpub.Dat.prodir.AsString);
			row.replaceField("tramite"           , ordpub.Dat.trAbrev.AsString);
			row.replaceField("hinro"           , ordpub.Dat.hinro.AsString);
			row.replaceField("hianio"          , ordpub.Dat.hianio.AsString);
		}
		#endregion Generar Lista			

		#region generarReporteTr
		private int generarReporteTr()
		{
			db				= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			ordpub = new Berke.DG.ViewTab.vOrdenPublicacion(db);

			this.aplicarFiltros();

			#region Ordenar
			ordpub.Dat.propietarioAnterior.Order = 1;
			ordpub.Dat.propietarioActual.Order   = 2;
			#endregion Ordenar

			ordpub.Adapter.ReadAll();
			if (ordpub.RowCount>0)
			{

				#region Cargar meses
				this.cargarListaMeses();
				#endregion Cargar meses

				#region seleccionar plantilla

				Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
				dp.Dat.Clave.Filter = CLAVE_ORDPUBTR;
				dp.Adapter.ReadAll();
				if (dp.RowCount == 0) 
				{
					ShowMessage("No se encuentra definida la plantilla para el reporte.");
				}
				string template = dp.Dat.PlantillaHTML.AsString;
				#endregion seleccionar plantilla

				Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
				Berke.Libs.CodeGenerator pag = cg.ExtraerTabla ("pag");
				Berke.Libs.CodeGenerator tab = pag.ExtraerTabla("tabla");
				Berke.Libs.CodeGenerator cab = tab.ExtraerFila ("cab"  );
				Berke.Libs.CodeGenerator row = tab.ExtraerFila ("row"  );

				ordpub.GoTop();

				string propAnt    = ordpub.Dat.propietarioAnterior.AsString ;
				string prop       = ordpub.Dat.propietarioActual.AsString ;
				DateTime fechasol = new DateTime(0);
				DateTime fechasit = ordpub.Dat.situacionfecha.AsDateTime;
				string lstactas = "";
				string comma    = "";

				for (; !ordpub.EOF; ordpub.Skip())
				{
					if ( (ordpub.Dat.propietarioAnterior.AsString != propAnt) ||
						(ordpub.Dat.propietarioActual.AsString   != prop) )
					{	
						#region replace tab
						tab.copyTemplateToBuffer();
						tab.replaceLabel("row", row.Texto);
						tab.replaceLabel("cab", cab.Template);
						tab.addBufferToText();
						#endregion replace tab
						#region replace pag
						pag.copyTemplateToBuffer();
						pag.replaceLabel("tabla", tab.Texto);
						pag.replaceField("propietarioAnterior", propAnt);
						pag.replaceField("propietarioActual"  , prop );
						pag.replaceField("listaactas"    , lstactas );
						this.replaceFecha(ordpub.Dat.situacionfecha.AsDateTime,"fecha",pag);
						this.replaceFecha(fechasol,"fechasol",pag);
						pag.addBufferToText();
						pag.addNewPageToText();
						#endregion replace pag
					
						#region Resetear valores de pag y actualizar prop/propAnt
						row.clearText();
						tab.clearText();

						propAnt		= ordpub.Dat.propietarioAnterior.AsString ;
						prop		= ordpub.Dat.propietarioActual.AsString ;
						fechasit    = ordpub.Dat.situacionfecha.AsDateTime;
						lstactas	= "";
						comma ="";
						#endregion Resetear valores de pag  y actualizar prop/propAnt

					}
					row.copyTemplateToBuffer();
					this.replaceTrLcData(row);
					row.addBufferToText();
					fechasol = ordpub.Dat.presentacionfecha.AsDateTime;
					lstactas += comma + ordpub.Dat.actanro.AsString;
					comma = ", ";				
				}
				#region replace tab
				tab.copyTemplateToBuffer();
				tab.replaceLabel("row", row.Texto);
				tab.replaceLabel("cab", cab.Template);
				tab.addBufferToText();
				#endregion replace tab
				#region replace pag
				pag.copyTemplateToBuffer();
				pag.replaceLabel("tabla", tab.Texto);
				pag.replaceField("propietarioAnterior", propAnt);
				pag.replaceField("propietarioActual"  , prop );
				pag.replaceField("listaactas"    , lstactas );
				this.replaceFecha(fechasit,"fecha",pag);
				this.replaceFecha(fechasol,"fechasol",pag);			
				pag.addBufferToText();
				#endregion replace pag
				#region replace cg
				cg.copyTemplateToBuffer();
				cg.replaceLabel("pag", pag.Texto);
				cg.addBufferToText();
				#endregion replace cg

				this.ActivarWord(cg.Texto);
			}
			return ordpub.RowCount;
		}
		#endregion generarReporteTr

		#region generarReporteLc
		private int generarReporteLc()
		{
			db				= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vOrdenPublicacionLic ordpublic = new Berke.DG.ViewTab.vOrdenPublicacionLic(db);
			#region Filtros
			ordpublic.Dat.situacionfecha.Filter = ObjConvert.GetFilter(txtFecha.Text);
			ordpublic.Dat.trAbrev.Filter        = ObjConvert.GetFilter(rbTramite.SelectedValue);
			#endregion Filtros

			#region Ordenar
			ordpublic.Dat.propietario.Order   = 1;
            ordpublic.Dat.ExpedienteSitID.Order = 2;
			ordpublic.Dat.licenciatario.Order = 3;
			#endregion Ordenar

			ordpublic.Adapter.ReadAll();
			if (ordpublic.RowCount>0)
			{

				#region Cargar meses
				this.cargarListaMeses();
				#endregion Cargar meses

				#region seleccionar plantilla

				Berke.DG.DBTab.DocumentoPlantilla dp = new Berke.DG.DBTab.DocumentoPlantilla(db);
				dp.Dat.Clave.Filter = CLAVE_ORDPUBLC;
				dp.Adapter.ReadAll();
				if (dp.RowCount == 0) 
				{
					ShowMessage("No se encuentra definida la plantilla para el reporte.");
				}
				string template = dp.Dat.PlantillaHTML.AsString;
				#endregion seleccionar plantilla

				Berke.Libs.CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
				Berke.Libs.CodeGenerator pag = cg.ExtraerTabla ("pag");
				Berke.Libs.CodeGenerator tab = pag.ExtraerTabla("tabla");
				Berke.Libs.CodeGenerator cab = tab.ExtraerFila ("cab"  );
				Berke.Libs.CodeGenerator row = tab.ExtraerFila ("row"  );

				#region inicializar datos 
				ordpublic.GoTop();

				string lic        = ordpublic.Dat.licenciatario.AsString ;
				string prop       = ordpublic.Dat.propietario.AsString ;
				string dom        = "";
				DateTime fechasol = new DateTime(0);
				DateTime fechasit = ordpublic.Dat.situacionfecha.AsDateTime;
				string lstactas = "";
				string comma    = "";
				#endregion inicializar datos

				for (; !ordpublic.EOF; ordpublic.Skip())
				{
					if ( (ordpublic.Dat.licenciatario.AsString != lic) ||
						(ordpublic.Dat.propietario.AsString   != prop) )
					{	
						#region replace tab
						tab.copyTemplateToBuffer();
						tab.replaceLabel("row", row.Texto);
						tab.replaceLabel("cab", cab.Template);
						tab.addBufferToText();
						#endregion replace tab
						#region replace pag
						pag.copyTemplateToBuffer();
						pag.replaceLabel("tabla", tab.Texto);
						pag.replaceField("propietario", prop);
						pag.replaceField("licenciatario"  , lic );
						pag.replaceField("licDomicilio"   , dom );
						pag.replaceField("listaactas"    , lstactas );
						this.replaceFecha(ordpublic.Dat.situacionfecha.AsDateTime,"fechasit",pag);
						this.replaceFecha(fechasol,"fechasol",pag);
						pag.addBufferToText();
						pag.addNewPageToText();
						#endregion replace pag
					
						#region Resetear valores de pag y actualizar prop/propAnt
						row.clearText();
						tab.clearText();

						lic      = ordpublic.Dat.licenciatario.AsString ;
						prop     = ordpublic.Dat.propietario.AsString ;
						fechasit = ordpublic.Dat.situacionfecha.AsDateTime;
						lstactas = "";
						comma    = "";
						#endregion Resetear valores de pag  y actualizar prop/propAnt

					}
					#region replace row
					row.copyTemplateToBuffer();
					row.replaceField("denominacion"      , ordpublic.Dat.denominacion.AsString);
					row.replaceField("concesionfecha"    , ordpublic.Dat.concesionFechaPadre.AsString );
					row.replaceField("registronro"       , ordpublic.Dat.registroPadre.AsString);
					row.replaceField("clase"             , ordpublic.Dat.claseNro.AsString);
					row.addBufferToText();
					#endregion replace row

					fechasol = ordpublic.Dat.presentacionfecha.AsDateTime;
					dom      = ordpublic.Dat.licDomicilio.AsString;
					lstactas+= comma + ordpublic.Dat.actanro.AsString;
					comma    = ", ";				
				}
				#region replace tab
				tab.copyTemplateToBuffer();
				tab.replaceLabel("row", row.Texto);
				tab.replaceLabel("cab", cab.Template);
				tab.addBufferToText();
				#endregion replace tab
				#region replace pag
				pag.copyTemplateToBuffer();
				pag.replaceLabel("tabla", tab.Texto);
				pag.replaceField("propietario", prop);
				pag.replaceField("licenciatario"  , lic );
				pag.replaceField("licDomicilio"   , dom );
				pag.replaceField("listaactas"    , lstactas );
				this.replaceFecha(fechasit,"fechasit",pag);
				this.replaceFecha(fechasol,"fechasol",pag);
				pag.addBufferToText();
				#endregion replace pag
				#region replace cg
				cg.copyTemplateToBuffer();
				cg.replaceLabel("pag", pag.Texto);
				cg.addBufferToText();
				#endregion replace cg

				this.ActivarWord(cg.Texto);
			}
			return ordpublic.RowCount;
		}
		#endregion generarReporteLc

		private void replaceTrLcData(Berke.Libs.CodeGenerator row)
		{
			row.replaceField("denominacion"      , ordpub.Dat.denominacion.AsString);
			row.replaceField("concesionfecha"    , ordpub.Dat.concesionFechaPadre.AsString );
			row.replaceField("registronro"       , ordpub.Dat.registroPadre.AsString);
			row.replaceField("clase"             , ordpub.Dat.claseNro.AsString);
		}

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion

		#region Activar MS-Word
		private void ActivarWord(string documento)
		{
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=marcastramite.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(documento); //Llamada al procedimiento HTML
			Response.End();
		}
		#endregion Activar MS-Word

		#region Obtener descripcion de mes
		private void cargarListaMeses()
		{
			m = new Berke.DG.DBTab.Mes(db);
			m.Dat.ididioma.Filter = (int)GlobalConst.Idioma.ESPANOL;
			m.Dat.Orden.Order = 1;			
			m.Adapter.ReadAll();
		}
		private string getDescripcionMes(int nmes)
		{
			m.Go(nmes-1);
			return m.Dat.Mes.AsString;
		}
		#endregion Obtener descripcion de mes

		private void replaceFecha( DateTime date, string prefix, Berke.Libs.CodeGenerator cg)
		{
			cg.replaceField(prefix+".mes"  , this.getDescripcionMes(date.Month) );
			cg.replaceField(prefix+".dia"  , date.Day.ToString());
			cg.replaceField(prefix+".anio" , date.Year.ToString());
			cg.replaceField(prefix+".hora" , date.ToString("HH:mm"));
		}
	}
}
