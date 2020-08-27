using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scholars
{
    public class ActionOfScholar
    {
        public string name { get; }
        public string scholar { get; }

        public ActionOfScholar(string name, string scholar)
        {
            this.name = name;
            this.scholar = scholar;
        }
    }
}