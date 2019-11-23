﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DNA.NETCORE3._0
{
    class Trimmer
    {

        private int window = 20;
        private int minwin = 10;
        private int minqual = 35;
        private int blocksize = 500;
        private int skew = 33;
        private int failedwindows = 2;
        private StreamReader one;
        private StreamReader two;
        private Filemanager f;
        private Filemanager x;
        private Filemanager y;
        string directa;
        string directb;

        private StreamWriter onea;
        private StreamWriter twoa;

        private List<string> titleline = new List<string>();
        private List<string> SequenceLine = new List<string>();
        private List<string> QualityLine = new List<string>();

        private List<string> titleline2 = new List<string>();
        private List<string> SequenceLine2 = new List<string>();
        private List<string> QualityLine2 = new List<string>();

        public Trimmer(int Quality, int WindowSize, int WindowQuality, int MaxWindowFail, int Offset)
        {
            minqual = Quality;
            window = WindowSize;
            minwin = WindowQuality;
            failedwindows = MaxWindowFail;
            skew = Offset;
        }

        public void SettingsStarter(int x, int y, int z)
        {
            window = x;
            minwin = y;
            minqual = z;
        }

        public void singlefile()
        {

            f = new Filemanager();
            one = f.trimmerselector();
            directa = f.directgetter();
            onea = new StreamWriter(directa);
            while (!one.EndOfStream)
            {
                blockreader(titleline, SequenceLine, QualityLine, one);
                TrimmerOneFile();
                filesaver(titleline, SequenceLine, QualityLine, onea);
            }
            onea.Close();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PreviewWindow))
                {
                    (window as PreviewWindow).StatusBox.Text = (window as PreviewWindow).StatusBox.Text + "\nTrimmer complete\n";

                }
            }


        }

        public void twofile()
        {

            x = new Filemanager();
            y = new Filemanager();

            one = x.trimmerselector();
            two = y.trimmerselector();

            directa = x.directgetter();
            directb = y.directgetter();
            onea = new StreamWriter(directa);
            twoa = new StreamWriter(directb);

            while (!one.EndOfStream && !two.EndOfStream)
            {
                blockreader(titleline, SequenceLine, QualityLine, one);
                blockreader(titleline2, SequenceLine2, QualityLine2, two);
                TrimmerTwoFIle();
                filesaver(titleline, SequenceLine, QualityLine, onea);
                filesaver(titleline2, SequenceLine2, QualityLine2, twoa);
            }

        }

        public void blockreader(List<string> title, List<string> seq, List<string> qual, StreamReader a)
        {
            for (int k = 0; k < blocksize; k++)
            {
                title.Add(a.ReadLine());
                seq.Add(a.ReadLine());
                a.ReadLine();
                qual.Add(a.ReadLine());
            }
        }

        public void filesaver(List<string> title, List<string> seq, List<string> qual, StreamWriter b)
        {
            while (title.Count > 0 && title[0] != null)
            {
                b.WriteLine(title[0]);
                title.RemoveAt(0);
                b.WriteLine(seq[0]);
                seq.RemoveAt(0);
                b.WriteLine("+");
                b.WriteLine(qual[0]);
                qual.RemoveAt(0);
            }
        }

        public void TrimmerOneFile()
        {
            char[] set = null;
            char[] qul = null;
            int z = 0;
            int average = 0;
            int windowaverage = 0;
            int windowcount = 0;

            for (int x = 0; x < titleline.Count; x++)
            {
                if (SequenceLine[z] != null)
                {

                    set = SequenceLine[z].ToCharArray();
                    qul = QualityLine[z].ToCharArray();
                    Boolean acceptwindows = true;
                    Boolean acceptaverage = true;
                    for (int y = 0; y < set.Length; y++)
                    {

                        windowaverage = windowaverage + (Convert.ToInt32(qul[y]) - skew);
                        average = average + (Convert.ToInt32(qul[y]) - skew);
                        if ((Convert.ToInt16(qul[y]) - skew) < minqual)
                        {
                            qul[y] = '!';
                            set[y] = 'W';
                        }


                        if (y != 0 && (y % window) == 0)
                        {
                            //average = windowaverage;
                            windowaverage = windowaverage / window;

                            if (windowaverage < minwin)
                            {
                                titleline.RemoveAt(z);
                                SequenceLine.RemoveAt(z);
                                QualityLine.RemoveAt(z);
                                //z = z - 1;
                                windowcount++;
                                if (windowcount >= failedwindows)
                                {
                                    acceptwindows = false;
                                    break;
                                }

                            }

                        }




                    }

                    average = average / set.Length;
                    if (average < minqual && acceptwindows == true)
                    {
                        titleline.RemoveAt(z);
                        SequenceLine.RemoveAt(z);
                        QualityLine.RemoveAt(z);
                        //z = z - 1;
                        acceptaverage = false;
                        //break;
                    }


                    if (acceptwindows == true && acceptaverage == true)
                    {
                        string t = new String(qul);
                        string u = new String(set);
                        SequenceLine[z] = u;
                        QualityLine[z] = t;
                        z++;
                    }
                    qul = null;
                    set = null;
                }

            }


        }

        public void TrimmerTwoFIle()
        {
            char[] set = null;
            char[] set1 = null;
            char[] qul = null;
            char[] qul1 = null;
            int z = 0;
            int average = 0;
            int windowaverage = 0;
            int average2 = 0;
            int windowaverage2 = 0;
            if (titleline.Count < titleline2.Count)
            {
                for (int x = 0; x < titleline.Count; x++)
                {
                    if (SequenceLine[z] != null && SequenceLine2[z] != null)
                    {

                        set = SequenceLine[z].ToCharArray();
                        qul = QualityLine[z].ToCharArray();

                        set1 = SequenceLine[z].ToCharArray();
                        qul1 = QualityLine2[z].ToCharArray();

                        Boolean acceptwindows = true;
                        Boolean acceptaverage = true;
                        for (int y = 0; y < set.Length; y++)
                        {

                            windowaverage = windowaverage + (Convert.ToInt32(qul[y]) - 33);

                            if ((Convert.ToInt32(qul[y]) - 33) < minqual)
                            {
                                qul[y] = '!';
                                set[y] = 'n';
                            }


                            if ((Convert.ToInt32(qul1[y]) - 33) < minqual)
                            {
                                qul1[y] = '!';
                                set1[y] = 'n';
                            }


                            if (y != 0 && (window % y) == 0)
                            {
                                average = windowaverage;
                                windowaverage = windowaverage / window;

                                if (windowaverage < minwin)
                                {
                                    titleline.RemoveAt(z);
                                    SequenceLine.RemoveAt(z);
                                    QualityLine.RemoveAt(z);
                                    //z = z - 1;
                                    acceptwindows = false;
                                    break;
                                }

                            }



                        }
                        average = average / set.Length;
                        if (average < minqual)
                        {
                            titleline.RemoveAt(z);
                            SequenceLine.RemoveAt(z);
                            QualityLine.RemoveAt(z);
                            //z = z - 1;
                            acceptaverage = false;
                        }

                        if (acceptwindows == true && acceptaverage == true)
                        {
                            string t = Convert.ToString(qul);
                            string u = Convert.ToString(set);
                            SequenceLine[z] = u;
                            QualityLine[z] = t;
                            z++;
                        }
                    }
                }


            }


        }
    }
}
