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

namespace Berke.Marcas.WebUI.Home
{
	using Berke.Libs.Base;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class CambioCliente : System.Web.UI.Page
	{
		#region Declaracion de Controles


		#endregion Declaracion de Controles
		
		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				this.txtCorrespAnio.Text = System.DateTime.Now.Year.ToString();
				this.lblCorrespondencia.Text = "";
                this.chkEliminarInstrVig.Checked = false;
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
			this.eccClienteNuevo.LoadRequested += new ecWebControls.LoadRequestedHandler(this.eccClienteNuevo_LoadRequested);
			this.eccCliente.LoadRequested += new ecWebControls.LoadRequestedHandler(this.eccCliente_LoadRequested);
			this.eccPropietario.LoadRequested += new ecWebControls.LoadRequestedHandler(this.eccPropietario_LoadRequested);

		}
		#endregion

		#region Eventos Controles

		#region btnAsignarMarcas

		protected void btnAsignarMarcas_Click(object sender, System.EventArgs e)
		{

			#region CambioCliente_Read

				#region Asignar Parametros
		
				
		
				// RegistrosActasClientePropietario
				Berke.DG.ViewTab.RegistrosActasClientePropietario inTB =   new Berke.DG.ViewTab.RegistrosActasClientePropietario();

				inTB.NewRow();
				inTB.Dat.ClienteID			.Value = DBNull.Value;   //Int32
				inTB.Dat.PropietarioID		.Value = DBNull.Value;   //Int32

				if (eccCliente.Mode == ecWebControls.ComboMode.ShowResult)
				{
					inTB.Dat.ClienteID			.Value = eccCliente.SelectedValue;   //Int32
				}
				if (eccPropietario.Mode == ecWebControls.ComboMode.ShowResult)
				{
					inTB.Dat.PropietarioID			.Value = eccPropietario.SelectedValue;   //Int32
				}
				inTB.Dat.Registros		.Value = txtRegistros.Text;   //String
				inTB.Dat.Actas			.Value = txtActas.Text;   //String
				inTB.Dat.EsTVS			.Value = "N";

				inTB.PostNewRow(); 
		
	
				#endregion Asignar Parametros
		
				Berke.DG.ViewTab.vMarcaClientePropietario vMCP =  Berke.Marcas.UIProcess.Model.CambioCliente.Read( inTB );

			#endregion CambioCliente_Read

			string	menError	= "Atención.";
		//	bool	inputError	= false;

			dgMarcasAsignadas.DataSource = vMCP.Table;
			dgMarcasAsignadas.DataBind();

			Session["MarcasAsignadas"] = vMCP.Table;

			if (dgMarcasAsignadas.Items.Count != 0)
			{
				pnlMostrarMarcas.Visible = true;
				pnlBusquedaMarcas.Visible = false;

			}
			else 
			{
				string scriptCliente= "<script language='javascript'>alert('No se encontraron marcas con los criterios de búsqueda dados')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
			if (menError != "Atención.")
			{
				//Sucedio al menos un error
				string scriptCliente= "<script language='javascript'>alert('" + menError + "')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}
//			if ((pnlMostrarMarcas.Visible == true))
//			{
//				pnlGrabar.Visible = true;
//			}
		}
		#endregion btnAsignarMarcas

		#region btnVolverMarcas

		protected void btnVolverMarcas_Click(object sender, System.EventArgs e)
		{
			pnlMostrarMarcas.Visible = false;
			pnlBusquedaMarcas.Visible = true;
			pnlGrabar.Visible = false;
		}

		#endregion btnVolverMarcas

		#region eccCliente

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

		#endregion eccCliente

		#region eccClienteNuevo

		private void eccClienteNuevo_LoadRequested(ecWebControls.ecCombo combo, System.EventArgs e)
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

		#endregion eccClienteNuevo

		#region btnGrabar

		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			if (eccClienteNuevo.Mode != ecWebControls.ComboMode.ShowResult)
			{
				string scriptCliente= "<script language='javascript'>alert('Por Favor seleccione el Cliente Nuevo!')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				return;   //Int32
			}

			#region Obtener ID de Funcionario

			Berke.DG.ViewTab.vFuncionario Func = new Berke.DG.ViewTab.vFuncionario();
			Func = UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
			int FuncionarioID = Func.Dat.ID.AsInt;

			#endregion Obtener ID de Funcionario

			Berke.DG.ViewTab.vMarcaClientePropietario vMCP	= new Berke.DG.ViewTab.vMarcaClientePropietario((DataTable) Session["MarcasSeleccionadas"]);

			#region CambioCliente_Upsert

			foreach( DataGridItem item in this.dgConfirmacion.Items )
			{
				vMCP.Go(item.ItemIndex);
                vMCP.Edit();
                vMCP.Dat.ClienteID.Value = this.chkEliminarInstrVig.Checked ? 
                                            Convert.ToInt32(eccClienteNuevo.SelectedValue) * -1 : 
                                            Convert.ToInt32(eccClienteNuevo.SelectedValue);
				vMCP.Dat.PropietarioID.Value = FuncionarioID; //FUNCIONARIO ID
				vMCP.Dat.Denominacion.Value = txtObsClientes.Text;//OBS
				vMCP.Dat.Registro.Value = lCorrID.Text;// CORRESPONDENCIA ID
				vMCP.PostEdit();
			}
			vMCP.AcceptAllChanges();
			#region Llamada al MODEL

			Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.CambioCliente.Upsert( vMCP );
			if (outTB.Dat.Logico.AsBoolean)
			{
				string scriptCliente= "<script language='javascript'>alert('Cambio de Cliente realizado!!!')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
				btnGrabar.Visible = false;
				btnSalir.Text = "Aceptar";
			}
			else
			{
				string scriptCliente= "<script language='javascript'>alert('Atención. Cambio de Cliente NO Realizado!')</script>";
				Page.RegisterClientScriptBlock("MsgCliente", scriptCliente );
			}

			#endregion Llamada al MODEL

			#endregion CambioCliente_Upsert


		}
		
		#endregion btnGrabar

		protected void btnSalir_Click(object sender, System.EventArgs e)
		{
            Response.Redirect("CambioCliente.aspx");
		}


		#endregion Eventos Controles

		#region eccPropietario
		private void eccPropietario_LoadRequested(ecWebControls.ecCombo combo, System.EventArgs e)
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
		
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.Propietario.ReadForSelect( inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		
		}
		#endregion eccPropietario

		protected void bConfirmar_Click(object sender, System.EventArgs e)
		{

			Berke.DG.ViewTab.vMarcaClientePropietario vMCP	= new Berke.DG.ViewTab.vMarcaClientePropietario((DataTable) Session["MarcasAsignadas"]);

			#region Eliminar los NO seleccionados
			vMCP.GoTop();

			foreach( DataGridItem item in this.dgMarcasAsignadas.Items )
			{
				CheckBox chkIncluir = (CheckBox) item.FindControl("chkSel");
				//vMCP.Go(item.ItemIndex);
				if( chkIncluir.Checked != true )
				{
					vMCP.Delete();
				}
				vMCP.Skip();
//				else
//				{
//					vMCP.Edit();
//					vMCP.Dat.ClienteID.Value = eccClienteNuevo.SelectedValue;
//					vMCP.Dat.PropietarioID.Value = FuncionarioID;
//					vMCP.Dat.Cliente.Value = eccClienteNuevo.Text;
//					vMCP.PostEdit();
//				}
			}
			vMCP.AcceptAllChanges();


			#endregion Eliminar los NO seleccionados

			dgConfirmacion.DataSource = vMCP.Table;
			dgConfirmacion.DataBind();
			Session["MarcasSeleccionadas"]= vMCP.Table;
			//pnlCliente.Visible = false;
			//eccClienteNuevo.Enabled = false;
			pnlAsignarMarcas.Visible = false;
			pnlConfirmacion.Visible = true;
			pnlGrabar.Visible = true;

		}

		protected void bVerificar_Click(object sender, System.EventArgs e)
		{
			this.Correspondencia_Read(txtCorrespNro.Text, txtCorrespAnio.Text);
		}


		#region Correspondencia_Read
		private void Correspondencia_Read( string Nro, string Anio )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			//			Berke.DG.DBTab.Correspondencia corresp = new Berke.DG.DBTab.Correspondencia( db );
			Berke.DG.ViewTab.vCorrespondencia corresp = new Berke.DG.ViewTab.vCorrespondencia(db );
			Berke.DG.ViewTab.vCorrespNro correspnroarea = new Berke.DG.ViewTab.vCorrespNro(db);

			corresp.Dat.Nro.Filter = Nro;
			corresp.Dat.Anio.Filter = Anio;
			corresp.Adapter.ReadAll();
			string path = "";

			if( corresp.RowCount > 0 )
			{

				correspnroarea.Adapter.ClearParams();
				correspnroarea.Adapter.AddParam("nro",corresp.Dat.Nro.AsInt);
				correspnroarea.Dat.vigente.Filter = true;
				correspnroarea.Adapter.ReadAll();

				if ( correspnroarea.RowCount == 1) 
				{
					if ((correspnroarea.Dat.IDArea.AsInt != 0 ))
					{
						 path = Berke.Libs.Base.DocPath.digitalDocPath(
							corresp.Dat.Anio.AsInt, corresp.Dat.Nro.AsInt, 
							correspnroarea.Dat.IDArea.AsInt );
					}
				}



				string des = "";
				des += "Corresp.ID: <b>" + corresp.Dat.ID.AsString + "</b> "+path+"<br>";
				des += "Fecha Ing.: <b>" + corresp.Dat.FechaAlta.AsString + "</b><br>";
				des += "Referencia: <b>" + corresp.Dat.RefCorresp.AsString + "</b><br>";
				des += "Remitente : <b>" + corresp.Dat.Nombre.AsString + "</b><br>";
	
				this.lblCorrespondencia.Text = des;
				lCorrID.Text = corresp.Dat.ID.AsString;
		
	
				db.CerrarConexion();
			}
			else
			{
				db.CerrarConexion();		
				ShowMessage( " La correspondencia "+Nro+"/"+Anio+" No está registrada en el sistema" );
			}

		}
		#endregion Correspondencia_Read

