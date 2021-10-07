using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QR_ver._2_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Qr qr = new Qr();
        ImageSource qrimage;
        System.Drawing.Image file;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SaveQR(System.Drawing.Image bmp1)  //сохранить qr
        {
            SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "QR";
            dialog.Filter = "PNG|*.png|JPEG|*.jpeg|GIF|*.gif|BMP|*.bmp";                                                    
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                string filename = dialog.FileName;
                bmp1.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        public System.Drawing.Image openfile() //открыть файл
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
            string filename = openFileDialog.FileName; //////////
            System.Drawing.Image newImage = null;
            try
            {
                newImage = System.Drawing.Image.FromFile(filename);
            }
            catch
            {
          
            }
            return newImage;
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            file = openfile();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveQR(qr.bmp);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                qrimage = qr.createQR(textBox.Text.Trim());
                image.Source = qrimage;
                textBox.Clear();
            } else
            {
                qrimage = qr.createQR(textBox.Text.Trim(), file);
                image.Source = qrimage;
                textBox.Clear();
            }
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
