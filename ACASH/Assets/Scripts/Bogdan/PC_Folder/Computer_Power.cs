using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Power : MonoBehaviour
{
    public static GameObject activepc;
   // static PlayerScript pScript;
    static AntiCheatCamera acc;
    private void Start()
    {

        acc = GameObject.FindObjectOfType<AntiCheatCamera>();
    }

/*    public void SwitchPower()
    {
        ip.computer = !ip.computer;
        GameObject pc = transform.GetChild(0).gameObject;
        Debug.Log(pc);
        pc.SetActive(!pc.activeSelf);// !false/true
        PlayerCamera.get.LockCursor(!pc.activeSelf);//!true/false
    }
*/
    public static void SwitchPower(GameObject pc)
    {
        activepc = pc;
        activepc.SetActive(!activepc.activeSelf);// !false/true
        if (!activepc.activeSelf)
            acc.DisableAntiCheatCamers();
        // pScript.DisableControl(activepc.activeSelf);// true/false true - выключить контроль
        PlayerCamera.get.LockCursor(!activepc.activeSelf);//!true/false
    }
}
