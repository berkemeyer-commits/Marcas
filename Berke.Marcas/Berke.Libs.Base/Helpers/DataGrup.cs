
//                  DATAGROUP 
using System;
using System.Data;
using Berke.Libs.Base.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.DG;



#region IDataGroup
namespace Berke.DG
{
	public interface IDataGroup
	{
		DataSet AsDataSet();
		string DataSetName 
		{
			get; 
		}


	} // end interface IDataGroup
} // end namespaces
#endregion IDataGroup


#region MarcaDG
namespace Berke.DG
{
	public class MarcaDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "MarcaDG";
		
		public Berke.DG.DBTab.Marca			Marca;
		public Berke.DG.DBTab.MarcaTipo			MarcaTipo;
		public Berke.DG.DBTab.Clase			Clase;
		public Berke.DG.DBTab.MarcaRegRen			MarcaRegRen;
		public Berke.DG.DBTab.Expediente			Expediente;
		public Berke.DG.DBTab.Propietario			Propietario;
		public Berke.DG.DBTab.Cliente			Cliente;
		public Berke.DG.DBTab.CAgenteLocal			CAgenteLocal;
		public Berke.DG.DBTab.Atencion				Atencion;
		public Berke.DG.DBTab.BussinessUnit			BussinessUnit;

		#endregion Datos Miembro

		#region Constructores y afines
		public MarcaDG()
		{
			
			Marca		= new Berke.DG.DBTab.Marca("Marca");
			MarcaTipo		= new Berke.DG.DBTab.MarcaTipo("MarcaTipo");
			Clase		= new Berke.DG.DBTab.Clase("Clase");
			MarcaRegRen		= new Berke.DG.DBTab.MarcaRegRen("MarcaRegRen");
			Expediente		= new Berke.DG.DBTab.Expediente("Expediente");
			Propietario		= new Berke.DG.DBTab.Propietario("Propietario");
			Cliente		= new Berke.DG.DBTab.Cliente("Cliente");
			CAgenteLocal		= new Berke.DG.DBTab.CAgenteLocal("CAgenteLocal");
			Atencion	= new Berke.DG.DBTab.Atencion("Atencion");
			BussinessUnit = new Berke.DG.DBTab.BussinessUnit("BussinessUnit");
		}

		public MarcaDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Marca	= new Berke.DG.DBTab.Marca( ds.Tables["Marca"], "Marca");
			MarcaTipo	= new Berke.DG.DBTab.MarcaTipo( ds.Tables["MarcaTipo"], "MarcaTipo");
			Clase	= new Berke.DG.DBTab.Clase( ds.Tables["Clase"], "Clase");
			MarcaRegRen	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["MarcaRegRen"], "MarcaRegRen");
			Expediente	= new Berke.DG.DBTab.Expediente( ds.Tables["Expediente"], "Expediente");
			Propietario	= new Berke.DG.DBTab.Propietario( ds.Tables["Propietario"], "Propietario");
			Cliente	= new Berke.DG.DBTab.Cliente( ds.Tables["Cliente"], "Cliente");
			CAgenteLocal	= new Berke.DG.DBTab.CAgenteLocal( ds.Tables["CAgenteLocal"], "CAgenteLocal");	
			Atencion	= new Berke.DG.DBTab.Atencion( ds.Tables["Atencion"], "Atencion");	
			BussinessUnit	= new Berke.DG.DBTab.BussinessUnit( ds.Tables["BussinessUnit"], "BussinessUnit");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Marca.Table.Copy() );
			ds.Tables.Add( MarcaTipo.Table.Copy() );
			ds.Tables.Add( Clase.Table.Copy() );
			ds.Tables.Add( MarcaRegRen.Table.Copy() );
			ds.Tables.Add( Expediente.Table.Copy() );
			ds.Tables.Add( Propietario.Table.Copy() );
			ds.Tables.Add( Cliente.Table.Copy() );
			ds.Tables.Add( CAgenteLocal.Table.Copy() );
			ds.Tables.Add( Atencion.Table.Copy() );
			ds.Tables.Add( BussinessUnit.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class MarcaDG



} // end namespaces
#endregion MarcaDG


