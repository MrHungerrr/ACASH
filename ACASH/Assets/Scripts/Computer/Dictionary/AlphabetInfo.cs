using System;

public static class AlphabetInfo
{


    public static int max_length = 5;
    public static int size = 4;

    private static int WordCount()
    {
        int result = 0;

        for (int i = 1; i < max_length + 1; i++)
        {
            result += (int) Math.Pow(size, i);
        }

        return result;
    }

    public static int word_count = WordCount();
}
