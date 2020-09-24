using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Memory
{
    public class OverwatchMemoryCell : IEnumerable<OverwatchMemoryMoment>
    {
        public int Size => _size;


        private readonly OverwatchMemoryMoment[] _moments;
        private int _size;



        public OverwatchMemoryCell(in int size)
        {
            _moments = new OverwatchMemoryMoment[size];
            _size = 0;
        }

        public OverwatchMemoryCell(OverwatchMemoryMoment[] moments)
        {
            _moments = moments;
            _size = moments.Length;
        }

        public OverwatchMemoryCell(List<OverwatchMemoryMoment> moments)
        {
            _moments = moments.ToArray();
            _size = _moments.Length;
        }

        public OverwatchMemoryCell(IEnumerable<OverwatchMemoryMoment> moments)
        {
            _moments = moments.ToArray();
            _size = _moments.Length;
        }



        public void Add(in OverwatchMemoryMoment moment)
        {
            _moments[_size] = moment;
            _size++;
        }


        public OverwatchMemoryMoment GetMoment(in int index)
        {
            return _moments[index];
        }





        #region Indexer and IEnumerator
        public OverwatchMemoryMoment this [in int index]
        {
            get
            {
                return _moments[index];
            }
        }

        public IEnumerator<OverwatchMemoryMoment> GetEnumerator()
        {
            return ((IEnumerable<OverwatchMemoryMoment>)_moments).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<OverwatchMemoryMoment>)_moments).GetEnumerator();
        }

        #endregion
    }
}
