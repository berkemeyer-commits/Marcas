#region ExpeMarcaCambioSit.	ReadList
namespace Berke.Marcas.BizActions.ExpeMarcaCambioSit
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
		
			Berke.DG.ViewTab.vMarcaCambioSit inTB	= new Berke.DG.ViewTab.vMarcaCambioSit( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ClienteID		= inTB.Dat.ClienteID.Value;
			object RegistroAnio		= inTB.Dat.RegistroAnio.Value;
			object RegistroNro_min	= inTB.Dat.RegistroNro.Value;
			object ExpeID_min		= inTB.Dat.ExpeID.Value;
			object TramiteSitID		= inTB.Dat.TramiteSitID.Value;
			object TramiteID		= inTB.Dat.TramiteID.Value;
			object ActaNro_min		= inTB.Dat.ActaNro.Value;
			object ActaAnio			= inTB.Dat.ActaAnio.Value;
			object Denominacion		= inTB.Dat.Denominacion.Value;
			object AltaFecha_min	= inTB.Dat.AltaFecha.Value;
			object FuncionarioID	= inTB.Dat.FuncionarioID.Value;

			inTB.Skip();
			object RegistroNro_max	= inTB.Dat.RegistroNro.Value;
			object ExpeID_max		= inTB.Dat.ExpeID.Value;
			object ActaNro_max		= inTB.Dat.ActaNro.Value;
			object AltaFecha_max	= inTB.Dat.AltaFecha.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vMarcaCambioSit outTB = new Berke.DG.ViewTab.vMarcaCambioSit( db );

			#region Filtros

			outTB.Dat.ClienteID		.Filter = ClienteID;
			outTB.Dat.RegistroAnio	.Filter = RegistroAnio;
			outTB.Dat.RegistroNro	.Filter = new DSFilter( RegistroNro_min, RegistroNro_max );
			outTB.Dat.ExpeID		.Filter = new DSFilter( ExpeID_min, ExpeID_max );
			outTB.Dat.TramiteSitID	.Filter = TramiteSitID;
			outTB.Dat.TramiteID		.Filter = TramiteID;
			outTB.Dat.ActaNro		.Filter = new DSFilter( ActaNro_min, ActaNro_max );
			outTB.Dat.ActaAnio		.Filter = ActaAnio;
			outTB.Dat.Denominacion	.Filter = ObjConvert.GetSqlPattern	( Denominacion );
			outTB.Dat.AltaFecha		.Filter = new DSFilter( AltaFecha_min, AltaFecha_max );
			outTB.Dat.FuncionarioID	.Filter	= FuncionarioID;
	
			#endregion Filtros

			string buffer = outTB.Adapter.ReadAll_CommandString();
			
			outTB.Adapter.ReadAll( 1000 );

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );
	
			#endregion Response
			db.CerrarConexion();

		}

	
	} // End ReadList class


}// end namespace 




#endregion  ExpeMarcaCambioSit.	ReadList