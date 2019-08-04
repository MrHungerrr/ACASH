using UnityEngine;
using System.Collections;

public class ObjectSelect : MonoBehaviour
{

    private Material mat;



    private void Awake()
    {
            mat = GetComponent<Renderer>().material;
            Deselect();
    }

    public void Select()
    {
            mat.SetColor("_EmissionColor", new Color(0.55f, 0.55f, 0.55f));
    }

    public void Deselect()
    {
            mat.SetColor("_EmissionColor", new Color(1.5f, 1.5f, 1.5f));
    }
}
