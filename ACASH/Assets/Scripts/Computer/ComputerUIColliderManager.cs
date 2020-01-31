using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ComputerUIColliderManager : MonoBehaviour
{

    [HideInInspector]
    private Dictionary<string, ComputerUICollider> colliders = new Dictionary<string, ComputerUICollider>();
    [HideInInspector]
    public RectTransform mouse;
    [SerializeField]
    private GameObject[] ui_colliders;



    [ContextMenu("Fill")]
    public void Fill()
    {
        ui_colliders = FindAllUIObjects();
    }

    private GameObject[] FindAllUIObjects()
    {
        List<GameObject> res = new List<GameObject>();
        Search(this.gameObject);

        void Search(GameObject obj)
        {
            if (obj.tag == "ComputerUIObject" || obj.GetComponent<ComputerSelect>() != null)
            {
                res.Add(obj);
                obj.tag = "ComputerUIObject";
            }


            for (int i = 0; i < obj.transform.childCount; i++)
            {
                Search(obj.transform.GetChild(i).gameObject);
            }
        }

        return res.ToArray();
    }

    private void Awake()
    {
        RefreshColliders();
    }


    public void RefreshColliders()
    {
        ui_colliders = FindAllUIObjects();
        foreach (GameObject i in ui_colliders)
        {
            ComputerUICollider buf_collider = new ComputerUICollider(i);
            colliders.Add(i.name, buf_collider);
        }
    }


    public string MouseCollision(Vector2 pos)
    {

        foreach (KeyValuePair<string, ComputerUICollider> pair in colliders)
        {
            if (pair.Value.MouseCollision(pos))
            {
                return pair.Key;
            }
        }
        return null;
    }
}

