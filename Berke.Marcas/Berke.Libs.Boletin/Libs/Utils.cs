using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Berke.Libs.Boletin.Libs;
using Berke.Libs.Base;
using Berke.Libs.Base.Helpers;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Configuration;

namespace Berke.Libs.Boletin.Libs
{
	/// <summary>
	/// Librería que provee utilidades varias
	/// Autor: Marcos Báez
	/// </summary>
	public class Utils
	{
		public static int STYLE_BOOL  = 0;
		public static int STYLE_TEXT  = 1;
		public static int STYLE_COMBO = 2;
		public static string TAB   = "\t";
		public static string ENTER = "\n";
		public static string FMT_FIELD_SEP = "~";

		public static int NOTIF_PRESENTADA   = 15;
		public static int NOTIF_DESISTIR     = 16;
		public static int NOTIF_CANCEL       = 16;
		public static int NOTIF_BOLETIN      = 17;
		public static int NOTIF_SUSTDATAFLEX = 18;
		public static int NOTIF_PROC_SUCCESS = 20;
		public static int NOTIF_MODIF_SUCCESS = 21;
		public static int NOTIF_MODIF_MARCA =  22;
		public static int NOTIF_PROC_MI     =  23;
		public static int NOTIF_PUBLICACION  =  24;
		public static int NOTIF_ELIM_SOLIICTADA = 36;
		public static int NOTIF_PUB_MARCA_VIG = 38;
		public static int NOTIF_ENTIDADES_CLIENTE = 39;

		private static bool BodyFormat;
        private static Configuration config;


		#region Constantes
		const string smtpServer	= "smtp.office365.com";
        const int smtpPort = 587;
        const string notifFrom = "Notificaciones@berke.com.py";
        const string SOURCE = "WEB";
        const string KEY_SOURCE = "source";
        const string KEY_SMTP = "smtp";
        const string KEY_PORT = "port";
        const string KEY_USER = "user";
        const string KEY_PASSWD = "passwd";
        const string KEY_FROM = "from";
        const string KEY_CC = "cc";
        const string KEY_BCC = "bcc";
        const string KEY_TEXTBODY = "textbody";
        private const int IDDENOMINATIVA = 1;
        private const int IDFIGURATIVA = 2;
        private const int IDMIXTA = 3;
        private const string C_DENOMINATIVA = "D";
        private const string C_FIGURATIVA = "F";
        private const string C_MIXTA = "M";
        private const string Y_ETIQUETA = "Y ETIQUETA";
        private const string Y_SLOGAN = "Y SLOGAN";
        private const string Y_ESLOGAN = "Y ESLOGAN";
        private const string Y_DISENHO = "Y DISEÑO";
        private const string Y_DISENO = "Y DISENO";
        private const string Y_LOGO = "Y LOGO";
        private const string Y_LOGOTIPO = "Y LOGOTIPO";
        private const string SLOGAN_Y_ETIQUETA = "SLOGAN Y ETIQUETA";
        private const string ESLOGAN_Y_ETIQUETA = "ESLOGAN Y ETIQUETA";
		#endregion

