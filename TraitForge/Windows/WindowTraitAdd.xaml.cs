using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace TraitForge.Windows
{
    /// <summary>
    /// Logica di interazione per WindowTraitAdd.xaml
    /// </summary>
    public partial class WindowTraitAdd : Window
    {
        public WindowTraitAdd()
        {
            InitializeComponent();
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files|*.bpm;*.jpeg;*.jpg;*.png;*.gif|BPM Files (*.bpm)|*.bpm|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            dlg.Multiselect = true;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                /*FileStream FS = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                textBox1.Text = img.ToString();*/

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
                ImageViewer.Source = bitmap;
            }
        }

        private void BtnTraitAddCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnTraitCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
