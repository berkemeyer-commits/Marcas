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
using Berke.Libs.Base;
using Berke.Libs.Base.DSHelpers;
using Berke.Marcas.WebUI.Helpers;
using System.Security.Principal;

namespace Berke.Marcas.WebUI.Home
{
	using UIPModel = UIProcess.Model;
	//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using Berke.Marcas.WebUI.Tools.Helpers;

	public partial class CtrlGenHDesc : System.Web.UI.Page
	{
		#region Controles del Web Form

















		protected System.Web.UI.WebControls.Label lblSustitida;
		#endregion 
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{

			this.cbxAgenteLocalID	.LoadRequested	+= new ecWebControls.LoadRequestedHandler(this.cbxAgenteLocalID_LoadRequested);
            this.cbxUsuarioID.LoadRequested += new ecWebControls.LoadRequestedHandler(this.cbxUsuarioID_LoadRequested);

			

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
		}


		#endregion Tramite DropDown


		
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				MostrarPanel_Busqueda();
                AsignarValoresIniciales();
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
			Buscar();			
		}

		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Hojas Descriptivas Generadas";
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

		#region Carga de Combo
        #region AgenteLocal
		private void cbxAgenteLocalID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
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
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. AgenteLocal.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
		#endregion AgenteLocal		

        #region Usuario
        private void cbxUsuarioID_LoadRequested(ecWebControls.ecCombo combo, EventArgs e)
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
			
			Berke.DG.ViewTab.ListTab outTB = Berke.Marcas.UIProcess.Model. Funcionario.ReadForSelect(inTB );
			combo.BindDataSource(outTB.Table, outTB.Dat.Descrip.Name, outTB.Dat.ID.Name );
		}
        #endregion Usuario

        #endregion Carga de Combo


        private object GetFilter_Str( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			if( cadena.IndexOf("*") != -1 )
			{
				cadena = cadena.Replace("*","%");
			}
			else
			{
				cadena+= "%";
			}
			return cadena;
		}

		private object GetFilter_Bool( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			bool ret = false;
			switch( cadena.ToUpper() )
			{
				case "SI":
				case "TRUE":
				case "1":
					ret = true;
					break;
				case "NO":
				case "False":
				case "0":
					ret = false;
					break;
			}
			return ret;
		}

		private object GetFilter( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo

			#region Rango
			if( cadena.IndexOf("-") != -1 )
			{
				string [] aVal = cadena.Split( ((String)"-").ToCharArray() );
				string min = aVal[0].Trim();
				string max = aVal[1].Trim();
				if( min != "" && max != "")
				{
					return new DSFilter( (object)min, (object)max );
				}
			}
			#endregion Rango

			#region Lista
			if( cadena.IndexOf(",") != -1 )
			{
				return new DSFilter( new ArrayList(cadena.Split( ((String)",").ToCharArray()) ));
			}
			#endregion Lista

			return new DSFilter( cadena );
		
		}

		private void Buscar()
		{

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

            Berke.DG.ViewTab.vCtrlGenHDesc vchd = new DG.ViewTab.vCtrlGenHDesc(db);

            #region Filtros
            vchd.ClearFilter();
            vchd.Dat.ExpedienteID.Filter = GetFilter(this.txtExpedienteID_min.Text);
            vchd.Dat.ActaNro.Filter = GetFilter(this.txtActaNro_min.Text);
            vchd.Dat.HINro.Filter = GetFilter(this.txtHINro.Text);
            vchd.Dat.HIAnio.Filter = GetFilter(this.txtActaAnio.Text);
            vchd.Dat.ActaAnio.Filter = GetFilter(this.txtActaAnio.Text);
            vchd.Dat.Denominacion.Filter = GetFilter_Str(this.txtDenominacion.Text);
            vchd.Dat.TramiteID.Filter = GetFilter(ddlTramiteID.SelectedValue);
            vchd.Dat.AgenteLocalID.Filter = GetFilter(this.cbxAgenteLocalID.SelectedValue);
            vchd.Dat.FuncionarioID.Filter = GetFilter(this.cbxUsuarioID.SelectedValue);
            vchd.Dat.FechaHoraGeneracion.Filter = ObjConvert.GetFilter_Date(this.txtFechaGeneracion.Text);
            #endregion Filtros
            
            #region Obtener datos
			int recuperados = -1;
			try
			{
                try 
				{
					vchd.Dat.FechaHoraGeneracion.Order = 1;
                    vchd.Dat.HINro.Order = 2;
                    vchd.Dat.HIAnio.Order = 3;

					recuperados = vchd.Adapter.Count();
					if( recuperados < 2000 )
					{
						recuperados = -1;
						string bf = vchd.Adapter.ReadAll_CommandString();
						vchd.Adapter.ReadAll( 2000 );
					}
				}
				catch ( Berke.Excep.Biz.TooManyRowsException ex )
				{	
					recuperados = ex.Recuperados;
				}
				catch( Exception exep ) 
				{
					throw new Exception("Class: CtrlGenHDesc ", exep );
				}
				#endregion Obtener datos

                #region Asignar DataSource de Resultados
                dgResult.DataSource = vchd.Table;
                dgResult.DataBind();
                #endregion Asignar DataSource de Resultados

                #region Mostrar Panel segun cantidad de registros obtenidos
                if (recuperados != -1)
                {
                    MostrarPanel_Busqueda();
                    lblMensaje.Text = "Excesiva cantidad de registros(" + recuperados.ToString() + ")";
                }
                else if (vchd.RowCount == 0)
                {
                    MostrarPanel_Busqueda();
                    lblMensaje.Text = "No se encontraron registros";
                }
                else
                {
                    lblTituloGrid.Text = "Hojas Descriptivas Generadas&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(" + vchd.RowCount + "&nbsp;regs.)";
                    MostrarPanel_Resultado();
                }
                #endregion
            }
            finally
            {
                db.CerrarConexion();
            }
		}
	
        protected void btnGenDoc_Click(object sender, System.EventArgs e)
		{
			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.word";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dgResult.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();			
		}

		protected void btnGenXls_Click(object sender, System.EventArgs e)
		{
			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dgResult.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();			
		}

		private string GetListaSituaciones(int tramiteid, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Tramite_Sit TramSit = new Berke.DG.DBTab.Tramite_Sit(db);
			TramSit.ClearFilter();
			TramSit.Dat.SituacionID.Filter = (int)GlobalConst.Situaciones.CONCEDIDA;

			if (tramiteid > 0)
			{
				TramSit.Dat.TramiteID.Filter = tramiteid;
			}
			else
			{
				TramSit.Dat.TramiteID.Filter = ObjConvert.GetFilter(Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + ',' + Convert.ToInt32(GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString());
			}

			TramSit.Adapter.ReadAll();

			string lista = "";
			for (TramSit.GoTop(); !TramSit.EOF; TramSit.Skip())
			{
				if (lista != "")
				{
					lista += ",";
				}
				lista += TramSit.Dat.ID.AsString;
			}
			return lista;
		}

		
		


	} // end class ExpeMarcaConsulta
} // end namespace Berke.Marcas.WebUI.Home


