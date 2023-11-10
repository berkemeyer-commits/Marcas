//Modificado

namespace Berke.Marcas.BizActions.CambioCliente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

#region CambioCliente.	Read	
	public class Read: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.RegistrosActasClientePropietario inTB	= new Berke.DG.ViewTab.RegistrosActasClientePropietario( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion Ejemplos para filtro
		
			#region Asigacion de Valores de Salida
		
			// vMarcaClientePropietario
			
			Berke.DG.ViewTab.vMarcaClientePropietario outTB	=   new Berke.DG.ViewTab.vMarcaClientePropietario();
			
//			outTB.Dat.MarcaID			.Value = DBNull.Value;   //Int32
//			outTB.Dat.Denominacion		.Value = DBNull.Value;   //String
//			outTB.Dat.DescripBreve		.Value = DBNull.Value;   //String
//			outTB.Dat.Acta				.Value = DBNull.Value;   //String
//			outTB.Dat.Registro			.Value = DBNull.Value;   //Int32
//			outTB.Dat.ClienteID			.Value = DBNull.Value;   //Int32
//			outTB.Dat.PropietarioID		.Value = DBNull.Value;   //Int32
//			outTB.Dat.Propietario		.Value = DBNull.Value;   //String
//			outTB.Dat.Cliente			.Value = DBNull.Value;   //String
			if (!inTB.Dat.ClienteID.IsNull)
			{
				outTB.Dat.ClienteID.Filter = inTB.Dat.ClienteID.AsInt;
			}
			if (!inTB.Dat.PropietarioID.IsNull)
			{
				outTB.Dat.PropietarioID.Filter = inTB.Dat.PropietarioID.AsInt;
			}
			if (!inTB.Dat.Actas.IsNull)
			{
				outTB.Dat.Acta.Filter = ObjConvert.GetFilter(inTB.Dat.Actas.AsString);
			}
			if (!inTB.Dat.Registros.IsNull)
			{
				outTB.Dat.Registro.Filter = ObjConvert.GetFilter(inTB.Dat.Registros.AsString);
			}

			outTB.InitAdapter( db );
			outTB.Adapter.ReadAll();
					
			#endregion Asigacion de Valores de Salida

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); 
			
			tmp_OutDS.Tables.Add( outTB.Table );
								
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}


	    
	} 
#endregion Read

#region CambioCliente.  ReadTVS
	public class ReadTVS: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.RegistrosActasClientePropietario inTB	= new Berke.DG.ViewTab.RegistrosActasClientePropietario( cmd.Request.RawDataSet.Tables[0]);
				
			#region Asigacion de Valores de Salida
		
			Berke.DG.ViewTab.vMarcaClientePropietarioTVS outTB_TVS	=   new Berke.DG.ViewTab.vMarcaClientePropietarioTVS();
		
			if (!inTB.Dat.ClienteID.IsNull)
			{
				outTB_TVS.Dat.ClienteID.Filter = inTB.Dat.ClienteID.AsInt;
			}
			if (!inTB.Dat.PropietarioID.IsNull)
			{
				outTB_TVS.Dat.PropietarioID.Filter = inTB.Dat.PropietarioID.AsInt;
			}
			if (!inTB.Dat.Actas.IsNull)
			{
				outTB_TVS.Dat.Acta.Filter = ObjConvert.GetFilter(inTB.Dat.Actas.AsString);
			}
			if (!inTB.Dat.Registros.IsNull)
			{
				outTB_TVS.Dat.Registro.Filter = ObjConvert.GetFilter(inTB.Dat.Registros.AsString);
			}

			outTB_TVS.InitAdapter(db);
			outTB_TVS.Adapter.ReadAll();
					
			#endregion Asigacion de Valores de Salida

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); 
			
			tmp_OutDS.Tables.Add( outTB_TVS.Table );
								
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}


	    
	} 

#endregion CambioCliente.  ReadTVS


}// end namespace 

