using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Computer
{
    public class ScholarComputer
    {
        public enum Program
        {
            Dictionary,
            Calculator,
            Browser,
            Rules,
            Text,
            Test,
            Code,
            None
        }


        public Program CurrentProgram => _currentProgram;

        private Program _currentProgram;


        public ScholarComputer()
        {

        }

        public void SetProgram(Program program)
        {
            _currentProgram = program;
        }

        public void SetProgram(string programName)
        {
            Program program;

            if (!Enum.TryParse(programName, out program))
                throw new InvalidEnumArgumentException();

            SetProgram(program);
        }

        public void ResetProgram()
        {
            _currentProgram = Program.None;
        }
    }
}
