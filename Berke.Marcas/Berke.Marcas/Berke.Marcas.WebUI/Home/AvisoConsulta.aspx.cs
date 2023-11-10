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
	public partial class AvisoConsulta : System.Web.UI.Page
	{

		#region Variables Globales
		Berke.DG.ViewTab.vAviso vAviso = new Berke.DG.ViewTab.vAviso();

		#endregion VAriables Globales

		#region Controles del Web Form










		#endregion 
	
		#region Push/POP

		#region Push_Aviso

		private void Push_Aviso ()
		{
			if( vAviso == null ){ vAviso = new Berke.DG.ViewTab.vAviso();}
			Session["vAviso"] = vAviso.Table;
		}

		#endregion Push_Aviso

		#region Pop_Aviso

		private void Pop_Aviso()
		{
			vAviso = new Berke.DG.ViewTab.vAviso((DataTable) Session["vAviso"] );
		}

		#endregion Pop_Aviso

		#endregion Push/POP

	
		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		
			txtFechaAviso_min.Text = "01/01/1980";
			txtFechaAviso_max.Text = DateTime.Today.ToString("d") +" 23:59";

			#region Destinatario DropDown
			Berke.DG.ViewTab.ListTab seDestinatario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlDestinatario.Fill( seDestinatario.Table, true);
			#endregion Destinatario DropDown
		
			#region Remitente DropDown
			Berke.DG.ViewTab.ListTab seRemitente = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlRemitente.Fill( seRemitente.Table, true);
			#endregion Remitente DropDown

			#region Prioridad DropDown
			Berke.DG.ViewTab.ListTab sePrioridad = Berke.Marcas.UIProcess.Model.Prioridad.ReadForSelect();
			ddlPrioridad.Fill( sePrioridad.Table, true );
			#endregion Prioridad DropDown
		

			Berke.DG.ViewTab.vFuncionario fun = Berke.Marcas.UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
			ddlDestinatario.Value = fun.Dat.ID.AsString;
			ddlPendiente.SelectedIndex = 1;

	
		}
		#endregion Asignar Valores Iniciales


		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				AsignarValoresIniciales();

				if( Buscar() > 0 )
				{
					this.btnMostrarFiltro.Visible = true;
					this.pnlBuscar.Visible		= false;
					MostrarPanel_Resultado();

				}
				else{
					this.btnMostrarFiltro.Visible = false;
					MostrarPanel_Busqueda();
					MostrarPanel_Resultado();

				}
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
						
			Buscar();

		}

		private int  Buscar(){
		
			#region Asignar Parametros (vAviso)
			Berke.DG.ViewTab.vAviso vAviso = new Berke.DG.ViewTab.vAviso();

	
			vAviso.NewRow(); 
	
			vAviso.Dat.Indicaciones	.Value = txtIndicaciones.Text;
			vAviso.Dat.Pendiente	.Value = ddlPendiente.SelectedValue;
			vAviso.Dat.Destinatario	.Value = ddlDestinatario.Value;
			vAviso.Dat.Remitente	.Value = ddlRemitente.Value;
			vAviso.Dat.FechaAlta	.Value = txtFechaAlta_min.Text;
			vAviso.Dat.FechaAviso	.Value = txtFechaAviso_min.Text;
			vAviso.Dat.ID	.Value = txtID_min.Text;
			vAviso.Dat.Asunto	.Value = txtAsunto.Text;
			vAviso.Dat.Contenido	.Value = txtContenido.Text;
			vAviso.Dat.PrioridadID	.Value = this.ddlPrioridad.SelectedValue;

			vAviso.PostNewRow();
	
			vAviso.NewRow(); 
	
			vAviso.Dat.FechaAlta	.Value = txtFechaAlta_max.Text;
			vAviso.Dat.FechaAviso	.Value = txtFechaAviso_max.Text;
			vAviso.Dat.ID	.Value = txtID_max.Text;

			vAviso.PostNewRow();
	
			#endregion Asignar Parametros ( vAviso )



			#region Obtener datos
			int cant = 0;
			int recuperados = -1;
			try 
			{
				vAviso =  Berke.Marcas.UIProcess.Model.Aviso.ReadList( vAviso );
				cant = vAviso.RowCount;

				this.Push_Aviso();

				#region Convertir a Link
				Berke.Html.HtmlTextFormater rojo = new Berke.Html.HtmlTextFormater();
				rojo.SetColor( System.Drawing.Color.SeaGreen );
//				rojo.Bold = true;

				for( vAviso.GoTop(); ! vAviso.EOF ; vAviso.Skip() )
				{
					vAviso.Edit();

					vAviso.Dat.Asunto.Value = HtmlGW.Redirect_Link(
						vAviso.Dat.ID.AsString, 
						vAviso.Dat.Asunto.AsString,
						"AvisoDetalle.aspx","AvisoID" );	
	 				string contenido =  vAviso.Dat.Contenido.AsString;
					if( contenido.Length > 4000 )
					{
						vAviso.Dat.Contenido.Value = vAviso.Dat.Contenido.AsString.Substring(0,4000) +
							HtmlGW.Spaces(5)+"** Contenido Truncado ** ";
					}else if( contenido.Length > 350 ) {
						vAviso.Dat.Contenido.Value = vAviso.Dat.Contenido.AsString.Substring(0,350) +
							HtmlGW.Spaces(5)+". . . ";
		
					}


					string indic = vAviso.Dat.Indicaciones.AsString;
					int pos = indic.LastIndexOf("->");
					if( pos == -1 ) pos = indic.LastIndexOf("- >");
					
					string buf = indic.Substring( 0, pos );
					int pos1 = buf.LastIndexOf("m.") + 2;
					string nom = buf.Substring( pos1, buf.Length - pos1 );				

					pos+= 2;
					buf = indic.Substring( pos, indic.Length - pos );
					pos1 = buf.IndexOf("*") + 1;

					indic = nom.Trim() + ":" + buf.Substring(pos1, buf.Length - pos1 );

					if( vAviso.Dat.Leido.AsBoolean )
					{
						vAviso.Dat.Indicaciones.Value = indic;
					}
					else{
						vAviso.Dat.Indicaciones.Value = rojo.Html( indic );					
					}

				

					vAviso.PostEdit();

				}
				#endregion  Convertir a Link
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
				cant = recuperados;
			}//Aqui
			catch( Exception exep ) 
			{
				throw new Exception("AvisoConsulta", exep );
			} // a Aqui

			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			dgResult.DataSource = vAviso.Table;
			dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( vAviso.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Avisos &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+vAviso.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
			return	cant;
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Avisos";
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= false;
		}
		#endregion MostrarPanel_Busqueda


		#region MostrarPanel_Resultado
		private void MostrarPanel_Resultado()
		{
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= true;			
		}
		#endregion MostrarPanel_Resultado

	

		private void ddlRemitente_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		#region Carga de Combo




		#endregion Carga de Combo


		#region MarcarAtendidos en masa
		private void MarcarAtendidos( )
		{	
			this.Pop_Aviso();
			this.vAviso.AcceptAllChanges();

			#region Asigna los cambios a vAviso

			this.vAviso.GoTop();
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			Berke.DG.DBTab.Aviso avisoTab = new Berke.DG.DBTab.Aviso( db );

			db.IniciarTransaccion();
			avisoTab.Adapter.ConcurrenceOn = false;
			foreach( DataGridItem item in this.dgResult.Items )
			{
				CheckBox chkSel = (CheckBox) item.FindControl("chkSel");
				if( chkSel.Checked )
				{
					Label lblID = (Label) item.FindControl("lblID");
			
					int avisoID = int.Parse( lblID.Text );

					avisoTab.Adapter.ReadByID( avisoID );

					avisoTab.Edit();
					avisoTab.Dat.Pendiente.Value = false;
					avisoTab.Dat.Indicaciones.Value += " <br>"+DateTime.Now.ToString()+" CONCLUIDO por "+ Berke.Libs.Base.Acceso.GetCurrentUser();
					avisoTab.PostEdit();

					avisoTab.Adapter.UpdateRow();
				}
			}
			db.Commit();
			db.CerrarConexion();

			#endregion Asigna los cambios a vAviso

			Buscar();

		}
	
		protected void btnAtender_Click(object sender, System.EventArgs e)
		{
			MarcarAtendidos( );
		}

		#endregion MarcarAtendidos en masa

		#region btnMarcar_Click  ( en masa )
		protected void btnMarcar_Click(object sender, System.EventArgs e)
		{
			foreach( DataGridItem item in this.dgResult.Items )
			{
				CheckBox chkSel = (CheckBox) item.FindControl("chkSel");
				chkSel.Checked = true;
			}
		}
		#endregion btnMarcar_Click  ( en masa )

		protected void btnMostrarFiltro_Click(object sender, System.EventArgs e)
		{
			this.btnMostrarFiltro.Visible = false;
			this.pnlBuscar.Visible		= true;

		}


	} // end class AvisoConsulta
} // end namespace Berke.Marcas.WebUI.Home


