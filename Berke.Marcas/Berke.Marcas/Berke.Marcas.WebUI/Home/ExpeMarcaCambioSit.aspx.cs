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
using System.Security.Principal;

namespace Berke.Marcas.WebUI.Home
{
	
	#region Using
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Helpers;
	#endregion Using


	/// <summary>
	/// Summary description for ExpeMarcaCambioSit.
	/// </summary>
	public partial class ExpeMarcaCambioSit : System.Web.UI.Page
	{
		#region Properties

		#region Plazo
		private int Plazo
		{
			get{ return Convert.ToInt32(( ViewState["Plazo"] == null )? 0 : ViewState["Plazo"] ) ; }
			set{ ViewState["Plazo"] = Convert.ToString( value );}
		}
		#endregion Plazo

		#region SituacionID
		private int SituacionID
		{
			get{ return Convert.ToInt32(( ViewState["SituacionID"] == null )? 0 : ViewState["SituacionID"] ) ; }
			set{ ViewState["SituacionID"] = Convert.ToString( value );}
		}
		#endregion SituacionID

		//		#region IDsValidos
		//		private string IDsValidos
		//		{
		//			get{ return Convert.ToString(( ViewState["IDsValidos"] == null )? "" : ViewState["IDsValidos"] ) ; }
		//			set{ ViewState["IDsValidos"] = Convert.ToString( value );}
		//		}
		//		#endregion IDsValidos

		#region Fecha
		private string Fecha
		{
			get{ return Convert.ToString(( ViewState["Fecha"] == null )? "" : ViewState["Fecha"] ) ; }
			set{ ViewState["Fecha"] = Convert.ToString( value );}
		}
		#endregion Fecha

		#region Registro
		private string Registro
		{
			get{ return Convert.ToString(( ViewState["Registro"] == null )? "" : ViewState["Registro"] ) ; }
			set{ ViewState["Registro"] = Convert.ToString( value );}
		}
		#endregion Registro

		#endregion Properties

		#region Controles del Form

		protected Berke.Libs.WebBase.Controls.DropDown ddlTramiteSitDestinod;
		#endregion Controles del Form

		#region Asignar Delegados
		private void AsignarDelegados()
		{
			this.ddlTramite.SelectedIndexChanged +=new EventHandler(ddlTramite_SelectedIndexChanged);
			this.ddlTramiteSitDestino.SelectedIndexChanged += new EventHandler(ddlTramiteSitDestino_SelectedIndexChanged);
			this.ddlSituacionDestino.SelectedIndexChanged  += new EventHandler(ddlSituacionDestino_SelectedIndexChanged_1);
			//DropDown Tramite
			//			this.ddlTramite.SelectedIndexChanged += new ;
			//
			//			//DropDown TramiteSitDestino
			//			this.ddlTramiteSitDestino.SelectedIndexChanged += new System.EventHandler(this.ddlTramiteSitDestino_SelectedIndexChanged);
			//		

		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			txtAnio.Text = DateTime.Today.Year.ToString();
			this.lblPlazo.Text = "";

			this.Fecha = string.Format("{0:d}", DateTime.Today);
			this.Registro = "";

			#region Visibles/No visibles
			this.btnGrabar.Visible = false;
			this.pnlReplicar.Visible = false;
			this.ddlTramiteSitDestino.Visible = false;
			this.ddlSituacionDestino.Visible = true;
			this.pnlProceso.Visible = false;
			#endregion Visibles/No visibles

			#region Llenar DropDown de Situaciones
			ddlSituacionDestino.Items.Clear();
			this.ddlSituacionDestino.Fill(UIPModel.Situacion.ReadForSelect().Table );
			#endregion Llenar DropDown de Situaciones

			#region Llenar DropDpwn de Tramites
			//			SimpleEntryDS se = UIPModelEntidades.Tramite.ReadForSelect( (int) Const.Proceso.MARCAS );
			//			ddlTramite.Fill( se.Tables[0], true);

			Berke.DG.ViewTab.ListTab lst = Berke.Marcas.UIProcess.Model.Tramite.ReadForSelect();
			ddlTramite.Fill( lst.Table, true);

			#endregion

		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !this.IsPostBack )
			{
				AsignarValoresIniciales();
                this.MakedtAgentesLocalesNuestros();
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

		#region ddlTramite_SelectedIndexChanged
		private void ddlTramite_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ddlTramite.SelectedIndex == 0)
			{
				this.ddlTramiteSitDestino.Items.Clear();
				this.ddlTramiteSitDestino.Visible	=  false;
				this.ddlSituacionDestino.Visible	= true;

			}
			else
			{
				this.ddlTramiteSitDestino.Visible	= true ;
				this.ddlSituacionDestino.Visible	= false;

				SimpleEntryDS situacion = UIPModel.TramiteSit.ReadForSelect( int.Parse(ddlTramite.Value) );
				this.ddlTramiteSitDestino.Fill( situacion.Tables[0], true );
			}
		}
		#endregion ddlTramite_SelectedIndexChanged

		#region ddlTramiteSitDestino_SelectedIndexChanged
		private void ddlTramiteSitDestino_SelectedIndexChanged(object sender, EventArgs e)
		{
			int tramiteSitID = int.Parse( this.ddlTramiteSitDestino.SelectedValue );
			
			Berke.DG.DBTab.Tramite_Sit trmSit = Berke.Marcas.UIProcess.Model.TramiteSit.ReadByID_M(tramiteSitID) ;
			Berke.DG.DBTab.Unidad unidad = Berke.Marcas.UIProcess.Model.Unidad.ReadByID( trmSit.Dat.UnidadID.AsInt );
			
			Plazo		= trmSit.Dat.Plazo.AsInt;		// Persistir
			SituacionID	= trmSit.Dat.SituacionID.AsInt; // Persistir


			string plazo = trmSit.Dat.Plazo.AsString;
			if( trmSit.Dat.Plazo.AsInt == 0 )
			{
				plazo = "";
			}
			else
			{
				plazo+= " " + unidad.Dat.Descrip.AsString;
			}
			this.lblPlazo.Text = "Plazo : " + plazo;
		}
		#endregion ddlTramiteSitDestino_SelectedIndexChanged

