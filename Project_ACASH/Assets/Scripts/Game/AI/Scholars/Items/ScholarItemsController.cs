using AI.Scholars.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using AI.Scholars;

namespace AI.Scholars.Items
{
    public class ScholarItemsController
    {
        public bool AmIHolding => CurrentItem != null;
        public IScholarItem CurrentItem { get; private set; }
        private HashSet<ScholarItem.Type> _itemsIHave;
        private readonly Scholar _scholar;


        public ScholarItemsController(Scholar scholar)
        {
            _scholar = scholar;
        }

        public void SetItems(IEnumerable<ScholarItem.Type> items)
        {
            if (items != null)
                _itemsIHave = new HashSet<ScholarItem.Type>(items);
            else
                _itemsIHave = new HashSet<ScholarItem.Type>();

            CurrentItem = null;
        }

        public void Take(ScholarItem.Type item)
        {
            SetCurrentItem(item);
            CurrentItem.Show();
        }

        public void Put()
        {
            CurrentItem.Hide();

            if (!AmIHolding)
                throw new Exception($"Мы ничего не держим!");

            CurrentItem = null;
        }

        private void SetCurrentItem(ScholarItem.Type item)
        {
            if(!_itemsIHave.Contains(item))
                throw new Exception($"У нас нет {item}!");

            if (AmIHolding)
                throw new Exception($"Мы еще держим {CurrentItem}!");

            CurrentItem = ScholarItem.Create(_scholar, item);
        }

        public bool Contains(ScholarItem.Type item)
        {
            return _itemsIHave.Contains(item);
        }
    }
}
