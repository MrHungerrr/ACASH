using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;


namespace Application
{
    public static class GOAPActionsItemFactory
    {
        //-------------------------------------------------------------------
        //ITEM
        private static IGOAPAction itemPhone = GOAPAction.Create("UseItem_Phone");

        //-------------------------------------------------------------------
        //ITEM ANY
        private static IGOAPAction itemAnyPhone = GOAPAction.CreateConnector("UseItem_Any_Phone");

        //-------------------------------------------------------------------
        //ITEM ALLOWED
        private static IGOAPAction itemAllowPhone = GOAPAction.CreateConnector("UseItem_Allow_Phone");

        //-------------------------------------------------------------------
        //ITEM PROHIBITED
        private static IGOAPAction itemProhibitPhone = GOAPAction.CreateConnector("UseItem_Prohibit_Phone");



        public static void Create()
        {
            CreateItem();
            CreateItemAny();
            CreateItemAllow();
            CreateItemProhibit();
        }

        public static void AddAllActions()
        {
            GOAPActionsManager.Instance.Add(itemPhone);

            GOAPActionsManager.Instance.Add(itemAnyPhone);

            GOAPActionsManager.Instance.Add(itemAllowPhone);

            GOAPActionsManager.Instance.Add(itemProhibitPhone);
        }

        private static void CreateItem()
        {
            itemPhone.SetCost(new BaseCost(10));
            itemPhone.GetEffect().Set("Item", "Phone");
            itemPhone.GetPreconditions().Add("Item_Phone_Have", true);
        }

        private static void CreateItemAny()
        {
            itemAnyPhone.SetCost(new BaseCost(0));
            itemAnyPhone.GetEffect().Set("Item", "Any");
            itemAnyPhone.GetPreconditions().Add("Item", "Phone");
        }

        private static void CreateItemAllow()
        {
            itemAllowPhone.SetCost(new BaseCost(0));
            itemAllowPhone.GetEffect().Set("Item", "Allowed");
            itemAllowPhone.GetPreconditions().Add("Item_Phone_Allowed", true);
            itemAllowPhone.GetPreconditions().Add("Item", "Phone");
        }

        private static void CreateItemProhibit()
        {
            itemProhibitPhone.SetCost(new BaseCost(0));
            itemProhibitPhone.GetEffect().Set("Item", "Prohibited");
            itemProhibitPhone.GetPreconditions().Add("Item_Phone_Allowed", false);
            itemProhibitPhone.GetPreconditions().Add("Item", "Phone");
        }
    }
}
