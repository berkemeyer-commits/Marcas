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
using UIPModel = Berke.Marcas.UIProcess.Model ;
using Berke.Marcas.WebUI.Helpers;
using Berke.Libs.WebBase.Helpers;
namespace Berke.Marcas.WebUI.Home
{
	/// <summary>
	/// Summary description for AvisoDetalle.
	/// </summary>
	public partial class AvisoDetalle : System.Web.UI.Page
	{
		#region Variables Globales
		Berke.DG.DBTab.Aviso aviso = new Berke.DG.DBTab.Aviso();

		#endregion Variables Globales

		#region Controles del Form



		#endregion

		#region Properties

		#region RountripCounter
		private int RountripCounter
		{
			get{ return Convert.ToInt32( ViewState["RountripCounter"]) ; }
			set{ ViewState["RountripCounter"] = Convert.ToString( value );}
		}
		#endregion RountripCounter

		#region AvisoID
		private int AvisoID
		{
			get{ return Convert.ToInt32( ViewState["AvisoID"] ) ; }
			set{ ViewState["AvisoID"] = Convert.ToString( value );}
		}
		#endregion AvisoID

		#endregion Properties

		#region Asignar Labels
		private void AsignarLabels()
		{
			string buf = "";
			#region Obtener datos de funcionario
			Berke.DG.ViewTab.vFuncionario remit = UIPModel.Funcionario.ReadByID( aviso.Dat.Remitente.AsInt);
//			string remitName = remit.Dat.PriNombre.AsString.Trim()+" "+ remit.Dat.SegNombre.AsString.Trim() +" "+remit.Dat.PriApellido.AsString.Trim();
			string remitName = remit.Dat.Funcionario.AsString.Trim();
			remitName = remitName.Trim()=="" ?"SGE":remitName;

			Berke.DG.ViewTab.vFuncionario dest = UIPModel.Funcionario.ReadByID( aviso.Dat.Destinatario.AsInt);
//			string destName = dest.Dat.PriNombre.AsString.Trim()+" "+ dest.Dat.SegNombre.AsString.Trim() +" "+dest.Dat.PriApellido.AsString.Trim();
			string destName = dest.Dat.Funcionario.AsString.Trim();

			destName = destName.Trim()=="" ?"???":destName;
			#endregion

			txtFechaAviso.Text = DateTime.Now.ToString("g"); // aviso.Dat.FechaAviso.AsString;

			buf= @"<Table bgColor=""white"" cellSpacing=""0"" cellPadding=""0""  border=""1"">";

		
			#region De
			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; De  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				       remitName + "</p>" +
				   "</td>";
			buf+= "</tr>";
			#endregion De

			#region Para
			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; Para  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				destName + "</p>" +
				"</td>";
			buf+= "</tr>";
			#endregion Para 

			#region Fecha
			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; Fecha  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				aviso.Dat.FechaAlta.AsString + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
				" Avisar:" + aviso.Dat.FechaAviso.AsString +"</p>" +
				"</td>";
			buf+= "</tr>";
			#endregion Fecha 

//			#region FechaAviso
//			buf+= "</tr>";
//			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; Avisar </td>";
//
//			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
//				aviso.Dat.FechaAviso.AsString + "</p>" +
//				"</td>";
//			buf+= "</tr>";
//			#endregion Fecha 

			#region Asunto
	

			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; Asunto  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				aviso.Dat.Asunto.AsString + "</p>" +
				"</td>";
			buf+= "</tr>";
			#endregion Asunto 


			#region Estado
			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left""  > &nbsp; Estado  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				(aviso.Dat.Pendiente.AsBoolean ? "PENDIENTE" : "CONCLUIDO") + "</p>" +
				"</td>";
			buf+= "</tr>";
			#endregion Estado 


			#region Contenido



			string contenido = HtmlGW.ConvertNewLines( aviso.Dat.Contenido.AsString );

			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 90px""  align=""left"" valign=""Top"" > &nbsp; Contenido  </td>";

			buf+= @"<tD style=""WIDTH: 450px"" ><P class=""Texto"" align=""left"">" +
				contenido + "</p>" +
				"</td>";
			buf+= "</tr>";
			#endregion Contenido 



			buf+= "</Table>";

			lblCabecera.Text = buf;

			#region Indicaciones
//			buf = "<b>Indicaciones</b><br>";

			buf= @"<Table bgColor=""white"" cellSpacing=""0"" cellPadding=""0""  border=""1"">";
		
			buf+= @"<tr  >";
			buf+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Indicaciones	</b></p></td>";
			buf+= @"</tr>";

			buf+= "</tr>";
			buf+= @"<tD style=""WIDTH: 650px"" ><P class=""Texto"" align=""center"">" + aviso.Dat.Indicaciones.AsString + "</p> </td>";
			buf+= "</tr>";
			buf+= "</Table>";
			lblIndicaciones.Text = buf;
			#endregion
			txtIndicaciones.Text = "";

		}
		#endregion Asignar Labels

		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales
		private void AsignarValoresIniciales()
		{
		
			#region Prioridad DropDown
			Berke.DG.ViewTab.ListTab sePrioridad = Berke.Marcas.UIProcess.Model.Prioridad.ReadForSelect();
			ddlPrioridad.Fill( sePrioridad.Table, true );
			#endregion Prioridad DropDown

			if( ! aviso.Dat.PrioridadID.IsNull ){
					ddlPrioridad.Items.FindByValue( aviso.Dat.PrioridadID.AsString ).Selected = true;
			}
	
			#region Destinatario DropDown
			Berke.DG.ViewTab.ListTab seDestinatario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlDestinatario.Fill( seDestinatario.Table, true);
			ddlDestinatario.Value = aviso.Dat.Destinatario.AsString;

			#endregion Destinatario DropDown

			AsignarLabels();
//		
//			#region Remitente DropDown
//			Berke.DG.ViewTab.ListTab seRemitente = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
//			ddlRemitente.Fill( seRemitente.Table, true);
//			#endregion Remitente DropDown
//
//			Berke.DG.ViewTab.vFuncionario fun = Berke.Marcas.UIProcess.Model.Funcionario.ReadByUserName(Acceso.GetCurrentUser());
//			ddlDestinatario.Value = fun.Dat.ID.AsString;
//			ddlPendiente.SelectedIndex = 1;


		}
		#endregion Asignar Valores Iniciales

