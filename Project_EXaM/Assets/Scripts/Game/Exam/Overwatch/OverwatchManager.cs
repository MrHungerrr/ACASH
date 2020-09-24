using Overwatch.Read;
using Overwatch.Record;
using Overwatch.Memorable;
using Overwatch.Player;
using UnityEngine;
using Exam;
using UnityEditor.PackageManager.Requests;

namespace Overwatch
{
    public static class OverwatchManager
    {

        public static void Setup()
        {
            OverwatchDataManager.Setup();
            ExamManager.Instance.OnExamStart += RecordStart;
            ExamManager.Instance.OnExamEnd -= RecordStop;
        }

        public static void SetupSchool()
        {
            MemorableManager.Reset();
        }

        private static void Reset()
        {
            OverwatchRecorder.Instance.Reset();
            OverwatchDataManager.Reset();
            OverwatchInfo.Reset();
        }


        public static void Update()
        {
            if (OverwatchInfo.RecordIsDone)
            {
                OverwatchPlayer.Instance.Update();
                OverwatchPlayerController.Instance.Update();
            }
        }


        public static void RecordStart()
        {
            Reset();
            OverwatchRecorder.Instance.RecordStart();
        }

        public static void RecordStop()
        {
            OverwatchRecorder.Instance.RecordDone();
            MemorableManager.CutAll();
            OverwatchPlayer.Instance.Setup();
            OverwatchInfo.StopRecord();
        }
    }
}
