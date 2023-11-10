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
using Berke.Libs.Base;



namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for PoderSelec.
	/// </summary>
	public partial class PoderConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form

		#endregion 
	
		#region Variables Globales

		#endregion Variables Globales
		
		#region Properties

		#region Url_Oper
		private string Url_Oper
		{
			get{ return (string) ViewState["Url_Oper"]  ; }
			set{ ViewState["Url_Oper"] = value ;}
		}
		#endregion Url_Oper

		#endregion Properties

		#region Asignar Delegados
		private void AsignarDelegados()
		{
			//this.cbxPropietarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.eccPropietario_LoadRequested);

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region PoderTipo DropDown
			SimpleEntryDS se = Berke.Marcas.UIProcess.Model.PoderTipo.ReadForSelect();
			//ddlPoderTipoID.Fill( se.Tables[0], true);
			#endregion 


		}
		#endregion Asignar Valores Iniciales


		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{

			if( !IsPostBack )
			{
				MostrarPanel_Busqueda();
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

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
		
			#region Asignar Parametros 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Framework.Core.Config.GetConfigParam("CURRENT_SERVER");

			Berke.DG.ViewTab.vPoderDatos view_PoderDatos = new Berke.DG.ViewTab.vPoderDatos(db);


			
			view_PoderDatos.Dat.ID.Filter           = ObjConvert.GetFilter(txtID_min.Text);
			view_PoderDatos.Dat.Denominacion.Filter = ObjConvert.GetFilter_Str(txtDenominacion.Text);
			view_PoderDatos.Dat.Concepto.Filter     = ObjConvert.GetFilter_Str(txtConcepto.Text);
 		    view_PoderDatos.Dat.ActaNro.Filter      = ObjConvert.GetFilter(txtActaNro.Text);
			view_PoderDatos.Dat.ActaAnio.Filter      = ObjConvert.GetFilter(txtActaAnio.Text);
			view_PoderDatos.Dat.InscripcionNro.Filter  = ObjConvert.GetFilter(txtInscripcionNro.Text);
			view_PoderDatos.Dat.InscripcionAnio.Filter = ObjConvert.GetFilter(txtInscripcionAnio.Text);
            view_PoderDatos.Dat.Domicilio.Filter = ObjConvert.GetFilter_Str(txtDomicilio.Text);

			view_PoderDatos.Dat.pais.Filter         = ObjConvert.GetFilter_Str(txtPais.Text);

			//view_PoderDatos.Dat.nomciudad.Filter = ObjConvert.GetFilter_Str(txtCiudad.Text);
			view_PoderDatos.Adapter.ReadAll();

	
			#endregion Asignar Parametros 

			#region Obtener datos
			int recuperados = -1;
			try 
			{

				#region Convertir a Link
				for( view_PoderDatos.GoTop(); ! view_PoderDatos.EOF ; view_PoderDatos.Skip() )
				{
					view_PoderDatos.Edit();

					view_PoderDatos.Dat.Denominacion.Value = HtmlGW.Redirect_Link(
						view_PoderDatos.Dat.ID.AsString, 
						view_PoderDatos.Dat.Denominacion.AsString,
						"PoderDatos.aspx","PoderID" );	
	 				
					

					/*string referencia = Berke.Marcas.BizActions.Lib.digitalDocPath(0,view_PoderDatos.Dat.ID.AsInt,9);
					view_PoderDatos.Dat.PoderStr.Value = referencia;*/
					string referencia = Berke.Libs.Base.DocPath.digitalDocPath(0,view_PoderDatos.Dat.ID.AsInt,9);
					view_PoderDatos.Dat.PoderStr.Value = referencia;

					view_PoderDatos.PostEdit();
				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			} 
			catch( Exception exep ) 
			{
				throw new Exception("PoderConsulta", exep );
			} 

			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = view_PoderDatos.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "Excesiva cantidad de registros ("+recuperados.ToString() + ")" ;

			}
			else if( view_PoderDatos.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Poderes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+view_PoderDatos.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
				
			}
			#endregion
		}
		#endregion Busqueda de registros 


		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda()
		{
			lblTituloGrid.Text = "Poderes";
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= true;
		}
		#endregion MostrarPanel_Busqueda


		#region MostrarPanel_Resultado
		private void MostrarPanel_Resultado()
		{
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= true;
			this.pnlBuscar.Visible		= true;			
		}
		#endregion MostrarPanel_Resultado

		#region Carga de Combo

		#region Propietario
		private void eccPropietario_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{

			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();

			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" ) 
			{
				inTB.Dat.Entero			.Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa			.Value = combo.Text;   //String
			}
			inTB.PostNewRow(); 
				
			Berke.DG.ViewTab.ListTab outTB =  Berke.Marcas.UIProcess.Model.Propietario.ReadForSelect( inTB );
	
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Propietario		

		#endregion Carga de Combo

	}
}
