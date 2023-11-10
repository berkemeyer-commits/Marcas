using System;
using Berke.Libs.Base;
namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// PropietarioSearch.cs
	/// Provee Servicios para manipular propietarios.
	/// Autor: Marcos Báez
	/// </summary>
	public class PropietarioSearch
	{
		protected Berke.DG.DBTab.Propietario propAnt;
		protected Berke.DG.DBTab.Propietario propSearch;
		protected Berke.Libs.Base.Helpers.AccesoDB db;

		#region Constructores
		public PropietarioSearch(Berke.Libs.Base.Helpers.AccesoDB db, int propID)
		{
			this.db       = db;
			propAnt  = new Berke.DG.DBTab.Propietario(db);
			propAnt.Adapter.ReadByID(propID);

			propSearch    = new Berke.DG.DBTab.Propietario(db);
		}

		public PropietarioSearch(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db       = db;
			propSearch    = new Berke.DG.DBTab.Propietario(db);
		}
		#endregion Constructores

		#region Métodos
		public void setPropietarioAnterior(int propID)
		{
			propAnt  = new Berke.DG.DBTab.Propietario(db);
			propAnt.Adapter.ReadByID(propID);
		}
		public Berke.DG.DBTab.Propietario searchCambioNombre(string name)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = ObjConvert.GetSqlPattern(name);
			propSearch.Dat.Direccion.Filter = propAnt.Dat.Direccion.AsString;
			propSearch.Adapter.ReadAll();
			return propSearch;
		}
		public Berke.DG.DBTab.Propietario searchCambioDomicilio(string address)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = propAnt.Dat.Nombre.AsString;
			//propSearch.Dat.Direccion.Filter = ObjConvert.GetSqlPattern(address);
			propSearch.Adapter.ReadAll();
			return propSearch;
		}

		public Berke.DG.DBTab.Propietario searchFusion(string name)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = ObjConvert.GetSqlPattern(name);
			//propSearch.Dat.Direccion.Filter = propAnt.Dat.Direccion.AsString;
			propSearch.Adapter.ReadAll();
			return propSearch;
		}
		public Berke.DG.DBTab.Propietario searchTransferencia(string name)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = ObjConvert.GetSqlPattern(name);
			//propSearch.Dat.Direccion.Filter = propAnt.Dat.Direccion.AsString;
			propSearch.Adapter.ReadAll();

			return propSearch;
		}
		public Berke.DG.DBTab.Propietario searchRenovacion(string name)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = ObjConvert.GetSqlPattern(name);
			//propSearch.Dat.Direccion.Filter = propAnt.Dat.Direccion.AsString;
			propSearch.Adapter.ReadAll();

			return propSearch;
		}
		#endregion Métodos
		public Berke.DG.DBTab.Propietario search(string propietario, string direccion)
		{
			propSearch.ClearFilter();
			propSearch.Dat.Nombre.Filter    = ObjConvert.GetSqlPattern(propietario);
			propSearch.Dat.Direccion.Filter = ObjConvert.GetSqlPattern(direccion);
			propSearch.Adapter.ReadAll(5000);

			return propSearch;
		}
		public Berke.DG.DBTab.Propietario search(string valor, int tipotr)
		{			
			switch(tipotr)
			{
				case (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_DOMICILIO:
					return this.searchCambioDomicilio(valor);
					
				case (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.CAMBIO_NOMBRE:
					return this.searchCambioNombre(valor);
				
				case (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.FUSION:
					return this.searchFusion(valor);
					
				case (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.TRANSFERENCIA:
					return this.searchTransferencia(valor);	
	
				case (int)Berke.Libs.Base.GlobalConst.Marca_Tipo_Tramite.RENOVACION:
					return this.searchRenovacion(valor);				
				default:break;
			}
			throw new Exceptions.BolImportException("Tipo de TV no válido.");
		
		}

		#region searchByMarca
		public int searchByMarca(int marcaID, string valor, int tipotr)
		{
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca(db);
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
			propXmarca.ClearFilter();
			propXmarca.Dat.MarcaID.Filter = marcaID;
			propXmarca.Adapter.ReadAll();
			int propID = -1;
			for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
			{
				this.setPropietarioAnterior(propXmarca.Dat.PropietarioID.AsInt);
				prop = this.search(valor, tipotr);									
				if ((prop.RowCount>0)&&(prop.Dat.ID.AsInt>0))
				{
					return prop.Dat.ID.AsInt;
				}
			}
			
			return propID;
		}


		#endregion searchByMarca	

		#region Obtener Propietario
		/// <summary>
		/// Obtiene el propietario a partir del ID
		/// </summary>
		/// <param name="propID">ID. Propietario</param>
		/// <returns>Propietario correspondiente al ID</returns>
		public Berke.DG.DBTab.Propietario getPropietario(int propID)
		{
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
			prop.Adapter.ReadByID(propID);
			return prop;
		}
		#endregion Obtener propietario
	
		public void copy(Berke.DG.DBTab.Propietario src, Berke.DG.DBTab.Propietario dst)
		{
			dst.Dat.ID.Value		= src.Dat.ID.AsInt;
			dst.Dat.Nombre.Value	= src.Dat.Nombre.AsString;
			dst.Dat.Direccion.Value = src.Dat.Direccion.AsString;
			dst.Dat.PaisID.Value    = src.Dat.PaisID.AsInt;
		}

	}
}
