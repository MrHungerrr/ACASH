using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GOAP
{
    public interface IGOAPStateStorageList : IGOAPStateStorage, IGOAPStateReadOnlyStorageList
    {
        void Add(string key, object value);
        void Add(string key, GOAPState state);
        void Add(KeyValuePair<string, GOAPState> state);
        void Remove(string key);
    }
}
