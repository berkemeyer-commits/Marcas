using System;
 
namespace Berke.Libs.WebBase.Controls
{
	using System.Web.UI.WebControls;
	using System.ComponentModel;
	using System.Data;
	
	/// <summary>
	/// DropDown: extends DropDownList with a couple of methods
	/// </summary>
	
	[Description("extends DropDownList with a couple of methods")]
	public class DropDown: DropDownList{

		#region Properties
		public string Value {
			get {
				return SelectedValue;
			}set{
				ClearSelection();
				ListItem item = Items.FindByValue( value );
				if( item != null)
					item.Selected = true;
			}
		}
		#endregion

		#region Fill
		[Description("Fills from a Datatable")]
		public void Fill( DataTable list ){
			Fill( list, true );
		}
		
		public void Fill( DataTable list, bool prependBlank ){
			DataSource = list;
			DataValueField = list.Columns[0].ColumnName;
			DataTextField = list.Columns[1].ColumnName;
			DataBind();

			if(prependBlank)
				Items.Insert( 0, string.Empty );
		}
		#endregion
	}
}
