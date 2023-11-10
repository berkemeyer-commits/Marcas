using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
namespace Berke.Libs.WebBase.Helpers
{
	/// <summary>
	/// Summary description for HtmlGW.
	/// </summary>
	public class HtmlGW
	{
		public HtmlGW()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Spaces
		public static string Spaces( int cantidad ) 
		{
			string buf= "";
			for( int i = 0; i < cantidad; i++ ){
				buf+="&nbsp;";
			}
			return buf;
		}
		#endregion Spaces

		#region ConvertNewLines
		public static string ConvertNewLines( string cadena ) 
		{
			string nl= @"
";			
			return cadena.Replace( nl, "<br>");
		}
		#endregion ConvertNewLines

		#region SessionID
		public static string SessionID 
		{
			get{ return HttpContext.Current.Session.SessionID.ToString();}
		}
		#endregion SessionID

		#region Alert_script
		public static string Alert_script( string Mensaje )
		{ 
			return "<script language='javascript'>alert('"+ Mensaje +"')</script>";
		}
		#endregion

		#region ClosePopUp_script
		public static string ClosePopUp_script( string controlDestino )
		{
			string template = @"
							<script language='JavaScript'>
							function restablecer(pCod){
							window.opener.document.Form1.{0}.value = pCod ;
							window.opener.focus();
							window.close();
							}
							</script>
							";
			return string.Format( template, controlDestino  );
		}
		#endregion ClosePopUp_script

		#region OpenPopUp_Link
		public static string OpenPopUp_Link( string pagina, string texto, string paramValue, string paramName )
		{
		
			string template = @"<A onclick=""window.open('{0}?{1}={2}','', 'width=700, height=450, scrollbars=yes '); return false;"" href=""#""> {3}</A>";

			return string.Format( template, pagina,paramName, paramValue, texto );
		}
		#endregion OpenPopUp_Link	

		#region ClosePopUp_Link
		public static string ClosePopUp_Link( string valor, string etiqueta )
		{
		
			string template = @"<A onclick=""restablecer('{0}' ); "" href=""#""> {1}</A>";

			return string.Format( template, valor, etiqueta );
		}
		#endregion ClosePopUp_Link

		#region Redirect_Link
		public static string  Redirect_Link( string valor, string etiqueta, string paginaDestino )
		{
		
			string template = @"<A href=""{0}?ID={1}"">{2}</A>";

			return string.Format( template,paginaDestino, valor, etiqueta );
		}

		public static string  Redirect_Link( string valor, string etiqueta, string paginaDestino, string paramName )
		{
		
			string template = @"<A href=""{0}?{1}={2}"">{3}</A>";

			return string.Format( template,paginaDestino,paramName,valor, etiqueta );
		}

		public static string  Redirect_Link( string valor, string etiqueta, string paginaDestino, string paramName, string complemento )
		{
		
			string template = @"<A href=""{0}?{1}={2}{4}"">{3}</A>";

			return string.Format( template,paginaDestino,paramName,valor, etiqueta, complemento );
		}

		#endregion  Redirect_Link

	}
}


#region Berke.Html  namespaces
namespace Berke.Html
{
	using System.Collections;

	#region HtmlTextFormater
	public class HtmlTextFormater
	{
	
		#region Datos Miembros

		private string _size = "";
		private bool   _bold = false;
		private string _color = "";
		private string _estilo = "";

		public System.Drawing.Color color = System.Drawing.Color.Black; 
	
		#endregion Datos Miembros


		#region Constructores
		public HtmlTextFormater (){}
		public HtmlTextFormater (string estilo){
			this._estilo = estilo;
		}
		public HtmlTextFormater (string size, bool   bold, System.Drawing.Color color  ){
			_size	= size;
			_bold	= bold;
			_color	= System.Drawing.ColorTranslator.ToHtml(color);
		}

		#endregion Constructores

		#region Propiedades
		
		public bool		Bold{ get{ return _bold;} set{ _bold = value; } }
		public string	Size{ get{ return _size;} set{ _size = value; } }
		public string	Color{ get{ return _color;} set{ _color = value; } }


		#endregion Propiedades

		#region Metodos
		
		public void SetColor( System.Drawing.Color color ){
			_color = System.Drawing.ColorTranslator.ToHtml(color);
		}

