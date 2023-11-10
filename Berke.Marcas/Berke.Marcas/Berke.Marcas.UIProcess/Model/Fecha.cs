using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class Fecha
	public class Fecha
	{
		#region SumarPlazo
		public static DateTime SumarPlazo( DateTime fechaInicial, int plazo, int unidadID )
		{
			#region Parametros
			Berke.DG.ViewTab.ParamTab inTB =   new Berke.DG.ViewTab.ParamTab();

			inTB.NewRow(); 
			inTB.Dat.Entero			.Value = plazo;				//plazo
			inTB.Dat.Fecha			.Value = fechaInicial;		//fechaInicial
			inTB.PostNewRow(); 

			inTB.NewRow(); 
			inTB.Dat.Entero			.Value = unidadID;		// unidadID
			inTB.PostNewRow(); 

			#endregion

			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "Fecha_SumarPlazo" , tmp_InDS );
			
			Berke.DG.ViewTab.ParamTab outTB	=  new Berke.DG.ViewTab.ParamTab( tmp_OutDS.Tables[0] );

			return outTB.Dat.Fecha.AsDateTime; // Fecha incrementada
		}
		#endregion SumarPlazo 
	
	}
	#endregion class Fecha

}// end namespace Berke.Marcas.UIProcess.Model