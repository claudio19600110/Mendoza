using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.IO;
using System.Drawing;
using Ean13Barcode;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PrinterTG2480H
{

    /// <summary>
    /// WindowsPrintSpooler
    /// Para funcionar utiliza la clase PrintDocument
    /// </summary>
    public class PrintSpooler
    {
        private static PrintSpooler instance;
        private PrintDocument pd;
        private StreamReader streamToPrint;
        private Font printFont;
        private Ean13 ean13 = null;

        public static PrintSpooler getInstance()
        {
            if (instance == null)
                instance = new PrintSpooler();
            return instance;
        }

        private PrintSpooler()
        {
            // Inicializar PrintDocument
            pd = new PrintDocument();

            // Set event handler
            pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);

            // Set default font
            printFont = new Font("Monaco", 13, FontStyle.Bold);
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                float yPos = 0;
                int count = 0;
                float leftMargin = 0;
                float topMargin = 100;
                string linea = null;
                string codeOperacion = String.Empty;

                // Iterar sobre el archivo imprimiendo cada línea
                for (int i = 0; ((linea = streamToPrint.ReadLine()) != null); i++)
                {
                    /*if (i == 12)
                    {
                        codeOperacion = linea;
                    }*/
                    // fast harcoded solution #2kMMK7IM #6
                    if (i == 7)
                    {
                        yPos = topMargin + (count * printFont.GetHeight(e.Graphics)) - 5;
                        printFont = new Font("Monaco", 18, FontStyle.Bold);
                    }
                    else
                    {
                        if (i == 8)
                        {
                            yPos = topMargin + (count * printFont.GetHeight(e.Graphics)) - 15;
                            printFont = new Font("Monaco", 22, FontStyle.Bold);
                        }
                        else
                        {
                            yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                            printFont = new Font("Monaco", 13, FontStyle.Bold);
                        }
                    }


                    e.Graphics.DrawString(linea, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    count++;
                }



                //int width = 300, height = 150;

                ////bitmap
                //Bitmap img = new Bitmap(width, height);

                ////random number
                //Random rand = new Random();

                ////create random pixels
                //for (int y = 0; y < height; y++)
                //{
                //    for (int x = 0; x < width; x++)
                //    {
                //        //generate random ARGB value
                //        int a = rand.Next(256);
                //        int r = rand.Next(256);
                //        int g0 = rand.Next(256);
                //        int b = rand.Next(256);

                //        int random = rand.Next(5);
                //        //set ARGB value
                //        img.SetPixel(x, y, (random>2)?Color.White:Color.Black);
                //    }
                //}

                //Graphics g1 = Graphics.FromImage(img);
                //g1.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.SystemColors.Control),
                //    new Rectangle(0, 0, width, height));
                //e.Graphics.DrawImage(img, new Rectangle(0, 400, img.Width, img.Height), 0, 0, width, height, GraphicsUnit.Pixel);
                //g1.Dispose();

                //int angle = 90;
                Graphics gr = e.Graphics;
                string path = Directory.GetCurrentDirectory();
                path += "\\recursos\\ctc_ROJO.png";
                Image img = new Bitmap(path);
                //Image img = new Bitmap(@"C:\Icitelco.jpg");
                img = ResizeImage(img, 430, 430);
                //    gr.TranslateTransform((float)img.Width / 4, (float)img.Height / 4);
                gr.DrawImage(img, new Point(70, 20));

                e.Graphics.DrawImage(img, new Rectangle(45, 0, img.Width, img.Height), 0, 430, img.Width, img.Height, GraphicsUnit.Pixel);



                /*int sizeX = 600;
                int sizeY = 400;
                int offsetY = 100;
                int desplazamientoY = 600 + offsetY;
                //Create a graphics with the size of the printer page's width
                Bitmap imageToPr = new Bitmap(sizeX, sizeY);
                Graphics g = Graphics.FromImage(imageToPr);
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.SystemColors.Control),
                    new Rectangle(0, desplazamientoY, sizeX, sizeY));
                string output = new string(codeOperacion.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                CreateEan13(output);
                ean13.Scale = (float)1.3;
                ean13.DrawEan13Barcode(g, new System.Drawing.Point(0, 0));
                //Draw barcode image into the printer graphics
                e.Graphics.DrawImage(imageToPr, new Rectangle(27, 430 + offsetY, imageToPr.Width, imageToPr.Height), 0, 0, sizeX, sizeY, GraphicsUnit.Pixel);
                g.Dispose();*/

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void CreateEan13(string codigoOperacion)
        {
            ean13 = new Ean13();
            ean13.CountryCode = codigoOperacion.PadLeft(6, '0');
            ean13.ManufacturerCode = codigoOperacion.PadLeft(6, '0');
            ean13.ProductCode = "";

        }



        public void Print(StreamReader sr)
        {
            try
            {
                this.streamToPrint = sr;
                pd.Print();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
