using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Single;
using PlayerOptions;

public class HintController: MonoBehaviour
{
    private KeyWord key_word = new KeyWord("Hint");
    private Image key;
    private TextMeshProUGUI information;
    private KeyAction onAction = new KeyAction();


    //Fading;
    private bool changing = false;
    private bool active = false;
    private float fade_goal = 1f;
    private float fade_now = 1f;



    public GetP.actions action;


    private void Update()
    {
     if(changing)
        {
            Fading();
        }
    }

    private void Setup()
    {
        Transform buf = transform.Find("Canvas");
        information = buf.Find("Text").GetComponent<TextMeshProUGUI>();
        key = buf.Find("Key").GetComponent<Image>();
    }


    public void SetHint(bool auto)
    {
        Setup();

        key_word *= action.ToString();
        key_word += 0;
        string text = ScriptManager.get.GetLine(key_word);
        information.text = text;

        Sprite buf = Resources.Load<Sprite>("Keys/Keyboard/" + action.ToString());

        key.sprite = buf;


        if(auto)
        {
            Enable();
            onAction.Setup(action); 
            onAction.OnKeyDown += Disable;
        }
    }


    private void Fading()
    {

        fade_now = Mathf.Lerp(fade_now, fade_goal, 3f * Time.deltaTime);

        if(Mathf.Abs(fade_now - fade_goal) < 0.01f)
        {
            fade_now = fade_goal;
            changing = false;
        }

        Color targetColor = new Color(1f, 1f, 1f, fade_now);

        key.color = targetColor;
        information.color = targetColor;

        if (!active && !changing)
            Remove();
    }

    public void Enable()
    {
        changing = true;
        active = true;
        fade_goal = 1f;
    }


    public void Disable()
    {
        changing = true;
        active = false;
        fade_goal = 0f;
    }


    private void Remove()
    {
        onAction.Remove();
        Destroy(gameObject);
    }

}
