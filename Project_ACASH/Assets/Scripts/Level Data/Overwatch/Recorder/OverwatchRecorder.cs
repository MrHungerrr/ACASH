using System;
using Single;
using UnityEngine;
using GameTime.Action;
using Overwatch.Watchable;
using Overwatch.Memory;
using MultiTasking;

namespace Overwatch.Record
{
    public class OverwatchRecorder : Singleton<OverwatchRecorder>
    {
        public OverwatchRecorderBuffer Buffer { get; private set; }


        private int _framesAmount;
        private int _index;


        public void SetLevel()
        {
            Buffer = new OverwatchRecorderBuffer();
            _framesAmount = 0;
        }


        public void RecordStart()
        {
            _index = 0;

            float timeToCapture = 1.0f / OverwatchData.FPS;

            Action capture = () =>
            {
                Debug.Log("Capturing!");
                Capture();
            };

            Func<bool> disableReason = () =>
            {
                //return !ExamManager.Instance.Exam;
                return false;
            };

            ActionPerTime.Create(timeToCapture, capture, disableReason);
        }


        public void Capture()
        {
            Buffer.StartCapture();

            IWatchableInfo info;

            for (int i = 0; i < WatchableManager.Wathcables.Count; i++)
            {
                if(WatchableManager.Wathcables[i].Enabled)
                {
                    info = WatchableManager.Wathcables[i].Capture();
                    Buffer.AddInfo(info);
                }
            }

            Buffer.EndCapture();
        }

        public void RecordDone()
        {
            Buffer.Save();
        }



        public void Save(OverwatchMemoryCell cell)
        {
            _framesAmount += cell.Size;
            var number = _index;
            _index++;

            Action save = () =>
            {
                OverwatchData.Save(cell, number);
            };

            ThreadTaskQueuer.AddTask(save);
        }
    }
}