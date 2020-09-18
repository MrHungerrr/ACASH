using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars.Actions.Operations;

namespace AI.Scholars.Actions
{
    public class ScholarAction
    {
        public IReadOnlyList<IScholarOperation> Operations => _operations;
        private readonly IScholarOperation[] _operations;


        public ScholarAction(List<IScholarOperation> operations)
        {
            _operations = operations.ToArray();
        }

        public ScholarAction(IScholarOperation[] operations)
        {
            _operations = operations;
        }
    }
}
