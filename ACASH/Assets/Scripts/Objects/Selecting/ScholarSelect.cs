using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarSelect : ObjectSelect
{
    //========================================================================================================
    //Возможность выбрать объект
    private bool selectable = true;


    public void SetScholarSelect()
    {
        SetSelect();
        Selectable(true);
    }



    public override void Select()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.select_col);
        }
    }

    public override void Deselect()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.col);
        }
    }

    public override bool CanISelect()
    {
        return selectable;
    }


    public void Selectable(bool option)
    {
        selectable = option;

        if (!option)
        {
            Deselect();
        }
    }
}
