using System;
using System.Text.RegularExpressions;

namespace Berke.Libs
{
	// Publics delegate for this namespaces
	public delegate void Fun_AsignaTexto( CodeGenerator gen ); 


	public class CodeGenerator
	{

		#region Datos Miembros
		private static string SALTO_PAGINA = @"<w:p><w:r><w:br w:type=""page""/><w:t> </w:t></w:r></w:p>";
	
		string _marcaInicial	= "<gen:";
		string _marcaFinal		= "</gen:";

		string _marcaOcultaInicial	= "";
		string _marcaOcultaFinal	= "";

		string _inicioCampo	= @"«";
		string _finCampo	= @"»";

		string _etiqueta;
		string _template;
		string _texto;
		
		string _buffer;

		bool _textNull = false;

		CodeGenerator[] _item = new CodeGenerator[10];
		int _cantItems = 0;
	
		// Delegate
		private Fun_AsignaTexto _Fun_AsignaTexto;

		#endregion Datos Miembros

		#region Constructores y afines        		
		
		#region Constructores

		public CodeGenerator()
		{
			_template = "";
			_texto = ""; 
			_etiqueta = "?";
		}
			
		public CodeGenerator( string template)
		{
			_template = template;
			_texto = ""; 
			_etiqueta = "?";
		}

		#endregion Constructores

	
		#region Inicializar

		public void Inicializar( string pTemplate )
		{
			inicializar(pTemplate,"Root");
		}

		private void inicializar( string pTemplate, string pEtiqueta)
		{
			
			int itemIdx = 0;
			int pos, pos2;

			string buf = pTemplate;

			_etiqueta = pEtiqueta;
			
			pos = buf.IndexOf( this._marcaInicial );
			if ( pos == -1 )
			{
				_template = pTemplate;
				_cantItems = 0;
				return;
			}

			while( pos >= 0 )
			{
				string previo = buf.Substring( 0, pos );
				pos2 = buf.IndexOf( ">", pos );

				pos += 	_marcaInicial.Length;
				
				string etiqueta = buf.Substring(pos, pos2 - pos );
				pos = pos2 + 1;
			
				pos2 =  buf.IndexOf( this._marcaFinal + etiqueta, pos );
				if( pos2 == -1) throw new Exception("No se encuentra fin de etiqueta " +_marcaFinal+etiqueta);

				string contenido = buf.Substring(pos, pos2 - pos );

				pos2 = buf.IndexOf( ">", pos2 );
				if( pos2 == -1) throw new Exception("Falta \">\" de cierre en  " +_marcaFinal+etiqueta);

				_item[ itemIdx ] = new CodeGenerator();
				_item[ itemIdx ].inicializar(contenido, etiqueta );

				itemIdx++;
				buf = previo + "@@" + etiqueta + "@@" + buf.Substring(pos2 + 1);
				pos = buf.IndexOf( this._marcaInicial );

			}

			_template = buf;
			_cantItems = itemIdx;
		}

		#endregion Inicializar

		#endregion Constructores y afines

		#region Propiedades 

		#region Funcion de asignacion de Texto 

		public Fun_AsignaTexto fn_AsignaTexto 
		{
			set { _Fun_AsignaTexto = value; }
		}

		/*Ejemplo de uso
		  
			gen.fn_AsignaTexto = new Fun_AsignaTexto( funcion_de_usuario );
		  
		  private void  funcion_de_usuario( CodeGenerator gen ){
		  
		  }
		
		 */

		#endregion Funcion de asignacion de Texto 

		#region Indexers
		public CodeGenerator this[ int idx ]
		{
			get{ return _item[ idx ];}
		}

		public CodeGenerator this[ string etiqueta ]
		{
			get
			{ 
				int idx = IndexOf(etiqueta);
				return idx == -1 ? null : _item[ idx ];
			}
		}
		#endregion Indexers

	
		#region TextoNulo
		public bool TextoNulo 
		{
			set{ _textNull  = value; }
			get{ return _textNull;  }
		}
		#endregion TextoNulo

