using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    private ScriptManager scriptMan;
    private SubtitlePlay subPlay;
    private string lastKey;
    [HideInInspector]
    public bool act;


    private void Awake()
    {
        scriptMan = GameObject.FindObjectOfType<ScriptManager>();
        subPlay = GameObject.FindObjectOfType<SubtitlePlay>();
    }


    public void Say(string key)
    {
        StartCoroutine(PlaySub(key));
    }

    public void PlaySubtitleVillian(string key)
    {
        StartCoroutine(PlaySubVillian(key));
    }

    public void StopSubtitile()
    {
        if (lastKey != string.Empty)
        {
            StopAllCoroutines();
            Debug.Log(lastKey);
        }
        Debug.Log("Da");
        subPlay.Clear();
        //Остановка аудиозаписи
    }



    private IEnumerator PlaySub(string key)
    {
        act = true;
        lastKey = key;
        int i = 0;
        var script = scriptMan.GetText(key);
        var duration = scriptMan.GetFloat(key);
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

    private IEnumerator PlaySubVillian(string key)
    {
        act = true;
        int i = 0;
        var script = scriptMan.GetText(key);
        var duration = scriptMan.GetFloat(key);
        FMODUnity.RuntimeManager.PlayOneShot(scriptMan.voicePath + key);

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