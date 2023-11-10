
#region Documento.	ReadList
namespace Berke.Marcas.BizActions.Documento
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
		
			Berke.DG.ViewTab.vDocum inTB	= new Berke.DG.ViewTab.vDocum( cmd.Request.RawDataSet.Tables[0]);

			#region Parametros
		
			object DocRefExt		= inTB.Dat.DocRefExt.Value;
			object ActaAnio		= inTB.Dat.ActaAnio.Value;
			object DocTipoID		= inTB.Dat.DocTipoID.Value;
			object ExpeID		= inTB.Dat.ExpeID.Value;
			object EsEscritoVario		= inTB.Dat.EsEscritoVario.Value;
			object ActaNro		= inTB.Dat.ActaNro.Value;
			object ID_min		= inTB.Dat.ID.Value;
			object DocNro		= inTB.Dat.DocNro.Value;
			object DocAnio		= inTB.Dat.DocAnio.Value;
			object Fecha_min		= inTB.Dat.Fecha.Value;
			object Descrip		= inTB.Dat.Descrip.Value;

			inTB.Skip();
			object ID_max		= inTB.Dat.ID.Value;
			object Fecha_max		= inTB.Dat.Fecha.Value;

			#endregion Parametros

			Berke.DG.ViewTab.vDocum outTB = new Berke.DG.ViewTab.vDocum( db );

			#region Filtros


			outTB.Dat.DocRefExt.		Filter = ObjConvert.GetSqlPattern	( DocRefExt );
			outTB.Dat.ActaAnio.		Filter = ActaAnio;
			outTB.Dat.DocTipoID.		Filter = DocTipoID;
			outTB.Dat.ExpeID.		Filter = ExpeID;
			outTB.Dat.EsEscritoVario.		Filter = EsEscritoVario;
			outTB.Dat.ActaNro.		Filter = ActaNro;
			outTB.Dat.ID.		Filter = new DSFilter( ID_min, ID_max );
			outTB.Dat.DocNro.		Filter = DocNro;
			outTB.Dat.DocAnio.		Filter = DocAnio;
			outTB.Dat.Fecha.		Filter = new DSFilter( Fecha_min, Fecha_max );
			outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern	( Descrip );
	
			#endregion Filtros
			
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
#endregion Documento.	ReadList