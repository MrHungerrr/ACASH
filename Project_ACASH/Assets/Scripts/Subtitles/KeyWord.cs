using UnityEngine;

public class KeyWord
{
    protected enum answer
    {
        Void,
        Yes,
        No
    }

    private readonly string _main;
    private readonly string _constKey;
    private string _key;
    private answer _answer;
    private int _number;
    private string _fullKey;




    public KeyWord(string main)
    {
        this._main = main;
        _constKey = string.Empty;
        Reset();
    }

    public KeyWord(string main, string const_key)
    {
        this._main = main;
        this._constKey = const_key;
        Reset();
    }


    public KeyWord()
    {
        this._main = string.Empty;
        _constKey = string.Empty;
        Reset();
    }

    public KeyWord(KeyWord key_word)
    {
        this._main = key_word._main;
        this._constKey = key_word._constKey;
        Reset();
    }

    public KeyWord(KeyWord key_word, string const_key)
    {
        this._main = key_word._main;
        this._constKey = const_key;
        Reset();
    }



    protected void Plus(string word)
    {
        if (_key != string.Empty)
            _key += "_";

        _key += word;

        if (_number != -1)
            _number = -1;

        if (_answer != answer.Void)
            _answer = answer.Void;
    }

    protected void Plus(KeyWord keyword)
    {
        Plus(keyword.GetKey());
    }

    public void Answer(bool option)
    {
        if (option)
        {
            _answer = answer.Yes;
        }
        else
        {
            _answer = answer.No;
        }
    }

    public void Number(int number)
    {
        this._number = number;
    }

    public void Key(string word)
    {
        _key = "_" + word;
    }

    protected void FullKey()
    {
        _fullKey = GetKey();

        if (_answer != answer.Void)
            _fullKey += "_" + _answer.ToString();

        if (_number != -1)
            _fullKey += "_" + _number;
    }

    public void Reset()
    {
        _key = string.Empty;
        _number = -1;
        _answer = answer.Void;
    }







    public int GetNumber()
    {
        return _number;
    }

    public string GetKey()
    {
        if (_constKey != string.Empty && _key != string.Empty)
            return _constKey + "_" + _key;
        else
            return _constKey + _key;
    }

    public string GetFullKey()
    {
        FullKey();
        return _fullKey;
    }

    public virtual string GetFullWord()
    {
        if (_main != string.Empty)
            return _main + "_" + GetFullKey();
        else
        {
            Debug.LogError("Ошибка в KeyWord - " + _fullKey);
            return string.Empty;
        }
    }



    public string GetMain()
    {
        return _main;
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
        keyword1.Number(keyword2._number);
        keyword1._answer = keyword2._answer;
        return keyword1;
    }
}
