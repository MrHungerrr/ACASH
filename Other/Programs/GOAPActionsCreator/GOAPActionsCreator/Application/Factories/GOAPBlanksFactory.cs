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
            CreateGlobal();
            CreateLocal();
        }


        private static void CreateGlobal()
        {
            GOAPBlanksManager.Instance.Add("Places_Toilets_Are_Busy", typeof(bool));
            GOAPBlanksManager.Instance.Add("Places_Sinks_Are_Busy", typeof(bool));
            GOAPBlanksManager.Instance.Add("Places_Outside_Are_Busy", typeof(bool));

            GOAPBlanksManager.Instance.Add("Cheat", typeof(bool));
            GOAPBlanksManager.Instance.Add("Cheat_Note", typeof(bool));
            GOAPBlanksManager.Instance.Add("Cheat_Phone", typeof(bool));
            GOAPBlanksManager.Instance.Add("Cheat_Calculator", typeof(bool));

            GOAPBlanksManager.Instance.Add("Special", typeof(bool));
            GOAPBlanksManager.Instance.Add("Special_Go_Out", typeof(bool));
        }

        private static void CreateLocal()
        {
            GOAPBlanksManager.Instance.Add("Items_Have_Note", typeof(bool));
            GOAPBlanksManager.Instance.Add("Items_Have_Phone", typeof(bool));
            GOAPBlanksManager.Instance.Add("Items_Have_Calculator", typeof(bool));
            GOAPBlanksManager.Instance.Add("Location", typeof(string));
            GOAPBlanksManager.Instance.Add("Want_Pee", typeof(bool));
            GOAPBlanksManager.Instance.Add("Want_Wash_Hands", typeof(bool));
            GOAPBlanksManager.Instance.Add("Want_Rest", typeof(bool));
            GOAPBlanksManager.Instance.Add("Pee", typeof(bool));
            GOAPBlanksManager.Instance.Add("Wash_Hands", typeof(bool));
            GOAPBlanksManager.Instance.Add("Rest", typeof(bool));
            GOAPBlanksManager.Instance.Add("Talk", typeof(bool));
        }
    }
}
