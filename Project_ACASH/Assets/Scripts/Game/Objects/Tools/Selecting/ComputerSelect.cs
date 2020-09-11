using UnityEngine;
using System.Collections;
using TMPro;
using UnityTools.Math;

public class ComputerSelect : ObjectSelect
{


    public override bool CanISelect()
    {
        if (BaseGeometry.LookingAngle2D(transform, Player.Instance.Move.Position()) < 70)
        {
            return base.CanISelect();
        }
        else
        {
            return false;
        }
    }
}
