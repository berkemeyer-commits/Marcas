using System;
using Berke.Libs.Base;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// ClienteService.cs
	/// Provee servicios varios para el tratamiento de Clientes.
	/// Autor: Marcos Báez
	/// </summary>
	public class ClienteService
	{
		#region Atributos
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected Berke.DG.DBTab.Cliente      cliente;
		protected Berke.DG.DBTab.ClienteXTramite clienteTramite;
		protected Berke.DG.DBTab.ClienteXVia clienteVia;
		protected Berke.DG.DBTab.AtencionXVia atencionVia;
		protected int clienteID;
		#endregion Atributos

		#region Constructores
		public ClienteService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public ClienteService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db = db;
			cliente        = new Berke.DG.DBTab.Cliente(db);
			clienteVia	   = new Berke.DG.DBTab.ClienteXVia(db);
			atencionVia    = new Berke.DG.DBTab.AtencionXVia(db);
			clienteTramite = new Berke.DG.DBTab.ClienteXTramite(db);
		}
		#endregion Constructores

		#region setters
		/// <summary>
		/// Establece el Id del Cliente
		/// </summary>
		/// <param name="clienteID">ID. del Cliente</param>
		public void setClienteID(int clienteID)
		{
			this.clienteID = clienteID;
			cliente.Adapter.ReadByID(clienteID);
		}
		public void setCliente(Berke.DG.DBTab.Cliente cliente)
		{
			this.cliente = cliente;
			this.clienteID = cliente.Dat.ID.AsInt;
		}
		#endregion setters

		#region getters
		/// <summary>
		/// Obtiene el Cliente
		/// </summary>
		/// <returns>Cliente indicado por setClienteID</returns>
		public Berke.DG.DBTab.Cliente getCliente()
		{
			return cliente;
		}

		/// <summary>
		/// Obtener datos de un cliente sea multiple o no
		/// </summary>
		/// <param name="ClienteID"></param>
		/// <returns>Datos del Cliente</returns>
		public Berke.DG.DBTab.Cliente getCliente(int ClienteID)
		{
			
			int clienteIDAsociado = -1;

			setClienteID(ClienteID);
			if ( isClienteMultiple() ) 
			{
				clienteIDAsociado = getClienteTramiteID (ClienteID, (int)GlobalConst.Marca_Tipo_Tramite.OPOSICION);
				setClienteID(clienteIDAsociado);
			}

			return cliente;
		}

		#endregion getters

		public bool isClienteMultiple() 
		{
			return cliente.Dat.Multiple.AsBoolean;
		}

		/// <summary>
		/// Obtiene el ID del cliente que atiende el trámite tramiteID, 
		/// y que esta asociado al clienteMultipleID
		/// </summary>
		/// <param name="tramiteID">ID. del trámite</param>
		/// <param name="clienteMultipleID">ID. del Cliente múltiple</param>
		/// <returns></returns>
		public int getClienteTramiteID (int clienteMultipleID, int tramiteID)
		{
			if ( clienteMultipleID > 0 ) 
			{
				clienteTramite.ClearFilter();
				clienteTramite.Dat.ClienteMultipleID.Filter = clienteMultipleID;
				clienteTramite.Dat.TramiteID.Filter        = tramiteID;
				clienteTramite.Adapter.ReadAll();

				if ( clienteTramite.RowCount > 0 ) 
				{
					return clienteTramite.Dat.ClienteID.AsInt;
				}
			}
			return -1;
		}

		/// <summary>
		/// Obtiene la via de una atención
		/// </summary>
		/// <param name="atencionID">ID. de la atención</param>
		/// <param name="via">ID de la Vía (mail, fax, etc..) </param>
		/// <returns>La descripción de la via</returns>
		public string getAtencionVia(int atencionID, int via)
		{
			if ( atencionID > 0  ) 
			{
				atencionVia.ClearFilter();
				atencionVia.Dat.AtencionID.Filter = atencionID;
				atencionVia.Dat.ViaID.Filter      = via; // MAIL
				atencionVia.Adapter.ReadAll();

				if ( atencionVia.RowCount > 0 ) 
				{
					return atencionVia.Dat.Descrip.AsString;
				}
			}

			return "";

		}

		/// <summary>
		/// Obtiene la via de un cliente
		/// </summary>
		/// <param name="clienteID">ID. del Cliente</param>
		/// <param name="via">ID de la Vía (mail, fax, etc..) </param>
		/// <returns>La descripción de la via</returns>
		public string getClienteVia(int clienteID, int via)
		{
			if ( clienteID  > 0 ) 
			{
				clienteVia.ClearFilter();
				clienteVia.Dat.ClienteID.Filter = clienteID;
				clienteVia.Dat.ViaID.Filter     = via; //FAX;
				clienteVia.Adapter.ReadAll();

				if ( clienteVia.RowCount > 0 ) 
				{
					return clienteVia.Dat.Descrip.AsString;
				}
			}

			return "";

		}

		/// <summary>
		/// Obtiene el código de pais + ciudad del fax del cliente
		/// </summary>
		/// <returns>Código de país + ciudad</returns>
		public string getCliCodPaisCiudad()
		{
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
			pais.ClearFilter();
			pais.Adapter.ReadByID(cliente.Dat.PaisID.AsInt);
			return pais.Dat.paistel.AsString + cliente.Dat.Ddi.AsString;
		}


		/// <summary>
		/// Obtiene la atención de un cliente para un área determinada
		/// </summary>
		/// <param name="clienteID">ID. del Cliente</param>
		/// <param name="areaID">ID. del área</param>
		/// <returns></returns>
		public Berke.DG.DBTab.Atencion getAtencion(int clienteID, int areaID)
		{
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion(db);

			atencion.ClearFilter();
			atencion.Dat.AreaID   .Filter = areaID;
			atencion.Dat.ClienteID.Filter = clienteID;
			atencion.ClearOrder();
			atencion.Dat.ID.Order = 1;
			atencion.Adapter.ReadAll();

			return atencion;
		}
		/// <summary>
		/// Obtiene la atención determinada por la atencionID
		/// </summary>
		/// <param name="atencionID">ID. de la Atención</param>
		/// <returns>La atención</returns>
		public Berke.DG.DBTab.Atencion getAtencion(int atencionID)
		{
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion(db);
			atencion.Adapter.ReadByID(atencionID);
			return atencion;
		}

		/// <summary>
		/// Obtenemos de un clienteID asociada a un tramiteID
		/// </summary>
		/// <param name="clienteID">ID. del Cliente</param>
		/// <param name="tramiteID">ID. del Trámite</param>
		/// <returns>La atención del Cliente</returns>
		public Berke.DG.DBTab.Atencion getAtencionTramite(int clienteID, int tramiteID)
		{
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();;

			int areaID = -1;
			if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO)   
			{
				areaID = (int)GlobalConst.Area.REGISTRO;
			} 
			else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) 
			{
				areaID = (int)GlobalConst.Area.RENOVACION;
			} 
				/* corresponde al servicio de vigilancia */
			else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.OPOSICION  ) 
			{
				/*inicialmente las atenciones estaban asociadas al area de Litigios
				 * y luego se creo el area de vigilancia. A pedido de los usuarios
				 * tomamos ahora el area de Vigilancia.
				 * */
				areaID = (int)GlobalConst.Area.VIGILANCIA;
				//areaID = (int)GlobalConst.Area.LITIGIOS;
			} 
			

			// Implementado solo para Registro/Renovación
			if ( areaID > 0 )
			{
				atencion = this.getAtencion(clienteID, areaID);
			
				// Si no ecuentra datos en el area especificada
				// buscar en el area GENERAL
				if (atencion.RowCount==0)
				{
					atencion = getAtencion(clienteID, (int)GlobalConst.Area.GENERAL );				
				}
			}

			return atencion;

		}


		/// <summary>
		/// Obtenemos de un clienteID asociada a un tramiteID
		/// </summary>
		/// <param name="clienteID">ID. del Cliente</param>
		/// <param name="tramiteID">ID. del Trámite</param>
		/// <param name="marcaID">ID. del Trámite</param>
		/// <returns>La atención del Cliente</returns>
		public Berke.DG.DBTab.Atencion getAtencionTramite(int clienteID, int tramiteID, int marcaID)
		{
			Berke.DG.DBTab.Atencion atencion = new Berke.DG.DBTab.Atencion();
			Berke.DG.DBTab.Marca marca = new Berke.DG.DBTab.Marca(this.db);
			Berke.DG.DBTab.BussinessUnit bussinessUnit = new Berke.DG.DBTab.BussinessUnit();
			Berke.DG.DBTab.AtencionxMarca atencionMarca = new Berke.DG.DBTab.AtencionxMarca();
			
			if (marcaID > 0)
			{
				marca.ClearFilter();
				marca.Adapter.ReadByID(marcaID);
			}

			if (marca.Dat.TipoAtencionxMarca.AsInt > 0)
			{
				#region Atenciones "Por Marca" (variantes)
				switch (marca.Dat.TipoAtencionxMarca.AsInt)
				{
					case (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCA:
						#region Atencion x Marca	
						atencion.InitAdapter(this.db);
						atencion.Adapter.ReadByID(marca.Dat.IDTipoAtencionxMarca.AsInt);
						break;
						#endregion Atencion x Marca
					case (int)GlobalConst.TipoAtencionxMarca.ATENCIONXBUNIT:
						#region Atencion x Bussiness Unit
						bussinessUnit.InitAdapter(this.db);
						bussinessUnit.Adapter.ReadByID(marca.Dat.IDTipoAtencionxMarca.AsInt);
						atencion.InitAdapter(this.db);
						atencion.Adapter.ReadByID(bussinessUnit.Dat.AtencionID.AsInt);
						break;
						#endregion Atencion x Bussiness Unit	
					case (int)GlobalConst.TipoAtencionxMarca.ATENCIONXMARCAXTRAMITE:
						#region Atencion x Marca x Trámite
						atencionMarca.InitAdapter(this.db);
						atencionMarca.ClearFilter();
						atencionMarca.Dat.MarcaID.Filter = marcaID;
						atencionMarca.Adapter.ReadAll();

						atencion.InitAdapter(this.db);

						if (atencionMarca.RowCount > 0)
						{
							for(atencionMarca.GoTop(); !atencionMarca.EOF; atencionMarca.Skip())
							{
								if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO)   
								{
									atencion.ClearFilter();
									atencion.Dat.ID.Filter = atencionMarca.Dat.AtencionID.AsInt;
									atencion.Dat.AreaID.Filter = (int)GlobalConst.Area.REGISTRO;
									atencion.Adapter.ReadAll();
								} 
								else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) 
								{
									atencion.ClearFilter();
									atencion.Dat.ID.Filter = atencionMarca.Dat.AtencionID.AsInt;
									atencion.Dat.AreaID.Filter = (int)GlobalConst.Area.RENOVACION;
									atencion.Adapter.ReadAll();
								} 
									/* corresponde al servicio de vigilancia */
								else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.OPOSICION  ) 
								{
									atencion.ClearFilter();
									atencion.Dat.ID.Filter = atencionMarca.Dat.AtencionID.AsInt;
									atencion.Dat.AreaID.Filter = (int)GlobalConst.Area.VIGILANCIA;
									atencion.Adapter.ReadAll();
								} 

								if (atencion.RowCount > 0)
								{
									break;
								}
							}
						}

						if ( atencion.RowCount == 0 )
						{
							atencion = getAtencion(clienteID, (int)GlobalConst.Area.GENERAL );
						}
						break;
						#endregion Atencion x Marca x Trámite
				}
				#endregion Atenciones "Por Marca" (variantes)
			}
			else
			{
				#region Atenciones "Normales"
				int areaID = -1;
				if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO)   
				{
					areaID = (int)GlobalConst.Area.REGISTRO;
				} 
				else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION ) 
				{
					areaID = (int)GlobalConst.Area.RENOVACION;
				} 
					/* corresponde al servicio de vigilancia */
				else if ( tramiteID == (int)GlobalConst.Marca_Tipo_Tramite.OPOSICION  ) 
				{
					/*inicialmente las atenciones estaban asociadas al area de Litigios
					* y luego se creo el area de vigilancia. A pedido de los usuarios
					* tomamos ahora el area de Vigilancia.
					* */
					areaID = (int)GlobalConst.Area.VIGILANCIA;
					//areaID = (int)GlobalConst.Area.LITIGIOS;
				} 
				


				// Implementado solo para Registro/Renovación
				if ( areaID > 0 )
				{
					atencion = this.getAtencion(clienteID, areaID);
				
					// Si no ecuentra datos en el area especificada
					// buscar en el area GENERAL
					if (atencion.RowCount==0)
					{
						atencion = getAtencion(clienteID, (int)GlobalConst.Area.GENERAL );				
					}
				}
				#endregion Atenciones "Normales"
			}

			return atencion;

		}

	}

}
