using System;
using System.Collections.Generic;
using Overwatch.Player;
using Single;

namespace Overwatch.Panel
{
    public class OverwatchButtonController: MonoSingleton<OverwatchButtonController>
    {
        public void StartPlaying()
        {
            OverwatchPlayerController.Instance.SwitchState(OverwatchPlayerController.PlayerState.Playing);
            OverwatchPlayerController.Instance.SetDeltaPlaying(1);
            OverwatchPlayerController.Instance.Play();
        }

        public void StartForwardRewind()
        {
            OverwatchPlayerController.Instance.SwitchState(OverwatchPlayerController.PlayerState.Rewinding);
            OverwatchPlayerController.Instance.SetDeltaRewinding(15);
            OverwatchPlayerController.Instance.Play();
        }

        public void StartBackwardRewind()
        {
            OverwatchPlayerController.Instance.SwitchState(OverwatchPlayerController.PlayerState.Rewinding);
            OverwatchPlayerController.Instance.SetDeltaRewinding(-15);
            OverwatchPlayerController.Instance.Play();
        }

        public void Stop()
        {
            OverwatchPlayerController.Instance.Stop();
        }
    }
}
