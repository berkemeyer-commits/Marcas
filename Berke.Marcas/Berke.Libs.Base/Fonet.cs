using System;
using System.Collections;

namespace Berke.Libs.Fonet
{
	#region Clases Asociadas con "Fonetic"

	public class Item 
	{
		public string Cad;
		public int Len;
		public int Pos;
		public int Order;

	}


	public class ItemComparer : IComparer  
	{
		int IComparer.Compare( Object obj1, Object obj2 )  
		{
			int val1 = ((Item) obj1).Pos;
			int val2 = ((Item) obj2).Pos;

			return val1 < val2 ? -1 : ( val1 == val2 ? 0 : 1 );
		}

	}

	#endregion 

	#region Fonetic Class

	public class Fonetic
	{
		#region Datos Miembros

		string _patronOriginal= "";
		string _palabraOriginal="";
		string _patronFonetizado="";
		string _palabraFonetizada="";

		string _patronSerializado="";
		string _palabraSerializada="";

		int  _caracteresCoincidentes=0;
		int  _caracteresNoCoincidentes=0;

		public int _iniVocal;
		public int _iniConsonante;
		public int _finVocal;
		public int _finConsonante;

		int  _longitudMaximaDeBloque = 0;
		int  _conFactorDeSecuencia   ;

		public ArrayList lstPat = new ArrayList();
		public ArrayList lstPal = new ArrayList();

		double _factorGlobal = 0.0;
		double _factor1 = 0.0;		// Caracteres coincidentes / Long_PalabraMenor
		double _factor2 = 0.0;		// Caracteres coincidentes / Long_PalabraMayor
		double _factor3 = 0.0;		// Factor de correccion por secuencia de aparicion de caracteres

		double _factorFonetico = 0.0; // Factor de vocales/consonantes iniciales/finales
		double _factorSintactico = 0.0;		
		double _pondSec =  20;		// Factor de ponderacion de secuencia

		#endregion Datos Miembros
	
		#region Constructores

		// Constructor
		public Fonetic()
		{
			_patronOriginal		= "";
			_palabraOriginal	= "";
			_patronFonetizado	= "";
			_palabraFonetizada	= "";
		}

		// Constructor
		public Fonetic( string palabra, string patron )
		{
			_patronOriginal		= patron;
			_palabraOriginal	= palabra;
			_patronFonetizado	= "";
			_palabraFonetizada	= "";
		}

		#endregion Constructores

		#region  Propiedades 

		#region LongitudMaximaDeBloque
		public int conFactorDeSecuencia
		{
			set {_conFactorDeSecuencia = value ;}
			
			    
			get
			{
				return _conFactorDeSecuencia;
			}
		}
		#endregion LongitudMaximaDeBloque


		#region LongitudMaximaDeBloque
		public int LongitudMaximaDeBloque
		{
			get{return _longitudMaximaDeBloque;}
		}
		#endregion LongitudMaximaDeBloque

		#region Puntaje
		public int Puntaje
		{ 
			get{ return (int) (_factorGlobal * 100 );}
		}
		#endregion


		#region  Factor1,Factor2,Factor3

		public int Factor1	{ get{ return (int) (_factor1 * 100);}	}
		public int Factor2 { get{ return (int) (_factor2 * 100);} }
		public int Factor3 {get{ return (int) (_factor3 * 100);} }
		public int FactorFonetico {get{ return (int) (_factorFonetico * 100);} }
		public int FactorLexicoGrafico {get{ return (int) (_factorSintactico * 100);} }
		#endregion

		#region PatronOriginal
		public string PatronOriginal
		{ 
			set{ _patronOriginal = value; }
			get{ return _patronOriginal;}
		}
		#endregion
		
		#region PalabraOriginal
		public string PalabraOriginal
		{
			set{ _palabraOriginal = value; }
			get{ return _palabraOriginal;}
		}
		#endregion PalabraOriginal

		#region PatronFonetizado, PalabraFonetizada
		
		public string PatronFonetizado{ get{ return _patronFonetizado ; }}
		public string PalabraFonetizada{ get{ return _palabraFonetizada ; }}
		
		#endregion PatroFonetizado, PalabraFonetizada
		
		#region PatronSerializado, PalabraSerializada
		
		public string PatronSerializado{ get{ return _patronSerializado ; }}
		public string PalabraSerializada{ get{ return _palabraSerializada ; }}
		
