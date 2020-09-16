using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vkimow.Unity.Tools.Single;
using UnityEngine.Events;

namespace Exam
{
    [RequireComponent(typeof(ObjectSelect))]
    class ExamStarter: MonoSingleton<ExamStarter>, IInteraction
    {
        private Action _action;
        private bool _active = true;


        public void SetLevel()
        {
            Reset();
            _action = null;
        }

        public void Reset()
        {
            _active = true;
        }

        public void Interact()
        {
            if (_active)
            {
                _action.Invoke();
                _active = false;
            }
        }

        public void AddListener(Action listener)
        {
            _action += listener;
        }
    }
}
