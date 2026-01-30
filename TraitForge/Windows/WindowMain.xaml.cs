using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace TraitForge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMain : Window
    {

        public WindowMain()
        {
            InitializeComponent();

            App.MainFrame = _NavigationFrame;

            App.MainFrame.Navigate(new PageMain());

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(App.MainFrame.CanGoBack)
            {
                App.MainFrame.GoBack();
            }
        }
    }
}