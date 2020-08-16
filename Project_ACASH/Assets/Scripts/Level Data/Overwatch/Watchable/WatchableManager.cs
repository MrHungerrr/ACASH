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


        public static IReadOnlyList<WatchableShell> Wathcables => _wathcables;


        private static List<WatchableShell> _wathcables;

        public static void SetLevel()
        {
            _wathcables = new List<WatchableShell>();
        }

        public static void AddWatchable(IWatchable watchable)
        {
            var watchableShell = new WatchableShell(watchable);
            _wathcables.Add(watchableShell);
        }
    }
}