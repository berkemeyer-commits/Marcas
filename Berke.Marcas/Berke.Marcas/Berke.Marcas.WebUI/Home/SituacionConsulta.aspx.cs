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
using Berke.Marcas.WebUI.Tools.Helpers; // UrlParam

namespace Berke.Marcas.WebUI.Home
{
	public partial class SituacionConsulta : System.Web.UI.Page
	{
		#region Oper_Param 
		private string Oper_Param // Operacion indicada en UrlParam  .Mantenimiento, Consulta,....
		{
			set{ ViewState["Oper_Param"] = value; }
			get{ return (string) ViewState["Oper_Param" ];}
		}
		#endregion Oper_Param

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


		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				Oper_Param	= Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam("Oper");  // Operacion indicada en UrlParam  .Mantenimiento, Consulta,....

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
						
			#region Asignar Parametros (Situacion)
			Berke.DG.DBTab.Situacion Situacion = new Berke.DG.DBTab.Situacion();
			Berke.DG.DBTab.Situacion param = new Berke.DG.DBTab.Situacion();

			param.DisableConstraints();

			param.NewRow(); 
	
			param.Dat.ID	.Value = txtID_min.Text;
			param.Dat.Descrip	.Value = txtDescrip.Text;
			param.Dat.Abrev	.Value = txtAbrev.Text;

			param.PostNewRow();
	
			param.NewRow(); 
	
			param.Dat.ID	.Value = txtID_max.Text;

			param.PostNewRow();
	
			#endregion Asignar Parametros ( Situacion )



			#region Obtener datos
			int recuperados = -1;
			try 
			{
				Situacion =  Berke.Marcas.UIProcess.Model.Situacion.Read( param );
				/*
				#region Convertir a Link
				if( Oper_Param == "Mantenimiento" )
				{
					for( Situacion.GoTop(); ! Situacion.EOF ; Situacion.Skip() )
					{
						Situacion.Edit();

						Situacion.Dat.Descrip.Value = HtmlGW.Redirect_Link(
							Situacion.Dat.ID.AsString, 
							Situacion.Dat.Descrip.AsString,
							"SituacionDetalle.aspx","SituacionID" );		 						
						Situacion.PostEdit();
					}
				}
				#endregion  Convertir a Link
				*/
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: SituacionConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
				dgResult.DataSource = Situacion.Table;
				dgResult.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( Situacion.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Situaciones &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+Situacion.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Situaciones";
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

		protected void BtnAgregar_Click(object sender, System.EventArgs e)
		{
			UrlParam param = new UrlParam();
			param.AddParam("Mode", "Insert");
			param.redirect("SituacionDetalle.aspx");
		}

		#region Carga de Combo




		#endregion Carga de Combo

	} // end class SituacionConsulta
} // end namespace Berke.Marcas.WebUI.Home


