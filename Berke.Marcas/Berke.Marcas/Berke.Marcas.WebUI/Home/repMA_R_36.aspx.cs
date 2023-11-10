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
	using Berke.Libs.Base;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.Base.DSHelpers;
	/// <summary>
	/// Summary description for repMA_R_36.
	/// </summary>
	public partial class repMA_R_36 : System.Web.UI.Page
	{
		#region Controles del Form
		#endregion Controles del Form
		
		#region Asignar Delegados
		//Delegados: son como punteros o atributos, lo cual permite a�adir metainformaci�n al c�digo. 
		//Los delegados tienen como objetivo almacenar referencias a m�todos de otras clases, de tal forma que,
		//al ser invocados, ejecutan todos estos m�todos almacenados de forma secuencial. 
		
		private void AsignarDelegados()
		{

			this.cbxPropietarioID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxPropietarioID_LoadRequested); 
			this.cbxClienteID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxClienteID_LoadRequested); 
		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			//	cbxClienteID.SetInitialValue( 591 );
			//			#region Tramite DropDown
			//			
			//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( 1 );// 1 = Proceso de Marcas
			//			ddlTramiteID.Fill( se.Tables[0], true);	
			//
			//			#endregion Tramite DropDown


		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
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
		
//		#region Agente
//		private void cbxAgenteID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
//		{
//			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();
//			inTB.NewRow(); 
//			if( combo.SelectedKeyValue == "ID" )	
//			{
//				inTB.Dat.Entero .Value = combo.Text;   //Int32
//			}
//			else
//			{
//				inTB.Dat.Alfa	.Value = combo.Text;   //String	
//			}
//			inTB.PostNewRow(); 				
//			
//			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model.AgenteLocal.ReadForSelect(inTB );
//			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
//		}
//		#endregion Agente		


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
			string buffer = "";
			if( ParametrosOk() )
			{
				AccesoDB db		= new AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
	
				Berke.DG.ViewTab.vExpeMarca vExpeMarca = new Berke.DG.ViewTab.vExpeMarca( db );
			
				#region Filtro
				ArrayList lst = new ArrayList();
				lst.Add( (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ); // REG
				lst.Add( (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION ); // REN
				vExpeMarca.Dat.TramiteID.Filter	= new DSFilter( lst );
				vExpeMarca.Dat.MarcaActiva.Filter = true;

				// mbaez. Modificado el 27/10/2006
				// Se listan las marcas PROPIAS y no solamente 
				// las nuestras. Indicado por Laura.
				//vExpeMarca.Dat.MarcaNuestra.Filter = true;
				vExpeMarca.Dat.Vigilada.Filter = true;

				if( this.cbxClienteID.SelectedValue.Trim() != "" )
				{
					vExpeMarca.Dat.ClienteID.Filter	= Convert.ToInt32( this.cbxClienteID.SelectedValue );
				}
				if( this.cbxPropietarioID.SelectedValue.Trim() != "" )
				{
					vExpeMarca.Dat.PropietarioID.Filter = Convert.ToInt32( cbxPropietarioID.SelectedValue );
				}
				#endregion Filtro

				#region Verificar registros Recuperados
				int cont  = vExpeMarca.Adapter.Count();
				if( cont == 0 )
				{
					this.ShowMessage("No se encontr� ningun registro !");
					return;
				}
				if( cont > 2010)
				{
					this.ShowMessage("Los "+cont.ToString()+" registros encontrados superan el l�mite de 2010");
					return;
				}
				#endregion Verificar registros Recuperados

				#region Ordenamiento
				vExpeMarca.Dat.ClienteID.Order = 1;
				vExpeMarca.Dat.PropietarioID.Order = 2;
				#endregion Ordenamiento
			
				#region Generar el reporte
				buffer = htmlString( vExpeMarca, db );
				#endregion Generar el reporte

				#region Activar MS-Word
				Response.Clear();
				Response.Buffer = true;
				Response.ContentType = "application/vnd.ms-word";
				Response.AddHeader("Content-Disposition", "attachment;filename=MA_R_36.doc" );
				Response.Charset = "UTF-8";
				Response.ContentEncoding = System.Text.Encoding.UTF8;
				Response.Write(buffer); //Llamada al procedimiento HTML
				Response.End();
				#endregion Activar MS-Word
			}
		}
		#endregion 


		#region ParametrosOk
		private bool ParametrosOk()
		{
			if( this.cbxClienteID.SelectedValue.Trim() == "" && 
				this.cbxPropietarioID.SelectedValue.Trim() == "")
			{
				ShowMessage("Debe elegir un agente, un propietario o ambos");
				return false;
			}
			return true;
		}
		#endregion ParametrosOk

		// Agregado por mbaez. Elimina los rows que corresponden
		// a marcas vencidas con hijos en tr�mite o activos.
		// Donde padre e hijo comparten la misma marcaid
		// 8va-8va / 7ma-7ma
		#region Eliminar Marcas vencidas de la 7ma.-7ma / 8va.-8va.
		private int EliminarDuplicados( Berke.DG.ViewTab.vExpeMarca  view ) 
		{
			int num_eliminados = 0;
			for(  view.GoTop(); ! view.EOF; view.Skip() ) 
			{
				if (existeHijoVigente(view)) 
				{
					view.Delete();
					num_eliminados++;
				}
			}
			return num_eliminados;
		}
		private bool existeHijoVigente(Berke.DG.ViewTab.vExpeMarca  view)
		{
			int curr_index = view.RowIndex;
			int expedienteid = view.Dat.ExpedienteID.AsInt;
			int marcaid      = view.Dat.MarcaID.AsInt;

			for(  view.GoTop(); ! view.EOF; view.Skip() ) 
			{
				if (   (view.Dat.ExpedienteIDPadre.AsInt == expedienteid )
					&& (view.Dat.MarcaID.AsInt == marcaid) ) 
				{
					view.Go(curr_index);
					return true;
				}
			}
			view.Go(curr_index);
			return false;
		}
		#endregion Eliminar Marcas vencidas de la 7ma.-7ma / 8va.-8va.

		#region htmlString()
		private string htmlString( Berke.DG.ViewTab.vExpeMarca vExpeMarca , Berke.Libs.Base.Helpers.AccesoDB db	)
		{

			
			#region Obtener plantilla 
			string plantilla = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern("MA_R_36", 2);
			if( plantilla == "" )
			{
				this.ShowMessage( "Error con la plantilla" );
				return "";
			}
			#endregion Obtener plantilla

			#region Inicializar Generadores de codigo
			Berke.Libs.CodeGenerator cg = new  Berke.Libs.CodeGenerator(plantilla);
			cg.Inicializar( plantilla );
			Berke.Libs.CodeGenerator root = cg["Root"];
			Berke.Libs.CodeGenerator propietario = root["propietario"];
			Berke.Libs.CodeGenerator fila = propietario["fila"];
			#endregion Inicializar Generadores de codigo
			
			vExpeMarca.Adapter.ReadAll(2000);
			
			#region Leer
			//lecturaOK = vExpeMarca.Adapter.DataReader_Read();
			Berke.DG.MarcaDG mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( vExpeMarca.Dat.MarcaID.AsInt );
			Berke.DG.DBTab.Expediente expeActual = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expeAnt	= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.MarcaRegRen regRenActual = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.MarcaRegRen regRenAnt = new Berke.DG.DBTab.MarcaRegRen( db );
			//Berke.DG.DBTab.CCliente cliente = new Berke.DG.DBTab.CCliente( db );
			//Berke.DG.DBTab.CPropietario prop = new Berke.DG.DBTab.CPropietario( db );
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario( db );
			

			expeActual.Adapter.ReadByID( vExpeMarca.Dat.ExpedienteID.Value );
			expeAnt.Adapter.ReadByID( expeActual.Dat.ExpedienteID.Value );
			regRenActual.Adapter.ReadByID( expeActual.Dat.MarcaRegRenID.Value );
			regRenAnt.Adapter.ReadByID( expeAnt.Dat.MarcaRegRenID.Value );
			cliente.Adapter.ReadByID( vExpeMarca.Dat.ClienteID.Value );
			prop.Adapter.ReadByID( vExpeMarca.Dat.PropietarioID.Value );
			#endregion Leer
			//			System.Globalization.DateTimeFormatInfo.CultureInfo culAnt = System.Threading.Thread.CurrentThread.CurrentUICulture;
			//			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo ( "en-US" );

			int clave;
			string nomcliente = "";
			root.clearText();
			root.copyTemplateToBuffer();

			propietario.clearText();
			int num_eliminados = this.EliminarDuplicados(vExpeMarca);
			vExpeMarca.GoTop();
			while( !vExpeMarca.EOF )
			{
				#region Asignar ID Actual
				clave = vExpeMarca.Dat.PropietarioID.AsInt;
				nomcliente = cliente.Dat.Nombre.AsString;
				while (vExpeMarca.IsRowDeleted && !vExpeMarca.EOF)  
				{
					vExpeMarca.Skip();
					clave = vExpeMarca.Dat.PropietarioID.AsInt;
					nomcliente = cliente.Dat.Nombre.AsString;

					mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( vExpeMarca.Dat.MarcaID.AsInt );
					expeActual.Adapter.ReadByID( vExpeMarca.Dat.ExpedienteID.Value );
					expeAnt.Adapter.ReadByID( expeActual.Dat.ExpedienteID.Value );
					regRenActual.Adapter.ReadByID( expeActual.Dat.MarcaRegRenID.Value );
					regRenAnt.Adapter.ReadByID( expeAnt.Dat.MarcaRegRenID.Value );
					cliente.Adapter.ReadByID( vExpeMarca.Dat.ClienteID.Value );
					prop.Adapter.ReadByID( vExpeMarca.Dat.PropietarioID.Value );
					
				}
				if (vExpeMarca.EOF) 
				{
					break;
				}
				#endregion Asignar ID Actual

				#region Asignar Datos de Propietario
				//int num_reg = vExpeMarca.Adapter.Count() - num_eliminados;
				propietario.copyTemplateToBuffer();
				propietario.replace("#PropietarioNombre#", prop.Dat.Nombre.AsString+" ("+vExpeMarca.Dat.PropietarioID.AsString+")");
				propietario.replace("#PropietarioDireccion#", prop.Dat.Direccion.AsString );

				
				#endregion Asignar Datos de Propietario
				int num_reg = 0;
				fila.clearText();
				while( (!vExpeMarca.EOF) && vExpeMarca.Dat.PropietarioID.AsInt == clave )
				{
					#region Procesar Fila
					if (!vExpeMarca.IsRowDeleted) 
					{
						fila.copyTemplateToBuffer();
						fila.replace("#Marca#",		mar.Marca.Dat.Denominacion.AsString );
						fila.replace("#T#",			mar.MarcaTipo.Dat.Abrev.AsString );
						fila.replace("#Cls#",		mar.Clase.Dat.Nro.AsString );
						fila.replace("#Trm#",		vExpeMarca.Dat.TramiteAbrev.AsString );
						fila.replace("#RegNro#",	regRenActual.Dat.RegistroNro.AsString );
						fila.replace("#fConc#",		regRenActual.Dat.ConcesionFecha.AsString );
						fila.replace("#RegAnt#",	regRenAnt.Dat.RegistroNro.AsString  );
						fila.replace("#BibExp#",	expeActual.Dat.Bib.AsString+" - "+expeActual.Dat.Exp.AsString );
						fila.replace("#Acta#",		expeActual.Dat.Acta.AsString );
						fila.replace("#fPrec#",		expeActual.Dat.PresentacionFecha.AsString );
						fila.replace("#Vencim#",	regRenActual.Dat.VencimientoFecha.AsString );
					
						fila.addBufferToText();
					}
					#endregion Procesar Fila 
					num_reg = num_reg + 1;
					#region Leer
					do 
				    {					
						/*
						 * ggaleano [18/04/2007] Se comenta debido a que
						 * la re-lectura de los DBTab no es necesaria cuando la fila
						 * ha sido marcada como borrada y se ha llegado al EOF.
						 */


						//					regRenAnt.DeleteAllRows();
						//					expeAnt.DeleteAllRows();
						
						
					    /*if (!vExpeMarca.EOF)
						{*/
						
							vExpeMarca.Skip();	
							mar = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( vExpeMarca.Dat.MarcaID.AsInt );
							expeActual.Adapter.ReadByID( vExpeMarca.Dat.ExpedienteID.Value );
							expeAnt.Adapter.ReadByID( expeActual.Dat.ExpedienteID.Value );
							regRenActual.Adapter.ReadByID( expeActual.Dat.MarcaRegRenID.Value );
							regRenAnt.Adapter.ReadByID( expeAnt.Dat.MarcaRegRenID.Value );
							cliente.Adapter.ReadByID( vExpeMarca.Dat.ClienteID.Value );
							prop.Adapter.ReadByID( vExpeMarca.Dat.PropietarioID.Value );
						
							
	    
						//}
						
						
					}while( !vExpeMarca.EOF && vExpeMarca.IsRowDeleted ) ;
					
				    
					#endregion Leer
				}	
				propietario.replace("#NumReg#", ""+  num_reg);
				propietario.replaceLabel("fila", fila.Texto );
				propietario.addBufferToText();
			}
			//vExpeMarca.Adapter.DataReader_Close();
			root.replaceLabel("propietario", propietario.Texto );
			root.replace("#ClienteNombre#", nomcliente/*cliente.Dat.Nombre.AsString*/); // this.cbxClienteID.SelectedText);
			root.replace("#FECHA#",  ObjConvert.AsString(DateTime.Today));
			root.addBufferToText();

			//			System.Threading.Thread.CurrentThread.CurrentUICulture = culAnt;
			return root.Texto;
		}
		#endregion htmlString()

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

		#endregion Funciones Genericas Replicadas



	}
}
