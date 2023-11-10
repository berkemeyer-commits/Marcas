//using System;
//
//namespace Berke.Marcas.BizActions
//{
//	/// <summary>
//	/// Summary description for ClaseClase.
//	/// </summary>
//	public class ClaseClase
//	{
//		public ClaseClase()
//		{
//			//
//			// TODO: Add constructor logic here
//			//
//		}
//	}
//}


// desde aquí



#region ClaseClase.	ReadList
namespace Berke.Marcas.BizActions.ClaseClase
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
		
			Berke.DG.ViewTab.vClaseClase inTB	= new Berke.DG.ViewTab.vClaseClase( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object ClaseRelacID			= inTB.Dat.ClaseRelacID.Value;
			object ClaseID				= inTB.Dat.ClaseID.Value;
			object ID_min				= inTB.Dat.ID.Value;
			object ClaseDescrip			= inTB.Dat.ClaseDescrip.Value;
			object ClaseRelacDescrip	= inTB.Dat.ClaseRelacDescrip.Value;
			object Ancestro				= inTB.Dat.Ancestro.Value;

			inTB.Skip();
			object ID_max				= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vClaseClase outTB = new Berke.DG.ViewTab.vClaseClase( db );

			#region Filtros


			outTB.Dat.ClaseRelacID.		Filter = ClaseRelacID;
			outTB.Dat.ClaseID.		Filter = ClaseID;
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.ClaseDescrip.		Filter = ObjConvert.GetSqlPattern	( ClaseDescrip );
			outTB.Dat.ClaseRelacDescrip.		Filter = ObjConvert.GetSqlPattern	( ClaseRelacDescrip );
			outTB.Dat.Ancestro.		Filter = Ancestro;
	
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

/*  Catalogo de la Action en fwk.Config 

					

*/


/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.ClaseClase


*/



#endregion  ClaseClase.	ReadList


#region ClaseClase.	ReadForSelect
namespace Berke.Marcas.BizActions.ClaseClase
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
			
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			#region Asigacion de Valores de Salida
		
			Berke.DG.ViewTab.vClaseClase ClaseClase = new Berke.DG.ViewTab.vClaseClase( db );

			//Funcionario.Dat.Funcionario	.Filter	= ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			ClaseClase.Dat.ClaseDescrip .Filter = ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString ); 

			//Funcionario.Dat.ID			.Filter	= inTB.Dat.Entero.Value;
			ClaseClase.Dat.ID				.Filter = inTB.Dat.Entero.Value;

			//Funcionario.Dat.Funcionario.Order = 1;
			ClaseClase.Dat.ClaseDescrip.Order   = 1;

			//Funcionario.Adapter.ReadAll();
			ClaseClase.Adapter.ReadAll ();

			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			for( ClaseClase.GoTop(); !ClaseClase.EOF; ClaseClase.Skip() )
			{
				outTB.NewRow(); 
				outTB.Dat.ID		.Value = ClaseClase.Dat.ID.AsInt;			//Int32
				outTB.Dat.Descrip	.Value = ClaseClase.Dat.ClaseDescrip.AsString;   //String
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