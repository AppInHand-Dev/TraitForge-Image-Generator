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
    /// Logica di interazione per WindowTraitTypeEdit.xaml
    /// </summary>
    public partial class WindowTraitTypeEdit : Window
    {

        private string OldName { get; set; }

        public WindowTraitTypeEdit()
        {
            InitializeComponent();

            TxtboxTraitTypeName.Text = TraitTypeController.TraitType.Name;
        }

        private void BtnTraitTypeEditApply_Click(object sender, RoutedEventArgs e)
        {
            OldName = TraitTypeController.TraitType.Name;

            TraitTypeController.TraitType.Name = TxtboxTraitTypeName.Text;

            TraitTypeController.Edit(App.c1.DbConnection, TraitTypeController.TraitType);

            Directory.CreateDirectory($@"./{CollectionController.Collection.Name}/{OldName}");

            Directory.Move($@"./{CollectionController.Collection.Name}/{OldName}", $@"./{CollectionController.Collection.Name}/{TraitTypeController.TraitType.Name}");
        }

        private void BtnTraitTypeEditCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
