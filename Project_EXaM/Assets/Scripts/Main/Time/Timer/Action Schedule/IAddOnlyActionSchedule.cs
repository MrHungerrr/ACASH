using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Tools.Single;

namespace GameTime.Action
{
    public interface IAddOnlyActionSchedule
    {
        void AddActionInTime(int inTime, System.Action action);
        void AddActionAtTime(int atTime, System.Action action);
    }
}







