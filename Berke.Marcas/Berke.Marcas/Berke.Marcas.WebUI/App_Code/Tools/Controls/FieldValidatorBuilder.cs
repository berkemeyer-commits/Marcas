using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.ComponentModel;
using System.Collections;

namespace Berke.Marcas.WebUI.Tools.Controls
{
	/// <summary>
	/// Summary description for FieldValidatorBuilder.
	/// </summary>
	public class FieldValidatorBuilder : ControlBuilder
	{
		public override Type GetChildControlType(string tagName, IDictionary attributes)
		{			
			if (tagName == "FieldValidator")
				return typeof(FieldValidator);
			else
				return null;
		}
 
		public override void AppendLiteralString(string s)
		{
		}
		
	}
}
