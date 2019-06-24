using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{

    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

    private string resourceFile = "script";

    public string defaultLanguage = "en";

    public string overrideLanguage = "";

    private void Awake()
    {
        var countryCode = LanguageHelper.Get2LetterISOCodeFromSystemLanguage();
        if(!string.IsNullOrEmpty(overrideLanguage))
        {
            countryCode = overrideLanguage;
        }

        string scriptFileName = resourceFile + "." + countryCode;
        var textAsset = Resources.Load<TextAsset>(scriptFileName);
        var voText = JsonUtility.FromJson<SubtitleStorage>(textAsset.text);

        foreach(var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    public string[] GetText(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;
        else
            return new string[] { "<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>" };
    }

    public void TalkControl(string talkKey)
    {
       //ScriptMan.GetText(talkKey);
       //AudioSource
    }
}