		#region CantItem
		public int CantItem{ get { return _cantItems;} }
		#endregion CantItem

		#region MarcaInicial
		public string MarcaInicial 
		{
			set{ _marcaInicial = value; }
			get{ return _marcaInicial;  }
		}
		#endregion MarcaInicial

		#region MarcaFinal
		public string MarcaFinal 
		{
			set{ _marcaFinal = value; }
			get{ return _marcaFinal;  }
		}
		#endregion MarcaFinal
		
		#region Texto
		public string Texto 
		{ 
			get{ return _texto ; }
			set{ _texto = value;}
		}
		#endregion Texto

		#region Template
		public string Template 
		{	
			get{ return _template;  }			
		}
		#endregion Template

		#region Etiqueta
		public string Etiqueta 
		{	
			get{ return _etiqueta;  }		
			set{ _etiqueta = value;	}
		}
		#endregion Etiqueta

		#endregion propiedades
		
		#region Metodos

		#region ExtraerTabla
		public CodeGenerator ExtraerTabla( string etiqueta  )
		{
			return ExtraerTabla( etiqueta, 1 );
		}
		public CodeGenerator ExtraerTabla( string etiqueta , int iteraciones )
		{
            return this.ExtraerEtiqueta(etiqueta,"<w:tbl>","</w:tbl>",true, iteraciones  );
		}
		#endregion ExtraerTabla

		#region ExtraerFila
		public CodeGenerator ExtraerFila( string etiqueta  )
		{
			return ExtraerFila( etiqueta, 1 );
		}
		public CodeGenerator ExtraerFila( string etiqueta , int iteraciones )
		{
            return this.ExtraerEtiqueta(etiqueta, "<w:tr>", "</w:tr>", true, iteraciones);
		}
		#endregion ExtraerFila

		#region ExtraerColumna
		public CodeGenerator ExtraerColumna( string etiqueta  )
		{
			return ExtraerFila( etiqueta, 1 );
		}
		public CodeGenerator ExtraerColumna( string etiqueta , int iteraciones )
		{
			return this.ExtraerEtiqueta(etiqueta,"<w:tc>","</w:tc>",true, iteraciones  );
		}
		#endregion ExtraerColumna

		#region ExtraerTexto
		public CodeGenerator ExtraerTexto( string etiqueta  )
		{
			return ExtraerFila( etiqueta, 1 );
		}
		public CodeGenerator ExtraerTexto( string etiqueta , int iteraciones )
		{
			return this.ExtraerEtiqueta(etiqueta,"<w:t>","</w:t>",true, iteraciones  );
		}
		#endregion ExtraerColumna

		#region ExtraerColumna
		public CodeGenerator ExtraerRowExcel( string etiqueta  )
		{
			return ExtraerRowExcel( etiqueta, 1 );
		}
		public CodeGenerator ExtraerRowExcel( string etiqueta , int iteraciones )
		{
			return this.ExtraerEtiqueta(etiqueta,"<Row>","</Row>",true, iteraciones  );
		}
		#endregion ExtraerColumna

		#region ExtraerEtiqueta
		public CodeGenerator ExtraerEtiqueta( string etiqueta , string marcaInicial, string marcaFinal)
		{
			return ExtraerEtiqueta( etiqueta, marcaInicial, marcaFinal, true, 1 );
		}

