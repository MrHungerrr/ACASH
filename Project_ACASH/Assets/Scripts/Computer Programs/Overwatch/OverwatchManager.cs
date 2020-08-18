using Overwatch.Read;
using Overwatch.Record;
using Overwatch.Watchable;
using Overwatch.Player;

namespace Overwatch
{
    public static class OverwatchManager
    {

        public static void SetLevel()
        {
            OverwatchRecorder.Instance.SetLevel();
            WatchableManager.SetLevel();
            OverwatchInfo.Reset();
        }


        public static void Update()
        {
            if (OverwatchInfo.RecordIsDone)
            {
                OverwatchPlayerController.Instance.Update();
                OverwatchPlayer.Instance.Update();
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
            WatchableManager.CutAll();
            OverwatchPlayer.Instance.Setup();
        }
    }
}
