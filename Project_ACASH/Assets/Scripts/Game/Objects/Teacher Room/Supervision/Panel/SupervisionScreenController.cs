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
    public class SupervisionScreenController : MonoSingleton<SupervisionScreenController>
    {
        [SerializeField] private RawImage _screen;
        private int _index;


        public void SetLevel()
        {
            SetCamera(0);
            _index = 0;
        }

        private void SetCamera(int index)
        {
            SetCamera(SupervisionManager.Instance.Cameras[index]);
        }

        private void SetCamera(Camera camera)
        {
            var renderTexture = new RenderTexture(SupervisionManager.Instance.RenderTextureReference);
            camera.targetTexture = renderTexture;
            _screen.texture = renderTexture;
        }


        public void Next()
        {
            _index++;

            if (_index >= SupervisionManager.Instance.Cameras.Length)
                _index = 0;

            SetCamera(_index);
        }

        public void Previous()
        {
            _index--;

            if (_index < 0)
                _index = SupervisionManager.Instance.Cameras.Length - 1;

            SetCamera(_index);
        }
    }
}