		public Utils()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Cambiar caracteres especiales
		public static string cambiarCaracteresEspeciales( string nom )
		{
			char[] aNom = nom.ToCharArray();
			string ret="";
			bool ver = false;
			for( int i = 0; i < aNom.GetLength(0); i++ )
			{
				char c = aNom[i];
				if( c > 127 )
				{	
					#region Reemplazar aNom[i]
					switch( c )
					{
						case (char)128 : aNom[i] = 'Ç' ;ver = true; break; 
						case (char)129 : aNom[i] = 'ü' ;ver = true; break; 
						case (char)130 : aNom[i] = 'é' ;ver = true; break; 
						case (char)131 : aNom[i] = 'â' ;ver = true; break; 
						case (char)132 : aNom[i] = 'ä' ;ver = true; break;
						case (char)133 : aNom[i] = 'à' ;ver = true; break; 
						case (char)135 : aNom[i] = 'ç' ;ver = true; break;
						case (char)136 : aNom[i] = 'ê' ;ver = true; break;
						case (char)137 : aNom[i] = 'ë' ;ver = true; break;
						case (char)138 : aNom[i] = 'è' ;ver = true; break;
						case (char)139 : aNom[i] = 'ï' ;ver = true; break;
						case (char)140 : aNom[i] = 'î' ;ver = true; break;
						case (char)141 : aNom[i] = 'ì' ;ver = true; break;
						case (char)142 : aNom[i] = 'Ä' ;ver = true; break;
						case (char)144 : aNom[i] = 'É' ;ver = true; break;
						case (char)147 : aNom[i] = 'ô' ;ver = true; break;
						case (char)148 : aNom[i] = 'ö' ;ver = true; break;
						case (char)149 : aNom[i] = 'ò' ;ver = true; break;
						case (char)150 : aNom[i] = 'û' ;ver = true; break;
						case (char)151 : aNom[i] = 'ù' ;ver = true; break;
						case (char)152 : aNom[i] = 'ÿ' ;ver = true; break;
						case (char)153 : aNom[i] = 'Ö' ;ver = true; break;
						case (char)154 : aNom[i] = 'Ü' ;ver = true; break;  
						case (char)160 : aNom[i] = 'á' ;ver = true; break;
						case (char)161 : aNom[i] = 'í' ;ver = true; break; 
						case (char)162 : aNom[i] = 'ó' ;ver = true; break;
						case (char)163 : aNom[i] = 'ú' ;ver = true; break; 
						case (char)164 : aNom[i] = 'ñ' ;ver = true; break;
						case (char)165 : aNom[i] = 'Ñ' ;ver = true; break; 
						case (char)166 : aNom[i] = 'ª' ;ver = true; break;
						case (char)167 : aNom[i] = 'º' ;ver = true; break;
						case (char)168 : aNom[i] = '¨' ; // ¿
							ver = true; 
							break;
						case (char)173 : aNom[i] = '¡' ;ver = true; break;
						case (char)181 : aNom[i] = 'Á' ;ver = true; break;
						case (char)182 : aNom[i] = 'Â' ;ver = true; break;
						case (char)183 : aNom[i] = 'À' ;ver = true; break;
						case (char)198 : aNom[i] = 'ã' ;ver = true; break;
						case (char)210 : aNom[i] = 'Ê' ;ver = true; break;
						case (char)212 : aNom[i] = 'È' ;ver = true; break;
						case (char)214 : aNom[i] = 'Í' ;ver = true; break;
						case (char)222 : aNom[i] = 'Ì' ;ver = true; break;
						case (char)224 : aNom[i] = 'Ó' ;ver = true; break; // à
						case (char)225 : aNom[i] = 'á' ;ver = true; break;
						case (char)226 : aNom[i] = 'Ô' ;ver = true; break;
						case (char)227 : aNom[i] = 'Ò' ;ver = true; break;
						case (char)229 : aNom[i] = 'Õ' ;ver = true; break;
						case (char)233 : aNom[i] = 'é' ;
							ver = true; 
							break;
						case (char)237 : aNom[i] = 'í' ;
							ver = true; 
							break;
						case (char)239 : aNom[i] = '´' ;ver = true; break;
						case (char)248 : aNom[i] = '°' ;ver = true; break;

						case (char)57327 : aNom[i] = ' ' ;ver = true; break;


						case (char)255 : aNom[i] = ' ' ;ver = true; break;
  
						case (char)170 : aNom[i] = 'ª' ;ver = true; break;
						case (char)176 : aNom[i] = '°' ;ver = true; break;
						case (char)180 : aNom[i] = '´' ;ver = true; break;
						case (char)186 : aNom[i] = 'º' ;ver = true; break;
						case (char)191 : aNom[i] = '¿' ;ver = true; break;
						case (char)192 : aNom[i] = 'À' ;ver = true; break;
						case (char)193 : aNom[i] = 'Á' ;ver = true; break;
						case (char)196 : aNom[i] = 'Ä' ;ver = true; break;
						case (char)199 : aNom[i] = 'Ç' ;ver = true; break;
						case (char)201 : aNom[i] = 'É' ;ver = true; break;
						case (char)204 : aNom[i] = 'Ì' ;ver = true; break;
						case (char)209 : aNom[i] = 'Ñ' ;ver = true; break;
						case (char)211 : aNom[i] = 'Ó' ;ver = true; break;
						case (char)220 : aNom[i] = 'Ü' ;ver = true; break;
						case (char)221 : aNom[i] = 'Ý' ;ver = true; break;
						case (char)223 : aNom[i] = 'á' ;ver = true; break;
						case (char)235 : aNom[i] = 'ë' ;ver = true; break;
						case (char)241 : aNom[i] = 'ñ' ;ver = true; break;
						case (char)242 : aNom[i] = 'ò' ;ver = true; break;
						case (char)243 : aNom[i] = 'ó' ;ver = true; break;
						case (char)250 : aNom[i] = 'ú' ;ver = true; break;
						case (char)252 : aNom[i] = 'ü' ;ver = true; break;
						case (char)218 : aNom[i] = 'Ú' ;
							ver = true;
							break;
						case (char)205 : aNom[i] = 'Í' ;ver = true; break;
						case (char)9500 :
							i++;
						switch( aNom[i] )
						{
							case (char)230 : aNom[i] = 'Ñ' ;ver = true; break; 
						
							case (char)9474 : aNom[i] = 'ó' ;ver = true; break; 
									
							case (char)235 : aNom[i] = 'É' ;ver = true; break; 
									
							case (char)9618 : aNom[i] = 'ñ' ;ver = true; break; 
							case (char) 237 : aNom[i] = 'á' ;ver = true; break;
							case (char) 169 : aNom[i] = 'º' ;ver = true; break;
						}
							break;
					}
					#endregion Reemplazar aNom[i]
					  
				}
				ret+= aNom[i];
			}// end for
			return ret;
		}
		#endregion

