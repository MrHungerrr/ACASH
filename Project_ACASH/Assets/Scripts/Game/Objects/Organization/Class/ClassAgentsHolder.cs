using Vkimow.Unity.Tools.Search;
using Vkimow.Unity.Tools.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Objects.Organization.ClassRoom
{
    public class ClassAgentsHolder : MonoSingleton<ClassAgentsHolder>
    #region IInitialization 
#if UNITY_EDITOR
 , IInitialization
#endif
    #endregion
    {
        public ClassAgent[] Classes => _classes;

        [SerializeField] private ClassAgent[] _classes;

        #region Initialization 
#if UNITY_EDITOR
        public bool AutoInitializate => true;

        public void Initializate()
        {
            _classes = SIC<ClassAgent>.ComponentsDown(transform).OrderBy(x => x.gameObject.name).ToArray();
        }
#endif
        #endregion
    }
}
