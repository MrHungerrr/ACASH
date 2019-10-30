using UnityEngine;
using System.Collections;


public class SelectorController: MonoBehaviour
{
    private SliderPiece[] pieces = new SliderPiece[10];
    private Arrow[] arrows = new Arrow[2];
    [SerializeField]
    private int nomber = 0;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
            pieces[i].nomber = i;
        }


        arrows[0] = transform.Find("Left_Arrow").GetComponent<Arrow>();
        arrows[0].plus = false;

        arrows[1] = transform.Find("Right_Arrow").GetComponent<Arrow>();
        arrows[1].plus = true;


        Enable(false);
    }

    public void Enable(bool u)
    {

        if (u)
        {
            SettingsManager.get.selector = this;
            SettingsManager.get.type_of_setting = "selector";
            Arrows(true);
        }
        else
        {
            SettingsManager.get.selector = null;
            SettingsManager.get.type_of_setting = null;
            Select();
            Arrows(false);
        }

    }

    public void Set(int nom)
    {
        nomber = nom;
        Select(nom);
        Debug.Log("Значение = " + nomber);
        //Применить настройки;
    }

    public void Select(int nom)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < nom)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }

    public void Select()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < nomber)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }

    public void SwitchSelect(bool plus)
    {
        if (plus)
        {
            nomber++;
            if (nomber > 9)
                nomber = 9;
        }
        else
        {
            nomber--;
            if (nomber < 0)
                nomber = 0;
        }
        Select();
    }

    private void Arrows(bool enable)
    {
        arrows[0].gameObject.SetActive(enable);
        arrows[1].gameObject.SetActive(enable);
    }
}
