using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{

    private Dictionary<string, string> lines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script.en";

    private void Awake()
    {
        var textAsset = Resources.Load<TextAsset>(resourceFile);
        var voText = JsonUtility.FromJson<SubtitleStorage>(textAsset.text);

        foreach(var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    public string GetText(string textKey)
    {
        string tmp = "";
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;
        else
            return string.Empty;
    }

    public void TalkControl(string talkKey)
    {
       //ScriptMan.GetText(talkKey);
       //AudioSource
    }
}
