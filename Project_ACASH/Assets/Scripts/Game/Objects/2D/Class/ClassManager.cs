using Vkimow.Tools.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Objects._2D.ClassRoom
{
    public class ClassManager : Singleton<ClassManager>
    {
        public ClassAgent[] Classes => _classes;

        private ClassAgent[] _classes;

        public void SetLevel()
        {
            _classes = ClassAgentsHolder.Instance.Classes;

            foreach (var classAgent in _classes)
            {
                classAgent.SetLevel();
            }
        }
    }
}
