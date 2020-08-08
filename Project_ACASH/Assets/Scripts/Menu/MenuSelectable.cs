using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSelectable : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [HideInInspector]
    public MenuSection section;
    [HideInInspector]
    public Image image;



    private void Awake()
    {
        image = GetComponent<Image>();
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Menu.Instance.Select(section.menu_number);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!section.settings)
            Menu.Instance.Enter();
    }
}
