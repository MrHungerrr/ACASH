using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
using UnityEngine.UI;


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