		#region LeerAviso
		private void LeerAviso()
		{
			aviso = Berke.Marcas.UIProcess.Model.Aviso.ReadByID( AvisoID );
			Session["Aviso"]= aviso.Table;
			aviso.AcceptAllChanges();

			string usuario = Berke.Libs.Base.Acceso.GetCurrentUser();

			#region Marcar como leido
			if( aviso.Dat.Destinatario.AsInt == MySession.FuncionarioID )
			{
				aviso.Edit();
				aviso.Dat.Leido.Value = true;
				aviso.PostEdit();
		

				#region Grabar en BD
				Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
				db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
				db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

				aviso.InitAdapter( db );
		
				db.IniciarTransaccion();

				aviso.Adapter.ConcurrenceOn = false;
		
				aviso.Adapter.UpdateRow();
				db.Commit();
			
				#endregion Grabar en BD

			}
			
			#endregion Marcar como leido

			AsignarValoresIniciales();
		}
		#endregion LeerAviso


		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{

			AsignarDelegados();
			if( !IsPostBack )
			{
				RountripCounter = 1;
				if( Request.UrlReferrer != null )
					this.litPaginaAnterior.Text = Request.UrlReferrer.PathAndQuery;
				else
					this.litPaginaAnterior.Text = Request.Path;

				#region Obtener ID
				if( Request.QueryString.Count < 1 )
				{
					throw new Exception("Falta parametro QueryString ");
				}
				else
				{
					AvisoID = Convert.ToInt32(Request.QueryString[0]);
				}
				#endregion Obtener ID

				 LeerAviso();

			}else {
				RountripCounter++;
			}
			this.btnVolver.Attributes.Add("onclick", "history.go(-"+ RountripCounter.ToString() +"); return false;" );

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

		#region Derivar
		protected void btnDerivar_Click(object sender, System.EventArgs e)
		{
			Berke.DG.DBTab.Aviso aviso;
			aviso = new Berke.DG.DBTab.Aviso( (System.Data.DataTable) Session["Aviso"] );

			string indicacionesAnteriores	= aviso.Dat.Indicaciones.AsString;
			string nuevaIndicacion			= "";
			string destinatarioID			= ddlDestinatario.SelectedValue;
			string usuario					= Berke.Libs.Base.Acceso.GetCurrentUser();
			string ahora					= DateTime.Now.ToString("g");
			string destinatarioUser			= "";

			Berke.DG.ViewTab.vFuncionario remit = UIPModel.Funcionario.ReadByUserName( usuario );
			Berke.DG.ViewTab.vFuncionario dest  = UIPModel.Funcionario.ReadByID( aviso.Dat.Remitente.AsInt );

			if( destinatarioID.Trim() != "" )
			{
				dest = Berke.Marcas.UIProcess.Model.Funcionario.ReadByID( Convert.ToInt32(destinatarioID ) );
//				destinatarioUser			= dest.Dat.Usuario.AsString;
				destinatarioUser			= dest.Dat.NombreCorto.AsString;
			}

			#region Nueva Indicacion
//			nuevaIndicacion += "<br>" + ahora + " " + usuario + " -> " + destinatarioUser + " * ";
			nuevaIndicacion += "<br>" + ahora + " " + remit.Dat.NombreCorto.AsString
				+ " -> " + destinatarioUser + " * ";
			if(  chkMarcarAtendido.Checked )
			{
				nuevaIndicacion += "ATENDIDO * ";			
			}
			nuevaIndicacion +=  txtIndicaciones.Text.Trim();			
			#endregion Nueva Indicacion

			aviso.Edit();
			aviso.Dat.Leido.Value			= false;
			aviso.Dat.Indicaciones.Value	= indicacionesAnteriores + nuevaIndicacion;
			aviso.Dat.Remitente.Value		= remit.Dat.ID.AsInt;
			aviso.Dat.Destinatario.Value	= dest.Dat.ID.AsInt;
		
			#region Fecha Aviso
			if(txtFechaAviso.Text.Trim() != "" )
			{
				aviso.Dat.FechaAviso.Value = txtFechaAviso.Text;
			}
			else{
				aviso.Dat.FechaAviso.Value = DateTime.Today;			
			}
			#endregion Fecha Aviso

			aviso.Dat.Remitente.Value = remit.Dat.ID.AsInt;

			aviso.Dat.PrioridadID.Value	= this.ddlPrioridad.SelectedValue;

			#region Pendiente
			if( chkMarcarAtendido.Checked )
			{
				aviso.Dat.Pendiente.Value = false;
				aviso.Dat.Indicaciones.Value += " <br>"+DateTime.Now.ToString()+" CONCLUIDO por "+ Berke.Libs.Base.Acceso.GetCurrentUser();

			}
			else
			{
				aviso.Dat.Pendiente.Value = true;
			}
			#endregion Pendiente

			aviso.PostEdit();

			#region Grabar en BD
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			aviso.InitAdapter( db );
		
			db.IniciarTransaccion();

			aviso.Adapter.ConcurrenceOn = false;
		
//			string buffer = aviso.Adapter.UpdateRow_CommandString();
			aviso.Adapter.UpdateRow();
			db.Commit();
		//	db.CerrarConexion();

			if( aviso.Dat.Pendiente.AsBoolean )
			{
				LeerAviso();
			}
			else
			{

				GoBack();
			}

			#endregion Grabar en BD
		}
		#endregion Derivar

		#region GoBack
		private void GoBack(){
			Response.Redirect( litPaginaAnterior.Text, true );
		}
		#endregion GoBack
	}
}
