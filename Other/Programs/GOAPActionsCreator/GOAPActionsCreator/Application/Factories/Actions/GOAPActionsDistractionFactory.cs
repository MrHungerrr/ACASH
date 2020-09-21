using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsDistractionFactory
    {
        //-------------------------------------------------------------------
        //DISTRACTION ANY
        private static IGOAPAction distractionAnyComputer = GOAPAction.CreateConnector("Distraction_Any_Computer");
        private static IGOAPAction distractionAnyRest = GOAPAction.CreateConnector("Distraction_Any_Special");

        //-------------------------------------------------------------------
        //COMPUTER DISTRACTION
        private static IGOAPAction distractionComputer = GOAPAction.CreateConnector("Distraction_Computer");

        //-------------------------------------------------------------------
        //SPECIAL DISTRACTION
        private static IGOAPAction distractionRestPee = GOAPAction.CreateConnector("Distarction_Rest_Pee");
        private static IGOAPAction distractionRestWashHands = GOAPAction.CreateConnector("Distarction_Rest_WashHands");
        private static IGOAPAction distractionRestAir = GOAPAction.CreateConnector("Distarction_Rest_Air");


        public static void Create()
        {
            CreateDistractionAny();
            CreateDistractionComputer();
            CreateDistractionSpecial();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(distractionAnyComputer);
            GOAPActionsManager.Instance.Add(distractionAnyRest);

            GOAPActionsManager.Instance.Add(distractionRestPee);
            GOAPActionsManager.Instance.Add(distractionRestWashHands);
            GOAPActionsManager.Instance.Add(distractionRestAir);

            GOAPActionsManager.Instance.Add(distractionComputer);
        }

        private static void CreateDistractionAny()
        {
            distractionAnyComputer.SetCost(new BaseCost(0));
            distractionAnyComputer.GetEffect().Set("Distraction", "Any");
            distractionAnyComputer.GetPreconditions().Add("Distraction", "Computer");

            distractionAnyRest.SetCost(new BaseCost(0));
            distractionAnyRest.GetEffect().Set("Distraction", "Any");
            distractionAnyRest.GetPreconditions().Add("Distraction", "Rest");
        }

        private static void CreateDistractionSpecial()
        {
            distractionRestPee.SetCost(new BaseCost(0));
            distractionRestPee.GetEffect().Set("Distraction", "Rest");
            distractionRestPee.GetPreconditions().Add("Place_Toilet_Allowed", true);
            distractionRestPee.GetPreconditions().Add("Location", "Toilet");
            distractionRestPee.GetPreconditions().Add("Pee", true);


            distractionRestWashHands.SetCost(new BaseCost(0));
            distractionRestWashHands.GetEffect().Set("Distraction", "Rest");
            distractionRestWashHands.GetPreconditions().Add("Place_Sink_Allowed", true);
            distractionRestWashHands.GetPreconditions().Add("Location", "Sink");
            distractionRestWashHands.GetPreconditions().Add("WashHands", true);


            distractionRestAir.SetCost(new BaseCost(0));
            distractionRestAir.GetEffect().Set("Distraction", "Rest");
            distractionRestAir.GetPreconditions().Add("Place_Hallway_Allowed", true);
            distractionRestAir.GetPreconditions().Add("Location", "Hallway");
            distractionRestAir.GetPreconditions().Add("Rest", true);

        }

        private static void CreateDistractionComputer()
        {
            distractionComputer.SetCost(new BaseCost(0));
            distractionComputer.GetEffect().Set("Distraction", "Computer");
            distractionComputer.GetPreconditions().Add("Location", "Desk");
            distractionComputer.GetPreconditions().Add("Program", "Allowed");
        }
    }
}
