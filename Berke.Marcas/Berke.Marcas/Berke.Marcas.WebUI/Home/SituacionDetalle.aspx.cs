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
using Berke.Libs.Base;

namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for SituacionDetalle.
	/// </summary>
	public partial class SituacionDetalle : System.Web.UI.Page
	{
		#region Controles del Form

		#endregion Controles del Form

		#region situacionTab Push/POP

		#region Push_situacionTab

		private void Push_situacionTab ()
		{
			Session["situacionTab"] = situacionTab.Table;
		}

		#endregion Push_situacionTab

		#region Pop_situacionTab

		private void Pop_situacionTab()
		{
			situacionTab = new Berke.DG.DBTab.Situacion ((DataTable) Session["situacionTab"] );
		}

		#endregion Pop_situacionTab

		#endregion situacionTab Push/POP

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

		private Berke.DG.DBTab.Situacion situacionTab;

		private int situacionID;

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

			situacionTab = new Berke.DG.DBTab.Situacion(db);

			situacionTab.Adapter.ReadByID( situacionID );
			
			this.Push_situacionTab();
	
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
					string paramSituacionID = UrlParam.GetParam("SituacionID");
					string paramMode		= UrlParam.GetParam("Mode");

					if( paramSituacionID == "" && paramMode != "Insert" )
					{ 
						throw new Exception("ID de situacion Nulo");
					}
					else
					{
						situacionID = ObjConvert.AsInt( paramSituacionID );					
					}

					if( paramMode == "" )
					{ 
						Mode = "Query";
					}
					else
					{
						Mode = paramMode;					
					}
					
					#endregion Obtener Parametros de QueryString

					SetMode( Mode );
					AsignarValoresIniciales();
				}
				else
				{
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
			this.Pop_situacionTab();
			txtID.Text = situacionTab.Dat.ID.AsString;
			txtDescrip.Text = situacionTab.Dat.Descrip.AsString;
			txtAbrev.Text = situacionTab.Dat.Abrev.AsString;
		}	
		#endregion Asignar valores a los controles

		#region Asignar valores a tabla
		private void AsignarValoresATabla()
		{		
//			situacionTab.Dat.ID.Value		= txtID.Text ;
			situacionTab.Dat.Descrip.Value	= txtDescrip.Text;
			situacionTab.Dat.Abrev.Value	= txtAbrev.Text;
		}	
		#endregion Asignar valores a tabla
	

		#region Vaciar Controles
		private void VaciarControles()
		{		
			txtID.Text		= "";
			txtDescrip.Text	= "";
			txtAbrev.Text	= "";
		}	
		#endregion Vaciar Controles

		#region SetReadOnly_ON
		private void SetReadOnly_ON(){
			txtID.ReadOnly		= true;
			txtDescrip.ReadOnly	= true;
			txtAbrev.ReadOnly	= true;

			txtID.BackColor			= System.Drawing.Color.WhiteSmoke;
			txtDescrip.BackColor	= System.Drawing.Color.WhiteSmoke;
			txtAbrev.BackColor		= System.Drawing.Color.WhiteSmoke;
		}
		#endregion

		#region SetReadOnly_OFF
		private void SetReadOnly_OFF()
		{
			txtID.ReadOnly		= true;
			txtDescrip.ReadOnly	= false;
			txtAbrev.ReadOnly	= false;

			txtDescrip.BackColor	= System.Drawing.Color.White;
			txtAbrev.BackColor		= System.Drawing.Color.White;
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
					SetReadOnly_OFF();
					lblDestino.ForeColor = System.Drawing.Color.Black;
					lblDestino.Text = "Modificar Registro";
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
			
			if (lblVolver.Text == "/Berke.Marcas.WebUI/Home/SituacionConsulta.aspx")
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
					this.Pop_situacionTab();
					situacionTab.DeleteAllRows();
					situacionTab.NewRow();
					AsignarValoresATabla();
					situacionTab.PostNewRow();

					situacionTab.InitAdapter( db );
					db.IniciarTransaccion();

					int newID =  situacionTab.Adapter.InsertRow();
					db.Commit();

					situacionTab.Adapter.ReadByID( newID );
			
					this.Push_situacionTab();

					break;

				case "Delete" :
					this.Pop_situacionTab();
					
					situacionTab.InitAdapter( db );
					db.IniciarTransaccion();

					situacionTab.Adapter.DeleteRow();
					db.Commit();
					Response.Redirect( PaginaPrevia, true );

					break;
				case "Modify" :
					this.Pop_situacionTab();
					
					situacionTab.Edit();
					AsignarValoresATabla();
					
					situacionTab.PostEdit();

					situacionTab.InitAdapter( db );
					db.IniciarTransaccion();
					
					situacionTab.Adapter.UpdateRow();
					db.Commit();

					break;
			}// end switch
			
	
			SetMode("Query");
		

			#region Cerrar Conexion
			db.CerrarConexion();
			#endregion
		}

	}// End Class 
}
