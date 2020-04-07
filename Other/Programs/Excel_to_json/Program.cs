using System;
using System.IO;

namespace Excel_to_json
{
    class Program
    {
        static string filesPath = "D:/GameDev/Unity/ACASH/ACASH/Assets/Resources/";
        static string excelPath = "D:/GameDev/Unity/ACASH/Docs/";
        static string audioPath = "D:/GameDev/Unity/ACASH/ACASH/FMOD/Assets/Voice/";
        static string[] nameOfFiles = new string[2] { "Script", "AudioDuration"};
        static int[] quantOfSheets = new int[2] { 8, 2 };

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
                    while (excel[s - 1].ReadCellText(i, 1) != "")
                    {
                        i++;
                    }

                    nSentence[s - 1] = i;
                    Console.WriteLine("    Sheet {0} containts {1} cells", s, i);
                    i = 2;
                }

                Console.WriteLine();

                while (excel[0].ReadCellText(0, j) != "")
                {
                    j++;
                }

                int nLang = j;

                for (j = 2; j < nLang; j++)
                {
                    string language = excel[0].ReadCellText(0, j);
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
                                if (excel[s].ReadCellText(i, j) != "")
                                {
                                    file.WriteLine("        {");
                                    file.WriteLine("            \"key\":\"{0}\",", excel[s].ReadCellText(i, 1));

                                    if(c == 0)
                                        file.Write("            \"line\":[\"{0}\"]", excel[s].ReadCellText(i, j));
                                    else
                                        file.Write("            \"duration\":[{0}]", excel[s].ReadCellNumber(i, j));

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
                    DictionaryWords(nSentence[3], 4);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("File {0}.txt is DONE\n\n", nameOfFiles[c]);
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




        static private void DictionaryWords(int nSentence, int nSheet)
        {
           var excel = new Excel(excelPath + nameOfFiles[0] + ".xlsx", nSheet);

            try
            {
                File.Create(filesPath + "Dictionary/Words.txt").Close();
                TextWriter file = new StreamWriter(filesPath + "Dictionary/Words.txt", true);

                for (int i = 2; i < nSentence; i++)
                {
                    file.WriteLine(excel.ReadCellText(i, 3));
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

            Excel[] excelAudio = new Excel[quantOfSheets[1]];
            Excel[] excelScript = new Excel[quantOfSheets[1]];
            int[] nSentence = new int[quantOfSheets[1]];

            int i = 2;

            for (byte s = 1; s < (quantOfSheets[1] + 1); s++)
            {
                excelAudio[s - 1] = new Excel(excelPath + nameOfFiles[1] + ".xlsx", s);
                excelScript[s - 1] = new Excel(excelPath + nameOfFiles[0] + ".xlsx", s);

                while (excelScript[s - 1].ReadCellText(i, 1) != "")
                {
                    i++;
                }

                nSentence[s - 1] = i;
            }


            i = 2;

            while (excelAudio[0].ReadCellText(0, i) != "")
            {
                i++;
            }

            int nLang = i;

            //Количество языков озвучки в файле с длительностью озвучки


            try
            {
                for (byte s = 0; s < (quantOfSheets[1]); s++)
                {
                    for (i = 2; i < nSentence[s]; i++)
                    {
                        string KeyWord = excelScript[s].ReadCellText(i, 1);

                        if (excelAudio[s].ReadCellText(i, 1) == "" || KeyWord != excelAudio[s].ReadCellText(i, 1))
                        {
                            excelAudio[s].WriteCell(i, 1, KeyWord);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }


            try
            {
                for (int j = 2; j < nLang; j++)
                {
                    string language = excelAudio[0].ReadCellText(0, j);

                    File.Create(excelPath + "Info/Missing Voice." + language + ".txt").Close();
                    TextWriter file = new StreamWriter(excelPath + "Info/Missing Voice." + language + ".txt", true);


                    for (byte s = 0; s < (quantOfSheets[1]); s++)
                    {
                        for (i = 2; i < nSentence[s]; i++)
                        {

                            int n = excelScript[s].QuantOfSentences(i, j);

                            if (n != 0)
                            {

                                string nameFile = audioPath + language + "/" + excelAudio[s].ReadCellText(i, 1);

                                if (excelAudio[s].ReadCellText(i, j) == "" || (n + 1) != excelAudio[s].QuantOfSentences(i, j) || !AudioIsExist(nameFile))
                                {

                                    double duration = 0;
                                    string cell = "";

                                    if (File.Exists(nameFile + ".wav"))
                                    {
                                        NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(nameFile + ".wav");
                                        duration = wf.TotalTime.TotalSeconds;
                                        cell += string.Format("{0:N1}", duration) + ';';
                                        duration /= n;

                                        string durStr = string.Format("{0:N1}", duration);

                                        for (int c = 0; c < n - 1; c++)
                                        {
                                            cell += durStr + ';';
                                        }
                                        cell += durStr;

                                        excelAudio[s].WriteCell(i, j, cell);
                                    }
                                    else if (File.Exists(nameFile + ".mp3"))
                                    {
                                        NAudio.Wave.Mp3FileReader mp3f = new NAudio.Wave.Mp3FileReader(nameFile + ".mp3");
                                        duration = mp3f.TotalTime.TotalSeconds;
                                        cell += string.Format("{0:N1}", duration) + ';';
                                        duration /= n;

                                        string durStr = string.Format("{0:N1}", duration);

                                        for (int c = 0; c < n - 1; c++)
                                        {
                                            cell += durStr + ';';
                                        }
                                        cell += durStr;

                                        excelAudio[s].WriteCell(i, j, cell);
                                    }
                                    else
                                    {
                                        duration = 2f;

                                        string durStr = string.Format("{0:N1}", duration);

                                        for (int c = 0; c < n - 1; c++)
                                        {
                                            cell += durStr + ';';
                                        }
                                        cell += durStr;

                                        excelAudio[s].WriteCell(i, j, cell);

                                        file.WriteLine(nameFile);

                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("    File {0} was not found", excelAudio[s].ReadCellText(i, 1));
                                        Console.ResetColor();
                                    }
                                }
                            }
                        }


                    }


                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }


            for (byte s = 0; s < (quantOfSheets[1]); s++)
            {
                excelAudio[s].Save();
                excelAudio[s].Close();
                excelScript[s].Close();
            }


            Console.WriteLine();
            Console.WriteLine("AudioDuration Complete\n\n");
        }




        static private bool AudioIsExist(string nameFile)
        {
            if(File.Exists(nameFile + ".mp3"))
            {
                return true;
            }


            if (File.Exists(nameFile + ".wav"))
            {
                return true;
            }

            return false;
        }


    }
}
