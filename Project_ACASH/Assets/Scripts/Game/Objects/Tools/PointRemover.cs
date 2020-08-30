using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRemover : MonoBehaviour
{
    private void Start()
    {
        Destroy(GetComponent<Renderer>());
        Destroy(this);
    }
}
