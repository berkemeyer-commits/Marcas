using System;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// MarcaRegRenService
	/// Provee servicios varios correspondientes a MarcaRegRen
	/// Autor: Marcos Báez
	/// </summary>
	public class MarcaRegRenService
	{
		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.MarcaRegRen       regren;
		#endregion Atributos

		#region Constructores
		public MarcaRegRenService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public MarcaRegRenService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			regren      = new Berke.DG.DBTab.MarcaRegRen(db);
		}
		#endregion Constructores

		#region setters
		public void setRegRenID(int regrenID)
		{
			regren.Adapter.ReadByID(regrenID);
		}
		#endregion setters

		#region getters
		/// <summary>
		/// Obtiene el Regren actual
		/// </summary>
		/// <returns>RegRen actual del objeto</returns>
		public Berke.DG.DBTab.MarcaRegRen getRegRen()
		{
			return regren;
		}

		public Berke.DG.DBTab.MarcaRegRen getRegRenByExpe(int expeID)
		{
			regren.Dat.ExpedienteID.Filter = expeID;
			regren.Adapter.ReadAll();
			return regren;
		}

		public Berke.DG.DBTab.MarcaRegRen getRegRenViaExpeVig(int expeID)
		{

			Berke.DG.DBTab.Expediente       expe = new Berke.DG.DBTab.Expediente(this.db);
			expe.Adapter.ReadByID(expeID);
			regren.Adapter.ReadByID(expe.Dat.MarcaRegRenID.AsInt);
			return regren;
		}

		/// <summary>
		/// Obtiene MarcaRegRen a partir del Nro de registro
		/// </summary>
		/// <param name="registronro">Nro. de registro</param>
		/// <returns>MarcaRenRen en cuestión</returns>
		public Berke.DG.DBTab.MarcaRegRen getRegRen(int registronro)
		{			
			regren.ClearFilter();
			regren.Dat.RegistroNro.Filter = registronro;
			if (registronro != 0)
			{
				regren.Adapter.ReadAll();
			} 
			else 
			{
				regren = new Berke.DG.DBTab.MarcaRegRen(db);
			}
			return regren;
		}
		#endregion getters
	}
}
