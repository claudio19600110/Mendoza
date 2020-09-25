using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Header
    {
        //VARIABLES DEL HEADER
        private string tipoDeHeader; //1 Caracater
        private string valorFijo; //3 Caracater
        private string numComercio; //4 Caracater
        private string fecha; //8 Caracater FORMATO AAAAMMDD
        //Cantidad de caracteres 16


        public Header(string tipoDeHeader, string valorFijo, string numComercio, string fecha)
        {
            this.tipoDeHeader = tipoDeHeader;
            this.valorFijo = valorFijo;
            this.numComercio = numComercio;
            this.fecha = fecha;
        }

        public string ImprimeCabezera()
        {
            string cadena = TipoDeHeader + ValorFijo + NumComercio + Fecha;

            while(cadena.Length != 100)
            {
                cadena += "0";
            }
            return cadena;
        }


        public string TipoDeHeader
        {
            get { return tipoDeHeader; }
            set { tipoDeHeader = value; }
        }
        public string ValorFijo
        {
            get { return valorFijo; }
            set { valorFijo = value; }
        }
        public string NumComercio
        {
            get { return numComercio; }
            set { numComercio = value; }
        }
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

    }
}
