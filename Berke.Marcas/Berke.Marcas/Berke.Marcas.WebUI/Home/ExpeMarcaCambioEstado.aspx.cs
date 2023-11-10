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
	using Berke.Marcas.WebUI.Helpers;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.Base.Helpers;

	/// <summary>
	/// Summary description for ExpeMarcaCambioEstado.
	/// </summary>
	public partial class ExpeMarcaCambioEstado : System.Web.UI.Page
	{

		#region Variables Globales


		#endregion Variables Globales

		#region Controles del Form
		protected System.Web.UI.WebControls.TextBox txtCorrNro;
		protected System.Web.UI.WebControls.TextBox txtCorrAnio;
		protected System.Web.UI.WebControls.Label Label10;
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

		#region MarcaID
		private int MarcaID
		{
			get{ return Convert.ToInt32(( ViewState["MarcaID"] == null )? -1 : ViewState["MarcaID"] ) ; }
			set{ ViewState["MarcaID"] = Convert.ToString( value );}
		}
		#endregion MarcaID

		#region AgenteLocalID
		private int AgenteLocalID
		{
			get{ return Convert.ToInt32(( ViewState["AgenteLocalID"] == null )? -1 : ViewState["AgenteLocalID"] ) ; }
			set{ ViewState["AgenteLocalID"] = Convert.ToString( value );}
		}
		#endregion AgenteLocalID
		

		#endregion Properties

		#region Asignar Delegados
		private void AsignarDelegados()
		{

			this.cbxCliente.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxCliente_LoadRequested); 

		}
		#endregion Asignar Delegados

		#region Carga de Combo


		#region Cliente
		private void cbxCliente_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
		{
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow(); 
			if( combo.SelectedKeyValue == "ID" )	
			{
				inTB.Dat.Entero .Value = combo.Text;   //Int32
			}
			else
			{
				inTB.Dat.Alfa	.Value = combo.Text;   //String	
			}
			inTB.PostNewRow(); 				
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Cliente.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion Cliente	
	
	
	
		#endregion Carga de Combo

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{

			Berke.DG.ViewTab.ListTab ltAgBerke = Berke.Marcas.UIProcess.Model.AgenteLocal.ReadForSelect();
			ddlAgenteBerke.Fill( ltAgBerke.Table, true);

			
			pnlBuscar.Visible		= true;
			pnlExpeDatos.Visible	= false;
			pnlDatos.Visible		= false;
			this.pnlBotones.Visible = false;
			this.pnlIncorporar.Visible = false;
	
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
		private bool ParametrosDeBusquedaNulos()
		{
			return (
				txtExpeID.Text.Trim()		== "" && 
				txtRegistroNro.Text.Trim()	== "" && 
				txtActaNro.Text.Trim()		== "" && 
				txtActaAnio.Text.Trim()		== "" &&
				this.txtMarcaID.Text.Trim() == ""
				);
			
		}
		#endregion ParametrosDeBusquedaNulos

		#region ParametrosOk
		private bool ParametrosOk()
		{
			bool ret = true;
			string mensaje = "";

//			if(  txtAnio.Text.Trim() == "" || txtNro.Text.Trim() == ""  )
//			{
//				mensaje+= "El Número de Correspondencia y el Año deben ser ingresados"; 
//				ret = false;
//			}
//			if( ddlInstruccion.SelectedValue == "" )
//			{
//				mensaje+= (mensaje=="" ?"":". Además, ")+ "Debe seleccionar la intrucción que corresponda"; 
//				ret = false;
//			
//			}
			if( ! ret )
			{
				ShowMessage( mensaje );
			}
			return ret;
		}
		#endregion ParametrosOk

		#region Buscar Expediente

		protected void txtBuscar_Click(object sender, System.EventArgs e)
		{

			if( ParametrosDeBusquedaNulos() )
			{
				ShowMessage( " Ingrese parámetros de búsqueda");
				return;
			}
			else{
				BuscarExpediente();
			}
		}
		
		private void BuscarExpediente(){
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
				
				pnlExpeDatos.Visible	= true;
				pnlDatos.Visible		= true;
				pnlBotones.Visible		= true;

				#region Nuestro
				string NuestroSN = "";
				if (resul.Dat.ExpeNuestro.AsBoolean  )
				{
					NuestroSN = "Nuestro";
				}
				else
				{
					NuestroSN = "Tercero";
				}
				#endregion Nuestro

				#region Datos de Expediente
				string expeID = resul.Dat.ExpedienteID.AsString ;

				this.AgenteLocalID = resul.Dat.AgenteLocalID.AsInt;	// Persiste via ViewState
				this.ExpedienteID = resul.Dat.ExpedienteID.AsInt;	// Persiste via ViewState

				expeID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link( 
					"MarcaDetalleL.aspx", 		// Pagina
					expeID,			// Texto
					expeID,			// Valor del parametro
					"ExpeID");		// Nombre del parametro


				this.lblExpeDescrip.Text = "" + "<table><tr><td><u>Expediente ID</u>:<b>" +expeID + "</b>" +  " <b>" + NuestroSN + "</b>" + " <u>Acta</u>:<b>"+ resul.Dat.Acta.AsString + 
					"</b><br> <u>Trámite</u>: <b>" + resul.Dat.TramiteAbrev.AsString + "</b> <u>Situación</u>: <b>" + resul.Dat.SituacionDecrip.AsString + "</b>"+ 
					"</td></tr><tr><td> <u>O. Trabajo</u>:<b>" +resul.Dat.OrdenTrabajo.AsString + "</b> <u>F. Venc.</u>: <b>" + resul.Dat.VencimientoFecha.AsString + "</td></tr></table>";

				#endregion 

				string marcaID = resul.Dat.MarcaID.AsString;
				marcaID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link( 
					"MarcaDetalleL.aspx", 		// Pagina
					marcaID,			// Texto
					marcaID,			// Valor del parametro
					"MarcaID");			// Nombre del parametro


				#region Datos de Marca
				this.MarcaID = resul.Dat.MarcaID.AsInt; // // Persiste via ViewState

				Berke.DG.DBTab.Marca mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID( resul.Dat.MarcaID.AsInt );


				this.lblMarcaDescrip.Text = "<u>Marca</u>: ID: "+ marcaID + "<b> "+ resul.Dat.Denominacion.AsString + "</b>"+
					" <b>"+ resul.Dat.Clase.AsString + "</b><br>";

				this.lblMarcaDescrip.Text+= "<b>";
				this.lblMarcaDescrip.Text+= mar.Dat.Nuestra.AsBoolean		? "Nuestra "	: "De Terceros  " + Berke.Libs.WebBase.Helpers.HtmlGW.Spaces( 5 );
				this.lblMarcaDescrip.Text+= mar.Dat.Vigilada.AsBoolean		? "Vigilada "	: "No-Vigilada  "	+ Berke.Libs.WebBase.Helpers.HtmlGW.Spaces( 5 );

				this.lblMarcaDescrip.Text+= mar.Dat.Sustituida.AsBoolean	? "Sustituida " : "No-Sustituida"	+ Berke.Libs.WebBase.Helpers.HtmlGW.Spaces( 5 );
				this.lblMarcaDescrip.Text+= mar.Dat.StandBy.AsBoolean		? "StandBy "	: "No-StandBy   "	+ Berke.Libs.WebBase.Helpers.HtmlGW.Spaces( 5 );
				this.lblMarcaDescrip.Text+= mar.Dat.Vigente.AsBoolean		? "Activa "	: "Histórico  "			+ Berke.Libs.WebBase.Helpers.HtmlGW.Spaces( 5 );
				this.lblMarcaDescrip.Text+= "</b><p></p>";
				#endregion Datos de Marca


				#region Asignar operaciones validas
				ddlOperacion.Items.Clear();
				ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem(" ","NADA") );
				
				#region Incorporar / Transpasar
				if( mar.Dat.Nuestra.AsBoolean )
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("DE TERCERO (Traspasar)","TRASPASAR") );				
				}
				else{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("TOMAR INTERVENCION (Incorporar) ","INCORPORAR") );				
				}
				#endregion Incorporar / Transpasar				
				
				#region Vigilar / No Vigilar	
				// Vigilar/No vigilar se muestra solamente para marcas de terceros. mbaez 31/01/2007
				if( mar.Dat.Vigilada.AsBoolean && !mar.Dat.Nuestra.AsBoolean)
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("NO VIGILADA","NO_VIGILAR") );				
				}
				else if (!mar.Dat.Nuestra.AsBoolean)
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("VIGILADA","VIGILAR") );				
				}
			
				#endregion  Vigilar / No Vigilar	

				#region Sustituir / Cancelar Sustitución	
				if( mar.Dat.Sustituida.AsBoolean )
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("NO SUSTITUIDA","NO_SUSTITUIR") );				
				}
				else
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("SUSTITUIDA","SUSTITUIR") );				
				}
			
				#endregion Sustituir / Cancelar Sustitución					

				#region Poner en StandBy / Quitar de StandBy	
				if( mar.Dat.StandBy.AsBoolean )
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("NO STANDBY","NO_PARADO") );				
				}
				else
				{
					//ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("STANDBY","PARADO") );				
				}
			
				#endregion Abandonar / Activar	

				#region Abandonar / Activar	
				if( mar.Dat.Vigente.AsBoolean )
				{
					//ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("HISTORICO (Abandonar)","ABANDONAR") );				
				}
				else
				{
					ddlOperacion.Items.Add(new System.Web.UI.WebControls.ListItem("ACTIVA","ACTIVAR") );				
				}
			
				#endregion Abandonar / Activar	


				#endregion Asignar operaciones validas	

			}
			else
			{
				ShowMessage( " No se encontraron registros " );
				pnlExpeDatos.Visible	= false;
				pnlDatos.Visible		= false;
				pnlBotones.Visible		= false;
			}

		}// end BuscarExpediente()

		#endregion Buscar Expediente

