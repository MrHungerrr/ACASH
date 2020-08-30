using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars
{
    public class ScholarInfo
    {
        private Scholar _scholar;
        public int GlobalIndex { get; }
        public int LocalIndex { get; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string FullName => $"{Name} {Surname}";


        public ScholarInfo(Scholar scholar, int globalIndex, int localIndex)
        {
            _scholar = scholar;
            GlobalIndex = globalIndex;
            LocalIndex = localIndex;
            SetNumber();
        }

        private void SetNumber()
        {
            _scholar.Text.SetNumber(GlobalIndex);
        }

        public void SetFullName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
