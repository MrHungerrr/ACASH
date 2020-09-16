using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using System.Drawing;
using GOAP;


namespace Application
{
    public static class GOAPInspector
    {

        public static void Test1()
        {

        }

        public static void Check()
        {
            GOAPConsoleWriter.WriteBlanks();
            GOAPConsoleWriter.WriteActions();
            GOAPConsoleWriter.WriteBlanksUsingBy();
            CheckForNotUsing();
            CheckActionsForEmpty();
        }

        private static void CheckActionsForEmpty()
        {
            bool needToWrite = false;

            var table = new ConsoleTable("Empty Effect", "Empty Cost", "Empty Precondition");

            foreach (var action in GOAPActionsManager.Instance.Actions)
            {
                bool effectEmpty = false;
                bool costEmpty = false;
                bool preconditionEmpty = false;

                if (action.Effect.IsEmpty)
                    effectEmpty = true;

                if (action.Cost == default)
                    costEmpty = true;

                if (action.Preconditions.IsEmpty)
                    preconditionEmpty = true;

                if(effectEmpty || costEmpty || preconditionEmpty)
                {
                    needToWrite = true;
                    table.AddRow(effectEmpty ? action.Name : "", costEmpty ? action.Name : "", preconditionEmpty ? action.Name : "");
                }
            }

            if (needToWrite)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n  EMPTINES DETECTED!\n");
                Console.ResetColor();
                table.Write();
            }
        }


        private static void CheckForNotUsing()
        {

            var table = new ConsoleTable("Blank Key", "Value Type");
            bool needToWrite = false;

            foreach (var blank in GOAPBlanksManager.Instance.Blanks)
            {

                bool isNotUsing = true;

                foreach (var action in GOAPActionsManager.Instance.Actions)
                {
                    if (action.Effect.Contains(blank.Key))
                    {
                        isNotUsing = false;
                        break;
                    }

                    if (action.Preconditions.Contains(blank.Key))
                    {
                        isNotUsing = false;
                        break;
                    }
                }



                if (isNotUsing)
                {
                    needToWrite = true;
                    table.AddRow(blank.Key, blank.Value);
                }
            }

            if (needToWrite)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n  UNUSED BLANKS DETECTED!\n");
                Console.ResetColor();
                table.Write();
            }
        }

        public static void SaveLoad()
        {

        }
    }
}
