using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Berke.Libs.WebBase.Helpers;
using Berke.Libs.Base.DSHelpers;
using Berke.Libs.WebBase.Controls;

namespace Berke.Marcas.WebUI.Home
{
	public partial class CorrespConsulta : System.Web.UI.Page
	{
		#region Controles del Web Form












		#endregion 
	
		#region Asignar Delegados
		private void AsignarDelegados()
		{



		}
		#endregion Asignar Delegados

		#region Asignar Valores Iniciales

        private bool manageContabilidadDisplay()
        {
            bool display = false;

            Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
            db.DataBaseName = WebUI.Helpers.MyApplication.CurrentDBName;
            db.ServerName = WebUI.Helpers.MyApplication.CurrentServerName;
        
            Berke.DG.DBTab.Usuario usu = new DG.DBTab.Usuario(db);
            usu.ClearFilter();
            usu.Dat.Usuario.Filter = Berke.Libs.Base.Acceso.GetCurrentUser();
            usu.Adapter.ReadAll();

            if ((usu.RowCount == 1) && (usu.Dat.AreaID.AsInt == (int)Berke.Libs.Base.GlobalConst.Area.CONTABILIDAD))
            {
                display = true;
            }
            return display;
        }

		private void AsignarValoresIniciales()
		{
            #region Prioridad DropDown
			

			Berke.DG.ViewTab.ListTab ltPrioridad = Berke.Marcas.UIProcess.Model.Prioridad.ReadForSelect();
			ddlPrioridadID.Fill( ltPrioridad.Table, true);
			
			#endregion Prioridad DropDown

			#region Area
		
			Berke.DG.ViewTab.ListTab ltArea = Berke.Marcas.UIProcess.Model.Area.ReadForSelect();
            ddlArea.Fill(ltArea.Table, true);
            ddlAreaAsignacion.Fill(ltArea.Table, true);
            

            if (!this.manageContabilidadDisplay())
            {
                #region Asignar PK para poder realizar eliminación en base a este
                DataColumn[] key = new DataColumn[1];
                key[0] = ltArea.Table.Columns["ID"];
                ltArea.Table.PrimaryKey = key;
                #endregion Asignar PK para poder realizar eliminación en base a este

                DataRow dr = ltArea.Table.Rows.Find((int)Berke.Libs.Base.GlobalConst.Area.CONTABILIDAD);
                ltArea.Table.Rows.Remove(dr);
                ltArea.Table.AcceptChanges();
            }

			ddlAreaDistrib.Fill(ltArea.Table, true);

			#endregion Area 


			#region TrabajoTipo DropDown
			
			//			Berke.DG.ViewTab.ListTab ltTrabajoTipo = Berke.Marcas.UIProcess.Model.TrabajoTipo.ReadForSelect();
			//			
			
			Berke.DG.ViewTab.ListTab l1 = new Berke.DG.ViewTab.ListTab();

			#region aSIGNAR VALORES HARD
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "! CARTA DE ADVERTENCIA"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  2; l1.Dat.Descrip.Value = "! NULIDAD ADMINISTRATIVA."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  3; l1.Dat.Descrip.Value = "! USO INDEBIDO DE MARCAS"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  4; l1.Dat.Descrip.Value = "!Recibir informaciones varias sobre patentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  5; l1.Dat.Descrip.Value = "!Recibir informaciones varias sobre Reg. de marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  6; l1.Dat.Descrip.Value = "!Recibir informaciones varias sobre Ren. de marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  7; l1.Dat.Descrip.Value = "!Recibir instrucciones sobre vigilancia de marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  8; l1.Dat.Descrip.Value = "Abandonar nombre de dominio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  9; l1.Dat.Descrip.Value = "Abandonar oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  10; l1.Dat.Descrip.Value = "Abandonar Patente/Diseño Industrial"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  11; l1.Dat.Descrip.Value = "Actualizar en COMPU Abandono de Solic. de Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  12; l1.Dat.Descrip.Value = "Actualizar en COMPU Cambio de Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  13; l1.Dat.Descrip.Value = "Actualizar en COMPU datos de Agentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  14; l1.Dat.Descrip.Value = "Actualizar en COMPU Instruc. de Renovación de Reg."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  15; l1.Dat.Descrip.Value = "Actualizar en COMPU Nuevo Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  16; l1.Dat.Descrip.Value = "Actualizar en COMPU Obs. de Marcas / Trámites"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  17; l1.Dat.Descrip.Value = "Actualizar en COMPU Propietario de Marcas/Trámites"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  18; l1.Dat.Descrip.Value = "Acusar Recibo de Instrucción de Renovacion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  19; l1.Dat.Descrip.Value = "Acusar Recibo de Pedido de Título"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  20; l1.Dat.Descrip.Value = "Acusar Recibo de Prosecucion de Oposicion."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  21; l1.Dat.Descrip.Value = "Acuse de Recibo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  22; l1.Dat.Descrip.Value = "Acuse de recibo de Renovación Mod. Ind."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  23; l1.Dat.Descrip.Value = "AMPLIAR OPOSICION"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  24; l1.Dat.Descrip.Value = "Anotar en Expte. Cambio de Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  25; l1.Dat.Descrip.Value = "Anotar en Expte. Instrucción de Acuse de Ren."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  26; l1.Dat.Descrip.Value = "Anotar en Expte. NO Renovar patente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  26; l1.Dat.Descrip.Value = "Anotar en Expte. NO Renovar Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  28; l1.Dat.Descrip.Value = "Anotar observaciones en COMPU"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  29; l1.Dat.Descrip.Value = "Apelar en oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  30; l1.Dat.Descrip.Value = "Apelar rechazo de registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  31; l1.Dat.Descrip.Value = "Apelar resolucion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  32; l1.Dat.Descrip.Value = "Archivar Certificados para Pruebas de Oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  33; l1.Dat.Descrip.Value = "Archivar Documentos varios Pruebas para Oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  34; l1.Dat.Descrip.Value = "Búsqueda de antecedentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  35; l1.Dat.Descrip.Value = "Búsqueda de antecedentes (piden aclaración)"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  36; l1.Dat.Descrip.Value = "Busqueda de Patentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  37; l1.Dat.Descrip.Value = "Cambio de agente en oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  38; l1.Dat.Descrip.Value = "Cambio de Nombre Patente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  39; l1.Dat.Descrip.Value = "Constitución de sociedad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  40; l1.Dat.Descrip.Value = "Consulta Casos Especiales "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  41; l1.Dat.Descrip.Value = "Consulta General"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  42; l1.Dat.Descrip.Value = "Consulta Juicio Ejecutivo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  43; l1.Dat.Descrip.Value = "Consulta Laboral (sueldos, IPS, MJT)"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  44; l1.Dat.Descrip.Value = "Consulta Nacionalidad Pya."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  45; l1.Dat.Descrip.Value = "Consulta Posible Conflicto"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  46; l1.Dat.Descrip.Value = "Consulta Posibles Juicios "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  47; l1.Dat.Descrip.Value = "Consulta s/  reg. de Dominio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Contratos"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ doc. p/ vista y/o rechazo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Juicio de Nulidad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Leyes "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Leyes Comerciales"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Licencia de Uso"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Licitaciones"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ Promociones"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta s/ reg. de dominio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Consulta sobre Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contactarse con Terceros p/ Acuerdo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar c/ s/ RENOVACION"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Abandono de Solic. de Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Agente Nuevo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Cambio de Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Cobertura de Productos o Servicios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Confirmac. Registro de Marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta confirmacion de renovacion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consentimiento"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta Cambio de Agente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta de Registro de Marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta Endoso Certif.de Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Consulta Gral. de MArcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ apelacion de OP."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ búsquedas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ certif. de registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ CN"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ investigacion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ leyes Paraguayas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ Modelo Industrial"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ patentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ renovacion"; l1.PostNewRow();	
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ Tramites Varios"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Marcas"; l1.PostNewRow();		
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta consulta s/ transf. de patentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar Carta Consulta s/Licencia "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta correspondencia gral."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Cotización"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta de NO recepción de Certif.Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Futuro Poder"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Información de plazos"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Legalización de Documentos"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta posible acuerdo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta posible apelación"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Posible Contestacion de Oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Posible Oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Posible Transferencia"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta revocacion de poder"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ actualizacion datos AGENTE"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ coexistencia de mc."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ derecho de autor"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ doc. de prioridad de reg."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ fusion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ legalizacion de doc. p/ opos."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta s/ publicaciones del Estudio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Cese de Uso de Marca"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status CN,CD,TR,FS,LC,DUP"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Marca c/Vista o Rechazo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Modelo Ind."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Patente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar carta Status Registro / Renovación"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar Expresión de Agravios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contestar Vista"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Contetar carta consulta de leyes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Corregir datos de Certificado de Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Corregir domicilio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Corregir marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Corregir sol. de reg."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Cotizar Patente/Diseño Industrial"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Counterfeit Products"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Desistir Nulidad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Desistir Oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar Certificados de Registro/Renovación"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar Cotización"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar Formulario de Poder"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar listado de marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar Poder"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Enviar Traduccion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío de Documento x courier"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío de Información"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío de presupuesto "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío documentos x fax"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío Poder legalizado del Extranjero."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío Poder p/ Asamblea"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío Poder p/ Juicios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío Poder p/ Nulidad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Envío Pruebas Legalizadas "; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Futuro envio de doc. oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Futuro envío de Poder"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H Inicio Registro de Marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Cambio de Domicilio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Cambio de Nombre"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Duplicado de Título"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Fusión"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Licencia de Uso"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Renovación de Registro"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "H.Inicio Transferencia de marca"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Hoja de inicio de reg. de Dominio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Incorporar Marca para Renovación"; l1.PostNewRow();

			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Incorporar marcas para vigilancia"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Iniciar Juicio Criminal por Falsificacion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucción Acuse de Recibo para Oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instruccion de No Renovar"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucción NO contestar demanda"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones contestar demanda"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones de abandonar la sol. de reg."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones de Apelar"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones Iniciar Juicio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones Iniciar Juicio Contencioso-Adm"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones Iniciar Nulidad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones NO apelar"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones NO iniciar juicio"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Instrucciones No iniciar juicio contencioso"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Investigación Marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Investigar persona o empresa"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Investigar Posible Infraccion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Investigar Posibles Falsificaciones"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Investigar Uso de la Marca"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Juicio Competencia Desleal"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Juicio CONTENCIOSO ADMINISTRATIVO"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Juicio EJECUTIVO"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Juicio Nulidad de Marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Limitar Artículos p/Expte. c/Vista."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Limitar Artículos p/Oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Limitar Artículos p/Sol. Reg. de marcas"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No aceptar limitación"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No ampliar oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No apelar"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No contestar Expresión de Agravios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No contestar oposicion"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No enviar avisos de oposición"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No Expresar Agravios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "NO OPONERSE!!!!!!!!"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No presentar acción contencioso-administrativa"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No presentar la sol. de reg."; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No ren. patente / diseño, modelo industrial"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "No solicitar reg. de marca"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Otros"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pago de Transf. Juicio de Nulidad"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pasar tramite de mc. a otros agentes"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pedido Cotización"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pedido envío documento"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pedido Status Juicios"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Pedir Poder al Cliente"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Acuerdo"; l1.PostNewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Cambio de Nombre"; l1.PostNewRow();


			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Cancelación por no Uso"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Cese de Uso"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible coexistencia para oposición de marcas"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Contencioso"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Copyright"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Juicio Criminal"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Juicio Ejecutivo"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Licencia de Uso"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Nulidad"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Registro de Marca"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Posible Transferencia"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar affidavit"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Escrito Contestación de Oposicion"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Escrito de Limitacion"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Escrito de Oposición"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Registro Extranjero"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Solicitud de Diseño Industrial"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Solicitud de Patente"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Solicitud de Registro Intelectual"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Preparar Transferencia de Patentes"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Presentar acuerdo"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Presentar Expresión de Agravios"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "PRESENTAR OPOSICION"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "PROCEDER AL PAGO DE ANUALIDADES"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Procesar con HTB"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Procesar Futuro Consentimiento"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Procesar varios s/solic.de n/Estudio fuera de PY"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Procesar varios-Cotiz: Adj.Tarif,Hacer Detalle,etc"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Carta de Autorización"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Certif. p/Registro en el Extranjero"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Certif.Extranjero p/Oposición"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Confirmacion de Dominio"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir copia de certific. p/ prueba"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir doc. de coexistencia"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir doc. de prioridad"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir doc. p/ vista"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Documentos de Prioridad para marcas"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Documentos de Transf.y otros Tramites Var."; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Documentos p/ Oposicion"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir informaciones varias p/ oposiciones"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "RECIBIR PODER"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Poder General (p/registro, oposiciones,etc"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Poder para Patente"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir Pruebas Legalizadas para Oposición"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Recibir tarifario para registros en el extranjero"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Rectificar Marca"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Reenviar por fax"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Registro Sanitario"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Ren. Diseño Industrial"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "REN. NOMBRE DE REG. DE DOMINIO "; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Ren. Patente"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "SUSPENDER BUSQUEDA"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Suspender Presentacion de op."; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Sustituir poder"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Tomar intervension en tramites de oposicion "; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Tomar Intervesion en tramites de marcas"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Tomar nota de futuro envio de poder p/registro"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Traducir articulo"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "Transferencia"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zInstrucciones de NO Expresar Agravios"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zInstrucciones de NO Expresar Agravios"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zNo iniciar Oposición"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zRecibir informaciones varias para oposiciones"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zRetirar Juicio"; l1.PostNewRow();		l1.NewRow();
			l1.NewRow(); l1.Dat.ID.Value =  1; l1.Dat.Descrip.Value = "zTerminación de Contrato"; l1.PostNewRow();		l1.NewRow();

			#endregion Asignar Valores HARD

			ddlTrabajoTipo.Fill(  l1.Table, true);


			
			#endregion TrabajoTipo DropDown

			#region Funcionario DropDown
			Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
			ddlAsignacion.Fill( seFuncionario.Table, true);
			ddlFuncionarioFiltro.Fill( seFuncionario.Table, true);
			#endregion Funcionario DropDown


		}
		#endregion Asignar Valores Iniciales

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AsignarDelegados();
			if( !IsPostBack )
			{
				AsignarValoresIniciales();
				MostrarPanel_Busqueda();
			}		
		}
		#endregion Page_Load

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Busqueda de registros 
		protected void btBuscar_Click(object sender, System.EventArgs e)
		{
			this.Buscar();			
		}

