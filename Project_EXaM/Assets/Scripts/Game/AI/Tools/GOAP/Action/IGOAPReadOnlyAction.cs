using System;
using GOAP.Cost;

namespace GOAP
{
    public interface IGOAPReadOnlyAction
    {
        string Name { get; }
        bool IsConnector { get; }
        IGOAPStateReadOnlyStorage Effect { get; }
        IGOAPStateReadOnlyStorageList Preconditions { get; }
        IGOAPCost Cost { get; }
        string ToString();
    }
}
