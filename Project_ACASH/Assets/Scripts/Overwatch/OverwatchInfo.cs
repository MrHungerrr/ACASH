using Overwatch.Record;
using Overwatch.Memorable;
using System.Security.Principal;

namespace Overwatch
{
    public static class OverwatchInfo
    {
        public const int FPS = 30;
        private const int SECONDS_IN_BUFFER = 3;
        public const int BUFFER_SIZE = FPS * SECONDS_IN_BUFFER;
        public const float TIME_TO_NEXT_MOMENT = 1f / FPS;


        public static bool RecordIsDone { get; private set; }
        public static int MomentsAmount { get; private set; }
        public static int CellsAmount { get; private set; }


        public static void Reset()
        {
            RecordIsDone = false;
            MomentsAmount = 0;
            CellsAmount = 0;
        }

        public static void SetFramesAmount(int amount)
        {
            MomentsAmount = amount;
        }

        public static void SetCellsAmount(int amount)
        {
            CellsAmount = amount;
        }

        public static void StopRecord()
        {
            RecordIsDone = true;
        }
    }
}
