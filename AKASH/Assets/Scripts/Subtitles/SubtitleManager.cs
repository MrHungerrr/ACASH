using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;



public class SubtitleManager : Singleton<SubtitleManager>
{
    private SubtitlePlay subPlay;
    private string lastKey;
    [HideInInspector]
    public bool act;


    private void Awake()
    {
        subPlay = GameObject.FindObjectOfType<SubtitlePlay>();
    }


    public void Say(string key)
    {
        StartCoroutine(PlaySub(key));
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
        var script = ScriptManager.get.GetText(key);
        var duration = ScriptManager.get.GetFloat(key);
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

    public void Enable(bool option)
    {
        subPlay.Enable(option);
    }
}