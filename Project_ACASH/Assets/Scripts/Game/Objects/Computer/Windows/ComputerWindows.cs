using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;


namespace Computers
{
    public class ComputerWindows : MonoBehaviour
    {
        public A_Window CurrentWindow => _currentWindow;

        private Transform _windowsParent;
        private GameObject _currentWindowObject;
        private A_Window _currentWindow;


        public void Setup()
        {
            _windowsParent = transform;
        }



        public void Set(string windowName)
        {
            #region Load New Window
            var windowPrefab = ComputerWindowsManager.Instance.GetWindow(windowName);

            var windowObject = Instantiate(windowPrefab, windowPrefab.transform.position, windowPrefab.transform.rotation);
            windowObject.transform.SetParent(_windowsParent, false);
            #endregion

            #region Destroying Old Window
            _currentWindow.Stop();
            Destroy(_currentWindowObject);
            #endregion

            #region Remember New Window
            _currentWindowObject = windowObject;
            _currentWindow = windowObject.GetComponent<A_Window>();
            _currentWindow.Setup();
            #endregion
        }
    }
}