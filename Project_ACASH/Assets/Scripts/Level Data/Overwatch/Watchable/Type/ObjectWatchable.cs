using System;
using UnityEngine;
using Overwatch.Record;


namespace Overwatch.Watchable
{
    public class ObjectWatchable : IWatchable
    {
        public bool IsHere { get; private set; }

        private readonly Transform _transform;
        private readonly Renderer _renderer;
        private WatchableObjectInfo _watchableInfo;




        public ObjectWatchable(Transform transformOfObject, Renderer renderer)
        {
            _transform = transformOfObject;
            _renderer = renderer;
            IsHere = _renderer.enabled;
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
                    _transform.position,
                    _transform.rotation
                );

            return _watchableInfo;
        }

        public void Appear(bool option)
        {
            _renderer.enabled = option;
            IsHere = option;
        }

        public void Remember(in IWatchableInfo info)
        {
            Set((WatchableObjectInfo)info);

            _transform.position = _watchableInfo.Position;
            _transform.rotation = _watchableInfo.Rotation;
        }


        private void Set(in WatchableObjectInfo info)
        {
            _watchableInfo = info;
        }
    }
}
