using System;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// AgenteLocalService.
	/// Servicios para Agentes Locales
	/// Autor: Marcos Báez
	/// </summary>
	public class AgenteLocalService
	{
		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.CAgenteLocal      ag;
		private Berke.DG.DBTab.CAgenteLocal agenteLocal;
		#endregion Atributos

		public AgenteLocalService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public AgenteLocalService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			ag      = new Berke.DG.DBTab.CAgenteLocal(db);
		}
		/// <summary>
		/// Obtiene el Agente Local de acuerdo al nro de matrícula.
		/// </summary>
		/// <param name="nroMatric">Nro. de matrícula del Agente Local</param>
		/// <returns>El agente Local.</returns>

		public Berke.DG.DBTab.CAgenteLocal getAgenteLocal(string nroMatric)
		{
			ag.ClearFilter();		
			
			if (nroMatric.Trim() != "")  
			{
				ag.Dat.nromatricula.Filter = nroMatric.Trim();
			} 
			else 
			{
				ag.Dat.nromatricula.Filter = "0";
			}
			ag.Adapter.ReadAll();
			return ag;
		}
		/// <summary>
		/// Obtiene los Agentes Locales Nuestros, y los conserva internamente
		/// </summary>
		/// <returns>Agentes Locales</returns>
		public Berke.DG.DBTab.CAgenteLocal getAgentesNuestros()
		{
			ag.ClearFilter();
			ag.Dat.Nuestro.Filter = true;
			ag.Adapter.ReadAll();
			return ag;
		}
		/// <summary>
		/// Obtiene la lista de matriculas de los agentes nuestros separados
		/// por comas.
		/// </summary>
		/// <param name="comillas">Caracter a utilizar como comillas</param>
		/// <returns>Lista de matriculas separadas por comas</returns>
		public string getListaMatricAgentesNuestros(string comillas)
		{
			ag = getAgentesNuestros();
			string matric = "";
			for (ag.GoTop(); !ag.EOF; ag.Skip()) 
			{
				if (matric == "") 
				{
					matric = comillas + ag.Dat.nromatricula.AsString + comillas;
				} 
				else 
				{
					matric = matric + "," + comillas + ag.Dat.nromatricula.AsString + comillas;
				}												
			}
			ag.GoTop();
			return matric;
		}
		/// <summary>
		/// Verifica si el agente indicado por el nro de matrícula es nuestro.
		/// Requiere de una llamada previa a getAgentesNuestros().
		/// </summary>
		/// <param name="nromatricula">Nro. de matrícula del agente</param>
		/// <returns>Si el agente es o no nuestro</returns>
		public bool isAgenteNuestro(string nromatricula)
		{
			for (ag.GoTop(); !ag.EOF; ag.Skip()) 
			{
				if (nromatricula == ag.Dat.nromatricula.AsString)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Verifica si el agente es de terceros. Requiere de una
		/// llamada previa a getAgentesNuestros()
		/// </summary>
		/// <param name="nromatricula">Nro. de matrícula del Agente</param>
		/// <returns>True si el nro de matricula del agente es de terceros</returns>
		public bool isAgenteTerceros(string nromatricula)
		{
			return ! this.isAgenteNuestro(nromatricula);
		}

		public Berke.DG.DBTab.CAgenteLocal crearAgenteLocal(string nromatricula)
		{
			ag.NewRow();
			ag.Dat.idagloc.Value = DBNull.Value;
			ag.Dat.identidad.Value = DBNull.Value;
			ag.Dat.idestado.Value = true;
			ag.Dat.nromatricula.Value = nromatricula;
			ag.Dat.obs.Value = DBNull.Value;
			ag.Dat.Nombre.Value = "(Completar - Insertado desde el boletín)";
			ag.Dat.Direccion.Value = DBNull.Value;
			ag.Dat.GrupoID.Value = DBNull.Value;
			ag.Dat.Nuestro.Value = false;
			ag.PostNewRow();
			int agenteLocalID = ag.Adapter.InsertRow();
			ag.Adapter.ReadByID(agenteLocalID);
			return ag;
		}

		
		

	}
}
