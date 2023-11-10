
#region Pais.	Read
namespace Berke.Marcas.BizActions.Pais
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
		
			Berke.DG.DBTab.CPais inTB	= new Berke.DG.DBTab.CPais( cmd.Request.RawDataSet.Tables[0]);
		
			object idpais	= inTB.Dat.idpais	.Value;   //int PK  Oblig.
			object paisalfa = inTB.Dat.paisalfa	.Value;   //nvarchar Oblig.
			object descrip	= inTB.Dat.descrip	.Value;   //nvarchar Oblig.
			object paistel	= inTB.Dat.paistel	.Value;   //nvarchar Oblig.
			object idbanco	= inTB.Dat.idbanco	.Value;   //varchar
			object abrev	= inTB.Dat.abrev	.Value;   //nvarchar

			#region Ejemplos para filtro
			/* Funciones para filtro		Clases: Libs.Base. ObjConvert , Libs.Base.DSHelpers.DSFilter
		
				...Filter = ObjConvert.GetSqlPattern	( arg ) <- busca arg como subcadena (%arg%)
				...Filter = ObjConvert.GetSqlStringValue( arg )	<- ignora si arg es vacio
				...Filter = new DSFilter( NroDesde, NroHasta );	<- busca un rango


				string buffer  = "123, 444, 533"; <- IDs a incluir 
				....Dat.ID.Filter = new DSFilter( new System.Collections.ArrayList(buffer.Split( ((String)",").ToCharArray() ))  );

			*/
			#endregion Ejemplos para filtro
		
			#region Asigacion de Valores de Salida
		
			#region CPais
		
			Berke.DG.DBTab.CPais outTB	=   new Berke.DG.DBTab.CPais();


			outTB.Dat.idpais	.Filter = idpais;   //int PK  Oblig.
			outTB.Dat.paisalfa	.Filter = ObjConvert.GetSqlPattern(paisalfa);   //nvarchar Oblig.
			outTB.Dat.descrip	.Filter = ObjConvert.GetSqlPattern(descrip);   //nvarchar Oblig.
			outTB.Dat.paistel	.Filter = ObjConvert.GetSqlPattern(paistel);   //nvarchar Oblig.
			outTB.Dat.idbanco	.Filter = ObjConvert.GetSqlPattern(idbanco);   //varchar
			outTB.Dat.abrev		.Filter = ObjConvert.GetSqlPattern(abrev);   //nvarchar


		
			outTB.InitAdapter( db );
			outTB.Dat.descrip.Order = 1;
			outTB.Adapter.ReadAll();

			#endregion CPais
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

	
	} // End Read class


}// end namespace 

#endregion Read