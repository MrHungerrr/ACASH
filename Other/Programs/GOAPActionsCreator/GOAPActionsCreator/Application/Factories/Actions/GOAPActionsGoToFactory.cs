using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsGoToFactory
    {
        //-------------------------------------------------------------------
        //GO TO
        private static IGOAPAction goToToilet = GOAPAction.Create("GoTo_Toilet");
        private static IGOAPAction goToSink = GOAPAction.Create("GoTo_Sink");
        private static IGOAPAction goToHallway = GOAPAction.Create("GoTo_Hallway");
        private static IGOAPAction goToDesk = GOAPAction.Create("GoTo_Desk");
        private static IGOAPAction goToDockStation = GOAPAction.Create("GoTo_DockStation");

        //-------------------------------------------------------------------
        //GO TO ANY
        private static IGOAPAction goToAnyToilet = GOAPAction.CreateConnector("GoTo_Any_Toilet");
        private static IGOAPAction goToAnySink = GOAPAction.CreateConnector("GoTo_Any_Sink");
        private static IGOAPAction goToAnyHallway = GOAPAction.CreateConnector("GoTo_Any_Hallway");
        private static IGOAPAction goToAnyDesk = GOAPAction.CreateConnector("GoTo_Any_Desk");

        //-------------------------------------------------------------------
        //GO TO ALLOWED
        private static IGOAPAction goToAllowToilet = GOAPAction.CreateConnector("GoTo_Allow_Toilet");
        private static IGOAPAction goToAllowSink = GOAPAction.CreateConnector("GoTo_Allow_Sink");
        private static IGOAPAction goToAllowHallway = GOAPAction.CreateConnector("GoTo_Allow_Hallway");
        private static IGOAPAction goToAllowDesk = GOAPAction.CreateConnector("GoTo_Allow_Desk");

        //-------------------------------------------------------------------
        //GO TO PROHIBITED
        private static IGOAPAction goToProhibitToilet = GOAPAction.CreateConnector("GoTo_Prohibit_Toilet");
        private static IGOAPAction goToProhibitSink = GOAPAction.CreateConnector("GoTo_Prohibit_Sink");
        private static IGOAPAction goToProhibitHallway = GOAPAction.CreateConnector("GoTo_Prohibit_Hallway");



        public static void Create()
        {
            CreateGoTo();
            CreateGoToAny();
            CreateGoToAllow();
            CreateGoToProhibit();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(goToToilet);
            GOAPActionsManager.Instance.Add(goToSink);
            GOAPActionsManager.Instance.Add(goToHallway);
            GOAPActionsManager.Instance.Add(goToDesk);
            GOAPActionsManager.Instance.Add(goToDockStation);

            GOAPActionsManager.Instance.Add(goToAnyToilet);
            GOAPActionsManager.Instance.Add(goToAnySink);
            GOAPActionsManager.Instance.Add(goToAnyHallway);
            GOAPActionsManager.Instance.Add(goToAnyDesk);

            GOAPActionsManager.Instance.Add(goToAllowToilet);
            GOAPActionsManager.Instance.Add(goToAllowSink);
            GOAPActionsManager.Instance.Add(goToAllowHallway);
            GOAPActionsManager.Instance.Add(goToAllowDesk);

            GOAPActionsManager.Instance.Add(goToProhibitToilet);
            GOAPActionsManager.Instance.Add(goToProhibitSink);
            GOAPActionsManager.Instance.Add(goToProhibitHallway);
        }

        private static void CreateGoTo()
        {
            goToToilet.SetCost(new BaseCost(10));
            goToToilet.GetEffect().Set("Location", "Toilet");
            goToToilet.GetPreconditions().Add("Place_Toilet_All_Busy", false);

            goToSink.SetCost(new BaseCost(10));
            goToSink.GetEffect().Set("Location", "Sink");
            goToSink.GetPreconditions().Add("Place_Sink_All_Busy", false);

            goToHallway.SetCost(new BaseCost(10));
            goToHallway.GetEffect().Set("Location", "Hallway");
            goToHallway.GetPreconditions().Add("Place_Hallway_All_Busy", false);

            goToDesk.SetCost(new BaseCost(10));
            goToDesk.GetEffect().Set("Location", "Desk");

            goToDockStation.SetCost(new BaseCost(10));
            goToDockStation.GetEffect().Set("Location", "DockStation");
        }

        private static void CreateGoToAny()
        {
            goToAnyToilet.SetCost(new BaseCost(0));
            goToAnyToilet.GetEffect().Set("Location", "Any");
            goToAnyToilet.GetPreconditions().Add("Location", "Toilet");

            goToAnySink.SetCost(new BaseCost(0));
            goToAnySink.GetEffect().Set("Location", "Any");
            goToAnySink.GetPreconditions().Add("Location", "Sink");

            goToAnyHallway.SetCost(new BaseCost(0));
            goToAnyHallway.GetEffect().Set("Location", "Any");
            goToAnyHallway.GetPreconditions().Add("Location", "Hallway");

            goToAnyDesk.SetCost(new BaseCost(0));
            goToAnyDesk.GetEffect().Set("Location", "Any");
            goToAnyDesk.GetPreconditions().Add("Location", "Desk");
        }


        private static void CreateGoToAllow()
        {
            goToAllowToilet.SetCost(new BaseCost(0));
            goToAllowToilet.GetEffect().Set("Location", "Allowed");
            goToAllowToilet.GetPreconditions().Add("Place_Toilet_Allowed", true);
            goToAllowToilet.GetPreconditions().Add("Location", "Toilet");

            goToAllowSink.SetCost(new BaseCost(0));
            goToAllowSink.GetEffect().Set("Location", "Allowed");
            goToAllowSink.GetPreconditions().Add("Place_Sink_Allowed", true);
            goToAllowSink.GetPreconditions().Add("Location", "Sink");

            goToAllowHallway.SetCost(new BaseCost(0));
            goToAllowHallway.GetEffect().Set("Location", "Allowed");
            goToAllowHallway.GetPreconditions().Add("Place_Hallway_Allowed", true);
            goToAllowHallway.GetPreconditions().Add("Location", "Hallway");

            goToAllowDesk.SetCost(new BaseCost(0));
            goToAllowDesk.GetEffect().Set("Location", "Allowed");
            goToAllowDesk.GetPreconditions().Add("Location", "Desk");
        }

        private static void CreateGoToProhibit()
        {
            goToProhibitToilet.SetCost(new BaseCost(0));
            goToProhibitToilet.GetEffect().Set("Location", "Prohibited");
            goToProhibitToilet.GetPreconditions().Add("Place_Toilet_Allowed", false);
            goToProhibitToilet.GetPreconditions().Add("Location", "Toilet");

            goToProhibitSink.SetCost(new BaseCost(0));
            goToProhibitSink.GetEffect().Set("Location", "Prohibited");
            goToProhibitSink.GetPreconditions().Add("Place_Sink_Allowed", false);
            goToProhibitSink.GetPreconditions().Add("Location", "Sink");

            goToProhibitHallway.SetCost(new BaseCost(0));
            goToProhibitHallway.GetEffect().Set("Location", "Prohibited");
            goToProhibitHallway.GetPreconditions().Add("Place_Hallway_Allowed", false);
            goToProhibitHallway.GetPreconditions().Add("Location", "Hallway");
        }
    }
}
