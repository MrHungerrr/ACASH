using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarTextBox : MonoBehaviour
{

    private Scholar _scholar;

    private bool _act = false;
    private bool _saying = false;
    private bool _filled = false;
    private float _timeClear_N;
    private float _timeClear = 0;


    [SerializeField] private TextMeshPro _textField;


    public void Setup(Scholar Scholar)
    {
        this._scholar = Scholar;
        Clear();
    }

    private void Update()
    {
        if (_filled)
        {
            if (_timeClear >= _timeClear_N)
            {
                Clear();
            }
            else
            {
                _timeClear += Time.deltaTime;
            }
        }

        if (_act)
        {
            if (_saying)
            {
                _scholar.Sound.Play(ScholarSounds.sounds.Talk);
            }
            else
            {
                _scholar.Sound.Stop(ScholarSounds.sounds.Talk);
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
        _timeClear_N = t;
    }


    public void Clear()
    {
        if (_act)
        {
            StopAllCoroutines();
            _scholar.Sound.Stop(ScholarSounds.sounds.Talk);
        }

        Text("");
        _act = false;
        _filled = false;
        _saying = false;
        _timeClear = 0;
    }



    private IEnumerator PlaySub(KeyWord key_word)
    {
        _act = true;
        var script = ScriptManager.Instance.GetText(key_word);

        if (script != null)
        {
            foreach (var line in script)
            {
                _saying = true;
                int quant = line.Length;
                for (int i = 0; i < quant; i++)
                {
                    TextPlus(line[i]);


                    if (line[i] == ' ' && BaseMath.Probability(0.25))
                    {
                        _saying = false;
                        yield return new WaitForSeconds(0.15f);
                        _saying = true;
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.05f);
                    }
                }
                _saying = false;
                yield return new WaitForSeconds(1f);
            }
        }

        _filled = true;
        _act = false;
    }

    public bool IsTalking()
    {
        return (_act || _filled);
    }

    private void Text(string text)
    {
        _textField.text = text;
    }

    private void TextPlus(char symbol)
    {
        _textField.text += symbol;
    }
}
