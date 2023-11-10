using System;

namespace Berke.Marcas.BizActions
{
	using Berke.Libs.Base.Helpers;
	using Framework.Core;
	using Framework.Channels;
	using Berke.DG;
	using Berke.Libs;
	using System.Web.Mail;
	/// <summary>
	///					Esta clase contiene metodos estaticos que implementan reglas de 
	///					negocios y que son accesibles directamente desde las "actions"
	/// </summary>
	public class Lib
	{
		#region Constantes
		const string smtpServer	= "192.168.200.6";

		#endregion 

		#region FechaMasPlazo
		/* Calcula la fecha que será cuando haya pasado un "plazo" dado, pertiendo de "fechaIni" */

		public static DateTime FechaMasPlazo( DateTime fechaIni, int tramiteSitID, AccesoDB db )
		{
			Berke.DG.DBTab.Tramite_Sit trmSitID = new Berke.DG.DBTab.Tramite_Sit( db );
			trmSitID.Adapter.ReadByID( tramiteSitID );
			return FechaMasPlazo( fechaIni, trmSitID.Dat.Plazo.AsInt, trmSitID.Dat.UnidadID.AsInt, db );
		}

		public static DateTime FechaMasPlazo( DateTime fechaIni, int plazo, int unidadID, AccesoDB db )
		{
			DateTime fechaFin = fechaIni;
			switch( unidadID )
			{
			
				case (int) Berke.Libs.Base.GlobalConst.Unidad.DIAS_HABILES	:	// Días hábiles	dd
					Berke.DG.DBTab.Feriado feriado = new Berke.DG.DBTab.Feriado( db );
					
					if ( plazo > 0 )
					{
						#region Plazos Positivos 
						for (int i=1; i<=plazo; i++)
						{
							fechaFin = fechaIni.AddDays(i);
							if (fechaFin.DayOfWeek == DayOfWeek.Saturday || fechaFin.DayOfWeek == DayOfWeek.Sunday)
							{
								plazo++;
							}
							else
							{
								feriado.Dat.Fecha.Filter = fechaFin;
								feriado.Adapter.ReadAll();
								if( feriado.RowCount > 0 )
								{
									plazo++;
								}
							}
						} // end for
						#endregion Plazos Positivos 
					}
					else
					{
						#region Plazos Negativos 
						for (int i=-1; i>=plazo; i--)
						{
							fechaFin = fechaIni.AddDays(i);
							if (fechaFin.DayOfWeek == DayOfWeek.Saturday || fechaFin.DayOfWeek == DayOfWeek.Sunday)
							{
								plazo--;
							}
							else
							{
								feriado.Dat.Fecha.Filter = fechaFin;
								feriado.Adapter.ReadAll();
								if( feriado.RowCount > 0 )
								{
									plazo--;
								}
							}
						} // end for
						#endregion Plazos Negativos
					}
					fechaFin = fechaIni.AddDays( Convert.ToDouble( plazo ) );
					break;
				case (int) Berke.Libs.Base.GlobalConst.Unidad.MES	:	// 	Mes	mm
					fechaFin = fechaIni.AddMonths( plazo );
					break;
				case (int) Berke.Libs.Base.GlobalConst.Unidad.ANHO	:	// 	Año	aaaa
					fechaFin = fechaIni.AddYears( plazo );
					break;
				case (int) Berke.Libs.Base.GlobalConst.Unidad.DIAS_CALENDARIO	:	// 	Días calendario	dc
					fechaFin = fechaIni.AddDays( Convert.ToDouble( plazo ) );
					break;
			}

			
			return fechaFin;
		}
		#endregion FechaMasPlazo

		#region Notificar 
		public static void Notificar( int notificacionID, string body , AccesoDB db )
		{
			Notificar( notificacionID, body, DateTime.Today, db );
		}

