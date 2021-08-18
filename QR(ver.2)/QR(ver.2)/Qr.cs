using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;
using System.Drawing;

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
            Bitmap result = new Bitmap(qr.Write(TeXt));
            bmp = result;
            BitmapSource b = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(result.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageSource IM = b;
            return b;
        }
        public ImageSource createQR(string TeXt, Image overlay)
        {
            Bitmap result = new Bitmap(qr.Write(TeXt));
            int deltaHeigth = height - overlay.Height;
            int deltaWidth = width - overlay.Width;
            Graphics g = Graphics.FromImage(bmp);//
            g.DrawImage(overlay, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));
            bmp = result;
            BitmapSource b = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(result.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageSource IM = b;
            return b;
        }

    }
}
