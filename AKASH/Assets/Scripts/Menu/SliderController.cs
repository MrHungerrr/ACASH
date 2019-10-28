using UnityEngine;
using System.Collections;


public class SliderController: MonoBehaviour
{
    private SliderPiece[] pieces = new SliderPiece[10];
    private GameObject[] arrows = new GameObject[2];
    [SerializeField]
    private bool settings;
    private int nomber = 0;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
            pieces[i].nomber = i;
        }

        if (settings)
        {
            arrows[0] = transform.Find("Left_Arrow").gameObject;
            arrows[1] = transform.Find("Right_Arrow").gameObject;
        }

        Enable(false);
    }

    public void Enable(bool u)
    {
        if (settings)
        {
            if (u)
            {
                Menu.get.slider = this;
                Arrows(true);
            }
            else
            {
                Menu.get.slider = null;
                Select();
                Arrows(false);
            }
        }
    }

    public void Set(int nom)
    {
        nomber = nom;

        if (!settings)
        {
            Select(nom);
        }

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
    }

    private void Arrows(bool enable)
    {
        arrows[0].SetActive(enable);
        arrows[1].SetActive(enable);
    }
}
