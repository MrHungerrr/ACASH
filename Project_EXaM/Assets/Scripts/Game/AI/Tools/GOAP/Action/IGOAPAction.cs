using Vkimow.Serializators.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP.Cost;

namespace GOAP
{
    public interface IGOAPAction : IGOAPReadOnlyAction, IXMLSerializable
    {
        void SetName(string name);
        IGOAPStateStorageList GetPreconditions();
        IGOAPStateStorage GetEffect();
        void SetCost(IGOAPCost cost);
    }
}