/* Entrada para el fwk.Config

				<action code="CambioCliente_Read" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.CambioCliente.Read,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/



//Chequeado

namespace Berke.Marcas.BizActions.CambioCliente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;

#region CambioCliente.	Upsert
	public class Upsert: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.vMarcaClientePropietario inTB	= new Berke.DG.ViewTab.vMarcaClientePropietario( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion

			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			// Tabla : Expediente_ClienteCambio
			Berke.DG.DBTab.Expediente_ClienteCambio eCC = new Berke.DG.DBTab.Expediente_ClienteCambio( db );
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca ( db );
			Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo( db );
            //DBTab para eliminar Instrucciones
            Berke.DG.DBTab.Expediente_Instruccion ei = new DG.DBTab.Expediente_Instruccion( db );
            //DBTab para eliminar Atenciones Por Marca
            Berke.DG.DBTab.AtencionxMarca am = new DG.DBTab.AtencionxMarca(db);
            Boolean eliminarInstruccionesVigilancia = false;

            //Si el valor es negativo indica que se deben eliminar las instrucciones de
            //Vigilancia
            if (inTB.Dat.ClienteID.AsInt < 0)
            {
                eliminarInstruccionesVigilancia = true;
                inTB.Edit();
                inTB.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt * -1;
                inTB.PostEdit();
            }

			#region Obtener TramiteSitID para situaciones archivada y envio de titulo
			Berke.DG.DBTab.Tramite_Sit ts = new Berke.DG.DBTab.Tramite_Sit( db );			
			string str_tramite = ((int) GlobalConst.Marca_Tipo_Tramite.REGISTRO).ToString() + "," +
							     ((int) GlobalConst.Marca_Tipo_Tramite.RENOVACION).ToString();
			string str_situacion = ((int) GlobalConst.Situaciones.TITULO_ENVIADO).ToString() + "," +
								   ((int) GlobalConst.Situaciones.ARCHIVADA).ToString();
			ts.Dat.SituacionID.Filter = ObjConvert.GetFilter(str_situacion);
			ts.Dat.TramiteID.Filter = ObjConvert.GetFilter(str_tramite);
			ts.Adapter.ReadAll();
			string str_tramitesitid = "";
			for (ts.GoTop(); !ts.EOF; ts.Skip()) {
				if (str_tramitesitid == "") {
					str_tramitesitid = ts.Dat.ID.AsString;
				} else {
					str_tramitesitid = str_tramitesitid + "," + ts.Dat.ID.AsString;
				}
			}

			#endregion Obtener TramiteSitID para situaciones archivada y envio de titulo

			try
			{
				db.IniciarTransaccion();				

				for (inTB.GoTop();!inTB.EOF;inTB.Skip())
				{
					//inTB.Dat.MarcaID.AsInt;
					marca.Adapter.ReadByID(inTB.Dat.MarcaID.AsInt);
                    marca.Edit();

                    #region Eliminar Atenciones Por Marca
                    //Si la marca tiene atenciones por marca se deben eliminar
                    //[ggaleano 07-08-2015]
                    if ((marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA)
                        || (marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
                        || (marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE))
                    {
                        if (marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE)
                        {
                            //Eliminamos las atenciones por marca
                            am.ClearFilter();
                            am.Dat.MarcaID.Filter = marca.Dat.ID.AsInt;
                            am.Adapter.ReadAll();

                            for (am.GoTop(); !am.EOF; am.Skip())
                            {
                                am.Delete();
                                am.Adapter.DeleteRow();
                            }
                        }

                        marca.Dat.TipoAtencionxMarca.Value = System.DBNull.Value;
                        marca.Dat.IDTipoAtencionxMarca.Value = System.DBNull.Value;
                    }
                    #endregion Eliminar Atenciones Por Marca

                    #region Eliminar Instrucciones
                    //Eliminamos instruciones de Marcas
                    //NO_RENOVAR = 3
                    //NO_ENVIAR_AVISOS_RENOVACION = 5
                    //[ggaleano 07-08-2015]
                    ei.ClearFilter();
                    ei.Dat.InstruccionTipoID.Filter = ObjConvert.GetFilter(Convert.ToInt32(GlobalConst.InstruccionTipo.NO_RENOVAR).ToString() + ',' + Convert.ToInt32(GlobalConst.InstruccionTipo.NO_ENVIAR_AVISOS_RENOVACION).ToString());
                    ei.Dat.ExpedienteID.Filter = inTB.Dat.ExpedienteID.AsInt;
                    ei.Adapter.ReadAll();

                    for (ei.GoTop(); !ei.EOF; ei.Skip())
                    {
                        ei.Delete();
                        ei.Adapter.DeleteRow();
                    }

                    //Eliminar instrucciones de Vigilancia
                    //NO_ENVIAR_AVISOS_OPO = 7
			        //NO_ENVIAR_2DO_AVISOS_OPO = 10
			        //NO_ENVIAR_AVISO_OPO_REGEXT = 47
                    if (eliminarInstruccionesVigilancia)
                    {
                        ei.ClearFilter();
                        ei.Dat.InstruccionTipoID.Filter = ObjConvert.GetFilter(Convert.ToInt32(GlobalConst.InstruccionTipo.NO_ENVIAR_AVISOS_OPO).ToString() + ',' +
                                                                Convert.ToInt32(GlobalConst.InstruccionTipo.NO_ENVIAR_2DO_AVISOS_OPO).ToString() + ',' +
                                                                Convert.ToInt32(GlobalConst.InstruccionTipo.NO_ENVIAR_AVISO_OPO_REGEXT).ToString());
                        ei.Dat.ExpedienteID.Filter = inTB.Dat.ExpedienteID.AsInt;
                        ei.Adapter.ReadAll();

                        for (ei.GoTop(); !ei.EOF; ei.Skip())
                        {
                            ei.Delete();
                            ei.Adapter.DeleteRow();
                        }
                    }

                    #endregion Eliminar Instrucciones

                    int marCli = marca.Dat.ClienteID.AsInt;
					marca.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt;
					marca.PostEdit();
					marca.Adapter.UpdateRow();

					// Se debe modificar solo el expediente del REG o REN
					
					expe.Adapter.ReadByID(inTB.Dat.ExpedienteID.AsInt);

					string [] sits = str_tramitesitid.Split(',');
					bool EsArchivada = false;

					foreach (string s in sits)
					{
						if (expe.Dat.TramiteSitID.AsString == s)
						{
							EsArchivada = true;
							break;
						}
					}

					#region Expediente_ClienteCambio
					eCC.NewRow(); 
					eCC.Dat.ExpedienteID.Value = expe.Dat.ID.Value;   //int Oblig.

					if (!EsArchivada)
					{
						eCC.Dat.ClienteAntID.Value = expe.Dat.ClienteID.Value;   //int Oblig.
					}
					else
					{
						eCC.Dat.ClienteAntID.Value = marCli;
					}

					eCC.Dat.CambioFecha	.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
					eCC.Dat.FuncionarioID.Value = inTB.Dat.PropietarioID.AsInt;//USUARIO ID
					eCC.Dat.Obs.Value = inTB.Dat.Denominacion.AsString;//OBS
					if (inTB.Dat.Registro.AsInt > 0) eCC.Dat.CorrespondenciaID.Value = inTB.Dat.Registro.AsInt;// CORRESPONDENCIA ID
					eCC.PostNewRow(); 
					eCC.Adapter.InsertRow();
					#endregion Expediente_ClienteCambio

					if(!EsArchivada) 
					{						
						#region Actualizar Expediente ***
						expe.Edit();
						expe.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt;
						expe.PostEdit();
						expe.Adapter.UpdateRow();
						#endregion Actualizar Expediente
						
					}

					
					#region modificar datos
					
					//Obtengo los expedientes
					/*
					expe.Dat.MarcaID.Filter = inTB.Dat.MarcaID.AsInt;

					expe.Adapter.ReadAll();

					for (expe.GoTop();!expe.EOF;expe.Skip())
					{
						#region Expediente_ClienteCambio
						eCC.NewRow(); 
						
						eCC.Dat.ExpedienteID.Value = expe.Dat.ID.Value;   //int Oblig.
						eCC.Dat.ClienteAntID.Value = expe.Dat.ClienteID.Value;   //int Oblig.
						eCC.Dat.CambioFecha	.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
						eCC.Dat.FuncionarioID.Value = inTB.Dat.PropietarioID.AsInt;//USUARIO ID
						eCC.Dat.Obs.Value = inTB.Dat.Denominacion.AsString;//OBS
						if (inTB.Dat.Registro.AsInt > 0) eCC.Dat.CorrespondenciaID.Value = inTB.Dat.Registro.AsInt;// CORRESPONDENCIA ID
						eCC.PostNewRow(); 
						eCC.Adapter.InsertRow();
						#endregion Expediente_ClienteCambio
												
						if( str_tramitesitid.IndexOf(expe.Dat.TramiteSitID.AsString) == -1 ) {						
							#region Actualizar Expediente ***
							expe.Edit();
							expe.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt;
							expe.PostEdit();
							expe.Adapter.UpdateRow();
							#endregion Actualizar Expediente

							#region Actualizar OrdenTrabajo
							
							  [BUG#315]
							if (expe.Dat.OrdenTrabajoID.AsInt > 0) 
							{
								ot.Adapter.ReadByID(expe.Dat.OrdenTrabajoID.AsInt);
								ot.Edit();
								ot.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt;
								ot.Dat+.AtencionID.SetNull();
								ot.Dat.RefCliente.SetNull();
								ot.PostEdit();
								ot.Adapter.UpdateRow();
							}
							
							#endregion Actualizar OrdenTrabajo
						}
					}

					*/
					#endregion


				}
				db.Commit();
				db.CerrarConexion();				
			}
			catch( Exception e )
			{
				db.RollBack();
				db.CerrarConexion();
				throw new Exception(" Error en CambioCliente Upsert" + e.Message );
			}

	
			#region Asigacion de Valores de Salida
		
			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Entero			.Value = DBNull.Value;   //Int32
			outTB.Dat.Alfa			.Value = DBNull.Value;   //String
			outTB.Dat.Fecha			.Value = DBNull.Value;   //DateTime
			outTB.Dat.Logico			.Value = true;   //Boolean

			outTB.PostNewRow(); 
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
		}

	
	} // End Upsert class

