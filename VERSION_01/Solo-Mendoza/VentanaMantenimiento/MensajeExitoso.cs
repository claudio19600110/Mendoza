using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VentanaMantenimiento
{
    public partial class MensajeExitoso : Form
    {
        public MensajeExitoso()
        {
            InitializeComponent();

            string path = Directory.GetCurrentDirectory();
            path += "\\iconos\\done128.png";
            pictureBox1.Image = Image.FromFile(path);
        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
