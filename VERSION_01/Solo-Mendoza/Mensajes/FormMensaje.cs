using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Mensajes
{
    public partial class FormMensaje : Form
    {
        public FormMensaje(string texto, string titulo)
        {
            InitializeComponent();
            this.lblMensaje.Text = texto;
            this.lblTitulo.Text = titulo;

            string path = Directory.GetCurrentDirectory();
            path += "\\recursos\\Check.png";
            pictureBox1.Image = Image.FromFile(path);

        }

        public void esperaYCierra()
        {
            Thread.Sleep(300);
            this.Close();
        }

        private void lblX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
