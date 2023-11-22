using ImageDif.Core;
using LiveCharts;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ImageDif
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ObservableCollection<Graph> Graphs { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            //Graphs = new ObservableCollection<Graph>();

            var values = new ChartValues<double> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            chart.Series[0].Values = values;
            chart.AxisY[0].MinValue = 0;
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {  
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                  string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                  string filePath = files[0];

                  // Load the image
                  BitmapImage image = new BitmapImage(new Uri(filePath));

                  // Resize the image
                  DroppedImage.Width = 150;
                  DroppedImage.Height = 200;
                  DroppedImage.Source = image;

                  // Save the image to the DownloadableImages folder
                  string savePath = Path.Combine("D:\\University\\Img\\ImageDif\\DownloadableImages", Path.GetFileName(filePath));
                  string directoryPath = Path.GetDirectoryName(savePath);
                  if (!Directory.Exists(directoryPath))
                  {
                      try
                      {
                          Directory.CreateDirectory(directoryPath);
                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine("An error occurred while creating the directory: " + ex.Message);
                          return;
                      }
                  }
                  using (FileStream stream = new FileStream(savePath, FileMode.Create))
                  {
                      JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                      encoder.Frames.Add(BitmapFrame.Create(image));
                      encoder.Save(stream);
                  }
              }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the image from the DroppedImage control
            BitmapImage image = DroppedImage.Source as BitmapImage;

            // Check if an image is loaded
            if (image != null)
            {
                // Save the image to the DownloadableImages folder
                string savePath = Path.Combine("D:\\University\\Img\\ImageDif\\DownloadableImages", "saved_image.jpg");
                string directoryPath = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directoryPath))
                {
                    try
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while creating the directory: " + ex.Message);
                        return;
                    }
                }
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);
                }
            }
        }
        private async void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the directory path
            string directoryPath = "D:\\University\\Img\\ImageDif\\DownloadableImages";

            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Delete all files in the directory
                Directory.EnumerateFiles(directoryPath).ToList().ForEach(File.Delete);

                // Clear the DroppedImage control
                DroppedImage.Source = null;
            }
        }
        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"D:\University\Img\ImageDif\DownloadableImages");
        }
    }
}

// checking if saved img exists
//string imagePath = @"D:\University\Img\ImageDif\DownloadableImages\saved_image.jpg";
//if (File.Exists(imagePath))
//{
//    DroppedImage.Source = new BitmapImage(new Uri(imagePath));
//}