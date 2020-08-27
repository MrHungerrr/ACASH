using UnityEngine;

namespace Computers
{
    public class TeacherComputer : A_Computer
    {
        [SerializeField] private ComputerControl _controller;

        public override void Setup()
        {
            base.Setup();
            _controller.SetLevel(this);
        }
    }
}