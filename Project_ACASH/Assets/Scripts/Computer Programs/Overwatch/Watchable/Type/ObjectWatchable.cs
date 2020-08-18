using System;
using UnityEngine;
using Overwatch.Record;


namespace Overwatch.Watchable
{
    public class ObjectWatchable : IWatchable
    {
        public bool IsHere { get; private set; }

        private readonly Object _object;
        private WatchableObjectInfo _watchableInfo;


        public ObjectWatchable(Object obj)
        {
            _object = obj;
            IsHere = _object.Renderer.enabled;
            StartWatch();
        }


        public void StartWatch()
        {
            int id = WatchableManager.Wathcables.Count;
            _watchableInfo = new WatchableObjectInfo(id);
            WatchableManager.AddWatchable(this);
        }

        public IWatchableInfo Capture()
        {
            _watchableInfo.Capture
                (
                    _object.transform.position,
                    _object.transform.rotation
                );

            return _watchableInfo;
        }

        public void Appear(bool option)
        {
            if (IsHere != option)
            {
                _object.Renderer.enabled = option;
                IsHere = option;
            }
        }

        public void Remember(in IWatchableInfo info)
        {
            Set((WatchableObjectInfo)info);

            _object.transform.position = _watchableInfo.Position;
            _object.transform.rotation = _watchableInfo.Rotation;
        }


        private void Set(in WatchableObjectInfo info)
        {
            _watchableInfo = info;
        }

        public void Fix()
        {
            _object.RB.GetComponent<Collider>().enabled = true;
            _object.RB.isKinematic = false;
        }

        public void Cut()
        {
            _object.RB.isKinematic = true;
            _object.RB.GetComponent<Collider>().enabled = false;
        }
    }
}
