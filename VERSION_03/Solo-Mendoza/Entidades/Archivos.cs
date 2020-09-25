using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public class Archivos
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archivo">ruta del archivo</param>
        /// <param name="datos">datos a ser guardado</param>
        /// <returns>true si funciono false si no</returns>
        public bool Guardar(string archivo, string datos)
        {
            bool retorno = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(archivo, false, UTF8Encoding.ASCII))
                {
                    sw.WriteLine(datos);
                    
                    retorno = true;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error Guardar");
            }

            return retorno;
        }

        public bool Guardar(string archivo, Object datos)
        {
            bool retorno = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(archivo, true, UTF8Encoding.ASCII))
                {
                    sw.WriteLine(datos);

                    retorno = true;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error Guardar");
            }

            return retorno;
        }

        public static string LeeUltimoRegistro(string archivo)
        {
            string lastLine = File.ReadLines(archivo).Last();
            return lastLine;
        }

        public static void EliminaUltimaLinea(string archivo)
        {
            List<string> lines = File.ReadAllLines(archivo).ToList();

            File.WriteAllLines(archivo, lines.GetRange(0, lines.Count - 1).ToArray());
        }

        public static DetalleSalida ArmaDetalleSalida(string line)
        {
            string tipoDeDetalle = line.Substring(0, 1); //1 Caracater
            string numCliente = line.Substring(1, 19); //19 Caracater
            string documento = line.Substring(20, 20); //20 Caracater
            string fechaVencimiento = line.Substring(40, 8); //8 Caracater
            string moneda = line.Substring(48, 1); //1 Caracater
            string fechaCobro = line.Substring(49, 8); //8 Caracater
            string importe = line.Substring(57, 11); //11 Caracater
            string codMovimiento = line.Substring(68, 1); //1 Caracater
            string fechaAcreditacion = line.Substring(69, 8); //8 Caracater
            string canalPago = line.Substring(77, 2); //2 Caracater
            string numControl = line.Substring(79, 4); //4 Caracater
            string codProvincia = line.Substring(83, 3); //3 Caracater
            string filler = line.Substring(86, 14); //14 Caracater
            string estadoPago = line.Substring(100, 1); //14 Caracater

            DetalleSalida detalleSalida = new DetalleSalida(numCliente, documento
                , fechaVencimiento, fechaCobro, importe, fechaAcreditacion,estadoPago);
            return detalleSalida;
        }

        public bool Leer(string archivo, out string texto)
        {
            texto = "";
            bool retorno = false;
            try
            {
                using (StreamReader sr = new StreamReader(archivo))
                {
                    retorno = true;
                    texto = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                
                throw new Exception("Error Leer");
            }

            return retorno;
        }


        public bool Leer(string archivo, out Deuda deuda)
        {
            bool retorno = false;

            Header header = null;
            List<Detalle> miListaDeDetalle = new List<Detalle>();
            Footer footer = null;

            try
            {
                using (StreamReader sr = new StreamReader(archivo))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string auxiliarLine = line;
                        string tipoLinea = line.Substring(0, 1);

                        if (tipoLinea == "0")
                        {
                            string tipoDeHeader = line.Substring(0, 1);
                            string valorFijo = line.Substring(1, 3);
                            string numComercio = line.Substring(4, 4);
                            string fecha = line.Substring(8, 8);
                            header = new Header(tipoDeHeader, valorFijo, numComercio, fecha);
                        }
                        if (tipoLinea == "5")
                        {
                            string tipoDeDetalle = line.Substring(0, 1); //1 Caracater

                            //string numCliente = line.Substring(1, 19); //19 Caracater
                            string nCliente = line.Substring(1, 11); //19 Caracater
                            string dni = line.Substring(12, 8); //19 Caracater

                            string documento = line.Substring(20, 20); //20 Caracater
                            string moneda = line.Substring(40, 1); //1 Caracater
                            string primerVencimiento = line.Substring(41, 8); //8 Caracater

                            //string importePrimerVencimiento = line.Substring(49, 11); //11 Caracater
                            string importePrimerVencimientoEntero = line.Substring(49, 9); //9 Caracater
                            string importePrimerVencimientoCentavo = line.Substring(58, 2); //2 Caracater
                            string importePrimerVencimiento = importePrimerVencimientoEntero+","+importePrimerVencimientoCentavo;

                            string segundoVencimiento = line.Substring(60, 8); //8 Caracater

                            //string importeSegundoVencimiento = line.Substring(68, 11); //11 Caracater
                            string importeSegundoVencimientoEntero = line.Substring(68, 9); //11 Caracater
                            string importeSegundoVencimientoCentavo = line.Substring(77, 2); //11 Caracater
                            string importeSegundoVencimiento = importeSegundoVencimientoEntero+","+ importeSegundoVencimientoCentavo; //11 Caracater

                            string tercerVencimiento = line.Substring(79, 8); //8 Caracater

                            //string importeTercerVencimiento = line.Substring(87, 11); //11 Caracater
                            string importeTercerVencimientoEntero = line.Substring(87, 9); //11 Caracater
                            string importeTercerVencimientoCentavo = line.Substring(96, 2); //11 Caracater
                            string importeTercerVencimiento = importeTercerVencimientoEntero+","+ importeTercerVencimientoCentavo; //11 Caracater

                            string valorFijoSegundo = line.Substring(98, 19); //19 Caracater
                            string numReferencia = line.Substring(117, 19); //19 Caracater
                            string mensajeTicket = line.Substring(136, 55); //55 Caracater
                            string codBarras = line.Substring(191, 38); //38 Caracater
                            string nombre = line.Substring(229, 51); //51 Caracater

                            Detalle detalle = new Detalle(tipoDeDetalle, nCliente,dni, documento,
                                moneda, primerVencimiento, importePrimerVencimiento,
                                segundoVencimiento, importeSegundoVencimiento, tercerVencimiento,
                                importeTercerVencimiento, valorFijoSegundo, numReferencia,
                                mensajeTicket, codBarras, nombre);

                            miListaDeDetalle.Add(detalle);
                        }
                        if (tipoLinea == "9")
                        {
                            string tipoDeFooter = line.Substring(0, 1); //1 Caracater
                            string valorFijoFooter = line.Substring(1, 3); //3 Caracater
                            string comercio = line.Substring(4, 4); //4 Caracater
                            string fechaFotter = line.Substring(8, 8); //8 Caracater formato AAAAMMDD
                            string cantRegistros = line.Substring(16, 7); //7 Caracater
                            string valorFijoSegundoDelFooter = line.Substring(23, 7); //7 Caracater
                            string importe = line.Substring(30, 11); //11 Caracater

                            footer = new Footer(tipoDeFooter, valorFijoFooter, comercio,
                                fechaFotter, cantRegistros, valorFijoSegundoDelFooter, importe);
                        }
                    }

                    Deuda deudaArchivo = new Deuda(header, miListaDeDetalle, footer);
                    retorno = true;
                    deuda = deudaArchivo;
                }
            }
            catch (Exception)
            {
                throw new Exception("Error Leer");
            }
            return retorno;
        }

        public static void Guardado(object queGuardo)
        {
            Archivos archivo = new Archivos();

            string path = Directory.GetCurrentDirectory();
            path += "\\archivoSalida.txt";

            archivo.Guardar(path, queGuardo);
        }

    }


}
