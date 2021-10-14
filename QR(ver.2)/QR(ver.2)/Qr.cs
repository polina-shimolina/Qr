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
        BarcodeWriter qrWriter;
        public Bitmap bmp;

        //конструктор
        public Qr(int _width = 300, int _height = 300)
        {
            width = _width;
            height = _height;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions { DisableECI = true, CharacterSet = "UTF-8", Width = width, Height = height };
            qrWriter = new BarcodeWriter();
            qrWriter.Options = options;
            qrWriter.Format = BarcodeFormat.QR_CODE;
        }
        //методы
        public ImageSource createQR(string text)
        {
            Bitmap baseQR = null;
            BitmapSource result = null;
            try
            {
                baseQR = new Bitmap(qrWriter.Write(text)); 
                bmp = baseQR;
                result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(baseQR.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                MessageBox.Show("Enter text", "Error!");
            }
            return result;
        }


        //public Bitmap createQR(int width, int height, string text)
        //{

        //    var bw = new ZXing.BarcodeWriter();

        //    var encOptions = new ZXing.Common.EncodingOptions
        //    {
        //        Width = width,
        //        Height = height,
        //        Margin = 0,
        //        PureBarcode = false
        //    };

        //    encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

        //    bw.Renderer = new BitmapRenderer();
        //    bw.Options = encOptions;
        //    bw.Format = ZXing.BarcodeFormat.QR_CODE;
        //    Bitmap bm = bw.Write(text);
        //    Bitmap overlay = new Bitmap(imagePath);

        //    int deltaHeigth = bm.Height - overlay.Height;
        //    int deltaWidth = bm.Width - overlay.Width;

        //    Graphics g = Graphics.FromImage(bm);
        //    g.DrawImage(overlay, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));

        //    return bm;
        //}

        public ImageSource createQR(string text, Image overlay)
        {
            Image imageOverlay = resizeImage(overlay, new Size(100, 100));
            Bitmap newBitmap = null;
            try
            {
                newBitmap = new Bitmap(qrWriter.Write(text));
            }
            catch
            {
                MessageBox.Show("Enter text", "Error!");
            }
          
            bmp = newBitmap;
            int deltaHeigth = height - imageOverlay.Height;
            int deltaWidth = width - imageOverlay.Width;
            Graphics graphics = Graphics.FromImage(bmp);//
            graphics.DrawImage(imageOverlay, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));
            BitmapSource result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newBitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return result;
        }

        private static Image resizeImage(Image imgToResize, Size size)
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
            Bitmap result = new Bitmap(destWidth, destHeight);
            Graphics graphics = Graphics.FromImage((Image)result);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            graphics.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            graphics.Dispose();
            return (Image)result;
        }
    }
}
