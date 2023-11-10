
#region DocumPlantilla.	Read
namespace Berke.Marcas.BizActions.DocumPlantilla
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
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.DBTab.DocumentoPlantilla inTB	= new Berke.DG.DBTab.DocumentoPlantilla( cmd.Request.RawDataSet.Tables[0]);
		
			#region Parametros
			object ID				= inTB.Dat.ID				.Value;   //int PK  Oblig.
			object Clave			= inTB.Dat.Clave			.Value;   //nvarchar Oblig.
			object IdiomaID			= inTB.Dat.IdiomaID			.Value;   //int Oblig.
			object TramiteID		= inTB.Dat.TramiteID		.Value;   //int
			object PlantillaHTML	= inTB.Dat.PlantillaHTML	.Value;   //ntext
			object DocumentoTipoID	= inTB.Dat.DocumentoTipoID	.Value;   //int
			object Descrip			= inTB.Dat.Descrip			.Value;   //nvarchar	

			#endregion  Parametros
	
			#region Asigacion de Valores de Salida
		
			// DocumentoPlantilla
			Berke.DG.DBTab.DocumentoPlantilla outTB	=   new Berke.DG.DBTab.DocumentoPlantilla();

			outTB.Dat.ID				.Filter = ID ;				//int PK  Oblig.
			outTB.Dat.Clave				.Filter = Clave;			//nvarchar Oblig.
			outTB.Dat.IdiomaID			.Filter = IdiomaID;			//int Oblig.
			outTB.Dat.TramiteID			.Filter = TramiteID;		//int
			outTB.Dat.PlantillaHTML		.Filter = PlantillaHTML;	//ntext
			outTB.Dat.DocumentoTipoID	.Filter = DocumentoTipoID;	//int
			outTB.Dat.Descrip			.Filter = Descrip;			//nvarchar

			outTB.InitAdapter( db );
			string comando = outTB.Adapter.ReadAll_CommandString();
			outTB.Adapter.ReadAll();
		
			#endregion 
		
			cmd.Response = new Response( outTB.AsDataSet() );	
			db.CerrarConexion();
		}

	
	} // End Read class


}// end namespace 


#endregion Read