using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using SesionIniciada;
using System.IO;
using Mensajes;
using System.Threading;
using VentanaMantenimiento;

namespace Cable_Mendoza
{
    public partial class Inicio : Form
    {
        public Deuda deuda = null;
        public Inicio()
        {
            InitializeComponent();
            Archivos archivo = new Archivos();
           
            string ruta = Directory.GetCurrentDirectory();
            ruta += "\\informacion.txt";

            


            Deuda deudaVerificar = null;
            bool retorno = archivo.Leer(ruta, out deudaVerificar);
            if (retorno == true)
            {
                this.deuda = deudaVerificar;
                Archivos.Guardado(deuda.Header.ImprimeCabezera());
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter)) { 
                FuncionamientoClickOEnter();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this.txtDNI.Text = "";
            }

        }


        private void btnDNI_Click(object sender, EventArgs e)
        {
            FuncionamientoClickOEnter();
        }

        public void FuncionamientoClickOEnter()
        {

            bool val = Validaciones.Licencia();
            if (val == true)
            {
                this.btnDNI.Enabled = false;
                Mantenimiento vm = new Mantenimiento();
                vm.ShowDialog();

                this.txtDNI.Enabled = false;

            }

            this.lblDNI.Text = "";
            bool retorno = Validaciones.ValidarQueSeaMayoraSieteCaracteres(this.txtDNI.Text, out string ingreso);
            if (retorno == false)
            {
                this.lblDNI.Location = new System.Drawing.Point(258, 188);
                this.lblDNI.Text = "Ingrese 8 caracteres";
            }
            else
            {
                if (TieneDeudaDNI(ingreso))
                {
                    MiSesionIniciada mi = new MiSesionIniciada();
                    mi.CargarDeuda(deuda);

                    mi.VerMiDeudaDNI(ingreso);
                    mi.Registro(ingreso);
                    mi.ShowDialog();
                    this.txtDNI.Text = "";

                    //otra prueba con eneable
                    this.txtDNI.Focus();
                    //this.txtDNI.Click() += new System.EventHandler(sender, e);
                    //this.btnDNI.Click += new System.EventHandler(this.btnDNI_Click);
                    //sad
                    deuda = mi.ActualizarDeuda();

                }
                else
                {
                    if (TieneDeudaNCliente(ingreso))
                    {
                        MiSesionIniciada mi = new MiSesionIniciada();
                        mi.CargarDeuda(deuda);

                        mi.Registro(ingreso);
                        mi.VerMiDeudaNCliente(ingreso);
                        mi.ShowDialog();
                        this.txtDNI.Text = "";
                        //otra prueba con eneable
                        this.txtDNI.Focus();
                        //this.txtDNI.Click() += new System.EventHandler(sender, e);
                        //this.btnDNI.Click += new System.EventHandler(this.btnDNI_Click);
                        //sad
                        deuda = mi.ActualizarDeuda();

                    }
                    else
                    {
                        this.lblDNI.Location = new System.Drawing.Point(183, 188);
                        this.lblDNI.Text = "No presenta facturas pendientes";
                    }
                }

            }
        }


        public bool TieneDeudaDNI(string dni)
        {
            bool retorno = deuda.BuscarDNI(dni,1);
            return retorno;
        }

        public bool TieneDeudaNCliente(string nCliente)
        {
            bool retorno = deuda.BuscarNCliente(nCliente, 1);
            return retorno;
        }



        private void ShowMyNonModalForm()
        {
            Form myForm = new Form();
            myForm.Text = "My Form";
            myForm.SetBounds(10, 10, 200, 200);

            myForm.Show();
            // Determine if the form is modal.
            if (myForm.Modal == true)
            {
                // Change borderstyle and make it not a top level window.
                myForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                myForm.TopLevel = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MostrarPantallaCompleta();
        }

        private void MostrarPantallaCompleta()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void groupBoxDNI_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.txtDNI.Text = "";
            this.lblDNI.Text = "";
        }

        
    }
}
