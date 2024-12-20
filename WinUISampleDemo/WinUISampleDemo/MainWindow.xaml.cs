using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

using Syncfusion.UI.Xaml.Charts;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUISampleDemo
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
            await SaveAsImageAsync(Chart, "chart.jpeg");
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
            var picturesFolder = await folder.CreateFileAsync( fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);


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
