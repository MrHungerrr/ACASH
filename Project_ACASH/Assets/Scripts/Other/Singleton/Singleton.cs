using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single
{
    public abstract class Singleton<T> where T: new()
    {

        private static T _instance;
        private static object _lock = new object();


        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }

                    return _instance;
                }
            }
        }

    }
}
