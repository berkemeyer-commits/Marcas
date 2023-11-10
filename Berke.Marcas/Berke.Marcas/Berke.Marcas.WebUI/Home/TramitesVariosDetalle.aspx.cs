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
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class TramitesVariosDetalle : System.Web.UI.Page
	{
		#region Declaracion de Controles

		protected System.Web.UI.WebControls.TextBox TextBox1;
		#endregion Declaracion de Controles
		
		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{

			if( !IsPostBack )
			{
				#region Asignar TV

				#region Asignar Parametros
		
				// ParamTab
				Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();

				inTB.NewRow(); 
				inTB.Dat.Entero			.Value = Convert.ToInt32(UrlParam.GetParam("otID"));   //Int32
				inTB.PostNewRow(); 
		
	
				#endregion Asignar Parametros
		
				Berke.DG.TVDG outDG =  Berke.Marcas.UIProcess.Model.TV.Read( inTB );

				#region txtCliente

				Berke.DG.ViewTab.ParamTab inTBC	=   new Berke.DG.ViewTab.ParamTab();
				inTBC.NewRow(); 
				inTBC.Dat.Entero.Value = outDG.OrdenTrabajo.Dat.ClienteID.Value;
				inTBC.PostNewRow(); 
				Berke.DG.ViewTab.ListTab outTBC = Berke.Marcas.UIProcess.Model.Cliente.ReadForSelect( inTBC );
				txtCliente.Text = outTBC.Dat.Descrip.AsString;
				lCodCliente.Text = outDG.OrdenTrabajo.Dat.ClienteID.AsString;
				#endregion txtCliente

				chkFacturable.Checked = outDG.OrdenTrabajo.Dat.Facturable.AsBoolean;
				txtObsClientes.Text = outDG.OrdenTrabajo.Dat.Obs.AsString;
				lbReferenciaCliente.Text = outDG.OrdenTrabajo.Dat.RefCliente.AsString;

				txtCorrespNro.Text = outDG.OrdenTrabajo.Dat.CorrNro.AsString;
				txtCorrespAnio.Text = outDG.OrdenTrabajo.Dat.CorrAnio.AsString;
				txtRefCorresp.Text = outDG.OrdenTrabajo.Dat.RefCorr.AsString;

				#region Atencion
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
				atencion.InitAdapter( db );
				atencion.Adapter.ReadByID( outDG.OrdenTrabajo.Dat.AtencionID.AsInt );
				lbAtencion.Text = atencion.Dat.Nombre.AsString;
				#endregion Atencion

				#region DerechoPropio
				Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente();
				expediente.InitAdapter( db );
				expediente.Dat.OrdenTrabajoID.Filter = Convert.ToInt32(UrlParam.GetParam("otID"));
				expediente.Adapter.ReadAll();                
				Berke.DG.DBTab.ExpedienteXPoder expeXpoder = new Berke.DG.DBTab.ExpedienteXPoder();
				expeXpoder.InitAdapter( db );
				expeXpoder.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				expeXpoder.Adapter.ReadAll();
				if (expeXpoder.RowCount <= 0) {
					cbDerechoPropio.Checked = true;
				} else {
					cbDerechoPropio.Checked = false;
				}
				#endregion DerechoPropio				

				#region ExpedienteInstruccion
                Berke.DG.DBTab.Expediente_Instruccion expeInstruccion = new Berke.DG.DBTab.Expediente_Instruccion();
				expeInstruccion.InitAdapter( db );

				//Instrucción Poder
				expeInstruccion.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				expeInstruccion.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.PODER;
				expeInstruccion.Adapter.ReadAll();
				lbInstruccionPoder.Text = expeInstruccion.Dat.Obs.AsString;

				//Instrucción Contabilidad
				expeInstruccion.Dat.ExpedienteID.Filter = expediente.Dat.ID.AsInt;
				expeInstruccion.Dat.InstruccionTipoID.Filter = (int) GlobalConst.InstruccionTipo.CONTABILIDAD;
				expeInstruccion.Adapter.ReadAll();
				lbInstruccionContabilidad.Text = expeInstruccion.Dat.Obs.AsString;
				#endregion ExpedienteInstruccion

				if (outDG.ExpedienteCampo.RowCount > 0)
				{
					dgAsignarDocumentos.DataSource = outDG.ExpedienteCampo.Table;
					dgAsignarDocumentos.DataBind();
					pnlAsignarDocumentos.Visible = true;
				}
//				dgPoderAnterior.DataSource = outDG.vPoderActual.Table;
//				dgPoderAnterior.DataBind();



////////////
				Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca();

				#region Renovacion_Fill


				string [] aActas = outDG.vRenovacionMarca.Dat.Denominacion.AsString.Split( ((String)";, ").ToCharArray() );
				int actanro = 0;
				int actaanio = 0;
				string [] actacompleta;


				foreach( string acta in aActas )
				{
					actacompleta=acta.Split(((String)"/").ToCharArray(),2);
					if( acta.Trim() == "" ) continue;
					actanro	= Convert.ToInt32( actacompleta[0]);
					actaanio= Convert.ToInt32( actacompleta[1]);

					#region Asignar Parametros
					// ActaRegistroPoder
					Berke.DG.ViewTab.ActaRegistroPoder inTB2 =   new Berke.DG.ViewTab.ActaRegistroPoder();
					inTB2.NewRow(); 
					inTB2.Dat.Acta			.Value = actanro;   //Int32
					inTB2.Dat.Anio			.Value = actaanio;   //Int32
					inTB2.PostNewRow(); 
					#endregion Asignar Parametros

					Berke.DG.RenovacionDG outDG2 =  Berke.Marcas.UIProcess.Model.Renovacion.Fill( inTB2 );
					vRM.Table.Rows.Add(outDG2.vRenovacionMarca.Table.Rows[0].ItemArray);

					
					if (vRM.Table.Columns.Contains("TipoTrabajoID"))
					{
						vRM.Table.Columns.Remove("TipoTrabajoID");
					}

					if (vRM.Table.Columns.Contains("HI_Nro"))
					{
						vRM.Table.Columns.Remove("HI_Nro");
					}

					if (vRM.Table.Columns.Contains("HI_Anho"))
					{
						vRM.Table.Columns.Remove("HI_Anho");
					}

					vRM.Table.Columns.Add("TipoTrabajoID");
					vRM.Table.Columns.Add("HI_Nro");
					vRM.Table.Columns.Add("HI_Anho");

					foreach (DataRow dr in vRM.Table.Rows)
					{
						dr["TipoTrabajoID"] = outDG.OrdenTrabajo.Dat.TrabajoTipoID.AsString;
						dr["HI_Nro"] = outDG.OrdenTrabajo.Dat.Nro.AsString;
						dr["HI_Anho"] = outDG.OrdenTrabajo.Dat.Anio.AsString;
					}
							
										
				} // end foreeach


				#endregion Renovacion_Fill

				dgMarcasAsignadas.DataSource = vRM.Table;
				dgMarcasAsignadas.DataBind();


				if (dgMarcasAsignadas.Items.Count != 0)
				{
//					Berke.DG.ViewTab.vPoder vPI = new Berke.DG.ViewTab.vPoder();
//					vPI.NewRow();
//					vPI.Dat.PoderID.Value = vRM.Dat.ClaseEdicionID.AsInt;   //Int32
//					vPI.PostNewRow();
//
//					Berke.DG.ViewTab.vPoder vPO = Berke.Marcas.UIProcess.Model.Poder.Read(vPI);
//					dgPoderActual.DataSource = vPO.Table;
//					dgPoderActual.DataBind();
//
//					Berke.DG.ViewTab.vPoderAnterior vPAnterior =  Berke.Marcas.UIProcess.Model.TV.FillPoderAnterior( vRM );
//					dgPoderActual.DataSource = vPAnterior.Table;
//					dgPoderActual.DataBind();
				}
/////////////////
				#region Obtener Funcionario
				Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
				Func = UIProcess.Model.Funcionario.ReadByID(outDG.OrdenTrabajo.Dat.FuncionarioID.AsInt);
				lFuncionario.Text = Func.Dat.Funcionario.AsString;
				#endregion Obtener Funcionario

				lfecha.Text = outDG.OrdenTrabajo.Dat.AltaFecha.AsString;

				bool existePoderActual = true;
				cbDerechoPropio.Enabled = false;
				pnlAnterior.Visible = true;
				pnlActual.Visible= false;

				switch(outDG.OrdenTrabajo.Dat.TrabajoTipoID.AsInt)
				{
					#region Transferencia
					
					case (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA :

						lblTV.Text = "Transferencia (TR)";
						lbDerechoPropio.Visible = true;
						cbDerechoPropio.Visible = true;
						break;

					#endregion Transferencia

					#region Cambio de Nombre

					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE :
						
						lblTV.Text = "Cambio de Nombre (CN)";
						lbDerechoPropio.Visible = true;
						cbDerechoPropio.Visible = true;
						break;

					#endregion Cambio de Nombre

					#region Fusion

					case (int) GlobalConst.Marca_Tipo_Tramite.FUSION :

						lblTV.Text = "Fusión (FUS)";
						lbDerechoPropio.Visible = false;
						cbDerechoPropio.Visible = false;
						break;

					#endregion Fusion

					#region Cambio de Domicilio

					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO :

						lblTV.Text = "Cambio de Domicilio (CD)";
						lbDerechoPropio.Visible = true;
						cbDerechoPropio.Visible = true;
						break;

					#endregion Cambio de Domicilio

					#region Licencia

					case (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA :

						lblTV.Text = "Licencia (LIC)";
						existePoderActual = false;						
						lbDerechoPropio.Visible = false;
						cbDerechoPropio.Visible = false;
						break;

					#endregion Licencia

					#region Duplicado de Titulo

					case (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO :

						lblTV.Text = "Duplicado de Título (DUP)";
						existePoderActual = false;
						lbDerechoPropio.Visible = false;
						cbDerechoPropio.Visible = false;
						dgMarcasAsignadas.Columns[7].Visible = false;

						/*
						Berke.DG.ViewTab.vHIPoder invHIPoder =   new Berke.DG.ViewTab.vHIPoder();
						invHIPoder.NewRow(); 
						invHIPoder.Dat.OrdenTrabajoID			.Value = Convert.ToInt32(UrlParam.GetParam("otID"));   //Int32
						invHIPoder.Dat.Anterior			.Value = true;   //Boolean
						invHIPoder.PostNewRow(); 
						Berke.DG.ViewTab.vPoderExpe PoderAnterior =  Berke.Marcas.UIProcess.Model.TV.FillPoder( invHIPoder );
						dgPoderAnterior.DataSource = PoderAnterior.Table;
						dgPoderAnterior.DataBind();
						*/
						break;

					#endregion Duplicado de Titulo

					#region Cambio de Nombre y Domicilio
					case (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC:						
						lblTV.Text = "Cambio de Nombre y Domicilio(CND)";
						lbDerechoPropio.Visible = true;
						cbDerechoPropio.Visible = true;
						break;

					#endregion Cambio de Nombre y Domicilio

					
					
					
			}

				lblHI.Text = "Hoja de Inicio Nro." + outDG.OrdenTrabajo.Dat.OrdenTrabajo.AsString;
				

				// vHIPoder
				Berke.DG.ViewTab.vHIPoder invHIPoder =   new Berke.DG.ViewTab.vHIPoder();
				invHIPoder.NewRow(); 
				invHIPoder.Dat.OrdenTrabajoID			.Value = Convert.ToInt32(UrlParam.GetParam("otID"));   //Int32
				invHIPoder.Dat.Anterior			.Value = true;   //Boolean
				invHIPoder.PostNewRow(); 
				Berke.DG.ViewTab.vPoderExpe PoderAnterior =  Berke.Marcas.UIProcess.Model.TV.FillPoder( invHIPoder );
				dgPoderAnterior.DataSource = PoderAnterior.Table;
				dgPoderAnterior.DataBind();

				if (existePoderActual) {
					Berke.DG.ViewTab.vHIPoder invHIPoder2 =   new Berke.DG.ViewTab.vHIPoder();
					invHIPoder2.NewRow(); 
					invHIPoder2.Dat.OrdenTrabajoID			.Value = Convert.ToInt32(UrlParam.GetParam("otID"));   //Int32
					invHIPoder2.Dat.Anterior			.Value = false;   //Boolean
					invHIPoder2.PostNewRow(); 
					Berke.DG.ViewTab.vPoderExpe PoderActual =  Berke.Marcas.UIProcess.Model.TV.FillPoder( invHIPoder2 );
					if (PoderActual.RowCount > 0) {
						dgPoderActual.DataSource = PoderActual.Table;
						dgPoderActual.DataBind();
						pnlActual.Visible= true;
					}
				}
				#endregion Asignar TV

				#region Habilitar Borrado de HI
				btnEliminar.Enabled = false;
				btnEliminar.Visible = false;
				if ( (Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("opBorrar") == "S") &
					 (Berke.Libs.Base.Acceso.OperacionPermitida("delHI")) ) {
					btnEliminar.Enabled = true;
					btnEliminar.Visible = true;
				}
				#endregion Habilitar Borrado de HI
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

		#region Eventos Controles


		#region btnGrabar

		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			 Response.Redirect("login.aspx");
		}
		
		#endregion btnGrabar

		#region btnEliminar_Click
		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			#region Asignar Parametros
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			inTB.Dat.Entero.Value = Convert.ToInt32(UrlParam.GetParam("otID"));
			inTB.PostNewRow();
			#endregion Asignar Parametros

			Berke.DG.ViewTab.ParamTab outTB = Berke.Marcas.UIProcess.Model.TV.Delete( inTB );
			if (outTB.Dat.Logico.AsBoolean) {				
				string scriptCliente= "<script language='javascript'>alert('La HI fue eliminada exitosamente')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			} 
			else 
			{
				string scriptCliente= "<script language='javascript'>alert('Atención. NO se pudo eliminar la HI : " + outTB.Dat.Alfa.AsString + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
		}
		#endregion btnEliminar_Click

		protected void dgMarcasAsignadas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		#endregion Eventos Controles
	}
}
