using System;
using AI.Scholars.Items;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class TakeItem : ScholarOperation
    {
        private readonly ScholarItem.Type _itemType;

        public TakeItem(Scholar scholar, ScholarItem.Type item): base(scholar)
        {
            _itemType = item;
        }

        public override void Execute()
        {
            _scholar.Items.Take(_itemType);
            OperationDone();
        }

        public override void Stop()
        {

        }

        public override string ToString()
        {
            return $"Take {_itemType}";
        }
    }
}
