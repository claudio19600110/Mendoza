using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Detalle
    {
        //VARIABLES MIAS
        private static int constId = 1;
        private int id;
        private int estado = 0;  //0 no se pago - 1 se pago - 2 pago parcial
        private int numDeVencimiento = 0; //0 sin asignar - 1 primer ven - 2 segundo ven - 3 tercer ven
        //VARIABLES DEL DETALLE
        private string tipoDeDetalle; //1 Caracater

        //private string numCliente; //19 Caracater
        private string nCliente; // 11 carcat
        private string dni;//8 caract

        private string documento; //20 Caracater
        private string moneda; //1 Caracater
        private string primerVencimiento; //8 Caracater
        private string importePrimerVencimiento; //11 Caracater
        private string segundoVencimiento; //8 Caracater
        private string importeSegundoVencimiento; //11 Caracater
        private string tercerVencimiento; //8 Caracater
        private string importeTercerVencimiento; //11 Caracater
        private string valorFijoSegundo; //19 Caracater
        private string numReferencia; //19 Caracater
        private string mensajeTicket; //55 Caracater
        private string codBarras; //38 Caracater
        //Cantidad de caracteres 229
        private string nombre;//51 ? 



        /*
        public Detalle(string tipoDeDetalle, string numCliente, string documento,
            string moneda, string primerVencimiento, string importePrimerVencimiento,
            string segundoVencimiento, string importeSegundoVencimiento,
            string tercerVencimiento, string importeTercerVencimiento,
            string valorFijoSegundo, string numReferencia, string mensajeTicket,
            string codBarras)
        {
            this.id = constId++;
            this.tipoDeDetalle = tipoDeDetalle;
            this.numCliente = numCliente;
            this.documento = documento;
            this.moneda = moneda;
            this.primerVencimiento = primerVencimiento;
            this.importePrimerVencimiento = importePrimerVencimiento;
            this.segundoVencimiento = segundoVencimiento;
            this.importeSegundoVencimiento = importeSegundoVencimiento;
            this.tercerVencimiento = tercerVencimiento;
            this.importeTercerVencimiento = importeTercerVencimiento;
            this.valorFijoSegundo = valorFijoSegundo;
            this.numReferencia = numReferencia;
            this.mensajeTicket = mensajeTicket;
            this.codBarras = codBarras;
        }*/

        public Detalle(string tipoDeDetalle, string nCliente,string dni, 
            string documento,string moneda, string primerVencimiento,
            string importePrimerVencimiento,  string segundoVencimiento,
            string importeSegundoVencimiento, string tercerVencimiento,
            string importeTercerVencimiento,  string valorFijoSegundo, 
            string numReferencia, string mensajeTicket,string codBarras, 
            string nombre)
        {
            this.id = constId++;
            this.tipoDeDetalle = tipoDeDetalle;
            // this.numCliente = numCliente;
            this.nCliente = nCliente;
            this.dni = dni;
            this.documento = documento;
            this.moneda = moneda;
            this.primerVencimiento = primerVencimiento;
            this.importePrimerVencimiento = importePrimerVencimiento;
            this.segundoVencimiento = segundoVencimiento;
            this.importeSegundoVencimiento = importeSegundoVencimiento;
            this.tercerVencimiento = tercerVencimiento;
            this.importeTercerVencimiento = importeTercerVencimiento;
            this.valorFijoSegundo = valorFijoSegundo;
            this.numReferencia = numReferencia;
            this.mensajeTicket = mensajeTicket;
            this.codBarras = codBarras;
            this.Nombre = nombre;
        }
        //Cantidad de caracteres 229
        //AHORA 3 CHARS MAS X LOS . DE LOS IMPORTES


        //Controlar
        public bool SonIgualesDNI(string dni)
        {
            bool retorno = false;
            string aux = this.Dni.Trim();
            if (dni == aux)
            {
                retorno = true;
            }
            return retorno;
        }

        public bool SonIgualesNCliente(string nCliente)
        {
            bool retorno = false;
            string aux = this.NCliente.Trim();
            if (nCliente == aux)
            {
                retorno = true;
            }
            return retorno;
        }


        #region Setter y Getters

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public int NumDeVencimiento
        {
            get { return numDeVencimiento; }
            set { numDeVencimiento = value; }
        }

        /*
        public string NumCliente
        {
            get { return numCliente; }
            set { numCliente = value; }
        }*/

        public string NCliente
        {
            get { return nCliente; }
            set { nCliente = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        public string PrimerVencimiento
        {
            get { return primerVencimiento; }
            set { primerVencimiento = value; }
        }

        public string ImportePrimerVencimiento
        {
            get { return importePrimerVencimiento; }
            set { importePrimerVencimiento = value; }
        }

        public string SegundoVencimiento
        {
            get { return segundoVencimiento; }
            set { segundoVencimiento = value; }
        }

        public string ImporteSegundoVencimiento
        {
            get { return importeSegundoVencimiento; }
            set { importeSegundoVencimiento = value; }
        }
        public string TercerVencimiento
        {
            get { return tercerVencimiento; }
            set { tercerVencimiento = value; }
        }

        public string ImporteTercerVencimiento
        {
            get { return importeTercerVencimiento; }
            set { importeTercerVencimiento = value; }
        }

        public string MensajeTicket
        {
            get { return mensajeTicket; }
            set { mensajeTicket = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        #endregion

    }

}