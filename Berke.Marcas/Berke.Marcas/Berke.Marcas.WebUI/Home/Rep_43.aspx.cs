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
	using Berke.Marcas.BizActions;

	/// <summary>
	/// Summary description for Rep_43.
	/// </summary>
	public partial class Rep_43 : System.Web.UI.Page
	{
		#region Variables Globales
		string[] aOmitir = new string[]{}; // array de instrucciones a omitir
		string[] aIncluir = new string[]{}; // array de instrucciones a incluir
		#endregion Variables Globales

		#region Properties

		#endregion Properties

		#region Controles del Web Form

		#endregion Controles del Web Form

		#region Asignar Delegados
		private void AsignarDelegados()
		{
			this.cbxPropietarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxPropietarioID_LoadRequested); 
			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
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

		#region Carga de Combo

		#region Propietario  
		private void cbxPropietarioID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)  
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
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Propietario.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}  
		#endregion Propietario		
		
		#region Cliente
		private void cbxClienteID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
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
		private void AplicarFiltro( Berke.DG.ViewTab.vMarcaVencim view )
		{
			 
			//			view.Dat.Denominacion	.Filter = ObjConvert.GetFilter_Str	( txtDenominacion.Text);
			//			view.Dat.AltaFecha		.Filter = ObjConvert.GetFilter		( txtAltaFecha_min.Text);
			//			view.Dat.FuncionarioID	.Filter = ObjConvert.GetFilter		( ddlFuncionario.SelectedValue);
			//			view.Dat.Nuestra		.Filter	= ObjConvert.GetFilter_Bool	( ddlNuestra.SelectedValue);
			//		
			#region Instrucciones a Incluir

			string defaultWhere = view.Adapter.DefaultWhere;
			if( this.txtInstruc.Text != "" )
			{
				string valores = "";
				aIncluir = this.txtInstruc.Text.Split(",".ToCharArray());
				foreach( string val in aIncluir)
				{
					if( val.Trim() != "" )
					{
						valores+=(valores != "" ? "," : "" )+"'"+val+"'";
					}
				}

				if( valores != "" )	
				{
					string inSQL = " exists ( select  i.Abrev from expediente_instruccion ei join"+
						"	expediente e "+
						"	on (ei.expedienteid = e.id) "+
						"	join instrucciontipo i " +
						"	on ( ei.instrucciontipoid = i.id) "+
						"	where  "+
						"	e.id = expe.id "+
						"   and i.Abrev in ("+valores+") "+
						this.ControlarUltimaInstruccion() +")";
						//"   and i.Abrev in ("+valores+"))";

					if( this.chkIncluirNulos.Checked )
					{
						//defaultWhere+= " AND ( dbo.InstruccionTipo.Abrev IN ( "+ valores + " ) OR dbo.InstruccionTipo.Abrev is null ) ";
						defaultWhere+= " AND ( "+ inSQL +"  )";

					}
					else
					{
						//defaultWhere+= " AND ( dbo.InstruccionTipo.Abrev IN ( "+ valores + " ) ) ";
						defaultWhere+= " AND ( "+ inSQL +" ) AND itipo.Abrev is not null ";

					}
					view.Adapter.SetDefaultWhere( defaultWhere );
				}
			}
			#endregion Instrucciones a Incluir

			#region Instrucciones a omitir
			defaultWhere = view.Adapter.DefaultWhere;
			if( this.txtOmitir.Text != "" )
			{
				string valores = "";
				aOmitir = this.txtOmitir.Text.Split(",".ToCharArray());
				foreach( string val in aOmitir)
				{
					if( val.Trim() != "" )
					{
						valores+=(valores != "" ? "," : "" )+"'"+val+"'";
					}
				}
				if( valores != "" )
				{

					string notInSQL = " not exists ( select  i.Abrev from expediente_instruccion ei join"+
						"	expediente e "+
						"	on (ei.expedienteid = e.id) "+
						"	join instrucciontipo i " +
						"	on ( ei.instrucciontipoid = i.id) "+
						"	where  "+
						"	e.id = expe.id "+
						"   and i.Abrev in ("+valores+") "+
						this.ControlarUltimaInstruccion() +")";
						//"   and i.Abrev in ("+valores+"))";

					if( this.chkIncluirNulos.Checked )
					{
						// Se corrigió problema de Registros que aparecían con instrucciones omitidas.
						//defaultWhere+= " AND ( dbo.InstruccionTipo.Abrev NOT IN ( "+ valores + " ) OR dbo.InstruccionTipo.Abrev is null )";
						defaultWhere+= " AND ( "+ notInSQL +"  )";
					}
					else
					{
						// REVISAR. No se deben incluir los que no tienen instrucciones.
						//defaultWhere+= " AND dbo.InstruccionTipo.Abrev NOT IN ( "+ valores + " ) ";
						defaultWhere+= " AND ( "+ notInSQL +" ) AND itipo.Abrev is not null ";
					}

					view.Adapter.SetDefaultWhere( defaultWhere );
				}
			}
			#endregion Instrucciones a omitir
			ArrayList lst = new ArrayList();
			lst.Add(1);
			lst.Add(2);
			view.Dat.TramiteID.Filter = new DSFilter(lst);

			#region Propietario
			if( txtPropietarioID.Text.Trim() != "" ){
				view.Dat.PropietarioID	.Filter = ObjConvert.GetFilter( txtPropietarioID.Text.Trim()) ;
			}else{
				view.Dat.PropietarioID.Filter = ObjConvert.GetFilter( this.cbxPropietarioID.SelectedValue );
			}
			#endregion Propietario

			#region Cliente
			if( txtClienteID.Text.Trim() != "" ){
				view.Dat.ClienteID		.Filter = ObjConvert.GetFilter( txtClienteID.Text.Trim() );
			}else{
				view.Dat.ClienteID.Filter	= ObjConvert.GetFilter( this.cbxClienteID.SelectedValue );
			}
			#endregion Cliente

			view.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter( this.txtVencim.Text );
		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vMarcaVencim view)
		{
			view.ClearOrder();
			view.Dat.ClienteID.Order	= 1;
			view.Dat.PropietarioID.Order	= 2;
			view.Dat.VencimientoFecha.Order = 3;
		}
		#endregion SetOrder

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vMarcaVencim view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			
			mensajeError = "";
			int recuperados = -1;
			try 
			{

				//				ArrayList aID = view.Adapter.GetListOfField( view.Dat.ExpeID, true );
				//				recuperados = aID.Count; // view.Adapter.Count();
				recuperados = view.Adapter.Count();
				if( recuperados != 0 )
				{
//					string comando = view.Adapter.ReadAll_CommandString();
					if( recuperados < limite )
					{
						view.Adapter.ReadAll( limite );
						
						#region Eliminar los repetidos -  Deprecated. Se hace corte de control
						/*
						int antID = -321;
						for( view.GoTop(); ! view.EOF ; view.Skip())
						{
							if( view.Dat.ExpeID.AsInt == antID )
							{
								view.Delete();
							}
							else
							{
								antID = view.Dat.ExpeID.AsInt;
							}
						}
						view.AcceptAllChanges();
						*/
						#endregion Eliminar los repetidos
						

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

			Berke.DG.ViewTab.vMarcaVencim view = new Berke.DG.ViewTab.vMarcaVencim( db );
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
		private void GenerarDocumento( Berke.DG.ViewTab.vMarcaVencim view )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= view.Adapter.Db;

			int idiomaID =  (int)GlobalConst.Idioma.ESPANOL;

			#region Leer Plantilla

			string template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_43_M", idiomaID );
		
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );
			
			CodeGenerator tabCli	= cg.ExtraerTabla("tabCli" );
//			CodeGenerator fila1		= tabCli.ExtraerFila("fila1" );
//			CodeGenerator fila2		= tabCli.ExtraerFila("fila2" );
			CodeGenerator tabTit		= tabCli.ExtraerTabla("tabTit" );

			CodeGenerator tabProp		= tabCli.ExtraerTabla("tabProp" );
		
			CodeGenerator filaTit		= tabProp.ExtraerFila("filaTit",2 );
			CodeGenerator fila			= tabProp.ExtraerFila("fila" );
			//			CodeGenerator encabezado= cg.ExtraerTabla("encabezado" );//Encabezado Ultimo
			#endregion Obtener "Generators"
			
			#region vars de acceso a datos
			/*
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.MarcaTipo marTipo = new Berke.DG.DBTab.MarcaTipo( db );
			Berke.DG.DBTab.Expediente expe	= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Marca_Poderdante marPdd= new Berke.DG.DBTab.Marca_Poderdante( db );
			Berke.DG.DBTab.Poderdante pdd = new Berke.DG.DBTab.Poderdante( db );
			Berke.DG.DBTab.CPropietario prop = new Berke.DG.DBTab.CPropietario( db );
			Berke.DG.DBTab.CCliente cli = new Berke.DG.DBTab.CCliente( db );
			Berke.DG.DBTab.CEntidad ent = new Berke.DG.DBTab.CEntidad( db );
			Berke.DG.DBTab.CIdioma idioma = new Berke.DG.DBTab.CIdioma( db );
			Berke.DG.DBTab.MarcaRegRen regRen = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase( db );
			Berke.DG.DBTab.Expediente_Instruccion expeIns = new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.InstruccionTipo inst = new Berke.DG.DBTab.InstruccionTipo( db );
			*/

			Berke.DG.DBTab.Marca			mar = new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.MarcaTipo		marTipo = new Berke.DG.DBTab.MarcaTipo( db );
			Berke.DG.DBTab.Expediente		expe	= new Berke.DG.DBTab.Expediente( db );
			//Berke.DG.DBTab.Marca_Poderdante marPdd= new Berke.DG.DBTab.Marca_Poderdante( db );
			//Berke.DG.DBTab.Poderdante		pdd = new Berke.DG.DBTab.Poderdante( db );

			Berke.DG.DBTab.Propietario		prop = new Berke.DG.DBTab.Propietario( db );
			Berke.DG.DBTab.Cliente			cli = new Berke.DG.DBTab.Cliente( db );

			//Berke.DG.DBTab.CEntidad			ent = new Berke.DG.DBTab.CEntidad( db );
			Berke.DG.DBTab.CIdioma			idioma = new Berke.DG.DBTab.CIdioma( db );
			Berke.DG.DBTab.MarcaRegRen		regRen = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.Clase			clase = new Berke.DG.DBTab.Clase( db );
			Berke.DG.DBTab.Expediente_Instruccion	expeIns = new Berke.DG.DBTab.Expediente_Instruccion( db );
			Berke.DG.DBTab.InstruccionTipo			inst = new Berke.DG.DBTab.InstruccionTipo( db );
			Berke.DG.DBTab.Correspondencia			corresp = new Berke.DG.DBTab.Correspondencia( db );

			//rgimenez: incluyo Atencion para reemplazar la vista que recupera la observacion
			Berke.DG.DBTab.Atencion		atencion = new Berke.DG.DBTab.Atencion( db );


			#endregion vars de acceso a datos

			
			#region Eliminar los que no corresponden - Deprecated. Se corrigió la consulta
			/* Mbaez. Se corrigió en la consulta.
			for(  view.GoTop(); ! view.EOF; view.Skip() )
			{
				#region Instrucciones
				bool omitir = false;
				expeIns.Dat.ExpedienteID.Filter = view.Dat.ExpeID.AsInt;
				ArrayList aInsID = expeIns.Adapter.GetListOfField( expeIns.Dat.InstruccionTipoID, true );
				if( aInsID.Count > 0 )
				{
					inst.Dat.ID.Filter = new DSFilter( aInsID );
					ArrayList aInsAbrev = inst.Adapter.GetListOfField( inst.Dat.Abrev );
					#region Verificar Instrucciones a Omitir
					foreach( string val in aInsAbrev )
					{
						foreach( string omit in aOmitir )
						{
							if( val == omit )
							{
								omitir = true;
							}
						}
					}
					#endregion Verificar Instrucciones a Omitir
				}
				if( omitir )
				{
					view.Delete();
				}
				#endregion Instrucciones
			}
			view.AcceptAllChanges();
			*/
			#endregion Eliminar los que no corresponden			

			cg.clearText();
			cg.copyTemplateToBuffer();
	
			tabCli.clearText();
			view.GoTop();

			string propName = "";
			string propDir = "";
	
			#region Leer

			expe.Adapter.ReadByID( view.Dat.ExpeID.Value );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.Value );
			marTipo.Adapter.ReadByID( mar.Dat.MarcaTipoID.Value );
			clase.Adapter.ReadByID( mar.Dat.ClaseID.Value );
			cli.Adapter.ReadByID( mar.Dat.ClienteID.Value );
			idioma.Adapter.ReadByID( cli.Dat.IdiomaID.Value );
			regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );

			
			
			#region Propietarios
			/*
			marPdd.Dat.MarcaID.Filter = mar.Dat.ID.AsInt;
			ArrayList aPpdID = marPdd.Adapter.GetListOfField( marPdd.Dat.PoderdanteID );
			if( aPpdID.Count > 0 )
			{
				pdd.Dat.ID.Filter = new DSFilter( aPpdID );
				ArrayList aPropID = pdd.Adapter.GetListOfField( pdd.Dat.PropietarioID );
				string [] id = (string [])aPropID.ToArray(typeof(string));

				prop.Dat.idprop.Filter =  new DSFilter( aPropID );
				ArrayList aPropName = prop.Adapter.GetListOfField( prop.Dat.Nombre , true );
				string [] a = (string [])aPropName.ToArray(typeof(string));

				ArrayList aPropDir = prop.Adapter.GetListOfField( prop.Dat.Direccion , true );
				string [] b = (string [])aPropDir.ToArray(typeof(string));

				propName = string.Join("/", id )+"."+string.Join("/", a );
				propDir  = string.Join("/", b );
			}
			*/

			propName = mar.Dat.Propietario.AsString;
			propDir  = mar.Dat.ProDir.AsString;
			//propDir  = "AQUI VA LA DIRECCION";
			

			#endregion Propietarios

			#endregion Leer	
			int ExpedienteID = -1;
			while(  ! view.EOF )
			{	
				int antCli = view.Dat.ClienteID.AsInt;
				idiomaID = idioma.Dat.ididioma.AsInt;
				tabCli.copyTemplateToBuffer();

				tabTit.clearText();
				tabTit.copyTemplateToBuffer();	
				tabTit.replaceField("IDCliente", antCli.ToString());
				string fecha = Lib.DateString( DateTime.Today, idiomaID );
				tabTit.replaceField("Hoy", fecha );

				tabProp.clearText();

				#region while Cliente
				while(  ! view.EOF && antCli == view.Dat.ClienteID.AsInt )
				{				
					int antProp = view.Dat.PropietarioID.AsInt;
					tabProp.copyTemplateToBuffer();
				
					filaTit.clearText();
					filaTit.copyTemplateToBuffer();
					filaTit.replaceField("Propietario", propName );
					filaTit.replaceField("Direccion", propDir );
					filaTit.addBufferToText();
					fila.clearText();
					#region while Propietario
					while(  ! view.EOF && antCli == view.Dat.ClienteID.AsInt && antProp == view.Dat.PropietarioID.AsInt )
					{					
						#region AsignarFilas
						fila.copyTemplateToBuffer();
	
						fila.replaceField("Denominacion", mar.Dat.Denominacion.AsString );
						fila.replaceField("T", marTipo.Dat.Abrev.AsString );
						fila.replaceField("Registro", regRen.Dat.RegistroNro.AsString );
						fila.replaceField("Clase", clase.Dat.Nro.AsString );
						if( !regRen.Dat.VencimientoFecha.IsNull ){
							fila.replaceField("Vencimiento", Lib.DateString( regRen.Dat.VencimientoFecha.AsDateTime, idiomaID ));
						}else{
							fila.replaceField("Vencimiento", "" );
						}

						fila.addBufferToText();
						#endregion AsignarFilas
						do 
						{
							view.Skip();
						}while( view.Dat.ExpeID.AsInt == ExpedienteID);

						#region Leer
						/*
						expe.Adapter.ReadByID( view.Dat.ExpeID.Value );
						mar.Adapter.ReadByID( expe.Dat.MarcaID.Value );
						marTipo.Adapter.ReadByID( mar.Dat.MarcaTipoID.Value );
						clase.Adapter.ReadByID( mar.Dat.ClaseID.Value );
						cli.Adapter.ReadByID( mar.Dat.ClienteID.Value );
						ent.Adapter.ReadByID( cli.Dat.identidad.Value );
						idioma.Adapter.ReadByID( ent.Dat.ididioma.Value );
						regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );
						*/
						expe.Adapter.ReadByID( view.Dat.ExpeID.Value );
						mar.Adapter.ReadByID( expe.Dat.MarcaID.Value );
						marTipo.Adapter.ReadByID( mar.Dat.MarcaTipoID.Value );
						clase.Adapter.ReadByID( mar.Dat.ClaseID.Value );
						cli.Adapter.ReadByID( mar.Dat.ClienteID.Value );
						idioma.Adapter.ReadByID( cli.Dat.IdiomaID.Value );
						regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );


						#region Propietarios
						
						propName = mar.Dat.Propietario.AsString;
						propDir  = mar.Dat.ProDir.AsString;
						//propDir  = "AQUI VA LA DIRECCION";
						/*
						marPdd.Dat.MarcaID.Filter = mar.Dat.ID.AsInt;
						aPpdID = marPdd.Adapter.GetListOfField( marPdd.Dat.PoderdanteID );
						if( aPpdID.Count > 0 )
						{
							pdd.Dat.ID.Filter = new DSFilter( aPpdID );
							ArrayList aPropID = pdd.Adapter.GetListOfField( pdd.Dat.PropietarioID );

							prop.Dat.idprop.Filter =  new DSFilter( aPropID );
							ArrayList aPropName = prop.Adapter.GetListOfField( prop.Dat.Nombre , true );
							string [] a = (string [])aPropName.ToArray(typeof(string));

							ArrayList aPropDir = prop.Adapter.GetListOfField( prop.Dat.Direccion , true );
							string [] b = (string [])aPropDir.ToArray(typeof(string));

							propName = string.Join("/", a );
							propDir  = string.Join("/", b );
						}
						*/
						#endregion Propietarios

						#endregion Leer	

					}// end while Propietario
					#endregion while Propietario
					tabProp.replaceLabel("filaTit", filaTit.Texto );
					tabProp.replaceLabel("fila", fila.Texto );
					tabProp.addBufferToText();

				}// end while cliente
				#endregion while Cliente

				tabTit.addBufferToText();
				tabCli.replaceLabel("tabTit", tabTit.Texto );
				tabCli.replaceLabel("tabProp", tabProp.Texto );

				#region Constantes a traducir
				/*
				tabCli.replaceField("Pagina",			Lib.traducir("Pagina", idiomaID, db ) );
				tabCli.replaceField("POBox",			Lib.traducir("Casilla de Correo", idiomaID, db ) );
				tabCli.replaceField("Titulo", Lib.traducir("Marcas a Renovar", idiomaID, db ).ToUpper() );
				tabCli.replaceField("tPROPIETARIO",		Lib.traducir("Propietario", idiomaID, db ) );
				tabCli.replaceField("tDIRECCION",		Lib.traducir("Direccion", idiomaID, db ) );
				tabCli.replaceField("tMarca",			Lib.traducir("Marca", idiomaID, db ) );
				tabCli.replaceField("tTipo",				Lib.traducir("Tipo (D/F/M)", idiomaID, db ) );
				tabCli.replaceField("tRegistro",		Lib.traducir("Registro Nro.", idiomaID, db ) );
				tabCli.replaceField("tClase",			Lib.traducir("Clase Int.", idiomaID, db ) );
				tabCli.replaceField("tVencim",			Lib.traducir("Vencim. (d/m/a)", idiomaID, db ) );
				*/

				tabCli.replaceField("Pagina","Pagina");
				tabCli.replaceField("POBox", "Casilla de Correo");
				tabCli.replaceField("Titulo","Marcas a Renovar");
				tabCli.replaceField("tPROPIETARIO", "Propietario" );
				tabCli.replaceField("tDIRECCION",	"Direccion" );
				tabCli.replaceField("tMarca",		"Marca" );
				tabCli.replaceField("tTipo",		"Tipo (D/F/M)" );
				tabCli.replaceField("tRegistro",	"Registro Nro." );
				tabCli.replaceField("tClase",		"Clase Int." );
				tabCli.replaceField("tVencim",		"Vencim. (d/m/a)" );

				
				#endregion Constantes a traducir



				tabCli.addBufferToText();
				if( ! view.EOF ){
					tabCli.addNewPageToText();
				}
			} // end for EOF

			cg.replaceLabel( "tabTit", tabTit.Texto );

			cg.replaceLabel( "tabCli", tabCli.Texto );
			cg.replaceField("Periodo", this.txtVencim.Text);

		
			cg.addBufferToText();

			/*	
			#region Activar WORD
			string carpeta = @"K:\Cache\Reportes\";
			 carpeta = @"\\Trinity\Siberk\Dev\BERKE.MARCA\Code\Berke.Marcas\Berke.Marcas.WebUI\Reports\";
			
			string archivo = @"rep_43";
			string ext		= ".doc";
			int version = 0;
			string path = carpeta+ archivo + "_v"+version.ToString()+ext;
			Berke.Libs.Base.Helpers.Files.SaveStringToFile(cg.Texto, path);
			lnkDocum.NavigateUrl = path;
			lnkDocum.Text = "Ver Documento";
			#endregion Activar WORD
			*/

			string buffer = cg.Texto;

			#region Activar MS-Word
			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-word";
			Response.AddHeader("Content-Disposition", "attachment;filename=rep_43.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
			#endregion Activar MS-Word

		}
		#endregion GenerarDocumento

		#region Agregar control de última instruccion
		/*ggaleano [10/04/2007] 
		 * Si se desea obtener el listado teniendo en
		 * cuenta la última fecha instrucción se agrega
		 * esta cadena SQL*/

		public string ControlarUltimaInstruccion()
		{
			string StrSQL = "";
			if (chkUltimaInstruccion.Checked)
			{
				StrSQL =	"   and ei.fecha = ( select MAX(ei1.fecha) " +
					"                    from expediente_instruccion ei1 " +
					"                    where ei1.expedienteid = e.id )";
				
			}
			return StrSQL;
		}

	}
	#endregion Agregar control de última instrucción



}
