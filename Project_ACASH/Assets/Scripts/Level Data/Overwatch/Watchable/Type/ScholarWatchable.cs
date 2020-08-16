using System;
using Overwatch.Record;
using UnityEngine;


namespace Overwatch.Watchable
{
    public class ScholarWatchable : IWatchable
    {
        public bool IsHere => true;

        private WatchableScholarInfo _watchableInfo;
        private readonly Scholar _scholar;



        public ScholarWatchable(Scholar scholar)
        {
            _scholar = scholar;
            StartWatch();
        }


        public void StartWatch()
        {
            int id = WatchableManager.Wathcables.Count;
            _watchableInfo = new WatchableScholarInfo(id);
            WatchableManager.AddWatchable(this);
        }

        public IWatchableInfo Capture()
        {
            _watchableInfo.Capture
                (
                    _scholar.Body.Body.position,
                    _scholar.Body.Body.rotation.eulerAngles.y,
                    _scholar.Body.Head.position,
                    _scholar.Body.Head.rotation,
                    _scholar.Anim.AnimationId,
                    _scholar.Anim.AnimationTime
                );

            return _watchableInfo;
        }

        public void Appear(bool option)
        {
        }

        public void Remember(in IWatchableInfo info)
        {
            Set((WatchableScholarInfo)info);

            _scholar.Body.Body.position = _watchableInfo.BodyPosition;
            _scholar.Body.Body.rotation = Quaternion.Euler(0, _watchableInfo.BodyRotation, 0);
            _scholar.Body.Head.position = _watchableInfo.HeadPosition;
            _scholar.Body.Head.rotation = _watchableInfo.HeadRotation;
            _scholar.Anim.SetAnimation(_watchableInfo.AnimationId);
            _scholar.Anim.SetTime(_watchableInfo.AnimationTime);
        }


        private void Set(in WatchableScholarInfo info)
        {
            _watchableInfo = info;
        }
    }
}
