namespace Berke.Libs.WebBase.Controls
{
	using System;
	using System.Web.UI;
	using System.Data;
	using Berke.Libs.Base;

	/// <summary>
	/// BaseEditingControl
	/// </summary>
	public class BaseEditingControl : UserControl
	{
		protected IABMProvider _abmProvider;

		#region Properties
		public IABMProvider ABMProvider {
			get {
				return _abmProvider;
			}
		}
		#endregion

		public virtual void Hydrate( DataSet dataSet ){}
		public virtual DataSet Dehydrate(){ return null;}
	}
}