		public CodeGenerator ExtraerEtiqueta( string etiqueta , string marcaInicial, string marcaFinal, bool incluirMarcas, int iteraciones )
		{
			CodeGenerator ret = new CodeGenerator();
			string buf = _template;
			int pos	= 0;
			if( _marcaOcultaInicial	!= marcaInicial )
			{
				pos = buf.IndexOf( marcaInicial, 0 );

                if ((pos == -1) && (marcaInicial == "<w:tr>"))
                {
                    //marcaInicial = @"<w:tr\s(.*)>";
                    Regex wtr = new Regex(@"<w:tr(.*?)>");

                    Match match = wtr.Match(buf);

                    if (match.Success)
                    {
                        marcaInicial = String.Format("<w:tr{0}>", match.Groups[1].Value);
                        pos = match.Groups[1].Index - 5;
                    }
                }
			}else{
				pos = buf.IndexOf( marcaInicial, marcaInicial.Length );
			}
			if( pos == -1 ) throw new Exception("CodeGenerator: Marca no encontrada - "+marcaInicial);

			string previo = buf.Substring( 0, pos );

			pos += marcaInicial.Length;
			int pos0 = pos;
			int iterCont = 0;
			int pos2 = 0;
			int lf = pos;
			do
			{
				iterCont++;
				pos2 =  buf.IndexOf( marcaFinal, pos );
				if( pos2 == -1 ) throw new Exception("CodeGenerator: Marca no encontrada - "+marcaFinal);
		
				#region verificar marcas internas
				
				int iPos =  buf.IndexOf( marcaInicial, pos );
				while( iPos != -1 && iPos < pos2 )
				{
					int cont = 0;
					while( iPos != -1 && iPos < pos2 )
					{	
						iPos =  buf.IndexOf( marcaInicial, lf );
						if( iPos != -1 && iPos < pos2  )
						{
							lf = iPos + marcaInicial.Length;
							cont++;
						}
					}
					for( int i = 0; i < cont  ; i++ )
					{
						lf  =  buf.IndexOf( marcaFinal, lf  );
						lf+= marcaFinal.Length;
					}
					pos2 =  buf.IndexOf( marcaFinal, lf );
					if( pos2 == -1 ) throw new Exception("CodeGenerator: Marca Desbalanceada - "+marcaFinal);
					iPos =  buf.IndexOf( marcaInicial, lf );
				}
				#endregion 

				pos = buf.IndexOf( marcaInicial, pos2 + marcaFinal.Length);

                if ((pos == -1) && (marcaInicial.StartsWith("<w:tr ")))
                {
                    //marcaInicial = @"<w:tr\s(.*)>";
                    Regex wtr = new Regex(@"<w:tr(.*?)>");

                    Match match = wtr.Match(buf, pos2 + marcaFinal.Length);

                    if (match.Success)
                    {
                        marcaInicial = String.Format("<w:tr{0}>", match.Groups[1].Value);
                        pos = match.Groups[1].Index - 5;
                    }
                }

				if( iterCont < iteraciones && pos == -1 ) throw new Exception("CodeGenerator: Marca no encontrada - "+marcaInicial);
				pos+=marcaFinal.Length;
			}while( iterCont < iteraciones );

			string contenido = buf.Substring(pos0, pos2 - pos0 );

			pos2 += marcaFinal.Length;
		
			buf = previo + "@@" + etiqueta + "@@" + buf.Substring(pos2);
			_template = buf;
			if( incluirMarcas )
			{
				ret._template = marcaInicial+contenido+marcaFinal;
				ret._marcaOcultaInicial	= marcaInicial;
				ret._marcaOcultaFinal	= marcaFinal;
			}
			else{
				ret._template = contenido;			
			}
			return ret;
		}
		#endregion ExtraerEtiqueta

		#region IndexOF
		public int IndexOf( string etiqueta )
		{
			for ( int i = 0 ; i < this.CantItem ; i++ )
			{
				if ( _item[i]._etiqueta == etiqueta ) return i;
			}
			return -1;
		}

		#endregion IndexOF

		#region Load
		public void Load( string fileName )
		{
			System.IO.StreamReader p = new System.IO.StreamReader(fileName);
			inicializar( p.ReadToEnd(), "Root" );			
			p.Close();
		}
		#endregion Load

		#region GetTree	
		public string getTree( )
		{
			return this.getTree( (int) 0 );
		}


		private string getTree( int level )
		{
			string buf="";
			string tab = "";
			for(int j=0; j < level; j++) tab+= '\t';
			tab.PadRight(level, ' ');

			for( int i=0 ; i < this.CantItem; i++ )
			{
				buf += nl() + tab + this[i].Etiqueta;
				buf += this[i].getTree( level + 1 );
			}
			return buf;
		}

