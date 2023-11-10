using System;
using Berke.Libs.Base;
using System.Collections;
using Berke.Libs.Base.DSHelpers;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// MarcaService.cs
	/// Provee servicios varios correspondientes a las Marcas.
	/// Autor: Marcos Báez
	/// </summary>
	public class MarcaService
	{
        //public static int EDICION_VIGENTE = (int) Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_11MA;
        public static int EDICION_VIGENTE = (int)Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;

        #region Atributos
        /// <summary> Objeto que maneja la conexión a la base </summary>
        protected Berke.Libs.Base.Helpers.AccesoDB db;
		/// <summary> Maneja las operaciones básicas sobre la tabla Marca </summary>
		protected Berke.DG.DBTab.Marca        mar;
		private int marcaID;
		#endregion Atributos

		#region Constructores
		public MarcaService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary> Constructor
		/// </summary>
		/// <param name="db">Conexión a la base</param>
		public MarcaService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			mar = new Berke.DG.DBTab.Marca(db);
		}
		#endregion Constructores

		#region setters
		public void setMarcaID(int marcaID)
		{
			this.marcaID = marcaID;
			mar = new Berke.DG.DBTab.Marca(db);
			mar.ClearFilter();
			mar.Adapter.ReadByID(marcaID);
		}
		public void setMarca(Berke.DG.DBTab.Marca marca)
		{
			this.marcaID = marca.Dat.ID.AsInt;
			this.mar = marca;
		}
		#endregion setters

		#region Obtener Clase vigente
		/// <summary>
		/// Obtiene la Clase vigente
		/// </summary>
		/// <param name="nro"></param>
		/// <returns> La clase Vigente, de acuerdo a la edición Niza.</returns>
		public static Berke.DG.DBTab.Clase getClaseVigente(string nro, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			clase.Dat.NizaEdicionID.Filter = (int) Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
			clase.Dat.Nro.Filter = nro;
			clase.Adapter.ReadAll();
			return clase;
		}
		#endregion Obtener Clase vigente

		#region Obtener id de Tipo de Marca
		/// <summary>
		/// Obtiene el ID del tipo de Marca, a partir de la Descripción
		/// </summary>
		/// <param name="strTipo">La descripción del tipo de la marca: "D", "F", "M"</param>
		/// <param name="db">Conexión a la Base</param>
		/// <returns>ID. del tipo de Marca</returns>
		public static int getMarcaTipo(string strTipo, Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.MarcaTipo mt = new Berke.DG.DBTab.MarcaTipo(db);
			strTipo = strTipo.Trim();
			mt.Dat.Abrev.Filter = strTipo;
			mt.Adapter.ReadAll();
			if (mt.RowCount>0)
			{
				return mt.Dat.ID.AsInt;
			}
			else
			{
				throw new Exceptions.BolImportException("Tipo de marca no registrada en el sistema:"+ strTipo);
			}

		}
		/// <summary>
		/// Provee la abreviatura del Tipo de la Marca
		/// </summary>
		/// <param name="tipo"></param>
		/// <returns></returns>
		public string getMarcaTipo(int tipo )
		{
			Berke.DG.DBTab.MarcaTipo mt = new Berke.DG.DBTab.MarcaTipo(db);
			mt.Adapter.ReadByID(tipo);
			if (mt.RowCount>0)
			{
				return mt.Dat.Abrev.AsString;
			}
			else
			{
				throw new Exception("Tipo de marca no registrada en el sistema:"+ tipo);
			}

		}

		public string getMarcaTipoNoBreak(int tipo )
		{
			Berke.DG.DBTab.MarcaTipo mt = new Berke.DG.DBTab.MarcaTipo(db);
			mt.Adapter.ReadByID(tipo);
			if (mt.RowCount>0)
			{
				return mt.Dat.Abrev.AsString;
			}
			else
			{
				return "";
			}

		}

		#endregion Obtener id de Tipo de Marca

		#region Obtener PropietariosxMarca
		/// <summary>
		/// Obtiene los propietarios pertenecientes a la marca
		/// de la tabla propietarioxmarca.
		/// </summary>
		/// <returns>Retorna ID de propietarios</returns>
		public Berke.DG.DBTab.PropietarioXMarca getPropietariosID()
		{
			Berke.DG.DBTab.PropietarioXMarca  propXmarca = new Berke.DG.DBTab.PropietarioXMarca(db);
			propXmarca.ClearFilter();
			propXmarca.Dat.MarcaID.Filter = marcaID;
			propXmarca.Adapter.ReadAll();
			return propXmarca;
		}
		/// <summary>
		/// Retorna una lista de ID de propietarios.
		/// </summary>
		/// <returns>Lista de propietarios, separados por coma.</returns>
		public string getListaPropietariosID(){

			Berke.DG.DBTab.PropietarioXMarca  propXmarca = this.getPropietariosID();
			string propID = "";
			for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
			{
				if (propID == "") 
				{
					propID = propXmarca.Dat.PropietarioID.AsString;
				} 
				else 
				{
					propID = propID + "," + propXmarca.Dat.PropietarioID.AsString;
				}												
			}
			return propID;
		}
		#endregion Obtener PropietariosxMarca

		#region Obtener marca
		public Berke.DG.DBTab.Marca getMarca()
		{
			return mar;
		}


		public Berke.DG.DBTab.Marca getMarca( int marcaID )
		{
			if (  marcaID != 0 )
			{
				try
				{
					mar.Adapter.ReadByID(marcaID);
				}
				catch(Exception ex)
				{
					//throw new Exceptions.BolImportException("Error al leer ActaNro: "+actanro+ " ActaAño: " + actaanio + ". Detalle:"+ex.Message);							
					throw new Exception("Error al leer MarcaID: "+marcaID + ". Detalle:"+ex.Message);							
				}
				return mar;
			}
			else 
			{
				mar = new Berke.DG.DBTab.Marca(db);
				return mar;
			}
		}
		#endregion Obtener marca

		#region Agente Local
		public string getAgenteLocal( int agLocalID )
		{
			Berke.DG.DBTab.CAgenteLocal agenteLocal = new Berke.DG.DBTab.CAgenteLocal(this.db);
			if (  agLocalID != 0 )
			{
				try
				{
					agenteLocal.Adapter.ReadByID(agLocalID);
				}
				catch(Exception ex)
				{
					throw new Exception("Error al leer MarcaID: "+marcaID + ". Detalle:"+ex.Message);							
				}
				return agenteLocal.Dat.Nombre.AsString;
			}
			else 
			{
				return "";
			}
		}
		#endregion


		#region Add propietario
		/// <summary>
		/// Agrega un propietario a la marca. La asociación se crea
		/// en la tabla propietarioxmarca.
		/// </summary>
		/// <param name="propID">ID. del Propietario</param>
		public void addPropietario(int propID)
		{
			Berke.DG.DBTab.PropietarioXMarca  propXmarca = new Berke.DG.DBTab.PropietarioXMarca(db);
			propXmarca.NewRow();
			propXmarca.Dat.MarcaID.Value		= this.marcaID;
			propXmarca.Dat.PropietarioID.Value	= propID;
			propXmarca.PostNewRow();
			propXmarca.Adapter.InsertRow();
		}
		#endregion Add propietario

		#region Actualizar Datos de Propietario
		/// <summary>
		/// Actualizar los datos de Propietario en la Marca
		/// </summary>
		/// <param name="prop">Propietario</param>
		public void updateDatosPropietario(Berke.DG.DBTab.Propietario prop)
		{
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
			pais.Adapter.ReadByID(prop.Dat.PaisID.AsInt);
			mar.Edit();
			mar.Dat.Propietario.Value = prop.Dat.Nombre.AsString;
			mar.Dat.ProPais.Value     = pais.Dat.paisalfa.AsString;
			mar.Dat.ProDir.Value      = prop.Dat.Direccion.AsString;
			mar.PostEdit();
			mar.Adapter.UpdateRow();
		}
		#endregion 		

		#region delete Propietarios By Marca
		/// <summary>
		/// Elimina los propietarios de una marca (asociación Propietarioxmarca)
		/// </summary>
		public void deletePropietarios()
		{
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca(db);
			propXmarca.ClearFilter();
			propXmarca.Dat.MarcaID.Filter = this.marcaID;
			propXmarca.Adapter.ReadAll();
			for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
			{
				propXmarca.Delete();
				propXmarca.Adapter.DeleteRow();
			}

		}
		#endregion delete Propietarios By Marca

		#region Pasar a Inactiva/Activa
		public void pasarInactiva()
		{
			mar.Edit();
			mar.Dat.Vigente.Value = false;
			mar.PostEdit();
			mar.Adapter.UpdateRow();
		}
		public void pasarActiva(int expedienteVigenteID)
		{
			mar.Edit();
			mar.Dat.Vigente.Value = true;
			mar.Dat.ExpedienteVigenteID.Value = expedienteVigenteID;
			mar.PostEdit();
			mar.Adapter.UpdateRow();
		}
		#endregion Pasar a Inactiva/Activa

		#region Actualizar desde campos
		/// <summary>
		/// Actualiza los datos de Propietario de la Marca a partir de los datos de 
		/// ExpedienteCampo
		/// </summary>
		/// <param name="campos">Entradas de ExpedienteCampo</param>
		/// <param name="actual">Actualizar a partir de los datos de Propietario Actual (true) o 
		/// Propietario Anterior (false)</param>
		/// <param name="udpProp">Actualizar datos de PropietarioXMarca</param>
		/// <param name="udpCli">Actualizar datos de Cliente de la Marca</param>
		public void updateFromCampos(Berke.DG.DBTab.ExpedienteCampo campos, bool actual, bool udpProp, bool udpCli)
		{
			mar.Edit();
			string propID    = "";
			string clienteID = "";
			for (campos.GoTop(); !campos.EOF; campos.Skip())
			{
				string campo = campos.Dat.Campo.AsString;
				string valor = campos.Dat.Valor.AsString;

				if (!actual && (campo == GlobalConst.PROP_ANTERIOR_DIR))
				{
					mar.Dat.ProDir.Value = valor;
				}
				else if (!actual && (campo == GlobalConst.PROP_ANTERIOR_ID))
				{
					propID = valor;
				}
				else if (!actual && (campo == GlobalConst.PROP_ANTERIOR_NOMBRE))
				{
					mar.Dat.Propietario.Value = valor;
				}
				else if (!actual && (campo == GlobalConst.PROP_ANTERIOR_PAIS))
				{
					mar.Dat.ProPais.Value = valor;
				}
				else if (actual && (campo == GlobalConst.PROP_ACTUAL_DIR))
				{
					mar.Dat.ProDir.Value = valor;
				}
				else if (actual && (campo == GlobalConst.PROP_ACTUAL_NOMBRE))
				{
					mar.Dat.Propietario.Value = valor;
				}
				else if (actual && (campo == GlobalConst.PROP_ACTUAL_PAIS))
				{
					mar.Dat.ProPais.Value = valor;
				}
				else if (campo == GlobalConst.VIGILADA_ANTERIOR)
				{
					bool vigilada = (valor=="No")?false:true;
					mar.Dat.Vigilada.Value = vigilada;

					#region Determinar si es Nuestra o de terceros
					if (!vigilada)
					{
						mar.Dat.Nuestra.Value = false;
					}
					else 
					{
						// Es vigilada, pero tenemos que determinar 
						// si era nuestra.
						int expeID = campos.Dat.ExpedienteID.AsInt;
						ExpedienteService expeServ = new ExpedienteService(db);
						expeServ.setExpeID(expeID);
						Berke.DG.DBTab.Expediente expe = expeServ.getExpedientePadre();
						if (expe!= null)
						{
							mar.Dat.Nuestra.Value = expe.Dat.Nuestra.AsBoolean;
						}
						

					}
					#endregion 
				}
				else if (campo == GlobalConst.CLI_ANTERIOR_ID)
				{
					clienteID = valor;
					if (udpCli && clienteID.Trim().Length>0)
					{
						mar.Dat.ClienteID.Value = clienteID;
					}
				}
			}			
		
			mar.PostEdit();
			mar.Adapter.UpdateRow();
			// Debo Actualizar el propietario?
			if (udpProp && propID.Trim().Length>0)
			{
				this.deletePropietarios();
				
				string [] apropID = propID.Split(",".ToCharArray());
				for (int i=0; i<apropID.Length; i++)
				{
					if ( apropID[i] != ""  )
					{
						this.addPropietario(Convert.ToInt32(apropID[i]));
						
					}
				}


			}

			

		}
		#endregion Actualizar desde campos

		#region Actualizar Datos de Propietarios desde Expediente
		public void updatePropFromExpe(Berke.DG.DBTab.Expediente expe)
		{
			Berke.DG.DBTab.PropietarioXMarca  propXmarca = new Berke.DG.DBTab.PropietarioXMarca(db);
			Berke.DG.DBTab.ExpedienteXPropietario expXProp = new Berke.DG.DBTab.ExpedienteXPropietario(db);
			
			/*Obtener Propietario del Exp de TV */
			expXProp.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
			expXProp.Adapter.ReadAll();

			if ( expXProp.RowCount == 0 ) 
			{
				throw new Exception("El expediente no tiene asociado los propietarios [ExpedienteXPropietario] ");
			}

			/* Borrar propietarios Por Marca */
			this.deletePropietarios();

			for (expXProp.GoTop(); !expXProp.EOF; expXProp.Skip())
			{
				this.addPropietario(expXProp.Dat.PropietarioID.AsInt);
			}
			
		}
		#endregion

		#region Obtener clases Relacionadas
		/// <summary>
		/// Obtiene las clases relacionadas de acuerdo al criterio de Marcas
		/// </summary>
		/// <param name="claseNro">Nro. de clase</param>
		/// <returns>ArrayList de clases relacionadas a claseNro.</returns>
		public ArrayList getClasesRelMarcas(int claseID)
		{
			return this.getClasesRel(claseID, false);
			
		}
		/// <summary>
		/// Obtiene las clases relacionadas de acuerdo al criterio de Vigilancia
		/// </summary>
		/// <param name="claseID">ID. de clase</param>
		/// <returns>ArrayList de clases relacionadas a claseID.</returns>
		public ArrayList getClasesRelVigilancia(int claseID)
		{
			return this.getClasesRel(claseID, true);
		}


		/// <summary>
		/// Obtiene las clases relacionadas de acuerdo al criterio de Vigilancia
		/// </summary>
		/// <param name="claseID">ID. de clase</param>
		/// <returns>ArrayList de clases relacionadas a claseID.</returns>
		private ArrayList getClasesRel(int claseID, bool vigilancia)
		{
			ArrayList lst = new ArrayList();
			Berke.DG.DBTab.Clase_Clase clsRel = new Berke.DG.DBTab.Clase_Clase(db);

			clsRel.ClearFilter();
			clsRel.Dat.ClaseID.Filter    = claseID;
			clsRel.Dat.Ancestro.Filter   = false;
			clsRel.Dat.Vigilancia.Filter = vigilancia;
					
			clsRel.Adapter.ReadAll();
			for( clsRel.GoTop() ; !clsRel.EOF; clsRel.Skip() )
			{
				lst.Add( clsRel.Dat.ClaseRelacID.AsInt );
				//if( clsRel.Dat.Ancestro.AsBoolean )
				//{
					#region Enlistar las relacionadas con los ancestros
					lst.AddRange(this.getClasesAncestras(clsRel.Dat.ClaseRelacID.AsInt));	
					#endregion Enlistar las relacionadas con los ancestros
				//}
			}
							

			return lst;
		}

		public ArrayList getClasesAncestras(int claseID)
		{
			Berke.DG.DBTab.Clase_Clase clsRel = new Berke.DG.DBTab.Clase_Clase(db);

			clsRel.ClearFilter();
			clsRel.Dat.ClaseID.Filter  = claseID;
			clsRel.Dat.Ancestro.Filter = true;
			clsRel.Adapter.ReadAll();
			ArrayList lst = new ArrayList();
			for( clsRel.GoTop() ; !clsRel.EOF; clsRel.Skip() )
			{
				lst.Add( clsRel.Dat.ClaseRelacID.AsInt );
			}
			return lst;
		}

		#endregion Obtener clases Relacionadas

		#region Lista de clases
		/// <summary>
		/// Convierte una lista de Números de clases separados por coma, en una lista de 
		/// ClaseIDs.
		/// </summary>
		/// <param name="claseNroList">Lista de ClaseNros separados por coma</param>
		/// <returns>Lista de ClaseID </returns>
		public ArrayList claseNroToID(string claseNroList)
		{ 
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			ArrayList lst = new ArrayList();
			string [] lista = claseNroList.Trim().Split(",".ToCharArray());
			if (claseNroList.Trim().Length == 0)
			{
				return new ArrayList();
			}

			for (int i=0; i< lista.Length; i++)
			{
				lst.Add(lista[i]);
			}			
			clase.ClearFilter();
			clase.Dat.Nro.Filter = new DSFilter(lst);
			clase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
			clase.Adapter.ReadAll();
			if (clase.RowCount == 0) 
			{
				return null;
			}

			lst = new ArrayList();
			for (clase.GoTop(); clase.EOF; clase.Skip())
			{
				lst.Add(clase.Dat.ID.AsInt);
			}
			return lst;

		}

		/// <summary>
		/// Convierte una lista de ID de clases separados por coma, en una lista de 
		/// ClaseNros
		/// </summary>
		/// <param name="claseNroList">Lista de ClaseNros separados por coma</param>
		/// <returns>Lista de ClaseNro</returns>
		public ArrayList claseIDToNro(string claseIDList)
		{ 
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			ArrayList lst = new ArrayList();
			string [] lista = claseIDList.Trim().Split(",".ToCharArray());
			if (claseIDList.Trim().Length == 0)
			{
				return new ArrayList();
			}

			for (int i=0; i< lista.Length; i++)
			{
				lst.Add(lista[i]);
			}			
			clase.ClearFilter();
			clase.Dat.ID.Filter = new DSFilter(lst);
			clase.Dat.NizaEdicionID.Filter = Berke.Libs.Base.GlobalConst.EdicionesNiza.EDICION_VIGENTE;
			clase.Adapter.ReadAll();
			if (clase.RowCount == 0) 
			{
				return null;
			}

			lst = new ArrayList();
			for (clase.GoTop(); !clase.EOF; clase.Skip())
			{
				lst.Add(clase.Dat.Nro.AsInt);
			}
			return lst;

		}

		#endregion Lista de clases

		#region Clase Nro
		/// <summary>
		/// Retorna el numero de clase a partir del ClaseID
		/// 
		/// </summary>
		/// <param name="claseID">Identificador de la clase</param>
		/// <returns>Numero de clase de la marca</returns>
		public int claseNro(int claseID)
		{ 
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase(db);
			clase.Adapter.ReadByID(claseID);
			return clase.Dat.Nro.AsInt;
		}
		#endregion Clase Nro

		#region Actualizar la marca con el ID marca anterior para casos de renovado por otro
		public void updateClienteIDRXO(int MarcaID, int ClienteID)
		{
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca(db);
			marca.ClearFilter();
			marca.Adapter.ReadByID(MarcaID);
			marca.Edit();
			marca.Dat.ClienteID.Value = ClienteID;
			marca.PostEdit();
			marca.Adapter.UpdateRow();
		}
		#endregion Actualizar la marca con el ID marca anterior para casos de renovado por otro

	}
}
