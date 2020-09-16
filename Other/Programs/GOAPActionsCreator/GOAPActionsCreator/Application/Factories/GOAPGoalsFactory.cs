using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;

namespace Application
{
    public static class GOAPGoalsFactory
    {
        public static void Create()
        {
            GOAPGoalsManager.Instance.Add("Cheat", "Cheat", true);
            GOAPGoalsManager.Instance.Add("Special", "Special", true);
        }
    }
}
