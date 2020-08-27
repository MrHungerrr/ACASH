using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRemover : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        Destroy(this);
    }
}