		public static void Notificar( int notificacionID, string body, DateTime fechaAviso , AccesoDB db )
		{
			string from			= "SGE";
		
			Berke.DG.DBTab.Notificacion notif = new Berke.DG.DBTab.Notificacion( db );
			notif.Adapter.ReadByID( notificacionID );
			if( notif.Dat.Activo.AsBoolean )
			{
				string subject = "Notif.["+notificacionID.ToString()+"] " + notif.Dat.Descrip.AsString;

				#region Envio de Mail
				string[] destino = notif.Dat.Mail_Destino.AsString.Split(new Char[]{';',','});

				foreach( string to in destino )
				{
					if( to != "" && to != null ) 
					{
                        Berke.Libs.Boletin.Libs.Utils.SendMail(String.Empty, to, String.Empty, String.Empty, subject, body);
                        //SendMail( from, to, subject, body  );
					}
				}// end foreach
				#endregion Envio de Mail

				#region Grabar Aviso
				destino = notif.Dat.Func_Destino.AsString.Split(new Char[]{';',','});

				foreach( string to in destino )
				{
					if( to != "" && to != null )
					{
						
						#region Funcionario
						Berke.DG.ViewTab.vFuncionario fun = new Berke.DG.ViewTab.vFuncionario( db );
						fun.Dat.Usuario.Filter = to;
						fun.Adapter.ReadAll();
						#endregion 
                        
						Berke.DG.DBTab.Aviso aviso = new Berke.DG.DBTab.Aviso( db );
	
						#region Asignar valores a Aviso

						string indicacion = DateTime.Now.ToString("dd/MM/yy" )+" " +
							DateTime.Now.ToString("t") + " SGE - > " +
							fun.Dat.Funcionario.AsString + @" - *Notificación*
";
						aviso.NewRow(); 
						//						aviso.Dat.ID			.Value = DBNull.Value;				//int PK  Oblig.
						aviso.Dat.Remitente		.Value = DBNull.Value;				//int
						aviso.Dat.Destinatario	.Value = fun.Dat.ID.AsInt;			//int
						aviso.Dat.FechaAlta		.Value = DateTime.Now;				//datetime
						aviso.Dat.FechaAviso	.Value = fechaAviso;				//datetime
						aviso.Dat.Pendiente		.Value = true;						//bit
						aviso.Dat.Asunto		.Value = subject;					//nvarchar
						aviso.Dat.Contenido		.Value = body;						//nvarchar
						aviso.Dat.Indicaciones	.Value =  indicacion;
								
						aviso.PostNewRow(); 
						#endregion Asignar valores a Aviso
						int avisoID = aviso.Adapter.InsertRow(); 
						avisoID = 0;
					}
				}// end foreach
				#endregion Grabar Aviso

			}// end if
		}
		#endregion Notificar 

		#region GrabarAviso
		public static void GrabarAviso( string usuario, string indicacion , string subject, string body, AccesoDB db )
		{
		 
			#region Funcionario
			Berke.DG.ViewTab.vFuncionario fun = new Berke.DG.ViewTab.vFuncionario( db );
			fun.Dat.Usuario.Filter = usuario;
			fun.Adapter.ReadAll();
			#endregion 
                        
			Berke.DG.DBTab.Aviso aviso = new Berke.DG.DBTab.Aviso( db );
	
			#region Asignar valores a Aviso

			indicacion = DateTime.Now.ToString("dd/MM/yy" )+" " +
				DateTime.Now.ToString("t") + " SGE - > " +
				fun.Dat.Usuario.AsString + " * " + indicacion;

			aviso.NewRow(); 
			//						aviso.Dat.ID			.Value = DBNull.Value;				//int PK  Oblig.
			aviso.Dat.Remitente		.Value = DBNull.Value;				//int
			aviso.Dat.Destinatario		.Value = fun.Dat.ID.AsInt;			//int
			aviso.Dat.FechaAlta		.Value = DateTime.Now;				//datetime
			aviso.Dat.FechaAviso		.Value = DateTime.Now;				//datetime
			aviso.Dat.Pendiente		.Value = true;						//bit
			aviso.Dat.Asunto			.Value = subject;					//nvarchar
			aviso.Dat.Contenido		.Value = body;						//nvarchar
			aviso.Dat.Indicaciones		.Value =  indicacion;
								
			aviso.PostNewRow(); 
			#endregion Asignar valores a Aviso
			int avisoID = aviso.Adapter.InsertRow(); 
		}
		#endregion GrabarAviso

		#region NewLine
		public static string NewLine
		{
			get
			{
				string nl = @"
";
				return nl;
			}
		}
		#endregion

