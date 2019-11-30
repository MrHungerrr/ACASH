using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitlePlay : MonoBehaviour
{

    public TextMeshProUGUI textBox;

    public void Clear()
    {
        textBox.text = string.Empty;
    }


    public void SetText(string text)
    {
            textBox.text = text;
    }


    public void Enable(bool option)
    {
        if (option)
        {
            textBox.faceColor = new Color(0f, 0f, 0f, 1f);
            textBox.outlineColor = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            textBox.faceColor = new Color(0f, 0f, 0f, 0f);
            textBox.outlineColor = new Color(1f, 1f, 1f, 0f);
        }

    }
}
