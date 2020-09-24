using System;
using Overwatch.Record;
using UnityEngine;
using AI.Scholars;


namespace Overwatch.Memorable
{
    public class ScholarMemorable : IMemorable
    {
        public bool IsHere => true;

        private MemorableScholarInfo _memorableInfo;
        private readonly Scholar _scholar;



        public ScholarMemorable(Scholar scholar)
        {
            _scholar = scholar;
            StartWatch();
        }


        public void StartWatch()
        {
            int id = MemorableManager.Wathcables.Count;
            _memorableInfo = new MemorableScholarInfo(id);
            MemorableManager.AddWatchable(this);
        }


        public IMemorableInfo Capture()
        {
            _memorableInfo.Capture
                (
                    _scholar.Move.Position,
                    _scholar.Sight.Angle
                );

            return _memorableInfo;
        }

        public void Appear(bool option)
        {
        }

        public void Remember(in IMemorableInfo info)
        {
            Set((MemorableScholarInfo)info);

            _scholar.Move.SetPosition(_memorableInfo.Position);
            _scholar.Sight.SetRotation(_memorableInfo.Rotation);
        }


        private void Set(in MemorableScholarInfo info)
        {
            _memorableInfo = info;
        }

        public void Fix()
        {
        }

        public void Cut()
        {
        }
    }
}