		public string Html ( string body )
		{
			if (_estilo != "") 
			{
				return "<span class=\""+_estilo+"\">"+body+"</span>";
			}
			string ret= body;
			string b = "";
			string bf = "";
			if( this._bold )
			{
				b = "<b>";
				bf= "</b>";
			}

			#region Color
			string colorBuf = "";
			if( _color != "" ){
				colorBuf = string.Format( @" color=""{0}"" ", _color );
			}
			#endregion

			#region Size
			string sizeBuf = "";
			if( _size != "" )
			{
				sizeBuf = string.Format( @" size=""{0}"" ", _size );
			}
			#endregion

			if( sizeBuf != "" || colorBuf != "" )
			{
				ret = @"<font "+  sizeBuf + colorBuf + @">" + b + body + bf + @"</font>";
			}


			return ret;
		
		}


		#endregion Metodos


	}
	#endregion HtmlTextFormater

	#region HtmlCellFormater
	public class HtmlCellFormater
	{
	
		#region Datos Miembros

		public string BgColor		= "white";
		private string estilo		= "";
		public HtmlTextFormater Text = new HtmlTextFormater();

		#endregion Datos Miembros

		#region Constructores
		public HtmlCellFormater (){}
		public HtmlCellFormater(string estilo)
		{
			this.estilo = estilo;
		}

		#endregion Constructores

		#region Propiedades
		
		public string Begin { 
			get {
				if (estilo == "")
				{
					return @"<TD bgColor="""+ BgColor +@""">"; 
				}
				else
				{
					return "<TD class=\""+estilo+"\" >"; 
				}
			} 
		}
		public string End  { get { return "</td>"; } }

		#endregion Propiedades

		#region Metodos

		public string Html( string body ){ return this.Begin + Text.Html( body ) + this.End; }


		#endregion Metodos


	}
	#endregion HtmlCellFormater

	#region HtmlRowFormater

	public class HtmlRowFormater
	{

		#region Propiedades

		public string Begin { get { return "<TR>"; } }
		public string End  { get { return "</tr>"; } }


		#endregion Propiedades


		#region Metodos

		public string Text( string body ){ return this.Begin + body + this.End; }


		#endregion Metodos

	}
	#endregion HtmlRowFormater





	#region Table
	public class HtmlTable
	{
		#region Datos Miembros
		public string BgColor		= "white";
		public string CellSpacing	= "0";
		public string CellPadding	= "0";
		public string Border		= "1";
		private string estilo       = "";

		public HtmlCellFormater cell	= new HtmlCellFormater();
		public HtmlRowFormater	row		= new HtmlRowFormater();
		HtmlTextFormater		text	= new HtmlTextFormater();

		ArrayList _HeadList = new ArrayList();
	
		string _buffer = "";

		private string initTag = @"<Table bgColor=""{0}"" cellSpacing=""{1}"" cellPadding=""{2}""  border=""{3}"">";
	
		#endregion Datos Miembros

		#region Constructores

		public HtmlTable(){}

		public HtmlTable(	string bgColor, string cellSpacing, string cellPadding, string border )
		{
			BgColor		= bgColor;
			CellSpacing = cellSpacing;
			CellPadding = cellPadding;
			Border		= border;
		}
		
		public HtmlTable(string tbl_estilo)
		{
			this.estilo = tbl_estilo;
			initTag = "<table cellspacing=\"0\" cellpadding=\"0\" class=\""+estilo+"\">";
		}
		public void setRowFormater(HtmlRowFormater row)
		{
			this.row = row;		
		}
		public void setCellFormater(HtmlCellFormater cell)
		{
			this.cell = cell;
		}
		public void setTextFormater(HtmlTextFormater text)
		{
			this.text = text;
		}

		#endregion Constructores
	
		#region Propiedades

		#region InitialTag
		public string Begin { 
						get { 
							if (estilo == ""){
								return string.Format(initTag, BgColor,CellSpacing,CellPadding,Border); 
							}
							else {
								return initTag;
							}

						 } 
		}
		public string End { get { return "</Table>"; } }
		#endregion InitialTag


		#endregion Propiedades


		#region Metodos

		public void BeginRow( )	{ _buffer+= row.Begin;  }
		public void EndRow( )	{ _buffer+= row.End;  }

		public void BeginCell( )	{ _buffer+= cell.Begin;  }
		public void EndCell( )		{ _buffer+= cell.End;  }

		public void AddText( string textString ) 
		{
			_buffer+= text.Html( textString );  
		}
		public void AddCell( string body ) {
			_buffer+= cell.Html( body );  
		}
		public void AddRow( string body ) {
			_buffer+= row.Text( body );  
		}
		public string Html( )
		{
			return this.Begin + _buffer + this.End;
		}

		#endregion Metodos
	}
	#endregion HtmlTable

	
}
	

#endregion Berke.Html  namespaces

