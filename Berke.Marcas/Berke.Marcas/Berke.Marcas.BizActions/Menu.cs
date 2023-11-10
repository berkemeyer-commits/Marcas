
#region Menu.	ReadAsHTMLString
namespace Berke.Marcas.BizActions.Menu
{
	using System;
	using Framework.Core;
	using System.Data;
	using Libs.Base;
	using Libs.Base.DSHelpers;
	using System.Collections;

	
	public class ReadAsHTMLString: IAction
	{	
		private int MAX_COLUMNS = 3;
		public void Execute( Command cmd ) 
		{		 
			Berke.Libs.Base.Helpers.AccesoDB db = new Berke.Libs.Base.Helpers.AccesoDB();
			db.DataBaseName = (string) Config.GetConfigParam("CURRENT_DATABASE");
			db.ServerName	 = (string) Config.GetConfigParam("CURRENT_SERVER");		
		
			Berke.DG.ViewTab.ParamTab inTB	= new Berke.DG.ViewTab.ParamTab( cmd.Request.RawDataSet.Tables[0]);
		
	
			#region Asigacion de Valores de Salida
			string user =  Berke.Libs.Base.Acceso.GetCurrentUser();
            
			const int charsForTab = 10;

			Berke.DG.DBTab.Menu menu = new Berke.DG.DBTab.Menu( db );
			menu.ClearOrder();
			menu.Dat.Ord.Order = 1;
			menu.Adapter.ReadAll();
			string menuStr= "";

			
			string tabla = "";
			string rows ="";
			ArrayList opciones = new ArrayList();
			for( menu.GoTop(); ! menu.EOF; menu.Skip() )
			{
				string sangria = 	Spaces( menu.Dat.Nivel.AsInt * charsForTab );
				string destino = menu.Dat.Destino.AsString.Trim();
				string param = menu.Dat.Param.AsString.Trim();
				string Texto = menu.Dat.Texto.AsString;
				string codigo = menu.Dat.Cod.AsString;
				string row = "";

				if (destino == "")
				{
					destino = "#";
				}
				
				if( Berke.Libs.Base.Acceso.chkOperacionPermitida( codigo, user, db ))
				{
					if (menu.Dat.Nivel.AsInt == 0)
					{
						if (opciones.Count>0)
						{
							rows=this.organizarRows(opciones);
						}
						tabla = tabla.Replace("@",rows);
						tabla += "<table class=\"td_menu\" width=\"750\"><tr><td colspan=\"3\" class=\"td_header\">"+ menu.Dat.Texto.AsString+"</td></tr>@</table><br>";
						opciones = new ArrayList();
					}
					else 
					{
						if( param != "" )
						{
							destino += "?"+param;
						}
						row = "<a  class=\"opcion"+ menu.Dat.Nivel.AsString+"\"   href=\""+ destino +"\" ><li class=\"menu"+ menu.Dat.Nivel.AsString+"\">"+ Texto + "</li></a>";
						opciones.Add(row);
					}
				}
			}
			rows=this.organizarRows(opciones);
			tabla = tabla.Replace("@",rows);
			//string msg = "<table style='margin-left:5px' class=\"tipMacro\" width=\"750\"><tr><td> Hola " + Acceso.GetCurrentUser() + "!<br> Selecciona una de las opciones del men&uacute; </td></tr></table><br>";
			menuStr = tabla;
	

			// ParamTab
			Berke.DG.ViewTab.ParamTab outTB	=   new Berke.DG.ViewTab.ParamTab();

			outTB.NewRow(); 
			outTB.Dat.Alfa		.Value = menuStr;   //String
			outTB.PostNewRow(); 
		
			#endregion 

			DataSet  tmp_OutDS;
			tmp_OutDS = new DataSet(); tmp_OutDS.Tables.Add( outTB.Table );
		
			cmd.Response = new Response( tmp_OutDS );	
			db.CerrarConexion();
		}

		private string organizarRows(ArrayList opt)
		{
			int nfilas = (int)Math.Ceiling((double)opt.Count/(double)MAX_COLUMNS);
			int size = 100 / MAX_COLUMNS;
			string rows = "";
			string row  = "";
			for(int i=0; i< opt.Count; i++)
			{
				if (i%nfilas == 0)
				{
					rows = rows.Replace("@", row);
					rows += "<td width=\""+size.ToString()+"%\">@</td>";
					row = "";
				}

				row +=(string)opt[i] +"<br>";
	
			}
			rows = rows.Replace("@", row);
			return "<tr>"+rows+"</tr>";
		}

		private string Spaces( int rep ){
			string str="";
			for( int i=0 ; i < rep; i++){
				str+="&nbsp;";
			}
			return str;
		}


		#region Formar opciones - Viejo
		/*
			for( menu.GoTop(); ! menu.EOF; menu.Skip() )
			{
				string sangria = 	Spaces( menu.Dat.Nivel.AsInt * charsForTab );
				string destino = menu.Dat.Destino.AsString.Trim();
				string param = menu.Dat.Param.AsString.Trim();
				string Texto = menu.Dat.Texto.AsString;
				string link = "";
				string codigo = menu.Dat.Cod.AsString;
				if( Berke.Libs.Base.Acceso.OperacionPermitida( codigo ))
				{
			
					if( destino != "")
					{
						if( param != "" )
						{
							destino += "?"+param;
						}
						link = @"<A href=""" + destino + @""" >"+ Texto + "</A>";
						Texto = "";
					}
					if( destino == "" || menu.Dat.Nivel.AsInt == 0 )
					{
						Texto = @"<font class=menutit"+menu.Dat.Nivel.AsInt+" >"+Texto+ "</font> ";
					}
					
					menuStr+= sangria+ Texto+ "&nbsp;" + link + BR;
				}
			}
			menuStr = @"<font size="""+ size +@""">" + menuStr + "</font>";
			*/
		#endregion Formar opciones


	
	} // End ReadAsHTMLString class


}// end namespace 
#endregion 