		#region ddlSituacionDestino_SelectedIndexChanged
		private void ddlSituacionDestino_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			this.lblPlazo.Text		= "";
			SituacionID	= int.Parse( this.ddlSituacionDestino.SelectedValue ); // Persistir

		}
		#endregion ddlSituacionDestino_SelectedIndexChanged

		#region btnBuscar_Click
		protected void btnBuscar_Click(object sender, System.EventArgs e)
		{
            pnlProceso.Visible = false;

            if (this.ddlTramiteSitDestino.SelectedValue.Trim() != "" ||
                this.ddlSituacionDestino.SelectedValue.Trim() != "")
            {
                if (this.txtIdentificadores.Text.Trim() != "")
                {
                    BuscarMarcas(txtIdentificadores.Text);
                }
                else
                {
                    this.ShowMessage("Ingrese uno o varios Números separados por comas");
                }
            }
            else
            {
                this.ShowMessage("Debe indicar la Situación Destino");
            }
		}
		#endregion btnBuscar_Click


		#region Configurar Grilla
		private void ConfigurarGrilla( GridGW dgw  )
		{
		
			//             CtrlName           Header       CtrlWidth
			dgw.AddCheck( "chkSel"			,"Sel."			, 30 );
			dgw.AddLabel( "lblExpeID"		,"Exp.ID"		, 40 );

			dgw.AddLabel( "lblDenominacion"	,"Denom"		, 150 );
			dgw.AddLabel( "lblClase"		,"Clase"		, 40 );


			dgw.AddLabel( "lblTramiteAbrev"		,"Trám." 	, 50 );

			dgw.AddLabel( "lblRegistro"		,"Reg.Vgte." 	, 50 );

			dgw.AddLabel( "lblVencimTitulo"	,"Vencim."		, 70 );

			
			dgw.AddLabel( "lblActaMarca"	,"ActaMarca"	, 70 );
			dgw.AddLabel( "lblActa"			,"Acta"			, 70 );
			dgw.AddText ( "txtFecha"		,"Fecha" 		, 80 );
			
			dgw.AddText ( "txtFinPlazo"		,"Fin Plazo" 	, 70 );
			dgw.AddText ( "txtBibExp"		,"Bib/Exp"		, 70 );
			dgw.AddText ( "txtRegistro"		,"Registro"		, 70 );
			dgw.AddText ( "txtActa"			,"Acta"			, 70 );
			dgw.AddText ( "txtVencimTitulo"	,"Venc.Titulo"	, 70 );
			dgw.AddText ( "txtPagAnio"		,"Pag/Año"		, 70 );
			dgw.AddText ( "txtDiario"		,"Diario"		, 70 );
			dgw.AddText ( "txtAgLocal"		,"Ag.Local"		, 70 );

			dgw.AddText ( "txtObs"			,"Observación"	, 200 );

			dgw.AddLabel( "lblError"		,"Error"		, 200 );
			dgw.AddLabel( "lblTramiteSitID"	,"TrmSitID"		, 70 );
		

		}
		#endregion Configurar Grilla

		#region Configurar Grilla que muestra los resultados del proceso
		private void ConfigurarGrillaProceso( GridGW dgw  )
		{
		
			//             CtrlName           Header       CtrlWidth
			dgw.AddCheck( "chkpSel"			,"Proc."			, 30 );
			dgw.AddLabel( "lblpExpeID"		,"Exp.ID"		, 40 );

			dgw.AddLabel( "lblpDenominacion"	,"Denom"		, 150 );
			dgw.AddLabel( "lblpClase"		,"Clase"		, 40 );
			dgw.AddLabel( "lblpTramiteAbrev"		,"Trám." 	, 50 );
			dgw.AddLabel( "lblpFecSit"	,"Fec. Sit."		, 70 );
			dgw.AddLabel( "lblpVencSit"	,"Venc. Sit."		, 70 );
			dgw.AddLabel( "lblpRegistro"		,"Reg.Vgte." 	, 50 );
			dgw.AddLabel( "lblpVencimTitulo"	,"Vencim."		, 70 );
			dgw.AddLabel( "lblpActa"			,"Acta"			, 70 );
			dgw.AddLabel( "lblpError"		,"Error"		, 200 );
			
		

		}
		#endregion Configurar Grilla

		#region BuscarMarcas
		private void BuscarMarcas( string identificadores )
		{
			this.txtReplicar.Text = this.Fecha;
			ddlReplicar.SelectedIndex = 0;
			bool errores = false;
			string faltantes ="";
			string existentes ="";

			#region Buscar Registros
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.ViewTab.vExpeMarca vMarca = new Berke.DG.ViewTab.vExpeMarca( db );
			
			//ArrayList idLst = new ArrayList(identificadores.Split( ((String)",").ToCharArray() ));
			ArrayList idLst;

			if (identificadores.IndexOf("-") > -1)
			{
				int limiteInferior = Convert.ToInt32(identificadores.Split('-')[0]);
				int limiteSuperior = Convert.ToInt32(identificadores.Split('-')[1]);

				idLst = new ArrayList();

				for(int x = limiteInferior; x <= limiteSuperior; x++)
				{
					idLst.Add(x.ToString());
				}
			}
			else
			{
				idLst = new ArrayList(identificadores.Split( ((String)",").ToCharArray() ));
			}

			ArrayList lstTramites ;
			
			#region Filtro segun Identificador
			switch( this.ddlIdentificador.SelectedValue )
			{
				case "RegVig" :
					vMarca.Dat.RegVigenteNro.Filter = new DSFilter( idLst );
					break;
				case "Reg" :
					vMarca.Dat.RegistroNro.Filter = new DSFilter( idLst );
					break;
				case "Acta" :
					vMarca.Dat.ActaNro.Filter = new DSFilter( idLst );
					vMarca.Dat.ActaAnio.Filter= this.txtAnio.Text;
					break;
				case "ExpeID" :
					vMarca.Dat.ExpedienteID.Filter = new DSFilter( idLst );
					break;
				case "HI" :
					vMarca.Dat.OtNro.Filter = new DSFilter( idLst );
					vMarca.Dat.OtAnio.Filter= this.txtAnio.Text;
					break;
				case "MarcaID" :
					vMarca.Dat.MarcaID.Filter = new DSFilter( idLst );
					break;
			}
			#endregion Filtro segun Identificador

			if( this.ddlTramite.SelectedValue.Trim() != "" )
			{
			    
				vMarca.Dat.TramiteID.Filter = Convert.ToInt32(this.ddlTramite.SelectedValue);			
			}
			
			int pasar_a_situacion = 0;
			string str_tramites = "1,2";
			

			if( ddlSituacionDestino.Visible == true )
			{
				if ( this.ddlSituacionDestino.SelectedValue.Trim() != "" ) 
				{
					pasar_a_situacion  = ( int.Parse(ddlSituacionDestino.SelectedValue) );
					switch( pasar_a_situacion)
					{
						case (int) GlobalConst.Situaciones.CANCELACION_REG :
							lstTramites= new ArrayList(str_tramites.Split( ((String)",").ToCharArray() ));
							vMarca.Dat.TramiteID.Filter = new DSFilter(lstTramites);
							break;
					}
				}
			}

			

			vMarca.Dat.ExpedienteID.Order = 1;
			//vMarca.Adapter.ReadAll( 150 );
			vMarca.Adapter.ReadAll();

			if( vMarca.RowCount < 1 )
			{
				this.ShowMessage("Ningún Registro encontrado");
				return;
			}


			#region Eliminar duplicados
			int ant = -111999;
			for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() )
			{
				if( vMarca.Dat.ExpedienteID.AsInt == ant )
				{
					vMarca.Delete();
				}
				else
				{
					ant = vMarca.Dat.ExpedienteID.AsInt;
				}
			}
			vMarca.AcceptAllChanges();
			#endregion Elimninar duplicados

			#region Ordenar por si hay mas de un reg. x Identificador (consulta x HI)
			vMarca.GoTop();
			vMarca.ClearOrder();
			if ( vMarca.Dat.TramiteID.AsInt == (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.REGISTRO)
				//if( vMarca.Dat.TramiteAbrev.AsString == "REG" ) // Registro
			{
				vMarca.Dat.Denominacion.Order	= 1;
				vMarca.Dat.ClaseNro.Order		= 2;
			}
			else if ( vMarca.Dat.TramiteID.AsInt == (int) Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION)
			{
				//else if ( vMarca.Dat.TramiteAbrev.AsString == "REN"){ // Renovaciones
				/* BUG#144
				 * Se ordena igual que la HI de Renovacion
				 * 
				 * */
				vMarca.Dat.VencimientoFecha.Order  = 1;
				vMarca.Dat.Denominacion.Order	   = 2;
				//vMarca.Dat.ExpedienteID.Order	= 1;
			}
			else  //  TV
			{ 		
				vMarca.Dat.ExpedienteID.Order	= 1;	
			}
			vMarca.Sort();
			#endregion 

			#region Reodenar segun secuencia de identificadores ingresada
			Berke.DG.ViewTab.vExpeMarca temp = new Berke.DG.ViewTab.vExpeMarca();
			foreach( String idIn in idLst )
			{
				string idRec = "";
				for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() )
				{
					#region idRec = Identificador recuperado
					switch( this.ddlIdentificador.SelectedValue )
					{
						case "RegVig" :
							idRec = vMarca.Dat.RegVigenteNro.AsString;
							break;
						case "Reg" :
							idRec = vMarca.Dat.RegistroNro.AsString;
							break;
						case "Acta" :
							idRec = vMarca.Dat.ActaNro.AsString;
							break;
						case "ExpeID" :
							idRec = vMarca.Dat.ExpedienteID.AsString;
							break;
						case "HI" :
							idRec = vMarca.Dat.OtNro.AsString;
							break;
						case "MarcaID" :
							idRec = vMarca.Dat.MarcaID.AsString;
							break;
					}
					#endregion idRec = Identificador
					if( idRec == idIn && ! vMarca.IsRowDeleted )
					{
						temp.Table.ImportRow( vMarca.Table.Rows[vMarca.RowIndex]);
						vMarca.Delete();
					}
				}// end for		
			}// end foreach
			vMarca = new Berke.DG.ViewTab.vExpeMarca( temp.Table );
			vMarca.InitAdapter( db );
			#endregion Reodenar segun secuencia ...

			#region "Marcar" las Inactivas
			for( vMarca.GoTop(); ! vMarca.EOF; vMarca.Skip() )
			{
				if( ! vMarca.Dat.MarcaActiva.AsBoolean )
				{
					vMarca.Edit();
					vMarca.Dat.Denominacion.Value = vMarca.Dat.Denominacion.AsString+" *INACTIVA* ";
					vMarca.PostEdit();
				}
			}
			#endregion "Marcar" las Inactivas

			#endregion Buscar Registros

			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			Berke.Libs.WebBase.Helpers.GridGW dgwProceso = new GridGW( dgProceso );

			this.ConfigurarGrilla( dgw );
			this.ConfigurarGrillaProceso( dgwProceso );
	
	
			#region Aplicar Config y Crear filas vacias en la Grilla
			dgw.Inicializar( vMarca.RowCount );
			dgwProceso.Inicializar( vMarca.RowCount );
			#endregion Aplicar Config y Crear filas vacias en la Grilla

			
			#region Columnas Visibles por defecto
			dgw.Visible("chkSel"			, true ); 
			dgw.Visible("lblExpeID"	, true); 

			dgw.Visible("lblDenominacion"	, true); 
			dgw.Visible("lblClase"			, true); 
			dgw.Visible("lblTramiteAbrev"	, true);
			dgw.Visible("lblRegistro"		, true); 
			dgw.Visible("lblVencimTitulo"	, true); 
			dgw.Visible("lblActaMarca"		, false ); 
			dgw.Visible("lblActa"			, true); 
			dgw.Visible("txtFecha"			, true); 
			dgw.Visible("txtObs"			, true); 
	
			dgw.Visible("txtFinPlazo"		, false); 
			dgw.Visible("txtBibExp"			, false); 
			dgw.Visible("txtRegistro"		, false); 
			dgw.Visible("txtActa"			, false); 
			dgw.Visible("txtVencimTitulo"	, false); 
		
			dgw.Visible("txtPagAnio"		, false); 
			dgw.Visible("txtDiario"			, false);

			dgw.Visible("txtAgLocal"		, false);
			dgw.Visible("lblError"			, false);

			dgw.Visible("lblTramiteSitID"	, false);
			
			#endregion Columnas  Visibles por defecto

			#region Columnas Visibles por defecto dgwProceso
			dgw.Visible("chkSel"			, true ); 
			dgw.Visible("lblExpeID"	, true); 

			dgw.Visible("lblDenominacion"	, true); 
			dgw.Visible("lblClase"			, true); 
			dgw.Visible("lblTramiteAbrev"	, true);
			dgw.Visible("lblRegistro"		, true); 
			dgw.Visible("lblVencimTitulo"	, true); 
			dgw.Visible("lblActaMarca"		, false ); 
			dgw.Visible("lblActa"			, true); 
			dgw.Visible("txtFecha"			, true); 
			dgw.Visible("txtObs"			, true); 
	
			dgw.Visible("txtFinPlazo"		, false); 
			dgw.Visible("txtBibExp"			, false); 
			dgw.Visible("txtRegistro"		, false); 
			dgw.Visible("txtActa"			, false); 
			dgw.Visible("txtVencimTitulo"	, false); 
		
			dgw.Visible("txtPagAnio"		, false); 
			dgw.Visible("txtDiario"			, false);

			dgw.Visible("txtAgLocal"		, false);
			dgw.Visible("lblError"			, true);

			dgw.Visible("lblTramiteSitID"	, false);
			
			#endregion Columnas  Visibles por defecto

			#region Asignar valores a las celdas

			Berke.DG.MarcaDG marDG = new Berke.DG.MarcaDG();
			int i = 0;
			//			IDsValidos = "";

			/*
			 * BUG#144
			 * Comento lo agregado por el bug indicado porque 
			 * necesita ser aclarado el alcance del cambio
			 * solicitado.
			 * 
			 * Se deja que el cambio de situacion ordene por 
			 * expediente como estaba originalmente
			 * 
			vMarca.ClearOrder();
			vMarca.Dat.VencimientoFecha.Order = 1;
			vMarca.Dat.Denominacion.Order = 2;
			vMarca.Sort();
			*/

			for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
			{
				string mensajeError = "";

				#region Clase
				string clase =  vMarca.Dat.Clase.AsString;
				clase = clase.Replace("Clase","").Trim();
				clase = clase.Replace("(8ª Ed.)"," 8ª").Trim();
				clase = clase.Replace("(7ª Ed.)"," 7ª").Trim();
				#endregion Clase

				#region Asignar valores leidos
				dgw.Set(i, "lblExpeID", vMarca.Dat.ExpedienteID.AsString );

				dgw.Set(i, "lblTramiteAbrev", vMarca.Dat.TramiteAbrev.AsString );
				dgw.Set(i, "lblRegistro", vMarca.Dat.RegVigenteNro.AsString );
				dgw.Set(i, "lblActa", vMarca.Dat.Acta.AsString );
				dgw.Set(i, "lblActaMarca", vMarca.Dat.ActaPadre.AsString );
				
				//				vMarca.Dat.TramiteAbrev

				#region Denominacion
				
				string denom = vMarca.Dat.Denominacion.AsString;
				if( denom.Trim() == "" )
				{
					denom = "???";
				}
				
				denom = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
					"MarcaDetalleL.aspx",denom,vMarca.Dat.ExpedienteID.AsString,"ExpeID");


				dgw.Set(i, "lblDenominacion", denom );
				#endregion Denominacion

				#region Tratamiento de MarcaID Null
				if( vMarca.Dat.MarcaID.IsNull )
				{
					dgw.Set(i, "lblError", "<b>marcaID=Null</b>" );
					errores  = true;
					i++;
					continue;
				}
				#endregion Tratamiento de MarcaID Null

				marDG = Berke.Marcas.UIProcess.Model.Marca.ReadByID_asDG( vMarca.Dat.MarcaID.AsInt );

				#region ToolTip
				string datos = "PROPIETARIO:  "+ marDG.Propietario.Dat.Nombre.AsString+ " - " +
					marDG.Propietario.Dat.Direccion.AsString + "\n\n";
				datos+= "CLIENTE:  "+ marDG.Cliente.Dat.Nombre.AsString + " - " +
					marDG.Cliente.Dat.Direccion.AsString + " \n";

				dgw.SetToolTip(i, "lblDenominacion", datos );
				dgw.SetToolTip(i, "lblTramiteAbrev", vMarca.Dat.TramiteDescrip.AsString );
				#endregion ToolTip

				dgw.Set(i, "lblClase", clase );

				dgw.Set(i, "txtVencimTitulo", vMarca.Dat.VencimientoFecha.AsString );
				dgw.Set(i, "lblVencimTitulo", vMarca.Dat.VencimientoFecha.AsString );

				#endregion Asignar valores leidos

				#region Es nuestra
				//				if( ! vMarca.Dat.MarcaNuestra.AsBoolean )  // cancelado 27/02/2006
				//				{
				//					mensajeError += "No es Nuestra.  ";
				//					errores = true;
				//				}
				#endregion Es nuestra

				#region La situacion Corresponde ?
				Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit( db );
				if( ddlTramiteSitDestino.Visible == true )
				{
					trmSit.Adapter.ReadByID( int.Parse(ddlTramiteSitDestino.SelectedValue) );
				}
				else
				{
					trmSit.Dat.TramiteID.Filter = vMarca.Dat.TramiteID.AsInt;
					trmSit.Dat.SituacionID.Filter = this.SituacionID;
					trmSit.Adapter.ReadAll();
				}
				dgw.Set(i, "lblTramiteSitID", trmSit.Dat.ID.AsString );

				Berke.DG.DBTab.Tramite_SitSgte trmSitSgte = new	 Berke.DG.DBTab.Tramite_SitSgte( db );
				trmSitSgte.Dat.TramiteSitID.Filter = vMarca.Dat.TramiteSitID.AsInt;
				trmSitSgte.Adapter.ReadAll( 50 );

				/* [BUG#345] Para las marcas sustituidas no debe controlarse el paso a traves
				 *           de las situaciones. El control se hace para las marcas Nuestras
				 */

				/*[ggaleano 06/06/2013] Se cambia el control de Marcas Nuesttras a Vigiladas por ser más amplio
				if( vMarca.Dat.MarcaNuestra.AsBoolean && !vMarca.Dat.Sustituida.AsBoolean)*/
				if( vMarca.Dat.Vigilada.AsBoolean && !vMarca.Dat.Sustituida.AsBoolean)
				{
					bool trmSitOK = false;
					for( trmSitSgte.GoTop(); !trmSitSgte.EOF; trmSitSgte.Skip() )
					{
						if( trmSit.Dat.ID.AsInt == trmSitSgte.Dat.TramiteSitSgteID.AsInt)
						{
							trmSitOK = true;
						}
					}
					if( ! trmSitOK )
					{
						mensajeError += "Situación previa :<b>" + vMarca.Dat.SituacionDecrip.AsString + "</b> No corresponde.  ";
						errores = true;
					}
				}
				#endregion La situacion Corresponde ?

				#region Evaluar Si hubo errores
				if( mensajeError == "")
				{
					//					IDsValidos+= ( IDsValidos == "" ?"":"," ) +vMarca.Dat.ExpedienteID.AsString;
					dgw.Set(i, "chkSel", "Si" );
					dgw.Set(i, "lblError", "OK" );
				}
				else
				{
					dgw.Set(i, "lblError", mensajeError );
				}
				#endregion Evaluar Si hubo errores

				#region Columnas visibles para tramites varios
				if( vMarca.Dat.TramiteID.AsInt != 1 && vMarca.Dat.TramiteID.AsInt != 2)
				{
					dgw.Visible("lblActaMarca"		, true); 
				}
				#endregion Columnas visibles para tramites varios

				i++;
			}
			#endregion Asignar valores a las celdas

			#region Columnas  Visibles segun datos
	
			if( errores )
			{
				dgw.Visible("lblError"			, true	);
				dgw.Visible("txtFecha"			, false	); 
				dgw.Visible("txtObs"			, false	);

				this.pnlReplicar.Visible = false;
				this.btnGrabar.Text = "Obtener expedientes procesables";
			}
			else
			{
				this.btnGrabar.Text = "Grabar";
				this.pnlReplicar.Visible = true;
				this.ddlReplicar.Items.Clear();
				this.ddlReplicar.Items.Add( new ListItem("Fecha","txtFecha"));	
				ColumnasVisiblesSegunSituacionDestino( dgw );
		
			}
			pnlGrillas.Visible = true;
			this.btnGrabar.Visible	= true;

			#endregion Columnas  Visibles segun datos

		
			db.CerrarConexion();

			#region Informar sobre marcas No encontradas
			string tipo = ddlIdentificador.SelectedValue;
			foreach( object obj in idLst)
			{
				bool ok = false;
				switch( tipo )
				{
					case "RegVig" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.RegVigenteNro.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;
					case "Reg" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.RegistroNro.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;
					case "Acta" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.ActaNro.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;
					case "ExpeID" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.ExpedienteID.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;
					case "HI" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.OtNro.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;
					case "MarcaID" :
						#region ciclo de comparacion
						for( vMarca.GoTop() ; ! vMarca.EOF; vMarca.Skip() )
						{
							if( ( vMarca.Dat.MarcaID.AsString == (string)obj ) )
							{
								ok= true;
								break; // Sale del for
							}			
						} // endfor
						#endregion 
						break;

				}// end switch


				if( ok )
				{
					existentes+= (string)obj+", ";
				}
				else
				{
					faltantes+= (string)obj+", ";
				}	
			}

			//			this.ShowMessage(" Existentes = "+ existentes+ "   Faltantes = " +faltantes);
			if( faltantes!= "" )
			{
				this.ShowMessage(" No se encontraron : " + faltantes );
			}
			#endregion Informar sobre marcas No encontradas
		}
		#endregion BuscarMarcas

		#region Visibles según Situacion destino
		private void ColumnasVisiblesSegunSituacionDestino( GridGW dgw )
		{
			this.btnCalcVencim.Visible = false;
			switch( SituacionID )
			{
				case  (int) GlobalConst.Situaciones.ARCHIVADA:
					dgw.Visible("txtBibExp"			, true ); 
					this.ddlReplicar.Items.Add( new ListItem("BibExp","txtBibExp"));	
					break;
				case  (int) GlobalConst.Situaciones.PRESENTADA:
					dgw.Visible("lblActa"			, false ); 
					dgw.Visible("txtActa"			, true ); 
					dgw.Visible("txtAgLocal"		, true );
					this.ddlReplicar.Items.Add( new ListItem("Acta","txtActa"));	
					this.ddlReplicar.Items.Add( new ListItem("Ag.Local","txtAgLocal"));	
				
					break;
				case (int) GlobalConst.Situaciones.PUBLICADA:
					dgw.Visible("txtPagAnio"		, true ); 
					dgw.Visible("txtDiario"			, true );
					this.ddlReplicar.Items.Add( new ListItem("Página/Año","txtPagAnio"));	
					this.ddlReplicar.Items.Add( new ListItem("Diario","txtDiario"));	

					break;
				case (int) GlobalConst.Situaciones.CONCEDIDA:
					dgw.Visible("txtRegistro"		, true ); 		
					dgw.Visible("txtVencimTitulo"	, true ); 		
					this.ddlReplicar.Items.Add( new ListItem("Nro.Registro","txtRegistro"));	
					this.btnCalcVencim.Visible = true;
					break;
				case (int) GlobalConst.Situaciones.TITULO_RETIRADO:
					// Cancelado a pedido de Mary Kovacs 9/03/2006
					//dgw.Visible("txtVencimTitulo"	, true ); 
					//this.ddlReplicar.Items.Add( new ListItem("Vencim.Título","txtVencimTitulo"));	

					break;
			}
		}
		#endregion Visibles según Situacion destino

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

		#region Validar
		private bool Validar( out string  mensaje )
		{
			mensaje = "";
			bool ret = true;
			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			foreach( DataGridItem item in dgResult.Items )
			{

				#region Extraer valores de la grilla

				string chkSel			= dgw.GetText( item, "chkSel" );
				string lblDenominacion	= dgw.GetText( item, "lblDenominacion" );
				string lblExpeID		= dgw.GetText( item, "lblExpeID" );
				string lblClase			= dgw.GetText( item, "lblClase" );
				string lblTramiteAbrev	= dgw.GetText( item, "lblTramiteAbrev" );
				string lblRegistro		= dgw.GetText( item, "lblRegistro" );
		
				
				string lblActa			= dgw.GetText( item, "lblActa" );
				string txtFecha			= dgw.GetText( item, "txtFecha" );
				string txtFinPlazo		= dgw.GetText( item, "txtFinPlazo" );
				string txtBibExp		= dgw.GetText( item, "txtBibExp" );
				string txtRegistro		= dgw.GetText( item, "txtRegistro" );
				string txtActa			= dgw.GetText( item, "txtActa" );
				string txtVencimTitulo	= dgw.GetText( item, "txtVencimTitulo" );
				string txtPagAnio		= dgw.GetText( item, "txtPagAnio" );
				string txtDiario		= dgw.GetText( item, "txtDiario" );
				string txtAgLocal		= dgw.GetText( item, "txtAgLocal" );
				string lblTramiteSitID	= dgw.GetText( item, "lblTramiteSitID" );


				#endregion Extraer valores de la grilla
				if( chkSel != "Si" )
				{
					continue;
				}

				if( txtFecha.Trim() == "" )
				{
					mensaje = "Las fechas deben ser asignadas ";
					return false;
				}

				switch( SituacionID )
				{
					case  (int) GlobalConst.Situaciones.ARCHIVADA:
						if( txtBibExp.Trim() == "" || -1 == txtBibExp.IndexOf( "/" ) )
						{
							mensaje = @"Ingrese Bibliorato y Expediente (separado por ""/"" )  ";
							return false;
						}
						break;
					case  (int) GlobalConst.Situaciones.PRESENTADA:
						if ( txtFecha.IndexOf(":") == -1 )
						{
							mensaje = "Debe incluir la hora además de la Fecha. Ej: 23/07/2006 8:34 ";
							return false;
						}
						if( txtActa.Trim() == ""  || -1 != txtActa.IndexOf( "/" ) )
						{
							mensaje = "Debe ingresar el numero de Acta ( sin año ) ";
							return false;
						}
                        //if( txtAgLocal.Trim() == "" || -1 == ((string)"HTB~MB~LCS~HB~AVB~AB~6~7~8~9~3717~3297").IndexOf( txtAgLocal.Trim()) )
                        if (txtAgLocal.Trim() == "" || -1 == (this.ObtenerAgenteLocalesNuestros().IndexOf(txtAgLocal.Trim())))
						{
                            mensaje = String.Format("Debe ingresar la inicial del Agente Local ({0}) o su número de matrícula.", this.ObtenerAgenteLocalesNuestros(true));// "Debe ingresar la inicial del Agente Local (HTB / MB / LCS / HB / AVB / AB) o su numero de matricula  ";
							return false;
						}
						break;
					case (int) GlobalConst.Situaciones.PUBLICADA:
						if( txtPagAnio.Trim() == "" || -1 == txtPagAnio.IndexOf( "/" ))
						{
							mensaje = @"Debe ingresar Pagina y Año, separados por ""/"" ";
							return false;
						}

						if( txtDiario.Trim() == "" )
						{
							mensaje = @"Ingrese la Inicial del Diario ";
							return false;
						}
						else
						{
							#region Obtener DiarioID
							Berke.DG.DBTab.Diario diario = new Berke.DG.DBTab.Diario();
							if( txtDiario != "" )
							{
								diario = Berke.Marcas.UIProcess.Model.Diario.ReadByAbrev( txtDiario );
							}
							#endregion Obtenr DiarioID
							
							if( diario.RowCount != 1 )
							{
								mensaje = @"Verifique la Inicial del Diario ( "+ txtDiario +")";
								return false;
							}
						}
						break;
					case (int) GlobalConst.Situaciones.CONCEDIDA:

						if( txtRegistro.Trim() == "" || -1 != txtRegistro.IndexOf( "/" ))
						{
							mensaje = @"Indique el número de Registro ( Sin Año )";
							return false;
						}
						if( txtVencimTitulo.Trim() == "" )
						{
							mensaje = @"Indique Fecha de Vencimiento del Registro";
							return false;
						}
						break;
					case (int) GlobalConst.Situaciones.TITULO_RETIRADO:
						//						if( txtVencimTitulo.Trim() == "" )
						//						{
						//							mensaje = @"Indique Fecha de Vencimiento del Registro";
						//							return false;
						//						}
						break;
				}



			}

			return ret;
		}

        private void MakedtAgentesLocalesNuestros()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("MatriculaNro", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Iniciales", typeof(String)));

            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["ID"];
            dt.PrimaryKey = key;

            Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
            db.DataBaseName = WebUI.Helpers.MyApplication.CurrentDBName;
            db.ServerName = WebUI.Helpers.MyApplication.CurrentServerName;

            Berke.DG.DBTab.CAgenteLocal agLoc = new DG.DBTab.CAgenteLocal(db);
            agLoc.Dat.Nuestro.Filter = true;
            agLoc.Adapter.ReadAll();

            for (agLoc.GoTop(); !agLoc.EOF; agLoc.Skip())
            {
                if (agLoc.Dat.nromatricula.AsInt >= 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = agLoc.Dat.idagloc.AsInt;
                    dr["MatriculaNro"] = agLoc.Dat.nromatricula.AsInt;
                    dr["Iniciales"] = agLoc.Dat.Iniciales.AsString;

                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
            }
            db.CerrarConexion();

            Session["AgentesLocalesNuestros"] = dt;

        }

        private string ObtenerAgenteLocalesNuestros(bool msj = false)
        {
            string ret = String.Empty;
            const string SEPARADOR_1 = "~";
            const string SEPARADOR_2 = "/";

            string matriculas = String.Empty;
            string iniciales = String.Empty;

            DataTable dt = (DataTable)Session["AgentesLocalesNuestros"];

            foreach (DataRow row in dt.Rows)
			{
                if (msj)
                {
                    iniciales += (iniciales != String.Empty ? SEPARADOR_2 : String.Empty) + row["Iniciales"].ToString();
                }
                else
                {
                    iniciales += (iniciales != String.Empty ? SEPARADOR_1 : String.Empty) + row["Iniciales"].ToString();
                    matriculas += (matriculas != String.Empty ? SEPARADOR_1 : String.Empty) + row["MatriculaNro"].ToString();
                }
            }

            if (msj)
                ret += iniciales;
            else
                ret += iniciales + SEPARADOR_1 + matriculas;

            return ret;
        }
		#endregion Validar

		#region ListaDeIdSeleccionados
		private string ListaDeIdSeleccionados( )
		{
			string ID_List = "";
			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			foreach( DataGridItem item in dgResult.Items )
			{
				#region Extraer valores de la grilla

				string chkSel			= dgw.GetText( item, "chkSel" );
				string lblDenominacion	= dgw.GetText( item, "lblDenominacion" );
				string lblExpeID		= dgw.GetText( item, "lblExpeID" );
				string lblError =  dgw.GetText( item, "lblError" );
				#endregion Extraer valores de la grilla
				
				if( lblError == "OK" && chkSel == "Si" )
				{
					ID_List += ( ID_List  == "" ?"":"," ) + lblExpeID ;
				}
			}
			return ID_List;
		}
		#endregion ListaDeIdSeleccionados

		#region btnGrabar
		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			string mensaje="";
			// Recalculamos automáticamente las fechas de vencimiento
			// mbaez 16/11/2006.
			if (btnCalcVencim.Visible) 
			{
				this.calcularFechaVenc();
			}

			if( btnGrabar.Text == "Grabar" )
			{
				if( ! Validar( out mensaje ) )
				{ 
					ShowMessage( mensaje );
					return;
				}

				CambiarSituacion( );
			}
			else  // Copia los IDs Validos
			{
				txtIdentificadores.Text = ListaDeIdSeleccionados();
				this.ddlIdentificador.SelectedIndex = 4;
				this.pnlGrillas.Visible = false;
				btnGrabar.Visible = false;
				BuscarMarcas( txtIdentificadores.Text );
			}
		}
		#endregion btnGrabar

		#region btnReplicar_Click
		protected void btnReplicar_Click(object sender, System.EventArgs e)
		{
			if( SituacionID == (int) GlobalConst.Situaciones.PRESENTADA )
			{
				if( ddlReplicar.SelectedIndex == 0 )
				{ // Fecha
					if ( this.txtReplicar.Text.IndexOf(":") == -1 )
					{
						ShowMessage("Debe incluir la hora además de la Fecha. Ej: 23/07/2006 8:34 ");
						return;
					}
				}
			}

			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			ConfigurarGrilla( dgw );
			foreach( DataGridItem item in dgResult.Items )
			{
				TextBox txt = (TextBox) item.FindControl( ddlReplicar.SelectedValue );
				txt.Text = this.txtReplicar.Text;

				if( ddlReplicar.SelectedValue == "txtRegistro" )
				{
					this.Registro = this.txtReplicar.Text;  // p/ Persistencia 
				}
			
				if( ddlReplicar.SelectedValue == "txtFecha" )  // p/ Persistencia 
				{
					this.Fecha = this.txtReplicar.Text;

					#region Asignar Fecha de Vencimiento
					if( this.SituacionID == (int) GlobalConst.Situaciones.CONCEDIDA  )
					{				
						string lblExpeID		= dgw.GetText( item, "lblExpeID" );
						#region Ubicar el expediente en cuestion
						Berke.DG.DBTab.Expediente expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( int.Parse(lblExpeID)); 
						#endregion Ubicar el expediente en cuestion
					
						DateTime fecha = ObjConvert.AsDateTime( this.txtReplicar.Text );
						DateTime vencimRegistro;
						vencimRegistro = VencimientoDeTitulo( expe, fecha );
						dgw.Set(item, "txtVencimTitulo", vencimRegistro.ToString() );
					}
					#endregion Asignar Fecha de Vencimiento
				}
			
			}
		}

		#endregion btnReplicar_Click


		#region ddlReplicar_SelectedIndexChanged
		protected void ddlReplicar_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.txtReplicar.Text = "";
			if( ddlReplicar.SelectedValue == "txtFecha" )
			{
				this.txtReplicar.Text = this.Fecha;
			}
			if( ddlReplicar.SelectedValue == "txtRegistro" )
			{
				this.txtReplicar.Text = this.Registro;
			}

		}
		#endregion ddlReplicar_SelectedIndexChanged

		#region VencimientoDeTitulo()
		private DateTime VencimientoDeTitulo( Berke.DG.DBTab.Expediente expe, DateTime fecha )
		{
	
			DateTime vencimRegistro;
			if( expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO )// Registro
			{
				vencimRegistro = fecha.AddYears( 10 );
			}
			else
			{
				Berke.DG.DBTab.Expediente expePadre = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( expe.Dat.ExpedienteID.AsInt); 
				vencimRegistro = expePadre.Dat.VencimientoFecha.AsDateTime.AddYears( 10 );
			}
			return vencimRegistro;
	
		}
		#endregion VencimientoDeTitulo()

		#region CambiarSituacion
		private void CambiarSituacion( )
		{
			int procesados=0;
			int it = 0;
			string exitos = "";
			string fallos = "";

			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			Berke.Libs.WebBase.Helpers.GridGW dgwProceso = new GridGW( dgProceso );
			this.ConfigurarGrillaProceso( dgwProceso );
			foreach( DataGridItem item in dgResult.Items )
			{

				#region Extraer valores de la grilla

				string chkSel			= dgw.GetText( item, "chkSel" );
				string lblDenominacion	= dgw.GetText( item, "lblDenominacion" );
				string lblExpeID		= dgw.GetText( item, "lblExpeID" );
				string lblClase			= dgw.GetText( item, "lblClase" );
				string lblTramiteAbrev	= dgw.GetText( item, "lblTramiteAbrev" );

				string lblRegistro		= dgw.GetText( item, "lblRegistro" );
				string lblActa			= dgw.GetText( item, "lblActa" );
				string txtFecha			= dgw.GetText( item, "txtFecha" );
				string txtFinPlazo		= dgw.GetText( item, "txtFinPlazo" );
				string txtBibExp		= dgw.GetText( item, "txtBibExp" );
				string txtRegistro		= dgw.GetText( item, "txtRegistro" );
				string txtActa			= dgw.GetText( item, "txtActa" );
				string txtVencimTitulo	= dgw.GetText( item, "txtVencimTitulo" );
				string txtPagAnio		= dgw.GetText( item, "txtPagAnio" );
				string txtDiario		= dgw.GetText( item, "txtDiario" );
				string txtAgLocal		= dgw.GetText( item, "txtAgLocal" );
				string lblTramiteSitID	= dgw.GetText( item, "lblTramiteSitID" );
				string txtObs			= dgw.GetText( item, "txtObs" );

				#endregion Extraer valores de la grilla

				if( chkSel != "Si" )
				{
					continue;
				}

				#region Ubicar el expediente en cuestion
				Berke.DG.DBTab.Expediente expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( int.Parse(lblExpeID)); 
				#endregion Ubicar el expediente en cuestion

				#region Ubicar TramiteSit
				int tramiteSitID = 0;
				Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit();
				if( this.ddlTramiteSitDestino.Visible )
				{
					tramiteSitID = int.Parse( this.ddlTramiteSitDestino.SelectedValue );
					//					trmSit = Berke.Marcas.UIProcess.Model.TramiteSit.ReadByID_M( expe.Dat.TramiteSitID.AsInt );
					trmSit = Berke.Marcas.UIProcess.Model.TramiteSit.ReadByID_M( tramiteSitID );
				}
				else
				{ // Solo se indica la situacion destina
					Berke.DG.DBTab.Tramite_Sit param = new Berke.DG.DBTab.Tramite_Sit();
					param.DisableConstraints();
					param.NewRow();
					param.Dat.TramiteID.Value		= expe.Dat.TramiteID.AsInt;
					param.Dat.SituacionID.Value		= int.Parse( this.ddlSituacionDestino.SelectedValue );
					param.PostNewRow();
					trmSit = Berke.Marcas.UIProcess.Model.TramiteSit.ReadByParam( param );
				}
				#endregion Ubicar TramiteSit 



				#region Obtener  BibExp
				string []aBibExp = txtBibExp.Split( ((string)"/").ToCharArray() );
				int bib		= 0;
				int exp	= 0;
				if( aBibExp.Length == 2 )
				{
					bib = ObjConvert.AsInt( aBibExp[0] );
					exp = ObjConvert.AsInt( aBibExp[1] );
				}
				#endregion Obtener  BibExp
		
				#region Obtener  PagAnio
				string []aPagAnio = txtPagAnio.Split( ((string)"/").ToCharArray() );
				int pagNro	= 0;
				int pagAnio	= 0;
				if( aPagAnio.Length == 2 )
				{
					pagNro	= ObjConvert.AsInt( aPagAnio[0] );
					pagAnio = ObjConvert.AsInt( aPagAnio[1] );
				}
				#endregion  Obtener PagAnio

				#region Obtener Agente Local
                //int agenteLocalID = 0;
                //switch( txtAgLocal.Trim().ToUpper())
                //{
                //    case "HT" :
                //    case "HTB" :
                //    case "6" :
                //        agenteLocalID = 6;
                //        break;
                //    case "MB" :
                //    case "7" :
                //        agenteLocalID = 7;
                //        break;
                //    case "LCS" :
                //    case "8" :
                //        agenteLocalID = 8;
                //        break;
                //    case "HB" :
                //    case "9" :
                //        agenteLocalID = 9;
                //        break;
                //    case "AVB" :
                //    case "3717" :
                //        agenteLocalID = 3061;
                //        break;
                //    case "AB" :
                //    case "3297" :
                //        agenteLocalID = 3011;
                //        break;

                //}
                DataTable dt = (DataTable)Session["AgentesLocalesNuestros"];

                int agenteLocalID = 0;
                if (txtAgLocal.Trim() != String.Empty)
                {
                    string selectString = String.Empty;
                    if (Int32.TryParse(txtAgLocal.Trim(), out agenteLocalID))
                    {
                        selectString = "MatriculaNro = " + txtAgLocal.Trim().ToUpper();
                        //rows = dt.Select("MatriculaNro = " + txtAgLocal.Trim().ToUpper());
                    }
                    else
                    {
                        selectString = "Iniciales = '" + txtAgLocal.Trim().ToUpper() + "'";
                    }

                    DataRow[] rows = dt.Select(selectString);

                    agenteLocalID = Convert.ToInt32(rows[0]["ID"].ToString());
                }

				#endregion Obtener Agente Local


				#region Obtener DiarioID
				Berke.DG.DBTab.Diario diario = new Berke.DG.DBTab.Diario();
				if( txtDiario != "" )
				{
					diario = Berke.Marcas.UIProcess.Model.Diario.ReadByAbrev( txtDiario );
				}
				#endregion Obtenr DiarioID

				#region Calcular Vencimiento
				DateTime fecha		= DateTime.Parse( txtFecha );
				DateTime fVencim	= Berke.Marcas.UIProcess.Model.TramiteSit.FechaVencim( fecha, trmSit.Dat.ID.AsInt );
				#endregion Calcular Vencimiento
	
				#region Asignar Parametros
		
				// CambioSitParam
				Berke.DG.ViewTab.CambioSitParam inTB =   new Berke.DG.ViewTab.CambioSitParam();

				int anio = DateTime.Parse(txtFecha).Year;
				inTB.NewRow(); 
				inTB.Dat.ExpedienteID		.Value = lblExpeID; 
				inTB.Dat.TramiteSitDestinoID.Value = lblTramiteSitID;
				inTB.Dat.SitFecha			.Value = txtFecha;
				inTB.Dat.SitHora			.Value = "7:00";
				inTB.Dat.Plazo				.Value = trmSit.Dat.Plazo.AsInt;
				inTB.Dat.UnidadID			.Value = trmSit.Dat.UnidadID.AsInt;
				inTB.Dat.SitVencim			.Value = fVencim;
				inTB.Dat.AgenteLocalID		.Value = agenteLocalID;
				inTB.Dat.NroActa			.Value = txtActa;
				inTB.Dat.AnioActa			.Value = anio;
				inTB.Dat.NroRegistro		.Value = txtRegistro;  
				inTB.Dat.AnioRegistro		.Value = anio;
				inTB.Dat.DiarioID			.Value = diario.Dat.ID.Value;
				inTB.Dat.PublicPagina		.Value = pagNro;
				inTB.Dat.PublicAnio			.Value = pagAnio;
				// Archivada
				inTB.Dat.Bib				.Value = bib; 
				inTB.Dat.Exp				.Value = exp;   
				// -- 
				inTB.Dat.RegVencim			.Value = txtVencimTitulo;
		
				inTB.Dat.Obs				.Value = txtObs;

				inTB.PostNewRow(); 
		
	
				#endregion Asignar Parametros
		
				#region Invocar Model
				try
				{
					Berke.DG.ViewTab.ParamTab outTB =  Berke.Marcas.UIProcess.Model.Expediente.CambiarSituacion( inTB );
					procesados++;
					exitos =  txtActa.ToString() + " - " ;
					dgwProceso.Set(it, "chkpSel", "S");

                    Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
                    db.DataBaseName = WebUI.Helpers.MyApplication.CurrentDBName;
                    db.ServerName = WebUI.Helpers.MyApplication.CurrentServerName;
                    Berke.Libs.Boletin.Services.ExpedienteService expeService = new Libs.Boletin.Services.ExpedienteService(db);
                    int tramSitID = expeService.getTramiteSitID((int)GlobalConst.Situaciones.PUBLICADA, trmSit.Dat.TramiteID.AsInt);
                    db.CerrarConexion();

					if (Convert.ToInt32(lblTramiteSitID) == tramSitID)
					{
						this.NotificarPublicacion(expe, fecha, fVencim);
					}
				}
				catch( Exception ex )
				{
					//this.ShowMessage("ERROR: "+ ex.Message );
					fallos =  ex.Message + " - " ;
					dgwProceso.Set(it, "lblpError", fallos  );
					
					//return;
				}
				#endregion

				OcultarResultado();

				
				dgwProceso.Set(it, "lblpExpeID", lblExpeID.ToString());
				dgwProceso.Set(it, "lblpClase", lblClase.ToString() );
				dgwProceso.Set(it, "lblpTramiteAbrev", lblTramiteAbrev.ToString() );
				dgwProceso.Set(it, "lblpRegistro", lblRegistro.ToString());
				dgwProceso.Set(it, "lblpActa", txtActa.ToString() );
				dgwProceso.Set(it, "lblpFecSit", fecha.ToShortDateString());
				dgwProceso.Set(it, "lblpVencSit", fVencim.ToShortDateString());
				
				string denom = lblDenominacion.ToString();
				if( denom.Trim() == "" )
				{
					denom = "???";
				}

				dgwProceso.Set(it, "lblpDenominacion", denom );
				
				
				it++;

				

			} //?? end foreach para extraer datos de la grilla

			pnlProceso.Visible= true;
			dgProceso.Visible = true;
			//this.ShowMessage("Resumen del Proceso: Exitos  " + exitos + " Fallos " + fallos );
			
		}
		#endregion CambiarSituacion

		private void MostrarResultado()
		{
			this.pnlReplicar.Visible = true;
			this.pnlGrillas.Visible = true;
			this.btnGrabar.Visible = true;
		}

		private void OcultarResultado()
		{
			this.pnlReplicar.Visible = false;
			this.pnlGrillas.Visible = false;
			this.btnGrabar.Visible = false;
		}
		#region Calcular Vencimiento
		protected void btnCalcVencim_Click(object sender, System.EventArgs e)
		{
			this.calcularFechaVenc();
		}
		
		private void calcularFechaVenc() 
		{
			Berke.Libs.WebBase.Helpers.GridGW dgw = new GridGW( dgResult );
			ConfigurarGrilla( dgw );
			foreach( DataGridItem item in dgResult.Items )
			{			
				#region Extraer valores de la grilla

				string chkSel			= dgw.GetText( item, "chkSel" );
				string lblDenominacion	= dgw.GetText( item, "lblDenominacion" );
				string lblExpeID		= dgw.GetText( item, "lblExpeID" );
				string lblClase			= dgw.GetText( item, "lblClase" );

				string lblTramiteAbrev	= dgw.GetText( item, "lblTramiteAbrev" );

				string lblRegistro		= dgw.GetText( item, "lblRegistro" );
				string lblActa			= dgw.GetText( item, "lblActa" );
				string txtFecha			= dgw.GetText( item, "txtFecha" );
				string txtFinPlazo		= dgw.GetText( item, "txtFinPlazo" );
				string txtBibExp		= dgw.GetText( item, "txtBibExp" );
				string txtRegistro		= dgw.GetText( item, "txtRegistro" );
				string txtActa			= dgw.GetText( item, "txtActa" );
				string txtVencimTitulo	= dgw.GetText( item, "txtVencimTitulo" );
				string txtPagAnio		= dgw.GetText( item, "txtPagAnio" );
				string txtDiario		= dgw.GetText( item, "txtDiario" );
				string txtAgLocal		= dgw.GetText( item, "txtAgLocal" );

				#endregion Extraer valores de la grilla
				#region Ubicar el expediente en cuestion
				Berke.DG.DBTab.Expediente expe = Berke.Marcas.UIProcess.Model.Expediente.ReadByID( int.Parse(lblExpeID)); 
				#endregion Ubicar el expediente en cuestion
				#region Ubicar TramiteSit
				Berke.DG.DBTab.Tramite_Sit trmSit = Berke.Marcas.UIProcess.Model.TramiteSit.ReadByID_M( expe.Dat.TramiteSitID.AsInt );
				#endregion Ubicar TramiteSit 
				DateTime fecha = ObjConvert.AsDateTime( txtFecha );
				DateTime vencimRegistro;
				vencimRegistro = VencimientoDeTitulo( expe, fecha );
				dgw.Set(item, "txtVencimTitulo", vencimRegistro.ToString() );
			}
		}

		#endregion Calcular Vencimiento

		private void NotificarPublicacion(Berke.DG.DBTab.Expediente expe, DateTime fecha, DateTime fechaVencim)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca(db);
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			Berke.DG.DBTab.Expediente_Instruccion expeInstr = new Berke.DG.DBTab.Expediente_Instruccion(db);

			DateTime fecPosibleOPO;
			bool enviarNotificacion = false;
			string BolNro = "";
			string BolAnio = "";
			string BolCarpeta = "";
			string AvisoID = "";

			expeInstr.ClearFilter();
			expeInstr.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expeInstr.Dat.InstruccionTipoID.Filter = (int)GlobalConst.InstruccionTipo.NOTIFICAR_PUBLICACION;
			expeInstr.Adapter.ReadAll();

			if (expeInstr.RowCount > 0)
			{
				enviarNotificacion = true;
			}
			else
			{
				#region Instruccion de OPO a verificar
				DSFilter dsfilter = new DSFilter();
				dsfilter.AddToList(GlobalConst.InstruccionTipo.NO_ENVIAR_AVISOS_OPO);
				dsfilter.AddToList(GlobalConst.InstruccionTipo.NO_ENVIAR_2DO_AVISOS_OPO);
				dsfilter.AddToList(GlobalConst.InstruccionTipo.INSTRUCCION_DE_OPO);
				dsfilter.AddToList(GlobalConst.InstruccionTipo.NO_OPO);
				dsfilter.AddToList(GlobalConst.InstruccionTipo.OPOSICION_CON_OTRO_AGENTE);
				#endregion Instruccion de OPO a verificar
				
				Berke.DG.ViewTab.vAvisoInstruccion aviInstruccion = new Berke.DG.ViewTab.vAvisoInstruccion(db);
				aviInstruccion.ClearFilter();
				aviInstruccion.ClearOrder();
				aviInstruccion.Dat.MarcaSolID.Filter = expe.Dat.MarcaID.AsInt;
				aviInstruccion.Dat.InstruccionTipoID.Filter = (int)GlobalConst.InstruccionTipo.POSIBLE_OPO;
				aviInstruccion.Dat.FecAlta.Order = -1;
				aviInstruccion.Adapter.ReadAll();

				if (aviInstruccion.RowCount > 0)
				{
					BolNro = aviInstruccion.Dat.BolNro.AsString;
					BolAnio = aviInstruccion.Dat.BolAnio.AsString;
					BolCarpeta = aviInstruccion.Dat.BolCarpeta.AsString;
					AvisoID = aviInstruccion.Dat.AvisoOpoCabID.AsString;

					aviInstruccion.GoTop();
					fecPosibleOPO = aviInstruccion.Dat.FecAlta.AsDateTime;

					aviInstruccion.ClearFilter();
					aviInstruccion.ClearOrder();
					aviInstruccion.Dat.MarcaSolID.Filter = expe.Dat.MarcaID.AsInt;
					aviInstruccion.Dat.InstruccionTipoID.Filter = dsfilter;
					aviInstruccion.Dat.FecAlta.Order = -1;
					aviInstruccion.Adapter.ReadAll();

					if (aviInstruccion.RowCount > 0)
					{
						enviarNotificacion = true;
						
						for (aviInstruccion.GoTop(); !aviInstruccion.EOF; aviInstruccion.Skip())
						{
							if (aviInstruccion.Dat.FecAlta.AsDateTime > fecPosibleOPO)
							{
								enviarNotificacion = false;
								break;
							}
						}
					}
					else
					{
						enviarNotificacion = true;
					}
				}
			}

			if (enviarNotificacion)
			{
				mar.ClearFilter();
				mar.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);

				clase.ClearFilter();
				clase.Adapter.ReadByID(mar.Dat.ClaseID.AsInt);

				string notif =	"<b><u>Datos de la Marca</u></b><br>" +
					"<b>Denominación: </b>" + mar.Dat.Denominacion.AsString + "<br>" +
					"<b>Acta: </b>" + expe.Dat.ActaNro.AsString + "/" + 
					expe.Dat.ActaAnio.AsString + "<br>" +
					"<b>Clase: </b>" + clase.Dat.Nro.AsString + "<br>" +
					"<b>Fecha de Solicitud: </b>" + expe.Dat.PresentacionFecha.AsString + "<br> " +
					"<font color='red'><b>Fecha de Publicación: </b>" + fecha.ToShortDateString() + "<br></font>" +
					"<font color='red'><b>Fecha Venc. Publicación: </b>" + fechaVencim.ToShortDateString() + "<br></font>" +
					"<b>Boletín: </b>" + BolNro + "/" + BolAnio + "<br>" +
					"<b>Carpeta Nro: </b>" + BolCarpeta + "<br>" +
					"<font color='red'><b>Aviso ID: </b>" + AvisoID + "<br></font>";
				Berke.Libs.Boletin.Libs.Utils.setBodyHTMLFormat(true);
                Berke.Libs.Boletin.Libs.Utils.enviarNotificacion(Berke.Libs.Boletin.Libs.Utils.NOTIF_PUB_MARCA_VIG, notif, db);
                
			}

			db.CerrarConexion();
			
		}	
	
	}
	


}
