using System;
using UnityEngine;


namespace ComputerActions
{
    public static class GetC
    {



        public enum commands
        {
            //Кнопки связанные с отрытием новых окон
            Login,
            Desktop,
            Student_Stress,
            Overwatch,
            Info,
            Calculator,
            Exam,
            Question_1,
            Question_2,
            Question_3,
            Rules,
            Text,
            Score,


            //Кнопки
            Input_Field_Login,
            Input_Field_Password,
            Log_In_Computer,
            number_0,
            number_1,
            number_2,
            number_3,
            number_4,
            Plus,
            Minus,
            Multiply,
            Divide,
            Mod,
            Calculate,
            Backspace,
            Reset,
            Finish_Exam,
            Answer_1,
            Answer_2,
            Answer_3,
            Answer_4,
            Close,
            Exit,
        }



        public static commands GetCommand(string command)
        {
            command.Replace(" ", "_");
            commands result;

            if (Enum.TryParse(command, out result))
            {
                return result;
            }
            else
            {
                Debug.LogError("Несуществующая комманда в списке - " + command);
                return default;
            }
        }

        public static string GetString(commands command)
        {
            string result = command.ToString();
            result = result.Replace("number_", "");
            result = result.Replace("_", " ");
            return result;
        }
    }
}
