using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForTeacher : MonoBehaviour
{

    LayerMask actLayerMask;
    float actRange = 7f;

    private void Start()
    {
        actLayerMask = LayerMask.GetMask("Selectable");
    }

    void CameraCreate()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, actRange, actLayerMask))
        {
           // Instantiate<GameObject>("геймобъекткамеры", hit.transform.position - Vector3.back, Quaternion.Euler(Vector3.back));
        }

    }
}
