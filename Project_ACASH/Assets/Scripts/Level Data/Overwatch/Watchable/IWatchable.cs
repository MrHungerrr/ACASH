using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Watchable
{
    public interface IWatchable: IDisappearable
    {
        void StartWatch();

        IWatchableInfo Capture();

        void Remember(in IWatchableInfo info);
    }
}
