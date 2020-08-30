using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class SliderPiece : MonoBehaviour
{
    private Image image;
    [HideInInspector] public int number;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Select(bool u)
    {
        if (u)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0.15f);
        }
    }

}

