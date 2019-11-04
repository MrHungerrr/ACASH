using TMPro;
using UnityEngine;

public class MenuSection : MonoBehaviour
{

    private TextMeshProUGUI text;
    [HideInInspector]
    public int menu_number;
    [HideInInspector]
    public bool settings;
    [HideInInspector]
    public SliderController slider;
    [HideInInspector]
    public SelectorController selector;
    private MenuSelectable select;



    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (slider == null && selector == null)
        {
            settings = false;
        }
        else
        {
            settings = true;
        }

        select = transform.GetComponentInChildren<MenuSelectable>();
        select.section = this;
        Select(false);
    }


    public void Select(bool u)
    {
        if (u)
        {
            select.image.color = new Color(1f, 1f, 1f, 0.3f);

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
            select.image.color = new Color(1f, 1f, 1f, 0f);

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
