using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;


//Добавляем библиотеки Chart
using Sparrow.Chart;

namespace Galois
{
    /// <summary>
    /// Interaction logic for Gisto.xaml
    /// </summary>
    /// 
    public partial class Gisto : Window
    {
        public Gisto()
        {
            InitializeComponent();
        }
        public void InitializeChart(int[] values)
        {
            chart.YAxis.MaxValue = values.Max() + (values.Max() * 0.05);
            chart.XAxis.MaxValue = values.Length;
            chart.XAxis.MinorTicksCount = values.Length / 8;
            SeriesCollection series = new SeriesCollection() {
                new ColumnSeries()
            };
            var points = new PointsCollection();
            for (int i = 0; i < values.Length; i++)
            {
                points.Add(new DoublePoint()
                {
                    Data = i,
                    Value = values[i]
                });
            }
            series[0].Points = points;
            chart.Series = series;
        }
        public object[] StartGisto(string path)
        {
            byte[] Array = File.ReadAllBytes(path);
            int[] MyArr2 = new int[256];
            int N; //кількість байт у файлі
            double H; //ентропія
            int[] gistoArray = GISTO_e(Array);
            Buffer.BlockCopy(gistoArray, 0, MyArr2, 0, 0x400);
            //отриманий масив байт ГИСТО (вказано скільки яких байт) &H400 - 256*4=1024 байта, тому що UInteger це 4 байта
            N = Array.Length;
            H = GISTO_entropy(MyArr2, N);

            return new object[] { H, MyArr2 };
        }
        private int[] GISTO_e(byte[] MyArr1)
        {
            int[] myArr2 = new int[256];
            int value;
            //byte index;

            //Значення з масиву MyArr1 роблю індексом масиву MyArr2.
            //і отримане значення по цьому індексу збільшую на 1
            foreach (byte index in MyArr1)
            {
                value = myArr2[index];
                value += 1;
                myArr2[index] = value;
            }
            return myArr2;
        }

        private double GISTO_entropy(int[] MyArr2, int N)
        {
            double p, H = 0;

            foreach (int value in MyArr2)
            {
                p = (double)((double)value / (double)N);
                if (p == 0.0) continue;
                p *= (-Math.Log(p, 2));
                H += p;
            }
            return H;
        }
        private void Copy(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText((sender as Label).Content.ToString());
            MessageBox.Show("Дані скопійовано у буфер!");
        }
        private void Export_File(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "PNG Image|*.png";
            if (saveFile.ShowDialog() == true)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(chart);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext ctx = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(chart);
                    ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }
                rtb.Render(dv);
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = (FileStream)saveFile.OpenFile();
                png.Save(fs);
                fs.Close();
                MessageBox.Show("Файл \"" + saveFile.FileName + "\" успішно збережено!");
            }
        }
        private void Export_Buffer(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            Rect bounds = VisualTreeHelper.GetDescendantBounds(chart);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(chart);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);
            Clipboard.SetImage(rtb);
            MessageBox.Show("Гістограму скопійовано у буфер!");
        }
    }
}
