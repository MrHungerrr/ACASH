using System.Collections;
using System.Collections.Generic;
using UnityTools.Single;
using UnityEngine;

namespace Minimap
{
    public class MinimapManager : MonoSingleton<MinimapManager>
    {
        public RenderTexture ReferenceRenderTexture => _referenceRenderTexture;


        [SerializeField] private RenderTexture _referenceRenderTexture;


        public void SetLevel()
        {
            var camera = FindMinimapCamera();
            SetMinimapCamera(camera);
        }

        private Camera FindMinimapCamera()
        {
            return GameObject.FindGameObjectWithTag("Minimap Camera").GetComponent<Camera>();
        }

        private void SetMinimapCamera(Camera camera)
        {
            MinimapScreen.Instance.SetCamera(camera);
        }
    }
}