		#endregion PatronSerializado, PalabraSerializada
		

		#region CaracteresCoincidentes,CaracteresNoCoincidentes
 
		public int CaracteresCoincidentes{ get{ return _caracteresCoincidentes;}}
		public int CaracteresNoCoincidentes{ get{ return _caracteresNoCoincidentes;}}

		#endregion

		#endregion  Propiedades 

		#region Metodos Publicos Estaticos

		#region Fonetizar

		public static string Fonetizar( string  cadena )
		{
			string ret = "";
			char[] let;
			char[] tmp;
			int k;

			let = cadena.ToUpper().ToCharArray();
			if ( let.Length  == 0 ) return "";
			//

			#region Cambiar Vocales acentuadas y con dieresis , ñ y Ñ
			for( int i = 0 ; i < let.Length ; i++)
			{
				switch(let[i])
				{
					case 'á' :
					case 'Á' :
					case 'ä' :
					case 'Ä' :
					case '@' :
						let[i] = 'A';
						break;
					case 'é' :
					case 'É' :
					case 'ë' :
					case 'Ë' :
						let[i] = 'E';
						break;
					case 'í' :
					case 'Í' :
					case 'ï' :
					case 'Ï' :
						let[i] = 'I';
						break;
					case 'ó' :
					case 'Ó' :
					case 'ö' :
					case 'Ö' :
						let[i] = 'O';
						break;
					case 'ü' :
					case 'Ü' :
					case 'ú' :
					case 'Ú' :
						let[i] = 'U';
						break;
					case 'ñ' :
					case 'Ñ' :
						let[i] = 'N';
						break;
				}
						
			}
			#endregion 
			
			#region Eliminar duplicados
			tmp = new char[let.Length];
			k = 0;
			tmp[k++] = let[0];
			for( int i = 1 ; i < let.Length ; i++)	{
				if( let[i] != let[i - 1])	tmp[k++] = let[i]; 
			}
			ret = new string( tmp, 0, k );

			let = ret.ToCharArray();
			#endregion 

			// Conversion  principal

			tmp = new char[let.Length];

			k=0;

			for( int i = 0 ; i < let.Length ; i++)
			{
				#region Reemplazar secuencias de 3 letras
				if( i+2 <  let.Length )
				{
					if (let[i] == 'Q' && let[i+1] == 'U' && let[i+2] == 'E' )
					{  // QUE
						tmp[k++] = 'K';	
						tmp[k++] = 'E';	
						i+=2;
						continue;
					}
					if (let[i] == 'Q' && let[i+1] == 'U' && let[i+2] == 'I' )
					{  // QUI
						tmp[k++] = 'K';	
						tmp[k++] = 'I';	
						i+=2;
						continue;
					}

				}
				#endregion Reemplazar secuencias de 3 letras

				#region Reemplazar Secuncias de 2 letras 
				if( i+1 <  let.Length )
				{
					if (let[i] == 'C' && let[i+1] == 'H' )
					{  // CH
						//						tmp[k++] = (char) 167;
						tmp[k++] = 'S';	
						i++;
						continue;
					}
					if (let[i] == 'S' && let[i+1] == 'H' )
					{  // CH
						//						tmp[k++] = (char) 167;	
						tmp[k++] = 'S';	
						i++;
						continue;
					}
					if (let[i] == 'C' && let[i+1] == 'I' )
					{  // CI
						tmp[k++] = 'S';	
						tmp[k++] = 'I';	
						i++;
						continue;
					}
					if (let[i] == 'C' && let[i+1] == 'Y' )
					{  // CI
						tmp[k++] = 'S';	
						tmp[k++] = 'I';	
						i++;
						continue;
					}
					if (let[i] == 'C' && let[i+1] == 'E' )
					{  // CE
						tmp[k++] = 'S';	
						tmp[k++] = 'E';	
						i++;
						continue;
					}
					if (let[i] == 'G' && let[i+1] == 'I' )
					{  // CI
						tmp[k++] = 'J';	
						tmp[k++] = 'I';	
						i++;
						continue;
					}
					if (let[i] == 'G' && let[i+1] == 'E' )
					{  // CE
						tmp[k++] = 'J';	
						tmp[k++] = 'E';	
						i++;
						continue;
					}
				}
				#endregion Reemplazar Secuncias de 2 letras 

				#region Reemplazar Letras simples
				switch(let[i])
				{
					case 'H' :	
					case '.' :	break;
					case 'Y' : tmp[k++] = 'I'; break;
					case '&' : tmp[k++] = 'I'; break;
					case 'Q' : tmp[k++] = 'K'; break;
					case 'Z' : tmp[k++] = 'S'; break;
					case 'X' : tmp[k++] = 'S'; break;
					case 'C' : tmp[k++] = 'K'; break;
					case 'V' : tmp[k++] = 'B'; break;
					case 'W' : tmp[k++] = 'B'; break;
					case ' ' : tmp[k++] = '_'; break;
					case '-' : tmp[k++] = '_'; break;
					case '/' : tmp[k++] = '_'; break;
					default  : tmp[k++] = let[i]; break;				
				}
				#endregion Reemplazar Letras simples
		
			}// end for
		
			ret = new string( tmp, 0, k );
			return ret;
		}


