using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars;
using Objects._2D.Places;


namespace Objects._2D.ClassRoom
{
    public class ClassElement
    {
        private Scholar _scholar;
        private Place _desk;
        private Place _dockStation;

        public ClassElement(Scholar scholar, Place desk, Place dockStation)
        {
            _scholar = scholar;
            _desk = desk;
            _dockStation = dockStation;
        }
    }

}