using UnityEngine;
using System.Collections;

public class DeskBlock : MonoBehaviour
{

    private Material mat;

    private void Awake()
    {
        Renderer buf;

        if(TryGetComponent<Renderer>(out buf))
            mat = buf.material;

        Draw(false);
    }


    public void Draw(bool option)
    {
        if(option)
        {
            if (mat != null)
            {
                mat.SetColor("_EmissionColor", SelectManager.get.col);
            }
        }
        else
        {
            if (mat != null)
            {
                mat.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
            }
        }
    }

}
