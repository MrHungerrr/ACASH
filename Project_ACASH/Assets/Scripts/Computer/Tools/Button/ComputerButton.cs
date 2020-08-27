using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UIElements;

namespace Computers
{
    public class ComputerButton : MonoBehaviour
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {
        public ComputerButtonSelect Select => _select;
        public ComputerButtonCollider Collider => _collider;
        public bool IsActive => gameObject.activeInHierarchy;



        [SerializeField] private UnityEvent _event;
        [SerializeField] private ComputerButtonSelect _select;
        [HideInInspector] [SerializeField] private ComputerButtonCollider _collider;




        #region Initialization
#if UNITY_EDITOR
        public bool AutoInitializate => true;

        public void Initializate()
        {
            if (_event.GetPersistentEventCount() == 0)
            {
                throw new Exception("Пустое поле _event");
            }

            if(_select.Images.Length == 0)
            {
                throw new Exception("Пустое поле _select");
            }

            _collider = new ComputerButtonCollider(GetComponent<RectTransform>());
        }

#endif
        #endregion



        public void Execute()
        {
            _event.Invoke();
        }
    }
}
