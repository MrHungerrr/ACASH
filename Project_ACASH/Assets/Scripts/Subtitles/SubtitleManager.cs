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

    public event ActionEvent.OnAction TalkDone;


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
        VoiceManager.get.Stop();
    }



    private IEnumerator PlaySub(KeyWord key_word)
    {
        HUDManager.Instance.SubtitleHUD(true);
        act = true;
        last_key = key_word;

        var script = ScriptManager.Instance.GetText(key_word);
        var duration = ScriptManager.Instance.GetFloat(key_word);

        VoiceManager.get.Play(key_word);


        //Debug.Log("<color=#0000ff>PlaySub</color>");

        for (int i = 0; i < script.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            subPlay.SetText(script[i]);
            yield return new WaitForSeconds(duration[i] - 0.05f);
            subPlay.Clear();
        }

        HUDManager.Instance.SubtitleHUD(false);

        act = false;

        if (TalkDone != null)
            TalkDone();
    }

}