using GOAP;
using System;
using System.Collections.Generic;
using System.Text;


public class GOAPStateGlobal : GOAPStateStorageList
{
    public GOAPStateGlobal()
    {
        Add("Places_Toilets_Are_Busy", false);
        Add("Places_Sinks_Are_Busy", false);
        Add("Places_Outside_Are_Busy", false);
    }
}
