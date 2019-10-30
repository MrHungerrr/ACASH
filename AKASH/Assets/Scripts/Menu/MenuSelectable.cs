using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSelectable : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    private TextMeshProUGUI text;
    [HideInInspector]
    public string topic;
    [HideInInspector]
    public int menu_nomber;
    [HideInInspector]
    public bool settings;
    [SerializeField]
    private SliderController slider;
    [SerializeField]
    private SelectorController selector;
    private Image select_image;



    private void Awake()
    {
        select_image = GetComponent<Image>();
        text = transform.parent.GetComponent<TextMeshProUGUI>();
        topic = text.name;

        if (slider == null && selector == null)
        {
            settings = false;
        }
        else
        {
            settings = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Menu.get.Select(menu_nomber);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!settings)
            Menu.get.Enter();
    }


    public void Select(bool u)
    {
        if (u)
        {
            //text.color = new Color(0.7f, 0.7f, 0.7f);
            select_image.color = new Color(1f, 1f, 1f, 0.3f);

            if (settings)
            {
                if (slider != null)
                {
                    slider.Enable(true);
                }

                if (selector != null)
                {
                    selector.Enable(true);
                }
            }
        }
        else
        {
            //text.color = new Color(1f, 1f, 1f);
            select_image.color = new Color(1f, 1f, 1f, 0f);

            if (settings)
            {
                if (slider != null)
                {
                    slider.Enable(false);
                }

                if (selector != null)
                {
                    selector.Enable(false);
                }
            }
        }
    }


}
