using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vkimow.Serializators.XML;

namespace GOAP
{
    public sealed class GOAPAction : IGOAPAction
    {
        public string Name { get; private set; }
        public bool IsConnector { get; private set; }
        public IGOAPStateReadOnlyStorage Effect => _effect;
        public IGOAPStateReadOnlyStorageList Preconditions => _preconditions;
        public IGOAPCost Cost { get; private set; }


        private readonly IGOAPStateStorageList _preconditions;
        private readonly IGOAPStateStorage _effect;


        public GOAPAction(string action , bool isConnector)
        {
            Name = action;
            IsConnector = isConnector;
            _preconditions = new GOAPStateStorageList();
            _effect = new GOAPStateStorageSingle();
        }

        public GOAPAction(string action)
        {
            Name = action;
            IsConnector = false;
            _preconditions = new GOAPStateStorageList();
            _effect = new GOAPStateStorageSingle();
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public IGOAPStateStorage GetEffect()
        {
            return _effect;
        }

        public IGOAPStateStorageList GetPreconditions()
        {
            return _preconditions;
        }

        public void SetCost(IGOAPCost cost)
        {
            Cost = cost;
        }

        XElement IXMLSerializable.ConvertToXML()
        {
            return ((IXMLSerializable)this).ConvertToXML("Action");
        }

        XElement IXMLSerializable.ConvertToXML(string name)
        {
            var xElement = new XElement(name,
                new XAttribute("Name", Name),
                new XAttribute("IsConnector", IsConnector));

            xElement.Add(Cost.ConvertToXML("Cost"));
            xElement.Add(_effect.ConvertToXML("Effect"));
            xElement.Add(_preconditions.ConvertToXML("Preconditions"));

            return xElement;
        }

        void IXMLSerializable.ReadXML(XElement xElement)
        {
            Name = (string)xElement.Attribute("Name");
            IsConnector = (bool)xElement.Attribute("IsConnector");
            Cost = GOAPCostSerializer.Instance.Deserialize(xElement.Element("Cost"));
            _effect.ReadXML(xElement.Element("Effect"));
            _preconditions.ReadXML(xElement.Element("Preconditions"));
        }

        public override string ToString()
        {
            if (!IsConnector)
                return ($"Action: \"{Name}\"");
            else
                return ($"Connector: \"{Name}\"");
        }
    }
}