#region LogoDG
namespace Berke.DG
{
	public class LogoDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "LogoDG";
		
		public Berke.DG.DBTab.Logotipo			Logotipo;
		public Berke.DG.ViewTab.vLogo			vLogo;

		#endregion Datos Miembro

		#region Constructores y afines
		public LogoDG()
		{
			
			Logotipo		= new Berke.DG.DBTab.Logotipo("Logotipo");
			vLogo		= new Berke.DG.ViewTab.vLogo("vLogo");
		}

		public LogoDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Logotipo	= new Berke.DG.DBTab.Logotipo( ds.Tables["Logotipo"], "Logotipo");
			vLogo	= new Berke.DG.ViewTab.vLogo( ds.Tables["vLogo"], "vLogo");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Logotipo.Table.Copy() );
			ds.Tables.Add( vLogo.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class LogoDG



} // end namespaces
#endregion LogoDG


#region RenovacionDG
namespace Berke.DG
{
	public class RenovacionDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "RenovacionDG";
		
		public Berke.DG.DBTab.OrdenTrabajo			OrdenTrabajo;
		public Berke.DG.DBTab.Expediente			Expediente;
		public Berke.DG.DBTab.Documento			Documento;
		public Berke.DG.DBTab.Expediente_Documento			Expediente_Documento;
		public Berke.DG.DBTab.MarcaRegRen			MarcaRegRen;
		public Berke.DG.DBTab.Marca			Marca;
		public Berke.DG.DBTab.Marca_ClaseIdioma			Marca_ClaseIdioma;
		public Berke.DG.ViewTab.vRenovacionMarca			vRenovacionMarca;
		public Berke.DG.ViewTab.vRenovacionLimitadas			vRenovacionLimitadas;
		public Berke.DG.DBTab.Expediente_Instruccion			Expediente_Instruccion;
		public Berke.DG.DBTab.ExpedienteCampo			ExpedienteCampo;
		public Berke.DG.DBTab.PropietarioXMarca			PropietarioXMarca;
		public Berke.DG.DBTab.ExpedienteXPropietario			ExpedienteXPropietario;
		public Berke.DG.DBTab.ExpedienteXPoder			ExpedienteXPoder;
		public Berke.DG.DBTab.Atencion			Atencion;
		public Berke.DG.DBTab.Expediente_Distribuidor	Expediente_Distribuidor;

		#endregion Datos Miembro

		#region Constructores y afines
		public RenovacionDG()
		{
			
			OrdenTrabajo		= new Berke.DG.DBTab.OrdenTrabajo("OrdenTrabajo");
			Expediente		= new Berke.DG.DBTab.Expediente("Expediente");
			Documento		= new Berke.DG.DBTab.Documento("Documento");
			Expediente_Documento		= new Berke.DG.DBTab.Expediente_Documento("Expediente_Documento");
			MarcaRegRen		= new Berke.DG.DBTab.MarcaRegRen("MarcaRegRen");
			Marca		= new Berke.DG.DBTab.Marca("Marca");
			Marca_ClaseIdioma		= new Berke.DG.DBTab.Marca_ClaseIdioma("Marca_ClaseIdioma");
			vRenovacionMarca		= new Berke.DG.ViewTab.vRenovacionMarca("vRenovacionMarca");
			vRenovacionLimitadas		= new Berke.DG.ViewTab.vRenovacionLimitadas("vRenovacionLimitadas");
			Expediente_Instruccion		= new Berke.DG.DBTab.Expediente_Instruccion("Expediente_Instruccion");
			ExpedienteCampo		= new Berke.DG.DBTab.ExpedienteCampo("ExpedienteCampo");
			PropietarioXMarca		= new Berke.DG.DBTab.PropietarioXMarca("PropietarioXMarca");
			ExpedienteXPropietario		= new Berke.DG.DBTab.ExpedienteXPropietario("ExpedienteXPropietario");
			ExpedienteXPoder		= new Berke.DG.DBTab.ExpedienteXPoder("ExpedienteXPoder");
			Atencion		= new Berke.DG.DBTab.Atencion("Atencion");
			Expediente_Distribuidor = new Berke.DG.DBTab.Expediente_Distribuidor("Expediente_Distribuidor");
		}

