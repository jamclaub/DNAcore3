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

namespace DNA.NETCORE3._0
{
    /// <summary>
    /// Interaction logic for TrimmerWindow.xaml
    /// </summary>
    public partial class TrimmerWindow : Window
    {
        public double PercentageData { get; set; }
        public MainWindow mainWindow;
        public TrimmerWindow(MainWindow mainWindow, double Percentage, double placeholder, double placeholder2)
        {
            InitializeComponent();
            PercentageData = Percentage;
            this.mainWindow = mainWindow;
            string showPercentage = PercentageData.ToString();
            PercentageSetting.Text = showPercentage;
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Your settings will not be saved.  \n Return to main menu?", "Return ", MessageBoxButton.YesNo);
            switch (choice)
            {
                case MessageBoxResult.Yes:
                    mainWindow.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
