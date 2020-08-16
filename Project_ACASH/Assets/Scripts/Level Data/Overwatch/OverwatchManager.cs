using Overwatch.Record;
using Overwatch.Watchable;

namespace Overwatch
{
    public static class OverwatchManager
    {
        public static void SetLevel()
        {
            OverwatchRecorder.Instance.SetLevel();
            WatchableManager.SetLevel();
            OverwatchRecorder.Instance.RecordStart();
        }
    }
}
