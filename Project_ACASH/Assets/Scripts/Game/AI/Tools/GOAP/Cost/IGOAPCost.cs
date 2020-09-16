using Vkimow.Serializators.XML;
using System;
using System.Collections.Generic;
using System.Text;

namespace GOAP.Cost
{
    public interface IGOAPCost : IComparable<IGOAPCost>, IXMLSerializable
    {
        void Add(IGOAPCost other);
        IGOAPCost GetSumWith(IGOAPCost other);
        string ToString();
    }
}