		#region ExisteArchivo
		public static bool existeArchivo( FileInfo fi) 
		{
			// Determina si existe el archivo.
			bool valor;
			if (fi.Exists)
				valor=true;
			else
				valor=false;
			return valor;
		}
		#endregion ExisteArchivo

		#region SemanaDelAño
		public static int semanaDelAnho( DateTime fecha )
		{
			DateTime fd = new DateTime(fecha.Year,1,1,12,0,0,0);
			int dia = fecha.DayOfYear;
			int ddf = (int)fd.DayOfWeek;
			int sem = (int)((dia + ddf - 1) / 7.0 + 1);
			if( ddf > 5 ){sem--;}
			return sem;
		}
		#endregion SemanaDelAño

		#region Obtener funcionario actual
		public static int getCurrentFuncionarioID(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Usuario user = new Berke.DG.DBTab.Usuario(db);
            string curr_user = Berke.Libs.Base.Acceso.GetCurrentUser();	
            user.Dat.Usuario.Filter = curr_user;
			user.Adapter.ReadAll();

            if (user.RowCount == 0)
            {
                user.ClearFilter();
                user.Dat.UsuarioDominio2.Filter = curr_user;
                user.Adapter.ReadAll();
            }

            return user.Dat.ID.AsInt;		

		}
		public static Berke.DG.DBTab.Usuario getCurrentFuncionario(Berke.Libs.Base.Helpers.AccesoDB db)
		{
			Berke.DG.DBTab.Usuario user = new Berke.DG.DBTab.Usuario(db);
			string curr_user = Berke.Libs.Base.Acceso.GetCurrentUser();	
			if (curr_user.Length == 0) 
			{
				throw new Exception("No se ha podido obtener el identificador del usuario actual");
			}
			user.Dat.Usuario.Filter = curr_user;
			user.Adapter.ReadAll();
			return user;		
		}
		#endregion Obtener funcionario actual

		#region Formatear Datagrid
		public static void addColStyle(	System.Windows.Forms.DataGrid dg,
			string tableName,
			string columnName,
			string columnHead,
			int width,
			bool sololectura,
			int style
			)
		{

			DataGridTableStyle Estilo;
			if ( dg.TableStyles.Contains(tableName) )
			{
		
				Estilo = dg.TableStyles[tableName];
			}
			else
			{
				Estilo = new DataGridTableStyle();
				Estilo.MappingName = tableName;
			}	

			DataGridColumnStyle ColEstilo; 
			if( style == STYLE_BOOL ) //Bool
			{
				ColEstilo =  new DataGridBoolColumn();
			}/*
			else if (style == STYLE_COMBO) // Combo
			{
				//ColEstilo = new MyComboColumn(new DataTable(),"name","value",false);
			}*/
			else // STYLE_TEXT
			{
				ColEstilo =  new DataGridTextBoxColumn();
			}

			ColEstilo.MappingName = columnName;
			ColEstilo.HeaderText  = columnHead;
			ColEstilo.Width = width;
			ColEstilo.NullText="";
			ColEstilo.ReadOnly = sololectura;

			Estilo.GridColumnStyles.Add( ColEstilo );

			if ( ! dg.TableStyles.Contains(tableName) )
			{
				dg.TableStyles.Add( Estilo );
			}

			//          Ejemplo de uso			
			//			addColStyle(this.dgDataGroup,"DataSets","Nombre","Nombre", 100, false );
			//			addColStyle(this.dgDataGroup,"DataSets","Generar","Generar", 50, true );
			//			addColStyle(this.dgDataGroup,"DataSets","Descripcion","Descripcion", 250, false );
			Estilo.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;

		}
		#endregion Formatear Datagrid	

