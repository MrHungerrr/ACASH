using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScholarSelect : MonoBehaviour, I_ObjectSelect
{

    private bool selectable = true;

    //========================================================================================================
    //Возможность выбрать объект

    private Material mat;
    private Material[] mats;
    [SerializeField]
    private Renderer[] renderers;
    [SerializeField]
    private TextMeshPro[] texts;


    public void SetScholarSelect()
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

        Selectable(true);
        Deselect();
    }



    public void Select()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", SelectHelper.select_col);
        }

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.select_col);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = SelectHelper.select_text;
        }
    }

    public void Deselect()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", SelectHelper.col);
        }

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", SelectHelper.col);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = SelectHelper.col;
        }

    }

    public void Selectable(bool u)
    {

        if (u)
        {
            selectable = true;
            StartCoroutine(SetSelectable());
        }
        else
        {
            this.gameObject.layer = 10;
            selectable = false;
            Deselect();
        }
    }



    private IEnumerator SetSelectable()
    {
        yield return new WaitForSeconds(0.1f);
        if (selectable)
        {
            this.gameObject.layer = 9;
        }
    }
}
