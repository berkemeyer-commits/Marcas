
#region Corresp.	ReadList
namespace Berke.Marcas.BizActions.Corresp
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
		
			Berke.DG.ViewTab.vCorrespondencia inTB	= new Berke.DG.ViewTab.vCorrespondencia( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object TrabajoTipo		= inTB.Dat.TrabajoTipo.Value;
			object PrioridadID		= inTB.Dat.PrioridadID.Value;
			object ID_min			= inTB.Dat.ID.Value;
			object Nro_min			= inTB.Dat.Nro.Value;
			object Anio				= inTB.Dat.Anio.Value;
			object FechaAlta_min	= inTB.Dat.FechaAlta.Value;
			object RefCorresp		= inTB.Dat.RefCorresp.Value;
			object Nombre			= inTB.Dat.Nombre.Value;
			object ClienteID		= inTB.Dat.ClienteID.Value;
			object Estado			= inTB.Dat.Estado.Value;
			object FuncionarioID	= inTB.Dat.FuncionarioID.Value;

			object AreaID		= inTB.Dat.AreaID		.Value ;

			object FuncAreaID		= inTB.Dat.FuncAreaID.Value;
			object Acusado			= inTB.Dat.Acusado.Value;
			object Facturable		= inTB.Dat.Facturable.Value;
			

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;
			object Nro_max		= inTB.Dat.Nro.Value;
			object FechaAlta_max		= inTB.Dat.FechaAlta.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vCorrespondencia outTB = new Berke.DG.ViewTab.vCorrespondencia( db );

			#region Filtros


			outTB.Dat.TrabajoTipo.		Filter = ObjConvert.GetSqlPattern	( TrabajoTipo );
			outTB.Dat.PrioridadID.		Filter = PrioridadID;
			outTB.Dat.ID.				Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Nro.				Filter = new DSFilter( Nro_min, Nro_max );
			outTB.Dat.Anio.				Filter = Anio;
			outTB.Dat.FechaAlta.		Filter = new DSFilter( FechaAlta_min, FechaAlta_max );
			outTB.Dat.RefCorresp.		Filter = ObjConvert.GetSqlPattern	( RefCorresp );
			outTB.Dat.Nombre.			Filter = ObjConvert.GetSqlPattern	( Nombre );

			outTB.Dat.ClienteID.		Filter = ClienteID ;

			outTB.Dat.Estado.			Filter = Estado;
			outTB.Dat.FuncionarioID.	Filter = FuncionarioID;
	
			//outTB.Dat.AreaID.			Filter = AreaID;
            //Se filtra por el AreaID de la distribución, ya que al realizarse la asignación se puede asignar la correspondencia
            //a otra área
            outTB.Dat.CorAreaID.Filter = AreaID;


			outTB.Dat.FuncAreaID.		Filter = FuncAreaID;
			outTB.Dat.Acusado.			Filter = Acusado;
			outTB.Dat.Facturable.		Filter = Facturable;

			//outTB.Dat.PrioridadID.		Filter = PrioridadID;

			outTB.Dat.TrabajoTipo.		Filter = ObjConvert.GetSqlPattern( TrabajoTipo );
			#endregion Filtros
			
			outTB.Dat.Anio.Order	= 1;
			outTB.Dat.Nro.Order		= 2;

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


#endregion  Corresp.	ReadList