
#region AgenteBerke.	ReadForSelect
namespace Berke.Marcas.BizActions.AgenteBerke
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
//			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
			
			#region Asigacion de Valores de Salida
		
			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			Berke.DG.ViewTab.vAgenteBerke agente = new Berke.DG.ViewTab.vAgenteBerke( db );
			agente.Adapter.ReadAll();

			for( agente.GoTop(); ! agente.EOF	; agente.Skip() )
			{
				outTB.NewRow(); 
				outTB.Dat.ID	.Value	= agente.Dat.ID.AsInt;   //Int32
				outTB.Dat.Descrip.Value	= agente.Dat.nombre.AsString + " (Matric.: "+ agente.Dat.NroMatricula.AsString + ")";
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