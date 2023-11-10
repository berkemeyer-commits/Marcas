//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\tools\controls\Stub_fieldvalidator_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'tools\controls\fieldvalidator.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace Berke.Marcas.WebUI.Tools.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

	/// <summary>
	///		Summary description for FormValidator.
	/// </summary>
	[DefaultProperty("Message")]
	public partial class Migrated_FieldValidator : FieldValidator
	{

		#region Atributos

		private string type = "";
		private string controlToValidate = "";
		private string message = "";
		private string depends = "";
		private string dependsType = "";
		private string dataType = "";
		#endregion Atributos

		protected void Page_Load(object sender, System.EventArgs e)
		{
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


		#region Propiedades
//		[Bindable(true),
//		Category("Appearance"),
//		DefaultValue(""),
//		Description("tipo de requerimiento")]
//		public string Type
//		{
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("tipo de requerimiento")]
		public override string Type
		{
			get
			{
				return type;
			}
 
			set
			{
				type = value;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Control que debe verificarse")]
		public override string ControlToValidate
		{
			get
			{
				return controlToValidate;
			}
 
			set
			{
				controlToValidate = value;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Mensaje en caso de no cumplirse la condición")]
		public override string Message
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
		Description("Tipo de dato del campo")]
		public override string DataType
		{
			get
			{
				return dataType;
			}
 
			set
			{
				dataType = value;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Controles de los cuales depende la evaluación de la condicion")]
		public override string Depends
		{
			get
			{
				return depends;
			}
 
			set
			{
				depends = value;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Tipo de dependencia")]
        public override string DependsType
		{
			get
			{
				return dependsType;
			}
 
			set
			{
				dependsType = value;
			}
		}
		#endregion Propiedades

		protected override void Render(System.Web.UI.HtmlTextWriter output)
		{
			string txt =  "     if ({0}) {{ \n"+
						  "          valid = false;  \n " +
						  "          msg = msg + '{1}' +'<br>'; \n"+
						  "     }} \n"; 

			string funcName = "!isValidRequired('{0}')";	
			string funcDep  = "isValidRequired('{0}')";	

			string boolOp = "";			
			string [] controles = ControlToValidate.Split(",".ToCharArray());
			if (type == TYPE_REQUIRED)
			{
				boolOp = " || ";
			}
			else if (type== TYPE_ONEREQUIRED)
			{
				boolOp = " && ";
			}
			else if (type== TYPE_NOREQUIRED)
			{
				boolOp = " && ";
				funcName = "isValidRequired('{0}')";
			}

			string funcNames   = this.getListaCondiciones(funcName, ControlToValidate,boolOp);
			string funcDepends = this.getListaCondiciones(funcDep , Depends, boolOp);

			if (funcDepends != "")
			{
				if (DependsType == DEPENDSTYPE_NOEXIST)
				{
					funcNames = "(!(" + funcDepends + ")) && (" + funcNames +")";
				}
				else 
				{
					funcNames = "(" + funcDepends + ") && (" + funcNames +")";
				}
			}

			txt = string.Format(txt,funcNames, Message);
			output.Write(txt);				
		}
		private string getListaCondiciones(string template, string controlLst, string boolOp)
		{
			string funcNames = "";
			string [] controles = controlLst.Split(",".ToCharArray());

			for(int i=0; i<controles.Length; i++)
			{
				if (controles[i] != "")
				{
					string funcFmt = string.Format(template, controles[i]);
					if (i==0)
					{
						funcNames = funcFmt;
					}
					else 
					{
						funcNames += boolOp + funcFmt;
					}
				}
			}
			return funcNames;
		}

	}
}
