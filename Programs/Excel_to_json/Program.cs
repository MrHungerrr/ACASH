using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Excel_to_json
{
    class Program
    {
        static string filesPath = "D:/Creativity/GitHub/AKASH_git/AKASH/Assets/Resources/";
        static string excelPath = "D:/Programs/Excel_to_json/";
        static string audioPath = "D:/Creativity/GitHub/AKASH_git/AKASH/FMOD/Assets/";
        static string[] nameOfFiles = new string[2] { "Script.", "AudioDuration." };
        static int[] quantOfSheets = new int[2] { 3, 1 };

        static void Main(string[] args)
        {

            AudioDuration();

            for (byte c = 0; c < 2; c++)
            {
                Console.WriteLine("File {0} begin", nameOfFiles[c] + "txt");
                int j = 1;
                int i = 2;
                Excel[] excel = new Excel[quantOfSheets[c]];
                int[] nSentence = new int[quantOfSheets[c]];

                for (byte s = 1; s < (quantOfSheets[c] + 1); s++)
                {
                    excel[s - 1] = new Excel(excelPath + nameOfFiles[c] + "xlsx", s);
                    while (excel[s - 1].ReadCell(i, 0) != "")
                    {
                        i++;
                    }

                    nSentence[s - 1] = i;
                    Console.WriteLine("Shit {0} containts {1} cells", s, i);
                    i = 2;
                }

                while (excel[0].ReadCell(0, j) != "")
                {
                    j++;
                }

                int nLang = j;

                for (j = 1; j < nLang; j++)
                {
                    string language = excel[0].ReadCell(0, j);
                    try
                    {
                        File.Create(filesPath + nameOfFiles[c] + language + ".txt").Close();
                        TextWriter file = new StreamWriter(filesPath + nameOfFiles[c] + language + ".txt", true);
                        file.WriteLine("{");
                        file.WriteLine("    \"lines\":");
                        file.WriteLine("    [");

                        for (byte s = 0; s < quantOfSheets[c]; s++)
                        {
                            for (i = 2; i < nSentence[s]; i++)
                            {

                                file.WriteLine("        {");
                                file.WriteLine("            \"key\":\"{0}\",", excel[s].ReadCell(i, 0));
                                file.Write("            \"line\":[\"{0}\"]", excel[s].ReadCell(i, j));
                                file.WriteLine();
                                file.Write("        }");
                                if (((s + 1) == quantOfSheets[c]) && ((i + 1) == nSentence[s]))
                                    file.WriteLine("");
                                else
                                {
                                    file.WriteLine(",");
                                }
                            }
                        }

                        file.WriteLine("    ]");
                        file.WriteLine("}");
                        file.Close();

                        Console.WriteLine("File {0} Complete", nameOfFiles[c] + language + ".txt");
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }
                for (byte s = 0; s < (quantOfSheets[c]); s++)
                {
                    excel[s].Close();
                }
            }
        }

        static private void AudioDuration()
        {
            Excel excel = new Excel(excelPath + nameOfFiles[1] + "xlsx", 1);
            Excel excel2 = new Excel(excelPath + nameOfFiles[0] + "xlsx", 1);
            int i = 2;
            int j = 1;

            while (excel.ReadCell(i, 0) != "")
            {
                i++;
            }
            int nSentence = i;

            while (excel.ReadCell(0, j) != "")
            {
                j++;
            }
            int nLang = j;

            try
            {
                for (j = 1; j < nLang; j++)
                {
                    string language = excel.ReadCell(0, j);

                    for (i = 2; i < nSentence; i++)
                    {
                        double duration = 0;
                        string nameFile = audioPath + language + "/" + excel.ReadCell(i, 0);

                        if (excel.ReadCell(i, j) == "" || (excel2.QuantOfSentences(i, 1) != excel.QuantOfSentences(i, 1)))
                        {
                            if (File.Exists(nameFile + ".wav"))
                            {
                                NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(nameFile + ".wav");
                                duration = wf.TotalTime.TotalSeconds;

                                int n = excel2.QuantOfSentences(i, 1);
                                duration = duration / n;
                                string cell = "";
                                string durStr = string.Format("{0:N2}", duration);

                                for (int c = 0; c < n - 1; c++)
                                {
                                    cell += durStr + ';';
                                }
                                cell += durStr;

                                excel.WriteCell(i, 1, cell);
                            }
                            else if (File.Exists(nameFile + ".mp3"))
                            {
                                NAudio.Wave.Mp3FileReader mp3f = new NAudio.Wave.Mp3FileReader(nameFile + ".mp3");
                                duration = mp3f.TotalTime.TotalSeconds;

                                int n = excel2.QuantOfSentences(i, 2);
                                duration = duration / n;
                                string cell = "";
                                string durStr = string.Format("{0:N1}", duration);

                                for (int c = 0; c < n - 1; c++)
                                {
                                    cell += durStr + ';';
                                }
                                cell += durStr;

                                excel.WriteCell(i, 1, cell);
                            }
                            else
                            {
                                Console.WriteLine("File {0} was not found", nameFile);
                            }
                        }
                    }

                    Console.WriteLine("AudioDuration Complete");
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            excel.Save();
            excel.Close();
            excel2.Close();
        }

    }
}
