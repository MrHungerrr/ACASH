using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;


namespace Computers
{
    public class ComputerWindowsManager : MonoSingleton<ComputerWindowsManager>
    {

        private Dictionary<string, GameObject> _windows;

        [SerializeField] private GameObject[] _windowsArray;



        public void Setup()
        {
            for(int i = 0; i < _windowsArray.Length; i++)
            {
                _windows.Add(_windowsArray[i].name, _windowsArray[i]);
            }
        }


        public GameObject GetWindow(string name)
        {
            return _windows[name];
        }
    }
}