		#region digitalDocPath
		public string digitalDocPath( int pAnio, int pNumero, int area )
		{
		
			string fileTemplate = "";
			string numero = pNumero.ToString();

			switch( area )
			{
				case	1  : //		Marcas	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 3  : //		Poder
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 6  : //		Litigios 
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosAdm\{0}\TIF\{1}.tif";
					break;
				case 7  : //		Patentes
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Patentes\{0}\TIF\{1}.tif";
					break;
				case 8  : //		Legal Division	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosJud\{0}\TIF\{1}.tif";
					break;
				case 10 : //		General	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 14 : //		Contabilidad	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Contabilidad\{0}\TIF\{1}.tif";
					break;
				default :
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
			}
			if( fileTemplate == "" )
			{
				return "";
			}

			//			string anchorTemplate = @"<A onclick=""window.open('File:{0}')"" href=""{0}"">&nbsp;&nbsp;Doc.Digital </a>";
			string anchorTemplate = @"<A href=""{0}"">&nbsp;&nbsp;{1} </a>";
			
			#region Llenar numero con ceros a la izquierda
			if( numero.Length < 5 && numero.Length > 0 )
			{
				numero = ((string)"00000").Substring(0, 5 - numero.Length) + numero;
			}
			#endregion

			string arch = string.Format( fileTemplate, pAnio.ToString(), numero );

			System.IO.FileInfo inf = new System.IO.FileInfo(arch);
			if( ! inf.Exists )
			{ 
				return string.Format( anchorTemplate, arch, "" );;
			}
			else
			{
				return string.Format( anchorTemplate, arch , "Ver Doc." );
			}
			//			return string.Format( anchorTemplate, arch );
		}
		#endregion digitalDocPath

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

	}
}
