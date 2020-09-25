using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleSalida
    {
        private const string tipoDeDetalle ="5"; //1 Caracater
        private string numCliente; //19 Caracater
        private string documento; //20 Caracater
        private string fechaVencimiento; //8 Caracater AAAAMMDD
        private const string moneda= "0"; //1 Caracater
        private string fechaDeCobro; //8 Caracater AAAAMMDD
        private string importe; //11 Caracater
        private const string codMovimiento = "0"; //1 Caracater
        private string fechaAcreditacion; //8 Caracater AAAAMMDD
        private const string canalPago = "00"; //2 Caracater
        private const string numControl = "0000"; //4 Caracater
        private const string codProvincia = "000"; //3 Caracater
        private const string filler = "00000000000000"; //14 Caracater
        //Un total de 100 caracteres
        private string estadoPago;//1 Caracter
        // FC == PLATA => 0   \\\ FC < PLATA =>1   \\\  FC > PLATA =>2
        public DetalleSalida( string numCliente, string documento, 
            string fechaVencimiento, string fechaDeCobro, string importe, 
            string fechaAcreditacion)
        {
            this.numCliente = numCliente;
            this.documento = documento;
            this.fechaVencimiento = fechaVencimiento;
            this.fechaDeCobro = fechaDeCobro;
            this.importe = importe;
            this.fechaAcreditacion = fechaAcreditacion;
        }

        public DetalleSalida(string numCliente, string documento,
            string fechaVencimiento, string fechaDeCobro, string importe,
            string fechaAcreditacion, string estadoPago)
        {
            this.numCliente = numCliente;
            this.documento = documento;
            this.fechaVencimiento = fechaVencimiento;
            this.fechaDeCobro = fechaDeCobro;
            this.importe = importe;
            this.fechaAcreditacion = fechaAcreditacion;
            this.estadoPago = estadoPago;
        }

        public override string ToString()
        {
            return tipoDeDetalle + numCliente + documento +
                fechaVencimiento + moneda + fechaDeCobro + importe +
                codMovimiento + fechaAcreditacion + canalPago + numControl +
                codProvincia + filler+ estadoPago;
        }

        public string Importe
        {
            get { return importe; }
            set { importe = value; }
        }

    }
}
