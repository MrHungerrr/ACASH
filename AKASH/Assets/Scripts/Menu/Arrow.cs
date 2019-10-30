﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Arrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    [HideInInspector]
    public bool plus;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Select(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Menu.get.move_cd <= 0)
        {
            SettingsManager.get.SwtichSettings(plus);
            Menu.get.MoveCD();
        }
    }


    public void Select(bool u)
    {
        if (u)
        {
            image.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }
    }

}

