using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Drawing2D;
using Size = System.Drawing.Size;
using ZXing.Rendering;
using ZXing.QrCode.Internal;

namespace QR_ver._2_
{
    class Qr
    {
        //поля
        private int width, height;
        BarcodeWriter qr;
        public Bitmap bmp;

        //конструктор
        public Qr(int w = 300, int h = 300)
        {
            width = w;
            height = h;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions { DisableECI = true, CharacterSet = "UTF-8", Width = width, Height = height };
            qr = new BarcodeWriter();
            qr.Options = options;
            qr.Format = ZXing.BarcodeFormat.QR_CODE;
        }
        //методы
        public ImageSource createQR(string TeXt)
        {
            Bitmap result = null;
            BitmapSource b = null;
            try
            {
                result = new Bitmap(qr.Write(TeXt)); 
                bmp = result;
                b = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(result.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); 
            }
            catch
            {
                MessageBox.Show("Enter text", "Error!");
            }
            finally
            {
                
            }
            return b;
        }


        public Bitmap createQR(int width, int height, string text)
        {

            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            Bitmap bm = bw.Write(text);
            Bitmap overlay = new Bitmap(imagePath);

            int deltaHeigth = bm.Height - overlay.Height;
            int deltaWidth = bm.Width - overlay.Width;

            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(overlay, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));

            return bm;
        }
        public ImageSource createQR(string TeXt, Image overlay)
        {
            System.Drawing.Image i = resizeImage(overlay, new Size(100, 100));
            Bitmap result = new Bitmap(qr.Write(TeXt));
            bmp = result;
            int deltaHeigth = height - i.Height;
            int deltaWidth = width - i.Width;
            Graphics g = Graphics.FromImage(bmp);//
            g.DrawImage(i, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));
            BitmapSource b = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(result.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return b;
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

    }
}
