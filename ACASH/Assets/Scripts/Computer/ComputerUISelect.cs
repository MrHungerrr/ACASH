using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ComputerUISelect : MonoBehaviour
{
    [SerializeField]
    private Image[] images;

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
            images[i].color = ComputerManager.colors[buf];
        }
    }
}
