using AI.Scholars.Items.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Items
{
    public abstract class ScholarItem : IScholarItem
    {
        public enum Type
        {
            Phone,
            Calculator,
            Note
        }


        protected readonly Scholar _scholar;

        protected ScholarItem(Scholar scholar)
        {
            _scholar = scholar;
        }

        public static IScholarItem Create(Scholar scholar, Type type)
        {
            switch (type)
            {
                case Type.Phone:
                    return new Phone(scholar);
                case Type.Calculator:
                    return new Calculator(scholar);
                case Type.Note:
                    return new Note(scholar);
            }

            throw new InvalidEnumArgumentException();
        }

        public abstract void Show();

        public abstract void Hide();

        public override abstract string ToString();
    }
}
