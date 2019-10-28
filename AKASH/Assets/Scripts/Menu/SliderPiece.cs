using UnityEngine;
using UnityEngine.EventSystems;



public class SliderPiece : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private SliderController slider;
    private SpriteRenderer render;
    [HideInInspector]
    public int nomber;
    [HideInInspector]
    public bool settings;

    private void Awake()
    {
        slider = transform.GetComponentInParent<SliderController>();
        render = GetComponent<SpriteRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(settings)
            slider.Select(nomber);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (settings)
            slider.Select();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (settings)
            slider.Set(nomber);
    }


    public void Select(bool u)
    {
        if (u)
        {
            render.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            render.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
    }

}

