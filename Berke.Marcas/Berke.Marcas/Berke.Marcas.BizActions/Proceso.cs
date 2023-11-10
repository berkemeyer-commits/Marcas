
#region Proceso.	ReadForSelect
namespace Berke.Marcas.BizActions.Proceso
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
		

			#endregion
		
			#region Asigacion de Valores de Salida
		
			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			Berke.DG.DBTab.Proceso proceso = new Berke.DG.DBTab.Proceso( db );
			proceso.Adapter.ReadAll();

			for( proceso.GoTop(); ! proceso.EOF	; proceso.Skip() )
			{
				outTB.NewRow(); 
				outTB.Dat.ID	.Value	= proceso.Dat.ID.AsInt;   //Int32
				outTB.Dat.Descrip.Value	= proceso.Dat.Descrip.AsString;
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



#region Proceso.	ReadList
namespace Berke.Marcas.BizActions.Proceso
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
		
			Berke.DG.DBTab.Proceso inTB	= new Berke.DG.DBTab.Proceso( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object PracticaID		= inTB.Dat.PracticaID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.DBTab.Proceso outTB = new Berke.DG.DBTab.Proceso( db );

			#region Filtros


			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.PracticaID.		Filter = PracticaID;
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

	
	} // End ReadList class


}// end namespace 

/*  Catalogo de la Action en fwk.Config 

					

*/


/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.Proceso


*/



#endregion  Proceso.	ReadList