		private void Buscar()
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			#region Asignar Parametros (corresp)
			Berke.DG.ViewTab.vCorrespondencia corresp = new Berke.DG.ViewTab.vCorrespondencia();

			

	
			corresp.NewRow(); 
	
			corresp.Dat.ID			.Value = txtID_min.Text;
			corresp.Dat.Nro			.Value = txtNro_min.Text;
			corresp.Dat.Anio		.Value = txtAnio.Text;
			corresp.Dat.FechaAlta	.Value = txtFechaAlta_min.Text;
			corresp.Dat.RefCorresp	.Value = txtRefCorresp.Text;
			corresp.Dat.ClienteID   .Value = txtClienteID.Text;
			corresp.Dat.Nombre		.Value = txtNombre.Text;
            //Area asignada en la distribución de correspondencia
			corresp.Dat.AreaID		.Value = this.ddlAreaDistrib.SelectedValue;
			corresp.Dat.PrioridadID	.Value = this.ddlPrioridadID.SelectedValue;

			/*if (this.chkProcFiltro.Checked)
			{
				corresp.Dat.Estado      .Value = this.chkProcFiltro.Checked;
			}*/
			corresp.Dat.Estado.Value	= GetFilter_Bool(rbProcesado.SelectedValue);
			corresp.Dat.Acusado.Value   = GetFilter_Bool(rbAcusado.SelectedValue);
			corresp.Dat.Facturable.Value   = GetFilter_Bool(rbFacturable.SelectedValue);

