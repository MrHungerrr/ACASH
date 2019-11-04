using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ComputerSelect : MonoBehaviour
{
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private TextMeshProUGUI[] texts;

    private Color[] colors = new Color[2]
    {
        new Color(0f,0f,0f,0f),
        new Color(1f,1f,1f,0.3f),
    }; 


    public void Select(bool option)
    {
        int buf;

        if(option)
        {
            buf = 1;
        }
        else
        {
            buf = 0;
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = colors[buf];
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = colors[buf];
        }

    }
}