		public RenovacionDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			OrdenTrabajo	= new Berke.DG.DBTab.OrdenTrabajo( ds.Tables["OrdenTrabajo"], "OrdenTrabajo");
			Expediente	= new Berke.DG.DBTab.Expediente( ds.Tables["Expediente"], "Expediente");
			Documento	= new Berke.DG.DBTab.Documento( ds.Tables["Documento"], "Documento");
			Expediente_Documento	= new Berke.DG.DBTab.Expediente_Documento( ds.Tables["Expediente_Documento"], "Expediente_Documento");
			MarcaRegRen	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["MarcaRegRen"], "MarcaRegRen");
			Marca	= new Berke.DG.DBTab.Marca( ds.Tables["Marca"], "Marca");
			Marca_ClaseIdioma	= new Berke.DG.DBTab.Marca_ClaseIdioma( ds.Tables["Marca_ClaseIdioma"], "Marca_ClaseIdioma");
			vRenovacionMarca	= new Berke.DG.ViewTab.vRenovacionMarca( ds.Tables["vRenovacionMarca"], "vRenovacionMarca");
			vRenovacionLimitadas	= new Berke.DG.ViewTab.vRenovacionLimitadas( ds.Tables["vRenovacionLimitadas"], "vRenovacionLimitadas");
			Expediente_Instruccion	= new Berke.DG.DBTab.Expediente_Instruccion( ds.Tables["Expediente_Instruccion"], "Expediente_Instruccion");
			ExpedienteCampo	= new Berke.DG.DBTab.ExpedienteCampo( ds.Tables["ExpedienteCampo"], "ExpedienteCampo");
			PropietarioXMarca	= new Berke.DG.DBTab.PropietarioXMarca( ds.Tables["PropietarioXMarca"], "PropietarioXMarca");
			ExpedienteXPropietario	= new Berke.DG.DBTab.ExpedienteXPropietario( ds.Tables["ExpedienteXPropietario"], "ExpedienteXPropietario");
			ExpedienteXPoder	= new Berke.DG.DBTab.ExpedienteXPoder( ds.Tables["ExpedienteXPoder"], "ExpedienteXPoder");
			Atencion	= new Berke.DG.DBTab.Atencion( ds.Tables["Atencion"], "Atencion");	
			Expediente_Distribuidor = new Berke.DG.DBTab.Expediente_Distribuidor( ds.Tables["Expediente_Distribuidor"], "Expediente_Distribuidor");
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( OrdenTrabajo.Table.Copy() );
			ds.Tables.Add( Expediente.Table.Copy() );
			ds.Tables.Add( Documento.Table.Copy() );
			ds.Tables.Add( Expediente_Documento.Table.Copy() );
			ds.Tables.Add( MarcaRegRen.Table.Copy() );
			ds.Tables.Add( Marca.Table.Copy() );
			ds.Tables.Add( Marca_ClaseIdioma.Table.Copy() );
			ds.Tables.Add( vRenovacionMarca.Table.Copy() );
			ds.Tables.Add( vRenovacionLimitadas.Table.Copy() );
			ds.Tables.Add( Expediente_Instruccion.Table.Copy() );
			ds.Tables.Add( ExpedienteCampo.Table.Copy() );
			ds.Tables.Add( PropietarioXMarca.Table.Copy() );
			ds.Tables.Add( ExpedienteXPropietario.Table.Copy() );
			ds.Tables.Add( ExpedienteXPoder.Table.Copy() );
			ds.Tables.Add( Atencion.Table.Copy() );
			ds.Tables.Add( Expediente_Distribuidor.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class RenovacionDG



} // end namespaces
#endregion RenovacionDG


#region ExpeMarCambioSitDG
namespace Berke.DG
{
	public class ExpeMarCambioSitDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "ExpeMarCambioSitDG";
		
		public Berke.DG.ViewTab.CambioSitParam			CambioSitParam;
		public Berke.DG.DBTab.Expediente			Expediente;
		public Berke.DG.DBTab.Expediente_Situacion			Expediente_Situacion;
		public Berke.DG.DBTab.Expediente_Situacion			Expediente_Situacion_bkp;
		public Berke.DG.DBTab.Marca			Marca;
		public Berke.DG.DBTab.MarcaRegRen			MarcaRegRen;
		public Berke.DG.ViewTab.vExpeMarca			vExpeMarca;
		public Berke.DG.DBTab.MarcaRegRen			MarcaRegRenPadre;

