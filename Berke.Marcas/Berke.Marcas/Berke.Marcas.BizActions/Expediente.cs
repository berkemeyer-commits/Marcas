using System;
using System.Data;
using Berke.Libs.Boletin.Services;



#region Expediente.	ReadByID_asDG
namespace Berke.Marcas.BizActions.Expediente
{
	using System;
	using Framework.Core;
	using System.Data;
//	using Libs.Base;
	using Libs.Base.DSHelpers;
	using System.Collections;
	using Berke.Libs.Base;
	
	public class ReadByID_asDG: IAction
	{	
		public Berke.DG.ExpedienteDG Execute( int expeID, Berke.Libs.Base.Helpers.AccesoDB db )
		{
			Berke.DG.ExpedienteDG outDG	= new Berke.DG.ExpedienteDG();

			#region Leer Expediente
			Berke.DG.DBTab.Expediente expe	= outDG.Expediente ;
			expe.InitAdapter( db );
			expe.Adapter.ReadByID( expeID );
			#endregion Leer Expediente

			#region Leer MarcaRegRen
			Berke.DG.DBTab.MarcaRegRen marRegRen	= outDG.MarcaRegRen ;
			marRegRen.InitAdapter( db );
			marRegRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.Value );
			#endregion Leer MarcaRegRen
		
			#region Leer Tramite
			Berke.DG.DBTab.Tramite trm	= outDG.Tramite ;
			trm.InitAdapter( db );
			trm.Adapter.ReadByID(expe.Dat.TramiteID.AsInt );
			#endregion Leer Tramite
		
			#region Leer Situacion
			Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit( db );
			trmSit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );

			Berke.DG.DBTab.Situacion sit	= outDG.Situacion ;
			sit.InitAdapter( db );
			sit.Adapter.ReadByID( trmSit.Dat.SituacionID.AsInt );

			#endregion Leer Situacion
			
			#region Leer  OrdenTrabajo
			Berke.DG.DBTab.OrdenTrabajo ot	= outDG.OrdenTrabajo ;
			ot.InitAdapter( db );
			ot.Adapter.ReadByID( expe.Dat.OrdenTrabajoID.Value );
			#endregion Leer OrdenTrabajo
		
			#region Leer Cliente
			Berke.DG.DBTab.Cliente cliente	= outDG.Cliente ;
			cliente.InitAdapter( db );
			cliente.Adapter.ReadByID( expe.Dat.ClienteID.Value );
			#endregion Leer Cliente

			#region Leer Agente Local
			Berke.DG.DBTab.CAgenteLocal agLoc	= outDG.CAgenteLocal ;
			agLoc.InitAdapter( db );
			agLoc.Adapter.ReadByID( expe.Dat.AgenteLocalID.Value );
			#endregion Leer Agente Local


			#region Leer Propietarios

			
			#region ExpedientesXPropietario
			Berke.DG.DBTab.ExpedienteXPropietario epp = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			epp.Dat.ExpedienteID.Filter = expeID;
			ArrayList aPropE = epp.Adapter.GetListOfField( epp.Dat.PropietarioID );
	
			#endregion ExpedientesXPropietario

			/* [15-09-2006] 
			 * El nombre y direccion de propietario se tomaran a partir de ahora
			 * de ExpedienteCampo de modo a centralizar de donde se toman los datos
			 * 
			 * El problema era que en algunos casos la direccion del propietario 
			 * no coincidia con la direccion del poder lo cual generaba confusion
			 * en la consulta de Marcas
			 * */

			/*
			Berke.DG.DBTab.Propietario propietario	= outDG.Propietario ;
			
			propietario.DisableConstraints();
			if( aPropE.Count > 0 ){	
				propietario.InitAdapter( db );
				propietario.Dat.ID.Filter =  new DSFilter( aPropE  );
				propietario.Adapter.ReadAll();
				for( propietario.GoTop(); ! propietario.EOF; propietario.Skip() )
				{
					epp.Dat.ExpedienteID.Filter = expeID;
					epp.Dat.PropietarioID.Filter = propietario.Dat.ID.AsInt;
					epp.Adapter.ReadAll();
					if( epp.Dat.DerechoPropio.AsBoolean ){
						propietario.Edit();
						propietario.Dat.Nombre.Value = propietario.Dat.Nombre.AsString + " (Der.Propio)";
						propietario.PostEdit();
					}
				}
			}
			*/
			#endregion Leer Propietarios	

			#region Leer Expediente Campo
	
			Berke.DG.DBTab.Propietario propietario	= outDG.Propietario ;
			Berke.DG.DBTab.ExpedienteCampo expCamp = outDG.ExpedienteCampo;
			expCamp.Dat.ExpedienteID.Filter = expeID;
			expCamp.InitAdapter( db );
			expCamp.Adapter.ReadAll();

			#endregion  Leer Expediente Campo

			#region Extraer datos de Propietarios de ExpedienteCampo
			Berke.DG.DBTab.Propietario propAnterior	= outDG.PropietarioAnt ;
			propAnterior.DisableConstraints();
			string nombre_Ant = "";
			string direccion_Ant = "";
			string paisAlfa_Ant = "";
			string propID_Ant = "";
			string nombre_Nue = "";
			string direccion_Nue = "";
			string paisAlfa_Nue = "";
			string propID_Nue = "";
			for( expCamp.GoTop(); ! expCamp.EOF ; expCamp.Skip() ){
				string campo = expCamp.Dat.Campo.AsString;
				switch( campo ){
					case (string) GlobalConst.PROP_ANTERIOR_NOMBRE :
						nombre_Ant = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ANTERIOR_DIR :
						direccion_Ant = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ANTERIOR_PAIS :
						paisAlfa_Ant = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ANTERIOR_ID :
						propID_Ant = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ACTUAL_NOMBRE :
						nombre_Nue = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ACTUAL_DIR  :
						direccion_Nue = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
					case (string) GlobalConst.PROP_ACTUAL_PAIS :
						paisAlfa_Nue = expCamp.Dat.Valor.AsString;
						expCamp.Delete();
						break;
			//		case (string) "Propietario_ID" :
			//			propID_Nue = expCamp.Dat.Valor.AsString;
			//			expCamp.Delete();
						break;						
				}
			}
			expCamp.AcceptAllChanges();
			#endregion Extraer datos de Propietarios de ExpedienteCampo

			#region Ingresar Propietario Nuevo ( Si hace falta )
			//if( nombre_Nue != "" && propietario.RowCount == 0 )

			if( nombre_Nue != "" )
			{
				Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais( db );
		
				propietario.NewRow();
				propietario.Dat.ID.Value = propID_Nue.ToString();
				propietario.Dat.Nombre.Value = nombre_Nue;
				propietario.Dat.Direccion.Value = direccion_Nue;
				if( paisAlfa_Nue != "" )
				{
					pais.Dat.paisalfa.Filter = paisAlfa_Nue;
					pais.Adapter.ReadAll();
					if( pais.RowCount == 1 )
					{
						propietario.Dat.PaisID.Value = pais.Dat.idpais.AsInt;
					}
				}
		
				propietario.PostNewRow();
			}
			#endregion Ingresar Propietario Nuevo

