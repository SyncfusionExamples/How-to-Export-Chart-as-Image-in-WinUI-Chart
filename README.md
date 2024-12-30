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

**Note:** By default, the chart background is transparent. When using JPEG format, RenderTargetBitmap converts the transparent background to black. To resolve this, set the chart’s BackgroundColor to white or any preferred color.

**Chart output**
 
 ![Chart-WinUI.png](https://support.syncfusion.com/kb/agent/attachment/article/18644/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MjM0Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.0wMM_ZaV1z0RXFn8efmG0bKwTOt6QMiZ7DQ5fa1QLyU)

**Exported Chart Image**

 ![chart.png](https://support.syncfusion.com/kb/agent/attachment/article/18644/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MjMyIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.VhSO304zS7VSvBDSySJWOBxdySgRK0LWuAdmx3Vl4No)

### KB link 
For a more detailed, refer to the [Export Chart View as Image KB.](https://support.syncfusion.com/agent/kb/18644)

