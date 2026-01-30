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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using TraitForge.MVC.Collection;
using TraitForge.MVC.Trait;
using TraitForge.MVC.TraitType;
using TraitForge.Windows;

namespace TraitForge.Pages
{
    /// <summary>
    /// Logica di interazione per PageTraitType.xaml
    /// </summary>
    public partial class PageTraitType : Page
    {

        public PageTraitType(int id)
        {
            InitializeComponent();

            TraitTypeController.TraitType = TraitTypeController.GetById(App.c1.DbConnection, id);

            LblTraitTypeName.Content = TraitTypeController.TraitType.Name;
            //LblTraitTypeCollectionName.Content = TraitTypeController.TraitType.Description;

            List<Trait> traits = TraitController.GetList(App.c1.DbConnection, id);

            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraits.ColumnDefinitions.Add(new ColumnDefinition());

            int i = 0;
            foreach (Trait trait in traits)
            {

                Button newBtn5 = new Button
                {
                    Content = "U",
                    //Name = "Button" + traitType.Id,
                    //Width = 20,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn4 = new Button
                {
                    Content = "D",
                    //Name = "Button" + traitType.Id,
                    //Width = 20,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Label newLbl1 = new Label
                {
                    Content = trait.Name,
                    // Name = "Lbl" + traitType.Id,
                    //Width = 200,
                    Height = 30,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn1 = new Button
                {
                    Content = "Edit",
                    Name = "Edit" + trait.Id,
                    //Width = 50,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn2 = new Button
                {
                    Content = "Delete",
                    Name = "Del" + trait.Id,
                    //Width = 50,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn3 = new Button
                {
                    Content = "Exclude",
                    //Name = "Button" + traitType.Id,
                    //Width = 50,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                GridTraits.RowDefinitions.Add(new RowDefinition());

                Grid.SetRow(newBtn5, i);
                Grid.SetColumn(newBtn5, 0);
                Grid.SetColumnSpan(newBtn5, 1);
                GridTraits.Children.Add(newBtn5);

                Grid.SetRow(newBtn4, i);
                Grid.SetColumn(newBtn4, 1);
                Grid.SetColumnSpan(newBtn4, 1);
                GridTraits.Children.Add(newBtn4);

                Grid.SetRow(newLbl1, i);
                Grid.SetColumn(newLbl1, 2);
                Grid.SetColumnSpan(newLbl1, 1);
                GridTraits.Children.Add(newLbl1);

                Grid.SetRow(newBtn1, i);
                Grid.SetColumn(newBtn1, 3);
                Grid.SetColumnSpan(newBtn1, 1);
                GridTraits.Children.Add(newBtn1);

                Grid.SetRow(newBtn2, i);
                Grid.SetColumn(newBtn2, 4);
                Grid.SetColumnSpan(newBtn2, 1);
                GridTraits.Children.Add(newBtn2);

                Grid.SetRow(newBtn3, i);
                Grid.SetColumn(newBtn3, 5);
                Grid.SetColumnSpan(newBtn3, 1);
                GridTraits.Children.Add(newBtn3);

                newBtn1.Click += new RoutedEventHandler(BtnTraitEdit_Click);
                newBtn2.Click += new RoutedEventHandler(BtnTraitDelete_Click);

                i++;
            }
        }

        private void BtnTraitTypeDetailsEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowTraitTypeEdit w = new WindowTraitTypeEdit();
            w.ShowDialog();
        }

        private void BtnTraitTypeDelete_Click(object sender, RoutedEventArgs e)
        {
            WindowDelete w = new WindowDelete(TraitTypeController.TraitType);
            w.ShowDialog();
        }

        private void BtnTraitAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowTraitAdd w = new WindowTraitAdd();
            w.ShowDialog();
        }

        private void BtnTraitEdit_Click(object sender, RoutedEventArgs e)
        {
            /*int id = Int32.Parse((((Button)sender).Name).Replace("Edit", ""));
            App.MainFrame.Navigate(new PageTraitType(id));*/
        }

        private void BtnTraitDelete_Click(object sender, RoutedEventArgs e)
        {
            /*WindowDelete w = new WindowDelete("trait");
            w.ShowDialog();*/
        }

        private void BtnTraitsAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files|*.bpm;*.jpeg;*.jpg;*.png;*.gif|BPM Files (*.bpm)|*.bpm|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            dlg.Multiselect = true;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                foreach (String file in dlg.FileNames)
                {
                    string filePath = file;

                    Trait trait = new Trait();

                    trait.Name = System.IO.Path.GetFileName(filePath);
                    trait.Raw = 0;
                    trait.Active = true;
                    trait.TraitTypeId = TraitTypeController.TraitType.Id;

                    TraitController.CreateNew(App.c1.DbConnection, trait);

                    File.Copy(filePath, $@"./{CollectionController.Collection.Name}/{TraitTypeController.TraitType.Name}/{trait.Name}");
                }
            }
        }
    }
}
