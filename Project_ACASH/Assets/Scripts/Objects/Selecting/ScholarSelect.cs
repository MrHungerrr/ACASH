using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarSelect : ObjectSelect
{
    //========================================================================================================
    //Возможность выбрать объект
    private Scholar Scholar;
    [HideInInspector]
    public bool selectable { get; private set; } = false;


    protected override void Awake()
    {
    }


    public void Setup(Scholar scholar)
    {
        Scholar = scholar;
        SetSelect();
        selectable = false;
    }

    public void Reset()
    {
        Selectable(true);
    }


    public override void Select()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_EmissionColor", SelectInfo.SELECT_COL);
        }
    }

    public override void Deselect()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_EmissionColor", SelectInfo.COL);
        }
    }

    public override bool CanISelect()
    {
        return selectable;
    }


    public void Selectable(bool option)
    {
        if (Scholar.Active)
        {
            selectable = option;

            if (!option)
            {
                Deselect();
            }
        }
    }
}
