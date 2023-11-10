

#region OtMarca.	ReadList
namespace Berke.Marcas.BizActions.OtMarca
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
			
		
			Berke.DG.ViewTab.vOtMarca inTB	= new Berke.DG.ViewTab.vOtMarca( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
			//object TramiteID		= inTB.Dat.TramiteID.Value;
			object TrabajoTipoID	= inTB.Dat.TrabajoTipoID.Value;
			object SituacionID		= inTB.Dat.SituacionID.Value;

			object RegistroNro_min		= inTB.Dat.RegistroNro.Value;
			object ActaNro_min		= inTB.Dat.ActaNro.Value;
			object ActaAnio		= inTB.Dat.ActaAnio.Value;
			object RegistroAnio		= inTB.Dat.RegistroAnio.Value;
			object Denominacion		= inTB.Dat.Denominacion.Value;
			object Nro_min		= inTB.Dat.Nro.Value;
			object Anio		= inTB.Dat.Anio.Value;
			object OtID_min		= inTB.Dat.OtID.Value;
			object Obs		= inTB.Dat.Obs.Value;
			object ExpClienteNombre =  inTB.Dat.ExpClienteNombre.Value;
			object ExpClienteID =  inTB.Dat.ExpClienteID.Value;

			object CorrespNro =  inTB.Dat.CorrespNro.Value;
			object CorrespAnio=  inTB.Dat.Correspanio.Value;

			

			inTB.Skip();
			object RegistroNro_max		= inTB.Dat.RegistroNro.Value;
			object ActaNro_max		= inTB.Dat.ActaNro.Value;
			object Nro_max		= inTB.Dat.Nro.Value;
			object OtID_max		= inTB.Dat.OtID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vOtMarca outTB = new Berke.DG.ViewTab.vOtMarca( db );

			#region Filtros

			//outTB.Dat.TramiteID.		Filter = TramiteID;
			outTB.Dat.TrabajoTipoID.	Filter = TrabajoTipoID;
			outTB.Dat.SituacionID.		Filter = SituacionID;
			outTB.Dat.RegistroNro.		Filter = new DSFilter( RegistroNro_min, RegistroNro_max );
			outTB.Dat.ActaNro.		Filter = new DSFilter( ActaNro_min, ActaNro_max );
			outTB.Dat.ActaAnio.		Filter = ActaAnio;
			outTB.Dat.RegistroAnio.		Filter = RegistroAnio;
			outTB.Dat.Denominacion.		Filter = ObjConvert.GetSqlPattern	( Denominacion );
			outTB.Dat.Nro.		Filter = new DSFilter( Nro_min, Nro_max );
			outTB.Dat.Anio.		Filter = Anio;
			outTB.Dat.OtID.		Filter = new DSFilter( OtID_min, OtID_max );
			outTB.Dat.Obs.		Filter = ObjConvert.GetSqlPattern	( Obs );
			outTB.Dat.ExpClienteID.		    Filter = ExpClienteID ;
			
			outTB.Dat.CorrespNro.		Filter = CorrespNro;
			outTB.Dat.Correspanio.		Filter = CorrespAnio;
	
			#endregion Filtros

			#region Orden
			   outTB.Dat.OtID.Order = 1;
			#endregion
			
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


#endregion  OtMarca.	ReadList