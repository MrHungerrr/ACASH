using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Computers;
using System;


[System.Serializable]
public struct ComputerButtonSelect
{
    public Image[] Images => _images;

    [SerializeField] private Image[] _images;

    public void Set(bool option)
    {
        int buf;

        if(option)
        {
            buf = 1;
        }
        else
        {
            buf = 0;
        }

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].color = ComputerData.COLORS[buf];
        }
    }
}
