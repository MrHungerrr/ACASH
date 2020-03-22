using UnityEngine;

public class KeyWord
{

    protected enum answer
    {
        Void,
        Yes,
        No
    }

    private string main { get;}
    protected string const_key { get; }
    protected string key { get; set; }
    protected answer ans { get; set; }
    protected int number { get; set; }
    protected string full_key { get; set; }




    public KeyWord(string main)
    {
        this.main = main;
        const_key = string.Empty;
        Reset();
    }

    public KeyWord(string main, string const_key)
    {
        this.main = main;
        this.const_key = const_key;
        Reset();
    }


    public KeyWord()
    {
        this.main = string.Empty;
        const_key = string.Empty;
        Reset();
    }

    public KeyWord(KeyWord key_word)
    {
        this.main = key_word.main;
        this.const_key = key_word.const_key;
        Reset();
    }

    public KeyWord(KeyWord key_word, string const_key)
    {
        this.main = key_word.main;
        this.const_key = const_key;
        Reset();
    }



    protected void Plus(string word)
    {
        if (key != string.Empty)
            key += "_";

        key += word;

        if (number != -1)
            number = -1;

        if (ans != answer.Void)
            ans = answer.Void;
    }

    protected void Plus(KeyWord keyword)
    {
        Plus(keyword.GetKey());
    }

    public void Answer(bool option)
    {
        if (option)
        {
            ans = answer.Yes;
        }
        else
        {
            ans = answer.No;
        }
    }

    public void Number(int number)
    {
        this.number = number;
    }

    public void Key(string word)
    {
        key = "_" + word;
    }

    protected void FullKey()
    {
        full_key = GetKey();

        if (ans != answer.Void)
            full_key += "_" + ans.ToString();

        if (number != -1)
            full_key += "_" + number;
    }

    public void Reset()
    {
        key = string.Empty;
        number = -1;
        ans = answer.Void;
    }






    public int GetNumber()
    {
        return number;
    }

    public string GetKey()
    {
        if (const_key != string.Empty && key != string.Empty)
            return const_key + "_" + key;
        else
            return const_key + key;
    }

    public string GetFullKey()
    {
        FullKey();
        return full_key;
    }

    public virtual string GetFullWord()
    {
        if (main != string.Empty)
            return main + "_" + GetFullKey();
        else
        {
            Debug.LogError("Ошибка в KeyWord - " + full_key);
            return string.Empty;
        }
    }






    public static KeyWord operator +(KeyWord keyword, string word)
    {
        keyword.Plus(word);
        return keyword;
    }

    public static KeyWord operator +(KeyWord keyword, int number)
    {
        keyword.Number(number);
        return keyword;
    }

    //Соединяются key1 и key2
    public static KeyWord operator +(KeyWord keyword1, KeyWord keyword2)
    {
        keyword1.Plus(keyword2);
        return keyword1;
    }

    //key заменяется word
    public static KeyWord operator *(KeyWord keyword, string word)
    {
        keyword.Reset();
        keyword.Plus(word);
        return keyword;
    }

    //key1 заменяется key2
    public static KeyWord operator *(KeyWord keyword1, KeyWord keyword2)
    {
        keyword1.Reset();
        keyword1.Plus(keyword2);
        keyword1.Number(keyword2.number);
        keyword1.ans = keyword2.ans;
        return keyword1;
    }
}
