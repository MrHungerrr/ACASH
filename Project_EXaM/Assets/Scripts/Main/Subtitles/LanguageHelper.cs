using UnityEngine;
using System.Collections;

public static class LanguageHelper
{
    public enum languange
    {
        en,
        ru,
    }


    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
        string res = "en";
        switch (lang)
        {
            case SystemLanguage.English: res = "en"; break;
            case SystemLanguage.Russian: res = "ru"; break;
        }

        return res;
    }
}