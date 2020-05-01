namespace Operations
{
    public static class GetO //Operation
    {
        public enum operation
        {
            Go_To,
            Check,
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
            Answer,
            Toilet_Is_Free,
            Sink_Is_Free,
            Outside_Is_Free,
        }
    }
}