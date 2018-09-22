using System;
using System.Collections.Generic;
using System.IO;
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

namespace Galois
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    /// 
    #region TABLE_8
    public class GistoTable_8
    {
        public int byte_0 { get; set; }
        public int byte_1{ get; set; }
        public int byte_2 { get; set; }
        public int byte_3 { get; set; }
        public int byte_4 { get; set; }
        public int byte_5 { get; set; }
        public int byte_6 { get; set; }
        public int byte_7 { get; set; }
    }
    #endregion
    #region TABLE_10
    public class GistoTable_10
    {
        public int byte_0 { get; set; }
        public int byte_1 { get; set; }
        public int byte_2 { get; set; }
        public int byte_3 { get; set; }
        public int byte_4 { get; set; }
        public int byte_5 { get; set; }
        public int byte_6 { get; set; }
        public int byte_7 { get; set; }
        public int byte_8 { get; set; }
        public int byte_9 { get; set; }
    }
    #endregion
    #region TABLE_16
    public class GistoTable_16
    {
        public int byte_0 { get; set; }
        public int byte_1 { get; set; }
        public int byte_2 { get; set; }
        public int byte_3 { get; set; }
        public int byte_4 { get; set; }
        public int byte_5 { get; set; }
        public int byte_6 { get; set; }
        public int byte_7 { get; set; }
        public int byte_8 { get; set; }
        public int byte_9 { get; set; }
        public int byte_10 { get; set; }
        public int byte_11 { get; set; }
        public int byte_12 { get; set; }
        public int byte_13 { get; set; }
        public int byte_14 { get; set; }
        public int byte_15 { get; set; }
    }
    #endregion

    public partial class Table : Window
    {
        public static int colsCount = 8;
        private int[] bytes;
        public Table()
        {
            InitializeComponent();
        }

        public void InitializeTable(int[] byteArray)
        {
            bytes = byteArray;
            DrawTable();
        }
        private void DrawTable()
        {
            switch (colsCount)
            {
                case 8:
                    dynamic TableData = new List<GistoTable_8>();
                    for (int row = 0; row < bytes.Length / colsCount; row++)
                    {
                        var buffer = SubArray(bytes, row * colsCount, colsCount);
                        TableData.Add(
                            new GistoTable_8()
                            {
                                byte_0 = buffer[0],
                                byte_1 = buffer[1],
                                byte_2 = buffer[2],
                                byte_3 = buffer[3],
                                byte_4 = buffer[4],
                                byte_5 = buffer[5],
                                byte_6 = buffer[6],
                                byte_7 = buffer[7]
                            }
                        );
                    }
                    gistoTable.ItemsSource = TableData;
                    window.Height = 750;
                    break;
                case 10:
                    TableData = new List<GistoTable_10>();
                    for (int row = 0; row < bytes.Length / colsCount; row++)
                    {
                        var buffer = SubArray(bytes, row * colsCount, colsCount);
                        TableData.Add(
                            new GistoTable_10()
                            {
                                byte_0 = buffer[0],
                                byte_1 = buffer[1],
                                byte_2 = buffer[2],
                                byte_3 = buffer[3],
                                byte_4 = buffer[4],
                                byte_5 = buffer[5],
                                byte_6 = buffer[6],
                                byte_7 = buffer[7],
                                byte_8 = buffer[8],
                                byte_9 = buffer[9]
                            }
                        );
                    }
                    gistoTable.ItemsSource = TableData;
                    window.Height = 620;
                    break;
                case 16:
                    TableData = new List<GistoTable_16>();
                    for (int row = 0; row < bytes.Length / colsCount; row++)
                    {
                        var buffer = SubArray(bytes, row * colsCount, colsCount);
                        TableData.Add(
                            new GistoTable_16()
                            {
                                byte_0 = buffer[0],
                                byte_1 = buffer[1],
                                byte_2 = buffer[2],
                                byte_3 = buffer[3],
                                byte_4 = buffer[4],
                                byte_5 = buffer[5],
                                byte_6 = buffer[6],
                                byte_7 = buffer[7],
                                byte_8 = buffer[8],
                                byte_9 = buffer[9],
                                byte_10 = buffer[10],
                                byte_11 = buffer[11],
                                byte_12 = buffer[12],
                                byte_13 = buffer[13],
                                byte_14 = buffer[14],
                                byte_15 = buffer[15]
                            }
                        );
                    }
                    gistoTable.ItemsSource = TableData;
                    window.Height = 430;
                    break;
            }
        }
        private T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        private void ChangeColsCount(object sender, SelectionChangedEventArgs e)
        {
            if (!window.IsLoaded) return;
            colsCount = Int32.Parse(((sender as ComboBox).SelectedItem as ComboBoxItem).Uid);
            DrawTable();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() % 2 == 0)
                e.Row.Background = Brushes.LightGray;
            e.Row.Header = (e.Row.GetIndex() * colsCount).ToString();
            e.Row.Height = 20;
            e.Row.HorizontalContentAlignment = HorizontalAlignment.Center;
        }
        private void DataGrid_LoadingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = e.Column.Header.ToString().Substring(4);
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
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)gistoTable.ActualWidth, (int)gistoTable.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(gistoTable);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext ctx = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(gistoTable);
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
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)gistoTable.ActualWidth, (int)gistoTable.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            Rect bounds = VisualTreeHelper.GetDescendantBounds(gistoTable);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(gistoTable);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);
            Clipboard.SetImage(rtb);
            MessageBox.Show("Таблицю скопійовано у буфер!");
        }
    }
}
