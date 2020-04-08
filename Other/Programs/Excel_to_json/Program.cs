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

        static void Main(string[] args)
        {

            AudioDuration();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excel to JSON is BEGIN!\n\n");
            Console.ResetColor();


            for (byte c = 0; c < 2; c++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("    File {0} is BEGIN\n", nameOfFiles[c] + ".txt");
                Console.ResetColor();

                int j = 1;
                int i = 1;
                Excel excel = new Excel(excelPath + nameOfFiles[c] + ".xlsx");
                int[] nSentence = new int[excel.count];

                for (byte s = 0; s < excel.count; s++)
                {
                    while (excel.ReadCellText(i, 1, s) != "")
                    {
                        i++;
                    }

                    nSentence[s] = i;
                    Console.WriteLine("        Sheet {0} containts {1} cells", s, i);
                    i = 2;
                }

                Console.WriteLine();

                while (excel.ReadCellText(0, j, 0) != "")
                {
                    j++;
                }

                int nLang = j;

                for (j = 2; j < nLang; j++)
                {
                    string language = excel.ReadCellText(0, j, 0);
                    try
                    {
                        File.Create(filesPath + "Subtitles/" + nameOfFiles[c] + "." + language + ".txt").Close();
                        TextWriter file = new StreamWriter(filesPath + "Subtitles/" + nameOfFiles[c] + "." + language + ".txt", true);
                        file.WriteLine("{");
                        file.WriteLine("    \"lines\":");
                        file.WriteLine("    [");

                        for (byte s = 0; s < excel.count; s++)
                        {
                            excel.SetSheet(s);

                            for (i = 2; i < nSentence[s]; i++)
                            {
                                if (excel.ReadCellText(i, j) != "")
                                {
                                    file.WriteLine("        {");
                                    file.WriteLine("            \"key\":\"{0}\",", excel.ReadCellText(i, 1));

                                    if (c == 0)
                                        file.Write("            \"line\":[\"{0}\"]", excel.ReadCellText(i, j));
                                    else
                                        file.Write("            \"duration\":[{0}]", excel.ReadCellNumber(i, j));

                                    file.WriteLine();
                                    file.Write("        }");
                                    if (((s + 1) == excel.count) && ((i + 1) == nSentence[s]))
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

                        Console.WriteLine("        File {0} Complete", nameOfFiles[c] + "." + language + ".txt");
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }

                if (c == 0)
                {
                    DictionaryWords(nSentence[3],excel, 3);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("    File {0}.txt is DONE\n\n", nameOfFiles[c]);
                Console.ResetColor();

                excel.Close();

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excel to JSON is DONE!");
            Console.ResetColor();
            Console.ReadKey(true);
        }




        static private void DictionaryWords(int nSentence,  Excel excel, int nSheet)
        {
            excel.SetSheet(nSheet);

            try
            {
                File.Create(filesPath + "Computer/Dictionary/Words.txt").Close();
                TextWriter file = new StreamWriter(filesPath + "Computer/Dictionary/Words.txt", true);

                for (int i = 2; i < nSentence; i++)
                {
                    file.WriteLine(excel.ReadCellText(i, 3));
                }

                file.Close();
                Console.WriteLine("        File Words.txt Complete");
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }




        static private void AudioDuration()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Audio Duration is BEGIN");
            Console.ResetColor();
            Console.WriteLine();

            Excel excelAudio = new Excel(excelPath + nameOfFiles[1] + ".xlsx");
            Excel excelScript = new Excel(excelPath + nameOfFiles[0] + ".xlsx");
            int[] nSentence = new int[excelAudio.count];

            int i = 2;

            for (byte s = 0; s < excelAudio.count; s++)
            {

                while (excelScript.ReadCellText(i, 1, s) != "")
                {
                    i++;
                }

                nSentence[s] = i;
            }


            i = 2;

            while (excelAudio.ReadCellText(0, i, 0) != "")
            {
                i++;
            }

            int nLang = i;

            //Количество языков озвучки в файле с длительностью озвучки


            try
            {
                for (byte s = 0; s < (excelAudio.count); s++)
                {
                    for (i = 2; i < nSentence[s]; i++)
                    {
                        string KeyWord = excelScript.ReadCellText(i, 1, s);

                        if (excelAudio.ReadCellText(i, 1, s) == "" || KeyWord != excelAudio.ReadCellText(i, 1, s))
                        {
                            excelAudio.WriteCell(i, 1, s, KeyWord);
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
                    string language = excelAudio.ReadCellText(0, j, 0);

                    File.Create(excelPath + "Info/Missing Voice." + language + ".txt").Close();
                    TextWriter file = new StreamWriter(excelPath + "Info/Missing Voice." + language + ".txt", true);


                    for (byte s = 0; s < (excelAudio.count); s++)
                    {

                        excelAudio.SetSheet(s);
                        excelScript.SetSheet(s);

                        for (i = 2; i < nSentence[s]; i++)
                        {

                            int n = excelScript.QuantOfSentences(i, j);

                            if (n != 0)
                            {

                                string name_file = excelAudio.ReadCellText(i, 1);
                                string path_to_file = audioPath + language + "/" + name_file;

                                if (excelAudio.ReadCellText(i, j) == "" || (n + 1) != excelAudio.QuantOfSentences(i, j, s) || !AudioIsExist(path_to_file))
                                {

                                    double duration = 0;
                                    string cell = "";

                                    if(AudioIsExist(path_to_file))
                                    {
                                        duration = GetDuration(path_to_file);
                                        duration = Math.Round(duration, 1);

                                        cell += duration.ToString() + ";\n";

                                        duration /= n;

                                        duration = Math.Round(duration, 1);

                                        for (int c = 1; c < n; c++)
                                        {
                                            cell += (duration * c).ToString() + ";\n";
                                        }
                                        cell += (duration * n).ToString();

                                        excelAudio.WriteCell(i, j, cell);

                                        file.WriteLine("Cell duration {0} has been changed", name_file);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        Console.WriteLine("    Cell duration {0} has been changed", name_file);
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        duration = 2f;

                                        for (int c = 1; c < n ; c++)
                                        {
                                            cell += (duration*c).ToString() + ";\n";
                                        }
                                        cell += (duration*n).ToString();

                                        excelAudio.WriteCell(i, j, cell);

                                        file.WriteLine("File {0} was not found\nSearch Location:{1}", name_file, path_to_file);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("    File {0} was not found", name_file);
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

            excelAudio.Save();

            excelScript.Close();
            excelAudio.Close();



            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Audio Duration is DONE\n\n");
            Console.ResetColor();

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



        static private double GetDuration(string nameFile)
        {

            if (File.Exists(nameFile + ".wav"))
            {
                NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(nameFile + ".wav");
                return wf.TotalTime.TotalSeconds;
            }

            if (File.Exists(nameFile + ".mp3"))
            {
                NAudio.Wave.Mp3FileReader mp3f = new NAudio.Wave.Mp3FileReader(nameFile + ".mp3");
                return mp3f.TotalTime.TotalSeconds;
            }

            return 0;
        }


    }
}
