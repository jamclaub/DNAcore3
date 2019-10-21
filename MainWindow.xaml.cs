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

namespace DNA.NETCORE3._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PreviewWindow Preview;
        TrimmerWindow Trimmer;

        public MainWindow()
        {
            InitializeComponent();
            Preview = new PreviewWindow(this);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Preview.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Trimmer = new TrimmerWindow(this, 0, 0, 0);
            Trimmer.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //win3.Show();
            this.Hide();
        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult chose = MessageBox.Show("Close Data Processor?", "Data Processor exit conformation.", MessageBoxButton.OKCancel);
            switch (chose)
            {
                case MessageBoxResult.OK:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
    }
}
