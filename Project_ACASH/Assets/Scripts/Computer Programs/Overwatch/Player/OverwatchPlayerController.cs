using System;
using System.IO;
using System.Collections.Generic;
using Overwatch.Memory;
using Overwatch.Watchable;
using Overwatch.Read;
using Single;
using UnityEngine;


namespace Overwatch.Player
{
    public class OverwatchPlayerController : Singleton<OverwatchPlayerController>
    {
        public bool _isPlaying;
        private float _timeToNextFrame;

       
        public void Reset()
        {
            _isPlaying = false;
            _timeToNextFrame = 0;
        }

        public void Update()
        {
            if (_isPlaying)
                Playing();
        }


        private void Playing()
        {
            _timeToNextFrame -= Time.deltaTime;

            if (_timeToNextFrame <= 0)
            {
                _timeToNextFrame = OverwatchInfo.TIME_TO_NEXT_MOMENT;
                Next();
            }
        }


        public void Play()
        {
            _timeToNextFrame = 0;
            _isPlaying = true;
            OverwatchPlayer.Instance.OnEndOfMemory += Stop;
        }

        public void Stop()
        {
            OverwatchPlayer.Instance.OnEndOfMemory -= Stop;
            _isPlaying = false;
        }


        public void Next()
        {
            int nextIndex = OverwatchPlayer.Instance.MomentIndex + 1;
            OverwatchPlayer.Instance.Select(nextIndex);
        }

        public void Previous()
        {
            int nextIndex = OverwatchPlayer.Instance.MomentIndex + 1;
            OverwatchPlayer.Instance.Select(nextIndex);
        }

        public void Rewind(int deltaSeconds)
        {
            int indexDifference = OverwatchInfo.FPS * deltaSeconds;
            int nextIndex = OverwatchPlayer.Instance.MomentIndex + indexDifference;
            OverwatchPlayer.Instance.Select(nextIndex);
        }
    }
}