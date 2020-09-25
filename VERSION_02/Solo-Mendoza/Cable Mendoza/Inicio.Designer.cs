namespace Cable_Mendoza
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxDNI = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.btnDNI = new System.Windows.Forms.Button();
            this.groupBoxDNI.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Symbol", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Black;
            this.lblTitulo.Location = new System.Drawing.Point(27, 27);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(552, 86);
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "Sistema de Pagos";
            // 
            // groupBoxDNI
            // 
            this.groupBoxDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBoxDNI.Controls.Add(this.label1);
            this.groupBoxDNI.Controls.Add(this.lblDNI);
            this.groupBoxDNI.Controls.Add(this.txtDNI);
            this.groupBoxDNI.Controls.Add(this.btnDNI);
            this.groupBoxDNI.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.groupBoxDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDNI.Location = new System.Drawing.Point(47, 294);
            this.groupBoxDNI.Name = "groupBoxDNI";
            this.groupBoxDNI.Size = new System.Drawing.Size(719, 383);
            this.groupBoxDNI.TabIndex = 5;
            this.groupBoxDNI.TabStop = false;
            this.groupBoxDNI.Text = "D.N.I.  o  Numero de Socio";
            this.groupBoxDNI.Enter += new System.EventHandler(this.groupBoxDNI_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(556, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 42);
            this.label1.TabIndex = 9;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.ForeColor = System.Drawing.Color.Red;
            this.lblDNI.Location = new System.Drawing.Point(303, 188);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(0, 29);
            this.lblDNI.TabIndex = 8;
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(126, 127);
            this.txtDNI.MaxLength = 8;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(478, 49);
            this.txtDNI.TabIndex = 0;
            this.txtDNI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // btnDNI
            // 
            this.btnDNI.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDNI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDNI.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold);
            this.btnDNI.ForeColor = System.Drawing.Color.White;
            this.btnDNI.Location = new System.Drawing.Point(240, 239);
            this.btnDNI.Name = "btnDNI";
            this.btnDNI.Size = new System.Drawing.Size(263, 54);
            this.btnDNI.TabIndex = 5;
            this.btnDNI.Text = "CONSULTAR";
            this.btnDNI.UseVisualStyleBackColor = false;
            this.btnDNI.Click += new System.EventHandler(this.btnDNI_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cable_Mendoza.Properties.Resources.FondoVerde2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 746);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxDNI);
            this.Controls.Add(this.lblTitulo);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Pagos empresa de cable Mendoza";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxDNI.ResumeLayout(false);
            this.groupBoxDNI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnDNI;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.Label label1;
    }
}


