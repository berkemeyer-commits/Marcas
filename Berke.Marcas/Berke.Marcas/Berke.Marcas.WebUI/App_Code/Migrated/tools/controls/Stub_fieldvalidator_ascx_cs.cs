//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\tools\controls\Stub_fieldvalidator_ascx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_FieldValidator' in file 'tools\controls\fieldvalidator.ascx.cs'.
// This allows the the base class to be referenced by all code files in your project.
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

abstract public class FieldValidator :  System.Web.UI.UserControl
{

		public static string TYPE_REQUIRED = "required";


		public static string TYPE_NOREQUIRED = "noRequired";


		public static string TYPE_ONEREQUIRED = "oneRequired";


		public static string DEPENDSTYPE_EXIST = "haveValue";


		public static string DEPENDSTYPE_NOEXIST = "haveNoValue";

		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("tipo de requerimiento")]
		public abstract string Type
		{
		  get;
		  set;
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Control que debe verificarse")]
		public abstract string ControlToValidate
		{
		  get;
		  set;
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Mensaje en caso de no cumplirse la condición")]
		public abstract string Message
		{
		  get;
		  set;
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Tipo de dato del campo")]
		public abstract string DataType
		{
		  get;
		  set;
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Controles de los cuales depende la evaluación de la condicion")]
		public abstract string Depends
		{
		  get;
		  set;
		}
		[Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("Tipo de dependencia")]
        public abstract string DependsType
		{
		  get;
		  set;
		}


}



}
