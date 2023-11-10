using System;
namespace Berke.Libs.WebBase.Helpers
{
	using System.Web.UI.WebControls;
	using System.Collections;
	using System.Data;

	#region GridColInfo class
	public class GridColInfo 
	{
		public string CtrlName;
		public string Header;
		public System.Type CtrlType;
		public bool Visible = true;
		public System.Type DataType;
		public int Width;
		public int Pos=0;
		public bool ReadOnly = false;

		public GridColInfo( string ctrlName, string header, System.Type ctrlType, bool visible, System.Type dataType,int width, int pos )
		{
			CtrlName	= ctrlName;
			Header		= header;
			CtrlType	= ctrlType;
			Visible		= visible;
			DataType	= dataType;
			Width		= width;
			Pos			= pos;
		}

		public GridColInfo( string ctrlName, string header, System.Type ctrlType, bool visible, System.Type dataType,int width, int pos, bool readOnly )
		{
			CtrlName	= ctrlName;
			Header		= header;
			CtrlType	= ctrlType;
			Visible		= visible;
			DataType	= dataType;
			Width		= width;
			Pos			= pos;
			ReadOnly    = readOnly;
		}

		public GridColInfo()
		{
		
		}

	}
	#endregion GridColInfo

	#region GridGW class
	public class GridGW 
	{

		#region Datos Miembros
		private DataGrid _dg;
		private DataTable _dt;
	
		System.Collections.Hashtable lista;
		int colCounter = 0;
		#endregion Datos Miembros

		#region Constructores
		public GridGW()
		{
			init();
		}

		public GridGW( DataGrid dataGrid )
		{
			_dg = dataGrid;
			
			init();
		}
		
		private void init()
		{
			lista = new Hashtable();
		}

		#endregion Constructores

		#region Propiedades
		public DataTable Table
		{
		get{ return _dt;}
		}						  

		#endregion Propiedades

		#region Metodos

		#region AddCol
		private void AddCol(string ctrlName,
							string header, 
							System.Type ctrlType, 
							bool visible,
							System.Type dataType,
							int width ){
			lista.Add( ctrlName, new GridColInfo( ctrlName, header, ctrlType,  visible, dataType,width, colCounter++ ));
		}

		private void AddCol(string ctrlName,
			string header, 
			System.Type ctrlType, 
			bool visible,
			System.Type dataType,
			int width, bool readOnly )
		{
			lista.Add( ctrlName, new GridColInfo( ctrlName, header, ctrlType,  visible, dataType,width, colCounter++, readOnly ));
		}

		#endregion AddCol

		#region AddText
		public void AddText( string ctrlName, string header, int width )
		{
			AddCol( ctrlName, header, typeof(TextBox ), true, typeof( string), width );
		}
		#endregion AddText

		#region AddCheck
		public void AddCheck( string ctrlName, string header , int width)
		{
			AddCol( ctrlName, header, typeof(CheckBox ), true, typeof( bool ), width );
		}
		#endregion AddCheck

		#region AddLabel
		public void AddLabel( string ctrlName, string header, int width )
		{
			AddCol( ctrlName, header, typeof(Label ), true, typeof( string), width );
		}
		#endregion AddLabel

		#region AddDropDown
		public void AddDropDown( string ctrlName, string header, int width )
		{
			AddCol( ctrlName, header, typeof(DropDownList), true, typeof( string), width );
		}
		#endregion AddDropDown

		#region Inicializar
		public void Inicializar( int cantFilas )
		{
			this._dt = new DataTable();

			#region Agregar Columnas
			foreach( DictionaryEntry obj in lista )
			{
				GridColInfo col = (GridColInfo)obj.Value;
				string name =  col.CtrlName;
				System.Data.DataColumn dc = new DataColumn( col.CtrlName, col.DataType );
				_dt.Columns.Add(dc);
			}
			#endregion Agregar Columnas		

			#region Agregar Filas
			for( int i = 0; i < cantFilas; i++)
			{
				_dt.Rows.Add( new Object[lista.Count]);
			}
			#endregion Agregar Filas
		
			this._dg.DataSource = _dt;
			_dg.DataBind();

			foreach( DictionaryEntry obj in lista )
			{
				GridColInfo col = (GridColInfo)obj.Value;
				foreach( DataGridItem item in _dg.Items )
				{
					((WebControl) item.FindControl(col.CtrlName)).Width = col.Width;
				}
			}
			
		}
		#endregion Inicializar

		#region Set   (by index)
		public void Set( int row, string colName, string text )
		{
			GridColInfo col = (GridColInfo)lista[colName];
			if( col.CtrlType == typeof( CheckBox ) )
			{
				if( text.Substring(0,1).ToUpper() == "Y" || text.Substring(0,1).ToUpper()  == "S" )
				{
					((CheckBox)_dg.Items[row].FindControl(colName)).Checked	= true;
				}
				else
				{
					((CheckBox)_dg.Items[row].FindControl(colName)).Checked	= false;
				}
			} else if( col.CtrlType == typeof( Label ) ) {
				((Label)_dg.Items[row].FindControl(colName)).Text	= text;
			} else if( col.CtrlType == typeof( DropDownList ) ) {
				((DropDownList)_dg.Items[row].FindControl(colName)).SelectedValue	= text;
			} else if( col.CtrlType == typeof( TextBox ) ) {
				 ((TextBox)_dg.Items[row].FindControl(colName)).Text	= text;	 
			}		
		}
		#endregion Set

