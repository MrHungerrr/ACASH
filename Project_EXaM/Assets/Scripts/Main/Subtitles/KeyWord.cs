using System;
using UnityEngine;

public class KeyWord
{
    public string FullWorld => $"{_main}_{FullKey}";
    public string FullKey => $"{Key}_{_number}";
    public int Number => _number;
    public string Key => GetKey();
    public string Main => _main;



    private readonly string _main;
    private readonly string _constKey;
    private string _key;
    private int _number;




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
    }

    protected void Plus(KeyWord keyword)
    {
        Plus(keyword.GetKey());
    }

    public void SetNumber(int number)
    {
        this._number = number;
    }

    public void SetKey(string word)
    {
        _key = "_" + word;
    }

    public void Reset()
    {
        _key = string.Empty;
        _number = 0;
    }


    private string GetKey()
    {
        if (_constKey != string.Empty && _key != string.Empty)
            return _constKey + "_" + _key;
        else
            return _constKey + _key;
    }




    public static KeyWord operator +(KeyWord keyword, string word)
    {
        keyword.Plus(word);
        return keyword;
    }


    public static KeyWord operator +(KeyWord keyword, int number)
    {
        keyword.SetNumber(number);
        return keyword;
    }

    /// <summary>
    /// Соединяются key1 и key2
    /// </summary>
    public static KeyWord operator +(KeyWord keyword1, KeyWord keyword2)
    {
        keyword1.Plus(keyword2);
        return keyword1;
    }

    /// <summary>
    /// key заменяется word
    /// </summary>
    public static KeyWord operator *(KeyWord keyword, string word)
    {
        keyword.Reset();
        keyword.Plus(word);
        return keyword;
    }

    /// <summary>
    /// key1 заменяется key2
    /// </summary>
    public static KeyWord operator *(KeyWord keyword1, KeyWord keyword2)
    {
        keyword1.Reset();
        keyword1.Plus(keyword2);
        keyword1.SetNumber(keyword2._number);
        return keyword1;
    }
}
