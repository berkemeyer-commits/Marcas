using System;
using System.Data;
using Berke.Libs.Base;

namespace Berke.Marcas.UIProcess.Model
{

	#region class ExpeInstruccion
	public class ExpeInstruccion
	{

		#region ReadByExpeID_asHtml
		public static string ReadByExpeID_asHtml( int expeID ){

			Berke.DG.ViewTab.vInstruccion inTB = new Berke.DG.ViewTab.vInstruccion();
			inTB.NewRow();
			inTB.Dat.ExpedienteID.Value = expeID;
			inTB.PostNewRow();
			DataSet tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeInstruccion_ReadList" , inTB.AsDataSet() );
			
			Berke.DG.ViewTab.vInstruccion instruc	=  new Berke.DG.ViewTab.vInstruccion( tmp_OutDS.Tables[0] );

			string buf1="";
			if( instruc.RowCount > 0 )
			{
				buf1 = "<b>Instrucciones</b><br>";

				buf1+= @"<Table bgColor=""white"" cellSpacing=""0"" cellPadding=""0""  border=""1"">";
				buf1+= @"<tr  >";
				buf1+= @"<tD bgColor=""silver""><P class=""Texto""><b> Fecha		</b></p></td>";
				buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Instrucción	</b></p></td>";
				buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Comentario	</b></p></td>";
				buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Correspondencia	</b></p></td>";
				buf1+= @"<tD bgColor=""silver"" ><P class=""Texto""><b> Usuario	</b></p></td>";

				buf1+= @"</tr>";
				for( instruc.GoTop(); ! instruc.EOF; instruc.Skip() )
				{
	
					string path = "";
					if( instruc.Dat.CorrespNro.AsString != "" )
					{
						path = Berke.Libs.Base.DocPath.digitalDocPath(
							instruc.Dat.CorrespAnio.AsInt, instruc.Dat.CorrespNro.AsInt, 
							1 ); // Corresp del area de Marcas
						/*path = Berke.Marcas.UIProcess.Model.Corresp.digitalDocPath(
							instruc.Dat.CorrespAnio.AsInt, instruc.Dat.CorrespNro.AsInt, 
							1 ); // Corresp del area de Marcas*/
					}
					buf1+= @"<tr>";
					buf1+= @"<tD style=""WIDTH: 70px"" ><P class=""Texto"" align=""center"">" + instruc.Dat.Fecha.AsString		+ "</p> </td>";
					buf1+= @"<tD style=""WIDTH: 260px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Descrip.AsString				+ "</p> </td>";
					buf1+= @"<tD style=""WIDTH: 260px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Obs.AsString				+ "</p> </td>";
					buf1+= @"<tD style=""WIDTH: 100px"" ><P class=""Texto"" align=""center"" ><nobr>" +"&nbsp;"+  instruc.Dat.CorrespNro.AsString+" / "+ instruc.Dat.CorrespAnio.AsString	+path+ "</nobr></p> </td>";
					buf1+= @"<tD style=""WIDTH: 100px"" ><P class=""Texto"" align=""center"">" +"&nbsp;"+  instruc.Dat.Nick.AsString + "</p> </td>";

					buf1+= "</tr>";
				}

				buf1+= "</Table>";
			}
			return buf1;

		}
		#endregion 

		#region Read
		public static Berke.DG.DBTab.Expediente_Instruccion Read( Berke.DG.DBTab.Expediente_Instruccion inTB )
		{
			DataSet  tmp_InDS	= inTB.AsDataSet();
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeInstruccion_Read" , tmp_InDS );
			
			Berke.DG.DBTab.Expediente_Instruccion outTB	=  new Berke.DG.DBTab.Expediente_Instruccion( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion Read 

		#region ReadList
		public static Berke.DG.ViewTab.vInstruccion ReadList( Berke.DG.ViewTab.vInstruccion inTB )
		{
			DataSet  tmp_InDS	= new DataSet(); tmp_InDS.Tables.Add( inTB.Table );
			DataSet  tmp_OutDS;

			tmp_OutDS = (DataSet) Berke.Libs.Gateway.Action.Execute( "ExpeInstruccion_ReadList" , tmp_InDS );
			
			Berke.DG.ViewTab.vInstruccion outTB	=  new Berke.DG.ViewTab.vInstruccion( tmp_OutDS.Tables[0] );

			return outTB;
		}
		#endregion ReadList

	}
	#endregion class ExpeInstruccion

}// end namespace Berke.Marcas.UIProcess.Model
