using UnityEngine;
using System.Collections;

public class ObjectSelect : MonoBehaviour
{

    private Material mat;
    public bool notSelectable;



    private void Awake()
    {
        if (!notSelectable)
        {
            mat = GetComponent<Renderer>().material;
            Deselect();
        }
    }

    public void Select()
    {
        if (!notSelectable)
        {
            mat.SetColor("_EmissionColor", new Color(0.55f, 0.55f, 0.55f));
        }
    }

    public void Deselect()
    {
        if (!notSelectable)
        {
            mat.SetColor("_EmissionColor", new Color(1.5f, 1.5f, 1.5f));
        }
    }
}
