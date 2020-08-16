using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overwatch.Watchable
{
    public interface IDisappearable
    {
        bool IsHere { get; }
        void Appear(bool option);
    }
}
