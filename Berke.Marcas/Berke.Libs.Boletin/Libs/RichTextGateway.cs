using System;

namespace Berke.Libs.Boletin.Libs
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Facilita el formateo de un RichTextBox
	/// Autor: (probablemente) Nestor Cáceres
	/// Modificado por mbaez
	/// </summary>
	public class RichTextGateway
	{
		private Font _font;
		private string _fontName;
		private int _fontSize;
		private Color _color;
		private RichTextBox _rText;
		private bool _isBold;
		private bool _isUnderLined;

		// Constructor 
		public RichTextGateway(RichTextBox rText )
		{
			_rText = rText;
			_fontName = "Arial";
			_fontSize = 12;
			SetFont();
			_color = Color.Black;
			_isBold = false;
			_isUnderLined = false;
		}
		// Propiedades 
		public string FontName { set { _fontName = value;  } }	
		public int FontSize { set { _fontSize = value; } }	
		public Color FontColor { set { _color = value; } }
		public bool Bold { set { _isBold = value; } }
		public bool SubRayado { set { _isUnderLined = value; } }
		
		// Metodos 
		private void SetFont()
		{
			System.Drawing.FontStyle fnt = new FontStyle();
			if( _isBold ) fnt = System.Drawing.FontStyle.Bold;
			if( _isUnderLined ) fnt = fnt | System.Drawing.FontStyle.Underline;
			_font = new Font( _fontName, _fontSize, fnt );
			this._rText.SelectionFont = _font;
			this._rText.SelectionColor = _color;
		}


		public void Clear(){ _rText.Clear();}

		public void Write( string texto )
		{
			SetFont();
			_rText.SelectedText  = texto;
		}
		
		public void Write( string texto , bool bold )
		{
			bool antBold = _isBold;
			_isBold = bold;
			SetFont();
			_rText.SelectedText  = texto;	
			_isBold = antBold;

		}
	
		public void Write( string texto ,Color color)
		{
			Color antColor = _color;
			_color = color;
			SetFont();
			_rText.SelectedText  = texto;	
			_color = antColor;

		}

		public void Write( string texto ,Color color, bool bold )
		{
			Color antColor = _color;
			_color = color;
			Write( texto, bold );
			_color = antColor;

		}

	}// end class	
}
