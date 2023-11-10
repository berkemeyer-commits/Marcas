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
using Berke.Libs.WebBase.Helpers;

namespace Berke.Marcas.WebUI.Home
{
	using System.Collections;

	using Libs.Base;
	using Libs.Base.DSHelpers;

	/// <summary>
	/// Summary description for InstruccionIngresar.
	/// </summary>
	public partial class InstruccionIngresar : System.Web.UI.Page
	{
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

		#region FechaNotificacion_Str
		private string FechaNotificacion_Str
		{
			get{ return Convert.ToString(( ViewState["FechaNotificacion_Str"] == null )? "" : ViewState["FechaNotificacion_Str"] ) ; }
			set{ ViewState["FechaNotificacion_Str"] = Convert.ToString( value );}
		}
		#endregion FechaNotificacion_Str

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

		#region MarcaDS
		private DataSet MarcaDS{
			get{ return ( ViewState["MarcaDS"] == null ) ? null : (DataSet)ViewState["MarcaDS"];}
			set{ ViewState["MarcaDS"] = value;}
		}
		#endregion MarcaDS

		#endregion Properties

		#region Controles 
	
		#endregion Controles 

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region DropDown de IntruccionTipo
			SimpleEntryDS seInstrucTipo = Berke.Marcas.UIProcess.Model.Instruccion.ReadForSelect();
			this.ddlInstruccion.Fill( seInstrucTipo.Tables[0], true );
			#endregion DropDown de IntruccionTipo

