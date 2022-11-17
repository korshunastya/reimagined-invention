using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matrix
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btGeneration_Click(object sender, RoutedEventArgs e)
        {
            wrpPanel1.Children.Clear();
            wrpPanel2.Children.Clear();
            int n = Convert.ToInt32(tbN.Text); //Размерность первой матрицы
            int m = Convert.ToInt32(tbM.Text); //Размерность второй матрицы
            Random ran = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TextBox x = new TextBox();
                    x.Height = wrpPanel1.Height / n;
                    x.Width = wrpPanel1.Width / n;
                    x.FontSize = 30;
                    x.Text = ran.Next(1, 15).ToString();
                    x.VerticalAlignment = VerticalAlignment.Center;
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel1.Children.Add(x);
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    TextBox x = new TextBox();
                    x.Height = wrpPanel2.Height / m;
                    x.Width = wrpPanel2.Width / m;
                    x.FontSize = 30;
                    x.Text = ran.Next(1, 15).ToString();
                    x.VerticalAlignment = VerticalAlignment.Center;
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel2.Children.Add(x);
                }
            }
        }

        private GMatrix<int> result;
        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            wrpPanel3.Children.Clear();
            int n = Convert.ToInt32(tbN.Text);
            int m = Convert.ToInt32(tbM.Text);

            GMatrix<int> Matrix1 = new GMatrix<int>(n, n);
            GMatrix<int> Matrix2 = new GMatrix<int>(m, m);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matrix1[i, j] = int.Parse((wrpPanel1.Children[i * n + j] as TextBox).Text);
                }
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                {
                    Matrix2[i, j] = int.Parse((wrpPanel2.Children[i * m + j] as TextBox).Text);
                }

            result = new GMatrix<int>(n, m);
            if (MethodSelection.SelectedIndex == 0)
                result = Matrix1 + Matrix2;
            if (MethodSelection.SelectedIndex == 1)
                result = Matrix1 * Matrix2;
            time.Stop();
            double time1 = time.ElapsedMilliseconds;
            tbTime.Text = "Время расчёта: " + Convert.ToString(time1) + " мс";
            for (int i = 0; i < result.Row; i++)
                for (int j = 0; j < result.Column; j++)
                {
                    TextBox x = new TextBox();
                    x.Text = result[i, j].ToString();
                    x.Height = wrpPanel3.Height / result.Row;
                    x.Width = wrpPanel3.Width / result.Column;
                    x.FontSize = 30;
                    x.VerticalAlignment = VerticalAlignment.Center;
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel3.Children.Add(x);
                }

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            using (var writer = new StreamWriter(@"P:\Visual Studio\Projects\Matrix\file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                for (int i = 0; i < result.Row; i++)
                {
                    for (int j = 0; j < result.Column; j++)
                    {
                        csv.WriteField(result[i, j]);
                    }
                    csv.NextRecord();
                }
            MessageBox.Show("Сохранено");
        }
    }
}