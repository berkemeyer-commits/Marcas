
using System;
using System.Data;
using System.Collections;

#region Marca.	ReadByID
namespace Berke.Marcas.BizActions.Marca
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class ReadByID: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName   = (string) Config.GetConfigParam("CURRENT_SERVER");
			
			Berke.DG.ViewTab.ParamTab param	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);	

			Berke.DG.DBTab.Marca marca	=   new Berke.DG.DBTab.Marca();
				
			marca.InitAdapter( db );

			marca.Adapter.ReadByID( param.Dat.Entero.AsInt );
		
			cmd.Response = new Response( marca.AsDataSet() );	
			db.CerrarConexion();

		}
		
	
	} // End ReadByID class


}// end namespace 



#endregion ReadByID

#region ReadByID_asDG
namespace Berke.Marcas.BizActions.Marca
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
	public class ReadByID_asDG: IAction
	{	

		public Berke.DG.MarcaDG Execute( int marcaID, Berke.Libs.Base.Helpers.AccesoDB db  ) 
		{
			Berke.DG.MarcaDG marcaDG = new Berke.DG.MarcaDG();

			#region Marca
			Berke.DG.DBTab.Marca marca	= marcaDG.Marca ;
			marca.InitAdapter( db );
			marca.Adapter.ReadByID(marcaID ) ;
			#endregion Marca

			#region MarcaTipo
			Berke.DG.DBTab.MarcaTipo marcaTipo	= marcaDG.MarcaTipo ;
			marcaTipo.InitAdapter( db );
			marcaTipo.Adapter.ReadByID( marca.Dat.MarcaTipoID.AsInt );
			#endregion MarcaTipo

			#region Clase
			Berke.DG.DBTab.Clase clase	= marcaDG.Clase ;

			clase.InitAdapter( db );
			clase.Adapter.ReadByID( marca.Dat.ClaseID.AsInt );
			#endregion Clase

			#region MarcaRegRen
			Berke.DG.DBTab.MarcaRegRen regRen	= marcaDG.MarcaRegRen ;

			regRen.InitAdapter( db );
			regRen.Adapter.ReadByID( marca.Dat.MarcaRegRenID.AsInt );
			#endregion MarcaRegRen

			#region Expediente
			Berke.DG.DBTab.Expediente expe	= marcaDG.Expediente ;

			expe.InitAdapter( db );
			expe.Adapter.ReadByID( marca.Dat.ExpedienteVigenteID.AsInt );
			#endregion Expediente
	
			#region Cliente
			Berke.DG.DBTab.Cliente cliente	= marcaDG.Cliente ;

			cliente.InitAdapter( db );
			cliente.Adapter.ReadByID( marca.Dat.ClienteID.Value );
			#endregion Cliente

			#region CAgenteLocal
			Berke.DG.DBTab.CAgenteLocal agLocal	= marcaDG.CAgenteLocal ;

			agLocal.InitAdapter( db );
			agLocal.Adapter.ReadByID( marca.Dat.AgenteLocalID.Value );
			#endregion CAgenteLocal
		
			#region Propietario

			Berke.DG.DBTab.PropietarioXMarca ppm = new Berke.DG.DBTab.PropietarioXMarca( db );
			ppm.Dat.MarcaID.Filter = marcaID;
			ArrayList aPropM = ppm.Adapter.GetListOfField( ppm.Dat.PropietarioID );
		
			Berke.DG.DBTab.Propietario propietario	= marcaDG.Propietario ;
			propietario.DisableConstraints();
			if( aPropM.Count > 0 )
			{	
				propietario.InitAdapter( db );
				propietario.Dat.ID.Filter =  new DSFilter( aPropM  );
				propietario.Adapter.ReadAll();
			}
			else // Si no tiene Propietario
			{
				propietario.NewRow();
				propietario.Dat.Nombre.Value = marca.Dat.Propietario.AsString+
					" ( "+marca.Dat.ProPais.AsString+ " )";
				propietario.Dat.Direccion.Value = marca.Dat.ProDir.Value;
				propietario.PostNewRow();
			}
			#endregion Propietario

			int atencionID = 0;
			if (marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA)
			{
				atencionID = marca.Dat.IDTipoAtencionxMarca.AsInt;
				
			}
			else if (marca.Dat.TipoAtencionxMarca.AsInt == (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT)
			{
				#region Bussiness Unit
				Berke.DG.DBTab.BussinessUnit bussinessUnit = marcaDG.BussinessUnit;
				bussinessUnit.InitAdapter( db );
				bussinessUnit.Adapter.ReadByID(marca.Dat.IDTipoAtencionxMarca.AsInt);

				atencionID = bussinessUnit.Dat.AtencionID.AsInt;
				#endregion Bussiness Unit
			}
			
			#region Atencion
			Berke.DG.DBTab.Atencion atencion = marcaDG.Atencion;
			atencion.InitAdapter( db );
			atencion.Adapter.ReadByID(atencionID);
			#endregion Atencion
			
	
			return marcaDG;
		}

		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		

			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
			int marcaID = inTB.Dat.Entero.AsInt;

			Berke.DG.MarcaDG marcaDG =	Execute( marcaID, db );
		
			cmd.Response = new Response( marcaDG.AsDataSet() );	
			db.CerrarConexion();

		}

	
	} // End ReadByID_asDG class

}

#endregion ReadByID_asDG






