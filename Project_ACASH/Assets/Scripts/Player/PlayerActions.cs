using UnityEngine;
using System.Collections;

public class PlayerActions
{

    public bool Doing => _doing;

    private bool _doing = false;
    private IInteraction _interactObject;


    public void Update()
    {
        if (Doing)
            Act();
    }


    public void SetDoing(bool option)
    {
        _doing = option;
    }


    private void Act()
    {
        if (Player.Instance.Select.SelectingIsActive)
        {
            if (Player.Instance.Select.SelectedObject.TryGetComponent<IInteraction>(out _interactObject))
            {
                _interactObject.Interaction();
            }
        }
    }
}
