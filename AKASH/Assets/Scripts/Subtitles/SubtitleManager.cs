using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    private ScriptManager ScriptMan;
    private SubtitlePlay SubPlay;

 
    public void PlaySubtitle(string name)
    {
       SubPlay.SetText(ScriptMan.GetText(name));
    }

    public void PlaySubtitleVillian(string name)
    {
        SubPlay.SetTextVillian(ScriptMan.GetText(name));
    }
}