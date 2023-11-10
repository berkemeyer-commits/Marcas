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
	/// Summary description for ExpeDocIngresar.
	/// </summary>
	public partial class ExpeDocIngresar : System.Web.UI.Page
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
			ddlInstruccion.Fill( seInstrucTipo.Tables[0], true );
			#endregion DropDown de IntruccionTipo

			this.txtAnio.Text = DateTime.Today.Year.ToString();

			MostrarPanelBusqueda();

		
			this.pnlExpeDatos.Visible	= false;
			this.pnlInstruccion.Visible	= false;
			pnlCorrespResult.Visible = false;
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
				txtExpeID.Text.Trim()		== "" && 
				txtRegistroNro.Text.Trim()	== "" && 
				txtActaNro.Text.Trim()		== "" && 
				txtActaAnio.Text.Trim()		== ""  &&
				this.txtMarcaID.Text.Trim() == ""
				);
			
		}
		#endregion ParametrosDeBusquedaNulos

		#region ParametrosOk
		private bool ParametrosOk()
		{
			bool ret = true;
			string mensaje = "";
			if( ddlInstruccion.SelectedValue == "" ){
				mensaje+= (mensaje=="" ?"":". Además, ")+ "Debe seleccionar la intrucción que corresponda"; 
				ret = false;
			
			}
			
//			if( ddlCorrespMov.SelectedValue == "" ){
//				mensaje = " Debe seleccionar uno de los detalles de correspondencia";
//				ret = false;
//			}
			
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
			this.pnlInstruccion.Visible	= false;
			pnlCorrespResult.Visible = false;
			lblMensaje.Text = "";
			btnAceptar.Visible = false;
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
			Berke.DG.ViewTab.vExpeMarca param = new Berke.DG.ViewTab.vExpeMarca();
			param.NewRow();

			
			param.Dat.ExpedienteID	.Value	= this.txtExpeID.Text;
			param.Dat.RegistroNro	.Value	= this.txtRegistroNro.Text;
			param.Dat.ActaNro		.Value	= this.txtActaNro.Text;
			param.Dat.ActaAnio		.Value	= this.txtActaAnio.Text;
			param.Dat.MarcaID		.Value	= this.txtMarcaID.Text;
			
			param.PostNewRow();

		

			Berke.DG.ViewTab.vExpeMarca resul;

			resul =  Berke.Marcas.UIProcess.Model.ExpeMarca.ReadList (param );
			lblMensaje.Text = "";
			if( resul.RowCount > 0 )
			{
				
				this.pnlExpeDatos.Visible	= true;
				this.pnlInstruccion.Visible	= true;


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
				 

                /*rgimenez >>>>>>>*/
				/*
				Berke.Libs.Base.Helpers.AccesoDB db	   = new Berke.Libs.Base.Helpers.AccesoDB();
				Berke.DG.ViewTab.vClienteDatos cliente = new Berke.DG.ViewTab.vClienteDatos(); 
			
				cliente.InitAdapter( db );
				cliente.Dat.ID.Filter= resul.Dat.ClienteID.AsString;
				cliente.Adapter.ReadAll();
				*/
				/*rgimenez <<<<<<<*/


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
				Berke.Libs.Base.Helpers.AccesoDB db	   = new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

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
							path = Berke.Libs.Base.DocPath.digitalDocPath(
								instruc.Dat.CorrespAnio.AsInt, instruc.Dat.CorrespNro.AsInt, 
								1 ); // Corresp del area de Marcas
							/*path = digitalDocPath(
								instruc.Dat.CorrespAnio.AsInt, instruc.Dat.CorrespNro.AsInt, 
								1 ); // Corresp del area de Marcas*/
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

				btnAceptar.Visible = true;
			}
			else
			{
				btnAceptar.Visible = false;
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
		protected void btnAceptar_Click(object sender, System.EventArgs e)
		{
			if( ParametrosOk() ) 
			{
				if( this.ExpedienteID != -1 )
				{
					VaciarValoresNoPersistentes();
							
					#region Inicializar db
					Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
					db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
					db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

					db.IniciarTransaccion();
					#endregion Inicializar db

					#region Leer Correspondencia
					Berke.DG.DBTab.Correspondencia corresp = new Berke.DG.DBTab.Correspondencia(db );
					if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
					{
						corresp.Adapter.ReadByID( CorrespondenciaID );
					}
					#endregion Leer Correspondencia
	
					#region Leer Expediente
					Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
					expe.Adapter.ReadByID( ExpedienteID );
					#endregion Leer Expediente

					#region Verificar que exista Docum
					Berke.DG.DBTab.Documento docum = new Berke.DG.DBTab.Documento(db );
					int documID = -1;
					if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
					{
						docum.Dat.CorrespondenciaID.Filter = CorrespondenciaID;
						docum.Adapter.ReadAll();
						if ( docum.RowCount > 0 )
						{
							documID = docum.Dat.ID.AsInt;
						}
						else
						{
							#region Insertar documento 
							docum.NewRow();
							docum.Dat.CorrespondenciaID.Value	= CorrespondenciaID;
							docum.Dat.IdentificadorNro.Value	= corresp.Dat.Nro.AsInt;
							docum.Dat.IdentificadorAnio.Value	= corresp.Dat.Anio.AsInt;
							docum.Dat.Fecha.Value				= corresp.Dat.FechaAlta.AsDateTime;
							docum.Dat.Descrip.Value				= corresp.Dat.RefCorresp.AsString;
							docum.Dat.DocumentoTipoID.Value		= 4; // Correspondencia Orden
							docum.PostNewRow();
							documID = docum.Adapter.InsertRow();
							#endregion Insertar documento 
						}
					}
					#endregion Verificar que exista Docum

					// Tabla : Expediente_Instruccion
					Berke.DG.DBTab.Expediente_Instruccion expeInstruc = new Berke.DG.DBTab.Expediente_Instruccion( db );

					#region Asignar valores a Expediente_Instruccion
					expeInstruc.NewRow(); 
//					expeInstruc.Dat.ID						.Value = DBNull.Value;					// int PK  Oblig.
					expeInstruc.Dat.MarcaID					.Value = expe.Dat.MarcaID.AsInt;		// int Oblig.
					expeInstruc.Dat.ExpedienteID			.Value = ExpedienteID;					// int Oblig.
					expeInstruc.Dat.FuncionarioID			.Value = WebUI.Helpers.MySession.FuncionarioID;
					if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
					{
						expeInstruc.Dat.CorrespondenciaID		.Value = this.CorrespondenciaID;
					}

					expeInstruc.Dat.InstruccionTipoID	.Value = ddlInstruccion.SelectedValue;	// int
					expeInstruc.Dat.Fecha				.Value = DateTime.Today;				// smalldatetime Oblig.
					expeInstruc.Dat.Obs					.Value = this.txtObservacion.Text;
					expeInstruc.PostNewRow(); 
					#endregion Asignar valores a Expediente_Instruccion

					int expeInstrucID = expeInstruc.Adapter.InsertRow(); 

					#region Insertar en Expediente_Documento
					if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
					{
						Berke.DG.DBTab.Expediente_Documento expeDoc = new Berke.DG.DBTab.Expediente_Documento( db );
						expeDoc.NewRow();
						expeDoc.Dat.DocumentoID.Value = documID;
						expeDoc.Dat.ExpedienteID.Value = ExpedienteID;
						expeDoc.PostNewRow();

						expeDoc.Adapter.InsertRow();
					}
					#endregion Insertar en Expediente_Documento

					db.Commit();
					db.CerrarConexion();
					btnAceptar.Visible = false;
					BuscarExpe();
					this.lblMensaje.Text = "Instruccion Grabada. ID:" + expeInstrucID.ToString();
				}
				else
				{
					ShowMessage( "Asegurese de seleccionar un Expediente");
				}
			}
		}

		#endregion btnAceptar_Click

		#region ShowMessage
		private void ShowMessage (string mensaje ){
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion

		#region CheckCorrespondencia
		protected void btnCheckCorresp_Click(object sender, System.EventArgs e)
		{
			if( this.txtNro.Text.Trim() != "" )
			{
				Correspondencia_Read( this.txtNro.Text, this.txtAnio.Text );
			}
			else{
				ShowMessage( @"Si no ingresa el Número de Correspondencia, se considerá como ""Instrucción Interna"" ");
				this.CorrespondenciaID = -1;
			}
		}
		#endregion CheckCorrespondencia

		#region Correspondencia_Read
		private void Correspondencia_Read( string Nro, string Anio )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

//			Berke.DG.DBTab.Correspondencia corresp = new Berke.DG.DBTab.Correspondencia( db );
			Berke.DG.ViewTab.vCorrespondencia corresp = new Berke.DG.ViewTab.vCorrespondencia(db );

			corresp.Dat.Nro.Filter = Nro;
			corresp.Dat.Anio.Filter = Anio;
			corresp.Dat.AreaID.Order = -1; // Si tiene varios mov, toma el de marcas
			corresp.Adapter.ReadAll();

			if( corresp.RowCount > 0 )
			{

				this.CorrespondenciaID = corresp.Dat.ID.AsInt; // Persiste via ViewState
				string path = Berke.Libs.Base.DocPath.digitalDocPath(
					corresp.Dat.Anio.AsInt, corresp.Dat.Nro.AsInt, 
					corresp.Dat.AreaID.AsInt );
				/*string path =digitalDocPath(
					corresp.Dat.Anio.AsInt, corresp.Dat.Nro.AsInt, 
					corresp.Dat.AreaID.AsInt );*/

				pnlCorrespResult.Visible = true;
				string des = "";
				des += "Corresp.ID: <b>" + corresp.Dat.ID.AsString + "</b> "+path+"<br>";
				des += "Fecha Ing.: <b>" + corresp.Dat.FechaAlta.AsString + "</b><br>";
				des += "Referencia: <b>" + corresp.Dat.RefCorresp.AsString + "</b><br>";
				des += "Remitente : <b>" + corresp.Dat.Nombre.AsString + "</b><br>";
	
//				Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento(db );
//				doc.Dat.CorrespondenciaID.Filter =	corresp.Dat.ID.AsInt;
//				doc.Adapter.ReadAll();
//				if ( doc.RowCount > 0 ){
//					docID = doc.Dat.ID.AsString;
//					des+= "Documen.ID: <b>"+ docID+ "</b>";
//				}
				this.lblCorresp.Text = des;
		
				#region Detalles de Correspondencia

//				if( corresp.Dat.TrabajoTipo.AsString.Trim() == "" ) // La corresp No tiene Movimiento
//				{ 
//					ddlCorrespMov.Items.Clear();
//					ddlCorrespMov.Items.Add(new System.Web.UI.WebControls.ListItem("Correspondencia sin Movimiento","NOMOV"));
//					ddlCorrespMov.SelectedIndex = 0;
//				}
//				else
//				{
//
//					ddlCorrespMov.Items.Clear();
//					ddlCorrespMov.Items.Add(new System.Web.UI.WebControls.ListItem("",""));
//					ddlCorrespMov.SelectedIndex = 0;
//					for( corresp.GoTop(); ! corresp.EOF; corresp.Skip() )
//					{
//						ddlCorrespMov.Items.Add(new System.Web.UI.WebControls.ListItem(
//							corresp.Dat.TrabajoTipo.AsString, corresp.Dat.movID.AsString ));
//					}
//					if( corresp.RowCount == 1 )
//					{
//						ddlCorrespMov.SelectedIndex = 1;
//					}
//					ddlCorrespMov.DataBind();
//				}
				#endregion Detalles de Correspondencia

				db.CerrarConexion();
			}
			else
			{
				this.CorrespondenciaID = -1;
				db.CerrarConexion();
				pnlCorrespResult.Visible = false;
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
