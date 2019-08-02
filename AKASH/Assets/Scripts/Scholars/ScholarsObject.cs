using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarsObject : MonoBehaviour
{
    [HideInInspector]
    public GameObject owner;

    private void Start()
    {
        owner = transform.parent.transform.parent.transform.Find("Model").gameObject;
    }

    public void Execute()
    {
        gameObject.SetActive(false);
    }
}
