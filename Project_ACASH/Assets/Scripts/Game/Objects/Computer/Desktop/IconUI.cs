using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class IconUI : MonoBehaviour
{
    private Image icon;
    private TextMeshProUGUI topic;


    public void Setup()
    {
        icon = transform.Find("Image").GetComponent<Image>();
        topic = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Change(Icon new_icon)
    {
        icon.sprite = new_icon.image;
        topic.text = new_icon.topic;
        name = new_icon.name;
    }

    public void Enable(bool option)
    {
        this.gameObject.SetActive(option);
    }
}
