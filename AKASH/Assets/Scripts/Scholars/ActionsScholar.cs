using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ActionsScholar : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool doing;
    private string keyWord;
    public Transform toilet;
    private Vector3 destination;
    private Vector3 home;


    private void Awake()
    {
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }


    public void Doing(string key)
    {
        Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key);
        keyWord = key;
    }

    private bool IsHere()
    {
        if ((transform.position - destination).magnitude <= 0.01)
            return true;
        else
            return false;
    }

    private IEnumerator Toilet_1()
    {
        SetDestination(toilet.position);

        while (!IsHere())
            yield return new WaitForSeconds(1f);
        Debug.Log("Я дошел");

        yield return new WaitForSeconds(5f);

        SetDestination(home);
        Debug.Log("Я пошел домой");

        while (!IsHere())
            yield return new WaitForSeconds(1f);
        Debug.Log("Я дома");

        doing = false;
    }


    private void SetDestination(Vector3 goal)
    {
        destination = new Vector3 (goal.x, transform.position.y, goal.z);
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        StopAllCoroutines();
        doing = false;
    }

    public void Continue()
    {

    }
}
