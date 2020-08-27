using System;
using System.Collections.Generic;
using Single;
using GameTime;
using Overwatch.Memorable;
using MultiTasking;
using Overwatch.Memory;
using UnityEngine;
using System.Linq;

namespace Overwatch.Record
{
    public class OverwatchRecorderBuffer
    {
        private OverwatchMemoryCell _overwatchMemory;
        private OverwatchMemoryMoment _moment;


        public OverwatchRecorderBuffer()
        {
            Reset();
        }

        private void Reset()
        {
            _overwatchMemory = new OverwatchMemoryCell(OverwatchInfo.BUFFER_SIZE);
        }


        public void StartCapture()
        {
            _moment = new OverwatchMemoryMoment(TimeManager.Instance.TimeInSec);
        }


        public void EndCapture()
        {
            _overwatchMemory.Add(_moment);

            if (_overwatchMemory.Size == OverwatchInfo.BUFFER_SIZE)
            {
                Save();
                Reset();
            }
        }




        #region Add Info
        public void AddInfo(in IMemorableInfo info)
        {
            _moment.Add(info);
        }
        #endregion


        public void Save()
        {
            if (_overwatchMemory.Size != 0)
            {
                Debug.Log("Overwatch Data Saving");
                OverwatchRecorder.Instance.Save(_overwatchMemory);
            }
        }
    }
}
