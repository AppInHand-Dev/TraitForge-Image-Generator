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
using TraitForge.Pages;
using TraitForge.Windows;

namespace TraitForge
{
    /// <summary>
    /// Logica di interazione per PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {

        public PageMain()
        {
            InitializeComponent();

            //MainWindow._NavigationFrame.Navigate(new PageNewCollection());
            //App.MyMainWindow.BtnBack.Visibility = Visibility.Hidden;
        }

        private void BtnNewCollection_Click(object sender, RoutedEventArgs e)
        {
            //App.MainFrame.Navigate(new Uri("PageNewCollection.xaml", UriKind.Relative));
            //Application.Current.MainWindow.;

            App.MainFrame.Navigate(new PageNewCollection());

        }

        private void BtnDuplicateCollection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLoadCollection_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new PageLoadCollection());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
