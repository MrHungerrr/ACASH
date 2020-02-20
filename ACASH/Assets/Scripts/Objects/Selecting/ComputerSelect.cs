using UnityEngine;
using System.Collections;
using TMPro;

public class ComputerSelect : ObjectSelect, I_ObjectSelect
{
    private void Awake()
    {
        SetSelect();
    }


    public override bool CanISelect()
    {
        if (BaseGeometry.LookingAngle(transform, Player.get.Move.Position()) < 70)
        {
            return base.CanISelect();
        }
        else
        {
            return false;
        }
    }
}
