using System;
using System.Collections;
using System.Collections.Generic;
using MultiTasking;
using GameTime;


namespace Overwatch.Memory
{
    public class OverwatchMemoryCellLoader
    {
        public OverwatchMemoryCell Cell => _cell;
        public bool IsLoaded => _isLoaded;


        private const float TIME_TO_UNLOAD = 15f;

        private OverwatchMemoryCell _cell;
        private readonly int _index;
        private bool _isLoaded;

        private float _timeToUnload;
        private bool _isUsing;



        public OverwatchMemoryCellLoader(int index)
        {
            _isLoaded = false;
            _index = index;
        }

        public void Load()
        {
            _isLoaded = false;

            Action load = () =>
            {
                _cell = OverwatchDataSaver.Load(_index);
                _isLoaded = true;

                UpdateManager.Instance.AddUpdate(TimeUpdate);
            };

            ThreadTaskQueuer.AddTask(load);
        }

        public void Load(Action ActionAfterLoad)
        {
            _isLoaded = false;

            Action load = () =>
            {
                _cell = OverwatchDataSaver.Load(_index);
                _isLoaded = true;
                ActionAfterLoad();

                UpdateManager.Instance.AddUpdate(TimeUpdate);
            };

            ThreadTaskQueuer.AddTask(load);
        }

        public void Unload()
        {
            _isLoaded = false;
            _cell = null;

            UpdateManager.Instance.RemoveUpdate(TimeUpdate);
        }

        public void SetUsing(bool option)
        {
            _isUsing = option;
            TimeReset();
        }

        private void TimeReset()
        {
            _timeToUnload = TIME_TO_UNLOAD;
        }


        private void TimeUpdate()
        {
            if (!_isUsing)
            {
                _timeToUnload -= UnityEngine.Time.deltaTime;

                if (_timeToUnload <= 0)
                    Unload();
            }
        }
    }
}
