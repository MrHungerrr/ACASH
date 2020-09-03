using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;



public class SubtitleManager : MonoSingleton<SubtitleManager>
{
    private SubtitleHUDController subPlay;
    private KeyWord last_key;
    [HideInInspector]
    public bool act;

    public event Action TalkDone;


    private void Awake()
    {
        subPlay = GameObject.FindObjectOfType<SubtitleHUDController>();
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
            Debug.Log("Остановка субтитров - " + last_key.GetFullKey());
        }

        subPlay.Clear();
        AudioManager.Instance.Voice.Stop();
    }



    private IEnumerator PlaySub(KeyWord key_word)
    {
        act = true;
        last_key = key_word;

        var script = ScriptManager.Instance.GetText(key_word);
        var duration = ScriptManager.Instance.GetFloat(key_word);

        AudioManager.Instance.Voice.Play(key_word);


        //Debug.Log("<color=#0000ff>PlaySub</color>");

        for (int i = 0; i < script.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetText(script[i]);
            yield return new WaitForSeconds(duration[i] - 0.05f);
            subPlay.Clear();
        }

        act = false;

        if (TalkDone != null)
            TalkDone();
    }

}