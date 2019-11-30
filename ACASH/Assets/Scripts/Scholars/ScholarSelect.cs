using UnityEngine;
using System.Collections;
using TMPro;

public class ScholarSelect : MonoBehaviour
{

    private Material mat;
    private TextMeshProUGUI emotion;


    private void Awake()
    {
        Renderer buf;

        if (TryGetComponent<Renderer>(out buf))
            mat = buf.material;

        emotion = transform.parent.Find("Face").GetComponent<TextMeshProUGUI>();

        Deselect();
    }

    public void Select()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", new Color(0.55f, 0.55f, 0.55f));
        }

        emotion.color = new Color(0.55f, 0.55f, 0.55f);
    }

    public void Deselect()
    {
        if (mat != null)
        {
            mat.SetColor("_EmissionColor", new Color(1.5f, 1.5f, 1.5f));
        }

        emotion.color = new Color(1f, 1f, 1f);
    }
}