		#endregion Datos Miembro

		#region Constructores y afines
		public ExpeMarCambioSitDG()
		{
			
			CambioSitParam		= new Berke.DG.ViewTab.CambioSitParam("CambioSitParam");
			Expediente		= new Berke.DG.DBTab.Expediente("Expediente");
			Expediente_Situacion		= new Berke.DG.DBTab.Expediente_Situacion("Expediente_Situacion");

			Expediente_Situacion_bkp		= new Berke.DG.DBTab.Expediente_Situacion("Expediente_Situacion_bkp");

			Marca		= new Berke.DG.DBTab.Marca("Marca");
			MarcaRegRen		= new Berke.DG.DBTab.MarcaRegRen("MarcaRegRen");
			vExpeMarca		= new Berke.DG.ViewTab.vExpeMarca("vExpeMarca");
			MarcaRegRenPadre		= new Berke.DG.DBTab.MarcaRegRen("MarcaRegRenPadre");
		}

		public ExpeMarCambioSitDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			CambioSitParam	= new Berke.DG.ViewTab.CambioSitParam( ds.Tables["CambioSitParam"], "CambioSitParam");
			Expediente	= new Berke.DG.DBTab.Expediente( ds.Tables["Expediente"], "Expediente");
			Expediente_Situacion	= new Berke.DG.DBTab.Expediente_Situacion( ds.Tables["Expediente_Situacion"], "Expediente_Situacion");
			Expediente_Situacion_bkp	= new Berke.DG.DBTab.Expediente_Situacion( ds.Tables["Expediente_Situacion_bkp"], "Expediente_Situacion_bkp");
			Marca	= new Berke.DG.DBTab.Marca( ds.Tables["Marca"], "Marca");
			MarcaRegRen	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["MarcaRegRen"], "MarcaRegRen");
			vExpeMarca	= new Berke.DG.ViewTab.vExpeMarca( ds.Tables["vExpeMarca"], "vExpeMarca");
			MarcaRegRenPadre	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["MarcaRegRenPadre"], "MarcaRegRenPadre");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( CambioSitParam.Table.Copy() );
			ds.Tables.Add( Expediente.Table.Copy() );
			ds.Tables.Add( Expediente_Situacion.Table.Copy() );

			ds.Tables.Add( Expediente_Situacion_bkp.Table.Copy() );

			ds.Tables.Add( Marca.Table.Copy() );
			ds.Tables.Add( MarcaRegRen.Table.Copy() );
			ds.Tables.Add( vExpeMarca.Table.Copy() );
			ds.Tables.Add( MarcaRegRenPadre.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class ExpeMarCambioSitDG



} // end namespaces
#endregion ExpeMarCambioSitDG


#region TVDG
namespace Berke.DG
{
	public class TVDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "TVDG";
		
		public Berke.DG.ViewTab.vRenovacionMarca			vRenovacionMarca;
		public Berke.DG.DBTab.OrdenTrabajo			OrdenTrabajo;
		public Berke.DG.DBTab.Documento			Documento;
		public Berke.DG.DBTab.DocumentoCampo			DocumentoCampo;
		public Berke.DG.ViewTab.vPoderActual			vPoderActual;
		public Berke.DG.ViewTab.vPoderAnterior			vPoderAnterior;
		public Berke.DG.DBTab.ExpedienteCampo			ExpedienteCampo;
		public Berke.DG.DBTab.Expediente_Instruccion			Expediente_Instruccion;
		public Berke.DG.DBTab.Atencion			Atencion;

		#endregion Datos Miembro

