using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsProgramFactory
    {
        //-------------------------------------------------------------------
        //PROGRAM
        private static IGOAPAction programCalculator = GOAPAction.Create("UseProgram_Calculator");
        private static IGOAPAction programDictionary = GOAPAction.Create("UseProgram_Dictionary");
        private static IGOAPAction programBrowser = GOAPAction.Create("UseProgram_Browser");
        private static IGOAPAction programRules = GOAPAction.Create("UseProgram_Rules");
        private static IGOAPAction programText = GOAPAction.Create("UseProgram_Text");
        private static IGOAPAction programTest = GOAPAction.Create("UseProgram_Test");
        private static IGOAPAction programCode = GOAPAction.Create("UseProgram_Code");

        //-------------------------------------------------------------------
        //PROGRAM ANY
        private static IGOAPAction programAnyCalculator = GOAPAction.CreateConnector("UseProgram_Any_Calculator");
        private static IGOAPAction programAnyDictionary = GOAPAction.CreateConnector("UseProgram_Any_Dictionary");
        private static IGOAPAction programAnyBrowser = GOAPAction.CreateConnector("UseProgram_Any_Browser");
        private static IGOAPAction programAnyRules = GOAPAction.CreateConnector("UseProgram_Any_Rules");
        private static IGOAPAction programAnyText = GOAPAction.CreateConnector("UseProgram_Any_Text");
        private static IGOAPAction programAnyTest = GOAPAction.CreateConnector("UseProgram_Any_Test");
        private static IGOAPAction programAnyCode = GOAPAction.CreateConnector("UseProgram_Any_Code");

        //-------------------------------------------------------------------
        //PROGRAM ALLOWED
        private static IGOAPAction programAllowCalculator = GOAPAction.CreateConnector("UseProgram_Allow_Calculator");
        private static IGOAPAction programAllowDictionary = GOAPAction.CreateConnector("UseProgram_Allow_Dictionary");
        private static IGOAPAction programAllowBrowser = GOAPAction.CreateConnector("UseProgram_Allow_Browser");
        private static IGOAPAction programAllowRules = GOAPAction.CreateConnector("UseProgram_Allow_Rules");
        private static IGOAPAction programAllowText = GOAPAction.CreateConnector("UseProgram_Allow_Text");
        private static IGOAPAction programAllowTest = GOAPAction.CreateConnector("UseProgram_Allow_Test");
        private static IGOAPAction programAllowCode = GOAPAction.CreateConnector("UseProgram_Allow_Code");

        //-------------------------------------------------------------------
        //PROGRAM PROHIBITED
        private static IGOAPAction programProhibitCalculator = GOAPAction.CreateConnector("UseProgram_Prohibit_Calculator");
        private static IGOAPAction programProhibitDictionary = GOAPAction.CreateConnector("UseProgram_Prohibit_Dictionary");
        private static IGOAPAction programProhibitBrowser = GOAPAction.CreateConnector("UseProgram_Prohibit_Browser");
        private static IGOAPAction programProhibitText = GOAPAction.CreateConnector("UseProgram_Prohibit_Text");
        private static IGOAPAction programProhibitCode = GOAPAction.CreateConnector("UseProgram_Prohibit_Code");



        public static void Create()
        {
            CreateProgram();
            CreateProgramAny();
            CreateProgramAllow();
            CreateProgramProhibit();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(programCalculator);
            GOAPActionsManager.Instance.Add(programDictionary);
            GOAPActionsManager.Instance.Add(programBrowser);
            GOAPActionsManager.Instance.Add(programRules);
            GOAPActionsManager.Instance.Add(programText);
            GOAPActionsManager.Instance.Add(programTest);
            GOAPActionsManager.Instance.Add(programCode);

            GOAPActionsManager.Instance.Add(programAnyCalculator);
            GOAPActionsManager.Instance.Add(programAnyDictionary);
            GOAPActionsManager.Instance.Add(programAnyBrowser);
            GOAPActionsManager.Instance.Add(programAnyRules);
            GOAPActionsManager.Instance.Add(programAnyText);
            GOAPActionsManager.Instance.Add(programAnyTest);
            GOAPActionsManager.Instance.Add(programAnyCode);

            GOAPActionsManager.Instance.Add(programAllowCalculator);
            GOAPActionsManager.Instance.Add(programAllowDictionary);
            GOAPActionsManager.Instance.Add(programAllowBrowser);
            GOAPActionsManager.Instance.Add(programAllowRules);
            GOAPActionsManager.Instance.Add(programAllowText);
            GOAPActionsManager.Instance.Add(programAllowTest);
            GOAPActionsManager.Instance.Add(programAllowCode);

            GOAPActionsManager.Instance.Add(programProhibitCalculator);
            GOAPActionsManager.Instance.Add(programProhibitDictionary);
            GOAPActionsManager.Instance.Add(programProhibitBrowser);
            GOAPActionsManager.Instance.Add(programProhibitText);
            GOAPActionsManager.Instance.Add(programProhibitCode);
        }

        private static void CreateProgram()
        {
            programCalculator.SetCost(new BaseCost(10));
            programCalculator.GetEffect().Set("Program", "Calculator");

            programDictionary.SetCost(new BaseCost(10));
            programDictionary.GetEffect().Set("Program", "Dictionary");

            programBrowser.SetCost(new BaseCost(10));
            programBrowser.GetEffect().Set("Program", "Browser");

            programRules.SetCost(new BaseCost(10));
            programRules.GetEffect().Set("Program", "Rules");

            programText.SetCost(new BaseCost(10));
            programText.GetEffect().Set("Program", "Text");

            programTest.SetCost(new BaseCost(10));
            programTest.GetEffect().Set("Program", "Test");

            programCode.SetCost(new BaseCost(10));
            programCode.GetEffect().Set("Program", "Code");
        }


        private static void CreateProgramAny()
        {
            programAnyCalculator.SetCost(new BaseCost(0));
            programAnyCalculator.GetEffect().Set("Program", "Any");
            programAnyCalculator.GetPreconditions().Add("Program", "Calculator");

            programAnyDictionary.SetCost(new BaseCost(0));
            programAnyDictionary.GetEffect().Set("Program", "Any");
            programAnyDictionary.GetPreconditions().Add("Program", "Dictionary");

            programAnyBrowser.SetCost(new BaseCost(0));
            programAnyBrowser.GetEffect().Set("Program", "Any");
            programAnyBrowser.GetPreconditions().Add("Program", "Browser");

            programAnyRules.SetCost(new BaseCost(0));
            programAnyRules.GetEffect().Set("Program", "Any");
            programAnyRules.GetPreconditions().Add("Program", "Rules");

            programAnyText.SetCost(new BaseCost(0));
            programAnyText.GetEffect().Set("Program", "Any");
            programAnyText.GetPreconditions().Add("Program", "Text");

            programAnyTest.SetCost(new BaseCost(0));
            programAnyTest.GetEffect().Set("Program", "Any");
            programAnyTest.GetPreconditions().Add("Program", "Test");

            programAnyCode.SetCost(new BaseCost(0));
            programAnyCode.GetEffect().Set("Program", "Any");
            programAnyCode.GetPreconditions().Add("Program", "Code");
        }

        private static void CreateProgramAllow()
        {
            programAllowCalculator.SetCost(new BaseCost(0));
            programAllowCalculator.GetEffect().Set("Program", "Allowed");
            programAllowCalculator.GetPreconditions().Add("Program_Calculator_Allowed", true);
            programAllowCalculator.GetPreconditions().Add("Program", "Calculator");

            programAllowDictionary.SetCost(new BaseCost(0));
            programAllowDictionary.GetEffect().Set("Program", "Allowed");
            programAllowDictionary.GetPreconditions().Add("Program_Dictionary_Allowed", true);
            programAllowDictionary.GetPreconditions().Add("Program", "Dictionary");

            programAllowBrowser.SetCost(new BaseCost(0));
            programAllowBrowser.GetEffect().Set("Program", "Allowed");
            programAllowBrowser.GetPreconditions().Add("Program_Browser_Allowed", true);
            programAllowBrowser.GetPreconditions().Add("Program", "Browser");
                    
            programAllowRules.SetCost(new BaseCost(0));
            programAllowRules.GetEffect().Set("Program", "Allowed");
            programAllowRules.GetPreconditions().Add("Program_Rules_Allowed", true);
            programAllowRules.GetPreconditions().Add("Program", "Rules");
                    
            programAllowText.SetCost(new BaseCost(0));
            programAllowText.GetEffect().Set("Program", "Allowed");
            programAllowText.GetPreconditions().Add("Program_Text_Allowed", true);
            programAllowText.GetPreconditions().Add("Program", "Text");
                    
            programAllowTest.SetCost(new BaseCost(0));
            programAllowTest.GetEffect().Set("Program", "Allowed");
            programAllowTest.GetPreconditions().Add("Program_Test_Allowed", true);
            programAllowTest.GetPreconditions().Add("Program", "Test");
                    
            programAllowCode.SetCost(new BaseCost(0));
            programAllowCode.GetEffect().Set("Program", "Allowed");
            programAllowCode.GetPreconditions().Add("Program_Code_Allowed", true);
            programAllowCode.GetPreconditions().Add("Program", "Code");
        }

        private static void CreateProgramProhibit()
        {
            programProhibitCalculator.SetCost(new BaseCost(0));
            programProhibitCalculator.GetEffect().Set("Program", "Prohibited");
            programProhibitCalculator.GetPreconditions().Add("Program_Calculator_Allowed", false);
            programProhibitCalculator.GetPreconditions().Add("Program", "Calculator");

            programProhibitDictionary.SetCost(new BaseCost(0));
            programProhibitDictionary.GetEffect().Set("Program", "Prohibited");
            programProhibitDictionary.GetPreconditions().Add("Program_Dictionary_Allowed", false);
            programProhibitDictionary.GetPreconditions().Add("Program", "Dictionary");

            programProhibitBrowser.SetCost(new BaseCost(0));
            programProhibitBrowser.GetEffect().Set("Program", "Prohibited");
            programProhibitBrowser.GetPreconditions().Add("Program_Browser_Allowed", true);
            programProhibitBrowser.GetPreconditions().Add("Program", "Browser");
                   
            programProhibitText.SetCost(new BaseCost(0));
            programProhibitText.GetEffect().Set("Program", "Prohibited");
            programProhibitText.GetPreconditions().Add("Program_Text_Allowed", true);
            programProhibitText.GetPreconditions().Add("Program", "Text");
                   
            programProhibitCode.SetCost(new BaseCost(0));
            programProhibitCode.GetEffect().Set("Program", "Prohibited");
            programProhibitCode.GetPreconditions().Add("Program_Code_Allowed", true);
            programProhibitCode.GetPreconditions().Add("Program", "Code");
        }
    }
}
