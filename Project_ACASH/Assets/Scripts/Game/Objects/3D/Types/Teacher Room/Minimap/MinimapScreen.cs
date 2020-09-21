using Minimap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Unity.Tools.Single;
using UnityEngine.UI;


namespace Minimap
{
    public class MinimapScreen : MonoSingleton<MinimapScreen>
    {
        [SerializeField] private RawImage _rawImage;

        public void SetCamera(Camera camera)
        {
            var renderTexture = new RenderTexture(MinimapManager.Instance.ReferenceRenderTexture);
            camera.targetTexture = renderTexture;
            _rawImage.texture = renderTexture;
        }
    }
}