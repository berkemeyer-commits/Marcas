
#region Unidad.	ReadForSelect
namespace Berke.Marcas.BizActions.Unidad
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
		
			#region Asigacion de Valores de Salida
		
			Berke.DG.DBTab.Unidad unidad = new Berke.DG.DBTab.Unidad( db );
			unidad.Dat.Descrip	.Filter	= ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			unidad.Dat.ID		.Filter	= inTB.Dat.Entero.Value;
			unidad.Adapter.ReadAll();

			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			for( unidad.GoTop(); !unidad.EOF; unidad.Skip() )
			{
				outTB.NewRow(); 
				outTB.Dat.ID		.Value = unidad.Dat.ID.AsInt;			//Int32
				outTB.Dat.Descrip	.Value = unidad.Dat.Descrip.AsString;   //String
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

#region Unidad.	Read
namespace Berke.Marcas.BizActions.Unidad
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
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.DBTab.Unidad inTB	= new Berke.DG.DBTab.Unidad( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;

			#endregion Parametros

			Berke.DG.DBTab.Unidad outTB = new Berke.DG.DBTab.Unidad( db );

			#region Filtros


			outTB.Dat.ID.		Filter = ID;
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern	( Abrev );
	
			#endregion Filtros
			
			outTB.Adapter.ReadAll( 500 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();
		}

	
	} // End Read class


}// end namespace 
#endregion 