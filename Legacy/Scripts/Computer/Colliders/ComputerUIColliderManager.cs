using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Searching;


public class ComputerUIColliderManager : MonoBehaviour
{

    [HideInInspector]
    private ComputerUICollider[] colliders;




    public void Setup()
    {
        ComputerUISelect[] selects;

        SIC<ComputerUISelect>.Components(gameObject, out selects);

        List <ComputerUICollider> colliders_list = new List<ComputerUICollider>();

        foreach (ComputerUISelect select in selects)
        {
            ComputerUICollider buf_collider = new ComputerUICollider(select);
            colliders_list.Add(buf_collider);
        }

        colliders = colliders_list.ToArray();
    }



    public string MouseCollision(Vector2 pos)
    {

        foreach (ComputerUICollider col in colliders)
        {
            if (col.MouseCollision(pos))
            {
                return col.obj.name;
            }
        }
        return null;
    }
}

