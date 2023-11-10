using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Berke.DG;
using Berke.Libs.Base;

namespace Berke.Libs.Boletin.Libs
{
	/// <summary>
	/// Summary description for MergeLibs.
	/// </summary>
	public class MergeLibs
	{
		public static string[] aMeses = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };
		public static string REGISTRO   = "1";
		public static string RENOVACION = "2";
		public static string OPOSICION  = "14";
		public static string mi_enter= "\r\n";

		public MergeLibs()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region FechaMasPlazo
		/* Calcula la fecha que será cuando haya pasado un "plazo" dado, pertiendo de "fechaIni" */

		public static DateTime FechaMasPlazo( DateTime fechaIni, int tramiteSitID, Berke.Libs.Base.Helpers.AccesoDB db )
		{
			

			Berke.DG.DBTab.Tramite_Sit trmSitID = new Berke.DG.DBTab.Tramite_Sit( db );
			trmSitID.Adapter.ReadByID( tramiteSitID );
			return FechaMasPlazo( fechaIni, trmSitID.Dat.Plazo.AsInt, trmSitID.Dat.UnidadID.AsInt, db );
		}

		public static DateTime FechaMasPlazo( DateTime fechaIni, int plazo, int unidadID, Berke.Libs.Base.Helpers.AccesoDB db )
		{
			DateTime fechaFin = fechaIni;
			switch( unidadID )
			{
			
				case 1	:	// Días hábiles	dd
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
				case 2	:	// 	Mes	mm
					fechaFin = fechaIni.AddMonths( plazo );
					break;
				case 3	:	// 	Año	aaaa
					fechaFin = fechaIni.AddYears( plazo );
					break;
				case 4	:	// 	Días calendario	dc
					fechaFin = fechaIni.AddDays( Convert.ToDouble( plazo ) );
					break;
			}

			
			return fechaFin;
		}
		#endregion FechaMasPlazo


		public static string DateString( DateTime fecha, int idiomaID )

		{

			string pattern = "";

			switch( idiomaID )

			{ 

				case 1 : // Ingles

					pattern = "MMM/dd/yyyy";

					break;

				case 2 : // Español

					pattern = "dd/MMM/yyyy";

					break;

				case 3 : // Aleman

					pattern = "dd-MMM-yyyy";

					break;

				case 4 : // Frances

					pattern = "dd/MMM/yyyy";

					break;

				default :

					pattern = "dd/MMM/yyyy";

					break;

			}

			return DateString( fecha, idiomaID, pattern );

		}


		public static string DateString( DateTime fecha, int idiomaID, string pattern )

		{

			System.Globalization.CultureInfo cul;

			switch( idiomaID )

			{ 

				case 1 : // Ingles

					cul = new System.Globalization.CultureInfo("en-US", false);

					break;

				case 2 : // Español

					cul = new System.Globalization.CultureInfo("es-ES", false);

					break;

				case 3 : // Aleman

					cul = new System.Globalization.CultureInfo("de-DE", false);

					break;

				case 4 : // Frances

					cul = new System.Globalization.CultureInfo("fr-FR", false);

					break;

				default :

					//cul = new System.Globalization.CultureInfo("en-US", false);
					cul = new System.Globalization.CultureInfo("es-PY", false);

					break;

			}


			string x = fecha.ToString(pattern, cul );

			return x;

		}




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


		#region Traducir fecha
		public static string traducirFecha(string fecha, string idiomaID, Berke.Libs.Base.Helpers.AccesoDB mydb)
		{   

			Berke.DG.DBTab.Mes mes;
			mes = new Berke.DG.DBTab.Mes(mydb);

			string f="";
			int dd=0; int mm=0; int aa=0;

			DateTime fec = DateTime.Parse(fecha.ToString());
					
			dd= fec.Day;  mm= fec.Month;  aa= fec.Year;

			mes.ClearFilter();
			mes.Dat.ididioma.Filter = ObjConvert.GetFilter(idiomaID.ToString());
			mes.Dat.Orden.Filter    = ObjConvert.GetFilter(mm.ToString());
			mes.Adapter.ReadAll();

			if ( idiomaID.ToString()=="1" ) /*ingles*/
			{
				f =  mes.Dat.Mes.AsString + " " + dd.ToString() + ", " + aa.ToString() ;    
			}

			if ( idiomaID.ToString()=="2" ) /*español*/
			{
				f = dd.ToString() + " de " + mes.Dat.Mes.AsString  + " de " + aa.ToString() ;    
			}

			if ( idiomaID.ToString()=="3" ) /*aleman*/
			{
				f = dd.ToString() + " " + mes.Dat.Mes.AsString  + " " + aa.ToString() ;    
			}

			if ( idiomaID.ToString()=="4" ) /*frances*/
			{
				f = dd.ToString() + " " + mes.Dat.Mes.AsString  + " " + aa.ToString() ;    
			}
			

			return f;
		}

		#endregion



	}
}
