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
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string ReadCellText(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
            {
                string buf = ws.Cells[i, j].Value2.ToString();
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
                res = res.Replace("\n", String.Empty);
                res = res.Replace("\r", String.Empty);
                return res;
            }
            else
                return "";
        }

        public string ReadCellNumber(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
            {
                string buf = ws.Cells[i, j].Value2.ToString();
                string res = "";
                bool first_number = true;

                for (int c = 0; c < buf.Length; c++)
                {
                    if (buf[c] != ';')
                    {
                        if(!first_number)
                            res += buf[c];
                    }
                    else
                    {
                        if (c != (buf.Length - 1) && !first_number)
                        {
                            res += ",";
                        }

                        first_number = false;
                    }
                }
                res = res.Replace("\n", String.Empty);
                res = res.Replace("\r", String.Empty);
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
            i++;
            j++;
            ws.Cells[i, j].Value2 = s;
        }

        public int QuantOfSentences(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
            {
                string buf = ws.Cells[i, j].Value2.ToString();
                int quant = 1;
                for (int c = 0; c < buf.Length; c++)
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
