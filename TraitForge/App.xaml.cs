using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace TraitForge
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static Class1 c1 { get; set; }
        public static WindowMain MyMainWindow {  get; set; }
        public static Frame MainFrame { get; set; }

        public App()
        {
            c1 = new Class1();
            //Class1.CreateDb("db");
            c1.ConnectToDb("db");
            //c1.InitDb();

            MyMainWindow = new WindowMain();

            MyMainWindow.Show();
        }
    }

}
