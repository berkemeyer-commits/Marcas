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
	using UIPModel = UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;

	
	public partial class RegAduanaDetalle : System.Web.UI.Page
	{
		#region Declaracion de los controles


		#endregion
	
		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

				db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
				db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

				Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
				Session["marcaID"]=0;
				inTB.NewRow(); 
				inTB.Dat.Entero			.Value = Const.NIZAEDICIONID_VIGENTE;   //Int32
				inTB.PostNewRow(); 
		
				Berke.DG.DBTab.Clase clase =  Berke.Marcas.UIProcess.Model.Renovacion.ReadClase( inTB );

				string OtID_Param;
				OtID_Param	= Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("OtID");  // Operacion indicada en UrlParam  .Mantenimiento, Consulta,....
				Berke.DG.RenovacionDG inDG = new Berke.DG.RenovacionDG();

				// OrdenTrabajo
				Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
				ot.NewRow(); 
				ot.Dat.ID			.Value = OtID_Param; //int PK  Oblig.
				ot.PostNewRow(); 
				Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Read( inDG );

				lRenovacion.Text =  "Nro." + outDG.OrdenTrabajo.Dat.OrdenTrabajo.AsString;
				lfecha.Text = outDG.OrdenTrabajo.Dat.AltaFecha.AsString;
				chkFacturable.Checked = outDG.OrdenTrabajo.Dat.Facturable.AsBoolean;
				lObs.Text = outDG.OrdenTrabajo.Dat.Obs.AsString;

				#region Obtener Funcionario
				Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
				Func = UIProcess.Model.Funcionario.ReadByID(outDG.OrdenTrabajo.Dat.FuncionarioID.AsInt);
				lFuncionario.Text = Func.Dat.Funcionario.AsString;
				#endregion Obtener Funcionario

//				#region Obtener Propietario y Direccion
//				this.lPropietario.Text = outDG.Marca.Dat.Propietario.AsString;
//				this.lPropDir.Text = outDG.Marca.Dat.ProDir.AsString;
//			
//				#endregion Obtener Propietario y Direccion

				#region Obtener Atencion
				Berke.DG.DBTab.Atencion ate = new Berke.DG.DBTab.Atencion( db);
				ate.Adapter.ReadByID(outDG.OrdenTrabajo.Dat.AtencionID.AsInt);
				this.lAtencion.Text = ate.Dat.Nombre.AsString;
				#endregion Obtener Atencion

				#region Obtener Instruccion de Poder y Contabilidad
				for (outDG.Expediente_Instruccion.GoTop();!outDG.Expediente_Instruccion.EOF;outDG.Expediente_Instruccion.Skip())
				{
					int poda=(int)Berke.Libs.Base.GlobalConst.InstruccionTipo.PODER;
					int conta=(int) Berke.Libs.Base.GlobalConst.InstruccionTipo.CONTABILIDAD;
					if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt ==poda )
					{
						this.lInstPoder.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
					}

					if (outDG.Expediente_Instruccion.Dat.InstruccionTipoID.AsInt == conta)
					{
						this.lInstCont.Text = outDG.Expediente_Instruccion.Dat.Obs.AsString;
					}
				}
				#endregion Obtener Instruccion de Poder y Contabilidad

				txtReferenciaNro.Text = outDG.OrdenTrabajo.Dat.CorrNro.AsString;
                txtReferenciaAnio.Text = outDG.OrdenTrabajo.Dat.CorrAnio.AsString;
				//lRefCorr.Text = outDG.Documento.Dat.ReferenciaExterna.AsString;

				lRefCorr.Text = outDG.OrdenTrabajo.Dat.RefCliente.AsString;

				#region Obtener Cliente
				Berke.DG.DBTab.Cliente cli = new Berke.DG.DBTab.Cliente( db );
				cli.Adapter.ReadByID( outDG.OrdenTrabajo.Dat.ClienteID.AsInt);

				//this.lDireccion.Text = cli.Dat.Correo.AsString;
				this.txtCorreo.Text  = cli.Dat.Correo.AsString;

				Berke.DG.DBTab.CIdioma idioma = new Berke.DG.DBTab.CIdioma( db );
				idioma.Adapter.ReadByID(cli.Dat.IdiomaID.AsInt);
				this.lidioma.Text = idioma.Dat.descrip.AsString;
				txtCliente.Text = cli.Dat.Nombre.AsString;
				lCodCliente.Text = cli.Dat.ID.AsString;

				#endregion Obtener Cliente 

				
				#region Detalle de Registro y Vencimiento
				
				outDG.vRenovacionMarca.Dat.Vencimiento.Order=1;
				outDG.vRenovacionMarca.Dat.Denominacion.Order=2;
				outDG.vRenovacionMarca.Sort();

				for (outDG.vRenovacionMarca.GoTop();!outDG.vRenovacionMarca.EOF;outDG.vRenovacionMarca.Skip())
				{
					lblRegistros.Text +=  outDG.vRenovacionMarca.Dat.RegistroNro.AsString + " " + outDG.vRenovacionMarca.Dat.Vencimiento.AsString + " - " ;

				}
				outDG.vRenovacionMarca.GoTop();
				
				#endregion Detalle de Registro y Vencimiento



				dtlMarcasAsignadas.DataSource=outDG.vRenovacionMarca.Table;
				dtlMarcasAsignadas.DataBind();
				lTotalMarcas.Text = lTotalMarcas.Text + outDG.vRenovacionMarca.RowCount.ToString();

				#region Obtener Poder
				Berke.DG.DBTab.Poder Pod = new Berke.DG.DBTab.Poder( db);
				Berke.DG.DBTab.ExpedienteXPoder eXP = new Berke.DG.DBTab.ExpedienteXPoder( db );
				eXP.Dat.ExpedienteID.Filter = outDG.vRenovacionMarca.Dat.ExpedienteID.AsInt;
				System.Collections.ArrayList list = eXP.Adapter.GetListOfField(eXP.Dat.PoderID);
				if(list.Count < 1)
				{
					lPoder.Text="Por Derecho Propio";
				}
				else
				{
					Pod.Dat.ID.Filter =new Libs.Base.DSHelpers.DSFilter(list); 
					Pod.Adapter.ReadAll();
					/*rgimenez*/
					/*
					dgPoderActual.DataSource = Pod.Table;
					dgPoderActual.DataBind();
					*/
					dtlPoderActual.DataSource= Pod.Table;
					dtlPoderActual.DataBind();

				}

				#endregion Obtener Poder

				#region Obtener Propietario
				Berke.DG.DBTab.Propietario Prop = new Berke.DG.DBTab.Propietario( db);
				Berke.DG.DBTab.ExpedienteXPropietario eXPro = new Berke.DG.DBTab.ExpedienteXPropietario( db );
				eXPro.Dat.ExpedienteID.Filter = outDG.vRenovacionMarca.Dat.ExpedienteID.AsInt;
				System.Collections.ArrayList list2 = eXPro.Adapter.GetListOfField(eXPro.Dat.PropietarioID);
				Prop.Dat.ID.Filter =new Libs.Base.DSHelpers.DSFilter(list2); 
				Prop.Adapter.ReadAll();
				dtlPropietario.DataSource = Prop.Table;
				dtlPropietario.DataBind();


				#endregion Obtener Poder

				#region Tratamiento para caso que venga de una consulta
				if( Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("oper") == "Consulta" )
				{
					BntGrabar.Text = "Volver";
					BntGrabar.Attributes.Add("onclick", "history.back(); return false;");
				}
				#endregion

				#region Habilitar Borrado de HI
				btnEliminar.Enabled = false;
				btnEliminar.Visible = false;
				if ( (Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("opBorrar") == "S") &
					(Berke.Libs.Base.Acceso.OperacionPermitida("delHI")) ) 
				{
					btnEliminar.Enabled = true;
					btnEliminar.Visible = true;
				}
				#endregion Habilitar Borrado de HI
			}
		}
		#endregion

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
			this.dtlMarcasAsignadas.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlMarcasAsignadas_ItemDataBound);

		}
		#endregion

		#region Accion de los controles
		protected void BntGrabar_Click(object sender, System.EventArgs e)
		{
						Response.Redirect("RegAduana.aspx");
		}
		
	
		#endregion

		#region eccCliente_LoadRequested
		private void eccCliente_LoadRequested(ecWebControls.ecCombo combo, System.EventArgs e)
		{		
			#region Asignar Parametros
			Berke.DG.ViewTab.ParamTab inTB	=   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )
			{
				inTB.Dat.Entero.Value = Convert.ToInt32( combo.Text );
			}
			else
			{
				inTB.Dat.Alfa.Value = combo.Text;			
			}
			inTB.PostNewRow(); 
			#endregion Asignar Parametros
		
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.Cliente.ReadForSelect( inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
	
		}
		#endregion eccCliente_LoadRequested

		#region btnCancelar_Click
		private void btnCancelar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Renovacion.aspx");
		}
		#endregion btnCancelar_Click

		#region dgMarcasAsignadas_DeleteCommand
		private void dgMarcasAsignadas_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Berke.DG.ViewTab.vRenovacionMarca vRM = new Berke.DG.ViewTab.vRenovacionMarca((DataTable) Session["MarcasAsignadas"]);
			vRM.Go(e.Item.ItemIndex);
			vRM.Delete();			
			vRM.AcceptAllChanges();
			Grid.Bind(this.dgMarcasAsignadas, vRM.Table);
			if( vRM.RowCount > 0 )
			{
//				pnlMarcasRenovar.Visible = true ;
//				pnlBotones.Visible = true;
			}
			else
			{
				pnlMarcasRenovar.Visible = false ;
				pnlBotones.Visible = false;	
			}
		}
		#endregion dgMarcasAsignadas_DeleteCommand

		#region btnEliminar_Click
		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			string OtID_Param = Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("OtID");
			Berke.DG.RenovacionDG inDG = new Berke.DG.RenovacionDG();
			Berke.DG.DBTab.OrdenTrabajo ot = inDG.OrdenTrabajo ;
			ot.NewRow(); 
			ot.Dat.ID.Value = OtID_Param;
			ot.PostNewRow();			
			Berke.DG.RenovacionDG outDG =  Berke.Marcas.UIProcess.Model.Renovacion.Read( inDG );
			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.Renovacion.RegAduanaDelete( outDG );
			if (outTB.Dat.Logico.AsBoolean) {				
				string scriptCliente= "<script language='javascript'>alert('La HI fue eliminada exitosamente')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			} else {
				string scriptCliente= "<script language='javascript'>alert('Atención. NO se pudo eliminar la HI : " + outTB.Dat.Alfa.AsString + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
		}
		#endregion btnEliminar_Click

		#region dtlMarcasAsignadas_ItemDataBound
		private void dtlMarcasAsignadas_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.ViewTab.vExpedienteDistribuidor vExpeDistr = new Berke.DG.ViewTab.vExpedienteDistribuidor();
			vExpeDistr.InitAdapter( db );
			
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				Label vlblIdExpediente = (Label) e.Item.FindControl("lblIdExpediente");
				System.Web.UI.WebControls.Label lblDistribuidores  = (System.Web.UI.WebControls.Label) e.Item.FindControl("lblDistribuidores");
				vExpeDistr.ClearFilter();
				vExpeDistr.Dat.ExpedienteID.Filter = Convert.ToInt32(vlblIdExpediente.Text);
				vExpeDistr.Adapter.ReadAll();
				if (vExpeDistr.RowCount > 0) 
				{
					lblDistribuidores.Text = this.getDistribuidoresTable(vExpeDistr);
				} 
			}

			/*Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente();
			expe.InitAdapter( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca();
			marca.InitAdapter( db );

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				Label vlblIdExpediente = (Label) e.Item.FindControl("lblIdExpediente");
				System.Web.UI.WebControls.Image vImgLogo = (System.Web.UI.WebControls.Image) e.Item.FindControl("ImgLogo");
				System.Web.UI.WebControls.Label lblLogo  = (System.Web.UI.WebControls.Label) e.Item.FindControl("lblLogo");
				expe.Adapter.ReadByID(Convert.ToInt32(vlblIdExpediente.Text));
				marca.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
				if (marca.Dat.LogotipoID.AsInt > 0) {
					//vImgLogo.ImageUrl = "Imagen.aspx?logotipoID=" + marca.Dat.LogotipoID.AsString;
					lblLogo.Text = "<img onLoad=\"javascript:redimensionar(this);\" src=\"Imagen.aspx?logotipoID="+marca.Dat.LogotipoID.AsString +"\"  >";
				} else {
					//vImgLogo.Visible = false;
					lblLogo.Visible = false;
				}
			}*/
		}
		#endregion dtlMarcasAsignadas_ItemDataBound

		public string getDistribuidoresTable(Berke.DG.ViewTab.vExpedienteDistribuidor view)
		{
			//string boxPattern		= "<div id='{0}'><table class=\"tabla_jrk\"><tr><td class=\"td_header\"><a  onclick=\"setVisible('{1}');\"><img id='img_{1}' src=\"../Tools/imx/minus.bmp\" border=\"0\"></a> {3} </td><td width='10' class='td_button'><a  onclick=\"closeDiv('{0}');\"> <img src=\"../Tools/imx/close.gif\" border=\"0\"></a></td></tr><tr><td><div id='{1}'> {2}</div></td></tr></table></div>";		
			//string boxPattern		= "<table class=\"tabla_jrk\"><tr><td class=\"td_header\"> {0}</td></tr></table>";		
		
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");

			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));

			#region Cabecera
			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("Producto Servicio");
			tab.AddCell("Distribuidor");
			tab.EndRow();
			
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			
			#region Detalle
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));

			for( view.GoTop(); ! view.EOF; view.Skip())
			{
				
				tab.BeginRow();
				tab.AddCell(this.chkSpc(view.Dat.Producto_Servicio.AsString));
				tab.AddCell(this.chkSpc(view.Dat.DistribuidorNombre.AsString));
				tab.EndRow();
			}

			#endregion Detalle


			Berke.Html.HtmlTextFormater txtFmt = new Berke.Html.HtmlTextFormater();
			txtFmt.Bold = true;
			txtFmt.Size = "-3";
			//return  string.Format(boxPattern, tab.Html());
			return tab.Html();

		}

		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}
	}
}