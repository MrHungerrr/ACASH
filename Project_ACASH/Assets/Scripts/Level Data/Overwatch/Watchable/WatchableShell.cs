using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Watchable
{
    public class WatchableShell
    {
        public bool Enabled => _watchable.IsHere;


        private bool _enabled;
        private IWatchable _watchable;


        public WatchableShell(IWatchable watchable)
        {
            _watchable = watchable;
        }

        public IWatchableInfo Capture()
        {
            return _watchable.Capture();
        }

        public void Remember(in IWatchableInfo info)
        {
            if (!_watchable.IsHere)
                Appear();

            _watchable.Remember(info);
        }

        public void Dissapear()
        {
            _watchable.Appear(false);
        }

        private void Appear()
        {
            _watchable.Appear(true);
        }
    }
}
