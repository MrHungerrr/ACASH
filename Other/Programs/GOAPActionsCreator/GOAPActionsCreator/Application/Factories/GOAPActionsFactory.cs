using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsFactory
    {
        //-------------------------------------------------------------------
        //CHEAT

        private static IGOAPAction cheatNote = new GOAPAction("Cheat_Note");
        private static IGOAPAction cheatNoteToilet = new GOAPAction("Cheat_Note_Toilet", true);
        private static IGOAPAction cheatNoteSink = new GOAPAction("Cheat_Note_Sink", true);
        private static IGOAPAction cheatNoteOutside = new GOAPAction("Cheat_Note_Outside", true);
        private static IGOAPAction cheatNoteDesk = new GOAPAction("Cheat_Note_Desk", true);

        private static IGOAPAction cheatPhone = new GOAPAction("Cheat_Phone");
        private static IGOAPAction cheatPhoneToilet = new GOAPAction("Cheat_Phone_Toilet", true);
        private static IGOAPAction cheatPhoneSink = new GOAPAction("Cheat_Phone_Sink", true);
        private static IGOAPAction cheatPhoneOutside = new GOAPAction("Cheat_Phone_Outside", true);
        private static IGOAPAction cheatPhoneDesk = new GOAPAction("Cheat_Phone_Desk", true);

        private static IGOAPAction cheatCalculator = new GOAPAction("Cheat_Calculator");
        private static IGOAPAction cheatCalculatorToilet = new GOAPAction("Cheat_Calculator_Toilet", true);
        private static IGOAPAction cheatCalculatorSink = new GOAPAction("Cheat_Calculator_Sink", true);
        private static IGOAPAction cheatCalculatorOutside = new GOAPAction("Cheat_Calculator_Outside", true);
        private static IGOAPAction cheatCalculatorDesk = new GOAPAction("Cheat_Calculator_Desk", true);

        //-------------------------------------------------------------------
        //SPECIAL

        private static IGOAPAction specialGoOut = new GOAPAction("Special_Go_Out", true);
        private static IGOAPAction specialGoPee = new GOAPAction("Special_Go_Pee", true);
        private static IGOAPAction specialGoWashHands = new GOAPAction("Special_Go_WashHands", true);
        private static IGOAPAction specialGoAir = new GOAPAction("Special_Go_Air", true);

        private static IGOAPAction pee = new GOAPAction("Special_Pee");
        private static IGOAPAction washHands = new GOAPAction("Special_WashHands");
        private static IGOAPAction rest = new GOAPAction("Special_Rest");
        private static IGOAPAction talk = new GOAPAction("Special_Talk");

        //-------------------------------------------------------------------
        //GO TO

        private static IGOAPAction goToToilet = new GOAPAction("GoTo_Toilet");
        private static IGOAPAction goToSink = new GOAPAction("GoTo_Sink");
        private static IGOAPAction goToOutside = new GOAPAction("GoTo_Outside");
        private static IGOAPAction goToDesk = new GOAPAction("GoTo_Desk");
        private static IGOAPAction goToDockStation = new GOAPAction("GoTo_DockStation");


        public static void Create()
        {
            CreateGoTo();
            CreateCheat();
            CreateSpecial();
            AddAllAcitons();
        }


        private static void CreateCheat()
        {
            CreateCheatNote();
            CreateCheatPhone();
            CreateCheatCalculator();
        }

        private static void CreateSpecial()
        {
            CreateSpecialActions();
            CreateSpecialGoOut();
        }

        private static void AddAllAcitons()
        {
            GOAPActionsManager.Instance.Add(cheatNote);
            GOAPActionsManager.Instance.Add(cheatNoteToilet);
            GOAPActionsManager.Instance.Add(cheatNoteSink);
            GOAPActionsManager.Instance.Add(cheatNoteOutside);
            GOAPActionsManager.Instance.Add(cheatNoteDesk);

            GOAPActionsManager.Instance.Add(cheatPhone);
            GOAPActionsManager.Instance.Add(cheatPhoneToilet);
            GOAPActionsManager.Instance.Add(cheatPhoneSink);
            GOAPActionsManager.Instance.Add(cheatPhoneOutside);
            GOAPActionsManager.Instance.Add(cheatPhoneDesk);

            GOAPActionsManager.Instance.Add(cheatCalculator);
            GOAPActionsManager.Instance.Add(cheatCalculatorToilet);
            GOAPActionsManager.Instance.Add(cheatCalculatorSink);
            GOAPActionsManager.Instance.Add(cheatCalculatorOutside);
            GOAPActionsManager.Instance.Add(cheatCalculatorDesk);

            GOAPActionsManager.Instance.Add(specialGoOut);
            GOAPActionsManager.Instance.Add(specialGoPee);
            GOAPActionsManager.Instance.Add(specialGoWashHands);
            GOAPActionsManager.Instance.Add(specialGoAir);

            GOAPActionsManager.Instance.Add(pee);
            GOAPActionsManager.Instance.Add(washHands);
            GOAPActionsManager.Instance.Add(rest);
            GOAPActionsManager.Instance.Add(talk);

            GOAPActionsManager.Instance.Add(goToToilet);
            GOAPActionsManager.Instance.Add(goToSink);
            GOAPActionsManager.Instance.Add(goToOutside);
            GOAPActionsManager.Instance.Add(goToDesk);
            GOAPActionsManager.Instance.Add(goToDockStation);
        }

        private static void CreateCheatNote()
        {
            cheatNote.SetCost(new BaseCost(15));
            cheatNote.GetEffect().Set("Cheat", true);
            cheatNote.GetPreconditions().Add("Items_Have_Note", true);
            cheatNote.GetPreconditions().Add("Cheat_Note", true);

            cheatNoteToilet.SetCost(new BaseCost(0));
            cheatNoteToilet.GetEffect().Set("Cheat_Note", true);
            cheatNoteToilet.GetPreconditions().Add("Location", "Toilet");

            cheatNoteSink.SetCost(new BaseCost(0));
            cheatNoteSink.GetEffect().Set("Cheat_Note", true);
            cheatNoteSink.GetPreconditions().Add("Location", "Sink");

            cheatNoteOutside.SetCost(new BaseCost(0));
            cheatNoteOutside.GetEffect().Set("Cheat_Note", true);
            cheatNoteOutside.GetPreconditions().Add("Location", "Outside");

            cheatNoteDesk.SetCost(new BaseCost(0));
            cheatNoteDesk.GetEffect().Set("Cheat_Note", true);
            cheatNoteDesk.GetPreconditions().Add("Location", "Desk");
        }

        private static void CreateCheatPhone()
        {
            cheatPhone.SetCost(new BaseCost(10));
            cheatPhone.GetEffect().Set("Cheat", true);
            cheatPhone.GetPreconditions().Add("Items_Have_Phone", true);
            cheatPhone.GetPreconditions().Add("Cheat_Phone", true);

            cheatPhoneToilet.SetCost(new BaseCost(0));
            cheatPhoneToilet.GetEffect().Set("Cheat_Phone", true);
            cheatPhoneToilet.GetPreconditions().Add("Location", "Toilet");

            cheatPhoneSink.SetCost(new BaseCost(0));
            cheatPhoneSink.GetEffect().Set("Cheat_Phone", true);
            cheatPhoneSink.GetPreconditions().Add("Location", "Sink");

            cheatPhoneOutside.SetCost(new BaseCost(0));
            cheatPhoneOutside.GetEffect().Set("Cheat_Phone", true);
            cheatPhoneOutside.GetPreconditions().Add("Location", "Outside");

            cheatPhoneDesk.SetCost(new BaseCost(0));
            cheatPhoneDesk.GetEffect().Set("Cheat_Phone", true);
            cheatPhoneDesk.GetPreconditions().Add("Location", "Desk");
        }

        private static void CreateCheatCalculator()
        {
            cheatCalculator.SetCost(new BaseCost(5));
            cheatCalculator.GetEffect().Set("Cheat", true);
            cheatCalculator.GetPreconditions().Add("Items_Have_Calculator", true);
            cheatCalculator.GetPreconditions().Add("Cheat_Calculator", true);

            cheatCalculatorToilet.SetCost(new BaseCost(0));
            cheatCalculatorToilet.GetEffect().Set("Cheat_Calculator", true);
            cheatCalculatorToilet.GetPreconditions().Add("Location", "Toilet");

            cheatCalculatorSink.SetCost(new BaseCost(0));
            cheatCalculatorSink.GetEffect().Set("Cheat_Calculator", true);
            cheatCalculatorSink.GetPreconditions().Add("Location", "Sink");

            cheatCalculatorOutside.SetCost(new BaseCost(0));
            cheatCalculatorOutside.GetEffect().Set("Cheat_Calculator", true);
            cheatCalculatorOutside.GetPreconditions().Add("Location", "Outside");

            cheatCalculatorDesk.SetCost(new BaseCost(0));
            cheatCalculatorDesk.GetEffect().Set("Cheat_Calculator", true);
            cheatCalculatorDesk.GetPreconditions().Add("Location", "Desk");
        }


        private static void CreateSpecialGoOut()
        {
            specialGoOut.SetCost(new BaseCost(0));
            specialGoOut.GetEffect().Set("Special", true);
            specialGoOut.GetPreconditions().Add("Special_Go_Out", true);

            specialGoPee.SetCost(new BaseCost(0));
            specialGoPee.GetEffect().Set("Special_Go_Out", true);
            specialGoPee.GetPreconditions().Add("Want_Pee", true);
            specialGoPee.GetPreconditions().Add("Pee", true);
            specialGoPee.GetPreconditions().Add("Location", "Toilet");

            specialGoWashHands.SetCost(new BaseCost(0));
            specialGoWashHands.GetEffect().Set("Special_Go_Out", true);
            specialGoWashHands.GetPreconditions().Add("Want_Wash_Hands", true);
            specialGoWashHands.GetPreconditions().Add("Wash_Hands", true);
            specialGoWashHands.GetPreconditions().Add("Location", "Sink");

            specialGoAir.SetCost(new BaseCost(0));
            specialGoAir.GetEffect().Set("Special_Go_Out", true);
            specialGoAir.GetPreconditions().Add("Want_Rest", true);
            specialGoAir.GetPreconditions().Add("Rest", true);
            specialGoAir.GetPreconditions().Add("Location", "Outside");


        }


        private static void CreateSpecialActions()
        {
            pee.SetCost(new BaseCost(10));
            pee.GetEffect().Set("Pee", true);

            washHands.SetCost(new BaseCost(10));
            washHands.GetEffect().Set("Wash_Hands", true);

            rest.SetCost(new BaseCost(10));
            rest.GetEffect().Set("Rest", true);

            talk.SetCost(new BaseCost(10));
            talk.GetEffect().Set("Talk", true);
        }

        private static void CreateGoTo()
        {
            goToToilet.SetCost(new BaseCost(10));
            goToToilet.GetEffect().Set("Location", "Toilet");
            goToToilet.GetPreconditions().Add("Places_Toilets_Are_Busy", false);

            goToSink.SetCost(new BaseCost(10));
            goToSink.GetEffect().Set("Location", "Sink");
            goToSink.GetPreconditions().Add("Places_Sinks_Are_Busy", false);

            goToOutside.SetCost(new BaseCost(10));
            goToOutside.GetEffect().Set("Location", "Outside");
            goToOutside.GetPreconditions().Add("Places_Outside_Are_Busy", false);

            goToDesk.SetCost(new BaseCost(10));
            goToDesk.GetEffect().Set("Location", "Desk");

            goToDockStation.SetCost(new BaseCost(10));
            goToDockStation.GetEffect().Set("Location", "Dock_Station");
        }
    }
}
