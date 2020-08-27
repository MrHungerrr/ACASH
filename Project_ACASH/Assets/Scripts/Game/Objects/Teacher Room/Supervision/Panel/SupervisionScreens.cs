using System;
using System.Collections.Generic;
using Overwatch.Player;
using System.Linq;
using Searching;
using Single;
using Supervision.Cameras;
using UnityEngine;
using UnityEngine.UI;

namespace Supervision.Panel
{
    public class SupervisionScreens : MonoSingleton<SupervisionScreens>
    {
        [SerializeField] private RawImage[] _screens;

        [ContextMenu("Reset Screens")]
        private void SetScreens()
        {
            var screens = SIC<RawImage>.ComponentsDown(transform).OrderBy(x => x.transform.parent.parent.name).ToArray();
            _screens = screens;
        }

        public void SetCameras(Camera[] cameras)
        {
            int i;

           for(i = 0; i < cameras.Length; i++)
            {
                var renderTexture = new RenderTexture(SupervisionManager.Instance.RenderTextureReference);
                cameras[i].targetTexture = renderTexture;
                _screens[i].texture = renderTexture;
            }

           for(i = cameras.Length; i < _screens.Length; i++)
            {
                _screens[i].enabled = false;
            }
        }
    }
}
