using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Berke.Libs.Boletin.Libs
{
	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblInput;
		private System.Windows.Forms.TextBox txtInput;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public static int BUTTON_ACEPTAR  = 0;
		public static int BUTTON_CANCELAR = 1;
		private System.Windows.Forms.Button btnAceptar;
		private System.Windows.Forms.Button btnCancelar;

		#region Atributos
		int clickedButton;
		string txtObligatorio;
		#endregion Atributos

		public InputBox()
		{
			InitializeComponent();
			clickedButton = -1;
			txtObligatorio = null;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InputBox));
			this.lblInput = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.btnAceptar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblInput
			// 
			this.lblInput.Location = new System.Drawing.Point(40, 8);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(312, 24);
			this.lblInput.TabIndex = 0;
			this.lblInput.Text = "lblInput";
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(40, 36);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(312, 32);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "";
			// 
			// btnAceptar
			// 
			this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
			this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAceptar.Location = new System.Drawing.Point(224, 75);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size(72, 24);
			this.btnAceptar.TabIndex = 10;
			this.btnAceptar.Text = "Aceptar";
			this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnAceptar.Click += new System.EventHandler(this.btnAsociarProp_Click);
			// 
			// btnCancelar
			// 
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
			this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancelar.Location = new System.Drawing.Point(112, 75);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(72, 24);
			this.btnCancelar.TabIndex = 18;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			// 
			// InputBox
			// 
			this.AcceptButton = this.btnAceptar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancelar;
			this.ClientSize = new System.Drawing.Size(408, 102);
			this.Controls.Add(this.btnCancelar);
			this.Controls.Add(this.btnAceptar);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.lblInput);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(416, 136);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(416, 136);
			this.Name = "InputBox";
			this.ShowInTaskbar = false;
			this.Text = "InputBox";
			this.ResumeLayout(false);

		}
		#endregion

		public void setLabel(string label)
		{
			this.lblInput.Text =  label;
		}
		public void setTitle(string title) 
		{
			this.Text = title;
		}
		public void setText(string text)
		{
			this.txtInput.Text = text;
		}
		public string getText()
		{
			return this.txtInput.Text;
		}
		public int getClickedButton()
		{
			return clickedButton;
		}

		private void btnCancelar_Click(object sender, System.EventArgs e)
		{
			this.clickedButton = InputBox.BUTTON_CANCELAR;
			this.Close();
		}

		private void btnAsociarProp_Click(object sender, System.EventArgs e)
		{
			if (txtObligatorio != null && (txtInput.Text.Trim().Length == 0))
			{
				this.Text += " -> " + txtObligatorio;
				txtInput.BackColor = System.Drawing.Color.Yellow;
				return;
			}
			this.clickedButton = InputBox.BUTTON_ACEPTAR;
			this.Close();
		}
		public void setTextObligatorio(string txt)
		{
			this.txtObligatorio = txt;
		}


		
	}
}
