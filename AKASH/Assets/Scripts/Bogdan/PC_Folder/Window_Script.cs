using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Window_Script : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    private Vector3 offset;
    private float minDist = 30;
    ScholarManager sm;

    private void Start()
    {
        sm = GameObject.FindObjectOfType<ScholarManager>();
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
    }


    private void OnEnable()
    {

        if (this.gameObject.name == "Tension_window")
        {

            sm = GameObject.FindObjectOfType<ScholarManager>();
            for (int i = 0; i < sm.scholars.Length; i++)
            {
                GameObject gm;
                gm = Instantiate<GameObject>(Resources.Load<GameObject>("PC_Prefabs/TensionOfStudent"), this.gameObject.transform.GetChild(0).transform);
                gm.transform.GetChild(2).GetComponent<Transform>().localScale = new Vector3(sm.GetStress(i) / 100f, 1, 1);
                gm.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().SetText((i+1).ToString());
            }
            StartCoroutine(TensionUpdate());
        }
    }

    IEnumerator TensionUpdate()
    {
        for (int i = 0; i < sm.scholars.Length; i++)
        {
            GameObject gm;
            gm = this.transform.GetChild(0).GetChild(i).gameObject;
            gm.transform.GetChild(2).GetComponent<Transform>().localScale = new Vector3(sm.GetStress(i) / 100f, 1, 1);
            gm.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().SetText((i + 1).ToString());
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(TensionUpdate());
    }

    private void OnDisable()
    {
        if (this.gameObject.name == "Tension_window")
        {

            sm = GameObject.FindObjectOfType<ScholarManager>();
            if(sm!=null)
            for (int i = 0; i < sm.scholars.Length; i++)
            {
                GameObject gm;
                gm = this.transform.GetChild(0).GetChild(i).gameObject;
                Destroy(gm);
            }
            StopCoroutine(TensionUpdate());
        }
    }


}
