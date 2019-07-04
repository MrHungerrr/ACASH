using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    private ScriptManager scriptMan;
    private SubtitlePlay subPlay;


    private void Awake()
    {
        scriptMan = GameObject.FindObjectOfType<ScriptManager>();
        subPlay = GameObject.FindObjectOfType<SubtitlePlay>();
    }


    public void PlaySubtitle(string name)
    {
        StartCoroutine(PlaySub(name));
    }

    public void PlaySubtitleVillian(string name)
    {
        StartCoroutine(PlaySubVillian(name));
    }

    public IEnumerator PlaySub(string name)
    {
        int i = 0;
        var script = scriptMan.GetText(name);
        var duration = scriptMan.GetFloat(name);
        FMODUnity.RuntimeManager.PlayOneShot(scriptMan.voicePath + name);

        foreach ( var line in script)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetText(line);
            yield return new WaitForSeconds(duration[i]);
            subPlay.Clear();
            i++;
        }
    }

    private IEnumerator PlaySubVillian(string name)
    {
        int i = 0;
        var script = scriptMan.GetText(name);
        var duration = scriptMan.GetFloat(name);
        FMODUnity.RuntimeManager.PlayOneShot(scriptMan.voicePath + name);

        foreach (var line in script)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetTextVillian(line);
            yield return new WaitForSeconds(duration[i]);
            subPlay.ClearVillian();
            i++;
        }
    }
}