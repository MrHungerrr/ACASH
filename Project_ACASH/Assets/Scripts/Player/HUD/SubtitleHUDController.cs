using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Single;

public class SubtitleHUDController: MonoBehaviour
{

    private TextMeshProUGUI textBox;

    private void Awake()
    {
        textBox = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }


    public void Clear()
    {
        textBox.text = string.Empty;
    }


    public void SetText(string text)
    {
        textBox.text = text;
    }
}
