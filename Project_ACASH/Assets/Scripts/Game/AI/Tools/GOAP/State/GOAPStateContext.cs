using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GOAP
{
    public class GOAPStateContext
    {
        private readonly IGOAPStateReadOnlyStorage[] _contexts;

        public GOAPStateContext(List<IGOAPStateReadOnlyStorage> contexts)
        {
            _contexts = contexts.ToArray();
        }

        public GOAPStateContext(params IGOAPStateReadOnlyStorage[] contexts)
        {
            _contexts = contexts;
        }

        public bool Contains(KeyValuePair<string, GOAPState> state)
        {
            for(int i = 0; i< _contexts.Length; i++)
            {
                if (_contexts[i].Contains(state))
                    return true;
            }

            return false;
        }
    }
}
