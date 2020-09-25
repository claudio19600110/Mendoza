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
using Entidades;
using PrinterTG2480H;
using System.Net.Sockets;
using Mensajes;


namespace SesionIniciada
{
    public partial class MiSesionIniciada : Form
    {
        public DataTable tabla;
        public Deuda deuda = null;
        public List<Detalle> deudaSujeto = null;


        public void CargarDeuda(Deuda deuda)
        {
            this.deuda = deuda; 
        }

        public MiSesionIniciada()
        {
            InitializeComponent();
            MostrarPantallaCompleta();

            
            string path = Directory.GetCurrentDirectory();
            path += "\\recursos\\CTC.png";
            pictureBox1.Image = Image.FromFile(path);
            this.btnPagar.Select();
        }

        private void MostrarPantallaCompleta()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }


        public void VerMiDeudaDNI(string dni)
        { 
            if (deuda.BuscarDNI(dni, out deudaSujeto))
            {

                foreach(Detalle item in deudaSujeto)
                {
                    if (item.Estado != 1)
                    {
                        IniciarTabla();
                        CargarDeuda(deudaSujeto);
                        this.lblTotalAPagar.Text = TotalDeuda(deudaSujeto).ToString();
                    }
                }
            }
        }

        public void VerMiDeudaNCliente(string nCliente)
        {
            if (deuda.BuscarNCliente(nCliente, out deudaSujeto))
            {

                foreach (Detalle item in deudaSujeto)
                {
                    if (item.Estado != 1)
                    {
                        IniciarTabla();
                        CargarDeuda(deudaSujeto);
                        this.lblTotalAPagar.Text = TotalDeuda(deudaSujeto).ToString();
                    }
                }
            }
        }



        private float TotalDeuda(List<Detalle> deudaSujeto)
        {
            float montoTotal = 0;
            int diaHoy = Validaciones.ObtenerDiaFormato();
            bool flag = false;

            foreach (Detalle item in deudaSujeto)
            {
                if(item.Estado != 1) { 
                    flag = false;
                    int primerVen = int.Parse(item.PrimerVencimiento);
                    int segundoVev = int.Parse(item.SegundoVencimiento);
                    int tercerVen = int.Parse(item.TercerVencimiento);

                    if (diaHoy <= primerVen)
                    {
                        montoTotal += float.Parse(item.ImportePrimerVencimiento);
                        flag = true;
                        item.NumDeVencimiento = 1;
                    }
                    if (diaHoy <= segundoVev && flag == false)
                    {
                        montoTotal += float.Parse(item.ImporteSegundoVencimiento);
                        flag = true;
                        item.NumDeVencimiento = 2;
                    }
                    if (flag == false)
                    {
                        montoTotal += float.Parse(item.ImporteTercerVencimiento);
                        flag = true;
                        item.NumDeVencimiento = 3;
                    }
                }
            }
            return montoTotal;
        }

        private void IniciarTabla()
        {
            tabla = new DataTable();
            tabla.Columns.Add("NumCliente");
            tabla.Columns.Add("1er vcto");
            tabla.Columns.Add("Monto 1er vcto");
            tabla.Columns.Add("2do vcto");
            tabla.Columns.Add("Monto 2do vcto");
            tabla.Columns.Add("3er vcto");
            tabla.Columns.Add("Monto 3er vcto");
            tabla.Columns.Add("Documento");
            this.grilla.DataSource = tabla;
        }



        private void CargarDeuda(List<Detalle> deudaSujeto)
        {
            Validaciones v = new Validaciones();
            this.lblNombre.Text = deudaSujeto[0].Nombre;
           // this.lblNombre.Text = "111111111122222222223333333333444444444455555555556";
            string a = this.lblNombre.Text.Substring(0, 36);
            string b = this.lblNombre.Text.Substring(36, 15);
            this.lblNombre.Text = a + Environment.NewLine + b;
            
            foreach (Detalle item in deudaSujeto)
            {
                if(item.Estado != 1) { //No sirvio
                    DataRow fila = tabla.NewRow();
                    fila["NumCliente"] = item.NCliente;
                    fila["1er vcto"] = Validaciones.EstiloFecha(item.PrimerVencimiento);
                    fila["Monto 1er vcto"] = Validaciones.QuitaCeros(item.ImportePrimerVencimiento).ToString();
                    fila["2do vcto"] = Validaciones.EstiloFecha(item.SegundoVencimiento);
                    fila["Monto 2do vcto"] = Validaciones.QuitaCeros(item.ImporteSegundoVencimiento).ToString();
                    fila["3er vcto"] = Validaciones.EstiloFecha(item.TercerVencimiento);
                    fila["Monto 3er vcto"] = Validaciones.QuitaCeros(item.ImporteTercerVencimiento).ToString();
                    fila["Documento"] = item.Documento;
                    tabla.Rows.Add(fila);
                }
            }
        }

