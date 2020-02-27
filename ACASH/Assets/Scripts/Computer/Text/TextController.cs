using UnityEngine;
using System.Collections;
using Searching;
using TMPro;

public class TextController : MonoBehaviour
{


    [HideInInspector]
    public InputFieldText input { get; private set; }


    public void SetTextController()
    {
        Transform dictionary = transform.Find("Text");
        input = transform.GetComponentInChildren<InputFieldText>();
    }

}
