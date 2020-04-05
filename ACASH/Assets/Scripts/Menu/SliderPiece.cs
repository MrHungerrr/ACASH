using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class SliderPiece : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [HideInInspector]
    public SliderController slider;
    private Image image;
    [HideInInspector]
    public int nomber;

    private void Awake()
    {
        slider = null;
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(slider != null)
            slider.Select(nomber+1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slider != null)
            slider.ClickSelect(nomber+1);
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

