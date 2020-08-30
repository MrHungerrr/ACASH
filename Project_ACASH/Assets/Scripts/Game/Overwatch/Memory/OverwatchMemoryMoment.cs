using System;
using System.Collections;
using System.Collections.Generic;
using Overwatch.Memorable;


namespace Overwatch.Memory
{
    public struct OverwatchMemoryMoment : IEnumerable<IMemorableInfo>
    {
        public float Time { get; }
        public IReadOnlyList<IMemorableInfo> Info => _info;


        private readonly List<IMemorableInfo> _info;


        public OverwatchMemoryMoment(in float time)
        {
            Time = time;
            _info = new List<IMemorableInfo>();
        }

        public void Add(in IMemorableInfo info)
        {
            _info.Add(info);
        }


        #region IEnumerotor

        public IEnumerator<IMemorableInfo> GetEnumerator()
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
