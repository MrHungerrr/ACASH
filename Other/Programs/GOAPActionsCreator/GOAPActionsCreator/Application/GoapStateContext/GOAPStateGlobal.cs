using GOAP;
using System;
using System.Collections.Generic;
using System.Text;


public class GOAPStateGlobal : GOAPStateStorageList
{
    public GOAPStateGlobal()
    {
        Add("Places_Toilet_Are_Busy", false);
        Add("Places_Sink_Are_Busy", false);
        Add("Places_Hallway_Are_Busy", false);
    }
}
