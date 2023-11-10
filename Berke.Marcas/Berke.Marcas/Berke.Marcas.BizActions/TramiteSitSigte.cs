#region TramiteSitSgte.	ReadForSelect
namespace Berke.Marcas.BizActions.TramiteSitSgte
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadForSelect: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida

			Berke.DG.DBTab.Tramite_SitSgte sigte = new Berke.DG.DBTab.Tramite_SitSgte(db);
			sigte.Dat.TramiteSitID.Filter	= inTB.Dat.Entero.AsInt; // TramiteSitID
			sigte.Adapter.ReadAll( 200 );
		
			// ListTab
			Berke.DG.ViewTab.ListTab	outTB	= new Berke.DG.ViewTab.ListTab();
			Berke.DG.DBTab.Tramite_Sit	ts		= new Berke.DG.DBTab.Tramite_Sit( db );
			Berke.DG.DBTab.Situacion	sit		= new Berke.DG.DBTab.Situacion( db );
			
			for( sigte.GoTop(); !sigte.EOF; sigte.Skip() )
			{
				ts.Adapter.ReadByID( sigte.Dat.TramiteSitSgteID.AsInt );
				sit.Adapter.ReadByID( ts.Dat.SituacionID.AsInt );

				outTB.NewRow(); 
				outTB.Dat.ID		.Value = sigte.Dat.TramiteSitSgteID.AsInt;   //Int32
				outTB.Dat.Descrip	.Value = sit.Dat.Descrip.AsString;   //String
				outTB.PostNewRow(); 
			}		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}

	
	} // End ReadForSelect class


}// end namespace 


#endregion ReadForSelect


#region SituacionSiguiente.	ReadList
namespace Berke.Marcas.BizActions.SituacionSiguiente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadList: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.vSituacionSigte inTB	= new Berke.DG.ViewTab.vSituacionSigte( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object TramiteSitID		= inTB.Dat.TramiteSitID.Value;
			object TramiteSitSgteID		= inTB.Dat.TramiteSitSgteID.Value;
			object TramiteID		= inTB.Dat.TramiteID.Value;
			object id_min		= inTB.Dat.id.Value;
			object T_Orig		= inTB.Dat.T_Orig.Value;
			object Origen		= inTB.Dat.Origen.Value;
			object Destino		= inTB.Dat.Destino.Value;

			inTB.Skip();
			object id_max		= inTB.Dat.id.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vSituacionSigte outTB = new Berke.DG.ViewTab.vSituacionSigte( db );

			#region Filtros


			outTB.Dat.TramiteSitID.		Filter = TramiteSitID;
			outTB.Dat.TramiteSitSgteID.		Filter = TramiteSitSgteID;
			outTB.Dat.TramiteID.		Filter = TramiteID;
			outTB.Dat.id.		Filter = new DSFilter( id_min, id_max );
			outTB.Dat.T_Orig.		Filter = ObjConvert.GetSqlPattern	( T_Orig );
			outTB.Dat.Origen.		Filter = ObjConvert.GetSqlPattern	( Origen );
			outTB.Dat.Destino.		Filter = ObjConvert.GetSqlPattern	( Destino );
	
			#endregion Filtros
			
//			string comando = outTB.Adapter.ReadAll_CommandString();
			outTB.Dat.T_Orig.Order = 1;
			outTB.Dat.Origen.Order = 2;
			outTB.Dat.Destino.Order = 3;

			outTB.Adapter.ReadAll( 1500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();

		}

	
	} // End ReadList class


}// end namespace 

/*  Catalogo de la Action en fwk.Config 

					
*/


/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.SituacionSiguiente


*/



#endregion  SituacionSiguiente.	ReadList