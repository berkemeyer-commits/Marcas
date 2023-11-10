using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Berke.Libs.Base.Helpers
{
	/// <summary>
	/// Summary description for Files.
	/// </summary>
	public class Files
	{
		#region Definido por Fran
		public static string File(int nombre, string extension)
		{
			string archivo;
			archivo = nombre + extension;
			return archivo;
		}

		public static string File(string nombre, string extension)
		{
			string archivo;
			archivo = nombre + extension;
			return archivo;
		}
		#endregion Definido por Fran


		public static bool IsReadOnly(string path )
		{
			bool ret = false;
			if (System.IO.File.Exists(path)) 
			{
				if ((System.IO.File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) 
				{
					ret = true;
				}

			}

			return ret;
		}

		#region GetBytesFromFile
		public static byte[] GetBytesFromFile( string path )
		{
			FileStream fs = new  FileStream( path, FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader( fs );
			byte[] buf = br.ReadBytes( (int) fs.Length );
			br.Close();
			fs.Close();
			return buf;
		}
		#endregion GetBytesFromFile

		#region GetStringFromFile
		public static string GetStringFromFile( string path )
		{
			FileStream fs = new  FileStream( path, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader( fs, System.Text.UTF8Encoding.UTF8);
			
			string buf = sr.ReadToEnd();
			sr.Close();
			fs.Close();
			
			return buf;
		}
		#endregion GetStringFromFile

		#region SaveStringToFile
		public static void SaveStringToFile( string buffer, string path )
		{
			FileStream fs = new  FileStream( path, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new  StreamWriter( fs, System.Text.Encoding.UTF8 );		
			sw.Write( buffer );
			sw.Close();
			fs.Close();
		}
		#endregion SaveStringToFile

		// Creado por Luis F. - 07/04/2008
		#region SaveStringToBinary
		public static byte [] SaveStringToBinary(string documento)
		{
			/* Convertimos el documento a un vector de bytes
			 **/
			return System.Text.Encoding.UTF8.GetBytes(documento);
		}
		#endregion SaveStringToBinary

		#region SaveBytesToFile
		public static void SaveBytesToFile( byte[] buffer, string path )
		{
			FileStream fs = new  FileStream( path, FileMode.Create, FileAccess.Write);
			BinaryWriter bw = new   BinaryWriter( fs, System.Text.Encoding.UTF8 );		
			bw.Write( buffer );
			bw.Close();
			fs.Close();
		}
		#endregion SaveBytesToFile

		// Creado por Luis F.
		#region CreateDirectory
		public static void CreateDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				// Si ya está creado el directorio, entonces no hace nada
				//Console.WriteLine("That path exists already.");
				return;
			}

			// En caso de que no esté creado el directorio, crea uno nuevo.
			DirectoryInfo di = Directory.CreateDirectory(path);
		}
		#endregion CreateDirectory

		// Creado por Luis F.
		#region Remove all files of a directory
		public static void RemoveFiles(string path)
		{
			if (!Directory.Exists(path))
			{
				// Si ya está creado el directorio, entonces no hace nada
				//Console.WriteLine("That path exists already.");
				return;
			}

			DirectoryInfo dTemp = new DirectoryInfo(path);
			FileInfo [] fileList = dTemp.GetFiles();
			for(int i = 0; i < fileList.Length; i++)
			{
				try
				{
					fileList[i].Delete();
				}
				catch (Exception e){}
                
			}
		}
		#endregion Remove all files of a directory

		// Creado por Luis F.
		#region Search directory of files from file ".html"
		public static string DirectoryFiles(string path_html_file)
		{
			string contenidoArchivo;
			int indice, comienzoNombreDirectorio, finNombreDirectorio;

			FileInfo ftemp =  new FileInfo(path_html_file);
			
			// Pregunta si el archivo existe
			if (ftemp.Exists)
			{
				contenidoArchivo = 	GetStringFromFile(path_html_file);

				// Busca la cadena "src=", si encuentra trae obtiene el indice de la ubicación de la cadena "src=".
				indice = contenidoArchivo.IndexOf("src=");
				
				if(indice >= 0)
				{
					indice += 5;
					// Busca la posición de la última ocurrencia de "_".
					comienzoNombreDirectorio = contenidoArchivo.IndexOf('_',indice);
					
					// Busca la posición de "/"
					finNombreDirectorio = contenidoArchivo.IndexOf('/',comienzoNombreDirectorio);

					// Retorna el nombre del directorio
					return @contenidoArchivo.Substring(comienzoNombreDirectorio, (finNombreDirectorio - comienzoNombreDirectorio)+1);
				}
				else
					// Si no encuentra la cadena "src=" retorna una cadena vacía.
					return "";
			}
			
				// Si el archivo no existe retorna una cadena vacía
			else
				return "";
		}	
		#endregion Search directory of files from file ".html"
		
		// Creado por Luis F. - 01/04/2008
		// Entrada: un archivo ".htm"
		// Salida: un hash cuyo key es el nombre del arhivo de la imagen sin su extension,
		// y el valor es el path absoluto a dicha imagen.
		// Se reemplaza del tag '<img src="..."' por '<img src="cid:..."'
        public static Hashtable HtmlParsingImage(string path_html_file)
		{
			Hashtable resul = new Hashtable();
			
			string contenidoArchivo = "";
			string path, nombreImagen, parte1, parte2;
			string [] nom;

			
			int comienzoPath, finPath, inicio, indiceInicioNombre;

			// Obtiene el path absoluto (hasta el directorio donde se encuentra ubicado
			// el archivo ".htm").
			inicio = path_html_file.LastIndexOf(@"\");
			string pathAbsoluto = path_html_file.Substring(0, inicio + 1);
						
			FileInfo ftemp =  new FileInfo(path_html_file);
			
			// Pregunta si el archivo existe
			if (ftemp.Exists)
			{
				contenidoArchivo = 	GetStringFromFile(path_html_file);

				inicio = 0;

				// Busca la cadena "<img", si encuentra obtiene el indice de la ubicación de la cadena "src=".
				while((comienzoPath = contenidoArchivo.IndexOf("<img", inicio)) >= 0)
				{
					if((comienzoPath = contenidoArchivo.IndexOf("src=", (comienzoPath + 5))) >= 0)
					{
						parte1 = contenidoArchivo.Substring(0, comienzoPath);

						comienzoPath += 5;
					
						finPath = contenidoArchivo.IndexOf('"', comienzoPath);
					
						// Contiene el path del archivo.
						path = @contenidoArchivo.Substring(comienzoPath, (finPath - comienzoPath));

						// Busca el comienzo del nombre del archivo de la imagen
						indiceInicioNombre = @contenidoArchivo.IndexOf('/', comienzoPath);
						indiceInicioNombre++;

						// Obtiene el nombre de la imagen
						nombreImagen = @contenidoArchivo.Substring(indiceInicioNombre, (finPath - indiceInicioNombre));
						nom = nombreImagen.Split('.');
						nombreImagen = nom[0];

						// Agrega al hashtable el nombre del archivo y el path donde se encuentra ubicado
						if (!resul.ContainsKey(nombreImagen))
							resul.Add(nombreImagen, pathAbsoluto + path);

						// Nuevo indice donde se empezará la nueva búsqueda
						inicio = comienzoPath + 4 + nombreImagen.Length;
			
						// Reemplaza src=... por src="cid:..."
						parte2 = contenidoArchivo.Substring(finPath + 1);
						contenidoArchivo = parte1 + "src=\"cid:" + nombreImagen + "\"" + parte2;
					}
					else
					{
						break;
					}
				}	
			}
	
			// Sobreescribe el mismo archivo de entrada con los valores cambiados.
			SaveStringToFile(@contenidoArchivo, path_html_file);

			return resul;
		}

		public static byte[] ReadFile(string filePath)
		{
			byte[] buffer;
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			try
			{
				int length = (int)fileStream.Length;  // get file length
				buffer = new byte[length];            // create buffer
				int count;                            // actual number of bytes read
				int sum = 0;                          // total number of bytes read

				// read until Read method returns 0 (end of the stream has been reached)
				while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
					sum += count;  // sum is a buffer offset for next reading
			}
			finally
			{
				fileStream.Close();
			}
			return buffer;
		}


	}
}
