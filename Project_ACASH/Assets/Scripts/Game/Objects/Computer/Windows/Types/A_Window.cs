using UnityTools.Search;
using UnityEngine;

namespace Computers
{
    public abstract class A_Window: MonoBehaviour
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {
        public ComputerButton[] Buttons => _buttons;

        [SerializeField] protected ComputerButton[] _buttons;

        #region Initialization
#if UNITY_EDITOR
        public bool AutoInitializate => true;

        public void Initializate()
        {
            _buttons = SIC<ComputerButton>.ComponentsDown(transform);

            if (_buttons == null || _buttons.Length == 0)
            {
                throw new System.Exception("Пустое поле _buttons");
            }
        }
#endif
        #endregion

        public abstract void Setup();

        public abstract void Stop();
    }
}
