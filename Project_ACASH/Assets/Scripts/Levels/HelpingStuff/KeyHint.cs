using System.Collections;
using UnityEngine;
using PlayerOptions;

public class KeyHint: MonoBehaviour
{
    public void Set(GetP.actions action)
    {
        HUDManager.Instance.HintHUD(action);
    }


    public void Disable()
    {
        HUDManager.Instance.CloseHintHUD();
    }
}
