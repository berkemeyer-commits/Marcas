			
	#region DocumentoTipo.	ReadList
		namespace Berke.Marcas.BizActions.DocumentoTipo
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
				db.ServerName = (string) Config.GetConfigParam("CURRENT_SERVER");

				Berke.DG.DBTab.DocumentoTipo inTB	= new Berke.DG.DBTab.DocumentoTipo( cmd.Request.RawDataSet.Tables[0]);

				#region Parametros
					
					object ID_min		= inTB.Dat.ID.Value;
					object Descrip		= inTB.Dat.Descrip.Value;
					object Abrev		= inTB.Dat.Abrev.Value;

					inTB.Skip();
					object ID_max		= inTB.Dat.ID.Value;

				#endregion Parametros

				Berke.DG.DBTab.DocumentoTipo outTB = new Berke.DG.DBTab.DocumentoTipo( db );

				#region Filtros

					outTB.Dat.ID.			Filter = new DSFilter(ID_min,ID_max);
					outTB.Dat.Descrip.		Filter = ObjConvert.GetSqlPattern (Descrip);
					outTB.Dat.Abrev.		Filter = ObjConvert.GetSqlPattern (Abrev);

				#endregion Filtros
			
				string buf = outTB.Adapter.ReadAll_CommandString();
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


	/*  Metodo a agregar en la clase   " MODEL "  :  Berke.Marcas.UIProcess.Model.DocumentoTipo

			
	*/



	#endregion  DocumentoTipo.	ReadList
		