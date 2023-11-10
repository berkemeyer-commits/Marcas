
#region Situacion.	Read
namespace Berke.Marcas.BizActions.Situacion
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
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE" );
			db.ServerName 	= (string) Config.GetConfigParam("CURRENT_SERVER"   );
		
			Berke.DG.DBTab.Situacion inTB	= new Berke.DG.DBTab.Situacion( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			
			Berke.DG.DBTab.Situacion outTB = new Berke.DG.DBTab.Situacion( db );

			#region Filtros
			// mbaez. si no se pasa 
			if ( ID_min.ToString() != "" && ID_max.ToString() == "" )
			{
				outTB.Dat.ID.		Filter = ID_min;
			}
			else if (ID_min.ToString() != "" && ID_max.ToString() != "" ){
				outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			}
			
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern	( Abrev );
	
			#endregion Filtros
			
			
			outTB.Dat.Descrip.Order = 1;

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







#endregion  Situacion.	Read
