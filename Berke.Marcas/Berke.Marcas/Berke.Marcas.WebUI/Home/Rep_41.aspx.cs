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
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	
	/// <summary>
	/// Summary description for Rep_41.
	/// </summary>
	public partial class Rep_41 : System.Web.UI.Page
	{
//		#region Variables Globales
//		string[] aOmitir = new string[]{}; // array de instrucciones a omitir
//		#endregion Variables Globales
	

		#region Properties

		#endregion Properties

		#region Controles del Web Form
		protected System.Web.UI.WebControls.Label lblTitulo;
		#endregion Controles del Web Form

		#region Asignar Delegados
		private void AsignarDelegados()
		{
		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Tramite DropDown
			
//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
//			ddlTramiteID.Fill( se.Tables[0], true);	

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramiteID.Fill( lst.Table, true);




			#endregion Tramite DropDown
		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			
			if( !IsPostBack )
			{
				//				pedidoID_param = Berke.Marcas.WebUI.Tools.Helpers.UrlParam.GetParam ("PedidoID");
				AsignarValoresIniciales();
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
			//			if( cbxPropietarioID.SelectedValue.Trim() == "")
			//			{
			//				mensajeError = "Debe Ingresar un Propietario";
			//				return false;
			//			}
			return true;
		}
		#endregion ParametrosOk

		#region AplicarFiltro
		private void AplicarFiltro( Berke.DG.ViewTab.vMarcaCambioSit view )
		{
			 
			//			view.Dat.Denominacion	.Filter = ObjConvert.GetFilter_Str	( txtDenominacion.Text);
			//			view.Dat.AltaFecha		.Filter = ObjConvert.GetFilter		( txtAltaFecha_min.Text);
			//			view.Dat.FuncionarioID	.Filter = ObjConvert.GetFilter		( ddlFuncionario.SelectedValue);
			//			view.Dat.Nuestra		.Filter	= ObjConvert.GetFilter_Bool	( ddlNuestra.SelectedValue);
			//		
			view.Dat.AltaFecha.Filter = ObjConvert.GetFilter( this.txtAltaFecha_min.Text);
			view.Dat.TramiteID.Filter = ObjConvert.GetFilter( this.ddlTramiteID.SelectedValue );
			view.Dat.SitAbrev.Filter = "OP"; // 3 = Con Orden de Publicación ( OP )
			view.Dat.Nuestra.Filter = true;
		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vMarcaCambioSit view)
		{
			view.ClearOrder();
			view.Dat.ExpeSitID.Order	= 1; // Para eliminar los duplicados
		
		}
		#endregion SetOrder

		#region Reordenar
		private void Reordenar(  Berke.DG.ViewTab.vMarcaCambioSit view)
		{
			view.ClearOrder();
			view.Dat.AltaFecha.Order	= 1;
			view.Dat.Denominacion.Order	= 2;
			view.Sort();
		}
		#endregion Reordenar

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vMarcaCambioSit view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			
			mensajeError = "";
			int recuperados = -1;
			try 
			{
				view.Adapter.Count();
				if( recuperados != 0 )
				{
					if( recuperados < limite )
					{
						view.Adapter.ReadAll( limite );
						#region Eliminar duplicados por tener mas de una instruccion
						int antID = -992277;
						for( view.GoTop(); !view.EOF ; view.Skip() )
						{
							if( view.Dat.ExpeSitID.AsInt == antID ){
								view.Delete();
							}else{
								antID = view.Dat.ExpeSitID.AsInt;
							}		
						}
						view.AcceptAllChanges();
						Reordenar( view );
						view.GoTop();
						#endregion Eliminar duplicados ...

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

			Berke.DG.ViewTab.vMarcaCambioSit view = new Berke.DG.ViewTab.vMarcaCambioSit( db );
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
				ShowMessage( mensajeError );
			}
			//----------
		}
		#endregion GenerarReporte

		#region GenerarDocumento
		private void GenerarDocumento( Berke.DG.ViewTab.vMarcaCambioSit view )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= view.Adapter.Db;
			Berke.DG.ExpedienteDG expeDG = new Berke.DG.ExpedienteDG();
			Berke.DG.MarcaDG marDG = new Berke.DG.MarcaDG();

			int idiomaID =  (int) GlobalConst.Idioma.ESPANOL;

			#region Leer Plantilla

			string template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_41", idiomaID );
		
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
			CodeGenerator tabla		= cg.ExtraerTabla("tabla" );

			#endregion Obtener "Generators"
				
			cg.clearText();
			cg.copyTemplateToBuffer();
			tabla.clearText();
			for(  view.GoTop(); ! view.EOF; view.Skip() )
			{	
				expeDG = Berke.Marcas.UIProcess.Model.Expediente.ReadByID_asDG( view.Dat.ExpeID.AsInt );
				marDG = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( expeDG.Expediente.Dat.MarcaID.AsInt );

				#region Poder
				string poderNumero = "";
				string PoderDenominacion = "";
				string poderDomicilio = "";
				if( expeDG.Poder.RowCount > 0 )
				{
					poderNumero = expeDG.Poder.Dat.Inscripcion.AsString;
					PoderDenominacion = expeDG.Poder.Dat.Denominacion.AsString;
					poderDomicilio = expeDG.Poder.Dat.Domicilio.AsString;
				}
//				else
//				{
//					poderNumero = expeDG.Poder.Dat.Inscripcion.AsString;
//					PoderDenominacion = expeDG.Poder.Dat.Denominacion.AsString;
//					poderDomicilio = expeDG.Poder.Dat.Domicilio.AsString;
//				}
				#endregion Poder 
				tabla.copyTemplateToBuffer();
				tabla.replace("*Denominacion*",view.Dat.Denominacion.AsString );
				tabla.replace("*Fecha*",view.Dat.SituacionFecha.AsString );
				tabla.replace("*Hora*", string.Format("{0:t}", view.Dat.SituacionFecha.AsDateTime ));
				tabla.replace("*Acta*", view.Dat.Acta.AsString );
				tabla.replace("*Clase*", view.Dat.ClaseNro.AsString );
				tabla.replace("*PoderDenominacion*", PoderDenominacion );
				tabla.replace("*PoderNumero*", poderNumero );
				tabla.replace("*PoderDomicilio*", poderDomicilio );
				tabla.replace("*TramiteDescrip*", view.Dat.TramiteDescrip.AsString );

//				tabla.replaceField("Ccliente.nombre", view.Dat.Nombre.AsString );
//				tabla.replaceField("Ccliente.direccion",view.Dat.Direccion.AsString );
//				tabla.replaceField("ClienteAtencion.Atencion", atencion);
				tabla.addBufferToText();
				tabla.addNewPageToText();
			} // end for EOF

			cg.replaceLabel( "tabla", tabla.Texto );
			cg.addBufferToText();
				
			#region Activar WORD
			
			string carpeta = @"K:\Cache\Reportes\";
			 //carpeta = @"\\Trinity\Siberk\Dev\BERKE.MARCA\Code\Berke.Marcas\Berke.Marcas.WebUI\Reports\";
			carpeta = Berke.Libs.Base.GlobalConst.CARPETA_REPORTE;
			
			//string carpeta = @"c:\Temp\";
			string archivo = @"Rep_41";
			string ext		= ".doc";
			int version = 0;
			string path = carpeta+ archivo + "_v"+version.ToString()+ext;
			//			string path = carpeta+ archivo + ext;

			//			while( System.IO.File.Exists( path ))
			//			{
			//				version++;
			//				path = carpeta+ archivo + "_v"+version.ToString()+ext;
			//			}
			Berke.Libs.Base.Helpers.Files.SaveStringToFile(cg.Texto, path);
			lnkDocum.NavigateUrl = path;
			lnkDocum.Text = "Ver Documento";
			#endregion Activar WORD
			//			this.Cache.Remove(path);

		}
		#endregion GenerarDocumento

	}
}
