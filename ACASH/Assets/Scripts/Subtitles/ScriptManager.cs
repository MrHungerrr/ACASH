using System;
using System.Collections.Generic;
using UnityEngine;
using Single;


public class ScriptManager : Singleton<ScriptManager>
{
    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, string[]> linesDuration = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);


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
    
    public string[] GetText(KeyWord key_word)
    {
        return GetText(KeyWordManager.GetScriptKey(key_word));
    }

    private string[] GetText(string textKey)
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


    public string GetLine(KeyWord key_word)
    {
        return GetLine(KeyWordManager.GetScriptKey(key_word));
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


    public float[] GetFloat(KeyWord key_word)
    {
        return GetFloat(KeyWordManager.GetScriptKey(key_word));
    }

    private float[] GetFloat(string textKey)
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
