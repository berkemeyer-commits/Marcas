using System;
using Berke.Libs.Base;

namespace Berke.Libs.Base.Helpers
{
	using Framework.Core;

	public class Calc
	{

		#region SitFechaVencim
		public static DateTime SitFechaVencim(DateTime fechaSit, int tramiteSitID )
		{
			AccesoDB db		= new AccesoDB();

			db.DataBaseName	= (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
			return SitFechaVencim(fechaSit, tramiteSitID, db );
		}

		public static DateTime SitFechaVencim(DateTime fechaSit, int tramiteSitID, AccesoDB db )
		{
			//fechaSit: fecha de la situación
			//tramiteSitID: ID de la situación

//			DBGateway dbg	= new DBGateway();

			Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit( db );
			trmSit.Adapter.ReadByID( tramiteSitID);
//			dbg.Tramite_Sit.ReadById(db, tramiteSitID);

//			int plazo = dbg.Tramite_Sit.Plazo.AsInt;
//			int unidad = dbg.Tramite_Sit.UnidadID.AsInt;
			int plazo = trmSit.Dat.Plazo.AsInt;
			int unidad = trmSit.Dat.UnidadID.AsInt;

			DateTime fechaFin = fechaSit;

			if ( unidad == (int) GlobalConst.Unidad.DIAS_HABILES ) //si es días hábiles
			{
				for (int i=1; i<=plazo; i++)
				{
					fechaFin = fechaSit.AddDays(i);
					if (fechaFin.DayOfWeek == DayOfWeek.Saturday || fechaFin.DayOfWeek == DayOfWeek.Sunday)
					{
						plazo++;
					}
					else
					{
						Berke.DG.DBTab.Feriado fer = new Berke.DG.DBTab.Feriado( db );
						fer.Dat.Fecha.Filter = new DSHelpers.DSFilter(fechaFin);
						fer.Adapter.ReadAll();
						if( fer.RowCount > 0 )plazo++;

//						dbg.Feriado.Clear();
//						dbg.Feriado.Fecha.FilterValue = fechaFin;
//						dbg.Feriado.ReadAll(db);
//						if (!dbg.Feriado.EOF) plazo++;
					}
				}
			}
			fechaSit = fechaFin;
			return fechaSit;
		}

		#endregion SitFechaVencim

		#region OrdenTrabajoNro
		public static int OrdenTrabajoNro(int tramiteID)
		{
			int nro;

			AccesoDB db		= new AccesoDB();
			db.DataBaseName	= (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
//			DBGateway dbg	= new DBGateway();

			db.Sql = "select max( nro ) from OrdenTrabajo where Anio = " + DateTime.Today.Year.ToString();

			object valor = db.getValue();
			if( valor == DBNull.Value )
			{
				nro = 0;			
			}
			else
			{
				nro = (int)valor;
			}

			nro ++;
			db.CerrarConexion();

//			Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo( db );
//
//			ot.Dat.Anio.Filter = DateTime.Today.Year;
//			ot.Dat.Nro.Order = -1;
//
//			ot.Adapter.ReadAll();
//
//			if( !ot.EOF )
//			{
//				nro = ot.Dat.Nro.AsInt + 1;
//			}
//			else
//			{
//				nro = 1;
//			}

			return nro;

//			dbg.vOrdenTrabajoNroAnio.ID.FilterValue = tramiteID;
//			dbg.vOrdenTrabajoNroAnio.Anio.FilterValue = DateTime.Today.Year;
//			dbg.vOrdenTrabajoNroAnio.Nro.Order = -1;
//          dbg.vOrdenTrabajoNroAnio.ReadAll(db);
//
//			if (!dbg.vOrdenTrabajoNroAnio.EOF)
//			{
//				int c = dbg.vOrdenTrabajoNroAnio.RowCount;
//				dbg.vOrdenTrabajoNroAnio.Go(--c);
//				nro = dbg.vOrdenTrabajoNroAnio.Nro.AsInt;
//				nro = ++nro;
//			}
//			else
//			{
//				nro = 1;
//			}
//            return nro;
		}
		
		#endregion OrdenTrabajoNro

		#region RegFechaVencim
		public static DateTime RegFechaVencim( DateTime fechaConcesion)
		{
			return fechaConcesion.AddYears(10);
		}

//		public static DateTime RegFechaVencim( int ExpedienteID )
//		{
//			DateTime regFechaVencim;
//			AccesoDB db		= new AccesoDB();
//
//			db.DataBaseName	= (string) Config.GetConfigParam("CURRENT_DATABASE");
//			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");
//
//			DBGateway dbg	= new DBGateway();
//			
//			dbg.Expediente.ReadById(db, ExpedienteID);
//			
//			dbg.MarcaRegRen.ExpedienteID.FilterValue = dbg.Expediente.ExpedienteID.AsInt;
//			dbg.MarcaRegRen.ReadAll(db);
//			if (!dbg.MarcaRegRen.EOF)
//			{
//				if( dbg.MarcaRegRen.ConcesionFecha.IsNull )
//				{
//					regFechaVencim = DateTime.Today;
//				}
//				else{
//					regFechaVencim = dbg.MarcaRegRen.ConcesionFecha.AsDateTime.AddYears(10);
//				}
//			}
//			else
//			{
//				//TODO: ERROR!!!
//				regFechaVencim = DateTime.Today;
//			}
//
//			return regFechaVencim;
//		}
		

		#endregion RegFechaVencim
		

	}
}
