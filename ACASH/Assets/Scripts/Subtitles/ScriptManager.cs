using System;
using System.Collections.Generic;
using UnityEngine;
using Single;


public class ScriptManager : Singleton<ScriptManager>
{

    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string[]> linesDuration = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, int> linesQuantity = new Dictionary<string, int>()
    {
        { "Teacher_Shout_", 10},
        { "Teacher_Bull_Talking_", 3},
        { "Teacher_Bull_Cheating_", 3},
        { "Teacher_Bull_Nothing_", 3},
        { "Teacher_Bull_Walking_", 3},
        { "Teacher_Joke_Talking_", 3},
        { "Teacher_Joke_Cheating_", 3},
        { "Teacher_Joke_Nothing_", 3},
        { "Teacher_Joke_Walking_", 3},
        { "Teacher_Bull_Talking_Sec_", 3},
        { "Teacher_Bull_Cheating_Sec_", 3},
        { "Teacher_Bull_Nothing_Sec_", 3},
        { "Teacher_Bull_Walking_Sec_", 3},
        { "Teacher_Joke_Talking_Sec_", 3},
        { "Teacher_Joke_Cheating_Sec_", 3},
        { "Teacher_Joke_Nothing_Sec_", 3},
        { "Teacher_Joke_Walking_Sec_", 3},
        { "Teacher_Bull_Pen_", 3},
        { "Teacher_Bull_Pen_Sec_", 3},
        { "Teacher_Joke_Pen_", 3},
        { "Teacher_Joke_Pen_Sec_", 3},
        { "Teacher_Thinking_", 5},
        { "Teacher_Execute_Walking_", 3},
        { "Teacher_Execute_Cheating_", 3},
        { "Teacher_Execute_Talking_", 3},
        { "Teacher_Answer_Permission_", 3},
        { "Answer_", 3},
    };


    private string resourceFile = "Script";
    private string resourceFileDuration = "AudioDuration";
    [HideInInspector]
    public string voicePath;

    private string textLanguage = "ru";

    private string voiceLanguage = "ru";


    private void Awake()
    {
        //var countryCode = LanguageHelper.Get2LetterISOCodeFromSystemLanguage();
        SwitchLanguageText(textLanguage);
        SwitchLanguageVoice(voiceLanguage);
    }
    
    public string[] GetText(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;
        else
        {
            //Сделать пустоту и нулевое время
            return new string[] { "<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>" };
        }
    }

    public string GetLine(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp[0];
        else
        {
            //Сделать пустоту и нулевое время
            return "<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>";
        }
    }


    public float[] GetFloat(string textKey)
    {
        string[] tmp = new string[] { };
        if (linesDuration.TryGetValue(textKey, out tmp))
        {
            float[] res = new float[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
                res[i] = float.Parse(tmp[i]);

            return res;
        }
        else
        {
            Debug.Log("<color=#ff00ff>Missing value for '" + textKey + "'</color>");
            return new float[] {2};
        }

    }


    public void SwitchLanguageText(string lang)
    {
        textLanguage = lang;
        string scriptFileName = resourceFile + "." + lang;
        var textAsset = Resources.Load<TextAsset>(scriptFileName);
        var voText = JsonUtility.FromJson<SubtitleStorage>(textAsset.text);

        foreach (var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    public void SwitchLanguageVoice(string lang)
    {
        voiceLanguage = lang;
        voicePath = "event:/" + lang + "/";

        string scriptFileName = resourceFileDuration + "." + lang;
        var textAsset = Resources.Load<TextAsset>(scriptFileName);
        var voText = JsonUtility.FromJson<SubtitleStorage>(textAsset.text);

        foreach (var t in voText.lines)
        {
            linesDuration[t.key] = t.line;
        }
    }
}
