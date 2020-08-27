using System;
using UnityEngine;
using System.Linq;
using Single;



namespace Supervision.Cameras
{
    public class SupervisionCamerasHolder : MonoSingleton<SupervisionCamerasHolder>
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {
        public Camera[] Cameras => _cameras;

        [SerializeField] private Camera[] _cameras;


        #region Initialization
#if UNITY_EDITOR
        public bool AutoInitializate => true;

        public void Initializate()
        {
            var cameras = FindObjectsOfType<Camera>();

            _cameras = cameras.Where(x => x.tag == "Supervision Camera").Where(x => x.gameObject.scene == gameObject.scene).OrderBy(x => x.name).ToArray();

            if (_cameras == null)
            {
                throw new Exception("Пустое поле _cameras");
            }

            if (_cameras.Length > 9)
            {
                throw new Exception("Слишком много SupervisionCameras");
            }
        }
#endif
        #endregion
    }
}