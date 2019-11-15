using UnityEngine;
using System.Collections;
using TMPro;

public class ObjectSelect : MonoBehaviour
{

    private Material mat;
    private Material[] mats;
    [SerializeField]
    private Renderer[] renderers;
    [SerializeField]
    private TextMeshPro[] texts;


    private void Awake()
    {
        Renderer buf;

        if (TryGetComponent<Renderer>(out buf))
            mat = buf.material;

        if (renderers != null)
        {
            mats = new Material[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                mats[i] = renderers[i].material;
            }
        }

        Deselect();
    }



    public void Select()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", SelectManager.get.select_col);
        }

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectManager.get.select_col);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = SelectManager.get.select_text;
        }
    }

    public void Deselect()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", SelectManager.get.col);
        }

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectManager.get.col);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = SelectManager.get.col;
        }

    }
}
