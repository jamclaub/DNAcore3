using System;
using System.Collections.Generic;
using System.Text;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.IO;
using System.Windows;


namespace DNA.NETCORE3._0
{
    class Previewer
    {
        private int winsize = 500;
        private StreamReader s;
        //private string direct;
        private char[][] sequ;
        private char[][] qual;

        private string[][] sizes;
        private string[][] seqnames = new string[8192][];
        string temp;
        Filemanager f;
        public List<int> avgs = new List<int>();
        public ChartValues<ObservablePoint> ValA { get; set; }
        public ChartValues<ObservablePoint> ValB { get; set; }
        public ChartValues<ObservablePoint> ValC { get; set; }
        public ChartValues<ObservablePoint> ValD { get; set; }
        public int offset = 0;

        public Previewer(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> B, ChartValues<ObservablePoint> C, ChartValues<ObservablePoint> D, int Offset)
        {
            ValA = A;
            ValB = B;
            ValC = C;
            ValD = D;
            offset = Offset;
        }
        public void fileselector()
        {
            // allows you to select a file to be previewed
            f = new Filemanager();
            s = f.fileselectordialg();
        }


        public void runRandomSampler()
        {
            // select a file and runs the sample
            fileselector();
            randomSampler(ValA, ValC);
        }
        public void runRandomSamplerTwoFiles()
        {
            // select two files and runs them
            fileselector();
            randomSampler(ValA, ValC);
            fileselector();
            randomSampler(ValB, ValD);
        }

        public void randomSampler(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> C)
        {
            int y = 0;
            int p = 0;

            for (int x = 0; x < f.z.Count; x++)
            {
                p = x;
                while (y < f.z[x] - y)
                {
                    // reads four lines from the file 
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    y++;
                }
                s.ReadLine();
                s.ReadLine();
                s.ReadLine();
                int z = 0;
                int c = s.Read();
                while (c != Convert.ToInt32('\n'))
                {

                    
                    if (avgs.Count <= z)
                    {
                        // adds the value if it meets the condition
                        avgs.Add(Convert.ToInt32(c) - offset);
                    }
                    else
                    {
                        avgs[z] = avgs[z] + Convert.ToInt32(c) - offset;
                    }

                    if (p % 10000 == 0)
                    {
                        // adds to the chart (limits the number of values in the charts so it does not get bogged down.
                        A.Add(new ObservablePoint(z + 1, Convert.ToInt32(c) - offset));
                    }

                    z++;

                    c = s.Read();
                }
                y++;
            }
            for (int i = 0; i < avgs.Count; i++)
            {
                // adds the averages to the bottom chart.
                avgs[i] = avgs[i] / f.z.Count;
                C.Add(new ObservablePoint(i + 1, avgs[i]));
                
            }
            // places a message in the statusbox that the previewer is done
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PreviewWindow))
                    {
                        (window as PreviewWindow).StatusBox.Text = (window as PreviewWindow).StatusBox.Text + "\nPreviewer complete\n";

                    }
                }
        }

        public char[][] fileopener()
        {

            for (int i = 0; i < winsize; i++)
            {   // read and store k reads into arrays

                sizes[i] = new string[512];
                seqnames[i] = new string[512];

                //read name line
                temp = s.ReadLine();

                //split name line to find length of sequence
                sizes[i] = temp.Split('=');

                //split name line to find name of sequence
                seqnames[i] = temp.Split(' ');

                //make int x equal the length of sequence
                int x = Convert.ToInt32(sizes[i][1]);

                //initialize sequence
                sequ[i] = new char[x];

                //initualize quality
                qual[i] = new char[x];

                //read sequence
                //s.Read(sequ[i], 0, x);

                //read rest of line
                s.ReadLine();

                //read rest of line
                s.ReadLine();

                //read quality
                s.Read(qual[i], 0, x);

                //read rest of line
                s.ReadLine();
            }
            return qual;
        }
    }
}