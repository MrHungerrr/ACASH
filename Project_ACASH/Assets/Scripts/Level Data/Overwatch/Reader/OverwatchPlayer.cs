using System;
using System.IO;
using System.Collections.Generic;
using Overwatch.Memory;
using Overwatch.Watchable;
using Single;


namespace Overwatch.Read
{
    public class OverwatchPlayer : Singleton<OverwatchPlayer>
    {

        private OverwatchPlayerBuffer _buffer;
        private OverwatchMemoryMoment _moment;

        private void Remember()
        {
            int id;
            HashSet<int> enabledIndexes = new HashSet<int>();

            for(int i = 0; i < _moment.Info.Count; i++)
            {
                id = _moment.Info[i].Id;
                WatchableManager.Wathcables[id].Remember(_moment.Info[i]);
                enabledIndexes.Add(id);
            }

            for(int i = 0; i < WatchableManager.Wathcables.Count; i++)
            {
                if(!enabledIndexes.Contains(i))
                {
                    WatchableManager.Wathcables[i].Dissapear();
                }
            }
        }



    }
}