		#endregion fonetizar

		#endregion Metodos Publicos Estaticos


		#region METODO PRIVADOS

		#region nl

		private string nl 
		{
			get	
			{
				return @"
";}
		}

		#endregion

		#region factorVocCon

		private void factorVocCon( string pat, string pal )
		{
			int vocIni = 0;	int vocFin = 0;
			int conIni = 0; int conFin = 0;
			string palVocal	= "";	string palConsonante = "";
			string patVocal	= "";	string patConsonante = "";
				
			#region Conformar lista de vocales y consonantes

			for( int i = 0; i < pat.Length; i++ )
			{
				switch( pat.Substring(i,1) )
				{
					case "A" :
					case "E" :
					case "I" :
					case "O" :
					case "U" :
						patVocal+= pat.Substring(i,1);
						break;
					default :
						patConsonante+= pat.Substring(i,1);
						break;
				}
			}
			for( int i = 0; i < pal.Length; i++ )
			{
				switch( pal.Substring(i,1) )
				{
					case "A" :
					case "E" :
					case "I" :
					case "O" :
					case "U" :
						palVocal+= pal.Substring(i,1);
						break;
					default :
						palConsonante+= pal.Substring(i,1);
						break;
				}
			}
			#endregion Conformar lista de vocales y consonantes
            
			#region Contar vocales iniciales
			for( int i = 0; i < palVocal.Length && i < patVocal.Length ; i++){
				if( patVocal.Substring(i,1) == palVocal.Substring(i,1) ) {	vocIni++; }
				else { break; }
			}
			
			#endregion Contar vocales iniciales

			#region Contarconsonantes iniciales
			for( int i = 0; i < palConsonante.Length &&  i < patConsonante.Length; i++)
			{
				if( patConsonante.Substring(i,1) == palConsonante.Substring(i,1) ) {	conIni++; }
				else { break; }
			}
			#endregion Contar consonantes iniciales
	
			#region Invertir cadenas
			string tmp="";
			for( int i = patVocal.Length- 1; i >= 0  ; i--){ tmp+= patVocal.Substring(i,1);}
			patVocal = tmp;
			
			tmp="";
			for( int i = palVocal.Length- 1; i >= 0  ; i--){ tmp+= palVocal.Substring(i,1);}
			palVocal = tmp;

			tmp="";
			for( int i = patConsonante.Length- 1; i >= 0  ; i--){ tmp+= patConsonante.Substring(i,1);}
			patConsonante = tmp;

			tmp="";
			for( int i = palConsonante.Length- 1; i >= 0  ; i--){ tmp+= palConsonante.Substring(i,1);}
			palConsonante = tmp;
			#endregion Invertir cadenas

			#region Contar vocales Finales
			for( int i = 0; i < palVocal.Length && i < patVocal.Length ; i++)
			{
				if( patVocal.Substring(i,1) == palVocal.Substring(i,1) ) {	vocFin++; }
				else { break; }
			}
			
			#endregion Contar vocales Finales

			#region Contarconsonantes Finales
			for( int i = 0; i < palConsonante.Length &&  i < patConsonante.Length; i++)
			{
				if( patConsonante.Substring(i,1) == palConsonante.Substring(i,1) ) { conFin++; }
				else { break; }
			}
			
			#endregion Contar consonantes Finales

			_iniVocal		= vocIni;
			_iniConsonante	= conIni;
			_finVocal		= vocFin;
			_finConsonante	= conFin;

			if( patVocal !="" && patConsonante != "" && patVocal != "" && patConsonante != "" )
			{
				_factorFonetico = 
					(	 (float) _iniVocal			/ (float) patVocal.Length + 
					1.5 * (float) _iniConsonante		/ (float) patConsonante.Length + 
					2.0 * (float) _finVocal		/ (float) patVocal.Length +
					 (float) _finConsonante	/ (float) patConsonante.Length ) / 5.5;

				/*
				_factorFonetico = 
					(	 (float) _iniVocal			/ (float) patVocal.Length + 
					(float) _iniConsonante		/ (float) patConsonante.Length + 
					2.0 * (float) _finVocal		/ (float) patVocal.Length +
					1.5 * (float) _finConsonante	/ (float) patConsonante.Length ) / 5.5;
			   */
			}
			else{
				_factorFonetico = 1;
			}
		}
		#endregion factorVocCon

		#region calcularFactorDeSecuencia
		private void calcularFactorDeSecuencia()
		{
			int car = 0;
			for( int i=0 ; i < this.lstPal.Count; i++)
			{
				for( int j=0 ; j < this.lstPal.Count; j++)
				{
					//					if( i == j ) continue;

					int iPos = indexInPattern( i );
					int jPos = indexInPattern( j );

					if( i == j ) car+= ((Item)this.lstPal[i]).Len;

					if( i < j && iPos < jPos) car+= ((Item)this.lstPal[i]).Len;

					if( i > j && iPos > jPos) car+= ((Item)this.lstPal[i]).Len;
				}
			}
			if( CaracteresCoincidentes > 0 )
			{
				_factor3 = (double) car / (double) ( CaracteresCoincidentes * lstPal.Count  );
				_factor3 = (_factor3 - 0.5) / 0.5;
			}
			else
			{
				_factor3 = 0.0;
			}
		}


		#region indexInPattern

		private int indexInPattern( int palIdx )
		{
			int ord = ((Item)this.lstPal[palIdx]).Order;
			for( int patIdx = 0; patIdx < lstPat.Count; patIdx++ )
			{
				if ( ((Item)this.lstPat[patIdx]).Order == ord) 
				{
					return patIdx;
				}
			} 
			throw new Exception("Error en Fonetic.indexInPattern");
		}
		#endregion

		#endregion calcularFactorDeSecuencia

		#region Serializar
		private string Serializar( bool patron )
		{
			string serializado = "";

			string buf="";
			// Patron
			int cus=0;

			ArrayList lista = this.lstPat;
			string cadena = this.PatronFonetizado;
			if( !patron )
			{
				lista = this.lstPal;
				cadena = this.PalabraFonetizada;
			}

			for(int  id = 0 ; id < lista.Count; id++ )
			{
				int pos 	= ((Berke.Libs.Fonet.Item) lista[id]).Pos;
				int len 	= ((Berke.Libs.Fonet.Item) lista[id]).Len;
				int order 	= ((Berke.Libs.Fonet.Item) lista[id]).Order;
				string cad 	= ((Berke.Libs.Fonet.Item) lista[id]).Cad;

				if( pos > cus )
				{
					string prev = cadena.Substring(cus, pos - cus);
					
					serializado += "0~" + prev + "|";

					buf+= prev; //*
				}
				
				serializado += ((int)order+1).ToString()+ "~" +cad + "|";

				buf+= cad; //*
				cus = pos + len;
			}
			if( cus < cadena.Length )
			{
	
				serializado += "0~" + cadena.Substring(cus);

				buf+= cadena.Substring(cus);
				//*
			}
			return serializado;
		}	

		#endregion Serializar
		
		#endregion METODO PRIVADOS		


		#region METODS PUBLICOS 

		#region estimarSemejanza 	
		public void  estimarSemejanza( int minBlock )
		{
			bool transtrocado = false;

			int order = 0;
			
			fonetizarPatron();
			fonetizarPalabra();

			string pat = _patronFonetizado;
			string pal = _palabraFonetizada;

			int patLen = pat.Length;
			int palLen = pal.Length;

			

			if ( _palabraOriginal.Equals("NEC" ) ) 
			{
				int p=0;
			}


			
			if( pat.Length > pal.Length )
			{
				/* [rgimenez:13-03-2008]
				 * Si la longitud de la palabra es menor al patron ajustamos
				 * el bloque minimo con la longitud de la palabra.
				 * Ver detalles en el BUG#796
				 */
				
				int diflen = (pat.Length - pal.Length);
				if ( (2 * pal.Length) >= diflen )
				{
					if ( pal.Length < minBlock )
					{
						minBlock = pal.Length ;
					}

				}
				
				/*
				if ( pal.Length < minBlock )
				{
					minBlock = pal.Length ;
				}
				*/
				

				transtrocado = true;
				string menor = pal;
				pal = pat;
				pat = menor;

				//				_patronFonetizado	= pat;
				//				_palabraFonetizada	= pal;
			}

			_caracteresCoincidentes = 0;
			int mLen;
			while ( true )
			{
				mLen = -1;
				string cad = "";
				Item itPat = new Item();
				Item itPal = new Item();

				for( int pos = 0; pos < pat.Length ; pos++ )
				{
					for( int len = 1; pos + len <= pat.Length; len++ )
					{
						cad = pat.Substring( pos, len );
						int palIdx = pal.IndexOf( cad );
						if ( palIdx > -1 ) 
						{
							if( len > mLen ) 
							{
								mLen = len;
								itPat.Cad = cad;
								itPat.Len = len;
								itPat.Pos = pos;

								itPal.Cad = cad;
								itPal.Len = len;
								itPal.Pos = palIdx;
							}
						}
					}
				}// end while
				if ( mLen == -1 ){ break;}
				if ( mLen < minBlock ){ break;}

				_caracteresCoincidentes+= itPat.Len;
				itPat.Order = order;
				itPal.Order = order;
				order++;
				lstPat.Add( itPat );
				lstPal.Add( itPal );
				// Reemplazar por caracteres especiales

	
				pal = pal.Substring(0,itPal.Pos) + ((string) "").PadRight(itPat.Len,'~')+pal.Substring(itPal.Pos + itPal.Len );
				pat = pat.Substring(0,itPat.Pos) + ((string) "").PadRight(itPat.Len,'|')+pat.Substring(itPat.Pos + itPal.Len );
			}

			#region LongitudMaximaDeBloque
			_longitudMaximaDeBloque = -1;
			for( int i = 0; i < lstPal.Count ; i++ )
			{
				if ( ((Berke.Libs.Fonet.Item) lstPat[i]).Len > _longitudMaximaDeBloque ){
					_longitudMaximaDeBloque = ((Berke.Libs.Fonet.Item) lstPat[i]).Len;
				}
			}
			#endregion LongitudMaximaDeBloque

			if( _caracteresCoincidentes > 0 )
			{
				factorVocCon( this._patronFonetizado, this._palabraFonetizada  );
			
				_factor1 = (double ) _caracteresCoincidentes / (double ) patLen;
				_factor2 = (double ) _caracteresCoincidentes / (double ) palLen;
				//			_factor2 = (double ) _caracteresCoincidentes / (double ) (palLen > patLen ? palLen : patLen );
			
				ItemComparer comp = new ItemComparer();
				lstPal.Sort( comp );
				lstPat.Sort( comp );
				if( transtrocado )
				{
					ArrayList temp;
					temp = lstPal;
					lstPal = lstPat;
					lstPat = temp;
				}

				double resta= 0;
				if (conFactorDeSecuencia==1) 
				{
					calcularFactorDeSecuencia();
					resta =  _pondSec - ( _pondSec * _factor3 );
				}
				_factorSintactico = (_factor1 + _factor2) / 2.0 ;
				_factorSintactico = _factorSintactico - resta / 100.0;
				_factorSintactico = _factorSintactico < 0 ? 0 : _factorSintactico;
				_factorGlobal = ( _factorSintactico + _factorFonetico ) / 2.0;

			}
			else
			{
				_factorSintactico = 0.0;
				_factorSintactico = 0.0;
				_factorGlobal = 0.0;

			}
			_patronSerializado	= this.Serializar( true );
			_palabraSerializada	=	this.Serializar( false );

		}
		#endregion estimarSemejanza 


		#region fonetizarPalabra

		//------------------------    Fonetizar Palabra -----------------------------
		public void fonetizarPalabra()
		{
			_palabraFonetizada = Fonetizar(_palabraOriginal);
		}

		#endregion fonetizarPalabra

		#region fonetizarPatron

		public void fonetizarPatron()
		{
			_patronFonetizado = Fonetizar(_patronOriginal);
		}

		#endregion fonetizarPatron

		
		#endregion METODOS PUBLICOS

	}// end Fonetic  class 

	#endregion Fonetic Class
} // end Berke.Libs  namespace
