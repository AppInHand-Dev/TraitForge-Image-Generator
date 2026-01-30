using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
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
using TraitForge.MVC.Collection;
using TraitForge.MVC.TraitType;

namespace TraitForge.Windows
{
    /// <summary>
    /// Logica di interazione per WindowDelete.xaml
    /// </summary>
    public partial class WindowDelete : Window
    {

        private static string Type { get; set; }
        private static Collection Collection { get; set; }
        private static TraitType TraitType { get; set; }
        //private static Trait Trait { get; set; }

        public WindowDelete(Collection collection)
        {
            InitializeComponent();

            Collection = collection;
            Type = "Collection";

        }

        public WindowDelete(TraitType traitType)
        {
            InitializeComponent();

            TraitType = traitType;
            Type = "TraitType";
        }

        /*public WindowDelete(TraitType traitType)
        {
            InitializeComponent();

            TraitType = traitType;
            Type = "TraitType";

        }*/

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {

            string folderPath = "";

            if(Type == "Collection")
            {
                CollectionController.Delete(App.c1.DbConnection, Collection);

                folderPath = $@"./{CollectionController.Collection.Name}";

                CollectionController.Collection = null;
            } else if(Type == "TraitType")
            {
                TraitTypeController.Delete(App.c1.DbConnection, TraitType);

                folderPath = $@"./{CollectionController.Collection.Name}/{TraitTypeController.TraitType.Name}";

                TraitTypeController.TraitType = null;
            } else if(Type == "Trait")
            {
            }

            if(folderPath != "")
            {
                Directory.Delete(folderPath, true);
            }

            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
