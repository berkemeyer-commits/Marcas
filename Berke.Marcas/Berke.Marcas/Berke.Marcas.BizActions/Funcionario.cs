
#region Funcionario.	ReadList  

namespace Berke.Marcas.BizActions.Funcionario
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
			
		
			Berke.DG.ViewTab.vFuncionario inTB	= new Berke.DG.ViewTab.vFuncionario( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object Funcionario		= inTB.Dat.Funcionario.Value;
//			object Documento		= inTB.Dat.Documento.Value;
//			object RUC				= inTB.Dat.RUC.Value;
			object Usuario			= inTB.Dat.Usuario.Value;
			object PriNombre		= inTB.Dat.NombreCorto.Value;
//			object SegNombre		= inTB.Dat.SegNombre.Value;
//			object PriApellido		= inTB.Dat.PriApellido.Value;
//			object SegApellido		= inTB.Dat.SegApellido.Value;
			
			
			object ID_min			= inTB.Dat.ID.Value;
						
			inTB.Skip();
			object ID_max			= inTB.Dat.ID.Value;



			#endregion Parametros

			Berke.DG.ViewTab.vFuncionario outTB = new Berke.DG.ViewTab.vFuncionario( db );

			#region Filtros


			//outTB.Dat.ID.				Filter = ID;
			outTB.Dat.ID.				Filter = new DSFilter(ID_min, ID_max);
			//outTB.Dat.Puntaje.				Filter = new DSFilter( Puntaje_min, Puntaje_max );
			outTB.Dat.Funcionario.		Filter = ObjConvert.GetSqlPattern	( Funcionario );
//			outTB.Dat.Documento.		Filter = ObjConvert.GetSqlPattern	( Documento );
//			outTB.Dat.RUC.				Filter = ObjConvert.GetSqlPattern	( RUC );
			outTB.Dat.Usuario.			Filter = ObjConvert.GetSqlPattern	( Usuario );
			outTB.Dat.NombreCorto.		Filter = ObjConvert.GetSqlPattern	( PriNombre );
//			outTB.Dat.SegNombre.		Filter = ObjConvert.GetSqlPattern	( SegNombre );
//			outTB.Dat.PriApellido.		Filter = ObjConvert.GetSqlPattern	( PriApellido );
//			outTB.Dat.SegApellido.		Filter = ObjConvert.GetSqlPattern	( SegApellido );
	
			#endregion Filtros

//			outTB.Dat.EstadoID.Filter = 1;
			outTB.Dat.Funcionario.Order = 1;
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

#endregion  Funcionario.	ReadList   



#region Funcionario.	ReadForSelect
namespace Berke.Marcas.BizActions.Funcionario
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
		
			Berke.DG.ViewTab.vFuncionario Funcionario = new Berke.DG.ViewTab.vFuncionario( db );

			Funcionario.Dat.Funcionario	.Filter	= ObjConvert.GetSqlPattern( inTB.Dat.Alfa.AsString );
			
			Funcionario.Dat.Activo		.Filter = true;
//			Funcionario.Dat.EstadoID	.Filter	= 1;

            //Funcionario.Dat.Funcionario.Order = 1;
//			Funcionario.Dat.PriApellido.Order = 2;
			Funcionario.Dat.NombrePila.Order = 1;

			if (inTB.Dat.Logico.AsBoolean)
			{
				Funcionario.Dat.AreaID.Filter = inTB.Dat.Entero.AsInt;
			}
			else
			{
				Funcionario.Dat.ID			.Filter	= inTB.Dat.Entero.Value;
			}
	

			Funcionario.Adapter.ReadAll();

			// ListTab
			Berke.DG.ViewTab.ListTab outTB	=   new Berke.DG.ViewTab.ListTab();

			for( Funcionario.GoTop(); !Funcionario.EOF; Funcionario.Skip() )
			{
				outTB.NewRow(); 
				outTB.Dat.ID		.Value = Funcionario.Dat.ID.AsInt;			//Int32
				//outTB.Dat.Descrip	.Value = Funcionario.Dat.Funcionario.AsString;   //String
				outTB.Dat.Descrip	.Value = Funcionario.Dat.NombrePila.AsString;   //String
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