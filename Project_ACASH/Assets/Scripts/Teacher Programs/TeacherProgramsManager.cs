using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supervision;
using Minimap;
using Single;

public class TeacherProgramsManager: Singleton<TeacherProgramsManager>
{
    public void SetLevel()
    {
        SupervisionManager.Instance.SetLevel();
        MinimapManager.Instance.SetLevel();
    }
}