			#region Ingresar Propietarios Viejos
			if( nombre_Ant != "" )
			{
				Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais( db );
		
				string [] apropID = propID_Ant.Split(",".ToCharArray());
				for (int i=0; i<apropID.Length; i++)
				{
					if ( apropID[i] != ""  )
					{
						propAnterior.NewRow();
						propAnterior.Dat.ID.Value = apropID[i];
						propAnterior.Dat.Nombre.Value = nombre_Ant;
						propAnterior.Dat.Direccion.Value = direccion_Ant;
						if( paisAlfa_Ant != "" )
						{
							pais.Dat.paisalfa.Filter = paisAlfa_Ant;
							pais.Adapter.ReadAll();
							if( pais.RowCount == 1 )
							{
								propAnterior.Dat.PaisID.Value = pais.Dat.idpais.AsInt;
							}
						}
		
						propAnterior.PostNewRow();

						
						
					}
				}

			}
			#endregion Ingresar Propietarios Viejos

			#region Poderes 

			#region ExpedientesXPoder
			Berke.DG.DBTab.ExpedienteXPoder eppd = new Berke.DG.DBTab.ExpedienteXPoder( db );
			eppd.Dat.ExpedienteID.Filter = expeID;
			ArrayList aPodE = eppd.Adapter.GetListOfField( eppd.Dat.PoderID );
	
			#endregion ExpedientesXPoder
			Berke.DG.DBTab.Poder poder	= outDG.Poder ;

			poder.DisableConstraints();
			if( aPodE.Count > 0 )
			{	
				poder.InitAdapter( db );
				poder.Dat.ID.Filter =  new DSFilter( aPodE  );
				poder.Adapter.ReadAll();
			}	
			#endregion Poderes 
	
			return outDG;
		}


		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	= (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
			int expeID = inTB.Dat.Entero.AsInt;

			Berke.DG.ExpedienteDG outDG;

			outDG = Execute( expeID, db );

			DataSet  tmp_OutDS = outDG.AsDataSet();
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End ReadByID_asDG class


}// end namespace 



#endregion ReadByID_asDG

#region Expediente.	ReadByID
namespace Berke.Marcas.BizActions.Expediente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadByID: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		

				
			#region Asigacion de Valores de Salida
		
			// Expediente
			Berke.DG.DBTab.Expediente outTB	=   new Berke.DG.DBTab.Expediente( db );
			outTB.Adapter.ReadByID( inTB.Dat.Entero.AsInt );
				
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End ReadByID class


}// end namespace 


#endregion ReadByID

#region Expediente.	Jerarquia
namespace Berke.Marcas.BizActions.Expediente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Jerarquia: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			string ret = Berke.Marcas.BizActions.Lib.ExpedienteJerarquia( inTB.Dat.Entero.AsInt, db );
			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Alfa	.Value = ret;   //String

			outTB.PostNewRow(); 
	
			DataSet  tmp_OutDS;
			tmp_OutDS = outTB.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End Jerarquia class


}// end namespace 

#endregion Jerarquia

#region Expediente.	CambiarSituacion
namespace Berke.Marcas.BizActions.Expediente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizActions;
	
	public class CambiarSituacion: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			#region Entrada
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.CambioSitParam inTB	= new Berke.DG.ViewTab.CambioSitParam( cmd.Request.RawDataSet.Tables[0]);
			#endregion

			#region Proceso
			int expeID = inTB.Dat.ExpedienteID.AsInt;

			Berke.DG.DBTab.Expediente_Situacion expeSit		= new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.Expediente_Pertenencia expePer	= new Berke.DG.DBTab.Expediente_Pertenencia( db );


			#region Fecha
