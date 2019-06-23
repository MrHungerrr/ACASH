using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour
{

    [HideInInspector]
    public bool game;
    [HideInInspector]
    public bool cutScene;
    [HideInInspector]
    public bool death;
    [HideInInspector]
    public bool disPlayer;
    private PlayerScript pScript;
    private CameraController cControl;
    

    //private ParticleSystem.VelocityOverLifetimeModule vel;
    //private ParticleSystem.ShapeModule shape;
    private void Start()
    {
        game = true;
        cutScene = false;
        death = false;
        disPlayer = false;
        pScript = GameObject.FindObjectOfType<PlayerScript>();
        cControl = GameObject.FindObjectOfType<CameraController>();
    }


    void Update()
    {
        if (game)
        {
            GameInput();
        }
        else
            MenuInput();
    }

 /*   void FixedUpdate()
    {
        if (game && !cutScene)
            GameFixInput();
    }


    private void GameFixInput()
    {
    }
*/


    private void GameInput()
    {
        if (!cutScene)
        {
            //Посмотреть ближе
            if (Input.GetMouseButton(1))
            {
                cControl.zoom = true;
                cControl.zooming = true;
            }
            else
            {
                cControl.zoom = false;
            }

            //Действие
            if (Input.GetKey(KeyCode.E))
            {
                pScript.doing = true;
            }
            else
            {
                pScript.doing = false;
                pScript.act = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            }
        }

        //Меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (true)
            {
                //Записка
            }
            else
            {
                Time.timeScale = 0;
                game = false;
            }
        }



    }

    private void MenuInput()
    {
        if (death)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                game = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(0);
                Time.timeScale = 1;
                game = true;
            }
        }
    }
}
