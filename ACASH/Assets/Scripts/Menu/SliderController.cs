using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class SliderController: MonoBehaviour, IPointerEnterHandler
{
    private SliderPiece[] pieces = new SliderPiece[10];
    private Arrow[] arrows = new Arrow[2];
    private int number;
    private int current_nomber;

    private Image select_image;
    [SerializeField]
    private MenuSection section;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
                pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
                pieces[i].slider = this;
                pieces[i].nomber = i;
        }

        arrows[0] = transform.Find("Left_Arrow").GetComponent<Arrow>();
        arrows[0].plus = false;

        arrows[1] = transform.Find("Right_Arrow").GetComponent<Arrow>();
        arrows[1].plus = true;

        select_image = GetComponent<Image>();

        section.slider = this;
        Enable(false);
    }

    private void OnEnable()
    {
        number = SettingsManager.get.settings_current[section.name];
        SetValue();
    }

    public void Enable(bool u)
    {
        if (u)
        {
            select_image.enabled = false;
            SettingsManager.get.slider = this;
            SettingsManager.get.type_of_setting = "slider";
            Arrows(true);
        }
        else
        {
            select_image.enabled = true;
            SettingsManager.get.slider = null;
            SettingsManager.get.type_of_setting = null;
            SetValue();
            Arrows(false);
        }
    }

    public void ClickSelect(int nom)
    {
        number = nom;
        SetValue();
    }



    public void SetValue()
    {
        current_nomber = number;
        Select(number);
        SettingsManager.get.settings_new[section.name] = number;    
    }


    public void Select(int nom)
    {
        current_nomber = nom;

        for (int i = 0; i < 10; i++)
        {
            if (i < nom)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }


    public void SwitchSelect(bool plus)
    {
        if (plus)
        {
            current_nomber++;
            if (current_nomber > 10)
                current_nomber = 10;
        }
        else
        {
            current_nomber--;
            if (current_nomber < 0)
                current_nomber = 0;
        }

        number = current_nomber;
        SetValue();
    }


    private void Arrows(bool enable)
    {
        arrows[0].gameObject.SetActive(enable);
        arrows[1].gameObject.SetActive(enable);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(select_image.enabled)
            Menu.get.Select(section.menu_number);
    }
}
