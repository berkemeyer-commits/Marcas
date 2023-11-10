using System;

namespace Berke.Libs.Base
{
	/// <summary>
	/// Summary description for GlobalConst.
	/// </summary>
	public class GlobalConst
	{
		//TODO: antes de implementar verificar si estos códigos siguen vigentes!
		public enum Marca_Tipo_Tramite
		{
			REGISTRO			= 1, // Registro de Marcas	
			RENOVACION			= 2, // Renovación de Marcas
			TRANSFERENCIA		= 3, // Transferencia sobre un registro	
			CAMBIO_NOMBRE		= 4, // Cambio de Nombre contra un registro	
			FUSION				= 5, // Fusión contra un registro
			CAMBIO_DOMICILIO	= 6, // Cambio de Domicilio	
			LICENCIA			= 7, // Inscripción de Licencia contra un registro
			DUPLICADO			= 8, // Duplicado de título de registro
			INSCRIPCION_PODER	= 9, // Inscripción del poder
			CAMBIO_NOMRE_DOMIC	= 10,// Cambio de nombre + cambio de domicilio
			OPOSICION			= 14,// Oposicion
			REG_ADUANA			= 30 // Registro en Aduanas 
		}

		public enum Situaciones
		{
			HOJA_INICIO						= 1,
			PRESENTADA						= 2,
			ORDEN_PUBLICACION				= 3,
			PUBLICADA						= 4,
			EXAMEN_FONDO					= 5,
			CONCEDIDA						= 6,
			TITULO_RETIRADO					= 7,
			TITULO_ENVIADO					= 8,
			ARCHIVADA						= 9,	
			CONSTANCIA						= 29,
			CONSTANCIA_RETIRADA				= 30,
			CONSTANCIA_ENVIADA				= 31,
			TITULO_A_FACTURACION			= 34,
			DESISTIDA						= 10,
			ESPERANDO_PODER					= 36,
			A_INSCRIBIR						= 37,
			INSCRIPTO						= 38,
			ABANDONADA						= 47,
			CANCELACION_REG					= 48,
			CANCELACION_TV					= 59,
			MARCA_ANULADA					= 60,
			CON_VISTA						= 12,
			CON_RECHAZO						= 13,
			CON_SUSPENSION					= 14,
			A_ADECUAR						= 15,
			SUSPENDIDA						= 28,
			MANIFESTACION_POR_ANTECEDENTES	= 35,
			CON_OPOSICION					= 40,
			CON_JUICIO_DE_NULIDAD			= 41,
			CONTENCIOSO_ADMINISTRATIVO		= 42,
			RECONSIDERACION					= 45,
			APELACION						= 46
		}

		public enum SituacionesXTramite
		{
			TRANSFERENCIA_HI				= 39,
			TRANSFERENCIA_PRESENTADA		= 40,
			CAMBIO_NOMBRE_HI				= 47,
			CAMBIO_NOMBRE_PRESENTADA		= 48,
			FUSION_HI						= 53,
			FUSION_PRESENTADA				= 54,
			CAMBIO_DOMICILIO_HI				= 59,
			CAMBIO_DOMICILIO_PRESENTADA		= 60,
			LICENCIA_HI						= 65,
            LICENCIA_PRESENTADA				= 66,
			DUPLICADO_HI					= 73,
			REGISTRO_TITULO_RETIRADO		= 7,
			REGISTRO_TITULO_CORREGIDO		= 151,
			RENOVACION_TITULO_RETIRADO		= 35,
			RENOVACION_TITULO_CORREGIDO		= 152
		}

		public enum Pendientes
		{
			OPOSICION = 4
		}

		public enum Unidad
		{
			DIAS_HABILES	= 1,
			MES				= 2,
			ANHO			= 3,
			DIAS_CALENDARIO	= 4
		}

		public enum PertentenciaMotivo
		{
			INTERVENCION_BERKE	= 11,
			OTROS				= 12
		}

		public enum Pertenencia
		{
			Nuestra = 1,
			Terceros = 2,
			Terceros_Vigilada = 3,
			Terceros_Sustituidas = 4,
			Nuestra_Abandonda_Berke = 5
		}

		public enum Idioma
		{
			INGLES	= 1,
			ESPANOL	= 2,
			ALEMAN  = 3,
			FRANCES	= 4
		}

		public enum Pais
		{
			PARAGUAY = 3684
		}
        
		public enum DocumentoTipo
		{
            HOJA_DESCRIPTIVA_TV = 0,
			DOCUMENTO_PRIORIDAD = 1,
			DOCUMENTO_TRANSFERENCIA = 2,
			CONTRATO_LICENCIA		= 3,
			CORRESPONDENCIA_ORDEN	= 4,
			HOJA_DESCRIPTIVA = 5,
			PUBLICACION = 66,
			CARTAS_RECAUDO = 7,
			PODER = 9,
			CEDULAS = 16,
			TITULO = 88,
			PRES_REGADUANA = 77
		}

		public enum PoderTipo
		{
			PODER_ORIGINAL			= 1,
			FAX_PODER				= 2,
			PODER_DERECHO_PROPIO	= 3
		}

		public enum Tabla
		{
			CPERSONAFISICA	= 9,
			RELPJAREA		= 10,
			CDIRECCION		= 13
		}

