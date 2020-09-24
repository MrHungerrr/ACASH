using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minimap;
using Vkimow.Tools.Single;

public class ExaminerRoomManager: Singleton<ExaminerRoomManager>
{
    public void SetupSchool()
    {
        MinimapManager.Instance.SetupSchool();
    }
}

