using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Searching;


public class ComputerUIColliderManager : MonoBehaviour
{

    [HideInInspector]
    private ComputerUICollider[] colliders;
    [SerializeField]
    private GameObject[] ui_colliders;



    [ContextMenu("Fill")]
    public void Fill()
    {
        SIC<ComputerUISelect>.Components(gameObject, out ui_colliders);
    }




    public void Setup()
    {
        SIC<ComputerUISelect>.Components(gameObject, out ui_colliders);

        List <ComputerUICollider> colliders_list = new List<ComputerUICollider>();

        foreach (GameObject i in ui_colliders)
        {
            ComputerUICollider buf_collider = new ComputerUICollider(i);
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

