using UnityEngine;
using System.Collections;

public class PlayerActions
{
    public bool doing { get; private set; } = false;
    private IInteraction interactObject;


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
        if (Player.Instance.Select.selected)
        {
            if (Player.Instance.Select.selected_obj.TryGetComponent<IInteraction>(out interactObject))
            {
                interactObject.Interaction();
            }
        }
    }
}
