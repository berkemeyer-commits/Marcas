using System;
using System.Data;


#region TramiteSit.	Read_AsViewTab
namespace Berke.Marcas.BizActions.TramiteSit
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Read_AsViewTab: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName   = (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.vTramiteSit inTB	= new Berke.DG.ViewTab.vTramiteSit( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida
		
			// vTramiteSit
			Berke.DG.ViewTab.vTramiteSit outTB	=   new Berke.DG.ViewTab.vTramiteSit();


			outTB.Dat.ID			.Filter = ObjConvert.GetSqlStringValue	( inTB.Dat.ID			.AsString );  
			outTB.Dat.TramiteID		.Filter = ObjConvert.GetSqlStringValue	( inTB.Dat.TramiteID	.AsString );
			outTB.Dat.SituacionID	.Filter = ObjConvert.GetSqlStringValue	( inTB.Dat.SituacionID	.AsString );
			outTB.Dat.Descrip		.Filter = ObjConvert.GetSqlPattern		( inTB.Dat.Descrip		.AsString );
			outTB.Dat.Vigente		.Filter = ObjConvert.GetSqlStringValue	( inTB.Dat.Vigente		.AsString );
			outTB.Dat.Automatico	.Filter = ObjConvert.GetSqlStringValue	( inTB.Dat.Automatico	.AsString );

		
			outTB.InitAdapter( db );
			outTB.Adapter.ReadAll();
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( outTB.AsDataSet() );	
			db.CerrarConexion();

		}

	
	} // End Read_AsViewTab class


}// end namespace 


#endregion Read_AsViewTab

#region ReadByPattern / ReadByID  ( Devuelven TramiteSitDS )
namespace Berke.Marcas.BizActions.TramiteSit
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
	#region ReadByPattern
	public class ReadByPattern: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.DSHelpers.ParamDS inDS;
			Berke.Libs.Base.DSHelpers.SimpleEntryDS outDS;
			outDS = new Berke.Libs.Base.DSHelpers.SimpleEntryDS();
			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();


			Berke.Libs.Base.DSHelpers.DSTab tb = new DSTab();

			#region Parametros de Entrada
			
			// SimpleInt

			tb.Bind(inDS.Tables["SimpleInt"] );
			int pattern = tb.AsInt("Value");

			#endregion Parametros de Entrada

			#region Asignacion de RESPONSE

			// SimpleEntry

			tb.Bind(outDS.Tables["SimpleEntry"] );

			Berke.DG.ViewTab.vTramiteSit vTrmSit = new Berke.DG.ViewTab.vTramiteSit( db );
			vTrmSit.Dat.TramiteID.Filter = pattern;
			vTrmSit.Dat.Vigente.Filter = true;
			vTrmSit.Dat.Descrip.Order = 1;

			vTrmSit.Adapter.ReadAll();
			for( vTrmSit.GoTop();!vTrmSit.EOF; vTrmSit.Skip() )
			{
				tb.NewRow();

				tb.Dat["ID"]	= vTrmSit.Dat.ID.AsInt ;
				tb.Dat["Descr"] = vTrmSit.Dat.Descrip.AsString;

				tb.PostNewRow();
			}
//			tb.SetOrder("Descr", 1);
//			tb.Sort();
			outDS.Tables.Remove("SimpleEntry");
			outDS.Tables.Add( tb.Table );
			#endregion Asignacion de RESPONSE

			cmd.Response = new Response( outDS );      
			db.CerrarConexion();

		}
	} // class ReadByPattern END

	#endregion


} // namespace Berke.Marcas.BizActions.TramiteSit END

#endregion ReadByPattern / ReadByID  ( Devuelven TramiteSitDS )

#region TramiteSit.	ReadByParam  ( AsDbTab )
namespace Berke.Marcas.BizActions.TramiteSit
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadByParam: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.DBTab.Tramite_Sit inTB	= new Berke.DG.DBTab.Tramite_Sit( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida
		
			// Tramite_Sit
			Berke.DG.DBTab.Tramite_Sit outTB	=   new Berke.DG.DBTab.Tramite_Sit( db );

			outTB.Dat.ID			.Filter = inTB.Dat.ID			.Value;   //int PK  Oblig.
			outTB.Dat.TramiteID		.Filter = inTB.Dat.TramiteID	.Value;   //int Oblig.
			outTB.Dat.SituacionID	.Filter = inTB.Dat.SituacionID	.Value;   //int Oblig.
			outTB.Dat.Plazo			.Filter = inTB.Dat.Plazo		.Value;   //int Oblig.
			outTB.Dat.UnidadID		.Filter = inTB.Dat.UnidadID		.Value;   //int Oblig.
			outTB.Dat.Vigente		.Filter = inTB.Dat.Vigente		.Value;   //bit Oblig.
			outTB.Dat.Automatico	.Filter = inTB.Dat.Automatico	.Value;   //bit
			outTB.Dat.Orden			.Filter = inTB.Dat.Orden		.Value;   //int
			outTB.Adapter.ReadAll();

			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}

	
	} // End ReadByParam class


}// end namespace 


#endregion ReadByParam


#region TramiteSit.	FechaVencim
namespace Berke.Marcas.BizActions.TramiteSit
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizActions;

	public class FechaVencim: IAction
	{	
		public void Execute( Command cmd ) 
		{		 

			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			#region Parametros
			int			tramiteSitID	=	inTB.Dat.Entero.AsInt;
			DateTime	fechaSit		=	inTB.Dat.Fecha.AsDateTime;
			#endregion
			
			Berke.DG.DBTab.Tramite_Sit tramSit = new Berke.DG.DBTab.Tramite_Sit( db );
			tramSit.Adapter.ReadByID( tramiteSitID );
			
			int plazo	= tramSit.Dat.Plazo.AsInt;
			int unidad	= tramSit.Dat.UnidadID.AsInt;

			DateTime fechaVencimiento = Lib.FechaMasPlazo(fechaSit,plazo,unidad,db);

			#region Asigacion de Valores de Salida
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();
			outTB.NewRow(); 
			outTB.Dat.Fecha	.Value = fechaVencimiento;  
			outTB.PostNewRow(); 
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
			db.CerrarConexion();

		}

	
	} // End FechaVencim class


}// end namespace 


#endregion FechaVencim

// desde aquí


#region TramiteSit.	ReadList
namespace Berke.Marcas.BizActions.TramiteSit
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
		
			Berke.DG.ViewTab.vTramiteSit inTB	= new Berke.DG.ViewTab.vTramiteSit( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object SituacionID		= inTB.Dat.SituacionID.Value;
			object TramiteID		= inTB.Dat.TramiteID.Value;
			object ID_min		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Vigente		= inTB.Dat.Vigente.Value;
			object Automatico		= inTB.Dat.Automatico.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vTramiteSit outTB = new Berke.DG.ViewTab.vTramiteSit( db );

			#region Filtros


			outTB.Dat.SituacionID.		Filter = SituacionID;
			outTB.Dat.TramiteID.		Filter = TramiteID;
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Vigente.		Filter = Vigente;
			outTB.Dat.Automatico.		Filter = Automatico;
	
			#endregion Filtros
			
			outTB.Adapter.ReadAll( 500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();

		}

	
	} // End ReadList class


}// end namespace 





#endregion  TramiteSit.	ReadList