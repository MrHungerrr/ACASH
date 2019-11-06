using UnityEngine;
using System.Collections;

public class ObjectSelect : MonoBehaviour
{

    private Material mat;
    private Material[] mats;
    [SerializeField]
    private Renderer[] renderers;



    private void Awake()
    {
        Renderer buf;

        if(TryGetComponent<Renderer>(out buf))
            mat = buf.material;

        if (renderers != null)
        {
            mats = new Material[renderers.Length];

            for (int i = 0; i< renderers.Length; i++)
            {
                mats[i] = renderers[i].material;
            }
        }

        Debug.Log(renderers.Length);
        Deselect();
    }

    public void Select()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", new Color(0.55f, 0.55f, 0.55f));
        }

        if (mats != null)
        {
            foreach(Material material in mats)
            {
                material.SetColor("_EmissionColor", new Color(0.55f, 0.55f, 0.55f));
            }
        }
    }

    public void Deselect()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", new Color(1.5f, 1.5f, 1.5f));
        }

        if (mats != null)
        {
            foreach (Material material in mats)
            {
                material.SetColor("_EmissionColor", new Color(1.5f, 1.5f, 1.5f));
            }
        }
    }
}
