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
        public static void Create()
        {
            GOAPActionsBasicFactory.Create();
            GOAPActionsCheatFactory.Create();
            GOAPActionsGoToFactory.Create();
            GOAPActionsItemFactory.Create();
            GOAPActionsProgramFactory.Create();
            GOAPActionsOtherFactory.Create();
            GOAPActionsDistractionFactory.Create();

            AddAllActions();
        }

        private static void AddAllActions()
        {
            GOAPActionsBasicFactory.AddAllActions();
            GOAPActionsCheatFactory.AddAllActions();
            GOAPActionsGoToFactory.AddAllActions();
            GOAPActionsItemFactory.AddAllActions();
            GOAPActionsProgramFactory.AddAllActions();
            GOAPActionsOtherFactory.AddAllActions();
            GOAPActionsDistractionFactory.AddAllActions();
        }
    }
}
