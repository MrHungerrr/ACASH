using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxScholar : MonoBehaviour
{

    private TextMeshPro textBox;
    private ScriptManager scriptMan;
    private bool saying = false;
    private bool question = false;
    private bool act = false;
    private bool filled = false;
    private float timeClear_N;
    private float timeClear = 0;


    private void Start()
    {
        scriptMan = GameObject.FindObjectOfType<ScriptManager>();
        textBox = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if(filled && !question)
        {
            if(timeClear>=timeClear_N)
            {
                Clear();
            }
            else
            {
                timeClear += Time.deltaTime;
            }
        }

        if (act)
        {
            if (saying)
            {
                //Звук говорения
            }
            else
            {
                //Минус звук говорения
            }
        }
    }

    public void Say(string key)
    {
        Clear();
        StartCoroutine(PlaySub(key));
        timeClear_N = 1f;
        question = false;
    }

    public void Say(string key, float t)
    {
        Clear();
        StartCoroutine(PlaySub(key));
        question = false;
        timeClear_N = t;
    }

    public void Question(string key)
    {
        Clear();
        StartCoroutine(PlaySub(key));
        question = true;
    }

    public void Clear()
    {
        if (act)
            StopAllCoroutines();
        textBox.text = "";
        act = false;
        filled = false;
        saying = false;
        timeClear = 0;
    }



    private IEnumerator PlaySub(string key)
    {
        act = true;
        var script = scriptMan.GetText(key);
        foreach (var line in script)
        {
            
            saying = true;
            int quant = line.Length;
            for(int i = 0; i<quant; i++)
            {
                
                textBox.text += line[i];
                yield return new WaitForSeconds(0.02f);
            }
            saying = false;
            textBox.text += " ";
            yield return new WaitForSeconds(1f);
        }
        filled = true;
        act = false;
        yield break;
    }

    public bool IsTalking()
    {
            return (act || filled);
    }
}
