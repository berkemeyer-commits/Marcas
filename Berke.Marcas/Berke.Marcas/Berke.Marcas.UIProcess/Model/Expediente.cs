using System;
using System.Data;


using System.Collections;
using System.ComponentModel;

using System.Drawing;
using System.Web;




using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.Base;


namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Helpers;
	using BizDocuments.Marca;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas;
	using Berke.Marcas.BizDocuments;
	




	public class Expediente
	{

	

		#region BasicDataAsHTLString
		public static string BasicDataAsHTL( int expeID, bool de_litigios )
		{
			
			return _BasicDataAsHTL( expeID, de_litigios );
		}
		#endregion

		#region BasicDataAsHTLString
		public static string BasicDataAsHTL( int expeID )
		{
			return _BasicDataAsHTL( expeID, false );
		}
		#endregion

		#region BasicDataAsHTLString
		public static string _BasicDataAsHTL( int expeID, bool de_litigios )
		{
			string buf;
			#region Template
			buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "ExpedienteBasicData" );
			if ( de_litigios  )
				buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "ExpdteBasicDataLit" );
			#endregion Template

			#region Leer Expediente
			Berke.DG.ExpedienteDG dg =  ReadByID_asDG( expeID );
			#endregion Leer Expediente

			#region Sustituido
			string sustituido="";
			if( dg.Expediente.Dat.Sustituida.AsBoolean )
			{
				sustituido = "  /  Sustituido";
			}
			#endregion

			#region Nuestro 
			string nuestro;
			if( dg.Expediente.Dat.Nuestra.AsBoolean )
			{
				nuestro = "Nuestro";
			}
			else
			{
				nuestro = "Exp.de Terceros";
			}
			#endregion Nuestro

			#region Cliente
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais();
			string cliPaisAbrev = "";

			#region Leer pais de cliente
			if( !dg.Cliente.Dat.PaisID.IsNull )
			{
				pais = Berke.Marcas.UIProcess.Model.Pais.ReadByID( dg.Cliente.Dat.PaisID.AsInt);
				cliPaisAbrev = pais.Dat.paisalfa.AsString;
			}
			if( cliPaisAbrev != "" )
			{
				cliPaisAbrev = " ( "+ cliPaisAbrev+ " )";
			}
			#endregion Leer pais de cliente

			string cliente = dg.Cliente.Dat.Nombre.AsString + " - " + 
				dg.Cliente.Dat.ID.AsString;
			if(dg.Cliente.Dat.Direccion.AsString.Trim() != "" )	
			{
				cliente +=" <BR> " + dg.Cliente.Dat.Direccion.AsString.Trim();
			}
			cliente += cliPaisAbrev;
			#endregion Cliente

			#region Propietarios
			string propietario = "";
			Berke.Html.HtmlTable	tab = null;

			tab = new Berke.Html.HtmlTable("tbl");
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera

			tab.cell.Text.Size = "-3";

			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();
