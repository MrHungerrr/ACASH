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
    public Vector3 toilet;
    private Vector3 destonation;
    private Vector3 home;


    private void Awake()
    {
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

    }

    public void Doing(string key)
    {
        doing = true;
        keyWord = key;
        StartCoroutine(keyWord);
    }

    private bool IsHere()
    {
        if (transform.position == destonation)
            return true;
        else
            return false;
    }

    private IEnumerator Toilet_1()
    {
        destonation = toilet;
        agent.SetDestination(destonation);
        while(!IsHere())
            yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(5f);
        destonation = home;
        agent.SetDestination(destonation);
        while (!IsHere())
            yield return new WaitForSeconds(1f);
        doing = false;
    }
}
