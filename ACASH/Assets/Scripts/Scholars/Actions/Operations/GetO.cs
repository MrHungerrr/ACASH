﻿namespace Operations
{
    public static class GetO //Operation
    {
        public enum operation
        {
            Check,
            Go_Home,
            Go_To_Desk,
            Question,
            Computer,
            Verify,
            Think_Aloud,
            Execute,
            Note,
            StartCheat,
            EndCheat,
        }

        public enum computer
        {
            Calculator,
            Text,
            Rules,
            Login,
        }

        public enum computer_spec
        {
            Question
        }


        public enum special
        {
            Go_To_Toilet,
            Go_To_Sink,
            Go_Outside,
            Pee,
            Think,
            Write,
            Think_Outside,
            Wash_Hands,
            Wait,
        }


        public enum verify
        {
            Teacher_Is_Here,
            Answer
        }
    }
}