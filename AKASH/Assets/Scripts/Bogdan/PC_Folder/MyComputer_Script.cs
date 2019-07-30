using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MyComputer_Script : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    int click = 0;

    private Vector3 startPosition;
    private Vector3 offset;
    private float minDist = 70;

    // public GameObject mycomputer;
    public GameObject pc;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        click++;
        if (click == 2)
        {
            pc.SetActive(true);
            StartCoroutine(dubleclick());
        }
        else
        {
            StartCoroutine(dubleclick());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

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

        if (transform.position.x < 50) 
        {
            transform.position = new Vector2(70, transform.position.y);
        }
        if (transform.position.x > Screen.width-50)
        {
            transform.position = new Vector2( Screen.width - 70, transform.position.y);
        }
        if (transform.position.y < 50)
        {
            transform.position = new Vector2(transform.position.x, 70);
        }
        if (transform.position.y > Screen.height - 50)
        {
            transform.position = new Vector2(transform.position.x, Screen.height - 70);
        }
    }


    IEnumerator dubleclick()
    {
        yield return new WaitForSeconds(1f);
        click = 0;
    }
}