		#region Format RichText
		public static void formatRtf(string txt , RichTextBox rtf )
		{
			System.Drawing.Color color_campo = System.Drawing.SystemColors.Highlight;
			System.Drawing.Color color_valor = Color.Black;
	
			#region Inicializar rt
			RichTextGateway rt = new RichTextGateway( rtf );
			rt.Clear();
			rt.Bold = true;
			rt.FontSize =  8;
			rt.FontName  = "Microsoft Sans Serif";			
			#endregion Inicializar rt
			txt = txt.Replace(@"\t", Utils.TAB);
			txt = txt.Replace(@"\n", Utils.ENTER);
			string[] alines = txt.Split( Utils.ENTER.ToCharArray() );
			foreach( string linea in alines )
			{
				if( linea.Trim() == "") continue;
				string[] field = linea.Split( Utils.FMT_FIELD_SEP.ToCharArray() );
				string campo = field[0];
				rt.FontColor = color_campo;
				rt.Bold      = true;
				rt.Write( campo );
				string valor = field[1] + Utils.ENTER + Utils.ENTER;
				rt.FontColor = color_valor;
				rt.Bold      = false;
				rt.Write( valor );
			}
				
		}
		#endregion Format RichText

		#region Notificación
		public static void enviarNotificacion(int notifCode, string msg, Berke.Libs.Base.Helpers.AccesoDB db ) 
		{

            Berke.DG.DBTab.Usuario user = new Berke.DG.DBTab.Usuario(db);

            //[ggaleano 22/02/2017] Ya no se envía como el usuario del sistema, se utiliza la cuenta Notificaciones@berke.com.py
            //cuyos datos se obtienen desde el web.config
            //string curr_user = Berke.Libs.Base.Acceso.GetCurrentUser();	
            //user.Dat.Usuario.Filter = curr_user;
            //user.Adapter.ReadAll();

            //string from = user.Dat.Email.AsString;

            //Berke.DG.DBTab.Notificacion Notificacion = new Berke.DG.DBTab.Notificacion(db);
			

			Berke.DG.DBTab.Notificacion notif = new Berke.DG.DBTab.Notificacion(db);
			notif.Dat.ID.Filter = notifCode;
			//[ggaleano 31/08/2007] Se verifica que el tipo de notificación este activo, es decir,
			//si deben o no enviarse notificaciones
			notif.Dat.Activo.Filter = true;
			notif.Adapter.ReadAll();
			
			//notif.Adapter.ReadByID(notifCode);

			// Enviamos la notiticación sólo si existe destinatario
			if (notif.Dat.Func_Destino.IsNull || notif.Dat.Func_Destino.AsString == "")
				return;
			/*[ggaleano 30/08/2007] Se obtiene separadamente el destinario principal y los secundarios
			 * de los destinatarios, de esta manera se envía un solo mail para notificar a todos los involucrados,
			 * esto agilizará el tiempo de procesamiento.*/
			#region Obtener destinario principal TO
			string [] users = notif.Dat.Func_Destino.AsString.Split(',');
			
			string userMailTo = users[0];
			
			user.ClearFilter();
			user.Dat.Usuario.Filter = userMailTo;
			user.Adapter.ReadAll();

			string MailTo = user.Dat.Email.AsString;
			#endregion Obtener destinario principal TO

			#region Obtener destinatarios secundarios CC
			string userMailCC = "";
		    int i = 0;

			for ( i = 1; i <= users.Length - 1; i++)
			{
				if (userMailCC != "")
				{
					userMailCC = userMailCC + ",";
				}
				userMailCC = userMailCC + users[i];
			}

			string MailCC = "";

			if (userMailCC != "")
			{
				user.ClearFilter();
				user.Dat.Usuario.Filter = ObjConvert.GetFilter(userMailCC);
				user.Adapter.ReadAll();
				
				for (user.GoTop() ; ! user.EOF; user.Skip())
				{
					if (MailCC != "")
					{
						MailCC = MailCC + ", ";
					}
					MailCC = MailCC + user.Dat.Email.AsString;
				}
			}
            #endregion Obtener destinatarios secundarios CC

            //SendMail(from, MailTo, MailCC, "", notif.Dat.Descrip.AsString,msg);				
            SendMail("", MailTo, MailCC, "", notif.Dat.Descrip.AsString, msg);
        }
		#endregion Notificación

