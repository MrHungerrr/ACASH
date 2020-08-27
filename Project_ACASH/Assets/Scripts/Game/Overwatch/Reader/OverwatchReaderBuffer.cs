using System.Collections.Generic;
using Overwatch.Memory;
using Single;
using MultiTasking;
using Overwatch.Record;
using UnityEngine.SocialPlatforms;
using GameTime;
using UnityEngine;

namespace Overwatch.Read
{
    public class OverwatchReaderBuffer
    {
        public OverwatchMemoryCellLoader Memory => _memory[_selectedIndex];

        private const int CELLS_COUNT = 2;


        private int _selectedIndex;
        private int _startIndex;
        private int _lastIndex;

        private OverwatchMemoryCellLoader[] _memory;


        public OverwatchReaderBuffer()
        {
            Reset();
        }


        public void Reset()
        {
            _memory = new OverwatchMemoryCellLoader[OverwatchInfo.CellsAmount];

            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = new OverwatchMemoryCellLoader(i);
            }

            NewCellsCalculate(0);

            for (int i = _startIndex; i <= _lastIndex; i++)
            {
                Debug.Log(i);

                if (!_memory[i].IsLoaded)
                    _memory[i].Load();

                _memory[i].SetUsing(true);
            }
        }

        public void SetCell(int index)
        {
            Debug.Log($"I'm select cell with index = {index}");

            UnselectPreviousCells();

            NewCellsCalculate(index);

            for (int i = _startIndex; i <= _lastIndex; i++)
            {
                Debug.Log(i);

                if (!_memory[i].IsLoaded)
                    _memory[i].Load();

                _memory[i].SetUsing(true);
            }
        }

        private void UnselectPreviousCells()
        {
            for (int i = _startIndex; i <= _lastIndex; i++)
            {
                _memory[i].SetUsing(false);
            }
        }

        private void NewCellsCalculate(int index)
        {
            _selectedIndex = index;

            _startIndex = _selectedIndex - CELLS_COUNT;

            if (_startIndex < 0)
                _startIndex = 0;

            _lastIndex = _selectedIndex + CELLS_COUNT;

            if (_lastIndex >= OverwatchInfo.CellsAmount)
                _lastIndex = OverwatchInfo.CellsAmount - 1;
        }
    }
}