		#region SendMail 
		public static void SendMail( string from, string to, string subject, string body  )
		{
			System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
			mail.From		= from;
			mail.To			= to;
			mail.Subject	= subject;
			mail.Body		= body;			
			System.Web.Mail.SmtpMail.SmtpServer = smtpServer ;
			System.Web.Mail.SmtpMail.Send( mail );
		}
		/*Envio con attachments mbaez*/
		public static void SendMail( string from, string to, string cc, string subject, string body, string attachPath )
		{
			System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
			mail.From		= from;
			mail.To			= to;
			mail.Cc			= cc;
			mail.Subject	= subject;
			mail.Body		= body;	

			// Crea un adjunto
			MailAttachment attachment = new	MailAttachment(attachPath);
			mail.Attachments.Add(attachment);

			System.Web.Mail.SmtpMail.SmtpServer = smtpServer ;
			System.Web.Mail.SmtpMail.Send( mail );
		}
		#endregion SendMail 

		#region ExpedienteRoorID
		/*
		  * Dado un ExpedienteID, devuelve el ID del Primer expediente ancestro
		  * Si no tiene ancestro,  entonces devuelve el mismo ExpedienteID
		  * 
		 */
		public static int ExpedienteRootID( int expedienteID , AccesoDB db )
		{
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			int rootID = 0;
			expe.Adapter.ReadByID( expedienteID );
			if( expe.Dat.ExpedienteID.IsNull && expe.RowCount == 1 )
			{
				rootID = expedienteID;
			}
			else
			{
				while( ! expe.Dat.ExpedienteID.IsNull  && expe.RowCount == 1  )
				{
					rootID = expedienteID;			
					expedienteID = expe.Dat.ExpedienteID.AsInt;
					expe.Adapter.ReadByID( expedienteID );
					if( expe.Dat.ExpedienteID.IsNull && expe.RowCount == 1 )
					{
						rootID = expedienteID;
					}
				}
			}
			return rootID;
		}
		#endregion ExpedienteRoorID
		
		#region ExpedienteJerarquia

		public static string ExpedienteJerarquia( int ExpedienteID , AccesoDB db )
		{
			Berke.Html.HtmlTextFormater str = new Berke.Html.HtmlTextFormater();
			str.Size = "-3";
			str.Bold = false;
			 
 
			string buf="";
			int rootID = ExpedienteRootID(ExpedienteID, db );

			buf="<Table cellspacing=\"0\" cellpadding=\"2\"><tr><td class=\"jrk_header\">Tramite</td>" +
				"<td class=\"jrk_header\">Acta</td>"+
				"<td class=\"jrk_header\">Registro</td>"+
				"<td class=\"jrk_header\">H.I.</td>"+
				"<td class=\"jrk_header\">Exped.ID</td></tr>";

			buf += iterItem( rootID, 1, ExpedienteID, db);
			buf+= @"</table>";

			return buf;

		}

		public static string space( int cantEspacios )
		{
			string buf = "";
			for( int i= 0 ; i < cantEspacios; i++ )
			{
				buf+="&nbsp;";
			}
			return buf;
		}

