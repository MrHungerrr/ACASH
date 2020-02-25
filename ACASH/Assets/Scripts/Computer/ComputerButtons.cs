using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class ComputerButtons : MonoBehaviour
{
    private A_Computer Comp;


    public void SetButtons(A_Computer c)
    {
        Comp = c;
    }



    private void PleaseWait(string command_type)
    {
        StartCoroutine(Waiting(command_type));
    }

    private void PleaseWait(string command_type, float seconds)
    {
        StartCoroutine(Waiting(command_type, seconds));
    }

    private IEnumerator Waiting(string command_type)
    {
        Debug.Log(command_type);
        yield return new WaitForSeconds(0.1f);
        Comp.Commands.Do(command_type);
    }

    private IEnumerator Waiting(string command_type, float seconds)
    {
        Debug.Log(command_type);
        yield return new WaitForSeconds(seconds);
        Comp.Commands.Do(command_type);
    }



    public void Do(string type)
    {
        switch (type)
        {
            //========================================================
            // Окна
            case "Student Stress":
                {
                    PleaseWait(type, 5);
                    break;
                }
            case "Overwatch":
                {
                    PleaseWait(type, 5);
                    break;
                }


            //========================================================
            // Кнопки

            case "Refresh":
                {
                    PleaseWait(type, 5);
                    break;
                }
            case "Log In Computer":
                {
                    PleaseWait(type, 3);
                    break;
                }
            case "Calculate":
                {
                    PleaseWait(type, 10);
                    break;
                }
            default:
                {
                    Comp.Commands.Do(type);
                    break;
                }
        }
    }
}
