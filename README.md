# How to Export Chart as Image in WinUI Chart
This article provides a detailed walkthrough on how to export a [WinUI Cartesian Chart](https://www.syncfusion.com/winui-controls/cartesian-charts) as an image. You can export the chart view in your desired file format, with supported formats being **JPEG** and **PNG.**

### Initialize SfCartesianChart:

Set up the **SfCartesianChart** following the [ Syncfusion WinUI Cartesian Chart documentation.](https://help.syncfusion.com/winui/cartesian-charts/getting-started)
 ```xml
<StackPanel Orientation="Vertical">
 <chart:SfCartesianChart x:Name="Chart" Background="White" IsTransposed="True" 
                         Header="Daily Water Consumption Tracking">
     ....
     <chart:ColumnSeries ItemsSource="{Binding DailyWaterIntake}"
                         XBindingPath="Day" 
                         YBindingPath="Liters"
                         ShowDataLabels="True">
         <chart:ColumnSeries.DataLabelSettings>
             <chart:CartesianDataLabelSettings Position="Inner"/>
         </chart:ColumnSeries.DataLabelSettings>
     </chart:ColumnSeries>
 </chart:SfCartesianChart>

     <Button x:Name="button" Content="Export as Image" Click="Button_Click" />
 </StackPanel> 
 ```

### Export the Chart as an Image:
 
 ```csharp
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
 ```

**Note:** By default, the chart background is transparent in both light and dark themes. You can customize this background by setting the **Background** property on the chart if needed. The exported image will have a transparent background unless you explicitly set it to a color.

**Chart output**
 
 ![Chart-WinUI.png](https://support.syncfusion.com/kb/agent/attachment/article/18644/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MjM0Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.0wMM_ZaV1z0RXFn8efmG0bKwTOt6QMiZ7DQ5fa1QLyU)

**Exported Chart Image**

 ![chart.png](https://support.syncfusion.com/kb/agent/attachment/article/18644/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MjMyIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.VhSO304zS7VSvBDSySJWOBxdySgRK0LWuAdmx3Vl4No)

### Github
For a more detailed implementation and to view the sample demo, refer to the [Export Chart View as Image GitHub sample.](https://github.com/SyncfusionExamples/How-to-Export-Chart-as-Image-in-WinUI-Chart)

### Conclusion

I hope you enjoyed learning about how to Export Chart as Image in WinUI Chart.

You can refer to our [WinUI charts feature tour page](https://www.syncfusion.com/winui-controls/cartesian-charts) to know about its other groundbreaking feature representations and [documentation](https://help.syncfusion.com/winui/charts/getting-started), and how to quickly get started for configuration specifications.

For current customers, you can check out our components from the [License and Downloads page](https://www.syncfusion.com/sales/teamlicense). If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/account/manage-trials/downloads) to check out our other controls.

If you have any queries or require clarifications, please let us know in the comments section below. You can also contact us through our [support forums](https://www.syncfusion.com/forums/), [Display-Trac](https://support.syncfusion.com/create), or [feedback portal](https://www.syncfusion.com/feedback/winui?control=sfcartesianchart). We are always happy to assist you!

