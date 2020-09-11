using System;
using System.IO;
using System.Collections.Generic;
using Overwatch.Memory;
using Overwatch.Memorable;
using Overwatch.Read;
using UnityTools.Single;
using UnityEngine;


namespace Overwatch.Player
{
    public class OverwatchPlayerController : Singleton<OverwatchPlayerController>
    {
        public enum PlayerState
        {
            Playing,
            Rewinding,
        }


        private bool _isActive;
        private PlayerState _state;
        private float _timeToNextMoment;
        private int _deltaRewinding;
        private int _deltaPlaying;


        public void Reset()
        {
            _isActive = false;
            _timeToNextMoment = 0;
            _state = PlayerState.Playing;
        }


        public void Play()
        {
            _timeToNextMoment = 0;
            _isActive = true;
            OverwatchPlayer.Instance.OnEndOfMemory += Stop;
        }

        public void Stop()
        {
            OverwatchPlayer.Instance.OnEndOfMemory -= Stop;
            Reset();
        }

        public void SetDeltaPlaying(int deltaMoments)
        {
            _deltaPlaying = deltaMoments;
        }

        public void SetDeltaRewinding(int deltaMoments)
        {
            _deltaRewinding = deltaMoments;
        }

        public void SetState(PlayerState state)
        {
            _state = state;
        }


        public void Update()
        {
            if (_isActive)
                switch(_state)
                {
                    case PlayerState.Playing:
                        {
                            Playing();
                            break;
                        }
                    case PlayerState.Rewinding:
                        {
                            Rewinding();
                            break;
                        }
                }
        }

        private void Playing()
        {
            if (_timeToNextMoment > 0)
            {
                _timeToNextMoment -= Time.deltaTime;
            }
            else if (OverwatchPlayer.Instance.IsLoaded)
            {
                _timeToNextMoment = OverwatchInfo.TIME_TO_NEXT_MOMENT;
                RewindMoments(_deltaPlaying);
            }
        }

        private void Rewinding()
        {
            _timeToNextMoment -= Time.deltaTime;

            if (_timeToNextMoment <= 0)
            {
                _timeToNextMoment = OverwatchInfo.TIME_TO_NEXT_MOMENT;
                RewindMoments(_deltaRewinding);
            }
        }

        private void RewindMoments(int deltaMoments)
        {
            int nextIndex = OverwatchPlayer.Instance.MomentIndex + deltaMoments;
            OverwatchPlayer.Instance.Select(nextIndex);
        }
    }
}