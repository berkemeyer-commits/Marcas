
#region HI.	ReadList
namespace Berke.Marcas.BizActions.HI
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
		
			Berke.DG.ViewTab.vHIresu inTB	= new Berke.DG.ViewTab.vHIresu( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object Anio		= inTB.Dat.Anio.Value;
			object Nro_min		= inTB.Dat.Nro.Value;
			object AltaFecha_min		= inTB.Dat.AltaFecha.Value;

			inTB.Skip();
			object Nro_max		= inTB.Dat.Nro.Value;
			object AltaFecha_max		= inTB.Dat.AltaFecha.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vHIresu outTB = new Berke.DG.ViewTab.vHIresu( db );

			#region Filtros

			outTB.Dat.Anio.		Filter = Anio;
			outTB.Dat.Nro.		Filter = new DSFilter( Nro_min, Nro_max );
			outTB.Dat.AltaFecha.		Filter = new DSFilter( AltaFecha_min, AltaFecha_max );
	
			#endregion Filtros
			
			outTB.Dat.Anio.Order	= 1;
			outTB.Dat.Nro.Order		= 2;

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


/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.HI


*/



#endregion  HI.	ReadList