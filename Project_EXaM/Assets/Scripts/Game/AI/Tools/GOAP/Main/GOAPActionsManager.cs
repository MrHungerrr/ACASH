using Vkimow.Serializators.XML;
using System.Collections.Generic;
using System.Xml.Linq;
using Vkimow.Tools.Single;
using System;

namespace GOAP
{
    public class GOAPActionsManager: Singleton<GOAPActionsManager>, IXMLSerializable
    {
        public IReadOnlyList<IGOAPReadOnlyAction> Actions => _actions;

        private List<IGOAPAction> _actions;

        private Dictionary<KeyValuePair<string, GOAPState>, List<IGOAPReadOnlyAction>> _effectsAndActions;

        public GOAPActionsManager()
        {
            _actions = new List<IGOAPAction>();
            _effectsAndActions = new Dictionary<KeyValuePair<string, GOAPState>, List<IGOAPReadOnlyAction>>();
        }

        public void Add(IGOAPAction action)
        {
            var effect = ((GOAPStateStorageSingle)action.Effect).GetState();

            if (!_effectsAndActions.ContainsKey(effect))
            {
                _effectsAndActions.Add(effect, new List<IGOAPReadOnlyAction>());
            }

            _effectsAndActions[effect].Add(action);
            _actions.Add(action);
        }

        public void Clear()
        {
            _actions = new List<IGOAPAction>();
            _effectsAndActions = new Dictionary<KeyValuePair<string, GOAPState>, List<IGOAPReadOnlyAction>>();
        }

        internal bool TryGetActionsWithEffect(KeyValuePair<string, GOAPState> needEffect, out List<IGOAPReadOnlyAction> needActions)
        {
            needActions = null;

            if (_effectsAndActions.ContainsKey(needEffect))
            {
                needActions = _effectsAndActions[needEffect];
                return true;
            }

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
