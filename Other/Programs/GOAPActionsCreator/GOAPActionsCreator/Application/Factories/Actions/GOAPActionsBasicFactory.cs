using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsBasicFactory
    {
        private static IGOAPAction basicAnyComputer = GOAPAction.CreateConnector("Basic_Any_Computer");
        private static IGOAPAction basicAnyItem = GOAPAction.CreateConnector("Basic_Any_Item");
        private static IGOAPAction basicAnyThink = GOAPAction.CreateConnector("Basic_Any_Think");

        private static IGOAPAction basicComputer = GOAPAction.CreateConnector("Basic_Computer");
        private static IGOAPAction basicItem = GOAPAction.CreateConnector("Basic_Item");
        private static IGOAPAction basicThink = GOAPAction.CreateConnector("Basic_Think");


        public static void Create()
        {
            CreateBasic();
            CreateBasicAny();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(basicItem);
            GOAPActionsManager.Instance.Add(basicThink);
            GOAPActionsManager.Instance.Add(basicComputer);

            GOAPActionsManager.Instance.Add(basicAnyItem);
            GOAPActionsManager.Instance.Add(basicAnyThink);
            GOAPActionsManager.Instance.Add(basicAnyComputer);
        }

        private static void CreateBasicAny()
        {
            basicAnyComputer.SetCost(new BaseCost(0));
            basicAnyComputer.GetEffect().Set("Basic", "Any");
            basicAnyComputer.GetPreconditions().Add("Basic", "Computer");

            basicAnyItem.SetCost(new BaseCost(0));
            basicAnyItem.GetEffect().Set("Basic", "Any");
            basicAnyItem.GetPreconditions().Add("Basic", "Item");

            basicAnyThink.SetCost(new BaseCost(0));
            basicAnyThink.GetEffect().Set("Basic", "Any");
            basicAnyThink.GetPreconditions().Add("Basic", "Think");
        }

        private static void CreateBasic()
        {
            basicComputer.SetCost(new BaseCost(0));
            basicComputer.GetEffect().Set("Basic", "Computer");
            basicComputer.GetPreconditions().Add("Location", "Desk");
            basicComputer.GetPreconditions().Add("Program", "Allowed");

            basicItem.SetCost(new BaseCost(0));
            basicItem.GetEffect().Set("Basic", "Item");
            basicItem.GetPreconditions().Add("Location", "Desk");
            basicItem.GetPreconditions().Add("Item", "Allowed");

            basicThink.SetCost(new BaseCost(0));
            basicThink.GetEffect().Set("Basic", "Think");
            basicThink.GetPreconditions().Add("Location", "Desk");
            basicThink.GetPreconditions().Add("Think", true);
        }
    }
}