        public void Registro(string dni)
        {
            this.lbl_DNI.Text = dni;
            //ACA EL NOMBRE
            //this.lblNombre.Text = deudaSujeto[0].Nombre;
        }

        public string BuscarNumDeSocio(string dni)
        {
            return "Numero de Socio";
        }
        public string BuscarDNI(string numSocio)
        {
            return "Numero de DNI";
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        public int Redondea(string numeroString)
        {
            bool flag = double.TryParse(numeroString, out double montoo);
            double a = Math.Round(montoo);
            int retorno = (int)a;
            return retorno;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                string montoPesos = this.lblTotalAPagar.Text;
                int monto = Redondea(montoPesos);
                bool flag = true;
                if (flag )
                {
                    string msg = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?><payment><command>cash</command><amount>" + monto/* 500*/ + "</amount><appId>12345678</appId><timeout>300</timeout></payment>";
                    string dineroIngresado = Connect("127.0.0.1", msg);

                    //chamuyo
                    //Console.WriteLine("Chamu");
                    //dineroIngresado = 5000.ToString();

                    bool bandera = int.TryParse(dineroIngresado, out int plataDepositada);
                    float montoTotalDeuda = TotalDeuda(deudaSujeto);
                    
                    float saldo = (float)plataDepositada - montoTotalDeuda ;
                    
                    
                    if (bandera && plataDepositada != -1){
                        //MockConnect("127.0.0.1", msg, out int plataDepositada);
                        bool flag2 = false;
                        Console.WriteLine("a");
                        deudaSujeto = OrdenoDeuda(deudaSujeto);
                        
                        CuandoEscribo(plataDepositada, deudaSujeto, out List<Detalle> detallesImpresion);
                        
                        Console.WriteLine("sa");
                        if(detallesImpresion.Count != 0)
                        {
                            ImprimeTicket(detallesImpresion, plataDepositada, saldo);
                            //MessageBox.Show("Sus facturas fueron pagadas con exito!\n\n Muchas gracias.");
                            flag2 = true;
                            FormMensaje mensaje = new FormMensaje("Sus facturas fueron pagadas con exito!\n\n Muchas gracias.", "Facturas pagadas con exito");
                            mensaje.ShowDialog();
                            this.Close();
                        }

                        float saldoConvertido = saldo * -1;
                        if(saldoConvertido != montoTotalDeuda && detallesImpresion.Count == 0 && flag2==false)
                        {
                            ImprimeTicketInsuficiente(plataDepositada);
                            FormMensaje mensaje = new FormMensaje("Su saldo disminuyo lo vera reflejado en \nsu siguiente factura!\n\n Muchas gracias.", "Variacion de Saldo");
                            mensaje.ShowDialog();
                            this.Close();
                        }
                    }
                }else
                {
                    this.lblTotalAPagar.Text="Error";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ImprimeTicketInsuficiente(int plataDepositada)
        {
            try
            {
                //saldo, plataDepositada, mensajes
                //float montoTotalDeuda =TotalDeuda(deudaSujeto);
                float plata = float.Parse(plataDepositada.ToString());
                //int saldo = plataDepositada - montoTotalDeuda;
                //float saldo = plataDepositada - montoTotalDeuda;

                PrintSpooler spooler = PrintSpooler.getInstance();
                PagoInsuficienteTemplete vueltoInsufTemplate = new PagoInsuficienteTemplete(plataDepositada, this.lblNombre.Text);
                spooler.Print(vueltoInsufTemplate.ObtenerTemplate());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public List<Detalle> OrdenoDeuda(List<Detalle> deudaSujeto)
        {
            List<Detalle> laNueva = null;
            try
            {
                foreach (var item in deudaSujeto)
                {
                    string mesString = item.Documento.Substring(16, 2);
                    string anioString = item.Documento.Substring(18, 2);

                    bool flag =int.TryParse(mesString, out int mes);
                    bool flag2 = int.TryParse(anioString, out int anio);
                    anio = 2000 + anio;
                    if(flag==true && flag2 == true)
                    {
                        item.Periodo = new DateTime(anio, mes, 01);
                    }
                }

                laNueva = deudaSujeto.OrderBy(x => x.Periodo).ToList();

            }
            catch(Exception e)
            {
                Console.WriteLine( "exception",e.Message);
            }
            
            if(laNueva != null){
                return laNueva;
            }
            else{
                return deudaSujeto;
            }
        }


        public void ImprimeTicket(List<Detalle> detallesImpresion, int plataDepositada, float saldoo)
        {
            try { 
                //saldo, plataDepositada, mensajes
                //float montoTotalDeuda =TotalDeuda(deudaSujeto);
                float plata =float.Parse(plataDepositada.ToString());
                //int saldo = plataDepositada - montoTotalDeuda;
                //float saldo = plataDepositada - montoTotalDeuda;

                PrintSpooler spooler = PrintSpooler.getInstance();
                VueltoTemplate vueltoTemplate = new VueltoTemplate((int)saldoo, plataDepositada,detallesImpresion);
                spooler.Print(vueltoTemplate.ObtenerTemplate());

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MockConnect(String server, String message, out int cuantaGuita)
        {
            //800 y 7700
            cuantaGuita = 8500;
        }

        
        public string Connect(String server, String message)
        {
            string dineroIngresado = "-1";
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 4040;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[1000];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                if (responseData.Contains("insertedAmount"))
                {
                    int inicio = responseData.IndexOf("<insertedAmount>") + "<insertedAmount>".Length;
                    int fin = responseData.IndexOf("</insertedAmount>");
                    //this.txtPlataIngresada.Text = responseData.Substring(inicio, fin - inicio);
                    Console.WriteLine(responseData.Substring(inicio, fin - inicio));
                }
                if (responseData.Contains("monto"))
                {
                    int inicio = responseData.IndexOf("<monto>") + "<monto>".Length;
                    int fin = responseData.IndexOf("</monto>");
                    dineroIngresado = responseData.Substring(inicio, fin - inicio);
                }
                else
                {
                    //this.txtPlataIngresada.Text = "Error ó Timeout";
                    Console.WriteLine("Error ó Timeout");
                }

                // Close everything.
                stream.Close();
                client.Close();

            }
            catch (ArgumentNullException e){
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e){
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            return dineroIngresado;
        }

        //Creo que este metodo no lo uso nunca, quedo desestimado
        public string SacaComa(string cadena)
        {
            string retorno = "no    funca";
            try { 
                if (cadena.Contains(","))
                {
                    string entero = cadena.Substring(0, 8);
                    string decimales = cadena.Substring(9);

                    retorno = "0" + entero + decimales;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retorno;
        }

        public void CuandoEscribo(int guitaIngresadaa, List<Detalle> deudaSujeto, out List<Detalle> detallesImpresion)
        {
            int dia = Validaciones.ObtenerDiaFormato();

            float guitaIngresada = float.Parse(guitaIngresadaa.ToString());
            detallesImpresion = new List<Detalle>();

            foreach (Detalle item in deudaSujeto)
            {
                if (item.NumDeVencimiento == 3 && guitaIngresada > 0 && item.Estado == 0)
                {
                    bool flag = float.TryParse(item.ImporteTercerVencimiento.ToString(), out float importe3);

                    if (flag == true && importe3 > 0 && guitaIngresada > 0)
                    {
                        if (importe3 < guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe3);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente+item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"1");

                            guitaIngresada -= importe3;
                            item.Estado = 1;
                            bool bandera =deuda.ManejaEstados(deuda.Detalles, item.Id,  1);
                            if(bandera == true)
                            {
                                
                            }
                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else if (importe3 == guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe3);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"0");

                            guitaIngresada -= importe3;
                            item.Estado = 1;
                            bool bandera = deuda.ManejaEstados(deuda.Detalles, item.Id, 1);
                            if (bandera == true)
                            {

                            }
                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else
                        {
                            string total = Validaciones.MontoOnceCaracteres(guitaIngresada);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"2");

                            guitaIngresada -= guitaIngresada;
                            item.Estado = 2;
                            Archivos.Guardado(detaSalida.ToString());
                        }

                    }
                }

            }

            foreach (Detalle item in deudaSujeto)
            {
                if (item.NumDeVencimiento == 2 && guitaIngresada > 0 && item.Estado == 0)
                {
                    bool flag = float.TryParse(item.ImporteSegundoVencimiento.ToString(), out float importe2);

                    if (flag == true && importe2 > 0 && guitaIngresada > 0)
                    {
                        if (importe2 < guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe2);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"1");

                            guitaIngresada -= importe2;
                            
                            item.Estado = 1;
                            bool bandera = deuda.ManejaEstados(deuda.Detalles, item.Id, 1);
                            if (bandera == true)
                            {
                                
                            }

                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else if (importe2 == guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe2);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"0");

                            guitaIngresada -= importe2;

                            item.Estado = 1;
                            bool bandera = deuda.ManejaEstados(deuda.Detalles, item.Id, 1);
                            if (bandera == true)
                            {

                            }

                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else
                        {
                            string total = Validaciones.MontoOnceCaracteres(guitaIngresada);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"2");

                            guitaIngresada -= guitaIngresada;
                            item.Estado = 2;
                            Archivos.Guardado(detaSalida.ToString());
                        }

                    }
                }
            }


            foreach (Detalle item in deudaSujeto)
            {
                if (item.NumDeVencimiento == 1 && guitaIngresada >= 0 && item.Estado == 0)
                {
                    //float valor =VuelveFloat(item.ImportePrimerVencimiento);
                    bool flag = float.TryParse(item.ImportePrimerVencimiento.ToString(), out float importe1);

                    if (flag == true && importe1 > 0 && guitaIngresada > 0)
                    {
                        if (importe1 < guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe1);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"1");

                            guitaIngresada -= importe1;
                            item.Estado = 1;
                            bool bandera = deuda.ManejaEstados(deuda.Detalles, item.Id,1);
                            if (bandera == true)
                            {
                                Console.WriteLine("a");
                            }

                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else if (importe1 == guitaIngresada)
                        {
                            string total = Validaciones.MontoOnceCaracteres(importe1);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"0");

                            guitaIngresada -= importe1;
                            item.Estado = 1;
                            bool bandera = deuda.ManejaEstados(deuda.Detalles, item.Id, 1);
                            if (bandera == true)
                            {
                                Console.WriteLine("a");
                            }

                            Archivos.Guardado(detaSalida.ToString());

                            detallesImpresion.Add(item);
                        }
                        else
                        {
                            string total = Validaciones.MontoOnceCaracteres(guitaIngresada);

                            DetalleSalida detaSalida = new DetalleSalida(item.NCliente + item.Dni
                            , item.Documento, dia.ToString(), dia.ToString(),
                            total, dia.ToString(),"1");

                            guitaIngresada -= guitaIngresada;
                            item.Estado = 2;
                            Archivos.Guardado(detaSalida.ToString());
                        }

                    }
                }
            }

            if (guitaIngresada > 0)
            {
                //int plataRestante = Redondea(guitaIngresada.ToString());
                string path = Directory.GetCurrentDirectory();
                path += "\\archivoSalida.txt";
                string mensaje = Archivos.LeeUltimoRegistro(path);

                DetalleSalida ds = Archivos.ArmaDetalleSalida(mensaje);

                bool flag = int.TryParse(ds.Importe, out int monto);
                if (flag == true)
                {
                    float montoNuevooo = VuelveFloat(monto.ToString(), 2);
                    guitaIngresada += montoNuevooo;
                    string nuevoMonto = Validaciones.MontoOnceCaracteres(guitaIngresada);
                    ds.Importe = nuevoMonto;
                    Archivos.EliminaUltimaLinea(path);
                    Archivos.Guardado(ds.ToString());
                }
            }
        }

        public float VuelveFloat(string valor, int cantDecimales)
        {
            string entero = valor.Substring(0, (valor.Length - cantDecimales));
            string centavos = valor.Substring(valor.Length - cantDecimales);
            string queda = entero + "," + centavos;

            bool flag = float.TryParse(queda, out float numFloat);
            if (flag)
            {
                return numFloat;
            }
            else
            {
                return 0;
            }
        }

        public float VuelveFloat(string valor)
        {
            string entero = valor.Substring(0, 9);
            string centavos = valor.Substring(9, 2);
            string queda = entero + "." + centavos;

            bool flag = float.TryParse(queda, out float numFloat);
            if (flag)
            {
                return numFloat;
            }
            else
            {
                return 0;
            }
        }

        public Deuda ActualizarDeuda()
        {
            return this.deuda;
        }


        private void lbl_DNI_Click(object sender, EventArgs e)
        {

        }

        private void lblFijoDni_Click(object sender, EventArgs e)
        {

        }

        private void MiSesionIniciada_Load(object sender, EventArgs e)
        {

        }

    }
}
