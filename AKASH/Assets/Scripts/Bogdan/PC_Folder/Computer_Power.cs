using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Power : MonoBehaviour
{
    CameraController cc;

    private void Start()
    {
        cc = GameObject.FindObjectOfType<CameraController>();
    }

    public void SwitchPower()
    {       
        var pc = this.gameObject.transform.GetChild(0).gameObject;
        pc.SetActive(!pc.activeSelf);// !false/true
        cc.LockCursor(!pc.activeSelf);//!true/false
    }
}
