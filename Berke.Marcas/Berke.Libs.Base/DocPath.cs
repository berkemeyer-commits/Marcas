using System;
using Berke.DG.DBTab;
using Berke.Libs.Base.Helpers;
using System.IO;
using System.Net;
using System.Web;

namespace Berke.Libs.Base
{
    /// <summary>
    /// Summary description for DocPath.
    /// </summary>
    /// 

    public class DocPath
    {

        public DocPath()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region digitalDocPath
        public static string digitalDocPath(int pAnio, int pNumero, string tipoTramite)
        {
            int tipoDocID = 0;
            switch (tipoTramite)
            {

                case "REN":
                case "RN":
                case "RE":
                case "REG":
                    tipoDocID = 5;
                    break;

                case "PA":
                case "PAN":
                    tipoDocID = 56;
                    break;


            }

            return (digitalDocPath(pAnio, pNumero, tipoDocID));
        }

        public static string digitalDocPath(int pAnio, int pNumero, int tipoDocID)
        {
            //string anchorTemplate = @"&nbsp;&nbsp;<a href=""{0}""><img border=0 alt='Ver Documento' src='../tools/imx/tif.gif'> </a>";		

            string anchorTemplate = @"&nbsp;&nbsp;<a href='{0}'><img border=0 alt='Ver Documento' src='../tools/imx/tif.gif'> </a>";

            Berke.Libs.Base.Helpers.AccesoDB db;
            db = new Berke.Libs.Base.Helpers.AccesoDB();
            db.DataBaseName = Berke.Libs.Base.Acceso.CurrentDB();
            db.ServerName = Berke.Libs.Base.Acceso.CurrentServer();

            string path = DocPath.getPath(pAnio, pNumero, tipoDocID, db);

            if (path.IndexOf(".pdf") > -1)
            {
                anchorTemplate = @"&nbsp;&nbsp;<a href='{0}'  target='_blank'><img border=0 alt='Ver Documento' src='../tools/imx/pdf.png'> </a>";
            }

            //db.CerrarConexion();

            if (path != "")
            {
                path = string.Format(anchorTemplate, path);
            }
            return path;

        }


