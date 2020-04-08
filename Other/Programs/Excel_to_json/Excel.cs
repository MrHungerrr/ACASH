using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Excel_to_json
{
    class Excel
    {
        private string path = "";
        public int count;
        private int sheet = -1;
        private int number;
        private _Application excel = new _Excel.Application();
        private Workbook wb;
        private Worksheet ws;


        public Excel(string path)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            count = wb.Worksheets.Count;
        }

        public void SetSheet(int sheet)
        {
            if (sheet < count && sheet >= 0)
            {
                this.sheet = sheet + 1;
                ws = wb.Worksheets[this.sheet];
            }
            else
            {
                this.sheet = -1;
            }
        }


        public string ReadCellText(int i, int j)
        {
            if (sheet != -1)
                return ReadCellText(i, j, ws);
            else
                return "";
        }

        public string ReadCellText(int i, int j, int sheet)
        {
            if (sheet < count && sheet >= 0)
            {
                sheet++;
                Worksheet ws = wb.Worksheets[sheet];

                return ReadCellText(i, j, ws);
            }
            else
            {
                return "";
            }
        }



        private string ReadCellText(int i, int j, Worksheet sheet)
        {
            i++;
            j++;

            if (sheet.Cells[i, j].Value2 != null)
            {

                string buf = sheet.Cells[i, j].Value2.ToString();

                buf = buf.Replace("\n", String.Empty);
                buf = buf.Replace("\r", String.Empty);

                string res = "";
                bool first_char = true;

                for (int c = 0; c < buf.Length; c++)
                {
                    if (buf[c] != ';')
                    {
                        if (!first_char)
                            res += JSONSymbol(buf[c]);
                        else if (buf[c] != ' ')
                        {
                            first_char = false;
                            char.ToUpper(buf[c]);
                            res += JSONSymbol(buf[c]);
                        }
                    }
                    else
                    {
                        if (c != (buf.Length - 1))
                        {
                            first_char = true;
                            res += "\",\"";
                        }
                    }
                }

                return res;
            }
            else
                return "";
        }



        public string ReadCellNumber(int i, int j)
        {
            if (sheet != -1)
                return ReadCellNumber(i, j, ws);
            else
                return "";
        }

        public string ReadCellNumber(int i, int j, int sheet)
        {
            if (sheet < count && sheet >= 0)
            {
                sheet++;
                Worksheet ws = wb.Worksheets[sheet];

                return ReadCellNumber(i, j, ws);
            }
            else
            {
                return "";
            }
        }


        private string ReadCellNumber(int i, int j, Worksheet ws)
        {
            i++;
            j++;

            if (ws.Cells[i, j].Value2 != null)
            {
                string buf = ws.Cells[i, j].Value2.ToString();

                buf = buf.Replace("\n", String.Empty);
                buf = buf.Replace("\r", String.Empty);
                buf = buf.Replace(" ", String.Empty);

                string res = "";
                string buf_res = "";
                double number = 0;
                double previous_number = 0;
                bool first_number = true;

                for (int c = 0; c < buf.Length; c++)
                {
                    if (buf[c] != ';')
                    {
                        if(!first_number)
                            buf_res += buf[c];
                    }

                    if (buf[c] == ';' || c == buf.Length - 1)
                    {
                        if (!first_number)
                        {
                            number = double.Parse(buf_res);
                            previous_number = number - previous_number;

                            previous_number = Math.Round(previous_number, 2);

                            buf_res = previous_number.ToString();
                            
                            res += buf_res;

                            previous_number = number;
                            buf_res = "";

                            if (c != (buf.Length - 1))
                                res += ',';
                        }
                        else
                        {
                            first_number = false;
                        }

                    }
                }
                return res;
            }
            else
                return "";
        }



        private string JSONSymbol(char symbol)
        {
            switch(symbol)
            {
                case '"':
                    return "\\\"";
                case '\'':
                    return "\\\\";
                case 'ё':
                    return "е";
                case 'Ё':
                    return "Е";
                default:
                    return symbol.ToString();
            }
        }



        public void WriteCell(int i, int j, string s)
        {
            if (sheet != -1)
                WriteCell(i, j, ws, s);
        }

        public void WriteCell(int i, int j, int sheet, string s)
        {
            if (sheet < count && sheet >= 0)
            {
                sheet++;
                Worksheet ws = wb.Worksheets[sheet];

                WriteCell(i, j, ws, s);
            }
        }

        private void WriteCell(int i, int j, Worksheet ws, string s)
        {
            i++;
            j++;

            ws.Cells[i, j].Value2 = s;
        }



        public int QuantOfSentences(int i, int j)
        {
            if (sheet != -1)
                return QuantOfSentences(i, j, ws);
            else
                return 0;

        }

        public int QuantOfSentences(int i, int j, int sheet)
        {
            if (sheet < count && sheet >= 0)
            {
                sheet++;
                Worksheet ws = wb.Worksheets[sheet];

                return QuantOfSentences(i, j, ws);
            }
            else
            {
                return 0;
            }

        }

        private int QuantOfSentences(int i, int j, Worksheet ws)
        {
            i++;
            j++;

            if (ws.Cells[i, j].Value2 != null)
            {
                string buf = ws.Cells[i, j].Value2.ToString();
                int quant = 1;
                for (int c = 0; c < buf.Length - 1; c++)
                {
                    if (buf[c] == ';')
                    {
                        quant++;
                    }
                }
                return quant;
            }
            else
                return 0;
        }


        public void Save()
        {
            wb.Save();
        }

        public void Close()
        {
            wb.Close();
        }
    }
}