#endregion CambioCliente.	Upsert

#region CambioCliente.  UpsertTVS
	public class UpsertTVS: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.vMarcaClientePropietarioTVS inTB	= new Berke.DG.ViewTab.vMarcaClientePropietarioTVS( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			// Tabla : Expediente_ClienteCambio
			Berke.DG.DBTab.Expediente_ClienteCambio eCC = new Berke.DG.DBTab.Expediente_ClienteCambio( db );
			
			try
			{
				db.IniciarTransaccion();				

				for (inTB.GoTop();!inTB.EOF;inTB.Skip())
				{
					expe.Adapter.ReadByID(inTB.Dat.ExpedienteID.AsInt);

					#region Expediente_ClienteCambio
					eCC.NewRow(); 
					eCC.Dat.ExpedienteID.Value = expe.Dat.ID.Value;   //int Oblig.
					eCC.Dat.ClienteAntID.Value = expe.Dat.ClienteID.Value;   //int Oblig.
					eCC.Dat.CambioFecha	.Value = System.DateTime.Now.Date;   //smalldatetime Oblig.
					eCC.Dat.FuncionarioID.Value = inTB.Dat.PropietarioID.AsInt;//USUARIO ID
					eCC.Dat.Obs.Value = inTB.Dat.Denominacion.AsString;//OBS
					if (inTB.Dat.Registro.AsInt > 0) eCC.Dat.CorrespondenciaID.Value = inTB.Dat.Registro.AsInt;// CORRESPONDENCIA ID
					eCC.PostNewRow(); 
					eCC.Adapter.InsertRow();
					#endregion Expediente_ClienteCambio

					#region Actualizar Expediente 
					expe.Edit();
					expe.Dat.ClienteID.Value = inTB.Dat.ClienteID.AsInt;
					expe.PostEdit();
					expe.Adapter.UpdateRow();
					#endregion Actualizar Expediente
		
				}
				db.Commit();
				db.CerrarConexion();				
			}
			catch( Exception e )
			{
				db.RollBack();
				db.CerrarConexion();
				throw new Exception(" Error en CambioCliente Upsert" + e.Message );
			}

	
			#region Asigacion de Valores de Salida
		
			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Entero			.Value = DBNull.Value;   //Int32
			outTB.Dat.Alfa			.Value = DBNull.Value;   //String
			outTB.Dat.Fecha			.Value = DBNull.Value;   //DateTime
			outTB.Dat.Logico			.Value = true;   //Boolean

			outTB.PostNewRow(); 
		
			#endregion Asigacion de Valores de Salida

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
		}

	
	} // End Upsert class

#endregion CambioCliente.  UpsertTVS
}// end namespace 

/* Entrada para el fwk.Config

				<action code="CambioCliente_Upsert" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.CambioCliente.Upsert,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/





