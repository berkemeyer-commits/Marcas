using System;
using db;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Berke.Libs.Base;
using Berke.Libs.Boletin.Services;
using Berke.Libs.Boletin.Libs;

namespace Berke.Libs.Boletin.Services
{
	/// <summary>
	/// BoletinService.cs
	/// Provee servicios varios para el proceso del Boletín.
	/// Autor: Marcos Báez
	/// </summary>
	public class BoletinService
	{
		#region Atributos globales
		private static string ENTER = @"
";
		public static string ARCH_BOL_A     = @"\BOL_A.dbf";
		public static string ARCH_BOL_B1    = @"\BOL_B1.dbf";
		public static string ARCH_BOL_B1A   = @"\BOL_B1A.dbf";
		public static int    MAX_ROWS       = 200000;
		public static int    BOL_FUNCID     = 6;
		public static string NEWPROP_NOMBRE = "PropNuevoNombre";
		public static string NEWPROP_DIR    = "PropNuevoDireccion";
		public static string NEWPROP_PAIS   = "PropNuevoPais";

		public static string OLDPROP_NOMBRE = "PropAntNombre";
		public static string OLDPROP_DIR    = "PropAntDireccion";
		public static string OLDPROP_PAIS   = "PropAntPais";
		public static string DEF_NEWPROP_OBS    = "Propietario generado por la aplicación de Boletín";
		//public static int    DEF_NEWPROP_IDIOMA = 2;
		public static string PROP_ID		= "PropID";		
		public static string PLANTILLA_KEY  = "bolReport";

		#endregion Atributos globales

		#region Atributos
		protected Berke.DG.DBTab.BoletinDet bol;
		protected Berke.DG.DBTab.Boletin boletin;
		protected Berke.Libs.Base.Helpers.AccesoDB db;
		protected xdbf mydb;
		protected System.Windows.Forms.ProgressBar pBar;
		protected ExpedienteService  expeServ;
		protected AgenteLocalService agServ;
		protected MarcaRegRenService regrenServ;
		protected RegistroService    regServ;
		protected RenovacionService  renServ;
		protected MarcaService       marServ;
		protected TVService			 tvServ;
		protected PropietarioSearch  propServ;
		protected string obs;
		protected BoletinStats stats;
		protected string activePrinter;
		#endregion Atributos
		
		#region Constructores
		public BoletinService(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			this.db		= db;
			bol			= new Berke.DG.DBTab.BoletinDet(db);
			boletin     = new Berke.DG.DBTab.Boletin(db);
			expeServ	= new ExpedienteService(db);
			agServ		= new AgenteLocalService(db);
			regrenServ	= new MarcaRegRenService(db);
			regServ     = new RegistroService(db);
			renServ		= new RenovacionService(db);
			marServ		= new MarcaService(db);
			tvServ      = new TVService(db);
			propServ	= new PropietarioSearch(db);
			stats       = new BoletinStats();
			activePrinter = "";
		}
		#endregion Constructores

		#region setters
		public void setProgressBar(System.Windows.Forms.ProgressBar pBar)
		{
			this.pBar = pBar;
		}
		public void setBoletin(Berke.DG.DBTab.BoletinDet bol)
		{
			this.bol = bol;
		}
		#endregion setters

		#region getters
		public string getObs()
		{
			return this.obs;
		}
		/// <summary>
		/// Obtiene estadísticas del último proceso 
		/// realizado.
		/// </summary>
		/// <returns>Estadísticas</returns>
		public BoletinStats getStats()
		{
			return this.stats;
		}
		/// <summary>
		/// Obtiene el BoletinDet
		/// </summary>
		/// <returns>BoletinDet</returns>
		public Berke.DG.DBTab.BoletinDet getBoletin()
		{
			return this.bol;
		}
		#endregion getters

