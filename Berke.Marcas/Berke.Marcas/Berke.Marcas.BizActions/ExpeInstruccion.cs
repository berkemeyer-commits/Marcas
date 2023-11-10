
#region ExpeInstruccion.	Read
namespace Berke.Marcas.BizActions.ExpeInstruccion
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
		
			Berke.DG.DBTab.Expediente_Instruccion inTB	= new Berke.DG.DBTab.Expediente_Instruccion( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID		= inTB.Dat.ID.Value;
			object MarcaID		= inTB.Dat.MarcaID.Value;
			object ExpedienteID		= inTB.Dat.ExpedienteID.Value;
			object CorrespondenciaMovID		= inTB.Dat.CorrespondenciaMovID.Value;
			object InstruccionTipoID		= inTB.Dat.InstruccionTipoID.Value;
			object Fecha		= inTB.Dat.Fecha.Value;
			object FuncionarioID		= inTB.Dat.FuncionarioID.Value;
			object CorrespondenciaID		= inTB.Dat.CorrespondenciaID.Value;
			object Obs		= inTB.Dat.Obs.Value;

			#endregion Parametros

			Berke.DG.DBTab.Expediente_Instruccion outTB = new Berke.DG.DBTab.Expediente_Instruccion( db );

			#region Filtros


			outTB.Dat.ID.		Filter = ID;
			outTB.Dat.MarcaID.		Filter = MarcaID;
			outTB.Dat.ExpedienteID.		Filter = ExpedienteID;
			outTB.Dat.CorrespondenciaMovID.		Filter = CorrespondenciaMovID;
			outTB.Dat.InstruccionTipoID.		Filter = InstruccionTipoID;
			outTB.Dat.Fecha.		Filter = Fecha;
			outTB.Dat.FuncionarioID.		Filter = FuncionarioID;
			outTB.Dat.CorrespondenciaID.		Filter = CorrespondenciaID;
			outTB.Dat.Obs.		Filter = ObjConvert.GetSqlPattern	( Obs );
	
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

#endregion  ExpeInstruccion.	Read


#region ExpeInstruccion.	ReadList
namespace Berke.Marcas.BizActions.ExpeInstruccion
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
		
			Berke.DG.ViewTab.vInstruccion inTB	= new Berke.DG.ViewTab.vInstruccion( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID		= inTB.Dat.ID.Value;
			object MarcaID		= inTB.Dat.MarcaID.Value;
			object ExpedienteID		= inTB.Dat.ExpedienteID.Value;
			object CorrespondenciaMovID		= inTB.Dat.CorrespondenciaMovID.Value;
			object InstruccionTipoID		= inTB.Dat.InstruccionTipoID.Value;
			object Fecha		= inTB.Dat.Fecha.Value;
			object Descrip		= inTB.Dat.Descrip.Value;
			object Abrev		= inTB.Dat.Abrev.Value;
			object CorrespNro		= inTB.Dat.CorrespNro.Value;
			object CorrespAnio		= inTB.Dat.CorrespAnio.Value;
			object CorrespFechaAlta		= inTB.Dat.CorrespFechaAlta.Value;
			object Obs		= inTB.Dat.Obs.Value;
			object FuncionarioID		= inTB.Dat.FuncionarioID.Value;
			object Nick		= inTB.Dat.Nick.Value;
			object userName		= inTB.Dat.userName.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vInstruccion outTB = new Berke.DG.ViewTab.vInstruccion( db );

			#region Filtros


			outTB.Dat.ID.		Filter = ID;
			outTB.Dat.MarcaID.		Filter = MarcaID;
			outTB.Dat.ExpedienteID.		Filter = ExpedienteID;
			outTB.Dat.CorrespondenciaMovID.		Filter = CorrespondenciaMovID;
			outTB.Dat.InstruccionTipoID.		Filter = InstruccionTipoID;
			outTB.Dat.Fecha.		Filter = Fecha;
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
			outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern	( Abrev );
			outTB.Dat.CorrespNro.		Filter = CorrespNro;
			outTB.Dat.CorrespAnio.		Filter = CorrespAnio;
			outTB.Dat.CorrespFechaAlta.		Filter = CorrespFechaAlta;
			outTB.Dat.Obs.		Filter = ObjConvert.GetSqlPattern	( Obs );
			outTB.Dat.FuncionarioID.		Filter = FuncionarioID;
			outTB.Dat.Nick.		Filter = ObjConvert.GetSqlPattern	( Nick );
			outTB.Dat.userName.		Filter = ObjConvert.GetSqlPattern	( userName );
	
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



#endregion  ExpeInstruccion.	ReadList