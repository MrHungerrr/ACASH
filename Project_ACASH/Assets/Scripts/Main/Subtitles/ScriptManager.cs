using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Single;


public class ScriptManager : MonoSingleton<ScriptManager>
{
    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, float[]> durations = new Dictionary<string, float[]>();

    private string resourcePath = "Subtitles/";
    private string resourceFile_script = "Script";
    private string resourceFile_duration = "AudioDuration";

    private string textLanguage = "en";
    [HideInInspector]
    public string voiceLanguage { get; private set; } = "ru";


    private void Awake()
    {
        SwitchLanguageText(textLanguage);
        SwitchLanguageVoice(voiceLanguage);
    }
    
    public string[] GetText(KeyWord keyWord)
    {
        return GetText(keyWord.FullWorld);
    }

    private string[] GetText(string textKey)
    {
        string[] tmp;
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;
        else
        {
            Debug.Log("<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>");
            return new string[] {""};
        }
    }


    public string GetLine(KeyWord keyWord)
    {
        return GetLine(keyWord.FullWorld);
    }

    public string GetLine(string textKey)
    {
        string[] tmp;
        if (lines.TryGetValue(textKey, out tmp))
            return tmp[0];
        else
        {
            //Сделать пустоту и нулевое время
            Debug.Log("<color=#ff00ff>MISSING TEXT for '" + textKey + "'</color>");
            return "";
        }
    }


    public float[] GetFloat(KeyWord keyWord)
    {
        return GetFloat(keyWord.FullWorld);
    }

    private float[] GetFloat(string textKey)
    {
        float[] tmp;
        if (durations.TryGetValue(textKey, out tmp))
        {
            return tmp;
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
        string scriptFileName = resourcePath + resourceFile_script + "." + lang;
        var textAsset = Resources.Load<TextAsset>(scriptFileName);
        var voText = JsonUtility.FromJson<TextStorage>(textAsset.text);

        foreach (var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    public void SwitchLanguageVoice(string lang)
    {
        voiceLanguage = lang;

        string scriptFileName = resourcePath + resourceFile_duration + "." + lang;
        var textAsset = Resources.Load<TextAsset>(scriptFileName);
        var voText = JsonUtility.FromJson<DurationStorage>(textAsset.text);

        foreach (var t in voText.lines)
        {
            durations[t.key] = t.duration;
        }
    }
}
