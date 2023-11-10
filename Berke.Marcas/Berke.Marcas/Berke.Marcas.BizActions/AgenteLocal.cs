
#region AgenteLocal.	ReadList
namespace Berke.Marcas.BizActions.AgenteLocal
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
			db.ServerName	= (string) Config.GetConfigParam("CURRENT_SERVER");
		
			Berke.DG.ViewTab.vAgenteLocal inTB	= new Berke.DG.ViewTab.vAgenteLocal( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ID_min		= inTB.Dat.ID.Value;
			object Nombre		= inTB.Dat.Nombre.Value;
			object NroMatricula		= inTB.Dat.NroMatricula.Value;
//			object EntidadID		= inTB.Dat.EntidadID.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vAgenteLocal outTB = new Berke.DG.ViewTab.vAgenteLocal( db );

			#region Filtros


			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.Nombre.		Filter = ObjConvert.GetSqlPattern	( Nombre );
			outTB.Dat.NroMatricula.		Filter = NroMatricula;
//			outTB.Dat.EntidadID.		Filter = EntidadID;
	
			#endregion Filtros
			
			//outTB.Adapter.ReadAll( 5000 );
            outTB.Adapter.ReadAll(50000);

			#region Response
	
			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );

			db.CerrarConexion();
	
			#endregion Response
		}

	
	} // End ReadList class


}// end namespace 







#endregion  AgenteLocal.	ReadList

#region AgenteLocal.	ReadForSelect
namespace Berke.Marcas.BizActions.AgenteLocal
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	//	using Libs.Base.DSHelpers;
	
	public class ReadForSelect: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			#region Obtener Valores
			Berke.DG.ViewTab.vAgenteLocal vAgenteLocal = new Berke.DG.ViewTab.vAgenteLocal( db );
			vAgenteLocal.Dat.ID.Filter		=	ObjConvert.GetSqlStringValue( inTB.Dat.Entero.AsString );
			vAgenteLocal.Dat.Nombre.Filter	=	ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			vAgenteLocal.Dat.Nombre.Order	= 1;
			vAgenteLocal.Adapter.ReadAll(3000);
			#endregion
		
			#region Asignacion de Valores de Salida
		
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			for( vAgenteLocal.GoTop()	;	! vAgenteLocal.EOF	;	vAgenteLocal.Skip() )
			{
				outTB.NewRow();
				outTB.Dat.ID			.Value = vAgenteLocal.Dat.ID.AsInt;			
				outTB.Dat.Descrip		.Value = vAgenteLocal.Dat.Nombre.AsString + " Mat.:"+ vAgenteLocal.Dat.NroMatricula.AsString;
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

