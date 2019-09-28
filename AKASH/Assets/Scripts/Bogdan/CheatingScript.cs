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

    public bool isUnderTeacherSupervision;


    private void Awake()
    {
        stress = Random.Range(0f, 15f);
    }

    void Start()
    {
        

        if (Mudak)
        { 

        }
        else if (Terpila)
        {
            StartCoroutine(CheatingCoroutineForTerpila());
        }
        else if(Daun)
        {
            StartCoroutine(CheatingCoroutineForDaun());
        }
    }


    //Мудаккод

    private void CheatingCoroutineForMudak()
    {
        if (CheatingMudak(stress))
        {
            CheatHelper.cheatingCount++;
            CheatHelper.cheatingCount--;
        }

    }

    public bool CheatingMudak(float stress)
    {
        if (CheatHelper.allowcheating)
        {
            if (isUnderTeacherSupervision)
            {
                if (Random.Range(1,100) <= 10)
                {
                    return ChanceMudak(stress);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return ChanceMudak(stress);
            }
        }
        return false;
    }

    bool ChanceMudak(float stress)
    {
        double y;
        float x = stress;
        y = 0.0001793981f * Mathf.Pow(x, 3) - 0.0382936508f * Mathf.Pow(x, 2) + 2.9857804233f * x + 4.8015873016f;
        if (Random.Range(1f, 100f) <= y)
        {
            return true; 
        }
        else
        {
            return false; 
        }
    }

    //Терпилакод

    IEnumerator CheatingCoroutineForTerpila()
    {
        float deltatime;
        WaitForSeconds wait;
        while (true)
        {
            deltatime = Random.Range(10f, 20f);
            wait = new WaitForSeconds(deltatime);
            // Debug.Log(deltatime + "Terpila");
            yield return wait;
            if (CheatingTerpila(stress))        
            {
                CheatHelper.cheatingCount++;
                wait = new WaitForSeconds(10f);
                yield return wait;
                CheatHelper.cheatingCount--;
            }
        }
    }

    public bool CheatingTerpila(float stress)
    {
        if (CheatHelper.allowcheating)
        {
            if (isUnderTeacherSupervision)
            {
                if (Random.Range(1, 100) <= 10)
                {
                    return ChanceTerpila(stress);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return ChanceTerpila(stress);
            }
        }

        return false;
    }

    bool ChanceTerpila(float stress)
    {
        double y;
        float x = stress;
        y = -0.0003334574 * Mathf.Pow(x, 3) + 0.0719518695 * Mathf.Pow(x, 2) - 5.0795904240 * x + 122.7986723721;
        if (Random.Range(1f, 100f) <= y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Даункод

    IEnumerator CheatingCoroutineForDaun()
    {
        float deltatime;
        WaitForSeconds wait;
        while (true)
        {
            deltatime = Random.Range(0f, 1f);
            wait = new WaitForSeconds(deltatime);
            //  Debug.Log(deltatime + "Daun");
            yield return wait;
            if (CheatingDaun())
            {
                CheatHelper.cheatingCount++;
                wait = new WaitForSeconds(10f);
                yield return wait;
                CheatHelper.cheatingCount--;
            }
        }
    }

    public bool CheatingDaun()
    {
        if (CheatHelper.allowcheating)
        {
            if (isUnderTeacherSupervision)
            {
                if (Random.Range(1, 100) <= 10)
                {

                    return ChanceDaun();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return ChanceDaun();
            }
        }
        return false;
    }

    bool ChanceDaun()
    {
        float y;
        y = 50 + Random.Range(-20f, 20f);
        if (Random.Range(1f, 100f) <= y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}