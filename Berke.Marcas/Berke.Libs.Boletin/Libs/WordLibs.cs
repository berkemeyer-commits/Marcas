using System;
using System.Windows.Forms;
//using Microsoft.Win32;
using Microsoft.Office.Interop;
using Microsoft.Office.Core;

namespace Berke.Libs.Boletin.Libs
{
	/// <summary>
	/// Summary description for WordLibs.
	/// </summary>
	public class WordLibs
	{
		object oMissing = System.Reflection.Missing.Value;
		object archivo ;
		object myTrue = true;    // Imprimir en background
		object myFalse = false;
        Microsoft.Office.Interop.Word.Application wapp;
        Microsoft.Office.Interop.Word.Document wdoc;
		//Word.Application wapp;
		//Word.Document    wdoc;
		object isVisible = true;
		object readOnly  = false;

		
	

		public WordLibs(string impresora)
		{
			//
			// TODO: Add constructor logic here
			//

            wapp = new Microsoft.Office.Interop.Word.Application();
			wapp.ActivePrinter = impresora;
			wapp.Visible = false;

			// Agregado por Luis F. - 21/04/2008
            Microsoft.Office.Interop.Word.DefaultWebOptions webOptions = wapp.DefaultWebOptions();
			webOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
		

		}
		public WordLibs()
		{
			//
			// TODO: Add constructor logic here
			//

            wapp = new Microsoft.Office.Interop.Word.Application();
			//wapp.ActivePrinter = impresora;
			wapp.Visible = false;
			
			// Agregado por Luis F. - 21/04/2008
            Microsoft.Office.Interop.Word.DefaultWebOptions webOptions = wapp.DefaultWebOptions();
			webOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

            

		}

		public void setActivePrinter(string impresora) 
		{
			wapp.ActivePrinter = impresora;
		}

		#region Agregar Documento
		public void agregarDocumento (string path)
		{
			archivo = @path;
			
			/*wdoc = wapp.Documents.Add( ref archivo,  ref oMissing, 
									   ref oMissing, ref oMissing );*/
									   

			
			wdoc = wapp.Documents.Open(ref  archivo,  ref  oMissing, ref readOnly, 
				ref  oMissing, ref  oMissing, ref oMissing, 
				ref  oMissing, ref  oMissing, ref oMissing, 
				ref  oMissing, ref  oMissing, ref isVisible, 
				ref  oMissing, ref  oMissing, ref oMissing,ref oMissing );
		   
		}
		#endregion

        #region Set Path Working Folder
        public void setWorkingDirectory(string path)
        {
            //w.Options.DefaultFilePath(Microsoft.Office.Interop.Word.WdDefaultFilePath.wdUserTemplatesPath) = working_folder
            //wapp.Options.DefaultFilePath(Microsoft.Office.Interop.Word.WdDefaultFilePath.wdUserTemplatesPath) = path;                
        }
        #endregion Set Path Working Folder



        #region Guardar Documento
        public void guardarComoHTML ()
		{
		  object myTrue = true;
		  string docHTML = wdoc.Name.Replace(".doc",".html");
		 
		  object fileToSave =@wdoc.Path.ToString() + @"\" + docHTML;
		  
	
		/*[ggaleano 23/05/2008] Se modificó el formato de guardado del HTML
		 * debido a que si no se guarda el doc como HTML filtrado, se agregan 
		 * marcas propias del Word que hacen que al abrirse el mail en un cliente
		 * de correo como por ejemplo el Outlook se emita un mensaje acerca de contenido
		 * Active X, que no ningún tipo de transformación en la información contenida en el 
		 * doc sino más bien incómoda al usuario.*/
  		  //object fileFormat = Word.WdSaveFormat.wdFormatHTML;
          object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML;



          object Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

		  wdoc.SaveAs(ref fileToSave, ref fileFormat, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref Encoding,ref oMissing,ref myTrue ,ref oMissing,ref oMissing);

		}

        public void guardarComoXML()
        {
            object myTrue = true;
            string docXML = wdoc.Name.Replace(".doc", ".xml").Replace(".rtf", ".xml");
            
            object fileToSave = @wdoc.Path.ToString() + @"\" + docXML;


            object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXML;
            
            object Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

            wdoc.SaveAs(ref fileToSave, ref fileFormat, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref Encoding, ref oMissing, ref myTrue, ref oMissing, ref oMissing);

        }

