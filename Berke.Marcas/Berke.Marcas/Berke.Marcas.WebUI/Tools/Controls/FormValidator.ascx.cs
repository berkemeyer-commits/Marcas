namespace Berke.Marcas.WebUI.Tools.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.UI;
	using System.ComponentModel;
	using System.Collections;	

	/// <summary>
	///	 Autor: Marcos Báez
	///	 Control que realiza validación de formularios
	/// </summary>
	[DefaultProperty("Items"),ControlBuilderAttribute(typeof(FieldValidatorBuilder)),ParseChildren(false)]
	public partial class FormValidator : System.Web.UI.UserControl,INamingContainer
	{

		#region Atributos
		string buttonId;
		ArrayList items;
		string message;
		#endregion Atributos

		protected void Page_Load(object sender, System.EventArgs e)
		{
			items = new ArrayList();
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion


		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("id del boton asociado al control")]
		public string ButtonId
		{
			get
			{
				return buttonId;
			}
 
			set
			{
				buttonId = value;
			}
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Mensaje de la ventana popup")]
		public string Message
		{
			get
			{
				return message;
			}
 
			set
			{
				message = value;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("id del boton asociado al control")]
		public System.Collections.ArrayList Items
		{
			get
			{
				return items;
			}
 
			set
			{
				items.Add(value);
			}
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			this.beginTag(writer);
			this.endTag(writer);
		}

		public void beginTag(System.Web.UI.HtmlTextWriter output)
		{
			#region Definición de template
			string cab = @"	<script type='text/javascript'>	
							var btn = document.getElementById('{0}');
							if (btn != null){{
								btn.onclick = {1}_onclick;
							}}" +"\n";
			string functionBegin = @"function {0}_onclick () {{
										var valid = true;
										var msg = '';
									";

			string msgTemplate = @" ""<span class='titulo'>{0}</span><br>"" + msg + "+
								 @" ""<br><input type='submit' class='btn_close' value='Cerrar' onclick='javascript:closeMessage()'>"" ";

			string functionEnd   =  "    if (!valid) displayStaticMessage({0},false); \n" + 
				"    return valid; \n "+
				"}}";
			msgTemplate = string.Format(msgTemplate,Message);
			functionEnd = string.Format(functionEnd,msgTemplate);

			#endregion Definición de template
			
			cab           = string.Format(cab, ButtonId, ButtonId);
			functionBegin = string.Format(functionBegin, ButtonId);
			output.Write(cab);

			output.Write(functionBegin);
			
			for (int i=0; i<Controls.Count; i++)
			{
				Controls[i].RenderControl(output);
			}
			
			output.Write(functionEnd);
		}

		public void endTag(System.Web.UI.HtmlTextWriter output)
		{
			output.Write(@"</script>");			
		}

	}


	/*internal class FieldValidatorBuilder : ControlBuilder
	{
		public override Type GetChildControlType(
			string tagName, IDictionary attributes)
		{			
			if (tagName == "FieldValidator")
				return typeof(FieldValidator);
			else
				return null;
		}
 
		public override void AppendLiteralString(string s)
		{
		}
	}*/


}
