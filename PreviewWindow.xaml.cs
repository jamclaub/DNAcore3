using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace DNA.NETCORE3._0
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public ChartValues<ObservablePoint> ValuesC { get; set; }
        public ChartValues<ObservablePoint> ValuesD { get; set; }
        private MainWindow mainWindow;
        public PreviewWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
            ValuesC = new ChartValues<ObservablePoint>();
            ValuesD = new ChartValues<ObservablePoint>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Previewer pre = new Previewer(ValuesA, ValuesB, ValuesC, ValuesD);
            pre.runRandomSampler();
            DataContext = this;
        }

        public double percentageData { get; set; }
        private void ChartOnDataClick(object sender, ChartPoint point)
        {
            percentageData = point.Y;
            string showPercentage = percentageData.ToString();
            PercentageChoice.Text = showPercentage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Would you like to send your settings to the trimmer?", "Timmer conformation", MessageBoxButton.YesNo);
            switch (choice)
            {
                case MessageBoxResult.Yes:
                    TrimmerWindow Trimmer = new TrimmerWindow(mainWindow, percentageData, 0, 0);
                    Trimmer.Show();
                    this.Hide();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }
    }
}