		#region Set   ( by item)
		public void Set( DataGridItem item, string colName, string text )
		{
			GridColInfo col = (GridColInfo)lista[colName];
			if( col.CtrlType == typeof( CheckBox ) )
			{
				if( text.Substring(0,1).ToUpper() == "Y" || text.Substring(0,1).ToUpper()  == "S" )
				{
					((CheckBox)item.FindControl(colName)).Checked	= true;
				}
				else
				{
					((CheckBox)item.FindControl(colName)).Checked	= false;
				}
			} else if( col.CtrlType == typeof( Label ) ) {
				((Label)item.FindControl(colName)).Text	= text;
			} else if( col.CtrlType == typeof( DropDownList ) ) {
				((DropDownList)item.FindControl(colName)).SelectedValue	= text;
			} else if( col.CtrlType == typeof( TextBox ) ) {
				 ((TextBox)item.FindControl(colName)).Text	= text;	 
			}
		
		}
		#endregion Set

		#region SetReadOnly
		public void SetReadOnly ( DataGridItem item, string colName, bool readOnly )
		{
			GridColInfo col = (GridColInfo)lista[colName];
			if( col.CtrlType == typeof( CheckBox ) )
			{
					((CheckBox)item.FindControl(colName)).Enabled	= readOnly;
			} 
			else if( col.CtrlType == typeof( DropDownList ) ) 
			{
				((DropDownList)item.FindControl(colName)).Enabled = readOnly;
			} 
			else if( col.CtrlType == typeof( TextBox ) ) 
			{
				((TextBox)item.FindControl(colName)).ReadOnly = readOnly;
			}
		
		}
		#endregion SetReadOnly

		#region GetText    ( by item)
		public string GetText( DataGridItem item, string colName)
		{
			string ret = "";
			System.Web.UI.Control ctrl = item.FindControl(colName);
			if( ctrl is Label )
			{
				ret = ((Label) ctrl).Text;
			} 
			else if(  ctrl is TextBox )
			{
				ret = ((TextBox) ctrl).Text;
			}
			else if(  ctrl is DropDownList )
			{
				ret = ((DropDownList) ctrl).SelectedValue;
			}
			else if ( ctrl is CheckBox )
			{
				if( ((CheckBox)ctrl).Checked )	
				{
					ret = "Si";
				}
				else
				{
					ret = "No";
				}
			}
			return ret;
		}
		#endregion GetText
		
		#region GetText  ( by index )
		public string GetText( int row, string colName )
		{
			string ret = "";
			if( row  < _dg.Items.Count && row >= 0 )
			{
				
				System.Web.UI.Control ctrl = _dg.Items[row].FindControl(colName);
				if( ctrl is Label )
				{
					ret = ((Label) ctrl).Text;
				} else if(  ctrl is TextBox ){
					ret = ((TextBox) ctrl).Text;
				} else if(  ctrl is DropDownList ) {
					ret = ((DropDownList) ctrl).SelectedValue;
				}
				else if ( ctrl is CheckBox )
				{
					if( ((CheckBox)ctrl).Checked )	{
						ret = "Si";
					}else{
						ret = "No";
					}
				}
			}
			return ret;
		}
		#endregion GetText

		#region SetToolTip
		public void SetToolTip( int row, string colName, string text )
		{
			GridColInfo col = (GridColInfo)lista[colName];
			if( col.CtrlType == typeof( CheckBox ) )
			{
				((CheckBox)_dg.Items[row].FindControl(colName)).ToolTip	= text;

			}
			else if( col.CtrlType == typeof( Label ) ){
				((Label)_dg.Items[row].FindControl(colName)).ToolTip	= text;
			}if( col.CtrlType == typeof( TextBox ) ) {
				 ((TextBox)_dg.Items[row].FindControl(colName)).ToolTip	= text;	 
			 }
		
		}
		#endregion SetToolTip

		#region Visible
		public void Visible( string colName, bool visible  )
		{
			GridColInfo col = (GridColInfo)lista[colName];
			_dg.Columns[ col.Pos ].Visible = visible;
		}
		#endregion Visible

		
		#endregion Metodos


	}
	#endregion GridGW class

}


namespace Berke.Libs.WebBase.Helpers
{
	using System.Web.UI.WebControls;
	using System.Data;
	using Base.Helpers;

	/// <summary>
	/// Summary description for Grid.
	/// </summary>
	public class Grid
	{
		public static void Bind( DataGrid grid, DataTable tbl ){
//			Nulls.CleanDataTable( tbl );
			grid.DataSource = tbl;
			grid.DataBind();
		}
	}
}
