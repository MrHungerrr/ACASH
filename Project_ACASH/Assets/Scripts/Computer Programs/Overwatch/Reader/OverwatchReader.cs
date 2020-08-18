using System;
using System.IO;
using System.Collections.Generic;
using Overwatch.Memory;
using Overwatch.Watchable;
using Single;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Overwatch.Read
{
    public class OverwatchReader
    {
        public bool IsLoaded => _buffer.Memory.IsLoaded;

        private int CellSelectedIndex => _momentIndex / OverwatchInfo.BUFFER_SIZE;
        private int MomentLocalIndex => _momentIndex % OverwatchInfo.BUFFER_SIZE;


        private OverwatchReaderBuffer _buffer;
        private int _momentIndex;


        public OverwatchReader()
        {
            _buffer = new OverwatchReaderBuffer();
            _momentIndex = 0;
        }


        public OverwatchMemoryMoment GetMoment()
        {
            if (IsLoaded)
                return _buffer.Memory.Cell[MomentLocalIndex];
            else
                throw new Exception("Cell не загружена");
        }


        public void Select(int momentIndex)
        {
            int previousCellIndex = CellSelectedIndex;

            _momentIndex = momentIndex;

            Debug.Log($"Moment Index = {momentIndex}");

            if (CellSelectedIndex != previousCellIndex)
            {
                _buffer.SetCell(CellSelectedIndex);
            }
        }
    }
}