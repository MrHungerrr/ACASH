using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxScholar : MonoBehaviour
{

    private TextMeshProUGUI textBox;
    private ScriptManager scriptMan = GameObject.FindObjectOfType<ScriptManager>();
    private bool saying = false;
    private bool question = false;
    private bool filled = false;
    private float time_N;
    private float time = 0;


    public TextBoxScholar(TextMeshProUGUI box)
    {
        textBox = box;
    }

    private void Update()
    {
        if(filled && !question)
        {
            time += Time.deltaTime;
            if(time>=time_N)
            {
                Clear();
            }
        }

        if(saying)
        {
            //Звук говорения
        }
        else
        {
            //Минус звук говорения
        }
    }

    public void Say(string name)
    {
        StopAllCoroutines();
        Clear();
        StartCoroutine(PlaySay(name));
        time_N = 1f;
        question = false;
    }

    public void Say(string name, float t)
    {
        StopAllCoroutines();
        Clear();
        StartCoroutine(PlaySay(name));
        question = false;
        time_N = t;
    }

    public void Question(string name)
    {
        StopAllCoroutines();
        Clear();
        StartCoroutine(PlaySay(name));
        question = true;
    }

    public void Clear()
    {
        textBox.text = "";
        filled = false;
        saying = false;
        time = 0;
    }

    private IEnumerator PlaySay(string name)
    {
        var script = scriptMan.GetText(name);
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
    }


}
