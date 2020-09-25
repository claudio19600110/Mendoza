using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Footer
    {
        //VARIABLES DEL FOOTER
        private string tipoDeFooter; //1 Caracater
        private string valorFijoFooter; //3 Caracater
        private string comercio; //4 Caracater
        private string fechaFotter; //8 Caracater formato AAAAMMDD
        private string cantRegistros; //7 Caracater
        private string valorFijoSegundoDelFooter; //7 Caracater
        private string importe; //11 Caracater
        //Cantidad de caracteres 41

        public Footer(string tipoDeFooter, string valorFijoFooter, string comercio,
            string fechaFotter, string cantRegistros, string valorFijoSegundoDelFooter,
            string importe)
        {
            this.tipoDeFooter = tipoDeFooter;
            this.valorFijoFooter = valorFijoFooter;
            this.comercio = comercio;
            this.fechaFotter = fechaFotter;
            this.cantRegistros = cantRegistros;
            this.valorFijoSegundoDelFooter = valorFijoSegundoDelFooter;
            this.importe = importe;
        }
        //Cantidad de caracteres 41


    }
}

