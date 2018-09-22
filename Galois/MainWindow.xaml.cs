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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.Numerics;
using System.Security.Cryptography;
using System.IO;

namespace Galois
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += (s, e) => (this.DataContext as IDisposable).Dispose();
        }
        
        private void OpenInputGisto(object sender, RoutedEventArgs e)
        {
            if (originalPath.Text == "" || originalPath.Text == null)
            {
                MessageBox.Show("Вкажіть шлях до вхідного файлу!");
                return;
            }
            var gisto = new Gisto();
            gisto.Title = "Вхідний файл";
            var enthropy = gisto.StartGisto(originalPath.Text);
            double E = (double)enthropy[0];
            int[] values = enthropy[1] as int[];
            gisto.enthropy.Content = E;
            gisto.InitializeChart(values);
            gisto.Show();

            var table = new Table();
            table.Title = "Вхідний файл";
            table.enthropy.Content = E;
            table.InitializeTable(values);
            table.Show();
        }
        private void OpenOutputGisto(object sender, RoutedEventArgs e)
        {
            if (chypherPath.Text == "" || chypherPath.Text == null)
            {
                MessageBox.Show("Вкажіть шлях до вихідного файлу!");
                return;
            }
            var gisto = new Gisto();
            gisto.Title = "Вихідний файл";
            var enthropy = gisto.StartGisto(chypherPath.Text);
            double E = (double)enthropy[0];
            int[] values = enthropy[1] as int[];
            gisto.enthropy.Content = E;
            gisto.InitializeChart(values);
            gisto.Show();

            var table = new Table();
            table.Title = "Вихідний файл";
            table.enthropy.Content = E;
            table.InitializeTable(values);
            table.Show();
        }
        private void SetEncryptionMode(object sender, RoutedEventArgs e)
        {
            if (!window.IsLoaded)
                return;
            startDecryption.Visibility = Visibility.Collapsed;
            startEncryption.Visibility = Visibility.Visible;
            originalPath.Text = "";
            chypherPath.Text = "";
        }
        private void SetDecryptionMode(object sender, RoutedEventArgs e)
        {
            if (!window.IsLoaded)
                return;
            startDecryption.Visibility = Visibility.Visible;
            startEncryption.Visibility = Visibility.Collapsed;
            originalPath.Text = "";
            chypherPath.Text = "";
        }
    }
}
