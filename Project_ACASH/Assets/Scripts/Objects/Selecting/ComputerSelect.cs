using UnityEngine;
using System.Collections;
using TMPro;

public class ComputerSelect : ObjectSelect
{


    public override bool CanISelect()
    {
        if (BaseGeometry.LookingAngle2D(transform, Player.get.Move.Position()) < 70 && Player.get.Select.active.GiveMeChance())
        {
            return base.CanISelect();
        }
        else
        {
            return false;
        }
    }
}
