using System;
using System.Data;
namespace Berke.Marcas.UIProcess.Model
{
	/// <summary>
	/// Summary description for Corresp.
	/// </summary>
	public  class Corresp
	{
		
		#region ReadList
		public static Berke.DG.ViewTab.vCorrespondencia ReadList( Berke.DG.ViewTab.vCorrespondencia inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Corresp_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vCorrespondencia outTB	=  new Berke.DG.ViewTab.vCorrespondencia( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

		#region digitalDocPath
		public static string digitalDocPath( int pAnio, int pNumero, int area )
		{
		
			string fileTemplate = "";
			string numero = pNumero.ToString();

			switch( area )
			{
				case	1  : //		Marcas	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 3  : //		Poder
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 6  : //		Litigios 
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosAdm\{0}\TIF\{1}.tif";
					break;
				case 7  : //		Patentes
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Patentes\{0}\TIF\{1}.tif";
					break;
				case 8  : //		Legal Division	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\LitigiosJud\{0}\TIF\{1}.tif";
					break;
				case 10 : //		General	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
				case 14 : //		Contabilidad	
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Contabilidad\{0}\TIF\{1}.tif";
					break;
				default :
					fileTemplate = @"\\trinity\Ofdig$\Correspondencia\Marcas\{0}\TIF\{1}.tif";
					break;
			}
			if( fileTemplate == "" )
			{
				return "";
			}

			//			string anchorTemplate = @"<A onclick=""window.open('File:{0}')"" href=""{0}"">&nbsp;&nbsp;Doc.Digital </a>";
			//string anchorTemplate = @"<A href=""{0}"">&nbsp;&nbsp;{1} </a>";

			string anchorTemplate = @"&nbsp;&nbsp;<a href=""{0}""><img border=0 alt='Ver Documento' src='../tools/imx/tif.gif'> </a>";
			
			#region Llenar numero con ceros a la izquierda
			if( numero.Length < 5 && numero.Length > 0 )
			{
				numero = ((string)"00000").Substring(0, 5 - numero.Length) + numero;
			}
			#endregion

			string arch = string.Format( fileTemplate, pAnio.ToString(), numero );
			string referencia ="";
			System.IO.FileInfo inf = new System.IO.FileInfo(arch);
			if( inf.Exists )
			{ 
				referencia = string.Format( anchorTemplate, arch );
			}
			return referencia;
		}
		#endregion digitalDocPath

	}
}