		#region Leer desde DBF
		public DataTable leerDesdeDbf(string dir, bool excluirActasDuplicadas = false)
		{
            string ActasLeidas = "", ActasFaltantes = "";
            int UltimoLeido = 0, ValorEsperado = 0;
            int ActasFalCant = 0;
            bool bandera = false;
            string archBOLA, archBOLB1, archBOLB1A = "";

            bol.Table.Clear();
            mydb = new xdbf();


            archBOLA = dir + ARCH_BOL_A;
            archBOLB1 = dir + ARCH_BOL_B1;
            archBOLB1A = dir + ARCH_BOL_B1A;

            FileInfo fiBOLA = new FileInfo(archBOLA);
            FileInfo fiBOLB1 = new FileInfo(archBOLB1);
            FileInfo fiBOLB1A = new FileInfo(archBOLB1A);

            #region Procesar Archivo BOL_A.dbf
            if (Utils.existeArchivo(fiBOLA))
            {
                mydb.Use(archBOLA);
                mydb.Go(0);
                if (mydb.Opened)
                {
                    //MessageBox.Show("ABIERTO. Numero de registros: "+ mydb.RecCount.ToString());

                    #region Recorrer Registros
                    while (!mydb.Eof)
                    {
                        bandera = true;
                        #region Asignar valores a BoletinDet
                        bol.NewRow();
                        bol.Dat.ID.Value = DBNull.Value;   //int PK  Oblig.
                        bol.Dat.BoletinID.Value = DBNull.Value;   //int


                        int año, mes, dia;
                        string AnioMesDia = mydb.Get("FEC_SOLI");
                        año = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(0, 4));
                        mes = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(4, 2));
                        dia = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(6, 2));
                        System.DateTime fecha = new DateTime(año, mes, dia);
                        bol.Dat.SolicitudFecha.Value = fecha;   //smalldatetime Oblig.
                        bol.Dat.ExpNro.Value = expeServ.ObtenerActaNro(mydb.Get("NUM_ACTA"));   //int Oblig.
                        bol.Dat.ExpAnio.Value = expeServ.ObtenerActaAnio(mydb.Get("NUM_ACTA"));   //int Oblig.
                        bol.Dat.Clase.Value = mydb.Get("NUM_CLASE").Trim();   //nvarchar
                        bol.Dat.MarcaTipo.Value = mydb.Get("S").Trim();   //nvarchar
                        bol.Dat.Tramite.Value = mydb.Get("TIP").Trim();   //nvarchar Oblig.
                        bol.Dat.Denominacion.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("NOM_DENOMI").Trim());   //nvarchar
                        //bol.Dat.BoletinID.Value = getBoletinID(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                        try
                        {
                            bol.Dat.Propietario.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("NOM_TITULA").Trim());   //nvarchar
                        }
                        catch
                        {
                            bol.Dat.Propietario.Value = "a";
                        }
                        bol.Dat.Pais.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("CO").Trim());   //nvarchar
                        try
                        {
                            bol.Dat.AgenteLocal.Value = mydb.Get("NUM_AGENTE").Trim();   //nvarchar
                        }
                        catch
                        {
                            bol.Dat.AgenteLocal.Value = 0;
                        }

                        bol.Dat.RefNro.Value = DBNull.Value;   //int
                        bol.Dat.RefAnio.Value = DBNull.Value;   //int
                        try
                        {
                            bol.Dat.RefRegNro.Value = mydb.Get("REG_VIEJO").Trim();   //int
                        }
                        catch
                        {
                            bol.Dat.RefRegNro.Value = 0;
                        }
                        bol.Dat.RefRegAnio.Value = DBNull.Value;   //int
                        bol.Dat.Obs.Value = DBNull.Value;   //nvarchar
                        bol.PostNewRow();
                        #endregion Asignar valores a BoletinDet


                        mydb.Skip(1);
                    }
                    #endregion Recorrer Registros

                    mydb.Close();
                }
                else
                {
                    MessageBox.Show(" NO ABIERTO");
                }
            }
            #endregion Procesar Archivo BOL_A.dbf

            #region Procesar Archivo BOL_B1.dbf
            if (Utils.existeArchivo(fiBOLB1))
            {
                mydb.Use(archBOLB1);
                mydb.Go(0);
                if (mydb.Opened)
                {
                    #region Recorrer Registros
                    while (!mydb.Eof)
                    {
                        bandera = true;
                        #region Asignar valores a BoletinDet
                        bol.NewRow();
                        bol.Dat.ID.Value = DBNull.Value;   //int PK  Oblig.
                        bol.Dat.BoletinID.Value = DBNull.Value;   //int


                        int año, mes, dia;
                        string AnioMesDia = mydb.Get("FEC_DOCU");
                        año = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(0, 4));
                        mes = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(4, 2));
                        dia = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(6, 2));
                        System.DateTime fecha = new DateTime(año, mes, dia);
                        bol.Dat.SolicitudFecha.Value = fecha;   //smalldatetime Oblig.
                        bol.Dat.ExpNro.Value = expeServ.ObtenerActaNro(mydb.Get("NUM_ACTA_D"));   //int Oblig.
                        bol.Dat.ExpAnio.Value = expeServ.ObtenerActaAnio(mydb.Get("NUM_ACTA_D"));   //int Oblig.
                        //bol.Dat.Clase		.Value = mydb.Get("CLASE");   //nvarchar
                        bol.Dat.MarcaTipo.Value = DBNull.Value;   //nvarchar
                        bol.Dat.Tramite.Value = mydb.Get("TIP_DOCU").Trim();   //nvarchar Oblig.
                        //bol.Dat.Denominacion.Value = mydb.Get("NOM_DENOMI");   //nvarchar
                        bol.Dat.Propietario.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("NOM_SOLICI").Trim());   //nvarchar
                        bol.Dat.Pais.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("COD_PAIS").Trim());   //nvarchar
                        //bol.Dat.BoletinID.Value = getBoletinID(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                        try
                        {
                            bol.Dat.AgenteLocal.Value = mydb.Get("NUM_AGENTE").Trim();   //nvarchar
                        }
                        catch
                        {
                            bol.Dat.AgenteLocal.Value = 0;
                        }

                        string expe = mydb.Get("NUM_ACTA").Trim();
                        if (expe != "0")
                        {
                            bol.Dat.RefNro.Value = expeServ.ObtenerActaNro(expe);
                            bol.Dat.RefAnio.Value = expeServ.ObtenerActaAnio(expe);
                        }
                        bol.Dat.RefRegNro.Value = mydb.Get("NUM_REGIST").Trim();   //int
                        //bol.Dat.RefRegAnio	.Value = mydb.Get("ANIO").Trim();   //int
                        bol.Dat.Obs.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("OBS_DOCUME").Trim());   //nvarchar
                        bol.PostNewRow();
                        #endregion Asignar valores a BoletinDet
                        //int bolID = bol.Adapter.InsertRow(); 
                        mydb.Skip(1);
                    }
                    #endregion Recorrer Registros
                    mydb.Close();
                }
                else
                {
                    MessageBox.Show(" NO ABIERTO");
                }
            }


            #endregion Procesar Archivo BOL_B1.dbf

            #region Procesar Archivo BOL_B1A.dbf
            if (Utils.existeArchivo(fiBOLB1A))
            {

                mydb.Use(archBOLB1A);
                mydb.Go(0);
                if (mydb.Opened)
                {
                    //MessageBox.Show("ABIERTO. Numero de registros: "+ mydb.RecCount.ToString());

                    #region Recorrer Registros
                    while (!mydb.Eof)
                    {
                        bandera = true;
                        #region Asignar valores a BoletinDet
                        bol.NewRow();
                        bol.Dat.ID.Value = DBNull.Value;   //int PK  Oblig.
                        bol.Dat.BoletinID.Value = DBNull.Value;   //int


                        int año, mes, dia;
                        string AnioMesDia = mydb.Get("FEC_DOCU");
                        año = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(0, 4));
                        mes = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(4, 2));
                        dia = Berke.Libs.Base.ObjConvert.AsInt(AnioMesDia.Substring(6, 2));
                        System.DateTime fecha = new DateTime(año, mes, dia);
                        bol.Dat.SolicitudFecha.Value = fecha;   //smalldatetime Oblig.
                        bol.Dat.ExpNro.Value = expeServ.ObtenerActaNro(mydb.Get("NUM_DOCUME"));   //int Oblig.
                        bol.Dat.ExpAnio.Value = expeServ.ObtenerActaAnio(mydb.Get("NUM_DOCUME"));   //int Oblig.
                        //bol.Dat.Clase		.Value = mydb.Get("CLASE");   //nvarchar
                        bol.Dat.MarcaTipo.Value = DBNull.Value;   //nvarchar
                        bol.Dat.Tramite.Value = mydb.Get("TIP_DOCU").Trim();   //nvarchar Oblig.
                        //bol.Dat.Denominacion.Value = mydb.Get("NOM_DENOMI");   //nvarchar
                        bol.Dat.Propietario.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("NOM_SOLICI").Trim());   //nvarchar
                        bol.Dat.Pais.Value = Utils.cambiarCaracteresEspeciales(mydb.Get("COD_PAIS").Trim());   //nvarchar
                        //bol.Dat.BoletinID.Value = getBoletinID(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                        try
                        {
                            bol.Dat.AgenteLocal.Value = mydb.Get("NUM_AGENTE").Trim();   //nvarchar
                        }
                        catch
                        {
                            bol.Dat.AgenteLocal.Value = 0;
                        }

                        string expe = mydb.Get("NUM_ACTA").Trim();
                        if (expe != "0")
                        {
                            bol.Dat.RefNro.Value = expeServ.ObtenerActaNro(expe);
                            bol.Dat.RefAnio.Value = expeServ.ObtenerActaAnio(expe);
                        }
                        bol.Dat.RefRegNro.Value = mydb.Get("NUM_REGIST").Trim();   //int
                        //bol.Dat.RefRegAnio	.Value = mydb.Get("ANIO");   //int
                        bol.Dat.Obs.Value = mydb.Get("OBS_DOCUME").Trim();   //nvarchar
                        bol.PostNewRow();
                        #endregion Asignar valores a BoletinDet
                        //int bolID = bol.Adapter.InsertRow(); 
                        mydb.Skip(1);
                    }
                    #endregion Recorrer Registros

                    mydb.Close();

                }
                else
                {
                    MessageBox.Show(" NO ABIERTO");
                }
            }
            #endregion Procesar Archivo BOL_B1A.dbf

            #region Revisar el rango de las actas


            bol.Dat.ExpNro.Order = 1;
            bol.Dat.ExpAnio.Order = 2;
            bol.Sort();
            string tramit;
            string UltimaActaNro = "", UltimaActaAnio = "";
            // Tabla : Expediente
            Berke.DG.DBTab.Expediente expediente = new Berke.DG.DBTab.Expediente(db);
            // Tabla : CAgenteLocal
            Berke.DG.DBTab.CAgenteLocal agenteL = new Berke.DG.DBTab.CAgenteLocal(db);
            // Tabla : MarcaRegRen
            Berke.DG.DBTab.MarcaRegRen marRR = new Berke.DG.DBTab.MarcaRegRen(db);
            //Barra de progreso
            pBar.Maximum = bol.RowCount;

            ActasFaltantes = "";

            for (bol.GoTop(); !bol.EOF; bol.Skip())
            {
                pBar.PerformStep();
                bol.Edit();
                bol.Dat.Incorporado.Value = false;
                bol.Dat.Enlazado.Value = false;
                bol.PostEdit();

                #region Proceso
                /*
                tramit= bol.Dat.Tramite.AsString.Trim();
                switch (tramit)
                {
                    case "REG" :
                #region REGISTRO

                        expediente = expeServ.getExpediente(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);

                        if (expediente.RowCount > 0)
                        {
                            //existe el expediente y lo enlazo
                            bol.Edit();
                            bol.Dat.ExpedienteID.Value = expediente.Dat.ID.AsInt;
                            bol.Dat.Enlazado.Value = true;
                            bol.PostEdit();
                        }
                        else
                        {
                            //Buscar agente
                            //Obtener agente local ID
							
                            if (!agServ.getAgenteLocal(bol.Dat.AgenteLocal.AsString).Dat.Nuestro.AsBoolean)
                            {
                                bol.Edit();
                                bol.Dat.Incorporado.Value = true;
                                bol.PostEdit();
                            }
                            else
                            {
                                bol.Edit();
                                bol.Dat.Incorporado.Value = false;
                                bol.PostEdit();
                            }
                        }
                #endregion REGISTRO
                        break;
                    case "REN" :
                #region RENOVACION
                        //BUSCAR SI EXISTE
                        expediente = expeServ.getExpediente(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                        if (expediente.RowCount > 0)
                        {
                            //existe el expediente y lo enlazo
                            bol.Edit();
                            bol.Dat.ExpedienteID.Value = expediente.Dat.ID.AsInt;
                            bol.Dat.Enlazado.Value = true;
                            bol.PostEdit();
                        }
                        else
                        {
                            marRR = regrenServ.getRegRen(bol.Dat.RefRegNro.AsInt);
                            if (bol.Dat.RefRegNro.AsInt==0)
                            {
                                obs = obs + ENTER+ @"Renovación de la marca '" + bol.Dat.Denominacion.AsString + "' no posee número de registro";
                            }
                            //* ---aacuna--- 23/ago/2006 Preguntar por marcaregren en lugar
                            // * de expediente 
                            //if (expediente.RowCount > 0)
                            //
                            if (marRR.RowCount > 0)
                            {
                                //existe el expediente y lo enlazo
                                bol.Edit();
                                bol.Dat.ExpedienteID.Value = marRR.Dat.ExpedienteID.AsInt;
                                bol.Dat.Enlazado.Value = true;
                                bol.PostEdit();

                                //Buscar agente
                                //Obtener agente local ID
								
                                if (!agServ.getAgenteLocal(bol.Dat.AgenteLocal.AsString).Dat.Nuestro.AsBoolean)
                                {
                                    bol.Edit();
                                    bol.Dat.Incorporado.Value = true;
                                    bol.PostEdit();
                                }
                                else
                                {
                                    bol.Edit();
                                    bol.Dat.Incorporado.Value = false;
                                    bol.PostEdit();
                                }
                            }
                        }
                #endregion RENOVACION
                        break;
                    case "TR" :
                    case "CN" :
                    case "CD" :
                    case "FS" :
                    case "LC" :
                #region TVs
                        //BUSCAR SI EXISTE
                        expediente = expeServ.getExpediente(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                        if (expediente.RowCount > 0)
                        {
                            //existe el expediente y lo enlazo
                            bol.Edit();
                            bol.Dat.ExpedienteID.Value = expediente.Dat.ID.AsInt;
                            bol.Dat.Enlazado.Value = true;
                            bol.PostEdit();
                        }
                        else
                        {
                            expediente = expeServ.getExpediente(bol.Dat.RefNro.AsInt, bol.Dat.RefAnio.AsInt);

                            //existe el expediente padre
                            if ( expediente.RowCount > 0 )
                            {
                                //verificar q no sea nuestra la marca
                                if (!agServ.getAgenteLocal(bol.Dat.AgenteLocal.AsString).Dat.Nuestro.AsBoolean)
                                {
                                    bol.Edit();
                                    bol.Dat.Incorporado.Value = true;
                                    bol.PostEdit();
                                }
                                else
                                {
                                    bol.Edit();
                                    bol.Dat.Incorporado.Value = false;
                                    bol.PostEdit();
                                }
                            }
                        }
                #endregion TVs
                        break;

                    default :
                #region VARIOS
                        //BUSCAR SI EXISTE
                        if (!bol.Dat.RefNro.IsNull)
                        {
                            expediente = expeServ.getExpediente(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                            if (expediente.RowCount > 0)
                            {
                                //existe el expediente y lo enlazo
                                bol.Edit();
                                bol.Dat.ExpedienteID.Value = expediente.Dat.ID.AsInt;
                                bol.Dat.Importado.Value = true;
                                bol.PostEdit();

                            }
                        }
                #endregion VARIOS
                        break;

                }
        */
                #endregion Proceso

                if (UltimoLeido != 0)
                {
                    ValorEsperado = UltimoLeido + 1;

                    /*[ggaleano 13.03.2017] Removemos el control de duplicidad en la lectura de boletines */
                    //Controlar que el acta no se encuentre duplicada en el boletin
                    if (ValorEsperado - 1 == bol.Dat.ExpNro.AsInt)
                    {
                        //if (!excluirActasDuplicadas)
                        //{
                        //    string msg = "El acta " + bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/" +
                        //        bol.Dat.ExpAnio.AsString + " se encuentra duplicado. " +
                        //        "Debe eliminar una de las actas de los archivos del boletin";
                        //    this.obs += msg + ENTER;
                        //    throw new Exceptions.BolImportException(msg);
                        //}
                        //else
                        //{
                        //    ValorEsperado--;
                        //    //bol.Delete();
                        //}
                        ValorEsperado--;
                    }

                    while (ValorEsperado != bol.Dat.ExpNro.AsInt)
                    {
                        ActasFaltantes = ActasFaltantes + ValorEsperado.ToString() + "/" + bol.Dat.ExpAnio.AsString + " ";
                        ActasFalCant++;
                        ValorEsperado++;
                        //if (ActasFalCant > 1500)
                        //{
                        //    string msg = "Excesiva Cantidad de Actas Faltantes en el Boletin";
                        //    this.obs += "ABORTADO: " + msg + Utils.ENTER + ActasFaltantes;
                        //    throw new Exceptions.BolImportException(msg);
                        //}

                    }

                }
                else
                {
                    ActasLeidas = bol.Dat.ExpNro.AsString + "/" + bol.Dat.ExpAnio.AsString + " - ";
                }
                UltimoLeido = bol.Dat.ExpNro.AsInt;
                UltimaActaNro = bol.Dat.ExpNro.AsString;
                UltimaActaAnio = bol.Dat.ExpAnio.AsString;
            }
            if (bandera)
            {
                ActasLeidas = ActasLeidas + UltimaActaNro + "/" + UltimaActaAnio;
            }
            obs += "Rango de Actas Leídas : " + ActasLeidas + ENTER + "Cantidad de Actas Faltantes en el Boletin : " + ActasFalCant.ToString() + ENTER;
            if (ActasFalCant > 0)
            {
                obs += "Actas Faltantes: " + ActasFaltantes + ENTER;
            }
            #endregion Revisar el rango de las actas

            // Posiblemente haya que borrar esta parte.
            DataTable dt = bol.Table;

            return dt;
		}
		#endregion Leer desde DBF

		#region Revisar Fechas y Nro. Boletin
		public DataTable procesarFechasBoletin()
		{
			DataTable tbl = new DataTable("fecha_boletin");
			tbl.Columns.Add("Fecha");
			tbl.Columns.Add("Nro");
			/*[ggaleano 08/11/2007] Agregamos clave primaria a la tabla para evitar
			 * que se dupliquen los datos.*/
			DataColumn[] keys = new DataColumn[2];
			keys[0] = tbl.Columns["Fecha"];
			keys[1] = tbl.Columns["Nro"];
			tbl.PrimaryKey = keys;

			string [] arr = new string[2];
			string fec_ant = "";
			
			for (bol.GoTop(); !bol.EOF; bol.Skip()) 
			{
				if (String.Format("{0:d}", bol.Dat.SolicitudFecha.AsDateTime) != fec_ant) 
				{
					arr[0] = String.Format("{0:d}", bol.Dat.SolicitudFecha.AsDateTime);
					arr[1] = Utils.semanaDelAnho(bol.Dat.SolicitudFecha.AsDateTime).ToString();
					fec_ant = String.Format("{0:d}", bol.Dat.SolicitudFecha.AsDateTime);
					if (!tbl.Rows.Contains(arr))
					{
						tbl.Rows.Add(arr);					
					}
				}
			}

			

			return tbl;
		}
		#endregion Revisar Fechas y Nro. Boletin

		#region Obtener Boletin
		public Berke.DG.DBTab.Boletin getBoletin(string nro, string anio)
		{
			boletin = new Berke.DG.DBTab.Boletin( db );
			boletin.Dat.Nro.Filter	= nro;			
			boletin.Dat.Anio.Filter	= anio;
			boletin.Adapter.ReadAll();
			return boletin;
		}
		public Berke.DG.DBTab.BoletinDet getBoletinDet(string actanro, string actaanio)
		{
			Berke.DG.DBTab.BoletinDet bdet = new Berke.DG.DBTab.BoletinDet( db );
			bdet.Dat.ExpNro.Filter		= actanro ;			
			bdet.Dat.ExpAnio.Filter		= actaanio;
			bdet.Adapter.ReadAll();
			return bdet;
		}

        public bool chkRegistroTercero(int actanro, int actaanio)
        {
            bool result = false;

            Berke.DG.DBTab.BoletinDet bdet = new DG.DBTab.BoletinDet(db);
            bdet.Dat.ExpNro.Filter = actanro;
            bdet.Dat.ExpAnio.Filter = actaanio;
            bdet.Adapter.ReadAll();

            if ((bdet.RowCount > 0) && (regServ.isRegistro(bdet.Dat.Tramite.AsString)))
            {
                result = !((Berke.DG.DBTab.CAgenteLocal)(agServ.getAgenteLocal(bdet.Dat.AgenteLocal.AsString))).Dat.Nuestro.AsBoolean;

            }

            return result;
        }

		#endregion Obtener Boletin

		#region Cargar Boletin
		public DataTable cargarBoletin(string carpnro, string carpanio)
		{
			boletin = this.getBoletin(carpnro, carpanio);
			if ((boletin.RowCount>0) && !boletin.Dat.Boletin.IsNull)
			{
				this.obs = boletin.Dat.Boletin.AsString + ENTER;
				bol.ClearFilter();
				bol.ClearOrder();
							
				bol.Dat.BoletinID.Filter = boletin.Dat.ID.AsInt;			
				bol.Adapter.ReadAll(MAX_ROWS);
						
				return bol.Table;		
			}
			return null;
		}
		public DataTable cargarBoletin(Berke.DG.DBTab.BoletinDet bdet)
		{
			bdet.Adapter.ReadAll(MAX_ROWS);
			if (bdet.RowCount>0)
			{
				this.bol = bdet;
				return bdet.Table;
			}
			return null;
		}
		#endregion Cargar Boletin

		#region Actualizar BoletinDet
		public string actualizarBoletinDet(string carpNro)
		{		
			int ndel  =0;
			int nmod  =0;
			int nins  =0;
			int nskip =0;
			try 
			{
				db.IniciarTransaccion();
				for(bol.GoTop(); !bol.EOF; bol.Skip())
				{
					if (bol.RowState== System.Data.DataRowState.Deleted)
					{
						/*
						bol.Delete();
						bol.Adapter.DeleteRow();
						*/					
						ndel++;

					}
					else if (bol.RowState== System.Data.DataRowState.Modified)
					{
						bol.Edit();
						// Actualizar el nro del Boletín cuando actualizan la fecha de presentación
						bol.Dat.BolNro.Value	= Utils.semanaDelAnho(bol.Dat.SolicitudFecha.AsDateTime).ToString();
						bol.Dat.BolAnio.Value	= bol.Dat.SolicitudFecha.AsDateTime.Year;
                        bol.Dat.Enlazado.Value  = !(bol.Dat.ExpedienteID.AsInt == 0);

						bol.PostEdit();
						bol.Adapter.UpdateRow();
						nmod++;
						this.obs+= "El tramite " + bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/" +
							bol.Dat.ExpAnio.AsString + " fue actualizado."+ENTER;
					}
					else if (bol.RowState== System.Data.DataRowState.Added)
					{
						Berke.DG.DBTab.BoletinDet bdup = new Berke.DG.DBTab.BoletinDet(db);
						bdup.Dat.ExpNro.Filter = bol.Dat.ExpNro.AsInt;
						bdup.Dat.ExpAnio.Filter = bol.Dat.ExpAnio.AsInt;
						bdup.Adapter.ReadAll();
						if (bdup.RowCount>0)
						{
							this.obs+= "El tramite " + bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/" +
							bol.Dat.ExpAnio.AsString + " no pudo ser agregado. Acta duplicada." + ENTER ;
							bol.Delete();
							continue;							
						}
						int boletinID = this.getBoletinID(bol.Dat.SolicitudFecha.AsDateTime, carpNro);
						bol.Edit();
						bol.Dat.BoletinID.Value = boletinID;
						//bol.Dat.BolNro.Value	= this.getNroBoletin(bol.Dat.SolicitudFecha.AsDateTime,dtNroBoletin);
						bol.Dat.BolNro.Value	= Utils.semanaDelAnho(bol.Dat.SolicitudFecha.AsDateTime).ToString();
						bol.Dat.BolAnio.Value	= bol.Dat.SolicitudFecha.AsDateTime.Year;
						bol.PostEdit();
						bol.Adapter.InsertRow();
						nins++;
						this.obs+= "El tramite " + bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/" +
							bol.Dat.ExpAnio.AsString + " fue agregado."+ENTER;
					}
					else 
					{
						nskip++;
					}
				}
				db.Commit();
				this.stats.nexcl = ndel;
				this.stats.nproc = nins + nmod;
				this.stats.nskip = nskip;
			}
			catch (Exception ex)
			{
				db.RollBack();
				throw new Exceptions.BolImportException("Error al actualizar cambios ("+bol.Dat.ExpNro.AsString+"/"+bol.Dat.ExpAnio.AsString+"):"+ ex.Message);
			}
			return "("+nins+") fila(s) insertada(s)"+ ENTER +"("+ nmod+") fila(s) modificada(s)"+ ENTER+
				"(<nexclude>) fila(s) excluida(s)";
		
		}
		#endregion Actualizar BoletinDet

		#region Verificar Fechas
		public string chkFechaSolicitud(DateTime solicitudFecha)
		{
			DateTime d2 = System.DateTime.Now;

			if ( d2 < solicitudFecha)
			{
				return "No inc. Acta "+bol.Dat.Tramite.AsString + " " +bol.Dat.ExpNro.AsString+"/"+bol.Dat.ExpAnio.AsString+ "."+
					"Fecha de solicitud mayor a fecha actual.";
				
			}
			return "";
		}
		#endregion Verificar Fechas

		#region Importar Boletin
		public bool importarBoletin(string bolNro, string bolAnio, DataTable dtNroBoletin, bool excluirRegTerceros = false)
		{			
			Berke.DG.DBTab.BoletinDet bdet;
			Berke.DG.DBTab.Boletin boletin = this.getBoletin(bolNro, bolAnio);
			int boletinID=-1;
            string actasDup = string.Empty;
            int cActasDup = 0;

			#region Procesar boletin
			try 
			{
                db.IniciarTransaccion();

				pBar.Maximum = bol.RowCount;
                
				#region Verificar si ya existe una entrada en Boletin para el Nro y Anio
				try 
				{
					if (boletin.RowCount==0)
					{
						#region Asignar valores a Boletin
						boletin.NewRow(); 
						boletin.Dat.ID			.Value = DBNull.Value;   //int PK  Oblig.
						boletin.Dat.Nro			.Value = bolNro;					
						boletin.Dat.Anio		.Value = bolAnio;
						boletin.Dat.Fecha		.Value = System.DateTime.Today;   //smalldatetime Oblig.
						if (this.obs.Length > 600)
						{
							boletin.Dat.Boletin		.Value = this.obs.Substring(0, 600);   //nvarchar
						}
						else
						{
							boletin.Dat.Boletin		.Value = this.obs;   //nvarchar
						}
						boletin.PostNewRow(); 
						boletinID = boletin.Adapter.InsertRow(); 
						#endregion Asignar valores a Boletin
					}
					else 
					{
						boletinID = boletin.Dat.ID.AsInt;
					}
				
				}
				catch(Exception ex) 
				{
					db.RollBack();
					MessageBox.Show("Error al generar información cabecera del Boletin: " + ex.Message);
				}
				#endregion Verificar si ya existe una entrada en Boletin para el Nro y Anio
			
				bol.Dat.ExpNro.Order  = 1;
				bol.Dat.ExpAnio.Order = 2;
				bol.Sort();

				stats.reset();

                for (bol.GoTop();!bol.EOF;bol.Skip())
				{				
					stats.nproc++;						
					pBar.PerformStep();

					bol.Edit();
					bol.Dat.BoletinID.Value = boletinID;
					bol.Dat.Completo.Value	= false;
					bol.Dat.BolNro.Value	= this.getNroBoletin(bol.Dat.SolicitudFecha.AsDateTime,dtNroBoletin);
					bol.Dat.BolAnio.Value	= bol.Dat.SolicitudFecha.AsDateTime.Year;
					bol.PostEdit();

					bdet = this.getBoletinDet(bol.Dat.ExpNro.AsString,bol.Dat.ExpAnio.AsString);

                    bool regTerceros = false;
                    if (excluirRegTerceros)
                    {
                        regTerceros = this.chkRegistroTercero(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt);
                    }

					if ((bdet.RowCount > 0) && (!regTerceros))
					{
						//throw new Exceptions.BolImportException("Ya se ha importado el trámite: "+bol.Dat.ExpNro.AsInt+"/"+bol.Dat.ExpAnio.AsInt);
                        actasDup += (actasDup != string.Empty ? " " : string.Empty) + bol.Dat.ExpNro.AsString + "/" + bol.Dat.ExpAnio.AsString;
                        cActasDup++;
					}

                    //if (bdet.RowCount == 0)
                    if (!regTerceros)
                        bol.Adapter.InsertRow();										
					
				}
				db.Commit();
			}
			catch(Exception ex)
			{
				stats.reset();
				db.RollBack();
				throw ex;
			}
			#endregion Procesar boletin

            if (actasDup != string.Empty)
            {
                MessageBox.Show("Existen " + cActasDup.ToString() +" actas duplicadas, verifique las observaciones." + Utils.ENTER +
                                "Actas Duplicadas: " + actasDup + Utils.ENTER,
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.obs += "Actas Duplicadas: " + actasDup + Utils.ENTER;
            }
            
			return true;
			
		}
		#endregion Importar Boletin

		#region Obtener Nro de Boletin
		public string getNroBoletin(DateTime solicitudFecha, DataTable tbl)
		{
		
			string fecha = "";
			string nro = "";
			foreach (DataRow row in tbl.Rows) 
			{
				fecha = (string) row["Fecha"];
				nro   = (string) row["Nro"];
				if (String.Format("{0:d}", solicitudFecha) == fecha) 
				{
					return nro;
				}
			}
			// si no existe un boletín en la fecha, se toma el último.
			return nro;
			
		}
		#endregion Obtener Nro de Boletin

		#region Incorporar Boletin
		public bool incorporarBoletin()
		{

			pBar.Maximum = bol.Table.Rows.Count;
			
			Berke.DG.DBTab.Marca mar					= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expe				= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.CAgenteLocal agenteL			= new Berke.DG.DBTab.CAgenteLocal( db );
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.Marca marcaPadre				= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expePadre;
			
			stats.reset();
								
			string bolTramite	= "";
			
			// Ya esta disponible a la hora de "Cargar"
			//boletin				= this.getBoletin(carpNro, carpAnio);

			#region Procesar boletin
			for (bol.GoTop();!bol.EOF;bol.Skip())
			{
				// Verificamos si la fecha de solicitud es coherente
				string msg = this.chkFechaSolicitud(bol.Dat.SolicitudFecha.AsDateTime);
				string notif = "";
				int nronotif = 0;
				if (msg.Length>0)
				{	
					stats.nskip++;
					this.obs+= msg + ENTER;
					continue;
				}
				// Procesamos sólo aquellos no excluidos (eliminados visualmente)
				pBar.PerformStep();
				pBar.Refresh();
				if ( bol.RowState != System.Data.DataRowState.Deleted )
				{										
					try
					{

						expe =  new Berke.DG.DBTab.Expediente( db );
						
						bolTramite = bol.Dat.Tramite.AsString.Trim();
						
						
						#region Controlar dependencias
						
						//MessageBox.Show("Verificar Dependencias");
						string lst_dep = this.haveBoletinDetHnos(bol);
						//MessageBox.Show("Dependencias Verificadas");
						if (lst_dep != "")
						{
							stats.nskip++;
							msg =  "El trámite "+bol.Dat.Tramite.AsString + " "+
								bol.Dat.ExpNro.AsString + "/" + bol.Dat.ExpAnio.AsString + " "+
								" necesita de los trámites: " + lst_dep;
							this.obs+= msg + ENTER;
							MessageBox.Show(msg);							
							continue;
						}
						
						#endregion Controlar dependencias

						db.IniciarTransaccion();

						if (regServ.isRegistro(bolTramite)) 
						{
							#region REGISTRO
							//BUSCAR SI EXISTE
							expe = expeServ.getExpediente(bol.Dat.ExpNro.AsInt, bol.Dat.ExpAnio.AsInt, (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO);

							if (expe.RowCount > 0) 
							{
								//existe el expediente y lo enlazo
								bol.Edit();
								bol.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
								bol.PostEdit();
								stats.nproc++;
							} 
							else 
							{
								agenteL = agServ.getAgenteLocal(bol.Dat.AgenteLocal.AsString);

								if (agenteL.RowCount == 0)
								{
									agenteL = agServ.crearAgenteLocal(bol.Dat.AgenteLocal.AsString);
								}
									
								//verificar q no sea nuestra la marca
								//if ( (agenteL.RowCount == 0) | (!agenteL.Dat.Nuestro.AsBoolean) ) 
								if (!agenteL.Dat.Nuestro.AsBoolean)
								{						
									regServ.setAgenteLocal(agenteL);
									regServ.setBoletin(bol);
									int expeID   = regServ.guardarExpediente();
									int regrenID = regServ.guardarRegRen();
									int marcaID  = regServ.guardarMarca(regrenID);

									//expeServ.setExpeID(expeID);
									//expeServ.asociarMarcaRegistro(marcaID,regrenID);
									regServ.asociarExpeMarcaRegistro(marcaID,regrenID);
										
									expeServ.addCampoPropietarioActualNombre(bol.Dat.Propietario.AsString, expeID);
									expeServ.addCampoPropietarioActualPais(bol.Dat.Pais.AsString,expeID);

									expeServ.addSituacion(RegistroService.SIT_TRAMITE,bol.Dat.SolicitudFecha.AsDateTime,1,expeID);

									bol.Edit();
									bol.Dat.ExpedienteID.Value	= expeID;
									bol.Dat.Enlazado.Value		= true;
									bol.Dat.Incorporado.Value	= true;
									bol.PostEdit();
									stats.nproc++;
								}
								else 
								{
									msg = "Nro. proc. tramite "+ bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/"+ bol.Dat.ExpAnio.AsString + " ("+bol.Dat.Denominacion.AsString +")";
									stats.nskip++;

									Berke.DG.DBTab.BoletinDet bdet = new Berke.DG.DBTab.BoletinDet(db);
									bdet.Adapter.ReadByID(bol.Dat.ID.AsInt);

									if (bdet.Dat.RefAnio.AsString != "")
									{
										msg = msg + " Acta/Año Referencia: " + bdet.Dat.RefNro.AsString + "/" + bdet.Dat.RefAnio.AsString;
									}
									else if (bdet.Dat.RefRegNro.AsString != "")
									{
										msg = msg + " Registro/Año de Referencia: " + bdet.Dat.RefRegNro.AsString + "/" + bdet.Dat.RefRegAnio.AsString;
									}

									msg = msg + " Clase: " + bdet.Dat.Clase.AsString + " Tipo de Marca: " + bdet.Dat.MarcaTipo.AsString + ". Posible situación H.I para trámite nuestro." + ENTER;

									this.obs+= msg;
									msg = "Hola,"+ Utils.ENTER + "El proceso de Boletín informa lo siguiente:"+ Utils.ENTER+ msg;
									
									Utils.enviarNotificacion(Utils.NOTIF_PRESENTADA,msg,db);
								}
							}
							#endregion REGISTRO
						}
						else if (renServ.isRenovacion(bolTramite))
						{
							#region RENOVACION
							int estado = this.procesarREN(bol,-1);
							if(estado < 0)
							{
								stats.nskip++;									
								db.RollBack();
								continue;
							}
							stats.nproc++;
							#endregion RENOVACION
						}
						else if(tvServ.isTramiteVario(bolTramite))
						{						
							#region TVs
							int estado = this.procesarTV(bol, -1);
							if ( estado < 0 )
							{
								stats.nskip++;
								db.RollBack();
								continue;
							}
							stats.nproc++;
							#endregion TVs
						}
						else 
						{
							#region VARIOS
							//BUSCAR SI EXISTE
							expePadre = expeServ.getExpedientePadre(bol);

							if ((expePadre != null) && (expePadre.RowCount > 0)) 
							{
								//existe el expediente y lo enlazo
								bol.Edit();
								bol.Dat.ExpedienteID.Value = expePadre.Dat.ID.AsInt;
								bol.Dat.Enlazado.Value     = true;
								bol.PostEdit();
								stats.nproc++;
								if (bol.Dat.Tramite.AsString == "MI")
								{
									notif = "Notificación de procesamiento de Modelo Industrial (MI)" + Utils.ENTER;
									notif += "El trámite MI " + bol.Dat.ExpNro.AsString + "/" + bol.Dat.ExpAnio.AsString + " ha sido procesado." + Utils.ENTER;
									nronotif = Utils.NOTIF_PROC_MI;
								}
							}
							else if (this.getListaVerificacion().Contains(bolTramite)) 
							{
								stats.nskip++;
								this.obs+= "No proc. tramite "+ bol.Dat.Tramite.AsString + " " + bol.Dat.ExpNro.AsString + "/"+ bol.Dat.ExpAnio.AsString +". Acta padre no válida." + ENTER ;
								db.RollBack();
								continue;
							}
							else 
							{
								stats.nskip++;
								db.RollBack();
								continue;
							}
							#endregion VARIOS
						}
						
						bol.Edit();
						//bol.Dat.BoletinID.Value = boletin.Dat.ID.AsInt;
						bol.Dat.Completo.Value  = true;
						bol.PostEdit();
						bol.Adapter.UpdateRow();
	
						db.Commit();
						if ((notif != "") && (nronotif != 0))
						{
							Utils.enviarNotificacion(nronotif, notif, db);
						}

					} 
					catch (Exception ex) 
					{						
						db.RollBack();
						throw new Exceptions.BolImportException("Error al procesar acta " + bol.Dat.ExpNro.AsString + " : " + ex.Message);
					}
				}
				else 
				{
					stats.nexcl++;
				}
					
			}
			#endregion Procesar boletin

			#region Actualizar Observación
			/*
						if (actualizarObs)
						{
							region Actualizar Obs del boletin
							try 
							{
								db.IniciarTransaccion();
								boletin.Adapter.ReadByID(boletinID);
								boletin.Edit();
								boletin.Dat.Boletin.Value = txtObs.Text;
								boletin.PostEdit();
								boletin.Adapter.UpdateRow();
								db.Commit();
							}
							catch(Exception ex)
							{
								db.RollBack();
								db.IniciarTransaccion();
								boletin.Adapter.ReadByID(boletinID);
								boletin.Edit();
								boletin.Dat.Boletin.Value = txtObs.Text.Substring(0,600);
								boletin.PostEdit();
								boletin.Adapter.UpdateRow();
								db.Commit();

								MessageBox.Show("La observación del boletín tuvo que ser truncada. Demasiadas actas no incorporadas.");
							}
							endregion Actualizar Obs del Boletin
								
							dgBoletin.DataSource=bol.Table;
							string message = "Boletín procesado correctamente. Procesadas ("+nproc+") actas. Desea salir?";
							string caption = "Proceso de Boletín";
							MessageBoxButtons buttons = MessageBoxButtons.YesNo;
							DialogResult result;
							result = MessageBox.Show(this, message, caption, buttons,
								MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 
								MessageBoxOptions.RightAlign);
			
							if(result == DialogResult.Yes) this.Close();
						}
						*/
			#endregion Actualizar Observación

			return true;
		}

		#endregion Incorporar Boletin

		#region Procesar TV
		/// <summary>
		/// Procesa un TV a partir de un detalle de Boletín
		/// </summary>
		/// <param name="bdet">BoletinDet</param>
		/// <param name="propID">Id del propietario. Setear -1 para obviar propietario</param>
		private int procesarTV(Berke.DG.DBTab.BoletinDet bdet, int propietarioID)
		{
			Berke.DG.DBTab.Marca mar					= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expe				= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.CAgenteLocal agenteL			= new Berke.DG.DBTab.CAgenteLocal( db );
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.Marca marcaPadre				= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.Propietario prop				= new Berke.DG.DBTab.Propietario(db);
			string bolTramite = bdet.Dat.Tramite.AsString;

			string err_base = "Nro. inc. " + bolTramite + " " + bdet.Dat.ExpNro.AsString + "/"+ bdet.Dat.ExpAnio.AsString +" ("+bdet.Dat.Denominacion.AsString+").";

			#region Recuperar Propietario
			if (propietarioID < 0)
			{
				prop = null;
			}
			else 
			{
				prop = propServ.getPropietario(propietarioID);							
			}
			#endregion Recuperar Propietario

			#region TVs
			//Buscar expediente correspondiente al trámite
			tvServ.setTramite(bolTramite);
			expe = expeServ.getExpediente(bdet.Dat.ExpNro.AsInt,bdet.Dat.ExpAnio.AsInt, tvServ.getTipoTramite());

			// Existe Expediente
			if (expe.RowCount > 0) 
			{
				#region Existe expediente
				// Obtenemos el Expediente padre a partir de registro o acta
				// de referencia
				expePadre = expeServ.getExpedientePadre(bdet);
						
				if (expePadre.RowCount > 0) 
				{
					// Reasignar el expediente padre de manera a que apunte al último
					// registro o renovación que se encuentre en trámite 
					// Si no existe, se utiliza el actual.
					//expeServ.setExpeID(expePadre.Dat.ID.AsInt);
					expeServ.setExpediente(expePadre);
					expePadre       = expeServ.getExpeRenRegPosterior();
					int expePadreID = expePadre.Dat.ID.AsInt;

					//existe el expediente padre
					bdet.Edit();
					bdet.Dat.ExpedienteID.Value = expePadreID;
					bdet.Dat.Enlazado.Value = true;
					bdet.PostEdit();

					if(prop != null)
					{
						#region Asociar propietarios
						marcaPadre = null;
						marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);
						marServ.updateDatosPropietario(prop);
						marServ.deletePropietarios();
						marServ.addPropietario(propietarioID);
						// Borramos los campos del propietario actual
						expeServ.delCampoPropietarioActual(expe.Dat.ID.AsInt);
						// Setear la información completa del propietario Actual									
						expeServ.addCampoPropietarioActual(prop,expe.Dat.ID.AsInt);
						// Se asocia el propietario al expediente
						expeServ.setExpeID(expe.Dat.ID.AsInt);
						expeServ.addPropietario(propietarioID,true);
						// El expediente Padre esta en trámite?
						expeServ.setExpeID(expePadreID);
									
						if ( expeServ.sinRegistro() && !tvServ.tieneTVPosterior(bdet) )
						{
							Berke.DG.DBTab.Poder poderAnt = new Berke.DG.DBTab.Poder(db);
							poderAnt = expeServ.getPoder();
							if (poderAnt.RowCount>0)
							{
								// Guarda la información de poder anterior en el 
								// expediente padre
								expeServ.addCampoPoderAnterior(poderAnt,expePadreID);
								expeServ.delPoder();
							}
							expeServ.delPropietarios();
							expeServ.addPropietario(propietarioID,true);										
						}
						else 
						{
							this.obs+= bolTramite + " " + bdet.Dat.ExpNro.AsString + "/"+ bdet.Dat.ExpAnio.AsString +". "+"No se actualizo el propietario del expediente padre." + ENTER;
						}
						#endregion Asociar propietarios									
					}
				}
				else 
				{
					this.obs += "No inc. Acta: "+ bdet.Dat.ExpNro.AsString + "/"+ bdet.Dat.ExpAnio.AsString +". Acta o registro padre no válida."+ENTER;
					return -1;
				}
				#endregion Existe Expediente
			} 
			else 
			{
				#region No Existe expediente
				expePadre = expeServ.getExpedientePadre(bdet);

				int cant_filas = 0;
				if (expePadre != null )
				{
					cant_filas = expePadre.RowCount;
				}

				if (cant_filas > 0) 
				{
					//existe el expediente padre
					int expePadreID = expePadre.Dat.ID.AsInt;
					int marPadreID  = expePadre.Dat.MarcaID.AsInt;
					agenteL         = agServ.getAgenteLocal(bdet.Dat.AgenteLocal.AsString);	
					
					if (agenteL.RowCount == 0)
					{
						agenteL = agServ.crearAgenteLocal(bol.Dat.AgenteLocal.AsString);
					}

					//verificar q no sea nuestra la marca
					//if ( (agenteL.RowCount == 0) | (!agenteL.Dat.Nuestro.AsBoolean) ) 
					if (!agenteL.Dat.Nuestro.AsBoolean) 
					{
						// Reasignar el expediente padre de manera a que apunte al último
						// registro o renovación que se encuentre en trámite 
						// Si no existe, se utiliza el actual.
						//expeServ.setExpeID(expePadreID);
						expeServ.setExpediente(expePadre);
						expePadre = expeServ.getExpeRenRegPosterior();
						expePadreID = expePadre.Dat.ID.AsInt;
						marPadreID  = expePadre.Dat.MarcaID.AsInt;																

						tvServ.setTramite(bolTramite);
						tvServ.setBoletin(bdet);
						tvServ.setAgenteLocal(agenteL);
						// guardamos el expediente
						int expeID = tvServ.guardarExpediente(expePadre);											
											
						#region Guardar expediente campo, etc
						if (tvServ.isLicencia(bolTramite)) 
						{ //LICENCIA 
							expeServ.addCampoLicenciatarioNombre(bdet.Dat.Propietario.AsString, expeID);
							expeServ.addCampoLicenciatarioDireccion(bdet.Dat.Pais.AsString, expeID);
						} 
						else 
						{
							marServ.setMarcaID(marPadreID);
							marcaPadre     = marServ.getMarca();
							string propIDs = marServ.getListaPropietariosID();

							expeServ.addCampoPropietarioAnteriorNombre(marcaPadre.Dat.Propietario.AsString, expeID);
							expeServ.addCampoPropietarioAnteriorPais(marcaPadre.Dat.ProPais.AsString, expeID);
							expeServ.addCampoPropietarioAnteriorDireccion(marcaPadre.Dat.ProDir.AsString, expeID);
							expeServ.addCampoPropietarioAnteriorID(propIDs,expeID);
							// Retrasar esta operación si es que se indico 
							// el propietario
							if (propietarioID<0)
							{
								expeServ.addCampoPropietarioActualNombre(bdet.Dat.Propietario.AsString,expeID);
								expeServ.addCampoPropietarioActualPais(bdet.Dat.Pais.AsString, expeID);
							}

							// Agregado por mbaez. 
							// para evitar perder información de prop. de marcas
							// propias.
							if(marcaPadre.Dat.Vigilada.AsBoolean)
							{
								// Tenemos en cuenta si es que se proporciona Propietario
								if (propietarioID>=0)
								{
									#region Asociar propietarios
									marServ.setMarcaID(marcaPadre.Dat.ID.AsInt);
									marServ.deletePropietarios();
									marServ.addPropietario(propietarioID);
									// Setear la información completa del propietario Actual									
									expeServ.addCampoPropietarioActual(prop, expeID);
									// Se asocia el propietario al expediente
									expeServ.setExpeID(expeID);
									expeServ.addPropietario(propietarioID,true);
									// El expediente Padre esta en trámite?
									expeServ.setExpeID(expePadreID);
									
									if ( expeServ.sinRegistro() && !tvServ.tieneTVPosterior(bdet) )
									{
										Berke.DG.DBTab.Poder poderAnt = new Berke.DG.DBTab.Poder(db);
										poderAnt = expeServ.getPoder();
										if (poderAnt.RowCount>0)
										{
											// Guarda la información de poder anterior en el 
											// expediente padre
											expeServ.addCampoPoderAnterior(poderAnt,expePadreID);
											expeServ.delPoder();
										}
										expeServ.delPropietarios();
										expeServ.addPropietario(propietarioID,true);
										
									}
									#endregion Asociar propietarios									
								}
								else 
								{
									this.obs += err_base + "Afecta a marcas propias. Verifique Trámites de Terceros." + ENTER; 
									return -1;
								}

								#region Verificar si existe un propietario similar - Deprecated
								/* Deprecated - Se controla en un proceso diferente
								//Agregado por mbaez												
								PropietarioSearch ps = new  PropietarioSearch(db);
								int propID = ps.searchByMarca(marPadreID,bdet.Dat.Denominacion.AsString, tvServ.getTipoTramite());
								if (propID!=-1)
								{
									ps.deleteByMarca(marPadreID);
									ps.addExistingProp(marPadreID,propID);
								}
								else
								{
									// Se mantiene el propietario (no se borra) pero 
									// tampoco se codifica uno nuevo (por el momento)
								}
								*/
								#endregion Verificar si existe un propietario similar
							}
							else 
							{
								//marServ.setMarcaID(marPadreID);
								marServ.setMarca(marcaPadre);
								#region Borrar PropietarioXMarca actuales
													
								propXmarca = marServ.getPropietariosID();

								//int propID = -1;
								for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
								{
									#region Verificar si existe  un propietario similar - Deprecated
									/* Deprecated - Se controla en un proceso aparte
									// Vemos si ya existe el propietario propietario
									if (propID>0) 
									{
										PropietarioSearch ps = new  PropietarioSearch(db,propXmarca.Dat.PropietarioID.AsInt);
										propID = ps.search(bdet.Dat.Denominacion.AsString, tvServ.getTipoTramite()).Dat.ID.AsInt;
									}
									*/
									#endregion Verificar si existe  un propietario similar - Deprecated
														
									propXmarca.Delete();
									propXmarca.Adapter.DeleteRow();
								}
								#endregion Borrar PropietarioXMarca actuales

								#region Agregar PropietarioXMarca si existe - Deprecated
								/* Deprecated - Se realiza en un proceso aparte.
								// Si ya existe el propietario, no hace falta volver a codificarlo.
								// y lo agregamos al propietarioxmarca
								if (propID != -1)
								{
									marServ.addPropietario(propID);
								}
								*/
								#endregion Agregar PropietarioXMarca
							}
							#region Actualizar valores de Marca
							marcaPadre.Edit();
							if (propietarioID<0)
							{
								marcaPadre.Dat.Propietario.Value = Utils.cambiarCaracteresEspeciales(bdet.Dat.Propietario.AsString);
								marcaPadre.Dat.ProPais.Value = Utils.cambiarCaracteresEspeciales(bdet.Dat.Pais.AsString);
								marcaPadre.Dat.ProDir.Value = DBNull.Value;
							}
							else 
							{
								marcaPadre.Dat.Propietario.Value = prop.Dat.Nombre.AsString;
								marcaPadre.Dat.ProPais.Value     = this.getPais(prop.Dat.PaisID.AsInt).Dat.paisalfa.AsString;
								marcaPadre.Dat.ProDir.Value      = prop.Dat.Direccion.AsString;
							}
							marcaPadre.PostEdit();
							marcaPadre.Adapter.UpdateRow();
							#endregion Actualizar valores de Marca
						}
						#endregion Guardar expedienteCampo, etc
											
						expeServ.addSituacion(tvServ.getSitTramite(),bdet.Dat.SolicitudFecha.AsDateTime, tvServ.getSitTramite(),expeID);
									
						bdet.Edit();
						bdet.Dat.ExpedienteID.Value	= expePadreID;
						bdet.Dat.Enlazado.Value		= true;
						bdet.Dat.Incorporado.Value	= true;
						bdet.PostEdit();
					} 
					else 
					{
						bdet.Edit();
						bdet.Dat.Incorporado.Value = false;
						bdet.PostEdit();
						string msg = err_base;

						if (bdet.Dat.RefAnio.AsString != "")
						{
							msg = msg + " Acta/Año Referencia: " + bdet.Dat.RefNro.AsString + "/" + bdet.Dat.RefAnio.AsString;
						}
						else if (bdet.Dat.RefRegNro.AsString != "")
						{
							msg = msg + " Registro/Año de Referencia: " + bdet.Dat.RefRegNro.AsString + "/" + bdet.Dat.RefRegAnio.AsString;
						}

						msg = msg + " Clase: " + bdet.Dat.Clase.AsString + " Tipo de Marca: " + bdet.Dat.MarcaTipo.AsString + ". Posible situación H.I." + ENTER;

						this.obs += msg + ENTER;

						msg = "Hola,"+ Utils.ENTER + "El proceso de Boletín informa lo siguiente:"+ Utils.ENTER+ msg + Utils.ENTER;
						Utils.enviarNotificacion(Utils.NOTIF_PRESENTADA,msg,db);
						return -1;
					}
				}
				else 
				{
					this.obs += "Acta o registro padre no válida." + ENTER;
					return -1;
				}
				#endregion No Existe expediente
			}
			#endregion TVs
			return 0;
		}
		#endregion Procesar TV

		#region Procesar REN
		/// <summary>
		/// Procesar una REN a partir de un detalle del boletin
		/// </summary>
		/// <param name="bdet">BoletinDet</param>
		/// <param name="propID">Id del propietario. Setear -1 para obviar propietario</param>
		public int procesarREN(Berke.DG.DBTab.BoletinDet bdet, int propietarioID)
		{
			Berke.DG.DBTab.Marca mar					= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expe				= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.CAgenteLocal agenteL			= new Berke.DG.DBTab.CAgenteLocal( db );
			Berke.DG.DBTab.PropietarioXMarca propXmarca = new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.Marca marcaPadre				= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.Propietario prop				= new Berke.DG.DBTab.Propietario(db);

			string bolTramite = bdet.Dat.Tramite.AsString;
			string err_base = "Nro. inc. " + bolTramite + " "+ bdet.Dat.ExpNro.AsString + "/"+ bdet.Dat.ExpAnio.AsString + " ("+bol.Dat.Denominacion.AsString +").";

			#region Recuperar Propietario
			if (propietarioID < 0)
			{
				prop = null;
			}
			else 
			{
				prop = propServ.getPropietario(propietarioID);							
			}
			#endregion Recuperar Propietario

			#region RENOVACION
			//expe = expeServ.getExpediente(bdet.Dat.ExpNro.AsInt, bdet.Dat.ExpAnio.AsInt);
			expe = expeServ.getExpediente(bdet.Dat.ExpNro.AsInt, bdet.Dat.ExpAnio.AsInt, (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION);
			if (expe.RowCount > 0) 
			{
				//existe el expediente y lo enlazo
				bdet.Edit();
				bdet.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
				bdet.Dat.Enlazado.Value = true;
				bdet.PostEdit();

				#region Asociar propietario
				if (prop!= null)
				{
					marServ.setMarcaID(expe.Dat.MarcaID.AsInt);
					marServ.deletePropietarios();
					marServ.addPropietario(prop.Dat.ID.AsInt);
					marServ.updateDatosPropietario(prop);
					expeServ.delCampoPropietarioActual(expe.Dat.ID.AsInt);
					expeServ.addCampoPropietarioActual(prop, expe.Dat.ID.AsInt);
					
					expeServ.setExpeID(expe.Dat.ID.AsInt);
					expeServ.delPropietarios();
					// En este caso se reeemplazan los datos que tenia anteriormente
					// no se debe a ningún tramite particular.
					Berke.DG.DBTab.Poder poderAnt = new Berke.DG.DBTab.Poder(db);
					poderAnt = expeServ.getPoder();
					if (poderAnt.RowCount>0)
					{
						// Guarda la información de poder anterior en el 
						// expediente padre
						expeServ.addCampoPoderAnterior(poderAnt,expe.Dat.ID.AsInt);
						expeServ.delPoder();
					}
					expeServ.addPropietario(prop.Dat.ID.AsInt,true);
				}
				#endregion Asociar propietario
			} 
			else 
			{
				#region No Existe expediente
				renServ.setBoletin(bdet);
				expePadre = renServ.getExpedientePadre();

				if (expePadre == null)
				{
					this.obs += err_base + ENTER;
					return -1;
				}									

				if (expePadre.RowCount > 0) 
				{
					#region Existe el acta Padre
					agenteL = agServ.getAgenteLocal(bdet.Dat.AgenteLocal.AsString);
					
					if (agenteL.RowCount == 0)
					{
						agenteL = agServ.crearAgenteLocal(bol.Dat.AgenteLocal.AsString);
					}

					renServ.setAgenteLocal(agenteL);

					//existe el expediente padre
					int expePadreID = expePadre.Dat.ID.AsInt;
					int marPadreID  = expePadre.Dat.MarcaID.AsInt;
										
					//verificar q no sea nuestra la marca
					//if ( (agenteL.RowCount == 0) | (!agenteL.Dat.Nuestro.AsBoolean) ) 
					if (!agenteL.Dat.Nuestro.AsBoolean) 
					{
						int expeID = renServ.guardarExpediente(expePadreID);
						int regrenID = renServ.guardarRegRen();
						// Recuperar marca Padre
						marServ.setMarcaID(marPadreID);
						marcaPadre     = marServ.getMarca();
						string propIDs = marServ.getListaPropietariosID();

						expeServ.addCampoPropietarioAnteriorNombre(marcaPadre.Dat.Propietario.AsString,expeID);
						expeServ.addCampoPropietarioAnteriorPais(marcaPadre.Dat.ProPais.AsString, expeID);
						expeServ.addCampoPropietarioAnteriorDireccion(marcaPadre.Dat.ProDir.AsString, expeID);
						expeServ.addCampoPropietarioAnteriorID(propIDs,expeID);	

						int marcaID = renServ.guardarMarca(marcaPadre,expePadre);

						#region Renovado x Otro
											
						if ( (marcaPadre.Dat.Nuestra.AsBoolean) ||
							(marcaPadre.Dat.Vigilada.AsBoolean) ) 
						{
							expeServ.addInstruccionRenxOtro(marPadreID,
								expePadreID,
								"Bol:" + bdet.Dat.BolNro.AsString + 
								"/" + bdet.Dat.BolAnio.AsString
								);
							expeServ.updateClienteIDRXO(expeID, marcaPadre.Dat.ClienteID.AsInt);
							marServ.updateClienteIDRXO(marcaID, marcaPadre.Dat.ClienteID.AsInt);
						}
						#endregion Renovado x Otro

						

						#region Actualizar Expediente
						renServ.asociarExpeMarcaRegistro(marcaID,regrenID);
						// Para evitar recargar el expediente, se hace
						// de otra forma.
						//expeServ.setExpeID(expeID);																						
						//expeServ.asociarMarcaRegistro(marcaID,regrenID);	
						#endregion Actualizar Expediente

						#region Asociar propietario
						// Si se proporciono el propietario, se asocia a la marca
						if (marcaPadre.Dat.Vigilada.AsBoolean && (propietarioID>=0))
						{
							marServ.setMarcaID(marcaID);
							marServ.deletePropietarios();
							marServ.addPropietario(propietarioID);							
							expeServ.addCampoPropietarioActual(prop, expeID);
							expeServ.setExpeID(expeID);
							expeServ.delPropietarios();
							expeServ.addPropietario(propietarioID,true);
														  
						} 
						else if (!marcaPadre.Dat.Vigilada.AsBoolean) 
						{
							marServ.setMarcaID(marcaID);
							marServ.deletePropietarios();
						}
						else if ( marcaPadre.Dat.Vigilada.AsBoolean && (propietarioID<0))
						{
							this.obs+= err_base + "El trámite afecta a marcas propias. Verifique Trámites de Terceros." + ENTER; 
							return -1;
						}
						// Se guarda la info básica cuando no se cuenta con el dato
						// del propietario
						if (propietarioID < 0)
						{
							expeServ.addCampoPropietarioActualNombre(bdet.Dat.Propietario.AsString,expeID);
							expeServ.addCampoPropietarioActualPais(bdet.Dat.Pais.AsString,expeID);
						}
						#endregion Asociar propietario

						int expeSitID = expeServ.addSituacion(RenovacionService.SIT_TRAMITE,bdet.Dat.SolicitudFecha.AsDateTime,1,expeID);																			
											
						//#region Actualizar Expediente
						//expeServ.setExpeID(expeID);																						
						//expeServ.asociarMarcaRegistro(expeID,marcaID,regrenID);	
						//#endregion Actualizar Expediente
											
						#region Actualizar Boletin
						bdet.Edit();
						bdet.Dat.ExpedienteID.Value	= expeID;
						bdet.Dat.Incorporado.Value	= true;
						bdet.Dat.Enlazado.Value		= true;
						bdet.PostEdit();
						#endregion Actualizar Boletin
					}
					else  // Es nuestra pero no existe en nuestro sistema!!
					{
						string msg = err_base;
						
						if (bdet.Dat.RefAnio.AsString != "")
						{
							msg = msg + " Acta/Año Referencia: " + bdet.Dat.RefNro.AsString + "/" + bdet.Dat.RefAnio.AsString;
						}
						else if (bdet.Dat.RefRegNro.AsString != "")
						{
							msg = msg + " Registro/Año de Referencia: " + bdet.Dat.RefRegNro.AsString + "/" + bdet.Dat.RefRegAnio.AsString;
						}

						msg = msg + " Clase: " + bdet.Dat.Clase.AsString + " Tipo de Marca: " + bdet.Dat.MarcaTipo.AsString + ". Posible situación H.I para trámite nuestro." + ENTER;

						this.obs+= msg;
						msg = "Hola,"+ Utils.ENTER + "El proceso de Boletín informa lo siguiente:"+ Utils.ENTER+ msg;
						Utils.enviarNotificacion(Utils.NOTIF_PRESENTADA,msg,db);
						return -1;
					}
					#endregion Existe el acta Padre											
				}
				else 
				{
					this.obs += err_base + "Acta o registro padre no válida." + ENTER;
					return -1;
				}
				#endregion No Existe expediente
			}
			#endregion RENOVACION
			return 0;
		}
		#endregion Procesar REN

		#region Lista de trámites a verificar
		private ArrayList getListaVerificacion()
		{
			#region Lista de tramites que deben verificarse
			ArrayList lst = new ArrayList();
			lst.Add("UG");
			lst.Add("CO");
			lst.Add("OP");
			lst.Add("CPP");
			lst.Add("MAN"); 
			lst.Add("PAN"); 
			lst.Add("REN");
			lst.Add("RN");			
			lst.Add("OJ");
			lst.Add("MO");
			lst.Add("UM");
			lst.Add("DS");
			lst.Add("IR");
			lst.Add("MN");
			lst.Add("US");
			lst.Add("MA");
			lst.Add("CL");
			lst.Add("MX");
			lst.Add("TS");
			lst.Add("AU");
			lst.Add("CP");
			lst.Add("JP");
			lst.Add("OL");
			lst.Add("LA");
			lst.Add("CV");
			lst.Add("IO");
			lst.Add("RG");
			lst.Add("DM");
			lst.Add("AP");
			lst.Add("UF");
			lst.Add("DE");
			lst.Add("TRS");
			lst.Add("CD");
			lst.Add("DO");
			lst.Add("OPM");
			lst.Add("AN");
			lst.Add("UGP");
			lst.Add("FR");
			lst.Add("OM");
			#endregion Lista de tramites que deben verificarse
			return lst;
		}
		#endregion Lista de trámites a verificar

		#region BorrarExpediente
		public bool borrarExpediente(Berke.DG.DBTab.BoletinDet bolDet)
		{
			#region Verificar tipo de trámite
			if (!tvServ.isTramiteVario(bolDet.Dat.Tramite.AsString) &&
				!regServ.isRegistro(bolDet.Dat.Tramite.AsString) &&
				!renServ.isRenovacion(bolDet.Dat.Tramite.AsString)) 
			{
				//Acta no corresponde a REG, REN o TV
				throw new Exceptions.BolImportException("Acta especificada "+bolDet.Dat.Tramite.AsString+ " "+
														bolDet.Dat.ExpNro.AsString+"/"+bolDet.Dat.ExpAnio.AsString+
                                                        " no corresponde a Registro, renovación o trámites varios");
			}
			#endregion

			#region Declaración de objetos locales
			bool ok = true;
			string msgEnganchadoVigilancia = "";
			string prop_anterior_nombre = "";
			string prop_anterior_pais = "";
			string prop_anterior_dir = "";
			string prop_anterior_id = "";
			string [] aPropietarios = null;

			Berke.DG.DBTab.Marca mar						= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Marca marPadre					= new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.MarcaRegRen marRR				= new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.Expediente expe					= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expePadre				= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Expediente expeHijos				= new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.ExpedienteCampo expeC			= new Berke.DG.DBTab.ExpedienteCampo( db );
			Berke.DG.DBTab.Expediente_Situacion expeS		= new Berke.DG.DBTab.Expediente_Situacion( db );
			Berke.DG.DBTab.CAgenteLocal agenteL				= new Berke.DG.DBTab.CAgenteLocal( db );
			Berke.DG.DBTab.Expediente_Instruccion expeInst	= new Berke.DG.DBTab.Expediente_Instruccion( db);
			Berke.DG.DBTab.PropietarioXMarca propXmarca		= new Berke.DG.DBTab.PropietarioXMarca( db );
			Berke.DG.DBTab.ExpedienteXPropietario expeXprop = new Berke.DG.DBTab.ExpedienteXPropietario( db );
			Berke.DG.DBTab.ExpedienteXPoder expeXpoder		= new Berke.DG.DBTab.ExpedienteXPoder( db );
			Berke.DG.DBTab.AvisoOpoDet avidet				= new Berke.DG.DBTab.AvisoOpoDet(db);
			Berke.DG.DBTab.AvisoInstruccion avisoInstr		= new Berke.DG.DBTab.AvisoInstruccion(db);
			#endregion Declaración de objetos locales
			
			try 
			{
				db.IniciarTransaccion();

				#region RecuperarDatos

				#region Recuperar expediente
				if (TVService.isTramiteVario(bolDet.Dat.Tramite.AsString,db))
				{
					tvServ.setTramite(bolDet.Dat.Tramite.AsString.Trim());
					expe = expeServ.getExpediente(bolDet.Dat.ExpNro.AsInt,bolDet.Dat.ExpAnio.AsInt, tvServ.getTipoTramite());
				} 
				else 
				{
					expeServ.setExpeID(bolDet.Dat.ExpedienteID.AsInt);
					expe = expeServ.getExpediente();
				}
				#endregion Recuperar expediente

				#region Realizar controles iniciales
				//Expediente no existe
				if (expe.RowCount == 0) 
				{
					throw new Exceptions.BolImportException("No se pudo recuperar el expediente");
				}
				//Expediente duplicado
				if (expe.RowCount > 1) 
				{
					throw new Exceptions.BolImportException("El expediente se encuentra duplicado");
				}
				//Boletin no tiene referencia a expediente generado
				if (bolDet.Dat.ExpedienteID.AsInt == 0) /*| (! bol.Dat.Incorporado.AsBoolean)*/
				{
					throw new Exceptions.BolImportException("El tramite "+ bolDet.Dat.Tramite.AsString + " "+
						bolDet.Dat.ExpNro.AsString + "/" + bolDet.Dat.ExpAnio.AsString +
						" no ha generado ningún expediente");
				}
				if (bolDet.Dat.AgenteLocal.AsString == "") 
				{
					throw new Exceptions.BolImportException("No pude determinarse el Agente correspondiente al tramite " +
						bolDet.Dat.Tramite.AsString + " " + bolDet.Dat.ExpNro.AsString + "/" + bolDet.Dat.ExpAnio.AsString +
						" por lo cual el expediente asociado no podrá ser eliminado.");					
				}

				#endregion Realizar controles iniciales

				marServ.setMarcaID(expe.Dat.MarcaID.AsInt);

				expeC		= expeServ.getCampos(expe.Dat.ID.AsInt);
				expeS		= expeServ.getSituaciones(expe.Dat.ID.AsInt);
				expeInst	= expeServ.getInstrucciones(expe.Dat.ID.AsInt);
				mar			= marServ.getMarca();
				marRR		= regrenServ.getRegRenByExpe(expe.Dat.ID.AsInt);
				propXmarca	= marServ.getPropietariosID();
				agenteL     = agServ.getAgenteLocal(bolDet.Dat.AgenteLocal.AsString);

				#region Eliminamos expedienteCampo
				for (expeC.GoTop(); ! expeC.EOF; expeC.Skip()) 
				{
					if (expeC.Dat.Campo.AsString == GlobalConst.PROP_ANTERIOR_NOMBRE) 
					{
						prop_anterior_nombre = expeC.Dat.Valor.AsString;
					}
					if (expeC.Dat.Campo.AsString == GlobalConst.PROP_ANTERIOR_PAIS) 
					{
						prop_anterior_pais = expeC.Dat.Valor.AsString;
					}
					if (expeC.Dat.Campo.AsString == GlobalConst.PROP_ANTERIOR_DIR) 
					{
						prop_anterior_dir = expeC.Dat.Valor.AsString;
					}
					if (expeC.Dat.Campo.AsString == GlobalConst.PROP_ANTERIOR_ID) 
					{
						prop_anterior_id = expeC.Dat.Valor.AsString;
					}
					expeC.Adapter.DeleteRow();
				}
				#endregion Eliminamos expedienteCampo

				#endregion RecuperarDatos

				#region Controles sobre datos recuperados	

				// Controlar datos de expediente_campo
				if ( renServ.isRenovacion(bolDet.Dat.Tramite.AsString) ||
					tvServ.isTramiteVario(bolDet.Dat.Tramite.AsString) )
				{					
					//No se cuentan con datos del propietario anterior para restaurar la marca
					if ( (prop_anterior_nombre == "") |
						(prop_anterior_pais == "") ) 
					{
						throw new Exceptions.BolImportException("No se puede eliminar el expediente porque " +
							"no se cuenta con información del propietario anterior " +
							"para restaurar los datos de la marca");
					}
				}

				//Tramite nuestro
				if (agenteL.Dat.Nuestro.AsBoolean) 
				{
					throw new Exceptions.BolImportException("El expediente correspondiente al acta nro. " +
						bolDet.Dat.ExpNro.AsString + "/" + bolDet.Dat.ExpAnio.AsString +
						" es nuestro y no podrá ser eliminado desde el boletin");
				}
				// No pueden eliminarse expedientes de registro o renovación, con trámites posteriores
				if ( (regServ.isRegistro(bolDet.Dat.Tramite.AsString) || renServ.isRenovacion(bolDet.Dat.Tramite.AsString)) 
					&& expeServ.tieneTramitesHijos(bolDet.Dat.ExpedienteID.AsInt) )				     
				{
					throw new Exceptions.BolImportException("El expediente correspondiente al acta nro. " +
						bolDet.Dat.ExpNro.AsString + "/" + bolDet.Dat.ExpAnio.AsString +
						" posee trámites dependientes y no podrá ser eliminado");

				}
				// No pueden eliminarse expedientes de TV con trámites posteriores a la fecha
				if ( TVService.isTramiteVario(bolDet.Dat.Tramite.AsString,db) )
				{
					expeHijos = expeServ.getTramites(bolDet.Dat.ExpedienteID.AsInt);
					//Marca tiene tramites posteriores a la fecha del acta
					for (expeHijos.GoTop(); !expeHijos.EOF; expeHijos.Skip()) 
					{
						if (expeHijos.Dat.PresentacionFecha.AsDateTime > bolDet.Dat.SolicitudFecha.AsDateTime) 
						{
							throw new Exceptions.BolImportException("No se puede eliminar el expediente porque existen tramites " +
								"con fecha posterior a la fecha del acta " +
								bolDet.Dat.ExpNro.AsString + "/" + bolDet.Dat.ExpAnio.AsString +
								" para la misma marca");
						}
					}

				}
				#endregion Controles varios para eliminación
				
				/*[ggaleano 14/11/2007] Eliminamos los datos de la tabla
				 * expedienteXpropietario - bug#635*/
								
				expeXprop.ClearFilter();
				expeXprop.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				expeXprop.Adapter.ReadAll();

				for (expeXprop.GoTop(); !expeXprop.EOF; expeXprop.Skip())
				{
					expeXprop.Adapter.DeleteRow();
				}

				/*[ggaleano 14/11/2007] También eliminamos los datos de la tabla
				 * expedienteXpoder*/
				expeXpoder.ClearFilter();
				expeXpoder.Dat.ExpedienteID.Filter = expe.Dat.ID.AsInt;
				expeXpoder.Adapter.ReadAll();

				for (expeXpoder.GoTop(); !expeXpoder.EOF; expeXpoder.Skip())
				{
					expeXpoder.Adapter.DeleteRow();
				}


				// Se sustituyo el switch por el if, el cual tiene menor
				// complejidad ciclomátrica.
				if (regServ.isRegistro(bolDet.Dat.Tramite.AsString) )
				{					
					#region Registro
					expe.Edit();
					expe.Dat.MarcaRegRenID.SetNull();
					expe.Dat.MarcaID.SetNull();
					expe.PostEdit();
					expe.Adapter.UpdateRow();

					mar.Edit();
					mar.Dat.ExpedienteVigenteID.SetNull();
					mar.PostEdit();
					mar.Adapter.UpdateRow();

					#region Eliminar restricción de MarcaSolID de tabla AvisoOpoDet de Vigilancia
					avidet.ClearFilter();
					avidet.Dat.MarcaSolID.Filter = mar.Dat.ID.AsInt;
					avidet.Adapter.ReadAll();

					if(avidet.RowCount > 0)
					{
						string avisosCabID = "";
						for(avidet.GoTop(); !avidet.EOF; avidet.Skip())
						{
							#region Insertar instrucción de "NO	ENVIAR AVISO OPO"
							avisoInstr.NewRow();
							avisoInstr.Dat.AvisoOpoDetID.Value = avidet.Dat.ID.AsInt;
							avisoInstr.Dat.InstruccionTipoID.Value = (int) GlobalConst.InstruccionTipo.NO_ENVIAR_AVISOS_OPO;
							avisoInstr.Dat.Obs.Value = "Instrucción insertada automáticamente debido a eliminación de la marca solicitada: " +
								mar.Dat.Denominacion.AsString;	
							avisoInstr.Dat.FuncionarioRegID.Value = Utils.getCurrentFuncionarioID(db);
							avisoInstr.Dat.FecAlta.Value = System.DateTime.Now;
							avisoInstr.PostNewRow();
							avisoInstr.Adapter.InsertRow();
							#endregion Insertar instrucción de "NO	ENVIAR AVISO OPO"
							
							#region Asignar valor "null" a MarcaSolID en AvisoOpoDet
							avidet.Edit();
							avidet.Dat.MarcaSolID.SetNull();
							avidet.PostEdit();
							avidet.Adapter.UpdateRow();
							#endregion Asignar valor "null" a MarcaSolID en AvisoOpoDet

							if (avisosCabID != "")
							{
								avisosCabID += ", ";
							}
							avisosCabID += avidet.Dat.AvisoOpoCabID.AsString;
							
						}
						msgEnganchadoVigilancia += "A los Avisos de Vigilancia Nro: " + avisosCabID + " que tenían como marca solicitada a "  + 
												   bolDet.Dat.Denominacion.AsString + " (Acta: " + bolDet.Dat.ExpNro.AsString + "/" +
												   bolDet.Dat.ExpAnio.AsString + " - Clase: " + bolDet.Dat.Clase + ")" + 
												   " del Boletín " + bolDet.Dat.BolNro.AsString + "/" + bolDet.Dat.BolAnio.AsString + " se les ha asignado la instrucción NO ENVIAR AVISO OPO debido a que dicha marca ha sido eliminada desde " +
												   "la aplicación de Boletín porque fue procesada como trámite de Registro cuando debió haber sido procesada " + 
												   "como Renovación, ya no se generaran Avisos de Vigilancia para dichos trámites.";
					}
					#endregion Eliminar restricción de MarcaSolID de tabla AvisoOpoDet de Vigilancia

					marRR.Adapter.DeleteRow();
					for (expeS.GoTop(); ! expeS.EOF; expeS.Skip()) 
					{
						expeS.Adapter.DeleteRow();
					}
					expe.Adapter.DeleteRow();
					mar.Adapter.DeleteRow();
					#endregion Registro
				}
				else if (renServ.isRenovacion(bolDet.Dat.Tramite.AsString))
				{
					//renServ.Borrar(expe);
					#region Renovación
					expe.Edit();
					expe.Dat.MarcaRegRenID.SetNull();
					expe.Dat.MarcaID.SetNull();
					expe.PostEdit();
					expe.Adapter.UpdateRow();

					marRR.Adapter.DeleteRow();
						
					#region Marca
					int marcaID           = mar.Dat.ID.AsInt;
					int claseActualID     = mar.Dat.ClaseID.AsInt;
					int expedientePadreID = expe.Dat.ExpedienteID.AsInt;
					int expedienteID      = expe.Dat.ID.AsInt;

					//Recuperar marca anterior
					expeServ.setExpeID(expedientePadreID);
					expePadre = expeServ.getExpediente();
					marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);
					marPadre  = marServ.getMarca(); 						

					if (marPadre.Dat.ClaseID.AsInt == mar.Dat.ClaseID.AsInt) 
					{
						/* Si la marca fue renovada en la misma clase y misma edición,
							 * se debe restaurar el propietario actual y actualizar el 
							 * expediente vigente de la marca */
						#region Actualizar expediente vigente y propietario en marca actual
						mar.ClearFilter();
						mar.Adapter.ReadByID(marcaID);
						mar.Edit();
						mar.Dat.ExpedienteVigenteID.Value = expedientePadreID;
						mar.Dat.Propietario.Value = prop_anterior_nombre;
						mar.Dat.ProDir.Value = prop_anterior_dir;
						mar.Dat.ProPais.Value = prop_anterior_pais;
						mar.PostEdit();
						mar.Adapter.UpdateRow();
						#endregion Actualizar expediente vigente y propietario en marca actual

						#region Actualizar PropietarioXMarca

						for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
						{
							propXmarca.Adapter.DeleteRow();
						}

						aPropietarios = prop_anterior_id.Split( ((String)",").ToCharArray() );
						for (int i = 0; i < aPropietarios.Length; i++) 
						{
							marServ.addPropietario(Convert.ToInt32(aPropietarios[i]));
						}
						#endregion Actualizar PropietarioXMarca
					} 
					else 
					{
						/* Si la marca fue renovada en otra edición, deberá ser borrada
							 * y en el caso en que no exista otra marca que apunte a la marca
							 * padre, se deberá restaurar a la marca padre como vigente */
						/*
							expe.ClearFilter();
							expe.Dat.ExpedienteID.Filter = expedientePadreID;							
							expe.Adapter.ReadAll();
							if (expe.RowCount == 1) {
							*/
						#region Revivir a la marca padre
						//expe.ClearFilter();
						//expe.Adapter.ReadByID(expedientePadreID);
						//mar.ClearFilter();
						//mar.Adapter.ReadByID(expe.Dat.MarcaID.AsInt);
						marPadre.Edit();
						marPadre.Dat.Vigente			.Value = true;   //bit
						marPadre.PostEdit();
						marPadre.Adapter.UpdateRow();
						#endregion Revivir a la marca padre
						//}
						#region Recuperar y borrar marca actual
						//mar.ClearFilter();
						//mar.Adapter.ReadByID(marcaID);
						//Eliminar PropietarioXMarca, ocurre en el caso de que la marca
						//haya sido pasada a terceros.
						for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
						{
							propXmarca.Adapter.DeleteRow();
						}

						mar.Adapter.DeleteRow();
						#endregion Recuperar y borrar marca actual
					}
					#endregion Marca

					#region Borramos instrucciones y situaciones
					for (expeS.GoTop(); ! expeS.EOF; expeS.Skip()) 
					{
						expeS.Adapter.DeleteRow();
					}
					for (expeInst.GoTop(); ! expeInst.EOF; expeInst.Skip()) 
					{
						expeInst.Adapter.DeleteRow();
					}
					#endregion Borramos instrucciones y expedienteCampo

					// Borramos el expediente
					expeServ.setExpeID(expedienteID);
					expe = expeServ.getExpediente();
					expe.Adapter.DeleteRow();
					#endregion Renovación

				}
				else if (tvServ.isTramiteVario(bolDet.Dat.Tramite.AsString) )
				{
					#region TVs
					for (expeS.GoTop(); ! expeS.EOF; expeS.Skip()) 
					{
						expeS.Adapter.DeleteRow();
					}

					//propXmarca.ClearFilter();
					//propXmarca.Dat.MarcaID.Filter = mar.Dat.ID.AsInt;
					//propXmarca.Adapter.ReadAll();

					for (propXmarca.GoTop(); !propXmarca.EOF; propXmarca.Skip()) 
					{
						propXmarca.Adapter.DeleteRow();
					}

					mar.Edit();
					mar.Dat.Propietario.Value	= prop_anterior_nombre;
					mar.Dat.ProPais.Value		= prop_anterior_pais;
					mar.Dat.ProDir.Value		= prop_anterior_dir;
					mar.PostEdit();
					mar.Adapter.UpdateRow();
						
					marServ.setMarcaID(mar.Dat.ID.AsInt);

					aPropietarios = prop_anterior_id.Split( ((String)",").ToCharArray() );
					for (int i = 0; i < aPropietarios.Length; i++) 
					{
						if (aPropietarios[i].Length>0)
						{
							marServ.addPropietario(Convert.ToInt32(aPropietarios[i]));
						}
					}
					expe.Adapter.DeleteRow();

					#endregion TVs
				}
				else 
				{
					// Esto nunca debería ocurrir
					throw new Exceptions.BolImportException("Se intenta borrar el expediente para un trámite no válido.");
				}

				#region Actualizar boletindet
				bolDet.Edit();
				bolDet.Dat.ExpedienteID.SetNull();
				bolDet.Dat.Incorporado.Value = false;
				bolDet.Dat.Enlazado.Value = false;
				bolDet.PostEdit();
				bolDet.Adapter.UpdateRow();
				#endregion Actualizar boletindet

				db.Commit();

				if (msgEnganchadoVigilancia != "")
				{
					Utils.enviarNotificacion(Utils.NOTIF_ELIM_SOLIICTADA, msgEnganchadoVigilancia, db);
				}
			} 
			catch (Exception ex) 
			{
				db.RollBack();
				throw ex;
			}
			return (ok);
		}
		#endregion BorrarExpediente

		#region Verificar sustituidas
		/// <summary>
		/// Verifica en el boletín actual, que trámites potencialemente
		/// podrían corresponder a marcas sustituidas de acuerdo a la
		/// información de AgLocal+Clase+TipoMarca
		/// </summary>
		/// <returns>Lista de detalles del boletín que posiblemente corresponden 
		/// a trámites sustituidos</returns>
		public Berke.DG.DBTab.BoletinDet obtenerPosiblesSustituidas()
		{			
			Berke.DG.ViewTab.vExpeService view;
			Berke.DG.DBTab.BoletinDet bdet = new Berke.DG.DBTab.BoletinDet(db);
			
			pBar.Maximum = bol.RowCount;
			agServ.getAgentesNuestros();

			stats.reset();

			for(bol.GoTop(); ! bol.EOF; bol.Skip())
			{
				pBar.PerformStep();
				// Las sustituidas se aplica unicamente a trámites de registro
				if ( (bol.RowState!= System.Data.DataRowState.Deleted) &&
					regServ.isRegistro(bol.Dat.Tramite.AsString))
				{
					view = expeServ.getSustituidas(bol.Dat.AgenteLocal.AsString,
						bol.Dat.Clase.AsString,
						bol.Dat.MarcaTipo.AsString);
					// Solo si aún no fue procesado,
					// presenta posible sustituida y 
					// el registro no existe en el sistema
					if ( (bol.Dat.ExpedienteID.AsString=="")&&
						(agServ.isAgenteTerceros(bol.Dat.AgenteLocal.AsString) ) &&
						(view.RowCount>0) && 
						(expeServ.getExpediente(bol.Dat.ExpNro.AsInt,bol.Dat.ExpAnio.AsInt, (int)GlobalConst.Marca_Tipo_Tramite.REGISTRO).RowCount==0) )
					{						
						bdet.NewRow();
						copyBoletinDet(bol,bdet);
						bdet.PostNewRow();						
						stats.nproc++;
						continue;
					}

				}
				stats.nskip++;
				
			}
			bdet.Table.Columns.Add("ExpeSustID");
			return bdet;
		}
		#endregion Obtener sustuidas

		#region Copy Boletin
		/// <summary>
		/// Copia un Row del origen al destino
		/// </summary>
		/// <param name="source">BoletinDet Origen</param>
		/// <param name="dst">BoletinDet destino</param>
		public static void copyBoletinDet(Berke.DG.DBTab.BoletinDet source, Berke.DG.DBTab.BoletinDet dst) 
		{
			dst.Dat.ID.Value			= source.Dat.ID.Value;
			dst.Dat.BoletinID.Value	= source.Dat.BoletinID.Value;	
			dst.Dat.SolicitudFecha.Value = source.Dat.SolicitudFecha.Value;	
			dst.Dat.ExpNro.Value = source.Dat.ExpNro.Value;	
			dst.Dat.ExpAnio.Value = source.Dat.ExpAnio.Value;	
			dst.Dat.Clase.Value = source.Dat.Clase.Value;	
			dst.Dat.MarcaTipo.Value = source.Dat.MarcaTipo.Value;	
			dst.Dat.Tramite.Value = source.Dat.Tramite.Value;	
			dst.Dat.Denominacion.Value = source.Dat.Denominacion.Value;	
			dst.Dat.Propietario.Value = source.Dat.Propietario.Value;	
			dst.Dat.Pais.Value = source.Dat.Pais.Value;	
			dst.Dat.AgenteLocal.Value = source.Dat.AgenteLocal.Value;	
			dst.Dat.RefNro.Value = source.Dat.RefNro.Value;	
			dst.Dat.RefAnio.Value = source.Dat.RefAnio.Value;	
			dst.Dat.RefRegNro.Value = source.Dat.RefRegNro.Value;	
			dst.Dat.RefRegAnio.Value = source.Dat.RefRegAnio.Value;	
			dst.Dat.Obs.Value = source.Dat.Obs.Value;	
			dst.Dat.Enlazado.Value = source.Dat.Enlazado.Value;	
			dst.Dat.Incorporado.Value = source.Dat.Incorporado.Value;	
			dst.Dat.ExpedienteID.Value = source.Dat.ExpedienteID.Value;	
			dst.Dat.BolAnio.Value = source.Dat.BolAnio.Value;	
			dst.Dat.BolNro.Value = source.Dat.BolNro.Value;	
			dst.Dat.Importado.Value = source.Dat.Importado.Value;	
			dst.Dat.Completo.Value = source.Dat.Completo.Value;	
		}
		#endregion Copy Boletin

		#region Procesar sustituidas
		/// <summary>
		/// Procesa las entradas de boletinDet, con un campo adicional:
		/// ExpeSustID. Actualiza el expediente indicado por ExpeSustID
		/// con el ExpNro y ExpAnio de BoletinDet.
		/// Además crea la situación presentada.
		/// </summary>
		/// <param name="bdet">Lista de trámites del boletin a procesar</param>
		public void procesarSustituidas(Berke.DG.DBTab.BoletinDet bdet)
		{
			pBar.Maximum = bdet.RowCount;
			Berke.DG.DBTab.Expediente expe;
			stats.reset();
			for (bdet.GoTop(); !bdet.EOF; bdet.Skip())
			{
				DataRow dr = bdet.Table.Rows[bdet.RowIndex];
				string expeSustID = dr["ExpeSustID"].ToString();
				if( expeSustID.Length>0)
				{
					expeServ.setExpeID(Convert.ToInt32(expeSustID));
					expe = expeServ.getExpediente();
					if (expe.Dat.ActaNro.IsNull)
					{
						// Add Situacion Presentada
						expeServ.addSituacion(RegistroService.SIT_TRAMITE,
							bdet.Dat.SolicitudFecha.AsDateTime,
							1, expe.Dat.ID.AsInt);
						#region Actualizar expediente
						expe.Edit();
						expe.Dat.ActaNro.Value = bdet.Dat.ExpNro.AsInt;
						expe.Dat.ActaAnio.Value = bdet.Dat.ExpAnio.AsInt;
						expe.Dat.PresentacionFecha.Value = bdet.Dat.SolicitudFecha.AsDateTime;
						expe.Dat.TramiteSitID.Value = RegistroService.SIT_TRAMITE;
						expe.PostEdit();
						expe.Adapter.UpdateRow();
						#endregion Actualizar expediente
						#region Actualizar boletindet
						bdet.Edit();
						bdet.Dat.ExpedienteID.Value = expe.Dat.ID.AsInt;
						bdet.Dat.Enlazado.Value = true;
						bdet.Dat.Completo.Value = true;
						// bdet.Dat.Sustituida.Value = true; -> quizás haga falta
						bdet.PostEdit();
						bdet.Adapter.UpdateRow();
						#endregion Actualizar boletindet
						stats.nproc++;
						string msg = "Cargar en DataFlex presentacion de marca sustituida:"+ Utils.ENTER +
							         " Denominacion: {0}"+ Utils.ENTER + 
						             " Acta : {1}/{2} "+ Utils.ENTER;
						msg = string.Format(msg, bdet.Dat.Denominacion.AsString, bdet.Dat.ExpNro.AsString, bdet.Dat.ExpAnio.AsString);							        
						Utils.enviarNotificacion(Utils.NOTIF_SUSTDATAFLEX,msg,db);
					}
					else 
					{
						this.obs+="Tramite "+bdet.Dat.Tramite.AsString + " "+ 
							bdet.Dat.ExpNro.AsString+ "/"+
							bdet.Dat.ExpAnio.AsString + " ya ha sido procesado previamente."+ENTER;
						stats.nskip++;
					}
				}
				else 
				{
					stats.nexcl++;
				}
				pBar.PerformStep();
			}
			
		}
		#endregion Procesar sustituidas

		#region Verificar Trámite de terceros
		/// <summary>
		/// Verifica en el boletín actual qué trámites de terceros afectan a 
		/// marcas nuestras. Estos trámites incluyen TV y Renovación
		/// </summary>
		/// <returns>Lista de detalles del boletín que pertenecen a terceros y que afectan a marcas nuestras</returns>
		public Berke.DG.DBTab.BoletinDet obtenerTrAfectanPropias()
		{						
			Berke.DG.DBTab.Expediente expePadre;
			Berke.DG.DBTab.Expediente expe;
			Berke.DG.DBTab.Marca mar;
			Berke.DG.DBTab.BoletinDet bdet = new Berke.DG.DBTab.BoletinDet(db);
			bdet.Table.Columns.Add(PROP_ID);
			bdet.Table.Columns.Add("PropDireccion");
			// Campos para el propietario anterior
			bdet.Table.Columns.Add(OLDPROP_NOMBRE);
			bdet.Table.Columns.Add(OLDPROP_DIR);
			bdet.Table.Columns.Add(OLDPROP_PAIS);
			// Campos para el nuevo Propietario
			bdet.Table.Columns.Add(NEWPROP_NOMBRE);
			bdet.Table.Columns.Add(NEWPROP_DIR);
			bdet.Table.Columns.Add(NEWPROP_PAIS);

			pBar.Maximum = bol.RowCount;
			agServ.getAgentesNuestros();

			stats.reset();

			for(bol.GoTop(); ! bol.EOF; bol.Skip())
			{
				pBar.PerformStep();
				string tramite = bol.Dat.Tramite.AsString;

				if ( (bol.RowState!= System.Data.DataRowState.Deleted) &&
					 (renServ.isRenovacion(tramite) ||
					  tvServ.isTramiteVario(tramite)) &&
					 agServ.isAgenteTerceros(bol.Dat.AgenteLocal.AsString)
					)
				{
					// Obtenemos el expediente padre
					expePadre = expeServ.getExpedientePadre(bol);
					// Obtenermos la marca asociada al expediente padre
					if (expePadre == null || expePadre.RowCount==0)
					{
						this.obs = "El trámite "+ tramite + " no posee trámite de Registro o Renovacion padre"+ ENTER;
						continue;
					}
					marServ.setMarcaID(expePadre.Dat.MarcaID.AsInt);
					mar  = marServ.getMarca();

					// Marca padre Vigilada
					// no posee tramites posteriores			

					if ( mar.Dat.Vigilada.AsBoolean  && 
							(!tvServ.isLicencia(tramite) && tvServ.isTramiteVario(tramite) && !tvServ.tieneTVPosterior(bdet) ||
							renServ.isRenovacion(tramite)  )
						)
					{			
						#region Verificar si ya tiene propietarios
						// Verificamos si el tramite en cuestion existe
						// y si tiene ya propietarios asociados. En este
						// caso no se incluye y se continua verificando
						int tramiteID;
						if (renServ.isRenovacion(tramite))
						{
							tramiteID = (int)GlobalConst.Marca_Tipo_Tramite.RENOVACION;
						}
						else
						{
							tvServ.setTramite(tramite);
							tramiteID = tvServ.getTipoTramite();
						}

						expe = expeServ.getExpediente(bol.Dat.ExpNro.AsInt,bol.Dat.ExpAnio.AsInt, tramiteID);
						expeServ.setExpediente(expe);
						if ( (expe.RowCount>0) && expeServ.getPropietariosID().RowCount>0)
						{
							continue;
						}
						#endregion Verificar si ya tiene propietarios
			
						bdet.NewRow();
						copyBoletinDet(bol,bdet);
						bdet.PostNewRow();	

						DataRow dr = bdet.Table.Rows[bdet.RowIndex];						
						dr[OLDPROP_DIR]     = mar.Dat.ProDir.AsString;
						dr[OLDPROP_NOMBRE]  = mar.Dat.Propietario.AsString;
						dr[OLDPROP_PAIS]    = mar.Dat.ProPais.AsString;
						dr["ExpedienteID"]  = expePadre.Dat.ID.AsInt;
						
						if (tvServ.isCambioNombre(tramite))
						{
							dr["PropDireccion"] = mar.Dat.ProDir.AsString;
						}
						stats.nproc++;
						continue;
					}
				}
				stats.nskip++;
			}

			return bdet;
		}
		#endregion Obtener sustuidas

		#region Check dependencias Boletindet 
		/// <summary>
		/// Obtiene la lista (si existe) de los trámites de los cuales
		/// depende el boletin, y que aún no han sido procesados.
		/// </summary>
		/// <param name="bdet">BoletinDet</param>
		/// <returns>Retorna "" si la lista es vacia, de otra forma
		/// retorna la lista de actanro/actaanio entre comas.</returns>
		public string haveBoletinDetHnos(Berke.DG.DBTab.BoletinDet bdet)
		{
			string lst_actas="";
			Berke.DG.DBTab.BoletinDet bdethnos = new Berke.DG.DBTab.BoletinDet(db);
			//Berke.DG.ViewTab.vBoletinDep bdethnos = new Berke.DG.ViewTab.vBoletinDep(db);
			//bdethnos.Adapter.AddParam("fechasolicitud", bdet.Dat.SolicitudFecha.AsString);
			//bdethnos.Adapter.AddParam("actanro",bdet.Dat.ExpNro.AsString);
			
			//MessageBox.Show("SetDefaultWhere");
			// Buscamos uno que no se haya procesado y 
			/*
			bdethnos.Adapter.SetDefaultWhere(" ExpedienteID is null AND " +
										 " Solicitudfecha <= '"+bdet.Dat.SolicitudFecha.AsString+"' AND "+
				                         " ExpNro < '"+bdet.Dat.ExpNro.AsString+"'");
			 */

			bdethnos.Dat.ExpAnio.Order = 1;
			bdethnos.Dat.ExpNro.Order  = 2;

			// Buscar alguno que tenga el mismo padre 
			//(registronro)			
			if(!bdet.Dat.RefRegNro.IsNull && bdet.Dat.RefRegNro.AsInt>0)
			{
				bdethnos.Dat.RefRegNro.Filter = bdet.Dat.RefRegNro.AsInt;
				bdethnos.Adapter.ReadAll();
					
				for(bdethnos.GoTop(); ! bdethnos.EOF && bdethnos.Dat.SolicitudFecha.AsDateTime <= bdet.Dat.SolicitudFecha.AsDateTime; bdethnos.Skip())
				{				
					if (bdethnos.Dat.ExpedienteID.IsNull && (bdethnos.Dat.ExpNro.AsInt < bdet.Dat.ExpNro.AsInt) )
					{
					
						if (lst_actas=="")
						{
							lst_actas+= "["+bdethnos.Dat.Tramite.AsString+"]"+bdethnos.Dat.ExpNro.AsString + "/" + bdethnos.Dat.ExpAnio.AsString;
						}
						else
						{
							lst_actas+= ", " + "["+bdethnos.Dat.Tramite.AsString+"]"+bdethnos.Dat.ExpNro.AsString + "/" + bdethnos.Dat.ExpAnio.AsString;
						}
					}
					
				}
				
				if (bdethnos.RowCount>0) 
				{
					return lst_actas;
				}
			}
			//(refnro, refanio)
			if (!bdet.Dat.RefNro.IsNull && (bdet.Dat.RefNro.AsInt>0) &&
				!bdet.Dat.RefAnio.IsNull && (bdet.Dat.RefAnio.AsInt>0) )
			{
											//MessageBox.Show("Leer hemanos por acta.");
				bdethnos.ClearFilter();
				bdethnos.Dat.RefNro.Filter  = bdet.Dat.RefNro.AsInt;
				bdethnos.Dat.RefAnio.Filter = bdet.Dat.RefAnio.AsInt;
				bdethnos.Adapter.ReadAll();
							//MessageBox.Show("Hermanos leidos");
				for(bdethnos.GoTop(); ! bdethnos.EOF; bdethnos.Skip())
				{					
					if (bdethnos.Dat.ExpedienteID.IsNull && (bdethnos.Dat.ExpNro.AsInt < bdet.Dat.ExpNro.AsInt) )
					{					
						if (lst_actas=="")
						{
							lst_actas+= "["+bdethnos.Dat.Tramite.AsString+"]"+bdethnos.Dat.ExpNro.AsString + "/" + bdethnos.Dat.ExpAnio.AsString;
						}
						else
						{
							lst_actas+= ", " + "["+bdethnos.Dat.Tramite.AsString+"]"+bdethnos.Dat.ExpNro.AsString + "/" + bdethnos.Dat.ExpAnio.AsString;
						}
					}
				}
			}
										//MessageBox.Show("Restituir default where");
			return lst_actas;			
		}
		#endregion Check dependencias Boletindet

		#region Propietarios semejantes
		/// <summary>
		/// Obtiene la lista de propietarios que se asemejan al indicado
		/// por el trámite en dr
		/// </summary>
		/// <param name="dr">DataRow con estructura utilizada en Propietarios</param>
		/// <returns></returns>
		public Berke.DG.DBTab.Propietario getPropSemejantes(DataRow dr)
		{
			Berke.DG.DBTab.Propietario propSemej = new Berke.DG.DBTab.Propietario(db);
			Berke.DG.DBTab.Propietario prop;
			int minLength = 4;

			PropietarioSearch ps = new PropietarioSearch(db);

			if ((dr["Propietario"].ToString().Length>0) && 
				(dr["Propietario"].ToString().Length < minLength))
			{
				return propSemej;
			}
			else if ((dr["PropDireccion"].ToString().Length>0) && dr["PropDireccion"].ToString().Length<minLength) 
			{				
				return propSemej;
			}

			if (dr["ExpedienteID"] != System.DBNull.Value)
			{
				/*
				int expeID = Convert.ToInt32(dr["ExpedienteID"].ToString());

				expeServ.setExpeID(expeID);
				expe = expeServ.getExpediente();
				marServ.setMarcaID(expe.Dat.ID.AsInt);
				pxm = marServ.getPropietariosID();
				*/
				//for(pxm.GoTop(); !pxm.EOF; pxm.Skip())
				//{
					string tramite  = dr["Tramite"].ToString();
					int tipotramite = 0;
					//ps.setPropietarioAnterior(pxm.Dat.PropietarioID.AsInt);
					if (TVService.isTramiteVario(tramite,db))
					{
						tvServ.setTramite(tramite);
						tipotramite = tvServ.getTipoTramite();
						prop = ps.search(dr["Propietario"].ToString(),dr["PropDireccion"].ToString());	
					}
					else 
					{
						tipotramite = RenovacionService.TIPO_TRAMITE;
						// Esto esta así porque posiblemente necesite enviar otros
						// parámetros
						prop = ps.search(dr["Propietario"].ToString(), dr["PropDireccion"].ToString());	
					}
													
					for (prop.GoTop(); !prop.EOF; prop.Skip())
					{
						propSemej.NewRow();
						ps.copy(prop, propSemej);
						propSemej.PostNewRow();
					}
				//}
			}
			return propSemej;			

		
		}
		#endregion Propietarios semejantes

		#region getPais
		public Berke.DG.DBTab.CPais getPais(int paisID)
		{
			Berke.DG.DBTab.CPais pais = new Berke.DG.DBTab.CPais(db);
			pais.Adapter.ReadByID(paisID);
			return pais;
		}
		#endregion getPais

		#region Crear nuevo Propietario
		/// <summary>
		/// Crea un nuevo propietario con los datos básicos del boletín
		/// </summary>
		/// <param name="str_nombre">Nombre del propietario</param>
		/// <param name="str_dir"> Dirección del propietario</param>
		/// <param name="str_pais">Abrev. del pais del Propietario</param>
		/// <returns>ID. del Nuevo propietario</returns>
		public int crearNuevoProp(string str_nombre, string str_dir, string str_pais)
		{
			Berke.DG.DBTab.CPais pais       = new Berke.DG.DBTab.CPais(db);
			pais.Dat.paisalfa.Filter = ObjConvert.GetFilter(str_pais);
			pais.Adapter.ReadAll();
			if (pais.RowCount==0)
			{
				throw new Exceptions.BolImportException("No existe el pais " + str_pais);
			}
			Berke.DG.DBTab.Propietario prop = new Berke.DG.DBTab.Propietario(db);
			prop.NewRow();
			prop.Dat.Nombre.Value		= str_nombre;
			prop.Dat.Direccion.Value	= str_dir;
			prop.Dat.PaisID.Value		= pais.Dat.idpais.AsInt;
			prop.Dat.Obs.Value			= BoletinService.DEF_NEWPROP_OBS;
			prop.Dat.IdiomaID.Value     = (int) Berke.Libs.Base.GlobalConst.Idioma.ESPANOL;
			prop.PostNewRow();
			int propID = prop.Adapter.InsertRow();
			return propID;
		}
		#endregion Crear nuevo Propietario

		#region Procesar trámites
		public void procesarTramites(DataTable dt)
		{
			Berke.DG.DBTab.BoletinDet bdet = new Berke.DG.DBTab.BoletinDet(db);
			pBar.Maximum = dt.Rows.Count;
			stats.reset();
			for(int i=0; i<dt.Rows.Count; i++)
			{
				try 
				{					
					pBar.PerformStep();
					DataRow dr = dt.Rows[i];
					if ( dr.RowState != System.Data.DataRowState.Deleted) 
					{						
						string str_id     = dr[PROP_ID].ToString();
						string str_nombre = dr[NEWPROP_NOMBRE].ToString();
						string str_dir    = dr[NEWPROP_DIR].ToString();
						string str_pais   = dr[NEWPROP_PAIS].ToString();
						string tramite    = dr["Tramite"].ToString();
						int propID;
						if ( (str_id != "")||(str_nombre != "") )
						{
							db.IniciarTransaccion();
							#region Procesar
							string err_base = "Trámite "+ tramite +" "+ 
								dr["ExpNro"].ToString()+ "/"+ dr["ExpAnio"].ToString()+
								" no procesado. ";

							bdet.Dat.ID.Filter = dr["ID"].ToString();
							bdet.Adapter.ReadAll();
							if (bdet.RowCount==0)
							{
								this.obs+= err_base + "Tramite no existe en Boletín." + ENTER;
								db.RollBack();
								stats.nskip++;
								continue;
							}
							// Verificar dependencias
							string lst_dep = this.haveBoletinDetHnos(bdet);
							if (lst_dep != "")
							{
								this.obs = err_base + "Trámites anteriores para el mismo registro padre "+
									"deben ser procesados: "+ lst_dep +"." + ENTER;								
								db.RollBack();
								stats.nskip++;
								continue;
							}
							// Crear el propietario si es nuevo ***
							if (str_id.Length==0)
							{
								propID = this.crearNuevoProp(str_nombre, str_dir, str_pais);
							}
							else
							{
								propID = Convert.ToInt32(str_id);
							}
							if (renServ.isRenovacion(tramite))
							{
								this.procesarREN(bdet, propID);
							}
							else if (TVService.isTramiteVario(tramite,db))
							{
								this.procesarTV(bdet, propID);
							}
							else 
							{
								throw new Exceptions.BolImportException("Solo pueden procesarse TV y REN");
							}
							bdet.Edit();
							bdet.Dat.Completo.Value  = true;
							bdet.PostEdit();
							bdet.Adapter.UpdateRow();
									
							// Borramos del la grilla los procesados
							dr.Delete();							
							#endregion Procesar
							db.Commit();
							stats.nproc++;
						}
						else 
						{
							// Excluidos por no realizar asociación.
							stats.nexcl++;
						}
					}
					else 
					{
						// Excluidos al eliminar
						stats.nexcl++;
					}
					
				}
				catch(Exception ex) 
				{
					dt.AcceptChanges();
					db.RollBack();
					this.obs+=ex.Message+ENTER;
					throw ex;
				}
				if (stats.nproc>0)
				{
					this.obs = "("+stats.nproc+") Procesados.";
					if (stats.nexcl>0) this.obs+="("+stats.nexcl+") Excluidos."+ENTER;
				}				
			}
			dt.AcceptChanges();			
		}
		#endregion Procesar trámites

		#region Limpiar obs
		public void clearObs()
		{
			this.obs = "";
		}
		#endregion Limpiar obs

		#region Obtener BoletinID
		private int getBoletinID(DateTime solicitudFecha, string carpNro)
		{
			int sem  = Utils.semanaDelAnho(solicitudFecha);
			int anho = solicitudFecha.Year;

			Berke.DG.DBTab.Boletin bolcab = new Berke.DG.DBTab.Boletin(db);
			bolcab.Dat.Nro.Filter  = carpNro;
			bolcab.Dat.Anio.Filter = anho;
			bolcab.Adapter.ReadAll();
			if (bolcab.RowCount>0)
			{
				return bolcab.Dat.ID.AsInt;
			}
			Berke.DG.DBTab.BoletinDet boldet = new Berke.DG.DBTab.BoletinDet(db);
			boldet.Dat.BolNro.Filter         = sem;
			boldet.Dat.BolAnio.Filter        = anho;
			boldet.Dat.SolicitudFecha.Filter = solicitudFecha;
			boldet.Adapter.ReadAll();
			if (boldet.RowCount>0)
			{
				return boldet.Dat.BoletinID.AsInt;
			}

			throw new Exceptions.BolImportException("No se especifico la carpeta, y no pudo deducirse.");
		}
		#endregion Obtener BoletinID

		#region Imprimir Grilla
		public void toDoc(string filename)
		{
			Berke.DG.DBTab.DocumentoPlantilla doc = new Berke.DG.DBTab.DocumentoPlantilla(db);
			doc.Dat.Clave.Filter = BoletinService.PLANTILLA_KEY;
			doc.Adapter.ReadAll();
			if (doc.RowCount==0)
			{
				MessageBox.Show("No se encuentra definida la plantilla de impresión");
				return;
			}
			string template = doc.Dat.PlantillaHTML.AsString;
			Berke.Libs.CodeGenerator cg = new CodeGenerator(template);
			Berke.Libs.CodeGenerator tblStatus = cg.ExtraerTabla("tblStatus");
			Berke.Libs.CodeGenerator tblBoletin = cg.ExtraerTabla("tblBoletin");
			Berke.Libs.CodeGenerator tblTh      = tblBoletin.ExtraerFila("tblTh");
			Berke.Libs.CodeGenerator tblTd		= tblBoletin.ExtraerFila("tblTd");

			string strPeriodo = "";
			DateTime fechaInf = DateTime.MaxValue;
			DateTime fechaSup = DateTime.MinValue;
			int nRows = 0;
			for(bol.GoTop(); ! bol.EOF; bol.Skip())
			{
				pBar.PerformStep();
				if (bol.RowState!= System.Data.DataRowState.Deleted) 
				{
					tblTd.copyTemplateToBuffer();
					tblTd.replaceField("bol.fecha", bol.Dat.SolicitudFecha.AsString);
					tblTd.replaceField("bol.expnro", bol.Dat.ExpNro.AsString);
					tblTd.replaceField("bol.expanio", bol.Dat.ExpAnio.AsString);
					tblTd.replaceField("bol.clase", bol.Dat.Clase.AsString);
					tblTd.replaceField("bol.denominacion", bol.Dat.Denominacion.AsString);
					tblTd.replaceField("bol.marcatipo", bol.Dat.MarcaTipo.AsString);
					tblTd.replaceField("bol.propietario", bol.Dat.Propietario.AsString);
					tblTd.replaceField("bol.pais", bol.Dat.Pais.AsString);
					tblTd.replaceField("bol.agentelocal", bol.Dat.AgenteLocal.AsString);
					tblTd.replaceField("bol.tramite", bol.Dat.Tramite.AsString);
					tblTd.replaceField("bol.nro", bol.Dat.BolNro.AsString);
					tblTd.replaceField("bol.anio", bol.Dat.BolAnio.AsString);
					tblTd.addBufferToText();

					#region Fechas para el periodo
					if (fechaInf > bol.Dat.SolicitudFecha.AsDateTime)
					{
						fechaInf = bol.Dat.SolicitudFecha.AsDateTime;
					}
					if (fechaSup < bol.Dat.SolicitudFecha.AsDateTime)
					{
						fechaSup = bol.Dat.SolicitudFecha.AsDateTime;
					}
					#endregion Fechas para el periodo

					nRows++;
				}
			}
			tblBoletin.copyTemplateToBuffer();
			tblBoletin.replaceLabel("tblTh", tblTh.Template);
			tblBoletin.replaceLabel("tblTd", tblTd.Texto);
			tblBoletin.addBufferToText();

			tblStatus.copyTemplateToBuffer();
			tblStatus.replaceField("periodo", fechaInf.ToString("dd/MM/yyyy") + " - " + fechaSup.ToString("dd/MM/yyyy"));
			tblStatus.replaceField("nregistros", nRows.ToString());
			tblStatus.addBufferToText();

			cg.copyTemplateToBuffer();
			cg.replaceField("today", System.DateTime.Now.ToLongDateString());
			cg.replaceLabel("tblStatus", tblStatus.Texto);
			cg.replaceLabel("tblBoletin", tblBoletin.Texto);
			cg.addBufferToText();

			#region Guardar documento en Archivo
			try 
			{
				Berke.Libs.Base.Helpers.Files.SaveStringToFile(cg.Texto, filename);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}
			#endregion		
			
			this.abrirDocumentoWord(filename,true);
		}
		#endregion Imprimir Grilla

		#region Abrir Documento Word
		private void abrirDocumentoWord( string filename, bool preliminar) 
		{
			WordLibs w;

			#region Seleccionar Impresora
			if ( activePrinter.Length == 0 ) 
			{
				activePrinter = Utils.seleccionarImpresora();
			}
			
			if ( activePrinter.Length == 0 ) 
			{
				MessageBox.Show("Se canceló la operación o no se escogió ninguna impresora");
				return;
			}
			#endregion

			#region Instanciar Word

			try
			{
				w = new WordLibs(activePrinter);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}
			#endregion
			
			#region Abrir documento

			w.agregarDocumento(filename);
			if ( preliminar ) 
			{
				w.imprimirPreliminar();
			} 
			else 
			{
				w.imprimirDocumento();
			}

			#endregion Abrir documento

			if(! preliminar) 
			{
				w.cerrarWord();
			}

		}
		#endregion Abrir Documento Word

	}


	#region ProcStats
	public class BoletinStats
	{
		public int nproc = 0;
		public int nskip = 0;
		public int nexcl = 0;
		public void reset()
		{
			this.nexcl = 0;
			this.nproc = 0;
			this.nskip = 0;
		}
		public override string ToString()
		{
			return "Procesados ("+nproc+"), No procesados ("+this.nskip+"), Excluídos por el usuario ("+nexcl+")";
		}
	}
	#endregion ProcStats
}