		private static string iterItem( int rootID, int nivel, int expeOrigID, AccesoDB db )
		{
			Berke.Html.HtmlTextFormater num = new Berke.Html.HtmlTextFormater();
			num.Size = "-3";
			num.Bold = true;
			Berke.Html.HtmlTextFormater str = new Berke.Html.HtmlTextFormater();
			str.Size = "-3";
			str.Bold = false;

			string registro	= "";
			string acta		= "";
			string tram		= "";
			string exped		= "";
			string buf = "";
			string descrip = "";
			string hi	= "";
			#region clases de Acceso a Datos
			Berke.DG.DBTab.Expediente expe = new Berke.DG.DBTab.Expediente( db );
			Berke.DG.DBTab.Tramite_Sit trmSit = new Berke.DG.DBTab.Tramite_Sit( db );
			Berke.DG.DBTab.Tramite	trm = new Berke.DG.DBTab.Tramite( db );
			Berke.DG.DBTab.Situacion sit = new Berke.DG.DBTab.Situacion( db );
			Berke.DG.DBTab.Marca mar = new Berke.DG.DBTab.Marca( db );
			Berke.DG.DBTab.Poder pod = new Berke.DG.DBTab.Poder( db );
			Berke.DG.DBTab.Proceso proc = new Berke.DG.DBTab.Proceso( db );
			Berke.DG.DBTab.Clase clase = new Berke.DG.DBTab.Clase( db );
			Berke.DG.DBTab.PoderTipo podTipo = new Berke.DG.DBTab.PoderTipo( db );
			Berke.DG.DBTab.Expediente_Documento expeDoc = new Berke.DG.DBTab.Expediente_Documento( db );
			Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento( db );
			Berke.DG.DBTab.DocumentoTipo docTipo = new Berke.DG.DBTab.DocumentoTipo( db );
			Berke.DG.DBTab.MarcaRegRen marRegRen = new Berke.DG.DBTab.MarcaRegRen( db );
			Berke.DG.DBTab.OrdenTrabajo ot = new Berke.DG.DBTab.OrdenTrabajo( db );
			#endregion clases de Acceso a Datos

			expe.Adapter.ReadByID( rootID );
			#region Determinar estilo
			//string resaltado = ( expeOrigID == rootID ) ? "<b>" : "";
			//string finResaltado = ( expeOrigID == rootID ) ? "</b>" : "";
			string estilo = "";
			if( expeOrigID == rootID )
			{
				//str.Bold = true;
				estilo = "jrk_expe_actual";
			}
			else
			{
				//str.Bold = false;
				estilo  = "jrk_expe";
			}
			string expeStr = rootID.ToString();
			expeStr = Berke.Libs.WebBase.Helpers.HtmlGW.OpenPopUp_Link("MarcaDetalleL.aspx",
				expeStr,expeStr,"expeID");
			#endregion Determinar estilo

			#region Leer datos de Expe
			trmSit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
			trm.Adapter.ReadByID( trmSit.Dat.TramiteID.AsInt );
			proc.Adapter.ReadByID( trm.Dat.ProcesoID.AsInt );
			sit.Adapter.ReadByID( trmSit.Dat.SituacionID.AsInt );

			ot.Adapter.ReadByID( expe.Dat.OrdenTrabajoID.Value );

			registro = "";
			if( proc.Dat.Abrev.AsString == "MARC" )
			{
				#region Registro
				marRegRen.Dat.ExpedienteID.Filter = rootID;
				marRegRen.Adapter.ReadAll();
				if( marRegRen.Dat.Registro.AsString.Trim() != "" )
				{
					//					 registro = space(5) + "Registro: "+ num.Html( marRegRen.Dat.Registro.AsString );
					registro = "<td> <span class=\""+estilo+"\">" + marRegRen.Dat.Registro.AsString + "</span></td>";
				}
				else
				{
					registro = "<td> <span class=\""+estilo+"\">&nbsp;</span></td>";
				}
				#endregion Registro

				mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
				clase.Adapter.ReadByID( mar.Dat.ClaseID.AsInt );
				descrip = mar.Dat.Denominacion.AsString+" ("+clase.Dat.DescripBreve.AsString + ")";
			}
			#endregion Leer datos de Expe

			#region Acta
			//			 acta = space(5) + "Acta: "+ num.Html( expe.Dat.Acta.AsString );
			acta = "<td> <span class=\""+estilo+"\">" + expe.Dat.Acta.AsString + "</span></td>";
			#endregion Acta

			#region HI
			hi = "<td> <span class=\""+estilo+"\">" + ot.Dat.OrdenTrabajo.AsString + "</span></td>";

			#endregion HI

			#region tramite
			tram = "<td class=\""+ estilo +"\" ><span  style=\"margin-left:"+(nivel*8)+"\">"+ trm.Dat.Descrip.AsString + "</span></td>";
			#endregion tramite
			 
			#region Exped
			exped = "<td> <span class=\""+estilo+"\">" + expeStr +	digitalDocPath( expe.Dat.ActaAnio.AsInt,
				expe.Dat.ActaNro.AsInt,	(int) Berke.Libs.Base.GlobalConst.DocumentoTipo.HOJA_DESCRIPTIVA ) +"</span></td>"; // 5-Hoja Descriptiva
			#endregion Exped

			buf+= "<tr>" + tram + acta + registro + hi + exped + "</tr>";
				 

			#region Documento y Escritos Varios
			//			 expeDoc.Dat.ExpedienteID.Filter = expe.Dat.ID.AsString;
			//			 expeDoc.Adapter.ReadAll();
			//			 if( expeDoc.RowCount > 0 )
			//			 {
			//				 for( expeDoc.GoTop(); ! expeDoc.EOF; expeDoc.Skip() )
			//				 {
			//					 doc.Adapter.ReadByID( expeDoc.Dat.DocumentoID.AsInt );
			//					 docTipo.Adapter.ReadByID( doc.Dat.DocumentoTipoID.AsInt );
			//					 if( docTipo.Dat.EsEscritoVario.AsBoolean )
			//					 {
			//						 buf+= space((nivel+1) * 4) + docTipo.Dat.Descrip.AsString + space(5)+docTipo.Dat.IdentifNombre.AsString+ ":"+
			//							 doc.Dat.IdentificadorNro.AsString + "/"+ doc.Dat.IdentificadorAnio.AsString+space(5) + 
			//							 " Docum.ID:" + doc.Dat.ID.AsString +
			//							 space(3) +
			//							 digitalDocPath( doc.Dat.IdentificadorAnio.AsInt,
			//							 doc.Dat.IdentificadorNro.AsInt,
			//							 0  ) +"<br>"; // 5-Hoja Descriptiva
			//					 }
			//
			//				 }
			//			 }
			#endregion Documento y Escritos Varios

			#region Expedientes Dependientes
			expe.Dat.ExpedienteID.Filter	= rootID;
			expe.Dat.OrdenTrabajoID.Order = 1;
			expe.Dat.ActaAnio.Order = 2;
			expe.Dat.ActaNro.Order = 3;
			expe.Adapter.ReadAll();
			for( expe.GoTop(); !expe.EOF; expe.Skip() )
			{
				#region Leer datos de Expe
				trmSit.Adapter.ReadByID( expe.Dat.TramiteSitID.AsInt );
				trm.Adapter.ReadByID( trmSit.Dat.TramiteID.AsInt );
				proc.Adapter.ReadByID( trm.Dat.ProcesoID.AsInt );
				sit.Adapter.ReadByID( trmSit.Dat.SituacionID.AsInt );
				if( proc.Dat.Abrev.AsString == "MARC" )
				{

					mar.Adapter.ReadByID( expe.Dat.MarcaID.AsInt );
					clase.Adapter.ReadByID( mar.Dat.ClaseID.AsInt );
					descrip = mar.Dat.Denominacion.AsString+" ("+clase.Dat.DescripBreve.AsString + ")";
				}
				#endregion Leer datos de Expe

				//				buf+= space((nivel+1) * 2) + "Acta:"  +expe.Dat.Acta.AsString + " * "+ descrip + " *ExpeID:" + rootID.ToString() +nl();
				buf+= iterItem( expe.Dat.ID.AsInt, nivel + 1, expeOrigID, db );
			}
			#endregion Expedientes Dependientes

			return buf;
		}// Fin iterItem