//			tab.AddCell("ID");
			tab.AddCell("Denominacion");
			tab.AddCell("Domicilio");
			tab.AddCell("País");
			if( dg.PropietarioAnt.RowCount > 0 )  // Tiene prop. Anterior
			{
				tab.AddCell("Ant./Nvo.");
			}
			tab.EndRow();	
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;

		

			#endregion Cabecera Propietarios
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));		
			#region Propietarios anteriores
			for( dg.PropietarioAnt.GoTop(); !dg.PropietarioAnt.EOF ; dg.PropietarioAnt.Skip() )
			{
	
				tab.BeginRow();
			//	tab.AddCell( chkSpc( dg.PropietarioAnt.Dat.ID.AsString ));
				tab.AddCell( chkSpc( dg.PropietarioAnt.Dat.Nombre.AsString ));
				tab.AddCell( chkSpc( dg.PropietarioAnt.Dat.Direccion.AsString ));
				pais = Berke.Marcas.UIProcess.Model.Pais.ReadByID( dg.PropietarioAnt.Dat.PaisID.AsInt );
				if( pais.RowCount == 1 )
				{
					tab.AddCell( chkSpc( pais.Dat.abrev.AsString ));
				}
				else
				{
					tab.AddCell( chkSpc( " " ));
				}
				tab.AddCell("Anterior");	
				tab.EndRow();
			}
			#endregion Propietarios anteriores

			#region Propietarios Nuevos
			for( dg.Propietario.GoTop(); !dg.Propietario.EOF ; dg.Propietario.Skip() )
			{
				tab.BeginRow();
			//	tab.AddCell( chkSpc( dg.Propietario.Dat.ID.AsString ));
				tab.AddCell( chkSpc( dg.Propietario.Dat.Nombre.AsString ));
				tab.AddCell( chkSpc( dg.Propietario.Dat.Direccion.AsString ));
				pais = Berke.Marcas.UIProcess.Model.Pais.ReadByID( dg.Propietario.Dat.PaisID.AsInt );
				if( pais.RowCount == 1 )
				{
					tab.AddCell( chkSpc( pais.Dat.abrev.AsString ));
				}
				else
				{
					tab.AddCell( chkSpc( " " ));
				}
				if( dg.PropietarioAnt.RowCount > 0 )  // Tiene prop. Anterior
				{
					tab.AddCell("Nuevo");
				}			
				tab.EndRow();
			}
			#endregion Propietarios Nuevos


			propietario  += tab.Html();
			#endregion Propietarios

			#region Poderes
			string strPoder = "";
			if( dg.Poder.RowCount > 0 )
			{
				tab = new Berke.Html.HtmlTable("tbl");
				tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
				#region Cabecera

				tab.cell.Text.Size = "-3";

				tab.cell.BgColor = "silver";
				tab.cell.Text.Bold = true;
				tab.BeginRow();

				tab.AddCell("ID");
				tab.AddCell("Acta");
				tab.AddCell("Inscip.");
				tab.AddCell("Denominacion");
				tab.AddCell("Domicilio");
				tab.AddCell("Poder");
			
				tab.EndRow();
				
				tab.cell.BgColor = "white";
				tab.cell.Text.Bold = false;

		

				#endregion Cabecera
				tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));
				for( dg.Poder.GoTop(); !dg.Poder.EOF ; dg.Poder.Skip() )
				{
					string poderID = dg.Poder.Dat.ID.AsString;
					
					string referencia =	Berke.Libs.Base.DocPath.digitalDocPath(0 , dg.Poder.Dat.ID.AsInt,9);



					poderID = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link("PoderDatos.aspx",
						poderID,poderID,"PoderID");
					string acta = "";
					if( ! dg.Poder.Dat.ActaNro.IsNull )
					{
						acta = dg.Poder.Dat.ActaNro.AsString+ "/"+dg.Poder.Dat.ActaAnio.AsString;
					}
					tab.BeginRow();
					tab.AddCell( chkSpc( poderID  ));
					tab.AddCell( chkSpc( acta ));
					tab.AddCell( chkSpc( dg.Poder.Dat.Inscripcion.AsString ));
					tab.AddCell( chkSpc( dg.Poder.Dat.Denominacion.AsString ));
					tab.AddCell( chkSpc( dg.Poder.Dat.Domicilio.AsString ));				
					tab.AddCell( chkSpc( referencia ));				
					tab.EndRow();
			
				}
				strPoder  +=  tab.Html();
			}
			else
			{
				strPoder = "*No registrado*";
			}
			
			#endregion Poderes

			#region Agente Local
			string agenteLocal = dg.CAgenteLocal.Dat.Nombre.AsString;
			if( agenteLocal != "" )
			{
				agenteLocal+= ". Matric.: " + dg.CAgenteLocal.Dat.nromatricula.AsString;
			}
			#endregion Agente Local

			#region Observaciones
			string obs = "";
			if( dg.Expediente.Dat.Label.AsString.Trim() != "" ){
				obs += "Ref.de la Marca: " + dg.Expediente.Dat.Label.AsString.Trim() + "<BR>";
			}
			if( dg.Expediente.Dat.Obs.AsString.Trim() != "" ){
				obs += dg.Expediente.Dat.Obs.AsString.Trim() + "<BR>";
			}
			obs+= dg.OrdenTrabajo.Dat.Obs.AsString.Trim();
			#endregion Observaciones

			#region  Datos Varios
			string varios = "";
			tab = new Berke.Html.HtmlTable("tbl");
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell_header"));
			#region Cabecera
			tab.cell.Text.Size = "-3";
			tab.cell.BgColor = "silver";
			tab.cell.Text.Bold = true;
			tab.BeginRow();
			tab.AddCell("Dato");
			tab.AddCell("Valor");			
			tab.EndRow();	
			tab.cell.BgColor = "white";
			tab.cell.Text.Bold = false;
			#endregion Cabecera
			tab.setCellFormater(new Berke.Html.HtmlCellFormater("cell"));
		
			for( dg.ExpedienteCampo.GoTop(); !dg.ExpedienteCampo.EOF ; dg.ExpedienteCampo.Skip() )
			{
				tab.BeginRow();
				tab.AddCell( chkSpc( dg.ExpedienteCampo.Dat.Campo.AsString  ));
				tab.AddCell( chkSpc( dg.ExpedienteCampo.Dat.Valor.AsString ));				
				tab.EndRow();
			}
			varios  +=  tab.Html();
			#endregion Datos Varios

			buf = buf.Replace("lblExpedienteID",	dg.Expediente.Dat.ID.AsString);
			buf = buf.Replace("lblNuestra",		nuestro	+  sustituido	);
			buf = buf.Replace("lblActa",			dg.Expediente.Dat.Acta.AsString);
			buf = buf.Replace("lblRegistro",		dg.MarcaRegRen.Dat.Registro.AsString);
			buf = buf.Replace("lblHI",			dg.OrdenTrabajo.Dat.OrdenTrabajo.AsString);
			buf = buf.Replace("lblTramite",		dg.Tramite.Dat.Descrip.AsString);
			buf = buf.Replace("lblSituacion",		dg.Situacion.Dat.Descrip.AsString);
			buf = buf.Replace("lblAgenteLocal",	 agenteLocal );
			buf = buf.Replace("lblCliente",		cliente );
			buf = buf.Replace("lblPropietario",	propietario );
			buf = buf.Replace("lblPoder",	strPoder );
			buf = buf.Replace("lblDatUltPublicacion", obtener_situacion(dg.Expediente.Dat.ID.AsInt));

			#region Observaciones
			if( 	obs != "" )
			{
				buf = buf.Replace("promtObservacion",	"Observaci&oacute;n:" );
				buf = buf.Replace("lblObservacion",	obs );
			}else{
				buf = buf.Replace("promtObservacion",	"" );
				buf = buf.Replace("lblObservacion",	"" );
			}
			#endregion Observaciones

			#region Varios
			if( dg.ExpedienteCampo.RowCount > 0 )
			{
				buf = buf.Replace("lblVarios",	varios );
				buf = buf.Replace("promptDatosVarios",	"Datos Varios:");
			}else{
				buf = buf.Replace("lblVarios",	"" );
				buf = buf.Replace("promptDatosVarios",	"");
			}
			#endregion Varios

			#region para test via xGen
