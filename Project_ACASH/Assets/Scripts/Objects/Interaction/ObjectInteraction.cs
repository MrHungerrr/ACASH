using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInteraction : MonoBehaviour, IInteraction
#region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
#endregion
{
    [SerializeField] private UnityEvent _event;


    #region Initialization
#if UNITY_EDITOR
    public bool AutoInitializate => true;

    public void Initializate()
    {
        if (_event.GetPersistentEventCount() == 0)
        {
            throw new Exception("Пустое поле _event");
        }
    }

#endif
    #endregion


    public void Interact()
    {
        _event.Invoke();
    }
}

