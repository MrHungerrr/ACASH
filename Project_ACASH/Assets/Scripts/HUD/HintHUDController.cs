using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Single;
using PlayerOptions;

public class HintHUDController : Singleton<HintHUDController>
{
    private KeyWord key_word = new KeyWord("Hint");
    private Image key;
    private TextMeshProUGUI information;



    private void Awake()
    {
        information = transform.Find("Box Hint").Find("Text").GetComponent<TextMeshProUGUI>();
        key = transform.Find("Box Key").Find("Key").GetComponent<Image>();
    }


    public void SetHint(GetP.actions action)
    {
        key_word *= action.ToString();
        key_word += 0;
        string text = ScriptManager.get.GetLine(key_word);
        information.text = text;

        Debug.Log("Keys/Keyboard/" + action.ToString());
        Sprite buf = Resources.Load<Sprite>("Keys/Keyboard/" + action.ToString());

        key.sprite = buf;
    }

}
