namespace Operations
{
    public static class GetO //Operation
    {
        public enum operation
        {
            Check,
            Go_Home,
            Question,
            Verify,
            Think_Aloud,
            Execute,
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
        }

        public enum verify
        {
            Teacher_Is_Here,
            Answer
        }
    }
}