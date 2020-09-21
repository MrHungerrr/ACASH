using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsCheatFactory
    {
        //-------------------------------------------------------------------
        //CHEAT
        private static IGOAPAction cheatBaseGoTo = GOAPAction.CreateConnector("Cheat_Base_GoTo");
        private static IGOAPAction cheatBaseComputer = GOAPAction.CreateConnector("Cheat_Base_Computer");


        public static void Create()
        {
            CreateCheatBase();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(cheatBaseGoTo);
            GOAPActionsManager.Instance.Add(cheatBaseComputer);
        }

        private static void CreateCheatBase()
        {
            cheatBaseGoTo.SetCost(new BaseCost(0));
            cheatBaseGoTo.GetEffect().Set("Cheat", true);
            cheatBaseGoTo.GetPreconditions().Add("Location", "Allowed");
            cheatBaseGoTo.GetPreconditions().Add("Item", "Prohibited");


            cheatBaseComputer.SetCost(new BaseCost(0));
            cheatBaseComputer.GetEffect().Set("Cheat", true);
            cheatBaseComputer.GetPreconditions().Add("Location", "Desk");
            cheatBaseComputer.GetPreconditions().Add("Program", "Prohibited");

        }
    }
}