//			DateTime	sitFecha = inTB.Dat.SitFecha.AsDateTime;
//			string hora = inTB.Dat.SitHora.AsString;
//			int pos = hora.IndexOf(":");
//			int hh = Convert.ToInt32( hora.Substring(0, pos ));
//			int min = Convert.ToInt32( hora.Substring( pos + 1 ));
//			sitFecha.AddHours( hh );
//			sitFecha.AddMinutes( min );

			int sitAnio = inTB.Dat.SitFecha.AsDateTime.Year;

			#endregion

			#region Leer Usuario
			Acceso acc = new Acceso();
			Berke.DG.DBTab.Usuario funcionario = new Berke.DG.DBTab.Usuario( db );
            funcionario.Dat.Usuario.Filter = acc.Usuario;
			funcionario.Adapter.ReadAll();
			if( funcionario.RowCount == 0 )
			{
				throw new Exception("Usuario "+ acc.Usuario + " No registrado");
			}
			#endregion

			#region Leer Expediente
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente(db);
			expe.Adapter.ReadByID( expeID );
			expeID = expe.Dat.ID.AsInt;
			#endregion

			#region Leer Expediente Anterior
			Berke.DG.DBTab.Expediente expeAnt = new Berke.DG.DBTab.Expediente(db);
			expeAnt.Adapter.ReadByID( expe.Dat.ExpedienteID.AsInt );
			int expeAntID =  (expeAnt.RowCount == 1 ) ? expeAnt.Dat.ID.AsInt : -1;
			#endregion

			#region Leer Marca 
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca( db );
			marca.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
			int marcaID	= ( marca.RowCount == 1 ) ? marca.Dat.ID.AsInt : -1;
			#endregion 

			#region Leer Marca  Anterior
			Berke.DG.DBTab.Marca marcaAnt = new Berke.DG.DBTab.Marca( db );
			marcaAnt.Adapter.ReadByID( expeAnt.Dat.MarcaID.AsInt );
			int marcaAntID	= ( marcaAnt.RowCount == 1 ) ? marcaAnt.Dat.ID.AsInt : -1;
			#endregion

			#region Leer RegRen
			Berke.DG.DBTab.MarcaRegRen regRen = new Berke.DG.DBTab.MarcaRegRen( db );
			regRen.Adapter.ReadByID( expe.Dat.MarcaRegRenID.AsInt );
			int regRenID	= ( regRen.RowCount == 1 ) ? regRen.Dat.ID.AsInt : -1;
			#endregion

			#region Leer RegRenAnt
			Berke.DG.DBTab.MarcaRegRen regRenAnt = new Berke.DG.DBTab.MarcaRegRen( db );
			regRenAnt.Adapter.ReadByID( expeAnt.Dat.MarcaRegRenID.AsInt );
			int regRenAntID	= ( regRenAnt.RowCount == 1 ) ? regRenAnt.Dat.ID.AsInt : -1;
			#endregion

			#region ExpePendientes (OPO)
			Berke.DG.DBTab.Expediente_Pendiente expePend = new Berke.DG.DBTab.Expediente_Pendiente( db );
			expePend.Dat.ExpedienteID.Filter	= expe.Dat.ID.AsInt;
			expePend.Dat.Concluido.Filter		= false;
			expePend.Dat.PendienteID.Filter		= (int) GlobalConst.Pendientes.OPOSICION;
			expePend.Adapter.ReadAll();
			#endregion

			#region Leer Licencias 
			Berke.DG.DBTab.Expediente expeLicen = new Berke.DG.DBTab.Expediente(db);
			expeLicen.Dat.ExpedienteID.Filter	= expe.Dat.ID.AsInt;
			expeLicen.Dat.Concluido.Filter		= false;
			expeLicen.Dat.TramiteID.Filter		= (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA;
			expeLicen.Adapter.ReadAll();
			#endregion

			#region Leer TramiteSit Destino
			Berke.DG.DBTab.Tramite_Sit tramSit_Destino = new Berke.DG.DBTab.Tramite_Sit( db );
			tramSit_Destino.Adapter.ReadByID( inTB.Dat.TramiteSitDestinoID.AsInt );
			int situacion_Destino	= tramSit_Destino.Dat.SituacionID.AsInt;
			int tramite_Destino		= tramSit_Destino.Dat.TramiteID.AsInt;

			Berke.DG.DBTab.Situacion sitDest = new Berke.DG.DBTab.Situacion( db );
			sitDest.Adapter.ReadByID( situacion_Destino );
			
			#endregion

			string datos = "";  
			switch( situacion_Destino)
			{
			
					#region PRESENTADA
				case (int) GlobalConst.Situaciones.PRESENTADA :

					#region Verificar que ya no exista el Acta a Registrar
					if (expe.Dat.TramiteID.AsInt != (int) GlobalConst.Marca_Tipo_Tramite.REG_ADUANA)
					{
						Berke.DG.DBTab.Expediente exp = new Berke.DG.DBTab.Expediente( db );
						exp.Dat.ActaNro.Filter	= inTB.Dat.NroActa.AsInt;
						exp.Dat.ActaAnio.Filter = inTB.Dat.AnioActa.AsInt;
						exp.Adapter.ReadAll();
						if( exp.RowCount > 0 )
						{
							string e_mensaje = "El Acta [ " + inTB.Dat.NroActa.AsString + "/" + inTB.Dat.AnioActa.AsString + " ] YA EXISTE. ";
							throw new Exception( e_mensaje );
						}
					}
					#endregion 
					
					#region Asigna NroActa y AgenteLocalID en Expediente
					expe.Edit();
					if( ! inTB.Dat.NroActa.IsNull )
					{
						expe.Dat.ActaNro.Value	= inTB.Dat.NroActa.AsInt;
						expe.Dat.ActaAnio.Value	= inTB.Dat.AnioActa.AsInt; // sitAnio;
						expe.Dat.PresentacionFecha.Value = inTB.Dat.SitFecha.Value;

						datos = "Acta: "+inTB.Dat.NroActa.AsString+"/"+inTB.Dat.AnioActa.AsString;

					}
					expe.Dat.AgenteLocalID.Value = inTB.Dat.AgenteLocalID.Value;
					expe.PostEdit();
					#endregion

					if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
						tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
					{
						marca.Edit();
						#region Asigna AgenteLocalID en Marca
					
						marca.Dat.AgenteLocalID.Value = inTB.Dat.AgenteLocalID.Value;
					
						#endregion 
						#region Actualiza MarcaRegRenID si no esta asignado
						if( marca.Dat.MarcaRegRenID.IsNull && regRenID != -1)
						{
							marca.Dat.MarcaRegRenID.Value = regRenID;
						}
						#endregion 
						marca.PostEdit();
					}
					break;
					#endregion PRESENTADA

					#region PUBLICADA
				case (int) GlobalConst.Situaciones.PUBLICADA :

					#region Asigna datos de Publicacion
					expe.Edit();
					expe.Dat.PublicPag.Value	= inTB.Dat.PublicPagina.AsInt;
					expe.Dat.PublicAnio.Value	= inTB.Dat.PublicAnio.AsInt;
					expe.Dat.DiarioID.Value		= inTB.Dat.DiarioID.AsInt;
					expe.PostEdit();
				
					#endregion
					Berke.DG.DBTab.Diario diario = new Berke.DG.DBTab.Diario( db );
					diario.Adapter.ReadByID( inTB.Dat.DiarioID.Value );

					datos = " Pág/Año: "+inTB.Dat.PublicPagina.AsString+"/"+inTB.Dat.PublicAnio.AsString + " ("+ diario.Dat.Descrip.AsString+ ")";

					break;
					#endregion

					#region CONCEDIDA
				case (int) GlobalConst.Situaciones.CONCEDIDA :
					
					if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
						tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
					{
						Berke.DG.DBTab.MarcaRegRen rr = new Berke.DG.DBTab.MarcaRegRen( db );
						rr.Dat.RegistroNro.Filter	= inTB.Dat.NroRegistro.AsInt;
						rr.Dat.RegistroAnio.Filter  = inTB.Dat.AnioRegistro.AsInt;
						rr.Adapter.ReadAll();
						if( rr.RowCount > 0 )
						{
							string erro_mensaje = "El Registro [ " + inTB.Dat.NroRegistro.AsString + "/" + inTB.Dat.AnioRegistro.AsString + " ] YA EXISTE. ";
							throw new Exception( erro_mensaje );
						}

						#region Actualizar Fecha Vencimiento de registro
						expe.Edit();
						expe.Dat.VencimientoFecha.Value	= inTB.Dat.RegVencim.AsDateTime;
						expe.PostEdit();	
	
						regRen.Edit();
						regRen.Dat.VencimientoFecha.Value = inTB.Dat.RegVencim.AsDateTime;
						regRen.PostEdit();

						#endregion

						#region Marca.Vigente = true.  Actualiza RegRenID
						marca.Edit();
						marca.Dat.Vigente.Value	= true;		
						marca.Dat.MarcaRegRenID.Value = regRen.Dat.ID.Value;
						marca.PostEdit();
						#endregion

						#region regRen.Vigente = true;								
						regRen.Edit();
						regRen.Dat.Vigente.Value	= true;
						regRen.PostEdit();
						#endregion

						#region Actualizar registro numero
						regRen.Edit();
						regRen.Dat.RegistroNro.Value	= inTB.Dat.NroRegistro.Value;
						regRen.Dat.RegistroAnio.Value	= sitAnio;
						regRen.Dat.ConcesionFecha.Value	= inTB.Dat.SitFecha.Value;
						regRen.PostEdit();
						#endregion

						datos = "Registro : "+inTB.Dat.NroRegistro.AsString+"/"+sitAnio.ToString();


						#region Procesar Sustituida 
						if( marca.Dat.Sustituida.AsBoolean ) // SUSTITUIDA
						{
							#region Pasar de Sustituida a Nuestra
							marca.Edit();
							marca.Dat.Nuestra.Value		= true;
							marca.Dat.Sustituida.Value	= false;
							marca.PostEdit();
							#endregion
    
							#region Insertar log de cambio de pertenencia
							expePer.DeleteAllRows();
							expePer.NewRow();

							expePer.Dat.ExpedienteID.Value	= expe.Dat.ID.AsInt;
							expePer.Dat.Fecha.Value			= inTB.Dat.SitFecha.Value;
							expePer.Dat.PertenenciaMotivoID.Value = (int) GlobalConst.PertentenciaMotivo.INTERVENCION_BERKE;
							expePer.Dat.FuncionarioID.Value	= funcionario.Dat.ID.Value;

							expePer.PostNewRow();
							#endregion

						}
						#endregion Sustituida

						#region Procesar Renovacion
						if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
						{
							#region regRenAnterior.Vigente = false;
							if( regRenAnt.RowCount > 0 )
							{
								#region RegRenAnt.vigente = false
								regRenAnt.Edit();
								regRenAnt.Dat.Vigente.Value	= false;
								regRenAnt.PostEdit();
								#endregion
							}
							#endregion

							if( marca.Dat.ID.AsInt != marcaAnt.Dat.ID.AsInt )
							{						
								#region MarcaAnterior.vigente = false
								if( marcaAnt.RowCount > 0 )
								{
									marcaAnt.Edit();
									marcaAnt.Dat.Vigente.Value	= false;								
									marcaAnt.PostEdit();
								}
								#endregion
							}
						}
						#endregion Renovacion

					}// end if Registro o Renovacion

					break;
					
					#endregion CONCEDIDA

					#region TITULO RETIRADO
				case (int) GlobalConst.Situaciones.TITULO_RETIRADO :
					if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
						tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
					{
						#region Actualizar Fecha Vencimiento de registro
						/* Según Laura esto nunca debería de ejecutarse. mbaez
						if( ! inTB.Dat.RegVencim.IsNull )
						{
							expe.Edit();
							expe.Dat.VencimientoFecha.Value	= inTB.Dat.RegVencim.AsDateTime;
							expe.PostEdit();		

							regRen.Edit();
							regRen.Dat.VencimientoFecha.Value = inTB.Dat.RegVencim.AsDateTime;
							regRen.PostEdit();

						}
						*/
						#endregion
					}
					break;
					#endregion TITULO RETIRADO

					#region ARCHIVADA
				case (int) GlobalConst.Situaciones.ARCHIVADA:
						expe.Edit();
						expe.Dat.Bib.Value			= inTB.Dat.Bib.Value;
						expe.Dat.Exp.Value			= inTB.Dat.Exp.Value;
						expe.Dat.Concluido.Value	= true;
						expe.PostEdit();

					datos = "Bib/Exp: "+inTB.Dat.Bib.AsString+"/"+inTB.Dat.Exp.AsString;

					#region Pasar a Historico si estaba standby

					if( marca.Dat.StandBy.AsBoolean ) 
					{
						
						Berke.DG.DBTab.Tramite_Sit SitActual = new Berke.DG.DBTab.Tramite_Sit(db);
						SitActual.ClearFilter();
						SitActual.Adapter.ReadByID(expe.Dat.TramiteSitID.AsInt);
                        
						#region Marca se pone Inactiva
						/*[ggaleano 20/09/2007] Si viene de "Abandonar Sin Gastos"
						 * la marca debe quedar activa*/
						if (SitActual.Dat.SituacionID.AsInt != (int) GlobalConst.Situaciones.ABANDONADA)
						{
							marca.Edit();
							marca.Dat.Vigente.Value		= false;
							marca.PostEdit();
						}
						#endregion
    
						#region Insertar log de cambio de pertenencia
						/*[ggaleano 20/09/2007] Si viene de "Abandonar Sin Gastos"
						 * no se debe cargar expediente_pertenencia*/
						if (SitActual.Dat.SituacionID.AsInt != (int) GlobalConst.Situaciones.ABANDONADA)
						{
							expePer.DeleteAllRows();
							expePer.NewRow();

							expePer.Dat.ExpedienteID.Value	= expe.Dat.ID.AsInt;
							expePer.Dat.Fecha.Value			= inTB.Dat.SitFecha.Value;
							expePer.Dat.PertenenciaMotivoID.Value = (int) GlobalConst.PertentenciaMotivo.OTROS; // Otros
							expePer.Dat.Obs.Value = "Pasó a INACTIVA (Histórico)";
							expePer.Dat.FuncionarioID.Value	= funcionario.Dat.ID.Value;

							expePer.PostNewRow();
						}
						#endregion
					
					}
					#endregion
					break;
					#endregion

					#region DESISTIDA
				case (int) GlobalConst.Situaciones.DESISTIDA :
				
							break;
					#endregion Desistida

			}// end switch( situacion_Destino)
			
		#region Aplicar Comandos
			bool resultado = false;
			try
			{
			
				db.IniciarTransaccion();
				
				switch( situacion_Destino)
				{
						#region Desistida
				  case (int) GlobalConst.Situaciones.DESISTIDA :

					if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
					{
						Berke.Libs.Boletin.Services.RenovacionService RENService =  new RenovacionService(db);
						RENService.desistir(expe);
					}

					if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO )
					{
						Berke.Libs.Boletin.Services.RegistroService REGService =  new RegistroService(db);
						REGService.desistir(expe);
					}

					if ( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO    ||
					 	 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE       ||
						 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC  ||
						 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO           ||
						 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.FUSION              ||
						 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA            ||
						 tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA  )
					{
					    Berke.Libs.Boletin.Services.TVService TVService =  new TVService(db);
						TVService.desistir(expe);
					}

					break;
							#endregion desistida

						#region Cancelacion
					case (int) GlobalConst.Situaciones.CANCELACION_REG :

						if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
						{
							Berke.Libs.Boletin.Services.RenovacionService RENService =  new RenovacionService(db);
							RENService.cancelar(expe);
						}

						if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO )
						{
							Berke.Libs.Boletin.Services.RegistroService REGService =  new RegistroService(db);
							REGService.cancelar(expe);
						}

						break;

					case (int) GlobalConst.Situaciones.CANCELACION_TV :
						if ( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO    ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE       ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC  ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO           ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.FUSION              ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA            ||
							tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA  )
						{
							Berke.Libs.Boletin.Services.TVService TVService =  new TVService(db);
							TVService.cancelar(expe);
						}

						break;

						#endregion Cancelacion

						
						#region Anular Marca
						/* 
						 * Anular tiene el mismo efecto que una Cancelacion 
						 * por tanto invocamos al metodo Cancelar 
						 * */
					case (int) GlobalConst.Situaciones.MARCA_ANULADA :

						if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
						{
							Berke.Libs.Boletin.Services.RenovacionService RENService =  new RenovacionService(db);
							RENService.cancelar(expe);
						}

						if( tramite_Destino == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO )
						{
							Berke.Libs.Boletin.Services.RegistroService REGService =  new RegistroService(db);
							REGService.cancelar(expe);
						}

						break;
				

						#endregion Anular Marca

				}// end switch( situacion_Destino)
			

					#region Actualizar Situacion en expediente
			expe.Edit();
			// mbaez. 22/11/2006. Por alguna razón en algunos casos no se actualiza
			// esta información. Sin embargo si se ingresa correctamente el log de
			// situaciones.., por lo tanto actualizamos de la misma fuente.
			// expe.Dat.TramiteSitID.Value	= inTB.Dat.TramiteSitDestinoID.AsInt;
			expe.Dat.TramiteSitID.Value	= tramSit_Destino.Dat.ID.Value;
			//-
			expe.PostEdit();
			#endregion

					#region Actualizar StandBy Segun Situacion Destino
			if( ! sitDest.Dat.StandBy.IsNull )
			{
				expe.Edit();
				expe.Dat.StandBy.Value = sitDest.Dat.StandBy.AsBoolean;
				expe.PostEdit();

				if( expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
					expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION ){
					marca.Edit();
					marca.Dat.StandBy.Value = sitDest.Dat.StandBy.AsBoolean;
					marca.PostEdit();
				}
			}
			#endregion Actualizar StandBy Segun Situacion Destino

					#region Log de Cambio de Situacion
			expeSit.DeleteAllRows();
			expeSit.NewRow();
			expeSit.Dat.AltaFecha		.Value = DateTime.Today;
			expeSit.Dat.ExpedienteID	.Value = inTB.Dat.ExpedienteID.Value;
			expeSit.Dat.FuncionarioID	.Value = funcionario.Dat.ID.Value;
			expeSit.Dat.SituacionFecha	.Value = inTB.Dat.SitFecha.Value;
			expeSit.Dat.TramiteSitID	.Value = tramSit_Destino.Dat.ID.Value;
			expeSit.Dat.VencimientoFecha.Value = inTB.Dat.SitVencim.IsNull ? DateTime.Today : inTB.Dat.SitVencim.Value;
			expeSit.Dat.Obs				.Value = inTB.Dat.Obs.Value;
			expeSit.Dat.Datos			.Value = datos;
			expeSit.PostNewRow();
			
			#endregion

				
			
				int newID=0;

				expe.Adapter.UpdateRow();
				marca.Adapter.ConcurrenceOn = false;
				marca.Adapter.UpdateRow();
				regRen.Adapter.UpdateRow();
				newID = expeSit.Adapter.InsertRow();
				newID = expePer.Adapter.InsertRow();
				regRenAnt.Adapter.UpdateRow();
				marcaAnt.Adapter.UpdateRow();	

				db.Commit();
				resultado = true;
			}
			catch(Exception ex )
			{
				db.RollBack();
				throw new Exception("Error al cambiar de situacion el expediente ID["+ expeID.ToString() +"] " + ex.Message.ToString(),ex );
			}


			#endregion Aplicar Comandos

			#endregion Proceso

			#region Notificaciones

			db.IniciarTransaccion(); // Se notifica si la transaccion tuvo Exito

			Berke.DG.DBTab.Situacion situacion = new Berke.DG.DBTab.Situacion(db);
			situacion.Adapter.ReadByID( situacion_Destino );

			string expeLink = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link(
				"MarcaDetalleL.aspx",
				expe.Dat.ID.AsString,
				expe.Dat.ID.AsString,
				"ExpeID");

			string mensaje ="Expediente ID:"+expeLink+ " Acta:"+ expe.Dat.Acta.AsString+ "  "+Lib.NewLine +
				"Pasó a: "+situacion.Dat.Descrip.AsString + Lib.NewLine +
				"Marca ID : " + marca.Dat.ID.AsString + Lib.NewLine +
				"Denominacion : " + marca.Dat.Denominacion.AsString + Lib.NewLine +
				"Usuario :" + funcionario.Dat.Usuario.AsString+ Lib.NewLine + inTB.Dat.Obs.AsString;
						 
			// Notificar CAMBIO DE SITUACION
			Lib.Notificar( 4, mensaje , db );

			#region Notificacion para carga manual de DFlex
			switch( situacion_Destino )
			{
				case (int) GlobalConst.Situaciones.DESISTIDA : // 	Desistida
				case (int) GlobalConst.Situaciones.CON_VISTA : // 	Con Vista
				case (int) GlobalConst.Situaciones.CON_RECHAZO : // 	Con Rechazo
				case (int) GlobalConst.Situaciones.CON_SUSPENSION : // 	Con Suspensión (Oficio)
				case (int) GlobalConst.Situaciones.A_ADECUAR : // 	A Adecuar (con Manifestación)
				case (int) GlobalConst.Situaciones.SUSPENDIDA : // 	Suspendida
				case (int) GlobalConst.Situaciones.MANIFESTACION_POR_ANTECEDENTES : // 	Manifestación por Antecedentes
				case (int) GlobalConst.Situaciones.CON_OPOSICION : // 	Con Oposición
				case (int) GlobalConst.Situaciones.CON_JUICIO_DE_NULIDAD : // 	Con Juicio de Nulidad
				case (int) GlobalConst.Situaciones.CONTENCIOSO_ADMINISTRATIVO : // 	Contencioso Administrativo
				case (int) GlobalConst.Situaciones.RECONSIDERACION : // 	Reconsideración
				case (int) GlobalConst.Situaciones.APELACION : // 	Apelación
					Lib.Notificar( 14, mensaje , db ); // Situacion Incidental
					break;
			}

			#endregion Notificacion para carga manual de DFlex

			if( marca.Dat.Nuestra.AsBoolean )
			{
				#region Concedida - Oposicion Pendiente
				if ( situacion_Destino ==  (int) GlobalConst.Situaciones.CONCEDIDA )
				{
					if( expePend.RowCount > 0 ) // Hay oposicion no concluida
					{
						// Notificar a marcas y legales
						Lib.Notificar( 2, mensaje , db );

					}
					
				}
				#endregion Concedida - Oposicion Pendiente

				#region Examen_Fondo - Oposicion Pendiente
				if ( situacion_Destino ==  (int) GlobalConst.Situaciones.EXAMEN_FONDO )
				{
					if( expePend.RowCount > 0 ) // Hay oposicion no concluida
					{
						// Notificar a marcas y legales
						Lib.Notificar( 3, mensaje , db );

					}
					
				}
				#endregion Examen_Fondo - Oposicion Pendiente

				#region Concedida - Con y sin licencia Pendiente
				if ( situacion_Destino ==  (int) GlobalConst.Situaciones.CONCEDIDA )
				{
					if( expeLicen.RowCount > 0 ) // Hay licencia no concluida 
					{
						// Notificar: Seguir Licencia
						Lib.Notificar( 5, mensaje , db );

					}
					else
					{
						// Notificar concesion de una marca nuestra
						Lib.Notificar( 1, mensaje , db );

					}
				
				}
				#endregion Concedida - Con y sin licencia Pendiente

			}
			else{  // Tercero
			
				#region Publicada - Terceros No-Vigiladas
				if ( situacion_Destino ==  (int) GlobalConst.Situaciones.PUBLICADA )
				{
					if( ! marca.Dat.Vigilada.AsBoolean )
					{
						Berke.Marcas.BizActions.Lib.Notificar( 7, mensaje, db );
					}
				}
				#endregion Publicada - Terceros No-Vigiladas

			}
			db.Commit();

			#endregion

			// Según Laura, la información para el merge debe generarse
			// únicamente para expedientes nuestros. mbaez 14/11/2006.
			if (expe.Dat.Nuestra.AsBoolean) 
			{
				#region Ingresar Datos para Merge
				try
				{
					db.IniciarTransaccion();

					Berke.DG.DBTab.MergeXSituacion mgrXSit = new Berke.DG.DBTab.MergeXSituacion( db );
					mgrXSit.Dat.TramiteSitID.Filter = tramSit_Destino.Dat.ID.AsInt;
					mgrXSit.Dat.Vigente.Filter		= true;
					mgrXSit.Adapter.ReadAll();
					for( mgrXSit.GoTop(); ! mgrXSit.EOF; mgrXSit.Skip() )
					{
						Berke.DG.DBTab.Merge_Expediente mgrExpe = new Berke.DG.DBTab.Merge_Expediente( db );
						#region Asignar valores a Merge_Expediente
						mgrExpe.NewRow(); 
						//				mgrExpe.Dat.ID				.Value	= DBNull.Value;					//int PK  Oblig.
						mgrExpe.Dat.ExpedienteID	.Value	= expeID;						//int Oblig.
						mgrExpe.Dat.TramiteID		.Value	= expe.Dat.TramiteID.AsInt;		//int
						mgrExpe.Dat.MergeID			.Value	= mgrXSit.Dat.MergeID.AsInt;	//int Oblig.
						mgrExpe.Dat.FuncionarioID	.Value	= funcionario.Dat.ID.AsInt;		//int
						mgrExpe.Dat.Fecha			.Value	= inTB.Dat.SitFecha.Value;		//datetime
						mgrExpe.Dat.EnTramite		.Value	= (regRenAnt.Dat.RegistroNro.AsInt == 0);   //bit
						mgrExpe.Dat.ExpedienteIDPadre.Value = expeAnt.Dat.ID.AsInt;			//int
						mgrExpe.Dat.Generado		.Value	= false;						//bit Oblig.
						mgrExpe.Dat.Terminado		.Value	= false;						//bit Oblig.
						mgrExpe.Dat.Anulado			.Value	= false;						//bit
						mgrExpe.Dat.MergeDocID		.Value	= DBNull.Value;					//int
						mgrExpe.PostNewRow();
						#endregion Asignar valores a Merge_Expediente
						int mgrExpeID = mgrExpe.Adapter.InsertRow(); 
					}
					db.Commit();
				}
				catch(Exception ex )
				{
					db.RollBack();
					throw new Exception("Error al Grabar datos de MERGE. Expediente ID["+ expeID.ToString() +"]", ex);
				}
				#endregion Ingresar Datos para Merge
			}

			#region Asigacion de Valores de Salida
		
			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Logico.Value = resultado;   //Boolean
			outTB.PostNewRow(); 
			
			#endregion 

			#region Salida
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
			cmd.Response = new Response( tmp_OutDS );	
			#endregion

			db.CerrarConexion();

		}
	} // End CambiarSituacion class


}// end namespace Berke.Marcas.BizActions.Expediente


