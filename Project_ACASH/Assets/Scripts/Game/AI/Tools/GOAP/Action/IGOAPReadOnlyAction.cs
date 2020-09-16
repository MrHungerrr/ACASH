using System;
using GOAP.Cost;

namespace GOAP
{
    public interface IGOAPReadOnlyAction
    {
        string Name { get; }
        IGOAPStateReadOnlySingle Effect { get; }
        IGOAPStateReadOnlyStorage Preconditions { get; }
        IGOAPCost Cost { get; }
        string ToString();
    }
}
