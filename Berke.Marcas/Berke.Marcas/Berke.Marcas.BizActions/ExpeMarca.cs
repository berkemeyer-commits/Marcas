
#region ExpeMarca.	ReadList
namespace Berke.Marcas.BizActions.ExpeMarca
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
		
			Berke.DG.ViewTab.vExpeMarca inTB	= new Berke.DG.ViewTab.vExpeMarca( cmd.Request.RawDataSet.Tables[0]);
			#region Parametros
		
			object MarcaNuestra		= inTB.Dat.MarcaNuestra.Value;
			object VencimientoFecha_min		= inTB.Dat.VencimientoFecha.Value;
			object MarcaActiva		= inTB.Dat.MarcaActiva.Value;
			object PropietarioID		= inTB.Dat.PropietarioID.Value;
			object AgenteLocalID		= inTB.Dat.AgenteLocalID.Value;
			object RegistroAnio		= inTB.Dat.RegistroAnio.Value;
			object OtNro_min		= inTB.Dat.OtNro.Value;
			object RegistroNro_min		= inTB.Dat.RegistroNro.Value;
			object ActaNro_min		= inTB.Dat.ActaNro.Value;
			object ActaAnio		= inTB.Dat.ActaAnio.Value;
			object ClienteID		= inTB.Dat.ClienteID.Value;
			object TramiteSitID		= inTB.Dat.TramiteSitID.Value;
			object TramiteID		= inTB.Dat.TramiteID.Value;
			object OtAnio		= inTB.Dat.OtAnio.Value;
			object ExpedienteID_min		= inTB.Dat.ExpedienteID.Value;
			object Denominacion		= inTB.Dat.Denominacion.Value;
			object MarcaID_min		= inTB.Dat.MarcaID.Value;
			object Vigilada			= inTB.Dat.Vigilada.Value;
			object Sustituida		= inTB.Dat.Sustituida.Value;
			object StandBy			= inTB.Dat.StandBy.Value;

			inTB.Skip();
			object VencimientoFecha_max		= inTB.Dat.VencimientoFecha.Value;
			object OtNro_max				= inTB.Dat.OtNro.Value;
			object RegistroNro_max			= inTB.Dat.RegistroNro.Value;
			object ActaNro_max				= inTB.Dat.ActaNro.Value;
			object ExpedienteID_max			= inTB.Dat.ExpedienteID.Value;
			object DenomEmpieza				= inTB.Dat.Denominacion.Value;
			object MarcaID_max				= inTB.Dat.MarcaID.Value;
			#endregion Parametros

			Berke.DG.ViewTab.vExpeMarca outTB = new Berke.DG.ViewTab.vExpeMarca( db );

			#region Filtros

			outTB.Dat.MarcaNuestra		.Filter = MarcaNuestra;
			outTB.Dat.VencimientoFecha	.Filter = new DSFilter( VencimientoFecha_min, VencimientoFecha_max );
			outTB.Dat.MarcaActiva		.Filter = MarcaActiva;
			outTB.Dat.PropietarioID		.Filter = PropietarioID;
			outTB.Dat.AgenteLocalID		.Filter = AgenteLocalID;


			#region Registro			
			string regs = ObjConvert.AsString( RegistroNro_min );
			if( regs.IndexOf(",") != -1 )
			{
				outTB.Dat.RegistroNro.Filter = new DSFilter( new System.Collections.ArrayList(regs.Split( ((String)",").ToCharArray() ))  );
			}
			else{ // Normal

				outTB.Dat.RegistroNro.		Filter = new DSFilter( RegistroNro_min, RegistroNro_max );
				outTB.Dat.RegistroAnio.		Filter = RegistroAnio;
			
			}
			#endregion Registro			


			outTB.Dat.OtNro.		Filter = new DSFilter( OtNro_min, OtNro_max );
			outTB.Dat.ActaNro.		Filter = new DSFilter( ActaNro_min, ActaNro_max );
			outTB.Dat.ActaAnio.		Filter = ActaAnio;
			outTB.Dat.ClienteID.		Filter = ClienteID;
			outTB.Dat.TramiteSitID.		Filter = TramiteSitID;
			outTB.Dat.TramiteID.		Filter = TramiteID;
			outTB.Dat.OtAnio.		Filter = OtAnio;
			outTB.Dat.ExpedienteID.		Filter = new DSFilter( ExpedienteID_min, ExpedienteID_max );
			outTB.Dat.Denominacion.		Filter = ObjConvert.GetSqlPattern	( Denominacion );
			outTB.Dat.MarcaID.			Filter = new DSFilter( MarcaID_min, MarcaID_max );

			outTB.Dat.Vigilada		.Filter = Vigilada;
			outTB.Dat.StandBy		.Filter = StandBy;
			outTB.Dat.Sustituida	.Filter = Sustituida;

			string strEmpieza = ObjConvert.AsString( DenomEmpieza );
			if( strEmpieza.Trim() != "" )
			{
				string defWhere = outTB.Adapter.DefaultWhere;

				defWhere += " AND " + " mar.Denominacion LIKE "+ "'"+ strEmpieza + "%' ";
			
				outTB.Adapter.SetDefaultWhere( defWhere );

			}

			#endregion Filtros
			string comando = outTB.Adapter.ReadAll_CommandString();

			outTB.Dat.Denominacion.Order = 1;

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


