using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarSelect : ObjectSelect
{
    public bool Selectable { get; private set; } = false;


    private Scholar _scholar;


    public void Setup(Scholar scholar)
    {
        _scholar = scholar;
        SetupMaterials();
    }

    public void Reset()
    {
        SetSelectable(true);
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
        return Selectable;
    }


    public void SetSelectable(bool option)
    {
        if (_scholar.Active)
        {
            Selectable = option;

            if (!option)
            {
                Deselect();
            }
        }
    }
}
