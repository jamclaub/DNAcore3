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
        public double percentageData = 0;
        public double WindowSize = 0;
        public double WindowQualityChoice = 0;
        
        private MainWindow mainWindow;
        public PreviewWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
            ValuesC = new ChartValues<ObservablePoint>();
            ValuesD = new ChartValues<ObservablePoint>();

            Percentage.Text = Convert.ToString(percentageData);
            WSize.Text = Convert.ToString(WindowSize);
            WQuality.Text = Convert.ToString(WindowQualityChoice);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Previewer pre1 = new Previewer(ValuesA, ValuesB, ValuesC, ValuesD);
            Previewer pre2 = new Previewer(ValuesA, ValuesB, ValuesC, ValuesD);
            if(SingleFileSelected && !TwoFilesSelected)
            {
                pre1.runRandomSampler();
            }
            else if(!SingleFileSelected && TwoFilesSelected)
            {
                pre1.runRandomSamplerTwoFiles();
            }
            else if(SingleFileSelected && TwoFilesSelected || !SingleFileSelected && !TwoFilesSelected)
            {
                MessageBoxResult choice = MessageBox.Show("Please select how many files you wish to preview", "File Number", MessageBoxButton.OK);
            }
            DataContext = this;
        }

        private void ChartOnDataClick(object sender, ChartPoint point)
        {
            percentageData = point.Y;
            Percentage.Text = Convert.ToString(percentageData);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Would you like to send your settings to the trimmer?", "Timmer conformation", MessageBoxButton.YesNo);
            switch (choice)
            {
                case MessageBoxResult.Yes:
                    percentageData = Convert.ToDouble(Percentage.Text);
                    WindowSize = Convert.ToDouble(WSize.Text);
                    WindowQualityChoice = Convert.ToDouble(WQuality.Text);
                    TrimmerWindow Trimmer = new TrimmerWindow(mainWindow, percentageData, WindowSize, WindowQualityChoice);
                    Trimmer.Show();
                    this.Hide();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private bool SingleFileSelected = false;
        private bool TwoFilesSelected = false;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SingleFileSelected = true;
            TwoFilesSelected = false;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            SingleFileSelected = false;
            TwoFilesSelected = true;
        }
    }
}