		#region SendMail 

        #region Deprecated
        //public static void SendMail( string from, string to, string cc, string subject, string body  )
        //{
        //    System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
        //    mail.BodyFormat = getBodyFormat();
        //    mail.From		= from;
        //    mail.To			= to;
        //    mail.Cc			= cc;
        //    mail.Subject	= subject;
        //    mail.Body		= body;			
        //    System.Web.Mail.SmtpMail.SmtpServer = smtpServer ;
        //    System.Web.Mail.SmtpMail.Send( mail );
        //}
        ///*Envio con attachments mbaez*/
        //public static void SendMail( string from, string to, string cc, string subject, string body, string attachPath )
        //{
        //    System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
        //    mail.BodyFormat = getBodyFormat();
        //    mail.From		= from;
        //    mail.To			= to;
        //    mail.Cc			= cc;
        //    mail.Subject	= subject;
        //    mail.Body		= body;	

        //    // Crea un adjunto
        //    MailAttachment attachment = new	MailAttachment(attachPath);
        //    mail.Attachments.Add(attachment);

        //    System.Web.Mail.SmtpMail.SmtpServer = smtpServer ;
        //    System.Web.Mail.SmtpMail.Send( mail );
        //}
        #endregion Deprecated

        public static void SendMail(string from, string to, string cc = "", string bcc = "", string subject = "", string body = "", string attachPath = "", string username = "", string password = "", string domain = "BERKE")
        {
            string smtp = smtpServer;
            int puerto = smtpPort;
            bool isHtmlBody = true;
                        
            bool sourceWeb = true;
            try
            {
                string source = WebConfigurationManager.AppSettings[KEY_SOURCE].ToString();
            }
            catch (NullReferenceException)
            {
                sourceWeb = false;
            }

            //if ((WebConfigurationManager.AppSettings != null) && (WebConfigurationManager.AppSettings[KEY_SOURCE].ToString() == SOURCE))
            Encrypt_Decrypt crypt = new Encrypt_Decrypt();

            if (sourceWeb)
            {
                smtp = WebConfigurationManager.AppSettings[KEY_SMTP].ToString();
                puerto = Convert.ToInt32(WebConfigurationManager.AppSettings[KEY_PORT].ToString());
                username = WebConfigurationManager.AppSettings[KEY_USER].ToString();
                password = crypt.DecryptString(WebConfigurationManager.AppSettings[KEY_PASSWD].ToString());
                from = username;
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                try
                {
                    smtp = config.AppSettings.Settings[KEY_SMTP].Value;
                    puerto = Convert.ToInt32(config.AppSettings.Settings[KEY_PORT].Value);

                    if (username == string.Empty)
                        username = config.AppSettings.Settings[KEY_USER].Value.ToString();
                    if (password == string.Empty)
                        password = crypt.DecryptString(config.AppSettings.Settings[KEY_PASSWD].Value.ToString());

                    if ((from == string.Empty) && (config.AppSettings.Settings[KEY_FROM] != null))
                    {
                        from = config.AppSettings.Settings[KEY_FROM].Value;
                    }

                    if ((cc == string.Empty) && (config.AppSettings.Settings[KEY_CC] != null))
                    {
                        cc = config.AppSettings.Settings[KEY_CC].Value;
                    }

                    if ((bcc == string.Empty) && (config.AppSettings.Settings[KEY_BCC] != null))
                    {
                        bcc = config.AppSettings.Settings[KEY_BCC].Value;
                    }

                    if (config.AppSettings.Settings[KEY_TEXTBODY] != null)
                    {
                        isHtmlBody = !Convert.ToBoolean(config.AppSettings.Settings[KEY_TEXTBODY].Value);
                    }
                }
                catch (NullReferenceException)
                {
                    smtp = smtpServer;
                    puerto = smtpPort;
                    from = notifFrom;
                    cc = string.Empty;
                    bcc = string.Empty;
                    username = notifFrom;
                }                
            }

            MailMessage mailMessage = new MailMessage();
            if (from != "")
            {
                mailMessage.From = new MailAddress(from);
                mailMessage.ReplyToList.Add(new MailAddress(from));
            }

            if (to != "")
            {
                string[] tos = (to.Replace(';', ',')).Split(','); ;
                foreach (string subTo in tos)
                {
                    mailMessage.To.Add(new MailAddress(subTo));
                }
            }

            if (cc != "")
            {
                string[] ccs = (cc.Replace(';', ',')).Split(','); ;
                foreach (string subCC in ccs)
                {
                    mailMessage.CC.Add(new MailAddress(subCC));
                }
            }

            if (bcc != "")
            {
                string[] bccs = (bcc.Replace(';', ',')).Split(','); ;
                foreach (string subBCC in bccs)
                {
                    mailMessage.Bcc.Add(new MailAddress(subBCC));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isHtmlBody;

            // Crea un adjunto
            if (attachPath != "")
            {
                mailMessage.Attachments.Add(new Attachment(attachPath));
            }

            //Para Exchange
            SmtpClient smtpClient = new SmtpClient(smtp, puerto);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            if (username != "")
            {
                //smtpClient.Credentials = new NetworkCredential(username, password, domain);
                smtpClient.Credentials = new NetworkCredential(username, password);
            }
            else
            {
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            
            smtpClient.Send(mailMessage);

        }

        #endregion SendMail

        public static void setBodyHTMLFormat(bool htmlFormat)
		{
			BodyFormat = htmlFormat;
		}

		public static System.Web.Mail.MailFormat getBodyFormat()
		{
			return BodyFormat ? System.Web.Mail.MailFormat.Html : System.Web.Mail.MailFormat.Text;
		}

        public static string LimpiarDenominacionClave(int tipoMarcaID, string denominacion)
        {
            string Result = String.Empty;

            if (tipoMarcaID == IDMIXTA)
            {
                Result = denominacion.ToUpper().Replace(ESLOGAN_Y_ETIQUETA, String.Empty).Replace(SLOGAN_Y_ETIQUETA, String.Empty)
                            .Replace(Y_ETIQUETA, String.Empty).Replace(Y_SLOGAN, String.Empty).Replace(Y_ESLOGAN, String.Empty)
                            .Replace(Y_DISENHO, String.Empty).Replace(Y_DISENO, String.Empty).Replace(Y_LOGO, string.Empty).Replace(Y_LOGOTIPO, String.Empty)
                            .TrimEnd();
            }
            else if (tipoMarcaID == IDDENOMINATIVA)
            {
                Result = denominacion;
            }

            return Result;
        }

        public static string LimpiarDenominacionClave(string tipoMarca, string denominacion)
        {
            string Result = String.Empty;

            if (tipoMarca == C_MIXTA)
            {
                Result = denominacion.ToUpper().Replace(ESLOGAN_Y_ETIQUETA, String.Empty).Replace(SLOGAN_Y_ETIQUETA, String.Empty)
                            .Replace(Y_ETIQUETA, String.Empty).Replace(Y_SLOGAN, String.Empty).Replace(Y_ESLOGAN, String.Empty)
                            .Replace(Y_DISENHO, String.Empty).Replace(Y_DISENO, String.Empty).Replace(Y_LOGOTIPO, String.Empty).Replace(Y_LOGO, string.Empty)
                            .TrimEnd();
            }
            else if (tipoMarca == C_DENOMINATIVA)
            {
                Result = denominacion;
            }

            return Result;
        }


		#region Seleccionar impresora
		public static string seleccionarImpresora()
		{
			string nombre_impresora="";
			PrintDialog printDialog1 = new PrintDialog();
			
			System.Drawing.Printing.PrintDocument pdocument = new System.Drawing.Printing.PrintDocument();
			printDialog1.Document = pdocument;
			DialogResult result   = printDialog1.ShowDialog();

			if (result == DialogResult.OK)
			{
				nombre_impresora = printDialog1.PrinterSettings.PrinterName.ToString();
			}
			return nombre_impresora;
		}
		#endregion Seleccionar Impresora

	}
}
