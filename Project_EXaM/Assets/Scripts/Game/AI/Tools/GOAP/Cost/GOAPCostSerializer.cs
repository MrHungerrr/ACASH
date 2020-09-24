using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vkimow.Serializators.XML;
using Vkimow.Tools.Single;

namespace GOAP.Cost
{
    public class GOAPCostSerializer: Singleton<GOAPCostSerializer>
    {
        public IGOAPCost Deserialize(XElement xElement)
        {
            switch ((string)xElement.Attribute("Type"))
            {
                case "BaseCost":
                    {
                        IGOAPCost cost = new BaseCost();
                        cost.ReadXML(xElement);
                        return cost;
                    }
            }

            throw new Exception("Неверный Тип GOAPCost");
        }
    }
}
