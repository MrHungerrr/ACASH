using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    private ScriptManager scriptMan;
    private SubtitlePlay subPlay;
    [HideInInspector]
    public bool act;


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

    private IEnumerator PlaySub(string name)
    {
        act = true;
        int i = 0;
        var script = scriptMan.GetText(name);
        var duration = scriptMan.GetFloat(name);
        //FMODUnity.RuntimeManager.PlayOneShot(scriptMan.voicePath + name);

        Debug.Log("PlaySub");
        foreach ( var line in script)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetText(line);
            yield return new WaitForSeconds(duration[i]);
            subPlay.Clear();
            i++;
        }
        act = false;
    }

    private IEnumerator PlaySubVillian(string name)
    {
        act = true;
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
        act = false;
    }
}