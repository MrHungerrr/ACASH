using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class SelectorController: MonoBehaviour, IPointerEnterHandler
{
    private Arrow[] arrows = new Arrow[2];
    private TextMeshProUGUI text;
    private string[] values;
    private int number = 0;


    private Image select_image;
    [SerializeField]
    private MenuSection section;


    private void Awake()
    {
        text = transform.GetComponentInChildren<TextMeshProUGUI>();

        arrows[0] = transform.Find("Left_Arrow").GetComponent<Arrow>();
        arrows[0].plus = false;

        arrows[1] = transform.Find("Right_Arrow").GetComponent<Arrow>();
        arrows[1].plus = true;

        select_image = GetComponent<Image>();

        section.selector = this;
        values = SettingsManager.get.settings[section.name];

        Enable(false);
    }

    private void OnEnable()
    {
        number = SettingsManager.get.settings_current[section.name];
        Select();
    }

    public void Enable(bool u)
    {
        if (u)
        {
            select_image.enabled = false;
            SettingsManager.get.selector = this;
            SettingsManager.get.type_of_setting = "selector";
            Arrows(true);
        }
        else
        {
            select_image.enabled = true;
            SettingsManager.get.slider = null;
            SettingsManager.get.type_of_setting = null;
            Select();
            Arrows(false);
        }
    }


    public void Select()
    {
        text.text = values[number];
        SettingsManager.get.settings_new[section.name] = number;
    }


    public void SwitchSelect(bool plus)
    {
        if (plus)
        {
            number++;
            if (number >= values.Length)
                number = 0;
        }
        else
        {
            number--;
            if (number < 0)
                number = values.Length-1;
        }

        Select();
    }


    private void Arrows(bool enable)
    {
        arrows[0].gameObject.SetActive(enable);
        arrows[1].gameObject.SetActive(enable);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (select_image.enabled)
            Menu.get.Select(section.menu_number);
    }
}