#endregion CambiarSituacion

#region Expediente.	ModifCambioSituacion
namespace Berke.Marcas.BizActions.Expediente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using UIPModel = UIProcess.Model;
	
	public class ModifCambioSituacion: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam(	"CURRENT_DATABASE"	);
			db.ServerName	= (string) Config.GetConfigParam(	"CURRENT_SERVER"	);
		
			Berke.DG.ExpeMarCambioSitDG inDG	= new Berke.DG.ExpeMarCambioSitDG( cmd.Request.RawDataSet );
		
			#region Tablas
			Berke.DG.DBTab.Expediente			expe		= inDG.Expediente ;
			Berke.DG.DBTab.Expediente_Situacion expeSit		= inDG.Expediente_Situacion ;
			Berke.DG.DBTab.Expediente_Situacion expeSit_bkp		= inDG.Expediente_Situacion_bkp ;
			Berke.DG.DBTab.MarcaRegRen			regRen		= inDG.MarcaRegRen ;
			Berke.DG.DBTab.MarcaRegRen			regRenPadre	= inDG.MarcaRegRenPadre ;
			Berke.DG.DBTab.Marca				mar			= inDG.Marca;

			expe.InitAdapter	   ( db );
			expeSit.InitAdapter	   ( db );
			expeSit_bkp.InitAdapter	   ( db );
			regRen.InitAdapter	   ( db );
			regRenPadre.InitAdapter( db );
			mar.InitAdapter        ( db );

	
			#endregion Tablas

			db.IniciarTransaccion();

			string datos = "";
			int SituacionActualID;
			
			// Mensaje para la notificacion de reversión
			string msgPattern = "Se ha revertido la situacion {0} para el acta {1}/{2}. Verifique la información con el usuario: " + Berke.Libs.Base.Acceso.GetCurrentUser() +".";;

			string msg = "";

			try 
			{

				#region Reversiones de : CANCELACION DE REGISTRO, DESISTIMIENTO Y CANCELACION TV

					#region Obtener SituacionActualID
						expeSit_bkp.GoTop();
						Berke.DG.DBTab.Tramite_Sit	trmSit;
						Berke.DG.DBTab.Situacion	sit;

						trmSit	= UIPModel.TramiteSit.ReadByID_M( expeSit_bkp.Dat.TramiteSitID.AsInt );
						sit		= UIPModel.Situacion.ReadByID( trmSit.Dat.SituacionID.AsInt );		
						SituacionActualID = trmSit.Dat.SituacionID.AsInt;
					#endregion Obtener SituacionActualID


				switch ( SituacionActualID )
				{
					case (int) Berke.Libs.Base.GlobalConst.Situaciones.CANCELACION_REG:
						
						if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO ) )
							
						{
							Berke.Libs.Boletin.Services.RegistroService REGService =  new RegistroService(db);
							REGService.revertirCancelar(expe);
						}

						
						if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) )
						{
							Berke.Libs.Boletin.Services.RenovacionService RENService =  new RenovacionService(db);
							RENService.revertirCancelar(expe);
						}
						msg += string.Format(msgPattern, "Cancelacion volutaria de registro", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);						
						break;

					case (int) Berke.Libs.Base.GlobalConst.Situaciones.DESISTIDA:

						/* Revertir desisitimiento de REG y REN */
						if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO ) )
							
						{
							Berke.Libs.Boletin.Services.RegistroService REGService =  new RegistroService(db);
							REGService.revertirDesistir(expe);

						}

						
						if ( (expe.Dat.TramiteID.AsInt == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) )
						{
							Berke.Libs.Boletin.Services.RenovacionService RENService =  new RenovacionService(db);
							RENService.revertirDesistir(expe);
						}
						
						/* Revertir desisitimiento de TVs */
						if ( expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO    ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE       ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC  ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO           ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.FUSION              ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA            ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA  )
						{
							Berke.Libs.Boletin.Services.TVService TVService =  new TVService(db);
							TVService.revertirDesistir(expe);
						}

						msg += string.Format(msgPattern, "Desistida", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);
						

						break;


					case (int) Berke.Libs.Base.GlobalConst.Situaciones.CANCELACION_TV:

						if (
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO    ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE       ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMRE_DOMIC  ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.DUPLICADO           ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.FUSION              ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.LICENCIA            ||
							expe.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA  )
						{
							Berke.Libs.Boletin.Services.TVService TVService =  new TVService(db);
							TVService.revertirCancelar(expe);

						}

						msg += string.Format(msgPattern, "Cancelacion de TV", expe.Dat.ActaNro.AsString, expe.Dat.ActaAnio.AsString);

						break;
				}
				#endregion		

				#region Aplicar Cambios de Expediente
				string comandStr = "";
				for( expe.GoTop(); ! expe.EOF ; expe.Skip() )
				{
					if( expe.IsRowAdded ) 
					{
						//					comandStr = expe.Adapter.InsertRow_CommandString();

						expe.Adapter.InsertRow();
						expe.AcceptRowChanges();
					}
	
					if( expe.IsRowModified ) 
					{
						#region Verificar que ya no exista el Acta 
						// Se verifica únicamente si es que actaNro y ActaAnio no son nulos. 
						// mbaez 17/11/2006
						if( (expe.Dat.ActaNro.AsInt> 0) && (expe.Dat.ActaAnio.AsInt > 0) && (expe.Dat.ActaNro.IsValueChanged || expe.Dat.ActaAnio.IsValueChanged) )
						{
							Berke.DG.DBTab.Expediente exp = new Berke.DG.DBTab.Expediente( db );
							exp.Dat.ActaNro.Filter	= expe.Dat.ActaNro.AsInt;
							exp.Dat.ActaAnio.Filter = expe.Dat.ActaAnio.AsInt;
							exp.Adapter.ReadAll();
							if( exp.RowCount > 0 )
							{
								string er_mensaje = "El Acta [ " + expe.Dat.ActaNro.AsString + "/" + expe.Dat.ActaNro.AsString + " ] YA EXISTE. ";
								throw new Exception( er_mensaje );
							}
						
						}
						#endregion 
						//					comandStr = expe.Adapter.UpdateRow_CommandString();
						expe.Adapter.UpdateRow();
						expe.AcceptRowChanges();
					}

					if( expe.IsRowDeleted )			
					{
						//					comandStr = expe.Adapter.DeleteRow_CommandString();
						expe.Adapter.DeleteRow();
						expe.AcceptRowChanges();
					}
				}
				#endregion Aplicar Cambios de Expediente

				#region Aplicar Cambios de Expediente_Situacion
				for( expeSit.GoTop(); ! expeSit.EOF ; expeSit.Skip() )
				{
					#region Asignar datos segun Situacion
					datos = "";  
					#region Leer TramiteSit Destino
					Berke.DG.DBTab.Tramite_Sit tramSit_Destino = new Berke.DG.DBTab.Tramite_Sit( db );
					tramSit_Destino.Adapter.ReadByID( expeSit.Dat.TramiteSitID.AsInt );

					int situacion_Destino	= tramSit_Destino.Dat.SituacionID.AsInt;
					#endregion Leer TramiteSit Destino

					Berke.DG.DBTab.Expediente exped = new Berke.DG.DBTab.Expediente( db );
					exped.Adapter.ReadByID( expeSit.Dat.ExpedienteID.Value );
					switch( situacion_Destino)
					{
						case (int) GlobalConst.Situaciones.PRESENTADA :
					
							datos = "Acta: "+exped.Dat.ActaNro.AsString+"/"+exped.Dat.ActaAnio.AsString;
					
							break;
						case (int) GlobalConst.Situaciones.PUBLICADA :
							Berke.DG.DBTab.Diario diario = new Berke.DG.DBTab.Diario( db );
							diario.Adapter.ReadByID( exped.Dat.DiarioID.Value );

							// No se utilizan los datos de Publicación, debido este no es calculado
							// sino hasta que se actualizan los datos en la base.
							// Se utilizan datos del expediente.
							//datos = " Pág/Año: "+ exped.Dat.Publicacion.AsString + " ("+ diario.Dat.Descrip.AsString+ ")";
						
							datos = " Pág/Año: "+exped.Dat.PublicPag.AsString+"/"+exped.Dat.PublicAnio.AsString + " ("+ diario.Dat.Descrip.AsString+ ")";
							break;
						case (int) GlobalConst.Situaciones.CONCEDIDA :
					
							if( tramSit_Destino.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.REGISTRO ||
								tramSit_Destino.Dat.TramiteID.AsInt == (int) GlobalConst.Marca_Tipo_Tramite.RENOVACION )
							{
								// mbaez. Se actualiza correctamente el campo datos de expesit
								//datos = "Registro : "+regRen.Dat.Registro.AsString;
								datos = "Registro: "+regRen.Dat.RegistroNro.AsString + "/" + regRen.Dat.RegistroAnio.AsString;
							}
							break;
						case (int) GlobalConst.Situaciones.ARCHIVADA:
			

							datos = "Bib/Exp: "+exped.Dat.Bib.AsString+"/"+exped.Dat.Exp.AsString;

							break;

					}// end switch( situacion_Destino)
		
					#endregion Asignar datos segun Situacion

					#region aplicar cambios sobre expediente_situacion

					if( expeSit.IsRowAdded ) 
					{
						expeSit.Edit();
						expeSit.Dat.Datos.Value = datos;
						expeSit.PostEdit();
						comandStr = expeSit.Adapter.InsertRow_CommandString();
						expeSit.Adapter.InsertRow();
						expeSit.AcceptRowChanges();
					}
	
	
					if( expeSit.IsRowModified ) 
					{
						expeSit.Edit();
						expeSit.Dat.Datos.Value = datos;
						expeSit.PostEdit();

						comandStr = expeSit.Adapter.UpdateRow_CommandString();
						expeSit.Adapter.UpdateRow();
						expeSit.AcceptRowChanges();
					}

					if( expeSit.IsRowDeleted )			
					{
						comandStr = expeSit.Adapter.DeleteRow_CommandString();
						expeSit.Adapter.DeleteRow();
						expeSit.AcceptRowChanges();
					}

					#endregion


				}
				#endregion Aplicar Cambios de Expediente_Situacion
	

				#region Aplicar Cambios de MarcaRegRen


				for( regRen.GoTop(); ! regRen.EOF ; regRen.Skip() )
				{
					if( regRen.IsRowAdded ) 
					{
						//					comandStr = regRen.Adapter.InsertRow_CommandString();
						regRen.Adapter.InsertRow();
						regRen.AcceptRowChanges();
					}
	
			
					if( regRen.IsRowModified ) 
					{
						#region Verificar que ya no exista el Registro
		
						if( (regRen.Dat.RegistroNro.AsInt > 0) && ( regRen.Dat.RegistroNro.IsValueChanged || regRen.Dat.RegistroAnio.IsValueChanged ))
						{
							Berke.DG.DBTab.MarcaRegRen rr = new Berke.DG.DBTab.MarcaRegRen( db );
							rr.Dat.RegistroNro.Filter	= regRen.Dat.RegistroNro.AsInt;
							rr.Dat.RegistroAnio.Filter  = regRen.Dat.RegistroAnio.AsInt;
							rr.Adapter.ReadAll();
							if( rr.RowCount > 0 )
							{
								string err_mensaje = "El Registro [ " + regRen.Dat.RegistroNro.AsString + "/" + regRen.Dat.RegistroAnio.AsString + " ] YA EXISTE. ";
								throw new Exception( err_mensaje );
							}
						
						}
						#endregion 
						//					comandStr = regRen.Adapter.UpdateRow_CommandString();
						regRen.Adapter.UpdateRow();
						regRen.AcceptRowChanges();
					}

					if( regRen.IsRowDeleted )			
					{
						//					comandStr = regRen.Adapter.DeleteRow_CommandString();
						regRen.Adapter.DeleteRow();
						regRen.AcceptRowChanges();
					}

				}
				#endregion Aplicar Cambios de MarcaRegRen

				// mbaez. Se corrigieron varios problemas en la reversión
				// de situaciones. 13/11/2006

				#region Aplicar cambios a MarcaRegRenPadre
				if ( (regRenPadre.RowCount >0) && regRenPadre.IsRowModified ) 
				{
					regRenPadre.Adapter.UpdateRow();
					regRenPadre.AcceptRowChanges();
				}
				#endregion Aplicar cambios a MarcaRegRenPadre

				/* XX Al parecer no es necesario. Agregado y comentado por mbaez. 
				 * Verificado si es necesario */
				#region    Aplicar cambios a Marca
				if ( mar.IsRowModified ) 
				{
					mar.Adapter.UpdateRow();
					mar.AcceptRowChanges();
				}
				#endregion Aplicar cambios a Marca
				db.Commit();
				db.IniciarTransaccion();
				if (msg != "") 
				{
					Lib.Notificar((int)GlobalConst.Notificacion.SIT_REVERSION, msg, db);
				}
				db.Commit();
			} 
			catch(Exception ex )
			{
				db.RollBack();
				db.CerrarConexion();
				throw ex;
			}					

			#region Salida
	
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Logico	.Value = true;
			outTB.PostNewRow(); 

			DataSet  tmp_OutDS;
			tmp_OutDS = outTB.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	
			#endregion Salida

			db.CerrarConexion();

		}

	
	} // End ModifCambioSituacion class


}// end namespace 


