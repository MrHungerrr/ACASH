using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHoldInteraction : MonoBehaviour, IHoldInteraction
#region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
#endregion
{
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _endEvent;


    #region Initialization
#if UNITY_EDITOR
    public bool AutoInitializate => true;

    public void Initializate()
    {
        if (_startEvent.GetPersistentEventCount() == 0)
        {
            throw new Exception("Пустое поле _startEvent");
        }

        if (_endEvent.GetPersistentEventCount() == 0)
        {
            throw new Exception("Пустое поле _endEvent");
        }
    }

#endif
    #endregion


    public void Interact()
    {
        _startEvent.Invoke();
    }

    public void InteractEnd()
    {
        _endEvent.Invoke();
    }
}