		#region Constructores y afines
		public TVDG()
		{
			
			vRenovacionMarca		= new Berke.DG.ViewTab.vRenovacionMarca("vRenovacionMarca");
			OrdenTrabajo		= new Berke.DG.DBTab.OrdenTrabajo("OrdenTrabajo");
			Documento		= new Berke.DG.DBTab.Documento("Documento");
			DocumentoCampo		= new Berke.DG.DBTab.DocumentoCampo("DocumentoCampo");
			vPoderActual		= new Berke.DG.ViewTab.vPoderActual("vPoderActual");
			vPoderAnterior		= new Berke.DG.ViewTab.vPoderAnterior("vPoderAnterior");
			ExpedienteCampo		= new Berke.DG.DBTab.ExpedienteCampo("ExpedienteCampo");
			Expediente_Instruccion		= new Berke.DG.DBTab.Expediente_Instruccion("Expediente_Instruccion");
			Atencion		= new Berke.DG.DBTab.Atencion("Atencion");
		}

		public TVDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			vRenovacionMarca	= new Berke.DG.ViewTab.vRenovacionMarca( ds.Tables["vRenovacionMarca"], "vRenovacionMarca");
			OrdenTrabajo	= new Berke.DG.DBTab.OrdenTrabajo( ds.Tables["OrdenTrabajo"], "OrdenTrabajo");
			Documento	= new Berke.DG.DBTab.Documento( ds.Tables["Documento"], "Documento");
			DocumentoCampo	= new Berke.DG.DBTab.DocumentoCampo( ds.Tables["DocumentoCampo"], "DocumentoCampo");
			vPoderActual	= new Berke.DG.ViewTab.vPoderActual( ds.Tables["vPoderActual"], "vPoderActual");
			vPoderAnterior	= new Berke.DG.ViewTab.vPoderAnterior( ds.Tables["vPoderAnterior"], "vPoderAnterior");
			ExpedienteCampo	= new Berke.DG.DBTab.ExpedienteCampo( ds.Tables["ExpedienteCampo"], "ExpedienteCampo");
			Expediente_Instruccion	= new Berke.DG.DBTab.Expediente_Instruccion( ds.Tables["Expediente_Instruccion"], "Expediente_Instruccion");
			Atencion	= new Berke.DG.DBTab.Atencion( ds.Tables["Atencion"], "Atencion");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( vRenovacionMarca.Table.Copy() );
			ds.Tables.Add( OrdenTrabajo.Table.Copy() );
			ds.Tables.Add( Documento.Table.Copy() );
			ds.Tables.Add( DocumentoCampo.Table.Copy() );
			ds.Tables.Add( vPoderActual.Table.Copy() );
			ds.Tables.Add( vPoderAnterior.Table.Copy() );
			ds.Tables.Add( ExpedienteCampo.Table.Copy() );
			ds.Tables.Add( Expediente_Instruccion.Table.Copy() );
			ds.Tables.Add( Atencion.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class TVDG



} // end namespaces
#endregion TVDG


#region ExpedienteDG
namespace Berke.DG
{
	public class ExpedienteDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "ExpedienteDG";
		
		public Berke.DG.DBTab.Expediente			Expediente;
		public Berke.DG.DBTab.Tramite			Tramite;
		public Berke.DG.DBTab.Situacion			Situacion;
		public Berke.DG.DBTab.OrdenTrabajo			OrdenTrabajo;
		public Berke.DG.DBTab.Cliente			Cliente;
		public Berke.DG.DBTab.Propietario			Propietario;
		public Berke.DG.DBTab.CAgenteLocal			CAgenteLocal;
		public Berke.DG.DBTab.Poder			Poder;
		public Berke.DG.DBTab.Propietario			PropietarioAnt;
		public Berke.DG.DBTab.ExpedienteCampo			ExpedienteCampo;
		public Berke.DG.DBTab.MarcaRegRen			MarcaRegRen;

		#endregion Datos Miembro

		#region Constructores y afines
		public ExpedienteDG()
		{
			
			Expediente		= new Berke.DG.DBTab.Expediente("Expediente");
			Tramite		= new Berke.DG.DBTab.Tramite("Tramite");
			Situacion		= new Berke.DG.DBTab.Situacion("Situacion");
			OrdenTrabajo		= new Berke.DG.DBTab.OrdenTrabajo("OrdenTrabajo");
			Cliente		= new Berke.DG.DBTab.Cliente("Cliente");
			Propietario		= new Berke.DG.DBTab.Propietario("Propietario");
			CAgenteLocal		= new Berke.DG.DBTab.CAgenteLocal("CAgenteLocal");
			Poder		= new Berke.DG.DBTab.Poder("Poder");
			PropietarioAnt		= new Berke.DG.DBTab.Propietario("PropietarioAnt");
			ExpedienteCampo		= new Berke.DG.DBTab.ExpedienteCampo("ExpedienteCampo");
			MarcaRegRen		= new Berke.DG.DBTab.MarcaRegRen("MarcaRegRen");
		}

