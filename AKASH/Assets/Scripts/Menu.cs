using UnityEngine;
using System.Collections;


public class Menu: MonoBehaviour
{

    private PostProcessManager PostManager;
    private const float dof_coef = 0.25f;
    private bool menuOn;
    private bool menuOff;


    void Start()
    {
        PostManager = GameObject.FindObjectOfType<PostProcessManager>();
    }




    void Update()
    {
        Switcher();
    }



    public void SwitchMenu(bool turnOn)
    {
        if (turnOn)
        {
            menuOn = true;
            menuOff = false;
        }
        else
        {
            menuOn = false;
            menuOff = true;
        }
    }


    private void Switcher()
    {

    }
    private IEnumerator MenuOn()
    {
        float dof = PostManager.GetDOF();

        while (dof > 0.1)
        {
            dof -= (dof/ 2)*dof_coef;
            PostManager.DOF(dof);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MenuOff()
    {
        float dof = PostManager.GetDOF();

        while (dof < 32)
        {
            dof += ((33-dof)/2)* dof_coef;
            PostManager.DOF(dof);
            yield return new WaitForEndOfFrame();
        }

    }

}
