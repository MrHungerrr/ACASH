using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GOAP
{
    public class GOAPStateContext
    {
        private readonly IGOAPStateStorage[] _contexts;

        public GOAPStateContext(params IGOAPStateStorage[] contexts)
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


        public void Set(KeyValuePair<string, GOAPState> state)
        {
            for (int i = 0; i < _contexts.Length; i++)
            {
                if (_contexts[i].Contains(state.Key))
                {
                    _contexts[i].Set(state);
                    return;
                }
            }

            throw new ArgumentException($"Отсутсвует GOAPState с Key:\"{state.Key}\"");
        }
    }
}