#endregion  ExpeMarca.	ReadList


#region ExpeMarca.	CambioSitByID
namespace Berke.Marcas.BizActions.ExpeMarca
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	
	public class CambioSitByID: IAction
	{	
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	= (string) Config.GetConfigParam("CURRENT_SERVER");		

			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
			Berke.DG.ExpeMarCambioSitDG outDG	= new Berke.DG.ExpeMarCambioSitDG();

			#region Asigacion de Valores de Salida
		
			int cont = 0;
			// Expediente
			Berke.DG.DBTab.Expediente expe	= outDG.Expediente ;
			
			expe.InitAdapter( db );
			expe.Adapter.ReadByID( inTB.Dat.Entero.AsInt );
			cont = expe.RowCount;

		
			// Expediente_Situacion
			Berke.DG.DBTab.Expediente_Situacion expeSit	= outDG.Expediente_Situacion ;		
			expeSit.InitAdapter( db );
			expeSit.Dat.ExpedienteID.Filter = inTB.Dat.Entero.AsInt;
			expeSit.Dat.ID.Order = -1;
			expeSit.Adapter.ReadAll();
			cont = expeSit.RowCount;


			// Expediente_Situacion_bkp
			Berke.DG.DBTab.Expediente_Situacion expeSit_bkp	= outDG.Expediente_Situacion_bkp ;		
			expeSit_bkp.InitAdapter( db );
			expeSit_bkp.Dat.ExpedienteID.Filter = inTB.Dat.Entero.AsInt;
			expeSit_bkp.Dat.ID.Order = -1;
			expeSit_bkp.Adapter.ReadAll();
			cont = expeSit_bkp.RowCount;


			// Marca
			Berke.DG.DBTab.Marca mar	= outDG.Marca ;
			mar.InitAdapter( db );
			mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );	
			cont = mar.RowCount;

			// MarcaRegRen
			Berke.DG.DBTab.MarcaRegRen regRen	= outDG.MarcaRegRen ;

			regRen.InitAdapter( db );
			regRen.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			regRen.Adapter.ReadAll();
			cont = regRen.RowCount;

			// vExpeMarca
			Berke.DG.ViewTab.vExpeMarca vExpeMar	= outDG.vExpeMarca ;
			vExpeMar.InitAdapter( db );
			vExpeMar.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			vExpeMar.Adapter.ReadAll();
			cont = vExpeMar.RowCount;

			// regRenPadre
			Berke.DG.DBTab.Expediente expePadre = new Berke.DG.DBTab.Expediente(db);
			expePadre.Adapter.ReadByID(expe.Dat.ExpedienteID.AsInt);
						
			Berke.DG.DBTab.MarcaRegRen regRenPadre = outDG.MarcaRegRenPadre;
			regRenPadre.InitAdapter( db );
			regRenPadre.Adapter.ReadByID(expePadre.Dat.MarcaRegRenID.AsInt);

			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = outDG.AsDataSet();
		
			cmd.Response = new Response( tmp_OutDS );	

			db.CerrarConexion();

		}

	
	} // End CambioSitByID class


}// end namespace 

/* Entrada para el fwk.Config
*/


#endregion CambioSitByID