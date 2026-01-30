using System.Windows;
using System.Windows.Controls;


namespace TraitForge
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static DatabaseManager c1 { get; set; }
        public static WindowMain MyMainWindow {  get; set; }
        public static Frame MainFrame { get; set; }

        public App()
        {
            const string DB_NAME = "db";

            c1 = new DatabaseManager(DB_NAME);
            c1.ConnectToDb(DB_NAME);
            c1.InitDb();

            MyMainWindow = new WindowMain();

            MyMainWindow.Show();
        }
    }

}