		#endregion ExpedienteJerarquia

		#region DocumentoCampos 
		public static string DocumentoCampos( int documentoID, AccesoDB db )
		{
			string buf = "";
			 
			#region clases de Acceso a Datos
			Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento( db );
			Berke.DG.DBTab.DocumentoTipo docTipo = new Berke.DG.DBTab.DocumentoTipo( db );
			Berke.DG.DBTab.DocumentoCampo docCampo = new Berke.DG.DBTab.DocumentoCampo( db );
			Berke.DG.DBTab.DocumentoTipoCampo dtc = new Berke.DG.DBTab.DocumentoTipoCampo(db);
			#endregion clases de Acceso a Datos

			#region Leer Documento
			doc.Adapter.ReadByID( documentoID );
			#endregion

			#region Leer Tipo Documento
			docTipo.Adapter.ReadByID( doc.Dat.DocumentoTipoID.AsInt );
			#endregion

			if( ! docTipo.Dat.EsEscritoVario.AsBoolean )
			{
				#region Datos de cabecera
				buf+= docTipo.Dat.Descrip.AsString + space(5)+docTipo.Dat.IdentifNombre.AsString+ ":"+
					doc.Dat.IdentificadorNro.AsString + "/"+ doc.Dat.IdentificadorAnio.AsString+space(5) + 
					" Docum.ID:" + doc.Dat.ID.AsString + space(3) +
					digitalDocPath( doc.Dat.IdentificadorAnio.AsInt,
					doc.Dat.IdentificadorNro.AsInt,
					doc.Dat.DocumentoTipoID.AsInt ) + "<br>";
				#endregion

				#region Leer DocumentoCampo
				docCampo.Dat.DocumentoID.Filter = doc.Dat.ID.AsInt;
				docCampo.Adapter.ReadAll();
				for( docCampo.GoTop(); ! docCampo.EOF; docCampo.Skip() )
				{
					string campo = docCampo.Dat.Campo.AsString;

					#region si es numero obtiene nombre de campo
					string let = campo.Substring(0,1);
					bool esNumero = false;
					switch ( let )
					{
						case "0" :
						case "1" :
						case "2" :
						case "3" :
						case "4" :
						case "5" :
						case "6" :
						case "7" :
						case "8" :
						case "9" :
							esNumero = true;
							break;
						default :
							esNumero = false;
							break;						
					}
					if( esNumero )
					{
						dtc.Adapter.ReadByID( Convert.ToInt32( campo ) );
						campo = dtc.Dat.Campo.AsString;
					}
					#endregion

					buf+=   space(5) + campo + ": "+docCampo.Dat.Valor.AsString+ "<br>";
				}
				
				#endregion	Leer DocumentoCampo
			}// end if
			return buf;
		}// end DocumentoCampos()
		#endregion  DocumentoCampos 

