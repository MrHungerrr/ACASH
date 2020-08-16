using System;
using System.Collections.Generic;
using Single;
using GameTime;
using Overwatch.Watchable;
using MultiTasking;
using Overwatch.Memory;
using UnityEngine;
using System.Linq;

namespace Overwatch.Record
{
    public class OverwatchRecorderBuffer
    {
        private const int BUFFER_SIZE = OverwatchData.SECONDS_IN_BUFFER * OverwatchData.FPS;


        private OverwatchMemoryCell _overwatchMemory;
        private OverwatchMemoryMoment _moment;


        public OverwatchRecorderBuffer()
        {
            Reset();
        }

        private void Reset()
        {
            _overwatchMemory = new OverwatchMemoryCell(BUFFER_SIZE);
        }


        public void StartCapture()
        {
            _moment = new OverwatchMemoryMoment(TimeManager.Instance.TimeInSec);
        }


        public void EndCapture()
        {
            _overwatchMemory.Add(_moment);

            if (_overwatchMemory.Size == BUFFER_SIZE)
            {
                Save();
                Reset();
            }
        }




        #region Add Info
        public void AddInfo(in IWatchableInfo info)
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
