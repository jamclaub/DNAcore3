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
        public double percentageData = 0;
        public double WindowSize = 0;
        public double WindowQualityChoice = 0; 
        
        public PreviewWindow mainWindow;
        public TrimmerWindow(PreviewWindow mainWindow, double Percentage, double Window,double WindowQuality)
        {
            InitializeComponent();
            
            this.mainWindow = mainWindow;

            percentageData = Percentage;
            WindowSize = Window;
            WindowQualityChoice = WindowQuality;
            PercentageChoice.Text = Convert.ToString(percentageData);
            WindowSizeChoice.Text = Convert.ToString(WindowSize);
            windowQuality.Text = Convert.ToString(WindowQualityChoice);
            
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Your settings and any progress will not be saved.  \n Return to main menu?", "Return to Menu", MessageBoxButton.YesNo);
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
