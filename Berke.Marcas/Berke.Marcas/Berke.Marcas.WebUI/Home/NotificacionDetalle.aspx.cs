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
using Berke.Marcas.WebUI.Tools.Helpers; // UrlParam

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for SituacionDetalle.
	/// </summary>
	public partial class NotificacionDetalle : System.Web.UI.Page
	{
		#region Controles del Form

			protected System.Web.UI.WebControls.TextBox txtID_min;

			
			

		#endregion Controles del Form

		#region notificacionTab Push/POP

		#region Push_notificacionTab

		private void Push_notificacionTab ()
		{
			Session["notificacionTab"] = notificacionTab.Table;
		}

		#endregion Push_notificacionTab

		#region Pop_notificacionTab

		private void Pop_notificacionTab()
		{
			notificacionTab = new Berke.DG.DBTab.Notificacion ((DataTable) Session["notificacionTab"] );
		}

		#endregion Pop_notificacionTab

		#endregion notificacionTab Push/POP

		#region Properties

		#region RoundTrip
		private int RoundTrip 
		{
			get{ return Convert.ToInt32( ViewState["roundTrip"] ); }
			set{ ViewState["roundTrip"] = value.ToString(); }
		}
		#endregion RoundTrip

		#region PaginaPrevia
		private string PaginaPrevia 
		{
			get{ return (string) ViewState["PaginaPrevia"] ; }
			set{ ViewState["PaginaPrevia"] = value.ToString(); }
		}
		#endregion PaginaPrevia

		#region Mode
			private string Mode 
			{
				get{ return (string) Session["mode"];}
				set{ Session["mode"] = value; }
			}
		#endregion Mode
		
		#endregion Properties

		#region Variables globales

			private Berke.DG.DBTab.Notificacion notificacionTab;
			private int notificacionID;

		#endregion Variables globales

		
		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
			#region Abrir Conexion
				//--- @@@ ---- Abrir conexion con la base de datos
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
				//----------------
			#endregion
//
//			#region Declarar tablas a ser accedidas
//				Berke.DG.DBTab.Situacion situacionTab	  = new Berke.DG.DBTab.Situacion(db);
//			#endregion Declarar tablas a ser accedidas

			#region Leer datos

				notificacionTab = new Berke.DG.DBTab.Notificacion(db);
				notificacionTab.Adapter.ReadByID( notificacionID );
				this.Push_notificacionTab();
		
			#endregion Leer datos
		
			AsignarValoresAControles();

			#region Cerrar Conexion
				db.CerrarConexion();
			#endregion
		}
		#endregion Asignar Valores Iniciales

	

		#region Page_Load

			protected void Page_Load(object sender, System.EventArgs e)
			{
				
				if( !IsPostBack )
				{
					lblVolver.Text = Request.UrlReferrer.LocalPath;
					RoundTrip = 1;
					PaginaPrevia = Request.UrlReferrer.PathAndQuery;

					#region Obtener Parametros de QueryString
					string paramMode		= UrlParam.GetParam("Mode");

					
					if( Request.QueryString["NotificacionID"] == null && paramMode != "Insert" )
					{ 
						throw new Exception("Id de Notificacion Nulo");
					}
					else
					{
						notificacionID = Convert.ToInt32( Request.QueryString["NotificacionID"] );					
					}

					if( Request.QueryString["Mode"] == null )
					{ 
						Mode = "Query";
					}
					else
					{
						Mode = Request.QueryString["Mode"];					
					}
					
					#endregion Obtener Parametros de QueryString
					SetMode( Mode );
					AsignarValoresIniciales();
				}
				else
				{
					//lblVolver.Text = "";
					RoundTrip++;
				}
				
				this.btnVolver.Attributes.Add("onclick", "history.go(-"+ RoundTrip.ToString() +"); return false;" );

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

		#region Asignar valores a los controles
		private void AsignarValoresAControles()
		{		
			this.Pop_notificacionTab();
			txtID.Text				= notificacionTab.Dat.ID.AsString;
			txtDescrip.Text			= notificacionTab.Dat.Descrip.AsString;
			txtMail_Destino.Text	= notificacionTab.Dat.Mail_Destino.AsString;
			txtFunc_Destino.Text	= notificacionTab.Dat.Func_Destino.AsString;
			txtActivo.Text			= notificacionTab.Dat.Activo.AsString;
			
		}	

		#endregion Asignar valores a los controles

		#region Asignar valores a tabla
		private void AsignarValoresATabla()
		{		
//			situacionTab.Dat.ID.Value		= txtID.Text ;
			notificacionTab.Dat.ID.Value			= txtID.Text;
			notificacionTab.Dat.Descrip.Value		= txtDescrip.Text;
			notificacionTab.Dat.Mail_Destino.Value	= txtMail_Destino.Text;
			notificacionTab.Dat.Func_Destino.Value	= txtFunc_Destino.Text;
			notificacionTab.Dat.Activo.Value		= txtActivo.Text;
 
			//txtActivo.
			//situacionTab.Dat.Abrev.Value	= txtAbrev.Text;
		}	
		#endregion Asignar valores a tabla
	

		#region Vaciar Controles
		private void VaciarControles()
		{		
			txtID.Text				= "";
			txtDescrip.Text			= "";
			txtMail_Destino.Text	= "";
			txtFunc_Destino.Text	= "";
			txtActivo.Text			= "";
			lblVolver.Text			= Request.UrlReferrer.LocalPath;
		}	

		#endregion Vaciar Controles

		#region SetReadOnly_ON
		private void SetReadOnly_ON(){
			txtID.ReadOnly					= true;
			txtDescrip.ReadOnly				= true;
			txtMail_Destino.ReadOnly		= true;
			txtFunc_Destino.ReadOnly		= true;
			txtActivo.ReadOnly				= true;

			txtID.BackColor					= System.Drawing.Color.WhiteSmoke;
			txtDescrip.BackColor			= System.Drawing.Color.WhiteSmoke;
			txtMail_Destino.BackColor		= System.Drawing.Color.WhiteSmoke;
			txtFunc_Destino.BackColor		= System.Drawing.Color.WhiteSmoke;
			txtActivo.BackColor				= System.Drawing.Color.WhiteSmoke;
		}
		#endregion

		#region SetReadOnly_OFF
		private void SetReadOnly_OFF()
		{
			txtID.ReadOnly				= true;
			txtDescrip.ReadOnly			= false;
			txtMail_Destino.ReadOnly	= false;
			txtFunc_Destino.ReadOnly	= false;
			txtActivo.ReadOnly			= false;

			txtDescrip.BackColor			= System.Drawing.Color.White;
			txtFunc_Destino.BackColor		= System.Drawing.Color.White;
			txtMail_Destino.BackColor		= System.Drawing.Color.White;
			txtActivo.BackColor				= System.Drawing.Color.White;
		}
		#endregion


		#region SetMode
		private void SetMode( string mode )
		{
			this.pnlMostrar.Visible = false;
			this.pnlEditar.Visible	= false;
			//lblAdvertencia.Visible  = false;
			Mode = mode;
			switch(mode )
			{
				case "Query" :
					SetReadOnly_ON();
					lblDestino.Text = " ";
					this.pnlMostrar.Visible = true;
					break;

				case "Insert" :
					VaciarControles();
					SetReadOnly_OFF();
					lblDestino.ForeColor = System.Drawing.Color.Black;
					lblDestino.Text = "Agregar Registro";
					this.pnlEditar.Visible	= true;
					break;

				case "Delete" :
					SetReadOnly_ON();
					lblDestino.ForeColor = System.Drawing.Color.Red;
					lblDestino.Text = "Eliminar Registro";
					this.pnlEditar.Visible	= true;
					break;
					
				case "Modify" :
					lblDestino.ForeColor = System.Drawing.Color.Black;
					lblDestino.Text = "Modificar Registro";
					SetReadOnly_OFF();
					this.pnlEditar.Visible	= true;
					break;
			}
		}
		#endregion SetMode

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			SetMode("Insert");
		}

		protected void btnModificar_Click(object sender, System.EventArgs e)
		{
			SetMode("Modify");
		}

		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			
			AsignarValoresAControles();
			SetMode("Delete");
		}

		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			if (lblVolver.Text == "/Berke.Marcas.WebUI/Home/NotificacionConsulta.aspx")
			{
				Response.Redirect( PaginaPrevia, true );
				//this.btnCancelar.Attributes.Add("onclick", "history.go(-"+ RoundTrip.ToString() +"); return false;" );
			}
			else
			{
				AsignarValoresAControles();
				SetMode("Query");
			}	
		}

		protected void btnConfirmar_Click(object sender, System.EventArgs e)
		{
			#region Abrir Conexion
			//--- @@@ ---- Abrir conexion con la base de datos
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;
			//----------------
			#endregion
			
			
			switch(Mode )
			{
				case "Insert" :
					this.Pop_notificacionTab();
					notificacionTab.DeleteAllRows();
					notificacionTab.NewRow();
					AsignarValoresATabla();
					notificacionTab.PostNewRow();
					
					
					notificacionTab.InitAdapter( db );
					db.IniciarTransaccion();

					int newID =  notificacionTab.Adapter.InsertRow();
					db.Commit();

					notificacionTab.Adapter.ReadByID( newID );
			
					this.Push_notificacionTab();

					break;

				case "Delete" :
					this.Pop_notificacionTab();
					
					notificacionTab.InitAdapter( db );
					db.IniciarTransaccion();

					notificacionTab.Adapter.DeleteRow();
					db.Commit();
					Response.Redirect( PaginaPrevia, true );

					break;
				case "Modify" :
					this.Pop_notificacionTab();
					notificacionTab.Edit();
					AsignarValoresATabla();
					notificacionTab.PostEdit();
					notificacionTab.InitAdapter( db );
					db.IniciarTransaccion();
					
					notificacionTab.Adapter.UpdateRow();
					db.Commit();

					break;
			}// end switch
			
	
			SetMode("Query");
		

			#region Cerrar Conexion
			db.CerrarConexion();
			#endregion
		}

		protected void btnVolver_Click(object sender, System.EventArgs e)
		{
		
		}

	}// End Class 
}
