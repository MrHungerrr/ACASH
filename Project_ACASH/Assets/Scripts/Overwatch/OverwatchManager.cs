using Overwatch.Read;
using Overwatch.Record;
using Overwatch.Memorable;
using Overwatch.Player;
using UnityEngine;

namespace Overwatch
{
    public static class OverwatchManager
    {

        public static void SetLevel()
        {
            OverwatchRecorder.Instance.SetLevel();
            MemorableManager.SetLevel();
            OverwatchInfo.Reset();
        }


        public static void Update()
        {
            if (OverwatchInfo.RecordIsDone)
            {
                OverwatchPlayer.Instance.Update();
                OverwatchPlayerController.Instance.MyUpdate();
            }
        }


        public static void RecordStart()
        {
            OverwatchRecorder.Instance.RecordStart();
        }

        public static void RecordStop()
        {
            OverwatchRecorder.Instance.RecordDone();
            OverwatchInfo.StopRecord();
            MemorableManager.CutAll();
            OverwatchPlayer.Instance.Setup();
        }
    }
}
