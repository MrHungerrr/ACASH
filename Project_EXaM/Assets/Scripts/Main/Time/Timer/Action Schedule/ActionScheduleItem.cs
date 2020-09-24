using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTime.Action
{
    class ActionScheduleItem
    {
        public int TimeToInvoke { get; private set; }

        private readonly System.Action _action;

        public ActionScheduleItem(System.Action action, int timeToInvoke)
        {
            _action = action;
            TimeToInvoke = timeToInvoke;
        }

        public void SecondDone()
        {
            TimeToInvoke--;
        }

        public void Invoke()
        {
            _action.Invoke();
        }
    }
}
