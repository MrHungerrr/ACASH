using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window_Script : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    private Vector3 offset;
    private float minDist = 30;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        offset = Input.mousePosition - startPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(startPosition, transform.position) < minDist)
        {
            transform.position = startPosition;
        }
    }

    private void OnEnable()
    {

        if (this.gameObject.name == "Tension_window")
        {


            for (int i = 0; i < GameObject.FindObjectOfType<CheatHelper>().studentcount; i++)
            {
                Instantiate<GameObject>(Resources.Load<GameObject>("PC_Prefabs/Tension_Slider"), this.gameObject.transform);
            }
        }
    }
}
