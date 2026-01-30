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


namespace TraitForge.Windows
{
    /// <summary>
    /// Logica di interazione per WindowCollectionEdit.xaml
    /// </summary>
    public partial class WindowCollectionEdit : Window
    {

        private string OldName { get; set; }

        public WindowCollectionEdit()
        {
            InitializeComponent();

            TxtboxCollectionName.Text = CollectionController.Collection.Name;
            TxtboxCollectionDescription.Text = CollectionController.Collection.Description;

        }

        private void BtnCollectionEditApply_Click(object sender, RoutedEventArgs e)
        {
            OldName = CollectionController.Collection.Name;

            CollectionController.Collection.Name = TxtboxCollectionName.Text;
            CollectionController.Collection.Description = TxtboxCollectionDescription.Text;

            CollectionController.Edit(App.c1.DbConnection, CollectionController.Collection);

            Directory.CreateDirectory($@"./{OldName}");

            Directory.Move($@"./{OldName}", $@"./{CollectionController.Collection.Name}");
        }

        private void BtnCollectionEditCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
