using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsidePointRemover : MonoBehaviour
{


    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        Destroy(this);
    }
}
