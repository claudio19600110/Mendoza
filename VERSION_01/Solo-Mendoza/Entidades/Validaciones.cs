using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public class Validaciones
    {

        public static bool Licencia()
        {
            bool retorno = false;
            Archivos archivo = new Archivos();

            string ruta2 = Directory.GetCurrentDirectory() + "\\recursos\\Propiedad_Intelectual.txt";
            //bool retorno2 = archivo.Leer(ruta2, out string mensaje);
            string mensaje = File.ReadLines(ruta2).Last();
            
            bool salio = DateTime.TryParse(mensaje, out DateTime dia);
            if (salio == true)
            {
                DateTime nuevo = dia.AddDays(90);
                DateTime fecha = DateTime.Today;
                int valor = DateTime.Compare(nuevo, fecha);
                if (valor <= 0)
                {
                    retorno = true;
                }
            }
            
            return retorno;
        }


        public static int ObtenerDiaFormato()
        {
            DateTime fecha = DateTime.Today;
            int dia = fecha.Day;
            int mes = fecha.Month;
            int anio = fecha.Year;

            string diaString = dia.ToString();
            string mesString = mes.ToString();
            string anioString = anio.ToString();

            if (mesString.Length == 1)
            {
                mesString = 0 + mesString;
            }
            if (diaString.Length == 1)
            {
                diaString = 0 + diaString;
            }
            string diaHoy = anioString + mesString + diaString;
            int diaInt = int.Parse(diaHoy);
            return diaInt;
        }
        public static bool ValidarQueSeaDni(string numeroString, out string dni)
        {
            bool retorno = false;
            dni = numeroString;
            if (numeroString.Length == 8){
                retorno = true;
            }
            else{
                retorno = false;
            }
            
            return retorno;
        }

        public static string MuestraNumero(string numero)
        {
            return numero.Trim();
        }

        public static string EstiloFecha(string fecha)
        {
            string anio = fecha.Substring(0, 4);
            string mes = fecha.Substring(4, 2);
            string dia = fecha.Substring(6, 2);

            return dia + "/" + mes + "/" + anio;
        }

        public static float QuitaCeros(string monto)
        {
            return float.Parse(monto);
        }


        /// <summary>
        /// Metodo que valida que un string sea un DNI valido
        /// </summary>
        /// <param name="numeroString">String a ser transfotmado a int y devuelto en el param DNI</param>
        /// <param name="dni">paramtro de tipo out int</param>
        /// /// <param name="tipodeError">paramtro de tipo out int</param>
        // 1 - Errror contiene letras
        // 2- Dni corto
        // 3 - correcto
        /// <returns>true si lo logro transformar a dni, false si no, Si lo logro devuelve un int dni</returns>
        public static bool ValidarQueSeaDni(string numeroString, out int dni, out int tipodeError)
        {
            bool retorno = false;

            bool flag = int.TryParse(numeroString, out int num);
            if (flag == true)
            {
                if (numeroString.Length == 7 || numeroString.Length == 8)
                {
                    retorno = true;
                    dni = num;
                    tipodeError = 3;
                }
                else
                {
                    dni = 0;
                    retorno = false;
                    tipodeError = 2;
                }
            }
            else
            {
                dni = 0;
                retorno = false;
                tipodeError = 1;
            }

            return retorno;
        }

        public static string MontoOnceCaracteres(int monto)
        {
            string montoString = monto.ToString();
            string cero = "";
            for (int i = 0; montoString.Length != 11; i++)
            {
                cero = cero + "0";
                string montotramposo = montoString;
                montotramposo = cero + montotramposo;
                if (montotramposo.Length == 11)
                {
                    return montotramposo;
                }
            }
            return montoString;
        }

        public static string MontoOnceCaracteres(float monto)
        {
            string montoString = monto.ToString();
            string cero = "";
            for (int i = 0; montoString.Length != 9; i++)
            {
                cero = cero + "0";
                string montotramposo = montoString;
                montotramposo = cero + montotramposo;
                if (montotramposo.Length == 9)
                {
                    montotramposo = montotramposo + "00";
                    return montotramposo;
                }
            }
            return montoString;
        }
    }
    


}
