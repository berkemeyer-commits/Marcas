using System;

namespace Berke.Marcas.BizDocuments.Helpers {
	/// <summary>
	/// Catalog: esta clase deberia hacerse con codemaker, mirando el fwk.config
	/// </summary>

	public enum SimpleEntries {  }
	
	public enum Actions
	{
		Clase_ReadByPattern,
		Marca_ReadByID,
		Marca_ReadList,
		MarcaRegRen_ReadByID,
		MarcaRegRen_ReadLByID,
		MarcaRegRen_Upsert,
		MarcaVarios_ReadByID,
		Expediente_ReadList,
		Marca_Upsert,
		MarcaTramite_ReadByID,
		OrdenTrabajo_ReadByID,
		OrdenTrabajo_ReadList,
		OrdenTrabajoTV_ReadByID,
		OrdenTrabajo_Upsert,
		OrdenTrabajo_Delete,
		Pertenencia_ReadByPattern,
		Poder_ReadByPattern,
		Poder_ReadByID,
		MarcaTipo_ReadByPattern,
		Diario_ReadByPattern,
		TramiteSit_ReadByPattern,
		Expediente_ClienteUpdate,
		GAD_ReadList
	}
	public enum Connections { Berke, BerkeEntidades }
}
