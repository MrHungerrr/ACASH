using System.Collections.Generic;


namespace GOAP
{
    public interface IGOAPStateReadOnlyStorage
    {
        bool IsEmpty { get; }
        bool Contains(string key);
        bool Contains(string key, object value);
        bool Contains(string key, GOAPState state);
        bool Contains(KeyValuePair<string, GOAPState> state);
        string ToString();
    }
}
