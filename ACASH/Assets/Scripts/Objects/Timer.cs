using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshPro text;

    private void Awake()
    {
        text = transform.GetComponentInChildren<TextMeshPro>();
    }


    public void Time(string t)
    {
        text.text = t;
    }
}
