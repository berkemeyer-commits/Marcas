
using System;

namespace Berke.Marcas.BizActions.Clase
{
	using Framework.Core;
	using Framework.Channels;
	using System.Data;
	using System.Data.SqlClient;
	using Libs.Base;
	using Libs.Base.Helpers;
	using Libs.Base.DSHelpers;
	using Berke.Marcas.BizDocuments.Helpers;
	
	#region ReadByPattern
	public class ReadByPattern: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.DSHelpers.ParamDS inDS;
			Berke.Libs.Base.DSHelpers.SimpleEntryDS outDS;
			outDS = new Berke.Libs.Base.DSHelpers.SimpleEntryDS();
			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;

			AccesoDB db		= new AccesoDB();

			db.DataBaseName	 = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
			
//			DBGateway dbg	= new DBGateway();
			TableGateway tb = new TableGateway();

			// Pattern
			tb.Bind(inDS.Tables["SimpleString"] );
			string  patron = tb.AsString("Value");
			bool isPatronNull = (patron.Trim() == "");

			// ID de NizaEdicion
			tb.Bind(inDS.Tables["SimpleInt"] );
			int  edicionID = tb.AsInt("Value");
			bool isEdicionNull = tb.IsNull("Value");

			#region Asignacion de RESPONSE
			Berke.DG.ViewTab.vClase vClase = new Berke.DG.ViewTab.vClase( db );

			if ( !isEdicionNull )
			{
//				dbg.vClase.Descrip.FilterValue = isPatronNull ? "" : "%"+patron+"%";
//				dbg.vClase.NizaEdicionID.FilterValue = edicionID.ToString();

				vClase.Dat.NizaEdicionID.Filter = edicionID;
				vClase.Dat.Descrip.Filter = isPatronNull ? "" : "%"+patron+"%";
			}

//			dbg.vClase.ReadAll(db);
			vClase.Adapter.ReadAll();
			// SimpleEntry
			tb.Bind(outDS.Tables["SimpleEntry"] );
			for( vClase.GoTop(); ! vClase.EOF ; vClase.Skip() )
			{
				tb.NewRow();
				tb.SetValue("ID", vClase.Dat.ID.AsInt );
				tb.SetValue("Descr", vClase.Dat.Nro.AsString.PadLeft(2) + " (" + vClase.Dat.NizaAbrev.AsString.Trim() + ")");
				tb.PostNewRow();
			}
			
			#endregion Asignacion de RESPONSE


			cmd.Response = new Response( outDS );       
			db.CerrarConexion();

		}
	} // class ReadByPattern END
	#endregion 

	#region ReadByID
	public class ReadByID: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.DSHelpers.ParamDS inDS;