//			Berke.Libs.Base.Helpers.Files.SaveStringToFile( buf, @"c:\Temp\dummy.html");
//			System.Diagnostics.Process.Start("iexplore.exe", @"c:\Temp\dummy.html" );
			#endregion 

			return buf;
		}
		#endregion BasicDataAsHTLString


		private static string chkSpc( string buf )
		{
			if( buf.Trim() == "" )
				return "&nbsp;";
			else
				return buf;
		}

		#region ReadByID_asDG
		public static Berke.DG.ExpedienteDG ReadByID_asDG(  int expeID )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = expeID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Expediente_ReadByID_asDG" , tmp_InDS );
			
			Berke.DG.ExpedienteDG outDG	=  new Berke.DG.ExpedienteDG( tmp_OutDS );

			return outDG;
		}
		#endregion ReadByID_asDG 
		
		#region ReadByID
		public static Berke.DG.DBTab.Expediente ReadByID( int expedienteID )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = expedienteID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Expediente_ReadByID" , tmp_InDS );
			
			Berke.DG.DBTab.Expediente outTB	=  new Berke.DG.DBTab.Expediente( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID 

		#region Jerarquia
		public static string Jerarquia( int ExpedienteID  )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = ExpedienteID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Expediente_Jerarquia" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB.Dat.Alfa.AsString;
		}
		#endregion Jerarquia 

		#region ModifCambioSituacion
		public static Berke.DG.ViewTab.ParamTab ModifCambioSituacion( Berke.DG.ExpeMarCambioSitDG inDG )
		{
			DataSet  tmp_InDS	= inDG.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Expediente_ModifCambioSituacion" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ModifCambioSituacion 
		
		#region CambiarSituacion
		public static Berke.DG.ViewTab.ParamTab CambiarSituacion( Berke.DG.ViewTab.CambioSitParam inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Expediente_CambiarSituacion" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion CambiarSituacion 

		public static ExpedienteListDS ClienteUpdate( ExpedienteListDS inDS )
		{
			ExpedienteListDS outDS;
			outDS = (ExpedienteListDS) Action.Execute( Actions.Expediente_ClienteUpdate.ToString() , inDS );
			return outDS;
		}

		private static string obtener_situacion( int ExpedienteID ) 
		{
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE" );
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER"   );

			Berke.DG.ViewTab.vExpeSituacion expeSit = new Berke.DG.ViewTab.vExpeSituacion( db );

			string npublic = "";
			string sit = "Sin publicación" ;
			expeSit.ClearOrder();
			expeSit.Dat.ExpedienteID   .Filter	= ExpedienteID;
			expeSit.Dat.TramiteSitID   .Filter  = (int) GlobalConst.Situaciones.PUBLICADA;
			expeSit.Dat.SituacionFecha .Order	= -1;
						
			expeSit.Adapter.ReadAll();
		
			if ( expeSit.RowCount > 0 ) {
					if (expeSit.RowCount > 1 ) { npublic =  " * " ; }

					sit = "Publicada el: " + expeSit.Dat.SituacionFecha.AsString + "   Vence: " + expeSit.Dat.VencimientoFecha.AsString + npublic; 
			}

			db.CerrarConexion();

			return sit;

		}

	}
}
