using System;
using System.Collections;
using System.Collections.Generic;
using Overwatch.Watchable;


namespace Overwatch.Memory
{
    public struct OverwatchMemoryMoment : IEnumerable<IWatchableInfo>
    {
        public float Time { get; }
        public IReadOnlyList<IWatchableInfo> Info => _info;


        private readonly List<IWatchableInfo> _info;


        public OverwatchMemoryMoment(in float time)
        {
            Time = time;
            _info = new List<IWatchableInfo>();
        }

        public void Add(IWatchableInfo info)
        {
            _info.Add(info);
        }


        #region IEnumerotor

        public IEnumerator<IWatchableInfo> GetEnumerator()
        {
            return _info.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _info.GetEnumerator();
        }

        #endregion
    }
}
