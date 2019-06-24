using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    private ScriptManager ScriptMan;
    private SubtitlePlay SubPlay;


    private void Awake()
    {
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
        SubPlay = GameObject.FindObjectOfType<SubtitlePlay>();
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
        var script = ScriptMan.GetText(name);
        foreach ( var line in script)
        {
            yield return new WaitForSeconds(0.05f);
            SubPlay.SetText(line);
            yield return new WaitForSeconds(1f);
            SubPlay.Clear();
        }
        Debug.Log("SubtitleMan");
    }

    private IEnumerator PlaySubVillian(string name)
    {
        var script = ScriptMan.GetText(name);
        foreach (var line in script)
        {
            yield return new WaitForSeconds(0.05f);
            SubPlay.SetTextVillian(line);
            yield return new WaitForSeconds(1f);
            SubPlay.ClearVillian();
        }
        Debug.Log("SubtitleMan");
    }
}