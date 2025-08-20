using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using Syncfusion.UI.Xaml.Charts;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace WinUIExportSample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await SaveAsImageAsync(Chart, "chart.png");
        }

        private async Task SaveAsImageAsync(SfCartesianChart chart, string fileName)
        {
            if (chart == null)
                return;

            // Render the chart to a RenderTargetBitmap
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(chart);

            // Get pixel data
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
            var pixels = pixelBuffer.ToArray();

            // Determine file format and encoder
            string extension = Path.GetExtension(fileName)?.ToLower() ?? ".png";
            var encoderId = extension == ".jpg" || extension == ".jpeg"
                ? BitmapEncoder.JpegEncoderId
                : BitmapEncoder.PngEncoderId;

            // Choose image save path
            var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(@"D:\");
            var picturesFolder = await folder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);


            // Encode the image
            using (var stream = await picturesFolder.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);

                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                   (uint)renderTargetBitmap.PixelWidth,
                   (uint)renderTargetBitmap.PixelHeight,
                    96.0, // DPI X
                    96.0, // DPI Y
                    pixels);

                await encoder.FlushAsync();
            }
        }
    }
}
