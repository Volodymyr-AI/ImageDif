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

            var values = new ChartValues<double> { 0, 1, 2, 3, 4, 5, 6 };
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
    }
}