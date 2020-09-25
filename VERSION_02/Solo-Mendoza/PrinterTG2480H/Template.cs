using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTG2480H
{
    public abstract class Template : ITemplate
    {
        private MemoryStream stream;
        protected StreamWriter writer;

        public Template()
        {
            this.stream = new MemoryStream();
            this.writer = new StreamWriter(stream);
        }

        protected abstract void CrearTemplate();


        public virtual StreamReader ObtenerTemplate()
        {
            try
            {
                writer.Flush();
                stream.Position = 0;
                return new StreamReader(stream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void AgregarLinea(string linea)
        {
            try
            {
                writer.WriteLine(linea);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}