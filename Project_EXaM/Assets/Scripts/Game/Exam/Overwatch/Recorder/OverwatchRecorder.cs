using System;
using Vkimow.Tools.Single;
using UnityEngine;
using GameTime.Action;
using Overwatch.Memorable;
using Overwatch.Memory;
using MultiTasking;

namespace Overwatch.Record
{
    public class OverwatchRecorder : Singleton<OverwatchRecorder>
    {
        private OverwatchRecorderBuffer _buffer;
        private int _index;

        public void Reset()
        {
            _buffer = new OverwatchRecorderBuffer();
        }


        public void RecordStart()
        {
            _index = 0;

            Action capture = () =>
            {
                Capture();
            };

            Func<bool> disableReason = () =>
            {
                return OverwatchInfo.RecordIsDone;
            };

            ActionPerTime.Create(OverwatchInfo.TIME_TO_NEXT_MOMENT, capture, disableReason);
        }


        public void Capture()
        {
            _buffer.StartCapture();

            IMemorableInfo info;

            for (int i = 0; i < MemorableManager.Wathcables.Count; i++)
            {
                if(MemorableManager.Wathcables[i].IsHere)
                {
                    info = MemorableManager.Wathcables[i].Capture();
                    _buffer.AddInfo(info);
                }
            }

            _buffer.EndCapture();
        }

        public void RecordDone()
        {
            _buffer.Save();
        }



        public void Save(OverwatchMemoryCell cell)
        {
            OverwatchInfo.SetFramesAmount(OverwatchInfo.MomentsAmount + cell.Size);
            OverwatchInfo.SetCellsAmount(OverwatchInfo.CellsAmount + 1);
            var number = _index;
            _index++;

            Action save = () =>
            {
                OverwatchDataManager.Save(cell, number);
            };

            ThreadTaskQueuer.AddTask(save);
        }
    }
}