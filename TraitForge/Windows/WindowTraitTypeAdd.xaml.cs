using System;
using System.Collections.Generic;
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
    /// Logica di interazione per WindowTraitTypeAdd.xaml
    /// </summary>
    public partial class WindowTraitTypeAdd : Window
    {

        public WindowTraitTypeAdd()
        {
            InitializeComponent();
        }

        private void BtnTraitTypeCreate_Click(object sender, RoutedEventArgs e)
        {
            TraitType traitType = new TraitType();

            traitType.Name = TxtboxTraitTypeName.Text;
            traitType.Order = 1;
            traitType.Active = true;
            traitType.CollectionId = CollectionController.Collection.Id;

            TraitTypeController.CreateNew(App.c1.DbConnection, traitType);

            Directory.CreateDirectory($@"./{CollectionController.Collection.Name}/{traitType.Name}");
        }

        private void BtnTraitTypeAddCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
