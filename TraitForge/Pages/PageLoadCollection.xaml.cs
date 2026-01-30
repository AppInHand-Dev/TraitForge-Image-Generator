using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraitForge.MVC.Collection;

namespace TraitForge.Pages
{
    /// <summary>
    /// Logica di interazione per PageLoadCollection.xaml
    /// </summary>
    public partial class PageLoadCollection : Page
    {
        public PageLoadCollection()
        {
            InitializeComponent();

            List<Collection> collections = CollectionController.GetList(App.c1.DbConnection);

            int i = 0;
            foreach (Collection collection in collections)
            {
                Button newBtn = new Button
                {
                    Content = collection.Name,
                    Name = "Button" + collection.Id,
                    Width = 200,
                    Height = 50,
                    Margin = new Thickness(0, 120 * i, 0, 0),
                    
                };

                newBtn.Click += new RoutedEventHandler(BtnCollectionLoad_Click);

                GridCollections.Children.Add(newBtn);

                i++;
            }

        }

        private void BtnCollectionLoad_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse((((Button)sender).Name).Replace("Button", ""));
            App.MainFrame.Navigate(new PageCollection(id));
        }

    }
}
