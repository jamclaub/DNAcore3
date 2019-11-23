﻿using System;
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
        public int percentageData = 0;
        public int WindowSize = 0;
        public int WindowQualityChoice = 0;
        public int MaxNumWindowFail = 0;
        private bool SingleFileSelected = false;
        private bool TwoFilesSelected = false;
        private bool SangerBool = false;
        private bool SolexaBool = false;
        private bool IlluminaV1Bool = false;
        private bool IlluminaV2Bool = false;
        private bool CustomBool = false;
        private string trimmername;
        private int offset = 0;
        
        public PreviewWindow()
        {
            InitializeComponent();
            //MessageBoxResult result = MessageBox.Show("some instructions here");
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
            ValuesC = new ChartValues<ObservablePoint>();
            ValuesD = new ChartValues<ObservablePoint>();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if(SingleFileSelected && TwoFilesSelected || !SingleFileSelected && !TwoFilesSelected)
            {
                MessageBoxResult choice = MessageBox.Show("Please select how many files you wish to preview", "File Number", MessageBoxButton.OK);
            }
            else if (!SangerBool && !SolexaBool && !IlluminaV1Bool && !IlluminaV2Bool && !CustomBool)
            {
                MessageBox.Show("Must choose a paradigm", "Paradigm", MessageBoxButton.OK);
            }
            else if (SingleFileSelected && !TwoFilesSelected &&  !CustomBool)
            {
                
                Previewer pre1 = new Previewer(ValuesA, ValuesB, ValuesC, ValuesD, offset);
                pre1.runRandomSampler();

            }
            else if(!SingleFileSelected && TwoFilesSelected && !CustomBool)
            {
                
                Previewer pre1 = new Previewer(ValuesA, ValuesB, ValuesC, ValuesD, offset);
                pre1.runRandomSamplerTwoFiles();
            }
            
            DataContext = this;
        }

        private void ChartOnDataClick(object sender, ChartPoint point)
        {
            percentageData = (int) point.Y;
            Percentage.Text = Convert.ToString(percentageData);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!SangerBool && !SolexaBool && !IlluminaV1Bool && !IlluminaV2Bool && !CustomBool)
            {
                MessageBox.Show("Must choose a paradigm", "Paradigm", MessageBoxButton.OK);
            }
            else if(string.IsNullOrEmpty(Percentage.Text) || string.IsNullOrEmpty(WSize.Text) || string.IsNullOrEmpty(WQuality.Text) 
                || string.IsNullOrEmpty(CustomOffset.Text) && CustomBool)
            {
                MessageBox.Show("Settings cannot be null", "Empty settings",MessageBoxButton.OK);
            }
            else
            {
                if (CustomBool)
                {
                    offset = Convert.ToInt32(CustomOffset.Text);
                }
                percentageData = Convert.ToInt32(Percentage.Text);
                WindowSize = Convert.ToInt32(WSize.Text);
                WindowQualityChoice = Convert.ToInt32(WQuality.Text);
                MaxNumWindowFail = Convert.ToInt32(MaxWinFailBox.Text);
                MessageBoxResult choice = MessageBox.Show("Quality of Nucleotides: " + percentageData + "\n" +
                                                          "Window Size: " + WindowSize + "\n" +
                                                          "Window Quality: " + WindowQualityChoice + "\n" +
                                                          "Max number of failed windows: " + MaxNumWindowFail + "\n" +
                                                          "Paradigm: " + trimmername + " " + offset + "\n" +
                                                          "Send these settings to the trimmer?", "Settings", MessageBoxButton.YesNo) ;
                switch (choice)
                {
                    case MessageBoxResult.Yes:
                        Trimmer Trim = new Trimmer(percentageData, WindowSize, WindowQualityChoice, MaxNumWindowFail, offset);
                        Trim.singlefile();
                        MessageBox.Show("Your file has been trimed", "Trimmer complete", MessageBoxButton.OK);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            
        }

        
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

        
        private void Sanger_Checked(object sender, RoutedEventArgs e)
        {
            trimmername = "Sanger";
            CustomOffset.IsReadOnly = true;
            CustomOffset.Background = Brushes.SlateGray;
            CustomOffset.BorderBrush = Brushes.SlateGray;
            SangerBool = true;
            SolexaBool = false;
            IlluminaV1Bool = false;
            IlluminaV2Bool = false;
            offset = 33;
        }

        private void SolexaIllumina_Checked(object sender, RoutedEventArgs e)
        {
            trimmername = "Solexa/Illumina";
            CustomOffset.IsReadOnly = true;
            CustomOffset.Background = Brushes.SlateGray;
            CustomOffset.BorderBrush = Brushes.SlateGray;
            SangerBool = false;
            SolexaBool = true;
            IlluminaV1Bool = false;
            IlluminaV2Bool = false;
            offset = 59;
        }

        private void IlluminaV1_Checked(object sender, RoutedEventArgs e)
        {
            trimmername = "Illumina 1.3-1.5";
            CustomOffset.IsReadOnly = true;
            CustomOffset.Background = Brushes.SlateGray;
            CustomOffset.BorderBrush = Brushes.SlateGray;
            SangerBool = false;
            SolexaBool = false;
            IlluminaV1Bool = true;
            IlluminaV2Bool = false;
            offset = 64;
        }

        private void IlluminaV2_Checked(object sender, RoutedEventArgs e)
        {
            trimmername = "illumina 1.5-1.8";
            CustomOffset.IsReadOnly = true;
            CustomOffset.Background = Brushes.SlateGray;
            CustomOffset.BorderBrush = Brushes.SlateGray;
            SangerBool = false;
            SolexaBool = false;
            IlluminaV1Bool = false;
            IlluminaV2Bool = true;
            offset = 0;
        }

        private void Custom_Checked(object sender, RoutedEventArgs e)
        {
            trimmername = " Custom";
            CustomOffset.IsReadOnly = false;
            CustomOffset.Background = Brushes.White;
            CustomOffset.BorderBrush = Brushes.White;
            SangerBool = false;
            SolexaBool = false;
            IlluminaV1Bool = false;
            IlluminaV2Bool = false;
            CustomBool = true;
        }

        private void Percentage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InRange(((TextBox)sender).Text + e.Text);
        }

        private void WSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InRange(((TextBox)sender).Text + e.Text);
        }

        private void WQuality_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InRange(((TextBox)sender).Text + e.Text);
        }
        private void MaxWinFailBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InRange(((TextBox)sender).Text + e.Text);
        }
        private void CustomOffset_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InRange(((TextBox)sender).Text + e.Text);
        }
        
        public bool InRange(string str)
        {
            double i;
            return double.TryParse(str, out i) && i > 0 && i < 101;
        }
    }
}