#endregion ModifCambioSituacion

namespace Berke.Marcas.BizActions.Expediente
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	using Berke.Marcas.BizDocuments.Marca;



	#region -----------  ClienteUpdate --------------

	public class ClienteUpdate: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Marcas.BizDocuments.Marca.ExpedienteListDS inDS;
			Berke.Marcas.BizDocuments.Marca.ExpedienteListDS outDS;
			outDS = new Berke.Marcas.BizDocuments.Marca.ExpedienteListDS();
			inDS  = ( Berke.Marcas.BizDocuments.Marca.ExpedienteListDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();
			TableGateway tb = new TableGateway();

			#region Parametros de Entrada

			// ExpedienteList

			tb.Bind(inDS.Tables["ExpedienteList"] );

			int       	ExpedienteID		= tb.AsInt("ExpedienteID");
			String    	Denominacion		= tb.AsString("Denominacion");
			String    	ClaseNro			= tb.AsString("ClaseNro");
			String    	Acta				= tb.AsString("Acta");
			String    	Registro			= tb.AsString("Registro");
			String    	Tramite				= tb.AsString("Tramite");
			int       	TramiteID			= tb.AsInt("TramiteID");
			DateTime  	PresentacionFecha	= tb.AsDateTime("PresentacionFecha");
			DateTime  	RegVtoFecha			= tb.AsDateTime("RegVtoFecha");
			String    	Mensajes			= tb.AsString("Mensajes");
			Boolean   	Seleccionado		= tb.AsBoolean("Seleccionado");
			int       	ClienteID			= tb.AsInt("ClienteID");
			String    	Cliente				= tb.AsString("Cliente");
			String    	MarcaTipo			= tb.AsString("MarcaTipo");

			#endregion Parametros de Entrada

			#region Asignacion de RESPONSE

			// ExpedienteList

			//			tb.Bind(outDS.Tables["ExpedienteList"] );
			//
			//			for( dbg?.Go(0);dbg?.CurrentRow < dbg?.RowCount; dbg?.Skip() ){
			//			tb.NewRow();
			//			tb.SetValue("ExpedienteID", dbg?.AsInt );
			//			tb.SetValue("Denominacion", dbg?.AsString );
			//			tb.SetValue("ClaseNro", dbg?.AsString );
			//			tb.SetValue("Acta", dbg?.AsString );
			//			tb.SetValue("Registro", dbg?.AsString );
			//			tb.SetValue("Tramite", dbg?.AsString );
			//			tb.SetValue("TramiteID", dbg?.AsInt );
			//			tb.SetValue("PresentacionFecha", dbg?.AsDateTime );
			//			tb.SetValue("RegVtoFecha", dbg?.AsDateTime );
			//			tb.SetValue("Mensajes", dbg?.AsString );
			//			tb.SetValue("Seleccionado", dbg?.AsBoolean );
			//			tb.SetValue("ClienteID", dbg?.AsInt );
			//			tb.SetValue("Cliente", dbg?.AsString );
			//			tb.SetValue("MarcaTipo", dbg?.AsString );
			//			tb.PostNewRow();
			//			}

			#endregion Asignacion de RESPONSE

			cmd.Response = new Response( outDS );           		
			db.CerrarConexion();
		}
	} // class ClienteUpdate END
	#endregion
}
