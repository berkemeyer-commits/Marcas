
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
	using BizDocuments.Marca;
	using Helpers;
	using Berke.Libs.Base.Helpers;
	using Berke.Libs.WebBase.Helpers;
	using Berke.Marcas.WebUI.Tools.Helpers;
	using Berke.Libs.Base;
	using Berke.Libs.Base.DSHelpers;

	public partial class ClienteDatos : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Panel pnlVClienteXVia;


		Berke.Libs.Base.Helpers.AccesoDB db	= new Berke.Libs.Base.Helpers.AccesoDB();

		#region Controles Web

		protected System.Web.UI.WebControls.DataGrid dgDireccion;
		
		#endregion Controles Web

		#region Datos Miembro


		#endregion Datos Miembro

		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{

				if( !IsPostBack )
				{	
					if(UrlParam.GetParam("ClienteID") != "")
					{
						MySession.ID = Convert.ToInt32(UrlParam.GetParam("ClienteID"));	
					}
					DesplegarCliente();
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

		

		private void DesplegarCliente()
		{

			#region Obtener Parametros
			int clienteID=0;

			if( UrlParam.GetParam("ClienteID") != "") 
			{
				clienteID = Convert.ToInt32(UrlParam.GetParam("ClienteID"));
			}

			
			db.DataBaseName						= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName						= WebUI.Helpers.MyApplication.CurrentServerName;

			#endregion Obtener Parametros

			#region Datos del cliente
			Berke.DG.ViewTab.vClienteDatos view_cliente = new Berke.DG.ViewTab.vClienteDatos(); 
			
			view_cliente.InitAdapter( db );
			view_cliente.Dat.ID.Filter= clienteID;
			view_cliente.Adapter.ReadAll();

			lblIDCliente.Text     = view_cliente.Dat.ID.AsString    ;
			lblNombre   .Text     = view_cliente.Dat.Nombre.AsString;
			lblObs      .Text     = view_cliente.Dat.Obs.AsString;

			lblPais     .Text     = view_cliente.Dat.pais.AsString;
			lblCiudad   .Text     = view_cliente.Dat.nomciudad.AsString;
			lblIdioma   .Text     = view_cliente.Dat.idioma.AsString;
			lblCorreo   .Text     = view_cliente.Dat.Correo.AsString;
			lblObs      .Text     = view_cliente.Dat.Obs.AsString;
			lblDDI      .Text     = view_cliente.Dat.paistel.AsString + " " + view_cliente.Dat.ddi.AsString;

			
			

			if ( view_cliente.Dat.Personeria.AsString == "F") 
			{
				lblPersoneria.Text    = "Fisica";
			} 
			else if ( view_cliente.Dat.Personeria.AsString == "J") 
			{
				lblPersoneria.Text    = "Juridica";
			}

			lblRuc.Text           = view_cliente.Dat.RUC.AsString;
			lblDocumento.Text     = view_cliente.Dat.Documento.AsString;   
			lblTraduccion.Text    = view_cliente.Dat.TraduccionAuto.AsString;

			lblActivo.Text        = view_cliente.Dat.Activo.AsString;
			lblNoUbicable.Text    = view_cliente.Dat.Inubicable.AsString;
			lblGrupoEmp.Text      = view_cliente.Dat.grupo.AsString;
			lblDistribuidor.Text  = view_cliente.Dat.Distribuidor.AsString;
			lblMultiple.Text      = view_cliente.Dat.Multiple.AsString;

			 
			/*
			 *  Se obtienen las vias de comunicacion del cliente
			 * 			 
			 * */
			Berke.DG.ViewTab.vClienteXVia view_clienteXVia = new Berke.DG.ViewTab.vClienteXVia();
			view_clienteXVia.InitAdapter( db );
			view_clienteXVia.Dat.ClienteID.Filter= clienteID;
			view_clienteXVia.Adapter.ReadAll();

			dgClienteXVia.DataSource = view_clienteXVia.Table;
			dgClienteXVia.DataBind();

			Berke.DG.ViewTab.vClientesXTramite view_clientesXTramite = new Berke.DG.ViewTab.vClientesXTramite();
			view_clientesXTramite.InitAdapter( db );
			view_clientesXTramite.Dat.clientemultipleid.Filter= clienteID;
			view_clientesXTramite.Adapter.ReadAll();

			

			#region Convertir a Link
			if ( view_clientesXTramite.RowCount > 0 ) 
			{
				pnlClienteXTramite.Visible = true ;
				for( view_clientesXTramite.GoTop(); ! view_clientesXTramite.EOF ; view_clientesXTramite.Skip() )
				{
					view_clientesXTramite.Edit();

					view_clientesXTramite.Dat.nombre.Value = HtmlGW.Redirect_Link(
						view_clientesXTramite.Dat.id.AsString, 
						view_clientesXTramite.Dat.nombre.AsString,
						"ClienteDatos.aspx","ClienteID" );		 						
					view_clientesXTramite.PostEdit();
				}

			} 
			else 
			{
				pnlClienteXTramite.Visible = false ;
			}
			#endregion  Convertir a Link

			dgClienteXTramite.DataSource = view_clientesXTramite.Table;
			dgClienteXTramite.DataBind();
			


			#endregion datos del Cliente

			#region Enlazar datos con grillas

			if( !view_clienteXVia.IsEmpty ) 
			{
				//Grid.Bind( dgVClienteXVia, VClienteXVia.Table );
				//pnlClienteXVia.Visible = true;
				//dgPoderObs.DataSource = poder.Table;
				//dgPoderObs.DataBind();
				//pnlPoderObs.Visible = true;
			}


			#region Atenciones 

			/* ATENCIONES */ 
			Berke.DG.ViewTab.vAtencionArea view_atencionArea = new Berke.DG.ViewTab.vAtencionArea();
			view_atencionArea.InitAdapter( db );
			view_atencionArea.Dat.ClienteID.Filter= clienteID;
			view_atencionArea.Adapter.ReadAll();

	
			#region Agregar Columnas para Usuarios
			if (!view_atencionArea.Table.Columns.Contains("UsuariosTarjeta"))
			{
				view_atencionArea.Table.Columns.Add(new DataColumn("UsuariosTarjeta", typeof(String)));
			}

			if (!view_atencionArea.Table.Columns.Contains("AtencionTarjeta"))
			{
				view_atencionArea.Table.Columns.Add(new DataColumn("AtencionTarjeta", typeof(String)));
			}

			/*if (!view_atencionArea.Table.Columns.Contains("Congresos"))
			{
				view_atencionArea.Table.Columns.Add(new DataColumn("Congresos", typeof(String)));
			}*/

			if (!view_atencionArea.Table.Columns.Contains("Tarjeta"))
			{
				view_atencionArea.Table.Columns.Add(new DataColumn("Tarjeta", typeof(String)));
			}
			#endregion Agregar Columnas para Usuarios

			/* Aqui necesito agregar el link para ver las vias de comunicacion 
			 * de las atenciones del cliente
			 * */

			if( !view_atencionArea.IsEmpty ) 
			{
				pnlAtenciones.Visible = true;
				#region Obtener datos
				int recuperados = -1;
				try 
				{

					#region Convertir a Link
					for( view_atencionArea.GoTop(); ! view_atencionArea.EOF ; view_atencionArea.Skip() )
					{
						view_atencionArea.Edit();

						view_atencionArea.Dat.Nombre.Value = HtmlGW.Redirect_Link(
							view_atencionArea.Dat.ID.AsString, 
							view_atencionArea.Dat.Nombre.AsString,
							"AtencionXVia.aspx","AtencionID","&atencion=" + view_atencionArea.Dat.Nombre.AsString );
						view_atencionArea.Dat.strVias.Value = this.getAtencionXVia(view_atencionArea.Dat.ID.AsInt);

						#region Asignar datos de tarjetas de Congresos
						//string [] datosTarjetas = this.getUsuariosTarjeta(view_atencionArea.Dat.ID.AsInt).Split(';');
						view_atencionArea.Table.Rows[view_atencionArea.RowIndex]["UsuariosTarjeta"] = this.getUsuariosTarjeta(view_atencionArea.Dat.ID.AsInt);
						//view_atencionArea.Table.Rows[view_atencionArea.RowIndex]["Congresos"] = datosTarjetas[1];

						if (view_atencionArea.Dat.TarjetaID.AsString != "")
						{
							view_atencionArea.Table.Rows[view_atencionArea.RowIndex]["Tarjeta"] = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
								"Imagen.aspx",
								"Ver", 
								view_atencionArea.Dat.TarjetaID.AsString,
								"TarjetaID");
						}
						#endregion Asignar datos de tarjetas de Congresos


						view_atencionArea.PostEdit();
					}
					#endregion  Convertir a Link
				
				}
				catch ( Berke.Excep.Biz.TooManyRowsException ex )
				{	
					recuperados = ex.Recuperados;
				} 
				catch( Exception exep ) 
				{
					throw new Exception("ClienteDatos", exep );
				} 

				#endregion Obtener datos
			} 
			else 
			{
				pnlAtenciones.Visible = false;
			}

			/*BoundColumn UsuariosTarjeta = new BoundColumn();
			UsuariosTarjeta.DataField = "UsuariosTarjeta";
			UsuariosTarjeta.HeaderText = "Func. Berke";
			dgAtenciones.Columns.AddAt(3, UsuariosTarjeta);

			BoundColumn Congresos = new BoundColumn();
			Congresos.DataField = "Congresos";
			Congresos.HeaderText = "Congreso";
			dgAtenciones.Columns.AddAt(4, Congresos);*/

			dgAtenciones.DataSource = view_atencionArea.Table;
			dgAtenciones.DataBind();

			#endregion Atenciones 

			#region Observaciones por Area
			/* OBSERVACIONES POR AREA*/

			Berke.DG.ViewTab.vClienteObs view_clienteObs = new Berke.DG.ViewTab.vClienteObs();
			view_clienteObs.InitAdapter( db );
			view_clienteObs.Dat.ClienteID.Filter= clienteID;
			view_clienteObs.Adapter.ReadAll();

			dgClienteObs.DataSource = view_clienteObs.Table;
			dgClienteObs.DataBind();


			if( !view_clienteObs.IsEmpty ) 
			{
               
				pnlObs.Visible = true;
			}


			#endregion Observaciones por Area

			#region Contactos

			Berke.DG.ViewTab.vClienteXUsuario view_clienteXUsuario = new Berke.DG.ViewTab.vClienteXUsuario();
			view_clienteXUsuario.InitAdapter( db );
			view_clienteXUsuario.Dat.ClienteID.Filter= clienteID;
			view_clienteXUsuario.Adapter.ReadAll();
			

			dgClienteXUsuario.DataSource = view_clienteXUsuario.Table;
			dgClienteXUsuario.DataBind();

			if( !view_clienteXUsuario.IsEmpty ) 
			{
				pnlContactos.Visible = true;
			}


			#endregion Contactos

			#region Instrucciones de Vigilancia
			Berke.DG.ViewTab.vConsultaPropClienteInstruccion vPropClienteInstr = new Berke.DG.ViewTab.vConsultaPropClienteInstruccion(db);
			vPropClienteInstr.Adapter.Distinct = true;
			vPropClienteInstr.ClearFilter();
			vPropClienteInstr.Dat.clienteid.Filter = clienteID;
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

			int gAnio;
			int gNro;
			int gCodArea;

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
				gAnio = 0;
				gNro = 0;
				gCodArea = 0;

				if (dr["anio"].ToString() != "")
				{
					gAnio = Convert.ToInt32(dr["anio"]);
				}
				if (dr["nro"].ToString() != "")
				{
					gNro = Convert.ToInt32(dr["nro"]);
				}
				if (dr["codarea"].ToString() != "")
				{
					gCodArea = Convert.ToInt32(dr["codarea"]);
				}

				dr["correspondencia"] = dr["nro"].ToString() + "/" + dr["anio"].ToString() +
										" " + Berke.Libs.Base.DocPath.digitalDocPath(gAnio,
																					 gNro,
																					 gCodArea);
				#endregion Correspondencia

				#region Propietario + ID
				dr["propnombre"] = HtmlGW.Redirect_Link(dr["propietarioid"].ToString(), dr["propnombre"].ToString() + " (" + dr["propietarioid"].ToString() + ")",
									   					   "PropietarioDatos.aspx", "PropietarioID");
				
				#endregion Propietario + ID
			}

			dgInstrucXVigilancia.DataSource = dt;
			dgInstrucXVigilancia.DataBind();

			#endregion Instrucciones de Vigilancia

			
			#endregion Enlazar datos con grillas
	
		}
		

		#region Get Usuarios que trajeron la misma tarjeta
		private string getUsuariosTarjeta(int atencionID)
		{
			Berke.DG.DBTab.UsuarioXCongreso uCongreso = new Berke.DG.DBTab.UsuarioXCongreso(db);
			uCongreso.ClearFilter();
			uCongreso.Dat.AtencionID.Filter = atencionID;
			uCongreso.Dat.CongresoID.Order = 1;
			uCongreso.Dat.UsuarioID.Order = 2;
			uCongreso.Adapter.ReadAll();

			string listaUsuarios = "";
			//string listaCongresos = "";
			
			int gCongreso = -1;
			int cUsuarios = 0;

			if (uCongreso.RowCount > 0)
			{
				Berke.DG.DBTab.Congresos congreso = new Berke.DG.DBTab.Congresos(db);
				Berke.DG.DBTab.Usuario usuario = new Berke.DG.DBTab.Usuario(db);

				for(uCongreso.GoTop(); !uCongreso.EOF; uCongreso.Skip())
				{
					congreso.Adapter.ReadByID(uCongreso.Dat.CongresoID.AsInt);
					if (gCongreso != congreso.Dat.ID.AsInt)
					{	
						if (listaUsuarios != "")
						{
							listaUsuarios += "<br>";
						}
						listaUsuarios += "<b>" + congreso.Dat.Descripcion.AsString + ":</b> ";
						gCongreso = congreso.Dat.ID.AsInt;
						cUsuarios = 0;
					}
					usuario.Adapter.ReadByID(uCongreso.Dat.UsuarioID.AsInt);
					if (cUsuarios > 0)
					{
						listaUsuarios += ", ";
					}
					listaUsuarios += usuario.Dat.NombrePila.AsString;
					cUsuarios++;
				}
			}
			return listaUsuarios;
		}
        #endregion Get Usuarios que trajeron la misma tarjeta

		private string getAtencionXVia(int atencionID)
		{
			Berke.DG.ViewTab.vAtencionXVia vAtencionXVia = new Berke.DG.ViewTab.vAtencionXVia(db);
			vAtencionXVia.ClearFilter();
			vAtencionXVia.Dat.AtencionID.Filter = atencionID;
			vAtencionXVia.Adapter.ReadAll();

			string atencionesxvia = "";

			for (vAtencionXVia.GoTop(); !vAtencionXVia.EOF; vAtencionXVia.Skip())
			{
				if (atencionesxvia != "")
				{
					atencionesxvia += Berke.Libs.Boletin.Libs.Utils.ENTER;
				}
				atencionesxvia += vAtencionXVia.Dat.DescripcionVia.AsString + ": " + vAtencionXVia.Dat.ValorVia.AsString;
			}

			return atencionesxvia;
		}

		#endregion Funciones

		#region Acciones de los controles

		#region dgVClienteXVia

		#endregion dgVClienteXVia

		#region dgContactos

		#region ItemDataBound
		#endregion ItemDataBound

		#endregion dgContactos

		#region dgDireccion

		#region ItemDataBound

		private void dgDireccion_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ^ e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int indice = e.Item.ItemIndex + 1;
				Label lblContDir = (Label)e.Item.FindControl("lblContDir");
				Label lblDirecciones = (Label)e.Item.FindControl("lblDirecciones");

				lblContDir.Text = indice.ToString();
				lblDirecciones.Text = ObjConvert.AsString(((DataRowView)e.Item.DataItem).Row.ItemArray[1]);
			} 
		}

		#endregion ItemDataBound

		#endregion dgDireccion

		#region dgViasComArea

		private void dgViasComArea_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ^ e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lblVia = (Label)e.Item.FindControl("lblVia");
				Label lblDatosVia = (Label)e.Item.FindControl("lblDatosVia");
				HyperLink hpDatosVia = (HyperLink)e.Item.FindControl("hpDatosVia");

				lblVia.Text = ObjConvert.AsString(((DataRowView)e.Item.DataItem).Row.ItemArray[1]);

				if((int)((DataRowView)e.Item.DataItem).Row.ItemArray[7] == 5)
				{
					hpDatosVia.Text = ObjConvert.AsString(((DataRowView)e.Item.DataItem).Row.ItemArray[2]);
					hpDatosVia.NavigateUrl = "http://" + ObjConvert.AsString(((DataRowView)e.Item.DataItem).Row.ItemArray[2]);
					hpDatosVia.Target = "_blank";
				}
				else
				{
					lblDatosVia.Text = ObjConvert.AsString(((DataRowView)e.Item.DataItem).Row.ItemArray[2]);				
				}
			} 
		}

		#endregion dgViasComArea

		#endregion Acciones de los controles


	}

	
}

