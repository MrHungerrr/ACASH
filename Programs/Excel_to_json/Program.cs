using System;
using System.IO;

namespace Excel_to_json
{
    class Program
    {
        static string filesPath = "D:/GameDev/Unity/ACASH/ACASH/Assets/Resources/";
        static string excelPath = "D:/GameDev/Unity/ACASH/Docs/";
        static string audioPath = "D:/GameDev/Unity/ACASH/ACASH/FMOD/Assets/";
        static string[] nameOfFiles = new string[2] { "Script", "AudioDuration"};
        static int[] quantOfSheets = new int[2] { 5, 1 };

        static void Main(string[] args)
        {

            AudioDuration();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excel to JSON is BEGIN!\n\n");
            Console.ResetColor();


            for (byte c = 0; c < 2; c++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("File {0} is BEGIN\n", nameOfFiles[c] + ".txt");
                Console.ResetColor();

                int j = 1;
                int i = 1;
                Excel[] excel = new Excel[quantOfSheets[c]];
                int[] nSentence = new int[quantOfSheets[c]];

                for (byte s = 1; s < (quantOfSheets[c] + 1); s++)
                {
                    excel[s - 1] = new Excel(excelPath + nameOfFiles[c] + ".xlsx", s);
                    while (excel[s - 1].ReadCell(i, 1) != "")
                    {
                        i++;
                    }

                    nSentence[s - 1] = i;
                    Console.WriteLine("    Shit {0} containts {1} cells", s, i);
                    i = 2;
                }

                Console.WriteLine();

                while (excel[0].ReadCell(0, j) != "")
                {
                    j++;
                }

                int nLang = j;

                for (j = 2; j < nLang; j++)
                {
                    string language = excel[0].ReadCell(0, j);
                    try
                    {
                        File.Create(filesPath + "Subtitles/" + nameOfFiles[c] + "." + language + ".txt").Close();
                        TextWriter file = new StreamWriter(filesPath + "Subtitles/" + nameOfFiles[c] + "." + language + ".txt", true);
                        file.WriteLine("{");
                        file.WriteLine("    \"lines\":");
                        file.WriteLine("    [");

                        for (byte s = 0; s < quantOfSheets[c]; s++)
                        {
                            for (i = 2; i < nSentence[s]; i++)
                            {
                                if (excel[s].ReadCell(i, j) != "")
                                {
                                    file.WriteLine("        {");
                                    file.WriteLine("            \"key\":\"{0}\",", excel[s].ReadCell(i, 1));
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


                            //Костыль на заполнение Слов для словаря
                        }

                        file.WriteLine("    ]");
                        file.WriteLine("}");
                        file.Close();

                        Console.WriteLine("    File {0} Complete", nameOfFiles[c] + "." + language + ".txt");
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }

                if (c == 0)
                {
                    DictionaryWords(nSentence[3]);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("File {0} is DONE\n\n", nameOfFiles[c]);
                Console.ResetColor();

                for (byte s = 0; s < (quantOfSheets[c]); s++)
                {
                    excel[s].Close();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excel to JSON is DONE!");
            Console.ResetColor();
            Console.ReadKey(true);
        }




        static private void DictionaryWords(int nSentence)
        {
           var excel = new Excel(excelPath + nameOfFiles[0] + ".xlsx", 4);

            try
            {
                File.Create(filesPath + "Dictionary/Words.txt").Close();
                TextWriter file = new StreamWriter(filesPath + "Dictionary/Words.txt", true);

                for (int i = 2; i < nSentence; i++)
                {
                    file.WriteLine(excel.ReadCell(i, 3));
                }

                file.Close();
                Console.WriteLine("    File Words.txt Complete");
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            excel.Close();
        }



        static private void AudioDuration()
        {

            Console.WriteLine("AudioDuration Begin");
            Console.WriteLine();

            Excel excelAudio = new Excel(excelPath + nameOfFiles[1] + ".xlsx", 1);
            Excel excelScript = new Excel(excelPath + nameOfFiles[0] + ".xlsx", 1);
            int i = 1;
            int j = 1;


            //Количество фраз в файле с фразами
            while (excelScript.ReadCell(i, 1) != "")
            {
                i++;
            }
            int nSentence = i;


            //Количество языков озвучки в файле с длительностью озвучки
            while (excelAudio.ReadCell(0, j) != "")
            {
                j++;
            }

            int nLang = j;


            try
            {
                for (i = 2; i < nSentence; i++)
                {
                    string KeyWord = excelScript.ReadCell(i, 1);

                    if (excelAudio.ReadCell(i, 1) == "" || (excelScript.ReadCell(i, 1) != excelAudio.ReadCell(i, 1)))
                    {
                        excelAudio.WriteCell(i, 1, excelScript.ReadCell(i, 1));

                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e);
            }


            try
            {
                for (j = 2; j < nLang; j++)
                {
                    string language = excelAudio.ReadCell(0, j);

                    for (i = 2; i < nSentence; i++)
                    {
                        if (excelAudio.ReadCell(i, j) == "" || (excelScript.QuantOfSentences(i, j) != excelAudio.QuantOfSentences(i, j)))
                        {
                            double duration = 0;
                            string nameFile = audioPath + language + "/" + excelAudio.ReadCell(i, 1);

                            if (File.Exists(nameFile + ".wav"))
                            {
                                NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(nameFile + ".wav");
                                duration = wf.TotalTime.TotalSeconds;

                                int n = excelScript.QuantOfSentences(i, 1);
                                duration = duration / n;
                                string cell = "";
                                string durStr = string.Format("{0:N2}", duration);

                                for (int c = 0; c < n - 1; c++)
                                {
                                    cell += durStr + ';';
                                }
                                cell += durStr;

                                excelAudio.WriteCell(i, j, cell);
                            }
                            else if (File.Exists(nameFile + ".mp3"))
                            {
                                NAudio.Wave.Mp3FileReader mp3f = new NAudio.Wave.Mp3FileReader(nameFile + ".mp3");
                                duration = mp3f.TotalTime.TotalSeconds;

                                int n = excelScript.QuantOfSentences(i, 2);
                                duration = duration / n;
                                string cell = "";
                                string durStr = string.Format("{0:N1}", duration);

                                for (int c = 0; c < n - 1; c++)
                                {
                                    cell += durStr + ';';
                                }
                                cell += durStr;

                                excelAudio.WriteCell(i, j, cell);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("    File {0} was not found", nameFile);
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            Console.WriteLine();
            Console.WriteLine("AudioDuration Complete\n\n");

            excelAudio.Save();
            excelAudio.Close();
            excelScript.Close();
        }

    }
}
