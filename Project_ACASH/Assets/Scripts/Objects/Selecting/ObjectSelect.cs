using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class ObjectSelect : MonoBehaviour, IObjectSelect
{
    protected static float range = 0.6f;
    protected Material[] mats;

    [SerializeField]
    public Renderer[] renderers;



    protected virtual void Awake()
    {
        SetSelect();
    }


    protected virtual void SetSelect()
    {
        Renderer buf;
        List<Material> list = new List<Material>();

        if (TryGetComponent<Renderer>(out buf))
        {
            list.Add(buf.material);
        }

        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                list.Add(renderers[i].material);
            }
        }

        mats = list.ToArray();

        Deselect();
    }




    public virtual void Select()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.select_col);
        }
    }



    public virtual void Deselect()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.col);
        }
    }



    public virtual bool CanISelect()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < range)
            return true;
        else
            return false;
    }
}
