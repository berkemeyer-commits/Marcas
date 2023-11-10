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
	public partial class DocumentoConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form
















		#endregion 
	
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		
			#region DocTipo DropDown

			Berke.DG.ViewTab.ListTab ltDocTipo = Berke.Marcas.UIProcess.Model.DocumentoTipo.ReadForSelect();
			ddlDocTipoID.Fill( ltDocTipo.Table, true);
			
			#endregion DocTipo DropDown


		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				AsignarValoresIniciales();
				MostrarPanel_Busqueda();
			}		
		}
		#endregion Page_Load

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
						


			#region Asignar Parametros (vDocum)
			Berke.DG.ViewTab.vDocum vDocum = new Berke.DG.ViewTab.vDocum();

	
			vDocum.NewRow(); 
	
			vDocum.Dat.DocRefExt	.Value = txtDocRefExt.Text;
			vDocum.Dat.ActaAnio	.Value = txtActaAnio.Text;
			vDocum.Dat.DocTipoID	.Value = ddlDocTipoID.Value;
			vDocum.Dat.ExpeID	.Value = txtExpeID.Text;
			vDocum.Dat.EsEscritoVario	.Value = ddlEsEscritoVario.SelectedValue;
			vDocum.Dat.ActaNro	.Value = txtActaNro.Text;
			vDocum.Dat.ID	.Value = txtID_min.Text;
			vDocum.Dat.DocNro	.Value = txtDocNro.Text;
			vDocum.Dat.DocAnio	.Value = txtDocAnio.Text;
			vDocum.Dat.Fecha	.Value = txtFecha_min.Text;
			vDocum.Dat.Descrip	.Value = txtDescrip.Text;

			vDocum.PostNewRow();
	
			vDocum.NewRow(); 
	
			vDocum.Dat.ID	.Value = txtID_max.Text;
			vDocum.Dat.Fecha	.Value = txtFecha_max.Text;

			vDocum.PostNewRow();
	
			#endregion Asignar Parametros ( vDocum )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				vDocum =  Berke.Marcas.UIProcess.Model.Documento.ReadList( vDocum );
				
				#region Convertir a Link
				for( vDocum.GoTop(); ! vDocum.EOF ; vDocum.Skip() )
				{
					string path="";
					if(vDocum.Dat.EsEscritoVario.AsBoolean ){
						path= Berke.Libs.Base.DocPath.digitalDocPath( 
							vDocum.Dat.DocAnio.AsInt,
							vDocum.Dat.DocNro.AsInt, 
							0 );
						/*path= Berke.Marcas.BizActions.Lib.digitalDocPath( 
								vDocum.Dat.DocAnio.AsInt,
								vDocum.Dat.DocNro.AsInt, 
								0 );*/
					}

					vDocum.Edit();
					vDocum.Dat.Descrip.Value = vDocum.Dat.Descrip.AsString + " " + path;
					
		
//					vDocum.Dat.?CampoDescrip?.Value = HtmlGW.Redirect_Link(
//						vDocum.Dat.ID.AsString, 
//						vDocum.Dat.?CampoDescrip?.AsString,
//						"DocumentoDetalle.aspx","DocumentoID" );	
	 						
						vDocum.PostEdit();
				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: DocumentoConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vDocum.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vDocum.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Documentos &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vDocum.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Documentos";
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




		#endregion Carga de Combo

	} // end class DocumentoConsulta
} // end namespace Berke.Marcas.WebUI.Home


