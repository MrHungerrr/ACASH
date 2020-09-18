using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars.Items;
using GOAP;
using Objects.Organization.Places;

namespace AI.Scholars.GOAP
{
    public class ScholarGOAPContext : GOAPStateStorageList
    {
        private readonly Scholar _scholar;

        public ScholarGOAPContext(Scholar scholar)
        {
            Add("Items_Have_Note", false);
            Add("Items_Have_Phone", false);
            Add("Items_Have_Calculator", false);
            Add("Location", "None");
            Add("Want_Pee", true);
            Add("Want_Wash_Hands", true);
            Add("Want_Rest", true);

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
