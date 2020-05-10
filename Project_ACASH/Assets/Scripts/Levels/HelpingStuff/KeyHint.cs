using System.Collections;
using UnityEngine;
using PlayerOptions;

public class KeyHint: MonoBehaviour
{
    public void Set(GetP.actions action)
    {
        HUDManager.get.HintHUD(action);
    }


    public void Disable()
    {
        HUDManager.get.CloseHintHUD();
    }
}