		public ExpedienteDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Expediente	= new Berke.DG.DBTab.Expediente( ds.Tables["Expediente"], "Expediente");
			Tramite	= new Berke.DG.DBTab.Tramite( ds.Tables["Tramite"], "Tramite");
			Situacion	= new Berke.DG.DBTab.Situacion( ds.Tables["Situacion"], "Situacion");
			OrdenTrabajo	= new Berke.DG.DBTab.OrdenTrabajo( ds.Tables["OrdenTrabajo"], "OrdenTrabajo");
			Cliente	= new Berke.DG.DBTab.Cliente( ds.Tables["Cliente"], "Cliente");
			Propietario	= new Berke.DG.DBTab.Propietario( ds.Tables["Propietario"], "Propietario");
			CAgenteLocal	= new Berke.DG.DBTab.CAgenteLocal( ds.Tables["CAgenteLocal"], "CAgenteLocal");
			Poder	= new Berke.DG.DBTab.Poder( ds.Tables["Poder"], "Poder");
			PropietarioAnt	= new Berke.DG.DBTab.Propietario( ds.Tables["PropietarioAnt"], "PropietarioAnt");
			ExpedienteCampo	= new Berke.DG.DBTab.ExpedienteCampo( ds.Tables["ExpedienteCampo"], "ExpedienteCampo");
			MarcaRegRen	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["MarcaRegRen"], "MarcaRegRen");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Expediente.Table.Copy() );
			ds.Tables.Add( Tramite.Table.Copy() );
			ds.Tables.Add( Situacion.Table.Copy() );
			ds.Tables.Add( OrdenTrabajo.Table.Copy() );
			ds.Tables.Add( Cliente.Table.Copy() );
			ds.Tables.Add( Propietario.Table.Copy() );
			ds.Tables.Add( CAgenteLocal.Table.Copy() );
			ds.Tables.Add( Poder.Table.Copy() );
			ds.Tables.Add( PropietarioAnt.Table.Copy() );
			ds.Tables.Add( ExpedienteCampo.Table.Copy() );
			ds.Tables.Add( MarcaRegRen.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class ExpedienteDG



} // end namespaces
#endregion ExpedienteDG


#region ClienteDG
namespace Berke.DG
{
	public class ClienteDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "ClienteDG";
		
		public Berke.DG.DBTab.Cliente			Cliente;

		#endregion Datos Miembro

		#region Constructores y afines
		public ClienteDG()
		{
			
			Cliente		= new Berke.DG.DBTab.Cliente("Cliente");
		}

		public ClienteDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Cliente	= new Berke.DG.DBTab.Cliente( ds.Tables["Cliente"], "Cliente");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Cliente.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class ClienteDG



} // end namespaces
#endregion ClienteDG


#region MergeDG
namespace Berke.DG
{
	public class MergeDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "MergeDG";
		
		public Berke.DG.DBTab.Expediente			Expediente;
		public Berke.DG.DBTab.Marca			Marca;
		public Berke.DG.DBTab.MarcaRegRen			regACT;
		public Berke.DG.DBTab.MarcaRegRen			regANT;
		public Berke.DG.DBTab.Clase			Clase;
		public Berke.DG.DBTab.Expediente_Situacion			Expediente_Situacion;
		public Berke.DG.DBTab.Expediente			expepadre;

		#endregion Datos Miembro

		#region Constructores y afines
		public MergeDG()
		{
			
			Expediente		= new Berke.DG.DBTab.Expediente("Expediente");
			Marca		= new Berke.DG.DBTab.Marca("Marca");
			regACT		= new Berke.DG.DBTab.MarcaRegRen("regACT");
			regANT		= new Berke.DG.DBTab.MarcaRegRen("regANT");
			Clase		= new Berke.DG.DBTab.Clase("Clase");
			Expediente_Situacion		= new Berke.DG.DBTab.Expediente_Situacion("Expediente_Situacion");
			expepadre		= new Berke.DG.DBTab.Expediente("expepadre");
		}

