using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class SelectManager : Singleton<SelectManager>
{
    public Material mat;
    public Material select_mat;
    [HideInInspector]
    public Color col = new Color(0.7f, 0.7f, 0.7f);
    [HideInInspector]
    public Color select_col = new Color(3f, 3f, 3f);
}