			corresp.Dat.FuncAreaID.Value = ddlArea.SelectedValue;
			corresp.Dat.FuncionarioID.Value = this.ddlFuncionarioFiltro.SelectedValue;

			int idx = this.ddlTrabajoTipo.SelectedIndex;

			corresp.Dat.TrabajoTipo	.Value = ddlTrabajoTipo.Items[idx].Text;

			corresp.PostNewRow();
	
			corresp.NewRow(); 
	
			corresp.Dat.ID			.Value = txtID_max.Text;
			corresp.Dat.Nro			.Value = txtNro_max.Text;
			corresp.Dat.FechaAlta	.Value = txtFechaAlta_max.Text;

			
				
			
			
			corresp.PostNewRow();
	
			#endregion Asignar Parametros ( corresp )

			#region Verificar rango de correspondencia para detectar area
			Berke.DG.ViewTab.vCorrespNro correspnroarea = new Berke.DG.ViewTab.vCorrespNro(db);
			#endregion

			#region Obtener datos
            int recuperados = -1;
			try 
			{
				corresp =  Berke.Marcas.UIProcess.Model.Corresp.ReadList( corresp );
				/*
				 * Se comenta por sugerencia de Nestor Caceres
				 * porque no se debe utilizar CArea
				 * 
				 * */

				#region Convertir a Link
				for( corresp.GoTop(); ! corresp.EOF ; corresp.Skip() )
				{
                    string path = "";
					
					correspnroarea.Adapter.ClearParams();
					correspnroarea.Adapter.AddParam("nro",corresp.Dat.Nro.AsInt);
					correspnroarea.Dat.vigente.Filter = true;
					correspnroarea.Adapter.ReadAll();
                    


					//if ( corresp.Dat.AreaID.AsInt != 0 ) 

					/* La correspondencia debe corresponder a un area y solo debe existir
					 * un rango vigente por ello se verifica la cantidad de filas recuperadas
					 * de los rangos de numeros configurados
					 */

					/*if ( correspnroarea.RowCount == 1) 
					{*/
					for(correspnroarea.GoTop(); !correspnroarea.EOF; correspnroarea.Skip())
					{
						if ((correspnroarea.Dat.IDArea.AsInt != 0 ))
						{
                            path = Berke.Libs.Base.DocPath.digitalDocPath(corresp.Dat.Anio.AsInt,
                                corresp.Dat.Nro.AsInt,
                                correspnroarea.Dat.IDArea.AsInt);

                            if (path != "")
							{
								corresp.Edit();
							
								//corresp.Dat.AreaID.AsInt );

								corresp.Dat.RefCorresp.Value = corresp.Dat.RefCorresp.AsString + " " + path;

								corresp.PostEdit();
								break;
	
							}

							corresp.Edit();
							
							//corresp.Dat.AreaID.AsInt );

							corresp.Dat.RefCorresp.Value = corresp.Dat.RefCorresp.AsString + " " + path;

							corresp.PostEdit();
						}
                    }
                    /*[gagaleanod 06/01/2014] Si el funcionario no es del área de Contabilidad no puede ver correspondencia del área*/
                    if ((corresp.Dat.CorAreaID.AsInt == (int)Berke.Libs.Base.GlobalConst.Area.CONTABILIDAD) && (!this.manageContabilidadDisplay()))
                    {
                        corresp.Delete();
                        //if (!corresp.EOF) corresp.Skip();
                    }
                }
                corresp.AcceptAllChanges();
				#endregion  Convertir a Link
				
				
			}
			catch ( Berke.Excep.Biz.TooManyRowsException ex )
			{	
				recuperados = ex.Recuperados;
			}
			catch( Exception exep ) 
			{
				throw new Exception("Class: CorrespConsulta ", exep );
			}
			#endregion Obtener datos
			
