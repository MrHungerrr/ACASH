using UnityEngine;
using System.Collections;
using TMPro;

public class DoorSelect : ObjectSelect
{

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool CanISelect()
    {
        if (!GetComponent<Door>().locked)
        {
            return base.CanISelect();
        }
        else
        {
            return false;
        }
    }
}
