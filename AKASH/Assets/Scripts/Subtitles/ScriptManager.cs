using System;
using System.Collections.Generic;
using UnityEngine;


public class ScriptManager : MonoBehaviour
{

    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string[]> linesDuration = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, int> linesQuantity = new Dictionary<string, int>()
    {
        { "Teacher_Shout_", 9},
        { "Teacher_Bull_Talking_", 2},
        { "Teacher_Bull_Cheating_", 2},
        { "Teacher_Bull_Nothing_", 2},
        { "Teacher_Bull_Walking_", 2},
        { "Teacher_Joke_Talking_", 2},
        { "Teacher_Joke_Cheating_", 2},
        { "Teacher_Joke_Nothing_", 2},
        { "Teacher_Joke_Walking_", 2},
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
            return new string[] { "<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>" };
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
