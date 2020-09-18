﻿using Vkimow.Serializators.XML;
using System.Collections.Generic;
using System.Xml.Linq;
using Vkimow.Tools.Single;

namespace GOAP
{
    public class GOAPActionsManager: Singleton<GOAPActionsManager>, IXMLSerializable
    {
        public IReadOnlyList<IGOAPReadOnlyAction> Actions => _actions;

        private List<IGOAPAction> _actions;

        public GOAPActionsManager()
        {
            _actions = new List<IGOAPAction>();
        }

        public void Add(IGOAPAction action)
        {
            _actions.Add(action);
        }

        public void Clear()
        {
            _actions = new List<IGOAPAction>();
        }

        internal bool TryGetActionsWithEffect(KeyValuePair<string, GOAPState> needEffect, out List<GOAPAction> resultActions)
        {
            resultActions = new List<GOAPAction>();

            foreach(GOAPAction action in _actions)
            {
                if (action.Effect.Contains(needEffect))
                {
                    resultActions.Add(action);
                }
            }

            if (resultActions.Count != 0)
                return true;
            else
                return false;
        }

        #region XML Serialization
        XElement IXMLSerializable.ConvertToXML()
        {
            return ((IXMLSerializable)this).ConvertToXML("GOAPActions");
        }

        XElement IXMLSerializable.ConvertToXML(string name)
        {
            var xElement = new XElement(name);

            foreach(var action in _actions)
            {
                xElement.Add(action.ConvertToXML());
            }

            return xElement;
        }

        void IXMLSerializable.ReadXML(XElement xMainElement)
        {
            foreach(var xElement in xMainElement.Elements())
            {
                IGOAPAction action = new GOAPAction("Temporary Name");
                action.ReadXML(xElement);
                Add(action);
            }
        }
        #endregion
    }
}