		public enum Area
		{
			GENERAL		= 1,
			RENOVACION	= 2,
			LITIGIOS	= 3,
			REGISTRO	= 4,
			VIGILANCIA  = 7,
			LEGALES		= 5,
            CONTABILIDAD = 14
		}

		public enum Vias
		{
			TELEFONO	= 2,
			FAX			= 3,
			EMAIL		= 4,
			INTERNET	= 5,
			CORREOAEREO	= 6,
			COURIER		= 7,
			INTERNO		= 8,
            CELULAR     = 9
		}
		
		public enum EdicionesNiza
		{
			EDICION_7MA	= 1,
			EDICION_8VA	= 2,
			EDICION_10MA = 3,
            EDICION_11MA = 5,
            EDICION_12MA = 7,
            EDICION_VIGENTE = 7
		}

		public enum MarcaTipo
		{
			DENOMINATIVA = 1,
			FIGURATIVA	 = 2,
			MIXTA		 = 3
		}

		public enum InstruccionTipo
		{
			ACUSE					 = 2,
			SOLICITARON_COTIZACION	 = 6,
			GENERAL					 = 18,
			PODER					 = 21,
			CONTABILIDAD			 = 22,
			RENOVAR					 = 1,
			NO_RENOVAR				 = 3,
			POSIBLE_NULIDAD			 = 9,
			NOTIFICAR_PUBLICACION    = 32,
			NO_ENVIAR_AVISOS_OPO	 = 7,
			POSIBLE_OPO				 = 33,
			NO_ENVIAR_2DO_AVISOS_OPO = 10,
			INSTRUCCION_DE_OPO		 = 11,
			NO_OPO					 = 35,
			OPOSICION_CON_OTRO_AGENTE = 36,
			NO_ENVIAR_AVISO_OPO_REGEXT = 47,
			NO_ENVIAR_AVISOS_RENOVACION = 5
		}

		public enum MergeTipo
		{
			CARTA_RECAUDO_MARCA = 1,
			CARTA_CONCESION		= 2,
			CARTA_ENVIO_TITULO	= 3,
			AVISOS_VENCIMIENTO	= 4,
			RECAUDO				= 5,
			PRESUPUESTO_ENV_TIT = 6,
			CONSTANCIA			= 7,
			ORDEN_PUBLICACION	= 8
		}

		public enum Notificacion
		{
			SIT_REVERSION			= 19
		}

		public enum TipoAtencionxMarca
		{
			ATENCIONXMARCA			= 1,
			ATENCIONXBUNIT			= 2,
			ATENCIONXMARCAXTRAMITE  = 3
		}

        
		public const string PROP_ANTERIOR_NOMBRE = "Propietario Anterior";
		public const string PROP_ANTERIOR_DIR = "Propietario Anterior_Dir";
		public const string PROP_ANTERIOR_PAIS = "Propietario Anterior_Pais";
		public const string PROP_ANTERIOR_ID = "Propietario Anterior_ID";
		public const string PROP_ACTUAL_NOMBRE = "Propietario";
		public const string PROP_ACTUAL_DIR = "Propietario_Dir";
		public const string PROP_ACTUAL_PAIS = "Propietario_Pais";
		public const string FUS_NOMBRE_OTROS_PROP = "Fusionado con";
		public const string FUS_DIR_OTROS_PROP = "Dirección otros fusionados";
		public const string LIC_NOMBRE = "Licenciatario";
		public const string LIC_DIRECCION = "Dirección";
		public const string LIC_VIGENC_DESDE = "Vigencia Desde";
		public const string LIC_VIGENC_HASTA = "Vigencia Hasta";
		public const string LIC_DESCRIPCION = "Descripción";
		public const string DUP_LEGISLACION_CONSULAR = "Legalización Consular";
		public const string CLI_ANTERIOR_ID = "Cliente Anterior_ID";
		public const string POD_ANTERIOR_ID = "Poder Anterior_ID";
		public const string VIGILADA_ANTERIOR = "Vigilada Anterior";
		public const string SOLICITUD_RENOVACION = "Solicitud de Renovación";
		public const string SOLICITUD_REGISTRO = "Solictud de Registro";
		public const int	DOCUMENTO_RUTA_PODERES = 9;
		public const int	DOCUMENTO_RUTA_PODERES_ANO_0 = 99;
		public const string CARPETA_REPORTE = @"\\Ber-app-01\Reports\";
		public static string URL_REPORTES = @"http://sge/marcas/Reports/";
		public const int PERIODO_GRACIA = 6; //MESES
		public const string BOLETIN_DESISTIDA = "DS";
		public const string FECHA_MINIMA = "01/01/1900";
		public const string REGISTROS_FALSOS = "99999,999999";
		public const string DESCRIP_ATENCIONXMARCA = "Atención por Marca";
		public const string DESCRIP_ATENCIONXBUNIT = "Atención por Bussiness Unit";
		public const string DESCRIP_ATENCIONXMARCAXTRAMITE = "Atención por Marca/Trámite";
        public const string KEY_BERKE_DOMAIN_NAME = "berkeDomainName";
	}

	public class PlantillaTipo 
	{
		public const string TMPL_REPORT_CLIENT_ACTIVITY = "repClienteActivity";
	}
}
