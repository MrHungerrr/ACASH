using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Single;


namespace Computers
{
    public class ComputerManager : Singleton<ComputerManager>
    {
        public void Setup()
        {
            ComputerWindowsManager.Instance.Setup();
            ComputerCursor.Setup();
        }


        public void SetLevel()
        {
            var computers = GameObject.FindObjectsOfType<A_Computer>();

            for (int i = 0; i < computers.Length; i++)
            {
                computers[i].Setup();
            }
        }
    }
}