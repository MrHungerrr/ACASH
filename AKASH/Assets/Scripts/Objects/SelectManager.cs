using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class SelectManager : Singleton<SelectManager>
{
    public Material mat;
    public Material select_mat;
    [HideInInspector]
    public Color col;
    [HideInInspector]
    public Color select_col;
    [HideInInspector]
    public Color select_text;

    private void Awake()
    {
        col = new Color(0.7f, 0.7f, 0.7f);
        select_col = new Color(2f, 2f, 2f);
        select_text = new Color(1f, 1f, 1f);
    }
}
