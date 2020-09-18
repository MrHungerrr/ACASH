﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minimap;
using Vkimow.Tools.Single;

public class TeacherRoomManager: Singleton<TeacherRoomManager>
{
    public void SetLevel()
    {
        MinimapManager.Instance.SetLevel();
    }
}
