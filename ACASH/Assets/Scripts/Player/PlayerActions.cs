using UnityEngine;
using System.Collections;

public class PlayerActions
{
    public bool doing = false;
    private I_Interaction interact_obj;


    public void Update()
    {
        if (doing)
            Act();
    }


    public void Doing(bool option)
    {
        doing = option;
    }


    private void Act()
    {
        if (Player.get.Select.selected)
        {
            if (Player.get.Select.selected_obj.TryGetComponent<I_Interaction>(out interact_obj))
            {
                interact_obj.Interaction();
            }
        }
    }
}
