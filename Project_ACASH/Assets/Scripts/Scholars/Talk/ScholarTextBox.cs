using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarTextBox : MonoBehaviour
{

    private Scholar Scholar;

    private TextMeshPro[] textBox = new TextMeshPro[3];
    private SliderWatch stressSlider;
    private bool saying = false;
    private bool question = false;
    [HideInInspector]
    public bool act = false;
    private bool filled = false;
    private float timeClear_N;
    private float timeClear = 0;
    private float timeUnselectable_N;
    private float timeUnselectable = 0;


    public void Setup(Scholar Scholar)
    {
        this.Scholar = Scholar;

        Transform buf = Scholar.transform.Find("Scholar").Find("Scholar").Find("Spine").Find("Text Box");
        textBox[0] = buf.Find("Text_0").GetComponent<TextMeshPro>();
        textBox[1] = buf.Find("Text_1").GetComponent<TextMeshPro>();
        textBox[2] = buf.Find("Text_2").GetComponent<TextMeshPro>();
        stressSlider = buf.Find("Stress Slider").GetComponentInChildren<SliderWatch>();
        stressSlider.Setup();
        Clear();
    }

    private void Update()
    {
        if (filled && !question)
        {
            if (timeClear >= timeClear_N)
            {
                Clear();
            }
            else if (timeUnselectable >= timeUnselectable_N)
            {
                timeClear += Time.deltaTime;
            }
            else
            {
                timeUnselectable += Time.deltaTime;
            }
        }

        if (act)
        {
            if (saying)
            {
                Scholar.Sound.Play(ScholarSounds.sounds.Talk);
            }
            else
            {
                Scholar.Sound.Stop(ScholarSounds.sounds.Talk);
            }
        }
    }

    public void Say(KeyWord key_word)
    {
        Say(key_word, 3f);
    }

    public void Say(KeyWord key_word, float t)
    {
        Clear();
        StartCoroutine(PlaySub(key_word));
        timeClear_N = t;
    }

    public void Question(KeyWord key_word)
    {
        Clear();
        question = true;
        StartCoroutine(PlaySub(key_word));
    }

    public void Clear()
    {
        if (act)
        {
            StopAllCoroutines();
            Scholar.Sound.Stop(ScholarSounds.sounds.Talk);
        }
        Text("");
        act = false;
        filled = false;
        question = false;
        saying = false;
        timeClear = 0;
    }



    private IEnumerator PlaySub(KeyWord key_word)
    {
        act = true;
        var script = ScriptManager.Instance.GetText(key_word);

        if (script != null)
        {
            foreach (var line in script)
            {

                saying = true;
                int quant = line.Length;
                for (int i = 0; i < quant; i++)
                {
                    TextPlus(line[i]);


                    if (line[i] == ' ' && BaseMath.Probability(0.25))
                    {
                        saying = false;
                        yield return new WaitForSeconds(0.15f);
                        saying = true;
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.05f);
                    }
                }
                saying = false;
                //TextPlus(' ');
                yield return new WaitForSeconds(1f);
            }
        }
        filled = true;
        act = false;
        yield break;
    }

    public bool IsTalking()
    {
        if(question)
            return (act);
        else
            return (act || filled);
    }

    private void Text(string text)
    {
        textBox[0].text = text;
    }

    private void TextPlus(char symbol)
    {
        textBox[0].text += symbol;
    }

    public void Number(int num)
    {
        textBox[1].text = (num+1).ToString();
    }

    public void StressLevel(int num)
    {
        textBox[2].text = num.ToString() + '%';
        stressSlider.Select(num);
    }

}