			#region Asignar dataSuorce de grilla
			
			dgResult.DataSource = corresp.Table;
			dgResult.DataBind();
			dgAsignacion.DataSource = corresp.Table;
			dgAsignacion.DataBind();
			#endregion

			#region Mostrar Panel segun cantidad de registros obtenidos
			if( recuperados != -1 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text ="Excesiva cantidad de registros("+recuperados.ToString()+ ")" ;
			}
			else if( corresp.RowCount == 0 )
			{
				MostrarPanel_Busqueda();
				lblMensaje.Text = "No se encontraron registros";
			}
			else 
			{
				lblTituloGrid.Text = "Correspondencias &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;("+corresp.RowCount+ "&nbsp;regs.)";
				MostrarPanel_Resultado();
			}
			#endregion

			pnlAsignacion.Visible = false;
			
		}
		#endregion Busqueda de registros 

		#region MostrarPanel_Busqueda
		private void MostrarPanel_Busqueda() 
		{
			lblTituloGrid.Text = "Correspondencias";
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= false;
			this.pnlBuscar.Visible		= true;
		}
		#endregion MostrarPanel_Busqueda


		#region MostrarPanel_Resultado
		private void MostrarPanel_Resultado()
		{
			lblMensaje.Text = "";
			this.pnlResultado.Visible	= true;
			this.pnlBuscar.Visible		= true;			
		}
		#endregion MostrarPanel_Resultado

