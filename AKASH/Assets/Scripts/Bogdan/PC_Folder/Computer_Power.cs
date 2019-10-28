using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Power : MonoBehaviour
{

    private void Start()
    {
    }

    public void SwitchPower()
    {       
        var pc = this.gameObject.transform.GetChild(0).gameObject;
        pc.SetActive(!pc.activeSelf);// !false/true
        PlayerCamera.get.LockCursor(!pc.activeSelf);//!true/false
    }
}
