//Modificado Nuevo Sistema
#region Cliente.	ReadForSelect
namespace Berke.Marcas.BizActions.Cliente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
//	using Libs.Base.DSHelpers;
	
	public class ReadForSelect: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.DBTab.Cliente cliente = new Berke.DG.DBTab.Cliente( db );
			cliente.Dat.ID.Filter		=	ObjConvert.GetSqlStringValue( inTB.Dat.Entero.AsString );
			cliente.Dat.Nombre.Filter	=	ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			cliente.Adapter.ReadAll();

			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();
			
			for( cliente.GoTop()	;	! cliente.EOF	;	cliente.Skip() )
			{
				outTB.NewRow();
				outTB.Dat.ID			.Value = cliente.Dat.ID.AsInt;			
				outTB.Dat.Descrip		.Value = cliente.Dat.Nombre.AsString;	
				outTB.PostNewRow();
			}

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}

	
	} // End ReadForSelect class


}// end namespace 


#endregion ReadForSelect

#region Cliente.	ReadList
namespace Berke.Marcas.BizActions.Cliente
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

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.vCliente inTB	= new Berke.DG.ViewTab.vCliente( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vCliente outTB = new Berke.DG.ViewTab.vCliente( db );

			#region Filtros

			
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
	
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




#endregion  Cliente.	ReadList
/*
#region Cliente.	Read
namespace Berke.Marcas.BizActions.Cliente
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class Read: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.ClienteDG outDG	= new Berke.DG.ClienteDG();

			#region Ejemplos para filtro

			#endregion
		
			#region Asigacion de Valores de Salida
		
			#region CCliente
			Berke.DG.DBTab.CCliente cliente	= outDG.CCliente ;
			cliente.InitAdapter( db );
			cliente.Adapter.ReadByID(inTB.Dat.Entero.AsInt);
			#endregion CCliente
		
			#region CEntidad
			Berke.DG.DBTab.CEntidad ent	= outDG.CEntidad ;
			ent.InitAdapter( db );
			ent.Adapter.ReadByID(cliente.Dat.identidad.AsInt);
			#endregion CEntidad
		
			#region ClienteAtencion
			Berke.DG.DBTab.ClienteAtencion atencion	= outDG.ClienteAtencion ;
			atencion.InitAdapter( db );
			atencion.Dat.ClienteID.Filter = cliente.Dat.idcli.AsInt;
			atencion.Adapter.ReadAll();
			#endregion ClienteAtencion
		
			#region ClienteViaCom
			Berke.DG.DBTab.ClienteViaCom vcom	= outDG.ClienteViaCom ;
			vcom.InitAdapter( db );
			vcom.Dat.ClienteID.Filter =  cliente.Dat.idcli.AsInt;
			vcom.Adapter.ReadAll();
			#endregion ClienteViaCom
		
			#region COfiCli
			Berke.DG.DBTab.COfiCli cofi	= outDG.COfiCli ;
			cofi.InitAdapter( db );
			cofi.Dat.idcli.Filter =  cliente.Dat.idcli.AsInt;
			cofi.Adapter.ReadAll();
			#endregion COfiCli
		
			#region DOfiCli
			Berke.DG.DBTab.DOfiCli dofi	= outDG.DOfiCli ;
			dofi.InitAdapter( db );
			dofi.Dat.idoficli.Filter = cofi.Dat.idoficli.AsInt;
			dofi.Adapter.ReadAll();
			#endregion DOfiCli	
	
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );
		}

	
	} // End Read class


}// end namespace 



#endregion Read
*/