using System;
using UnityEngine;
using Overwatch.Record;


namespace Overwatch.Memorable
{
    public class ObjectMemorable : IMemorable
    {
        public bool IsHere { get; private set; }

        private readonly Object _object;
        private MemorableObjectInfo _memorableInfo;


        public ObjectMemorable(Object obj)
        {
            _object = obj;
            IsHere = _object.Renderer.enabled;
            StartWatch();
        }


        public void StartWatch()
        {
            int id = MemorableManager.Wathcables.Count;
            _memorableInfo = new MemorableObjectInfo(id);
            MemorableManager.AddWatchable(this);
        }

        public IMemorableInfo Capture()
        {
            _memorableInfo.Capture
                (
                    _object.transform.position,
                    _object.transform.rotation
                );

            return _memorableInfo;
        }

        public void Appear(bool option)
        {
            if (IsHere != option)
            {
                _object.Renderer.enabled = option;
                IsHere = option;
            }
        }

        public void Remember(in IMemorableInfo info)
        {
            Set((MemorableObjectInfo)info);

            _object.transform.position = _memorableInfo.Position;
            _object.transform.rotation = _memorableInfo.Rotation;
        }


        private void Set(in MemorableObjectInfo info)
        {
            _memorableInfo = info;
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
