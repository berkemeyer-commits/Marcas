using System;

namespace Berke.Marcas.WebUI {

	/// <summary>
	/// Const
	/// </summary>
	public class Const {

		// Trascendent paths
		public const string IMAGES_PATH			= @"~/Tools/imx/";

		// Menu file
		public const string MENU_FILE			= @"~/tools/menu/menu.xml";

		// Generic Pages
		public const string PAGE_LOGIN			= @"~/Home/Login.aspx";
		public const string PAGE_DEFAULT		= @"~/Home/Default.aspx";
		public const string PAGE_ERROR			= @"~/Generic/Message.aspx";
		public const string PAGE_ERROR_HTM		= @"~/Generic/Message.htm";

		// App pages
		public const string PAGE_ORDENTRABAJOCONSULTAR	= @"~/Home/OrdenTrabajoConsultar.aspx";
		public const string PAGE_ORDENTRABAJOMODIFICAR	= @"~/Home/OrdenTrabajoModificar.aspx";
		public const string PAGE_CAMBIOSITUACIONINGRESAR = @"~/Home/CambioSitIngresar.aspx";
		public const string PAGE_CAMBIOSITUACIONMODIFICAR = @"~/Home/CambioSitIngresar.aspx";
		public const string PAGE_ORDENTRABAJORENOVING = @"~/Home/Renovacion.aspx";
//		public const string PAGE_ORDENTRABAJORENOVING = @"~/Home/OrdenTrabajoRenovacion.aspx";
		public const string PAGE_ORDENTRABAJOTVCD = @"~/Home/OrdenTrabajoCDAME.aspx";
		public const string PAGE_ORDENTRABAJOTVTR = @"~/Home/OrdenTrabajoTRAME.aspx";
		public const string PAGE_ORDENTRABAJOTVFS = @"~/Home/OrdenTrabajoFSAME.aspx";
		public const string PAGE_ORDENTRABAJOTVLC = @"~/Home/OrdenTrabajoLCAME.aspx";
		public const string PAGE_ORDENTRABAJOTVCN = @"~/Home/OrdenTrabajoCNAME.aspx";
		public const string PAGE_ORDENTRABAJOTVDT = @"~/Home/OrdenTrabajoDTAME.aspx";
//		public const string PAGE_SITUACIONINGRESAR = @"~/Home/CambioSitIngresar.aspx";
		public const string PAGE_SITUACIONINGRESAR = @"~/Home/ExpedienteCambioSit.aspx";
		public const string PAGE_BOLETINMODIFICAR = @"~/Home/BoletinConsultar.aspx";
		public const string PAGE_BOLETININGRESAR = @"~/Home/BoletinModificar.aspx";
		public const string PAGE_BOLETINIMPORTAR = @"~/Home/BoletinImportar.aspx";
		public const string PAGE_CARTASGENERAR = @"~/Home/CartasGenerar.aspx";
//		public const string PAGE_CARTASGENERAR = @"~/Home/Merge.aspx";
		public const string PAGE_PODERCONSULTA = @"~/Home/PoderConsulta.aspx";
		public const string PAGE_PODERSITUACION = @"~/Home/PoderCambioSit.aspx";
 
		//Entidades
		public const string PAGE_ENTIDADESING = @"http://SGE/Entidades/Entidades/ABM/WebForm_Ingreso.aspx";
		public const string PAGE_ENTIDADESCONACT = @"http://SGE/Entidades/Administrativas/ABM/frmBsqPersona.aspx";
		public const string PAGE_CONTACTOSING	= @"http://SGE/Entidades/Administrativas/ABM/frmBsqPersona.aspx";
		public const string PAGE_CONTACTOSCONACT	= @"~/Home/MarcaModificar.aspx";

		// agregar aqui proximas paginas

		// ID de Area
		public enum Area
		{
			MARCAS = 1,
			PODER  = 2,
			LITIGIOS_ADM = 3,
		}

		public enum Proceso
		{
			MARCAS = 1,
			PODER = 2,
			LITIGIOS_ADM = 3,
			PATENTE	= 4
		}

		public enum Marca_Tipo_Tramite
		{
			REGISTRO = 1,
			RENOVACION = 2,
			TRANSFERENCIA = 3,
			CAMBIO_NOMBRE = 4,
			FUSION = 5,
			CAMBIO_DOMICILIO = 6,
			LICENCIA = 7,
			DUPLICADO = 8
		}

		public enum Situaciones
		{
			PRESENTADA = 2,
			PUBLICADA = 4,
			CONCEDIDA = 6,
			TITULO_RETIRADO = 7,
			TITULO_ENVIADO = 8,
			ARCHIVADA = 9,	
			CONSTANCIA = 29,
			CONSTANCIA_RETIRADA = 30,
			CONSTANCIA_ENVIADA = 31
		}
		//ID de la Edición de Niza Vigente
		public const int NIZAEDICIONID_VIGENTE = 2;
	}
}