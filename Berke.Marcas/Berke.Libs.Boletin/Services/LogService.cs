using System;
using Berke.Libs.Boletin.Libs;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// LogService.cs
	/// Provee servicios para el registro de Logs
	/// Autor: Marcos Báez
	/// </summary>
	public class LogService
	{
		#region Atributos
		Berke.Libs.Base.Helpers.AccesoDB db;
		Berke.DG.DBTab.BoletinLog blog;
		#endregion Atributos

		public LogService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			blog = new Berke.DG.DBTab.BoletinLog(db);
		}
		public void newEntry(string op)
		{
			blog.NewRow();
			blog.Dat.funcionarioID.Value = Utils.getCurrentFuncionarioID(db);
			blog.Dat.operacion.Value     = op;
		}
		public void saveEntry(string status)
		{
			blog.Dat.estado.Value = status;
			blog.Dat.fecha.Value  = System.DateTime.Now;
			blog.PostNewRow();
			blog.Adapter.InsertRow();
		}
		public void setFilter(string filter)
		{
			blog.Dat.filter.Value        = filter;
		}
		public void setObs(string obs)
		{
			blog.Dat.obs.Value        = obs;
		}
		public void setStats(BoletinStats stats)
		{
			blog.Dat.nproc.Value    = stats.nproc;
			blog.Dat.nskip.Value    = stats.nskip;
			blog.Dat.nexclude.Value = stats.nexcl;
		}
	}
}
