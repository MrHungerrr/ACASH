using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Memorable
{
    public static class MemorableManager
    {
        public enum types
        {
            Scholar,
            Object
        }


        public static IReadOnlyList<IMemorable> Wathcables => _wathcables;


        private static List<IMemorable> _wathcables;

        public static void SetLevel()
        {
            _wathcables = new List<IMemorable>();
        }


        public static void AddWatchable(IMemorable watchable)
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