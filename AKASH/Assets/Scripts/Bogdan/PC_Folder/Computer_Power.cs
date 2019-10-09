using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Power : MonoBehaviour
{

    static CameraController cc;
    static InputManager ip;
    public static GameObject activepc;
   // static PlayerScript pScript;
    static AntiCheatCamera acc;
    private void Start()
    {
        cc = GameObject.FindObjectOfType<CameraController>();
        ip = GameObject.FindObjectOfType<InputManager>();
        acc = GameObject.FindObjectOfType<AntiCheatCamera>();
    }

/*    public void SwitchPower()
    {
        ip.computer = !ip.computer;
        GameObject pc = transform.GetChild(0).gameObject;
        Debug.Log(pc);
        pc.SetActive(!pc.activeSelf);// !false/true
        cc.LockCursor(!pc.activeSelf);//!true/false
    }
*/
    public static void SwitchPower(GameObject pc)
    {
        activepc = pc;

        ip.computer = !ip.computer;
        activepc.SetActive(!activepc.activeSelf);// !false/true
        if (!activepc.activeSelf)
            acc.DisableAntiCheatCamers();
        // pScript.DisableControl(activepc.activeSelf);// true/false true - выключить контроль
        cc.LockCursor(!activepc.activeSelf);//!true/false
    }
}
