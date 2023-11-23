using LiveCharts;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace ImageDif
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            chart.AxisY[0].MinValue = 0;
            chart.AxisX[0].MinValue = 0;
            chart.AxisY[0].LabelFormatter = value => value.ToString("F2");
            chart.AxisX[0].LabelFormatter = value => value.ToString("F2");
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
        private void Border_Drop2(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string filePath = files[0];

                // Load the image
                BitmapImage image = new BitmapImage(new Uri(filePath));

                // Resize the image
                DroppedImage2.Width = 150;
                DroppedImage2.Height = 200;
                DroppedImage2.Source = image;

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
        private void SaveImage(Image image, string savePath)
        {
            // Get the image from the DroppedImage control
            BitmapImage bitmapImage = image.Source as BitmapImage;

            // Check if an image is loaded
            if (bitmapImage != null)
            {
                // Save the image to the DownloadableImages folder
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
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(stream);
                }
            }
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the SaveImage method for each image
            SaveImage(DroppedImage, "D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image1.jpg");
            SaveImage(DroppedImage2, "D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image2.jpg");

            // Calculate statistic for each image
            var statistic1 = CalculateStatistic("D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image1.jpg");
            var statistic2 = CalculateStatistic("D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image2.jpg");

            // Use the statistic to build the charts
            chart.Series[0].Values = new ChartValues<double>(statistic1);
            chart.Series[1].Values = new ChartValues<double>(statistic2);
        }
        private void DeleteImage(Image image)
        {
            // Get the directory path
            string directoryPath = "D:\\University\\Img\\ImageDif\\DownloadableImages";
            string savedPath = "D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved";

            // Check if the directory exists
            if (Directory.Exists(directoryPath) && Directory.Exists(savedPath))
            {
                // Delete all files in the directory
                Directory.EnumerateFiles(directoryPath).ToList().ForEach(File.Delete);
                Directory.EnumerateFiles(savedPath).ToList().ForEach(File.Delete);
                // Clear the DroppedImage control
                image.Source = null;
            }
        }

        private async void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the DeleteImage method for each image
            DeleteImage(DroppedImage);
            DeleteImage(DroppedImage2);
        }
        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"D:\University\Img\ImageDif\DownloadableImages");
        }

        private List<double> CalculateStatistic(string imagePath)
        {
            using (var image = new System.Drawing.Bitmap(imagePath))
            {
                // Calculation of statistical characteristics
                // In this example we will use the average color value of the image
                List<double> values = new List<double>();
                for (int y = 0; y < image.Height; y++)
                {
                    double sum = 0;
                    for (int x = 0; x < image.Width; x++)
                    {
                        var pixel = image.GetPixel(x, y);
                        sum += pixel.GetBrightness();
                    }
                    values.Add(sum / image.Width);
                }
                return values;
            }
        }
        private async void Build_Click(object sender, RoutedEventArgs e)
        {
            // Call the SaveImage method for each image
            SaveImage(DroppedImage, "D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image1.jpg");
            SaveImage(DroppedImage2, "D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image2.jpg");

            // Calculate statistic for each image
            var statistic1 = CalculateStatistic("D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image1.jpg");
            var statistic2 = CalculateStatistic("D:\\University\\Img\\ImageDif\\DownloadableImages\\Saved\\saved_image2.jpg");

            // Find the maximum value in the first statistic
            var max1 = statistic1.Max();

            // Scale the second statistic by the maximum value of the first statistic
            var scaledStatistic2 = statistic2.Select(value => value * max1 / statistic2.Max()).ToList();

            // Use the statistic to build the charts
            chart.AxisY[0].MinValue = 0;
            chart.AxisX[0].MinValue = 0;
            chart.Series[0].Values = new ChartValues<double>(statistic1);
            chart.Series[1].Values = new ChartValues<double>(scaledStatistic2);
        }
    } 
}

// checking if saved imge exists
//string imagePath = @"D:\University\Img\ImageDif\DownloadableImages\saved_image.jpg";
//if (File.Exists(imagePath))
//{
//    DroppedImage.Source = new BitmapImage(new Uri(imagePath));
//}


//var values = new ChartValues<double> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
//chart.Series[0].Values = values;
//chart.AxisY[0].MaxValue = 30;