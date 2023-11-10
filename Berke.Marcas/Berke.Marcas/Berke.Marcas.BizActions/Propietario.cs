//Modificado
#region Propietario.	ReadForSelect
namespace Berke.Marcas.BizActions.Propietario
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
			
			
			#region Obtener Datos
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.DBTab.Propietario propietario = new Berke.DG.DBTab.Propietario( db );
			propietario.Dat.ID.Filter		=	ObjConvert.GetSqlStringValue( inTB.Dat.Entero.AsString );
			propietario.Dat.Nombre.Filter	=	ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			propietario.Adapter.ReadAll();

			#endregion 

			#region Asigacion de Valores de Salida
		
			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();
			
			for( propietario.GoTop()	;	! propietario.EOF	;	propietario.Skip() )
			{
				outTB.NewRow();
				outTB.Dat.ID			.Value = propietario.Dat.ID.AsInt;			
				outTB.Dat.Descrip		.Value = propietario.Dat.Nombre.AsString;	
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



#region Propietario.	ReadList
namespace Berke.Marcas.BizActions.Propietario
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
			
		
			Berke.DG.ViewTab.vPropietario inTB	= new Berke.DG.ViewTab.vPropietario( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object Descrip		= inTB.Dat.Descrip.Value;
			object ID_min		= inTB.Dat.ID.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vPropietario outTB = new Berke.DG.ViewTab.vPropietario( db );

			#region Filtros


			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
	
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


#endregion  Propietario.	ReadList