        public static string getPath(int pAnio, int pNumero, int tipoDocID, Berke.Libs.Base.Helpers.AccesoDB db)
        {
            string fileTemplate = "";
            string numero = pNumero.ToString();

            //			1	Documento de Prioridad	
            //			2	Documento de Transferencia
            //			3	Contrato de Licencia	
            //			4	Correspondencia Orden	
            //			5	Hoja Descriptiva	
            //			6	Publicación	
            //			7	Cartas de Recaudo	
            //			8	Titulo de Marca		<---- Agregado
            //			9	Poder				<---- Agregado
            //			10  Cédulas (Litigios)  <---- Agregado mbaez

            Berke.DG.DBTab.DocumentoRuta DocumentoRuta;
            DocumentoRuta = new Berke.DG.DBTab.DocumentoRuta(db);
            DocumentoRuta.ClearFilter();
            DocumentoRuta.Dat.idDocumento.Filter = tipoDocID;
            DocumentoRuta.Adapter.ReadAll();
            fileTemplate = @DocumentoRuta.Dat.path.AsString;

            if (tipoDocID == Berke.Libs.Base.GlobalConst.DOCUMENTO_RUTA_PODERES && pAnio.ToString() == "0")
            {
                DocumentoRuta.ClearFilter();
                DocumentoRuta.Dat.idDocumento.Filter = Berke.Libs.Base.GlobalConst.DOCUMENTO_RUTA_PODERES_ANO_0;
                DocumentoRuta.Adapter.ReadAll();
                fileTemplate = @DocumentoRuta.Dat.path.AsString;
            }

            db.CerrarConexion();

            #region Deprecated
            /*switch( tipoDocID )
			{
				case 0 : //	Escrito Vario
					fileTemplate = @"\\trinity\Ofdig$\HojasDescriptivas\Varias\{0}\TIF\{1}.tif";
					break;
				case 4 : //	Correspondencia Orden
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 5 : //	Hoja Descriptiva
					fileTemplate = @"\\trinity\Ofdig$\HojasDescriptivas\Marcas\{0}\TIF\{1}.tif";
					break;

				case 56 : //Hoja Descriptiva Patentes
					fileTemplate = @"\\trinity\Ofdig$\HojasDescriptivas\Patentes\{0}\TIF\{1}.tif";
					break;

				case 66 : //	Publicación	
					DocumentoRuta.Dat.idDocumento.Filter = 66;
					DocumentoRuta.Adapter.ReadAll();
					fileTemplate = @DocumentoRuta.Dat.path.AsString;
					//fileTemplate = @"\\trinity\Ofdig$\Publicaciones\Publicacion\{0}\TIF\{1}.tif";
					break;
				case 88 : //	Titulo de Marca	
					fileTemplate = @"\\trinity\Ofdig$\Titulos\Titulo\{0}\TIF\{1}.tif";
					break;
				case 9 : //	Poder
					fileTemplate = @"\\trinity\Ofdig$\Poderes\Poder\{0}\TIF\{1}.tif";
					if ( pAnio.ToString()=="0" ) 
					{
						fileTemplate = @"\\trinity\Ofdig$\Poderes\Poder\TIF\{0}.tif";
					} 
					
					break;*/

            /*[ggaleano 31/05/2007] Opciones para Correspondencia, se ubica en este lugar
                 * para unificar las llamadas al método digitalDocPath ya que en todo el código del
                 * sistema existen varias versiones de código generalmente replicadas
                 * innecesariamente.*/

            /*case	1  : //		Marcas	
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
                    break;
                case 3  : //		Poder
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
                    break;
                case 6  : //		Litigios 
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosAdm\{0}\TIF\{1}.tif";
                    break;
                case 7  : //		Patentes
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Patentes\{0}\TIF\{1}.tif";
                    break;
                case 8  : //		Legal Division	
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosJud\{0}\TIF\{1}.tif";
                    break;
                case 10 : //		General	
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
                    break;
                case 14 : //		Contabilidad	
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Contabilidad\{0}\TIF\{1}.tif";
                    break;
                default :
                    fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
                    break;
            }*/

            #endregion Deprecated

            if (fileTemplate == "")
            {
                return "";
            }

            #region Llenar numero con ceros a la izquierda
            if (numero.Length < 5 && numero.Length > 0)
            {
                numero = ((string)"00000").Substring(0, 5 - numero.Length) + numero;
            }
            #endregion


            string arch = string.Format(fileTemplate, pAnio.ToString(), numero);

            /* [11-04-2007]
              * La carpeta Poderes no esta dividido por año */
            //if ( tipoDocID.ToString()=="9" && pAnio.ToString()=="0") 
            if (tipoDocID == Berke.Libs.Base.GlobalConst.DOCUMENTO_RUTA_PODERES && pAnio.ToString() == "0")
            {
                arch = string.Format(fileTemplate, numero);
            }

            System.IO.FileInfo inf = new System.IO.FileInfo(arch);
            if (!inf.Exists)
            {
                arch = arch.Replace(".tif", ".pdf");
                inf = new System.IO.FileInfo(arch);

                if (!inf.Exists)
                    return "";
            }
            
            return arch;

        }
        #endregion digitalDocPath

        #region Path Cedulas

        /// <summary>
        /// Obtiene el path a la cédulas
        /// </summary>
        public static string[] getPathCedulas(int actaanio, int actanro, Berke.Libs.Base.Helpers.AccesoDB db)
        {
            Berke.DG.DBTab.DocumentoRuta DocumentoRuta;
            DocumentoRuta = new Berke.DG.DBTab.DocumentoRuta(db);
            DocumentoRuta.ClearFilter();
            DocumentoRuta.Dat.idDocumento.Filter = (int)GlobalConst.DocumentoTipo.CEDULAS;
            DocumentoRuta.Adapter.ReadAll();
            string fileTemplate = @DocumentoRuta.Dat.path.AsString;
            string anchorTemplate = @"&nbsp;&nbsp;<a href=""{0}""><img border=0 alt='Ver Documento' src='../tools/imx/tif.gif'> </a>";

            fileTemplate = string.Format(fileTemplate, actaanio);

            #region Llenar numero con ceros a la izquierda
            string numero = actanro.ToString();
            if (numero.Length < 5 && numero.Length > 0)
            {
                numero = ((string)"00000").Substring(0, 5 - numero.Length) + numero;
            }
            #endregion

            DirectoryInfo di = new DirectoryInfo(fileTemplate);

            if (!di.Exists)
            {
                return null;
            }

            System.IO.FileInfo[] archivos = di.GetFiles(numero + "-*.tif");
            string[] path = new string[archivos.Length];
            for (int i = 0; i < archivos.Length; i++)
            {
                path[i] = string.Format(anchorTemplate, archivos[i].FullName);

            }
            return path;

        }
        #endregion Path Cedulas
    }
}
