#region Merge.	Fill
namespace Berke.Marcas.BizActions.Merge
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Fill: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.MergeDG outDG	= new Berke.DG.MergeDG();

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion


			switch(inTB.Dat.Entero.AsInt)
			{
					#region Recaudo
				case (int) GlobalConst.MergeTipo.CARTA_RECAUDO_MARCA:

					break;
					#endregion Recaudo

					#region Concedida
				case (int) GlobalConst.MergeTipo.CARTA_CONCESION:

					break;
					#endregion Concedida

					#region Envio de Titulos
				case (int) GlobalConst.MergeTipo.CARTA_ENVIO_TITULO:
					break;
					#endregion Envio de Titulos

					#region Aviso de Vencimiento
				case (int) GlobalConst.MergeTipo.AVISOS_VENCIMIENTO:
					break;
					#endregion Aviso de Vencimiento

					#region Recaudo Constancias
				case (int) GlobalConst.MergeTipo.RECAUDO:
					break;
					#endregion Recaudo Constancias

					#region Presupuesto de Titulos
				case (int) GlobalConst.MergeTipo.PRESUPUESTO_ENV_TIT:
					break;
					#endregion Presupuesto de Titulos

					#region Constancias
				case (int) GlobalConst.MergeTipo.CONSTANCIA:
					break;
					#endregion Constancias
						
			}
		
			#region Asigacion de Valores de Salida

			#region Expediente_Situacion
			Berke.DG.DBTab.Expediente_Situacion expeSit	= outDG.Expediente_Situacion ;
			expeSit.InitAdapter( db );
			expeSit.Dat.AltaFecha.Filter = ObjConvert.GetFilter(inTB.Dat.Alfa.AsString);
			System.Collections.ArrayList list = expeSit.Adapter.GetListOfField(expeSit.Dat.ExpedienteID,true);
			#endregion Expediente_Situacion

		
			#region Expediente
			Berke.DG.DBTab.Expediente expe	= outDG.Expediente ;
			expe.InitAdapter( db );
			expe.Dat.ID.Filter= new DSFilter(list);
			expe.Dat.Nuestra.Filter = true;
			expe.Adapter.ReadAll();
			#endregion Expediente

//		
//			#region Marca
//			Berke.DG.DBTab.Marca marca	= outDG.Marca ;
//			//		marca.InitAdapter( db );
//			//		marca.Adapter.ReadAll();
//			#endregion Marca
//		
//			#region regACT
//			Berke.DG.DBTab.MarcaRegRen regACT	= outDG.regACT ;
//			//		regACT.InitAdapter( db );
//			//		regACT.Adapter.ReadAll();
//			#endregion regACT
//		
//			#region regANT
//			Berke.DG.DBTab.MarcaRegRen regANT	= outDG.regANT ;
//			regANT.InitAdapter( db );
//			regANT.Adapter.ReadAll();
//			#endregion regANT
//		
//			#region Clase
//			Berke.DG.DBTab.Clase clase	= outDG.Clase ;
//			//		clase.InitAdapter( db );
//			//		clase.Adapter.ReadAll();
//			#endregion Clase
//		
//			#region proACT
//			Berke.DG.DBTab.CPropietario proACT	= outDG.proACT ;
//			//		proACT.InitAdapter( db );
//			//		proACT.Adapter.ReadAll();
//			#endregion proACT
//		
//			#region proANT
//			Berke.DG.DBTab.CPropietario proANT	= outDG.proANT ;
//			//		proANT.InitAdapter( db );
//			//		proANT.Adapter.ReadAll();
//			#endregion proANT
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	
		}

	
	} // End Fill class


}// end namespace 

/* Entrada para el fwk.Config

				<action code="Merge_Fill" tx-mode="None" application="Framework" handler-class="Berke.Marcas.BizActions.Merge.Fill,Berke.Marcas.BizActions"
					request-class="Framework.Core.Request,fwkcommon" response-class="Framework.Core.Response,fwkcommon"
					DebugMode="true" log-header="true" log-request="true" log-response="true" />
*/


#endregion Fill