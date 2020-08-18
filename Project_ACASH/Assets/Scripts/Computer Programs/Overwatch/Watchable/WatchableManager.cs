using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Watchable
{
    public static class WatchableManager
    {
        public enum types
        {
            Scholar,
            Object
        }


        public static IReadOnlyList<IWatchable> Wathcables => _wathcables;


        private static List<IWatchable> _wathcables;

        public static void SetLevel()
        {
            _wathcables = new List<IWatchable>();
        }


        public static void AddWatchable(IWatchable watchable)
        {
            _wathcables.Add(watchable);
        }

        public static void FixAll()
        {
            for(int i = 0; i < _wathcables.Count; i++)
            {
                _wathcables[i].Fix();
            }
        }

        public static void CutAll()
        {
            for (int i = 0; i < _wathcables.Count; i++)
            {
                _wathcables[i].Cut();
            }
        }
    }
}