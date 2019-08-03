using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarsSubject : Subject
{
    [HideInInspector]
    public GameObject owner;

    private void Start()
    {
        owner = transform.parent.transform.parent.transform.Find("Scholar").transform.Find("Model").gameObject;
        this.tag = "ScholarsSubject";
    }

    public void Execute(string key)
    {
        StartCoroutine(Ex(key));
        Execute();
    }

    private IEnumerator Ex(string key)
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3f));

        switch (owner.tag)
        {
            case "Dumb":
                {
                    var scholar = owner.GetComponent<Dumb>();
                    if (!scholar.executed)
                        scholar.BullingForSubjects(key, this.name);
                    break;
                }
        }
    }
}
