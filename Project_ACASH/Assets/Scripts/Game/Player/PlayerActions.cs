using System;
using UnityEngine;
using System.Collections;

public class PlayerActions
{
    public bool Doing => _doing;


    private bool _doing = false;
    private IInteraction _interactObject;


    public void Act()
    {
        if (Player.Instance.Select.SelectingIsActive)
        {
            if (Player.Instance.Select.SelectedObject.TryGetComponent<IInteraction>(out _interactObject))
            {
                Interact();
            }
        }
    }

    public void Stop()
    {
        if (_doing)
        {
            _doing = false;
            InteractEnd();
        }
    }


    private void Interact()
    {
        _interactObject.Interact();

        if (_interactObject is IHoldInteraction)
        {
            _doing = true;
        }
    }

    private void InteractEnd()
    {
        ((IHoldInteraction)_interactObject).InteractEnd();
    }
}
