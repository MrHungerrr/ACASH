using System;
using UnityEngine;
using Overwatch.Record;


namespace Overwatch.Memorable
{
    public class ObjectMemorable : IMemorable
    {
        public bool IsHere { get; private set; }

        private readonly GameObject _object;
        private MemorableObjectInfo _memorableInfo;


        public ObjectMemorable(GameObject obj)
        {
            _object = obj;
            IsHere = true;
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
        }

        public void Cut()
        {
        }
    }
}
