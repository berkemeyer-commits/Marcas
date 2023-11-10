using System;
using System.Data;

namespace Berke.Marcas.UIProcess.Model
{

	#region class NizaEdicion
	public class NizaEdicion
	{
		#region Read
		public static Berke.DG.DBTab.NizaEdicion Read( Berke.DG.DBTab.NizaEdicion inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "NizaEdicion_Read" , tmp_InDS );
			
			Berke.DG.DBTab.NizaEdicion outTB	=  new Berke.DG.DBTab.NizaEdicion( tmp_OutDS.Tables[0] );

			return outTB;

			
		}
		#endregion Read 
	
		#region ReadForSelect
    
		public static Berke.DG.ViewTab.ListTab ReadForSelect()
		{
			Berke.DG.DBTab.NizaEdicion inTB =   new Berke.DG.DBTab.NizaEdicion();
			
			#region Asignar Parametros
			inTB.DisableConstraints();
			inTB.NewRow(); 
			inTB.Dat.Descrip			.Value = "";   //nvarchar Oblig.
			inTB.PostNewRow(); 
			#endregion Asignar Parametros
		
			Berke.DG.DBTab.NizaEdicion outTB =  Berke.Marcas.UIProcess.Model.NizaEdicion.Read( inTB );

			Berke.DG.ViewTab.ListTab ret = new Berke.DG.ViewTab.ListTab();
			for( outTB.GoTop() ; ! outTB.EOF; outTB.Skip() )
			{
				ret.NewRow();
				ret.Dat.ID.Value = outTB.Dat.ID.AsInt;
				ret.Dat.Descrip.Value = outTB.Dat.Descrip.AsString;
				ret.PostNewRow();
			}
			return ret;		  
		}

		#endregion



	}
	#endregion class NizaEdicion



}// end namespace Berke.Marcas.UIProcess.Model