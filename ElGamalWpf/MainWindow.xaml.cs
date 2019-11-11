using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace ElGamalWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _loadFilePath;
        private string _saveFilePath;
        private object bigintger;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void LoadClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                _loadFilePath = openFileDialog.FileName;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                _saveFilePath = saveFileDialog.FileName;
            }
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(p.Text))
            {
                g.Items.Clear();
                return;
            }
            BigInteger tempP = BigInteger.Parse(p.Text);

            for (var item = 1; item < tempP - 1; item++)
                if (FastPow(item, (tempP - 1) / 2, p) != 1)
                    if (FastPow(item, (tempP - 1) / 3, p) != 1)
                        Console.Write($"{item} ");

            if (string.IsNullOrEmpty(g.SelectedItem.ToString()))
                return;
            if (string.IsNullOrEmpty(x.Text) || string.IsNullOrEmpty(k.Text))
                return;

            Cipher.IsEnabled = true;
        }
    }
}
