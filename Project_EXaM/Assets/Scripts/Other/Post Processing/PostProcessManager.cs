using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace PostProcessing
{

    public static class PostProcessManager
    {
        private static VolumeProfile _volume;
        private static DepthOfField _depthOfField;
        private static ColorAdjustments _colorAdjustments;


        public static void Setup()
        {
            _volume = GameObject.FindObjectOfType<Volume>().profile;
        }

        public static void SetFocusDistance(float value)
        {
            if (_volume.TryGet(out _depthOfField))
                _depthOfField.focusDistance.value = value;
        }

        public static float GetFocuseDistance()
        {
            if (_volume.TryGet(out _depthOfField))
                return _depthOfField.focusDistance.value;
            else
                return 0;
        }

        public static void SetContrast(float value)
        {
            if (_volume.TryGet(out _colorAdjustments))
                _colorAdjustments.contrast.value = value;
        }

        public static float GetContrast()
        {
            if (_volume.TryGet(out _colorAdjustments))
                return _colorAdjustments.contrast.value;
            else
                return 0;
        }

        public static void SetSaturation(float value)
        {
            if (_volume.TryGet(out _colorAdjustments))
                _colorAdjustments.saturation.value = value;
        }

        public static float GetSaturation()
        {
            if (_volume.TryGet(out _colorAdjustments))
                return _colorAdjustments.saturation.value;
            else
                return 0;
        }
    }
}
