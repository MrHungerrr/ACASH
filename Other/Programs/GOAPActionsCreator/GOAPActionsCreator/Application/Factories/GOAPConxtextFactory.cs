using GOAP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class GOAPConxtextFactory
    {
        public static IGOAPStateReadOnlyStorageList ScholarContext => _scholarContext;
        public static IGOAPStateReadOnlyStorageList ClassContext => _classContext;

        private static GOAPStateStorageList _scholarContext;
        private static GOAPStateStorageList _classContext;


        public static void Create()
        {
            _scholarContext = new GOAPStateStorageList();
            _classContext = new GOAPStateStorageList();

            SetupScholarContext();
            SetupClassContext();
        }

        private static void SetupScholarContext()
        {
            _scholarContext.Add("Item_Phone_Have", true);

            _scholarContext.Add("Program", "None");
            _scholarContext.Add("Item", "None");
            _scholarContext.Add("Location", "Desk");
        }

        private static void SetupClassContext()
        {
            _classContext.Add("Program_Calculator_Allowed", false);
            _classContext.Add("Program_Dictionary_Allowed", true);
            _classContext.Add("Program_Browser_Allowed", false);
            _classContext.Add("Program_Rules_Allowed", true);
            _classContext.Add("Program_Test_Allowed", true);
            _classContext.Add("Program_Text_Allowed", true);
            _classContext.Add("Program_Code_Allowed", false);

            _classContext.Add("Item_Phone_Allowed", false);

            _classContext.Add("Place_Toilet_All_Busy", false);
            _classContext.Add("Place_Sink_All_Busy", false);
            _classContext.Add("Place_Hallway_All_Busy", false);

            _classContext.Add("Place_Toilet_Allowed", true);
            _classContext.Add("Place_Sink_Allowed", true);
            _classContext.Add("Place_Hallway_Allowed", true);
        }

    }
}
