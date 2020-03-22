using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;



public class SubtitleManager : Singleton<SubtitleManager>
{
    private SubtitlePlay subPlay;
    private KeyWord last_key;
    [HideInInspector]
    public bool act;


    private void Awake()
    {
        subPlay = GameObject.FindObjectOfType<SubtitlePlay>();
    }


    public void Say(KeyWord key)
    {
        StopSubtitile();
        StartCoroutine(PlaySub(key));
    }

    public void StopSubtitile()
    {
        if (last_key != null)
        {
            StopAllCoroutines();
            Debug.Log("Остановка субтитров - " + last_key);
        }

        subPlay.Clear();
        //Остановка аудиозаписи
    }



    private IEnumerator PlaySub(KeyWord key_word)
    {
        act = true;
        last_key = key_word;

        var script = ScriptManager.get.GetText(key_word);
        var duration = ScriptManager.get.GetFloat(key_word);
        //FMODUnity.RuntimeManager.PlayOneShot(scriptMan.voicePath + name);

        Debug.Log("<color=#0000ff>PlaySub</color>");

        for (int i = 0; i < script.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetText(script[i]);
            yield return new WaitForSeconds(duration[i]);
            subPlay.Clear();
        }

        act = false;
    }

    public void Enable(bool option)
    {
        subPlay.Enable(option);
    }
}