		#region EscritoVarioCampos
		public static string EscritoVarioCampos( int documentoID, AccesoDB db )
		{
			string buf = "";
			 
			#region clases de Acceso a Datos
			Berke.DG.DBTab.Documento doc = new Berke.DG.DBTab.Documento( db );
			Berke.DG.DBTab.DocumentoTipo docTipo = new Berke.DG.DBTab.DocumentoTipo( db );
			Berke.DG.DBTab.DocumentoCampo docCampo = new Berke.DG.DBTab.DocumentoCampo( db );
			Berke.DG.DBTab.DocumentoTipoCampo dtc = new Berke.DG.DBTab.DocumentoTipoCampo(db);
			#endregion clases de Acceso a Datos

			#region Leer Documento
			doc.Adapter.ReadByID( documentoID );
			#endregion

			#region Leer Tipo Documento
			docTipo.Adapter.ReadByID( doc.Dat.DocumentoTipoID.AsInt );
			#endregion

			if( docTipo.Dat.EsEscritoVario.AsBoolean ) 
			{
				#region Datos de cabecera
				string fecha = doc.Dat.Fecha.IsNull ? "Fecha:?" : "Fecha:"+ doc.Dat.Fecha.AsString;
				buf+= docTipo.Dat.Descrip.AsString + space(5) + fecha + space(5)+docTipo.Dat.IdentifNombre.AsString+ ":"+
					doc.Dat.IdentificadorNro.AsString + "/"+ doc.Dat.IdentificadorAnio.AsString+space(5) + 
					" Docum.ID:" + doc.Dat.ID.AsString + "<br>";
				#endregion

				#region Leer DocumentoCampo
				docCampo.Dat.DocumentoID.Filter = doc.Dat.ID.AsInt;
				docCampo.Adapter.ReadAll();
				for( docCampo.GoTop(); ! docCampo.EOF; docCampo.Skip() )
				{
					string campo = docCampo.Dat.Campo.AsString;

					#region si es numero obtiene nombre de campo
					string let = campo.Substring(0,1);
					bool esNumero = false;
					switch ( let )
					{
						case "0" :
						case "1" :
						case "2" :
						case "3" :
						case "4" :
						case "5" :
						case "6" :
						case "7" :
						case "8" :
						case "9" :
							esNumero = true;
							break;
						default :
							esNumero = false;
							break;						
					}
					if( esNumero )
					{
						dtc.Adapter.ReadByID( Convert.ToInt32( campo ) );
						campo = dtc.Dat.Campo.AsString;
					}
					#endregion

					buf+=   space(5) + campo + ": "+docCampo.Dat.Valor.AsString+ "<br>";
				}
				
				#endregion	Leer DocumentoCampo
			}// end if
			return buf;
		}// end DocumentoCampos()
		#endregion  EscritoVarioCampos 

		#region No se utiliza
		#region digitalDocPath
		
		public static string digitalDocPath( int pAnio, int pNumero, int tipoDocID )
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