//		private void btnAceptar_Click(object sender, System.EventArgs e)
//		{
//			if( ParametrosOk() ) 
//			{
//				
//			}
//		}

		#region ShowMessage
		private void ShowMessage (string mensaje )
		{
	
			this.RegisterClientScriptBlock("key",Berke.Libs.WebBase.Helpers.HtmlGW.Alert_script(mensaje));

		}
		#endregion
	

		
		#region Asignar DropDown de Motivo al cambiar la operacion a Realizar
		protected void ddlOperacion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool verDropMotivo = false;
			bool verTxtPropietarios = false;
			bool verTxtPoder = false;
			string MotivoDefValue="";

			Berke.DG.DBTab.PertenenciaMotivo param = new Berke.DG.DBTab.PertenenciaMotivo();
			param.NewRow();
			param.DisableConstraints(); // Necesario cuando un dbTab se usa como parmetro


			switch( ddlOperacion.SelectedValue)
			{
				#region Transpasar/Incorporar
				case "TRASPASAR" :
					MotivoDefValue="11";
					verDropMotivo = true;
					break;
				case "INCORPORAR" :
					MotivoDefValue="11";
					verDropMotivo = false;
					verTxtPropietarios = true;
					verTxtPoder = true;
					this.pnlIncorporar.Visible = true;

					break;

				#endregion 

				#region Vigilar / No Vigilar
				case "VIGILAR" :
					param.Dat.Vigilada.Value = true;
					verDropMotivo = true;
					this.pnlIncorporar.Visible = true;
					verTxtPropietarios = true;
					break;

				case "NO_VIGILAR" :
					param.Dat.Vigilada.Value = true;
					verDropMotivo = true;

					break;

				#endregion Vigilar / No Vigilar

				#region Sustituir/ No Sustituir
				case "SUSTITUIR" :
					MotivoDefValue="2";
					verDropMotivo = true;
					break;
				case "NO_SUSTITUIR" :
					verDropMotivo = true;
					break;
				#endregion Sustituir/ No Sustituir

				#region Parado / No Parado
				case "PARADO" :
					param.Dat.Parada.Value = true;
					verDropMotivo = true;
				
					break;
				case "NO_PARADO" :
					param.Dat.Parada.Value = true;
					verDropMotivo = true;
					
					break;
				#endregion Parado / No Parado

				#region Abandonar / Activar
				case "ABANDONAR" :
					param.Dat.StandBy.Value = true;
					verDropMotivo = true;

					break;
				case "ACTIVAR" :
					param.Dat.StandBy.Value = true;
					verDropMotivo = true;
					
					break;

				#endregion Abandonar / Activar
			}
			param.PostNewRow();
			
			if( verDropMotivo )
			{
				MostrarMotivo();
				#region Cargar DropDown de Motivo
				Berke.DG.DBTab.PertenenciaMotivo motivo  = Berke.Marcas.UIProcess.Model.PertenenciaMotivo.ReadList( param );
			
				this.ddlMotivo.Items.Clear();  
				ddlMotivo.Items.Add(new System.Web.UI.WebControls.ListItem("","-1") );				
            
				for( motivo.GoTop(); ! motivo.EOF ; motivo.Skip() ) 
				{		
					ddlMotivo.Items.Add(new System.Web.UI.WebControls.ListItem(motivo.Dat.Descrip.AsString,motivo.Dat.ID.AsString ) );				
			
				}

				if ( MotivoDefValue.Length > 0 ) 
				{
					ddlMotivo.SelectedValue=MotivoDefValue;
				}
				#endregion 
			}
			else{
				OcultarMotivo();
			}
			// Agregado por mbaez. 15/11/2006
			// Se habilita/deshabilita el campo para agregar propietarios.
			txtPropietarios.Visible = verTxtPropietarios;
			lblPropietario.Visible  = verTxtPropietarios;

			/*[rgimenez:21-mar-07]*/
			lblPoder.Visible        = verTxtPoder;
			txtPoder.Visible        = verTxtPoder;
			

		

		}
		#endregion Asignar DropDown de Motivo al cambiar la operacion a Realizar

		#region Mostrar DropDown de Motivos
		private void MostrarMotivo()
		{
			ddlMotivo.Visible = true;
			this.lblMotivo.Visible = true;
		}
		#endregion 

		#region Ocultar DropDown de Motivos
		private void OcultarMotivo()
		{
			ddlMotivo.Visible = false;
			this.lblMotivo.Visible = false;
		}
		#endregion 

		#region Grabar
		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			switch( ddlOperacion.SelectedValue)
			{
					#region Transpasar/Incorporar
				case "TRASPASAR" :

					Traspasar();
					BuscarExpediente();
					break;

				case "INCORPORAR" :
					
					#region Check de Parametros Nulos
					if( this.ddlFacturable.SelectedValue == "" ||
						this.ddlAgenteBerke.SelectedValue == "" ||
						this.cbxCliente.SelectedValue == "" )
					{
				
						this.ShowMessage( "Verifique los parametros Facturable,Agente y Cliente");
						return;

					}
					#endregion  Check de Parametros Nulos

					Incorporar();
					BuscarExpediente();
					break;

					#endregion Transpasar/Incorporar

					#region Vigilar / No Vigilar
				case "VIGILAR" :
					if (this.txtPropietarios.Text.Trim() == "") 
					{
						ShowMessage("Debe especificar el propietario.");
						return;
					}
					Vigilar_ON();
					BuscarExpediente();

					break;

				case "NO_VIGILAR" :
	
					Vigilar_OFF();
					BuscarExpediente();

					break;

					#endregion Vigilar / No Vigilar

					#region Sustituir/ No Sustituir
				case "SUSTITUIR" :
					this.ShowMessage(" SUSTITUIR No Implementado");

					break;
				
				case "NO_SUSTITUIR" :
					this.ShowMessage(" NO_SUSTITUIR No Implementado");

					break;

					#endregion Sustituir/ No Sustituir

					#region Parado / No Parado

				case "PARADO" :
					this.ShowMessage(" PARADO No Implementado");
			
					break;

				case "NO_PARADO" :
					this.ShowMessage(" NO_PARADO No Implementado");
			
					break;
					#endregion Parado / No Parado

					#region Abandonar / Activar

				case "ABANDONAR" :
					this.ShowMessage(" ABANDONAR No Implementado. Se implementa como SITUACION ");

					break;

				case "ACTIVAR" :
					this.ShowMessage(" ACTIVAR No Implementado. Se implementó como MODIFICACION DE MARCA");
					
					break;

					#endregion Abandonar / Activar


			}
		}
		#endregion Grabar

		#region Actualizar Propietario
		/* Este procedimiento es invocado cuando se pasa a VIGILAR o TOMAR INTERVENCION
		 * VIGILAR  : requiere ingresar Propietario
		 * TOMAR INT: requiere ingresar Propietario o Poder
		 * Solo en TOMAR INT se agrega datos en ExpedienteXPropietario
		 * pasar_a es un parametro que permite identificar el estado desde el cual fue 
		 * invocado el procedimiento 
		 * */
		private int actualizar_propietario(Berke.DG.DBTab.Marca mar, Berke.DG.DBTab.Expediente expe, Berke.Libs.Base.Helpers.AccesoDB db, string pasar_a) 
		{

			string Nombre="";
			string Direccion="";
			string paisalfa="";
			string msg = "Para TOMAR INTERVENCION debe ingresar el poder o el propietario";

			#region Verificar Derecho Propio
			bool w_DerechoPropio = false;

			if ( (( txtPropietarios.Text.Length == 0) && (txtPoder.Text.Length == 0)) || (( txtPropietarios.Text.Length > 0 && txtPoder.Text.Length > 0)))
			{
				
				if ( pasar_a == "VIGILAR" ){
					msg= "Para VIGILAR debe ingresar el propietario";
				}

				ShowMessage(msg);
				return 1;
			}
			 w_DerechoPropio = txtPropietarios.Text.Length > 0;
			#endregion Verificar Derecho Propio

			if ( w_DerechoPropio ) 
			{
				#region Por Derecho Propio

				#region Recuperamos datos del propietario
				int propietarioID = int.Parse( this.txtPropietarios.Text );
				Berke.DG.DBTab.Propietario pro_prop = new Berke.DG.DBTab.Propietario( db );
		
				pro_prop.Adapter.ReadByID(propietarioID);

				if (pro_prop.RowCount == 0) 
				{
					ShowMessage("El propietario ("+ txtPropietarios.Text +") no existe.");
					return 1;
				}
			
				Nombre    = pro_prop.Dat.Nombre.AsString;
				Direccion = pro_prop.Dat.Direccion.AsString;

				#endregion Recuperamos datos del propietario


				#region Insertar Expediente por Propietario
				/* Solo asignamos Propietario al Expediente cuando se toma Intervencion
				 * Tomar Intervencion implica adueñarse del tramite cuando este aun
				 * no ha concluido.
				 * */

				if ( pasar_a == "INCORPORAR" )
				{
					# region Eliminamos entradas existentes en ExpedienteXPropietario
					eliminar_expXPropietario( expe , db );
					#endregion Eliminamos entradas existentes en ExpedienteXPropietario

					insertar_expXPropietario(expe, db, pro_prop.Dat.ID.AsInt, w_DerechoPropio);
				}
				#endregion Insertar Expediente por Propietario
			
				#region Recuperamos datos del pais
				paisalfa = obtener_pais( pro_prop.Dat.PaisID.AsInt , db );
				#endregion Recuperamos datos del pais

				#endregion Por Derecho Propio

				# region Eliminamos entradas existentes en PropietarioxMarca
				eliminar_propXMarca( expe , db );
				#endregion Eliminamos entradas existentes en PropietarioxMarca

				#region Insertar PropietarioXMarca
				insertar_propXMarca(expe.Dat.MarcaID.AsInt, propietarioID, db);
				#endregion Insertar PropietarioXMarca
			} 
			else {

				#region Por Poder
				/* El poder se permite ingresar solo cuando se pasa a TOMAR INTERVENCION 
				 * por tanto no es necesario verificar desde donde fue invocado 
				 * el procedimiento "actualizar_propietario" 
				 */			
				#region Recuperamos datos del poder

				int poderID = int.Parse( this.txtPoder.Text );
				Berke.DG.DBTab.Poder poder = new Berke.DG.DBTab.Poder( db );
				poder.Adapter.ReadByID(poderID);

				if (poder.RowCount == 0) 
				{
					ShowMessage("El poder ("+ txtPoder.Text +") no existe.");
					return 1;
				}

				/* Para actualizar en la marca */
				Nombre   = poder.Dat.Denominacion.AsString;	
				Direccion= poder.Dat.Domicilio.AsString;
				paisalfa = obtener_pais( poder.Dat.PaisID.AsInt , db );

				#endregion Recuperamos datos del poder


				#region Recuperamos datos PropietarioXPoder
				Berke.DG.DBTab.PropietarioXPoder pxpoder = new Berke.DG.DBTab.PropietarioXPoder( db );

				pxpoder.Dat.PoderID.Filter = poder.Dat.ID.AsInt;
				pxpoder.Adapter.ReadAll();

				if ( pxpoder.RowCount == 0 ){
					ShowMessage("El poder no tiene asociado ningun propietario");
					return 1;
				}

				# region Eliminamos entradas existentes en PropietarioxMarca
				eliminar_propXMarca( expe , db );
				#endregion Eliminamos entradas existentes en PropietarioxMarca

				#region Eliminamos entradas existentes en ExpedienteXPropietario
					eliminar_expXPropietario( expe , db );
				#endregion Eliminamos entradas existentes en ExpedienteCPropietario

				for (pxpoder.GoTop(); ! pxpoder.EOF ;pxpoder.Skip()) 
				{

					#region Insertar PropietarioXMarca
					insertar_propXMarca(expe.Dat.MarcaID.AsInt, pxpoder.Dat.PropietarioID.AsInt,db);
					#endregion Insertar PropietarioXMarca

					#region Insertar Expediente por Propietario
					insertar_expXPropietario(expe, db, pxpoder.Dat.PropietarioID.AsInt, w_DerechoPropio);
					#endregion Insertar Expediente por Propietario

				}


				#region insertar Expediente por Poder
				Berke.DG.DBTab.ExpedienteXPoder expXPoder = new Berke.DG.DBTab.ExpedienteXPoder( db );
				expXPoder.NewRow();
				expXPoder.Dat.PoderID.Value      = poder.Dat.ID.AsInt;
				expXPoder.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt ;
				expXPoder.PostNewRow();
				expXPoder.Adapter.InsertRow();

				#endregion

				#endregion Recuperamos datos PropietarioXPoder
			

				#endregion Poder
				
			}

			#region Actualizar propietario de la marca
			actualizar_propietario_enMarca(mar,Nombre, Direccion, paisalfa);
			#endregion Actualizar propietario de la marca
	

			
			return 0;
		}

		#endregion Actualizar Propietario

		#region Eliminar_expXPropietario
		private void eliminar_expXPropietario(Berke.DG.DBTab.Expediente expe, Berke.Libs.Base.Helpers.AccesoDB db )
		{

			Berke.DG.DBTab.ExpedienteXPropietario expXProp = new Berke.DG.DBTab.ExpedienteXPropietario( db );

			# region Eliminamos entradas existentes en ExpedienteXPropietario
			expXProp.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expXProp.Adapter.ReadAll();
			for (expXProp.GoTop(); ! expXProp.EOF ;expXProp.Skip()) 
			{
				expXProp.Delete();
				expXProp.Adapter.DeleteRow();
			}
			#endregion Eliminamos entradas existentes en ExpedienteXPropietario

		}
		#endregion Eliminar_expXPropietario

		#region Eliminar_PropXMarca
		private void eliminar_propXMarca(Berke.DG.DBTab.Expediente expe, Berke.Libs.Base.Helpers.AccesoDB db )
		{
			Berke.DG.DBTab.PropietarioXMarca pxm = new Berke.DG.DBTab.PropietarioXMarca( db );

			# region Eliminamos entradas existentes en PropietarioxMarca
			pxm.Dat.MarcaID.Filter = expe.Dat.MarcaID.AsInt;
			pxm.Adapter.ReadAll();
			for (pxm.GoTop(); ! pxm.EOF ;pxm.Skip()) 
			{
				pxm.Delete();
				pxm.Adapter.DeleteRow();
			}
			#endregion Eliminamos entradas existentes en PropietarioxMarca

		}
		#endregion Eliminar_PropXMarca

		#region Insertar PropietarioXMarca
		private void insertar_propXMarca(int MarcaID , int propietarioID, Berke.Libs.Base.Helpers.AccesoDB db ) 
		{
			Berke.DG.DBTab.PropietarioXMarca pxm = new Berke.DG.DBTab.PropietarioXMarca( db );
			pxm.NewRow();
			pxm.Dat.MarcaID.Value = MarcaID;
			pxm.Dat.PropietarioID.Value = propietarioID;
			pxm.PostNewRow();
			pxm.Adapter.InsertRow();
		}

		#endregion Insertar PropietarioXMarca

		#region Insertar Expediente por Propietario
		private void insertar_expXPropietario(Berke.DG.DBTab.Expediente expe, Berke.Libs.Base.Helpers.AccesoDB db,   int propietarioID , bool derPropio )  
		{
			Berke.DG.DBTab.ExpedienteXPropietario expedienteXPropietario = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			expedienteXPropietario.NewRow();
			expedienteXPropietario.Dat.ExpedienteID .Value = expe.Dat.ID.AsInt;
			expedienteXPropietario.Dat.PropietarioID.Value = propietarioID;
			expedienteXPropietario.Dat.DerechoPropio.Value = derPropio ;
			expedienteXPropietario.PostNewRow();
			expedienteXPropietario.Adapter.InsertRow();
			
		}
		#endregion

		#region Actualizar Propietario de la Marca
		private void actualizar_propietario_enMarca(Berke.DG.DBTab.Marca mar , string Nombre, string Direccion, string paisalfa)
		{
			mar.Dat.Propietario.Value = Nombre;
			mar.Dat.ProDir.Value      = Direccion;
			mar.Dat.ProPais.Value     = paisalfa;
		}
		#endregion Actualizar Propietario de la Marca

		#region obtener_pais
		private string obtener_pais(int PaisID, Berke.Libs.Base.Helpers.AccesoDB db) 
		{
			Berke.DG.DBTab.CPais pa_pais = new Berke.DG.DBTab.CPais( db );
			pa_pais.Adapter.ReadByID( PaisID );			
			return ( pa_pais.Dat.paisalfa.AsString) ;
		}

	

		#endregion obtener_pais


		#region 	Vigilar_ON
		private void 	Vigilar_ON()
		{
		
	
			#region Crear parametro para insertar ExpePertenencia
			Berke.DG.DBTab.Expediente_Pertenencia param = new Berke.DG.DBTab.Expediente_Pertenencia();
		
			param.NewRow();
			param.Dat.ExpedienteID			.Value	= this.ExpedienteID;
			param.Dat.Nuestra				.SetNull();
			param.Dat.Vigilada				.Value	= true;
			param.Dat.Sustituida			.SetNull();
			param.Dat.StandBy				.SetNull();
			param.Dat.PertenenciaMotivoID	.Value	= this.ddlMotivo.SelectedValue;
			param.Dat.Fecha					.Value	= DateTime.Today;
			param.Dat.FuncionarioID			.Value	= MySession.FuncionarioID;
			param.Dat.Obs					.Value	= this.txtObs.Text;
			param.Dat.AgenteLocalID			.Value	= this.AgenteLocalID;
		
			param.PostNewRow();
			#endregion 

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;
			db.IniciarTransaccion();

			Berke.DG.DBTab.Expediente	expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Marca		mar	= new Berke.DG.DBTab.Marca( db );

			expe.Adapter.ReadByID( param.Dat.ExpedienteID.AsInt );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );

			param.InitAdapter( db );

			// Agregado por mbaez. 15/11/2006
			// Al pasar a vigilada se agrega propietario obligatoriamente.
			int error = this.actualizar_propietario(mar,expe,db, "VIGILAR");
			if (error == 1) 
			{
				return;
			}
			#region Asignar valores
			expe.Dat.Vigilada.Value	= true;
			mar.Dat.Vigilada.Value	= true;
			if( this.cbxCliente.SelectedValue != "" ){
				mar.Dat.ClienteID.Value = this.cbxCliente.SelectedValue;
				expe.Dat.ClienteID.Value = this.cbxCliente.SelectedValue;
			}
			if( this.ddlAgenteBerke.SelectedValue != "" )
			{	
				expe.Dat.AgenteLocalID	.Value	= this.ddlAgenteBerke.SelectedValue;
				mar.Dat.AgenteLocalID	.Value  = this.ddlAgenteBerke.SelectedValue;
			}
			#endregion Asignar valores
		
			param.Adapter.InsertRow();
			expe.Adapter.UpdateRow();
			mar.Adapter.UpdateRow();

			db.Commit();
			db.CerrarConexion();
		}
		#endregion 	Vigilar_ON

		#region 	Vigilar_OFF
		private void 	Vigilar_OFF()
		{
		
			//			Berke.DG.ViewTab.vFuncionario func = Berke.Marcas.UIProcess.Model.Funcionario.ReadByUserName( 
			Berke.DG.DBTab.Expediente_Pertenencia param = new Berke.DG.DBTab.Expediente_Pertenencia();
		
			param.NewRow();
			param.Dat.ExpedienteID			.Value	= this.ExpedienteID;
			param.Dat.Nuestra				.SetNull();
			param.Dat.Vigilada				.Value	= false;
			param.Dat.Sustituida			.SetNull();
			param.Dat.StandBy				.SetNull();
			param.Dat.PertenenciaMotivoID	.Value	= this.ddlMotivo.SelectedValue;
			param.Dat.Fecha					.Value	= DateTime.Today;
			param.Dat.FuncionarioID			.Value	= MySession.FuncionarioID;
			param.Dat.Obs					.Value	= this.txtObs.Text;
			param.Dat.AgenteLocalID			.Value	= this.AgenteLocalID;

			
		
			param.PostNewRow();

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;
			db.IniciarTransaccion();

			Berke.DG.DBTab.Expediente	expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Marca		mar	= new Berke.DG.DBTab.Marca( db );
			//Berke.DG.DBTab.Expediente_Pertenencia expePer = new Berke.DG.DBTab.Expediente_Pertenencia(db );

			expe.Adapter.ReadByID( param.Dat.ExpedienteID.AsInt );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );

			param.InitAdapter( db );

			expe.Dat.Vigilada.Value	= false;
			mar.Dat.Vigilada.Value	= false;

			param.Adapter.InsertRow();
			expe.Adapter.UpdateRow();
			mar.Adapter.UpdateRow();

			db.Commit();
			db.CerrarConexion();
		}
		#endregion 	Vigilar_OFF

		#region	Incorporar();
		private void Incorporar(){
		

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;
			db.IniciarTransaccion();

			Berke.DG.DBTab.Expediente				expe	= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Marca					mar		= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente_Pertenencia	expePer	= new Berke.DG.DBTab.Expediente_Pertenencia(db );
			Berke.DG.DBTab.Tramite					tramite = new Berke.DG.DBTab.Tramite( db );
			Berke.DG.DBTab.MarcaRegRen				regRen	= new Berke.DG.DBTab.MarcaRegRen( db );
			#region Leer datos
			expe.Adapter.ReadByID( this.ExpedienteID );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
			tramite.Adapter.ReadByID( expe.Dat.TramiteID.AsInt );
			regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.AsInt );

			#endregion Leer datos


			#region Verificar estado del Tramite (Concluido o No)
			/* [rgimenez: 22-mar-07]
			 * Solo se puede TOMAR INTERVENCION mientras el tramite no ha concluido 
			 */

			/* Esto no se puede controlar porque para los tramites varios 
			 * solo se registra la situacion Presentada
			 * 
			 * Para este control se requiere tener informacion si 
			 * el tramite concluyo o no
			 * 
			 * */

			#endregion Verificar estado del Tramite (Concluido o No)
			


			#region OT
			// Tabla : OrdenTrabajo
			int otID = 0;
			if( expe.Dat.OrdenTrabajoID.IsNull )
			{
				Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo( db );
			
				ot.NewRow(); 
				//			ot.Dat.ID				.Value = DBNull.Value;						//int PK  Oblig.
				ot.Dat.ClienteID		.Value = this.cbxCliente.SelectedValue;		//int Oblig.
				ot.Dat.FuncionarioID	.Value = MySession.FuncionarioID;			//int Oblig.
				ot.Dat.TrabajoTipoID	.Value = tramite.Dat.TrabajoTipoID.AsInt;   //int Oblig.
				ot.Dat.Nro				.Value = Calc.OrdenTrabajoNro( expe.Dat.TramiteID.AsInt );   //int Oblig.
				ot.Dat.Anio				.Value = DateTime.Today.Year;   //int Oblig.
				ot.Dat.Facturable		.Value = this.ddlFacturable.SelectedValue;   //bit Oblig.
				ot.Dat.AltaFecha		.Value = DateTime.Today;					 //smalldatetime Oblig.
				ot.Dat.Obs				.Value = this.txtObs.Text;					 //nvarchar
				//		ot.Dat.OrdenTrabajo			.Value = DBNull.Value;   //nvarchar ReadOnly

				// --
				ot.PostNewRow(); 
				otID = ot.Adapter.InsertRow(); 
			}else{
				otID = expe.Dat.OrdenTrabajoID.AsInt;
			}
			

			#endregion OT

			// Agregado por mbaez. 15/11/2006
			// Al pasar a NUESTRA se agrega propietario obligatoriamente.

			/*[rgimenez: 22-mar-07]
			 * actualizar_propietario: modificado para aisgnar Propietario o Poder
			 * cuando se pasa a TOMAR INTERVENCION (Incorporar)			
			 */
			int error = this.actualizar_propietario(mar,expe,db,"INCORPORAR");
			if (error == 1) 
			{
				return;
			}			

			

			#region Expediente_Pertenencia
			expePer.NewRow();
			expePer.Dat.ExpedienteID		.Value	= this.ExpedienteID;
			expePer.Dat.Nuestra				.Value	= true;
			expePer.Dat.Vigilada			.Value	= true;
			expePer.Dat.Sustituida			.Value	= DBNull.Value;
			expePer.Dat.StandBy				.Value	= DBNull.Value;
			expePer.Dat.PertenenciaMotivoID	.Value	= this.ddlMotivo.SelectedValue; // Intervencion Berke en REG-REN
			expePer.Dat.Fecha				.Value	= DateTime.Today;
			expePer.Dat.FuncionarioID		.Value	= MySession.FuncionarioID;
			expePer.Dat.Obs					.Value	= this.txtObs.Text;
			expePer.Dat.AgenteLocalID		.Value	= expe.Dat.AgenteLocalID.AsInt;
			expePer.PostNewRow();
			
			// --
			expePer.Adapter.InsertRow();

			#endregion Expediente_Pertenencia

			#region Expediente
			expe.Dat.AgenteLocalID	.Value	= this.ddlAgenteBerke.SelectedValue;
			expe.Dat.ClienteID		.Value	= this.cbxCliente.SelectedValue;
			expe.Dat.OrdenTrabajoID	.Value	= otID;
			expe.Dat.Nuestra		.Value	= true;
			expe.Dat.Vigilada		.Value	= true;
			
			// --
			expe.Adapter.UpdateRow();

			#endregion Expediente
			
			#region Marca

			mar.Dat.ClienteID		.Value	= this.cbxCliente.SelectedValue;
			mar.Dat.AgenteLocalID	.Value	= this.ddlAgenteBerke.SelectedValue;
			mar.Dat.Nuestra			.Value	= true;
			mar.Dat.Vigilada		.Value	= true;
			mar.Dat.Limitada		.Value	= false;

			// --
			mar.Adapter.UpdateRow();

			#endregion Marca

			#region MarcaRegRen
			
			regRen.Dat.Limitada.Value = false;

			#endregion MarcaRegRen


			db.Commit();
			db.CerrarConexion();		


		}
		#endregion Incorporar();

		#region	Traspasar();
		private void Traspasar()
		{
		

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.ServerName	= MyApplication.CurrentServerName;
			db.DataBaseName	= MyApplication.CurrentDBName;
			db.IniciarTransaccion();

			Berke.DG.DBTab.Expediente				expe	= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Marca					mar		= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente_Pertenencia	expePer	= new Berke.DG.DBTab.Expediente_Pertenencia(db );
			Berke.DG.DBTab.Tramite					tramite = new Berke.DG.DBTab.Tramite( db );
			Berke.DG.DBTab.MarcaRegRen				regRen	= new Berke.DG.DBTab.MarcaRegRen( db );

			#region Leer datos
			expe.Adapter.ReadByID( this.ExpedienteID );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
			tramite.Adapter.ReadByID( expe.Dat.TramiteID.AsInt );
			regRen.Adapter.ReadByID( expe.Dat.ExpedienteID.AsInt );

			#endregion Leer datos

			#region Expediente_Pertenencia
			expePer.NewRow();
			expePer.Dat.ExpedienteID		.Value	= this.ExpedienteID;
			expePer.Dat.Nuestra				.Value	= false;
			expePer.Dat.Vigilada			.Value	= false;
			expePer.Dat.Sustituida			.Value	= DBNull.Value;
			expePer.Dat.StandBy				.Value	= DBNull.Value;
			expePer.Dat.PertenenciaMotivoID	.Value	= this.ddlMotivo.SelectedValue;
			expePer.Dat.Fecha				.Value	= DateTime.Today;
			expePer.Dat.FuncionarioID		.Value	= MySession.FuncionarioID;
			expePer.Dat.Obs					.Value	= this.txtObs.Text;
			expePer.Dat.AgenteLocalID		.Value	= DBNull.Value;
			expePer.PostNewRow();
			
			// --
			expePer.Adapter.InsertRow();

			#endregion Expediente_Pertenencia

			#region Expediente
			// Si el registro aún no se ha concluído
			if ( regRen.Dat.RegistroNro.IsNull)
			{
				expe.Dat.AgenteLocalID	.Value	=  DBNull.Value;
				//			expe.Dat.ClienteID		.Value	= this.cbxCliente.SelectedValue;
				//			expe.Dat.OrdenTrabajoID	.Value	= otID;
				expe.Dat.Nuestra		.Value	= false;
				expe.Dat.Vigilada		.Value	= false;
			    expe.Adapter.UpdateRow();
			}
			
			// --


			#endregion Expediente
			
			#region Marca

//			mar.Dat.ClienteID		.Value	= this.cbxCliente.SelectedValue;
			mar.Dat.AgenteLocalID	.Value	=  DBNull.Value;
			mar.Dat.Nuestra			.Value	= false;
			mar.Dat.Vigilada		.Value	= false;

			// --
			mar.Adapter.UpdateRow();

			#endregion Marca

			db.Commit();
			db.CerrarConexion();		


		}
		#endregion Traspasar();


	} // End Class
}// End Namespaces
