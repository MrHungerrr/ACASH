using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTime.Action
{
    public interface ITimeAction
    {
        void Update(in float deltaTime);
    }
}