			VaciarControles();
		}
		#endregion Asignar Valores Iniciales

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( ! this.IsPostBack ){
				AsignarValoresIniciales();

				this.txtAnio.Text = DateTime.Today.Year.ToString();
			}
		}

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

		#region CheckCorrespondencia
		protected void btnCheckCorresp_Click(object sender, System.EventArgs e)
		{
			if( txtNro.Text.Trim() == "" || this.txtAnio.Text.Trim() == "")
			{
				this.ShowMessage("Ingrese la información para ubicar la correspondencia");
			}
			else
			{
				Correspondencia_Read( this.txtNro.Text, this.txtAnio.Text );
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
			Berke.DG.ViewTab.vCorrespNro correspnroarea = new Berke.DG.ViewTab.vCorrespNro(db);


			corresp.Dat.Nro.Filter = Nro;
			corresp.Dat.Anio.Filter = Anio;
			corresp.Adapter.ReadAll();

			if( corresp.RowCount > 0 )
			{

				correspnroarea.Adapter.ClearParams();
				correspnroarea.Adapter.AddParam("nro",corresp.Dat.Nro.AsInt);
				correspnroarea.Dat.vigente.Filter = true;
				correspnroarea.Adapter.ReadAll();


				this.CorrespondenciaID = corresp.Dat.ID.AsInt; // Persiste via ViewState

				string path = "";
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
				des +=  "ClienteID : <b>" + HtmlGW.OpenPopUp_Link("ClienteDatos.aspx", corresp.Dat.ClienteID.AsString, corresp.Dat.ClienteID.AsString, "ClienteID") + "</b><br>";
				//des += "ClienteID : <b>" + corresp.Dat.ClienteID.AsString + "</b><br>";
	
				//				Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento(db );
				//				doc.Dat.CorrespondenciaID.Filter =	corresp.Dat.ID.AsInt;
				//				doc.Adapter.ReadAll();
				//				if ( doc.RowCount > 0 ){
				//					docID = doc.Dat.ID.AsString;
				//					des+= "Documen.ID: <b>"+ docID+ "</b>";
				//				}
				this.lblCorresp.Text = des;
		
				
				db.CerrarConexion();
			}
			else
			{
				db.CerrarConexion();
		
				ShowMessage( " La correspondencia "+Nro+"/"+Anio+" No está registrada en el sistema" );
			}

		}
		#endregion Correspondencia_Read

		#region Click VerificarMarcas
		protected void btnCheckMarcas_Click(object sender, System.EventArgs e)
		{
			if( ! chkNoCorresp.Checked && this.CorrespondenciaID == -1 ){
				this.ShowMessage("Ingrese la correspondencia. Si desea grabar una instrucción sin correspondencia, active el CheckBox Correspondiente.");
				return;
			}

			if( txtRegistros.Text.Trim() != "" || ( this.txtActas.Text.Trim() != "" && this.txtActaAnio.Text != "") ){
				if( !(txtRegistros.Text.Trim() != "" &&  txtActas.Text.Trim() != "" )){
					VerificarMarcas( this.txtRegistros.Text, this.txtActas.Text, this.txtActaAnio.Text );
				}else{
					this.ShowMessage("Ingrese Número(s) de Registro o Acta(s), pero no ambos");
				}
			}		
			else
			{
				this.ShowMessage("Ingrese Número de Registro o Acta. Si son varios, separe con comas");
			}
		}
		#endregion Click VerificarMarcas

		#region VerificarMarcas
		private void VerificarMarcas( string strRegistros, string strActas, string strAnio )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

//			Berke.DG.ViewTab.vAgenteLocal agLoc = new Berke.DG.ViewTab.vAgenteLocal( db );
			Berke.DG.ViewTab.vCliente cliente = new Berke.DG.ViewTab.vCliente( db );

			Berke.DG.ViewTab.vExpeMarca vMarca = new Berke.DG.ViewTab.vExpeMarca( db );
			Berke.DG.DBTab.Marca Marca = new Berke.DG.DBTab.Marca( db );

			if( strRegistros != "" )
			{
				vMarca.Dat.RegistroNro.Filter = new DSFilter( new ArrayList(strRegistros.Split( ((String)",").ToCharArray() ))  );
				//mbaez solo registro y renovacion. Bug #65
				ArrayList lstTr = new ArrayList();
				lstTr.Add((int)GlobalConst.Marca_Tipo_Tramite.REGISTRO);
				lstTr.Add((int)GlobalConst.Marca_Tipo_Tramite.RENOVACION);
				vMarca.Dat.TramiteID.Filter = new DSFilter(lstTr);
			}
			else
			{
				vMarca.Dat.ActaNro.Filter = new DSFilter( new ArrayList(strActas.Split( ((String)",").ToCharArray() ))  );
				vMarca.Dat.ActaAnio.Filter = strAnio;
			}
			vMarca.Adapter.ReadAll( 250 );

			#region eliminar Duplicados
			int antID = -19992221;
			for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() ){
				if( vMarca.Dat.ExpedienteID.AsInt == antID ){
					vMarca.Delete();
				}else{
					antID = vMarca.Dat.ExpedienteID.AsInt;
				}
			}// end for
			vMarca.AcceptAllChanges();

			#endregion eliminar Duplicados


			if( vMarca.RowCount > 0 )
			{
				this.MarcaDS = vMarca.AsDataSet();
			}
			Berke.Html.HtmlTable	tab = new Berke.Html.HtmlTable("tbl");
			Berke.Html.HtmlTextFormater resaltado = new Berke.Html.HtmlTextFormater("-2",true,System.Drawing.Color.Red);
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera de la Tabla
			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();

			tab.AddCell("ExpID" );
			tab.AddCell("Trám." );
			tab.AddCell("Registro" );
			tab.AddCell("Acta" );

			tab.AddCell( "Denominación ");
			tab.AddCell( "Clase");

			tab.AddCell( "Pert.");

			tab.AddCell( "Vencimiento");
			tab.AddCell( "Cliente");

			tab.AddCell( "Propietario");
	
			tab.AddCell( "Instrucciones");
	
			
			tab.EndRow();
		
			tab.cell.BgColor = "White";
			tab.cell.Text.Bold = false;
			#endregion Cabecera de la Tabla
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));

			DateTime minVencim = DateTime.MinValue;
	

			for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() )
			{
				/*
				 * [rgimenez: El BUG#583 Indica que debe asignarse la instruccion al cliente de 
				  la marca y no del expediente
				  */
					
				//cliente.Dat.ID.Filter = vMarca.Dat.ClienteID.AsInt;
				
				Marca.Adapter.ReadByID(vMarca.Dat.MarcaID.AsInt);
				cliente.Dat.ID.Filter = Marca.Dat.ClienteID.AsInt;
				cliente.Adapter.ReadAll(5);

				tab.BeginRow();


				#region Expe ID
				string strExpeID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
					"MarcaDetalleL.aspx", // Pagina
					vMarca.Dat.ExpedienteID.AsString,  // Texto
					vMarca.Dat.ExpedienteID.AsString,  // Valor
					"ExpeID");  // Parametro


				tab.AddCell( chkSpc( strExpeID ));
				#endregion Expe ID

				#region TramiteAbrev
				tab.AddCell( chkSpc( vMarca.Dat.TramiteAbrev.AsString ));
				#endregion TramiteAbrev

				#region Registro
				tab.AddCell( resaltado.Html(chkSpc( vMarca.Dat.RegistroNro.AsString ))+ "/"+  vMarca.Dat.RegistroAnio.AsString);
				#endregion Registro

				#region Acta
				tab.AddCell( chkSpc( vMarca.Dat.Acta.AsString ));
				#endregion Acta

				#region Denominacion
				tab.AddCell( resaltado.Html(chkSpc( vMarca.Dat.Denominacion.AsString )));
				#endregion Denominacion

				#region Clase
				tab.AddCell( chkSpc( vMarca.Dat.Clase.AsString ));
				#endregion Clase

				#region Pertenencia
				Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca( db );
				marca.Adapter.ReadByID( vMarca.Dat.MarcaID.AsInt );
				string pert = "";
				if( marca.Dat.Nuestra.AsBoolean )
				{
					pert = "Ntra.";
				}
				else if (marca.Dat.Vigilada.AsBoolean)
				{
					pert = "Vig.";
				}
				else
				{
					pert = "3ros.";
				}
				tab.AddCell( chkSpc( pert ));
				#endregion Pertenencia

				#region Vencimiento
				string strVencim = string.Format("{0:ddd dd/MM/yy}",vMarca.Dat.VencimientoFecha.AsDateTime);
				if( vMarca.Dat.VencimientoFecha.IsNull ){
					strVencim = "";
				}
				tab.AddCell( chkSpc( strVencim ));

				if( ! vMarca.Dat.VencimientoFecha.IsNull )
				{
					if( minVencim  == DateTime.MinValue  ){
						minVencim = vMarca.Dat.VencimientoFecha.AsDateTime;
					}
					if( vMarca.Dat.VencimientoFecha.AsDateTime < minVencim )
					{
						minVencim = vMarca.Dat.VencimientoFecha.AsDateTime;
					}
				}
				#endregion Vencimiento

				#region Cliente
				tab.AddCell( chkSpc(cliente.Dat.Descrip.AsString ));
				#endregion

				#region Propietario

				tab.AddCell( chkSpc(vMarca.Dat.PropietarioNombre.AsString));

				#endregion

				#region Instrucciones Previas
				Berke.DG.ViewTab.vInstruccion instruc = new Berke.DG.ViewTab.vInstruccion( db );
				instruc.Dat.ExpedienteID.Filter = vMarca.Dat.ExpedienteID.AsInt;
				instruc.Adapter.ReadAll( 50 );

				string bfInstruc="";

				if( instruc.RowCount > 0 )
				{
					int cont=0;
					for( instruc.GoTop(); !instruc.EOF; instruc.Skip() ){
						if( cont++ > 0){
							bfInstruc+="<br>";
						}
						string corresp= "";
						if(instruc.Dat.CorrespNro.AsInt != 0){
							corresp = ".Corr:" + instruc.Dat.CorrespNro.AsString+ "/" +
												instruc.Dat.CorrespAnio.AsString +" ";
						}
						bfInstruc+= instruc.Dat.Fecha.AsString+"."+ corresp +
							resaltado.Html(instruc.Dat.Descrip.AsString)+"."+
							instruc.Dat.Nick.AsString ;
					}
				}
				tab.AddCell(  bfInstruc=="" ? "&nbsp;" : bfInstruc );

				#endregion Instrucciones previas
			
				tab.EndRow();													  
													
			}
			lblMarcas.Text = tab.Html();

			#region Calcular fecha de notificacion
			txtFechaNotif.Text = "";
			
			if( this.ddlInstruccion.SelectedIndex != 0 )
			{

				if( Convert.ToInt32( this.ddlInstruccion.SelectedValue) == (int)GlobalConst.InstruccionTipo.RENOVAR  || Convert.ToInt32( this.ddlInstruccion.SelectedValue) == (int)GlobalConst.InstruccionTipo.SOLICITARON_COTIZACION ) // RENOVAR o COTIZAR
				{
					if( minVencim != DateTime.MinValue )
					{
						if( minVencim < DateTime.Today )
						{
							minVencim = DateTime.Today;
						}
						minVencim = Berke.Marcas.BizActions.Lib.FechaMasPlazo( minVencim, -2, 1, db ); // 1= Dias Habiles
						txtFechaNotif.Text		= ObjConvert.AsString(minVencim,Berke.Libs.Base.CultureFormat.UI_Culture,Berke.Libs.Base.DateFormat.Fecha ) ;
						lblNotif.Visible		= true;
						txtFechaNotif.Visible	= true;
						chkNotif.Visible		= true;
						Label3.Visible			= true;
					}
				}
			}
			
			#endregion Calcular fecha de notificacion

			db.CerrarConexion();
	
		}
		#endregion VerificarMarcas

		#region Funciones Genericas Replicadas
		
		#region chkSpc 
		private string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}
		#endregion chkSpc 
	
		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion ShowMessage

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

		#endregion Funciones Genericas Replicadas

		#region Vaciar Controles 
		private void VaciarControles()
		{
			this.txtNro.Text = "";
			lblCorresp.Text = "";
			this.txtRegistros.Text = "";
			this.lblMarcas.Text = "";

			lblNotif.Visible = false;
			txtFechaNotif.Visible = false;
			Label3.Visible			= false;
			txtFechaNotif.Text = "";
			chkNotif.Visible = false;
		}
		#endregion Vaciar Controles 
		
		#region datosObligatoriosOK
		private bool datosObligatoriosOK( out string mensaje )
		{
			bool ret = true;
			mensaje = "";
			if( ddlInstruccion.SelectedIndex == 0 ){
				ret = false;
				mensaje += "Debe elegir una Instrucción";
			}
			if( this.txtRegistros.Text.Trim() == "" && this.txtActas.Text.Trim() == "" ){
				ret = false;
				mensaje += ". Debe ingresar por lo menos un registro o nro. de acta";			
			}
			return ret;
		}
		#endregion datosObligatoriosOK

		#region btnAceptar_Click
		protected void btnAceptar_Click(object sender, System.EventArgs e)
		{
			string mensaje = "";
			if( datosObligatoriosOK( out mensaje ))
			{
				if( Grabar( out mensaje ) )
				{
					VaciarControles();
					this.MarcaDS = null;
					
				}
			}
			ShowMessage(mensaje);
		}

		#endregion btnAceptar_Click

		#region Grabar
		private bool Grabar(out string mensaje )
		{
			string nl = @"
";

			DataSet marcaDS = this.MarcaDS;
			if( marcaDS == null ){
				mensaje = "Debe seleccionar alguna marca";
				return false;
			}
			Berke.DG.ViewTab.vExpeMarca vMarca = new Berke.DG.ViewTab.vExpeMarca( marcaDS.Tables[0] );
	
			#region Inicializar db
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			db.IniciarTransaccion();
			#endregion Inicializar db

			string body = "";

			
			 body = "Corresp. " + this.txtNro.Text + "/" + this.txtAnio.Text + " ";

			/*
			if( vMarca.RowCount > 1 ){ 
				body += @"Verificar renovacion de las marcas" + nl ;
			}else{
				body += @"Verificar renovacion de la marca" + nl ;
			}
			*/
	
			for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() )
			{

				body = "Corresp. " + this.txtNro.Text + "/" + this.txtAnio.Text + " ";
				body += @"Verificar renovacion de la(s) marca(s) " + nl ;
				body += "Exped.ID:"+ vMarca.Dat.ExpedienteID.AsString + " "+  
					@"Registro: <font color=""#FF0000"">" + vMarca.Dat.Registro.AsString + "</font>" +
					" Vence: " + vMarca.Dat.VencimientoFecha.AsString+ " Denom: <strong>" + vMarca.Dat.Denominacion.AsString + "</strong>" + nl+nl;
				#region Aplicar cambios en BD

				this.ExpedienteID = vMarca.Dat.ExpedienteID.AsInt;

				#region Leer Correspondencia
				Berke.DG.DBTab.Correspondencia corresp = new Berke.DG.DBTab.Correspondencia(db );
				if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
				{
					corresp.Adapter.ReadByID( CorrespondenciaID );
				}
				#endregion Leer Correspondencia
	
				#region Leer Expediente
				Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
				expe.Adapter.ReadByID( this.ExpedienteID );
				#endregion Leer Expediente

 				#region Verificar que exista Docum   ** Obsoleto **
//				Berke.DG.DBTab.Documento docum = new Berke.DG.DBTab.Documento(db );
//				int documID = -1;
//				if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
//				{
//					docum.Dat.CorrespondenciaID.Filter = CorrespondenciaID;
//					docum.Adapter.ReadAll();
//					if ( docum.RowCount > 0 )
//					{
//						documID = docum.Dat.ID.AsInt;
//					}
//					else
//					{
//						#region Insertar documento 
//						docum.NewRow();
//						docum.Dat.CorrespondenciaID.Value	= CorrespondenciaID;
//						docum.Dat.IdentificadorNro.Value	= corresp.Dat.Nro.AsInt;
//						docum.Dat.IdentificadorAnio.Value	= corresp.Dat.Anio.AsInt;
//						docum.Dat.Fecha.Value				= corresp.Dat.FechaAlta.AsDateTime;
//						docum.Dat.Descrip.Value				= corresp.Dat.RefCorresp.AsString;
//						docum.Dat.DocumentoTipoID.Value		= 4; // Correspondencia Orden
//						docum.PostNewRow();
//						documID = docum.Adapter.InsertRow();
//						#endregion Insertar documento 
//					}
//				}
				#endregion Verificar que exista Docum

				// Tabla : Expediente_Instruccion
				Berke.DG.DBTab.Expediente_Instruccion expeInstruc = new Berke.DG.DBTab.Expediente_Instruccion( db );

				#region Asignar valores a Expediente_Instruccion
				expeInstruc.NewRow(); 
				//					expeInstruc.Dat.ID						.Value = DBNull.Value;					// int PK  Oblig.
				expeInstruc.Dat.MarcaID					.Value = expe.Dat.MarcaID.AsInt;		// int Oblig.
				expeInstruc.Dat.ExpedienteID			.Value = this.ExpedienteID;					// int Oblig.
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

				#region Insertar en Expediente_Documento  ** Obsoleto **
//				if( this.CorrespondenciaID != -1 ) // Se eligió correspondencia
//				{
//					Berke.DG.DBTab.Expediente_Documento expeDoc = new Berke.DG.DBTab.Expediente_Documento( db );
//					expeDoc.NewRow();
//					expeDoc.Dat.DocumentoID.Value = documID;
//					expeDoc.Dat.ExpedienteID.Value = ExpedienteID;
//					expeDoc.PostNewRow();
//
//					expeDoc.Adapter.InsertRow();
//				}
				#endregion Insertar en Expediente_Documento
		
				#endregion Aplicar cambios en BD

				#region Notificar 
				if( txtFechaNotif.Text != "" && this.chkNotif.Checked )
				{
					if(  int.Parse( this.ddlInstruccion.SelectedValue ) == (int)GlobalConst.InstruccionTipo.RENOVAR ) // RENOVAR
					{
						Berke.Marcas.BizActions.Lib.Notificar(
							11,
							body,
							ObjConvert.AsDateTime( this.txtFechaNotif.Text ),
							db );
					}

					if(  int.Parse( this.ddlInstruccion.SelectedValue ) == (int)GlobalConst.InstruccionTipo.SOLICITARON_COTIZACION ) // Solicitud de Cotizacion
					{
						Berke.Marcas.BizActions.Lib.Notificar(
							12,
							body,
							ObjConvert.AsDateTime( this.txtFechaNotif.Text ),
							db );
					}

				}
				#endregion Notificar 
			} // endfor - vMarca.

			#region Notificar *** Original
			/*
			if( txtFechaNotif.Text != "" && this.chkNotif.Checked )
			{
				if(  int.Parse( this.ddlInstruccion.SelectedValue ) == 1 ) // RENOVAR
				{
					Berke.Marcas.BizActions.Lib.Notificar(
						11,
						body,
						ObjConvert.AsDateTime( this.txtFechaNotif.Text ),
						db );
				}

				if(  int.Parse( this.ddlInstruccion.SelectedValue ) == 6 ) // Solicitud de Cotizacion
				{
					Berke.Marcas.BizActions.Lib.Notificar(
						12,
						body,
						ObjConvert.AsDateTime( this.txtFechaNotif.Text ),
						db );
				}

			}
			*/
			#endregion Notificar 
			db.Commit();
			db.CerrarConexion();
		
			mensaje = "Instrucciones Grabadas !!!";

			return true;
		}
		#endregion Grabar

	}// end class
}
