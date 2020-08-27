using System;
using Overwatch.Record;
using UnityEngine;


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
                    _scholar.Body.Body.position,
                    _scholar.Body.Body.rotation.eulerAngles.y,
                    _scholar.Body.Head.position,
                    _scholar.Body.Head.rotation,
                    _scholar.Anim.AnimationId,
                    _scholar.Anim.AnimationTime
                );

            return _memorableInfo;
        }

        public void Appear(bool option)
        {
        }

        public void Remember(in IMemorableInfo info)
        {
            Set((MemorableScholarInfo)info);

            _scholar.Body.Body.position = _memorableInfo.BodyPosition;
            _scholar.Body.Body.rotation = Quaternion.Euler(0, _memorableInfo.BodyRotation, 0);
            _scholar.Body.Head.position = _memorableInfo.HeadPosition;
            _scholar.Body.Head.rotation = _memorableInfo.HeadRotation;
            _scholar.Anim.SetAnimation(_memorableInfo.AnimationId);
            _scholar.Anim.SetTime(_memorableInfo.AnimationTime);
        }


        private void Set(in MemorableScholarInfo info)
        {
            _memorableInfo = info;
        }

        public void Fix()
        {
            _scholar.GetComponent<Collider>().enabled = true;
            _scholar.Move.RB.isKinematic = false;
        }

        public void Cut()
        {
            _scholar.GetComponent<Collider>().enabled = false;
            _scholar.Move.RB.isKinematic = true;
        }
    }
}
