using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class ObjectSelect : MonoBehaviour, IObjectSelect
{
    protected const float RANGE = 0.6f;
    protected Material[] _materials;

    [SerializeField] private Renderer[] _renderers;



    protected virtual void Awake()
    {
        SetupMaterials();
    }


    protected virtual void SetupMaterials()
    {
        Renderer buf;
        List<Material> list = new List<Material>();

        if (TryGetComponent<Renderer>(out buf))
        {
            list.Add(buf.material);
        }

        if (_renderers != null)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                list.Add(_renderers[i].material);
            }
        }

        _materials = list.ToArray();

        Deselect();
    }




    public virtual void Select()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_EmissionColor", SelectInfo.SELECT_COL);
        }
    }



    public virtual void Deselect()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_EmissionColor", SelectInfo.COL);
        }
    }



    public virtual bool CanISelect()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < RANGE)
            return true;
        else
            return false;
    }
}
