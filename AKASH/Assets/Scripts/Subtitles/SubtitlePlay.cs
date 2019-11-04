using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitlePlay : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    private bool active;

    public void Clear()
    {
        textBox.text = string.Empty;
    }

    public void SetText(string text)
    {
        if(active)
            textBox.text = text;
    }

    public void Enable(bool option)
    {
        if (option)
        {
            textBox.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            textBox.color = new Color(1f, 1f, 1f, 0f);
        }

    }
}
