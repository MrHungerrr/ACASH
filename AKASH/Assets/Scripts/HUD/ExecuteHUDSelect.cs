using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ExecuteHUDSelect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [HideInInspector]
    public int number;
    [HideInInspector]
    public Image image;



    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Select(bool option)
    {
        if(option)
        {
            image.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        }
        else
        {
            image.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    public void Disable()
    {
        image.color = new Color(0f, 0f, 0f, 0f);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ExecuteHUDController.get.Select(number);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ExecuteHUDController.get.Accept();
    }
}

