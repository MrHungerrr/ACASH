using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Vkimow.Tools.Single;



namespace Computers
{
    public class ComputerManager : Singleton<ComputerManager>
    {
        public void Setup()
        {
            ComputerWindowsManager.Instance.Setup();
        }


        public void SetupSchool()
        {
            var computers = GameObject.FindObjectsOfType<A_Computer>();

            for (int i = 0; i < computers.Length; i++)
            {
                computers[i].Setup();
            }
        }
    }
}