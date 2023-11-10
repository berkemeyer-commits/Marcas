
#region PertenenciaMotivo.	ReadList
namespace Berke.Marcas.BizActions.PertenenciaMotivo
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
		
			Berke.DG.DBTab.PertenenciaMotivo inTB	= new Berke.DG.DBTab.PertenenciaMotivo( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID			= inTB.Dat.ID.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;
			object Nuestra		= inTB.Dat.Nuestra.Value;
			object Vigilada		= inTB.Dat.Vigilada.Value;
			object Sustituida	= inTB.Dat.Sustituida.Value;
			object StandBy		= inTB.Dat.StandBy.Value;
			object Parada		= inTB.Dat.Parada.Value;

			#endregion Parametros

			Berke.DG.DBTab.PertenenciaMotivo outTB = new Berke.DG.DBTab.PertenenciaMotivo( db );

			#region Filtros


			outTB.Dat.ID.			Filter = ID;
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern	( Abrev );
			outTB.Dat.Nuestra.		Filter = Nuestra;
			outTB.Dat.Vigilada.		Filter = Vigilada;
			outTB.Dat.Sustituida.	Filter = Sustituida;
			outTB.Dat.StandBy.		Filter = StandBy;
			outTB.Dat.Parada.		Filter = Parada;
	
			#endregion Filtros
//			string buff = outTB.Adapter.ReadAll_CommandString();
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



#endregion  PertenenciaMotivo.	ReadList