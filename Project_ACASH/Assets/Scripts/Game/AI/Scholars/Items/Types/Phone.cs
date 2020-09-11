using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Items.Types
{
    public class Phone : ScholarItem
    {
        public Phone(Scholar scholar) : base(scholar) { }


        public override void Show()
        {
        }

        public override void Hide()
        {
        }

        public override string ToString()
        {
            return "Phone";
        }
    }
}
