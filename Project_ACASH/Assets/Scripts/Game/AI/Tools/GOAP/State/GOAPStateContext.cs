using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GOAP
{
    public class GOAPStateContext
    {
        private IGOAPStateStorage[] _localStates;

        public GOAPStateContext(params IGOAPStateStorage[] localState)
        {
            _localStates = localState;
        }

        public bool Contains(KeyValuePair<string, GOAPState> state)
        {
            for(int i = 0; i< _localStates.Length; i++)
            {
                if (_localStates[i].Contains(state))
                    return true;
            }

            return false;
        }


        public void Set(KeyValuePair<string, GOAPState> state)
        {
            for (int i = 0; i < _localStates.Length; i++)
            {
                if (_localStates[i].Contains(state.Key))
                {
                    _localStates[i].Set(state);
                    return;
                }
            }

            throw new ArgumentException($"Отсутсвует GOAPState с Key:\"{state.Key}\"");
        }
    }
}
