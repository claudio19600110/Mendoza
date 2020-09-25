using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace PrinterTG2480H
{

    public class VueltoTemplate : Template
    {
        private static string titulo = "Vuelto";
        private int monto { get; set; }
        private string codigoUnicoOperacion { get; set; }

        private int saldo;
        private int plataDepositada;
        private List<Detalle> detalles;

        public VueltoTemplate(int monto, string codigoUnicoOperacion)
        {
            this.monto = monto;
            this.codigoUnicoOperacion = codigoUnicoOperacion;
            this.CrearTemplate();
        }

        public VueltoTemplate(int saldo, int plataDepositada, List<Detalle> detalles)
        {
            this.saldo = saldo;
            this.plataDepositada = plataDepositada;
            this.detalles = detalles;
            //this.codigoUnicoOperacion = codigoUnicoOperacion;
            this.CrearTemplate();
        }

        public override StreamReader ObtenerTemplate()
        {
            // validaciones del template que se va a retornar

            return base.ObtenerTemplate();
        }

        protected override void CrearTemplate()
        {
            try
            {
                string fechaHoy = DateTime.Now.ToString("g");
                string fechaVencimiento = DateTime.Now.AddDays(1).ToString("g");

                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea(""); //hasta aca no imprimir nada no se ve x la imagen
                AgregarLinea("FECHA " + fechaHoy);
                AgregarLinea("");
                AgregarLinea("  USTED INGRESO");//7
                AgregarLinea(("$" + this.plataDepositada).PadLeft(12, ' '));//8 
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                if(detalles != null) { 
                    foreach(Detalle item in detalles)
                    {
                        string mensaje = item.MensajeTicket;
                        int i = mensaje.Length;
                        string primer = mensaje.Substring(0, 22);
                        string seg = mensaje.Substring(23, 15);
                        string ter = mensaje.Substring(40, 15);
                        AgregarLinea(""+ primer);
                        AgregarLinea("" + seg);
                        AgregarLinea("" + ter);
                        AgregarLinea("");
                    }
                }
                if (saldo != 0)
                {
                    if(saldo > 0) { 
                        AgregarLinea("SU SALDO ES DE + $" + this.saldo);
                    }
                    if (saldo < 0)
                    {
                        AgregarLinea("SU SALDO ES DE $" + this.saldo);
                    }
                }
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("POR FAVOR, CONSERVE");
                AgregarLinea("            EL TICKET");

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