        public void guardarComoRTF()
        {
            object myTrue = true;
            string docRTF = wdoc.Name.Replace(".doc", ".rtf").Replace(".xml", ".rtf");

            object fileToSave = @wdoc.Path.ToString() + @"\" + docRTF;


            object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;

            object Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

            wdoc.SaveAs(ref fileToSave, ref fileFormat, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref 
				      oMissing, ref oMissing, ref oMissing, ref Encoding, ref oMissing, ref myTrue, ref oMissing, ref oMissing);

        }


		// Agregado por Luis F. -  24/03/2008
		public void guardarComoUnSoloArchivoHTML ()
		{
			object myTrue = true;
			string docHTML = wdoc.Name.Replace(".doc",".mhtml");
		 
			object fileToSave =@wdoc.Path.ToString() + @"\" + docHTML;

            object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatWebArchive;
	
			wdoc.SaveAs(ref fileToSave, ref fileFormat, ref oMissing, ref 
				oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref 
				oMissing, ref oMissing, ref oMissing,ref oMissing,ref oMissing,ref myTrue ,ref oMissing,ref oMissing);

		}
		#endregion


		#region Imprimir Documento
		public void imprimirDocumento ()
		{
			
			wdoc.PrintOut(ref myTrue,
				ref myFalse,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing, //cantidad de copias
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing );

			//this.cerrarDocumento();

			// Nos aseguramos que todos los documentos fueron enviados de la cola
				
			while(wapp.BackgroundPrintingStatus > 0)
			{
				System.Threading.Thread.Sleep(250);
			}

			this.borrarArchivo();

		}

		public void imprimirDocNoBorrar ()
		{

			wdoc.PrintOut(ref myTrue,
				ref myFalse,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing, //cantidad de copias
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing );

			//this.cerrarDocumento();

			// Nos aseguramos que todos los documentos fueron enviados de la cola
				
			while(wapp.BackgroundPrintingStatus > 0)
			{
				System.Threading.Thread.Sleep(250);
			}
            
		}

		#endregion

		#region Imprimir Documento
		public void imprimirDocumento (int copias)
		{
			object w_copias= 1;

			if ( copias > 0 )
			{
				w_copias= copias;
			}

			wdoc.PrintOut(ref myTrue,
				ref myFalse,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref w_copias, 
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing );


			// Nos aseguramos que todos los documentos fueron enviados de la cola
				
			while(wapp.BackgroundPrintingStatus > 0)
			{
				System.Threading.Thread.Sleep(250);
			}

			this.borrarArchivo();

		}
		#endregion

		#region Imprimir Preliminar
		public void imprimirPreliminar ()
		{
			wapp.Visible = true;
			    
			wdoc.PrintPreview();
			
		}
		#endregion 

		public void imprimirTiff(object tempTiffFile)
		{
			object w_copias = 1;
			wapp.ActivePrinter = "Microsoft Office Document Image Writer";  
			wdoc.PrintOut(ref myTrue,
				ref oMissing,
				ref oMissing,
				ref tempTiffFile,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref w_copias, 
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing,
				ref oMissing );  

			while(wapp.BackgroundPrintingStatus > 0)
			{
				System.Threading.Thread.Sleep(250);
			}

            wdoc.Close(ref myTrue, ref oMissing, ref oMissing);
                            
		}

		#region Cerrar Documento
		public void cerrarDocumento ()
		{
			//	wdoc.Close(ref myFalse, ref oMissing, ref oMissing);
			wdoc.Close(ref myTrue , ref oMissing, ref oMissing);
			
		}
		#endregion


		#region Cerrar Word
		public void cerrarWord() 
		{
			wapp.Quit(ref myFalse, ref oMissing, ref oMissing);
		}
		#endregion

		#region Borrar Archivo
		public void borrarArchivo() 
		{
			this.cerrarDocumento();
			//System.IO.FileInfo fileInfo  = new System.IO.FileInfo(@archivo);
			try
			{
				System.IO.File.Delete(archivo.ToString());
			}

			catch( Exception theException )
			{
				
				MessageBox.Show(theException.Message.ToString(),"Error"); 
			}


		}
		#endregion



	}
}
