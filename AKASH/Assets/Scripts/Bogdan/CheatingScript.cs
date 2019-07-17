using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CheatingScript : MonoBehaviour
{

    [Header("Класс Ученика")]
   
    [Range(0,100)]
    
    public float stress;

    public bool Mudak;
    public bool Terpila;
    public bool Daun;

    static int SOMEBODYisCheating;

    public bool isUnderTeacherSupervision;

    

    void RS()
    {
        stress = Random.Range(0f, 100f);
    }

    void Start()
    {
        stress = Random.Range(0f, 15f);
        StartCoroutine(CheatingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        isUnderTeacherSupervision = !isUnderTeacherSupervision;
        Debug.Log(isUnderTeacherSupervision);
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

        if (SOMEBODYisCheating < 1)
        {
            if (isUnderTeacherSupervision)
            {
                if (Random.Range(1,100) <= 10)
                {

                }
            }
            else
            {

            }
        }
    /*    else
        {
            cheat = false;
        }
    */
        return cheat;
    }
}