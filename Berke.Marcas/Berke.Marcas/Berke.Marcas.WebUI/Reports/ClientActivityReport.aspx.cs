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

namespace Berke.Marcas.WebUI.Reports
{
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base;
	using Berke.Marcas.BizActions.Reports;

	/// <summary>
	/// Reporte que presenta estadísticas sobre los trámites de los clientes
	/// </summary>
	public partial class ClientActivityReport : System.Web.UI.Page
	{
		#region Atributos

		int MAX_ROWS = 10000;
		System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");

		#endregion Atributos
	
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

			//Asignar delegados
			this.cbxClienteID.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
			this.cbxPropietarioID.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxPropietarioID_LoadRequested); 
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
			this.generar();
		}

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));
		}
		#endregion ShowMessage

		private DateTime fecdesde;
		private DateTime fechasta;
		private bool parametrosOK() 
		{
			try 
			{
				//DateTime.Parse(txtDesde.Text.Trim().ToString(), _UI_NumberCultureFormat );
				//DateTime.Parse(txtHasta.Text.Trim().ToString(), _UI_NumberCultureFormat );
				fecdesde = Convert.ToDateTime(txtFecInicio.Text.Trim().ToString(), culture);
				fechasta = Convert.ToDateTime(txtFecFin.Text.Trim().ToString(), culture);
			}
			catch(Exception e) 
			{
				ShowMessage("Formato de fecha incorrecto.");
				return false;
			}
			return true;
		}

		private void generar()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			if (!parametrosOK()) return;

			ClientActivityDoc doc = new ClientActivityDoc(db);

			try 
			{
				Berke.DG.ViewTab.vClientActivity vClient = new Berke.DG.ViewTab.vClientActivity(db);
				vClient.Dat.ClienteID.Filter = ObjConvert.GetFilter(this.cbxClienteID.SelectedValue);
				vClient.Dat.PropietarioID.Filter = ObjConvert.GetFilter(this.cbxPropietarioID.SelectedValue);
				vClient.Adapter.AddParam("fecinicio", fecdesde);
                vClient.Adapter.AddParam("fecfin",    fechasta);
				vClient.Adapter.Distinct = true;
				// Orden
				vClient.Dat.ClienteID.Order     = 1;
				vClient.Dat.PropietarioID.Order = 2;
				vClient.Adapter.ReadAll(MAX_ROWS);
				//string SQL = vClient.Adapter.ReadAll_CommandString();
				//int cant_filas = vClient.Adapter.Count();

				if (vClient.RowCount > 0)
				{
					doc.setDataSet(vClient.Table);
					doc.setFechaInf(fecdesde);
					doc.setFechaSup(fechasta);
					doc.showNuestras(chkIncNuestras.Checked);
					doc.showTerceros(chkInTerceros.Checked);
					doc.showResumen(chkIncResumen.Checked);

					string xml = doc.generar();

					ReportUtils.ActivarWord(Response,"client-activity.doc", xml);
				}
				else
				{
					ShowMessage("No se encontraron registros.");
				}
			}
			catch(Exception ex)
			{
				db.RollBack();
				db.CerrarConexion();
				throw ex;
			}
			db.CerrarConexion();
			
		}
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

	}
}
