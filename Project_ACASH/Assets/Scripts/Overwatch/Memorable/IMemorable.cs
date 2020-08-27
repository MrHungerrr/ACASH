using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Overwatch.Memorable
{
    public interface IMemorable: IDisappearable, ICuttable
    {
        void StartWatch();

        IMemorableInfo Capture();

        void Remember(in IMemorableInfo info);
    }
}