		private string nl()
		{
			return @"
";
		}
		#endregion GetTree	

		#region AsignarTextoRecursivamente

		public void AsignarTextoRecursivamente()
		{

			// Veificar si el texto debe ser nulo
			if( this._textNull )
			{
				this._texto = "";
				return;
			}

			// Copiar el template al texto
			this._texto = this._template;
			if( _Fun_AsignaTexto != null )
			{
				_Fun_AsignaTexto( this );
			}

			// Asignar texto de los SubItems
			for( int i = 0; i < this.CantItem; i++ )
			{
				this[i].AsignarTextoRecursivamente();
			}

			// Remplazar las cadenas de sustitucion
			for( int i = 0; i < this.CantItem; i++ )
			{
				string marca = "@@" + this[i]._etiqueta + "@@";
				string texto = this[i]._texto;
				if ( this._texto != string.Empty )
				{
					this._texto = this._texto.Replace( marca, texto );
				}
			}
		}

		#endregion AsignarTexto

		#region clearText
		public void clearText()
		{
			this._texto = "";
		}
		#endregion clearText

		#region clearBuffer
		public void clearBuffer()
		{
			this._buffer = "";
		}
		#endregion clearBuffer

		#region copyTemplateToBuffer
		public void copyTemplateToBuffer()
		{
			this._buffer = this._template;
		}
		#endregion copyTemplateToBuffer

		#region replaceLabel
		public void replaceLabel( string label, string valor )
		{
			this._buffer = _buffer.Replace( "@@"+label+"@@", valor );
		}
		#endregion replaceLabel

		#region replace
		public void replace( string oldString, string newString )
		{
			this._buffer = _buffer.Replace( oldString, newString );
		}
		#endregion replace

		#region replaceField
		public void replaceField( string fieldName, string newValue )
		{
			this._buffer = _buffer.Replace( this._inicioCampo + fieldName + this._finCampo, newValue );
		}

		// Agregado por Luis F. -  24/03/2008
		// para dar soporte a imágenes.
		// Se pasan como parámetros:
		// -La marca con que se identificó "fieldName"
		// -El contenido de la imagen en formato base64 "Base64Data".
		// -El tipo de de la imagen "ImageType".
		// -El ancho de la imagen "width".
		// -El alto de la imagen "height".
		public void replaceFieldImage(string fieldName, string Base64Data, string ImageType, int width, int height, string idImage)
		{
			this._buffer = _buffer.Replace( this._inicioCampo + fieldName + this._finCampo, "<w:pict>" + "<w:binData w:name=\"wordml://" + idImage /*//02000001."*/ + "." + ImageType + "\">" + @Base64Data + @"</w:binData>" + "<v:shape id=\"_x0000_i1025\" type=\"#_x0000_t75\" style=\"width:" + width.ToString() + "pt;height:" + height.ToString() + "pt\"><v:imagedata src=\"wordml://" + idImage /*//02000001."*/ + "." + ImageType + "\" o:title=\"Imagen\"/></v:shape></w:pict>");
		}
		#endregion replaceField

		#region addBufferToText
		public void addBufferToText()
		{
			#region Dar tratamiento a caracteres especiales
			_buffer = _buffer.Replace("&nbsp;","|*nbsp*|;");
			
			_buffer = _buffer.Replace("&amp;","|*amp*|;");
			_buffer = _buffer.Replace("&","&amp;");
			_buffer = _buffer.Replace("|*amp*|;","&amp;");
			_buffer = _buffer.Replace("|*nbsp*|;","&nbsp;");
	
			#endregion Dar tratamiento a caracteres especiales

			this.Texto+= _buffer;
		}
		#endregion addBufferToText

		#region addNewPageToText
		public void addNewPageToText()
		{
			this.Texto+= SALTO_PAGINA;
		}
		#endregion addNewPageToText
		
		#endregion Metodos

	}

}