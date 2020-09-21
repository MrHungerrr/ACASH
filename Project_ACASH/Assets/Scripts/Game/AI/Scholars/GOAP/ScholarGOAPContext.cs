using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars.Items;
using GOAP;
using Objects._2D.Places;

namespace AI.Scholars.GOAP
{
    public class ScholarGOAPContext : GOAPStateStorageList
    {
        private readonly Scholar _scholar;

        public ScholarGOAPContext(Scholar scholar)
        {
            Add("Item_Phone_Have", true);

            Add("Program", "None");
            Add("Item", "None");
            Add("Location", "DockStation");

            _scholar = scholar;
            scholar.Items.OnItemsChanged += ItemsChanged;
            scholar.Location.OnLocationChanged += LocationChanged;
        }

        private void ItemsChanged()
        {
            foreach (ScholarItem.Type item in Enum.GetValues(typeof(ScholarItem.Type)))
            {
                Set($"Items_Have_{item.ToString()}", _scholar.Items.Contains(item));
            }
        }

        private void LocationChanged()
        {
            Set("Location", _scholar.Location.CurrentPlace.tag);
        }
    }
}
