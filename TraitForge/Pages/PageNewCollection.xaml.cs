using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraitForge.MVC.Collection;

namespace TraitForge
{
    /// <summary>
    /// Logica di interazione per Page1.xaml
    /// </summary>
    public partial class PageNewCollection : Page
    {

        private CollectionController CollectionController { get; set; }
        public PageNewCollection()
        {
            InitializeComponent();

            CollectionController = new CollectionController();

        }

        private void BtnCollectionCreate_Click(object sender, RoutedEventArgs e)
        {
            Collection collection = new Collection(TxtboxCollectionName.Text, TxtboxCollectionDescription.Text);

            CollectionController.CreateNew(App.c1.DbConnection, collection);

            Directory.CreateDirectory($@"./{collection.Name}");
        }
    }
}
