using System.Collections.Generic;

namespace GOAP
{
    public interface IGOAPStateReadOnlyStorageList : IGOAPStateReadOnlyStorage, IEnumerable<KeyValuePair<string, GOAPState>>
    {
        GOAPState this[string key] { get; }
    }
}
