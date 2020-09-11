using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions
{
    public class ScholarActionsUpdater
    {
        public bool IsActive { get; private set; }
        private readonly Scholar _scholar;

        public ScholarActionsUpdater(Scholar scholar)
        {
            _scholar = scholar;
            IsActive = false;
        }

        public void Enable(bool option)
        {
            IsActive = option;
        }

        public ScholarAction GetSomeAction()
        {
            if (!IsActive)
                throw new Exception();

            return ScholarActionsTemplates.WaitFor(_scholar, 5f);
        }
    }
}
