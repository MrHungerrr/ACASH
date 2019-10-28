using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Awake()
    {
        text = transform.parent.GetComponent<TextMeshProUGUI>();
        topic = text.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Menu.get.Select(menu_nomber);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Menu.get.Enter();
    }


    public void Select(bool u)
    {
        if (u)
        {
            text.color = new Color(0.7f, 0.7f, 0.7f);
            text.fontStyle = FontStyles.Underline;

            if (slider != null)
            {
                slider.Enable(true);
            }
        }
        else
        {
            text.color = new Color(1f, 1f, 1f);
            text.fontStyle = FontStyles.Normal;

            if (slider != null)
            {
                slider.Enable(false);
            }
        }
    }
}
