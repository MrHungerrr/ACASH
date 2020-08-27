using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
using Supervision.Panel;
using Supervision.Cameras;

namespace Supervision
{
    public class SupervisionManager : MonoSingleton<SupervisionManager>
    {
        public Camera[] Cameras { get; private set; }
        public RenderTexture RenderTextureReference => _reference;


        [SerializeField] private RenderTexture _reference;



        public void SetLevel()
        {
            Cameras = SupervisionCamerasHolder.Instance.Cameras;
            SupervisionScreens.Instance.SetCameras(Cameras);
        }
    }
}
