﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class InputManager : MonoBehaviour
{

    [HideInInspector]
    public bool game;
    [HideInInspector]
    public bool cutScene;
    [HideInInspector]
    public bool disPlayer;
    private PlayerScript pScript;
    private CameraController cControl;
    private Menu menu;
    private Computer_Power[] comp;
    

    //private ParticleSystem.VelocityOverLifetimeModule vel;
    //private ParticleSystem.ShapeModule shape;
    private void Start()
    {
        game = true;
        cutScene = false;
        disPlayer = false;
        pScript = GameObject.FindObjectOfType<PlayerScript>();
        cControl = GameObject.FindObjectOfType<CameraController>();
        comp = GameObject.FindObjectsOfType<Computer_Power>();
        //menu = GameObject.FindObjectOfType<Menu>();
        //menu.SwitchMenu(false);
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
            if (Input.GetKey(KeyCode.Q))
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
            else if(pScript.doing)
            {
                pScript.doing = false;
                pScript.act = false;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(!pScript.act)
                    pScript.Shout();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!pScript.act && pScript.actReady && pScript.actTag == "Scholar")
                {
                    if (pScript.asked)
                        pScript.Answer(false);
                    else
                        pScript.Bull(true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!pScript.act && pScript.actReady && pScript.actTag == "Scholar")
                {
                    if (pScript.asked)
                        pScript.Answer(true);
                    else
                        pScript.Bull(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!pScript.act && pScript.actReady)
                    pScript.Execute();
            }

        }
        else
        {
        }

        //Меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (false)
            {
                //Записка
            }
            else
            {
                Time.timeScale = 0;
                game = false;
               //menu.SwitchMenu(true);
            }
        }



    }

    private void MenuInput()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (disPlayer)
                {
                    pScript.DisableControl(false);
                    
                }
            else
                {
                    Time.timeScale = 1;
                    game = true;
                    //menu.SwitchMenu(false);
                }

        }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
                game = true;
            }

            
    }
}