			switch( tipoDocID )
			{
				case 0 : //	Escrito Vario
					fileTemplate = @"\\trinity\Ofdig$\HojasDescriptivas\Varias\{0}\TIF\{1}.tif";
					break;
				case 4 : //	Correspondencia Orden
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 5 : //	Hoja Descriptiva REG y REN
					fileTemplate = @"\\trinity\Ofdig$\HojasDescriptivas\Marcas\{0}\TIF\{1}.tif";
					break;


				case 6 : //	Publicación	
					fileTemplate = @"\\trinity\Ofdig$\Publicaciones\Publicacion\{0}\TIF\{1}.tif";
					break;
				case 8 : //	Titulo de Marca	
					fileTemplate = @"\\trinity\Ofdig$\Titulos\Titulo\{0}\TIF\{1}.tif";
					break;
				case 9 : //	Poder
					fileTemplate = @"\\trinity\Ofdig$\Poderes\Poder\{0}\TIF\{1}.tif";
					if ( pAnio.ToString()=="0" ) 
					{
					  fileTemplate = @"\\trinity\Ofdig$\Poderes\Poder\TIF\{0}.tif";
					} 
					
					break;
			}

			if( fileTemplate == "" )
			{
				return "";
			}

			
			//string anchorTemplate = @"<A href=""{0}"">&nbsp;&nbsp;{1} </a>";
			//string anchorTemplate = @"<a href=""{0}"">&nbsp;&nbsp;{1} <img border=0 src='../tools/imx/tif.gif'> </a>";
			string anchorTemplate = @"&nbsp;&nbsp;<a href=""{0}""><img alt='Ver Documento'  border=0 src='../tools/imx/tif.gif'> </a>";
			
			#region Llenar numero con ceros a la izquierda
			if( numero.Length < 5 && numero.Length > 0 )
			{
				numero = ((string)"00000").Substring(0, 5 - numero.Length) + numero;
			}
			#endregion

			
			string arch, referencia ;
			arch = string.Format( fileTemplate, pAnio.ToString(), numero );
			
			/* [11-04-2007]
			  * La carpeta Poderes no esta dividido por año */
			if ( tipoDocID.ToString()=="9" && pAnio.ToString()=="0") 
			{
				arch = string.Format( fileTemplate, numero );
			}

			System.IO.FileInfo inf = new System.IO.FileInfo(arch);
			if( ! inf.Exists )
			{ 
				//return string.Format( anchorTemplate, arch, "" );;
				referencia= "";
			}
			else
			{
				//referencia = string.Format( anchorTemplate, arch , "Ver Doc." );
				referencia = string.Format( anchorTemplate, arch );
			}
			return referencia;
			
		}
		#endregion digitalDocPath
		#endregion No se utiliza

		#region Traducir
		public static string traducir( string texto, int idiomaID ,AccesoDB db )
		{
			Berke.DG.DBTab.Traduccion traduc = new Berke.DG.DBTab.Traduccion();
			traduc.InitAdapter( db );
			traduc.Dat.Texto.Filter = texto;
			traduc.Dat.IdiomaID.Filter = idiomaID;
			traduc.Adapter.ReadAll();

			return traduc.Dat.Traducido.AsString;
		}
		#endregion Traducir

