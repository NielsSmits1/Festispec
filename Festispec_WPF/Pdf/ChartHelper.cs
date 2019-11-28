using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Separator = LiveCharts.Wpf.Separator;

namespace Festispec_WPF.Helpers
{
    public static class ChartHelper
    {
        private const string Location = "D:/documents/temp/";
        public static string GenerateChart(List<string> GivenAwnsers, string chartType)
        {
            var awn = GivenAwnsers.Distinct().OrderBy(a => a).ToArray();
            ChartValues<int> values = new ChartValues<int>();
            foreach (var l in awn)
            {
                values.Add(GivenAwnsers.Where(x => x.Equals(l)).Count());
            }

            CartesianChart myChart;
            if (chartType == "Bar")
               myChart = GenerateBar(awn, values);
            else if (chartType == "Row")
               myChart = GenerateRow(awn, values);
            else
                myChart = GeneratePie(awn, values);


            var viewbox = new Viewbox();
            viewbox.Child = myChart;
            viewbox.Measure(myChart.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), myChart.RenderSize));
            myChart.Update(true, true); //force chart redraw
            viewbox.UpdateLayout();

            var name = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss-fff") + ".png";
            SaveToPng(myChart, name);


            return Location + name;
        }

        private static CartesianChart GenerateRow(string[] awn, ChartValues<int> values)
        {
            Series chart = new RowSeries()
            {
                Values = values
            };

            var myChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = 500,
                Height = 500,

                // based on new paramter switch around the charts
                Series = new SeriesCollection
                    {
                       chart
                    }
            };

            var awnsersAxis = new Axis
            {
                Labels = awn,
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                }
            };

            var countAxis = new Axis
            {
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                }
            };


            myChart.AxisX.Add(countAxis);
            myChart.AxisY.Add(awnsersAxis);

            return myChart;
        }

        private static CartesianChart GenerateBar(string[] awn, ChartValues<int> values)
        {
            Series chart = new ColumnSeries()
            {
                Values = values
            };

            var myChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = 500,
                Height = 500,

                // based on new paramter switch around the charts
                Series = new SeriesCollection
                    {
                       chart
                    }
            };

            var awnsersAxis = new Axis
            {
                Labels = awn,
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                }
            };

            var countAxis = new Axis
            {
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                }
            };

            myChart.AxisX.Add(awnsersAxis);
            myChart.AxisY.Add(countAxis);

            return myChart;
        }

        private static CartesianChart GeneratePie(string[] awn, ChartValues<int> values)
        {
            Series chart = new ColumnSeries()
            {
                Values = values
            };

            var myChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = 500,
                Height = 500,

                // based on new paramter switch around the charts
                Series = new SeriesCollection
                    {
                       chart
                    }
            };

            return myChart;
        }

        private static void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(Location + fileName)) encoder.Save(stream);
        }
    }
}
