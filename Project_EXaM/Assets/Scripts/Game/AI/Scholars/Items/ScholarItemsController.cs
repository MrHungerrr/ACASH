using AI.Scholars.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using AI.Scholars;

namespace AI.Scholars.Items
{
    public class ScholarItemsController
    {
        public event Action OnItemsChanged;
        public bool AmIHolding => CurrentItem != null;
        public IScholarItem CurrentItem { get; private set; }
        private HashSet<ScholarItem.Type> _itemsIHave;
        private readonly Scholar _scholar;


        public ScholarItemsController(Scholar scholar)
        {
            _scholar = scholar;
            _itemsIHave = new HashSet<ScholarItem.Type>();
        }

        public void SetItems(IEnumerable<ScholarItem.Type> items)
        {
            if (items == null)
                throw new NullReferenceException();

            if (AmIHolding)
                Put();

            _itemsIHave = new HashSet<ScholarItem.Type>(items);

            OnItemsChanged?.Invoke();
        }

        public void AddItems(IEnumerable<ScholarItem.Type> items)
        {
            if (items == null)
                throw new NullReferenceException();

            _itemsIHave.UnionWith(items);

            OnItemsChanged?.Invoke();
        }

        public void AddItem(ScholarItem.Type item)
        {
            _itemsIHave.Add(item);
            OnItemsChanged?.Invoke();
        }

        public bool Contains(ScholarItem.Type item)
        {
            return _itemsIHave.Contains(item);
        }

        public void Take(ScholarItem.Type item)
        {
            SetCurrentItem(item);
            CurrentItem.Show();
        }

        public void Put()
        {
            if (!AmIHolding)
                throw new Exception($"Мы ничего не держим!");

            CurrentItem.Hide();
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
    }
}
