using UnityEngine;
using System.Collections;
using TMPro;

public class ElevatorSelect : ObjectSelect, I_ObjectSelect
{
    private void Awake()
    {
        SetSelect();
    }


    public override bool CanISelect()
    {
        if (ElevatorController.get.ready)
        {
            return base.CanISelect();
        }
        else
        {
            return false;
        }
    }
}