		#region DateString
		public static string DateString( DateTime fecha, int idiomaID )
		{
			string pattern = "";
			switch( idiomaID )
			{		
				case (int) Berke.Libs.Base.GlobalConst.Idioma.INGLES : // Ingles
					pattern = "MMM/dd/yyyy";
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.ESPANOL : // Español
					pattern = "dd/MMM/yyyy";
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.ALEMAN : // Aleman
					pattern = "dd-MMM-yyyy";
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.FRANCES : // Frances
					pattern = "dd/MMM/yyyy";
					break;
				default :
					pattern = "dd/MMM/yyyy";
					break;
			}
			return DateString( fecha, idiomaID, pattern  );
		}
		public static string DateString( DateTime fecha, int idiomaID, string pattern )
		{
			System.Globalization.CultureInfo cul;

			switch( idiomaID ){		
				case (int) Berke.Libs.Base.GlobalConst.Idioma.INGLES : // Ingles
					cul = new System.Globalization.CultureInfo("en-US", false);
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.ESPANOL : // Español
					cul = new System.Globalization.CultureInfo("es-ES", false);
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.ALEMAN : // Aleman
					cul = new System.Globalization.CultureInfo("de-DE", false);
					break;
				case (int) Berke.Libs.Base.GlobalConst.Idioma.FRANCES : // Frances
					cul = new System.Globalization.CultureInfo("fr-FR", false);
					break;
				default :
					cul = new System.Globalization.CultureInfo("en-US", false);
					break;
			}
		
			string x = fecha.ToString(pattern, cul );
			return x;
		}
		#endregion DateString

//		 #region GenerarReporte "Pedido" 
//		 public static String htmlPedidoString(int pedidoID, AccesoDB db )
//		 {
//			 Berke.DG.BusqFonDG inDS = new BusqFonDG();
//
//			 #region Obtener Plantilla
//
//			 CodeGenerator cg = new CodeGenerator();
//
//			 string clavePlantilla = "BusqFon_01";
//			 Berke.DG.DBTab.DocumentoPlantilla plantilla = new Berke.DG.DBTab.DocumentoPlantilla( db );
//
//			 plantilla.Dat.Clave.Filter = clavePlantilla;
//			 plantilla.Dat.IdiomaID.Filter = 1; // 
//			 plantilla.Adapter.ReadAll();
//			 if( plantilla.RowCount < 1 ) throw new Exception("Plantilla [" + clavePlantilla + "] No registrada");
//
//			 cg.Inicializar( plantilla.Dat.PlantillaHTML.AsString );
//							
//			 CodeGenerator pagina = cg["Pagina"];
//
//			 CodeGenerator tabla = pagina["Tabla"];
//
//			 CodeGenerator fila = tabla["Fila"];
//				
//			 #endregion Obtener Plantilla
//
//			 Berke.DG.DBTab.BusqFonDet detalle = new Berke.DG.DBTab.BusqFonDet();
//			 detalle.InitAdapter( db );
//			 detalle.Dat.BusqFonID.Filter = pedidoID;
//			 detalle.Adapter.ReadAll();
//			
//			 tabla.clearText();
//			 for( detalle.GoTop(); !detalle.EOF; detalle.Skip() )
//			 {
//				 int detalleID = detalle.Dat.ID.AsInt;
//				 string patron = detalle.Dat.Patron.AsString;
//
//				 Berke.DG.DBTab.BusqFonResul resul = new Berke.DG.DBTab.BusqFonResul();
//				 resul.InitAdapter( db );
//				 resul.Dat.BusqFonDetID.Filter = detalleID;
//				 resul.Adapter.ReadAll();
//				 resul.Dat.Puntaje.Order = -1;
//				 resul.Sort();
//
//				 fila.clearText();
//				 for ( resul.GoTop(); ! resul.EOF ; resul.Skip() ) 
//				 {
//					 if ( resul.Dat.Imprimir.AsBoolean )	
//					 {
//						 fila.copyTemplateToBuffer();
//						 fila.replace("#DENOM#", 	resul.Dat.Denominacion.AsString );
//						 fila.replace("#ACTA#", 		resul.Dat.Acta.AsString );
//						 fila.replace("#TIPO#", 		resul.Dat.MarcaTipo.AsString );
//						 fila.replace("#CLASE#", 	resul.Dat.ClaseNumero.AsString );
//						 fila.replace("#VENCIM#", 	resul.Dat.FVencimiento.AsString );
//						 fila.replace("#CLIENTE#", 	resul.Dat.ClienteNombre.AsString );
//						 fila.replace("#PROPIETARIO#",	resul.Dat.Propietarios.AsString  );
//						 fila.replace("#PUNT#",		resul.Dat.Puntaje.AsString  );
//						 fila.addBufferToText();
//					 } // if
//
//				 } // endfor  resul
//
//				 tabla.copyTemplateToBuffer();
//				 tabla.replaceLabel("Fila", fila.Texto );
//				 tabla.replace("#BASE#", patron );
//				 tabla.addBufferToText();
//
//			 }// end for detalle
//
//			 pagina.clearText();
//			 pagina.copyTemplateToBuffer();
//			 pagina.replaceLabel("Tabla", tabla.Texto);
//			 pagina.addBufferToText();
//
//			 cg.clearText();
//			 cg.copyTemplateToBuffer();
//			 cg.replaceLabel("Pagina", pagina.Texto );
//
//			 cg.replace("#TITULO#", "Lista de Marcas Semejante" );
//			 cg.replace("#FECHA#", DateTime.Now.ToString() );
//		
//			 cg.addBufferToText();
//
//
//			 return cg.Texto;
//		 } // end htmlPedidoString()
//		 #endregion GenerarReporte "Pedido"


	}
		 


	
}