		public MergeDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Expediente	= new Berke.DG.DBTab.Expediente( ds.Tables["Expediente"], "Expediente");
			Marca	= new Berke.DG.DBTab.Marca( ds.Tables["Marca"], "Marca");
			regACT	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["regACT"], "regACT");
			regANT	= new Berke.DG.DBTab.MarcaRegRen( ds.Tables["regANT"], "regANT");
			Clase	= new Berke.DG.DBTab.Clase( ds.Tables["Clase"], "Clase");
			Expediente_Situacion	= new Berke.DG.DBTab.Expediente_Situacion( ds.Tables["Expediente_Situacion"], "Expediente_Situacion");
			expepadre	= new Berke.DG.DBTab.Expediente( ds.Tables["expepadre"], "expepadre");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Expediente.Table.Copy() );
			ds.Tables.Add( Marca.Table.Copy() );
			ds.Tables.Add( regACT.Table.Copy() );
			ds.Tables.Add( regANT.Table.Copy() );
			ds.Tables.Add( Clase.Table.Copy() );
			ds.Tables.Add( Expediente_Situacion.Table.Copy() );
			ds.Tables.Add( expepadre.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class MergeDG



} // end namespaces
#endregion MergeDG


#region RegistroDG
namespace Berke.DG
{
	public class RegistroDG : IDataGroup
	{
		#region Datos Miembro
		private const string _DataSetName = "RegistroDG";
		
		public Berke.DG.DBTab.Poder			Poder;
		public Berke.DG.DBTab.OrdenTrabajo			OrdenTrabajo;
		public Berke.DG.DBTab.Propietario			Propietario;
		public Berke.DG.ViewTab.vExpeMarca			vExpeMarca;
		public Berke.DG.DBTab.Atencion			Atencion;

		#endregion Datos Miembro

		#region Constructores y afines
		public RegistroDG()
		{
			
			Poder		= new Berke.DG.DBTab.Poder("Poder");
			OrdenTrabajo		= new Berke.DG.DBTab.OrdenTrabajo("OrdenTrabajo");
			Propietario		= new Berke.DG.DBTab.Propietario("Propietario");
			vExpeMarca		= new Berke.DG.ViewTab.vExpeMarca("vExpeMarca");
			Atencion		= new Berke.DG.DBTab.Atencion("Atencion");
		}

		public RegistroDG( DataSet ds ){
			if( ds.DataSetName.ToUpper() != _DataSetName.ToUpper() ) { 
				throw new Berke.Excep.Tech.DataSetNameMissmatch(ds.DataSetName,_DataSetName);
			}
			
			Poder	= new Berke.DG.DBTab.Poder( ds.Tables["Poder"], "Poder");
			OrdenTrabajo	= new Berke.DG.DBTab.OrdenTrabajo( ds.Tables["OrdenTrabajo"], "OrdenTrabajo");
			Propietario	= new Berke.DG.DBTab.Propietario( ds.Tables["Propietario"], "Propietario");
			vExpeMarca	= new Berke.DG.ViewTab.vExpeMarca( ds.Tables["vExpeMarca"], "vExpeMarca");
			Atencion	= new Berke.DG.DBTab.Atencion( ds.Tables["Atencion"], "Atencion");	
		}

		#endregion Constructores y afines

		#region Properties 
		public string DataSetName { get{return _DataSetName;} }
		#endregion Properties 

		#region Metodos

		public DataSet AsDataSet() {
			DataSet ds = new DataSet(_DataSetName);
			
			ds.Tables.Add( Poder.Table.Copy() );
			ds.Tables.Add( OrdenTrabajo.Table.Copy() );
			ds.Tables.Add( Propietario.Table.Copy() );
			ds.Tables.Add( vExpeMarca.Table.Copy() );
			ds.Tables.Add( Atencion.Table.Copy() );
			return ds;
		}

		#endregion Metodos

	} // end class RegistroDG



} // end namespaces
#endregion RegistroDG

