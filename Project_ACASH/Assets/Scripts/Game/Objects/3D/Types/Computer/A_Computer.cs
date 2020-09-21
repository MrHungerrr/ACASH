using UnityEngine;


namespace Computers
{
    public abstract class A_Computer : MonoBehaviour
    {
        public ComputerWindows Windows => _windows;
        public ComputerSounds Sound { get; private set; }


        [SerializeField] private ComputerWindows _windows;


        public virtual void Setup()
        {
            Sound = new ComputerSounds(this, gameObject);
            Windows.Setup();
        }
    }
}