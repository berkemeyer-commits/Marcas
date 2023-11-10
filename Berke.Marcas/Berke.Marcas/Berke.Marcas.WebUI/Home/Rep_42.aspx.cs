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

	/// <summary>
	/// Summary description for Rep_42.
	/// </summary>
	public partial class Rep_42 : System.Web.UI.Page
	{
		#region Variables Globales
		string[] aOmitir = new string[]{}; // array de instrucciones a omitir
		string[] aIncluir = new string[]{}; // array de instrucciones a Incluir
		//int RENOVACION = 2;
		int cantidad_registros = 0;

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
		private void AplicarFiltro( Berke.DG.ViewTab.vMarcaVencim1 view )
		{

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
						/*this.ControlarUltimaInstruccion() +*/")";

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
						/*this.ControlarUltimaInstruccion() +*/")";

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
			if( txtPropietarioID.Text.Trim() != "" )
			{
				view.Dat.PropietarioID	.Filter = ObjConvert.GetFilter( txtPropietarioID.Text.Trim()) ;
			}
			else
			{
				view.Dat.PropietarioID.Filter = ObjConvert.GetFilter( this.cbxPropietarioID.SelectedValue );
			}
			#endregion Propietario

			#region Cliente
			if( txtClienteID.Text.Trim() != "" )
			{
				view.Dat.ClienteID		.Filter = ObjConvert.GetFilter( txtClienteID.Text.Trim() );
			}
			else
			{
				view.Dat.ClienteID.Filter	= ObjConvert.GetFilter( this.cbxClienteID.SelectedValue );
			}
			#endregion Cliente
			view.Dat.VencimientoFecha.Filter = ObjConvert.GetFilter( this.txtVencim.Text );

		}
		#endregion AplicarFiltro

		#region SetOrder
		private void SetOrder(  Berke.DG.ViewTab.vMarcaVencim1 view)
		{
			view.ClearOrder();
			view.Dat.VencimientoFecha.Order	= 1;
			view.Dat.RegistroNro.Order	= 2;
			view.Dat.ExpeID.Order = 3;
		}
		#endregion SetOrder

		#region ObtenerDatosOk
		private bool ObtenerDatosOk(  Berke.DG.ViewTab.vMarcaVencim1 view, int limite, out string mensajeError , Berke.Libs.Base.Helpers.AccesoDB db )
		{
			bool resultado = false;
			
			mensajeError = "";
			int recuperados = -1;
			try 
			{

				//				ArrayList aID = view.Adapter.GetListOfField( view.Dat.ExpeID, true );
				//				recuperados = aID.Count; // view.Adapter.Count();
				recuperados = view.Adapter.Count();
				cantidad_registros = recuperados;
				if( recuperados != 0 )
				{
					if( recuperados < limite )
					{
						string comando = view.Adapter.ReadAll_CommandString();

						view.Adapter.ReadAll( limite );

						cantidad_registros =  recuperados - ControlarUltimaInstruccion(view, view.Adapter.Db);
						/*if (chkUltimaInstruccion.Checked)
						{
							cantidad_registros =  recuperados - ControlarUltimaInstruccion(view, view.Adapter.Db);
						}*/

						/* mbaez. No hace falta. Se hace corte de control por instrucción.
						#region Eliminar los repetidos
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
						#endregion Eliminar los repetidos
						*/
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

			Berke.DG.ViewTab.vMarcaVencim1 view = new Berke.DG.ViewTab.vMarcaVencim1( db );
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
		private void GenerarDocumento( Berke.DG.ViewTab.vMarcaVencim1 view )
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= view.Adapter.Db;

			int idiomaID =  (int)GlobalConst.Idioma.ESPANOL ;

			#region Leer Plantilla

			string template = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("REP_42", idiomaID );
		
			#endregion Leer Plantilla

			#region Obtener "Generators"
			CodeGenerator cg = new Berke.Libs.CodeGenerator( template );

			CodeGenerator tabla		= cg.ExtraerTabla("tabla" );
		    
			CodeGenerator filaTit		= tabla.ExtraerFila("filaTit" );
			CodeGenerator fila			= tabla.ExtraerFila("fila" );
			//			CodeGenerator encabezado= cg.ExtraerTabla("encabezado" );//Encabezado Ultimo
			#endregion Obtener "Generators"
			
			/*
					Berke.DG.DBTab.Marca			mar = new Berke.DG.DBTab.Marca( db );
					Berke.DG.DBTab.MarcaTipo		marTipo = new Berke.DG.DBTab.MarcaTipo( db );
					Berke.DG.DBTab.Expediente		expe	= new Berke.DG.DBTab.Expediente( db );
					Berke.DG.DBTab.Marca_Poderdante marPdd= new Berke.DG.DBTab.Marca_Poderdante( db );
					Berke.DG.DBTab.Poderdante		pdd = new Berke.DG.DBTab.Poderdante( db );
					Berke.DG.DBTab.CPropietario		prop = new Berke.DG.DBTab.CPropietario( db );
					Berke.DG.DBTab.CCliente			cli = new Berke.DG.DBTab.CCliente( db );
					Berke.DG.DBTab.CEntidad			ent = new Berke.DG.DBTab.CEntidad( db );
					Berke.DG.DBTab.CIdioma			idioma = new Berke.DG.DBTab.CIdioma( db );
					Berke.DG.DBTab.MarcaRegRen		regRen = new Berke.DG.DBTab.MarcaRegRen( db );
					Berke.DG.DBTab.Clase			clase = new Berke.DG.DBTab.Clase( db );
					Berke.DG.DBTab.Expediente_Instruccion	expeIns = new Berke.DG.DBTab.Expediente_Instruccion( db );
					Berke.DG.DBTab.InstruccionTipo			inst = new Berke.DG.DBTab.InstruccionTipo( db );
					Berke.DG.DBTab.Correspondencia			corresp = new Berke.DG.DBTab.Correspondencia( db );
					Berke.DG.ViewTab.vOfiCliObservacion		ofiCliObs = new Berke.DG.ViewTab.vOfiCliObservacion( db );
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

			//rgimenez: incluyo ClienteObs para reemplazar la vista que recupera la observacion
			Berke.DG.DBTab.ClienteObs	clienteObs = new Berke.DG.DBTab.ClienteObs( db );

			//ggaleano: se incluye para obtener PropietarioID a través de la MarcaID
			Berke.DG.DBTab.PropietarioXMarca PropietarioXMarca = new Berke.DG.DBTab.PropietarioXMarca(db);

			//Berke.DG.ViewTab.vOfiCliObservacion		ofiCliObs = new Berke.DG.ViewTab.vOfiCliObservacion( db );

			cg.clearText();
			cg.copyTemplateToBuffer();
			tabla.clearText();
			tabla.copyTemplateToBuffer();
			filaTit.clearText();
			filaTit.copyTemplateToBuffer();
			filaTit.addBufferToText();

			fila.clearText();
			/*
					#region Eliminar los que no corresponden
					for(  view.GoTop(); ! view.EOF; view.Skip() ){
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
								foreach( string omit in aOmitir ){
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
					#endregion Eliminar los que no corresponden
					*/
			/*int ExpedienteID = -1;
			string instruc;*/
			for(  view.GoTop(); ! view.EOF; view.Skip())
			{
				if (!view.IsRowDeleted)
				{
					expe.Adapter.ReadByID( view.Dat.ExpeID.Value );
					mar.Adapter.ReadByID( expe.Dat.MarcaID.Value );
					marTipo.Adapter.ReadByID( mar.Dat.MarcaTipoID.Value );
					clase.Adapter.ReadByID( mar.Dat.ClaseID.Value );
					cli.Adapter.ReadByID( mar.Dat.ClienteID.Value );
					idioma.Adapter.ReadByID( cli.Dat.IdiomaID.Value );
					regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );
					PropietarioXMarca.Dat.MarcaID.Filter = mar.Dat.ID.AsInt;
					PropietarioXMarca.Adapter.ReadAll();

					#region Observaciones del Oficial del cliente
					//aqui modificar para traer la observacion de la atencion

					string ofiCliObservacion = "";
					clienteObs.Dat.ClienteID.Filter = mar.Dat.ClienteID.AsInt;
					clienteObs.Dat.AreaID.Filter    =  (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION;

					clienteObs.Adapter.ReadAll();
					for( clienteObs.GoTop(); ! clienteObs.EOF; clienteObs.Skip() )
					{
						ofiCliObservacion+= " *Obs: "+ clienteObs.Dat.Obs.AsString + 
							"("+clienteObs.Dat.AreaID.AsString+")";
					}

					/*
								ofiCliObs.Dat.idcli.Filter = mar.Dat.ClienteID.AsInt;
								ofiCliObs.Adapter.ReadAll();
								for( ofiCliObs.GoTop(); ! ofiCliObs.EOF; ofiCliObs.Skip() ){
									ofiCliObservacion+= " *Obs: "+ ofiCliObs.Dat.obs.AsString + 
										"("+ofiCliObs.Dat.FuncionarioNick.AsString+")";
								}
								*/
					#endregion 
					
					#region Propietarios

					/* rgimenez: ojo
								* reemplazar esta parte de tal forma que recupere el nombre del propietario 
								* esto esta en marca.propietario
								* 
								* */
					string propName = "";
					string PropietarioID = "";
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


									propName = string.Join("/", id )+"."+string.Join("/", a );
									*/

					propName = mar.Dat.Propietario.AsString;
					//ggaleano
					PropietarioID = PropietarioXMarca.Dat.PropietarioID.AsString;
					    
		
					//}
					#endregion Propietarios


					/*
					#region Instrucciones
								bool omitir = false;
								expeIns.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
								ArrayList aInsID = expeIns.Adapter.GetListOfField( expeIns.Dat.InstruccionTipoID, true );
								string instruc = "";
								if( aInsID.Count > 0 )
								{
					#region Verificar si no esta en la lista de Instucciones a omitir
									ArrayList aInsAbrev = inst.Adapter.GetListOfField( inst.Dat.Abrev );
									foreach( string val in aInsAbrev ){
										foreach( string omit in aOmitir ){
											if( val == omit ){
												omitir = true;
											}
										}
									}
				//					instruc = string.Join(",", (string [])aInsAbrev.ToArray(typeof(string)) );
					#endregion Verificar si no esta....
									if( !omitir ){
										expeIns.Adapter.ReadAll();
										for( expeIns.GoTop(); ! expeIns.EOF; expeIns.Skip() )
										{
											inst.Adapter.ReadByID( expeIns.Dat.InstruccionTipoID.Value );
											corresp.Adapter.ReadByID( expeIns.Dat.CorrespondenciaID.Value );
											string cad = inst.Dat.Abrev.AsString;
											if( ! corresp.Dat.Nro.IsNull ){
												cad+= " ("+corresp.Dat.Nro.AsString +"/"+ corresp.Dat.Anio.AsString+") ";
											}
											instruc+= (instruc=="" ? "" : ",") + cad;
										}
									}
								}
								if( omitir )continue;
					#endregion Instrucciones
								*/

					/*#region Corte de control para Instrucciones
					instruc = "";
					do 
					{
						instruc+= (instruc=="" ? "" : ",") + view.Dat.Abrev.AsString;
						ExpedienteID = view.Dat.ExpeID.AsInt;
						view.Skip();
					}while (view.Dat.ExpeID.AsInt == ExpedienteID);
					#endregion Corte de control para Instrucciones*/

					fila.copyTemplateToBuffer();
			
					fila.replaceField("Vencim", regRen.Dat.VencimientoFecha.AsString);
					fila.replaceField("Registro", regRen.Dat.RegistroNro.AsString );
					fila.replaceField("Cl", clase.Dat.Nro.AsString );
					fila.replaceField("T", marTipo.Dat.Abrev.AsString );
					fila.replaceField("Denominacion", mar.Dat.Denominacion.AsString );
					fila.replaceField("Bib-Ex", expe.Dat.Bib.AsString+"/"+expe.Dat.Exp.AsString );
					//fila.replaceField("Inst", instruc );
					fila.replaceField("Inst", view.Dat.InstrCadena.AsString );
					
					fila.replaceField("Codcliente", cli.Dat.ID.AsString);
					fila.replaceField("Cliente", cli.Dat.Nombre.AsString + ofiCliObservacion);
					//fila.replaceField("Cliente", cli.Dat.ID.AsString +"."+cli.Dat.Nombre.AsString + ofiCliObservacion);
					fila.replaceField("Codpropietario", PropietarioID);
					fila.replaceField("Propietario", propName );
					fila.replaceField("Idioma", idioma.Dat.abrev.AsString );

					fila.addBufferToText();
				}
			} // end for EOF		

		    
			tabla.replaceLabel("fila", fila.Texto );
			tabla.replaceLabel("filaTit", filaTit.Texto );
			tabla.addBufferToText();
			cg.replaceLabel( "tabla", tabla.Texto );
			cg.replaceField("Hoy", string.Format("{0:d}", DateTime.Today));
			cg.replaceField("Periodo", this.txtVencim.Text);
			//cg.replaceField("CantReg",view.RowCount.ToString()); 
			cg.replaceField("CantReg", cantidad_registros.ToString());
			cg.addBufferToText();
				
			//#region Activar WORD
			
			//string carpeta = @"K:\Cache\Reportes\";
			//carpeta = @"\\Trinity\Siberk\Dev\BERKE.MARCA\Code\Berke.Marcas\Berke.Marcas.WebUI\Reports\";
			
			/*
					string carpeta = @"c:\Temp\";
					string archivo = @"rep_42";
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
			Response.AddHeader("Content-Disposition", "attachment;filename=rep_42.doc" );
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			Response.Write(buffer); //Llamada al procedimiento HTML
			Response.End();
			#endregion Activar MS-Word


		}
		#endregion GenerarDocumento

		#region Agregar control de última instruccion

		private int ControlarUltimaInstruccion(Berke.DG.ViewTab.vMarcaVencim1 view, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.InstruccionTipo InstruccionTipo = new Berke.DG.DBTab.InstruccionTipo(db);
			bool orden_no_renovar = false;
			int cantidad_eliminados = 0;
			for (view.GoTop(); !view.EOF; view.Skip())
			{
				string[] instrucciones = view.Dat.InstrCadena.AsString.Split(',');

				foreach (string s in instrucciones)
				{
					InstruccionTipo.ClearFilter();
					InstruccionTipo.Dat.ID.Filter = Convert.ToInt32(Berke.Libs.Base.GlobalConst.InstruccionTipo.NO_RENOVAR);
					InstruccionTipo.Adapter.ReadAll();
					/* [ggaleano 06/08/2007] Revisamos que la cadena de instrucciones contenga una orden de NO RENOVAR*/
					if (InstruccionTipo.Dat.Abrev.AsString == s)
					{
						orden_no_renovar = true;
					}
				}

				InstruccionTipo.ClearFilter();
				InstruccionTipo.Dat.ID.Filter = Convert.ToInt32(Berke.Libs.Base.GlobalConst.InstruccionTipo.ACUSE);
				InstruccionTipo.Adapter.ReadAll();
				string AbrevAcuse = InstruccionTipo.Dat.Abrev.AsString;
					
				InstruccionTipo.ClearFilter();
				InstruccionTipo.Dat.ID.Filter = Convert.ToInt32(Berke.Libs.Base.GlobalConst.InstruccionTipo.SOLICITARON_COTIZACION);
				InstruccionTipo.Adapter.ReadAll();
				string AbrevCotizacion = InstruccionTipo.Dat.Abrev.AsString;

				/* [ggaleano 06/08/2007] Si existe una orden de renovar y no está seguida de un Acuse o una Cotización
				 * entonces no se muestra en la lista */
				if ((orden_no_renovar) && ((AbrevAcuse != instrucciones[0]) || (AbrevCotizacion != instrucciones[0])))
				{
					view.Delete();
					cantidad_eliminados++;
				}
				
			}
		    
			return cantidad_eliminados;

			/*string StrSQL = "";
			if (chkUltimaInstruccion.Checked)
			{
				StrSQL =	"   and ei.fecha = ( select MAX(ei1.fecha) " +
					"                    from expediente_instruccion ei1 " +
					"                    where ei1.expedienteid = e.id )";
				
			}
			return StrSQL;*/
		}

	}
	#endregion Agregar control de última instrucción

	

		

	
}
