using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject: MonoBehaviour
{

    private void Start()
    {
        this.tag = "Subject";
    }

    public void Execute()
    {
        StartCoroutine(Ex());
    }

    private IEnumerator Ex()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
