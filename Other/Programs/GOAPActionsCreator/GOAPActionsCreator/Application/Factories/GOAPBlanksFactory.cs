using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPBlanksFactory
    {
        public static void Create()
        {
            CreateNoContextBlanks();
            CreateGlobalContextBlanks();
            CreateLocalContextBlanks();
        }


        private static void CreateGlobalContextBlanks()
        {
            GOAPBlanksManager.Instance.Add("Program_Calculator_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Dictionary_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Browser_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Rules_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Test_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Text_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Program_Code_Allowed", typeof(bool));

            GOAPBlanksManager.Instance.Add("Item_Phone_Allowed", typeof(bool));

            GOAPBlanksManager.Instance.Add("Place_Toilet_All_Busy", typeof(bool));
            GOAPBlanksManager.Instance.Add("Place_Sink_All_Busy", typeof(bool));
            GOAPBlanksManager.Instance.Add("Place_Hallway_All_Busy", typeof(bool));
            GOAPBlanksManager.Instance.Add("Place_Toilet_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Place_Sink_Allowed", typeof(bool));
            GOAPBlanksManager.Instance.Add("Place_Hallway_Allowed", typeof(bool));
        }

        private static void CreateLocalContextBlanks()
        {
            GOAPBlanksManager.Instance.Add("Item_Phone_Have", typeof(bool));

            GOAPBlanksManager.Instance.Add("Location", typeof(string));
            GOAPBlanksManager.Instance.Add("Program", typeof(string));
            GOAPBlanksManager.Instance.Add("Item", typeof(string));
        }

        private static void CreateNoContextBlanks()
        {
            GOAPBlanksManager.Instance.Add("Cheat", typeof(bool));
            GOAPBlanksManager.Instance.Add("Basic", typeof(string));
            GOAPBlanksManager.Instance.Add("Distraction", typeof(string));


            GOAPBlanksManager.Instance.Add("Pee", typeof(bool));
            GOAPBlanksManager.Instance.Add("WashHands", typeof(bool));
            GOAPBlanksManager.Instance.Add("Rest", typeof(bool));
            GOAPBlanksManager.Instance.Add("Talk", typeof(bool));
            GOAPBlanksManager.Instance.Add("Think", typeof(bool));
            GOAPBlanksManager.Instance.Add("Wait", typeof(bool));
            GOAPBlanksManager.Instance.Add("None", typeof(bool));
            GOAPBlanksManager.Instance.Add("End", typeof(bool));
            GOAPBlanksManager.Instance.Add("Idle", typeof(bool));
        }
    }
}
