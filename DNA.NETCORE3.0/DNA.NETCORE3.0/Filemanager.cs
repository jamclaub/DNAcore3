using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DNA.NETCORE3._0
{
    class Filemanager
    {
        public List<int> z = new List<int>();
        string direct = null;
        //public int si;

        public StreamReader trimmerselector()
        {
            StreamReader fs;

            //Opens file selection dialog, inputs it into string Direct, and opens fs streamreader with direct
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Fastq Files (*.Fastq)|*.Fastq";


           
            System.Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                direct = dlg.FileName;
            }
            fs = new StreamReader(direct);

            return fs;
        }

        public StreamReader fileselectordialg()
        {

            StreamReader fl;
            Byte[] first = new Byte[512];
            Byte[] last = new Byte[512];
            string[] temp = new string[8192];
            string[] fnum = new string[8192];
            

            string[] lnum = new string[8192];



            StreamReader fs;
            //string direct = null;
            //Opens file selection dialog, inputs it into string Direct, and opens fs streamreader with direct
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Fastq Files (*.Fastq)|*.Fastq";

            System.Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                direct = dlg.FileName;
            }
            int counter = 0;
            fl = new StreamReader(direct);
            //counts number of reads
            while (!fl.EndOfStream)
            {
                fl.ReadLine();
                fl.ReadLine();
                fl.ReadLine();
                fl.ReadLine();
                counter++;

            }
           
            Random y = new Random();
            int q;
            //generates random numbers between 1 and total read number across 5% of read number
            for (int i = 0; i < .05 * counter; i++)
            {



                q = y.Next(1, counter);
                //Predicate<int> t = q;
                if (z.IndexOf(q) == -1)
                {
                    z.Add(q);
                }

            }

            fs = new StreamReader(direct);
            z.Sort();

            return fs;

        }

        public string directgetter()
        {
            //returns directory
            string direct2 = direct.Substring(0, (direct.Length) - 5);
            direct2 = direct2 + "_trimmed.fastq";
            return direct2;
        }
    }
}
