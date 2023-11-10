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
	/// Summary description for repRenovadas.
	/// </summary>
	public partial class repRenovadas : System.Web.UI.Page
	{

		System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");

		#region Constantes
		public const int SOLO_HI = 0;
		public const int HI_ACTA = 1;
		public const int SIN_HI_NI_ACTA = 2;
		public const int TODAS = 3;
		#endregion Constantes

		#region Controles del Web Form
		#endregion Controles del Web Form

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			cbPerGracia.CheckedChanged += new EventHandler(cbPerGracia_CheckedChanged);			
			lblAdvertencia.Text = "";
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
			
			if ((txtFechaInstruc.Text.Trim() == "") && (txtVencimReg.Text.Trim() == ""))
			{
				mensajeError = "Debe ingresar al menos un criterio de filtro.";
				return false;
			}

			return true;
		}
		#endregion ParametrosOk

		#region AplicarFiltro
		private void AplicarFiltro( Berke.DG.ViewTab.vRenovados view )
		{
			string defWhere = view.Adapter.DefaultWhere;

			if (rblOpciones.SelectedIndex == SOLO_HI)
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}

				defWhere += " ot.OrdenTrabajo IS NOT NULL and expe.Acta IS NULL ";
			}

			if (rblOpciones.SelectedIndex == HI_ACTA)
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}

				defWhere += " ot.OrdenTrabajo IS NOT NULL AND expe.Acta IS NOT NULL";
			}
			/*if (rblOpciones.SelectedIndex == SIN_HI_NI_ACTA)
			{
				if( defWhere != "" )
				{
					defWhere += " and ";
				}

				defWhere += " ot.OrdenTrabajo IS NULL AND expe.Acta IS NULL ";
			}*/

			view.Adapter.SetDefaultWhere(defWhere);

			if (cbPerGracia.Checked)
			{
				
				view.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter(this.SplitFechas(txtVencimReg.Text));
			}
			else
			{
				view.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter(txtVencimReg.Text);
			}

			
			view.Dat.FechaInstruccion.Filter = ObjConvert.GetFilter(txtFechaInstruc.Text);
			
		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vRenovados view)
		{
			view.ClearOrder();
			view.Dat.Registro.Order = 1;
		}
		#endregion SetOrder

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vRenovados view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			
			mensajeError = "";
			int recuperados = -1;
			
			try 
			{

				recuperados = view.Adapter.Count();

				if (rblOpciones.SelectedIndex == TODAS)
				{
					recuperados += this.MergeView(view, 'C');
				}
				else if (rblOpciones.SelectedIndex == SIN_HI_NI_ACTA)
				{
					recuperados = this.MergeView(view, 'C');
				}

				
				if (recuperados != 0 )
				{
					if( recuperados < limite )
					{
						string comando = view.Adapter.ReadAll_CommandString();

						view.Adapter.ReadAll( limite );

						if ((rblOpciones.SelectedIndex == TODAS) || (rblOpciones.SelectedIndex == SIN_HI_NI_ACTA))
						{
							this.MergeView(view, 'O');
						}

										
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
			
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vRenovados view = new Berke.DG.ViewTab.vRenovados( db );
			if( parametrosOK( out mensajeError ) )
			{
				AplicarFiltro( view );
				SetOrder( view );
				if( ObtenerDatosOk(view, 5000, out mensajeError, db ))
				{
					GenerarDocumento( view );
				}
			}
		
			if(mensajeError != "" ) 
			{
				this.ShowMessage( mensajeError );
				//return;
			}
			//----------
		}

		#endregion GenerarReporte

		#region GenerarDocumento
		private void GenerarDocumento( Berke.DG.ViewTab.vRenovados view )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= view.Adapter.Db;

			int idiomaID =  (int)GlobalConst.Idioma.ESPANOL ;

			#region Leer Plantilla

			string template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("repRenovadas", idiomaID );
		
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );

			CodeGenerator tabla		= cg.ExtraerTabla("tabla" );
		    
			CodeGenerator filaTit		= tabla.ExtraerFila("filaTit" );
			CodeGenerator fila			= tabla.ExtraerFila("fila" );
			
			#endregion Obtener "Generators"
			
			cg.clearText();
			cg.copyTemplateToBuffer();
			tabla.clearText();
			tabla.copyTemplateToBuffer();
			filaTit.clearText();
			filaTit.copyTemplateToBuffer();
			filaTit.addBufferToText();

			fila.clearText();

			int filas = 0;

			for (view.GoTop(); !view.EOF; view.Skip())
			{
				if (!view.IsRowDeleted)
				{
					fila.copyTemplateToBuffer();
				
					if ((view.Dat.CorrespNro.AsString != "") || (view.Dat.CorrespAnio.AsString != ""))
					{
						fila.replaceField("corresp", view.Dat.CorrespNro.AsString + '/' + view.Dat.CorrespAnio.AsString);
					}
					else
					{
						fila.replaceField("corresp", "");
					}

					fila.replaceField("registro", view.Dat.RegistroNro.AsString);
					fila.replaceField("vencimiento", view.Dat.VencimientoFecha.AsString);
					fila.replaceField("denominacion", view.Dat.Denominacion.AsString);
					fila.replaceField("clase", view.Dat.Clase.AsString);
					fila.replaceField("hi", view.Dat.OrdenTrabajo.AsString);
					fila.replaceField("acta", view.Dat.Acta.AsString);

					fila.addBufferToText();

					filas++;
				}
			}

			tabla.replaceLabel("fila", fila.Texto );
			tabla.replaceLabel("filaTit", filaTit.Texto );
			tabla.addBufferToText();
			cg.replaceLabel( "tabla", tabla.Texto );
			cg.replaceField("Hoy", string.Format("{0:d}", DateTime.Today));

			#region Cadena de Filtro
			string filtro = "";
			if (txtVencimReg.Text.Trim() != "")
			{
				string txtvenc = "";
				if (cbPerGracia.Checked)
				{
					txtvenc = this.SplitFechas(txtVencimReg.Text) + " (Periodo de gracia)";
				}
				else
				{
					txtvenc = txtVencimReg.Text;
				}
				filtro = filtro + "Fecha Venc. Registro: " + txtvenc + ". ";
			}
			if (txtFechaInstruc.Text.Trim() != "")
			{
				filtro = filtro + "Fecha Instrucción: " + txtFechaInstruc.Text + ". ";
			}

			cg.replaceField("filtro", filtro);

			#endregion Cadena de Filtro
			
			cg.replaceField("CantReg", filas.ToString());//view.RowCount.ToString());
			cg.addBufferToText();

			string buffer = cg.Texto;

			#region Activar MS-Word
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=repRenovadas.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
			#endregion Activar MS-Word


		}
		#endregion GenerarDocumento

		private int MergeView(Berke.DG.ViewTab.vRenovados view, char oper)
		{
			Berke.DG.ViewTab.vRenovadasSinHIActa vRenSinHIActa = new Berke.DG.ViewTab.vRenovadasSinHIActa(view.Adapter.Db);
			vRenSinHIActa.ClearFilter();

			if ((txtVencimReg.Text.Trim() != "") && (cbPerGracia.Checked))
			{
				vRenSinHIActa.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter(this.SplitFechas(txtVencimReg.Text));
			}
			else
			{
				vRenSinHIActa.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter(txtVencimReg.Text);
			}
			vRenSinHIActa.Dat.FechaInstruccion.Filter = ObjConvert.GetFilter(txtFechaInstruc.Text);
			vRenSinHIActa.ClearOrder();
			string cmd = vRenSinHIActa.Adapter.ReadAll_CommandString();

			if (oper == 'C') 
			{
				vRenSinHIActa.Adapter.ReadAll();
			}
			else
			{
				vRenSinHIActa.Dat.Registro.Order = 1;
				vRenSinHIActa.Adapter.ReadAll();
				
				if (rblOpciones.SelectedIndex == SIN_HI_NI_ACTA)
				{
					view.DeleteAllRows();
				}

				if (vRenSinHIActa.RowCount > 0)
				{
					for (vRenSinHIActa.GoTop(); !vRenSinHIActa.EOF; vRenSinHIActa.Skip())
					{
						view.NewRow();
						view.Dat.CorrespNro.Value = vRenSinHIActa.Dat.CorrespNro.AsInt;
						view.Dat.CorrespAnio.Value = vRenSinHIActa.Dat.CorrespAnio.AsInt;
						view.Dat.ExpedienteID.Value = vRenSinHIActa.Dat.ExpedienteID.AsInt;
						view.Dat.ExpePadre.Value = vRenSinHIActa.Dat.ExpePadre.AsInt;
						view.Dat.Registro.Value = vRenSinHIActa.Dat.Registro.AsString;
						view.Dat.VencimientoFecha.Value = vRenSinHIActa.Dat.VencimientoFecha.AsDateTime;
						view.Dat.RegistroNro.Value = vRenSinHIActa.Dat.RegistroNro.AsInt;
						view.Dat.RegistroAnio.Value = vRenSinHIActa.Dat.RegistroAnio.AsInt;
						view.Dat.Denominacion.Value = vRenSinHIActa.Dat.Denominacion.AsString;
						view.Dat.Clase.Value = vRenSinHIActa.Dat.Clase.AsInt;
						view.Dat.OrdenTrabajo.Value = vRenSinHIActa.Dat.OrdenTrabajo.AsString;
						view.Dat.Acta.Value = vRenSinHIActa.Dat.Acta.AsString;
						view.Dat.FechaInstruccion.Value = vRenSinHIActa.Dat.FechaInstruccion.AsDateTime;
						view.PostNewRow();
					}

					if (rblOpciones.SelectedIndex == TODAS)
					{
						view.Sort();
					}
				}
			}
			
			return vRenSinHIActa.RowCount;
											
		}


		private string SplitFechas(string cadena)
		{
			string [] fechas_sep = {};
			string resultado = "";
			string sep = "";

			if (cadena != "")
			{
				if (cadena.IndexOf('-') != -1)
				{
					sep = "-";
					fechas_sep = cadena.Split(sep.ToCharArray());
				}
				else if (cadena.IndexOf(',') != -1)
				{
					sep = ",";
					fechas_sep = cadena.Split(sep.ToCharArray());
				}
				else
				{
					fechas_sep = cadena.Split(sep.ToCharArray());
				}
			}

			foreach (string s in fechas_sep)
			{
				if (sep == "-")
				{
					if (resultado != "")
					{
						resultado += sep;
					}
					resultado += DateTime.Parse(s).AddMonths(- (int) GlobalConst.PERIODO_GRACIA).ToShortDateString();
				}
				else if (sep == ",")
				{
					if (resultado != "")
					{
						resultado += sep;
					}
					resultado += DateTime.Parse(s).AddMonths(- (int) GlobalConst.PERIODO_GRACIA).ToShortDateString();
				}
				else
				{
					resultado += DateTime.Parse(s).AddMonths(- (int) GlobalConst.PERIODO_GRACIA).ToShortDateString();
				}

			}

			return resultado;
		}

		private void cbPerGracia_CheckedChanged(object sender, EventArgs e)
		{
			if ((cbPerGracia.Checked) && (txtVencimReg.Text != ""))
			{
				txtVencimReg.ReadOnly = true;
				lblAdvertencia.Text = "El listado será generado teniendo en cuenta solamente el periodo de gracia.<br>" +
					"Fecha Vencimiento Registro considerada: " + this.SplitFechas(txtVencimReg.Text);
			}
			else
			{
				lblAdvertencia.Text = "";
				txtVencimReg.ReadOnly = false;
			}
		}
	}
		

	
}
