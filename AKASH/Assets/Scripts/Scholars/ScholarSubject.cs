using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarSubject : Subject
{
    [HideInInspector]
    public Scholar owner;

    private void Start()
    {
        owner = transform.parent.transform.parent.transform.Find("Scholar Holder").transform.Find("Script Holder").transform.GetComponentInChildren<Scholar>();
        this.tag = "ScholarSubject";
    }



    public void Execute(string key)
    {
        StartCoroutine(Ex(key));
        Execute();
    }


    private IEnumerator Ex(string key)
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));

        if (!owner.executed)
            owner.BullingForSubjects(key, this.name + "_");
    }
}
