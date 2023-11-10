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
	using Berke.Libs.Base.DSHelpers;
	/// <summary>
	/// Summary description for EliminarInstruccion.
	/// </summary>
	public partial class EliminarInstruccion : System.Web.UI.Page
	{
		#region Variables Globales


		#endregion Variables Globales

		#region Controles del Form

		protected System.Web.UI.WebControls.TextBox txtNro1;
		protected Berke.Libs.WebBase.Controls.DropDown DrpInstruccion;
		protected System.Web.UI.WebControls.LinkButton btnEliminarInstrucción;

		#endregion Controles del Form

		#region Properties

		#region RountripCounter
		private int RountripCounter
		{
			get{ return Convert.ToInt32( ViewState["RountripCounter"]) ; }
			set{ ViewState["RountripCounter"] = Convert.ToString( value );}
		}
		#endregion RountripCounter

		#region ExpedienteID
		private int ExpedienteID
		{
			get{ return Convert.ToInt32(( ViewState["ExpedienteID"] == null )? -1 : ViewState["ExpedienteID"] ) ; }
			set{ ViewState["ExpedienteID"] = Convert.ToString( value );}
		}
		#endregion ExpedienteID

		#region CorrespondenciaID
		private int CorrespondenciaID
		{
			get{ return Convert.ToInt32( ( ViewState["CorrespondenciaID"] == null )? -1 : ViewState["CorrespondenciaID"] ) ; }
			set{ ViewState["CorrespondenciaID"] = Convert.ToString( value );}
		}
		#endregion ExpedienteID

		#region UltimaInstruccionID
		private int UltimaInstruccionID
		{
			get{ return Convert.ToInt32( ( ViewState["UltimaInstruccionID"] == null )? -1 : ViewState["UltimaInstruccionID"] ) ; }
			set{ ViewState["UltimaInstruccionID"] = Convert.ToString( value );}
		}
		#endregion UltimaInstruccionID

		#endregion Properties

		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region DropDown de IntruccionTipo
			SimpleEntryDS seInstrucTipo = Berke.Marcas.UIProcess.Model.Instruccion.ReadForSelect();

			#endregion DropDown de IntruccionTipo

			MostrarPanelBusqueda();
		
			this.pnlExpeDatos.Visible	= false;
			lblMensaje.Text = "";

		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			AsignarDelegados();
			if( !IsPostBack )
			{
				RountripCounter = 1;
				AsignarValoresIniciales();
			}
			else 
			{
				RountripCounter++;
			}
//			this.btnVolver.Attributes.Add("onclick", "history.go(-"+ RountripCounter.ToString() +"); return false;" );

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

		#region ParametrosDeBusquedaNulos
		private bool ParametrosDeBusquedaNulos(){
			return (
				txtRegistroNro.Text.Trim()	== "" && 
				txtActaNro.Text.Trim()		== "" && 
				txtActaAnio.Text.Trim()		== ""  
				//this.txtMarcaID.Text.Trim() == ""
				);
			
		}
		#endregion ParametrosDeBusquedaNulos

		#region ParametrosOk
		private bool ParametrosOk()
		{
			bool ret = true;
			string mensaje = "";
			
			if( ! ret ){
				ShowMessage( mensaje );
			}
			return ret;
		}
		#endregion ParametrosOk

		#region Buscar Expediente


		#region Mostrar solo panel de Busqueda

		private void MostrarPanelBusqueda()
		{
			this.pnlBuscar.Visible		= true;
			this.pnlExpeDatos.Visible	= false;
			//this.pnlInstruccion.Visible	= false;
			//pnlCorrespResult.Visible = false;
			lblMensaje.Text = "";
			//btnAceptar.Visible = false;
		}
		#endregion Mostrar solo panel de Busqueda

		protected void txtBuscar_Click(object sender, System.EventArgs e)
		{
			 BuscarExpe();
		}

		private void BuscarExpe()
		{
			
//			MostrarPanelBusqueda();
			if( ParametrosDeBusquedaNulos() )
			{
				ShowMessage( " Ingrese parámetros de búsqueda");
				return;
			}

			//Berke.DG.ViewTab.vExpeMarca param = new Berke.DG.ViewTab.vExpeMarca();
			/*Berke.DG.ViewTab.vExpeMarca param = new Berke.DG.ViewTab.vExpeMarca();
			param.NewRow();

			
			//param.Dat.ExpedienteID	.Value	= this.txtExpeID.Text;
			param.Dat.RegistroNro	.Value	= this.txtRegistroNro.Text;
			param.Dat.ActaNro		.Value	= this.txtActaNro.Text;
			param.Dat.ActaAnio		.Value	= this.txtActaAnio.Text;
			
			//param.Dat.MarcaID		.Value	= this.txtMarcaID.Text;
			
			param.PostNewRow();*/

		
			Berke.Libs.Base.Helpers.AccesoDB db	   = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vExpeMarca resul = new Berke.DG.ViewTab.vExpeMarca(db);

			//string filtroTramite = Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + "," + Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString();

			resul.ClearFilter();
			resul.Dat.RegistroNro.Filter = this.txtRegistroNro.Text;
			resul.Dat.ActaNro.Filter = this.txtActaNro.Text;
			resul.Dat.ActaAnio.Filter = this.txtActaAnio.Text;
			//resul.Dat.TramiteID.Filter = ObjConvert.GetFilter(filtroTramite);
			resul.Adapter.ReadAll();

			//resul =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList (param );
			lblMensaje.Text = "";
			if( resul.RowCount > 0 )
			{
				
				this.pnlExpeDatos.Visible	= true;
				//this.pnlInstruccion.Visible	= true;


				string NuestroSN = "";
				if (resul.Dat.ExpeNuestro.AsBoolean  )
				{
					NuestroSN = "Nuestro";
				}
				else
				{
					NuestroSN = "Tercero";
				}
				string expeID = resul.Dat.ExpedienteID.AsString ;

				this.ExpedienteID = resul.Dat.ExpedienteID.AsInt; // Persiste via ViewState

				expeID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link( 
					"MarcaDetalleL.aspx", 		// Pagina
					expeID,			// Texto
					expeID,			// Valor del parametro
					"expeID");		// Nombre del parametro


				Berke.DG.ViewTab.vCliente cliente = Berke.Marcas.UIProcess.Model.Cliente.ReadByID( resul.Dat.ClienteID.AsInt );

				this.lblExpeDescrip.Text = "" + "<table><tr><td><u>Expediente ID</u>:<b>" +expeID + "</b>" +  " <b>" + NuestroSN + "</b>" + " <u>Acta</u>:<b>"+ resul.Dat.Acta.AsString + 
					"</b><br> <u>Trámite</u>: <b>" + resul.Dat.TramiteAbrev.AsString + "</b> <u>Situación</u>: <b>" + resul.Dat.SituacionDecrip.AsString + "</b>"+ 
					"</td></tr><tr><td> <u>O. Trabajo</u>:<b>" +resul.Dat.OrdenTrabajo.AsString + "</b> <u>F. Venc.</u>: <b>" + resul.Dat.VencimientoFecha.AsString + "</td></tr>" +
					 "<tr><td> <u>Cliente</u>:<b>" +cliente.Dat.Descrip.AsString + "</b> " + "</td></tr>" +"</table>";
				 
           


				this.lblExpeDescrip.Text = "" + "<table><tr><td><u>Expediente ID</u>:<b>" +expeID + "</b>" +  " <b>" + NuestroSN + "</b>" + " <u>Acta</u>:<b>"+ resul.Dat.Acta.AsString + 
					"</b><br> <u>Trámite</u>: <b>" + resul.Dat.TramiteAbrev.AsString + "</b> <u>Situación</u>: <b>" + resul.Dat.SituacionDecrip.AsString + "</b>"+ 
					"</td></tr><tr><td> <u>O. Trabajo</u>:<b>" +resul.Dat.OrdenTrabajo.AsString + "</b> <u>F. Venc.</u>: <b>" + resul.Dat.VencimientoFecha.AsString + "</td></tr>" +
					"<tr><td> <u>Cliente</u>:<b>" +cliente.Dat.Descrip.AsString + "</b> " + "</td></tr>" +"</table>";




				string marcaID = resul.Dat.MarcaID.AsString;
				marcaID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link( 
					"MarcaDetalleL.aspx", 		// Pagina
					marcaID,			// Texto
					marcaID,			// Valor del parametro
					"MarcaID");			// Nombre del parametro

				this.lblMarcaDescrip.Text = "<u>Marca</u>: ID: "+ marcaID + "<b> "+ resul.Dat.Denominacion.AsString + "</b>"+
					 " <b>"+ resul.Dat.Clase.AsString + "</b><br>";
			
				#region Instrucciones
				/*Berke.Libs.Base.Helpers.AccesoDB db	   = new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;*/

				Berke.DG.ViewTab.vCorrespNro correspnroarea = new Berke.DG.ViewTab.vCorrespNro(db);
			

				string buf1 = "";
				Berke.DG.ViewTab.vInstruccion instruc = new Berke.DG.ViewTab.vInstruccion( db );
				instruc.Dat.ExpedienteID	.Filter	=	this.ExpedienteID;
				instruc.Dat.ID.Order	= 1;

				instruc.Adapter.ReadAll();
				UltimaInstruccionID = -1;
				if( instruc.RowCount > 0 )
				{

					btnEliminarInstruccion.Visible = true;
					this.chkDeleteInstruc.Visible = true;
					buf1 = "<b>Instrucciones</b><br>";

					buf1+= @"<Table bgColor=""white"" cellSpacing=""0"" cellPadding=""0""  border=""1"">";
					buf1+= @"<tr  >";
					buf1+= @"<tD bgColor=""silver""><P class=""Texto""><b> Fecha		</b></p></td>";
					buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Instrucción	</b></p></td>";
					buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Comentario	</b></p></td>";
					buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Correspondencia	</b></p></td>";
					buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Usuario	</b></p></td>";

					buf1+= @"</tr>";
					for( instruc.GoTop(); ! instruc.EOF; instruc.Skip() )
					{
						this.UltimaInstruccionID = instruc.Dat.ID.AsInt;

						string path = "";
						if( instruc.Dat.CorrespNro.AsString != "" )
						{
							correspnroarea.Adapter.ClearParams();
							correspnroarea.Adapter.AddParam("nro",instruc.Dat.CorrespNro.AsInt);
							correspnroarea.Dat.vigente.Filter = true;
							correspnroarea.Adapter.ReadAll();

							if ( correspnroarea.RowCount == 1) 
							{
								if ((correspnroarea.Dat.IDArea.AsInt != 0 ))
								{
									path = Berke.Libs.Base.DocPath.digitalDocPath(
										   instruc.Dat.CorrespAnio.AsInt, instruc.Dat.CorrespNro.AsInt, 
										   correspnroarea.Dat.IDArea.AsInt );

								}
							}

				

						}
						buf1+= @"<tr>";
						buf1+= @"<tD style=""WIDTH: 70px"" ><P class=""Texto"" align=""center"">" + instruc.Dat.Fecha.AsString		+ "</p> </td>";
						buf1+= @"<tD style=""WIDTH: 260px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Descrip.AsString				+ "</p> </td>";
						buf1+= @"<tD style=""WIDTH: 260px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Obs.AsString				+ "</p> </td>";
						buf1+= @"<tD style=""WIDTH: 100px"" ><P class=""Texto"" align=""center"" ><nobr>" +"&nbsp;"+  instruc.Dat.CorrespNro.AsString+" / "+ instruc.Dat.CorrespAnio.AsString	+path+ "</nobr></p> </td>";
						buf1+= @"<tD style=""WIDTH: 100px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Nick.AsString + "</p> </td>";

						buf1+= "</tr>";
					}

					buf1+= "</Table>";
				}
				else{
					btnEliminarInstruccion.Visible = false;
					this.chkDeleteInstruc.Visible = false;
				}
				lblInstrucciones.Text	= buf1;
				db.CerrarConexion();
				#endregion Instrucciones

				//btnAceptar.Visible = true;
			}
			else
			{
				//btnAceptar.Visible = false;
				ShowMessage( " No se encontraron registros " );
			}

		}

		#endregion Buscar Expediente

		#region VaciarValoresNoPersistentes
		private void VaciarValoresNoPersistentes(){
			#region Correspondencia
//			txtNro.Text = "";
//			this.CorrespondenciaID = -1;
//			pnlCorrespResult.Visible = false;
			#endregion Correspondencia
		}
		#endregion VaciarValoresNoPersistentes

		#region btnAceptar_Click
		private void btnAceptar_Click(object sender, System.EventArgs e)
		{			
		}

		#endregion btnAceptar_Click

		#region ShowMessage
		private void ShowMessage (string mensaje ){
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion


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
				return string.Format( anchorTemplate, arch , "Doc." );
			}
			//			return string.Format( anchorTemplate, arch );
		}
		#endregion digitalDocPath

		#region btnEliminarInstruccion_Click_1
		protected void btnEliminarInstruccion_Click_1(object sender, System.EventArgs e)
		{
			if ( chkDeleteInstruc.Checked && this.UltimaInstruccionID != -1 )
			{
				
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

				Berke.DG.DBTab.Expediente_Instruccion expeIns = new Berke.DG.DBTab.Expediente_Instruccion( db );
				expeIns.Adapter.ReadByID( UltimaInstruccionID );
				db.IniciarTransaccion();
				expeIns.Adapter.DeleteRow();
				db.Commit();

				db.CerrarConexion();
				BuscarExpe();
			}
			else
			{
				this.ShowMessage("Esta operación es irreversible. Confírmela con el checkbox de al lado");
			}

		}

		#endregion btnEliminarInstruccion_Click_1

	}
}
