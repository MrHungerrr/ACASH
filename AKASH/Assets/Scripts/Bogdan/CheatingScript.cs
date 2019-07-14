using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatingScript : MonoBehaviour
{
    float stress;

    void Start()
    {
        stress = Random.Range(0f, 15f);
        StartCoroutine(CheatingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheatingCoroutine()
    {
        float deltatime;
        WaitForSeconds wait;
        while (true)
        {
            deltatime = Random.Range(5f, 20f);
            wait = new WaitForSeconds(deltatime);
            Debug.Log(deltatime);
            yield return wait;
            Cheating(stress);
        }
    }

    public bool Cheating(float stress)
    {
       bool cheat = false;
       return cheat;
    }
}