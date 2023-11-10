
#region NizaEdicion.	Read
namespace Berke.Marcas.BizActions.NizaEdicion
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
			db.ServerName   = (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.DBTab.NizaEdicion inTB	= new Berke.DG.DBTab.NizaEdicion( cmd.Request.RawDataSet.Tables[0]);
		

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango

			*/
			#endregion
		
			#region Asigacion de Valores de Salida
		
			// NizaEdicion
			Berke.DG.DBTab.NizaEdicion outTB	=   new Berke.DG.DBTab.NizaEdicion();

			outTB.Dat.ID			.Filter = inTB.Dat.ID.Value ;  
			outTB.Dat.Descrip		.Filter = ObjConvert.GetSqlPattern( inTB.Dat.Descrip.AsString );		//nvarchar Oblig.
			outTB.Dat.Vigente		.Filter = inTB.Dat.Vigente.Value;		//bit Oblig.
			outTB.Dat.FechaDesde	.Filter = inTB.Dat.FechaDesde.Value;	//smalldatetime Oblig.
			outTB.Dat.FechaHasta	.Filter = inTB.Dat.FechaHasta.Value;	//smalldatetime
			outTB.Dat.Abrev			.Filter = ObjConvert.GetSqlPattern( inTB.Dat.Abrev.AsString );			//nvarchar

			outTB.InitAdapter( db );
			string buf = outTB.Adapter.ReadAll_CommandString();
			outTB.Adapter.ReadAll();

			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();

		}

	
	} // End Read class


}// end namespace 

#endregion Read