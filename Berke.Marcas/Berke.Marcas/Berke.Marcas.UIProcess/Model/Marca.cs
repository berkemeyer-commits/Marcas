using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{
	using Framework.Core;
	using Berke.Libs.Gateway;
	using Berke.Libs.Base;
	using BizDocuments.Helpers;
	using BizDocuments.Marca;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments;
	using Berke.Libs.WebBase.Helpers;

	/// <summary>
	/// Summary description for Marca.
	/// </summary>
	public class Marca
	{

		#region BasicDataAsHTLString
		public static string BasicDataAsHTL( int marcaID, bool de_litigios )
		{
			
			return _BasicDataAsHTL( marcaID, de_litigios );
		}
		#endregion


		#region BasicDataAsHTLString
		public static string BasicDataAsHTL( int marcaID )
		{
			return _BasicDataAsHTL( marcaID, false );

		}
		
		public static string _BasicDataAsHTL( int marcaID, bool de_litigios )
		{
			//lblTipoAtencion lblAtencionporMarca

			string buf="";
			
			Berke.DG.MarcaDG dg =  ReadByID_asDG( marcaID );

			#region Template
			if ( dg.Marca.Dat.TipoAtencionxMarca.AsInt > 0)
			{
				buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "MarcaBasicDataAt" );
			}
			else
			{
				buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "MarcaBasicData" );
			}

			if ( de_litigios  )
			{
				if ( dg.Marca.Dat.TipoAtencionxMarca.AsInt > 0)
				{
					buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "MarBasicDatLitAt" );
				}
				else
				{
					buf = Berke.Marcas.UIProcess.Model.DocumPlantilla.GetPattern( "MarcaBasicDataLit" );
				}
			}
				
			
			#endregion Template

			string nuestra;
			if( dg.Marca.Dat.Nuestra.AsBoolean )
			{
				nuestra = "Nuestra";
			}
			else
			{
				nuestra = "De Terceros";
				if( dg.Marca.Dat.Vigilada.AsBoolean ){
					nuestra = "Vigilada";
				}
			}

			string linkCliente = HtmlGW.Redirect_Link(
				dg.Cliente.Dat.ID.AsString, 
				dg.Cliente.Dat.ID.AsString,
				"ClienteDatos.aspx","ClienteID" );

			string cliente = dg.Cliente.Dat.Nombre.AsString +"&nbsp;"+ linkCliente + "<br>" +
				dg.Cliente.Dat.Direccion.AsString;

			#region Propietario / s
			string propietario = "";
			string proplnk = "";
			/*
			 * mbaez. Se despliega el propietario de la marca.
			 * Se vuelve a habilitar Bug#12
			 */
			for(dg.Propietario.GoTop(); ! dg.Propietario.EOF; dg.Propietario.Skip() )
			{
				string propietarioLink = HtmlGW.Redirect_Link(
					dg.Propietario.Dat.ID.AsString, 
					dg.Propietario.Dat.ID.AsString,
					"PropietarioDatos.aspx","PropietarioID" );

				proplnk += propietarioLink +"&nbsp;";
			}
			
			String espacio = "&nbsp;";
			String tab = espacio + espacio + espacio + espacio + espacio + espacio; 
			string propDir  =  dg.Marca.Dat.ProDir.AsString;
			string propPais =  dg.Marca.Dat.ProPais.AsString;
			propietario = dg.Marca.Dat.Propietario.AsString;
			#endregion Propietario / s

			string agenteLocal = dg.CAgenteLocal.Dat.Nombre.AsString;
			if( agenteLocal != "" )
			{
				agenteLocal+= ". Matric.: " + dg.CAgenteLocal.Dat.nromatricula.AsString;
			}

			string limitada = "";
			if( dg.Marca.Dat.Limitada.AsBoolean){
				limitada = "Limitada ";
			}
			buf = buf.Replace("lblDenominacion",	dg.Marca.Dat.Denominacion.AsString);
			buf = buf.Replace("lblDenomClave",	dg.Marca.Dat.DenominacionClave.AsString);
			buf = buf.Replace("lblNuestra",		nuestra		);
			buf = buf.Replace("lblMarcaID",	dg.Marca.Dat.ID.AsString);
			buf = buf.Replace("lblMarcaTipo",	dg.MarcaTipo.Dat.Descrip.AsString);
			buf = buf.Replace("lblClaseDes",	dg.Clase.Dat.DescripBreve.AsString+" "+dg.Marca.Dat.ClaseDescripEsp.AsString);
			buf = buf.Replace("lblLimitada",	limitada );
			buf = buf.Replace("lblRegNro",	dg.MarcaRegRen.Dat.Registro.AsString);
			buf = buf.Replace("lblTraducciones", HtmlGW.OpenPopUp_Link("ClaseIdioma.aspx", "Traducciones", marcaID.ToString(), "MarcaID"));
			string vigente = dg.MarcaRegRen.Dat.Vigente.AsBoolean ? "Vigente" : "NO vigente";	
	
			string activa = dg.Marca.Dat.Vigente.AsBoolean ? "Activa" : "Inactiva";
		
			string sustituida = dg.Marca.Dat.Sustituida.AsBoolean ? "Sustituida" : "";
			string standby = dg.Marca.Dat.StandBy.AsBoolean ? "Standby" : "";

			buf = buf.Replace("lblRegVig",	vigente );

			buf = buf.Replace("lblActiva",	activa );
			buf = buf.Replace("lblSustituida",	sustituida );
			buf = buf.Replace("lblStandBy",	standby );

			buf = buf.Replace("lblRegConc",	dg.MarcaRegRen.Dat.ConcesionFecha.AsString);
			buf = buf.Replace("lblRegVenc",	dg.MarcaRegRen.Dat.VencimientoFecha.AsString);
	
			buf = buf.Replace("lblAgenteLocal",	 agenteLocal );
			buf = buf.Replace("lblCliente",		cliente );
			buf = buf.Replace("lblPropietario",	propietario );
			buf = buf.Replace("lblPropDir" ,	propDir ); 
			buf = buf.Replace("lblPropPais",	propPais );
			buf = buf.Replace("lblPropIDs",	    proplnk );

			if ( dg.Marca.Dat.TipoAtencionxMarca.AsInt > 0)
			{
				string linkAtencion = "";
				if (dg.Marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE)
				{
					linkAtencion = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
									"AtencionesxMarca.aspx",
									GlobalConst.DESCRIP_ATENCIONXMARCAXTRAMITE,
									dg.Marca.Dat.ID.AsString,
									"MarcaID");
				}
				else
				{
					linkAtencion = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
						"AtencionXVia.aspx",
						dg.Atencion.Dat.Nombre.AsString + " (" + dg.Atencion.Dat.ID.AsString + ")", 
						dg.Atencion.Dat.ID.AsString,
						"AtencionID");
				}

				if (dg.Marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA)
				{
					buf = buf.Replace("lblTipoAtencion", GlobalConst.DESCRIP_ATENCIONXMARCA );
				}
				else if (dg.Marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
				{
					buf = buf.Replace("lblTipoAtencion", GlobalConst.DESCRIP_ATENCIONXBUNIT  + " (" + dg.BussinessUnit.Dat.Descripcion.AsString + ")" );
				}
				else if (dg.Marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE)
				{
					buf = buf.Replace("lblTipoAtencion", GlobalConst.DESCRIP_ATENCIONXMARCAXTRAMITE);
				}


				buf = buf.Replace("lblAtencionporMarca", linkAtencion);
			}


			if (dg.Marca.Dat.LogotipoID.AsInt > 0) {
				buf = buf.Replace("logotipoID",dg.Marca.Dat.LogotipoID.AsString);
				buf = buf.Replace("lblLogotipo", 
								  "<img onLoad=\"javascript:redimensionar(this);\" id=\"logotipo\" " + 
								  "src=\"Imagen.aspx?logotipoID=" + dg.Marca.Dat.LogotipoID.AsString + "\"");								  
				buf = buf.Replace("lblEtiqueta", "Etiqueta");
				buf = buf.Replace("lblSeparador", ":");
			} else {
				buf = buf.Replace("lblLogotipo", "");
				buf = buf.Replace("logotipoID","");
				buf = buf.Replace("lblEtiqueta", "");
				buf = buf.Replace("lblSeparador", "");
			}
			
//			Berke.Libs.Base.Helpers.Files.SaveStringToFile( buf, @"c:\Temp\dummy.html");
//			System.Diagnostics.Process.Start("iexplore.exe", @"c:\Temp\dummy.html" );
			return buf;
		}
		#endregion BasicDataAsHTLString


		#region ReadByID_asDG
		public static Berke.DG.MarcaDG ReadByID_asDG(  int marcaID )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = marcaID;
			inTB.PostNewRow();
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Marca_ReadByID_asDG" , tmp_InDS );
			
			Berke.DG.MarcaDG outDG	=  new Berke.DG.MarcaDG( tmp_OutDS );

			return outDG;
		}
		#endregion ReadByID_asDG 
	

		#region ReadByID
		public static Berke.DG.DBTab.Marca ReadByID( int marcaID )
		{
			Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab();
			inTB.NewRow();
			inTB.Dat.Entero.Value = marcaID;
			inTB.PostNewRow();

			DataSet  tmp_InDS	= inTB.AsDataSet(); 
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Marca_ReadByID" , tmp_InDS );
			
			Berke.DG.DBTab.Marca outTB	=  new Berke.DG.DBTab.Marca( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadByID 


	}
}



