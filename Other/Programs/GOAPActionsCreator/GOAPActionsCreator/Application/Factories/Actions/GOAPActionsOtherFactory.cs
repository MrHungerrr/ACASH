using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsOtherFactory
    {
        //-------------------------------------------------------------------
        //Rest
        private static IGOAPAction otherPee = GOAPAction.Create("Other_Pee");
        private static IGOAPAction otherWashHands = GOAPAction.Create("Other_WashHands");
        private static IGOAPAction otherRest = GOAPAction.Create("Other_Rest");
        private static IGOAPAction otherThink = GOAPAction.Create("Other_Think");
        private static IGOAPAction otherTalk = GOAPAction.Create("Other_Talk");
        private static IGOAPAction otherWait = GOAPAction.Create("Other_Wait");

        //-------------------------------------------------------------------
        //OTHER
        private static IGOAPAction otherNone = GOAPAction.CreateConnector("None");
        private static IGOAPAction otherIdle = GOAPAction.CreateConnector("Idle");
        private static IGOAPAction otherEnd = GOAPAction.CreateConnector("End");

        public static void Create()
        {
            CreateSpecial();
            CreateOther();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(otherPee);
            GOAPActionsManager.Instance.Add(otherWashHands);
            GOAPActionsManager.Instance.Add(otherRest);

            GOAPActionsManager.Instance.Add(otherThink);
            GOAPActionsManager.Instance.Add(otherTalk);
            GOAPActionsManager.Instance.Add(otherWait);

            GOAPActionsManager.Instance.Add(otherNone);
            GOAPActionsManager.Instance.Add(otherEnd);
            GOAPActionsManager.Instance.Add(otherIdle);
        }

        private static void CreateSpecial()
        {
            otherPee.SetCost(new BaseCost(10));
            otherPee.GetEffect().Set("Pee", true);

            otherWashHands.SetCost(new BaseCost(10));
            otherWashHands.GetEffect().Set("WashHands", true);

            otherRest.SetCost(new BaseCost(10));
            otherRest.GetEffect().Set("Rest", true);

            otherThink.SetCost(new BaseCost(10));
            otherThink.GetEffect().Set("Think", true);

            otherTalk.SetCost(new BaseCost(10));
            otherTalk.GetEffect().Set("Talk", true);

            otherWait.SetCost(new BaseCost(10));
            otherWait.GetEffect().Set("Wait", true);
        }

        private static void CreateOther()
        {
            otherNone.SetCost(new BaseCost(0));
            otherNone.GetEffect().Set("None", true);

            otherEnd.SetCost(new BaseCost(0));
            otherEnd.GetEffect().Set("End", true);
            otherEnd.GetPreconditions().Add("Location", "DockStation");
            otherEnd.GetPreconditions().Add("Wait", true);

            otherIdle.SetCost(new BaseCost(0));
            otherIdle.GetEffect().Set("Idle", true);
            otherIdle.GetPreconditions().Add("Wait", true);
        }
    }
}