		protected void btnAsignar_Click(object sender, System.EventArgs e)
		{
			this.Asignar();
		}

		#region Carga de Combo




		#endregion Carga de Combo


		#region digitalDocPath

		
		#endregion digitalDocPath

		private void Asignar()
		{
			pnlResultado.Visible = false;
			pnlAsignacion.Visible = true;
			dgAsignacion.Columns[1].Visible = false;
			dgAsignacion.Columns[10].Visible = true;
			dgAsignacion.Columns[11].Visible = true;
            dgAsignacion.Columns[12].Visible = false;
			dgAsignacion.Columns[13].Visible = false;
			dgAsignacion.Columns[15].Visible = false;
			ddlAsignacion.Visible = true;
			btnAsignacion.Visible = true;
			btnGrabar.Visible = true;
			btnGrabarProcesado.Visible = false;
			chkMarcar.Text = "Marcar todo";
			chkMarcar.Checked = false;
			lblAreaAsignacion.Visible = true;
			ddlAreaAsignacion.Visible = true;
		}

		protected void chkMarcar_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkMarcar.Checked)
			{
				chkMarcar.Text = "Desmarcar todo";

				foreach( DataGridItem item in dgAsignacion.Items )
				{
					CheckBox chkBox = (CheckBox) item.FindControl("chkSel");
					chkBox.Checked = true;
				}
			}
			else
			{
				chkMarcar.Text = "Marcar todo";

				foreach( DataGridItem item in dgAsignacion.Items )
				{
					CheckBox chkBox = (CheckBox) item.FindControl("chkSel");
					chkBox.Checked = false;
				}
			}
		}

		protected void btnAsignacion_Click(object sender, System.EventArgs e)
		{
			this.Asignacion();
		}

		private void Asignacion()
		{
			foreach(DataGridItem item in dgAsignacion.Items)
			{
				CheckBox chkBox = (CheckBox) item.FindControl("chkSel");
				if (chkBox.Checked)
				{
					TextBox txt = (TextBox) item.FindControl("txtAsignado");
					TextBox txtIDFuncionario = (TextBox) item.FindControl("txtIdFuncionario");
					txtIDFuncionario.Text = ddlAsignacion.SelectedValue;
					txt.Text = ddlAsignacion.SelectedItem.Text;
				}
				
			}
		}

		protected void btnGrabar_Click(object sender, System.EventArgs e)
		{
			if (this.GrabarAsignacion(0)) {this.Buscar();};
			
		}

		private bool GrabarAsignacion(int oper)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			Berke.DG.DBTab.Correspondencia correspondencia = new Berke.DG.DBTab.Correspondencia(db);

			foreach(DataGridItem item in dgAsignacion.Items)
			{
				
				CheckBox chkBox = (CheckBox) item.FindControl("chkSel");

				if (chkBox.Checked)
				{
					db.IniciarTransaccion();
					try
					{
						correspondencia.ClearFilter();
						correspondencia.Adapter.ReadByID(Convert.ToInt32(item.Cells[1].Text));
						
						correspondencia.Edit();
						TextBox txtIDFuncionario = (TextBox) item.FindControl("txtIdFuncionario");

						if (oper == 0)
						{
							if (correspondencia.Dat.Estado.AsBoolean)
							{
								throw new Exception("Esta correspondencia ya ha sido procesada, no se puede modificar la asignación");
							}
							
							correspondencia.Dat.FuncionarioID.Value = txtIDFuncionario.Text;
							CheckBox chkFactUpd = (CheckBox) item.FindControl("chkFact");
							correspondencia.Dat.Facturable.Value = chkFactUpd.Checked;
							
						}
						else if (oper == 1)
						{
							if (correspondencia.Dat.FuncionarioID.AsInt != Berke.Libs.Boletin.Libs.Utils.getCurrentFuncionarioID(db))
							{
								throw new Exception("Sólo puede marcar como acusadas o procesadas correspondencias asignadas a Ud.");
							}
							CheckBox chk = (CheckBox) item.FindControl("chkProc");
							correspondencia.Dat.Estado.Value = chk.Checked;
							CheckBox chkAcuUpd = (CheckBox) item.FindControl("chkAcu");
							correspondencia.Dat.Acusado.Value = chkAcuUpd.Checked;
                            //[ggaleano 20-04-2021] Guardar facturable
                            CheckBox chkFactUpd = (CheckBox)item.FindControl("chkFact");
                            correspondencia.Dat.Facturable.Value = chkFactUpd.Checked;
                        }
						correspondencia.PostEdit();
						correspondencia.Adapter.UpdateRow();
						db.Commit();
					}
					catch(Exception e) 
					{
						db.RollBack();
						dgAsignacion.Columns[15].Visible = true;
						item.Cells[15].Text = e.Message;
						return false;
					}
				}
			}
			return true;
		}

	
		protected bool GenerateBindString(object dataItem)
		{
			bool ret = false;

			// if column is null set checkbox.checked = false

			if (dataItem.ToString() == "")
				ret = false;
			else // set checkbox.checked to boolean value in Status column
				ret = (bool)dataItem;
    
			return ret;
		}

		protected void btnProcesado_Click(object sender, System.EventArgs e)
		{
			pnlResultado.Visible = false;
			pnlAsignacion.Visible = true;
			dgAsignacion.Columns[1].Visible = false;
			dgAsignacion.Columns[10].Visible = false;
			dgAsignacion.Columns[11].Visible = true;//Facturable
			dgAsignacion.Columns[12].Visible = true;
			dgAsignacion.Columns[13].Visible = true;
			dgAsignacion.Columns[15].Visible = false;
			lblAsignar.Visible = false;
			ddlAsignacion.Visible = false;
			btnAsignacion.Visible = false;
			btnGrabar.Visible = false;
			btnGrabarProcesado.Visible = true;
			chkMarcar.Text = "Marcar todo";
			chkMarcar.Checked = false;
			lblAreaAsignacion.Visible = false;
			ddlAreaAsignacion.Visible = false;
		}

		protected void btnGrabarProcesado_Click(object sender, System.EventArgs e)
		{
			if (this.GrabarAsignacion(1)) {this.Buscar();};
		}

		protected void ddlArea_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		   //Label6.Text =  ddlArea.SelectedIndex.ToString();

			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			if ( ddlArea.SelectedIndex > 0)
			{
				Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab(db);
				inTB.NewRow();
				inTB.Dat.Logico.Value = true;
				inTB.Dat.Entero.Value = ddlArea.SelectedValue;
				inTB.PostNewRow();
				Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect(inTB);
				ddlFuncionarioFiltro.Fill( seFuncionario.Table, true );
			}
			else
			{
				Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
				ddlFuncionarioFiltro.Fill( seFuncionario.Table, true );
			}
		}

		private object GetFilter_Bool( string cadena )
		{
			#region Filtro Nulo
			if( cadena.Trim() == "" )
			{
				return DBNull.Value;
			}
			#endregion  Filtro Nulo
			bool ret = false;
			switch( cadena.ToUpper() )
			{
				case "SI":
				case "TRUE":
				case "1":
					ret = true;
					break;
				case "NO":
				case "False":
				case "0":
					ret = false;
					break;
			}
			return ret;
		}

		protected void ddlAreaAsignacion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Berke.Libs.Base.Helpers.AccesoDB db		= new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName	= WebUI.Helpers.MyApplication.CurrentDBName;
			db.ServerName	= WebUI.Helpers.MyApplication.CurrentServerName;

			if ( ddlAreaAsignacion.SelectedIndex > 0)
			{
				Berke.DG.ViewTab.ParamTab inTB = new Berke.DG.ViewTab.ParamTab(db);
				inTB.NewRow();
				inTB.Dat.Logico.Value = true;
				inTB.Dat.Entero.Value = ddlAreaAsignacion.SelectedValue;
				inTB.PostNewRow();
				Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect(inTB);
				ddlAsignacion.Fill( seFuncionario.Table, true );
			}
			else
			{
				Berke.DG.ViewTab.ListTab seFuncionario = Berke.Marcas.UIProcess.Model.Funcionario.ReadForSelect();
				ddlAsignacion.Fill( seFuncionario.Table, true );
			}
		}

		protected void Check_Clicked(object sender, System.EventArgs e)
		{
			CheckBox chk = (CheckBox)sender;
			DataGridItem dgItem = (DataGridItem)chk.NamingContainer;
			CheckBox chkSelector = (CheckBox)dgItem.FindControl("chkSel");
			chkSelector.Checked = chk.Checked;
		}


		protected void btnGenDoc_Click(object sender, System.EventArgs e)
		{
			/*[ggaleano 03/03/2013] Las dos lineas siguientes permiten que los caracteres latinos como acentos, letra "ñ" y otros
			 * puedan ser mostrados correctamente en los archivos generados*/
			HttpContext.Current.Response.Charset = "utf-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.word";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dgResult.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
		}

		protected void btnGenXls_Click(object sender, System.EventArgs e)
		{
			/*[ggaleano 03/03/2013] Las dos lineas siguientes permiten que los caracteres latinos como acentos, letra "ñ" y otros
			 * puedan ser mostrados correctamente en los archivos generados*/
			HttpContext.Current.Response.Charset = "utf-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dgResult.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();	
		}

		/*[ggaleano 03/03/2013] El siguiente método sobreescribe el método de verificar el renderizado de los controles sólo para
		 * esta página de tal manera a que el contenido de la grilla de resultados puedas ser renderizado a HTML y de ésta forma
		 * hacer posible la exportación a Word y Excel de los datos contenidos en la misma*/
		public override void VerifyRenderingInServerForm(Control control) 
		{
			return;
		}


	} // end class CorrespConsulta
} // end namespace Berke.Marcas.WebUI.Home










