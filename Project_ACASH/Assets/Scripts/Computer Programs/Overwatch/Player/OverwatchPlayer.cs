using System;
using System.IO;
using System.Collections.Generic;
using Overwatch.Memory;
using Overwatch.Watchable;
using Overwatch.Read;
using Single;
using FMOD;

namespace Overwatch.Player
{
    public class OverwatchPlayer : Singleton<OverwatchPlayer>
    {
        public int MomentIndex => _momentIndex;
        public Action OnEndOfMemory { get; set; }

        private bool _needToUpdate;

        private OverwatchMemoryMoment _moment;
        private OverwatchReader _reader;
        private int _momentIndex;


        public void Setup()
        {
            _reader = new OverwatchReader();
            Select(0);
        }


        public void Update()
        {
            if (_needToUpdate)
            {
                if (_reader.IsLoaded)
                    Show();
            }
        }

        public void Select(int index)
        {
            SetIndex(index);
            SetMoment();
        }

        private void Show()
        {
            _needToUpdate = false;
            GetMoment();
            Remember();
        }

        private void Remember()
        {
            int id;
            HashSet<int> enabledIndexes = new HashSet<int>();

            for (int i = 0; i < _moment.Info.Count; i++)
            {
                id = _moment.Info[i].Id;
                WatchableManager.Wathcables[id].Appear(true);
                WatchableManager.Wathcables[id].Remember(_moment.Info[i]);
                enabledIndexes.Add(id);
            }

            for (int i = 0; i < WatchableManager.Wathcables.Count; i++)
            {
                if (!enabledIndexes.Contains(i))
                {
                    WatchableManager.Wathcables[i].Appear(false);
                }
            }
        }

        private void GetMoment()
        {
            _moment = _reader.GetMoment();
        }


        private void SetMoment()
        {
            _reader.Select(_momentIndex);
            _needToUpdate = true;
        }

        private void SetIndex(int index)
        {
            if (index < 0)
            {
                _momentIndex = 0;
                return;
            }

            if (index >= OverwatchInfo.MomentsAmount)
            {
                _momentIndex = OverwatchInfo.MomentsAmount - 1;
                OnEndOfMemory?.Invoke();
                return;
            }

            _momentIndex = index;
        }
    }
}