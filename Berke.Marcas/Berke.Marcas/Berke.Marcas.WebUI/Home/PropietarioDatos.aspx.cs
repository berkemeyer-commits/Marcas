
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
	using UIPModel = UIProcess.Model;
//	using UIPModelEntidades = Berke.Entidades.UIProcess.Model;
	using BizDocuments.Marca;
	using Helpers;
//	using Berke.Marcas.BizActions;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;

	public partial class PropietarioDatos : System.Web.UI.Page
	{
		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();

        #region Controles Web

		



		
		
		#endregion Controles Web


		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{

				if( !IsPostBack )
				{	
					if(UrlParam.GetParam("PropietarioID") != "")
					{
						MySession.ID = Convert.ToInt32(UrlParam.GetParam("PropietarioID"));	
					}
					DesplegarPropietario();
				}

			}
			catch(Exception m)
			{
				string redirectString = "../Generic/Message.aspx" + "?page=" + m.Message + "(" + m.Source + ")";
				Response.Redirect(redirectString);
				redirectString = "";
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

		#region Funciones

		

		private void DesplegarPropietario()
		{

			#region Obtener Parametros
			int propietarioID=0;

			if( UrlParam.GetParam("PropietarioID") != "") 
			{
				propietarioID = Convert.ToInt32(UrlParam.GetParam("PropietarioID"));
			}

			
			db.DataBaseName		= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName		= WebUI.Helpers.MyApplication.CurrentServerName;

			#endregion Obtener Parametros

			#region Datos del Propietario
			Berke.DG.ViewTab.vPropietarioDatos view_propietario = new Berke.DG.ViewTab.vPropietarioDatos(); 
			
			view_propietario.InitAdapter( db );
			view_propietario.Dat.ID.Filter= propietarioID;
			view_propietario.Adapter.ReadAll();

			lblIDPropietario.Text = view_propietario.Dat.ID.AsString    ;
			lblNombre   .Text     = view_propietario.Dat.Nombre.AsString;
			lblDireccion.Text     = view_propietario.Dat.Direccion.AsString;
			lblObs      .Text     = view_propietario.Dat.Obs.AsString;
			lblPais     .Text     = view_propietario.Dat.pais.AsString;
			lblCiudad   .Text     = view_propietario.Dat.nomciudad.AsString;
			lblIdioma   .Text     = view_propietario.Dat.idioma.AsString;
			

			if ( view_propietario.Dat.Personeria.AsString == "F") 
			{
				lblPersoneria.Text    = "Fisica";
			} 
			else if ( view_propietario.Dat.Personeria.AsString == "J") 
			{
				lblPersoneria.Text    = "Juridica";
			}

			lblRuc.Text           = view_propietario.Dat.RUC.AsString;
			lblDocumento.Text     = view_propietario.Dat.Documento.AsString;   
			lblGrupoEmp.Text      = view_propietario.Dat.grupo.AsString;

			#endregion datos del propietario

			#region Vias del propietario 
			Berke.DG.ViewTab.vPropietarioXVia view_propietarioXVia = new Berke.DG.ViewTab.vPropietarioXVia();
			view_propietarioXVia.InitAdapter( db );
			view_propietarioXVia.Dat.PropietarioID.Filter= propietarioID;
			view_propietarioXVia.Adapter.ReadAll();

			dgPropietarioXVia.DataSource = view_propietarioXVia.Table;
			dgPropietarioXVia.DataBind();

			#endregion Vias del propietario


			Berke.DG.ViewTab.vPoderesPropietario view_poderesPropietario = new Berke.DG.ViewTab.vPoderesPropietario();
			view_poderesPropietario.InitAdapter( db );
			view_poderesPropietario.Dat.propietarioid.Filter= propietarioID;
			view_poderesPropietario.Adapter.ReadAll();

			int filas = view_poderesPropietario.RowCount;

			dgPoderes.DataSource = view_poderesPropietario.Table;
			dgPoderes.DataBind();

		
			#region Poderes del Propietario

			#endregion

			#region Enlazar datos con grillas

			
			if( !view_propietarioXVia.IsEmpty ) 
			{
				pnlVias.Visible = true;
			}


			if( !view_poderesPropietario.IsEmpty ) 
			{
				pnlPoderes.Visible = true;
			}

			#region Instrucciones de Vigilancia
			Berke.DG.ViewTab.vConsultaPropClienteInstruccion vPropClienteInstr = new Berke.DG.ViewTab.vConsultaPropClienteInstruccion(db);
			vPropClienteInstr.Adapter.Distinct = true;
			vPropClienteInstr.ClearFilter();
			vPropClienteInstr.Dat.propietarioid.Filter = propietarioID;
			vPropClienteInstr.Adapter.ReadAll();

			if (!vPropClienteInstr.IsEmpty)
			{
				pnlInstrucciones.Visible = true;
			}
			else
			{
				pnlInstrucciones.Visible = false;
			}

			DataTable dt = vPropClienteInstr.Table;

			#region Agregar columnas
			if (!dt.Columns.Contains("funcregnombre"))
			{
				dt.Columns.Add("funcregnombre");
			}
			if (!dt.Columns.Contains("funcrecnombre"))
			{
				dt.Columns.Add("funcrecnombre");
			}
			if (!dt.Columns.Contains("correspondencia"))
			{
				dt.Columns.Add("correspondencia");
			}
			#endregion Agregar columnas

			Berke.DG.DBTab.Usuario usu = new Berke.DG.DBTab.Usuario(db);
			foreach(DataRow dr in dt.Rows)
			{
				#region Registrado por
				
				if (dr["funcionarioregid"].ToString() != "")
				{
					usu.ClearFilter();
					usu.Adapter.ReadByID(Convert.ToInt32(dr["funcionarioregid"]));
					dr["funcregnombre"] = usu.Dat.Nick.AsString;
				}
				else
				{
					dr["funcregnombre"] = "";
				}
				#endregion Registrado por

				#region Registrado por
				if (dr["funcionariorecid"].ToString() != "")
				{
					usu.ClearFilter();
					usu.Adapter.ReadByID(Convert.ToInt32(dr["funcionariorecid"]));
					dr["funcrecnombre"] = usu.Dat.Nick.AsString;
				}
				else
				{
					dr["funcrecnombre"] = "";
				}
				#endregion Registrado por

				#region Correspondencia
				dr["correspondencia"] = dr["nro"].ToString() + "/" + dr["anio"].ToString() +
										" " + Berke.Libs.Base.DocPath.digitalDocPath(Convert.ToInt32(dr["anio"]),
																					 Convert.ToInt32(dr["nro"]),
																					 Convert.ToInt32(dr["codarea"]));
				#endregion Correspondencia

				#region Cliente + ID
				dr["clientenombre"] = HtmlGW.Redirect_Link(dr["clienteid"].ToString(), dr["clientenombre"].ToString() + " (" + dr["clienteid"].ToString() + ")",
														"ClienteDatos.aspx", "ClienteID");
				
				#endregion Cliente + ID
			}

			dgInstrucXVigilancia.DataSource = dt;
			dgInstrucXVigilancia.DataBind();

			#endregion Instrucciones de Vigilancia
		
			#endregion Enlazar datos con grillas
	
		}


		#endregion Funciones





	}
}