//			Berke.Marcas.BizDocuments.Marca.ClaseDS outDS;
//			outDS = new Berke.Marcas.BizDocuments.Marca.ClaseDS();
			inDS  = ( Berke.Libs.Base.DSHelpers.ParamDS ) cmd.Request.RawDataSet;
			AccesoDB db		= new AccesoDB();
			db.DataBaseName	= (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		

//			DBGateway dbg	= new DBGateway();
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase( db );
			TableGateway tb = new TableGateway();

			#region Parametros de Entrada
			// SimpleInt
			tb.Bind(inDS.Tables["SimpleInt"] );
			int       	SimpleInt_Value = tb.AsInt("Value");

			#endregion Parametros de Entrada

			#region Asignacion de RESPONSE
			
//			dbg.Clase.ReadById(db, SimpleInt_Value);
			clase.Adapter.ReadByID( SimpleInt_Value );
//			// NizaEdicion
//			dbg.NizaEdicion.ReadById(db,dbg.Clase.NizaEdicionID.AsInt);
//			tb.Bind(outDS.Tables["NizaEdicion"] );
//			for( dbg.NizaEdicion.Go(0);!dbg.NizaEdicion.EOF; dbg.NizaEdicion.Skip() )
//			{
//				tb.NewRow();
//				tb.SetValue("ID", dbg.NizaEdicion.ID.AsInt );
//				tb.SetValue("Descrip", dbg.NizaEdicion.Descrip.AsString );
//				tb.SetValue("Vigente", dbg.NizaEdicion.Vigente.AsBoolean );
//				tb.SetValue("FechaDesde", dbg.NizaEdicion.FechaDesde.AsDateTime );
//				tb.SetValue("FechaHasta", dbg.NizaEdicion.FechaHasta.AsDateTime );
//				tb.PostNewRow();
//			}
//			
//			// ClaseTipo
//			dbg.ClaseTipo.ReadById(db,dbg.Clase.ClaseTipoID.AsInt);
//			tb.Bind(outDS.Tables["ClaseTipo"] );
//			for( dbg.ClaseTipo.Go(0);!dbg.ClaseTipo.EOF; dbg.ClaseTipo.Skip() )
//			{
//				tb.NewRow();
//				tb.SetValue("ID", dbg.ClaseTipo.ID.AsInt );
//				tb.SetValue("Descrip", dbg.ClaseTipo.Descrip.AsString );
//				tb.SetValue("Abrev", dbg.ClaseTipo.Abrev.AsString );
//				tb.PostNewRow();
//			}
//
//			// Clase
//			
//			tb.Bind(outDS.Tables["Clase"] );
//			for( dbg.Clase.Go(0);!dbg.Clase.EOF; dbg.Clase.Skip() )
//			{
//				tb.NewRow();
//				tb.SetValue("ID", dbg.Clase.ID.AsInt );
//				tb.SetValue("Nro", dbg.Clase.Nro.AsInt );
//				tb.SetValue("NizaEdicionID", dbg.Clase.NizaEdicionID.AsInt );
//				tb.SetValue("ClaseTipoID", dbg.Clase.ClaseTipoID.AsInt );
//				tb.SetValue("Descrip", dbg.Clase.Descrip.AsString );
//				tb.SetValue("DescripBreve", dbg.Clase.DescripBreve.AsString );
//				tb.PostNewRow();
//			}
//
//			// Clase_Idioma
//			tb.Bind(outDS.Tables["Clase_Idioma"] );
//			dbg.Clase_Idioma.ClaseID.FilterValue = SimpleInt_Value;
//			dbg.Clase_Idioma.ReadAll(db);
//
//			for( dbg.Clase_Idioma.Go(0);!dbg.Clase_Idioma.EOF; dbg.Clase_Idioma.Skip() )
//			{
//				tb.NewRow();
//				tb.SetValue("ID", dbg.Clase_Idioma.ID.AsInt );
//				tb.SetValue("ClaseID", dbg.Clase_Idioma.ClaseID.AsInt );
//				tb.SetValue("IdiomaID", dbg.Clase_Idioma.IdiomaID.AsInt );
//				tb.SetValue("Descrip", dbg.Clase_Idioma.Descrip.AsString );
//				tb.SetValue("DescripBreve", dbg.Clase_Idioma.DescripBreve.AsString );
//				dbg.CIdioma.ReadById(db,dbg.Clase_Idioma.IdiomaID.AsInt);
//				tb.SetValue("Idioma", dbg.CIdioma.descrip.AsString );
//				tb.PostNewRow();
//			}
//
			#endregion Asignacion de RESPONSE


//			cmd.Response = new Response( outDS );   
			cmd.Response = new Response(  clase.AsDataSet() );
			db.CerrarConexion();
	
		}
	} // class ReadByID END
	#endregion 

} // namespace Berke.Marcas.BizActions.Clase END


#region Clase.	ReadList
namespace Berke.Marcas.BizActions.Clase
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
		
			Berke.DG.ViewTab.vClase inTB	= new Berke.DG.ViewTab.vClase( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
				object Nro						= inTB.Dat.Nro.Value;
				object Descrip					= inTB.Dat.Descrip.Value;
				object NizaEdicionID			= inTB.Dat.NizaEdicionID.Value;
				object NizaAbrev				= inTB.Dat.NizaAbrev.Value;
				
				object ID_min					= inTB.Dat.ID.Value;
							
				inTB.Skip();
				object ID_max					= inTB.Dat.ID.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vClase outTB = new Berke.DG.ViewTab.vClase( db );

			#region Filtros
				
				outTB.Dat.ID.				Filter = new DSFilter(ID_min, ID_max);
				//outTB.Dat.ID.				Filter = ID;
				outTB.Dat.Nro.				Filter = Nro;
				outTB.Dat.Descrip.			Filter = ObjConvert.GetSqlPattern	( Descrip );
				outTB.Dat.NizaEdicionID.	Filter = NizaEdicionID;
				outTB.Dat.NizaAbrev.		Filter = ObjConvert.GetSqlPattern	( NizaAbrev );
	
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




#endregion  Clase.	ReadList

