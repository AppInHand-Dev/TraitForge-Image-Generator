using System;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using TraitForge.MVC.Collection;
using TraitForge.MVC.Trait;
using TraitForge.MVC.TraitType;
using TraitForge.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TraitForge.Pages
{
    /// <summary>
    /// Logica di interazione per PageCollection.xaml
    /// </summary>
    public partial class PageCollection : Page
    {

        public PageCollection(int id)
        {
            InitializeComponent();

            CollectionController.Collection = CollectionController.GetById(App.c1.DbConnection, id);

            LblCollectionName.Content = CollectionController.Collection.Name;
            LblCollectionDescription.Content = CollectionController.Collection.Description;

            List<TraitType> traitTypes = TraitTypeController.GetList(App.c1.DbConnection, id);

            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());
            GridTraitTypes.ColumnDefinitions.Add(new ColumnDefinition());

            int i = 0;
            foreach (TraitType traitType in traitTypes)
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
                    Content = traitType.Name,
                    // Name = "Lbl" + traitType.Id,
                    //Width = 200,
                    Height = 30,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn1 = new Button
                {
                    Content = "Edit",
                    Name = "Edit" + traitType.Id,
                    //Width = 50,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Button newBtn2 = new Button
                {
                    Content = "Delete",
                    Name = "Del" + traitType.Id,
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

                Label newLbl2 = new Label
                {
                    Content = "tot img",
                    //Name = "Lbl" + traitType.Id,
                    //Width = 20,
                    Height = 30,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                GridTraitTypes.RowDefinitions.Add(new RowDefinition());
                
                Grid.SetRow(newBtn5, i);
                Grid.SetColumn(newBtn5, 0);
                Grid.SetColumnSpan(newBtn5, 1);
                GridTraitTypes.Children.Add(newBtn5);

                Grid.SetRow(newBtn4, i);
                Grid.SetColumn(newBtn4, 1);
                Grid.SetColumnSpan(newBtn4, 1);
                GridTraitTypes.Children.Add(newBtn4);

                Grid.SetRow(newLbl1, i);
                Grid.SetColumn(newLbl1, 2);
                Grid.SetColumnSpan(newLbl1, 1);
                GridTraitTypes.Children.Add(newLbl1);

                Grid.SetRow(newBtn1, i);
                Grid.SetColumn(newBtn1, 3);
                Grid.SetColumnSpan(newBtn1, 1);
                GridTraitTypes.Children.Add(newBtn1);

                Grid.SetRow(newBtn2, i);
                Grid.SetColumn(newBtn2, 4);
                Grid.SetColumnSpan(newBtn2, 1);
                GridTraitTypes.Children.Add(newBtn2);

                Grid.SetRow(newBtn3, i);
                Grid.SetColumn(newBtn3, 5);
                Grid.SetColumnSpan(newBtn3, 1);
                GridTraitTypes.Children.Add(newBtn3);

                Grid.SetRow(newLbl2, i);
                Grid.SetColumn(newLbl2, 6);
                Grid.SetColumnSpan(newLbl2, 1);
                GridTraitTypes.Children.Add(newLbl2);

                newBtn1.Click += new RoutedEventHandler(BtnTraitTypeEdit_Click);
                newBtn2.Click += new RoutedEventHandler(BtnTraitTypeDelete_Click);

                i++;
            }

        }

        private void BtnCollectionDetailsEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowCollectionEdit w = new WindowCollectionEdit();
            w.ShowDialog();
        }

        private void BtnCollectionDelete_Click(object sender, RoutedEventArgs e)
        {
            WindowDelete w = new WindowDelete(CollectionController.Collection);
            w.ShowDialog();
        }

        private void BtnTraitTypeAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowTraitTypeAdd w = new WindowTraitTypeAdd();
            w.ShowDialog();
        }

        private void BtnTraitTypeEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse((((Button)sender).Name).Replace("Edit", ""));
            App.MainFrame.Navigate(new PageTraitType(id));
        }

        private void BtnTraitTypeDelete_Click(object sender, RoutedEventArgs e)
        {
            TraitTypeController.TraitType = TraitTypeController.GetById(App.c1.DbConnection, Int32.Parse((((Button)sender).Name).Replace("Del", "")));

            WindowDelete w = new WindowDelete(TraitTypeController.TraitType);
            w.ShowDialog();
        }

        private void BtnShuffle_Click(object sender, RoutedEventArgs e)
        {
            int max = 1;

            List<TraitType> traitTypes = TraitTypeController.GetList(App.c1.DbConnection, CollectionController.Collection.Id);

            for (int i = 0; i < traitTypes.Count; i++)
            {
                traitTypes[i].Traits = TraitController.GetList(App.c1.DbConnection, traitTypes[i].Id);

                max *= (traitTypes[i].Traits.Count > 0) ? traitTypes[i].Traits.Count : 1;
            }

            List<Canvas> canvases = new List<Canvas>(max);

            for (int i = 0; i < max; i++)
            {
                canvases.Add(new Canvas());
                canvases[i].Width = 600;
                canvases[i].Height = 600;
            }

            Directory.CreateDirectory($@"./output/{CollectionController.Collection.Name}");

            List<string> imagePaths = new List<string>(); // come inserirla nella funzione? invece che string, usare direttamente Uri per velocizzare bypassando la creazione dell'istanza?
            generateCollection(canvases, traitTypes, imagePaths, 0, 0);

            for (int i = 0; i<canvases.Count; i++)
            {
                saveCanvasToFile(canvases[i], $"./output/{CollectionController.Collection.Name}", $"nft-{i}");
            }

            Debug.WriteLine("Done!");

        }

        private int generateCollection(List<Canvas> canvases, List<TraitType> traitTypes, List<string> imagePaths, int indexOfCurrentCanvas, int indexOfCurrentTraitType)
        {
            for (int i = 0; i < traitTypes[indexOfCurrentTraitType].Traits.Count; i++)
            {
                string imagePath = AppContext.BaseDirectory + $@"/{CollectionController.Collection.Name}/{traitTypes[indexOfCurrentTraitType].Name}/{traitTypes[indexOfCurrentTraitType].Traits[i].Name}";

                if (i > 0)
                {
                    foreach (string _imagePath in imagePaths)
                    {
                        Image _image = new Image
                        {
                            Source = new BitmapImage(new Uri(_imagePath)),
                        };
                        canvases[indexOfCurrentCanvas].Children.Add(_image);
                    }
                }

                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath)),
                };

                canvases[indexOfCurrentCanvas].Children.Add(image);
                indexOfCurrentCanvas++;

                while (traitTypes.ElementAtOrDefault(indexOfCurrentTraitType + 1) != null && traitTypes[indexOfCurrentTraitType + 1].Traits.Count > 0)
                {
                    indexOfCurrentCanvas--;
                    imagePaths.Add(imagePath);
                    indexOfCurrentCanvas = generateCollection(canvases, traitTypes, imagePaths, indexOfCurrentCanvas, indexOfCurrentTraitType + 1); // recursive
                    imagePaths.RemoveAt(imagePaths.Count - 1);
                    break;
                }
            }

            return indexOfCurrentCanvas;
        }

        private void saveCanvasToFile(Canvas canvas, string filePath, string fileName)
        {
            // Save current canvas transform
            Transform transform = canvas.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            canvas.LayoutTransform = null;

            // Get the size of canvas
            Size size = new Size(canvas.Width, canvas.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(canvas);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream($"{filePath}/{fileName}.png", FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }

            // Restore previously saved layout
            canvas.LayoutTransform = transform;
        }

    }
}
