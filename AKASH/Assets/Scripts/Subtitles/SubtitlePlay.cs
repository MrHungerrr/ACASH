using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitlePlay : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public TextMeshProUGUI textBoxVillian;

    public void Clear()
    {
        textBox.text = string.Empty;
    }

    public void SetText(string text)
    {
        textBox.text = text;
    }
    public void ClearVillian()
    {
        textBoxVillian.text = string.Empty;
    }

    public void SetTextVillian(string text)
    {
        textBoxVillian.text = text;
    }
}
