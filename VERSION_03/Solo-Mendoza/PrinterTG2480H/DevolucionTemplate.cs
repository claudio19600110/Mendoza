using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTG2480H
{
    public class DevolucionTemplate : Template
    {
        private static string titulo = "Devolución";
        private int monto { get; set; }
        private string codigoUnicoOperacion { get; set; }


        public DevolucionTemplate(int monto, string codigoUnicoOperacion)
        {
            this.monto = monto;
            this.codigoUnicoOperacion = codigoUnicoOperacion;
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
                AgregarLinea("");
                AgregarLinea("SU DEVOLUCIÓN ES");
                AgregarLinea(("$" + this.monto).PadLeft(12, ' '));
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("FECHA " + fechaHoy);
                AgregarLinea("");
                AgregarLinea("OPERACIÓN Nº " + this.codigoUnicoOperacion);
                AgregarLinea("");
                AgregarLinea("VENCE " + fechaVencimiento);
                AgregarLinea("");
                AgregarLinea("RETIRE POR CAJA");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("             COOL PAY");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("");
                AgregarLinea("                                   -");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}